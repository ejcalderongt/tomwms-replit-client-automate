Imports DevExpress.XtraEditors

Public Class frmHorario_Laboral

    Public pObjEnc As New clsBeHorario_laboral_enc
    Private pListObjT As New List(Of clsTabla)
    Private pListOjbDet As New List(Of clsBeHorario_laboral_det)
    Private pObjDet As New clsBeHorario_laboral_det
    Private gDia As Integer = 0
    Private gDiaAnterior As Integer = 0

    Public Delegate Sub Listar_Horario_Laboral()
    Public Property Listar As Listar_Horario_Laboral

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

    Private Sub frmHorario_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("turno")

            If Not AP.Listar_Bodegas_By_Usuario(cmbBodega) Then
                XtraMessageBox.Show("No hay Bodegas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf Not IMS.Listar_JornadasPorBodega(cmdJornada, cmbBodega.EditValue) Then
                XtraMessageBox.Show("No hay Jornadas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf Not IMS.Listar_Turnos(cmbTurno) Then
                XtraMessageBox.Show("No hay Turnos definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Select Case Modo
                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnHorario_laboral_enc.MaxID
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                    cmbBodega.Enabled = True
                    cmdJornada.Enabled = True
                    cmbTurno.Enabled = True

                    cmbTurno.Properties.DataSource = Nothing
                    cmdJornada.Properties.DataSource = Nothing

                    Set_Jornada_Laboral()

                    Set_Turno_Laboral()


                Case TipoTrans.Editar

                    clsLnHorario_laboral_enc.Obtener(pObjEnc)
                    lblCodigo.Text = pObjEnc.IdHorarioLaboralEnc
                    cmbBodega.EditValue = pObjEnc.IdBodega
                    cmdJornada.EditValue = pObjEnc.IdJornada
                    cmbTurno.EditValue = pObjEnc.IdTurno
                    txtNombre.Text = pObjEnc.Nombre
                    User_agrTextEdit.Text = pObjEnc.User_agr
                    Fec_agrDateEdit.Text = pObjEnc.Fec_agr
                    User_modTextEdit.Text = pObjEnc.User_mod
                    Fec_modDateEdit.Text = pObjEnc.Fec_mod

                    groupDetalle.Visible = True
                    cmbBodega.Enabled = False
                    cmdJornada.Enabled = False
                    cmbTurno.Enabled = False
                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    cargarDetalleHorarios(False)

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Me.Focus()
        txtNombre.Focus()

    End Sub

    Private Function Actualizar() As Boolean
        Actualizar = False
        Try
            If (Datos_Correctos()) Then
                pObjEnc.Nombre = txtNombre.Text.Trim()
                Actualizar = clsLnHorario_laboral_enc.Actualizar(pObjEnc) > 0
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try
            mnuGuardar.Enabled = False

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar_Encabezado() Then

                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        dtmHoraInicio.Focus()
                        groupDetalle.Visible = True
                        mnuGuardar.Enabled = False
                        cmbBodega.Enabled = False
                        cmdJornada.Enabled = False
                        cmbTurno.Enabled = False

                    End If
                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If
                End If

            End If
            mnuGuardar.Enabled = True

        Catch ex As Exception
            mnuGuardar.Enabled = True
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try
            mnuActualizar.Enabled = False

            If Actualizar() Then
                XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If
                Close()
            End If
            mnuActualizar.Enabled = True

        Catch ex As Exception
            mnuActualizar.Enabled = True
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try
            mnuEliminar.Enabled = False

            If MessageBox.Show("¿Desactivar el Horario Laboral?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                clsLnHorario_laboral_enc.Eliminar(pObjEnc)

                XtraMessageBox.Show("Se eliminó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If

                Close()

            End If
            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub limpiarHorarioLaboral()
        chkLunes.Checked = False
        chkMartes.Checked = False
        chkMiercoles.Checked = False
        chkJueves.Checked = False
        chkViernes.Checked = False
        chkSabado.Checked = False
        chkDomingo.Checked = False
        dtmHoraInicio.Value = Now
        dtmHoraFin.Value = Now
        txtTiempoRetraso.Value = 0
        chkHoraExtra.Checked = False
        txtMMI1.Value = 0
        txtMMI2.Value = 0
        txtMMS1.Value = 0
        txtMMS2.Value = 0
    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick

        limpiarHorarioLaboral()

        Try

            Dim Dr As DataRowView = GridView1.GetFocusedRow

            Dim lIndex As Integer = -1
            lIndex = pListOjbDet.FindIndex(Function(b) b.IdHorarioLaboralDet = CInt(Dr.Item("Código")))
            If lIndex > -1 Then
                cmdAdd.Tag = pListOjbDet(lIndex).IdHorarioLaboralDet
                dtmHoraInicio.Value = pListOjbDet(lIndex).Hora_inicio
                dtmHoraFin.Value = pListOjbDet(lIndex).Hora_fin
                txtTiempoRetraso.Value = pListOjbDet(lIndex).Tiempo_retraso_permitido
                txtMMI1.Value = pListOjbDet(lIndex).Minimo_min_hora_ingreso
                txtMMI2.Value = pListOjbDet(lIndex).Maximo_min_hora_ingreso
                txtMMS1.Value = pListOjbDet(lIndex).Maximo_min_hora_salida
                txtMMS2.Value = pListOjbDet(lIndex).Maximo_min_hora_salida
                chkHoraExtra.Checked = pListOjbDet(lIndex).Horas_extras
                validarDiaSeleccionado(pListOjbDet(lIndex).Dia)
                gDia = pListOjbDet(lIndex).Dia
                gDiaAnterior = pListOjbDet(lIndex).Dia

                cmdAdd.Visible = False
                cmdActualizar.Visible = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub validarDiaSeleccionado(ByVal dia As Integer)
        Try
            If dia = 1 Then
                chkLunes.Checked = True
            ElseIf dia = 2 Then
                chkMartes.Checked = True
            ElseIf dia = 3 Then
                chkMiercoles.Checked = True
            ElseIf dia = 4 Then
                chkJueves.Checked = True
            ElseIf dia = 5 Then
                chkViernes.Checked = True
            ElseIf dia = 6 Then
                chkSabado.Checked = True
            ElseIf dia = 7 Then
                chkDomingo.Checked = True
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    'ENCABEZADO
    Private Function Guardar_Encabezado() As Boolean

        Guardar_Encabezado = False

        Try

            Dim objEnc As New clsBeHorario_laboral_enc() With
                {.IdHorarioLaboralEnc = clsLnHorario_laboral_enc.MaxID(),
                .IdBodega = cmbBodega.EditValue,
                .IdJornada = cmdJornada.EditValue,
                .IdTurno = cmbTurno.EditValue,
                .Nombre = txtNombre.Text.Trim(),
                .Activo = True,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now}

            If clsLnHorario_laboral_enc.Exists(cmbBodega.EditValue, cmdJornada.EditValue, cmbTurno.EditValue) Then
                XtraMessageBox.Show("Ya existe un horario con esta configuración", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Guardar_Encabezado = clsLnHorario_laboral_enc.Insertar(objEnc) > 0
                pObjEnc.IdHorarioLaboralEnc = objEnc.IdHorarioLaboralEnc
                Guardar_Encabezado = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf cmdJornada.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Jornada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function GuardarHorarioLaboral(ByVal dia As Integer) As Boolean

        GuardarHorarioLaboral = False

        Try

            Dim objDet As New clsBeHorario_laboral_det() With {.IdHorarioLaboralDet = clsLnHorario_laboral_det.MaxID(),
                .IdHorarioLaboralEnc = pObjEnc.IdHorarioLaboralEnc,
                .Dia = dia,
                .Hora_inicio = dtmHoraInicio.Value,
                .Hora_fin = dtmHoraFin.Value,
                .Minimo_min_hora_ingreso = txtMMI1.Value,
                .Maximo_min_hora_ingreso = txtMMI2.Value,
                .Minimo_min_hora_salida = txtMMS1.Value,
                .Maximo_min_hora_salida = txtMMS2.Value,
                .Tiempo_retraso_permitido = txtTiempoRetraso.Value,
                .Horas_extras = chkHoraExtra.Checked,
                .Activo = True,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now}

            If clsLnHorario_laboral_det.Exists(pObjEnc.IdHorarioLaboralEnc, dia) Then
                XtraMessageBox.Show("Ya existe un horario con este dia", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                GuardarHorarioLaboral = clsLnHorario_laboral_det.Insertar(objDet) > 0
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cargarDetalleHorarios(ByVal pGuardo As Boolean)

        Try

            If pGuardo = False Then
                pListOjbDet = clsLnHorario_laboral_det.getAllByHorarioEnc(pObjEnc.IdHorarioLaboralEnc, chkActivos.Checked).ToList()
            End If

            Dim DT As New DataTable("Horarios")
            DT.Columns.Add("Código", GetType(Integer))
            DT.Columns.Add("codDia", GetType(Integer))
            DT.Columns.Add("Dia", GetType(String))
            DT.Columns.Add("Hora Inicio", GetType(String))
            DT.Columns.Add("Hora Fin", GetType(String))
            DT.Columns.Add("Minimo Entrada", GetType(Integer))
            DT.Columns.Add("Maximo Entrada", GetType(Integer))
            DT.Columns.Add("Minimo Salida", GetType(Integer))
            DT.Columns.Add("Maximo Salida", GetType(Integer))
            DT.Columns.Add("Horas Extras", GetType(Boolean))
            Dgrid.DataSource = Nothing

            For Each Obj As clsBeHorario_laboral_det In pListOjbDet.OrderBy(Function(o) o.IdHorarioLaboralEnc)
                Dim lRow As DataRow = DT.NewRow

                lRow(0) = Obj.IdHorarioLaboralDet
                lRow(1) = Obj.Dia
                lRow(2) = Obj.NombreDia
                lRow(3) = Obj.NHoraInicio
                lRow(4) = Obj.NHoraFin
                lRow(5) = Obj.Minimo_min_hora_ingreso
                lRow(6) = Obj.Maximo_min_hora_ingreso
                lRow(7) = Obj.Maximo_min_hora_salida
                lRow(8) = Obj.Maximo_min_hora_salida
                lRow(9) = Obj.Horas_extras
                DT.Rows.Add(lRow)
            Next

            Dgrid.DataSource = DT
            GridView1.Columns("codDia").Visible = False

            Dgrid.Refresh()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click

        Try

            guardarHorario()

            cargarDetalleHorarios(False)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub guardarHorario()

        Try

            If chkLunes.Checked Then
                GuardarHorarioLaboral(1)
            End If
            If chkMartes.Checked Then
                GuardarHorarioLaboral(2)
            End If
            If chkMiercoles.Checked Then
                GuardarHorarioLaboral(3)
            End If
            If chkJueves.Checked Then
                GuardarHorarioLaboral(4)
            End If
            If chkViernes.Checked Then
                GuardarHorarioLaboral(5)
            End If
            If chkSabado.Checked Then
                GuardarHorarioLaboral(6)
            End If
            If chkDomingo.Checked Then
                GuardarHorarioLaboral(7)
                GuardarHorarioLaboral(0)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function ActualizarHorario() As Boolean

        ActualizarHorario = False

        Try

            Dim Dr As DataRowView = GridView1.GetFocusedRow

            pObjDet.IdHorarioLaboralDet = CInt(Dr.Item("Código"))
            pObjDet.IdHorarioLaboralEnc = pObjEnc.IdHorarioLaboralEnc
            pObjDet.Dia = gDia
            pObjDet.Hora_inicio = dtmHoraInicio.Value
            pObjDet.Hora_fin = dtmHoraFin.Value
            pObjDet.Minimo_min_hora_ingreso = txtMMI1.Value
            pObjDet.Maximo_min_hora_ingreso = txtMMI2.Value
            pObjDet.Minimo_min_hora_salida = txtMMS1.Value
            pObjDet.Maximo_min_hora_salida = txtMMS2.Value
            pObjDet.Tiempo_retraso_permitido = txtTiempoRetraso.Value
            pObjDet.Horas_extras = chkHoraExtra.Checked
            pObjDet.Activo = True
            pObjDet.User_mod = AP.UsuarioAp.IdUsuario
            pObjDet.Fec_mod = Now

            If clsLnHorario_laboral_det.Exists(pObjEnc.IdHorarioLaboralEnc, gDiaAnterior) AndAlso
               Not clsLnHorario_laboral_det.Exists(pObjEnc.IdHorarioLaboralEnc, gDia) AndAlso
                gDia <> gDiaAnterior Then
                If XtraMessageBox.Show("Ya existe un horario con el dia " & gDiaAnterior & " desea cambiarlo por el dia " & gDia, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    ActualizarHorario = clsLnHorario_laboral_det.Actualizar(pObjDet) > 0
                End If
            ElseIf Not clsLnHorario_laboral_det.Exists(pObjEnc.IdHorarioLaboralEnc, gDia) Then
                ActualizarHorario = clsLnHorario_laboral_det.Actualizar(pObjDet) > 0
            ElseIf clsLnHorario_laboral_det.Exists(pObjEnc.IdHorarioLaboralEnc, gDia) AndAlso gDia <> gDiaAnterior Then
                XtraMessageBox.Show("Ya existe un horario para  el dia " & gDia & " no es posible realizar la actualización", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            Else
                XtraMessageBox.Show("No existe un horario para  el dia " & gDia & " no es posible realizar la actualización", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            End If

            cargarDetalleHorarios(False)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click

        Try

            Dim Dr As DataRowView = GridView1.GetFocusedRow
            pObjDet = clsLnHorario_laboral_det.GetSingle(Dr.Item("Código"))

            If pObjDet.Activo = False Then

                If MessageBox.Show("¿Registro inactivo. Desea activarlo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    pObjDet.Activo = True
                    pObjDet.IdHorarioLaboralDet = CInt(Dr.Item("Código"))
                    pObjDet.IdHorarioLaboralEnc = pObjEnc.IdHorarioLaboralEnc

                    If clsLnHorario_laboral_det.Actualizar(pObjDet) > 0 Then
                        XtraMessageBox.Show("Se ha activado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cargarDetalleHorarios(False)
                        limpiarHorarioLaboral()
                    End If

                End If

            Else

                If MessageBox.Show("¿Desactivar Horario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    pObjDet.Activo = False
                    pObjDet.IdHorarioLaboralDet = CInt(Dr.Item("Código"))
                    pObjDet.IdHorarioLaboralEnc = pObjEnc.IdHorarioLaboralEnc

                    If clsLnHorario_laboral_det.Actualizar(pObjDet) > 0 Then
                        XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cargarDetalleHorarios(False)
                        limpiarHorarioLaboral()
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    'Validaciones para que los controles tipo numericos no acepte datos incorrectos

    Private Sub txtMMI1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMMI1.KeyPress
        Try
            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If
            If e.KeyChar = "." Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtMMI2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMMI2.KeyPress
        Try
            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If
            If e.KeyChar = "." Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtMMS1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMMS1.KeyPress
        Try
            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If
            If e.KeyChar = "." Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtMMS2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMMS2.KeyPress
        Try
            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If
            If e.KeyChar = "." Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtTiempoRetraso_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTiempoRetraso.KeyPress
        Try
            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If
            If e.KeyChar = "." Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivos.CheckedChanged
        cargarDetalleHorarios(False)
    End Sub

    Private Sub cmdActualizar_Click(sender As Object, e As EventArgs) Handles cmdActualizar.Click

        If ActualizarHorario() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            limpiarHorarioLaboral()
        End If

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Set_Jornada_Laboral()

        Set_Turno_Laboral()

    End Sub

    Private Sub Set_Turno_Laboral()

        Try



            Dim DT1 As New DataTable
            DT1 = clsLnTurno.Get_Turnos_por_Bodega(cmbBodega.EditValue)
            cmbTurno.Properties.DataSource = DT1
            cmbTurno.Properties.ValueMember = "IdTurno"
            cmbTurno.Properties.DisplayMember = "nombre"

            If DT1.Rows.Count = 1 Then
                cmbTurno.Text = DT1.Rows(0).Item("nombre").ToString
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

        End Try

    End Sub

    Private Sub Set_Jornada_Laboral()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnJornada_laboral.Listar(cmbBodega.EditValue)
            cmdJornada.Properties.DataSource = DT1
            cmdJornada.Properties.ValueMember = "IdJornada"
            cmdJornada.Properties.DisplayMember = "nombre_jornada"

            If DT1.Rows.Count = 1 Then
                cmdJornada.Text = DT1.Rows(0).Item("nombre_jornada").ToString
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

        End Try

    End Sub

    Private Sub frmHorario_Laboral_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub chkLunes_CheckedChanged(sender As Object, e As EventArgs) Handles chkLunes.CheckedChanged
        gDia = 1
    End Sub

    Private Sub chkMartes_CheckedChanged(sender As Object, e As EventArgs) Handles chkMartes.CheckedChanged
        gDia = 2
    End Sub

    Private Sub chkMiercoles_CheckedChanged(sender As Object, e As EventArgs) Handles chkMiercoles.CheckedChanged
        gDia = 3
    End Sub

    Private Sub chkJueves_CheckedChanged(sender As Object, e As EventArgs) Handles chkJueves.CheckedChanged
        gDia = 4
    End Sub

    Private Sub chkViernes_CheckedChanged(sender As Object, e As EventArgs) Handles chkViernes.CheckedChanged
        gDia = 5
    End Sub

    Private Sub chkSabado_CheckedChanged(sender As Object, e As EventArgs) Handles chkSabado.CheckedChanged
        gDia = 6
    End Sub

    Private Sub chkDomingo_CheckedChanged(sender As Object, e As EventArgs) Handles chkDomingo.CheckedChanged
        gDia = 7
    End Sub

    Private Sub cmdNuevo_Click(sender As Object, e As EventArgs) Handles cmdNuevo.Click
        Try
            chkLunes.Checked = False
            chkMartes.Checked = False
            chkMiercoles.Checked = False
            chkJueves.Checked = False
            chkViernes.Checked = False
            chkSabado.Checked = False
            chkDomingo.Checked = False
            chkHoraExtra.Checked = False
            txtMMI1.Value = 0
            txtMMI2.Value = 0
            txtMMS1.Value = 0
            txtMMS2.Value = 0
            chkActivos.Checked = True
            txtTiempoRetraso.Value = 0

            cmdAdd.Visible = True
            cmdActualizar.Visible = False

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Dgrid_Click(sender As Object, e As EventArgs) Handles Dgrid.Click

    End Sub
End Class