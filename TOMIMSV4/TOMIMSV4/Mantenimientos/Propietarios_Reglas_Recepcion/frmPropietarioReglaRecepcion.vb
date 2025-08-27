Imports DevExpress.XtraEditors

Public Class frmPropietarioReglaRecepcion

    Public pIdPropietario As Integer
    Public pIdReglaPropietarioEnc As Integer
    Public pNombrePropietario As String

    Private pObjEnc As New clsBePropietario_reglas_enc
    Private pListObjR As New List(Of clsBePropietario_reglas_det)

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

    Private Sub frmPropietarioReglaRecepcion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            lblNombrePropietario.Text = pNombrePropietario

            Select Case Modo

                Case TipoTrans.Nuevo

                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now
                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                    'cmbRegla.Enabled = True
                    'cmbMensaje.Enabled = True

                Case TipoTrans.Editar

                    pObjEnc = clsLnPropietario_reglas_enc.GetSingle(pIdReglaPropietarioEnc)

                    lblCodigo.Text = pObjEnc.IdReglaPropietarioEnc

                    If pObjEnc.IdReglaPropietarioEnc > 0 Then
                        txtIdRegla.Text = pObjEnc.IdReglaRecepcion
                        txtNombreRegla.Text = pObjEnc.Regla.Nombre
                    End If

                    If pObjEnc.IdMensajeRegla > 0 Then
                        txtIdMensaje.Text = pObjEnc.IdMensajeRegla
                        txtNombreMensaje.Text = pObjEnc.Mensaje.Nombre
                    End If

                    chkActivo.Checked = pObjEnc.Activo

                    User_agrTextEdit.Text = pObjEnc.User_agr
                    Fec_agrDateEdit.Text = pObjEnc.Fec_agr
                    User_modTextEdit.Text = pObjEnc.User_mod
                    Fec_modDateEdit.Text = pObjEnc.Fec_mod

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = True
                    mnuEliminar.Enabled = True

                    pListObjR = clsLnPropietario_reglas_det.Get_All_By_IdReglaPropietarioEnc(pObjEnc.IdReglaPropietarioEnc).ToList

                    Cargar_Destinatarios()

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            txtIdRegla.Focus()
        End Try

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtIdRegla.Text.Trim()) Then
                XtraMessageBox.Show("Seleccione Regla", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtIdMensaje.Text.Trim()) Then
                XtraMessageBox.Show("Seleccione Mensaje", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf pListObjR.Count = 0 Then
                XtraMessageBox.Show("Ingrese los destinatarios para la regla", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            pObjEnc = New clsBePropietario_reglas_enc()

            pObjEnc.IdPropietario = pIdPropietario

            If String.IsNullOrEmpty(txtIdRegla.Text.Trim()) = False Then
                pObjEnc.IdReglaRecepcion = CInt(txtIdRegla.Text.Trim())
            End If

            If String.IsNullOrEmpty(txtIdMensaje.Text.Trim()) = False Then
                pObjEnc.IdMensajeRegla = CInt(txtIdMensaje.Text.Trim())
            End If

            pObjEnc.User_agr = AP.UsuarioAp.IdUsuario
            pObjEnc.Fec_agr = Now
            pObjEnc.User_mod = AP.UsuarioAp.IdUsuario
            pObjEnc.Fec_mod = Now
            pObjEnc.Activo = True
            pObjEnc.IsNew = True

            clsLnPropietario_reglas_enc.Guarda(pObjEnc, pListObjR)

            Return True

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

                If String.IsNullOrEmpty(txtIdRegla.Text.Trim()) = False Then
                    pObjEnc.IdReglaRecepcion = CInt(txtIdRegla.Text.Trim())
                End If

                If String.IsNullOrEmpty(txtIdMensaje.Text.Trim()) = False Then
                    pObjEnc.IdMensajeRegla = CInt(txtIdMensaje.Text.Trim())
                End If

                pObjEnc.User_agr = AP.UsuarioAp.IdUsuario
                pObjEnc.Fec_agr = Now
                pObjEnc.User_mod = AP.UsuarioAp.IdUsuario
                pObjEnc.Fec_mod = Now
                pObjEnc.Activo = chkActivo.Checked
                pObjEnc.IsNew = False

                clsLnPropietario_reglas_enc.Guarda(pObjEnc, pListObjR)

                Return True

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
        mnuGuardar.Enabled = False
        If Datos_Correctos() Then
            If XtraMessageBox.Show("¿Guardar Regla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                End If
            End If
        End If
        mnuGuardar.Enabled = True
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        mnuActualizar.Enabled = False
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
        mnuActualizar.Enabled = True
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False
            If XtraMessageBox.Show("¿Desactivar Regla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                clsLnPropietario_reglas_enc.Desactivar_Regla(pObjEnc.IdReglaPropietarioEnc)
                XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
                frmPropietarioReglaRecepcion_List.Dgrid.Refresh()
            End If
            mnuEliminar.Enabled = True
        Catch ex As Exception
            mnuEliminar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub GridDestinatarios_DoubleClick(sender As Object, e As EventArgs) Handles GridDestinatarios.DoubleClick

        Try

            If ViewDestinatario.RowCount > 0 Then

                Dim Dr As DataRowView = ViewDestinatario.GetFocusedRow

                Dim lIndex As Integer = -1

                lIndex = pListObjR.FindIndex(Function(b) b.IdReglaPropietarioEnc = pObjEnc.IdReglaPropietarioEnc And b.IdReglaPropietarioDet = CInt(Dr.Item("IdReglaPropietarioDet")))

                Dim Destinatario As New frmAgregaDestinatario
                Destinatario.pIndex = lIndex
                Destinatario.pIdReglaPropietarioEnc = pObjEnc.IdReglaPropietarioEnc
                Destinatario.pIdReglaPropietarioDet = CInt(Dr.Item("IdReglaPropietarioDet"))
                Destinatario.pListObjPR = pListObjR
                Destinatario.pIdPropietario = pIdPropietario
                Destinatario.pNombrePropietario = lblNombrePropietario.Text
                Destinatario.Cargar = New frmAgregaDestinatario.Operar(AddressOf Cargar_Destinatarios)
                Destinatario.ShowDialog()

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

    Private Sub Cargar_Destinatarios()

        Try

            GridDestinatarios.DataSource = Nothing

            If pListObjR IsNot Nothing AndAlso pListObjR.Count > 0 Then

                Dim DT As New DataTable("Destinatario")
                DT.Columns.Add("IdReglaPropietarioDet", GetType(Integer))
                DT.Columns.Add("Código", GetType(Integer))
                DT.Columns.Add("NombreDestinatario", GetType(String))

                For Each r As clsBePropietario_reglas_det In pListObjR.FindAll(Function(b) b.Activo = chkActivoD.Checked)
                    DT.Rows.Add(r.IdReglaPropietarioDet, r.IdDestinatarioPropietario, r.NombreDestinatario)
                Next

                GridDestinatarios.DataSource = DT

                If ViewDestinatario.Columns.Count > 0 Then
                    ViewDestinatario.Columns("IdReglaPropietarioDet").Visible = False
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

    Private Sub chkActivoD_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivoD.CheckedChanged
        'pListObjR = clsLnPropietario_reglas_enc.GetAllByEncabezado(pObjEnc.IdReglaPropietarioEnc, chkActivoD.Checked).ToList
        Cargar_Destinatarios()
    End Sub

    Private Sub cmdEliminarDestinatario_Click(sender As Object, e As EventArgs) Handles cmdDesactivar.Click

        Try

            If ViewDestinatario.RowCount > 0 Then

                Dim Dr As DataRowView = ViewDestinatario.GetFocusedRow

                If XtraMessageBox.Show("¿Desactivar destinatario: " & Dr.Item("NombreDestinatario") & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    '#EJC202107141547_RP: Corrección al eliminar un destinatario en regla de propietario cuando aún no se ha guardado la regla.
                    If Modo = TipoTrans.Nuevo Then

                        Dim ObjReglaDetProp As New clsBePropietario_reglas_det()
                        ObjReglaDetProp = pListObjR.Find(Function(x) x.NombreDestinatario = Dr.Item("NombreDestinatario"))

                        If Not ObjReglaDetProp Is Nothing Then
                            pListObjR.Remove(ObjReglaDetProp)
                            XtraMessageBox.Show("Destinatario Desactivado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        clsLnPropietario_reglas_det.Desactivar(Dr.Item("IdReglaPropietarioDet"))
                        pListObjR = clsLnPropietario_reglas_det.Get_All_By_IdReglaPropietarioEnc(pObjEnc.IdReglaPropietarioEnc).ToList
                        XtraMessageBox.Show("Destinatario Desactivado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    Cargar_Destinatarios()

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

    Private Sub lnkRegla_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkRegla.LinkClicked

        txtIdRegla.Text = String.Empty
        txtNombreRegla.Text = String.Empty

        Try

            Dim Regla As New frmReglas_recepcion_List()
            Regla.Modo = frmReglas_recepcion_List.pModo.Seleccion
            Regla.ShowDialog()

            If Regla.pObjRegla IsNot Nothing AndAlso Regla.pObjRegla.IdReglaRecepcion > 0 Then
                If clsLnPropietario_reglas_enc.ExisteRegla(Regla.pObjRegla.IdReglaRecepcion, pIdPropietario) Then
                    Throw New Exception("La regla " & Regla.pObjRegla.Nombre & " ya existe.")
                End If
                txtIdRegla.Text = Regla.pObjRegla.IdReglaRecepcion
                txtNombreRegla.Text = Regla.pObjRegla.Nombre
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

    Private Sub lnkMensaje_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkMensaje.LinkClicked

        txtIdMensaje.Text = String.Empty
        txtNombreMensaje.Text = String.Empty

        Try

            Dim Mensaje As New frmMensajeRegla_List()
            Mensaje.Modo = frmMensajeRegla_List.pModo.Seleccion
            Mensaje.ShowDialog()

            If Mensaje.pObjMensaje IsNot Nothing AndAlso Mensaje.pObjMensaje.IdMensajeRegla > 0 Then
                txtIdMensaje.Text = Mensaje.pObjMensaje.IdMensajeRegla
                txtNombreMensaje.Text = Mensaje.pObjMensaje.Nombre
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

    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click

        Try

            Dim Destinatario As New frmAgregaDestinatario
            Destinatario.pIndex = -1
            Destinatario.pListObjPR = pListObjR
            Destinatario.pIdPropietario = pIdPropietario
            Destinatario.pNombrePropietario = lblNombrePropietario.Text
            Destinatario.Cargar = New frmAgregaDestinatario.Operar(AddressOf Cargar_Destinatarios)
            Destinatario.ShowDialog()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdRegla_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdRegla.PreviewKeyDown

        Try

            If e.KeyData = Keys.Tab Then

                If txtIdRegla.Text > "0" Then

                    If String.IsNullOrEmpty(txtIdRegla.Text.Trim()) = False Then

                        Dim Obj As New clsBeReglas_recepcion

                        Obj.IdReglaRecepcion = (txtIdRegla.Text.Trim())
                        clsLnReglas_recepcion.GetSingle(Obj)

                        If Obj IsNot Nothing AndAlso Obj.IdReglaRecepcion > 0 Then
                            txtNombreRegla.Text = Obj.Nombre
                        Else
                            XtraMessageBox.Show(String.Format("No existe Regla con código {0}", txtIdRegla.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtIdRegla.Text = String.Empty
                            txtNombreRegla.Focus() : txtIdRegla.SelectAll()
                        End If

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

    Private Sub txtIdMensaje_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdMensaje.PreviewKeyDown

        Try

            If e.KeyData = Keys.Tab Then

                If txtIdMensaje.Text > "0" Then

                    If String.IsNullOrEmpty(txtIdMensaje.Text.Trim()) = False Then

                        Dim Obj As clsBeMensaje_regla = clsLnMensaje_regla.GetSingle(txtIdMensaje.Text.Trim())

                        If Obj IsNot Nothing AndAlso Obj.IdMensajeRegla > 0 Then
                            txtNombreMensaje.Text = Obj.Nombre
                        Else
                            XtraMessageBox.Show(String.Format("No existe Mensaje con código {0}", txtIdMensaje.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtIdMensaje.Text = String.Empty
                            txtNombreMensaje.Focus() : txtIdMensaje.SelectAll()
                        End If

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

    Private Sub ViewDestinatario_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ViewDestinatario.RowStyle

        Try

            ViewDestinatario.OptionsBehavior.Editable = False
            ViewDestinatario.OptionsSelection.EnableAppearanceFocusedCell = False

            ViewDestinatario.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            ViewDestinatario.OptionsSelection.EnableAppearanceFocusedRow = True
            ViewDestinatario.OptionsSelection.EnableAppearanceHideSelection = True
            ViewDestinatario.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            ViewDestinatario.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            ViewDestinatario.Appearance.FocusedRow.ForeColor = Color.White
            ViewDestinatario.Appearance.SelectedRow.ForeColor = Color.White

            ViewDestinatario.Appearance.SelectedRow.Options.UseBackColor = True
            ViewDestinatario.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try


    End Sub
End Class