Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FrmNotificacionLayoutMnt

    Private _be As clsBENotificacionLayout
    Private _dtLayouts As DataTable
    Private _esNuevo As Boolean = True
    Private _dxValidationProvider As DXValidationProvider

    Private Sub FrmNotificacionLayoutMnt_Load(sender As Object, e As EventArgs) Handles Me.Load
        _dxValidationProvider = New DXValidationProvider()
        ConfigurarValidaciones()
        ConfigurarEventosUI()
        CargarLayouts()
        PrepararNuevo()
    End Sub

    Private Sub ConfigurarEventosUI()
        AddHandler mnuNuevo.ItemClick, AddressOf mnuNuevo_ItemClick
        AddHandler mnuGuardar.ItemClick, AddressOf mnuGuardar_ItemClick
        AddHandler mnuEliminar.ItemClick, AddressOf mnuEliminar_ItemClick
        AddHandler mnuRefrescar.ItemClick, AddressOf mnuRefrescar_ItemClick

        AddHandler txtBuscar.TextChanged, AddressOf txtBuscar_TextChanged
        AddHandler gvLayouts.FocusedRowChanged, AddressOf gvLayouts_FocusedRowChanged
        AddHandler gvLayouts.DoubleClick, AddressOf gvLayouts_DoubleClick
    End Sub

    Private Sub ConfigurarValidaciones()
        Dim reglaCodigo As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El código del layout es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(txtCodigoLayout, reglaCodigo)

        Dim reglaNombre As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El nombre del layout es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(txtNombreLayout, reglaNombre)
    End Sub

    Private Sub CargarLayouts()
        _dtLayouts = clsLnNotificacionLayout.Listar()

        If _dtLayouts Is Nothing Then
            _dtLayouts = New DataTable()
        End If

        gcLayouts.DataSource = Nothing
        gcLayouts.DataSource = _dtLayouts

        ConfigurarColumnasGrid()
    End Sub

    Private Sub ConfigurarColumnasGrid()
        If gvLayouts.Columns("IdLayout") IsNot Nothing Then gvLayouts.Columns("IdLayout").Caption = "Id"
        If gvLayouts.Columns("CodigoLayout") IsNot Nothing Then gvLayouts.Columns("CodigoLayout").Caption = "Código"
        If gvLayouts.Columns("NombreLayout") IsNot Nothing Then gvLayouts.Columns("NombreLayout").Caption = "Nombre"
        If gvLayouts.Columns("Activo") IsNot Nothing Then gvLayouts.Columns("Activo").Caption = "Activo"
        If gvLayouts.Columns("EsDefault") IsNot Nothing Then gvLayouts.Columns("EsDefault").Caption = "Default"

        If gvLayouts.Columns("HeaderHtml") IsNot Nothing Then gvLayouts.Columns("HeaderHtml").Visible = False
        If gvLayouts.Columns("FooterHtml") IsNot Nothing Then gvLayouts.Columns("FooterHtml").Visible = False
        If gvLayouts.Columns("CssInline") IsNot Nothing Then gvLayouts.Columns("CssInline").Visible = False
        If gvLayouts.Columns("FechaCreacion") IsNot Nothing Then gvLayouts.Columns("FechaCreacion").Visible = False
        If gvLayouts.Columns("UsuarioCreacion") IsNot Nothing Then gvLayouts.Columns("UsuarioCreacion").Visible = False
        If gvLayouts.Columns("FechaModificacion") IsNot Nothing Then gvLayouts.Columns("FechaModificacion").Visible = False
        If gvLayouts.Columns("UsuarioModificacion") IsNot Nothing Then gvLayouts.Columns("UsuarioModificacion").Visible = False

        gvLayouts.BestFitColumns()
    End Sub

    Private Sub PrepararNuevo()
        _esNuevo = True
        _be = New clsBENotificacionLayout()

        txtIdLayout.EditValue = Nothing
        txtCodigoLayout.EditValue = Nothing
        txtNombreLayout.EditValue = Nothing
        txtHeaderHtml.EditValue = Nothing
        txtFooterHtml.EditValue = Nothing
        txtCssInline.EditValue = Nothing
        chkActivo.Checked = True
        chkEsDefault.Checked = False

        User_agrTextEdit.EditValue = Nothing
        Fec_agrDateEdit.EditValue = Nothing
        User_modTextEdit.EditValue = Nothing
        Fec_modDateEdit.EditValue = Nothing

        txtCodigoLayout.Focus()
    End Sub

    Private Function ConstruirEntidadDesdePantalla() As clsBENotificacionLayout
        Dim be As clsBENotificacionLayout

        If _be Is Nothing Then
            be = New clsBENotificacionLayout()
        Else
            be = _be
        End If

        be.IdLayout = If(String.IsNullOrWhiteSpace(txtIdLayout.Text), 0, Convert.ToInt32(txtIdLayout.EditValue))
        be.CodigoLayout = If(txtCodigoLayout.Text, "").Trim().ToUpperInvariant()
        be.NombreLayout = If(txtNombreLayout.Text, "").Trim()
        be.HeaderHtml = If(txtHeaderHtml.Text, "").Trim()
        be.FooterHtml = If(txtFooterHtml.Text, "").Trim()
        be.CssInline = If(txtCssInline.Text, "").Trim()
        be.Activo = chkActivo.Checked
        be.EsDefault = chkEsDefault.Checked

        If be.IdLayout = 0 Then
            be.FechaCreacion = Date.Now
            be.UsuarioCreacion = Environment.UserName
        End If

        be.FechaModificacion = Date.Now
        be.UsuarioModificacion = Environment.UserName

        Return be
    End Function

    Private Sub CargarDetalle(ByVal pIdLayout As Integer)
        If pIdLayout <= 0 Then Exit Sub

        Dim be As New clsBENotificacionLayout With {
            .IdLayout = pIdLayout
        }

        If clsLnNotificacionLayout.Obtener(be) Then
            _be = be
            _esNuevo = False

            txtIdLayout.EditValue = be.IdLayout
            txtCodigoLayout.EditValue = be.CodigoLayout
            txtNombreLayout.EditValue = be.NombreLayout
            txtHeaderHtml.EditValue = be.HeaderHtml
            txtFooterHtml.EditValue = be.FooterHtml
            txtCssInline.EditValue = be.CssInline
            chkActivo.Checked = be.Activo
            chkEsDefault.Checked = be.EsDefault

            User_agrTextEdit.EditValue = be.UsuarioCreacion
            Fec_agrDateEdit.EditValue = If(be.FechaCreacion = Date.MinValue, CType(Nothing, Object), be.FechaCreacion)

            User_modTextEdit.EditValue = be.UsuarioModificacion

            If be.FechaModificacion = Date.MinValue Then
                Fec_modDateEdit.EditValue = Nothing
            Else
                Fec_modDateEdit.EditValue = be.FechaModificacion
            End If
        End If
    End Sub

    Private Sub Guardar()
        If Not _dxValidationProvider.Validate() Then Exit Sub

        Dim be As clsBENotificacionLayout = ConstruirEntidadDesdePantalla()

        If String.IsNullOrWhiteSpace(be.CodigoLayout) Then
            Throw New Exception("El código del layout es obligatorio.")
        End If

        If String.IsNullOrWhiteSpace(be.NombreLayout) Then
            Throw New Exception("El nombre del layout es obligatorio.")
        End If

        If clsLnNotificacionLayout.ExisteCodigo(be.CodigoLayout, be.IdLayout) Then
            Throw New Exception("Ya existe un layout con ese código.")
        End If

        If _esNuevo OrElse be.IdLayout = 0 Then
            clsLnNotificacionLayout.Insertar(be)

            If be.EsDefault Then
                clsLnNotificacionLayout.QuitarDefaultOtros(be.IdLayout)
            End If

            XtraMessageBox.Show("Registro creado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            clsLnNotificacionLayout.Actualizar(be)

            If be.EsDefault Then
                clsLnNotificacionLayout.QuitarDefaultOtros(be.IdLayout)
            End If

            XtraMessageBox.Show("Registro actualizado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        CargarLayouts()
        EnfocarRegistro(be.IdLayout)
    End Sub

    Private Sub Eliminar()
        If _esNuevo OrElse String.IsNullOrWhiteSpace(txtIdLayout.Text) Then
            XtraMessageBox.Show("Seleccione un registro existente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim idLayout As Integer = Convert.ToInt32(txtIdLayout.EditValue)

        Dim r = XtraMessageBox.Show(
            String.Format("¿Desea eliminar el layout {0}?", txtCodigoLayout.Text),
            "Confirmación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If r <> DialogResult.Yes Then Exit Sub

        Dim be As New clsBENotificacionLayout With {
            .idLayout = idLayout
        }

        clsLnNotificacionLayout.Eliminar(be)

        XtraMessageBox.Show("Registro eliminado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)

        CargarLayouts()
        PrepararNuevo()
    End Sub

    Private Sub EnfocarRegistro(ByVal pIdLayout As Integer)
        If pIdLayout <= 0 Then Exit Sub

        For i As Integer = 0 To gvLayouts.RowCount - 1
            Dim valor As Object = gvLayouts.GetRowCellValue(i, "IdLayout")
            If valor IsNot Nothing AndAlso Not IsDBNull(valor) AndAlso Convert.ToInt32(valor) = pIdLayout Then
                gvLayouts.FocusedRowHandle = i
                gvLayouts.MakeRowVisible(i)
                CargarDetalle(pIdLayout)
                Exit Sub
            End If
        Next
    End Sub

    Private Sub AplicarFiltro()
        If _dtLayouts Is Nothing Then Exit Sub

        Dim texto As String = If(txtBuscar.Text, "").Trim().Replace("'", "''")
        Dim dv As New DataView(_dtLayouts)

        If String.IsNullOrWhiteSpace(texto) Then
            dv.RowFilter = ""
        Else
            dv.RowFilter = String.Format(
                "CodigoLayout LIKE '%{0}%' OR NombreLayout LIKE '%{0}%'",
                texto)
        End If

        gcLayouts.DataSource = dv
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
            Dim idActual As Integer = If(String.IsNullOrWhiteSpace(txtIdLayout.Text), 0, Convert.ToInt32(txtIdLayout.EditValue))
            CargarLayouts()

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

    Private Sub gvLayouts_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        If e.FocusedRowHandle < 0 Then Exit Sub

        Dim valor As Object = gvLayouts.GetRowCellValue(e.FocusedRowHandle, "IdLayout")
        If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub

        CargarDetalle(Convert.ToInt32(valor))
    End Sub

    Private Sub gvLayouts_DoubleClick(sender As Object, e As EventArgs)
        If gvLayouts.FocusedRowHandle < 0 Then Exit Sub

        Dim valor As Object = gvLayouts.GetRowCellValue(gvLayouts.FocusedRowHandle, "IdLayout")
        If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub

        CargarDetalle(Convert.ToInt32(valor))
    End Sub

End Class