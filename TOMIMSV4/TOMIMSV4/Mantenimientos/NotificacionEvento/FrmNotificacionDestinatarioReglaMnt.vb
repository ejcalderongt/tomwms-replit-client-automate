Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider
Public Class FrmNotificacionDestinatarioReglaMnt

    Private ReadOnly clsADNotificacionDestinatarioRegla As New clsLnNotificacionDestinatarioRegla()
    Private ReadOnly clsADNotificacionEvento As New clsLnNotificacionEvento()

    Private _be As clsBENotificacionDestinatarioRegla
    Private _dtReglas As DataTable
    Private _dtEventos As DataTable
    Private _esNuevo As Boolean = True
    Private _dxValidationProvider As DXValidationProvider

    Private Sub FrmNotificacionDestinatarioReglaMnt_Load(sender As Object, e As EventArgs) Handles Me.Load
        _dxValidationProvider = New DXValidationProvider()
        ConfigurarValidaciones()
        ConfigurarEventosUI()
        CargarCombos()
        CargarReglas()
        PrepararNuevo()
    End Sub

    Private Sub ConfigurarEventosUI()
        AddHandler mnuNuevo.ItemClick, AddressOf mnuNuevo_ItemClick
        AddHandler mnuGuardar.ItemClick, AddressOf mnuGuardar_ItemClick
        AddHandler mnuEliminar.ItemClick, AddressOf mnuEliminar_ItemClick
        AddHandler mnuRefrescar.ItemClick, AddressOf mnuRefrescar_ItemClick
        AddHandler txtBuscar.TextChanged, AddressOf txtBuscar_TextChanged
        AddHandler gvReglas.FocusedRowChanged, AddressOf gvReglas_FocusedRowChanged
        AddHandler gvReglas.DoubleClick, AddressOf gvReglas_DoubleClick
        AddHandler cboOrigenDestinatario.SelectedIndexChanged, AddressOf cboOrigenDestinatario_SelectedIndexChanged
    End Sub

    Private Sub ConfigurarValidaciones()
        Dim reglaEvento As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "Debe seleccionar un evento."
        }
        _dxValidationProvider.SetValidationRule(lueEvento, reglaEvento)

        Dim reglaTipo As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El tipo de destinatario es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(cboTipoDestinatario, reglaTipo)

        Dim reglaOrigen As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El origen del destinatario es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(cboOrigenDestinatario, reglaOrigen)

        Dim reglaValor As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El valor origen es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(txtValorOrigen, reglaValor)
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

    Private Sub CargarReglas()
        _dtReglas = clsLnNotificacionDestinatarioRegla.Listar()

        If _dtReglas Is Nothing Then
            _dtReglas = New DataTable()
        End If

        gcReglas.DataSource = Nothing
        gcReglas.DataSource = _dtReglas

        ConfigurarColumnasGrid()
    End Sub

    Private Sub ConfigurarColumnasGrid()
        If gvReglas.Columns("IdReglaDestinatario") IsNot Nothing Then gvReglas.Columns("IdReglaDestinatario").Caption = "Id"
        If gvReglas.Columns("IdEvento") IsNot Nothing Then gvReglas.Columns("IdEvento").Caption = "Evento"
        If gvReglas.Columns("TipoDestinatario") IsNot Nothing Then gvReglas.Columns("TipoDestinatario").Caption = "Tipo"
        If gvReglas.Columns("OrigenDestinatario") IsNot Nothing Then gvReglas.Columns("OrigenDestinatario").Caption = "Origen"
        If gvReglas.Columns("ValorOrigen") IsNot Nothing Then gvReglas.Columns("ValorOrigen").Caption = "Valor"
        If gvReglas.Columns("EmpresaCodigo") IsNot Nothing Then gvReglas.Columns("EmpresaCodigo").Caption = "Empresa"
        If gvReglas.Columns("SucursalCodigo") IsNot Nothing Then gvReglas.Columns("SucursalCodigo").Caption = "Sucursal"
        If gvReglas.Columns("TipoDocumento") IsNot Nothing Then gvReglas.Columns("TipoDocumento").Caption = "Tipo Doc."
        If gvReglas.Columns("ContextoBodega") IsNot Nothing Then gvReglas.Columns("ContextoBodega").Caption = "Contexto"
        If gvReglas.Columns("CodigoBodegaFiltro") IsNot Nothing Then gvReglas.Columns("CodigoBodegaFiltro").Caption = "Bodega Filtro"
        If gvReglas.Columns("Prioridad") IsNot Nothing Then gvReglas.Columns("Prioridad").Caption = "Prioridad"
        If gvReglas.Columns("Activo") IsNot Nothing Then gvReglas.Columns("Activo").Caption = "Activo"

        Dim ocultas = {"RequiereCoincidenciaExacta", "Observaciones", "FechaCreacion", "UsuarioCreacion", "FechaModificacion", "UsuarioModificacion"}
        For Each nombre In ocultas
            If gvReglas.Columns(nombre) IsNot Nothing Then gvReglas.Columns(nombre).Visible = False
        Next

        gvReglas.BestFitColumns()
    End Sub

    Private Sub PrepararNuevo()
        _esNuevo = True
        _be = New clsBENotificacionDestinatarioRegla()

        txtIdReglaDestinatario.EditValue = Nothing
        lueEvento.EditValue = Nothing
        cboTipoDestinatario.EditValue = Nothing
        cboOrigenDestinatario.EditValue = Nothing
        txtValorOrigen.EditValue = Nothing
        txtEmpresaCodigo.EditValue = Nothing
        txtSucursalCodigo.EditValue = Nothing
        txtTipoDocumento.EditValue = Nothing
        cboContextoBodega.EditValue = ""
        txtCodigoBodegaFiltro.EditValue = Nothing
        spnPrioridad.EditValue = 1
        chkRequiereCoincidenciaExacta.Checked = False
        chkActivo.Checked = True
        txtObservaciones.EditValue = Nothing

        User_agrTextEdit.EditValue = Nothing
        Fec_agrDateEdit.EditValue = Nothing
        User_modTextEdit.EditValue = Nothing
        Fec_modDateEdit.EditValue = Nothing

        AplicarReglasPantalla()
        lueEvento.Focus()
    End Sub

    Private Sub AplicarReglasPantalla()
        Dim origen As String = If(cboOrigenDestinatario.Text, "").Trim().ToUpperInvariant()
        Dim esBodega As Boolean = (origen = "BODEGA")

        cboContextoBodega.Enabled = esBodega
        txtCodigoBodegaFiltro.Enabled = esBodega

        If Not esBodega Then
            cboContextoBodega.EditValue = ""
            txtCodigoBodegaFiltro.EditValue = Nothing
        End If
    End Sub

    Private Function ConstruirEntidadDesdePantalla() As clsBENotificacionDestinatarioRegla
        Dim be As clsBENotificacionDestinatarioRegla = If(_be Is Nothing, New clsBENotificacionDestinatarioRegla(), _be)

        be.IdReglaDestinatario = If(String.IsNullOrWhiteSpace(txtIdReglaDestinatario.Text), 0, Convert.ToInt32(txtIdReglaDestinatario.EditValue))
        be.IdEvento = If(lueEvento.EditValue Is Nothing, 0, Convert.ToInt32(lueEvento.EditValue))
        be.TipoDestinatario = If(cboTipoDestinatario.Text, "").Trim().ToUpperInvariant()
        be.OrigenDestinatario = If(cboOrigenDestinatario.Text, "").Trim().ToUpperInvariant()
        be.ValorOrigen = If(txtValorOrigen.Text, "").Trim()
        be.EmpresaCodigo = If(txtEmpresaCodigo.Text, "").Trim().ToUpperInvariant()
        be.SucursalCodigo = If(txtSucursalCodigo.Text, "").Trim().ToUpperInvariant()
        be.TipoDocumento = If(txtTipoDocumento.Text, "").Trim().ToUpperInvariant()
        be.ContextoBodega = If(cboContextoBodega.Text, "").Trim().ToUpperInvariant()
        be.CodigoBodegaFiltro = If(txtCodigoBodegaFiltro.Text, "").Trim().ToUpperInvariant()
        be.Prioridad = Convert.ToInt32(spnPrioridad.EditValue)
        be.RequiereCoincidenciaExacta = chkRequiereCoincidenciaExacta.Checked
        be.Activo = chkActivo.Checked
        be.Observaciones = If(txtObservaciones.Text, "").Trim()

        If be.IdReglaDestinatario = 0 Then
            be.FechaCreacion = Date.Now
            be.UsuarioCreacion = Environment.UserName
        End If

        be.FechaModificacion = Date.Now
        be.UsuarioModificacion = Environment.UserName

        Return be
    End Function

    Private Sub CargarDetalle(ByVal pIdReglaDestinatario As Integer)
        If pIdReglaDestinatario <= 0 Then Exit Sub

        Dim be As New clsBENotificacionDestinatarioRegla With {
            .IdReglaDestinatario = pIdReglaDestinatario
        }

        If clsLnNotificacionDestinatarioRegla.Obtener(be) Then
            _be = be
            _esNuevo = False

            txtIdReglaDestinatario.EditValue = be.IdReglaDestinatario
            lueEvento.EditValue = If(be.IdEvento = 0, CType(Nothing, Object), be.IdEvento)
            cboTipoDestinatario.EditValue = be.TipoDestinatario
            cboOrigenDestinatario.EditValue = be.OrigenDestinatario
            txtValorOrigen.EditValue = be.ValorOrigen
            txtEmpresaCodigo.EditValue = be.EmpresaCodigo
            txtSucursalCodigo.EditValue = be.SucursalCodigo
            txtTipoDocumento.EditValue = be.TipoDocumento
            cboContextoBodega.EditValue = be.ContextoBodega
            txtCodigoBodegaFiltro.EditValue = be.CodigoBodegaFiltro
            spnPrioridad.EditValue = If(be.Prioridad <= 0, 1, be.Prioridad)
            chkRequiereCoincidenciaExacta.Checked = be.RequiereCoincidenciaExacta
            chkActivo.Checked = be.Activo
            txtObservaciones.EditValue = be.Observaciones

            User_agrTextEdit.EditValue = be.UsuarioCreacion
            Fec_agrDateEdit.EditValue = If(be.FechaCreacion = Date.MinValue, CType(Nothing, Object), be.FechaCreacion)
            User_modTextEdit.EditValue = be.UsuarioModificacion
            Fec_modDateEdit.EditValue = If(be.FechaModificacion = Date.MinValue, CType(Nothing, Object), be.FechaModificacion)

            AplicarReglasPantalla()
        End If
    End Sub

    Private Sub Guardar()
        If Not _dxValidationProvider.Validate() Then Exit Sub

        Dim be = ConstruirEntidadDesdePantalla()

        If be.IdEvento <= 0 Then Throw New Exception("Debe seleccionar un evento.")
        If String.IsNullOrWhiteSpace(be.TipoDestinatario) Then Throw New Exception("El tipo de destinatario es obligatorio.")
        If String.IsNullOrWhiteSpace(be.OrigenDestinatario) Then Throw New Exception("El origen del destinatario es obligatorio.")
        If String.IsNullOrWhiteSpace(be.ValorOrigen) Then Throw New Exception("El valor origen es obligatorio.")
        If be.Prioridad <= 0 Then Throw New Exception("La prioridad debe ser mayor que cero.")

        If be.OrigenDestinatario = "BODEGA" AndAlso String.IsNullOrWhiteSpace(be.ContextoBodega) Then
            Throw New Exception("Cuando el origen es BODEGA, el contexto de bodega es obligatorio.")
        End If

        If be.OrigenDestinatario <> "BODEGA" Then
            be.ContextoBodega = ""
            be.CodigoBodegaFiltro = ""
        End If

        If clsLnNotificacionDestinatarioRegla.ExisteRegla(be.IdEvento, be.TipoDestinatario, be.OrigenDestinatario, be.ValorOrigen, be.EmpresaCodigo, be.SucursalCodigo, be.TipoDocumento, be.ContextoBodega, be.CodigoBodegaFiltro, be.IdReglaDestinatario) Then
            Throw New Exception("Ya existe una regla con esa combinación.")
        End If

        If _esNuevo OrElse be.IdReglaDestinatario = 0 Then
            clsLnNotificacionDestinatarioRegla.Insertar(be)
            XtraMessageBox.Show("Registro creado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            clsLnNotificacionDestinatarioRegla.Actualizar(be)
            XtraMessageBox.Show("Registro actualizado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        CargarReglas()
        EnfocarRegistro(be.IdReglaDestinatario)
    End Sub

    Private Sub Eliminar()
        If _esNuevo OrElse String.IsNullOrWhiteSpace(txtIdReglaDestinatario.Text) Then
            XtraMessageBox.Show("Seleccione un registro existente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim idRegla As Integer = Convert.ToInt32(txtIdReglaDestinatario.EditValue)

        Dim r = XtraMessageBox.Show(
            String.Format("¿Desea eliminar la regla {0} - {1}?", cboTipoDestinatario.Text, txtValorOrigen.Text),
            "Confirmación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If r <> DialogResult.Yes Then Exit Sub

        Dim be As New clsBENotificacionDestinatarioRegla With {.IdReglaDestinatario = idRegla}
        clsLnNotificacionDestinatarioRegla.Eliminar(be)

        XtraMessageBox.Show("Registro eliminado correctamente.", "Notificaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)

        CargarReglas()
        PrepararNuevo()
    End Sub

    Private Sub EnfocarRegistro(ByVal pIdReglaDestinatario As Integer)
        If pIdReglaDestinatario <= 0 Then Exit Sub

        For i As Integer = 0 To gvReglas.RowCount - 1
            Dim valor As Object = gvReglas.GetRowCellValue(i, "IdReglaDestinatario")
            If valor IsNot Nothing AndAlso Not IsDBNull(valor) AndAlso Convert.ToInt32(valor) = pIdReglaDestinatario Then
                gvReglas.FocusedRowHandle = i
                gvReglas.MakeRowVisible(i)
                CargarDetalle(pIdReglaDestinatario)
                Exit Sub
            End If
        Next
    End Sub

    Private Sub AplicarFiltro()
        If _dtReglas Is Nothing Then Exit Sub

        Dim texto As String = If(txtBuscar.Text, "").Trim().Replace("'", "''")
        Dim dv As New DataView(_dtReglas)

        If String.IsNullOrWhiteSpace(texto) Then
            dv.RowFilter = ""
        Else
            dv.RowFilter = String.Format(
                "Convert(IdEvento, 'System.String') LIKE '%{0}%' OR TipoDestinatario LIKE '%{0}%' OR OrigenDestinatario LIKE '%{0}%' OR ValorOrigen LIKE '%{0}%' OR EmpresaCodigo LIKE '%{0}%' OR SucursalCodigo LIKE '%{0}%' OR TipoDocumento LIKE '%{0}%'",
                texto)
        End If

        gcReglas.DataSource = dv
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
            Dim idActual As Integer = If(String.IsNullOrWhiteSpace(txtIdReglaDestinatario.Text), 0, Convert.ToInt32(txtIdReglaDestinatario.EditValue))
            CargarCombos()
            CargarReglas()

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

    Private Sub gvReglas_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        If e.FocusedRowHandle < 0 Then Exit Sub

        Dim valor As Object = gvReglas.GetRowCellValue(e.FocusedRowHandle, "IdReglaDestinatario")
        If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub

        CargarDetalle(Convert.ToInt32(valor))
    End Sub

    Private Sub gvReglas_DoubleClick(sender As Object, e As EventArgs)
        If gvReglas.FocusedRowHandle < 0 Then Exit Sub

        Dim valor As Object = gvReglas.GetRowCellValue(gvReglas.FocusedRowHandle, "IdReglaDestinatario")
        If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub

        CargarDetalle(Convert.ToInt32(valor))
    End Sub

    Private Sub cboOrigenDestinatario_SelectedIndexChanged(sender As Object, e As EventArgs)
        AplicarReglasPantalla()
    End Sub

End Class