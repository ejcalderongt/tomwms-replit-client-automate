Imports System.Text.RegularExpressions
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.Utils
Imports DevExpress.Utils.SuperToolTip

Public Class FrmNotificacionContactoBodegaMnt

    Private _be As clsBENotificacionContacto
    Private _dtContactos As DataTable
    Private _dxValidationProvider As DXValidationProvider
    Private _esNuevo As Boolean = True

    Private DT As DataTable
    Private pListObjCB As List(Of clsBENotificacionContactoBodega)
    Private DtContactoBodega As DataTable
    Private Sub CrearEstructuraDtContactoBodega()

        DtContactoBodega = New DataTable("Data")
        DtContactoBodega.Columns.Add("IdBodega", GetType(Integer))
        DtContactoBodega.Columns.Add("CodigoBodega", GetType(String))
        DtContactoBodega.Columns.Add("Bodega", GetType(String))
        DtContactoBodega.Columns.Add("Seleccion", GetType(Boolean))
        DtContactoBodega.Columns.Add("IdContactoBodega", GetType(Integer))
        DtContactoBodega.Columns.Add("IdInterno", GetType(Integer))

    End Sub
    Private Sub FrmNotificacionContactoBodegaMnt_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _dxValidationProvider = New DXValidationProvider()

            CrearEstructuraDtContactoBodega()
            ConfigurarValidaciones()
            ConfigurarEventosUI()
            CargarContactos()
            PrepararNuevo()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

            clsLnLog_error_wms.Agregar_Error(ex.Message)
        End Try
    End Sub

    Private Sub ConfigurarEventosUI()
        AddHandler mnuNuevo.ItemClick, AddressOf mnuNuevo_ItemClick
        AddHandler mnuGuardar.ItemClick, AddressOf mnuGuardar_ItemClick
        AddHandler mnuEliminar.ItemClick, AddressOf mnuEliminar_ItemClick
        AddHandler mnuRefrescar.ItemClick, AddressOf mnuRefrescar_ItemClick

        AddHandler txtBuscarContacto.TextChanged, AddressOf txtBuscarContacto_TextChanged
        AddHandler gvContactos.FocusedRowChanged, AddressOf gvContactos_FocusedRowChanged
        AddHandler gvContactos.DoubleClick, AddressOf gvContactos_DoubleClick
    End Sub

    Private Sub ConfigurarValidaciones()
        Dim reglaNombre As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El nombre es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(txtNombre, reglaNombre)

        Dim reglaCorreo As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El correo es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(txtCorreo, reglaCorreo)

        Dim reglaTipo As New ConditionValidationRule With {
            .ConditionOperator = ConditionOperator.IsNotBlank,
            .ErrorText = "El tipo de contacto es obligatorio."
        }
        _dxValidationProvider.SetValidationRule(cboTipoContacto, reglaTipo)
    End Sub

    Private Sub CargarContactos()
        Try
            _dtContactos = clsLnNotificacionContacto.Listar()

            If _dtContactos Is Nothing Then
                _dtContactos = New DataTable()
            End If

            gcContactos.DataSource = Nothing
            gcContactos.DataSource = _dtContactos

            ConfigurarColumnasContactos()

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ConfigurarColumnasContactos()
        Try
            If gvContactos.Columns("IdContacto") IsNot Nothing Then gvContactos.Columns("IdContacto").Caption = "Id"
            If gvContactos.Columns("Nombre") IsNot Nothing Then gvContactos.Columns("Nombre").Caption = "Nombre"
            If gvContactos.Columns("Correo") IsNot Nothing Then gvContactos.Columns("Correo").Caption = "Correo"
            If gvContactos.Columns("TipoContacto") IsNot Nothing Then gvContactos.Columns("TipoContacto").Caption = "Tipo"
            If gvContactos.Columns("Activo") IsNot Nothing Then gvContactos.Columns("Activo").Caption = "Activo"

            Dim ocultas = {
                "PermiteTo",
                "PermiteCc",
                "PermiteBcc",
                "EsPrincipal",
                "Observaciones",
                "FechaCreacion",
                "UsuarioCreacion",
                "FechaModificacion",
                "UsuarioModificacion"
            }

            For Each nombre In ocultas
                If gvContactos.Columns(nombre) IsNot Nothing Then
                    gvContactos.Columns(nombre).Visible = False
                End If
            Next

            gvContactos.OptionsBehavior.Editable = False
            gvContactos.OptionsBehavior.ReadOnly = True
            gvContactos.OptionsSelection.EnableAppearanceFocusedCell = False
            gvContactos.OptionsView.ShowGroupPanel = False
            gvContactos.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
            gvContactos.BestFitColumns()

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub PrepararNuevo()
        _esNuevo = True
        _be = New clsBENotificacionContacto()

        txtIdContacto.EditValue = Nothing
        txtNombre.EditValue = Nothing
        txtCorreo.EditValue = Nothing
        cboTipoContacto.EditValue = Nothing
        chkPermiteTo.Checked = True
        chkPermiteCc.Checked = True
        chkPermiteBcc.Checked = False
        chkEsPrincipal.Checked = False
        chkActivo.Checked = True
        txtObservaciones.EditValue = Nothing

        ValidaBodegas()

        txtNombre.Focus()
    End Sub

    Private Function ConstruirEntidadDesdePantalla() As clsBENotificacionContacto
        Dim be As clsBENotificacionContacto = If(_be Is Nothing, New clsBENotificacionContacto(), _be)

        be.IdContacto = If(String.IsNullOrWhiteSpace(txtIdContacto.Text), 0, Convert.ToInt32(txtIdContacto.EditValue))
        be.Nombre = If(txtNombre.Text, "").Trim()
        be.Correo = If(txtCorreo.Text, "").Trim().ToLowerInvariant()
        be.TipoContacto = If(cboTipoContacto.Text, "").Trim().ToUpperInvariant()
        be.PermiteTo = chkPermiteTo.Checked
        be.PermiteCc = chkPermiteCc.Checked
        be.PermiteBcc = chkPermiteBcc.Checked
        be.EsPrincipal = chkEsPrincipal.Checked
        be.Activo = chkActivo.Checked
        be.Observaciones = If(txtObservaciones.Text, "").Trim()

        If be.IdContacto = 0 Then
            be.FechaCreacion = Date.Now
            be.UsuarioCreacion = AP.UsuarioAp.Nombres
        End If

        be.FechaModificacion = Date.Now
        be.UsuarioModificacion = AP.UsuarioAp.Nombres

        Return be
    End Function

    Private Sub CargarDetalle(ByVal pIdContacto As Integer)
        Try
            If pIdContacto <= 0 Then Exit Sub

            Dim be As New clsBENotificacionContacto With {.IdContacto = pIdContacto}

            If clsLnNotificacionContacto.Obtener(be) Then
                _be = be
                _esNuevo = False

                txtIdContacto.EditValue = be.IdContacto
                txtNombre.EditValue = be.Nombre
                txtCorreo.EditValue = be.Correo
                cboTipoContacto.EditValue = be.TipoContacto
                chkPermiteTo.Checked = be.PermiteTo
                chkPermiteCc.Checked = be.PermiteCc
                chkPermiteBcc.Checked = be.PermiteBcc
                chkEsPrincipal.Checked = be.EsPrincipal
                chkActivo.Checked = be.Activo
                txtObservaciones.EditValue = be.Observaciones

                ValidaBodegas()
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ValidaBodegas()

        Try
            DtContactoBodega.Clear()
            DT = IMS.Listar_Bodegas()

            If _be IsNot Nothing AndAlso _be.IdContacto <> 0 Then
                pListObjCB = New List(Of clsBENotificacionContactoBodega)
                pListObjCB = clsLnNotificacionContactoBodega.Get_All_Bodegas_By_IdContacto(_be.IdContacto)
            Else
                pListObjCB = New List(Of clsBENotificacionContactoBodega)
            End If

            Lista_Bodegas()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

            clsLnLog_error_wms.Agregar_Error(ex.Message)

        End Try

    End Sub

    Private Sub Lista_Bodegas()

        Try

            gvBodegas.BeginUpdate()

            DtContactoBodega.Clear()

            If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then

                Dim vIdBodega As Integer = 0
                Dim vNombreBodega As String = ""
                Dim vCodigo As String = ""

                For i As Integer = 0 To DT.Rows.Count - 1

                    vIdBodega = CInt(DT.Rows(i)(0))
                    vCodigo = DT.Rows(i)(3)
                    vNombreBodega = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)

                    Dim lRow As DataRow = DtContactoBodega.NewRow()
                    lRow.Item("IdBodega") = vIdBodega
                    lRow.Item("CodigoBodega") = vCodigo.ToString()
                    lRow.Item("Bodega") = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)
                    lRow.Item("Seleccion") = False
                    lRow.Item("IdContactoBodega") = 0
                    lRow.Item("IdInterno") = 0

                    If Not _esNuevo Then

                        If pListObjCB IsNot Nothing AndAlso pListObjCB.Count > 0 Then

                            For Each obj As clsBENotificacionContactoBodega In pListObjCB

                                If obj.IdBodega = vIdBodega AndAlso obj.Activo Then
                                    lRow.Item("Seleccion") = True
                                    lRow.Item("IdContactoBodega") = obj.IdContactoBodega
                                    lRow.Item("IdInterno") = obj.IdContactoBodega
                                    Exit For
                                End If

                            Next

                        End If

                    End If

                    DtContactoBodega.Rows.Add(lRow)

                Next

            End If

            gcBodegas.DataSource = Nothing
            gcBodegas.DataSource = DtContactoBodega

            gvBodegas.EndUpdate()
            gcBodegas.ForceInitialize()

            If gvBodegas.Columns("IdBodega") IsNot Nothing Then
                gvBodegas.Columns("IdBodega").Visible = False
            End If

            If gvBodegas.Columns("Bodega") IsNot Nothing Then
                gvBodegas.Columns("Bodega").Caption = "Bodega"
                gvBodegas.Columns("Bodega").VisibleIndex = 1
            End If

            If gvBodegas.Columns("Seleccion") IsNot Nothing Then
                gvBodegas.Columns("Seleccion").Caption = "Selección"
                gvBodegas.Columns("Seleccion").VisibleIndex = 0
            End If

            If gvBodegas.Columns("IdContactoBodega") IsNot Nothing Then
                gvBodegas.Columns("IdContactoBodega").Visible = False
            End If

            If gvBodegas.Columns("IdInterno") IsNot Nothing Then
                gvBodegas.Columns("IdInterno").Visible = False
            End If

            gvBodegas.OptionsBehavior.Editable = True
            gvBodegas.OptionsView.ShowGroupPanel = False
            gvBodegas.BestFitColumns()

            Dim ritem As RepositoryItemCheckEdit =
            TryCast(gvBodegas.Columns("Seleccion").RealColumnEdit, RepositoryItemCheckEdit)

            If ritem IsNot Nothing Then
                RemoveHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged
                AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged
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

    Private Sub ConfigurarColumnasBodegas()
        Try
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In gvBodegas.Columns
                col.Visible = False
            Next

            If gvBodegas.Columns("Seleccion") IsNot Nothing Then
                gvBodegas.Columns("Seleccion").Visible = True
                gvBodegas.Columns("Seleccion").Caption = "Selección"
                gvBodegas.Columns("Seleccion").VisibleIndex = 0
            End If

            If gvBodegas.Columns("IdBodega") IsNot Nothing Then
                gvBodegas.Columns("IdBodega").Visible = False
            End If

            If gvBodegas.Columns("Bodega") IsNot Nothing Then
                gvBodegas.Columns("Bodega").Visible = True
                gvBodegas.Columns("Bodega").Caption = "Bodega"
                gvBodegas.Columns("Bodega").VisibleIndex = 1
            End If

            If gvBodegas.Columns("CodigoBodega") IsNot Nothing Then
                gvBodegas.Columns("CodigoBodega").Visible = True
                gvBodegas.Columns("CodigoBodega").Caption = "Código"
                gvBodegas.Columns("CodigoBodega").VisibleIndex = 1
            End If

            If gvBodegas.Columns("IdContactoBodega") IsNot Nothing Then gvBodegas.Columns("IdContactoBodega").Visible = False
            If gvBodegas.Columns("IdInterno") IsNot Nothing Then gvBodegas.Columns("IdInterno").Visible = False

            gvBodegas.OptionsBehavior.Editable = True
            gvBodegas.OptionsView.ShowGroupPanel = False
            gvBodegas.BestFitColumns()

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)
        Try
            gvBodegas.PostEditor()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function EsCorreoValido(ByVal pCorreo As String) As Boolean
        If String.IsNullOrWhiteSpace(pCorreo) Then Return False
        Return Regex.IsMatch(pCorreo.Trim(), "^[^@\s]+@[^@\s]+\.[^@\s]+$")
    End Function

    Private Sub Guardar()
        Dim be As clsBENotificacionContacto = ConstruirEntidadDesdePantalla()

        If Not _dxValidationProvider.Validate() Then Exit Sub

        If String.IsNullOrWhiteSpace(be.Nombre) Then
            Throw New Exception("El nombre es obligatorio.")
        End If

        If String.IsNullOrWhiteSpace(be.Correo) Then
            Throw New Exception("El correo es obligatorio.")
        End If

        If Not EsCorreoValido(be.Correo) Then
            Throw New Exception("El formato del correo no es válido.")
        End If

        If String.IsNullOrWhiteSpace(be.TipoContacto) Then
            Throw New Exception("El tipo de contacto es obligatorio.")
        End If

        If clsLnNotificacionContacto.ExisteCorreo(be.Correo, be.IdContacto) Then
            Throw New Exception("Ya existe un contacto con ese correo.")
        End If

        If _esNuevo OrElse be.IdContacto = 0 Then
            clsLnNotificacionContacto.Insertar(be)
        Else
            clsLnNotificacionContacto.Actualizar(be)
        End If

        GuardarBodegas(be.IdContacto)

        XtraMessageBox.Show("Registro guardado correctamente.",
                            "Notificaciones",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)

        CargarContactos()
        EnfocarRegistro(be.IdContacto)
    End Sub

    Private Sub GuardarBodegas(ByVal pIdContacto As Integer)

        Try
            clsLnNotificacionContactoBodega.EliminarPorContacto(pIdContacto)

            If DtContactoBodega IsNot Nothing AndAlso DtContactoBodega.Rows.Count > 0 Then

                For Each dr As DataRow In DtContactoBodega.Rows

                    Dim seleccionado As Boolean =
                    If(IsDBNull(dr("Seleccion")), False, CBool(dr("Seleccion")))

                    If seleccionado Then

                        Dim vIdBodega As Integer = If(IsDBNull(dr("IdBodega")), 0, CInt(dr("IdBodega")))

                        If vIdBodega > 0 Then
                            Dim oBe As New clsBENotificacionContactoBodega
                            oBe.IdContacto = pIdContacto
                            oBe.IdBodega = vIdBodega
                            oBe.User_agr = AP.UsuarioAp.Nombres
                            oBe.Fec_agr = Date.Now
                            oBe.User_mod = AP.UsuarioAp.Nombres
                            oBe.Fec_mod = Date.Now
                            oBe.Activo = True

                            clsLnNotificacionContactoBodega.Insertar(oBe)
                        End If

                    End If

                Next

            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub Eliminar()
        Try
            If _esNuevo OrElse String.IsNullOrWhiteSpace(txtIdContacto.Text) Then
                XtraMessageBox.Show("Seleccione un contacto existente.",
                                    "Notificaciones",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim idContacto As Integer = Convert.ToInt32(txtIdContacto.EditValue)

            Dim r = XtraMessageBox.Show(
                String.Format("¿Desea eliminar el contacto {0}?", txtCorreo.Text),
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If r <> DialogResult.Yes Then Exit Sub

            clsLnNotificacionContactoBodega.EliminarPorContacto(idContacto)

            Dim be As New clsBENotificacionContacto With {.IdContacto = idContacto}
            clsLnNotificacionContacto.Eliminar(be)

            XtraMessageBox.Show("Registro eliminado correctamente.",
                                "Notificaciones",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)

            CargarContactos()
            PrepararNuevo()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            clsLnLog_error_wms.Agregar_Error(ex.Message)
        End Try
    End Sub

    Private Sub EnfocarRegistro(ByVal pIdContacto As Integer)
        If pIdContacto <= 0 Then Exit Sub

        For i As Integer = 0 To gvContactos.RowCount - 1
            Dim valor As Object = gvContactos.GetRowCellValue(i, "IdContacto")

            If valor IsNot Nothing AndAlso Not IsDBNull(valor) AndAlso Convert.ToInt32(valor) = pIdContacto Then
                gvContactos.FocusedRowHandle = i
                gvContactos.MakeRowVisible(i)
                CargarDetalle(pIdContacto)
                Exit Sub
            End If
        Next
    End Sub

    Private Sub AplicarFiltroContactos()
        If _dtContactos Is Nothing Then Exit Sub

        Dim texto As String = If(txtBuscarContacto.Text, "").Trim().Replace("'", "''")
        Dim dv As New DataView(_dtContactos)

        If String.IsNullOrWhiteSpace(texto) Then
            dv.RowFilter = ""
        Else
            dv.RowFilter = String.Format(
                "Nombre LIKE '%{0}%' OR Correo LIKE '%{0}%' OR TipoContacto LIKE '%{0}%'",
                texto)
        End If

        gcContactos.DataSource = dv
    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        PrepararNuevo()
    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Try
            Guardar()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            clsLnLog_error_wms.Agregar_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Eliminar()
    End Sub

    Private Sub mnuRefrescar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Try
            Dim idActual As Integer = If(String.IsNullOrWhiteSpace(txtIdContacto.Text), 0, Convert.ToInt32(txtIdContacto.EditValue))

            CargarContactos()

            If idActual > 0 Then
                EnfocarRegistro(idActual)
            Else
                PrepararNuevo()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            clsLnLog_error_wms.Agregar_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtBuscarContacto_TextChanged(sender As Object, e As EventArgs)
        AplicarFiltroContactos()
    End Sub

    Private Sub gvContactos_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        Try
            If e.FocusedRowHandle < 0 Then Exit Sub

            Dim valor As Object = gvContactos.GetRowCellValue(e.FocusedRowHandle, "IdContacto")
            If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub

            CargarDetalle(Convert.ToInt32(valor))

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            clsLnLog_error_wms.Agregar_Error(ex.Message)
        End Try
    End Sub

    Private Sub gvContactos_DoubleClick(sender As Object, e As EventArgs)
        Try
            If gvContactos.FocusedRowHandle < 0 Then Exit Sub

            Dim valor As Object = gvContactos.GetRowCellValue(gvContactos.FocusedRowHandle, "IdContacto")
            If valor Is Nothing OrElse IsDBNull(valor) Then Exit Sub

            CargarDetalle(Convert.ToInt32(valor))

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            clsLnLog_error_wms.Agregar_Error(ex.Message)
        End Try
    End Sub

    Private Function CrearSuperTip(ByVal titulo As String, ByVal descripcion As String) As SuperToolTip

        Dim st As New SuperToolTip()

        Dim titleItem As New ToolTipTitleItem()
        titleItem.Text = titulo

        Dim contentItem As New ToolTipItem()
        contentItem.LeftIndent = 6
        contentItem.Text = descripcion
        contentItem.AllowHtmlText = DefaultBoolean.True

        st.Items.Add(titleItem)
        st.Items.Add(contentItem)

        Return st

    End Function
    Private Sub CargarToolTipsLayoutDatos()

        txtIdContacto.SuperTip = CrearSuperTip(
            "Id del contacto",
            "Identificador interno del registro." & Environment.NewLine &
            "Este campo lo asigna automáticamente el sistema y se utiliza para relacionar el contacto con sus bodegas."
        )

        txtNombre.SuperTip = CrearSuperTip(
            "Nombre del contacto",
            "Nombre completo o descripción corta de la persona o contacto." & Environment.NewLine &
            "Ejemplo: <b>Erik Calderon</b>."
        )

        txtCorreo.SuperTip = CrearSuperTip(
            "Correo electrónico",
            "Dirección de correo que recibirá las notificaciones." & Environment.NewLine &
            "Debe escribirse en formato válido, por ejemplo: <b>usuario@dominio.com</b>."
        )

        cboTipoContacto.SuperTip = CrearSuperTip(
            "Tipo de contacto",
            "Clasifica el uso del contacto dentro del proceso." & Environment.NewLine &
            "Ejemplo: Operativo, Supervisor, Jefe, Auditoría, Despacho o Recepción."
        )

        chkPermiteTo.SuperTip = CrearSuperTip(
            "Permite To",
            "Indica que este contacto puede usarse como destinatario principal del correo." & Environment.NewLine &
            "Se utiliza cuando el contacto debe recibir el mensaje directamente."
        )

        chkPermiteCc.SuperTip = CrearSuperTip(
            "Permite Cc",
            "Indica que este contacto puede usarse como copia del correo." & Environment.NewLine &
            "Úselo cuando el contacto solo debe estar enterado."
        )

        chkPermiteBcc.SuperTip = CrearSuperTip(
            "Permite Bcc",
            "Indica que este contacto puede usarse como copia oculta del correo." & Environment.NewLine &
            "Útil para auditoría o seguimiento sin mostrar al destinatario."
        )

        chkEsPrincipal.SuperTip = CrearSuperTip(
            "Es principal",
            "Marca este contacto como el principal dentro de sus asignaciones." & Environment.NewLine &
            "Puede servir como referencia preferida cuando existan varios contactos para una misma bodega."
        )

        chkActivo.SuperTip = CrearSuperTip(
            "Activo",
            "Define si el contacto está disponible para ser utilizado por el sistema." & Environment.NewLine &
            "Si está inactivo, no debería participar en las notificaciones."
        )

        txtObservaciones.SuperTip = CrearSuperTip(
            "Observaciones",
            "Espacio para notas adicionales sobre el contacto." & Environment.NewLine &
            "Ejemplo: horario, uso específico, restricciones o comentarios administrativos."
        )

    End Sub

End Class