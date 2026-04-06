Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FrmNotificacionPlantillaMnt

    Private _be As clsBENotificacionPlantilla
    Private _dtPlantillas As DataTable
    Private _dtEventos As DataTable
    Private _dtLayouts As DataTable
    Private _esNuevo As Boolean = True
    Private _dxValidationProvider As DXValidationProvider

    Private Sub FrmNotificacionPlantillaMnt_Load(sender As Object, e As EventArgs) Handles Me.Load
        _dxValidationProvider = New DXValidationProvider()
        ConfigurarValidaciones()
        ConfigurarEventosUI()
        CargarCombos()
        CargarPlantillas()
        PrepararNuevo()
    End Sub

    Private Sub ConfigurarEventosUI()
        AddHandler mnuNuevo.ItemClick, AddressOf mnuNuevo_ItemClick
        AddHandler mnuGuardar.ItemClick, AddressOf mnuGuardar_ItemClick
        AddHandler mnuEliminar.ItemClick, AddressOf mnuEliminar_ItemClick
        AddHandler mnuRefrescar.ItemClick, AddressOf mnuRefrescar_ItemClick
        AddHandler txtBuscar.TextChanged, AddressOf txtBuscar_TextChanged
        AddHandler gvPlantillas.FocusedRowChanged, AddressOf gvPlantillas_FocusedRowChanged
        AddHandler gvPlantillas.DoubleClick, AddressOf gvPlantillas_DoubleClick
        AddHandler chkUsaLayoutComun.CheckedChanged, AddressOf chkUsaLayoutComun_CheckedChanged
    End Sub

    Private Sub ConfigurarValidaciones()
        Dim reglaCodigo As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El código de la plantilla es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(txtCodigoPlantilla, reglaCodigo)

        Dim reglaNombre As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El nombre de la plantilla es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(txtNombrePlantilla, reglaNombre)

        Dim reglaEvento As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "Debe seleccionar un evento."
        }
        _dxValidationProvider.SetValidationRule(lueEvento, reglaEvento)
    End Sub

    Private Sub CargarCombos()
        _dtEventos = clsLnNotificacionEvento.Listar()
        _dtLayouts = clsLnNotificacionLayout.Listar()

        lueEvento.Properties.DataSource = _dtEventos
        lueEvento.Properties.DisplayMember = "NombreEvento"
        lueEvento.Properties.ValueMember = "IdEvento"
        lueEvento.Properties.PopulateColumns()
        If lueEvento.Properties.Columns("IdEvento") IsNot Nothing Then lueEvento.Properties.Columns("IdEvento").Visible = False
        If lueEvento.Properties.Columns("CodigoEvento") IsNot Nothing Then lueEvento.Properties.Columns("CodigoEvento").Caption = "Código"
        If lueEvento.Properties.Columns("NombreEvento") IsNot Nothing Then lueEvento.Properties.Columns("NombreEvento").Caption = "Nombre"
        For Each col In lueEvento.Properties.Columns
            If col.FieldName <> "CodigoEvento" AndAlso col.FieldName <> "NombreEvento" Then
                col.Visible = False
            End If
        Next

        lueLayout.Properties.DataSource = _dtLayouts
        lueLayout.Properties.DisplayMember = "NombreLayout"
        lueLayout.Properties.ValueMember = "IdLayout"
        lueLayout.Properties.PopulateColumns()
        If lueLayout.Properties.Columns("IdLayout") IsNot Nothing Then lueLayout.Properties.Columns("IdLayout").Visible = False
        If lueLayout.Properties.Columns("CodigoLayout") IsNot Nothing Then lueLayout.Properties.Columns("CodigoLayout").Caption = "Código"
        If lueLayout.Properties.Columns("NombreLayout") IsNot Nothing Then lueLayout.Properties.Columns("NombreLayout").Caption = "Nombre"
        For Each col In lueLayout.Properties.Columns
            If col.FieldName <> "CodigoLayout" AndAlso col.FieldName <> "NombreLayout" Then
                col.Visible = False
            End If
        Next
    End Sub

    Private Sub CargarPlantillas()
        _dtPlantillas = clsLnNotificacionPlantilla.Listar()

        If _dtPlantillas Is Nothing Then
            _dtPlantillas = New DataTable()
        End If

        gcPlantillas.DataSource = Nothing
        gcPlantillas.DataSource = _dtPlantillas

        ConfigurarColumnasGrid()
    End Sub

    Private Sub ConfigurarColumnasGrid()
        If gvPlantillas.Columns("IdPlantilla") IsNot Nothing Then gvPlantillas.Columns("IdPlantilla").Caption = "Id"
        If gvPlantillas.Columns("CodigoPlantilla") IsNot Nothing Then gvPlantillas.Columns("CodigoPlantilla").Caption = "Código"
        If gvPlantillas.Columns("NombrePlantilla") IsNot Nothing Then gvPlantillas.Columns("NombrePlantilla").Caption = "Nombre"
        If gvPlantillas.Columns("Canal") IsNot Nothing Then gvPlantillas.Columns("Canal").Caption = "Canal"
        If gvPlantillas.Columns("VersionPlantilla") IsNot Nothing Then gvPlantillas.Columns("VersionPlantilla").Caption = "Versión"
        If gvPlantillas.Columns("Activo") IsNot Nothing Then gvPlantillas.Columns("Activo").Caption = "Activo"

        Dim ocultas = {"IdEvento", "IdLayout", "AsuntoTemplate", "BodyHtmlTemplate", "UsaLayoutComun", "FechaCreacion", "UsuarioCreacion", "FechaModificacion", "UsuarioModificacion"}
        For Each nombre In ocultas
            If gvPlantillas.Columns(nombre) IsNot Nothing Then gvPlantillas.Columns(nombre).Visible = False
        Next

        gvPlantillas.BestFitColumns()
    End Sub

    Private Sub PrepararNuevo()
        _esNuevo = True
        _be = New clsBENotificacionPlantilla()

        txtIdPlantilla.EditValue = Nothing
        lueEvento.EditValue = Nothing
        lueLayout.EditValue = Nothing
        txtCodigoPlantilla.EditValue = Nothing
        txtNombrePlantilla.EditValue = Nothing
        txtCanal.EditValue = "EMAIL"
        txtVersionPlantilla.EditValue = "1"
        txtAsuntoTemplate.EditValue = Nothing
        txtBodyHtmlTemplate.EditValue = Nothing
        chkUsaLayoutComun.Checked = True
        chkActivo.Checked = True

        User_agrTextEdit.EditValue = Nothing
        Fec_agrDateEdit.EditValue = Nothing
        User_modTextEdit.EditValue = Nothing
        Fec_modDateEdit.EditValue = Nothing

        AplicarEstadoLayout()
        txtCodigoPlantilla.Focus()
    End Sub

    Private Sub AplicarEstadoLayout()
        lueLayout.Enabled = chkUsaLayoutComun.Checked
        If Not chkUsaLayoutComun.Checked Then
            lueLayout.EditValue = Nothing
        End If
    End Sub

    Private Function ConstruirEntidadDesdePantalla() As clsBENotificacionPlantilla
        Dim be As clsBENotificacionPlantilla = If(_be Is Nothing, New clsBENotificacionPlantilla(), _be)

        be.IdPlantilla = If(String.IsNullOrWhiteSpace(txtIdPlantilla.Text), 0, Convert.ToInt32(txtIdPlantilla.EditValue))
        be.IdEvento = If(lueEvento.EditValue Is Nothing, 0, Convert.ToInt32(lueEvento.EditValue))
        be.IdLayout = If(lueLayout.EditValue Is Nothing, 0, Convert.ToInt32(lueLayout.EditValue))
        be.CodigoPlantilla = If(txtCodigoPlantilla.Text, "").Trim().ToUpperInvariant()
        be.NombrePlantilla = If(txtNombrePlantilla.Text, "").Trim()
        be.Canal = If(txtCanal.Text, "").Trim().ToUpperInvariant()
        be.AsuntoTemplate = If(txtAsuntoTemplate.Text, "").Trim()
        be.BodyHtmlTemplate = If(txtBodyHtmlTemplate.Text, "").Trim()
        be.UsaLayoutComun = chkUsaLayoutComun.Checked
        be.VersionPlantilla = If(String.IsNullOrWhiteSpace(txtVersionPlantilla.Text), 1, Convert.ToInt32(txtVersionPlantilla.EditValue))
        be.Activo = chkActivo.Checked

        If be.IdPlantilla = 0 Then
            be.FechaCreacion = Date.Now
            be.UsuarioCreacion = Environment.UserName
        End If

        be.FechaModificacion = Date.Now
        be.UsuarioModificacion = Environment.UserName

        Return be
    End Function

    Private Sub CargarDetalle(ByVal pIdPlantilla As Integer)
        If pIdPlantilla <= 0 Then Exit Sub

        Dim be As New clsBENotificacionPlantilla With {.IdPlantilla = pIdPlantilla}

        If clsLnNotificacionPlantilla.Obtener(be) Then
            _be = be
            _esNuevo = False

            txtIdPlantilla.EditValue = be.IdPlantilla
            lueEvento.EditValue = If(be.IdEvento = 0, CType(Nothing, Object), be.IdEvento)
            lueLayout.EditValue = If(be.IdLayout = 0, CType(Nothing, Object), be.IdLayout)
            txtCodigoPlantilla.EditValue = be.CodigoPlantilla
            txtNombrePlantilla.EditValue = be.NombrePlantilla
            txtCanal.EditValue = be.Canal
            txtVersionPlantilla.EditValue = be.VersionPlantilla
            txtAsuntoTemplate.EditValue = be.AsuntoTemplate
            txtBodyHtmlTemplate.EditValue = be.BodyHtmlTemplate
            chkUsaLayoutComun.Checked = be.UsaLayoutComun
            chkActivo.Checked = be.Activo

            User_agrTextEdit.EditValue = be.UsuarioCreacion
            Fec_agrDateEdit.EditValue = If(be.FechaCreacion = Date.MinValue, CType(Nothing, Object), be.FechaCreacion)
            User_modTextEdit.EditValue = be.UsuarioModificacion
            Fec_modDateEdit.EditValue = If(be.FechaModificacion = Date.MinValue, CType(Nothing, Object), be.FechaModificacion)

            AplicarEstadoLayout()
        End If
    End Sub

    Private Sub Guardar()
        If Not _dxValidationProvider.Validate() Then Exit Sub

        Dim be = ConstruirEntidadDesdePantalla()

        If be.IdEvento <= 0 Then Throw New Exception("Debe seleccionar un evento.")
        If String.IsNullOrWhiteSpace(be.CodigoPlantilla) Then Throw New Exception("El código de la plantilla es obligatorio.")
        If String.IsNullOrWhiteSpace(be.NombrePlantilla) Then Throw New Exception("El nombre de la plantilla es obligatorio.")
        If String.IsNullOrWhiteSpace(be.Canal) Then Throw New Exception("El canal es obligatorio.")
        If be.VersionPlantilla <= 0 Then Throw New Exception("La versión debe ser mayor que cero.")
        If be.UsaLayoutComun AndAlso be.IdLayout <= 0 Then Throw New Exception("Debe seleccionar un layout cuando Usa Layout Común está marcado.")
        If clsLnNotificacionPlantilla.ExisteCodigo(be.CodigoPlantilla, be.IdPlantilla) Then Throw New Exception("Ya existe una plantilla con ese código.")

        If _esNuevo OrElse be.IdPlantilla = 0 Then
            clsLnNotificacionPlantilla.Insertar(be)
            XtraMessageBox.Show("Registro creado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            clsLnNotificacionPlantilla.Actualizar(be)
            XtraMessageBox.Show("Registro actualizado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        CargarPlantillas()
        EnfocarRegistro(be.IdPlantilla)
    End Sub

    Private Sub Eliminar()
        If _esNuevo OrElse String.IsNullOrWhiteSpace(txtIdPlantilla.Text) Then
            XtraMessageBox.Show("Seleccione un registro existente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim idPlantilla As Integer = Convert.ToInt32(txtIdPlantilla.EditValue)

        Dim r = XtraMessageBox.Show(
            String.Format("¿Desea eliminar la plantilla {0}?", txtCodigoPlantilla.Text),
            "Confirmación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If r <> DialogResult.Yes Then Exit Sub

        Dim be As New clsBENotificacionPlantilla With {.IdPlantilla = idPlantilla}
        clsLnNotificacionPlantilla.Eliminar(be)

        XtraMessageBox.Show("Registro eliminado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)

        CargarPlantillas()
        PrepararNuevo()
    End Sub

    Private Sub EnfocarRegistro(ByVal pIdPlantilla As Integer)
        If pIdPlantilla <= 0 Then Exit Sub

        For i As Integer = 0 To gvPlantillas.RowCount - 1
            Dim valor As Object = gvPlantillas.GetRowCellValue(i, "IdPlantilla")
            If valor IsNot Nothing AndAlso Not IsDBNull(valor) AndAlso Convert.ToInt32(valor) = pIdPlantilla Then
                gvPlantillas.FocusedRowHandle = i
                gvPlantillas.MakeRowVisible(i)
                CargarDetalle(pIdPlantilla)
                Exit Sub
            End If
        Next
    End Sub

    Private Sub AplicarFiltro()
        If _dtPlantillas Is Nothing Then Exit Sub

        Dim texto As String = If(txtBuscar.Text, "").Trim().Replace("'", "''")
        Dim dv As New DataView(_dtPlantillas)

        If String.IsNullOrWhiteSpace(texto) Then
            dv.RowFilter = ""
        Else
            dv.RowFilter = String.Format("CodigoPlantilla LIKE '%{0}%' OR NombrePlantilla LIKE '%{0}%'", texto)
        End If

        gcPlantillas.DataSource = dv
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
            Dim idActual As Integer = If(String.IsNullOrWhiteSpace(txtIdPlantilla.Text), 0, Convert.ToInt32(txtIdPlantilla.EditValue))
            CargarCombos()
            CargarPlantillas()
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

    Private Sub gvPlantillas_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        If e.FocusedRowHandle < 0 Then Exit Sub
        Dim valor As Object = gvPlantillas.GetRowCellValue(e.FocusedRowHandle, "IdPlantilla")
        If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub
        CargarDetalle(Convert.ToInt32(valor))
    End Sub

    Private Sub gvPlantillas_DoubleClick(sender As Object, e As EventArgs)
        If gvPlantillas.FocusedRowHandle < 0 Then Exit Sub
        Dim valor As Object = gvPlantillas.GetRowCellValue(gvPlantillas.FocusedRowHandle, "IdPlantilla")
        If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub
        CargarDetalle(Convert.ToInt32(valor))
    End Sub

    Private Sub chkUsaLayoutComun_CheckedChanged(sender As Object, e As EventArgs)
        AplicarEstadoLayout()
    End Sub

End Class