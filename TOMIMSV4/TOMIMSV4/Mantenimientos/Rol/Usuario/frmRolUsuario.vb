Imports DevExpress.XtraEditors

Public Class frmRolUsuario

    Public BeRol As New clsBeRol
    Public Delegate Sub cargarListadeRoles()
    Public Property InvokeListarRolesUsuario As cargarListadeRoles

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

    Private Sub frmRol_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            For i As Integer = 0 To chkListaOpciones.Items.Count - 1
                chkListaOpciones.SetItemChecked(i, False)
            Next

            Init_DT_EstadosUsuario()

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, AP.IdBodega)

            clsLnProducto_estado.Listar_By_IdPropietario_And_IdBodega(cmbPropietarioBodega.EditValue, AP.IdBodega)

            Select Case Modo

                Case TipoTrans.Nuevo

                    txtIdRol.Text = clsLnRol.MaxID()
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                    clsLnRol.CargaMenuRol(dgMenuRol, Integer.Parse(txtIdRol.Text.ToString))
                    clsLnRol.CargaMenuSistema(dgMenuSistema)

                    txtNombreRol.Text = ""
                    chkMenuSistema.CheckState = CheckState.Checked
                    txtNombreRol.Focus()

                Case TipoTrans.Editar

                    clsLnRol.Obtener(BeRol)

                    txtNombreRol.Text = BeRol.Nombre
                    txtIdRol.Text = BeRol.IdRol.ToString

                    clsLnRol.CargaMenuRol(dgMenuRol, Integer.Parse(txtIdRol.Text.ToString))
                    clsLnRol.CargaMenuSistema(dgMenuSistema)

                    chkActivo.Checked = BeRol.Activo
                    chkRegistraClave.Checked = BeRol.Registrar_clave_autorizacion

                    User_agrTextEdit.Text = BeRol.User_agr.ToString
                    Fec_agrDateEdit.Text = BeRol.Fec_agr.ToShortDateString
                    User_modTextEdit.Text = BeRol.User_mod.ToString
                    Fec_modDateEdit.Text = BeRol.Fec_mod.ToShortDateString

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    Cargar_Estados_Rol()

            End Select

            txtNombreRol.Focus()

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Try

            BeRol.IdRol = clsLnRol.MaxID()
            BeRol.IdEmpresa = AP.IdEmpresa
            BeRol.Nombre = txtNombreRol.Text
            BeRol.Activo = chkActivo.Checked
            BeRol.Registrar_clave_autorizacion = chkRegistraClave.Checked
            BeRol.User_agr = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
            BeRol.Fec_agr = Now
            BeRol.User_mod = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
            BeRol.Fec_mod = Now

            Return clsLnRol.Insertar(BeRol) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                BeRol.Nombre = txtNombreRol.Text.Trim()
                BeRol.Activo = chkActivo.Checked
                BeRol.User_mod = AP.UsuarioAp.IdUsuario
                BeRol.Registrar_clave_autorizacion = chkRegistraClave.Checked
                BeRol.Fec_mod = Now

                Actualizar = clsLnRol.Actualizar(BeRol) > 0

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

            If XtraMessageBox.Show("¿Guardar Rol?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarRolesUsuario.Invoke
                    Close()
                End If

            End If

        End If

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarRolesUsuario.Invoke
            Close()
        End If

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombreRol.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombreRol.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        If Global.Microsoft.VisualBasic.Interaction.MsgBox("¿Desactivar el Rol?", Global.Microsoft.VisualBasic.MsgBoxStyle.YesNo, Me.Text) = Global.Microsoft.VisualBasic.MsgBoxResult.Yes Then

            If clsLnRol.Eliminar(BeRol) > 0 Then
                XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                InvokeListarRolesUsuario.Invoke
                Close()
            End If

        End If

    End Sub

    Private Sub cmdAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click

        Try

            If Datos_Correctos() Then

                If (clsLnRol.Existe_Rol(txtIdRol.Text)) Then
                    Registrar_Permisos()
                Else

                    If MessageBox.Show("¿Guardar Rol Operador?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        If Guardar() Then
                            Registrar_Permisos()
                            Close()
                        Else
                            XtraMessageBox.Show("Error al guardar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdQuitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuitar.Click

        Dim I As Integer, vMenuID As String
        Dim ObjetomenuRolSistema As clsBeMenu_rol

        Try

            prg.Visible = True
            prg.Maximum = dgMenuRol.Rows.Count
            I = 0

            For Each r In dgMenuRol.Rows

                If (r.Cells(0).Value) Then

                    vMenuID = r.Cells(2).Value

                    ObjetomenuRolSistema = New clsBeMenu_rol()
                    ObjetomenuRolSistema.IdMenuRol = r.Cells(3).Value
                    ObjetomenuRolSistema.IdMenu = vMenuID
                    ObjetomenuRolSistema.IdRol = Integer.Parse(txtIdRol.Text)
                    ObjetomenuRolSistema.User_agr = User_agrTextEdit.Text.Trim
                    ObjetomenuRolSistema.Fec_agr = Fec_agrDateEdit.Text.Trim
                    ObjetomenuRolSistema.User_mod = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    ObjetomenuRolSistema.Fec_mod = Now
                    ObjetomenuRolSistema.Visible = False
                    ObjetomenuRolSistema.Activo = True
                    clsLnMenu_rol.Actualizar(ObjetomenuRolSistema)

                    Application.DoEvents()

                    prg.Value = I : I += 1

                End If

            Next

            clsLnRol.CargaMenuRol(dgMenuRol, txtIdRol.Text.ToString)

            chkMenuRol.Checked = False
            chkMenuSistema.Checked = False

            txtrol.Text = ""

            XtraMessageBox.Show("Se han desactivado los Permisos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            prg.Visible = False
        End Try

    End Sub

    Private Sub Registrar_Permisos()

        Dim I As Integer, vMenuID As String

        Dim ObjetomenuRolSistema As clsBeMenu_rol

        Try

            prg.Visible = True

            prg.Maximum = dgMenuSistema.Rows.Count
            I = 0

            Dim vMaxId As Integer = clsLnMenu_rol.MaxID()

            For Each r In dgMenuSistema.Rows

                If (r.Cells(0).Value) Then

                    vMenuID = r.Cells(2).Value

                    If vMaxId = "1055" Then
                        MsgBox("Espera")
                    End If

                    ObjetomenuRolSistema = New clsBeMenu_rol()
                    ObjetomenuRolSistema.IdMenuRol = vMaxId
                    ObjetomenuRolSistema.IdMenu = vMenuID
                    ObjetomenuRolSistema.IdRol = Integer.Parse(txtIdRol.Text)
                    ObjetomenuRolSistema.User_agr = User_agrTextEdit.Text
                    ObjetomenuRolSistema.Fec_agr = Fec_agrDateEdit.Text
                    ObjetomenuRolSistema.User_mod = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    ObjetomenuRolSistema.Fec_mod = Fec_modDateEdit.Text
                    ObjetomenuRolSistema.Visible = True
                    ObjetomenuRolSistema.Activo = True

                    If (clsLnRol.ExistePermiso(vMenuID, txtIdRol.Text)) Then
                        clsLnMenu_rol.Actualizar(ObjetomenuRolSistema)
                    Else
                        clsLnMenu_rol.Insertar(ObjetomenuRolSistema)
                        vMaxId += 1
                    End If

                    I += 1 : prg.Value = I

                End If

                Application.DoEvents()

            Next

            clsLnRol.CargaMenuRol(dgMenuRol, Integer.Parse(txtIdRol.Text.ToString))
            clsLnRol.CargaMenuSistema(dgMenuSistema)

            chkMenuRol.Checked = False
            chkMenuSistema.Checked = False

            txtmenu.Text = ""

            XtraMessageBox.Show("Se han activado las Permisos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            prg.Visible = False
        End Try
    End Sub

    Private Sub txtmenu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmenu.KeyPress

        Try

            If e.KeyChar = Convert.ToChar(8) AndAlso txtmenu.Text.Length = 0 Then
                txtmenu.Text = String.Empty
                clsLnRol.CargaMenuSistema(dgMenuSistema)
            Else
                txtmenu_LostFocus(Nothing, Nothing)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtmenu_LostFocus(sender As Object, e As EventArgs) Handles txtmenu.LostFocus

        Try

            If String.IsNullOrEmpty(txtmenu.Text.Trim()) = False Then

                clsLnRol.CargaMenuSistema(dgMenuSistema, txtmenu.Text.Trim)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtmeu_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtmenu.PreviewKeyDown

        Try
            If e.KeyData = Keys.Tab Then
                txtmenu_LostFocus(Nothing, Nothing)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtrol_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtrol.PreviewKeyDown

        Try
            If e.KeyData = Keys.Tab Then
                txtrol_LostFocus(Nothing, Nothing)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtrol_LostFocus(sender As Object, e As EventArgs) Handles txtrol.LostFocus


        Try

            If String.IsNullOrEmpty(txtrol.Text.Trim()) = False Then

                clsLnRol.CargaMenuRol(dgMenuRol, Integer.Parse(txtIdRol.Text.ToString), txtrol.Text.Trim)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtrol_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtrol.KeyPress

        Try

            If e.KeyChar = Convert.ToChar(8) AndAlso txtrol.Text.Length = 1 Then
                txtrol.Text = String.Empty
                clsLnRol.CargaMenuRol(dgMenuRol, Integer.Parse(txtIdRol.Text.ToString))
            Else
                txtrol_LostFocus(Nothing, Nothing)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub dgMenuSistema_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgMenuSistema.CurrentCellDirtyStateChanged
        Dim vMenuSistema As New clsBeMenu_sistema

        Try

            If dgMenuSistema.Rows.Count > 0 Then

                dgMenuSistema.EndEdit()

                Dim i As Integer = dgMenuSistema.CurrentCell.RowIndex
                Dim Sel As Boolean = IIf(IsDBNull(dgMenuSistema.Rows(i).Cells(3).Value()), False, dgMenuSistema.Rows(i).Cells(3).Value())

                vMenuSistema.IdMenu = dgMenuSistema.Rows(i).Cells(2).Value()

                clsLnMenu_sistema.GetSingle(vMenuSistema)

                vMenuSistema.Solicitar_clave_autorizacion = Sel

                clsLnMenu_sistema.Actualizar(vMenuSistema)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub chkMenuSistema_CheckedChanged_1(sender As Object, e As EventArgs) Handles chkMenuSistema.CheckedChanged

        Try

            For Each m In dgMenuSistema.Rows
                m.Cells(0).Value = (chkMenuSistema.Checked = True)
            Next

            dgMenuSistema.Refresh()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub chkMenuRol_CheckedChanged(sender As Object, e As EventArgs) Handles chkMenuRol.CheckedChanged

        Try

            For Each m In dgMenuRol.Rows
                m.Cells(0).Value = (chkMenuRol.Checked = True)
            Next

            dgMenuRol.Refresh()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmRolUsuario_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub cmdAgregarOpciones_Click(sender As Object, e As EventArgs) Handles cmdAgregarOpciones.Click

        Dim I As Integer, vMenuID As String
        Dim ObjetomenuRolSistema As clsBeMenu_rol

        Try

            prg.Visible = True
            prg.Maximum = dgMenuRol.Rows.Count
            I = 0

            For Each r In dgMenuRol.Rows

                If (r.Cells(0).Value) Then

                    vMenuID = r.Cells(2).Value

                    ObjetomenuRolSistema = New clsBeMenu_rol()
                    ObjetomenuRolSistema.IdMenu = vMenuID
                    ObjetomenuRolSistema.IdRol = Integer.Parse(txtIdRol.Text)
                    ObjetomenuRolSistema.User_mod = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    ObjetomenuRolSistema.Fec_mod = Now

                    ObjetomenuRolSistema.Leer = False
                    ObjetomenuRolSistema.Modificar = False
                    ObjetomenuRolSistema.Eliminar = False

                    For Each item In chkListaOpciones.Items
                        ' Obtener el nombre del ítem
                        Dim itemName As String = item.ToString()

                        ' Verificar si el ítem está seleccionado (marcado)
                        Dim isChecked As Boolean = chkListaOpciones.GetItemChecked(chkListaOpciones.Items.IndexOf(item))

                        If isChecked Then
                            Select Case itemName
                                Case "Leer"
                                    ObjetomenuRolSistema.Leer = True
                                Case "Modificar"
                                    ObjetomenuRolSistema.Modificar = True
                                Case "Eliminar"
                                    ObjetomenuRolSistema.Eliminar = True
                            End Select
                        End If

                    Next

                    clsLnMenu_rol.Actualizar_Opciones(ObjetomenuRolSistema)

                    Application.DoEvents()

                    prg.Value = I : I += 1

                End If

            Next

            For j As Integer = 0 To chkListaOpciones.Items.Count - 1
                chkListaOpciones.SetItemChecked(j, False)
            Next

            chkMenuRol.Checked = False
            chkMenuSistema.Checked = False

            txtrol.Text = ""

            XtraMessageBox.Show("Se han aplicado las opciones de lectura y escritura", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            prg.Visible = False
        End Try

    End Sub

    Private Sub dgMenuRol_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgMenuRol.CellDoubleClick
        Try

            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then

                Dim selectedCell As DataGridViewRow = dgMenuRol.Rows(e.RowIndex)

                Dim vMenuID As String = selectedCell.Cells(2).Value.ToString
                Dim vIdRol = Integer.Parse(txtIdRol.Text)

                clsLnRol.CargaMenuRol_Opciones(chkListaOpciones, vIdRol, vMenuID)

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub cmbPropietarioBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietarioBodega.EditValueChanged

        Dim DT As New DataTable

        Try

            Dim vIdPropietario As Integer = clsLnPropietarios.Get_IdPropietario(AP.IdBodega, cmbPropietarioBodega.EditValue)

            DT = clsLnProducto_estado.Listar_By_IdPropietario_And_IdBodega(vIdPropietario, AP.IdBodega)

            If DT.Rows.Count > 0 Then
                cmbEstadoOrigen.Properties.DisplayMember = "nombre"
                cmbEstadoOrigen.Properties.ValueMember = "IdEstado"
                cmbEstadoOrigen.Properties.DataSource = DT
                cmbEstadoOrigen.Properties.PopupWidth = 1400
                cmbEstadoOrigen.Properties.BestFit()
                cmbEstadoOrigen.Properties.NullText = ""
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub cmdGuardarEstado_Click(sender As Object, e As EventArgs) Handles cmdGuardarEstado.Click


        Try

            Dim vIndice As Integer = 0
            Dim vIdUsuarioEstado As Integer = 0
            Dim vIdPropietario As Integer = clsLnPropietarios.Get_IdPropietario(AP.IdBodega, cmbPropietarioBodega.EditValue)

            If (Val(cmdGuardarEstado.Tag) = 0) Then 'Es un nuevo registro
                vIdUsuarioEstado = Val(lblCodigoEstado.Text)
            Else
                vIdUsuarioEstado = Val(cmdGuardarEstado.Tag)
            End If

            vIndice = lEstadosUsuario.FindIndex(Function(x) x.IdRolUsuarioEstado = vIdUsuarioEstado)

            If vIndice = -1 Then '#Es nuevo.                

                '#EJC20241022: (Ya se que es confuso)
                ''pero: en el combo cmbPropietarioBodega se listan únicamente los propietarios de la bodega en sersión, por eso se llama propietariobodega,
                ''pero dentro se listan los propietarios
                'En consecuencia el value, tiene el IdPropietario y no el IdPropietarioBodega, aclaro.

                Dim BeUsuarioEstado As New clsBeRol_usuario_estado()
                BeUsuarioEstado.IdRolUsuarioEstado = clsLnRol_usuario_estado.MaxID() + 1
                BeUsuarioEstado.IdRolUsuario = BeRol.IdRol
                BeUsuarioEstado.IdPropietario = vIdPropietario 'cmbPropietarioBodega.EditValue
                BeUsuarioEstado.IdEstadoOrigen = cmbEstadoOrigen.EditValue
                BeUsuarioEstado.IdEstadoDestino = cmbEstadoDestino.EditValue
                BeUsuarioEstado.Permitir = chkPermitirFlujoEstado.IsOn
                BeUsuarioEstado.Activo = True
                BeUsuarioEstado.User_agr = AP.UsuarioAp.IdUsuario
                BeUsuarioEstado.Fec_agr = Now
                BeUsuarioEstado.User_mod = AP.UsuarioAp.IdUsuario
                BeUsuarioEstado.Fec_mod = Now

                clsLnRol_usuario_estado.Insertar(BeUsuarioEstado)

                XtraMessageBox.Show("Registro agregado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else

                Dim BeUsuarioEstado As New clsBeRol_usuario_estado()
                BeUsuarioEstado = lEstadosUsuario(vIndice)
                BeUsuarioEstado.IdPropietario = vIdPropietario 'cmbPropietarioBodega.EditValue
                BeUsuarioEstado.IdEstadoOrigen = cmbEstadoOrigen.EditValue
                BeUsuarioEstado.IdEstadoDestino = cmbEstadoDestino.EditValue
                BeUsuarioEstado.Permitir = chkPermitirFlujoEstado.IsOn
                BeUsuarioEstado.Activo = True
                BeUsuarioEstado.User_mod = AP.UsuarioAp.IdUsuario
                BeUsuarioEstado.Fec_mod = Now
                clsLnRol_usuario_estado.Actualizar(BeUsuarioEstado)

                XtraMessageBox.Show("Registro actualizado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

            Cargar_Estados_Rol()

            Limpiar_Campos_Para_Nuevo_Estado()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Cargar_Estados_Rol()

        Try

            lEstadosUsuario = clsLnRol_usuario_estado.Get_All_By_IdRol(BeRol.IdRol)

            dGridEstados.DataSource = Nothing

            If lEstadosUsuario.Count > 0 Then

                Dim vNomPresentacionContenidaEnPallet As String = ""
                Dim vNomBodega As String = ""

                DTEstadosUsuario.Rows.Clear()

                For Each BeUsuarioEstado As clsBeRol_usuario_estado In lEstadosUsuario.FindAll(Function(b) b.Activo = chkEstadosActivos.Checked)

                    DTEstadosUsuario.Rows.Add(BeUsuarioEstado.IdRolUsuarioEstado,
                                              BeUsuarioEstado.NombrePropietario,
                                              BeUsuarioEstado.NombreEstadoOrigen,
                                              BeUsuarioEstado.NombreEstadoDestino,
                                              BeUsuarioEstado.Permitir,
                                              BeUsuarioEstado.Activo)
                Next

                dGridEstados.DataSource = DTEstadosUsuario

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

    Private Sub Limpiar_Campos_Para_Nuevo_Estado()

        cmbPropietarioBodega.EditValue = Nothing
        cmbEstadoOrigen.EditValue = Nothing
        cmbEstadoDestino.EditValue = Nothing
        cmbPropietarioBodega.Focus()
        lblCodigoEstado.Text = clsLnRol_usuario_estado.MaxID() + 1
        cmdGuardarEstado.Tag = 0

    End Sub

    Private Sub cmdNuevoEstado_Click(sender As Object, e As EventArgs) Handles cmdNuevoEstado.Click

        Try

            Limpiar_Campos_Para_Nuevo_Estado()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub dGridEstados_Click(sender As Object, e As EventArgs) Handles grdEstados.DoubleClick


        Try

            If grdEstados.RowCount > 0 Then

                Dim Dr As DataRowView = grdEstados.GetFocusedRow

                Dim lIndex As Integer = -1
                lIndex = lEstadosUsuario.FindIndex(Function(b) b.IdRolUsuarioEstado = Dr.Item("Código"))

                Dim vIdPropietarioBodega As Integer = 0

                If lIndex > -1 Then

                    vIdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(lEstadosUsuario(lIndex).IdPropietario, AP.IdBodega)

                    pBeUsuarioEstado = lEstadosUsuario.Find(Function(b) b.IdRolUsuarioEstado = Dr.Item("Código"))

                    lblCodigoEstado.Text = lEstadosUsuario(lIndex).IdRolUsuarioEstado
                    cmdGuardarEstado.Tag = lEstadosUsuario(lIndex).IdRolUsuarioEstado
                    cmbPropietarioBodega.EditValue = vIdPropietarioBodega
                    cmbEstadoOrigen.EditValue = lEstadosUsuario(lIndex).IdEstadoOrigen
                    cmbEstadoDestino.EditValue = lEstadosUsuario(lIndex).IdEstadoDestino
                    chkPermitirFlujoEstado.IsOn = lEstadosUsuario(lIndex).Permitir
                    chkEstadoActivo.Checked = lEstadosUsuario(lIndex).Activo

                    cmbEstadoOrigen.Focus()

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

    Private Sub cmdDesactivarEstado_Click(sender As Object, e As EventArgs)

        Try

            Dim BeUsuarioEstado As New clsBeRol_usuario_estado()
            BeUsuarioEstado.IdRolUsuarioEstado = lblCodigoEstado.Text
            BeUsuarioEstado.User_mod = AP.UsuarioAp.IdUsuario
            BeUsuarioEstado.Fec_mod = Now
            clsLnRol_usuario_estado.Desactivar(BeUsuarioEstado)

            XtraMessageBox.Show("Registro desactivado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Cargar_Estados_Rol()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmbEstadoOrigen_EditValueChanged(sender As Object, e As EventArgs) Handles cmbEstadoOrigen.EditValueChanged

        Dim DT As New DataTable

        Try

            If Not cmbEstadoOrigen.EditValue Is Nothing Then

                Dim vIdPropietario As Integer = clsLnPropietarios.Get_IdPropietario(AP.IdBodega, cmbPropietarioBodega.EditValue)

                ' Obtener la lista de estados basados en el propietario y la bodega
                DT = clsLnProducto_estado.Listar_By_IdPropietario_And_IdBodega(vIdPropietario, AP.IdBodega)

                ' Filtrar el DataTable para excluir el valor seleccionado en cmbEstadoOrigen
                Dim filteredRows As DataRow() = DT.Select($"IdEstado <> {cmbEstadoOrigen.EditValue}")

                ' Verificar si hay filas resultantes después de la exclusión
                If filteredRows.Length > 0 Then
                    ' Convertir las filas filtradas nuevamente en un DataTable
                    DT = filteredRows.CopyToDataTable()

                    ' Configurar las propiedades del comboBox
                    cmbEstadoDestino.Properties.DisplayMember = "nombre"
                    cmbEstadoDestino.Properties.ValueMember = "IdEstado"
                    cmbEstadoDestino.Properties.DataSource = DT
                    cmbEstadoDestino.Properties.PopupWidth = 1400
                    cmbEstadoDestino.Properties.BestFit()
                    cmbEstadoDestino.Properties.NullText = ""
                End If

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private lEstadosUsuario As New List(Of clsBeRol_usuario_estado)
    Private pBeUsuarioEstado As New clsBeRol_usuario_estado()
    Private DTEstadosUsuario As New DataTable("EstadosUsuario")
    Private Sub Init_DT_EstadosUsuario()

        DTEstadosUsuario.Columns.Add("Código", GetType(Integer))
        DTEstadosUsuario.Columns.Add("Propietario", GetType(String))
        DTEstadosUsuario.Columns.Add("EstadoOrigen", GetType(String))
        DTEstadosUsuario.Columns.Add("EstadoDestino", GetType(String))
        DTEstadosUsuario.Columns.Add("Permitir", GetType(Boolean))
        DTEstadosUsuario.Columns.Add("Activo", GetType(Boolean))

    End Sub

End Class