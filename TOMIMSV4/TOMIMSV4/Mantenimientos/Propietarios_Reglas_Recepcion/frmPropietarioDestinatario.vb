Imports DevExpress.XtraEditors

Public Class frmPropietarioDestinatario

    Public pIdPropietario As Integer
    Public pNombrePropietario As String

    Private pListDestinatarios As New List(Of clsBePropietario_destinatario)
    Private pObjDestinatario As New clsBePropietario_destinatario

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

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

                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                    CargarDestinatarios()

                    'Case TipoTrans.Editar

                    '    mnuGuardar.Enabled = False
                    '    mnuActualizar.Enabled = True
                    '    mnuEliminar.Enabled = True

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        txtNombreDestinatario.Focus()

    End Sub

    Private Function Datos_Correctos()
        Datos_Correctos = False
        Try

            If pListDestinatarios Is Nothing Then
                XtraMessageBox.Show("Ingrese Destinatarios.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf pListDestinatarios.Count > 0 = False Then
                XtraMessageBox.Show("Ingrese Destinatarios.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

            clsLnPropietario_destinatario.GuardarDestinatario(pListDestinatarios)
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

                clsLnPropietario_destinatario.GuardarDestinatario(pListDestinatarios)
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
            If XtraMessageBox.Show("¿Guardar Destinatarios?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
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

            If XtraMessageBox.Show("¿Eliminar todos los Destinatarios?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                clsLnPropietario_destinatario.EliminarDestinatario(pListDestinatarios)
                XtraMessageBox.Show("Se ha elliminado todos los registros", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            End If
            mnuEliminar.Enabled = True
        Catch ex As Exception
            mnuEliminar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub CargarDestinatarios()

        Try

            'If pGuardo = False Then
            '    pListDestinatarios = clsLnPropietario_bodega.GetAllByIdPropietario(pIdPropietario).ToList()
            'End If

            Dim DT As New DataTable("Destinatarios")
            DT.Columns.Add("Correlativo", GetType(Integer))
            DT.Columns.Add("IdPropietario", GetType(Integer))
            DT.Columns.Add("Nombre", GetType(String))
            DT.Columns.Add("Apellido", GetType(String))
            DT.Columns.Add("Correo Electronico", GetType(String))
            DT.Columns.Add("Telefono", GetType(String))
            DT.Columns.Add("Telefono2", GetType(String))
            DT.Columns.Add("Cargo", GetType(String))
            'DT.Columns.Add("Activo", GetType(Boolean))

            GridDestinatarios.DataSource = Nothing

            For Each Obj As clsBePropietario_destinatario In pListDestinatarios.OrderBy(Function(o) o.IdDestinatarioPropietario AndAlso o.Activo = chkActivoD.Checked)
                Dim lRow As DataRow = DT.NewRow

                lRow(0) = Obj.IdDestinatarioPropietario
                lRow(1) = Obj.IdPropietario
                lRow(2) = Obj.Nombre
                lRow(3) = Obj.Apellido
                lRow(4) = Obj.Correo_electronico
                lRow(5) = Obj.Telefono
                lRow(6) = Obj.Telefono1
                lRow(7) = Obj.Cargo
                'lRow(8) = Obj.Activo

                DT.Rows.Add(lRow)

            Next

            GridDestinatarios.DataSource = DT
            ViewDestinatario.Columns("IdPropietario").Visible = False
            GridDestinatarios.Refresh()

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
        CargarDestinatarios()
    End Sub

    Private Sub cmdGuardar_Click(sender As Object, e As EventArgs) Handles cmdGuardar.Click

        Me.Cursor = Cursors.WaitCursor

        Try

            cmdGuardar.Enabled = False

            If String.IsNullOrEmpty(txtNombreDestinatario.Text) And String.IsNullOrEmpty(txtNombreDestinatario.Text) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombreDestinatario.Focus()
                Return
            ElseIf String.IsNullOrEmpty(txtApellidoDestinatario.Text) And String.IsNullOrEmpty(txtApellidoDestinatario.Text) Then
                XtraMessageBox.Show("Ingrese Apellido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtApellidoDestinatario.Focus()
                Return
            ElseIf String.IsNullOrEmpty(txtEmailDestinatario.Text) And String.IsNullOrEmpty(txtEmailDestinatario.Text) Then
                XtraMessageBox.Show("Ingrese Email", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtEmailDestinatario.Focus()
                Return
            ElseIf String.IsNullOrEmpty(txtTelefono1.Text) And String.IsNullOrEmpty(txtTelefono1.Text) Then
                XtraMessageBox.Show("Ingrese Número de Teléfono", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtEmailDestinatario.Focus()
                Return
            End If

            Dim pIndex As Integer = -1

            pIndex = pListDestinatarios.FindIndex(Function(b) b.IdDestinatarioPropietario = CInt(cmdGuardar.Tag))

            If pIndex > -1 Then
                pListDestinatarios(pIndex).Nombre = txtNombreDestinatario.Text
                pListDestinatarios(pIndex).Apellido = txtApellidoDestinatario.Text
                pListDestinatarios(pIndex).Correo_electronico = txtEmailDestinatario.Text
                pListDestinatarios(pIndex).Telefono = txtTelefono1.Text
                pListDestinatarios(pIndex).Telefono1 = txtTelefono2.Text
                pListDestinatarios(pIndex).Cargo = txtCargoDestinatario.Text
                'pListDestinatarios(pIndex).Activo = chkDestinatarioActivo.Checked
                cmdGuardar.Tag = Nothing
            Else

                Dim ObjDestinatario As New clsBePropietario_destinatario

                ObjDestinatario.IdPropietario = pIdPropietario

                If pListDestinatarios IsNot Nothing AndAlso pListDestinatarios.Count > 0 Then
                    ObjDestinatario.IdDestinatarioPropietario = pListDestinatarios.Max(Function(b) b.IdDestinatarioPropietario) + 1
                Else
                    ObjDestinatario.IdDestinatarioPropietario = 1
                End If

                ObjDestinatario.Nombre = txtNombreDestinatario.Text
                ObjDestinatario.Apellido = txtApellidoDestinatario.Text
                ObjDestinatario.Correo_electronico = txtEmailDestinatario.Text
                ObjDestinatario.Telefono = txtTelefono1.Text
                ObjDestinatario.Telefono1 = txtTelefono2.Text
                ObjDestinatario.Cargo = txtCargoDestinatario.Text
                ObjDestinatario.Activo = True
                ObjDestinatario.IsNew = True
                pListDestinatarios.Add(ObjDestinatario)

            End If

            CargarDestinatarios()
            Limpiar()

            cmdGuardar.Enabled = True

        Catch ex As Exception
            cmdGuardar.Enabled = True
            Me.Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click

        Try

            cmdDelete.Enabled = False

            If MessageBox.Show("¿Desactivar el Destinatario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjDestinatario.IdDestinatarioPropietario = CInt(cmdGuardar.Tag)

                Dim lIndex As Integer = -1
                If pObjDestinatario.IdDestinatarioPropietario > 0 Then
                    lIndex = pListDestinatarios.FindIndex(Function(b) b.IdDestinatarioPropietario = pObjDestinatario.IdDestinatarioPropietario AndAlso b.Activo = chkActivoD.Checked)
                End If

                If lIndex > -1 Then
                    clsLnPropietario_destinatario.DeleteDestinatario(pObjDestinatario.IdDestinatarioPropietario)
                    pListDestinatarios.RemoveAt(lIndex)
                    CargarDestinatarios()
                    Limpiar()
                End If

            End If
            cmdDelete.Enabled = True

        Catch ex As Exception
            cmdDelete.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Limpiar()
        lblCodigoD.Text = "-"
        txtNombreDestinatario.Text = String.Empty
        txtApellidoDestinatario.Text = String.Empty
        txtEmailDestinatario.Text = String.Empty
        txtTelefono1.Text = String.Empty
        txtTelefono2.Text = String.Empty
        txtCargoDestinatario.Text = String.Empty
    End Sub


    Private Sub GridDestinatarios_DoubleClick(sender As Object, e As EventArgs) Handles GridDestinatarios.DoubleClick

        Try

            If ViewDestinatario.RowCount > 0 Then

                Dim Dr As DataRowView = ViewDestinatario.GetFocusedRow

                Dim lIndex As Integer = -1

                lIndex = pListDestinatarios.FindIndex(Function(b) b.IdDestinatarioPropietario = CInt(Dr.Item("Correlativo")))

                If lIndex > -1 Then
                    cmdGuardar.Tag = pListDestinatarios(lIndex).IdDestinatarioPropietario
                    lblCodigoD.Text = cmdGuardar.Tag
                    txtNombreDestinatario.Text = pListDestinatarios(lIndex).Nombre
                    txtApellidoDestinatario.Text = pListDestinatarios(lIndex).Apellido
                    txtEmailDestinatario.Text = pListDestinatarios(lIndex).Correo_electronico
                    txtTelefono1.Text = pListDestinatarios(lIndex).Telefono
                    txtTelefono2.Text = pListDestinatarios(lIndex).Telefono1
                    txtCargoDestinatario.Text = pListDestinatarios(lIndex).Cargo
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