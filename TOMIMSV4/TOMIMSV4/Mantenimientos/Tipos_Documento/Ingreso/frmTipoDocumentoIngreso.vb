Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmTipoDocumentoIngreso

    Private lBeProductoEstado As New List(Of clsBeProducto_estado)

    Public pObjTransOCTI As New clsBeTrans_oc_ti

    Public Delegate Sub listarProductoTipo()
    Public Property InvokeListarProductoTipo As listarProductoTipo

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

    Private Sub frmTipoDocumentoIngreso_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Llenar_Propietarios()
            Llenar_Estados()

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnTrans_oc_ti.MaxID()
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

                    lblCodigo.Text = pObjTransOCTI.IdTipoIngresoOC
                    txtNombre.Text = pObjTransOCTI.Nombre
                    chkEsDevolucion.IsOn = pObjTransOCTI.Es_devolucion
                    chkActivo.Checked = pObjTransOCTI.Activo
                    chkControlPoliza.IsOn = pObjTransOCTI.Control_Poliza
                    chkRequerirDocumentoRef.IsOn = pObjTransOCTI.Requerir_Documento_Ref
                    chkPolizaEsConsolidada.IsOn = pObjTransOCTI.Es_Poliza_Consolidada
                    chkGeneraTareaIngreso.IsOn = pObjTransOCTI.Genera_Tarea_Ingreso
                    chkRequerirProveedorEsBodegaWMS.IsOn = pObjTransOCTI.Requerir_Proveedor_Es_Bodega_WMS
                    chkExigirCampoReferencia.IsOn = pObjTransOCTI.Exigir_Campo_Referencia
                    chkMarcarRegistrosEnviadosMI3.IsOn = pObjTransOCTI.Marcar_Registros_Enviados_MI3
                    chkPreguntarEnBackOrder.IsOn = pObjTransOCTI.Preguntar_En_BackOrder
                    chkPermiteVencidoIngreso.IsOn = pObjTransOCTI.Permitir_Vencido_Ingreso
                    User_agrTextEdit.Text = pObjTransOCTI.User_agr
                    Fec_agrDateEdit.Text = pObjTransOCTI.Fec_agr
                    User_modTextEdit.Text = pObjTransOCTI.User_mod
                    Fec_modDateEdit.Text = pObjTransOCTI.Fec_mod

                    If pObjTransOCTI.IdPropietario > 0 Then
                        cmbPropietario.EditValue = pObjTransOCTI.IdPropietario
                    Else
                        cmbPropietario.EditValue = -1
                    End If

                    If pObjTransOCTI.IdProductoEstado > 0 Then
                        cmbEstado.EditValue = pObjTransOCTI.IdProductoEstado
                    Else
                        cmbEstado.EditValue = -1
                    End If

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Modificar
                        mnuAsignacion.Enabled = OpcionesMenu.Eliminar
                    End If

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

            Dim pObjTransOCTI As New clsBeTrans_oc_ti
            pObjTransOCTI.IdTipoIngresoOC = clsLnProducto_tipo.MAXIdTipoProducto()
            pObjTransOCTI.Nombre = txtNombre.Text
            pObjTransOCTI.Es_devolucion = chkEsDevolucion.IsOn
            pObjTransOCTI.Activo = pObjTransOCTI.Activo
            pObjTransOCTI.Control_Poliza = chkControlPoliza.IsOn
            pObjTransOCTI.Requerir_Documento_Ref = chkRequerirDocumentoRef.IsOn
            pObjTransOCTI.Es_Poliza_Consolidada = pObjTransOCTI.Es_Poliza_Consolidada
            pObjTransOCTI.Genera_Tarea_Ingreso = pObjTransOCTI.Genera_Tarea_Ingreso
            pObjTransOCTI.Requerir_Proveedor_Es_Bodega_WMS = chkRequerirProveedorEsBodegaWMS.IsOn
            pObjTransOCTI.Activo = True
            pObjTransOCTI.User_agr = AP.UsuarioAp.IdUsuario
            pObjTransOCTI.Fec_agr = Now
            pObjTransOCTI.User_mod = AP.UsuarioAp.IdUsuario
            pObjTransOCTI.Fec_mod = Now
            pObjTransOCTI.Exigir_Campo_Referencia = chkExigirCampoReferencia.IsOn
            pObjTransOCTI.Marcar_Registros_Enviados_MI3 = chkMarcarRegistrosEnviadosMI3.IsOn
            pObjTransOCTI.Preguntar_En_BackOrder = chkPreguntarEnBackOrder.IsOn
            pObjTransOCTI.Permitir_Vencido_Ingreso = chkPermiteVencidoIngreso.IsOn
            clsLnTrans_oc_ti.Insertar(pObjTransOCTI)

            Guardar = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjTransOCTI.Nombre = txtNombre.Text
                pObjTransOCTI.Es_devolucion = chkEsDevolucion.IsOn
                pObjTransOCTI.Activo = pObjTransOCTI.Activo
                pObjTransOCTI.Control_Poliza = chkControlPoliza.IsOn
                pObjTransOCTI.Requerir_Documento_Ref = chkRequerirDocumentoRef.IsOn
                pObjTransOCTI.Es_Poliza_Consolidada = pObjTransOCTI.Es_Poliza_Consolidada
                pObjTransOCTI.Genera_Tarea_Ingreso = pObjTransOCTI.Genera_Tarea_Ingreso
                pObjTransOCTI.Requerir_Proveedor_Es_Bodega_WMS = chkRequerirProveedorEsBodegaWMS.IsOn
                pObjTransOCTI.Activo = chkActivo.Checked
                pObjTransOCTI.User_agr = AP.UsuarioAp.IdUsuario
                pObjTransOCTI.Fec_agr = Now
                pObjTransOCTI.User_mod = AP.UsuarioAp.IdUsuario
                pObjTransOCTI.Fec_mod = Now
                pObjTransOCTI.Requerir_Ubic_Rec_Ingreso = chkRequerirUbicRecIngreso.IsOn
                pObjTransOCTI.Exigir_Campo_Referencia = chkExigirCampoReferencia.IsOn
                pObjTransOCTI.Marcar_Registros_Enviados_MI3 = chkMarcarRegistrosEnviadosMI3.IsOn
                pObjTransOCTI.Preguntar_En_BackOrder = chkPreguntarEnBackOrder.IsOn
                pObjTransOCTI.Permitir_Vencido_Ingreso = chkPermiteVencidoIngreso.IsOn
                pObjTransOCTI.IdPropietario = cmbPropietario.EditValue
                pObjTransOCTI.IdProductoEstado = cmbEstado.EditValue
                clsLnTrans_oc_ti.Actualizar(pObjTransOCTI)
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

                If MessageBox.Show("¿Guardar el tipo de documento de ingreso?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then
                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If InvokeListarProductoTipo IsNot Nothing Then
                            InvokeListarProductoTipo.Invoke()
                        End If
                        Close()
                    End If

                End If

            End If

            mnuGuardar.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
             Text,
             MessageBoxButtons.OK,
             MessageBoxIcon.Error)
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

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmTipoDocumentoIngreso_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged
        Try

            If cmbPropietario.EditValue > 0 Then

                Llenar_Estados()

            End If

        Catch ex As Exception

        End Try
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

        End Try

    End Sub

End Class