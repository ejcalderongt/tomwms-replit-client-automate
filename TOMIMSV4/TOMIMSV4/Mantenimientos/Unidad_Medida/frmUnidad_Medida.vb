Imports DevExpress.XtraEditors

Public Class frmUnidad_Medida

    Private pListObjT As New List(Of clsTabla)
    Public pObjUM As New clsBeUnidad_medida
    Public Delegate Sub listarUM()
    Public Property InvokeListarUM As listarUM
    Public Property IdPropietario As Integer = 0

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Dim vUnidadMedidaBasica As String = ""

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmProducto_Tipo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("unidad_medida")
            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, AP.IdEmpresa)

            If IdPropietario <> 0 Then
                cmbPropietario.EditValue = IdPropietario
                cmbPropietario.Enabled = False
            Else
                cmbPropietario.Enabled = True
            End If

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblIdUnidadMedida.Text = clsLnUnidad_medida.MaxID()
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False
                    cmbPropietario.Enabled = True

                Case TipoTrans.Editar

                    lblIdUnidadMedida.Text = pObjUM.IdUnidadMedida
                    cmbPropietario.EditValue = pObjUM.IdPropietario
                    cmbPropietario.Enabled = False

                    txtCodigo.Text = pObjUM.Codigo.Trim()
                    txtNombre.Text = pObjUM.Nombre.Trim()
                    chkActivo.Checked = pObjUM.Activo

                    User_agrTextEdit.Text = pObjUM.User_agr
                    Fec_agrDateEdit.Text = pObjUM.Fec_agr
                    User_modTextEdit.Text = pObjUM.User_mod
                    Fec_modDateEdit.Text = pObjUM.Fec_mod
                    chkUMCobro.Checked = pObjUM.es_um_cobro
                    txtFactor.Value = pObjUM.factor

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                        mnuAsignacion.Enabled = OpcionesMenu.Modificar
                    End If

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Me.Focus()
        Me.txtNombre.Focus()

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            '#GT10042025: previamente se removieron caracteres especiales en vUnidadMedidaBasica

            Dim ObjN As New clsBeUnidad_medida() With {.IdUnidadMedida = clsLnUnidad_medida.MaxID(),
                .IdPropietario = cmbPropietario.EditValue,
                .Codigo = txtCodigo.Text.Trim(),
                .Nombre = vUnidadMedidaBasica,
                .Activo = True,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now}

            ObjN.factor = txtFactor.Value
            ObjN.es_um_cobro = chkUMCobro.Checked

            Guardar = clsLnUnidad_medida.Insertar(ObjN) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try
            '#GT10042025: previamente para vUnidadMedidaBasica se removieron caracteres no permitidos

            If Datos_Correctos() Then

                pObjUM.Codigo = txtCodigo.Text.Trim()
                pObjUM.Nombre = vUnidadMedidaBasica
                pObjUM.Activo = chkActivo.Checked
                pObjUM.User_mod = AP.UsuarioAp.IdUsuario
                pObjUM.Fec_mod = Now
                pObjUM.factor = txtFactor.Value
                pObjUM.es_um_cobro = chkUMCobro.Checked

                Actualizar = clsLnUnidad_medida.Actualizar(pObjUM) > 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_Correctos() As Boolean
        Datos_Correctos = False

        Try

            vUnidadMedidaBasica = clsPublic.Quitar_Caracteres_No_Permitidos(txtNombre.Text.Trim())

            If cmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPropietario.Focus()
            ElseIf String.IsNullOrEmpty(txtCodigo.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Código.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigo.Focus()
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf clsLnUnidad_medida.Existe_By_Nombre(vUnidadMedidaBasica, cmbPropietario.EditValue) Then
                XtraMessageBox.Show("El nombre ya existe.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            If MessageBox.Show("¿Guardar la Unidad de Medida?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If InvokeListarUM IsNot Nothing Then
                    InvokeListarUM.Invoke()
                End If
                Close()

            End If

        End If

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarUM IsNot Nothing Then
                InvokeListarUM.Invoke()
            End If
            Close()
        End If

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If pObjUM.Activo = False Then

                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            ElseIf clsLnUnidad_medida.ExisteProductoLigado(pObjUM.IdUnidadMedida) Then

                If MessageBox.Show("¿Desactivar la Unidad de Medida?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnUnidad_medida.Eliminar(pObjUM)
                    If InvokeListarUM IsNot Nothing Then
                        InvokeListarUM.Invoke()
                    End If
                    Close()
                    frmUnidad_MedidaList.Dgrid.Refresh()
                End If

            Else

                If MessageBox.Show("¿Eliminar la Unidad de Medida?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnUnidad_medida.Delete(pObjUM.IdUnidadMedida)
                    If InvokeListarUM IsNot Nothing Then
                        InvokeListarUM.Invoke()
                    End If
                    Close()
                    frmUnidad_MedidaList.Dgrid.Refresh()
                End If

            End If


        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("unidad_medida", pObjUM.IdUnidadMedida)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmUnidad_Medida_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub


End Class