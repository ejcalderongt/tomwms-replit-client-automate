Imports DevExpress.XtraEditors

Public Class frmOrdenCompraTI

    Public pBeOCTI As New clsBeTrans_oc_ti

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)

        InitializeComponent()
        Modo = pModo

    End Sub

    Private Sub frmOrdenCompraTI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = "-"
                    User_agrTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_modDateEdit.Text = Now
                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False

                Case TipoTrans.Editar

                    lblCodigo.Text = pBeOCTI.IdTipoIngresoOC
                    txtNombre.Text = pBeOCTI.Nombre
                    chkDevolucion.IsOn = pBeOCTI.Es_devolucion
                    chkControlPoliza.IsOn = pBeOCTI.Control_Poliza
                    chkRequerirDocumentoRef.IsOn = pBeOCTI.Requerir_Documento_Ref
                    chkEsPolizaConsolidada.IsOn = pBeOCTI.Es_Poliza_Consolidada
                    chkActivo.Checked = pBeOCTI.Activo
                    chkPreguntarEnBackOrder.IsOn = pBeOCTI.Preguntar_En_BackOrder

                    User_agrTextEdit.Text = pBeOCTI.User_agr
                    Fec_agrDateEdit.Text = pBeOCTI.Fec_agr
                    User_modTextEdit.Text = pBeOCTI.User_mod
                    Fec_modDateEdit.Text = pBeOCTI.Fec_mod

                    chkBloquearLotes.IsOn = pBeOCTI.Bloquear_Lotes
                    chkPermitirExcedenteLotes.IsOn = pBeOCTI.Permitir_Excedente_Lotes

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = True
                    mnuEliminar.Enabled = True
                    mnuAsignacion.Enabled = True

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim BeTransOCTI As New clsBeTrans_oc_ti
            BeTransOCTI.IdTipoIngresoOC = clsLnTrans_oc_ti.MaxID() + 1
            BeTransOCTI.Nombre = txtNombre.Text.Trim()
            BeTransOCTI.Es_devolucion = chkDevolucion.IsOn
            BeTransOCTI.Control_Poliza = chkControlPoliza.IsOn
            BeTransOCTI.Requerir_Documento_Ref = chkRequerirDocumentoRef.IsOn
            BeTransOCTI.Es_Poliza_Consolidada = chkEsPolizaConsolidada.IsOn
            BeTransOCTI.Activo = True
            BeTransOCTI.User_agr = AP.UsuarioAp.IdUsuario
            BeTransOCTI.Fec_agr = Now
            BeTransOCTI.User_mod = AP.UsuarioAp.IdUsuario
            BeTransOCTI.Fec_mod = Now
            BeTransOCTI.Genera_Tarea_Ingreso = chkGeneraTareaIngreso.IsOn
            BeTransOCTI.Preguntar_En_BackOrder = chkPreguntarEnBackOrder.IsOn
            BeTransOCTI.Bloquear_Lotes = chkBloquearLotes.IsOn
            BeTransOCTI.Permitir_Excedente_Lotes = chkPermitirExcedenteLotes.IsOn

            clsLnTrans_oc_ti.Insertar(BeTransOCTI)

            Guardar = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pBeOCTI.Nombre = txtNombre.Text.Trim()
                pBeOCTI.Es_devolucion = chkDevolucion.IsOn
                pBeOCTI.Activo = chkActivo.Checked
                pBeOCTI.Control_Poliza = chkControlPoliza.IsOn
                pBeOCTI.Requerir_Documento_Ref = chkRequerirDocumentoRef.IsOn
                pBeOCTI.Es_Poliza_Consolidada = chkEsPolizaConsolidada.IsOn
                pBeOCTI.Genera_Tarea_Ingreso = chkGeneraTareaIngreso.IsOn
                pBeOCTI.User_mod = AP.UsuarioAp.IdUsuario
                pBeOCTI.Fec_mod = Now
                pBeOCTI.Preguntar_En_BackOrder = chkPreguntarEnBackOrder.IsOn
                pBeOCTI.Bloquear_Lotes = chkBloquearLotes.IsOn
                pBeOCTI.Permitir_Excedente_Lotes = chkPermitirExcedenteLotes.IsOn

                clsLnTrans_oc_ti.Actualizar(pBeOCTI)

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

        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar el Tipo Ingreso OC?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                End If

            End If

        End If

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If pBeOCTI.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                If MessageBox.Show("¿Desactivar el Tipo Ingreso OC?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If clsLnTrans_oc_ti.Eliminar(pBeOCTI) > 0 Then
                        Close()
                        frmOrdenCompraTI_List.Dgrid.Refresh()
                    End If

                End If

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class