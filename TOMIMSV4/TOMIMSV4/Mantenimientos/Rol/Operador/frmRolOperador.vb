Imports DevExpress.XtraEditors

Public Class frmRolOperador

    Public ObjetoRolOperador As New clsBeRol_operador
    Public Delegate Sub cargarListadeRoles()
    Public Property InvokeListarRolOperador As cargarListadeRoles

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

    Private Sub frmRolOperador_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Try


            Select Case Modo

                Case TipoTrans.Nuevo

                    txtID.Text = clsLnRol_operador.MaxID()
                    txtNombre.Text = ""

                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    mnuActualizar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    clsLnRol_operador.CargaMenuRol(lvMenuRol, Integer.Parse(txtID.Text.ToString))
                    clsLnRol_operador.CargaMenuSistema(lvMenuSistema)

                    CheckBox1.CheckState = CheckState.Checked

                Case TipoTrans.Editar
                    txtID.Text = ObjetoRolOperador.IdRolOperador
                    If (clsLnRol_operador.Obtener(ObjetoRolOperador)) Then
                        txtNombre.Text = ObjetoRolOperador.Nombre

                        clsLnRol_operador.CargaMenuRol(lvMenuRol, Integer.Parse(txtID.Text.ToString))
                        clsLnRol_operador.CargaMenuSistema(lvMenuSistema)

                        Dim ObjetomenuRolOPerador = New clsBeMenu_rol_op() With {.IdRolOperador = Integer.Parse(txtID.Text)}

                        'utilizo este metodo para capturar la fecha de creacion de la entrada de los registros
                        If (lvMenuRol.Items.Count < 1) Then
                            User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                            Fec_agrDateEdit.Text = Date.Now
                        Else
                            ObjetomenuRolOPerador.IdMenuSistemaOP = lvMenuRol.Items(0).SubItems(1).Text
                            clsLnMenu_rol_op.Obtener(ObjetomenuRolOPerador)
                            User_agrTextEdit.Text = ObjetomenuRolOPerador.User_agr
                            Fec_agrDateEdit.Text = ObjetomenuRolOPerador.Fec_agr
                        End If

                        User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                        Fec_modDateEdit.Text = Date.Now

                        If OpcionesMenu IsNot Nothing Then
                            mnuActualizar.Enabled = OpcionesMenu.Modificar
                        End If

                        mnuGuardar.Enabled = False
                    Else
                        XtraMessageBox.Show("No se puedo cargar la informacion del Rol de Operador", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        Try

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar Rol Operador?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then
                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        InvokeListarRolOperador.Invoke
                        Close()
                    Else
                        XtraMessageBox.Show("Error al guardar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Datos_Correctos() As Boolean
        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If Datos_Correctos() Then

                If Actualizar() Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarRolOperador.Invoke
                    Close()
                Else
                    XtraMessageBox.Show("No se logro actualizar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Function Guardar() As Boolean
        Try
            ObjetoRolOperador.IdRolOperador = Integer.Parse(txtID.Text)
            ObjetoRolOperador.Nombre = txtNombre.Text
            Return clsLnRol_operador.Insertar(ObjetoRolOperador) > 0
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Try

            ObjetoRolOperador.IdRolOperador = Integer.Parse(txtID.Text)
            ObjetoRolOperador.Nombre = txtNombre.Text
            Return clsLnRol_operador.Actualizar(ObjetoRolOperador) > 0
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            For Each m As ListViewItem In lvMenuSistema.Items
                m.Checked = (CheckBox1.Checked = True)
            Next
            Application.DoEvents()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Try
            For Each m As ListViewItem In lvMenuRol.Items
                m.Checked = (CheckBox2.Checked = True)
            Next
            Application.DoEvents()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click

        Try

            If Datos_Correctos() Then

                If (clsLnRol_operador.ExistOperador(txtID.Text)) Then
                    RegistrandoPermisos()

                Else
                    If MessageBox.Show("¿Guardar Rol Operador?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        If Guardar() Then
                            RegistrandoPermisos()
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

    Private Sub cmdQuitar_Click(sender As Object, e As EventArgs) Handles cmdQuitar.Click
        Dim I As Integer, vMenuID$
        Dim ObjetomenuRolOPerador As clsBeMenu_rol_op
        Try

            prg.Visible = True
            prg.Maximum = lvMenuRol.Items.Count

            For I = 0 To lvMenuRol.Items.Count - 1

                If lvMenuRol.Items(I).Checked = True Then

                    vMenuID$ = lvMenuRol.Items(I).SubItems(1).Text

                    ObjetomenuRolOPerador = New clsBeMenu_rol_op()
                    ObjetomenuRolOPerador.IdMenuSistemaOP = vMenuID$
                    ObjetomenuRolOPerador.IdRolOperador = Integer.Parse(txtID.Text)
                    ObjetomenuRolOPerador.User_agr = User_agrTextEdit.Text.Trim
                    ObjetomenuRolOPerador.Fec_agr = Fec_agrDateEdit.Text.Trim
                    ObjetomenuRolOPerador.User_mod = String.Format("{0} {1}", AP.UsuarioAp.User_agr, AP.UsuarioAp.User_mod)
                    ObjetomenuRolOPerador.Fec_mod = Fec_modDateEdit.Text
                    ObjetomenuRolOPerador.Visible = False
                    ObjetomenuRolOPerador.Activo = True
                    clsLnMenu_rol_op.Actualizar(ObjetomenuRolOPerador)

                    Application.DoEvents()

                    prg.Value = I

                End If

            Next

            'clsLnRol.CargaMenuRol(lvMenuRol, CInt(txtID.Text.ToString))
            clsLnRol_operador.CargaMenuRol(lvMenuRol, txtID.Text.ToString)

            XtraMessageBox.Show("Se han desactivado las Permisos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            prg.Visible = False
        End Try

    End Sub

    Private Sub RegistrandoPermisos()

        Dim ObjetomenuRolOPerador As clsBeMenu_rol_op
        Dim I As Integer, vMenuID$

        Try

            prg.Visible = True

            prg.Maximum = (lvMenuSistema.Items.Count)

            For I = 0 To lvMenuSistema.Items.Count - 1
                vMenuID$ = lvMenuSistema.Items(I).SubItems(1).Text

                If lvMenuSistema.Items(I).Checked = True Then
                    ObjetomenuRolOPerador = New clsBeMenu_rol_op()
                    ObjetomenuRolOPerador.IdMenuSistemaOP = vMenuID$
                    ObjetomenuRolOPerador.IdRolOperador = Integer.Parse(txtID.Text)
                    ObjetomenuRolOPerador.User_agr = User_agrTextEdit.Text
                    ObjetomenuRolOPerador.Fec_agr = Fec_agrDateEdit.Text
                    ObjetomenuRolOPerador.User_mod = User_modTextEdit.Text
                    ObjetomenuRolOPerador.Fec_mod = Fec_modDateEdit.Text
                    ObjetomenuRolOPerador.Visible = lvMenuSistema.Items(I).Checked
                    ObjetomenuRolOPerador.Activo = True

                    If (clsLnRol_operador.ExistePermiso(vMenuID$, txtID.Text)) Then
                        clsLnMenu_rol_op.Actualizar(ObjetomenuRolOPerador)
                    Else
                        clsLnMenu_rol_op.Insertar(ObjetomenuRolOPerador)
                    End If

                End If

                prg.Value = I

                Application.DoEvents()

            Next I

            clsLnRol_operador.CargaMenuRol(lvMenuRol, Integer.Parse(txtID.Text.ToString))

            XtraMessageBox.Show("Se han activado las Permisos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            prg.Visible = False
        End Try

    End Sub

    Private Sub frmRolOperador_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class