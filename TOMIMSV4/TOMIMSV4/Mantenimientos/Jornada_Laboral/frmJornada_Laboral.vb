Imports DevExpress.XtraEditors

Public Class frmJornada_Laboral

    Private pListObjT As New List(Of clsTabla)
    Public pObjBEJ As New clsBeJornada_laboral
    Public Delegate Sub Listar_JL()
    Public Property Listar As Listar_JL

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

    Private Sub frmJornadaLaboral_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("jornada_laboral")

            If AP.Listar_Bodegas_By_Usuario(cmbBodega) Then
            Else
                Throw New Exception("No hay Bodegas definidas para la aplicación")
            End If

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnJornada_laboral.MaxID()
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    cmbBodega.Enabled = True
                    dtmFechaInicio.DateTime = Today
                    dtmFechaFin.DateTime = Today

                Case TipoTrans.Editar

                    clsLnJornada_laboral.Obtener(pObjBEJ)

                    lblCodigo.Text = pObjBEJ.IdJornada
                    cmbBodega.EditValue = pObjBEJ.IdBodega
                    cmbBodega.Enabled = False

                    txtNombre.Text = pObjBEJ.Nombre_jornada
                    txtHoras.Text = pObjBEJ.Horas_trabajadas
                    dtmFechaInicio.EditValue = pObjBEJ.Fecha_inicio
                    dtmFechaFin.EditValue = pObjBEJ.Fecha_fin

                    User_agrTextEdit.Text = pObjBEJ.User_agr
                    Fec_agrDateEdit.Text = pObjBEJ.Fec_mod
                    User_modTextEdit.Text = pObjBEJ.User_mod
                    Fec_modDateEdit.Text = pObjBEJ.Fec_mod

                    chkActivo.Checked = pObjBEJ.Activo

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        txtNombre.Focus()

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE_JORNADA").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE_JORNADA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim Obj As New clsBeJornada_laboral() With {.IdBodega = cmbBodega.EditValue,
                .IdJornada = clsLnJornada_laboral.MaxID(),
                .Nombre_jornada = txtNombre.Text.Trim(),
                .Horas_trabajadas = txtHoras.Text,
                .Fecha_inicio = dtmFechaInicio.EditValue,
                .Fecha_fin = dtmFechaFin.EditValue,
                .Activo = True,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now}

            Guardar = clsLnJornada_laboral.Insertar(Obj) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjBEJ.IdBodega = cmbBodega.EditValue
                pObjBEJ.Nombre_jornada = txtNombre.Text.Trim()
                pObjBEJ.Horas_trabajadas = txtHoras.Text
                pObjBEJ.Fecha_inicio = dtmFechaInicio.EditValue
                pObjBEJ.Fecha_fin = dtmFechaFin.EditValue
                pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
                pObjBEJ.Fec_mod = Now
                pObjBEJ.Activo = chkActivo.Checked

                Actualizar = clsLnJornada_laboral.Actualizar(pObjBEJ) > 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try
            mnuGuardar.Enabled = False

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then
                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Listar IsNot Nothing Then
                            Listar.Invoke()
                        End If
                        Close()
                    End If

                End If

            End If
            mnuGuardar.Enabled = True

        Catch ex As Exception
            mnuGuardar.Enabled = True
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try
            mnuActualizar.Enabled = False

            If Actualizar() Then
                XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If
                Close()
            End If
            mnuActualizar.Enabled = True

        Catch ex As Exception
            mnuActualizar.Enabled = True
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try
            mnuEliminar.Enabled = False

            If MessageBox.Show("¿Desactivar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                pObjBEJ.Activo = False
                pObjBEJ.Fecha_baja = Now

                If clsLnJornada_laboral.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If
                    Close()
                End If

            End If
            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtHoras_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHoras.KeyPress
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
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub frmJornada_Laboral_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class