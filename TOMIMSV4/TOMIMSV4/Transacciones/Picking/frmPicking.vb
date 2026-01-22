Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports DevExpress.Mvvm.Native
Imports DevExpress.Utils.DragDrop
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen

Public Class frmPicking

    Public BePickingEnc As clsBeTrans_picking_enc

    Private DTOperadores As DataTable
    Private DTStockRes As New DataTable("StockRes")

    Public pListaPedidos As New List(Of Integer)

    Private BeListPickingDet As New List(Of clsBeTrans_picking_det)
    Private ReadOnly BeListPickingParam As New List(Of clsBeTrans_picking_det_parametros)
    Private BeListOp As New List(Of clsBeTrans_picking_op)
    Private pListBePickingUbic As New List(Of clsBeTrans_picking_ubic)
    Private pListObjSP As New List(Of clsBeVW_stock_res)
    Private lStockP As New List(Of clsBeStock_parametro)
    Dim ObjBePickingDet As New clsBeTrans_picking_det
    Public pObjBeB As New List(Of clsBeBodega)
    Public Delegate Sub ListarPicking()
    Public Delegate Sub ActualizarIdPickingPedido()

    Public Property InvokeListarPicking As ListarPicking
    Public Property InvokeActualizarIdPicking As ActualizarIdPickingPedido
    Public Property IdBodega As Integer = 0

    Private lPropietarios As New List(Of clsBePropietario_bodega)
    Private lBePresentacion As New List(Of clsBeProducto_Presentacion)
    Private lBePedidoDet As New List(Of clsBeTrans_pe_det)
    Public Property Llamado_Desde_Pedido As Boolean = False
    Public Property IsClosing As Boolean = False
    Public Property pIdPedidoEnc As DataGridViewCell
    Private IsGettingValoresGrid As Boolean = False

    Private BeBodega As clsBeBodega

    Public Delegate Sub ListarPedidos()
    Public Property InvokeListarPedidos As ListarPedidos

    Public Property pReferencia As String
    Private IsLoading As Boolean = False

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Private Property PickingAuto As Boolean
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public Property InvokeCargarPedido As Action(Of SqlConnection, SqlTransaction)
    Public Property InvokeCargarObjetoPedido As Action
    Sub New(ByVal pModo As TipoTrans, pPickingAuto As Boolean)
        InitializeComponent()
        Modo = pModo
        PickingAuto = pPickingAuto
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Lista_Productos_Dañados()

        Try

            Dim DT As New DataTable

            DT = clsLnTrans_picking_enc.Get_Dañados_Verificacion_Picking_ByPickingEnc(BePickingEnc.IdPickingEnc)

            If DT.Rows.Count > 0 Then

                grdProductosDañados.DataSource = DT

                GridView1.Columns("CantidadDañada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadDañada").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("CantidadDañada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadDañada").DisplayFormat.FormatString = "{0:n6}"

                lblRegistros.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                GridView1.BestFitColumns(True)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Set_Menu_Edicion()

        mnuGuardar.Enabled = False
        mnuActualizar.Enabled = BePickingEnc.Estado <> "Despachado"
        mnuEliminar.Enabled = BePickingEnc.Estado <> "Despachado"
        lnkQuitarPedido.Enabled = BePickingEnc.Estado <> "Despachado"
        cmdImprimir.Enabled = True
        mnuProcesar.Enabled = BePickingEnc.Estado <> "Despachado"
        mnuProcesarLinea.Enabled = BePickingEnc.Estado <> "Despachado"
        mnuPendientePicking.Enabled = (BePickingEnc.Estado <> "Nuevo" AndAlso BePickingEnc.Estado <> "Pendiente")
        mnuPendientePacking.Enabled = BePickingEnc.Requiere_Preparacion
        mnuActualizarPicking.Enabled = BePickingEnc.Estado <> "Despachado"
        cmdNoPickeado.Enabled = BePickingEnc.Estado <> "Despachado"
        cmdNoVerificado.Enabled = BePickingEnc.Estado <> "Despachado"
        cmdVerificarNuevamente.Enabled = BePickingEnc.Estado <> "Despachado"
        mnuDespachado.Enabled = BePickingEnc.Estado <> "Despachado"
        mnuVerificarPickeados.Enabled = BePickingEnc.Estado <> "Despachado"

    End Sub

    Public Sub SetDatataTable()

        DTStockRes.Columns.Add("Pedido", GetType(Int32)).ReadOnly = True
        DTStockRes.Columns.Add("Picking", GetType(Int32)).ReadOnly = True
        DTStockRes.Columns.Add("Código", GetType(String)).ReadOnly = True
        DTStockRes.Columns.Add("Producto", GetType(String)).ReadOnly = True
        DTStockRes.Columns.Add("Presentación", GetType(String)).ReadOnly = True
        DTStockRes.Columns.Add("Estado", GetType(String)).ReadOnly = True
        DTStockRes.Columns.Add("Unidad_Medida", GetType(String)).ReadOnly = True
        DTStockRes.Columns.Add("Propietario", GetType(String)).ReadOnly = True
        DTStockRes.Columns.Add("Ubicación", GetType(String)).ReadOnly = True
        DTStockRes.Columns.Add("Lote", GetType(String)).ReadOnly = True
        DTStockRes.Columns.Add("Lic Plate", GetType(String)).ReadOnly = True
        DTStockRes.Columns.Add("Fecha Vence", GetType(DateTime)).ReadOnly = True
        DTStockRes.Columns.Add("Factor", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Cant_Ped_Pres", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Cant_Ped_UmBas", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Cant_Pick_Pres", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Cant_Pick_UMBas", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Cant_Veri_Pres", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Cant_Veri_UMBas", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Cant_Desp_Pres", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Cant_Desp_UMBas", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Peso_Pick", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Peso_Veri", GetType(Double)).ReadOnly = True
        DTStockRes.Columns.Add("Encontrado", GetType(Boolean)).ReadOnly = True
        DTStockRes.Columns.Add("Acepto", GetType(Boolean)).ReadOnly = True
        DTStockRes.Columns.Add("Fecha Ingreso", GetType(DateTime)).ReadOnly = True
        DTStockRes.Columns.Add("IdStockRes", GetType(Int32)).ReadOnly = True
        DTStockRes.Columns.Add("Operador_Asignado", GetType(String)).ReadOnly = False
        DTStockRes.Columns.Add("Manufactura", GetType(Boolean)).ReadOnly = True
        DTStockRes.Columns.Add("Talla", GetType(String)).ReadOnly = True
        DTStockRes.Columns.Add("Color", GetType(String)).ReadOnly = True

    End Sub

    Private Function Valida_Datos() As Boolean

        Valida_Datos = False

        Try

            dgridDetallePicking.CommitEdit(DataGridViewDataErrorContexts.Commit)
            dgridDetallePicking.EndEdit()

            If cmbBodegas.ItemIndex = -1 Then
                xtpDatosPicking.SelectedTabPage = XtratabPageDato
                'Throw New Exception("Seleccione Bodega.")
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf cmbPropietario.ItemIndex = -1 Then
                xtpDatosPicking.SelectedTabPage = XtratabPageDato
                'Throw New Exception("Seleccione Propietario.")
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtIdUbicacion.Text.Trim) OrElse Val(txtIdUbicacion.Text = 0) Then
                xtpDatosPicking.SelectedTabPage = XtratabPageDato
                txtIdUbicacion.Focus()
                'Throw New Exception("Ingrese una ubicación de picking válida.")
                XtraMessageBox.Show("Ingrese una ubicación de picking válida.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf chkDetalleOperador.Checked AndAlso Not ValidaDetalleOperadores() Then '#CKFK 20210803 Cambié el AND por al ANDALSO porque daba error con los operadores
                dgridPedidos.Focus()
            ElseIf BeListPickingDet Is Nothing OrElse BeListPickingDet.Count > 0 = False Then
                xtpDatosPicking.SelectedTabPage = XtratabPagePedido
                XtraMessageBox.Show("Debe ingresar Pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Not chkDetalleOperador.Checked And Not ValidaDetalleOperadores_manual() Then
                xtpDatosPicking.SelectedTabPage = XtraTabPageUbicacionPicking
                XtraMessageBox.Show("Debe seleccionar un operador.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Not ValidaFilas() Then
                xtpDatosPicking.SelectedTabPage = XtratabPagePedido
            ElseIf Pedidos_Requieren_Muelle() AndAlso cmbMuelle.Text = "" Then
                xtpDatosPicking.SelectedTabPage = XtraTabPageUbicacionPicking
                XtraMessageBox.Show("Debe seleccionar un muelle porque algún pedido del picking lo requiere.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                ' validar que el codigo ingresado en la ubicacion de picking si exista 
                ' y que ese codigo pertenezca a una ubicacion de picking

                Valida_Datos = True
            End If


            'ValidaFilas()

        Catch ex As Exception

            Valida_Datos = False

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Function

    Private Function ValidaDetalleOperadores_manual() As Boolean

        ValidaDetalleOperadores_manual = True

        Try

            grdOperadorBodega.EndUpdate()

            If Not chkDetalleOperador.Checked Then

                'Dim IdOperadorBodega As Boolean
                Dim vOperadorSeleccionado As Integer

                For Each Op In BeListOp
                    vOperadorSeleccionado = vOperadorSeleccionado + 1
                Next

                If vOperadorSeleccionado = 0 Then
                    ValidaDetalleOperadores_manual = False
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Pedidos_Tienen_Picking_Asociado(ByVal IdPickingEnc As Integer) As Boolean

        Pedidos_Tienen_Picking_Asociado = True

        Try

            Dim IdPickingGen As Integer = 0

            For Each Ped In pListaPedidos

                If clsLnTrans_pe_enc.Tiene_Picking(Ped, IdPickingGen) Then
                    If IdPickingGen <> IdPickingEnc Then '#EJC20180627: Se está actualizando el mismo picking
                        Throw New Exception(String.Format("Este es un problema poco usual de concurrencia.
                        Un usuario ya ha generado el picking #: {0} para el pedido #: {1} por lo que no puede generarse otro picking para el mismo pedido.", IdPickingGen, Ped))
                    End If
                End If

            Next

            Pedidos_Tienen_Picking_Asociado = False

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function ValidaDetalleOperadores() As Boolean

        ValidaDetalleOperadores = True

        If dgridDetallePicking.Rows.Count > 0 Then
            For i As Integer = 0 To dgridDetallePicking.Rows.Count - 1
                If dgridDetallePicking.Rows(i).Cells("Producto").Value IsNot Nothing AndAlso dgridDetallePicking.Rows(i).Cells("OperadorBodega").Value Is Nothing Then
                    xtpDatosPicking.SelectedTabPage = XtratabPagePedido
                    XtraMessageBox.Show(String.Format("Debe seleccionar Operador en la fila ", i + 1),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ValidaDetalleOperadores = False
                End If
            Next
        End If

    End Function

    Private Sub lnkUbicacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacion.LinkClicked

        Try

            Dim Ubicacion As New frmBodegaUbicacion_List() With {.pUbicacionPicking = True, .Modo = frmBodegaUbicacion_List.pModo.Seleccion, .pIdBodega = cmbBodegas.EditValue}

            If OpcionesMenu IsNot Nothing Then
                Ubicacion.OpcionesMenu = OpcionesMenu
                Ubicacion.mmuActualizar.Enabled = OpcionesMenu.Leer
            End If

            Ubicacion.ShowDialog()

            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtIdUbicacion.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicacion.Text = Ubicacion.pObj.Descripcion
            End If

            Ubicacion.Close()
            Ubicacion.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub txtIdUbicacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdUbicacion.KeyPress

        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If

        If e.KeyChar = "." Then
            e.Handled = True
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If

        If e.KeyChar = Convert.ToChar(8) AndAlso txtIdUbicacion.Text.Length = 1 Then
            txtNombreUbicacion.Text = String.Empty
        End If

    End Sub

    Private Function Guardar_Picking() As Boolean

        Dim vContinuar As Boolean = False

        Guardar_Picking = False

        Try

            '#EJC20220303: Prevent data loss if the HH has already pickings on progres.
            Existen_Diferencias_Memoria_vs_BD(vContinuar)

            If Not vContinuar Then Exit Function

            Dim BeTareaHH As New clsBeTarea_hh

            If BePickingEnc.IsNew Then

                BePickingEnc.User_agr = AP.UsuarioAp.IdUsuario
                BePickingEnc.Fec_agr = Now

                BeTareaHH.IdPropietario = IMS.Get_IdPropietario_By_IdBodega(cmbBodegas.EditValue, cmbPropietario.EditValue)
                BeTareaHH.IdBodega = cmbBodegas.EditValue
                BeTareaHH.IdEstado = 1
                BeTareaHH.IdPrioridad = 1
                BeTareaHH.IdTipoTarea = clsDataContractDI.tTipoTarea.PIK
                '#EJC20220428:Se asignará en el guardar de la transacción.
                BeTareaHH.IdTransaccion = BePickingEnc.IdPickingEnc
                BeTareaHH.Tipo = 0
                BeTareaHH.FechaInicio = dtmHoraIhh.Value
                BeTareaHH.FechaFin = dtmHoraFhh.Value
                BeTareaHH.DiaCompleto = False
                BeTareaHH.Asunto = "Picking de Productos"
                BeTareaHH.Descripcion = ""
                BeTareaHH.CreaTarea = True
                BeTareaHH.IsNew = True

            End If

            BePickingEnc.IdBodega = cmbBodegas.EditValue
            BePickingEnc.IdPropietarioBodega = cmbPropietario.EditValue
            BePickingEnc.IdUbicacionPicking = CInt(txtIdUbicacion.Text.Trim)
            BePickingEnc.Fecha_picking = dtmFechaPicking.EditValue
            BePickingEnc.Hora_ini = dtmHoraIhh.Value
            BePickingEnc.Hora_fin = dtmHoraFhh.Value
            'If BePickingEnc.IsNew Then
            '    BePickingEnc.Estado = "Guardando"
            'Else
            BePickingEnc.Estado = lblEstado.Text
            'End If
            BePickingEnc.User_mod = AP.UsuarioAp.IdUsuario
            BePickingEnc.Fec_mod = Now
            BePickingEnc.Detalle_operador = chkDetalleOperador.Checked
            BePickingEnc.Activo = chkActivo.Checked
            BePickingEnc.verifica_auto = chkverifica_auto.Checked
            BePickingEnc.procesado_bof = chkProcesarDesdeBOF.Checked

            BePickingEnc.Requiere_Preparacion = (chkEmpaqueAGranel.Checked OrElse chkEmpaquePorTarima.Checked)
            BePickingEnc.Fotografia_Verificacion = chkFotografiaVerificacion.Checked

            If BePickingEnc.Requiere_Preparacion Then
                BePickingEnc.Estado_Preparacion = "Nuevo"
            Else
                BePickingEnc.Estado_Preparacion = "N/A"
            End If

            BePickingEnc.Fecha_Inicio_Preparacion = New Date(1900, 1, 1)
            BePickingEnc.Fecha_Fin_Preparacion = New Date(1900, 1, 1)

            If BePickingEnc.Requiere_Preparacion Then
                BePickingEnc.Tipo_Preparacion = IIf(chkEmpaquePorTarima.Checked, chkEmpaquePorTarima.Tag, chkEmpaqueAGranel.Tag)
            Else
                BePickingEnc.Tipo_Preparacion = ""
            End If

            BePickingEnc.Referencia = txtReferencia.Text
            BePickingEnc.Observacion = clsPublic.Quitar_Caracteres_No_Permitidos(txtObservacion.Text.Trim)
            BePickingEnc.IdBodegaMuelle = cmbMuelle.EditValue

            If rbAlto.Checked Then
                BePickingEnc.IdPrioridadPicking = 2
            ElseIf rbMedio.Checked Then
                BePickingEnc.IdPrioridadPicking = 1
            ElseIf rbBajo.Checked Then
                BePickingEnc.IdPrioridadPicking = 0
            End If

            If Not Pedidos_Tienen_Picking_Asociado(BePickingEnc.IdPickingEnc) Then

                If Not pListBePickingUbic.Count = 0 Then

                    Guardar_Picking = clsLnTrans_picking_enc.Guardar(BePickingEnc,
                                                                     BeTareaHH,
                                                                     BeListPickingDet,
                                                                     BeListPickingParam,
                                                                     BeListOp,
                                                                     pListBePickingUbic)


                    '#GT27022023: se guarda log del picking
                    If BePickingEnc.IsNew Then
                        '#MECR23102025: Se agrego bitacora para logs de picking
                        'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302271656: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " guardó el IdPickingEnc: " & BePickingEnc.IdPickingEnc)
                        clsLnLog_error_wms_pick.Agregar_Error("ADVERTENCIA_202302271656: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " guardó el IdPickingEnc: " & BePickingEnc.IdPickingEnc,
                                                              pIdEmpresa:=AP.IdEmpresa,
                                                              pIdBodega:=AP.IdBodega,
                                                              pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                              pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                              pIdPickingEnc:=BePickingEnc.IdPickingEnc)
                    Else
                        '#MECR23102025: Se agrego bitacora para logs de picking
                        'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302271656A: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " actualizó el IdPickingEnc: " & BePickingEnc.IdPickingEnc)
                        clsLnLog_error_wms_pick.Agregar_Error("ADVERTENCIA_202302271656A: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " actualizó el IdPickingEnc: " & BePickingEnc.IdPickingEnc,
                                                              pIdEmpresa:=AP.IdEmpresa,
                                                              pIdBodega:=AP.IdBodega,
                                                              pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                              pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                              pIdPickingEnc:=BePickingEnc.IdPickingEnc)
                    End If

                    Cargar_Datos()

                    If Not InvokeCargarObjetoPedido Is Nothing Then
                        InvokeCargarObjetoPedido.Invoke()
                    End If

                    If Not InvokeCargarPedido Is Nothing Then

                        Dim clsTrans As New clsTransaccion

                        Try

                            clsTrans.Begin_Transaction()
                            InvokeCargarPedido.Invoke(clsTrans.lConnection, clsTrans.lTransaction)
                            clsTrans.Commit_Transaction()

                        Catch ex As Exception
                            clsTrans.RollBack_Transaction()
                            'ejc, ambiente controlado, no disparar fuegos artificiales.
                        End Try

                    End If
                    '#EJC20220610:Duplicado el mensaje.
                    'XtraMessageBox.Show("Se guardó la tarea de picking", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Else
                    Throw New Exception("Al parecer el picking no tiene líneas, no se podrá guardar la transacción.")
                End If

            End If

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Private Sub Validar_Operadores()

        Try

            DsOrdenCompraRecepcionOperador.Clear()

            DTOperadores = clsLnOperador_bodega.Get_All_By_IdBodega_For_Tarea_DT(cmbBodegas.EditValue,
                                                                                 clsDataContractDI.tTipoTarea.PICK)

            If BePickingEnc IsNot Nothing AndAlso BePickingEnc.IdPickingEnc > 0 Then
                BeListOp = clsLnTrans_picking_op.Get_All_By_IdPickingEnc(BePickingEnc.IdPickingEnc).ToList
            Else
                BeListOp = New List(Of clsBeTrans_picking_op)
            End If

            Listar_Operadores()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    '#CKFK20220403 agregué la condición de si se llamó desde el botón porque cuando el picking se llama desde el pedido,
    'el boton de Ninguno no funciona
    Private Sub Listar_Operadores(Optional pSeleccionarTodos As Boolean = True,
                                  Optional PresionoBotonNinguno As Boolean = False)

        Dim SeleccionarTodos As Boolean = False

        Try

            grdOperadorBodega.BeginUpdate()

            If DTOperadores.Rows.Count > 0 Then

                DsOrdenCompraRecepcionOperador.Clear()

                '#EJC20210120: Si no hay operadores (Es un picking lanzado desde el pedido, marcar a todos para la Ola y el adiós)
                If BeListOp.Count = 0 AndAlso Llamado_Desde_Pedido And Not PresionoBotonNinguno Then
                    SeleccionarTodos = True
                End If

                '#EJC20210826:Se utiliza cuando se hace clic en el botón "desmarcar todos"
                If Not pSeleccionarTodos Then SeleccionarTodos = False

                '#GT08032022_1239: cargo la lista de operadores asignados
                If Modo = TipoTrans.Editar Then
                    '#GT08032022_0940: cargo operadores guardados, se puede hacer desde valida_operadores, pero el método se usa en otras
                    'partes y se me hizo complejo adecuarlo
                    If BePickingEnc IsNot Nothing AndAlso BePickingEnc.IdPickingEnc > 0 Then
                        BeListOp = clsLnTrans_picking_op.Get_All_By_IdPickingEnc(BePickingEnc.IdPickingEnc).ToList
                    Else
                        BeListOp = New List(Of clsBeTrans_picking_op)()
                    End If
                End If

                For i As Integer = 0 To DTOperadores.Rows.Count - 1

                    Dim lRow As DataRow = DsOrdenCompraRecepcionOperador.Data.NewRow
                    lRow.Item("IdOperadorBodega") = DTOperadores(i)(0)
                    lRow.Item("Operador") = DTOperadores(i)(1)
                    lRow.Item("Selección") = SeleccionarTodos
                    lRow.Item("colUsaHH") = CBool(DTOperadores(i)(2))

                    If Not SeleccionarTodos And PresionoBotonNinguno Then
                        lRow.Item("Selección") = False
                    Else

                        If SeleccionarTodos AndAlso Llamado_Desde_Pedido Then

                            If BeBodega.control_operador_ubicacion Then
                                Dim Obj As New clsBeTrans_picking_op() With {.IdOperadorBodega = DTOperadores(i)(0),
                            .User_agr = AP.UsuarioAp.IdUsuario,
                            .Fec_agr = Now,
                            .User_mod = AP.UsuarioAp.IdUsuario,
                            .Fec_mod = Now,
                            .IsNew = True}
                                BeListOp.Add(Obj)
                            Else
                                lRow.Item("Selección") = False
                            End If
                        End If

                    End If

                    If Modo = TipoTrans.Nuevo Then

                        If Not SeleccionarTodos And PresionoBotonNinguno Then
                            lRow.Item("Selección") = False
                        Else

                            If Not SeleccionarTodos Then
                                '#GT08032022_0844: si tiene control operador se llena la lista
                                If BeBodega.control_operador_ubicacion Then
                                    lRow.Item("Selección") = True
                                    Dim Obj As New clsBeTrans_picking_op() With {.IdOperadorBodega = DTOperadores(i)(0),
                                    .User_agr = AP.UsuarioAp.IdUsuario,
                                    .Fec_agr = Now,
                                    .User_mod = AP.UsuarioAp.IdUsuario,
                                    .Fec_mod = Now,
                                    .IsNew = True}
                                    BeListOp.Add(Obj)
                                Else
                                    lRow.Item("Selección") = False
                                End If
                            End If

                        End If
                    Else
                        lRow.Item("IdOperadorRec") = 0
                    End If

                    If Modo = TipoTrans.Editar Then

                        If Not SeleccionarTodos And PresionoBotonNinguno Then
                            lRow.Item("Selección") = False
                        Else

                            If SeleccionarTodos Then

                                lRow.Item("Selección") = True
                                Dim Obj As New clsBeTrans_picking_op() With {.IdOperadorBodega = DTOperadores(i)(0),
                                .User_agr = AP.UsuarioAp.IdUsuario,
                                .Fec_agr = Now,
                                .User_mod = AP.UsuarioAp.IdUsuario,
                                .Fec_mod = Now,
                                .IsNew = True}
                                BeListOp.Add(Obj)
                            Else
                                If BeListOp IsNot Nothing AndAlso BeListOp.Count > 0 Then
                                    For Each Obj As clsBeTrans_picking_op In BeListOp
                                        If Obj.IdOperadorBodega = CInt(DTOperadores(i)(0)) Then
                                            lRow.Item("Selección") = True
                                            lRow.Item("IdOperadorRec") = Obj.IdOperadorPicking
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If

                        End If

                    End If

                    DsOrdenCompraRecepcionOperador.Data.AddDataRow(lRow)
                Next

            Else
                XtraMessageBox.Show("ERROR_CONFIG_20220708: Al parecer no hay operadores configurados para tareas de picking, marque en mantenimiento de operadores la opción de Picking para los operadores que corresponden.",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            End If

            grdOperadorBodega.EndUpdate()
            grdOperadorBodega.ForceInitialize()

            Dim ritem As RepositoryItemCheckEdit = TryCast(DgridOperadorBodega.Columns("Selección").RealColumnEdit, RepositoryItemCheckEdit)

            If Not ritem Is Nothing Then
                RemoveHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged
                AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged
            End If


            If DgridOperadorBodega.RowCount > 0 Then
                lblRegs1.Caption = String.Format("Registros: {0}", DgridOperadorBodega.RowCount)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = DgridOperadorBodega.GetFocusedRow

                If Not Dr Is Nothing Then

                    Dim lIndex As Integer = -1
                    Dim d As Integer = Dr.Item("IdOperadorBodega")

                    lIndex = BeListOp.FindIndex(Function(b) b.IdOperadorBodega = d)

                    If lIndex > -1 Then

                        If Not ritem.Checked Then '#EJC20210826: Lo está eliminando.
                            If BeListOp(lIndex).IdOperadorPicking > 0 AndAlso BeListOp(lIndex).IdPickingEnc > 0 Then
                                clsLnTrans_picking_op.Delete(BeListOp(lIndex).IdOperadorPicking)
                                DsOrdenCompraRecepcionOperador.Clear()
                                Listar_Operadores()
                            End If
                        Else
                            '#EJC20220616:Está asignando un operador, cuando el picking es nuevo y fue lanzado desde el pedido.
                            'Al que me borre esto lo despido.
                            Dim Obj As New clsBeTrans_picking_op() With {.IdOperadorBodega = Dr.Item("IdOperadorBodega"),
                            .User_agr = AP.UsuarioAp.IdUsuario,
                            .Fec_agr = Now,
                            .User_mod = AP.UsuarioAp.IdUsuario,
                            .Fec_mod = Now,
                            .IsNew = True}
                            BeListOp.Add(Obj)
                        End If

                    Else
                        Dim Obj As New clsBeTrans_picking_op() With {.IdOperadorBodega = Dr.Item("IdOperadorBodega"),
                            .User_agr = AP.UsuarioAp.IdUsuario,
                            .Fec_agr = Now,
                            .User_mod = AP.UsuarioAp.IdUsuario,
                            .Fec_mod = Now,
                            .IsNew = True}
                        BeListOp.Add(Obj)
                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private lListasTramoZonaPickingPorBodega As New List(Of clsBeZona_picking_tramo)

    Private Sub Get_Tramos_Zona_Picking_Por_Bodega()

        Try

            'Para la bodega
            lListasTramoZonaPickingPorBodega = clsLnZona_picking_tramo.Get_All_By_IdEmpresa_And_IdBodega(AP.IdEmpresa, cmbBodegas.EditValue)

            Dim vDiaSemana As Integer = Now.DayOfWeek

            'Obtener las configuraciones de zonas de picking para los operadores para el día actual.
            vListasOperadorTramoZonaPickingPorBodega = clsLnOperador_zona_picking_tramo.Get_All_By_IdEmpresa_And_IdBodega(AP.IdEmpresa,
                                                                                                                          cmbBodegas.EditValue,
                                                                                                                          vDiaSemana)


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Public Sub SetProducto(ByVal pBeTransPeDet As clsBeTrans_pe_det,
                           ByVal PedidoEnc As clsBeTrans_pe_enc)

        Dim vIndicador As Double = 0

        Try

            If PedidoEnc Is Nothing Then Exit Sub

            vIndicador = 1

            If BeListPickingDet IsNot Nothing AndAlso BeListPickingDet.Count > 0 Then

                Dim lIndexE As Integer = -1

                lIndexE = BeListPickingDet.FindIndex(Function(b) b.IdPedidoDet = pBeTransPeDet.IdPedidoDet)

                If lIndexE > -1 Then
                    'Throw New Exception(String.Format("El Producto {0} ya fue agregado.", pBeTransPeDet.NombreProducto))
                    Exit Sub
                End If

                ObjBePickingDet = New clsBeTrans_picking_det()
                ObjBePickingDet.IdPickingDet = BeListPickingDet.Max(Function(b) b.IdPickingDet) + 1

            Else

                If ObjBePickingDet Is Nothing Then
                    ObjBePickingDet = New clsBeTrans_picking_det()
                End If

                ObjBePickingDet.IdPickingDet = 1

                vIndicador = 2

            End If

            Dim i As Integer = dgridDetallePicking.Rows.Add()

            dgridDetallePicking.Rows(i).Cells("Codigo").Value = pBeTransPeDet.Producto.Codigo
            dgridDetallePicking.Rows(i).Cells("Producto").Value = pBeTransPeDet.Producto.Nombre
            dgridDetallePicking.Rows(i).Cells("Presentacion").Value = pBeTransPeDet.Nom_presentacion
            dgridDetallePicking.Rows(i).Cells("UnidadMedida").Value = pBeTransPeDet.Nom_unid_med
            dgridDetallePicking.Rows(i).Cells("Estado").Value = pBeTransPeDet.Nom_estado
            dgridDetallePicking.Rows(i).Cells("Cantidad").Value = pBeTransPeDet.Cantidad
            dgridDetallePicking.Rows(i).Cells("IdPedidoDet").Value = pBeTransPeDet.IdPedidoDet
            dgridDetallePicking.Rows(i).Cells("IdPedidoEnc").Value = pBeTransPeDet.IdPedidoEnc
            dgridDetallePicking.Rows(i).Cells("ClienteDias").Value = 0

            vIndicador = 3

            Cargar_Operador(i)

            vIndicador = 4

            dgridDetallePicking.CommitEdit(DataGridViewDataErrorContexts.Commit)
            dgridDetallePicking.EndEdit()

            vIndicador = 5

            ObjBePickingDet.IdPedidoEnc = pBeTransPeDet.IdPedidoEnc
            ObjBePickingDet.IdPedidoDet = pBeTransPeDet.IdPedidoDet
            ObjBePickingDet.Cantidad = pBeTransPeDet.Cantidad
            ObjBePickingDet.User_agr = AP.UsuarioAp.IdUsuario
            ObjBePickingDet.Fec_agr = Now
            ObjBePickingDet.User_mod = AP.UsuarioAp.IdUsuario
            ObjBePickingDet.Fec_mod = Now
            ObjBePickingDet.Activo = True
            ObjBePickingDet.IsNew = True

            vIndicador = 6

            Dim ObjProducto As New clsBeProducto

            ObjBePickingDet.Presentacion = New clsBeProducto_Presentacion
            ObjBePickingDet.UnidadMedida = New clsBeUnidad_medida
            ObjBePickingDet.ProductoEstado = New clsBeProducto_estado

            If Not (pBeTransPeDet.Producto Is Nothing) Then
                If pBeTransPeDet.IdProductoBodega <> 0 Then
                    ObjProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pBeTransPeDet.IdProductoBodega)
                    If Not ObjProducto Is Nothing Then
                        pBeTransPeDet.Producto = ObjProducto
                    End If
                End If
            Else
                '#EJC20211220: En caso de que por alguna razón sea nothing... buscar
                If pBeTransPeDet.IdProductoBodega <> 0 Then
                    ObjProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pBeTransPeDet.IdProductoBodega)
                    If Not ObjProducto Is Nothing Then
                        pBeTransPeDet.Producto = ObjProducto
                    End If
                End If
                If pBeTransPeDet.Producto Is Nothing Then
                    Throw New Exception("#20211218: No Se encontró el objeto de producto asociado a la línea de detalle para la línea del pedido: " & pBeTransPeDet.No_linea & " y Código de producto: " & pBeTransPeDet.Codigo_Producto)
                End If
            End If

            vIndicador = 7

            If pBeTransPeDet.Producto.IdProducto <> 0 Then

                ObjProducto = clsLnProducto.Get_Single_By_IdProducto(pBeTransPeDet.Producto.IdProducto)

                vIndicador = 8.4

                If Not ObjProducto Is Nothing Then

                    ObjBePickingDet.Producto = New clsBeProducto()
                    ObjBePickingDet.Producto.Codigo = ObjProducto.Codigo
                    ObjBePickingDet.Producto.Nombre = pBeTransPeDet.Producto.Nombre
                    ObjBePickingDet.Codigo = ObjProducto.Codigo
                    ObjBePickingDet.NombreProducto = pBeTransPeDet.Producto.Nombre
                    ObjBePickingDet.Presentacion.IdPresentacion = pBeTransPeDet.IdPresentacion
                    ObjBePickingDet.Presentacion.Nombre = pBeTransPeDet.Nom_presentacion
                    ObjBePickingDet.UnidadMedida.IdUnidadMedida = pBeTransPeDet.IdUnidadMedidaBasica
                    ObjBePickingDet.UnidadMedida.Nombre = pBeTransPeDet.Nom_unid_med
                    ObjBePickingDet.ProductoEstado.IdEstado = pBeTransPeDet.IdEstado
                    ObjBePickingDet.ProductoEstado.Nombre = pBeTransPeDet.Nom_estado
                    ObjBePickingDet.Cantidad = pBeTransPeDet.Cantidad
                    ObjBePickingDet.Cantidad_recibida = 0
                    ObjBePickingDet.Cliente_dias = 0

                    BeListPickingDet.Add(ObjBePickingDet)

                    Dim TiempoCliente As New clsBeCliente_tiempos
                    TiempoCliente = clsLnCliente_tiempos.GetSingle(PedidoEnc.IdCliente, ObjProducto.IdFamilia, ObjProducto.IdClasificacion)

                    If Not TiempoCliente Is Nothing Then
                        dgridDetallePicking.Rows(i).Cells("ClienteDias").Value = IIf(PedidoEnc.Local, TiempoCliente.Dias_Local, TiempoCliente.Dias_Exterior)
                    Else
                        dgridDetallePicking.Rows(i).Cells("ClienteDias").Value = -1
                    End If

                    If pBeTransPeDet.ListaStockRes IsNot Nothing AndAlso pBeTransPeDet.ListaStockRes.Count > 0 Then

                        Dim BePresentacion As New clsBeProducto_Presentacion

                        vIndicador = 8.3

                        Dim BeUbicacion As New clsBeBodega_ubicacion

                        '#EJC20220603: Se utiliza para filtrar el resultado de las zonas de picking identificadas acorde a la ubicación del producto.
                        Dim vSingularidadTramoUbicacionEnZP As New List(Of clsBeZona_picking_tramo)
                        Dim SingleBeZonaPIckingTramoOP As New clsBeOperador_zona_picking_tramo

                        For Each bo As clsBeStock_res In pBeTransPeDet.ListaStockRes

                            Dim ObjU As New clsBeTrans_picking_ubic()

                            With ObjU

                                If Val(bo.Cantidad) = 0 Then

                                    Dim vMensaje As String = String.Format("Una línea del picking es inconsistente, 
                                        éste es un error poco usual y estamos trabajando para resolverlo. 
                                        El IdStock: {0} reporta una cantidad: {1} aunque ésto puede no afectar
                                        se considera una línea no válida para el sistema, 
                                        de forma preventiva se ha restringido el picking de este documento,
                                        lo sentimos, EJC.", bo.IdStock, bo.Cantidad)

                                    '#EJC20180619: Punto de control agregado para evitar que se generen despachos sobre cantidad 0.                            
                                    Throw New Exception(vMensaje)

                                Else

                                    .IdPedidoEnc = bo.IdPedido
                                    .IdPedidoDet = bo.IdPedidoDet
                                    .IdStockRes = bo.IdStockRes
                                    .IdPickingDet = ObjBePickingDet.IdPickingDet
                                    .IdStock = bo.IdStock
                                    .IdPropietarioBodega = bo.IdPropietarioBodega
                                    .IdProductoBodega = bo.IdProductoBodega
                                    .IdProductoEstado = bo.IdProductoEstado
                                    .IdPresentacion = bo.IdPresentacion
                                    .IdUnidadMedida = bo.IdUnidadMedida
                                    .IdUbicacionAnterior = Val(bo.Ubicacion_ant)
                                    .IdRecepcion = bo.IdRecepcion
                                    .IdUbicacion = bo.IdUbicacion
                                    .Lote = bo.Lote
                                    .Fecha_Vence = bo.Fecha_vence
                                    .Serial = bo.Serial
                                    .Lic_plate = bo.Lic_plate
                                    .Peso_solicitado = bo.Peso
                                    .IdBodega = cmbBodegas.EditValue

                                    ''#EJC20180926: Insertar cantidad de presentación en picking_ubic 
                                    If bo.IdPresentacion = 0 Then
                                        .Cantidad_Solicitada = bo.Cantidad
                                    Else
                                        BePresentacion = clsLnProducto_presentacion.GetSingle(bo.IdPresentacion)
                                        If Not BePresentacion Is Nothing Then
                                            If BePresentacion.Factor = 0 Then BePresentacion.Factor = 1
                                            '#EJC20191122: Quité redondeo para obtener exactitud en la conversión.
                                            '.Cantidad_Solicitada = Math.Round(bo.Cantidad / BePresentacion.Factor, 6)
                                            .Cantidad_Solicitada = bo.Cantidad / BePresentacion.Factor
                                        Else
                                            Throw New Exception("No se obtuvo el detalle de la presentación para el código de presentación: " & bo.IdPresentacion)
                                        End If
                                    End If

                                    .Cantidad_Recibida = 0.0
                                    .Fecha_real_vence = bo.Fecha_vence
                                    .User_agr = AP.UsuarioAp.IdUsuario
                                    .Fec_agr = Now
                                    .User_mod = AP.UsuarioAp.IdUsuario
                                    .Fec_mod = Now
                                    .Activo = True
                                    .IsNew = True
                                    .IdProductoTallaColor = bo.IdProductoTallaColor

                                End If

                            End With

                            vIndicador = 8.1

                            pListBePickingUbic.Add(ObjU)

                            If ObjProducto.IsNew = False AndAlso ObjProducto.IdProducto > 0 Then
                                Set_Stock_Parametro(bo.IdStock)
                            End If

                            vIndicador = 8.2

                        Next

                    End If

                End If

                vIndicador = 8

            End If

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message & " Indicador: " & vIndicador)
            'clsLnLog_error_wms.Agregar_Error(vMsgError & " Indicador: " & vIndicador)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Private Sub Cargar_Operador(ByVal pIndex As Integer,
                                ByVal pConnection As SqlConnection,
                                ByVal pTransaction As SqlTransaction,
                                Optional ByVal pIdOperadorBodega As Integer = 0)

        Try

            Dim DT As DataTable = clsLnOperador_bodega.Get_All_By_IdBodega_DT(cmbBodegas.EditValue,
                                                                              pConnection,
                                                                              pTransaction)

            If Not DT Is Nothing Then

                Dim DgCombo As New DataGridViewComboBoxCell()
                DgCombo = TryCast(dgridDetallePicking.Rows(pIndex).Cells("OperadorBodega"), DataGridViewComboBoxCell)

                DgCombo.DataSource = DT
                DgCombo.ValueMember = "IdOperadorBodega"
                DgCombo.DisplayMember = "Nombres"

                If pIdOperadorBodega > 0 Then
                    DgCombo.Value = pIdOperadorBodega
                End If

            End If

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Private Sub grdListaPickingD_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgridDetallePicking.CellValueChanged

        Try

            If dgridDetallePicking.Rows.Count > 0 AndAlso dgridDetallePicking.CurrentRow IsNot Nothing Then

                dgridDetallePicking.CommitEdit(DataGridViewDataErrorContexts.Commit)
                dgridDetallePicking.EndEdit()

                If BeListPickingDet IsNot Nothing AndAlso BeListPickingDet.Count > 0 Then
                    Dim lIndex As Integer = -1
                    lIndex = BeListPickingDet.FindIndex(Function(b) b.IdPedidoDet = dgridDetallePicking.CurrentRow.Cells("IdPedidoDet").Value)

                    If lIndex > -1 Then

                        If dgridDetallePicking.Columns(e.ColumnIndex).Name() = "ClienteDias" _
                            AndAlso dgridDetallePicking.CurrentRow.Cells("ClienteDias").Value IsNot DBNull.Value _
                            AndAlso dgridDetallePicking.CurrentRow.Cells("ClienteDias").Value > -1 _
                            AndAlso dgridDetallePicking.CurrentRow.Cells("ClienteDias").Value IsNot Nothing Then
                            BeListPickingDet(lIndex).Cliente_dias = dgridDetallePicking.CurrentRow.Cells("ClienteDias").Value
                        ElseIf dgridDetallePicking.Columns(e.ColumnIndex).Name() = "OperadorBodega" AndAlso dgridDetallePicking.CurrentRow.Cells("OperadorBodega").Value IsNot DBNull.Value AndAlso dgridDetallePicking.CurrentRow.Cells("OperadorBodega").Value IsNot Nothing AndAlso dgridDetallePicking.CurrentRow.Cells("OperadorBodega").Value > 0 Then
                            BeListPickingDet(lIndex).IdOperadorBodega = dgridDetallePicking.CurrentRow.Cells("OperadorBodega").Value
                        End If

                        BeListPickingDet(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                        BeListPickingDet(lIndex).Fec_mod = Now

                    End If

                End If

                dgridDetallePicking.CommitEdit(DataGridViewDataErrorContexts.Commit)
                dgridDetallePicking.EndEdit()

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub
    Private Function ValidaFilas() As Boolean

        ValidaFilas = True

        Try

            For i As Integer = 0 To dgridDetallePicking.Rows.Count - 1
                If dgridDetallePicking.Rows(i).Cells("Producto").Value IsNot DBNull.Value AndAlso dgridDetallePicking.Rows(i).Cells("Producto").Value IsNot Nothing Then
                    If dgridDetallePicking.Rows(i).Cells("ClienteDias").Value Is DBNull.Value OrElse dgridDetallePicking.Rows(i).Cells("ClienteDias").Value Is Nothing Then
                        xtpDatosPicking.SelectedTabPage = XtratabPagePedido
                        'Throw New Exception(String.Format("Ingrese días Cliente en fila {0}", i + 1))

                        XtraMessageBox.Show(String.Format("Ingrese días Cliente en fila {0}", i + 1),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        ValidaFilas = False
                    End If
                End If
            Next

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Private Sub Set_Stock_Parametro(ByVal pIdStock As Integer)

        Try
            '#CKFK 20180208 09:00AM se puso esto en comentario porque los parámetros deben guardarse en la lista de parámetros BeListPickingParam
            'Dim lParametros As New List(Of clsBeTrans_picking_det_parametros)
            Dim BeParametros As New clsBeTrans_picking_det_parametros

            lStockP = clsLnStock_parametro.Get_All_By_IdStock(pIdStock)

            For Each Obj As clsBeStock_parametro In lStockP

                BeParametros = New clsBeTrans_picking_det_parametros
                'BeParametros.IdParametroPicking = clsLnTrans_picking_det_parametros.MaxID()
                BeParametros.IdPickingDet = ObjBePickingDet.IdPickingDet
                BeParametros.IdProductoParametro = Obj.IdProductoParametro
                BeParametros.Valor_texto = Obj.Valor_texto
                BeParametros.Valor_numerico = Obj.Valor_numerico
                BeParametros.Valor_logico = Obj.Valor_logico
                BeParametros.Valor_fecha = Obj.Valor_fecha
                BeParametros.Fec_agr = Now
                BeParametros.User_agr = AP.UsuarioAp.Nombres
                BeParametros.IsNew = True
                BeListPickingParam.Add(BeParametros)

            Next

            '#CKFK 20180208 09:00AM se puso esto en comentario porque los parámetros no se pueden guardar hasta que esté creado el picking
            'clsLnTrans_picking_det_parametros.Guardar(lParametros)

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Sub

    Private Sub GrdOperadorBobega_RowStyle(sender As Object, e As RowStyleEventArgs) Handles DgridOperadorBodega.RowStyle

        Try

            DgridOperadorBodega.OptionsBehavior.Editable = True
            DgridOperadorBodega.OptionsSelection.EnableAppearanceFocusedCell = True

            DgridOperadorBodega.FocusRectStyle = DrawFocusRectStyle.RowFocus

            DgridOperadorBodega.OptionsSelection.EnableAppearanceFocusedRow = True
            DgridOperadorBodega.OptionsSelection.EnableAppearanceHideSelection = True
            DgridOperadorBodega.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            DgridOperadorBodega.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            DgridOperadorBodega.Appearance.FocusedRow.ForeColor = Color.White
            DgridOperadorBodega.Appearance.SelectedRow.ForeColor = Color.White

            DgridOperadorBodega.Appearance.SelectedRow.Options.UseBackColor = True
            DgridOperadorBodega.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Mostrar_Datos_Encabezado(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            If BePickingEnc IsNot Nothing Then

                lblC.Text = BePickingEnc.IdPickingEnc
                cmbBodegas.EditValue = BePickingEnc.IdBodega
                cmbPropietario.EditValue = BePickingEnc.IdPropietarioBodega

                txtIdUbicacion.Text = BePickingEnc.IdUbicacionPicking
                txtNombreUbicacion.Text = BePickingEnc.NombreUbicacionPicking

                dtmFechaPicking.EditValue = BePickingEnc.Fecha_picking

                dtmHoraIhh.Value = BePickingEnc.Hora_ini
                dtmHoraFhh.Value = BePickingEnc.Hora_fin
                lblEstado.Text = BePickingEnc.Estado

                Dim BeUsuarioAgr As New clsBeUsuario
                BeUsuarioAgr.IdUsuario = BePickingEnc.User_agr
                clsLnUsuario.GetSingle(BeUsuarioAgr, lConnection, lTransaction)
                User_agrTextEdit1.Text = BePickingEnc.User_agr & " - " & BeUsuarioAgr.Nombres & " " & BeUsuarioAgr.Apellidos

                Dim BeUsuarioMod As New clsBeUsuario
                BeUsuarioMod.IdUsuario = BePickingEnc.User_agr
                clsLnUsuario.GetSingle(BeUsuarioMod, lConnection, lTransaction)
                User_modTextEdit1.Text = BePickingEnc.User_mod & " - " & BeUsuarioMod.Nombres & " " & BeUsuarioMod.Apellidos

                Fec_agrDateEdit1.Text = BePickingEnc.Fec_agr
                Fec_modDateEdit1.Text = BePickingEnc.Fec_mod

                chkDetalleOperador.Checked = BePickingEnc.Detalle_operador
                chkActivo.Checked = BePickingEnc.Activo
                chkverifica_auto.Checked = BePickingEnc.verifica_auto
                chkFotografiaVerificacion.Checked = BePickingEnc.Fotografia_Verificacion

                chkEmpaqueAGranel.Checked = IIf(BePickingEnc.Tipo_Preparacion = "Granel", True, False)
                chkEmpaquePorTarima.Checked = IIf(BePickingEnc.Tipo_Preparacion = "Tarima", True, False)

                txtReferencia.Text = BePickingEnc.Referencia

                chkProcesarDesdeBOF.Checked = BePickingEnc.procesado_bof

                cmbMuelle.EditValue = BePickingEnc.IdBodegaMuelle

                Select Case BePickingEnc.IdPrioridadPicking
                    Case 0
                        rbBajo.Checked = True
                    Case 1
                        rbMedio.Checked = True
                    Case 2
                        rbAlto.Checked = True
                End Select

                txtObservacion.Text = BePickingEnc.Observacion

            End If

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Sub

    Private Sub Mostrar_Pedidos_Asociados(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            If BePickingEnc IsNot Nothing Then

                Dim ListaPedidosPicking As List(Of Integer) = (From picking In BePickingEnc.ListaPickingDet Select picking.IdPedidoEnc).Distinct.ToList
                pListaPedidos = ListaPedidosPicking

                Dim BePickingDet As clsBeTrans_picking_det
                Dim BePedidoEnc As New clsBeTrans_pe_enc
                Dim ie As Integer = 0

                lPedidosPicking = New List(Of clsBeTrans_pe_enc)

                For Each IdPedidoEnc As Integer In ListaPedidosPicking

                    BePickingDet = BePickingEnc.ListaPickingDet.ToList.Find(Function(b) b.IdPedidoEnc = IdPedidoEnc)
                    '#CKFK20250227 Cambié esta función de Get_Single_Without_Picking
                    BePedidoEnc = clsLnTrans_pe_enc.Get_Single_Sin_Picking(IdPedidoEnc, lConnection, lTransaction)

                    ie = dgridPedidos.Rows.Add()

                    dgridPedidos.Rows(ie).Cells("IdPedido").Value = BePickingDet.IdPedidoEnc
                    dgridPedidos.Rows(ie).Cells("Referencia").Value = BePickingDet.Referencia
                    dgridPedidos.Rows(ie).Cells("Bodega").Value = BePickingDet.Bodega
                    dgridPedidos.Rows(ie).Cells("Cliente").Value = BePickingDet.Cliente
                    dgridPedidos.Rows(ie).Cells("Propietario").Value = BePickingDet.Propietario
                    dgridPedidos.Rows(ie).Cells("FechaPedido").Value = BePickingDet.FechaPedido
                    dgridPedidos.Rows(ie).Cells("EstadoP").Value = BePedidoEnc.Estado
                    lPedidosPicking.Add(BePedidoEnc)

                Next

                dgridPedidos.CommitEdit(DataGridViewDataErrorContexts.Commit)
                dgridPedidos.EndEdit()

                BeListPickingDet = BePickingEnc.ListaPickingDet.ToList()

            End If

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Private Sub Get_Stock_Res(ByVal pListPickingUbic As List(Of clsBeTrans_picking_ubic),
                              ByVal DespachoRealizado As Boolean)

        Try

            If pListPickingUbic.Count > 0 Then

                Dim vCantidadReservadaUMBas As Double = 0
                Dim vCantidadReservadaPres As Double = 0
                Dim vPesoReservado As Double = 0
                Dim vCantidadRecPres As Double = 0
                Dim vCantidadVerPres As Double = 0
                Dim vCantidadDespPres As Double = 0
                Dim BePresentacion As New clsBeProducto_Presentacion
                Dim BePedidoDet As New clsBeTrans_pe_det
                Dim BePropietario As New clsBePropietario_bodega
                Dim Idx As Integer = 0

                '#EJC20220410: Si se limpia aquí (esta función se llama dentro de un ciclo) se limpian los registros anteriores.
                'DTStockRes.Rows.Clear()

                For Each Obj In pListPickingUbic

                    Idx = lBePedidoDet.FindIndex(Function(x) x.IdPedidoDet = Obj.IdPedidoDet)

                    '#EJC20191121: Optimización por memoria.
                    If Idx = -1 Then
                        BePedidoDet.IdPedidoDet = Obj.IdPedidoDet
                        clsLnTrans_pe_det.GetSingle(BePedidoDet)
                        lBePedidoDet.Add(BePedidoDet)
                    Else
                        BePedidoDet = lBePedidoDet(Idx)
                    End If

                    Idx = lPropietarios.FindIndex(Function(x) x.IdPropietario = Obj.IdPropietarioBodega)

                    If Idx = -1 Then
                        BePropietario = clsLnPropietario_bodega.Get_Single_With_Propietario_By_IdPropietarioBodega(Obj.IdPropietarioBodega)
                        lPropietarios.Add(BePropietario)
                    Else
                        BePropietario = lPropietarios(Idx)
                    End If

                    '#CKFK20221121 Puse esto en comentario OrElse (BePedidoDet.IdPresentacion = 0) para tomar como referencia la unidad de medida de la reserva
                    If (Obj.IdPresentacion = 0) Then
                        vCantidadReservadaUMBas = Obj.Cantidad_Solicitada
                        vCantidadReservadaPres = 0
                        vCantidadRecPres = 0
                        vCantidadVerPres = 0
                        vCantidadDespPres = 0
                    Else

                        '#EJC20191121_1456: Optimización por memoria.
                        Idx = lBePresentacion.FindIndex(Function(x) x.IdPresentacion = Obj.IdPresentacion)

                        If Idx = -1 Then
                            BePresentacion.IdPresentacion = Obj.IdPresentacion
                            clsLnProducto_presentacion.GetSingle(BePresentacion)
                            lBePresentacion.Add(BePresentacion)
                        Else
                            BePresentacion = lBePresentacion(Idx)
                        End If

                        '#CM_20190206: Se cambio el orden en el que se realizaban las operaciones porque la cantidad
                        'cuando tiene presentación el picking la devuelve en presentación.
                        If Not BePresentacion Is Nothing Then

                            If (Modo = TipoTrans.Nuevo) Then

                                '#CKFK20221121 Puse esto en comentario AndAlso (BePedidoDet.IdPresentacion <> 0) para tomar como referencia la unidad de medida de la reserva
                                If (Obj.IdPresentacion <> 0) Then
                                    vCantidadReservadaPres = Obj.Cantidad_Solicitada
                                    vCantidadReservadaUMBas = Math.Round(Obj.Cantidad_Solicitada * BePresentacion.Factor, 6)
                                    vCantidadRecPres = Obj.Cantidad_Recibida
                                    Obj.Cantidad_Recibida = Math.Round(Obj.Cantidad_Recibida * BePresentacion.Factor, 6)
                                    vCantidadVerPres = Obj.Cantidad_Verificada
                                    Obj.Cantidad_Verificada = Math.Round(Obj.Cantidad_Verificada * BePresentacion.Factor, 6)
                                    vCantidadDespPres = Obj.Cantidad_despachada
                                    Obj.Cantidad_despachada = Math.Round(Obj.Cantidad_despachada * BePresentacion.Factor, 6)
                                Else
                                    vCantidadReservadaPres = Obj.Cantidad_Solicitada
                                    vCantidadReservadaUMBas = Math.Round(Obj.Cantidad_Solicitada * BePresentacion.Factor, 6)
                                    vCantidadRecPres = Obj.Cantidad_Recibida
                                    Obj.Cantidad_Recibida = Math.Round(Obj.Cantidad_Recibida * BePresentacion.Factor, 6)
                                    vCantidadVerPres = Obj.Cantidad_Verificada
                                    Obj.Cantidad_Verificada = Math.Round(Obj.Cantidad_Verificada * BePresentacion.Factor, 6)
                                    vCantidadDespPres = Obj.Cantidad_despachada
                                    Obj.Cantidad_despachada = Math.Round(Obj.Cantidad_despachada * BePresentacion.Factor, 6)
                                End If

                            Else
                                vCantidadReservadaPres = Obj.Cantidad_Solicitada
                                vCantidadReservadaUMBas = Math.Round(Obj.Cantidad_Solicitada * BePresentacion.Factor, 6)
                                vCantidadRecPres = Obj.Cantidad_Recibida
                                Obj.Cantidad_Recibida = Math.Round(Obj.Cantidad_Recibida * BePresentacion.Factor, 6)
                                vCantidadVerPres = Obj.Cantidad_Verificada
                                Obj.Cantidad_Verificada = Math.Round(Obj.Cantidad_Verificada * BePresentacion.Factor, 6)
                                vCantidadDespPres = Obj.Cantidad_despachada
                                Obj.Cantidad_despachada = Math.Round(Obj.Cantidad_despachada * BePresentacion.Factor, 6)
                            End If

                        End If

                    End If

                    Dim vTieneManufactura As Boolean = clsLnTrans_pe_det.Tiene_Manufactura_Asociada(Obj.IdPedidoEnc, Obj.IdPedidoDet)

                    DTStockRes.Rows.Add(Obj.IdPedidoEnc,
                                        Obj.IdPickingEnc,
                                        Obj.CodigoProducto,
                                        Obj.NombreProducto,
                                        Obj.ProductoPresentacion,
                                        Obj.ProductoEstado,
                                        Obj.ProductoUnidadMedida,
                                        BePropietario.Propietario.Nombre_comercial,
                                        Obj.NombreUbicacion,
                                        Obj.Lote,
                                        Obj.Lic_plate,
                                        Obj.Fecha_Vence,
                                        0,
                                        vCantidadReservadaPres,
                                        vCantidadReservadaUMBas,
                                        vCantidadRecPres,
                                        Obj.Cantidad_Recibida,
                                        vCantidadVerPres,
                                        Obj.Cantidad_Verificada,
                                        vCantidadDespPres,
                                        Obj.Cantidad_despachada,
                                        Obj.Peso_recibido,
                                        Obj.Peso_verificado,
                                        Obj.Encontrado,
                                        Obj.Acepto,
                                        "01/01/1900",
                                         vTieneManufactura)

                Next

            End If

            dgridPickingUbic.DataSource = DTStockRes

            grdvPickingUbic.OptionsView.ShowFooter = True

            RemoveHandler grdvPickingUbic.RowCellStyle, AddressOf grdPickingUbic_RowCellStyle
            AddHandler grdvPickingUbic.RowCellStyle, AddressOf grdPickingUbic_RowCellStyle

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Set_Formato_Grid_Picking_Ubic()

        Try

            IsLoading = True

            If grdvPickingUbic.Columns.Count > 0 Then

                If grdvPickingUbic.RowCount > 0 Then

                    'Create and setup the first summary item.
                    Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cant_Ped_UmBas",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Ped_UmBas")}
                    grdvPickingUbic.GroupSummary.Add(item)

                    Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cant_Pick_UMBas",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Pick_UMBas")}
                    grdvPickingUbic.GroupSummary.Add(item1)

                    Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cant_Veri_Pres",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Veri_Pres")}
                    grdvPickingUbic.GroupSummary.Add(item2)

                    Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Peso_Pick",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Peso_Pick")}
                    grdvPickingUbic.GroupSummary.Add(item3)

                    Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Peso_Veri",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Peso_Veri")}
                    grdvPickingUbic.GroupSummary.Add(item4)

                    Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cant_Desp_Pres",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Desp_Pres")}
                    grdvPickingUbic.GroupSummary.Add(item5)

                    Dim item6 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cant_Pick_Pres",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Pick_Pres")}
                    grdvPickingUbic.GroupSummary.Add(item6)

                    Dim item7 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cant_Veri_UMBas",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Veri_UMBas")}
                    grdvPickingUbic.GroupSummary.Add(item7)

                    Dim item8 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cant_Desp_UMBas",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Desp_UMBas")}
                    grdvPickingUbic.GroupSummary.Add(item8)

                    grdvPickingUbic.Columns("Pedido").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    grdvPickingUbic.Columns("Pedido").SummaryItem.DisplayFormat = "{0:n0}"

                    grdvPickingUbic.Columns("Cant_Ped_UmBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    grdvPickingUbic.Columns("Cant_Ped_UmBas").SummaryItem.DisplayFormat = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Ped_UmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grdvPickingUbic.Columns("Cant_Ped_UmBas").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Pick_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    grdvPickingUbic.Columns("Cant_Pick_Pres").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Pick_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grdvPickingUbic.Columns("Cant_Pick_Pres").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Pick_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    grdvPickingUbic.Columns("Cant_Pick_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Pick_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grdvPickingUbic.Columns("Cant_Pick_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Veri_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    grdvPickingUbic.Columns("Cant_Veri_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Veri_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grdvPickingUbic.Columns("Cant_Veri_Pres").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Veri_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    grdvPickingUbic.Columns("Cant_Veri_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Veri_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grdvPickingUbic.Columns("Cant_Veri_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Desp_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    grdvPickingUbic.Columns("Cant_Desp_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Desp_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grdvPickingUbic.Columns("Cant_Desp_Pres").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Desp_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    grdvPickingUbic.Columns("Cant_Desp_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    grdvPickingUbic.Columns("Cant_Desp_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grdvPickingUbic.Columns("Cant_Desp_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.Columns("Peso_Pick").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    grdvPickingUbic.Columns("Peso_Pick").SummaryItem.DisplayFormat = "{0:n6}"

                    grdvPickingUbic.Columns("Peso_Pick").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grdvPickingUbic.Columns("Peso_Pick").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.Columns("Peso_Veri").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    grdvPickingUbic.Columns("Peso_Veri").SummaryItem.DisplayFormat = "{0:n6}"

                    grdvPickingUbic.Columns("Peso_Veri").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grdvPickingUbic.Columns("Peso_Veri").DisplayFormat.FormatString = "{0:n6}"

                    grdvPickingUbic.ExpandAllGroups()
                    grdvPickingUbic.BestFitColumns()

                    lblRegs.Caption = String.Format("Registros: {0}", grdvPickingUbic.RowCount)

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsLoading = False
        End Try

    End Sub

    Private lPedidosPicking As New List(Of clsBeTrans_pe_enc)

    Private Sub Cargar_Datos()

        Dim clsTransaccion As New clsTransaccion

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Actualizando picking...")

            IsLoading = True

            If BePickingEnc IsNot Nothing Then

                clsTransaccion.Open_Connection() : clsTransaccion.Begin_Transaction()

                BePickingEnc = clsLnTrans_picking_enc.GetSingle(BePickingEnc.IdPickingEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                Mostrar_Datos_Encabezado(clsTransaccion.lConnection, clsTransaccion.lTransaction)

                Mostrar_Pedidos_Asociados(clsTransaccion.lConnection, clsTransaccion.lTransaction)

                Cargar_Pedidos_Impresion(clsTransaccion.lConnection, clsTransaccion.lTransaction)

                If BePickingEnc.ListaPickingDet IsNot Nothing AndAlso BePickingEnc.ListaPickingDet.Count > 0 Then

                    dgridPedidos.Rows.Clear()
                    DTStockRes.Rows.Clear()

                    Dim ListaPedidosPicking As List(Of Integer) = (From picking In BePickingEnc.ListaPickingDet Select picking.IdPedidoEnc).Distinct.ToList()

                    pListaPedidos = ListaPedidosPicking

                    Dim BePickingDet As New clsBeTrans_picking_det
                    Dim vPedido As New clsBeTrans_pe_enc()
                    Dim ie As Integer = 0
                    Dim TiempoCliente As New clsBeCliente_tiempos()

                    For Each IdPedidoEnc As Integer In ListaPedidosPicking

                        BePickingDet = BePickingEnc.ListaPickingDet.ToList.Find(Function(b) b.IdPedidoEnc = IdPedidoEnc)

                        ie = dgridPedidos.Rows.Add()

                        dgridPedidos.Rows(ie).Cells("IdPedido").Value = BePickingDet.IdPedidoEnc
                        dgridPedidos.Rows(ie).Cells("Referencia").Value = BePickingDet.Referencia
                        dgridPedidos.Rows(ie).Cells("Bodega").Value = BePickingDet.Bodega
                        dgridPedidos.Rows(ie).Cells("Cliente").Value = BePickingDet.Cliente
                        dgridPedidos.Rows(ie).Cells("Propietario").Value = BePickingDet.Propietario
                        dgridPedidos.Rows(ie).Cells("FechaPedido").Value = BePickingDet.FechaPedido
                        dgridPedidos.Rows(ie).Cells("EstadoP").Value = vPedido.Estado

                        If BePickingEnc.Estado = "Despachado" Then

                            If Not lPedidosPicking Is Nothing Then

                                If Not lPedidosPicking.Count = 0 Then

                                    vPedido = lPedidosPicking.Find(Function(x) x.IdPedidoEnc = IdPedidoEnc)

                                    If vPedido Is Nothing Then
                                        vPedido = clsLnTrans_pe_enc.Get_Single_Without_Picking(IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                    End If

                                Else
                                    vPedido = clsLnTrans_pe_enc.Get_Single_Without_Picking(IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                End If

                            End If

                            For Each BePedidoDet As clsBeTrans_pe_det In vPedido.Detalle

                                SplashScreenManager.Default.SetWaitFormDescription("Listando producto: " & BePedidoDet.Codigo_Producto)

                                BePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_Despachado_By_IdPedidoDet(BePedidoDet.IdPedidoEnc,
                                                                                                                                     BePedidoDet.IdPedidoDet,
                                                                                                                                     0,
                                                                                                                                     clsTransaccion.lConnection,
                                                                                                                                     clsTransaccion.lTransaction)

                                If Not BePedidoDet.ListaPickingUbic Is Nothing Then
                                    Get_Stock_Res(BePedidoDet.ListaPickingUbic, True)
                                End If

                            Next

                        Else
                            Set_Stock_Res(BePickingDet.IdPedidoEnc)
                        End If

                    Next

                    dgridPedidos.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    dgridPedidos.EndEdit()

                    BeListPickingDet = BePickingEnc.ListaPickingDet

                    Dim i As Integer = -1

                    dgridDetallePicking.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    dgridDetallePicking.EndEdit()

                    Try
                        dgridDetallePicking.Rows.Clear()
                    Catch ex As Exception
                        Debug.Write("No se porque da error aquí a veces: " & ex.Message)
                    End Try


                    Dim ObjDP As clsBeTrans_picking_det_parametros
                    Dim ObjlU As New List(Of clsBeTrans_picking_ubic)
                    Dim PedidoEnc As New clsBeTrans_pe_enc

                    pListBePickingUbic = New List(Of clsBeTrans_picking_ubic)

                    For Each det As clsBeTrans_picking_det In BePickingEnc.ListaPickingDet

                        i = dgridDetallePicking.Rows.Add(det.Producto.Codigo)

                        dgridDetallePicking.Rows(i).Cells("IdPedidoEnc").Value = det.IdPedidoEnc
                        dgridDetallePicking.Rows(i).Cells("IdPedidoDet").Value = det.IdPedidoDet
                        dgridDetallePicking.Rows(i).Cells("Codigo").Value = det.Codigo '#CKFK 20200514 lo cambié por det.producto.codigo
                        dgridDetallePicking.Rows(i).Cells("Producto").Value = det.NombreProducto '#CKFK 20200514 lo cambié por det.producto.nombre
                        dgridDetallePicking.Rows(i).Cells("Presentacion").Value = det.Presentacion.Nombre
                        dgridDetallePicking.Rows(i).Cells("UnidadMedida").Value = det.UnidadMedida.Nombre
                        dgridDetallePicking.Rows(i).Cells("Estado").Value = det.ProductoEstado.Nombre
                        dgridDetallePicking.Rows(i).Cells("Cantidad").Value = det.Cantidad

                        '#EJC20171026_0523PM: Validación de días cliente en cargadatos picking.
                        PedidoEnc.IdPedidoEnc = det.IdPedidoEnc

                        TiempoCliente = clsLnCliente_tiempos.GetSingle(PedidoEnc.IdCliente,
                                                                       det.Producto.IdFamilia,
                                                                       det.Producto.IdClasificacion,
                                                                       clsTransaccion.lConnection,
                                                                       clsTransaccion.lTransaction)

                        If Not TiempoCliente Is Nothing Then
                            dgridDetallePicking.Rows(i).Cells("ClienteDias").Value = IIf(PedidoEnc.Local, TiempoCliente.Dias_Local, TiempoCliente.Dias_Exterior)
                        Else
                            dgridDetallePicking.Rows(i).Cells("ClienteDias").Value = -1
                        End If

                        dgridDetallePicking.Rows(i).Cells("CantidadRecibida").Value = det.Cantidad_recibida

                        Cargar_Operador(i,
                                        clsTransaccion.lConnection,
                                        clsTransaccion.lTransaction,
                                        det.IdOperadorBodega)

                        ObjDP = det.ListaDetalleParametro.ToList.Find(Function(b) b.IdPickingDet = det.IdPickingDet)

                        If ObjDP IsNot Nothing Then
                            BeListPickingParam.Add(ObjDP)
                        End If

                        '#EJC20171026_1212PM: Anteriormente se buscaba solo un objeto, pero cada IdPIckingDet tiene N picking ubic asociados, por lo que se debe hacer un findAll
                        ObjlU = BePickingEnc.ListaPickingUbic.FindAll(Function(b) b.IdPickingDet = det.IdPickingDet)

                        If ObjlU IsNot Nothing Then
                            '#EJC20171026_1212PM: Asociar a cada objeto de la lista el IdPedidoDet al que pertenece.
                            ObjlU.ForEach(Sub(x) x.IdPedidoDet = det.IdPedidoDet)
                            pListBePickingUbic.AddRange(ObjlU)
                        End If

                        'det.ListaDetalleUbicacion = ObjlU

                    Next

                End If

                If BePickingEnc.Estado = "Anulado" Then
                    Deshabilitar_items()
                ElseIf BePickingEnc.Estado = "Verificado" Then
                    If pListBePickingUbic.Count = 0 Then
                        mnuDespachado.Enabled = True
                    End If
                End If

                clsTransaccion.Commit_Transaction()

                Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

                BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                      AP.UsuarioAp.IdUsuario,
                                                                                      AP.HostName,
                                                                                      vNombreArchivoLayOutGrid)

                '#GT01122025: Estoy infiriendo que mampa no trabaja picking consolidados.
                Dim tmpPedido = lPedidosPicking(0)
                Dim BePedido = clsLnTrans_pe_enc.Get_Single_Without_Picking(tmpPedido.IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                If BePedido IsNot Nothing Then
                    If BePedido.TipoPedido.Verificar_con_imagen Then
                        BloquearControles_Por_VerificacionBOF(False)
                    Else
                        BloquearControles_Por_VerificacionBOF(True)
                    End If
                End If

                If Not BeConfiguracionUsuarioDet Is Nothing Then
                    grdvPickingUbic.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                    mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If

            End If

            Actualizar_Gaugue_Progreso()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
            IsLoading = False
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub Set_IdUbicacion_Picking()

        Try

            Dim pIdBodega As Integer = cmbBodegas.EditValue
            Dim Obj = New clsBeBodega() With {.IdBodega = pIdBodega}
            clsLnBodega.Obtener(Obj)
            txtIdUbicacion.Text = Obj.Ubic_picking
            txtIdUbicacion_Validated(txtIdUbicacion, Nothing)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Deshabilitar_items()

        mnuGuardar.Enabled = False
        mnuActualizar.Enabled = False
        mnuEliminar.Enabled = False
        lnkQuitarPedido.Enabled = False
        cmdImprimir.Enabled = False
        mnuProcesar.Enabled = False
        mnuProcesarLinea.Enabled = False
        dtmFechaPicking.Enabled = False
        cmbBodegas.Enabled = False
        cmbPropietario.Enabled = False
        chkActivo.Enabled = False
        chkDetalleOperador.Enabled = False
        lnkUbicacion.Enabled = False
        txtIdUbicacion.Enabled = False
        txtNombreUbicacion.Enabled = False
        dtmFechaTarea.Enabled = False
        dtmHoraI.Enabled = False
        dtmHoraF.Enabled = False
        dtmHoraIhh.Enabled = False
        dtmHoraFhh.Enabled = False
        lnkAgregarPedido.Enabled = False
        lnkVerParametro.Enabled = False
        cmbAgrupamiento.Enabled = False
        mnuActualizarPicking.Enabled = False
        mnuDespachado.Enabled = False
        cmdNoPickeado.Enabled = False
        cmdNoVerificado.Enabled = False
        cmdVerificarNuevamente.Enabled = False
        mnuPendientePicking.Enabled = False

    End Sub

    Private Sub lnkVerParametro_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkVerParametro.LinkClicked

        Try

            If dgridDetallePicking.Rows.Count > 0 AndAlso dgridDetallePicking.CurrentRow IsNot Nothing Then

                If dgridDetallePicking.CurrentRow.Cells("Codigo").Value IsNot DBNull.Value AndAlso dgridDetallePicking.CurrentRow.Cells("Codigo").Value IsNot Nothing _
                    AndAlso dgridDetallePicking.CurrentRow.Cells("IdPedidoDet").Value IsNot DBNull.Value AndAlso dgridDetallePicking.CurrentRow.Cells("IdPedidoDet").Value IsNot Nothing Then

                    Dim lCodigo As Object = dgridDetallePicking.CurrentRow.Cells("Codigo").Value
                    Dim lIdPedidoDet As Object = dgridDetallePicking.CurrentRow.Cells("IdPedidoDet").Value

                    If lCodigo Is Nothing OrElse lIdPedidoDet Is Nothing Then Return

                    If BeListPickingDet IsNot Nothing AndAlso BeListPickingDet.Count > 0 Then
                        Dim lIndex As Integer = BeListPickingDet.FindIndex(Function(b) b.Producto.Codigo = lCodigo AndAlso b.IdPedidoDet = lIdPedidoDet)
                        If lIndex > -1 Then
                            'ObjProducto = clsLnProducto.GetSingle(BeListPickingDet(lIndex).Producto.IdProducto)
                            '#EJC20171002: No tengo el IdStock
                            'setStockParametro(ObjProducto, pListObjD(lIndex).IdPickingDet, lIndex)
                        End If
                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick
        Anular_Picking()
    End Sub

    Private Sub Anular_Picking()

        Dim vLiberarStock As Boolean = False

        Try

            mnuEliminar.Enabled = False

            If XtraMessageBox.Show("¿Anular picking?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                '#EJC20220708:Registrar que operador va a anular el picking/pedido
                BePickingEnc.User_mod = AP.UsuarioAp.IdUsuario

                If XtraMessageBox.Show("¿Liberar stock reservado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    vLiberarStock = True
                End If

                If clsLnTrans_picking_enc.Anular_Picking(BePickingEnc,
                                                         BePickingEnc.ListaPickingDet,
                                                         BeListOp,
                                                         AP.IdEmpresa,
                                                         vLiberarStock) Then

                    Dim vMsgError As String = "El usuario" & AP.UsuarioAp.IdUsuario & " anuló el picking " & BePickingEnc.IdPickingEnc
                    'clsLnLog_error_wms.Agregar_Error(vMsgError)
                    clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                          pIdEmpresa:=AP.IdEmpresa,
                                                          pIdBodega:=AP.IdBodega,
                                                          pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                          pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                          pIdPickingEnc:=BePickingEnc.IdPickingEnc)

                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Tarea de picking anulada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    '#EJC20180622: Si el pedido está abierto, se actualiza la información del picking
                    For Each vIdPedido In pListaPedidos
                        If Application.OpenForms().OfType(Of frmPedido).Any Then
                            If frmPedido.pBePedidoEnc.IdPedidoEnc = vIdPedido Then
                                frmPedido.pBePedidoEnc.IdPickingEnc = 0
                                frmPedido.Actualizar_Info_Picking()
                            End If
                        End If
                    Next

                    If Not InvokeListarPicking Is Nothing Then
                        InvokeListarPicking.Invoke()
                    End If

                    If Not InvokeActualizarIdPicking Is Nothing Then
                        InvokeActualizarIdPicking.Invoke()
                    End If

                    Close()

                Else
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("No se logró anular el Picking.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If

            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Try
                SplashScreenManager.CloseForm(False)
            Catch ex As Exception
            End Try
        End Try

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        mnuGuardar.Enabled = False
        Process_Guardar_Picking(True)
        mnuGuardar.Enabled = True

    End Sub

    Private Sub Process_Guardar_Picking(Optional ByVal Preguntar As Boolean = True)

        Try

            If Valida_Datos() Then

                If Preguntar Then

                    If Not XtraMessageBox.Show("¿Guardar Picking?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Exit Sub
                    End If

                End If

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

                If Guardar_Picking() Then

                    Modo = TipoTrans.Editar

                    Set_Menu_Edicion()

                    '#EJC20171120:
                    'Crear un parámetro despues para hacer ésto.
                    If chkProcesarDesdeBOF.Checked Then
                        SplashScreenManager.CloseForm(False)
                        mnuProcesar_ItemClick(Nothing, Nothing)
                    Else

                        SplashScreenManager.CloseForm(False)

                        XtraMessageBox.Show("Se guardó la tarea de picking", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If InvokeListarPicking IsNot Nothing Then
                            InvokeListarPicking.Invoke
                        End If

                        If Modal Then
                            DialogResult = DialogResult.OK
                        Else
                            Close()
                        End If

                    End If

                End If

            End If


        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        mnuActualizar.Enabled = False
        Guardar_Picking()
        mnuActualizar.Enabled = True

    End Sub

    Private Sub txtIdUbicacion_Validated(sender As Object, e As EventArgs) Handles txtIdUbicacion.Validated
        Valida_Ubicacion()
    End Sub
    Public Sub Valida_Ubicacion()

        Try

            If String.IsNullOrEmpty(txtIdUbicacion.Text.Trim()) = False AndAlso txtIdUbicacion.Text > "0" Then

                Dim BeBodegaUbicacion As New clsBeBodega_ubicacion

                BeBodegaUbicacion = clsLnBodega_ubicacion.Get_Ubicacion_Picking(txtIdUbicacion.Text.Trim, BeBodega.IdBodega)

                If BeBodegaUbicacion IsNot Nothing AndAlso BeBodegaUbicacion.IdUbicacion > 0 Then
                    txtNombreUbicacion.Text = BeBodegaUbicacion.Descripcion
                Else
                    XtraMessageBox.Show(String.Format("No existe Ubicación de Picking con código {0}", txtIdUbicacion.Text.Trim(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
                    txtIdUbicacion.Focus()
                    txtIdUbicacion.SelectAll()
                End If

            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub
    Private Sub cmdListaUbicacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdListaUbicacion.ItemClick

        Try

            If dgridPickingUbic.DefaultView.RowCount > 0 Then
                If MsgBox("¿Imprimir ubicaciones de picking?", MsgBoxStyle.YesNo, Text) = MsgBoxResult.Yes Then
                    Imprimir_Vista()
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub frmPicking_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F2 Then
            lnkAgregarPedido_ItemClick(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.F3 Then
            lnkVerParametro_LinkClicked(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            If Not chkProcesarDesdeBOF.Checked Then
                chkProcesarDesdeBOF.Checked = True
                mnuProcesar_ItemClick(Nothing, Nothing)
            Else
                chkProcesarDesdeBOF.Checked = False
            End If
        End If

    End Sub

    '#CM20172310_1231PM: Se agregaron campos al GridView de ubicación picking. 
    Public Sub Set_Stock_Res(ByVal pIdPedidoEnc As Integer)

        Try

            pListObjSP = clsLnTrans_pe_det.Get_All_Stock_Res_By_IdPedidoEnc_And_IdPickingEnc(pIdPedidoEnc,
                                                                                             BePickingEnc.IdPickingEnc,
                                                                                             False,
                                                                                             (Modo = TipoTrans.Nuevo),
                                                                                             True)

            If pListObjSP.Count > 0 Then

                Dim vCantidadReservadaUMBas As Double = 0
                Dim vCantidadReservadaPres As Double = 0
                Dim vPesoReservado As Double = 0
                Dim vCantidadRecPres As Double = 0
                Dim vCantidadVerPres As Double = 0
                Dim vCantidadDespPres As Double = 0
                Dim BePresentacion As New clsBeProducto_Presentacion
                Dim BePedidoDet As New clsBeTrans_pe_det
                Dim Idx As Integer = -1

                Dim BeUbicacion As New clsBeBodega_ubicacion()
                Dim vSingularidadTramoUbicacionEnZP As New List(Of clsBeZona_picking_tramo)
                Dim SingleBeZonaPIckingTramoOP As New clsBeOperador_zona_picking_tramo
                Dim BeOperadorBodega As New clsBeOperador_bodega
                Dim vNombreOperadorAsingadoAuto As String = ""

                '#EJC20190607: No limpiar, porque si se cargan más pedidos al picking se limpia el stock anterior del grid.
                If Modo = TipoTrans.Editar Then
                    'DTStockRes.Rows.Clear()
                End If

                For Each ObjStock As clsBeVW_stock_res In pListObjSP

                    Idx = lBePedidoDet.FindIndex(Function(x) x.IdPedidoDet = ObjStock.IdPedidoDet)
                    '#EJC20191121: Optimización por memoria.
                    If Idx = -1 Then
                        BePedidoDet.IdPedidoDet = ObjStock.IdPedidoDet
                        clsLnTrans_pe_det.GetSingle(BePedidoDet)
                        lBePedidoDet.Add(BePedidoDet.Clone())
                    Else
                        BePedidoDet = lBePedidoDet(Idx).Clone()
                    End If

                    '#CKFK20221121 Voy a quitar esta condición OrElse (BePedidoDet.IdPresentacion = 0)
                    If (ObjStock.IdPresentacion = 0) Then

                        vCantidadReservadaUMBas = ObjStock.CantidadReservadaUMBas
                        vCantidadReservadaPres = 0
                        vPesoReservado = ObjStock.Peso
                        '#CKFK 20210728 Coloqué en 0 estas variables cuando el producto no tiene presentacion vCantidadRecPres y vCantidadVerPres
                        vCantidadRecPres = 0
                        vCantidadVerPres = 0

                    Else

                        '#EJC20191121_1456: Optimización por memoria.
                        Idx = lBePresentacion.FindIndex(Function(x) x.IdPresentacion = ObjStock.IdPresentacion)

                        If Idx = -1 Then
                            BePresentacion = New clsBeProducto_Presentacion()
                            BePresentacion.IdPresentacion = ObjStock.IdPresentacion
                            clsLnProducto_presentacion.GetSingle(BePresentacion)
                            lBePresentacion.Add(BePresentacion.Clone())
                        Else
                            BePresentacion = New clsBeProducto_Presentacion()
                            BePresentacion = lBePresentacion(Idx).Clone()
                        End If

                        If Not BePresentacion Is Nothing Then

                            If (Modo = TipoTrans.Nuevo) Then

                                If (ObjStock.IdPresentacion <> 0) AndAlso (BePedidoDet.IdPresentacion <> 0) Then
                                    vCantidadReservadaPres = Math.Round(ObjStock.CantidadReservadaUMBas / BePresentacion.Factor, 6)
                                    vCantidadReservadaUMBas = ObjStock.CantidadReservadaUMBas
                                    ObjStock.Cantidad_Pickeada = Math.Round(ObjStock.Cantidad_Pickeada / BePresentacion.Factor, 6)
                                    ObjStock.Cantidad_Verificada = Math.Round(ObjStock.Cantidad_Verificada / BePresentacion.Factor, 6)
                                    vCantidadRecPres = Math.Round(ObjStock.Cantidad_Pickeada / BePresentacion.Factor, 6)
                                    ObjStock.Cantidad_Pickeada = ObjStock.Cantidad_Pickeada
                                    vCantidadVerPres = Math.Round(ObjStock.Cantidad_Verificada / BePresentacion.Factor, 6)
                                    ObjStock.Cantidad_Verificada = ObjStock.Cantidad_Verificada
                                    vCantidadDespPres = 0
                                End If

                            Else
                                '#CM_20190206: Se cambio el orden en el que se realizaban las operaciones porque la cantidad cuando tiene presentación el picking la devuelve en presentación.
                                '#CKFK20221121 Puse esto en comentario AndAlso (BePedidoDet.IdPresentacion <> 0) para que solo se guíe por la reserva
                                If (ObjStock.IdPresentacion <> 0) Then
                                    If BePedidoDet.IdStockEspecifico = 0 Then
                                        If (ObjStock.IdPresentacion <> 0) Then ' BePedidoDet.IdPresentacion
                                            vCantidadReservadaPres = ObjStock.CantidadReservadaUMBas
                                            vCantidadReservadaUMBas = Math.Round(ObjStock.CantidadReservadaUMBas * BePresentacion.Factor, 6)
                                            vCantidadRecPres = ObjStock.Cantidad_Pickeada
                                            ObjStock.Cantidad_Pickeada = Math.Round(ObjStock.Cantidad_Pickeada * BePresentacion.Factor, 6)
                                            vCantidadVerPres = ObjStock.Cantidad_Verificada
                                            ObjStock.Cantidad_Verificada = Math.Round(ObjStock.Cantidad_Verificada * BePresentacion.Factor, 6)
                                            vCantidadDespPres = ObjStock.Cantidad_Despachada
                                            ObjStock.Cantidad_Despachada = Math.Round(ObjStock.Cantidad_Despachada * BePresentacion.Factor, 6)
                                        Else
                                            vCantidadReservadaPres = ObjStock.CantidadReservadaUMBas
                                            vCantidadReservadaUMBas = Math.Round(ObjStock.CantidadReservadaUMBas * BePresentacion.Factor, 6)
                                            ObjStock.Cantidad_Pickeada = ObjStock.Cantidad_Pickeada
                                            ObjStock.Cantidad_Verificada = ObjStock.Cantidad_Verificada
                                        End If
                                    Else
                                        vCantidadReservadaPres = ObjStock.CantidadReservadaUMBas
                                        vCantidadReservadaUMBas = Math.Round(ObjStock.CantidadReservadaUMBas * BePresentacion.Factor, 6)
                                        '#CKFK 20210728 Llené la variable de la cantidad recibida con presentacion que no se estaba llenando
                                        'y en la cantidad pickeada asigné la cantidad en presentación multiplicada por el factor
                                        vCantidadRecPres = ObjStock.Cantidad_Pickeada
                                        ObjStock.Cantidad_Pickeada = Math.Round(ObjStock.Cantidad_Pickeada * BePresentacion.Factor, 6)
                                        '#CKFK 20210728 Llené la variable de la cantidad verificada con presentacion que no se estaba llenando
                                        'y en la cantidad verificad asigné la cantidad en presentación multiplicada por el factor
                                        vCantidadVerPres = ObjStock.Cantidad_Verificada
                                        ObjStock.Cantidad_Verificada = Math.Round(ObjStock.Cantidad_Verificada * BePresentacion.Factor, 6)
                                    End If
                                End If

                            End If

                        Else
                            Throw New Exception("No se pudo obtener la presentación con identificador: " & ObjStock.IdPresentacion)
                        End If

                        vPesoReservado = ObjStock.Peso

                    End If

                    '#CKFK20220704 Agregué esto para poder eliminar la fila y que no se inserte dos veces en el grid
                    If DTStockRes.Rows.Count > 0 Then
                        Dim vRow As DataRow

                        vRow = DTStockRes.Select("IdStockRes=" & ObjStock.IdStockRes).FirstOrDefault

                        If vRow IsNot Nothing Then
                            DTStockRes.Rows.Remove(vRow)
                        End If

                    End If

                    Dim vTieneManufactura As Boolean = clsLnTrans_pe_det.Tiene_Manufactura_Asociada(ObjStock.IdPedido, ObjStock.IdPedidoDet)

                    DTStockRes.Rows.Add(ObjStock.IdPedido,
                                        ObjStock.IdPicking,
                                        ObjStock.Codigo_Producto,
                                        ObjStock.Nombre_Producto,
                                        ObjStock.Nombre_Presentacion,
                                        ObjStock.NomEstado,
                                        ObjStock.UMBas,
                                        ObjStock.Propietario,
                                        ObjStock.Ubicacion_Nombre,
                                        ObjStock.Lote,
                                        ObjStock.Lic_plate,
                                        ObjStock.Fecha_Vence,
                                        ObjStock.Factor,
                                        vCantidadReservadaPres,
                                        vCantidadReservadaUMBas,
                                        vCantidadRecPres,
                                        ObjStock.Cantidad_Pickeada,
                                        vCantidadVerPres,
                                        ObjStock.Cantidad_Verificada,
                                        vCantidadDespPres,
                                        ObjStock.Cantidad_Despachada,
                                        ObjStock.peso_pickeado,
                                        ObjStock.peso_verificado,
                                        ObjStock.encontrado,
                                        ObjStock.acepto,
                                        ObjStock.Fecha_ingreso,
                                        ObjStock.IdStockRes,
                                        vNombreOperadorAsingadoAuto,
                                        vTieneManufactura,
                                        ObjStock.Codigo_Talla,
                                        ObjStock.Codigo_Color)

                Next

            End If

            If DTStockRes.Rows.Count > 0 Then

                If BePickingEnc.Estado <> "Despachado" Then
                    dgridPickingUbic.DataSource = DTStockRes
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub grdListaPickingD_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgridDetallePicking.RowValidating

        Try

            If Not String.IsNullOrEmpty(dgridDetallePicking.Rows(e.RowIndex).Cells("Producto").Value) Then

                If Not dgridDetallePicking.Rows(e.RowIndex).Cells("ClienteDias").Value Is Nothing Then

                    If dgridDetallePicking.Rows(e.RowIndex).Cells("ClienteDias").Value <> -1 Then
                        e.Cancel = True
                        dgridDetallePicking.Rows(e.RowIndex).Cells("ClienteDias").ErrorText = "El Número de días-cliente no puede estar vacía."
                    ElseIf IsNumeric(dgridDetallePicking.Rows(e.RowIndex).Cells("ClienteDias").Value) = False Then
                        e.Cancel = True
                        dgridDetallePicking.Rows(e.RowIndex).Cells("ClienteDias").ErrorText = "El Número de días-cliente debe ser un valor númerico."
                    Else
                        e.Cancel = False
                        dgridDetallePicking.Rows(e.RowIndex).Cells("ClienteDias").ErrorText = String.Empty
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmbAgrupamiento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAgrupamiento.SelectedIndexChanged

        Try

            If cmbAgrupamiento.SelectedIndex > -1 Then

                If cmbAgrupamiento.SelectedIndex = 0 Then

                    Consolidar()

                ElseIf cmbAgrupamiento.SelectedIndex = 1 Then

                    dgridDetallePicking.Rows.Clear()

                    'grdListaPickingD.SuspendLayout()

                    Dim PedidoEnc As New clsBeTrans_pe_enc
                    Dim TiempoCliente As New clsBeCliente_tiempos

                    For Each Obj As clsBeTrans_picking_det In BeListPickingDet

                        Dim i As Integer = dgridDetallePicking.Rows.Add()

                        dgridDetallePicking.Rows(i).Cells("Codigo").Value = Obj.Producto.Codigo
                        dgridDetallePicking.Rows(i).Cells("Producto").Value = Obj.Producto.Nombre
                        dgridDetallePicking.Rows(i).Cells("Presentacion").Value = Obj.Presentacion.Nombre
                        dgridDetallePicking.Rows(i).Cells("UnidadMedida").Value = Obj.UnidadMedida.Nombre
                        dgridDetallePicking.Rows(i).Cells("Estado").Value = Obj.ProductoEstado.Nombre
                        dgridDetallePicking.Rows(i).Cells("Cantidad").Value = Obj.Cantidad

                        '#EJC20171026_0523PM: Validación de días cliente en cargadatos picking. en cmbAgrupamiento_SelectedIndexChanged
                        PedidoEnc.IdPedidoEnc = Obj.IdPedidoEnc

                        PedidoEnc = clsLnTrans_pe_enc.GetSingle(PedidoEnc.IdPedidoEnc)

                        TiempoCliente = clsLnCliente_tiempos.GetSingle(PedidoEnc.IdCliente,
                                                                       Obj.Producto.IdFamilia,
                                                                       Obj.Producto.IdClasificacion)

                        If Not TiempoCliente Is Nothing Then
                            dgridDetallePicking.Rows(i).Cells("ClienteDias").Value = IIf(PedidoEnc.Local, TiempoCliente.Dias_Local, TiempoCliente.Dias_Exterior)
                        Else
                            dgridDetallePicking.Rows(i).Cells("ClienteDias").Value = -1
                        End If

                        'grdListaPickingD.Rows(i).Cells("ClienteDias").Value = Obj.Cliente_dias

                        dgridDetallePicking.Rows(i).Cells("CantidadRecibida").Value = Obj.Cantidad_recibida
                        dgridDetallePicking.Rows(i).Cells("IdPedidoDet").Value = Obj.IdPedidoDet
                        dgridDetallePicking.Rows(i).Cells("IdPedidoEnc").Value = Obj.IdPedidoEnc

                    Next

                    'grdListaPickingD.ResumeLayout()                    

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Consolidar()

        Try

            dgridDetallePicking.Rows.Clear()
            'grdListaPickingD.SuspendLayout()

            Dim lCodigos As List(Of String) = (From p In BeListPickingDet Select p.Producto.Codigo).Distinct.ToList

            For Each lCodigo As String In lCodigos

                Dim lPresentaciones As List(Of Integer) = (From pr In BeListPickingDet Where pr.Producto.Codigo = lCodigo Select pr.Presentacion.IdPresentacion).Distinct.ToList

                For Each lPresentacion As Integer In lPresentaciones

                    Dim lEstados As List(Of Integer) = (From es In BeListPickingDet Where es.Producto.Codigo = lCodigo _
                                                        And es.Presentacion.IdPresentacion = lPresentacion Select es.ProductoEstado.IdEstado).Distinct.ToList

                    For Each lEstado As Integer In lEstados

                        Dim bo As clsBeTrans_picking_det = BeListPickingDet.Find(Function(f) f.Producto.Codigo = lCodigo AndAlso f.Presentacion.IdPresentacion = lPresentacion AndAlso f.ProductoEstado.IdEstado = lEstado)

                        Dim lCantidad As Double = BeListPickingDet.Where(Function(c) c.Producto.Codigo = lCodigo _
                                                                  AndAlso c.Presentacion.IdPresentacion = lPresentacion AndAlso c.ProductoEstado.IdEstado = lEstado).Sum(Function(s) s.Cantidad)
                        Dim lCantidadRecibida As Double = BeListPickingDet.Where(Function(c) c.Producto.Codigo = lCodigo _
                                                                          AndAlso c.Presentacion.IdPresentacion = lPresentacion AndAlso c.ProductoEstado.IdEstado = lEstado).Sum(Function(s) s.Cantidad_recibida)

                        Dim i As Integer = dgridDetallePicking.Rows.Add()

                        dgridDetallePicking.Columns("IdPedidoEnc").Visible = False
                        dgridDetallePicking.Columns("IdPedidoDet").Visible = False
                        dgridDetallePicking.Columns("ClienteDias").Visible = False

                        dgridDetallePicking.Rows(i).Cells("Codigo").Value = bo.Producto.Codigo
                        dgridDetallePicking.Rows(i).Cells("Producto").Value = bo.Producto.Nombre
                        dgridDetallePicking.Rows(i).Cells("Presentacion").Value = bo.Presentacion.Nombre
                        dgridDetallePicking.Rows(i).Cells("UnidadMedida").Value = bo.UnidadMedida.Nombre
                        dgridDetallePicking.Rows(i).Cells("Estado").Value = bo.ProductoEstado.Nombre
                        dgridDetallePicking.Rows(i).Cells("Cantidad").Value = lCantidad
                        dgridDetallePicking.Rows(i).Cells("CantidadRecibida").Value = lCantidadRecibida

                    Next

                Next

            Next

            'grdListaPickingD.ResumeLayout()            

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Sub

    Private Sub mnuProcesar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuProcesar.ItemClick

        Dim pListBeStockRes As New List(Of clsBeStock_res)
        Dim vContinuar As Boolean = True

        Try

            If BePickingEnc.Estado = "" AndAlso lblEstado.Text = "Nuevo" Then
                Guardar_Picking()
                Exit Sub
            End If

            Dim pedidosUnicos As New HashSet(Of Integer)(From item In BePickingEnc.ListaPickingDet
                                                         Select item.IdPedidoEnc)
            Dim pedidosUnicosList As List(Of Integer) = pedidosUnicos.ToList()

            For Each pedido In pedidosUnicosList

                If clsLnTrans_pe_enc.Tiene_Manufactura_Asociada_Sin_Finalizar(pedido) Then
                    XtraMessageBox.Show("El pedido " & pedido & " está asociado a un proceso de manufactura sin finalizar, no se puede procesar",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                    Return
                End If

            Next

            If XtraMessageBox.Show("¿Procesar picking desde el BOF?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Existen_Diferencias_Memoria_vs_BD(vContinuar)

                If Not vContinuar Then Return

                pListBeStockRes = clsLnStock_res.Get_All_By_IdPickingEnc(BePickingEnc.IdPickingEnc)

                '#EJC20180926_0437PM:Validar que tenga stock_res para procesar
                If pListBeStockRes.Count = 0 Then
                    Throw New Exception("No se puede procesar el picking, el detalle de stock_res está vacío")
                End If

                BePickingEnc.procesado_bof = True
                BePickingEnc.verifica_auto = chkverifica_auto.Checked

                '#EJC20171021_0437PM: Cambié el AP.UsuarioAp.Codigo por AP.UsuarioAp.IdUsuario porque daba error al convertir integer (parámetro esperado) a string (valor enviado en código)
                clsLnTrans_picking_ubic.Procesar_Picking_Desde_BOF(pListBePickingUbic,
                                                                   AP.UsuarioAp.IdUsuario,
                                                                   BeListPickingDet,
                                                                   BePickingEnc,
                                                                   pListBeStockRes)

                '#EJC20171021_0437PM: Mostrar mensaje que se procesó correctamente
                XtraMessageBox.Show("Se procesó el picking y los productos asociados",
                                    Text, MessageBoxButtons.OK,
                                    MessageBoxIcon.Information)

                If Not InvokeListarPicking Is Nothing Then
                    InvokeListarPicking.Invoke 'Actualizar lista de picking
                End If

                If Not InvokeCargarObjetoPedido Is Nothing Then
                    InvokeCargarObjetoPedido.Invoke()
                End If

                If Not InvokeCargarPedido Is Nothing Then

                    Dim clsTrans As New clsTransaccion

                    Try

                        clsTrans.Begin_Transaction()
                        InvokeCargarPedido.Invoke(clsTrans.lConnection, clsTrans.lTransaction)
                        clsTrans.Commit_Transaction()

                    Catch ex As Exception
                        clsTrans.RollBack_Transaction()
                        'ejc, ambiente controlado, no disparar fuegos artificiales.
                    End Try

                End If

                If Modal Then

                    DialogResult = DialogResult.OK '#EJC20171021_0437PM: Cerrar forma después de procesar

                    If BePickingEnc.procesado_bof Then

                        Dim vPedidos = BeListPickingDet _
                                        .Where(Function(x) x IsNot Nothing) _
                                        .Select(Function(x) x.IdPedidoEnc) _
                                        .Distinct() _
                                        .ToList()

                        ' Ejemplo de uso con If como pediste
                        If vPedidos.Count() = 1 Then
                            Dim IdPedidoEnc = vPedidos(0)
                            Dim BePedidoEnc As clsBeTrans_pe_enc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc)
                            If XtraMessageBox.Show("¿Generar Despacho?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                BePedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(BePickingEnc.IdPickingEnc)
                                Nuevo_Despacho(BePedidoEnc)
                            End If
                        End If


                    End If

                Else
                    Close()
                End If

            Else

                '#EJC20171213;: Cerrar forma después de generar el picking si no se procesa.
                XtraMessageBox.Show("Se generó el picking, procese tarea en handheld.",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

                If Not InvokeListarPicking Is Nothing Then
                    InvokeListarPicking.Invoke()
                End If

                If Modal Then
                    DialogResult = DialogResult.OK '#EJC20171021_0437PM: Cerrar forma después de procesar
                Else
                    Close()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Existen_Diferencias_Memoria_vs_BD(ByRef Continuar As Boolean)

        Existen_Diferencias_Memoria_vs_BD = False

        Try

            Continuar = True

            If Not BePickingEnc.IsNew Then

                Dim vBePickingEnc As New clsBeTrans_picking_enc
                vBePickingEnc = clsLnTrans_picking_enc.GetSingle(BePickingEnc.IdPickingEnc)

                Dim vCantidadPickeadaBD As Double = vBePickingEnc.ListaPickingUbic.Sum(Function(b) b.Cantidad_Recibida)
                Dim vCantidadPickeadaMemoria As Double = BePickingEnc.ListaPickingUbic.Sum(Function(b) b.Cantidad_Recibida)

                If vCantidadPickeadaBD <> vCantidadPickeadaMemoria Then

                    If XtraMessageBox.Show("El picking fue modificado (probablemente se procesaron líneas en la HH),
                                            para guardarlo es necesario recargar y aplicar nuevamente los cambios realizados, 
                                            ¿Recargar picking?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        Cargar_Datos()

                    Else

                        Continuar = False

                    End If

                End If

            End If

            Existen_Diferencias_Memoria_vs_BD = Continuar

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmbBodegas_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodegas.EditValueChanged

        Try

            If cmbBodegas.EditValue > -1 Then
                cmbPropietario.Properties.DataSource = Nothing
                IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodegas.EditValue)

                '#EJC20220301: Set BeBodega en Nuevo.
                Dim pIdBodega As Integer = cmbBodegas.EditValue
                BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega)
                Validar_Operadores()
            End If

            Set_IdUbicacion_Picking()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub
    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged
        Try

            If cmbPropietario.ItemIndex > -1 Then
                If cmbBodegas.Text <> "" Then
                    cmbPropietario.Tag = IMS.Get_IdPropietario_By_IdBodega(cmbBodegas.EditValue, cmbPropietario.EditValue)
                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try
    End Sub

    '#CKFK 20180607 11:55 AM Creación de botón para cambiar estado del picking a Pendiente
    Private Sub mnuPendiente_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPendientePicking.ItemClick

        Try

            Dim vEstado As String = clsLnTrans_picking_enc.Get_Estado_By_IdPickingEnc(BePickingEnc.IdPickingEnc)

            If Not vEstado = "Despachado" Then

                If XtraMessageBox.Show("¿Está seguro de modificar el picking a estado pendiente?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    BePickingEnc.Estado = "Pendiente"

                    Dim vMsgError As String = "El usuario" & AP.UsuarioAp.IdUsuario & " cambió el picking " & BePickingEnc.IdPickingEnc & " a estado " & BePickingEnc.Estado
                    'clsLnLog_error_wms.Agregar_Error(vMsgError)
                    clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                          pIdEmpresa:=AP.IdEmpresa,
                                                          pIdBodega:=AP.IdBodega,
                                                          pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                          pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                          pIdPickingEnc:=BePickingEnc.IdPickingEnc)

                    If clsLnTrans_picking_enc.Actualizar_Estado(BePickingEnc) > 0 Then

                        '#AT20240106 Obtener tarea por IdPickingEnc y TipoTarea 8
                        Dim BeTarea = clsLnTarea_hh.Get_Tarea_By_IdTransaccion_And_TipoTarea(BePickingEnc.IdPickingEnc, 8)

                        If BeTarea IsNot Nothing Then
                            BeTarea.IdEstado = 2
                            BeTarea.IdOperadorBodega_Cerro = 0
                            BeTarea.Host_Cerro = ""

                            clsLnTarea_hh.Actualizar_Estado_Tarea_PickingPendiente(BeTarea)
                        End If

                        Cargar_Datos()

                        XtraMessageBox.Show("Se actualizó el picking a estado pendiente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If Not InvokeListarPicking Is Nothing Then
                            InvokeListarPicking.Invoke()
                        End If

                        Close()

                    End If

                End If

            Else

                XtraMessageBox.Show("El picking fue despachado, no se puede cambiar a estado pendiente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub dgridPickingUbic_DoubleClick(sender As Object, e As EventArgs) Handles dgridPickingUbic.DoubleClick
        Process_Linea_Picking()
    End Sub

    Private Sub Imprimir_Vista()

        Try

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
            printLink.Component = dgridPickingUbic
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de ubicaciones de Picking"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdSavePR_Click_1(sender As Object, e As EventArgs) Handles cmdSavePR.Click

        Try

            Dim IdOperadorBodega As Integer = 0
            Dim vSeleccionado As Boolean = False
            Dim vCantidadRegistrosRecorridos As Integer = 0
            Dim rh As Integer = 0

            For i As Integer = 0 To DgridOperadorBodega.DataRowCount - 1

                rh = DgridOperadorBodega.GetRowHandle(i)

                IdOperadorBodega = DgridOperadorBodega.GetRowCellValue(rh, "IdOperadorBodega")
                vSeleccionado = DgridOperadorBodega.GetRowCellValue(rh, "Selección")

                Debug.WriteLine("IdOperadorBodega: " & IdOperadorBodega & " Seleccionado: " & vSeleccionado)

                If IdOperadorBodega = 15 Then
                    Debug.WriteLine("IdOperadorBodega: " & IdOperadorBodega & " Seleccionado: " & vSeleccionado)
                End If

                If Not vSeleccionado Then
                    DgridOperadorBodega.SetRowCellValue(rh, "Selección", True)
                End If

                CheckOperadorPicking(IdOperadorBodega, True)

                vCantidadRegistrosRecorridos += 1

            Next

            Application.DoEvents()

            Debug.WriteLine("vCantidadRegistrosRecorridos: " & vCantidadRegistrosRecorridos)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub cmdDesactivarPresentacion_Click(sender As Object, e As EventArgs) Handles cmdDesactivarPresentacion.Click

        Try

            Dim IdOperadorBodega As Integer = 0
            Dim rh As Integer = 0

            For i As Integer = 0 To DgridOperadorBodega.DataRowCount - 1

                rh = DgridOperadorBodega.GetRowHandle(i)

                IdOperadorBodega = DgridOperadorBodega.GetRowCellValue(rh, "IdOperadorBodega")
                DgridOperadorBodega.SetRowCellValue(rh, "Selección", False)
                'GT 17082021: no se valida, porque el boton desmarca lo seleccionado, no hay que volver a validar
                CheckOperadorPicking(IdOperadorBodega, False)

            Next

            Listar_Operadores(False, True)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub CheckOperadorPicking(ByVal pIdOperadorBodega As Integer, ByVal pSeleccion As Boolean)

        Try

            Dim lIndex As Integer = -1
            Dim d As Integer = pIdOperadorBodega

            lIndex = BeListOp.FindIndex(Function(b) b.IdOperadorBodega = d)

            If lIndex > -1 Then

                If Not pSeleccion Then
                    If BeListOp(lIndex).IdOperadorPicking > 0 AndAlso BeListOp(lIndex).IdPickingEnc > 0 Then
                        clsLnTrans_picking_op.Delete(BeListOp(lIndex).IdOperadorPicking)
                    End If
                    BeListOp.RemoveAt(lIndex)
                End If

            Else
                Dim Obj As New clsBeTrans_picking_op() With {.IdOperadorBodega = pIdOperadorBodega,
                        .User_agr = AP.UsuarioAp.IdUsuario,
                        .Fec_agr = Now,
                        .User_mod = AP.UsuarioAp.IdUsuario,
                        .Fec_mod = Now,
                        .IsNew = True}
                BeListOp.Add(Obj)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub chkDetalleOperador_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkDetalleOperador.CheckedChanged
        If chkDetalleOperador.Checked Then
            dgridDetallePicking.Columns("OperadorBodega").Visible = True
            'dgridDetallePicking.Columns("Operador").Visible = True
        Else
            dgridDetallePicking.Columns("OperadorBodega").Visible = False
            'dgridDetallePicking.Columns("Operador").Visible = False
        End If
    End Sub

    Private Sub chkEmpaqueAGranel_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkEmpaqueAGranel.CheckedChanged

        If chkEmpaquePorTarima.Checked Then
            chkEmpaquePorTarima.Checked = False
        End If

    End Sub

    Private Sub chkEmpaquePorTarima_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkEmpaquePorTarima.CheckedChanged

        If chkEmpaqueAGranel.Checked Then
            chkEmpaqueAGranel.Checked = False
        End If

    End Sub

    '#EJC20220603:Obtener todas las configuraciones de tramos y zonas de picking por empresa.
    Dim vListasOperadorTramoZonaPickingPorBodega As New List(Of clsBeOperador_zona_picking_tramo)
    Private Sub frmPicking_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        CheckForIllegalCrossThreadCalls = False

        '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
        vNombreArchivoLayOutGrid = "dgridPickingUbic.xml"

        If Not PickingAuto Then
            dgridPedidos.Rows.Clear()
            dgridDetallePicking.Rows.Clear()
        End If

        dtmHoraI.Value = Now
        dtmHoraF.Value = Now.AddHours(1)

        Try
            SplashScreenManager.CloseForm(False)
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
        End Try

        Try

            vNombreArchivoLayOutGrid = dgridPickingUbic.Name & ".xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodegas)

            '#CKFK20181001: Colocar bodega por defecto.
            cmbBodegas.EditValue = Integer.Parse(AP.IdBodega)
            cmbBodegas.RefreshEditValue()

            dgridPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgridDetallePicking.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            'chkDetalleOperador_CheckedChanged(Nothing, Nothing)
            If Not PickingAuto Then SetDatataTable()

            '#EJC20220603: Obtener la configuración de zonas de picking (para operadores) para la bodega
            Get_Tramos_Zona_Picking_Por_Bodega()

            IMS.Listar_Muelles(cmbMuelle, cmbBodegas.EditValue)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblC.Text = clsLnTrans_picking_enc.MaxID()
                    lblEstado.Text = "Nuevo"
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now
                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    lnkQuitarPedido.Enabled = False
                    cmdImprimir.Enabled = False
                    mnuProcesar.Enabled = False
                    mnuProcesarLinea.Enabled = False
                    mnuPendientePicking.Enabled = False
                    dtmFechaPicking.DateTime = Today
                    dtmFechaTarea.DateTime = Today
                    mnuPendientePacking.Enabled = False
                    mnuActualizarPicking.Enabled = False
                    mnuDespachado.Enabled = False
                    cmdNoPickeado.Enabled = False
                    cmdNoVerificado.Enabled = False
                    cmdVerificarNuevamente.Enabled = False
                    mnuProcesarLinea.Enabled = False
                    mnuVerificarPickeados.Enabled = False
                    txtReferencia.Text = pReferencia

                    If Not PickingAuto Then
                        BePickingEnc = New clsBeTrans_picking_enc With {.IsNew = True}
                    End If

                    '#GT30042025: infiero que desde un pedido la lista solo maneja un registro
                    If pListaPedidos IsNot Nothing AndAlso pListaPedidos.Count > 0 Then
                        Dim tmpPedidoEnc = pListaPedidos.FirstOrDefault()
                        Dim pPedido = clsLnTrans_pe_enc.Get_Single_By_IdPedidoEnc(tmpPedidoEnc)

                        If pPedido IsNot Nothing AndAlso pPedido.IdMuelle > 0 Then
                            cmbMuelle.EditValue = pPedido.IdMuelle
                        End If
                    End If

                Case TipoTrans.Editar

                    If Not BePickingEnc Is Nothing Then

                        Set_Menu_Edicion()

                        Cargar_Datos()

                        Set_Formato_Grid_Picking_Ubic()

                        If txtReferencia.Text = "" Then
                            If pReferencia IsNot Nothing Then
                                txtReferencia.Text = pReferencia
                            Else
                                txtReferencia.Text = BePickingEnc.Referencia
                            End If
                        End If

                        Lista_Productos_Dañados()

                    End If

            End Select

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                grdvPickingUbic.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

            Validar_Operadores()

            WindowState = FormWindowState.Maximized

            Focus()

            xtpDatosPicking.SelectedTabPage = XtratabPageDato

            txtIdUbicacion.Focus()

            HandleBehaviorDragDropEvents()

            RemoveHandler grdvPickingUbic.RowCellStyle, AddressOf grdPickingUbic_RowCellStyle
            AddHandler grdvPickingUbic.RowCellStyle, AddressOf grdPickingUbic_RowCellStyle

            If Llamado_Desde_Pedido Then
                xtpDatosPicking.SelectedTabPage = XtraTabPageUbicacionPicking
            End If

            Set_Formato_Grid_Picking_Ubic()

            '#GT09032023: bloquear bodega para no mezclar ingresos de distintas bodegas
            cmbBodegas.Enabled = False

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    '#CKFK20250705 Puse en comentario la funcionalidad anterior, ya que el packing actualmente se hace por pedido
    'y no por picking
#Region "Funcionalidad anterior para cambiar estado al packing"
    'Private Sub mnuPendientePacking_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPendientePacking.ItemClick

    '    Try

    '        If pListaPedidos.Count = 1 Then

    '            If BePickingEnc.Estado_Preparacion = "Finalizado" Then

    '                If XtraMessageBox.Show("¿Está seguro de modificar el packing a estado pendiente?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

    '                    If clsLnTrans_picking_enc.Set_Estado_Pendiente_Packing(BePickingEnc.IdPickingEnc) > 0 Then

    '                        Cargar_Datos()

    '                        XtraMessageBox.Show("Se actualizó el packing a estado pendiente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

    '                    End If

    '                End If

    '            Else
    '                XtraMessageBox.Show("El packing no se puede cambiar a estado pendiente, el estado actual es: " & BePickingEnc.Estado_Preparacion, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            End If

    '        Else

    '        End If

    '    Catch ex As Exception

    '        XtraMessageBox.Show(ex.Message,
    '        Text,
    '        MessageBoxButtons.OK,
    '        MessageBoxIcon.Error)

    '        Dim vMsgError As String = ex.Message
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)

    '    End Try

    'End Sub
#End Region

    Private ReleaseRowPacking As Integer = -1
    Private Sub mnuPendientePacking_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPendientePacking.ItemClick
        mnuPendientePacking.Enabled = False

        Try
            ' Validación de fila seleccionada
            If ReleaseRowPacking <= -1 Then
                XtraMessageBox.Show("Seleccione el pedido que va a poner en estado pendiente de packing", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Cargar valores del grid
            Get_ValoresGrid(ReleaseRowPacking)

            ' Validación de ID de pedido
            If Val(pIdPedidoEnc.Value) <= 0 Then Exit Sub

            ' Iniciar transacción
            Dim clsTrans As New clsTransaccion
            clsTrans.Begin_Transaction()

            Try

                'Validar si el pedido TienePacking
                If Not clsLnTrans_pe_enc.Tiene_Packing(pIdPedidoEnc.Value, clsTrans.lConnection, clsTrans.lTransaction) Then
                    XtraMessageBox.Show("El pedido no tiene packing activo.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                ' Validar si el packing no está despachado
                If clsLnTrans_pe_enc.Packing_Finalizado(pIdPedidoEnc.Value,
                                                        clsTrans.lConnection,
                                                        clsTrans.lTransaction) Then
                    XtraMessageBox.Show("El pedido ya fue despachado, no se puede activar el packing.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                ' Intentar eliminar el pedido del picking
                Dim actualizado As Boolean = clsLnTrans_packing_enc.Actualizar_Estado_Packing(pIdPedidoEnc.Value,
                                                                                              False,
                                                                                              AP.UsuarioAp.IdUsuario,
                                                                                              clsTrans.lConnection,
                                                                                              clsTrans.lTransaction)

                If actualizado Then

                    XtraMessageBox.Show("El packing fue actualizado a estado pendiente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Else
                    Throw New ApplicationException("No se pudo actualizar el estado del packing.")
                End If

            Catch exTrans As Exception
                clsTrans.RollBack_Transaction()
                Throw ' Propaga al bloque exterior
            End Try

        Catch ex As Exception
            XtraMessageBox.Show("Error al intentar cambiar el estado al packing del pedido: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Finally
            mnuPendientePacking.Enabled = True
            ReleaseRowPacking = -1
        End Try

    End Sub

    Private Sub BWDatosPicking_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BWDatosPicking.DoWork

        Try

            Select Case Modo

                Case TipoTrans.Editar

                    Cargar_Datos()

            End Select


        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
        End Try

    End Sub

    Private Sub tmrActualizarDatosPicking_Tick(sender As Object, e As EventArgs) Handles tmrActualizarDatosPicking.Tick

        Try

            If Not BePickingEnc Is Nothing Then

                If Not BePickingEnc.Estado = "Despachado" Then

                    Select Case Modo

                        Case TipoTrans.Editar

                            tmrActualizarDatosPicking.Enabled = False

                            If Not IsClosing Then
                                Cargar_Datos()
                            End If

                    End Select

                Else
                    tmrActualizarDatosPicking.Enabled = False
                End If

            End If

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        Finally
            tmrActualizarDatosPicking.Enabled = True
        End Try

    End Sub

    Private Sub frmPicking_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        IsClosing = True
    End Sub

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGrid As String = ""

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            If File.Exists(vNombreArchivoLayOutGrid) Then
                File.Delete(vNombreArchivoLayOutGrid)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

            XtraMessageBox.Show("Diseño de grid eliminado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub mnuActualizarPicking_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarPicking.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub mnuDespachado_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDespachado.ItemClick

        Try
            '#GT02102025: se añade el etado Pendiente
            If BePickingEnc.Estado = "Verificado" OrElse BePickingEnc.Estado = "Procesado" OrElse BePickingEnc.Estado = "Pendiente" Then

                If BePickingEnc.Estado = "Verificado" OrElse BePickingEnc.Estado = "Procesado" Then

                    If XtraMessageBox.Show("¿Modificar picking a estado despachado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then



                        '#GT02102025: validación especial para PENDIENTE,, requiere confirmar que no tenga reserva el pedido y por lo tanto que no sea pedido consolidado
                        If BePickingEnc.Estado = "Pendiente" Then

                            Dim listaPickingDet = BePickingEnc.ListaPickingDet
                            'Dim EsPickingConsolidado As Boolean = listaPickingDet _
                            '                                    .GroupBy(Function(x) x.IdPedidoEnc) _
                            '                                    .Any(Function(g) g.Count() > 1)

                            Dim EsPickingConsolidado As Boolean = listaPickingDet _
                                                                   .Select(Function(x) x.IdPedidoEnc) _
                                                                   .Distinct() _
                                                                   .Count() > 1


                            If Not EsPickingConsolidado Then

                                Dim IdPedidoEnc As Integer? = listaPickingDet _
                                                                .Select(Function(x) x.IdPedidoEnc) _
                                                                .Distinct() _
                                                                .SingleOrDefault()

                                Dim stockres As Integer = clsLnTrans_pe_enc.Get_StockRes_By_IdPedido(IdPedidoEnc)

                                If stockres > 0 Then
                                    XtraMessageBox.Show("No se puede cambiar a estado despachado, hay reserva asociada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If

                            Else
                                XtraMessageBox.Show("No se puede cambiar a estado despachado, el picking pertenece a un pedido consolidado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                        End If

                        If clsLnTrans_picking_enc.Actualizar_Estado(BePickingEnc) > 0 Then

                            BePickingEnc.Estado = "Despachado"

                    If clsLnTrans_picking_enc.Actualizar_Estado(BePickingEnc) > 0 Then

                        Cargar_Datos()

                        If Not InvokeListarPicking Is Nothing Then
                            InvokeListarPicking.Invoke()
                        End If

                        Close()

                    End If

                End If

            Else

                XtraMessageBox.Show("No se puede cambiar a estado despachado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Process_Linea_Picking()

        Dim Dr As DataRowView = grdvPickingUbic.GetFocusedRow
        Dim lSelectionIndex As Integer = grdvPickingUbic.FocusedRowHandle
        Dim IdStockRes As Integer = IIf(IsDBNull(Dr.Item("IdStockRes")), 0, Dr.Item("IdStockRes"))
        Dim vCodigoProducto As String = IIf(IsDBNull(Dr.Item("Código")), 0, Dr.Item("Código"))
        Dim vNombreProducto As String = IIf(IsDBNull(Dr.Item("Producto")), 0, Dr.Item("Producto"))
        Dim vCantidadPedidaPres As Double = IIf(IsDBNull(Dr.Item("Cant_Ped_Pres")), 0, Dr.Item("Cant_Ped_Pres"))
        Dim vCantidadPedidaUMBas As Double = IIf(IsDBNull(Dr.Item("Cant_Ped_UmBas")), 0, Dr.Item("Cant_Ped_UmBas"))
        Dim vUnidadMedida As String = IIf(IsDBNull(Dr.Item("Unidad_Medida")), "", Dr.Item("Unidad_Medida"))
        Dim vPedido As Integer = IIf(IsDBNull(Dr.Item("Pedido")), 0, Dr.Item("Pedido"))
        Dim pListBeStockRes As New List(Of clsBeStock_res)
        Dim vContinuar As Boolean = True

        Try

            If clsLnTrans_pe_enc.Tiene_Manufactura_Asociada_Sin_Finalizar(vPedido) Then
                XtraMessageBox.Show("Esta línea pertenece a un pedido con proceso de manufactura sin finalizar, no se puede procesar",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
                Return
            End If

            Dim vMensaje As String = "¿Procesar línea de picking para el código: " & vCodigoProducto & " - " & vNombreProducto &
                    " Solicidado UMBas: " & vCantidadPedidaUMBas & " (" & vCantidadPedidaPres & " en Pres.)?"

            If XtraMessageBox.Show(vMensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Existen_Diferencias_Memoria_vs_BD(vContinuar)

                If Not vContinuar Then Return

                If Not IdStockRes = 0 Then

                    pListBeStockRes = clsLnStock_res.Get_Single_By_IdStockRes(IdStockRes)

                    If Not pListBeStockRes Is Nothing Then

                        If pListBeStockRes.Count > 0 Then

                            Dim BePickingUbic As New clsBeTrans_picking_ubic
                            BePickingUbic = clsLnTrans_picking_ubic.Get_Single_By_IdStockRes_And_IdPickingEnc(IdStockRes,
                                                                                                              BePickingEnc.IdPickingEnc,
                                                                                                              BePickingEnc.IdBodega)

                            If Not BePickingUbic Is Nothing Then

                                Dim vlPickingUbicByIdStockRes As New List(Of clsBeTrans_picking_ubic) From {
                                BePickingUbic}

                                clsLnTrans_picking_ubic.Procesar_Picking_Desde_BOF(vlPickingUbicByIdStockRes,
                                                                                   AP.UsuarioAp.IdUsuario,
                                                                                   BeListPickingDet,
                                                                                   BePickingEnc,
                                                                                   pListBeStockRes)

                                '#EJC20171021_0437PM: Mostrar mensaje que se procesó correctamente
                                XtraMessageBox.Show("Se procesó la línea de picking",
                                                    Text,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information)

                                'Set_Stock_Res(pListBeStockRes(0).IdPedido)

                                'Set_Formato_Grid_Picking_Ubic()

                                Cargar_Datos()

                            Else
                                Throw New Exception("ERROR_20250419: No se pudo obtener el picking_ubic.")
                            End If

                        Else
                            Throw New Exception("ERROR_202209081302A: Error al procesar la línea, no se obtuvo el stock reservado asociado (Count =0)")
                        End If

                    Else
                        Throw New Exception("ERROR_202209081302B: Error al procesar la línea, no se obtuvo el stock reservado asociado (lista is nothing)")
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
        End Try

    End Sub

    Private Sub mnuProcesarLinea_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuProcesarLinea.ItemClick
        Process_Linea_Picking()
    End Sub

    Private Sub cmdUbicRes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdUbicRes.ItemClick

        Try

            Genera_Reporte_Ubicaciones_Resumido()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Genera_Reporte_Ubicaciones_Resumido()

        Try
            Dim Rep As New rptUbicacionesPickingResumido
            Rep.DataSource = clsLnTrans_picking_ubic.Get_Reporte_Ubicaciones_Resumido(BePickingEnc.IdPickingEnc)
            Rep.DataMember = "Result"
            Rep.ShowPreview()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdNoPickeado_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdNoPickeado.ItemClick
        Linea_No_Pickeada()
    End Sub

    Private Sub Linea_No_Pickeada()

        Try

            Dim Dr As DataRowView = grdvPickingUbic.GetFocusedRow
            Dim lSelectionIndex As Integer = grdvPickingUbic.FocusedRowHandle
            Dim IdStockRes As Integer = IIf(IsDBNull(Dr.Item("IdStockRes")), 0, Dr.Item("IdStockRes"))
            Dim vCodigoProducto As String = IIf(IsDBNull(Dr.Item("Código")), 0, Dr.Item("Código"))
            Dim vNombreProducto As String = IIf(IsDBNull(Dr.Item("Producto")), 0, Dr.Item("Producto"))

            If Dr Is Nothing Then Return

            Dim vContinuar As Boolean = True

            Dim vMensaje As String = "¿Marcar línea de picking para el código: " & vCodigoProducto & " - " & vNombreProducto &
                " como no pickeada?"

            If XtraMessageBox.Show(vMensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Not IdStockRes = 0 Then

                    Existen_Diferencias_Memoria_vs_BD(vContinuar)

                    If Not vContinuar Then Return

                    Dim BePickingUbic As New clsBeTrans_picking_ubic
                    BePickingUbic = pListBePickingUbic.Where(Function(x) x.IdStockRes = IdStockRes).First()

                    clsLnTrans_picking_ubic.Marcar_Linea_No_Pickeada(BePickingUbic,
                                                                     AP.UsuarioAp.IdUsuario)

                    Cargar_Datos()

                    '#EJC20171021_0437PM: Mostrar mensaje que se procesó correctamente
                    XtraMessageBox.Show("Se marcó línea de picking como no pickeada",
                                        Text,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information)

                End If

            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub cmdNoVerificado_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdNoVerificado.ItemClick
        Linea_No_Verificada()
    End Sub

    Private Sub Linea_No_Verificada()

        Try

            Dim Dr As DataRowView = grdvPickingUbic.GetFocusedRow
            Dim lSelectionIndex As Integer = grdvPickingUbic.FocusedRowHandle
            Dim IdStockRes As Integer = IIf(IsDBNull(Dr.Item("IdStockRes")), 0, Dr.Item("IdStockRes"))
            Dim vCodigoProducto As String = IIf(IsDBNull(Dr.Item("Código")), 0, Dr.Item("Código"))
            Dim vNombreProducto As String = IIf(IsDBNull(Dr.Item("Producto")), 0, Dr.Item("Producto"))

            If Dr Is Nothing Then Return

            Dim vContinuar As Boolean = True

            Dim vMensaje As String = "¿Marcar línea de picking para el código: " & vCodigoProducto & " - " & vNombreProducto &
                    " como no verificada?"

            If XtraMessageBox.Show(vMensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Not IdStockRes = 0 Then

                    Existen_Diferencias_Memoria_vs_BD(vContinuar)

                    If Not vContinuar Then Return

                    Dim BePickingUbic As New clsBeTrans_picking_ubic
                    BePickingUbic = pListBePickingUbic.Where(Function(x) x.IdStockRes = IdStockRes).FirstOrDefault()

                    clsLnTrans_picking_ubic.Marcar_Linea_No_Verificada(BePickingUbic,
                                                                       AP.UsuarioAp.IdUsuario)

                    Cargar_Datos()

                    XtraMessageBox.Show("Se marcó línea de picking como no verificada",
                                            Text,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information)

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub cmdVerificarNuevamente_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdVerificarNuevamente.ItemClick
        Verificar_Nuevamente()
    End Sub

    Private Sub Verificar_Nuevamente()

        Try

            Dim vCantidadVerificada As Double = BePickingEnc.ListaPickingUbic.Sum(Function(b) b.Cantidad_Verificada)
            Dim vMensaje As String = "¿Marcar picking como no verificado?"
            Dim vContinuar As Boolean = True
            Dim clsTrans As New clsTransaccion

            If XtraMessageBox.Show(vMensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Existen_Diferencias_Memoria_vs_BD(vContinuar)

                If Not vContinuar Then Return

                If vCantidadVerificada = 0 Then

                    If XtraMessageBox.Show("El picking no tiene líneas verificadas", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Return
                    End If

                End If

                Try

                    clsTrans.Begin_Transaction()

                    If clsLnTrans_packing_enc.Tiene_Packing_By_IdPicking(lblC.Text, clsTrans.lConnection, clsTrans.lTransaction) Then
                        If XtraMessageBox.Show("El picking tiene pedidos con packing asociados, ¿continuar con el proceso?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            If XtraMessageBox.Show("Tendremos que eliminar los packing asociados, ¿continuar con el proceso?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Return
                            End If
                        End If

                    End If

                    Dim ListaIdPedidoEncUnicos As New List(Of Integer)

                    For Each PickingUbic In pListBePickingUbic

                        clsLnTrans_picking_ubic.Marcar_Linea_No_Verificada(PickingUbic, AP.UsuarioAp.IdUsuario, clsTrans.lConnection, clsTrans.lTransaction)

                        If Not ListaIdPedidoEncUnicos.Contains(PickingUbic.IdPedidoEnc) Then
                            ListaIdPedidoEncUnicos.Add(PickingUbic.IdPedidoEnc)
                        End If

                    Next

                    For Each vIdPedidoEnc In ListaIdPedidoEncUnicos
                        clsLnTrans_pe_enc.Actualizar_Estado_Pendiente(vIdPedidoEnc, AP.UsuarioAp.IdUsuario, clsTrans.lConnection, clsTrans.lTransaction)
                    Next

                    clsLnTrans_packing_enc.Eliminar_All_By_IdPIckingEnc(lblC.Text, clsTrans.lConnection, clsTrans.lTransaction)

                    clsTrans.Commit_Transaction()

                    Cargar_Datos()

                    XtraMessageBox.Show("Se marcó picking como no verificado",
                                            Text,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information)


                Catch ex As Exception
                    clsTrans.RollBack_Transaction()
                    Throw ex
                End Try

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub grdvPickingUbic_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdvPickingUbic.RowStyle

        Try

            grdvPickingUbic.OptionsBehavior.Editable = False
            grdvPickingUbic.OptionsSelection.EnableAppearanceFocusedCell = False
            grdvPickingUbic.OptionsSelection.MultiSelect = True
            grdvPickingUbic.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
            grdvPickingUbic.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            grdvPickingUbic.OptionsSelection.EnableAppearanceFocusedRow = True
            grdvPickingUbic.OptionsSelection.EnableAppearanceHideSelection = True
            grdvPickingUbic.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            grdvPickingUbic.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            grdvPickingUbic.Appearance.FocusedRow.ForeColor = Color.White
            grdvPickingUbic.Appearance.SelectedRow.ForeColor = Color.White
            grdvPickingUbic.Appearance.SelectedRow.Options.UseBackColor = True
            grdvPickingUbic.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try
    End Sub

    Public Sub HandleBehaviorDragDropEvents()

        AddHandler DragDropManager.Default.DragDrop, AddressOf PickingUbicBehavior_DragDrop
        AddHandler DragDropManager.Default.DragOver, AddressOf Behavior_DragOver

    End Sub
    Private Sub OperadorDragDropEvent_DragOver(ByVal sender As Object, ByVal e As DragOverEventArgs) Handles OperadorDragDropEvent.DragOver

        Dim args As DragOverGridEventArgs = DragOverGridEventArgs.GetDragOverGridEventArgs(e)
        e.InsertType = args.InsertType
        e.InsertIndicatorLocation = args.InsertIndicatorLocation
        e.Action = args.Action
        Cursor.Current = args.Cursor
        args.Handled = True

    End Sub

    Private Sub Behavior_DragOver(ByVal sender As Object, ByVal e As DragOverEventArgs)

        Dim args As DragOverGridEventArgs = DragOverGridEventArgs.GetDragOverGridEventArgs(e)
        e.InsertType = args.InsertType
        e.InsertIndicatorLocation = args.InsertIndicatorLocation
        e.Action = args.Action
        Cursor.Current = args.Cursor
        args.Handled = True

    End Sub

    Private Sub PickingUbicBehavior_DragDrop(ByVal sender As Object, ByVal e As DragDropEventArgs) Handles pickingUbicDragDropEvent.DragDrop

        Try

            Dim targetGrid As GridView = TryCast(e.Target, GridView)
            Dim sourceGrid As GridView = TryCast(e.Source, GridView)

            If Not (sourceGrid.Name = "grdvPickingUbic" AndAlso targetGrid.Name = "DgridOperadorBodega") Then
                Exit Sub
            End If

            Dim sourceTable As DataTable = TryCast(sourceGrid.GridControl.DataSource, DataTable)
            Dim hitPoint As Point = targetGrid.GridControl.PointToClient(Cursor.Position)
            Dim hitInfo As GridHitInfo = targetGrid.CalcHitInfo(hitPoint)
            Dim sourceHandles As Integer() = e.GetData(Of Integer())()
            Dim targetRowHandle As Integer = hitInfo.RowHandle
            Dim targetRowIndex As Integer = targetGrid.GetDataSourceRowIndex(targetRowHandle)
            Dim draggedRows As List(Of DataRow) = New List(Of DataRow)()

            Dim vIdStockRes As Integer = 0
            Dim vNomOperador As String = ""
            Dim vIdOperadorBodegaDestino As Integer = 0
            Dim vIdOperadorRec As Integer = 0

            For Each sourceHandle As Integer In sourceHandles

                vIdStockRes = sourceGrid.GetRowCellValue(sourceHandle, "IdStockRes")
                vIdOperadorBodegaDestino = IIf(IsDBNull(targetGrid.GetRowCellValue(targetRowHandle, "IdOperadorBodega")), 0, targetGrid.GetRowCellValue(targetRowHandle, "IdOperadorBodega"))
                vIdOperadorRec = IIf(IsDBNull(targetGrid.GetRowCellValue(targetRowHandle, "IdOperadorRec")), 0, targetGrid.GetRowCellValue(targetRowHandle, "IdOperadorRec"))
                vNomOperador = IIf(IsDBNull(targetGrid.GetRowCellValue(targetRowHandle, "Operador")), "", targetGrid.GetRowCellValue(targetRowHandle, "Operador"))

                Try

                    If Not vNomOperador Is Nothing Then

                        pListObjSP.Find(Function(x) x.IdStockRes = vIdStockRes).IdOperadorBodega_Asignado = vIdOperadorBodegaDestino
                        sourceGrid.SetRowCellValue(sourceHandle, "Operador_Asignado", vNomOperador)

                        If Not DTStockRes Is Nothing Then
                            DTStockRes.Rows(sourceHandle).Item("Operador_Asignado") = vNomOperador
                            targetGrid.SetRowCellValue(targetRowHandle, "Selección", True)
                        End If

                    End If

                Catch ex As Exception
                    '#MECR23102025: Se agrego bitacora para logs de picking
                    Dim vMsgError As String = ex.Message
                    'clsLnLog_error_wms.Agregar_Error(vMsgError)
                    clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                          pIdEmpresa:=AP.IdEmpresa,
                                                          pIdBodega:=AP.IdBodega,
                                                          pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                          pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                          pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                          pStackTrace:=ex.StackTrace)
                End Try

            Next

            e.Handled = True

            sourceGrid.RefreshData()

            'Set_Rule_Format()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub mnuPorAtributo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPorAtributo.ItemClick

        XtraMessageBox.Show("La fecha aproximada de esta funcionalidad es: 12/12/2022, lo siento EJC.",
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)

    End Sub

    Private Sub mnuInteligenciaArtificial_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInteligenciaArtificial.ItemClick

        XtraMessageBox.Show("La fecha aproximada de esta funcionalidad es: 15/04/2023, lo siento EJC.",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation)

    End Sub

    Private Sub grdPickingUbic_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles grdvPickingUbic.RowCellStyle

        Try

            Dim View1 As GridView = sender

            If View1.Columns Is Nothing Then Exit Sub
            If View1.Columns.Count = 0 Then Exit Sub

            Dim Operador_Asignado As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Operador_Asignado"))), "0", View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Operador_Asignado")))
            Dim IdStockRes As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("IdStockRes"))), 0, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("IdStockRes")))

            Dim vCantidadSolicitada As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Cant_Ped_UmBas"))), 0, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Cant_Ped_UmBas")))
            Dim vCantidadPickeada As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Cant_Pick_UMBas"))), 0, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Cant_Pick_UMBas")))
            Dim vCantidadVerificada As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Cant_Veri_UMBas"))), 0, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Cant_Veri_UMBas")))
            Dim vTieneManufactura As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Manufactura"))), False, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Manufactura")))

            If IdStockRes <> "" Then

                If Val(IdStockRes) > 0 Then

                    If Operador_Asignado = "" Then

                        If (Val(vCantidadSolicitada) > Val(vCantidadPickeada) OrElse Val(vCantidadPickeada) = 0) Then

                            e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                            e.Appearance.BackColor = Color.Salmon
                            e.Appearance.BackColor2 = Color.SeaShell
                            e.Appearance.ForeColor = Color.Black

                        ElseIf Val(vCantidadVerificada) < Val(vCantidadPickeada) Then

                            If Val(vCantidadVerificada) = 0 And vTieneManufactura = "Checked" Then
                                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                                e.Appearance.BackColor = Color.LightSalmon
                                e.Appearance.BackColor2 = Color.White
                                e.Appearance.ForeColor = Color.Black
                            Else
                                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                                e.Appearance.BackColor = Color.Yellow
                                e.Appearance.BackColor2 = Color.White
                                e.Appearance.ForeColor = Color.Black
                            End If

                        ElseIf Val(vCantidadPickeada) = Val(vCantidadVerificada) Then

                            e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                            e.Appearance.BackColor = Color.LimeGreen
                            e.Appearance.BackColor2 = Color.White
                            e.Appearance.ForeColor = Color.Black

                        ElseIf Val(vCantidadPickeada) < Val(vCantidadVerificada) AndAlso Val(vCantidadVerificada) = 0 Then

                        End If


                    Else

                        e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                        e.Appearance.BackColor = Color.LightGreen
                        e.Appearance.BackColor2 = Color.White
                        e.Appearance.ForeColor = Color.Black

                    End If

                End If

            End If



        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                  Text,
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub lnkAgregarPedido_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles lnkAgregarPedido.ItemClick

        Try

            lnkAgregarPedido.Enabled = False

            '#CKFK20171026_0721PM: Le envío a la forma los pedidos ya incluidos en el Picking
            Dim bo As New frmPedidoDetalleBuscador() With {.pListaPedidos = pListaPedidos, .IdBodega = cmbBodegas.EditValue}
            bo.ShowDialog()

            If Not bo.pBePedidoEnc Is Nothing Then

                If bo.pBePedidoEnc.IdCliente <> 0 AndAlso bo.pBePedidoEnc.IdPropietarioBodega <> 0 Then

                    chkverifica_auto.Checked = bo.pBePedidoEnc.TipoPedido.Verificar
                    chkFotografiaVerificacion.Checked = bo.pBePedidoEnc.TipoPedido.Fotografia_Verificacion
                    chkEmpaquePorTarima.Checked = bo.pBePedidoEnc.TipoPedido.Empaque_Tarima

                    bo.pBePedidoEnc.Detalle = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(bo.pBePedidoEnc.IdPedidoEnc)

                    If bo.pBePedidoEnc.Detalle IsNot Nothing AndAlso bo.pBePedidoEnc.Detalle.Count > 0 Then

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Pedido")
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
                        Set_Stock_Res(bo.pBePedidoEnc.IdPedidoEnc)

                        '#CKFK20171026_0721PM: Agregué lista de pedidos donde se adicionan los pedidos ya incluidos en el Picking
                        pListaPedidos.Add(bo.pBePedidoEnc.IdPedidoEnc)
                        For Each BeTransPeDet As clsBeTrans_pe_det In bo.pBePedidoEnc.Detalle
                            Application.DoEvents()

                            xtpDatosPicking.SelectedTabPage = XtratabPagePedido

                            BeTransPeDet.ListaStockRes = clsLnTrans_pe_det.Get_All_Stock_Res_By_IdPedidoDet(BeTransPeDet.IdPedidoDet, BeTransPeDet.IdPedidoEnc)
                            SetProducto(BeTransPeDet, bo.pBePedidoEnc)
                        Next

                        Set_Formato_Grid_Picking_Ubic()
                        RemoveHandler grdvPickingUbic.RowCellStyle, AddressOf grdPickingUbic_RowCellStyle
                        AddHandler grdvPickingUbic.RowCellStyle, AddressOf grdPickingUbic_RowCellStyle

                    End If

                Else

                    XtraMessageBox.Show("El pedido no se creó correctamente: No tiene asociado cliente o propietario, corríjalo desde el mantenimiento de pedidos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else

                '#EJC20220613: No se seleccionó ningún pedido, no complain, no mostrar mensaje.
                'XtraMessageBox.Show("El pedido no se creó correctamente: No tiene asociado cliente o propietario, corríjalo desde el mantenimiento de pedidos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

            SplashScreenManager.CloseForm(False)
            Cursor = Cursors.Default
            xtpDatosPicking.SelectedTabPage = XtratabPagePedido
        Catch ex As Exception
            Cursor = Cursors.Default
            SplashScreenManager.CloseForm(False)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            lnkAgregarPedido.Enabled = True
        End Try

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuZonasPicking.ItemClick

#Region "Asignar automáticamente operador en base a reglas"

        '#EJC20220603: Asignacióna automática de operador.
        'Deshabilitado para que el operador lo haga con los botones.
        'ObjStock.IdOperadorBodega_Asignado = 0

        'BeUbicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(ObjStock.IdUbicacion,
        '                                                                           ObjStock.IdBodega)

        'If Not BeUbicacion Is Nothing Then

        '    '#EJC20220603:Determinar si para todas las zonas de picking (para operadores) por bodega,
        '    'Existe una tal, que cumpla con las condiciones de la ubicación
        '    'En otras palabras diría Jose Luis: vamos a buscar si nuestro elemento, está dentro de un conjunto.
        '    vSingularidadTramoUbicacionEnZP = lListasTramoZonaPickingPorBodega.FindAll(Function(x) x.IdBodega = BeUbicacion.IdBodega _
        '                                                                                AndAlso x.Min_x >= BeUbicacion.Indice_x AndAlso BeUbicacion.Indice_x <= x.Max_x _
        '                                                                                AndAlso x.Min_y >= BeUbicacion.Nivel AndAlso BeUbicacion.Nivel <= x.Max_y)


        '    If Not vSingularidadTramoUbicacionEnZP Is Nothing Then

        '        'Si existe un X|X Necesitamos averiguar si el operador tiene zonas de picking definidas.                                            
        '        If Not vListasOperadorTramoZonaPickingPorBodega Is Nothing Then

        '            'Ahora busquemos una coincidencia, entre las zonas de picking encontradas, si alguna está asociada a algún operador.
        '            'A traves del IdZonaPikcingTramo.
        '            For Each Zp In vSingularidadTramoUbicacionEnZP

        '                SingleBeZonaPIckingTramoOP = vListasOperadorTramoZonaPickingPorBodega.Find(Function(x) x.IdZonaPickingTramo = Zp.IdZonaPickingTramo)

        '                'Si existe esta coincidencia, quiere decir que un operador, está asignado en una zona de picking.
        '                'De la que se necesita tomar producto, solo nos queda averiguar el IdOperadorBodega asociado a ese operador.
        '                If Not SingleBeZonaPIckingTramoOP Is Nothing Then

        '                    '#EJC20220603: Asignacióna automática de operador A.
        '                    ObjStock.IdOperadorBodega_Asignado = clsLnOperador_bodega.Get_IdOperadorBodega_By_IdOperador(SingleBeZonaPIckingTramoOP.IdOperador,
        '                                                                                                                 cmbBodegas.EditValue)


        '                    '#EJC20220606:En el futuro aquí hacer el balanceo de carga antes de la asignación, de momento, vamos a joder al mismo (siempre jaja)
        '                    Exit For

        '                End If


        '            Next

        '        Else
        '            'De lo contrario, solo quiere decir que la zona de picking está creada, pero no hay asignaciones por operador.
        '        End If

        '    End If

        '    If ObjStock.IdOperadorBodega_Asignado = 0 Then
        '        Console.WriteLine("No existe operador definido para la singularidad.")
        '    End If


        'End If

        'If Not ObjStock.IdOperadorBodega_Asignado = 0 Then

        '    BeOperadorBodega = clsLnOperador_bodega.Get_OperadorBodega_By_IdOperadoBodega(ObjStock.IdOperadorBodega_Asignado)

        '    If Not BeOperadorBodega Is Nothing Then
        '        vNombreOperadorAsingadoAuto = BeOperadorBodega.Operador.Nombres & " " & BeOperadorBodega.Operador.Apellidos
        '    End If

        'End If
#End Region

    End Sub


    Private Function Asignar_Nombre_Operador_Grid(ByVal pIdStockRes As Integer, ByVal pNombreOperador As String) As Boolean

        Asignar_Nombre_Operador_Grid = False

        Try

            Dim rh As Integer = 0
            Dim vIdStockResGrid As Integer = 0

            For i As Integer = 0 To grdvPickingUbic.RowCount - 1

                rh = grdvPickingUbic.GetRowHandle(i)
                vIdStockResGrid = IIf(IsDBNull(grdvPickingUbic.GetRowCellValue(rh, "IdStockRes")), 0, grdvPickingUbic.GetRowCellValue(rh, "IdStockRes"))

                If vIdStockResGrid = pIdStockRes Then
                    grdvPickingUbic.SetRowCellValue(rh, "Operador_Asignado", pNombreOperador)
                    Asignar_Nombre_Operador_Grid = True
                    Exit For
                End If

            Next


        Catch ex As Exception

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Function
    Private Function Repartir_Lineas_Picking_En_Operadores() As Boolean

        Repartir_Lineas_Picking_En_Operadores = False

        Try

            Dim lOperadoresPicking As New List(Of clsBeOperador_bodega)
            lOperadoresPicking = clsLnOperador_bodega.Get_All_Operador_For_Picking_By_IdBodega(cmbBodegas.EditValue)

            If Not lOperadoresPicking Is Nothing Then

                If lOperadoresPicking.Count > 0 Then

                    If Not pListBePickingUbic Is Nothing Then

                        If pListBePickingUbic.Count > 0 Then

                            Dim vCantidadLineasARepartir As Integer = pListBePickingUbic.Count - 1
                            Dim vCantidadOperadoresQueHacenPicking As Integer = lOperadoresPicking.Count - 1
                            Dim vPromedioLineasPorOperador As Double = vCantidadLineasARepartir / vCantidadOperadoresQueHacenPicking
                            Dim vCantidadLineasEnteras As Integer = 0
                            Dim vCantidadLineasDecimal As Decimal = 0
                            Dim vCantLineasAsignadas As Integer = 0
                            Dim vMaxLIneasPorOperador As Integer = 0
                            Dim vIndiceOpeador As Integer = 0
                            Dim vNombreOperador As String = ""
                            Dim vContadorLineasAsignadas As Integer = 0

                            If clsLnTrans_picking_ubic.Get_Info_Ubicacion_By_ListaPickingUbic(pListBePickingUbic) Then

                                '#EJC20220611_2244: Si hay cantidad decimal, quiere decir que a un operador le tocarán eventualmente más líneas que a otro.
                                clsPublic.Split_Decimal(vPromedioLineasPorOperador, vCantidadLineasEnteras, vCantidadLineasDecimal)

                                vMaxLIneasPorOperador = vCantidadLineasEnteras

                                Dim vResumenPosicionesPorRack = (From i In pListBePickingUbic
                                                                 Group i By Keys = New With {Key i.Ubicacion.Tramo.IdTramo} Into Group
                                                                 Select New With {.IdTramo = Keys.IdTramo,
                                                                          .Posiciones = Group.Count()}).ToList()

                                Dim vDetalleUbicacionesPorRack As New List(Of clsBeTrans_picking_ubic)

                                Dim lUbicacionesGrafo As New List(Of GrafoUbicacionesPicking)
                                Dim BeGrafoUbicacionesPicking As New GrafoUbicacionesPicking
                                Dim vNodo As Integer = 1

                                For Each Tramo In vResumenPosicionesPorRack

                                    'Buscar todas las ubicaciones de ese tramo.
                                    vDetalleUbicacionesPorRack = pListBePickingUbic.FindAll(Function(x) x.Ubicacion.IdTramo = Tramo.IdTramo).OrderBy(Function(x) x.Ubicacion.Indice_x).ThenBy(Function(x) x.Ubicacion.Nivel).ToList()

                                    Dim vCantidadNodosPorTramo As Integer = 0

                                    If Not vDetalleUbicacionesPorRack Is Nothing Then
                                        vCantidadNodosPorTramo = vDetalleUbicacionesPorRack.Count - 1
                                    End If

                                    For Each U In vDetalleUbicacionesPorRack

                                        BeGrafoUbicacionesPicking = New GrafoUbicacionesPicking()
                                        BeGrafoUbicacionesPicking.IdNodo = vNodo
                                        BeGrafoUbicacionesPicking.IdPickingUbic = U.IdPickingUbic
                                        BeGrafoUbicacionesPicking.IdTramo = U.Ubicacion.IdTramo
                                        BeGrafoUbicacionesPicking.IdUbicacion = U.IdUbicacion
                                        BeGrafoUbicacionesPicking.IndiceX = U.Ubicacion.Indice_x
                                        BeGrafoUbicacionesPicking.IndiceY = U.Ubicacion.Nivel
                                        BeGrafoUbicacionesPicking.AnchoNodo = U.Ubicacion.Ancho

                                        If vNodo = 1 Then
                                            BeGrafoUbicacionesPicking.IdNodoAnterior = 0
                                        Else
                                            BeGrafoUbicacionesPicking.IdNodoAnterior = vNodo - 1
                                        End If

                                        If Not vCantidadNodosPorTramo = (vNodo + 1) Then
                                            BeGrafoUbicacionesPicking.IdNodoSiguiente = vNodo + 1
                                        Else
                                            BeGrafoUbicacionesPicking.IdNodoSiguiente = 0
                                        End If

                                        lUbicacionesGrafo.Add(BeGrafoUbicacionesPicking)

                                        vNodo += 1

                                    Next

                                Next

                                Dim vPosicionesPorRackYColumna = (From i In pListBePickingUbic
                                                                  Group i By Keys = New With {Key i.Ubicacion.Tramo.Descripcion,
                                                                              Key i.Ubicacion.Indice_x,
                                                                              Key i.IdUbicacion} Into Group
                                                                  Select New With {.Tramo = Keys.Descripcion,
                                                                   .Columna = Keys.Indice_x,
                                                                   .Posiciones = Group.Count(Function(x) x.IdUbicacion)}).OrderBy(Function(x) x.Tramo).ThenBy(Function(x) x.Columna)

                                'Hay una cantidad exacta de lineas para cada operador
                                If vCantidadLineasDecimal = 0 Then

                                    'Recorrer la lista de picking
                                    For Each Pu In pListBePickingUbic.OrderBy(Function(X) X.IdUbicacion)

                                        If Not vCantLineasAsignadas = vMaxLIneasPorOperador Then

                                            Pu.IdOperadorBodega_Asignado = lOperadoresPicking(vIndiceOpeador).IdOperadorBodega
                                            vCantLineasAsignadas += 1
                                            vNombreOperador = lOperadoresPicking(vIndiceOpeador).Operador.Nombres
                                            Asignar_Nombre_Operador_Grid(Pu.IdStockRes, vNombreOperador)
                                            vContadorLineasAsignadas += 1

                                        Else

                                            vCantLineasAsignadas = 0 : vIndiceOpeador += 1

                                            If vIndiceOpeador < lOperadoresPicking.Count - 1 Then
                                                Pu.IdOperadorBodega_Asignado = lOperadoresPicking(vIndiceOpeador).IdOperadorBodega
                                                vCantLineasAsignadas += 1
                                                vNombreOperador = lOperadoresPicking(vIndiceOpeador).Operador.Nombres
                                                Asignar_Nombre_Operador_Grid(Pu.IdStockRes, vNombreOperador)
                                                vContadorLineasAsignadas += 1
                                            End If

                                        End If

                                        'sourceGrid.SetRowCellValue(sourceHandle, "Operador_Asignado", vNomOperador)

                                    Next

                                Else
                                    'Hay que repartir las líneas considerando que nos van a sobrar lineas que hay que ponerle demás a algún operador.

                                    If vCantidadLineasEnteras >= 1 Then

                                        'Recorrer la lista de picking
                                        For Each Pu In pListBePickingUbic

                                            If Not vCantLineasAsignadas = vMaxLIneasPorOperador Then

                                                Pu.IdOperadorBodega_Asignado = lOperadoresPicking(vIndiceOpeador).IdOperadorBodega
                                                vCantLineasAsignadas += 1
                                                vNombreOperador = lOperadoresPicking(vIndiceOpeador).Operador.Nombres
                                                Asignar_Nombre_Operador_Grid(Pu.IdStockRes, vNombreOperador)
                                                vContadorLineasAsignadas += 1

                                            Else

                                                vCantLineasAsignadas = 0 : vIndiceOpeador += 1

                                                If vIndiceOpeador <= lOperadoresPicking.Count - 1 Then
                                                    Pu.IdOperadorBodega_Asignado = lOperadoresPicking(vIndiceOpeador).IdOperadorBodega
                                                    vCantLineasAsignadas += 1
                                                    vNombreOperador = lOperadoresPicking(vIndiceOpeador).Operador.Nombres
                                                    Asignar_Nombre_Operador_Grid(Pu.IdStockRes, vNombreOperador)
                                                    vContadorLineasAsignadas += 1
                                                Else
                                                    'Empezar a asignar a los primeros operadores las lineas restantes
                                                    'Lo sé, no muy justo, pero es lo que hay de momento, EJC. 11062022 23:43PM
                                                    vIndiceOpeador = 0 : vCantLineasAsignadas = 0
                                                    Pu.IdOperadorBodega_Asignado = lOperadoresPicking(vIndiceOpeador).IdOperadorBodega
                                                    vCantLineasAsignadas += 1
                                                    vNombreOperador = lOperadoresPicking(vIndiceOpeador).Operador.Nombres
                                                    Asignar_Nombre_Operador_Grid(Pu.IdStockRes, vNombreOperador)
                                                    vContadorLineasAsignadas += 1
                                                End If

                                            End If

                                            Debug.Write("Lineas asignadas: " & vContadorLineasAsignadas)

                                        Next

                                    Else
                                        XtraMessageBox.Show("La cantidad de líneas a repartir por operador es inferior a 1, el sistema no puede comprender este escenario, EJC.",
                                                          Text,
                                                          MessageBoxButtons.OK,
                                                          MessageBoxIcon.Exclamation)
                                        Exit Function
                                    End If

                                End If

                            Else
                                'No se pudo obtener la información de las ubicaciones asociadas al picking.
                            End If

                        Else
                            'No hay líneas de picking?
                        End If

                    Else
                        'No hay líneas de picking?
                    End If

                Else
                    XtraMessageBox.Show("No hay operadores que tengan configuración de picking activa (Operador, flag Pickea = True)",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)
                End If

            Else
                XtraMessageBox.Show("No hay operadores que tengan configuración de picking activa (Operador, flag Pickea = True)",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Function

    Private Sub mnuRepartirOperadores_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuRepartirOperadores.ItemClick

        If XtraMessageBox.Show("¿Repartir líneas de picking entre operadores?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            Repartir_Lineas_Picking_En_Operadores()

        End If


    End Sub

    Private Sub grdvPickingUbic_Layout(sender As Object, e As EventArgs) Handles grdvPickingUbic.Layout

        If Not IsLoading Then Guardar_Layout()

    End Sub
    Private Sub Guardar_Layout()

        Try

            Dim Ms As New MemoryStream
            grdvPickingUbic.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub GridView1_PopupMenuShowing(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing

        Dim view As GridView = TryCast(sender, GridView)

        Try

            If e.MenuType = GridMenuType.Row Then
                If e.HitInfo.Column Is Nothing Then
                    Exit Sub
                End If
                If (e.HitInfo.Column.Name = "colIdPickingUbic") Then
                    Dim rowHandle As Integer = e.HitInfo.RowHandle
                    e.Menu.Items.Clear()
                    Dim item As DXMenuItem = CreateMenuItemTLV04(view, rowHandle)
                    item.BeginGroup = True
                    e.Menu.Items.Add(item)
                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Function CreateMenuItemTLV04(ByVal view As GridView, ByVal rowHandle As Integer) As DXMenuItem
        Dim menuItem As DXMenuItem = New DXMenuItem("Revertir reemplazo de picking.", New EventHandler(AddressOf OnTLV04Click))
        menuItem.Tag = New RowInfo(view, rowHandle)
        'checkItem.ImageOptions.Image = ImageCollection1.Images(1)
        Return menuItem
    End Function

    Private Sub OnTLV04Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim item As DXMenuItem = TryCast(sender, DXMenuItem)
        Dim info As RowInfo = TryCast(item.Tag, RowInfo)
        Dim vIdPickingUbic As Integer = 0

        Try

            vIdPickingUbic = IIf(IsDBNull(GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "colIdPickingUbic")), 0, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "colIdPickingUbic"))

            If Not vIdPickingUbic = 0 Then
                If GridView1.FocusedColumn.Name = "colIdPickingUbic" Then

                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub mnuVerificarPickeados_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuVerificarPickeados.ItemClick

        Dim pListBeStockRes As New List(Of clsBeStock_res)
        Dim vContinuar As Boolean = True

        Try

            If BePickingEnc.Estado = "" AndAlso lblEstado.Text = "Nuevo" Then
                Guardar_Picking()
                Exit Sub
            End If

            pListBeStockRes = clsLnStock_res.Get_All_Pickeados_By_IdPickingEnc(BePickingEnc.IdPickingEnc)

            '#EJC20180926_0437PM:Validar que tenga stock_res para procesar
            If pListBeStockRes.Count = 0 Then
                XtraMessageBox.Show("La lista no contiene productos pickeados",
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
            Else

                Dim pedidosUnicos As New HashSet(Of Integer)(From item In BePickingEnc.ListaPickingDet
                                                             Select item.IdPedidoEnc)
                Dim pedidosUnicosList As List(Of Integer) = pedidosUnicos.ToList()

                For Each pedido In pedidosUnicosList

                    If clsLnTrans_pe_enc.Tiene_Manufactura_Asociada_Sin_Finalizar(pedido) Then
                        XtraMessageBox.Show("El pedido " & pedido & " está asociado a un proceso de manufactura sin finalizar, no se puede procesar",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                        Return
                    End If

                Next

                If XtraMessageBox.Show("¿Verificar productos pickeados?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Existen_Diferencias_Memoria_vs_BD(vContinuar)

                    If Not vContinuar Then Return

                    BePickingEnc.procesado_bof = True
                    BePickingEnc.verifica_auto = chkverifica_auto.Checked

                    '#EJC20171021_0437PM: Cambié el AP.UsuarioAp.Codigo por AP.UsuarioAp.IdUsuario porque daba error al convertir integer (parámetro esperado) a string (valor enviado en código)
                    clsLnTrans_picking_ubic.Procesar_Verificacion_Desde_BOF(pListBePickingUbic,
                                                                            AP.UsuarioAp.IdUsuario,
                                                                            BeListPickingDet,
                                                                            BePickingEnc,
                                                                            pListBeStockRes)

                    '#EJC20171021_0437PM: Mostrar mensaje que se procesó correctamente
                    XtraMessageBox.Show("Se verificaron los productos pickeados",
                                        Text,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information)

                    If Not InvokeListarPicking Is Nothing Then
                        InvokeListarPicking.Invoke 'Actualizar lista de picking
                    End If

                    If Modal Then
                        DialogResult = DialogResult.OK '#EJC20171021_0437PM: Cerrar forma después de procesar
                    Else
                        Close()
                    End If

                Else

                    '#EJC20171213;: Cerrar forma después de generar el picking si no se procesa.
                    XtraMessageBox.Show("Se generó el picking, procese tarea en handheld.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                    If Not InvokeListarPicking Is Nothing Then
                        InvokeListarPicking.Invoke()
                    End If

                    If Modal Then
                        DialogResult = DialogResult.OK '#EJC20171021_0437PM: Cerrar forma después de procesar
                    Else
                        Close()
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

    Class RowInfo
        Public Sub New(ByVal view As GridView, ByVal rowHandle As Integer)
            Me.RowHandle = rowHandle
            Me.View = view
        End Sub

        Public View As GridView
        Public RowHandle As Integer
    End Class

    Public Class GrafoUbicacionesPicking

        Public Property IdPickingUbic = 0
        Public Property IdTramo = 0
        Public Property IdUbicacion = 0
        Public Property IndiceX As Double = 0
        Public Property IndiceY As Double = 0
        Public Property IdNodo As Integer = 0
        Public Property IdNodoAnterior As Integer = 0
        Public Property IdNodoSiguiente As Integer = 0

        Public Property AnchoNodo As Double = 0

    End Class
    Private Sub DgridOperadorBodega_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles DgridOperadorBodega.RowCellStyle

        Try

            Dim View1 As GridView = sender

            If View1.Columns Is Nothing Then Exit Sub
            If View1.Columns.Count = 0 Then Exit Sub

            Dim TieneMarcaje As Boolean = False 'IIf(IsDBNull(View1.GetRowCellValue(e.RowHandle, "IngresoHH")), False, View1.GetRowCellValue(e.RowHandle, "IngresoHH"))

            If TieneMarcaje Then 'Ya se logueo en la HH
                e.Appearance.BackColor = Color.LightGreen
                e.Appearance.BackColor2 = Color.White
            Else 'No se ha logueado en la HH
                'e.Appearance.BackColor = Color.PaleVioletRed
                e.Appearance.BackColor2 = Color.White
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbMuelle_EditValueChanged(sender As Object, e As EventArgs) Handles cmbMuelle.EditValueChanged

        Try

            If cmbMuelle.EditValue > -1 Then

                Dim BeBodegaMuelle As New clsBeBodega_muelles
                BeBodegaMuelle = clsLnBodega_muelles.GetSingle(cmbMuelle.EditValue)

                If Not BeBodegaMuelle Is Nothing Then
                    If Not BeBodegaMuelle.IdUbicacionDefecto = 0 Then
                        txtIdUbicacionMuelle.Text = BeBodegaMuelle.IdUbicacionDefecto
                    End If
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Actualizar_Gaugue_Progreso()

        Try

            Dim vCantidadSolicitada As Double = BePickingEnc.ListaPickingUbic.Sum(Function(x) x.Cantidad_Solicitada)
            Dim vCantidadRecibida As Double = BePickingEnc.ListaPickingUbic.Sum(Function(x) x.Cantidad_Recibida)
            Dim vDeltaDifPickeado As Double = 0
            Dim vProgreso As Double = 0

            If vCantidadSolicitada > 0 AndAlso vCantidadRecibida > 0 Then
                vProgreso = Math.Round((vCantidadRecibida / vCantidadSolicitada), 2)
            Else
                vProgreso = 0
            End If

            cgProgresoPicking.Scales(0).MaxValue = 100
            cgProgresoPicking.Scales(0).MinValue = 0
            cgProgresoPicking.Scales(0).Value = vProgreso * 100

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Operador(ByVal pIndex As Integer,
                                Optional ByVal pIdOperadorBodega As Integer = 0)

        Try

            Dim DT As DataTable = clsLnOperador_bodega.Get_All_By_IdBodega_DT(cmbBodegas.EditValue)

            If Not DT Is Nothing Then

                Dim DgCombo As New DataGridViewComboBoxCell()
                DgCombo = TryCast(dgridDetallePicking.Rows(pIndex).Cells("OperadorBodega"), DataGridViewComboBoxCell)

                DgCombo.DataSource = DT
                DgCombo.ValueMember = "IdOperadorBodega"
                DgCombo.DisplayMember = "Nombres"

                If pIdOperadorBodega > 0 Then
                    DgCombo.Value = pIdOperadorBodega
                End If

            End If

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Sub

    Private Function Pedidos_Requieren_Muelle() As Boolean

        Pedidos_Requieren_Muelle = False

        Try

            Dim IdPickingGen As Integer = 0

            For Each Ped In pListaPedidos
                '#AT20250710 Cambie de escanear_muelle_picking a Mover_Producto_Zona_Muelle
                'Porque escanear_muelle_picking se utiliza para saber si se debe o no escanear el muelle en la HH
                If clsLnTrans_pe_tipo.Get_Single_By_IdPedidoEnc(Ped)?.Mover_Producto_Zona_Muelle Then
                    Return True
                End If

            Next

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmdListaPedidos_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdListaPedidos.ItemClick

        Try

            Imprimir_Vista_Pedidos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Cargar_Pedidos_Impresion(ByVal lConnection As SqlConnection,
                                          ByVal lTransaction As SqlTransaction)

        Dim dt As DataTable

        Try

            dt = clsLnTrans_pe_enc.Get_Pedidos_By_IdPickinEnc(BePickingEnc.IdPickingEnc, lConnection, lTransaction)

            grdImpresionPedidos.DataSource = Nothing

            If dt.Rows.Count > 0 Then

                grdImpresionPedidos.DataSource = dt

            End If

            If grdViewImpresionPedidos.Columns.Count > 0 Then
                grdViewImpresionPedidos.BestFitColumns(True)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Imprimir_Vista_Pedidos()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea_Pedidos

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

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdImpresionPedidos
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea_Pedidos(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Pedidos " &
                                     vbNewLine & "Picking WMS#: " & BePickingEnc.IdPickingEnc &
                                     vbNewLine & "Observación: " & IIf(BePickingEnc.Observacion <> "", BePickingEnc.Observacion & " ", "")

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 100)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)
    End Sub

    Private ReleaseRowPedido As Integer = -1
    Private Sub lnkQuitarPedido_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles lnkQuitarPedido.ItemClick
        lnkQuitarPedido.Enabled = False

        Try
            ' Validación de fila seleccionada
            If ReleaseRowPedido <= -1 Then
                XtraMessageBox.Show("Seleccione el pedido que quiere eliminar del picking", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Cargar valores del grid
            Get_ValoresGrid(ReleaseRowPedido)

            ' Validación de ID de pedido
            If Val(pIdPedidoEnc.Value) <= 0 Then Exit Sub

            ' Iniciar transacción
            Dim clsTrans As New clsTransaccion
            clsTrans.Begin_Transaction()

            Try
                ' Validar si hay productos pickeados
                Dim tienePickeados As Boolean = clsLnTrans_pe_enc.Tiene_Productos_Pickeados(pIdPedidoEnc.Value,
                                                                                         BePickingEnc.IdPickingEnc,
                                                                                         clsTrans.lConnection,
                                                                                         clsTrans.lTransaction)

                If tienePickeados Then
                    XtraMessageBox.Show("El pedido tiene productos pickeados, no se puede eliminar del picking.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    clsTrans.RollBack_Transaction()
                    Exit Sub
                End If

                ' Intentar eliminar el pedido del picking
                Dim eliminado As Boolean = clsLnTrans_pe_enc.Eliminar_Pedido_Picking(pIdPedidoEnc.Value,
                                                                                     BePickingEnc.IdPickingEnc,
                                                                                     clsTrans.lConnection,
                                                                                     clsTrans.lTransaction,
                                                                                     AP.UsuarioAp.IdUsuario)

                If eliminado Then

                    BePickingEnc = clsLnTrans_picking_enc.GetSingle(BePickingEnc.IdPickingEnc, clsTrans.lConnection, clsTrans.lTransaction)

                    Mostrar_Pedidos_Asociados(clsTrans.lConnection, clsTrans.lTransaction)

                    clsTrans.Commit_Transaction()

                    XtraMessageBox.Show("El pedido fue eliminado correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Else
                    Throw New ApplicationException("No se pudo eliminar el pedido del picking.")
                End If

            Catch exTrans As Exception
                clsTrans.RollBack_Transaction()
                Throw ' Propaga al bloque exterior
            End Try

        Catch ex As Exception
            XtraMessageBox.Show("Error al intentar eliminar el pedido: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Finally
            lnkQuitarPedido.Enabled = True
            ReleaseRowPedido = -1
        End Try

    End Sub


    Private Sub dgridPedidos_MouseClick(sender As Object, e As MouseEventArgs) Handles dgridPedidos.MouseClick

        Try

            If Not dgridPedidos.CurrentCell Is Nothing Then
                ReleaseRowPedido = dgridPedidos.CurrentCell.RowIndex
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Get_ValoresGrid(ByVal IndiceFila As Integer)

        If IsGettingValoresGrid Then Exit Sub

        IsGettingValoresGrid = True

        Try

            dgridPedidos.EndEdit()

            Dim row As DataGridViewRow = dgridPedidos.Rows(IndiceFila)
            pIdPedidoEnc = row.Cells(dgridPedidos.Columns("IdPedido").Index)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsGettingValoresGrid = False
        End Try

    End Sub


    Private Sub mnuReemplazo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReemplazo.ItemClick

        Try

            Dim Dr As DataRowView = grdvPickingUbic.GetFocusedRow
            Dim lSelectionIndex As Integer = grdvPickingUbic.FocusedRowHandle
            Dim IdStockRes As Integer = IIf(IsDBNull(Dr.Item("IdStockRes")), 0, Dr.Item("IdStockRes"))
            Dim vCodigoProducto As String = IIf(IsDBNull(Dr.Item("Código")), 0, Dr.Item("Código"))
            Dim vNombreProducto As String = IIf(IsDBNull(Dr.Item("Producto")), 0, Dr.Item("Producto"))
            Dim vCantidadSolPres As Double = IIf(IsDBNull(Dr.Item("Cant_Ped_Pres")), 0, Dr.Item("Cant_Ped_Pres"))
            Dim vCantidadSolUmBas As Double = IIf(IsDBNull(Dr.Item("Cant_Ped_Umbas")), 0, Dr.Item("Cant_Ped_Umbas"))
            Dim vCantidadPickPres As Double = IIf(IsDBNull(Dr.Item("Cant_Pick_Pres")), 0, Dr.Item("Cant_Pick_Pres"))
            Dim vCantidadPickUmbas As Double = IIf(IsDBNull(Dr.Item("Cant_Pick_Umbas")), 0, Dr.Item("Cant_Pick_Umbas"))
            Dim vCantidadVeriPres As Double = IIf(IsDBNull(Dr.Item("Cant_Veri_Pres")), 0, Dr.Item("Cant_Veri_Pres"))
            Dim vCantidadVeriUmbas As Double = IIf(IsDBNull(Dr.Item("Cant_Veri_Umbas")), 0, Dr.Item("Cant_Veri_Umbas"))

            If Dr Is Nothing Then Return

            Dim vContinuar As Boolean = True

            Dim vMensaje As String = "¿Marcar línea de picking para reemplazo: " & vCodigoProducto & " - " & vNombreProducto & "?"

            If XtraMessageBox.Show(vMensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Not IdStockRes = 0 Then

                    Existen_Diferencias_Memoria_vs_BD(vContinuar)

                    If Not vContinuar Then Return

                    Dim BePickingUbic As New clsBeTrans_picking_ubic
                    BePickingUbic = pListBePickingUbic.Where(Function(x) x.IdStockRes = IdStockRes).First()

                    Dim result As Tuple(Of Integer, Integer) = clsLnTrans_pe_enc.Get_IdCliente_And_IdPedidoEnc_By_IdPickingUbic(BePickingUbic.IdPickingUbic, BePickingUbic.IdPickingEnc)
                    Dim idCliente As Integer = result.Item1
                    Dim idPedidoEnc As Integer = result.Item2
                    Dim BeTipoPedido As clsBeTrans_pe_tipo = clsLnTrans_pe_enc.Get_TipoPedido_By_IdPickingEnc(BePickingUbic.IdPickingEnc.ToString)

                    Dim frmCant As New frmCantidadreemplazo
                    frmCant.IdCliente = idCliente
                    frmCant.BeTipoPedido = BeTipoPedido
                    frmCant.IdBodega = BePickingUbic.IdBodega
                    frmCant.Codigo_Producto = vCodigoProducto
                    frmCant.txtCantidadReemplazo.Maximum = IIf(vCantidadPickPres = 0, vCantidadPickUmbas, vCantidadPickPres)
                    frmCant.txtCantidadReemplazo.Value = IIf(vCantidadPickPres = 0, vCantidadPickUmbas, vCantidadPickPres)
                    frmCant.Cantidad_Reemplazo = IIf(vCantidadPickPres = 0, vCantidadPickUmbas, vCantidadPickPres)
                    frmCant.Cantidad_Total = frmCant.Cantidad_Reemplazo
                    frmCant.IdPresentacion = BePickingUbic.IdPresentacion
                    frmCant.txtIdProducto.Text = vCodigoProducto
                    frmCant.txtNombreProducto.Text = vNombreProducto
                    frmCant.BePickingUbic = BePickingUbic

                    If (vCantidadVeriPres > 0) OrElse (vCantidadSolUmBas = vCantidadPickUmbas) Then
                        frmCant.Modo_Reemplazo = frmCantidadreemplazo.eModoReemplazo.verificacion
                    Else
                        frmCant.Modo_Reemplazo = frmCantidadreemplazo.eModoReemplazo.picking
                    End If

                    If frmCant.ShowDialog() = DialogResult.OK Then

                        Cargar_Datos()

                        XtraMessageBox.Show("Se reemplazó la línea de picking.",
                                            Text,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information)

                    End If

                End If

            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub dgridPickingUbic_Click(sender As Object, e As EventArgs) Handles dgridPickingUbic.Click

        Dim Dr As DataRowView = grdvPickingUbic.GetFocusedRow
        Dim lSelectionIndex As Integer = grdvPickingUbic.FocusedRowHandle
        Dim IdStockRes As Integer = IIf(IsDBNull(Dr.Item("IdStockRes")), 0, Dr.Item("IdStockRes"))
        Dim vCodigoProducto As String = IIf(IsDBNull(Dr.Item("Código")), 0, Dr.Item("Código"))
        Dim vNombreProducto As String = IIf(IsDBNull(Dr.Item("Producto")), 0, Dr.Item("Producto"))
        Dim vCantidadPedidaPres As Double = IIf(IsDBNull(Dr.Item("Cant_Ped_Pres")), 0, Dr.Item("Cant_Ped_Pres"))
        Dim vCantidadPedidaUMBas As Double = IIf(IsDBNull(Dr.Item("Cant_Ped_UmBas")), 0, Dr.Item("Cant_Ped_UmBas"))
        Dim vUnidadMedida As String = IIf(IsDBNull(Dr.Item("Unidad_Medida")), "", Dr.Item("Unidad_Medida"))
        Dim vPedido As Integer = IIf(IsDBNull(Dr.Item("Pedido")), 0, Dr.Item("Pedido"))
        Dim pListBeStockRes As New List(Of clsBeStock_res)
        Dim vContinuar As Boolean = True
        Dim vCantidadPickPres As Double = IIf(IsDBNull(Dr.Item("Cant_Pick_Pres")), 0, Dr.Item("Cant_Pick_Pres"))
        Dim vCantidadPickUmbas As Double = IIf(IsDBNull(Dr.Item("Cant_Pick_Umbas")), 0, Dr.Item("Cant_Pick_Umbas"))
        Dim vCantidadVeriPres As Double = IIf(IsDBNull(Dr.Item("Cant_Veri_Pres")), 0, Dr.Item("Cant_Veri_Pres"))
        Dim vCantidadVeriUmbas As Double = IIf(IsDBNull(Dr.Item("Cant_Veri_Umbas")), 0, Dr.Item("Cant_Veri_Umbas"))

        Try

            Existen_Diferencias_Memoria_vs_BD(vContinuar)

            If Not vContinuar Then Return

            If Not IdStockRes = 0 Then

                pListBeStockRes = clsLnStock_res.Get_Single_By_IdStockRes(IdStockRes)

                If Not pListBeStockRes Is Nothing Then

                    If pListBeStockRes.Count > 0 Then

                        Dim BePickingUbic As New clsBeTrans_picking_ubic
                        BePickingUbic = clsLnTrans_picking_ubic.Get_Single_By_IdStockRes_And_IdPickingEnc(IdStockRes,
                                                                                                              BePickingEnc.IdPickingEnc,
                                                                                                              BePickingEnc.IdBodega)

                        If Not BePickingUbic Is Nothing Then

                            Dim vlPickingUbicByIdStockRes As New List(Of clsBeTrans_picking_ubic) From {
                                BePickingUbic}

                            If vCantidadPickPres = 0 OrElse vCantidadPickUmbas = 0 Then
                                mnuReemplazo.Caption = "Reemplazo picking"
                            Else
                                mnuReemplazo.Caption = "Reemplazo verificación"
                            End If


                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pIdPedidoEnc:=BePickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=BePickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
        End Try
    End Sub
    Private Sub Nuevo_Despacho(BePedidoEnc As clsBeTrans_pe_enc)

        Try

            Cierra_Instancia_Previa(frmDespacho)

            With frmDespacho
                .Modo = frmDespacho.TipoTrans.Nuevo
                .WindowState = FormWindowState.Maximized
                .Activate()
                .Show()
                .Agregar_Pedido(BePedidoEnc)
                .InvokeCargarPedido = AddressOf Cargar_Datos
                .Focus()
                .BringToFront()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
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

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    '#GT01122025: si tipo pedido es verificar por bof, bloquear controles que permiten modificar la verificación en la HH
    Public Sub BloquearControles_Por_VerificacionBOF(ByVal estado As Boolean)
        'cmdNoPickeado.Enabled = estado
        mnuProcesarLinea.Enabled = estado
        mnuProcesar.Enabled = estado
        mnuVerificarPickeados.Enabled = estado
        cmdVerificarNuevamente.Enabled = estado
        cmdNoVerificado.Enabled = estado
        mnuDespachado.Enabled = estado
        chkProcesarDesdeBOF.Checked = False
        chkProcesarDesdeBOF.Enabled = estado
        chkverifica_auto.Checked = False
        chkverifica_auto.Enabled = estado
    End Sub
End Class