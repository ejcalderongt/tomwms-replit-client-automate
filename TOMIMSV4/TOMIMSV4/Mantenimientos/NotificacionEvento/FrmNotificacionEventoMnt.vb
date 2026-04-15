Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FrmNotificacionEventoMnt

    Private _be As clsBENotificacionEvento
    Private _dtEventos As DataTable
    Private _esNuevo As Boolean = True
    Private _dxValidationProvider As DXValidationProvider

    Private Sub FrmNotificacionEventoMnt_Load(sender As Object, e As EventArgs) Handles Me.Load
        _dxValidationProvider = New DXValidationProvider()
        ConfigurarValidaciones()
        ConfigurarEventosUI()
        CargarEventos()
        PrepararNuevo()
    End Sub

    Private Sub ConfigurarEventosUI()
        AddHandler mnuNuevo.ItemClick, AddressOf mnuNuevo_ItemClick
        AddHandler mnuGuardar.ItemClick, AddressOf mnuGuardar_ItemClick
        AddHandler mnuEliminar.ItemClick, AddressOf mnuEliminar_ItemClick
        AddHandler mnuRefrescar.ItemClick, AddressOf mnuRefrescar_ItemClick

        AddHandler txtBuscar.TextChanged, AddressOf txtBuscar_TextChanged
        AddHandler gvEventos.FocusedRowChanged, AddressOf gvEventos_FocusedRowChanged
        AddHandler gvEventos.DoubleClick, AddressOf gvEventos_DoubleClick
    End Sub

    Private Sub ConfigurarValidaciones()
        Dim reglaCodigo As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El código del evento es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(txtCodigoEvento, reglaCodigo)

        Dim reglaNombre As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El nombre del evento es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(txtNombreEvento, reglaNombre)
    End Sub

    Private Sub CargarEventos()
        _dtEventos = clsLnNotificacionEvento.Listar()

        If _dtEventos Is Nothing Then
            _dtEventos = New DataTable()
        End If

        gcEventos.DataSource = Nothing
        gcEventos.DataSource = _dtEventos

        ConfigurarColumnasGrid()
    End Sub

    Private Sub ConfigurarColumnasGrid()
        If gvEventos.Columns("IdEvento") IsNot Nothing Then gvEventos.Columns("IdEvento").Caption = "Id"
        If gvEventos.Columns("CodigoEvento") IsNot Nothing Then gvEventos.Columns("CodigoEvento").Caption = "Código"
        If gvEventos.Columns("NombreEvento") IsNot Nothing Then gvEventos.Columns("NombreEvento").Caption = "Nombre"
        If gvEventos.Columns("Modulo") IsNot Nothing Then gvEventos.Columns("Modulo").Caption = "Módulo"
        If gvEventos.Columns("Descripcion") IsNot Nothing Then gvEventos.Columns("Descripcion").Caption = "Descripción"
        If gvEventos.Columns("Activo") IsNot Nothing Then gvEventos.Columns("Activo").Caption = "Activo"

        If gvEventos.Columns("FechaCreacion") IsNot Nothing Then gvEventos.Columns("FechaCreacion").Visible = False
        If gvEventos.Columns("UsuarioCreacion") IsNot Nothing Then gvEventos.Columns("UsuarioCreacion").Visible = False
        If gvEventos.Columns("FechaModificacion") IsNot Nothing Then gvEventos.Columns("FechaModificacion").Visible = False
        If gvEventos.Columns("UsuarioModificacion") IsNot Nothing Then gvEventos.Columns("UsuarioModificacion").Visible = False

        gvEventos.BestFitColumns()
    End Sub

    Private Sub PrepararNuevo()
        _esNuevo = True
        _be = New clsBENotificacionEvento()

        txtIdEvento.EditValue = Nothing
        txtCodigoEvento.EditValue = Nothing
        txtNombreEvento.EditValue = Nothing
        txtModulo.EditValue = Nothing
        txtDescripcion.EditValue = Nothing
        chkActivo.Checked = True

        User_agrTextEdit.EditValue = Nothing
        Fec_agrDateEdit.EditValue = Nothing
        User_modTextEdit.EditValue = Nothing
        Fec_modDateEdit.EditValue = Nothing

        txtCodigoEvento.Focus()
    End Sub

    Private Function ConstruirEntidadDesdePantalla() As clsBENotificacionEvento
        Dim be As clsBENotificacionEvento

        If _be Is Nothing Then
            be = New clsBENotificacionEvento()
        Else
            be = _be
        End If

        be.IdEvento = If(String.IsNullOrWhiteSpace(txtIdEvento.Text), 0, Convert.ToInt32(txtIdEvento.EditValue))
        be.CodigoEvento = If(txtCodigoEvento.Text, "").Trim().ToUpperInvariant()
        be.NombreEvento = If(txtNombreEvento.Text, "").Trim()
        be.Modulo = If(txtModulo.Text, "").Trim().ToUpperInvariant()
        be.Descripcion = If(txtDescripcion.Text, "").Trim()
        be.Activo = chkActivo.Checked

        If be.IdEvento = 0 Then
            be.FechaCreacion = Date.Now
            be.UsuarioCreacion = Environment.UserName
        End If

        be.FechaModificacion = Date.Now
        be.UsuarioModificacion = Environment.UserName

        Return be
    End Function

    Private Sub CargarDetalle(ByVal pIdEvento As Integer)
        If pIdEvento <= 0 Then Exit Sub

        Dim be As New clsBENotificacionEvento With {
            .IdEvento = pIdEvento
        }

        If clsLnNotificacionEvento.Obtener(be) Then
            _be = be
            _esNuevo = False

            txtIdEvento.EditValue = be.IdEvento
            txtCodigoEvento.EditValue = be.CodigoEvento
            txtNombreEvento.EditValue = be.NombreEvento
            txtModulo.EditValue = be.Modulo
            txtDescripcion.EditValue = be.Descripcion
            chkActivo.Checked = be.Activo

            User_agrTextEdit.EditValue = be.UsuarioCreacion
            Fec_agrDateEdit.EditValue = If(be.FechaCreacion = Date.MinValue, CType(Nothing, Object), be.FechaCreacion)
            User_modTextEdit.EditValue = be.UsuarioModificacion
            Fec_modDateEdit.EditValue = If(be.FechaModificacion = Date.MinValue, CType(Nothing, Object), be.FechaModificacion)
        End If
    End Sub

    Private Sub Guardar()
        If Not _dxValidationProvider.Validate() Then Exit Sub

        Dim be As clsBENotificacionEvento = ConstruirEntidadDesdePantalla()

        If String.IsNullOrWhiteSpace(be.CodigoEvento) Then
            Throw New Exception("El código del evento es obligatorio.")
        End If

        If String.IsNullOrWhiteSpace(be.NombreEvento) Then
            Throw New Exception("El nombre del evento es obligatorio.")
        End If

        If clsLnNotificacionEvento.ExisteCodigo(be.CodigoEvento, be.IdEvento) Then
            Throw New Exception("Ya existe un evento con ese código.")
        End If

        If _esNuevo OrElse be.IdEvento = 0 Then
            clsLnNotificacionEvento.Insertar(be)
            XtraMessageBox.Show("Registro creado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            clsLnNotificacionEvento.Actualizar(be)
            XtraMessageBox.Show("Registro actualizado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        CargarEventos()
        EnfocarRegistro(be.IdEvento)
    End Sub

    Private Sub Eliminar()
        If _esNuevo OrElse String.IsNullOrWhiteSpace(txtIdEvento.Text) Then
            XtraMessageBox.Show("Seleccione un registro existente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim idEvento As Integer = Convert.ToInt32(txtIdEvento.EditValue)

        If clsLnNotificacionEvento.TieneDependencias(idEvento) Then
            Throw New Exception("No se puede eliminar el evento porque tiene dependencias relacionadas.")
        End If

        Dim r = XtraMessageBox.Show(
            String.Format("¿Desea eliminar el evento {0}?", txtCodigoEvento.Text),
            "Confirmación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If r <> DialogResult.Yes Then Exit Sub

        Dim be As New clsBENotificacionEvento With {
            .IdEvento = idEvento
        }

        clsLnNotificacionEvento.Eliminar(be)

        XtraMessageBox.Show("Registro eliminado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)

        CargarEventos()
        PrepararNuevo()
    End Sub

    Private Sub EnfocarRegistro(ByVal pIdEvento As Integer)
        If pIdEvento <= 0 Then Exit Sub

        For i As Integer = 0 To gvEventos.RowCount - 1
            Dim valor As Object = gvEventos.GetRowCellValue(i, "IdEvento")
            If valor IsNot Nothing AndAlso Not IsDBNull(valor) AndAlso Convert.ToInt32(valor) = pIdEvento Then
                gvEventos.FocusedRowHandle = i
                gvEventos.MakeRowVisible(i)
                CargarDetalle(pIdEvento)
                Exit Sub
            End If
        Next
    End Sub

    Private Sub AplicarFiltro()
        If _dtEventos Is Nothing Then Exit Sub

        Dim texto As String = If(txtBuscar.Text, "").Trim().Replace("'", "''")
        Dim dv As New DataView(_dtEventos)

        If String.IsNullOrWhiteSpace(texto) Then
            dv.RowFilter = ""
        Else
            dv.RowFilter = String.Format(
                "CodigoEvento LIKE '%{0}%' OR NombreEvento LIKE '%{0}%' OR Modulo LIKE '%{0}%'",
                texto)
        End If

        gcEventos.DataSource = dv
    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        PrepararNuevo()
    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Try
            Guardar()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Try
            Eliminar()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuRefrescar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Try
            Dim idActual As Integer = If(String.IsNullOrWhiteSpace(txtIdEvento.Text), 0, Convert.ToInt32(txtIdEvento.EditValue))
            CargarEventos()

            If idActual > 0 Then
                EnfocarRegistro(idActual)
            Else
                PrepararNuevo()
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs)
        AplicarFiltro()
    End Sub

    Private Sub gvEventos_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        If e.FocusedRowHandle < 0 Then Exit Sub

        Dim valor As Object = gvEventos.GetRowCellValue(e.FocusedRowHandle, "IdEvento")
        If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub

        CargarDetalle(Convert.ToInt32(valor))
    End Sub

    Private Sub gvEventos_DoubleClick(sender As Object, e As EventArgs)
        If gvEventos.FocusedRowHandle < 0 Then Exit Sub

        Dim valor As Object = gvEventos.GetRowCellValue(gvEventos.FocusedRowHandle, "IdEvento")
        If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub

        CargarDetalle(Convert.ToInt32(valor))
    End Sub

End Class