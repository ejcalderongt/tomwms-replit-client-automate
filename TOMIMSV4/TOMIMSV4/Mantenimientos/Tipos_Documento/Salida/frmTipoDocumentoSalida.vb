Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmTipoDocumentoSalida
    Public pObjPT As New clsBeTrans_pe_tipo
    Public Delegate Sub Listar_Tipo_Documentos_Salida()
    Public Property InvokeListarProductoTipo As Listar_Tipo_Documentos_Salida

    Private lBeProductoEstado As New List(Of clsBeProducto_estado)

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

    Private Sub frmTipoDocumentoSalida_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Llenar_Propietarios()
            'IMS.Listar_Propietarios_By_IdBodega(cmbPropietario,
            '                                        AP.Bodega.IdBodega)

            Llenar_Estados()

            IMS.Listar_TipoIngresoOC(cmbTipoDocumentoIngreso, False)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnTrans_pe_tipo.MaxID()
                    User_agrTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False

                Case TipoTrans.Editar

                    lblCodigo.Text = pObjPT.IdTipoIngresoOC
                    txtNombre.Text = pObjPT.Nombre

                    chkActivo.Checked = pObjPT.Activo
                    txtObservacion.Text = pObjPT.Descripcion
                    chkPreparar.IsOn = pObjPT.Preparar
                    chkVerificar.IsOn = pObjPT.Verificar
                    chkReservaStock.IsOn = pObjPT.ReservaStock
                    chkImprimeBarrasPacking.IsOn = pObjPT.ImprimeBarrasPacking
                    chkImprimirBarrasPicking.IsOn = pObjPT.ImprimeBarrasPicking
                    chkControlPoliza.IsOn = pObjPT.Control_Poliza
                    chkGenerarPedidoIngresoBodegaDestino.IsOn = pObjPT.Generar_pedido_ingreso_bodega_destino
                    cmbTipoDocumentoIngreso.EditValue = pObjPT.IdTipoIngresoOC
                    chkRequiereDocumentoRef.IsOn = pObjPT.Requerir_Documento_Ref
                    chkTrasladarLotesDocIngreso.IsOn = pObjPT.Trasladar_Lotes_Doc_Ingreso
                    chkActivo.Checked = pObjPT.Activo
                    chkRequerirClienteEsBodegaWMS.IsOn = pObjPT.Requerir_Cliente_Es_Bodega_WMS
                    chkMarcar_Registros_Enviados_MI3.IsOn = pObjPT.Marcar_Registros_Enviados_MI3
                    chkcontrol_cliente_en_detalle.IsOn = pObjPT.Control_Cliente_En_Detalle
                    chkpermitir_despacho_parcial.IsOn = pObjPT.Permitir_Despacho_Parcial
                    chkPermitirDespachoMultiple.IsOn = pObjPT.Permitir_Despacho_Multiple
                    chkFotografiaVerificacion.IsOn = pObjPT.Fotografia_Verificacion
                    chkEsDevolucion.IsOn = pObjPT.Es_Devolucion

                    If pObjPT.IdPropietario > 0 Then
                        cmbPropietario.EditValue = pObjPT.IdPropietario
                    End If

                    If pObjPT.IdProductoEstado > 0 Then
                        cmbEstado.EditValue = pObjPT.IdProductoEstado
                    End If

                    chkEscanearMuellePicking.IsOn = pObjPT.Escanear_Muelle_Picking
                    chkMoverAMuelle.IsOn = pObjPT.Mover_Producto_Zona_Muelle

                    chkGenerarRecepcionAutoBodegaDestino.IsOn = pObjPT.Generar_Recepcion_Auto_Bodega_Destino
                    chkRecibirProductoAutoBodegaDestino.IsOn = pObjPT.Recibir_Producto_Auto_Bodega_Destino

                    chkEmpaqueTarima.IsOn = pObjPT.Empaque_Tarima

                    chkTransferirUbicacion.IsOn = pObjPT.Transferir_Ubicacion

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = OpcionesMenu.Modificar
                    mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    mnuAsignacion.Enabled = OpcionesMenu.Modificar

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Me.Focus()

        txtNombre.Focus()

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim objPe_Tipo As New clsBeTrans_pe_tipo
            objPe_Tipo.IdTipoIngresoOC = clsLnTrans_pe_tipo.MaxID()
            objPe_Tipo.Nombre = txtNombre.Text
            objPe_Tipo.Activo = chkActivo.Checked
            objPe_Tipo.Descripcion = txtObservacion.Text.Trim
            objPe_Tipo.Preparar = chkPreparar.IsOn
            objPe_Tipo.Verificar = chkVerificar.IsOn
            objPe_Tipo.ReservaStock = chkReservaStock.IsOn
            objPe_Tipo.ImprimeBarrasPacking = chkImprimeBarrasPacking.IsOn
            objPe_Tipo.ImprimeBarrasPicking = chkImprimirBarrasPicking.IsOn
            objPe_Tipo.Control_Poliza = chkControlPoliza.IsOn
            objPe_Tipo.Generar_pedido_ingreso_bodega_destino = chkGenerarPedidoIngresoBodegaDestino.IsOn
            objPe_Tipo.IdTipoIngresoOC = cmbTipoDocumentoIngreso.EditValue
            objPe_Tipo.Requerir_Documento_Ref = chkRequiereDocumentoRef.IsOn
            objPe_Tipo.Trasladar_Lotes_Doc_Ingreso = chkTrasladarLotesDocIngreso.IsOn
            objPe_Tipo.Requerir_Cliente_Es_Bodega_WMS = chkRequerirClienteEsBodegaWMS.IsOn
            objPe_Tipo.Marcar_Registros_Enviados_MI3 = chkMarcar_Registros_Enviados_MI3.IsOn
            objPe_Tipo.Control_Cliente_En_Detalle = chkcontrol_cliente_en_detalle.IsOn
            objPe_Tipo.Recibir_Producto_Auto_Bodega_Destino = True
            objPe_Tipo.Permitir_Despacho_Parcial = chkpermitir_despacho_parcial.IsOn
            objPe_Tipo.Permitir_Despacho_Multiple = chkPermitirDespachoMultiple.IsOn
            objPe_Tipo.Fotografia_Verificacion = chkFotografiaVerificacion.IsOn
            objPe_Tipo.Es_Devolucion = chkEsDevolucion.IsOn
            '#GT05042025: estado por defecto para el producto a despachar
            objPe_Tipo.IdProductoEstado = IIf(cmbEstado.EditValue > 0, cmbEstado.EditValue, 0)
            objPe_Tipo.IdPropietario = IIf(cmbPropietario.EditValue > 0, cmbPropietario.EditValue, 0)
            '#GT15042025:scan del muelle en proceso de picking
            objPe_Tipo.Mover_producto_zona_muelle = chkMoverAMuelle.IsOn
            objPe_Tipo.Escanear_Muelle_Picking = chkEscanearMuellePicking.IsOn
            objPe_Tipo.Generar_Recepcion_Auto_Bodega_Destino = chkGenerarRecepcionAutoBodegaDestino.IsOn
            objPe_Tipo.Recibir_Producto_Auto_Bodega_Destino = chkRecibirProductoAutoBodegaDestino.IsOn
            objPe_Tipo.Empaque_Tarima = chkEmpaqueTarima.IsOn
            objPe_Tipo.Transferir_Ubicacion = chkTransferirUbicacion.IsOn

            clsLnTrans_pe_tipo.Insertar(objPe_Tipo)

            Guardar = True


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjPT.Nombre = txtNombre.Text
                pObjPT.Activo = True
                pObjPT.Descripcion = txtObservacion.Text.Trim
                pObjPT.Preparar = chkPreparar.IsOn
                pObjPT.Verificar = chkVerificar.IsOn
                pObjPT.ReservaStock = chkReservaStock.IsOn
                pObjPT.ImprimeBarrasPacking = chkImprimeBarrasPacking.IsOn
                pObjPT.ImprimeBarrasPicking = chkImprimirBarrasPicking.IsOn
                pObjPT.Control_Poliza = chkControlPoliza.IsOn
                pObjPT.Generar_pedido_ingreso_bodega_destino = chkGenerarPedidoIngresoBodegaDestino.IsOn
                pObjPT.IdTipoIngresoOC = cmbTipoDocumentoIngreso.EditValue
                pObjPT.Requerir_Documento_Ref = chkRequiereDocumentoRef.IsOn
                pObjPT.Trasladar_Lotes_Doc_Ingreso = chkTrasladarLotesDocIngreso.IsOn
                pObjPT.Requerir_Cliente_Es_Bodega_WMS = chkRequerirClienteEsBodegaWMS.IsOn
                pObjPT.Marcar_Registros_Enviados_MI3 = chkMarcar_Registros_Enviados_MI3.IsOn
                pObjPT.Control_Cliente_En_Detalle = chkcontrol_cliente_en_detalle.IsOn
                pObjPT.Permitir_Despacho_Parcial = chkpermitir_despacho_parcial.IsOn
                pObjPT.Permitir_Despacho_Multiple = chkPermitirDespachoMultiple.IsOn
                pObjPT.Fotografia_Verificacion = chkFotografiaVerificacion.IsOn
                pObjPT.Es_Devolucion = chkEsDevolucion.IsOn
                pObjPT.IdProductoEstado = IIf(cmbEstado.EditValue > 0, cmbEstado.EditValue, 0)
                pObjPT.IdPropietario = IIf(cmbPropietario.EditValue > 0, cmbPropietario.EditValue, 0)
                pObjPT.Escanear_Muelle_Picking = chkEscanearMuellePicking.IsOn
                pObjPT.Mover_Producto_Zona_Muelle = chkMoverAMuelle.IsOn
                pObjPT.Generar_Recepcion_Auto_Bodega_Destino = chkGenerarRecepcionAutoBodegaDestino.IsOn
                pObjPT.Recibir_Producto_Auto_Bodega_Destino = chkRecibirProductoAutoBodegaDestino.IsOn
                pObjPT.Empaque_Tarima = chkEmpaqueTarima.IsOn
                pObjPT.Transferir_Ubicacion = chkTransferirUbicacion.IsOn

                clsLnTrans_pe_tipo.Actualizar(pObjPT)
                Actualizar = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf String.IsNullOrEmpty(txtObservacion.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese descripción del documento.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            mnuGuardar.Enabled = False

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar tipo de documento de salida?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then

                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If InvokeListarProductoTipo IsNot Nothing Then
                            InvokeListarProductoTipo.Invoke()
                        End If

                        Close()

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        Finally
            mnuGuardar.Enabled = True
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        mnuActualizar.Enabled = False
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarProductoTipo IsNot Nothing Then
                InvokeListarProductoTipo.Invoke()
            End If
            Close()
        End If
        mnuActualizar.Enabled = True

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        'Try
        '    mnuEliminar.Enabled = False

        '    If pObjPT.Activo = False Then
        '        XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    ElseIf clsLnTrans_pe_tipo.ExisteProductoLigado(pObjPT.IdTipoProducto) Then
        '        If MessageBox.Show("¿Desactivar el Tipo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '            clsLnTrans_pe_tipo.Eliminar(pObjPT)
        '            If InvokeListarProductoTipo IsNot Nothing Then
        '                InvokeListarProductoTipo.Invoke()
        '            End If
        '            Close()
        '            frmTipoDocumentoSalidaList.Dgrid.Refresh()
        '        End If
        '    Else
        '        If MessageBox.Show("¿Eliminar el Tipo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '            clsLnTrans_pe_tipo.Delete(pObjPT.IdTipoProducto)
        '            If InvokeListarProductoTipo IsNot Nothing Then
        '                InvokeListarProductoTipo.Invoke()
        '            End If
        '            Close()
        '            frmTipoDocumentoSalidaList.Dgrid.Refresh()
        '        End If
        '    End If

        '    mnuEliminar.Enabled = True

        'Catch ex As Exception
        '    mnuEliminar.Enabled = True
        '    If ex.HResult = -2146233088 Then TablasRelacionadas("Trans_pe_tipo", pObjPT.IdTipoProducto)
        'End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmTipoDocumentoSalida_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub


    Private Sub Llenar_Estados()
        Try

            Dim pIdPropietario As Integer = 0
            Dim fila As Object = cmbPropietario.GetSelectedDataRow
            If Not fila Is Nothing Then
                pIdPropietario = fila.Item("IdPropietario")
            End If

            lBeProductoEstado = clsLnProducto_estado.GetAllByPropietario(pIdPropietario)

            If lBeProductoEstado IsNot Nothing AndAlso lBeProductoEstado.Count > 0 Then
                Dim DT As New DataTable("Estado")
                DT.Columns.Add("IdEstado", GetType(Integer))
                DT.Columns.Add("IdPropietario", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))

                For Each BeProductoEstado As clsBeProducto_estado In lBeProductoEstado
                    DT.Rows.Add(BeProductoEstado.IdEstado,
                                        BeProductoEstado.IdPropietario,
                                        BeProductoEstado.Nombre)
                Next

                cmbEstado.Properties.ValueMember = "IdEstado"
                cmbEstado.Properties.DisplayMember = "Nombre"
                cmbEstado.Properties.DataSource = DT
                cmbEstado.Properties.PopulateColumns()
                cmbEstado.Properties.Columns(1).Visible = False
                cmbEstado.Properties.PopupWidth = 700
                cmbEstado.Properties.BestFit()
                cmbEstado.Properties.NullText = ""

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Llenar_Propietarios()
        Try
            Dim DT1 As New DataTable

            DT1 = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(AP.IdBodega)

            cmbPropietario.Properties.ValueMember = "IdPropietarioBodega"
            cmbPropietario.Properties.DisplayMember = "Nombre"
            cmbPropietario.Properties.DataSource = DT1
            cmbPropietario.Properties.PopulateColumns()
            cmbPropietario.Properties.Columns(0).Visible = False
            cmbPropietario.Properties.Columns(1).Visible = False
            cmbPropietario.Properties.PopupWidth = 700
            cmbPropietario.Properties.BestFit()
            cmbPropietario.Properties.NullText = ""
            'cmbPropietario.ItemIndex = 0

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged
        Try

            If cmbPropietario.EditValue > 0 Then
                Llenar_Estados()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmTipoDocumentoSalida_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub
End Class