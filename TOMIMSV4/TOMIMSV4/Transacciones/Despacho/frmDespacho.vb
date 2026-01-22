Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.Xpf.Editors.Helpers
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen

Public Class frmDespacho

    Private DTStockRes As New DataTable("StockRes")
    Public BeDespachoEnc As New clsBeTrans_despacho_enc
    Private ReadOnly lTransPickingDet As New List(Of clsBeTrans_picking_det)
    Private ReadOnly lTransPickingUbic As New List(Of clsBeTrans_picking_ubic)
    Private BeListPickingDet As New List(Of clsBeTrans_pe_enc)
    Private pListObjSP As New List(Of clsBeVW_stock_res)
    Private oDateTimePicker As DateTimePicker
    Public gBeRoadRuta As New clsBeRoad_ruta
    Public pListaPedidos As New List(Of Integer)
    Public Delegate Sub ListarDespacho()
    Public Property InvokeListarDespacho As ListarDespacho
    Private BePedidoEnc As New clsBeTrans_pe_enc

    Public Delegate Sub Cargar_Despacho_Pedido()
    Public Delegate Sub Actualizar_Stock_Reservado_En_Pedido()
    Public Property InvokeGetDespachoEnPedido As Cargar_Despacho_Pedido
    Public Property InvokeActualizarStockReservadoEnPedido As Actualizar_Stock_Reservado_En_Pedido

    Private DTVehiculos As New DataTable
    Private pNuevoVehiculo As New clsBeEmpresa_transporte_vehiculos

    ''' <summary>
    ''' #EJC20200723: 21:39PM
    ''' Esta variable la tuve que implementar por un problema de desfase de stock en CLC
    ''' El objetivo es que si el IdStock (Ej: 1090) del que se intenta eliminar la cantidad reservada (10), tiene una existencia menor (5)
    ''' Esto indica que en alguna transacción a ese idstock (1090) se le restó stock y ya no tiene disponible los 10
    ''' Por consiguiente se debe eliminar ese idstock para que permita continuar el despacho.
    ''' </summary>
    Private AllowNegativeExceptionOnStock As Boolean = False

    Public Property listaPresentaciones As New List(Of clsBeProducto_Presentacion)
    Public Property InvokeCargarPedido As Action(Of SqlConnection, SqlTransaction)
    Public Property InvokeCargarObjetoPedido As Action
    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Set_Columns_DT_StockRes()

        If DTStockRes.Columns.Count = 0 Then

            DTStockRes.Columns.Add("Pedido", GetType(Integer)).ReadOnly = True
            DTStockRes.Columns.Add("Picking", GetType(Integer)).ReadOnly = True
            DTStockRes.Columns.Add("Código", GetType(String)).ReadOnly = True
            DTStockRes.Columns.Add("Producto", GetType(String)).ReadOnly = True
            DTStockRes.Columns.Add("Presentación", GetType(String)).ReadOnly = True
            DTStockRes.Columns.Add("Estado", GetType(String)).ReadOnly = True
            DTStockRes.Columns.Add("Unidad_Medida", GetType(String)).ReadOnly = True
            DTStockRes.Columns.Add("Propietario", GetType(String)).ReadOnly = True
            DTStockRes.Columns.Add("Ubicación", GetType(String)).ReadOnly = True
            DTStockRes.Columns.Add("Lote", GetType(String)).ReadOnly = True
            DTStockRes.Columns.Add("Licencia", GetType(String)).ReadOnly = True
            DTStockRes.Columns.Add("Fecha Vence", GetType(Date)).ReadOnly = True
            DTStockRes.Columns.Add("Factor", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Cant_Ped_Pres", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Cant_Ped_UmBas", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Cant_Pick_Pres", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Cant_Pick_UMBas", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Cant_Veri_Pres", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Cant_Veri_UMBas", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Cant_Desp_Pres", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Cant_Desp_UMBas", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Cant_Pendiente", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Peso_Pick", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Peso_Veri", GetType(Double)).ReadOnly = True
            DTStockRes.Columns.Add("Encontrado", GetType(Boolean)).ReadOnly = True
            DTStockRes.Columns.Add("Acepto", GetType(Boolean)).ReadOnly = True
            DTStockRes.Columns.Add("Fecha Ingreso", GetType(DateTime)).ReadOnly = True
            DTStockRes.Columns.Add("IdStockRes", GetType(Integer)).ReadOnly = True
            DTStockRes.Columns.Add("IdProductoTallaColor", GetType(Integer)).ReadOnly = True
            DTStockRes.Columns.Add("Talla", GetType(String)).ReadOnly = True
            DTStockRes.Columns.Add("Color", GetType(String)).ReadOnly = True
        End If

    End Sub

    Private Sub oDateTimePicker_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)

        If grdListaDespacho.CurrentCell.ColumnIndex = 8 Then
            grdListaDespacho.CurrentCell.Value = oDateTimePicker.Text
        End If

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione bodega",
              Text,
              MessageBoxButtons.OK,
              MessageBoxIcon.Exclamation)
            ElseIf cmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione propietario",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            ElseIf BeDespachoEnc.ListaPedidos.Count = 0 Then
                XtraMessageBox.Show("No ha adicionado pedidos al despacho",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            ElseIf Not Pedidos_Tienen_Cantidad_Verificada() Then
                XtraMessageBox.Show("No existen productos verificados para realizar el despacho",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            ElseIf Not Pedidos_Tienen_Packing_Finalizado() Then
                XtraMessageBox.Show("Existen pedidos sin el packing finalizado",
               Text,
               MessageBoxButtons.OK,
               MessageBoxIcon.Exclamation)
            ElseIf Existe_Diferencia_Despacho_vrs_Packing() Then
                XtraMessageBox.Show("Existe diferencia entre el packing y el despacho",
               Text,
               MessageBoxButtons.OK,
               MessageBoxIcon.Exclamation)
            Else
                Datos_Correctos = True
            End If

            grdListaDespacho.CommitEdit(DataGridViewDataErrorContexts.Commit)
            grdListaDespacho.EndEdit()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Pedidos_Tienen_Cantidad_Verificada() As Boolean

        Pedidos_Tienen_Cantidad_Verificada = False

        Try

            If Not BeDespachoEnc.ListaPedidos Is Nothing Then

                For Each Ped In BeDespachoEnc.ListaPedidos

                    For Each Det In Ped.Detalle

                        If Not Det.ListaPickingUbic Is Nothing Then

                            If Not Det.ListaPickingUbic Is Nothing Then

                                For Each Pu In Det.ListaPickingUbic

                                    If (Pu.Cantidad_Verificada > 0) AndAlso
                                        (Pu.Cantidad_Verificada - Pu.Cantidad_despachada) > 0 Then
                                        Pedidos_Tienen_Cantidad_Verificada = True
                                        Exit Function
                                    End If

                                Next

                            Else
                                'El pedido no tiene picking_ubic?, no se ha asociado picking al pedido?
                            End If

                        End If

                    Next

                Next

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Pedidos_Permiten_Despacho_Multiple() As Boolean

        Pedidos_Permiten_Despacho_Multiple = False
        Dim ListDespacho As New List(Of clsBeTrans_despacho_det)
        Dim vListaPedidosVerificados As New List(Of Integer)

        Try

            If Not BeDespachoEnc.ListaPedidos Is Nothing Then

                Dim BeTipoDocumento As New clsBeTrans_pe_tipo()

                For Each Ped In BeDespachoEnc.ListaPedidos

                    BeTipoDocumento = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(Ped.IdTipoPedido)

                    If Not BeTipoDocumento.Permitir_Despacho_Multiple Then

                        If Not vListaPedidosVerificados.Contains(BePedidoEnc.IdPedidoEnc) Then

                            ListDespacho = clsLnTrans_despacho_det.Get_All_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc)

                            If Not ListDespacho Is Nothing Then

                                Dim vMensaje As String = "La configuración del tipo de documento: " &
                                                          BeTipoDocumento.Descripcion & " para el IdPedido: " &
                                                          Ped.IdPedidoEnc & " Ref.: " & Ped.Referencia &
                                                          " No permite despachos múltiples y el pedido ya reporta un despacho. "

                                SplashScreenManager.CloseForm(False)

                                If XtraMessageBox.Show(vMensaje & vbNewLine & "¿Continuar Despacho?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                    Pedidos_Permiten_Despacho_Multiple = True
                                Else
                                    Pedidos_Permiten_Despacho_Multiple = False
                                    Exit For
                                End If

                            Else
                                Pedidos_Permiten_Despacho_Multiple = True
                            End If

                        End If

                    Else
                        '#EJC20220618_1125AM.
                        Pedidos_Permiten_Despacho_Multiple = True
                    End If

                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Pedidos_Tienen_Verificacion_Parcial() As Boolean

        '#CKFK20220624 Inicialicé la función en False
        Pedidos_Tienen_Verificacion_Parcial = False
        Dim vListaPedidosVerificados As New List(Of Integer)

        Try

            If Not BeDespachoEnc.ListaPedidos Is Nothing Then

                Dim BeTipoDocumento As New clsBeTrans_pe_tipo()

                For Each Ped In BeDespachoEnc.ListaPedidos

                    BeTipoDocumento = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(Ped.IdTipoPedido)

                    If Not BeTipoDocumento.Permitir_Despacho_Parcial Then

                        For Each Det In Ped.Detalle

                            If Not Det.ListaPickingUbic Is Nothing Then

                                If Not Det.ListaPickingUbic Is Nothing Then

                                    '#EJC20250804: Filtrar registros que no fueron pickeados (aceptado) y que no estén dañados en picking o verificación
                                    Dim lPickingUbicValido = Det.ListaPickingUbic.FindAll(Function(x) x.Dañado_verificacion = False AndAlso x.Dañado_picking = False AndAlso x.Cantidad_Recibida > 0)

                                    For Each Pu In lPickingUbicValido

                                        If (Pu.Cantidad_Solicitada - Pu.Cantidad_Verificada) <> 0 Then

                                            '#CKFK20220705 No es necesario inicializar esto en true
                                            'Pedidos_Tienen_Verificacion_Parcial = True

                                            Dim vMensaje As String = "La configuración del tipo de documento: " &
                                                                    BeTipoDocumento.Descripcion & " para el IdPedido: " &
                                                                    Ped.IdPedidoEnc & " Ref.: " & Ped.Referencia &
                                                                    " No permite despachos parciales. " & "Producto: " & Det.Codigo_Producto &
                                                                    " Solicitado: " & Det.Cantidad & " Verificado: " & Pu.Cantidad_Verificada

                                            SplashScreenManager.CloseForm(False)

                                            If Not vListaPedidosVerificados.Contains(Ped.IdPedidoEnc) Then
                                                If XtraMessageBox.Show(vMensaje & vbNewLine & "¿Continuar Despacho?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                                    Pedidos_Tienen_Verificacion_Parcial = False
                                                    vListaPedidosVerificados.Add(Ped.IdPedidoEnc)
                                                Else
                                                    Pedidos_Tienen_Verificacion_Parcial = True
                                                    Exit For
                                                End If
                                            End If

                                        End If

                                    Next

                                Else
                                    'El pedido no tiene picking_ubic?, no se ha asociado picking al pedido?
                                End If

                            End If

                        Next

                    Else
                        '#EJC20220618_1125AM.
                        Pedidos_Tienen_Verificacion_Parcial = False
                    End If

                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            If Datos_Correctos() Then

                If XtraMessageBox.Show("¿Guardar Despacho?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

                    '#EJC20220525_1614: Validar por pedido y tipo de documento si permite despachos parciales.
                    If Not Pedidos_Tienen_Verificacion_Parcial() Then

                        '#EJC20220627:Pendiente validar si funciona que no permita despachos múltiples para un mismo pedido.
                        If Pedidos_Permiten_Despacho_Multiple() Then

                            Guardar()

                            SplashScreenManager.CloseForm(False)

                            '#EJC20220726: Actualiza el listado de despacho.
                            If Not InvokeListarDespacho Is Nothing Then
                                InvokeListarDespacho.Invoke()
                            End If

                            '#EJC20210912: Actualiza el número de despacho en el pedido.
                            If Not InvokeGetDespachoEnPedido Is Nothing Then
                                InvokeGetDespachoEnPedido.Invoke()
                            End If

                            '#EJC20220726: Actualizar el stock reservado en el pedido, si el despacho se invocó 
                            'desde la pantalla de pedido.
                            If Not InvokeActualizarStockReservadoEnPedido Is Nothing Then
                                InvokeActualizarStockReservadoEnPedido.Invoke()
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

                            Close()

                        End If

                    Else
                        '"Pedido parcial, no se puede despachar"
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show("Error al guardar el despacho: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Guardar()

        Dim hora_server As DateTime

        Try

            hora_server = clsServidor.Get_Fecha_Servidor()

            BeDespachoEnc.IdBodega = cmbBodega.EditValue
            BeDespachoEnc.IdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(BeDespachoEnc.IdBodega)
            BeDespachoEnc.IdPropietarioBodega = cmbPropietario.EditValue
            BeDespachoEnc.IdPiloto = Val(lcmbPiloto.EditValue)
            BeDespachoEnc.IdVehiculo = Val(lcmbVehiculo.EditValue)
            BeDespachoEnc.IdRuta = Val(lcmbRuta.EditValue)
            BeDespachoEnc.Fecha = hora_server
            BeDespachoEnc.No_pase = Val(txtNoPase.Value)
            BeDespachoEnc.Observacion = txtObservacion.Text.Trim
            BeDespachoEnc.Hora_ini = dtmHoraIhh.Value
            BeDespachoEnc.Hora_fin = dtmHoraFhh.Value
            BeDespachoEnc.Estado = "Finalizado"
            BeDespachoEnc.Numero = txtNumero.Value
            BeDespachoEnc.Marchamo = txtMarchamo.Text.Trim
            BeDespachoEnc.Cant_bultos = txtCantidadBultos.Value

            If BeDespachoEnc.IsNew Then
                BeDespachoEnc.User_agr = AP.UsuarioAp.IdUsuario
                BeDespachoEnc.Fec_agr = hora_server
            End If

            BeDespachoEnc.User_mod = AP.UsuarioAp.IdUsuario
            BeDespachoEnc.Fec_mod = hora_server
            BeDespachoEnc.Activo = chkActivo.Checked

            If clsLnTrans_despacho_enc.Guardar(BeDespachoEnc, AllowNegativeExceptionOnStock) Then

                SplashScreenManager.CloseForm(False)

                If XtraMessageBox.Show("Se guardó el despacho, ¿Imprimir documento de salida?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Genera_Reporte_Despacho()
                    Generar_Reporte_Packing()
                End If

                If clsBD.Instancia.IdConfiguracionInterface = 1989 Then
                    If Despacho_Tiene_Pedido_Para_Road(BeDespachoEnc) Then
                        'If Enviar_Despacho_A_Road(BeDespachoEnc) Then
                        '    SplashScreenManager.Default.SetWaitFormDescription("Documento enviado correctamente!")
                        'End If
                    End If
                Else

                    If AP.IdConfiguracionInterface <> -1 Then

                        Dim BeINavConfig As New clsBeI_nav_config_enc
                        BeINavConfig = clsLnI_nav_config_enc.GetSingle(AP.IdConfiguracionInterface)

                        Dim vArgumentosAEnviarAInterface As String = ""

                        If Not BeINavConfig Is Nothing Then

                            If BeINavConfig.Ejecutar_En_Despacho_Automaticamente Then

                                Dim tipoDocumento As New clsDataContractDI.tTipoDocumentoSalida

                                tipoDocumento = BeDespachoEnc.ListaPedidos(0).IdTipoPedido

                                Select Case tipoDocumento
                                    Case clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor
                                        vArgumentosAEnviarAInterface = "10-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & Val(BeDespachoEnc.ListaPedidos(0).Referencia) & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                    Case clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                                        vArgumentosAEnviarAInterface = "9-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & Val(BeDespachoEnc.ListaPedidos(0).Referencia) & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                    Case clsDataContractDI.tTipoDocumentoSalida.Requisicion
                                        vArgumentosAEnviarAInterface = "11-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & Val(BeDespachoEnc.ListaPedidos(0).Referencia) & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                    Case clsDataContractDI.tTipoDocumentoSalida.Transferencia_Directa
                                        vArgumentosAEnviarAInterface = "6-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & Val(BeDespachoEnc.ListaPedidos(0).Referencia) & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                    Case clsDataContractDI.tTipoDocumentoSalida.Traslado_Por_Estados_SAP
                                        vArgumentosAEnviarAInterface = "11-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & Val(BeDespachoEnc.ListaPedidos(0).Referencia) & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                End Select

                                Ejecutar_Interface(vArgumentosAEnviarAInterface, Me)

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Function Despacho_Tiene_Pedido_Para_Road(ByVal BeDespachoEnc As clsBeTrans_despacho_enc) As Boolean

        Despacho_Tiene_Pedido_Para_Road = False

        Try

            For Each P In BeDespachoEnc.ListaPedidos
                If P.RoadIdRuta <> 0 Then
                    Despacho_Tiene_Pedido_Para_Road = True
                    Exit For
                End If
            Next

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Sub CargarDatos()

        Try

            If BeDespachoEnc IsNot Nothing Then

                lblC.Text = BeDespachoEnc.IdDespachoEnc
                dtmFechaDespacho.EditValue = BeDespachoEnc.Fecha
                cmbBodega.EditValue = BeDespachoEnc.IdBodega
                '#GT09032023: evita que el operdor cambie la bodega y mezcle pedidos de distintas bodegas.
                cmbBodega.Enabled = False

                cmbPropietario.EditValue = BeDespachoEnc.IdPropietarioBodega

                If BeDespachoEnc.IdPiloto > 0 Then
                    lcmbPiloto.EditValue = BeDespachoEnc.IdPiloto
                End If

                If BeDespachoEnc.IdVehiculo > 0 Then
                    lcmbVehiculo.EditValue = BeDespachoEnc.IdVehiculo
                End If

                If BeDespachoEnc.IdRuta > 0 Then
                    lcmbRuta.EditValue = BeDespachoEnc.IdRuta
                End If

                txtNoPase.Value = BeDespachoEnc.No_pase
                txtObservacion.Text = BeDespachoEnc.Observacion

                dtmHoraIhh.Value = BeDespachoEnc.Hora_ini
                dtmHoraFhh.Value = BeDespachoEnc.Hora_fin
                lblEstado.Text = BeDespachoEnc.Estado

                txtNumero.Value = BeDespachoEnc.Numero
                txtMarchamo.Text = BeDespachoEnc.Marchamo
                txtCantidadBultos.Value = BeDespachoEnc.Cant_bultos
                txtDocumentoExterno.Text = BeDespachoEnc.No_Documento_Externo

                User_agrTextEdit.Text = BeDespachoEnc.User_agr
                Fec_agrDateEdit.Text = BeDespachoEnc.Fec_agr
                User_modTextEdit.Text = BeDespachoEnc.User_mod
                Fec_modDateEdit.Text = BeDespachoEnc.Fec_mod

                chkActivo.Checked = BeDespachoEnc.Activo

                If BeDespachoEnc.ListaDetalle IsNot Nothing AndAlso BeDespachoEnc.ListaDetalle.Count > 0 Then

                    Dim i As Integer = -1

                    grdListaDespacho.SuspendLayout() : grdListaDespacho.Rows.Clear()

                    Dim ListaP As List(Of Integer) = (From despacho In BeDespachoEnc.ListaDetalle Select despacho.IdPedidoEnc).Distinct.ToList
                    pListaPedidos = ListaP

                    For Each det As clsBeTrans_despacho_det In BeDespachoEnc.ListaDetalle

                        i = grdListaDespacho.Rows.Add

                        grdListaDespacho.Rows(i).Cells("IdPickingUbic").Value = det.IdPickingUbic
                        grdListaDespacho.Rows(i).Cells("Codigo").Value = det.Codigo
                        grdListaDespacho.Rows(i).Cells("Producto").Value = det.NombreProducto
                        grdListaDespacho.Rows(i).Cells("Presentacion").Value = det.ProductoPresentacion
                        grdListaDespacho.Rows(i).Cells("Estado").Value = det.ProductoEstado
                        grdListaDespacho.Rows(i).Cells("UnidadMedida").Value = det.ProductoUnidadMedida
                        grdListaDespacho.Rows(i).Cells("Ubicacion").Value = det.NombreUbicacion
                        grdListaDespacho.Rows(i).Cells("IdDespachoDet").Value = det.IdDespachoDet
                        grdListaDespacho.Rows(i).Cells("Fecha").Value = det.Fecha

                    Next

                    grdListaDespacho.ResumeLayout()

                    Mostrar_Pedidos_Asociados()

                End If

                If BeDespachoEnc.Estado.ToUpper = "ANULADO" Then
                    GrpDato.Enabled = False
                    GrpDetalle.Enabled = False
                    mnuActualizar.Enabled = False
                    mnuAnular.Enabled = False
                ElseIf BeDespachoEnc.Estado.ToUpper = "NUEVO" Then
                    mnuGuardar.Enabled = True
                    mnuAnular.Enabled = False
                    mnuActualizar.Enabled = False
                    mnuImprimir.Enabled = False
                Else
                    GrpDato.Enabled = True
                    GrpDetalle.Enabled = True
                    mnuAnular.Enabled = False
                    mnuActualizar.Enabled = False
                    mnuGuardarDatosCabecera.Enabled = True
                End If

                '#CKFK 20211124 Pruebas de despacho
                ' Enviar_Despacho_A_Road(BeDespachoEnc)

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Mostrar_Pedidos_Asociados()

        Dim clsTransaccion As New clsTransaccion
        Try

            If BeDespachoEnc IsNot Nothing Then

                Dim ListaP As List(Of Integer) = (From Despacho In BeDespachoEnc.ListaDetalle Select Despacho.IdPedidoEnc).Distinct().ToList()

                pListaPedidos = ListaP

                Dim ObjEA As clsBeTrans_despacho_det
                Dim vPedido As New clsBeTrans_pe_enc
                Dim ie As Integer = 0
                Dim vSubListaPresentaciones As New List(Of clsBeProducto_Presentacion)
                Dim vlSubListaPresentaciones As New List(Of clsBeProducto_Presentacion)

                clsTransaccion.Begin_Transaction()

                For Each lPedido As Integer In ListaP

                    ObjEA = BeDespachoEnc.ListaDetalle.ToList.Find(Function(b) b.IdPedidoEnc = lPedido AndAlso b.IdDespachoEnc = BeDespachoEnc.IdDespachoEnc)

                    vPedido = clsLnTrans_pe_enc.GetSingle_By_IdDespachoEnc(lPedido,
                                                                          True,
                                                                          BeDespachoEnc.IdDespachoEnc,
                                                                          clsTransaccion.lConnection,
                                                                          clsTransaccion.lTransaction)

                    ie = grdPedido.Rows.Add()

                    grdPedido.Rows(ie).Cells("Pedido").Value = vPedido.IdPedidoEnc
                    grdPedido.Rows(ie).Cells("Referencia").Value = vPedido.Referencia
                    grdPedido.Rows(ie).Cells("Bodega").Value = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vPedido.IdBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    grdPedido.Rows(ie).Cells("Cliente").Value = vPedido.Cliente.Nombre_comercial
                    grdPedido.Rows(ie).Cells("Propietario").Value = vPedido.PropietarioBodega.Propietario.Nombre_comercial
                    grdPedido.Rows(ie).Cells("FechaPedido").Value = vPedido.Fecha_Pedido

                    Application.DoEvents()

                    vSubListaPresentaciones = clsLnProducto_presentacion.Get_All_BePresentacion_By_IdBodega_And_IdPedido(BeDespachoEnc.IdBodega, lPedido, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    If Not listaPresentaciones Is Nothing Then

                        If Not vSubListaPresentaciones Is Nothing Then

                            If listaPresentaciones.Count > 0 Then

                                vlSubListaPresentaciones = New List(Of clsBeProducto_Presentacion)
                                vlSubListaPresentaciones = vSubListaPresentaciones.Except(listaPresentaciones).ToList()

                                If Not vlSubListaPresentaciones Is Nothing Then
                                    If vlSubListaPresentaciones.Count > 0 Then
                                        listaPresentaciones.AddRange(vlSubListaPresentaciones)
                                    End If
                                End If

                            Else
                                listaPresentaciones.AddRange(vSubListaPresentaciones)
                            End If

                        End If

                    End If

                    Dim vPedidosConPicking = vPedido.Detalle.Where(Function(x) x.ListaPickingUbic.Count > 0 AndAlso x.Stock_Liberado = 0)

                    For Each BePedidoDet As clsBeTrans_pe_det In vPedidosConPicking
                        SetProducto(BePedidoDet, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        SetProducto_By_Lista_PickingUbic(BePedidoDet.ListaPickingUbic)
                    Get_Stock_Res(BePedidoDet, True)
                    Application.DoEvents()
                Next

                If vPedido.IdPickingEnc <> 0 Then

                        Dim vTienePacking = clsLnTrans_pe_enc.Tiene_Packing(vPedido.IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                        If vTienePacking Then
                        Llena_Packing(vPedido.IdPickingEnc, vPedido.IdPedidoEnc, BeDespachoEnc.IdDespachoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    End If

                End If

                'Get_Stock_Res(vPedido, True)

                Next

                grdPedido.CommitEdit(DataGridViewDataErrorContexts.Commit)
                grdPedido.EndEdit()

                BeListPickingDet = BeDespachoEnc.ListaPedidos.ToList

                Formetar_Grid_Picking_Ubic()

            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Public Sub Get_Detalle_Pedido_Despacho(ByVal pPedido As clsBeTrans_pe_enc)

        Try

            If pPedido.Detalle.Count > 0 Then

                DTStockRes.Rows.Clear()

                If pListObjSP.Count > 0 Then

                    Dim vCantidadReservadaUMBas As Double = 0
                    Dim vCantidadReservadaPres As Double = 0
                    Dim vPesoReservado As Double = 0
                    Dim BePresentacion As New clsBeProducto_Presentacion

                    For Each Obj As clsBeVW_stock_res In pListObjSP

                        If Obj.IdPresentacion = 0 Then
                            vCantidadReservadaUMBas = Obj.CantidadReservadaUMBas
                            vCantidadReservadaPres = 0
                            vPesoReservado = Obj.Peso
                        Else

                            BePresentacion.IdPresentacion = Obj.IdPresentacion
                            clsLnProducto_presentacion.GetSingle(BePresentacion)

                            If Not BePresentacion Is Nothing Then
                                vCantidadReservadaPres = Math.Round(Obj.CantidadReservadaUMBas / BePresentacion.Factor, 6)
                                vCantidadReservadaUMBas = Obj.CantidadReservadaUMBas
                                Obj.Cantidad_Pickeada = Math.Round(Obj.Cantidad_Pickeada * BePresentacion.Factor, 6)
                                Obj.Cantidad_Verificada = Math.Round(Obj.Cantidad_Verificada * BePresentacion.Factor, 6)
                                Obj.Nombre_Presentacion = BePresentacion.Nombre
                            Else
                                Throw New Exception("No se pudo obtener la presentación con identificador: " & Obj.IdPresentacion)
                            End If

                            'vCantidadReservadaUMBas = Obj.CantidadReservadaUMBas
                            vPesoReservado = Obj.Peso

                        End If

                        DTStockRes.Rows.Add(Obj.IdPedido,
                                            Obj.IdPicking,
                                            Obj.Codigo_Producto,
                                            Obj.Nombre_Producto,
                                            Obj.Nombre_Presentacion,
                                            Obj.NomEstado,
                                            Obj.UMBas,
                                            Obj.Propietario,
                                            Obj.UbicacionActual.NombreCompleto,
                                            Obj.Lote,
                                            Obj.Lic_plate,
                                            Obj.Fecha_Vence,
                                            Obj.Factor,
                                            vCantidadReservadaPres,
                                            vCantidadReservadaUMBas,
                                            0,
                                            Obj.Cantidad_Pickeada,
                                            0,
                                            Obj.Cantidad_Verificada,
                                            0,
                                            Obj.Cantidad_Despachada,
                                            0,
                                            Obj.peso_pickeado,
                                            Obj.peso_verificado,
                                            Obj.encontrado,
                                            Obj.acepto,
                                            Obj.Fecha_ingreso,
                                            Obj.IdStockRes)

                    Next

                End If

                grdUbicPicking.DataSource = DTStockRes

                Set_Formato_Grid_Stock_Res()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Set_Formato_Grid_Stock_Res()

        Try

            grdvPickingUbic.OptionsView.ShowFooter = True

            grdvPickingUbic.Columns("Código").GroupIndex = 0

            grdvPickingUbic.Columns("Factor").Visible = False
            grdvPickingUbic.Columns("Factor").OptionsColumn.ShowInCustomizationForm = True

            Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Factor",
                .SummaryType = SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Factor")}
            grdvPickingUbic.GroupSummary.Add(item1)

            Dim item01 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cant_Ped_Pres",
                .SummaryType = SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Ped_Pres")}
            grdvPickingUbic.GroupSummary.Add(item01)

            Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cant_Ped_UmBas",
                .SummaryType = SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Ped_UmBas")}
            grdvPickingUbic.GroupSummary.Add(item2)

            Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cant_Pick",
                .SummaryType = SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Pick")}
            grdvPickingUbic.GroupSummary.Add(item4)

            Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cant_Veri",
                .SummaryType = SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Veri")}
            grdvPickingUbic.GroupSummary.Add(item5)

            'lblRegs.Caption = String.Format("Registros: {0}", pListObjSP.Count)

            Dim vFontNumero As Font = New Font("Arial monospaced for SAP", 12, FontStyle.Bold)

            If grdvPickingUbic.Columns.Count > 0 Then

                grdvPickingUbic.Columns("Cant_Ped_Pres").AppearanceCell.Font = vFontNumero
                grdvPickingUbic.Columns("Cant_Ped_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Ped_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Ped_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Ped_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Pick_UMBas").AppearanceCell.Font = vFontNumero
                grdvPickingUbic.Columns("Cant_Pick_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Pick_UMBas").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Veri_UMBas").AppearanceCell.Font = vFontNumero
                grdvPickingUbic.Columns("Cant_Veri_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Veri_UMBas").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Ped_UmBas").AppearanceCell.Font = vFontNumero
                grdvPickingUbic.Columns("Cant_Ped_UmBas").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Ped_UmBas").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Ped_UmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Ped_UmBas").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Factor").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Factor").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Factor").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Factor").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Pick_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Pick_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Pick_Pres").AppearanceCell.Font = vFontNumero
                grdvPickingUbic.Columns("Cant_Pick_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Pick_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Veri_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Veri_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Veri_Pres").AppearanceCell.Font = vFontNumero
                grdvPickingUbic.Columns("Cant_Veri_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Veri_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Peso_Pick").Visible = False
                grdvPickingUbic.Columns("Peso_Pick").OptionsColumn.ShowInCustomizationForm = True
                grdvPickingUbic.Columns("Peso_Pick").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Peso_Pick").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Peso_Pick").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Peso_Pick").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Peso_Veri").Visible = False
                grdvPickingUbic.Columns("Peso_Veri").OptionsColumn.ShowInCustomizationForm = True
                grdvPickingUbic.Columns("Peso_Veri").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Peso_Veri").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Peso_Veri").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Peso_Veri").DisplayFormat.FormatString = "{0:n6}"

            End If

            grdvPickingUbic.ExpandAllGroups()
            grdvPickingUbic.BestFitColumns()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Set_Formato_Grid_Stock_Res_A()

        Try

            grdvPickingUbic.OptionsView.ShowFooter = True

            'grdvPickingUbic.Columns("Código").Group()

            Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Cantidad_Disp",
                   .SummaryType = SummaryItemType.Sum,
                   .DisplayFormat = "{0:n6}",
                    .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cantidad_Disp")}
            grdvPickingUbic.GroupSummary.Add(item)

            Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Factor",
                        .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Factor")}
            grdvPickingUbic.GroupSummary.Add(item1)

            Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad",
                        .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cantidad")}
            grdvPickingUbic.GroupSummary.Add(item2)

            Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Solicitada",
                        .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cantidad_Solicitada")}
            grdvPickingUbic.GroupSummary.Add(item3)

            Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Pickeada",
                        .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cantidad_Pickeada")}
            grdvPickingUbic.GroupSummary.Add(item4)

            Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Verificada",
                        .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cantidad_Verificada")}
            grdvPickingUbic.GroupSummary.Add(item5)

            Dim item6 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cant_Pendiente",
                        .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Pendiente")}
            grdvPickingUbic.GroupSummary.Add(item6)

            Dim item7 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Peso_Recibido",
                        .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Peso_Recibido")}
            grdvPickingUbic.GroupSummary.Add(item7)

            Dim item8 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Peso_Verificado",
                        .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Peso_Verificado")}
            grdvPickingUbic.GroupSummary.Add(item8)

            grdvPickingUbic.Columns("Factor").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Factor").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Factor").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Factor").DisplayFormat.FormatString = "{0:n}"

            grdvPickingUbic.Columns("Cant_Ped_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Cant_Ped_Pres").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Cant_Ped_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Cant_Ped_Pres").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.Columns("Cant_Ped_UmBas").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Cant_Ped_UmBas").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Cant_Ped_UmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Cant_Ped_UmBas").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.Columns("Cant_Pick_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Cant_Pick_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Cant_Pick_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Cant_Pick_UMBas").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.Columns("Cant_Pick_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Cant_Pick_Pres").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Cant_Pick_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Cant_Pick_Pres").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.Columns("Cant_Veri_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Cant_Veri_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Cant_Veri_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Cant_Veri_UMBas").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.Columns("Cant_Veri_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Cant_Veri_Pres").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Cant_Veri_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Cant_Veri_Pres").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.Columns("Peso_Pick").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Peso_Pick").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Peso_Pick").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Peso_Pick").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.Columns("Peso_Veri").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Peso_Veri").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Peso_Veri").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Peso_Veri").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.Columns("Cant_Desp_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Cant_Desp_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Cant_Desp_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Cant_Desp_UMBas").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.Columns("Cant_Desp_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Cant_Desp_Pres").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Cant_Desp_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Cant_Desp_Pres").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.Columns("Cant_Pendiente").SummaryItem.SummaryType = SummaryItemType.Sum
            grdvPickingUbic.Columns("Cant_Pendiente").SummaryItem.DisplayFormat = "{0:n6}"
            grdvPickingUbic.Columns("Cant_Pendiente").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            grdvPickingUbic.Columns("Cant_Pendiente").DisplayFormat.FormatString = "{0:n6}"

            grdvPickingUbic.ExpandAllGroups()
            grdvPickingUbic.BestFitColumns(True)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If Datos_Correctos() Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando...")

                Guardar()

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("Se actualizó el Despacho.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                If Not InvokeListarDespacho Is Nothing Then InvokeListarDespacho.Invoke

                Close()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAnular.ItemClick

        Try

            If XtraMessageBox.Show("¿Anular Despacho?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                If clsLnTrans_despacho_enc.Anular_Despacho(BeDespachoEnc.IdDespachoEnc) Then
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Se anuló el Despacho", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarDespacho.Invoke
                    Close()
                Else
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("No se logró anular el Despacho.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub txtNoPase_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoPase.KeyPress

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

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtNumero_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNumero.KeyPress

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

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnkPiloto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkPiloto.LinkClicked

        Try

            Dim Piloto As New frmEmpresa_Transporte_PilotoList() With
                {.Modo = frmEmpresa_Transporte_PilotoList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                Piloto.OpcionesMenu = OpcionesMenu
                Piloto.mnuNuevo.Enabled = OpcionesMenu.Modificar
                Piloto.mmuActualizar.Enabled = OpcionesMenu.Leer
            End If

            Piloto.ShowDialog()

            If Piloto.pObjPiloto IsNot Nothing AndAlso Piloto.pObjPiloto.IdPiloto > 0 Then
                lcmbPiloto.EditValue = Piloto.pObjPiloto.IdPiloto
            End If

            Piloto.Close()
            Piloto.Dispose()

            Llena_PilotosLookUp_Grid()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnkVehiculo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkVehiculo.LinkClicked

        Try

            Dim Vehiculo As New frmEmpresa_Transporte_VehiculoList() With {.Modo = frmEmpresa_Transporte_VehiculoList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                Vehiculo.OpcionesMenu = OpcionesMenu
                Vehiculo.mnuNuevo.Enabled = OpcionesMenu.Modificar
                Vehiculo.mmuActualizar.Enabled = OpcionesMenu.Leer
            End If
            Vehiculo.ShowDialog()

            If Vehiculo.pObjVehiculo IsNot Nothing AndAlso Vehiculo.pObjVehiculo.IdVehiculo > 0 Then
                lcmbVehiculo.EditValue = Vehiculo.pObjVehiculo.IdVehiculo
            End If

            Vehiculo.Close()
            Vehiculo.Dispose()

            Llena_VehiculosLookUp_Grid()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnkRuta_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkRuta.LinkClicked

        Try

            Dim Ruta As New frmListaRoadRuta() With {.Modo = frmListaRoadRuta.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                Ruta.OpcionesMenu = OpcionesMenu
                Ruta.mnuNuevo.Enabled = OpcionesMenu.Modificar
                Ruta.mnuActualizar.Enabled = OpcionesMenu.Leer
            End If

            Ruta.ShowDialog()

            If gBeRoadRuta IsNot Nothing AndAlso Ruta.gBeRoadRuta.IdRuta > 0 Then
                lcmbRuta.EditValue = Ruta.gBeRoadRuta.IdRuta
            End If

            Ruta.Close()
            Ruta.Dispose()

            Llena_RutasLookUp_Grid()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnkAgregarPickingUbicacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAgregarPickingUbicacion.LinkClicked

        Try

            Dim bo As New frmPickingUbicacionBuscador() With {.pIdPropietarioBodega = cmbPropietario.EditValue}
            bo.ShowDialog()

            If bo.pObjPickingUbicacion IsNot Nothing AndAlso bo.pObjPickingUbicacion.IdPickingUbic > 0 Then
                If bo.pObjPickingUbicacion.Cantidad_Verificada > 0 Then
                    Throw New Exception(String.Format("El Producto {0} no ha sido verificado.", bo.pObjPickingUbicacion.NombreProducto))
                End If
                Adicionar_Producto_A_Detalle_Despacho(bo.pObjPickingUbicacion)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmDespacho_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F2 Then
            lnkPedido_LinkClicked(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.KeyCode = Keys.F9 Then
            AllowNegativeExceptionOnStock = Not AllowNegativeExceptionOnStock
            btwnegativeStock.Visibility = IIf(AllowNegativeExceptionOnStock, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
            btwnegativeStock.Checked = Not AllowNegativeExceptionOnStock
            Guardar()
        End If

    End Sub

    Private Sub SetProducto_By_Lista_PickingUbic(ByVal listPickingUbic As List(Of clsBeTrans_picking_ubic))

        Try

            If Not listPickingUbic Is Nothing Then
                For Each BePickingUbic In listPickingUbic
                    Adicionar_Producto_A_Detalle_Despacho(BePickingUbic)
                Next
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Adicionar_Producto_A_Detalle_Despacho(ByVal BeTransPickingUbic As clsBeTrans_picking_ubic)

        Try

            Dim BeDespachoDet As New clsBeTrans_despacho_det

            If BeDespachoEnc.ListaDetalle IsNot Nothing AndAlso BeDespachoEnc.ListaDetalle.Count > 0 Then
                BeDespachoDet.IdDespachoDet = BeDespachoEnc.ListaDetalle.Max(Function(b) b.IdDespachoDet) + 1
            Else
                BeDespachoDet.IdDespachoDet = 1
            End If

            BeDespachoDet.Codigo = BeTransPickingUbic.CodigoProducto
            BeDespachoDet.NombreProducto = BeTransPickingUbic.NombreProducto
            BeDespachoDet.NombreEstado = BeTransPickingUbic.ProductoEstado
            BeDespachoDet.IdPickingUbic = BeTransPickingUbic.IdPickingUbic
            BeDespachoDet.IdProductoBodega = BeTransPickingUbic.IdProductoBodega
            BeDespachoDet.IdProductoEstado = BeTransPickingUbic.IdProductoEstado
            BeDespachoDet.IdPresentacion = BeTransPickingUbic.IdPresentacion
            BeDespachoDet.IdUnidadMedidaBasica = BeTransPickingUbic.IdUnidadMedida
            BeDespachoDet.IdPedidoEnc = BeTransPickingUbic.IdPedidoEnc
            BeDespachoDet.IdPedidoDet = BeTransPickingUbic.IdPedidoDet
            BeDespachoDet.IdPickingUbic = BeTransPickingUbic.IdPickingUbic
            BeDespachoDet.CantidadDespachada = BeTransPickingUbic.Cantidad_Verificada
            BeDespachoDet.PesoDespachado = BeTransPickingUbic.Peso_verificado
            BeDespachoDet.User_agr = AP.UsuarioAp.IdUsuario
            BeDespachoDet.Fec_agr = Now
            BeDespachoDet.User_mod = AP.UsuarioAp.IdUsuario
            BeDespachoDet.NombreUbicacion = BeTransPickingUbic.NombreUbicacion
            BeDespachoDet.Fec_mod = Now
            BeDespachoDet.Activo = True
            BeDespachoDet.IsNew = True
            BeDespachoDet.FechaPedido = BePedidoEnc.Fecha_Pedido

            If BeTransPickingUbic.IdProductoTallaColor > 0 Then
                BeDespachoDet.Talla = BeTransPickingUbic.Codigo_Talla
                BeDespachoDet.Color = BeTransPickingUbic.Codigo_Color
                BeDespachoDet.IdProductoTallaColor = BeTransPickingUbic.IdProductoTallaColor
            Else
                BeDespachoDet.Talla = ""
                BeDespachoDet.Color = ""

            End If

            BeDespachoEnc.ListaDetalle.Add(BeDespachoDet)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub grdListaDespacho_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdListaDespacho.CellValueChanged

        Try

            If grdListaDespacho.Rows.Count > 0 AndAlso grdListaDespacho.CurrentRow IsNot Nothing Then

                grdListaDespacho.CommitEdit(DataGridViewDataErrorContexts.Commit)
                grdListaDespacho.EndEdit()

                If BeDespachoEnc.ListaDetalle IsNot Nothing AndAlso BeDespachoEnc.ListaDetalle.Count > 0 Then

                    Dim lIndex As Integer = -1

                    lIndex = BeDespachoEnc.ListaDetalle.FindIndex(Function(b) b.IdDespachoDet = grdListaDespacho.CurrentRow.Cells("IdDespachoDet").Value)

                    If lIndex > -1 Then

                        If grdListaDespacho.Columns(e.ColumnIndex).Name() = "Fecha" AndAlso
                            grdListaDespacho.CurrentRow.Cells("Fecha").Value IsNot DBNull.Value AndAlso
                            grdListaDespacho.CurrentRow.Cells("Fecha").Value IsNot Nothing AndAlso
                            IsDate(grdListaDespacho.CurrentRow.Cells("Fecha").Value) Then

                            BeDespachoEnc.ListaDetalle(lIndex).Fecha = CDate(grdListaDespacho.CurrentRow.Cells("Fecha").Value)

                        End If

                        BeDespachoEnc.ListaDetalle(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                        BeDespachoEnc.ListaDetalle(lIndex).Fec_mod = Now

                    End If

                End If

                grdListaDespacho.CommitEdit(DataGridViewDataErrorContexts.Commit)
                grdListaDespacho.EndEdit()

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub grdListaDespacho_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles grdListaDespacho.CellBeginEdit

        Try

            If grdListaDespacho.Focused AndAlso grdListaDespacho.CurrentCell.ColumnIndex = 8 Then

                oDateTimePicker.Location = grdListaDespacho.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False).Location
                oDateTimePicker.Visible = True

                If grdListaDespacho.CurrentCell.Value IsNot DBNull.Value AndAlso IsDate(grdListaDespacho.CurrentCell.Value) Then
                    oDateTimePicker.Value = grdListaDespacho.CurrentCell.Value
                Else
                    oDateTimePicker.Value = Today
                End If

            Else
                oDateTimePicker.Visible = False
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub grdListaDespacho_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grdListaDespacho.CellEndEdit

        Try

            If grdListaDespacho.Focused AndAlso grdListaDespacho.CurrentCell.ColumnIndex = 10 Then
                grdListaDespacho.CurrentCell.Value = oDateTimePicker.Value.Date
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub lnkPedido_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Agregar_Pedido()
    End Sub


    Private Sub Agregar_Pedido()

        Dim clsTransaccion As New clsTransaccion

        Try

            Dim fila As Object = cmbPropietario.GetSelectedDataRow

            If Not fila Is Nothing Then

                Dim idPropietarioBodega As Integer = fila.Item("IdPropietarioBodega")

                Dim bo As New frmPedidoDetalleBuscador() With {.Modo = frmPedidoDetalleBuscador.ProcesoSolicitante.Despacho,
                                                            .pListaPedidos = pListaPedidos,
                                                            .IdBodega = cmbBodega.EditValue,
                                                            .idPropietarioBodega = cmbPropietario.EditValue}
                Dim Result As DialogResult = bo.ShowDialog()

                If Result = DialogResult.OK Then

                    If bo.pBePedidoEnc IsNot Nothing Then

                        If bo.pBePedidoEnc.Detalle IsNot Nothing Then

                            If bo.pBePedidoEnc.Detalle.Count > 0 Then

                                If bo.pBePedidoEnc.IdPropietarioBodega = idPropietarioBodega Then

                                    clsTransaccion.Begin_Transaction()

                                    '#EJC20190214_0340AM: Puntero local hacia objeto de pedido en forma de buscador..
                                    BePedidoEnc = bo.pBePedidoEnc

                                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                    SplashScreenManager.Default.SetWaitFormCaption("Pedido")

                                    Cursor = Cursors.WaitCursor

                                    Application.DoEvents()

                                    Dim i As Integer = grdPedido.Rows.Add()

                                    '#EJC20200501_0033AM: Revisar que los objetos del encabezado traigan valores.
                                    grdPedido.Rows(i).Cells("Pedido").Value = bo.pBePedidoEnc.IdPedidoEnc
                                    grdPedido.Rows(i).Cells("Referencia").Value = bo.pBePedidoEnc.Referencia
                                    grdPedido.Rows(i).Cells("Bodega").Value = bo.pBePedidoEnc.IdBodega
                                    grdPedido.Rows(i).Cells("Cliente").Value = bo.pBePedidoEnc.Cliente.Codigo + " " + bo.pBePedidoEnc.Cliente.Nombre_comercial
                                    grdPedido.Rows(i).Cells("Propietario").Value = bo.pBePedidoEnc.PropietarioBodega.Propietario.Nombre_comercial
                                    grdPedido.Rows(i).Cells("FechaPedido").Value = bo.pBePedidoEnc.Fecha_Pedido
                                    grdPedido.Columns("FechaPedido").DefaultCellStyle.Format = "d"
                                    grdPedido.CommitEdit(DataGridViewDataErrorContexts.Commit)
                                    grdPedido.EndEdit()

                                    Get_Stock_Res(bo.pBePedidoEnc, True)

                                    pListaPedidos.Add(bo.pBePedidoEnc.IdPedidoEnc)

                                    XtraTabControl1.SelectedTabPage = tbDetalleProducto

                                    ' Obtener IDs de ProductoBodega donde hay diferencia entre lo verificado y lo despachado
                                    Dim idsConDiferencia = bo.pBePedidoEnc.Picking.ListaPickingUbic _
                                                            .Where(Function(x) x.Cantidad_Verificada > x.Cantidad_despachada) _
                                                            .Select(Function(x) x.IdProductoBodega) _
                                                            .Distinct() _
                                                            .ToList()

                                    ' Recorrer los detalles del pedido que coincidan con esos IDs
                                    '#GT16092025: tomar detalle de pedido que no tenga lineas liberadas de stock
                                    For Each BePedidoDet As clsBeTrans_pe_det In bo.pBePedidoEnc.Detalle.Where(Function(x) idsConDiferencia.Contains(x.IdProductoBodega))

                                        SetProducto(BePedidoDet, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                        SetProducto_By_Lista_PickingUbic(BePedidoDet.ListaPickingUbic)
                                    Next


                                    BeDespachoEnc.ListaPedidos.Add(bo.pBePedidoEnc)

                                    XtraTabControl1.SelectedTabPage = tbPedido

                                    If BePedidoEnc.IdPickingEnc <> 0 Then

                                        Dim BePicking As New clsBeTrans_picking_enc
                                        BePicking = clsLnTrans_picking_enc.GetSingle(BePedidoEnc.IdPickingEnc,
                                                                                 clsTransaccion.lConnection,
                                                                                 clsTransaccion.lTransaction)

                                        If Not BePicking Is Nothing Then

                                            If BePicking.Requiere_Preparacion Then

                                                If Not clsLnTrans_pe_enc.Tiene_Packing(BePedidoEnc.IdPedidoEnc) Then

                                                    SplashScreenManager.CloseForm(False)

                                                    XtraMessageBox.Show(String.Format("El picking asociado al pedido {0} tiene configurado packing y no se ha realizado el proceso, no se puede agregar al Despacho", BePedidoEnc.IdPedidoEnc), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                                Else

                                                    Dim vFinalizado As Boolean = False
                                                    vFinalizado = clsLnTrans_pe_enc.Packing_Finalizado(BePedidoEnc.IdPedidoEnc)

                                                    If Not vFinalizado Then
                                                        SplashScreenManager.CloseForm(False)
                                                        XtraMessageBox.Show("El packing no se ha finalizado, no se puede despachar", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Return

                                                    End If

                                                    Llena_Packing(BePedidoEnc.IdPickingEnc, BePedidoEnc.IdPedidoEnc, BeDespachoEnc.IdDespachoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                                                End If

                                            End If

                                        End If

                                    End If

                                    Set_Formato_Grid_Stock_Res_A()

                                Else

                                    XtraMessageBox.Show("El producto a despachar no es del propietario seleccionado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                    XtraTabControl1.SelectedTabPage = tbPagina1
                                    cmbPropietario.Focus()

                                End If

                                clsTransaccion.Commit_Transaction()

                            Else
                                '#CKFK El pedido no tiene lineas

                            End If

                        Else
                            '#EJC20190214_0344: Playa Girón, si no seleccionó ningún pedido..
                            BePedidoEnc = Nothing
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Cursor = Cursors.Default
            SplashScreenManager.CloseForm(False)
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Public Sub Agregar_Pedido(ByVal pBePedidoEnc As clsBeTrans_pe_enc)

        Dim clsTransaccion As New clsTransaccion

        Try

            If Not pBePedidoEnc Is Nothing Then

                BePedidoEnc = pBePedidoEnc

                cmbPropietario.EditValue = pBePedidoEnc.IdPropietarioBodega

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Pedido")

                Cursor = Cursors.WaitCursor

                'Application.DoEvents()

                Dim i As Integer = grdPedido.Rows.Add()

                '#EJC20200501_0033AM: Revisar que los objetos del encabezado traigan valores.
                grdPedido.Rows(i).Cells("Pedido").Value = pBePedidoEnc.IdPedidoEnc
                grdPedido.Rows(i).Cells("Referencia").Value = pBePedidoEnc.Referencia
                grdPedido.Rows(i).Cells("Bodega").Value = pBePedidoEnc.IdBodega
                grdPedido.Rows(i).Cells("Cliente").Value = pBePedidoEnc.Cliente.Codigo + " " + pBePedidoEnc.Cliente.Nombre_comercial
                grdPedido.Rows(i).Cells("Propietario").Value = pBePedidoEnc.PropietarioBodega.Propietario.Nombre_comercial
                grdPedido.Rows(i).Cells("FechaPedido").Value = pBePedidoEnc.Fecha_Pedido
                grdPedido.Columns("FechaPedido").DefaultCellStyle.Format = "d"
                grdPedido.CommitEdit(DataGridViewDataErrorContexts.Commit)
                grdPedido.EndEdit()

                clsTransaccion.Begin_Transaction()

                Get_Stock_Res(pBePedidoEnc, True, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                pListaPedidos.Add(pBePedidoEnc.IdPedidoEnc)

                XtraTabControl1.SelectedTabPage = tbDetalleProducto

                For Each BePedidoDet As clsBeTrans_pe_det In pBePedidoEnc.Detalle
                    SetProducto(BePedidoDet, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    SetProducto_By_Lista_PickingUbic(BePedidoDet.ListaPickingUbic)
                Next

                BeDespachoEnc.ListaPedidos.Add(pBePedidoEnc)

                XtraTabControl1.SelectedTabPage = tbPedido

                If BePedidoEnc.IdPickingEnc <> 0 Then

                    Dim BePicking As New clsBeTrans_picking_enc
                    BePicking = clsLnTrans_picking_enc.GetSingle(BePedidoEnc.IdPickingEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    If Not BePicking Is Nothing Then

                        If BePicking.Requiere_Preparacion Then

                            Dim lPacking As New List(Of clsBeTrans_packing_enc)
                            lPacking = clsLnTrans_packing_enc.Get_All_By_IdPicking(BePicking.IdPickingEnc, False, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                            If lPacking.Count = 0 Then
                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show("El picking asociado al pedido requiere preparación o packing y no se ha realizado, ingrese a tomwms en la handheld y seleccione Packing.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Else
                                Llena_Packing(BePedidoEnc.IdPickingEnc, BePedidoEnc.IdPedidoEnc, BeDespachoEnc.IdDespachoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                            End If

                        End If

                    End If

                End If

            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Cursor = Cursors.Default
            SplashScreenManager.CloseForm(False)
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private DTPacking As New DataTable

    Private Sub Llena_Packing(ByVal IdPickingEnc As Integer,
                              ByVal IdPedidoEnc As Integer,
                              ByVal IdDespachoEnc As Integer,
                              ByVal lConnection As SqlConnection,
                              ByVal lTransaction As SqlTransaction)

        Try

            Dim DT As New DataTable
            DT = clsLnTrans_packing_enc.Get_All_By_IdPicking_And_IdPedido_And_IdDespacho_DT(IdPickingEnc, IdPedidoEnc, IdDespachoEnc, lConnection, lTransaction)

            DTPacking.Merge(DT)

            dgridPacking.DataSource = DTPacking

            If grdvPickingUbic.Columns.Count > 1 Then

                gvPacking.Columns("no_linea").Group()

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem()
                item1.FieldName = "cantidad_bultos_packing"
                item1.ShowInGroupColumnFooter = gvPacking.Columns("cantidad_bultos_packing")
                item1.SummaryType = SummaryItemType.Sum
                item1.DisplayFormat = "Sum = {0:n3}"
                gvPacking.GroupSummary.Add(item1).ToString()

                gvPacking.Columns("cantidad_bultos_packing").SummaryItem.SummaryType = SummaryItemType.Sum
                gvPacking.Columns("cantidad_bultos_packing").SummaryItem.DisplayFormat = "{0:n6}"
                gvPacking.Columns("cantidad_bultos_packing").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gvPacking.Columns("cantidad_bultos_packing").DisplayFormat.FormatString = "{0:n6}"

                gvPacking.Columns("cantidad_camas_packing").SummaryItem.SummaryType = SummaryItemType.Sum
                gvPacking.Columns("cantidad_camas_packing").SummaryItem.DisplayFormat = "{0:n6}"
                gvPacking.Columns("cantidad_camas_packing").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gvPacking.Columns("cantidad_camas_packing").DisplayFormat.FormatString = "{0:n6}"

                gvPacking.BestFitColumns()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Packing_By_Despacho(ByVal IdPickingEnc As Integer,
                                          ByVal IdPedidoEnc As Integer,
                                          ByVal IdDespachoEnc As Integer,
                                          ByVal lConnection As SqlConnection,
                                          ByVal lTransaction As SqlTransaction)

        Try

            Dim DT As New DataTable
            DT = clsLnTrans_packing_enc.Get_All_By_IdPicking_DT(IdPickingEnc, IdPedidoEnc, lConnection, lTransaction)

            DTPacking.Merge(DT)

            dgridPacking.DataSource = DTPacking

            If grdvPickingUbic.Columns.Count > 1 Then

                gvPacking.Columns("no_linea").Group()

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem()
                item1.FieldName = "cantidad_bultos_packing"
                item1.ShowInGroupColumnFooter = gvPacking.Columns("cantidad_bultos_packing")
                item1.SummaryType = SummaryItemType.Sum
                item1.DisplayFormat = "Sum = {0:n3}"
                gvPacking.GroupSummary.Add(item1).ToString()

                gvPacking.Columns("cantidad_bultos_packing").SummaryItem.SummaryType = SummaryItemType.Sum
                gvPacking.Columns("cantidad_bultos_packing").SummaryItem.DisplayFormat = "{0:n6}"
                gvPacking.Columns("cantidad_bultos_packing").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gvPacking.Columns("cantidad_bultos_packing").DisplayFormat.FormatString = "{0:n6}"

                gvPacking.Columns("cantidad_camas_packing").SummaryItem.SummaryType = SummaryItemType.Sum
                gvPacking.Columns("cantidad_camas_packing").SummaryItem.DisplayFormat = "{0:n6}"
                gvPacking.Columns("cantidad_camas_packing").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gvPacking.Columns("cantidad_camas_packing").DisplayFormat.FormatString = "{0:n6}"

                gvPacking.BestFitColumns()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_Stock_Res(ByVal pBePedidoDet As clsBeTrans_pe_det, ByVal DespachoRealizado As Boolean)

        Try

            If pBePedidoDet.ListaPickingUbic IsNot Nothing Then

                If pBePedidoDet.ListaPickingUbic.Count > 0 Then

                    Dim vCantidadReservadaUMBas As Double = 0
                    Dim vCantidadReservadaPres As Double = 0
                    Dim vCantidadRecPres As Double = 0
                    Dim vCantidadVerPres As Double = 0
                    Dim vCantidadDespPres As Double = 0
                    Dim vCantPendiente As Double = 0
                    Dim vPesoReservado As Double = 0
                    Dim BePresentacion As New clsBeProducto_Presentacion

                    For Each BeTransPickingUbic In pBePedidoDet.ListaPickingUbic

                        If (BeTransPickingUbic.IdPresentacion = 0) OrElse (pBePedidoDet.IdPresentacion = 0) Then
                            vCantidadReservadaUMBas = BeTransPickingUbic.Cantidad_Solicitada
                            vCantidadReservadaPres = 0
                            vCantidadRecPres = 0
                            vCantidadVerPres = 0
                            vCantidadDespPres = 0
                        Else

                            BePresentacion = listaPresentaciones.Find(Function(x) x.IdPresentacion = BeTransPickingUbic.IdPresentacion)

                            If Not BePresentacion Is Nothing Then
                                vCantidadReservadaPres = BeTransPickingUbic.Cantidad_Solicitada
                                vCantidadReservadaUMBas = Math.Round(BeTransPickingUbic.Cantidad_Solicitada * BePresentacion.Factor, 6)
                                vCantidadRecPres = BeTransPickingUbic.Cantidad_Recibida
                                BeTransPickingUbic.Cantidad_Recibida = Math.Round(BeTransPickingUbic.Cantidad_Recibida * BePresentacion.Factor, 6)
                                vCantidadVerPres = BeTransPickingUbic.Cantidad_Verificada
                                BeTransPickingUbic.Cantidad_Verificada = Math.Round(BeTransPickingUbic.Cantidad_Verificada * BePresentacion.Factor, 6)
                                vCantidadDespPres = BeTransPickingUbic.Cantidad_despachada
                                BeTransPickingUbic.Cantidad_despachada = Math.Round(BeTransPickingUbic.Cantidad_despachada * BePresentacion.Factor, 6)
                            Else
                                Throw New Exception("No se pudo obtener la presentación con identificador: " & BeTransPickingUbic.IdPresentacion)
                            End If

                        End If

                        vCantPendiente = vCantidadReservadaUMBas - BeTransPickingUbic.Cantidad_Recibida

                        If vCantPendiente = 0 Then
                            vCantPendiente = BeTransPickingUbic.Cantidad_Recibida - BeTransPickingUbic.Cantidad_Verificada
                        End If

                        DTStockRes.Rows.Add(BeTransPickingUbic.IdPedidoEnc,
                                            BeTransPickingUbic.IdPickingEnc,
                                            BeTransPickingUbic.CodigoProducto,
                                            BeTransPickingUbic.NombreProducto,
                                            BeTransPickingUbic.ProductoPresentacion,
                                            BeTransPickingUbic.ProductoEstado,
                                            BeTransPickingUbic.ProductoUnidadMedida,
                                            BeTransPickingUbic.IdPropietarioBodega,
                                            BeTransPickingUbic.Ubicacion.NombreCompleto,
                                            BeTransPickingUbic.Lote,
                                            BeTransPickingUbic.Lic_plate,
                                            BeTransPickingUbic.Fecha_Vence,
                                            BePresentacion.Factor,
                                            vCantidadReservadaPres,
                                            vCantidadReservadaUMBas,
                                            vCantidadRecPres,
                                            BeTransPickingUbic.Cantidad_Recibida,
                                            vCantidadVerPres,
                                            BeTransPickingUbic.Cantidad_Verificada,
                                            vCantidadDespPres,
                                            BeTransPickingUbic.Cantidad_despachada,
                                            vCantPendiente,
                                            BeTransPickingUbic.Peso_recibido,
                                            BeTransPickingUbic.Peso_verificado,
                                            BeTransPickingUbic.Encontrado,
                                            BeTransPickingUbic.Acepto,
                                            "01/01/1900",
                                            BeTransPickingUbic.IdStockRes,
                                            BeTransPickingUbic.IdProductoTallaColor,
                                            BeTransPickingUbic.Codigo_Talla,
                                            BeTransPickingUbic.Codigo_Color)

                        Application.DoEvents()

                    Next

                End If

                grdUbicPicking.DataSource = DTStockRes

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    ''' <summary>
    ''' #EJC202210131054: WMS - Crear método Formetar_Grid_Picking_Ubic para no hacerlo por cada fila el formateo sino al final de Get_Stock_Res
    ''' </summary>
    Private Sub Formetar_Grid_Picking_Ubic()

        Try

            If grdvPickingUbic.RowCount > 0 Then

                grdvPickingUbic.OptionsView.ShowFooter = True

                grdvPickingUbic.Columns("Código").Group()

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad_Disp",
               .SummaryType = SummaryItemType.Sum,
               .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cantidad_Disp")}
                grdvPickingUbic.GroupSummary.Add(item)

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Factor",
                    .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}",
                    .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Factor")}
                grdvPickingUbic.GroupSummary.Add(item1)

                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Cantidad",
                    .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}",
                    .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cantidad")}
                grdvPickingUbic.GroupSummary.Add(item2)

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Cantidad_Solicitada",
                    .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}",
                    .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cantidad_Solicitada")}
                grdvPickingUbic.GroupSummary.Add(item3)

                Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Cantidad_Pickeada",
                    .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}",
                    .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cantidad_Pickeada")}
                grdvPickingUbic.GroupSummary.Add(item4)

                Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Cantidad_Verificada",
                    .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}",
                    .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cantidad_Verificada")}
                grdvPickingUbic.GroupSummary.Add(item5)

                Dim item6 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Cant_Pendiente",
                    .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}",
                    .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Cant_Pendiente")}
                grdvPickingUbic.GroupSummary.Add(item6)

                Dim item7 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Peso_Recibido",
                    .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}",
                    .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Peso_Recibido")}
                grdvPickingUbic.GroupSummary.Add(item7)

                Dim item8 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Peso_Verificado",
                    .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}",
                    .ShowInGroupColumnFooter = grdvPickingUbic.Columns("Peso_Verificado")}
                grdvPickingUbic.GroupSummary.Add(item8)

                grdvPickingUbic.Columns("Factor").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Factor").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Factor").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Factor").DisplayFormat.FormatString = "{0:n}"

                grdvPickingUbic.Columns("Cant_Ped_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Ped_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Ped_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Ped_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Ped_UmBas").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Ped_UmBas").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Ped_UmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Ped_UmBas").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Pick_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Pick_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Pick_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Pick_UMBas").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Pick_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Pick_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Pick_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Pick_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Veri_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Veri_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Veri_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Veri_UMBas").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Veri_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Veri_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Veri_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Veri_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Peso_Pick").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Peso_Pick").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Peso_Pick").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Peso_Pick").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Peso_Veri").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Peso_Veri").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Peso_Veri").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Peso_Veri").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Desp_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Desp_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Desp_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Desp_UMBas").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Desp_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Desp_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Desp_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Desp_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("Cant_Pendiente").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvPickingUbic.Columns("Cant_Pendiente").SummaryItem.DisplayFormat = "{0:n6}"
                grdvPickingUbic.Columns("Cant_Pendiente").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("Cant_Pendiente").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.ExpandAllGroups()
                grdvPickingUbic.BestFitColumns(True)

            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub Get_Stock_Res(ByVal pBePedidoEnc As clsBeTrans_pe_enc,
                             ByVal PendientesDeDespacho As Boolean)



        Try

            If DTStockRes.Columns.Count = 0 Then
                Set_Columns_DT_StockRes()
            End If

            pListObjSP = clsLnTrans_pe_det.Get_All_Stock_Res_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                            PendientesDeDespacho,
                                                                            False,
                                                                            False)

            If pListObjSP.Count = 0 Then
                pListObjSP = clsLnTrans_pe_det.Get_All_Despacho_Det_By_IdPedidoEnc(Val(lblC.Text))
            End If

            If pListObjSP.Count > 0 Then

                Dim vCantidadReservadaUMBas As Double = 0
                Dim vCantidadReservadaPres As Double = 0
                Dim vCantidadRecPres As Double = 0
                Dim vCantidadVerPres As Double = 0
                Dim vCantidadDespPres As Double = 0
                Dim vPesoReservado As Double = 0
                Dim vCantPendiente As Double = 0
                Dim BePresentacion As New clsBeProducto_Presentacion
                Dim BePedidoDet As New clsBeTrans_pe_det

                For Each BeVWStockRes As clsBeVW_stock_res In pListObjSP

                    BePedidoDet = pBePedidoEnc.Detalle.Find(Function(x) x.IdPedidoDet = BeVWStockRes.IdPedidoDet)

                    If (BeVWStockRes.IdPresentacion = 0) OrElse (BePedidoDet.IdPresentacion = 0) Then
                        vCantidadReservadaUMBas = BeVWStockRes.CantidadReservadaUMBas
                        vCantidadReservadaPres = 0
                        vCantidadRecPres = 0
                        vCantidadVerPres = 0
                        vPesoReservado = BeVWStockRes.Peso
                    Else

                        BePresentacion.IdPresentacion = BeVWStockRes.IdPresentacion
                        clsLnProducto_presentacion.GetSingle(BePresentacion)

                        'El pedido tiene presentación
                        If BePedidoDet.IdPresentacion <> 0 Then

                            If Not BePresentacion Is Nothing Then
                                vCantidadReservadaPres = BeVWStockRes.CantidadReservadaUMBas
                                BeVWStockRes.CantidadReservadaUMBas = Math.Round(BeVWStockRes.CantidadReservadaUMBas * BePresentacion.Factor, 6)
                                vCantidadReservadaUMBas = BeVWStockRes.CantidadReservadaUMBas
                                vCantidadRecPres = BeVWStockRes.Cantidad_Pickeada
                                BeVWStockRes.Cantidad_Pickeada = Math.Round(BeVWStockRes.Cantidad_Pickeada * BePresentacion.Factor, 6)
                                vCantidadVerPres = BeVWStockRes.Cantidad_Verificada
                                BeVWStockRes.Cantidad_Verificada = Math.Round(BeVWStockRes.Cantidad_Verificada * BePresentacion.Factor, 6)
                            Else
                                Throw New Exception("No se pudo obtener la presentación con identificador: " & BeVWStockRes.IdPresentacion)
                            End If

                        ElseIf (BeVWStockRes.IdPresentacion <> 0) OrElse (BePedidoDet.IdPresentacion = 0) Then

                            If Not BePresentacion Is Nothing Then
                                vCantidadReservadaPres = BeVWStockRes.CantidadReservadaUMBas
                                vCantidadReservadaUMBas = Math.Round(BeVWStockRes.CantidadReservadaUMBas * BePresentacion.Factor, 6)
                                BeVWStockRes.Cantidad_Pickeada = BeVWStockRes.Cantidad_Pickeada
                                BeVWStockRes.Cantidad_Verificada = BeVWStockRes.Cantidad_Verificada / BePresentacion.Factor
                            Else
                                Throw New Exception("No se pudo obtener la presentación con identificador: " & BeVWStockRes.IdPresentacion)
                            End If

                        End If

                        'vCantidadReservadaUMBas = Obj.CantidadReservadaUMBas
                        vPesoReservado = BeVWStockRes.Peso

                    End If

                    vCantPendiente = vCantidadReservadaUMBas - BeVWStockRes.Cantidad_Pickeada

                    If vCantPendiente = 0 Then
                        vCantPendiente = BeVWStockRes.Cantidad_Pickeada - BeVWStockRes.Cantidad_Verificada
                    End If

                    DTStockRes.Rows.Add(BeVWStockRes.IdPedido,
                                        BeVWStockRes.IdPicking,
                                        BeVWStockRes.Codigo_Producto,
                                        BeVWStockRes.Nombre_Producto,
                                        BeVWStockRes.Nombre_Presentacion,
                                        BeVWStockRes.NomEstado,
                                        BeVWStockRes.UMBas,
                                        BeVWStockRes.Propietario,
                                        BeVWStockRes.UbicacionActual.NombreCompleto,
                                        BeVWStockRes.Lote,
                                        BeVWStockRes.Lic_plate,
                                        BeVWStockRes.Fecha_Vence,
                                        BeVWStockRes.Factor,
                                        vCantidadReservadaPres,
                                        vCantidadReservadaUMBas,
                                        vCantidadRecPres,
                                        BeVWStockRes.Cantidad_Pickeada,
                                        vCantidadVerPres,
                                        BeVWStockRes.Cantidad_Verificada,
                                        vCantidadDespPres,
                                        BeVWStockRes.Cantidad_Despachada,
                                        vCantPendiente,
                                        BeVWStockRes.peso_pickeado,
                                        BeVWStockRes.peso_verificado,
                                        BeVWStockRes.encontrado,
                                        BeVWStockRes.acepto,
                                        BeVWStockRes.Fecha_ingreso,
                                        BeVWStockRes.IdStockRes,
                                        BeVWStockRes.IdProductoTallaColor,
                                        BeVWStockRes.Codigo_Talla,
                                        BeVWStockRes.Codigo_Color)

                    Application.DoEvents()

                Next

            End If

            grdUbicPicking.DataSource = DTStockRes

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Get_Stock_Res(ByVal pBePedidoEnc As clsBeTrans_pe_enc,
                             ByVal PendientesDeDespacho As Boolean,
                             ByVal lConnection As SqlConnection,
                             ByVal lTransaction As SqlTransaction)

        Try

            pListObjSP = clsLnTrans_pe_det.Get_All_Stock_Res_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                            PendientesDeDespacho,
                                                                            False,
                                                                            False,
                                                                            lConnection,
                                                                            lTransaction)

            'DTStockRes.Rows.Clear()

            If pListObjSP.Count > 0 Then

                Dim vCantidadReservadaUMBas As Double = 0
                Dim vCantidadReservadaPres As Double = 0
                Dim vCantidadRecPres As Double = 0
                Dim vCantidadVerPres As Double = 0
                Dim vCantidadDespPres As Double = 0
                Dim vPesoReservado As Double = 0
                Dim vCantPendiente As Double = 0
                Dim BePresentacion As New clsBeProducto_Presentacion
                Dim BePedidoDet As New clsBeTrans_pe_det

                If DTStockRes.Columns.Count = 0 Then

                    Set_Columns_DT_StockRes()

                End If

                If pBePedidoEnc.Detalle Is Nothing Then
                    pBePedidoEnc.Detalle = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc)
                End If

                For Each Obj As clsBeVW_stock_res In pListObjSP

                    BePedidoDet = pBePedidoEnc.Detalle.Find(Function(x) x.IdPedidoDet = Obj.IdPedidoDet)

                    '#EJC20190214_1210PM: Se optimizó a traves de la lista y búsqueda en memoria porque consumía muchos recursos..
                    'BePedidoDet.IdPedidoDet = Obj.IdPedidoDet
                    'clsLnTrans_pe_det.GetSingle(BePedidoDet)

                    If (Obj.IdPresentacion = 0) OrElse (BePedidoDet.IdPresentacion = 0) Then
                        vCantidadReservadaUMBas = Obj.CantidadReservadaUMBas
                        vCantidadReservadaPres = 0
                        vCantidadRecPres = 0
                        vCantidadVerPres = 0
                        vPesoReservado = Obj.Peso
                    Else

                        BePresentacion.IdPresentacion = Obj.IdPresentacion
                        clsLnProducto_presentacion.GetSingle(BePresentacion, lConnection, lTransaction)

                        'El pedido tiene presentación
                        If BePedidoDet.IdPresentacion <> 0 Then

                            If Not BePresentacion Is Nothing Then
                                vCantidadReservadaPres = Obj.CantidadReservadaUMBas
                                Obj.CantidadReservadaUMBas = Math.Round(Obj.CantidadReservadaUMBas * BePresentacion.Factor, 6)
                                vCantidadReservadaUMBas = Obj.CantidadReservadaUMBas
                                vCantidadRecPres = Obj.Cantidad_Pickeada
                                Obj.Cantidad_Pickeada = Math.Round(Obj.Cantidad_Pickeada * BePresentacion.Factor, 6)
                                vCantidadVerPres = Obj.Cantidad_Verificada
                                Obj.Cantidad_Verificada = Math.Round(Obj.Cantidad_Verificada * BePresentacion.Factor, 6)
                            Else
                                Throw New Exception("No se pudo obtener la presentación con identificador: " & Obj.IdPresentacion)
                            End If

                        ElseIf (Obj.IdPresentacion <> 0) OrElse (BePedidoDet.IdPresentacion = 0) Then

                            If Not BePresentacion Is Nothing Then
                                vCantidadReservadaPres = Obj.CantidadReservadaUMBas
                                vCantidadReservadaUMBas = Math.Round(Obj.CantidadReservadaUMBas * BePresentacion.Factor, 6)
                                Obj.Cantidad_Pickeada = Obj.Cantidad_Pickeada
                                Obj.Cantidad_Verificada = Obj.Cantidad_Verificada / BePresentacion.Factor
                            Else
                                Throw New Exception("No se pudo obtener la presentación con identificador: " & Obj.IdPresentacion)
                            End If

                        End If

                        'vCantidadReservadaUMBas = Obj.CantidadReservadaUMBas
                        vPesoReservado = Obj.Peso

                    End If

                    vCantPendiente = vCantidadReservadaUMBas - Obj.Cantidad_Pickeada

                    If vCantPendiente = 0 Then
                        vCantPendiente = Obj.Cantidad_Pickeada - Obj.Cantidad_Verificada
                    End If

                    DTStockRes.Rows.Add(
                                    Obj.IdPedido,
                                    Obj.IdPicking,
                                    Obj.Codigo_Producto,
                                    Obj.Nombre_Producto,
                                    Obj.Nombre_Presentacion,
                                    Obj.NomEstado,
                                    Obj.UMBas,
                                    Obj.Propietario,
                                    Obj.UbicacionActual.NombreCompleto,
                                    Obj.Lote,
                                    Obj.Lic_plate,
                                    Obj.Fecha_Vence,
                                    Obj.Factor,
                                    vCantidadReservadaPres,
                                    vCantidadReservadaUMBas,
                                    vCantidadRecPres,
                                    Obj.Cantidad_Pickeada,
                                    vCantidadVerPres,
                                    Obj.Cantidad_Verificada,
                                    vCantidadDespPres,
                                    Obj.Cantidad_Despachada,
                                    vCantPendiente,
                                    Obj.peso_pickeado,
                                    Obj.peso_verificado,
                                    Obj.encontrado,
                                    Obj.acepto,
                                    Obj.Fecha_ingreso,
                                    Obj.IdStockRes,
                                    Obj.IdProductoTallaColor,
                                    Obj.Codigo_Talla,
                                    Obj.Codigo_Color)

                    Application.DoEvents()

                Next

            End If

            grdUbicPicking.DataSource = DTStockRes

            '#GT21032025: sino tiene filas, lanza excepcion asignar nombres a columnas y formato a los valores
            If DTStockRes.Rows.Count > 0 Then
                Set_Formato_Grid_Stock_Res()
            End If


        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub SetProducto(ByVal pObj As clsBeTrans_pe_det)

        Try

            Dim BeTransPickingDet As New clsBeTrans_picking_det

            If lTransPickingDet IsNot Nothing AndAlso lTransPickingDet.Count > 0 Then
                Dim lIndexE As Integer = -1
                lIndexE = lTransPickingDet.FindIndex(Function(b) b.IdPedidoDet = pObj.IdPedidoDet)
                If lIndexE > -1 Then
                    Throw New Exception(String.Format("El Producto {0} ya fue agregado.", pObj.NombreProducto))
                End If
                BeTransPickingDet.IdPickingDet = lTransPickingDet.Max(Function(b) b.IdPickingDet) + 1
            Else
                BeTransPickingDet.IdPickingDet = 1
            End If

            ' Dim i As Integer = RowIndex()
            Dim i As Integer = grdProducto.Rows.Add()

            If pObj.IdPresentacion <> 0 Then

                Dim BePresentacion As New clsBeProducto_Presentacion
                BePresentacion = listaPresentaciones.Find(Function(x) x.IdPresentacion = pObj.IdPresentacion)
                'clsLnProducto_presentacion.GetSingle(BePresentacion)
                If Not BePresentacion Is Nothing Then
                    pObj.Nom_presentacion = BePresentacion.Nombre
                End If

            End If

            grdProducto.Rows(i).Cells("CodigoPedido").Value = pObj.Producto.Codigo
            grdProducto.Rows(i).Cells("ProductoDetalle").Value = pObj.Producto.Nombre
            grdProducto.Rows(i).Cells("PresentacionDetalle").Value = pObj.Nom_presentacion
            grdProducto.Rows(i).Cells("UMDetalle").Value = pObj.Nom_unid_med
            grdProducto.Rows(i).Cells("EstadoDetalle").Value = pObj.Nom_estado
            grdProducto.Rows(i).Cells("Cantidad").Value = pObj.Cantidad
            grdProducto.Rows(i).Cells("PedidoDet").Value = pObj.IdPedidoDet
            grdProducto.Rows(i).Cells("PedidoEnc").Value = pObj.IdPedidoEnc
            grdProducto.Rows(i).Cells("colTalla").Value = pObj.Talla
            grdProducto.Rows(i).Cells("colColor").Value = pObj.Color

            'CargarOperador(i)

            grdProducto.CommitEdit(DataGridViewDataErrorContexts.Commit)
            grdProducto.EndEdit()

            BeTransPickingDet.IdPedidoEnc = pObj.IdPedidoEnc
            BeTransPickingDet.IdPedidoDet = pObj.IdPedidoDet
            BeTransPickingDet.Cantidad = pObj.Cantidad
            BeTransPickingDet.User_agr = AP.UsuarioAp.IdUsuario
            BeTransPickingDet.Fec_agr = Now
            BeTransPickingDet.User_mod = AP.UsuarioAp.IdUsuario
            BeTransPickingDet.Fec_mod = Now
            BeTransPickingDet.Activo = True
            BeTransPickingDet.IsNew = True
            BeTransPickingDet.Producto = New clsBeProducto
            BeTransPickingDet.Producto.IdProducto = pObj.Producto.IdProducto
            BeTransPickingDet.Producto.Codigo = pObj.Producto.Codigo
            BeTransPickingDet.Producto.Nombre = pObj.Producto.Nombre
            BeTransPickingDet.Presentacion.IdPresentacion = pObj.IdPresentacion
            BeTransPickingDet.Presentacion.Nombre = pObj.Nom_presentacion
            BeTransPickingDet.UnidadMedida.IdUnidadMedida = pObj.IdUnidadMedidaBasica
            BeTransPickingDet.UnidadMedida.Nombre = pObj.Nom_unid_med
            BeTransPickingDet.ProductoEstado.IdEstado = pObj.IdEstado
            BeTransPickingDet.ProductoEstado.Nombre = pObj.Nom_estado
            BeTransPickingDet.Cantidad = pObj.Cantidad
            BeTransPickingDet.Cantidad_recibida = 0
            BeTransPickingDet.Cliente_dias = 0

            lTransPickingDet.Add(BeTransPickingDet)

            If pObj.ListaStockRes IsNot Nothing AndAlso pObj.ListaStockRes.Count > 0 Then

                For Each bo As clsBeStock_res In pObj.ListaStockRes

                    Dim ObjU As New clsBeTrans_picking_ubic() With {.IdPedidoDet = bo.IdPedidoDet,
                        .IdStockRes = bo.IdStockRes,
                        .IdPickingDet = BeTransPickingDet.IdPickingDet,
                        .IdUbicacion = bo.IdUbicacion,
                        .Lote = bo.Lote,
                        .Fecha_Vence = bo.Fecha_vence,
                        .Serial = bo.Serial,
                        .Lic_plate = bo.Lic_plate,
                        .Peso_solicitado = bo.Peso,
                        .Cantidad_Solicitada = bo.Cantidad,
                        .Cantidad_Recibida = 0.0,
                        .Fecha_real_vence = bo.Fecha_vence,
                        .User_agr = AP.UsuarioAp.IdUsuario,
                        .Fec_agr = Now,
                        .User_mod = AP.UsuarioAp.IdUsuario,
                        .Fec_mod = Now,
                        .Activo = True,
                        .IsNew = True}
                    lTransPickingUbic.Add(ObjU)

                    Application.DoEvents()

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged
        Try

            If cmbPropietario.ItemIndex > -1 Then
                If cmbBodega.Text <> "" Then
                    cmbPropietario.Tag = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, cmbPropietario.EditValue)
                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        Try

            If cmbBodega.ItemIndex > -1 Then
                cmbPropietario.Properties.DataSource = Nothing
                IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub grdvPickingUbic_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles grdvPickingUbic.RowCellStyle

        If e.Column.FieldName = "Cant_Pendiente" Then

            Dim View As GridView = sender
            Dim CantidadRes As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant_Pick_UMBas"))
            Dim CantidadVer As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant_Veri_UMBas"))
            Dim CantidadSol As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant_Ped_UmBas"))

            If Val(CantidadRes) <> Val(CantidadSol) Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf Val(CantidadRes) <> Val(CantidadVer) Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Yellow
                e.Appearance.BackColor2 = Color.Silver
            ElseIf Val(CantidadRes) = Val(CantidadVer) Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            End If

        End If

    End Sub

    Private Sub Genera_Reporte_Despacho()

        Try
            Dim pProcesado_Bof As Boolean
            Dim BeUsuarioEntrega As New clsBeUsuario
            Dim BeUsuario As New clsBeUsuario
            BeUsuario = clsLnUsuario.GetSingle(BeDespachoEnc.User_agr)
            Dim IdPedidoEnc As String = ""

            '#EJC20200714: Este valor se debe configurar en el ini bajo el nombre FORMATO_DESPACHO
            Dim TipoReporteDespacho As Integer = Val(clsBD.Instancia.Formato_Despacho)

            If BeDespachoEnc.IdDespachoEnc > 0 Then

                Select Case TipoReporteDespacho

                    Case 1 '#EJC20200714: Formato para CLC

                        Dim dt As DataTable = clsLnTrans_despacho_enc.Get_Reporte_Despacho(BeDespachoEnc.IdDespachoEnc)

                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If Not IsDBNull(dt.Rows(0)("IdPedidoEnc")) Then
                                IdPedidoEnc = dt.Rows(0)("IdPedidoEnc")
                            End If
                        End If

                        Dim Rep As New rptDespacho
                        Rep.DataSource = clsLnTrans_despacho_enc.Get_Reporte_Despacho(BeDespachoEnc.IdDespachoEnc)
                        Rep.DataMember = "Result"
                        Rep.Parameters("Empresa").Value = AP.NomEmpresa
                        Rep.Parameters("Empresa").Visible = False
                        Rep.Parameters("IdPedidoEnc").Value = IdPedidoEnc
                        Rep.Parameters("IdPedidoEnc").Visible = False
                        Rep.Parameters("Bodega").Value = AP.NomBodega
                        Rep.Parameters("Bodega").Visible = False
                        Rep.RequestParameters = False

                        Rep.MostrarEncabezadoSoloEnPrimeraPagina = (Not mnuRepetirEncabezadoEnCadaPagina.Checked)

                        If clsLnEmpresa.GetImagen(AP.IdEmpresa) Is Nothing Then
                            Rep.XrLogo.Image = Nothing
                        Else
                            Rep.XrLogo.Image = clsPublic.ByteArrayToImage(clsLnEmpresa.GetImagen(AP.IdEmpresa))
                        End If

                        Rep.ShowPreview()

                    Case 2 '#EJC20200714: Formato para IDEALSA

                        'GT 15042021 el formato de despacho Idealsa usa parametro UsuarioDespacho.
                        Dim Rep As New rptDespachoFIdealsa
                        Rep.DataSource = clsLnTrans_despacho_enc.Get_Reporte_Despacho(BeDespachoEnc.IdDespachoEnc)
                        Rep.DataMember = "Result"
                        Rep.Parameters("Empresa").Value = AP.NomEmpresa
                        Rep.Parameters("Empresa").Visible = False
                        Rep.Parameters("Bodega").Value = AP.NomBodega
                        Rep.Parameters("Bodega").Visible = False
                        Rep.Parameters("NoBultos").Value = BeDespachoEnc.Cant_bultos
                        Rep.Parameters("NoBultos").Visible = False
                        Rep.Parameters("UsuarioDespacho").Value = BeUsuario.Nombres + " " + BeUsuario.Apellidos
                        Rep.Parameters("UsuarioDespacho").Visible = False
                        Rep.RequestParameters = False
                        Rep.ShowPreview()

                    Case 3 '#EJC20220427: Formato para CEALSA

                        Dim DT As New DataTable
                        DT = clsLnTrans_despacho_enc.Get_Reporte_Despacho(BeDespachoEnc.IdDespachoEnc)

                        Dim vNombreOperadorPickeo As String = ""

                        If Not DT Is Nothing Then

                            If DT.Rows.Count > 0 Then

                                Dim vIdOperadorPicking As Integer = DT.Rows(0).Item("IdOperadorBodega_Pickeo")
                                Dim vIdPickingEnc As Integer = DT.Rows(0).Item("IdPickingEnc")
                                Dim BeOperadorBodega As New clsBeOperador_bodega
                                BeOperadorBodega.IdOperadorBodega = vIdOperadorPicking

                                '#GT19052025: utilizar el picking para validar que fue procesado bof o no, para mostrar al usuario/operador correcto en el doc de despacho.
                                Dim pPickingEnc = clsLnTrans_picking_enc.GetSingle(vIdPickingEnc)
                                If pPickingEnc IsNot Nothing Then
                                    pProcesado_Bof = pPickingEnc.procesado_bof
                                End If

                                If pProcesado_Bof Then

                                    If vIdOperadorPicking > 0 Then
                                        '#GT17072025: operado por BOF significa que el usuario es quien debe mostrarse en el reporte
                                        If pProcesado_Bof Then
                                            BeUsuarioEntrega = clsLnUsuario.GetSingle(vIdOperadorPicking)
                                            If BeUsuarioEntrega IsNot Nothing Then
                                                vNombreOperadorPickeo = BeUsuarioEntrega.Nombres + " " + BeUsuarioEntrega.Apellidos
                                            End If
                                        Else

                                            If clsLnOperador_bodega.GetSingle(BeOperadorBodega) Then

                                                    Dim BeOperador As New clsBeOperador
                                                    BeOperador.IdOperador = BeOperadorBodega.IdOperador
                                                    If clsLnOperador.GetSingle(BeOperador) Then
                                                        vNombreOperadorPickeo = BeOperador.Nombres + " " + BeOperador.Apellidos
                                                    End If
                                                End If

                                            End If

                                        End If



                                    End If

                                End If

                                '#EJC20220301 el formato de Cealsa usa parametro UsuarioDespacho.
                                Dim RepCealsa As New rptDespachofCealsa
                                RepCealsa.DataSource = DT
                                RepCealsa.DataMember = "Result"
                                RepCealsa.Parameters("Empresa").Value = AP.NomEmpresa
                                RepCealsa.Parameters("Empresa").Visible = False
                                RepCealsa.Parameters("Bodega").Value = AP.NomBodega
                                RepCealsa.Parameters("Bodega").Visible = False
                                RepCealsa.Parameters("Entregado_Por").Value = vNombreOperadorPickeo
                                RepCealsa.Parameters("Entregado_Por").Visible = False
                                RepCealsa.Parameters("Autorizado_Por").Value = BeUsuario.Nombres + " " + BeUsuario.Apellidos
                                RepCealsa.Parameters("Autorizado_Por").Visible = False
                                RepCealsa.RequestParameters = False

                                If clsLnEmpresa.GetImagen(AP.IdEmpresa) Is Nothing Then
                                    RepCealsa.XrLogo.Image = Nothing
                                Else
                                    RepCealsa.XrLogo.Image = clsPublic.ByteArrayToImage(clsLnEmpresa.GetImagen(AP.IdEmpresa))
                                End If

                                RepCealsa.ShowPreview()

                                Case 4 '#CKFK20220801: Formato para DyD

                                Dim Rep As New rptDespachofDyD
                                Rep.DataSource = clsLnTrans_despacho_enc.Get_Reporte_Despacho_DyD(BeDespachoEnc.IdDespachoEnc)
                                Rep.DataMember = "Result"
                                Rep.Parameters("Empresa").Value = AP.NomEmpresa
                                Rep.Parameters("Empresa").Visible = False
                                Rep.Parameters("Bodega").Value = AP.NomBodega
                                Rep.Parameters("Bodega").Visible = False
                                Rep.RequestParameters = False
                                Rep.ShowPreview()

                                Case 5 '#MECR29082025: Formato para MAMPA
                                Dim Rep As New rptDespachoMAMPA
                                Rep.DataSource = clsLnTrans_despacho_enc.Get_Reporte_Despacho(BeDespachoEnc.IdDespachoEnc)
                                Rep.DataMember = "Result"
                                Rep.Parameters("Empresa").Value = AP.NomEmpresa
                                Rep.Parameters("Empresa").Visible = False
                                Rep.Parameters("Bodega").Value = AP.NomBodega
                                Rep.Parameters("Bodega").Visible = False
                                Rep.RequestParameters = False

                                Rep.MostrarEncabezadoSoloEnPrimeraPagina = (Not mnuRepetirEncabezadoEnCadaPagina.Checked)

                                If clsLnEmpresa.GetImagen(AP.IdEmpresa) Is Nothing Then
                                    Rep.XrLogo.Image = Nothing
                                Else
                                    Rep.XrLogo.Image = clsPublic.ByteArrayToImage(clsLnEmpresa.GetImagen(AP.IdEmpresa))
                                End If

                                Rep.ShowPreview()

                                Case Else

                                Dim Rep As New rptDespacho
                                Rep.DataSource = clsLnTrans_despacho_enc.Get_Reporte_Despacho(BeDespachoEnc.IdDespachoEnc)
                                Rep.DataMember = "Result"
                                Rep.Parameters("Empresa").Value = AP.NomEmpresa
                                Rep.Parameters("Empresa").Visible = False
                                Rep.Parameters("Bodega").Value = AP.NomBodega
                                Rep.Parameters("Bodega").Visible = False
                                Rep.RequestParameters = False
                                Rep.ShowPreview()

                End Select

            Else
                Throw New Exception("No hay datos para generar el reporte.")
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al generar documento de despacho: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub Generar_Reporte_Packing()

        Try

            Dim DT As New DataTable
            DT = clsLnTrans_despacho_enc.Get_Reporte_Packing(BeDespachoEnc.IdDespachoEnc)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    If AP.Bodega.Control_Talla_Color Then
                        Dim Rep As New rptPackingTallaColor
                        Rep.DataSource = DT
                        Rep.DataMember = "Result"
                        Rep.Parameters("Empresa").Value = AP.NomEmpresa
                        Rep.Parameters("Empresa").Visible = False
                        Rep.Parameters("Bodega").Value = AP.NomBodega
                        Rep.Parameters("Bodega").Visible = False
                        Rep.RequestParameters = False

                        Rep.MostrarEncabezadoSoloEnPrimeraPagina = False

                        Rep.ShowPreview()
                    Else
                        Dim Rep As New rptPackingPorBarra
                        Rep.DataSource = DT
                        Rep.DataMember = "Result"
                        Rep.Parameters("Empresa").Value = AP.NomEmpresa
                        Rep.Parameters("Empresa").Visible = False
                        Rep.Parameters("Bodega").Value = AP.NomBodega
                        Rep.Parameters("Bodega").Visible = False
                        Rep.RequestParameters = False

                        Rep.MostrarEncabezadoSoloEnPrimeraPagina = False

                        Rep.ShowPreview()
                    End If




                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                  Text,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub lnkPedido_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles lnkPedido.ItemClick
        lnkPedido.Enabled = False
        Agregar_Pedido()
        lnkPedido.Enabled = True
    End Sub

    Private Sub frmDespacho_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Dim hora_server As DateTime
        grdListaDespacho.Rows.Clear()
        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Cargando Despacho...")

        Try

            oDateTimePicker = New DateTimePicker
            oDateTimePicker.Format = DateTimePickerFormat.Short
            oDateTimePicker.Visible = False
            oDateTimePicker.Width = 80
            grdListaDespacho.Controls.Add(oDateTimePicker)

            AddHandler oDateTimePicker.ValueChanged, AddressOf oDateTimePicker_ValueChanged

            AP.Listar_Bodegas_By_Usuario(cmbBodega)
            grdListaDespacho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            grdPedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            grdProducto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            Set_Columns_DT_StockRes()

            grdPedido.Columns("FechaPedido").DefaultCellStyle.Format = "d"
            grdListaDespacho.Columns("Fecha").DefaultCellStyle.Format = "d"
            grdListaDespacho.AllowUserToResizeRows = False
            grdListaDespacho.AllowUserToResizeColumns = False

            '#CKFK20181001: Colocar bodega por defecto.
            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
            '#GT09032023: se bloquea para evitar mezclar pedidos de distintas bodegas.
            cmbBodega.Enabled = False

            cmbBodega.RefreshEditValue()

            '#EJC20220318: Listar pilotos en lookup
            Llena_PilotosLookUp_Grid()
            Llena_VehiculosLookUp_Grid()
            Llena_RutasLookUp_Grid()

            '#EJC20200714: Este valor se debe configurar en el ini bajo el nombre FORMATO_DESPACHO
            Dim TipoReporteDespacho As Integer = Val(clsBD.Instancia.Formato_Despacho)

            If BeDespachoEnc.IdDespachoEnc > 0 Then

                Select Case TipoReporteDespacho
                    Case 1 '#EJC20200714: Forma
                        mnuRepetirEncabezadoEnCadaPagina.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                        mnuRepetirEncabezadoEnCadaPagina.Checked = True
                    Case 5 '#MECR29082025: Se agrego forma para MAMPA
                        mnuRepetirEncabezadoEnCadaPagina.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                        mnuRepetirEncabezadoEnCadaPagina.Checked = True
                    Case Else
                        mnuRepetirEncabezadoEnCadaPagina.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                        mnuRepetirEncabezadoEnCadaPagina.Checked = False
                End Select

            End If

            Select Case Modo

                Case TipoTrans.Nuevo

                    hora_server = clsServidor.Get_Fecha_Servidor()

                    lblC.Text = ""
                    lblEstado.Text = "Nuevo"
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    mnuGuardar.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)

                    mnuActualizar.Enabled = False
                    mnuAnular.Enabled = False
                    mnuGuardarDatosCabecera.Enabled = False
                    lnkPedido.Enabled = True
                    mnuImprimir.Enabled = False
                    'dtmFechaDespacho.DateTime = Today
                    'dtmFechaTarea.DateTime = Today
                    dtmFechaDespacho.DateTime = hora_server
                    dtmFechaTarea.DateTime = hora_server
                    BeDespachoEnc = New clsBeTrans_despacho_enc With {.IsNew = True}
                    txtDocumentoExterno.Text = ""

                    If BePedidoEnc.IdPedidoEnc > 0 Then
                        Agregar_Pedido(BePedidoEnc)
                    End If

                Case TipoTrans.Editar

                    mnuGuardar.Enabled = False

                    mnuActualizar.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    mnuAnular.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Eliminar, True)

                    lnkPedido.Enabled = False

                    CargarDatos()

            End Select

            WindowState = FormWindowState.Maximized

            Focus()

            lcmbPiloto.Focus()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimirDespacho.ItemClick
        Genera_Reporte_Despacho()
    End Sub

    Private Sub mnuImprimirListaEmpaque_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimirListaEmpaque.ItemClick
        Generar_Reporte_Packing()
    End Sub

    Private DTPilotos As New DataTable


    Private Sub Llena_PilotosLookUp_Grid()

        Try

            DTPilotos = clsLnEmpresa_transporte_pilotos.Listar_For_Despacho(True)

            lcmbPiloto.Properties.DataSource = DTPilotos
            lcmbPiloto.Properties.DisplayMember = "Nombres"
            lcmbPiloto.Properties.ValueMember = "IdPiloto"
            lcmbPiloto.Properties.PopulateColumns()
            lcmbPiloto.Properties.PopupWidth = 700
            lcmbPiloto.Properties.BestFit()
            lcmbPiloto.Properties.NullText = ""

            If lcmbPiloto.Properties.Columns.Count > 0 Then
                lcmbPiloto.Properties.Columns(0).Visible = False
                lcmbPiloto.Properties.Columns(1).Visible = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_VehiculosLookUp_Grid()

        Try

            '#GT23032022: uso el grid privado de Vehiculos
            'Dim DTPilotos As New DataTable
            'DTPilotos = clsLnEmpresa_transporte_vehiculos.Listar_For_Despacho(True)
            DTVehiculos = clsLnEmpresa_transporte_vehiculos.Listar_For_Despacho(True)
            'lcmbVehiculo.Properties.DataSource = DTPilotos
            lcmbVehiculo.Properties.DataSource = DTVehiculos
            lcmbVehiculo.Properties.DisplayMember = "Placa"
            lcmbVehiculo.Properties.ValueMember = "IdVehiculo"
            lcmbVehiculo.Properties.PopulateColumns()
            lcmbVehiculo.Properties.PopupWidth = 700
            lcmbVehiculo.Properties.BestFit()
            lcmbVehiculo.Properties.NullText = ""

            If lcmbVehiculo.Properties.Columns.Count > 0 Then
                lcmbVehiculo.Properties.Columns(0).Visible = False
                lcmbVehiculo.Properties.Columns(1).Visible = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_RutasLookUp_Grid()

        Try

            Dim DTRutas As New DataTable
            DTRutas = clsLnRoad_ruta.Listar_RoadRutas(True)
            lcmbRuta.Properties.DataSource = DTRutas
            lcmbRuta.Properties.DisplayMember = "Nombre"
            lcmbRuta.Properties.ValueMember = "IdRuta"
            lcmbRuta.Properties.PopulateColumns()
            lcmbRuta.Properties.PopupWidth = 700
            lcmbRuta.Properties.BestFit()
            lcmbRuta.Properties.NullText = ""

            If lcmbRuta.Properties.Columns.Count > 0 Then
                lcmbRuta.Properties.Columns(0).Visible = True
                lcmbRuta.Properties.Columns(1).Visible = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private pNuevoPiloto As New clsBeEmpresa_transporte_pilotos

    '#EJC20220320:Accept new record from lookupedit for piloto.
    Private Sub lcmbPiloto_ProcessNewValue(ByVal sender As Object, ByVal e As ProcessNewValueEventArgs) Handles lcmbPiloto.ProcessNewValue

        Try

            If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Agregar a: '" & e.DisplayValue.ToString() & "' al listado de pilotos?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Nuevo_Piloto(e.DisplayValue.ToString.Trim())
                lcmbPiloto.Refresh()
                e.Handled = True
                If Not pNuevoPiloto Is Nothing Then
                    lcmbPiloto.EditValue = pNuevoPiloto.IdPiloto
                    e.DisplayValue = pNuevoPiloto.Nombres + " " + pNuevoPiloto.Apellidos
                    lcmbPiloto.RefreshEditValue()
                    Application.DoEvents()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub


    '#EJC20220320:Accept new record from lookupedit for vehiculo.
    Private Sub lcmbVehiculo_ProcessNewValue(ByVal sender As Object, ByVal e As ProcessNewValueEventArgs) Handles lcmbVehiculo.ProcessNewValue

        Try

            If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Agregar a: '" & e.DisplayValue.ToString() & "' al listado de vehiculos?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Nuevo_Vehiculo(e.DisplayValue.ToString.Trim())
                lcmbVehiculo.Refresh()
                e.Handled = True
                If Not pNuevoVehiculo Is Nothing Then
                    lcmbVehiculo.EditValue = pNuevoVehiculo.IdVehiculo
                    e.DisplayValue = pNuevoVehiculo.Placa
                    lcmbVehiculo.RefreshEditValue()
                    Application.DoEvents()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub


    Private Sub Nuevo_Piloto(ByVal pNombresPiloto As String)

        pNuevoPiloto = Nothing

        Try

            'lcmbPiloto.EditValue = Nothing
            'lcmbPiloto.Properties.DataSource = Nothing

            Dim vFrmPiloto As New frmEmpresa_Transporte_Piloto
            vFrmPiloto.Modo = frmEmpresa_Transporte_Piloto.TipoTrans.Nuevo
            vFrmPiloto.WindowState = FormWindowState.Normal
            vFrmPiloto.StartPosition = FormStartPosition.CenterScreen

            If OpcionesMenu IsNot Nothing Then
                vFrmPiloto.OpcionesMenu = OpcionesMenu
                vFrmPiloto.mnuGuardar.Enabled = vFrmPiloto.OpcionesMenu.Modificar
                vFrmPiloto.mnuActualizar.Enabled = vFrmPiloto.OpcionesMenu.Modificar
                vFrmPiloto.mnuEliminar.Enabled = vFrmPiloto.OpcionesMenu.Eliminar
            End If

            vFrmPiloto.txtNombres.Text = pNombresPiloto
            vFrmPiloto.ShowDialog()

            'Llena_PilotosLookUp_Grid()

            pNuevoPiloto = vFrmPiloto.pObjBEJ

            If Not pNuevoPiloto Is Nothing Then
                Add_New_Piloto_To_DT(vFrmPiloto.pObjBEJ)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub Nuevo_Vehiculo(ByVal pPlaca As String)

        pNuevoVehiculo = Nothing

        Try

            Dim vFrmVehiculo As New frmEmpresa_Transporte_Vehiculo
            vFrmVehiculo.Modo = frmEmpresa_Transporte_Vehiculo.TipoTrans.Nuevo
            vFrmVehiculo.WindowState = FormWindowState.Normal
            vFrmVehiculo.StartPosition = FormStartPosition.CenterScreen
            vFrmVehiculo.txtPlaca.Text = pPlaca

            If OpcionesMenu IsNot Nothing Then
                vFrmVehiculo.OpcionesMenu = OpcionesMenu
                vFrmVehiculo.mnuGuardar.Enabled = vFrmVehiculo.OpcionesMenu.Modificar
                vFrmVehiculo.mnuActualizar.Enabled = vFrmVehiculo.OpcionesMenu.Modificar
                vFrmVehiculo.mnuEliminar.Enabled = vFrmVehiculo.OpcionesMenu.Eliminar
            End If

            vFrmVehiculo.ShowDialog()

            pNuevoVehiculo = vFrmVehiculo.pObjBEJ

            If Not pNuevoVehiculo Is Nothing Then
                Add_New_Vehiculo_To_DT(vFrmVehiculo.pObjBEJ)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
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

    Private Sub Add_New_Piloto_To_DT(ByVal BePiloto As clsBeEmpresa_transporte_pilotos)

        Try

            Dim R As DataRow = DTPilotos.NewRow
            R("IdEmpresaTransporte") = BePiloto.IdEmpresaTransporte
            R("IdPiloto") = BePiloto.IdPiloto
            R("Nombres") = BePiloto.Nombres + " " + BePiloto.Apellidos
            R("No_DPI") = BePiloto.No_dpi
            R("No_Licencia") = BePiloto.No_Licencia
            R("Dirección") = BePiloto.Direccion
            DTPilotos.Rows.Add(R)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub Add_New_Vehiculo_To_DT(ByVal BeVehiculo As clsBeEmpresa_transporte_vehiculos)

        Try

            Dim R As DataRow = DTVehiculos.NewRow
            R("IdEmpresaTransporte") = BeVehiculo.IdEmpresaTransporte
            R("IdVehiculo") = BeVehiculo.IdVehiculo
            R("Placa") = BeVehiculo.Placa
            R("Marca") = BeVehiculo.Marca
            R("Modelo") = BeVehiculo.Modelo
            DTVehiculos.Rows.Add(R)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuGuardarDatosCabecera_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardarDatosCabecera.ItemClick

        Try

            If XtraMessageBox.Show("¿Actualizar encabezado del despacho?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

                If Actualizar_Encabezado() Then

                    SplashScreenManager.CloseForm(False)

                    If XtraMessageBox.Show("Se actualizó el despacho, ¿Imprimir documento de salida?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Genera_Reporte_Despacho()
                        Generar_Reporte_Packing()
                    End If

                    If Not InvokeListarDespacho Is Nothing Then
                        InvokeListarDespacho.Invoke
                    End If

                    '#EJC20210912: Actualiza el número de despacho en el pedido.
                    If Not InvokeGetDespachoEnPedido Is Nothing Then
                        InvokeGetDespachoEnPedido.Invoke
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

                    Close()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show("Error al guardar el despacho: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Function Actualizar_Encabezado() As Boolean

        Actualizar_Encabezado = False

        Try

            BeDespachoEnc.IdBodega = cmbBodega.EditValue
            BeDespachoEnc.IdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(BeDespachoEnc.IdBodega)
            BeDespachoEnc.IdPropietarioBodega = cmbPropietario.EditValue
            BeDespachoEnc.IdPiloto = Val(lcmbPiloto.EditValue)
            BeDespachoEnc.IdVehiculo = Val(lcmbVehiculo.EditValue)
            BeDespachoEnc.IdRuta = Val(lcmbRuta.EditValue)
            BeDespachoEnc.Fecha = Now
            BeDespachoEnc.No_pase = Val(txtNoPase.Value)
            BeDespachoEnc.Observacion = txtObservacion.Text.Trim
            BeDespachoEnc.Hora_ini = dtmHoraIhh.Value
            BeDespachoEnc.Hora_fin = dtmHoraFhh.Value
            BeDespachoEnc.Estado = "Finalizado" 'lblEstado.Text
            BeDespachoEnc.Numero = txtNumero.Value
            BeDespachoEnc.Marchamo = txtMarchamo.Text.Trim
            BeDespachoEnc.Cant_bultos = txtCantidadBultos.Value

            If BeDespachoEnc.IsNew Then
                BeDespachoEnc.User_agr = AP.UsuarioAp.IdUsuario
                BeDespachoEnc.Fec_agr = Now
            End If

            BeDespachoEnc.User_mod = AP.UsuarioAp.IdUsuario
            BeDespachoEnc.Fec_mod = Now
            BeDespachoEnc.Activo = chkActivo.Checked

            If clsLnTrans_despacho_enc.Actualizar_Encabezado(BeDespachoEnc) Then

                Actualizar_Encabezado = True

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Sub SetProducto(ByVal pObj As clsBeTrans_pe_det, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            Dim BeTransPickingDet As New clsBeTrans_picking_det

            If lTransPickingDet IsNot Nothing AndAlso lTransPickingDet.Count > 0 Then
                Dim lIndexE As Integer = -1
                lIndexE = lTransPickingDet.FindIndex(Function(b) b.IdPedidoDet = pObj.IdPedidoDet)
                If lIndexE > -1 Then
                    Throw New Exception(String.Format("El Producto {0} ya fue agregado.", pObj.NombreProducto))
                End If
                BeTransPickingDet.IdPickingDet = lTransPickingDet.Max(Function(b) b.IdPickingDet) + 1
            Else
                BeTransPickingDet.IdPickingDet = 1
            End If

            ' Dim i As Integer = RowIndex()
            Dim i As Integer = grdProducto.Rows.Add()

            If pObj.IdPresentacion <> 0 Then

                Dim BePresentacion As New clsBeProducto_Presentacion With {.IdPresentacion = pObj.IdPresentacion}
                BePresentacion = clsLnProducto_presentacion.GetSingle(pObj.IdPresentacion, lConnection, lTransaction)
                pObj.Nom_presentacion = BePresentacion.Nombre

            End If

            grdProducto.Rows(i).Cells("CodigoPedido").Value = pObj.Producto.Codigo
            grdProducto.Rows(i).Cells("ProductoDetalle").Value = pObj.Producto.Nombre
            grdProducto.Rows(i).Cells("PresentacionDetalle").Value = pObj.Nom_presentacion
            grdProducto.Rows(i).Cells("UMDetalle").Value = pObj.Nom_unid_med
            grdProducto.Rows(i).Cells("EstadoDetalle").Value = pObj.Nom_estado
            grdProducto.Rows(i).Cells("Cantidad").Value = pObj.Cantidad
            grdProducto.Rows(i).Cells("PedidoDet").Value = pObj.IdPedidoDet
            grdProducto.Rows(i).Cells("PedidoEnc").Value = pObj.IdPedidoEnc
            grdProducto.Rows(i).Cells("colTalla").Value = pObj.Talla
            grdProducto.Rows(i).Cells("colColor").Value = pObj.Color

            'CargarOperador(i)

            grdProducto.CommitEdit(DataGridViewDataErrorContexts.Commit)
            grdProducto.EndEdit()

            BeTransPickingDet.IdPedidoEnc = pObj.IdPedidoEnc
            BeTransPickingDet.IdPedidoDet = pObj.IdPedidoDet
            BeTransPickingDet.Cantidad = pObj.Cantidad
            BeTransPickingDet.User_agr = AP.UsuarioAp.IdUsuario
            BeTransPickingDet.Fec_agr = Now
            BeTransPickingDet.User_mod = AP.UsuarioAp.IdUsuario
            BeTransPickingDet.Fec_mod = Now
            BeTransPickingDet.Activo = True
            BeTransPickingDet.IsNew = True

            BeTransPickingDet.Producto = New clsBeProducto
            BeTransPickingDet.Producto.IdProducto = pObj.Producto.IdProducto
            BeTransPickingDet.Producto.Codigo = pObj.Producto.Codigo
            BeTransPickingDet.Producto.Nombre = pObj.Producto.Nombre

            BeTransPickingDet.Presentacion.IdPresentacion = pObj.IdPresentacion
            BeTransPickingDet.Presentacion.Nombre = pObj.Nom_presentacion

            BeTransPickingDet.UnidadMedida.IdUnidadMedida = pObj.IdUnidadMedidaBasica
            BeTransPickingDet.UnidadMedida.Nombre = pObj.Nom_unid_med

            BeTransPickingDet.ProductoEstado.IdEstado = pObj.IdEstado
            BeTransPickingDet.ProductoEstado.Nombre = pObj.Nom_estado

            BeTransPickingDet.Cantidad = pObj.Cantidad
            BeTransPickingDet.Cantidad_recibida = 0
            BeTransPickingDet.Cliente_dias = 0

            lTransPickingDet.Add(BeTransPickingDet)

            If BeDespachoEnc.Estado = "Finalizado" Then

                Dim resultado As List(Of clsBeTrans_picking_ubic) = clsLnTrans_despacho_det.Get_All_PickingUbic_By_IdPedidoDet(pObj.IdPedidoDet,
                                                                                                                               pObj.IdPedidoEnc,
                                                                                                                               BeDespachoEnc.IdDespachoEnc,
                                                                                                                               lConnection,
                                                                                                                               lTransaction)
                If resultado IsNot Nothing Then
                    pObj.ListaPickingUbic = resultado.ToList()
                Else
                    pObj.ListaPickingUbic = New List(Of clsBeTrans_picking_ubic)
                End If

                If pObj.ListaPickingUbic IsNot Nothing AndAlso pObj.ListaPickingUbic.Count > 0 Then

                    For Each bo As clsBeTrans_picking_ubic In pObj.ListaPickingUbic

                        Dim ObjU As New clsBeTrans_picking_ubic() With {.IdPedidoDet = bo.IdPedidoDet,
                        .IdStockRes = bo.IdStockRes,
                        .IdPickingDet = BeTransPickingDet.IdPickingDet,
                        .IdUbicacion = bo.IdUbicacion,
                        .Lote = bo.Lote,
                        .Fecha_Vence = bo.Fecha_Vence,
                        .Serial = bo.Serial,
                        .Lic_plate = bo.Lic_plate,
                        .Peso_solicitado = bo.Peso_solicitado,
                        .Cantidad_Solicitada = bo.Cantidad_Solicitada,
                        .Cantidad_Recibida = 0.0,
                        .Fecha_real_vence = bo.Fecha_Vence,
                        .User_agr = AP.UsuarioAp.IdUsuario,
                        .Fec_agr = Now,
                        .User_mod = AP.UsuarioAp.IdUsuario,
                        .Fec_mod = Now,
                        .Activo = True,
                        .IsNew = True,
                        .IdProductoTallaColor = bo.IdProductoTallaColor,
                        .Codigo_Talla = bo.Codigo_Talla,
                        .Codigo_Color = bo.Codigo_Color}
                        lTransPickingUbic.Add(ObjU)

                        Application.DoEvents()

                    Next

                End If

            Else

                If pObj.ListaStockRes IsNot Nothing AndAlso pObj.ListaStockRes.Count > 0 Then

                    For Each bo As clsBeStock_res In pObj.ListaStockRes

                        Dim ObjU As New clsBeTrans_picking_ubic() With {.IdPedidoDet = bo.IdPedidoDet,
                        .IdStockRes = bo.IdStockRes,
                        .IdPickingDet = BeTransPickingDet.IdPickingDet,
                        .IdUbicacion = bo.IdUbicacion,
                        .Lote = bo.Lote,
                        .Fecha_Vence = bo.Fecha_vence,
                        .Serial = bo.Serial,
                        .Lic_plate = bo.Lic_plate,
                        .Peso_solicitado = bo.Peso,
                        .Cantidad_Solicitada = bo.Cantidad,
                        .Cantidad_Recibida = 0.0,
                        .Fecha_real_vence = bo.Fecha_vence,
                        .User_agr = AP.UsuarioAp.IdUsuario,
                        .Fec_agr = Now,
                        .User_mod = AP.UsuarioAp.IdUsuario,
                        .Fec_mod = Now,
                        .Activo = True,
                        .IsNew = True,
                        .IdProductoTallaColor = bo.IdProductoTallaColor,
                        .Codigo_Talla = bo.Talla,
                        .Nombre_Color = bo.Color}

                        lTransPickingUbic.Add(ObjU)

                        Application.DoEvents()

                    Next

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#CKFK20250324 Creé esta función para validar si los pedidos tienen los packing finalizados
    Public Function Pedidos_Tienen_Packing_Finalizado() As Boolean

        '#CKFK20250324 Inicialicé la función en true
        Pedidos_Tienen_Packing_Finalizado = True

        Try

            If Not BeDespachoEnc.ListaPedidos Is Nothing Then

                For Each Ped In BeDespachoEnc.ListaPedidos

                    If Ped.Picking.Requiere_Preparacion Then
                        If clsLnTrans_pe_enc.Tiene_Packing(Ped.IdPedidoEnc) Then
                            If Not clsLnTrans_pe_enc.Packing_Finalizado(Ped.IdPedidoEnc) Then
                                Return False
                            End If
                        End If
                    End If

                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK20250324 Creé esta función para validar si existe diferencia entre packing y despacho
    Public Function Existe_Diferencia_Despacho_vrs_Packing() As Boolean

        '#CKFK20220624 Inicialicé la función en False
        Existe_Diferencia_Despacho_vrs_Packing = False

        Try

            If Not BeDespachoEnc.ListaPedidos Is Nothing Then

                For Each Ped In BeDespachoEnc.ListaPedidos

                    If Ped.Picking.Requiere_Preparacion Then

                        Dim vCantidadPacking As Double = 0

                        gvPacking.Columns("cantidad_bultos_packing").Summary.Add(SummaryItemType.Sum, "", "{0:N2}")

                        vCantidadPacking = Math.Round(Convert.ToDouble(gvPacking.Columns("cantidad_bultos_packing").SummaryItem.SummaryValue), 6)

                        Dim vCantidadADespachar As Double = 0
                        Dim vCantidadDespachada As Double = 0

                        grdvPickingUbic.Columns(18).Summary.Add(SummaryItemType.Sum, "", "{0:N2}")
                        vCantidadADespachar = Math.Round(Convert.ToDouble(grdvPickingUbic.Columns(18).SummaryItem.SummaryValue), 6)
                        grdvPickingUbic.Columns(20).Summary.Add(SummaryItemType.Sum, "", "{0:N2}")
                        vCantidadDespachada = Math.Round(Convert.ToDouble(grdvPickingUbic.Columns(20).SummaryItem.SummaryValue), 6)

                        If vCantidadADespachar - vCantidadDespachada <> vCantidadPacking Then
                            Return True
                        End If
                    End If

                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class