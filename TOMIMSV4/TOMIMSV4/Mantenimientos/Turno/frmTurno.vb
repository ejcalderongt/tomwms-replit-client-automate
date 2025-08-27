Imports DevExpress.XtraEditors

Public Class frmTurno

    Private pListObjT As New List(Of clsTabla)
    Public pObjBET As New clsBeTurno
    Public Delegate Sub Listar_Turno()
    Public Property Listar As Listar_Turno

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


    Private Sub frmTurno_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("turno")

            If AP.Listar_Bodegas_By_Usuario(cmbBodega) Then

            Else
                XtraMessageBox.Show("No hay Bodegas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnTurno.MaxID
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

                Case TipoTrans.Editar

                    clsLnTurno.Obtener(pObjBET)

                    lblCodigo.Text = pObjBET.IdTurno
                    cmbBodega.EditValue = pObjBET.IdBodega
                    cmbBodega.Enabled = False

                    txtNombre.Text = pObjBET.Nombre
                    chkActivo.Checked = pObjBET.Activo

                    User_agrTextEdit.Text = pObjBET.User_agr
                    Fec_agrDateEdit.Text = pObjBET.Fec_agr
                    User_modTextEdit.Text = pObjBET.User_mod
                    Fec_modDateEdit.Text = pObjBET.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        Focus()

        txtNombre.Focus()

    End Sub


    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try
            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim Obj As New clsBeTurno() With {.IdTurno = clsLnTurno.MaxID(),
                .IdBodega = cmbBodega.EditValue,
                .Nombre = txtNombre.Text.Trim(),
                .Activo = True,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now}

            Guardar = clsLnTurno.Insertar(Obj) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjBET.IdBodega = cmbBodega.EditValue
                pObjBET.Nombre = txtNombre.Text.Trim()
                pObjBET.User_mod = AP.UsuarioAp.IdUsuario
                pObjBET.Fec_mod = Now
                pObjBET.Activo = chkActivo.Checked

                Actualizar = clsLnTurno.Actualizar(pObjBET) > 0

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

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

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If Actualizar() Then
                XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If
                Close()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If MessageBox.Show("¿Desactivar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                pObjBET.Activo = False

                If clsLnTurno.Actualizar(pObjBET) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If
                    Close()
                End If

            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("turno", pObjBET.IdTurno)
        End Try

    End Sub

    Private Sub frmTurno_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class