Imports DevExpress.XtraEditors

Public Class frmTipoContenedor

    Private pListObjT As New List(Of clsTabla)
    Public pObjBEJ As New clsBeTipo_contenedor
    Public Delegate Sub ListarTipoContenedor()
    Public Property InvokeListarTipoContenedor As ListarTipoContenedor

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmTipoContenedor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("tipo_contenedor")

            Select Case Modo
                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnTipo_contenedor.MaxID
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    clsLnTipo_contenedor.Obtener(pObjBEJ)

                    lblCodigo.Text = pObjBEJ.IdTipoContenedor
                    txtNombre.Text = pObjBEJ.Nombre
                    txtAlto.Value = pObjBEJ.Alto
                    txtLargo.Value = pObjBEJ.Largo
                    txtAncho.Value = pObjBEJ.Ancho
                    txtPies.Value = pObjBEJ.Pies
                    txtToneladas.Value = pObjBEJ.Tonealadas

                    chkActivo.Checked = pObjBEJ.Activo

                    User_agrTextEdit.Text = pObjBEJ.User_agr
                    Fec_agrDateEdit.Text = pObjBEJ.Fec_agr
                    User_modTextEdit.Text = pObjBEJ.User_mod
                    Fec_modDateEdit.Text = pObjBEJ.Fec_mod

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

        Me.Focus()
        txtNombre.Focus()

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                DevExpress.XtraEditors.XtraMessageBox.Show("El Nombre debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            pObjBEJ = New clsBeTipo_contenedor()

            pObjBEJ.IdTipoContenedor = clsLnTipo_contenedor.MaxID()
            pObjBEJ.Nombre = txtNombre.Text.Trim()

            pObjBEJ.Alto = txtAlto.Value
            pObjBEJ.Largo = txtLargo.Value
            pObjBEJ.Ancho = txtAncho.Value
            pObjBEJ.Pies = txtPies.Value
            pObjBEJ.Tonealadas = txtToneladas.Value

            pObjBEJ.User_agr = AP.UsuarioAp.IdUsuario
            pObjBEJ.Fec_agr = Now
            pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
            pObjBEJ.Fec_mod = Now
            pObjBEJ.Activo = True

            Guardar = clsLnTipo_contenedor.Insertar(pObjBEJ) > 0

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

                pObjBEJ.Nombre = txtNombre.Text.Trim()

                pObjBEJ.Alto = txtAlto.Value
                pObjBEJ.Largo = txtLargo.Value
                pObjBEJ.Ancho = txtAncho.Value
                pObjBEJ.Pies = txtPies.Value
                pObjBEJ.Tonealadas = txtToneladas.Value

                pObjBEJ.Activo = chkActivo.Checked

                pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
                pObjBEJ.Fec_mod = Now

                Actualizar = clsLnTipo_contenedor.Actualizar(pObjBEJ) > 0
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
        If Datos_Correctos() Then
            If XtraMessageBox.Show("¿Guardar Tipo Contenedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If InvokeListarTipoContenedor IsNot Nothing Then
                        InvokeListarTipoContenedor.Invoke()
                    End If

                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarTipoContenedor IsNot Nothing Then
                InvokeListarTipoContenedor.Invoke()
            End If
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Desactivar el Tipo Contenedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False
                If clsLnTipo_contenedor.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarTipoContenedor IsNot Nothing Then
                        InvokeListarTipoContenedor.Invoke()
                    End If
                    Close()
                    frmTipoTarima_List.Dgrid.Refresh()
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

    Private Sub frmTipoContenedor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class