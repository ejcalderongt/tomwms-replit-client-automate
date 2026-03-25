Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FrmNotificacionEventoVariableMnt

    Private _be As clsBENotificacionEventoVariable
    Private _dtVariables As DataTable
    Private _dtEventos As DataTable
    Private _esNuevo As Boolean = True
    Private _dxValidationProvider As DXValidationProvider

    Private Sub FrmNotificacionEventoVariableMnt_Load(sender As Object, e As EventArgs) Handles Me.Load
        _dxValidationProvider = New DXValidationProvider()
        ConfigurarValidaciones()
        ConfigurarEventosUI()
        CargarCombos()
        CargarVariables()
        PrepararNuevo()
    End Sub

    Private Sub ConfigurarEventosUI()
        AddHandler mnuNuevo.ItemClick, AddressOf mnuNuevo_ItemClick
        AddHandler mnuGuardar.ItemClick, AddressOf mnuGuardar_ItemClick
        AddHandler mnuEliminar.ItemClick, AddressOf mnuEliminar_ItemClick
        AddHandler mnuRefrescar.ItemClick, AddressOf mnuRefrescar_ItemClick
        AddHandler txtBuscar.TextChanged, AddressOf txtBuscar_TextChanged
        AddHandler gvVariables.FocusedRowChanged, AddressOf gvVariables_FocusedRowChanged
        AddHandler gvVariables.DoubleClick, AddressOf gvVariables_DoubleClick
    End Sub

    Private Sub ConfigurarValidaciones()
        Dim reglaEvento As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "Debe seleccionar un evento."
        }
        _dxValidationProvider.SetValidationRule(lueEvento, reglaEvento)

        Dim reglaVariable As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El nombre de la variable es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(txtNombreVariable, reglaVariable)
    End Sub

    Private Sub CargarCombos()
        _dtEventos = clsLnNotificacionEvento.Listar()

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
    End Sub

    Private Sub CargarVariables()
        _dtVariables = clsLnNotificacionEventoVariable.Listar()

        If _dtVariables Is Nothing Then
            _dtVariables = New DataTable()
        End If

        gcVariables.DataSource = Nothing
        gcVariables.DataSource = _dtVariables

        ConfigurarColumnasGrid()
    End Sub

    Private Sub ConfigurarColumnasGrid()
        If gvVariables.Columns("IdEventoVariable") IsNot Nothing Then gvVariables.Columns("IdEventoVariable").Caption = "Id"
        If gvVariables.Columns("IdEvento") IsNot Nothing Then gvVariables.Columns("IdEvento").Caption = "Id Evento"
        If gvVariables.Columns("NombreVariable") IsNot Nothing Then gvVariables.Columns("NombreVariable").Caption = "Variable"
        If gvVariables.Columns("Descripcion") IsNot Nothing Then gvVariables.Columns("Descripcion").Caption = "Descripción"
        If gvVariables.Columns("EjemploValor") IsNot Nothing Then gvVariables.Columns("EjemploValor").Caption = "Ejemplo"
        If gvVariables.Columns("Obligatoria") IsNot Nothing Then gvVariables.Columns("Obligatoria").Caption = "Obligatoria"
        If gvVariables.Columns("Activo") IsNot Nothing Then gvVariables.Columns("Activo").Caption = "Activo"

        gvVariables.BestFitColumns()
    End Sub

    Private Sub PrepararNuevo()
        _esNuevo = True
        _be = New clsBENotificacionEventoVariable()

        txtIdEventoVariable.EditValue = Nothing
        lueEvento.EditValue = Nothing
        txtNombreVariable.EditValue = Nothing
        txtDescripcion.EditValue = Nothing
        txtEjemploValor.EditValue = Nothing
        chkObligatoria.Checked = False
        chkActivo.Checked = True

        txtNombreVariable.Focus()
    End Sub

    Private Function ConstruirEntidadDesdePantalla() As clsBENotificacionEventoVariable
        Dim be As clsBENotificacionEventoVariable = If(_be Is Nothing, New clsBENotificacionEventoVariable(), _be)

        be.IdEventoVariable = If(String.IsNullOrWhiteSpace(txtIdEventoVariable.Text), 0, Convert.ToInt32(txtIdEventoVariable.EditValue))
        be.IdEvento = If(lueEvento.EditValue Is Nothing, 0, Convert.ToInt32(lueEvento.EditValue))
        be.NombreVariable = If(txtNombreVariable.Text, "").Trim()
        be.Descripcion = If(txtDescripcion.Text, "").Trim()
        be.EjemploValor = If(txtEjemploValor.Text, "").Trim()
        be.Obligatoria = chkObligatoria.Checked
        be.Activo = chkActivo.Checked

        Return be
    End Function

    Private Sub CargarDetalle(ByVal pIdEventoVariable As Integer)
        If pIdEventoVariable <= 0 Then Exit Sub

        Dim be As New clsBENotificacionEventoVariable With {
            .IdEventoVariable = pIdEventoVariable
        }

        If clsLnNotificacionEventoVariable.Obtener(be) Then
            _be = be
            _esNuevo = False

            txtIdEventoVariable.EditValue = be.IdEventoVariable
            lueEvento.EditValue = If(be.IdEvento = 0, CType(Nothing, Object), be.IdEvento)
            txtNombreVariable.EditValue = be.NombreVariable
            txtDescripcion.EditValue = be.Descripcion
            txtEjemploValor.EditValue = be.EjemploValor
            chkObligatoria.Checked = be.Obligatoria
            chkActivo.Checked = be.Activo
        End If
    End Sub

    Private Sub Guardar()
        If Not _dxValidationProvider.Validate() Then Exit Sub

        Dim be = ConstruirEntidadDesdePantalla()

        If be.IdEvento <= 0 Then Throw New Exception("Debe seleccionar un evento.")
        If String.IsNullOrWhiteSpace(be.NombreVariable) Then Throw New Exception("El nombre de la variable es obligatorio.")
        If clsLnNotificacionEventoVariable.ExisteVariable(be.IdEvento, be.NombreVariable, be.IdEventoVariable) Then Throw New Exception("Ya existe esa variable para el evento seleccionado.")

        If _esNuevo OrElse be.IdEventoVariable = 0 Then
            clsLnNotificacionEventoVariable.Insertar(be)
            XtraMessageBox.Show("Registro creado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            clsLnNotificacionEventoVariable.Actualizar(be)
            XtraMessageBox.Show("Registro actualizado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        CargarVariables()
        EnfocarRegistro(be.IdEventoVariable)
    End Sub

    Private Sub Eliminar()
        If _esNuevo OrElse String.IsNullOrWhiteSpace(txtIdEventoVariable.Text) Then
            XtraMessageBox.Show("Seleccione un registro existente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim idVariable As Integer = Convert.ToInt32(txtIdEventoVariable.EditValue)

        Dim r = XtraMessageBox.Show(
            String.Format("¿Desea eliminar la variable {0}?", txtNombreVariable.Text),
            "Confirmación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If r <> DialogResult.Yes Then Exit Sub

        Dim be As New clsBENotificacionEventoVariable With {.IdEventoVariable = idVariable}
        clsLnNotificacionEventoVariable.Eliminar(be)

        XtraMessageBox.Show("Registro eliminado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)

        CargarVariables()
        PrepararNuevo()
    End Sub

    Private Sub EnfocarRegistro(ByVal pIdEventoVariable As Integer)
        If pIdEventoVariable <= 0 Then Exit Sub

        For i As Integer = 0 To gvVariables.RowCount - 1
            Dim valor As Object = gvVariables.GetRowCellValue(i, "IdEventoVariable")
            If valor IsNot Nothing AndAlso Not IsDBNull(valor) AndAlso Convert.ToInt32(valor) = pIdEventoVariable Then
                gvVariables.FocusedRowHandle = i
                gvVariables.MakeRowVisible(i)
                CargarDetalle(pIdEventoVariable)
                Exit Sub
            End If
        Next
    End Sub

    Private Sub AplicarFiltro()
        If _dtVariables Is Nothing Then Exit Sub

        Dim texto As String = If(txtBuscar.Text, "").Trim().Replace("'", "''")
        Dim dv As New DataView(_dtVariables)

        If String.IsNullOrWhiteSpace(texto) Then
            dv.RowFilter = ""
        Else
            dv.RowFilter = String.Format(
                "Convert(IdEvento, 'System.String') LIKE '%{0}%' OR NombreVariable LIKE '%{0}%' OR Descripcion LIKE '%{0}%'", texto)
        End If

        gcVariables.DataSource = dv
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
            Dim idActual As Integer = If(String.IsNullOrWhiteSpace(txtIdEventoVariable.Text), 0, Convert.ToInt32(txtIdEventoVariable.EditValue))
            CargarCombos()
            CargarVariables()

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

    Private Sub gvVariables_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        If e.FocusedRowHandle < 0 Then Exit Sub

        Dim valor As Object = gvVariables.GetRowCellValue(e.FocusedRowHandle, "IdEventoVariable")
        If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub

        CargarDetalle(Convert.ToInt32(valor))
    End Sub

    Private Sub gvVariables_DoubleClick(sender As Object, e As EventArgs)
        If gvVariables.FocusedRowHandle < 0 Then Exit Sub

        Dim valor As Object = gvVariables.GetRowCellValue(gvVariables.FocusedRowHandle, "IdEventoVariable")
        If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub

        CargarDetalle(Convert.ToInt32(valor))
    End Sub

End Class