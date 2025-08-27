Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen

Public Class frmCuadrilla

    Public pBeCuadrillaEnc As New clsBeCuadrilla_enc

    Private DTOperadores As New DataTable
    Private DTMontaCargas As New DataTable

    Private pListOpe As New List(Of clsBeCuadrilla_det_operador)
    Private pListMC As New List(Of clsBeCuadrilla_det_montacarga)

    Private DTOperadoresGrid As New DataTable
    Private DTMontacargasGrid As New DataTable

    Private vTotalCostoHoraMontaCarga As Double = 0
    Private vTotalCostoHoraOperadores As Double = 0

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
        InitDatatable_Operadores()
        InitDatatable_Montacargas()
    End Sub

    Private Sub InitDatatable_Operadores()

        DTOperadoresGrid.Columns.Add("Seleccionar", GetType(Boolean))
        DTOperadoresGrid.Columns.Add("IdOperadorBodega", GetType(Integer)).Caption = "Id"
        DTOperadoresGrid.Columns.Add("Nombre", GetType(String))
        DTOperadoresGrid.Columns.Add("UsaHH", GetType(Boolean))
        DTOperadoresGrid.Columns.Add("Recibe", GetType(Boolean))
        DTOperadoresGrid.Columns.Add("Ubica", GetType(Boolean))
        DTOperadoresGrid.Columns.Add("Transporta", GetType(Boolean))
        DTOperadoresGrid.Columns.Add("Pickea", GetType(Boolean))
        DTOperadoresGrid.Columns.Add("Verifica", GetType(Boolean))
        DTOperadoresGrid.Columns.Add("Costo_Hora", GetType(Double))

    End Sub

    Private Sub InitDatatable_Montacargas()

        DTMontacargasGrid.Columns.Add("Seleccionar", GetType(Boolean))
        DTMontacargasGrid.Columns.Add("IdMontacargaBodega", GetType(Integer)).Caption = "Id"
        DTMontacargasGrid.Columns.Add("Nombre", GetType(String))
        DTMontacargasGrid.Columns.Add("Tipo", GetType(String))
        DTMontacargasGrid.Columns.Add("Costo_Hora", GetType(Double))

    End Sub

    Private Sub frmCuadrilla_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Try

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            Llena_Tipo_Cuadrilla()

            Select Case Modo

                Case TipoTrans.Nuevo

                    txtIdCuadrilla.Text = clsLnCuadrilla_enc.MaxID()
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    clsLnCuadrilla_enc.GetSingle(pBeCuadrillaEnc)

                    pListOpe = pBeCuadrillaEnc.DetalleOperadores
                    pListMC = pBeCuadrillaEnc.DetalleMontaCargas

                    txtIdCuadrilla.Text = pBeCuadrillaEnc.IdCuadrillaEnc
                    txtNombre.Text = pBeCuadrillaEnc.Nombre
                    chkActivo.Checked = pBeCuadrillaEnc.Activo
                    cmbBodega.EditValue = pBeCuadrillaEnc.IdBodega
                    cmbTipoCuadrilla.EditValue = pBeCuadrillaEnc.IdTipoCuadrilla

                    User_agrTextEdit.Text = pBeCuadrillaEnc.User_agr
                    Fec_agrDateEdit.Text = pBeCuadrillaEnc.Fec_agr
                    User_modTextEdit.Text = pBeCuadrillaEnc.User_mod
                    Fec_modDateEdit.Text = pBeCuadrillaEnc.Fec_mod

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = OpcionesMenu.Modificar
                    mnuEliminar.Enabled = OpcionesMenu.Eliminar

            End Select

            Valida_Operadores()

            Valida_Montacargas()

            CheckTotals()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        'txtCodigo.Focus()

    End Sub

    Private Sub CheckTotals()

        Try


            Dim vMontoTotal As Double = vTotalCostoHoraMontaCarga + vTotalCostoHoraOperadores
            lblCostoPorHora.Text = FormatCurrency(vMontoTotal, 6,, TriState.True, TriState.True)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
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

            pBeCuadrillaEnc = New clsBeCuadrilla_enc
            pBeCuadrillaEnc.IdCuadrillaEnc = 0
            pBeCuadrillaEnc.IdEmpresa = AP.IdEmpresa
            pBeCuadrillaEnc.IdBodega = cmbBodega.EditValue
            pBeCuadrillaEnc.IdTipoCuadrilla = cmbTipoCuadrilla.EditValue
            pBeCuadrillaEnc.Nombre = txtNombre.Text.Trim()
            pBeCuadrillaEnc.Activo = True
            pBeCuadrillaEnc.User_agr = AP.UsuarioAp.IdUsuario
            pBeCuadrillaEnc.Fec_agr = Now
            pBeCuadrillaEnc.User_mod = AP.UsuarioAp.IdUsuario
            pBeCuadrillaEnc.Fec_mod = Now

            Guardar = clsLnCuadrilla_enc.Guardar(pBeCuadrillaEnc,
                                                 pListOpe,
                                                 pListMC) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pBeCuadrillaEnc.IdEmpresa = AP.IdEmpresa
                pBeCuadrillaEnc.IdBodega = cmbBodega.EditValue
                pBeCuadrillaEnc.IdTipoCuadrilla = cmbTipoCuadrilla.EditValue
                pBeCuadrillaEnc.Nombre = txtNombre.Text.Trim()
                pBeCuadrillaEnc.Activo = True
                pBeCuadrillaEnc.IsNew = False
                pBeCuadrillaEnc.User_mod = AP.UsuarioAp.IdUsuario
                pBeCuadrillaEnc.Fec_mod = Now

                Actualizar = clsLnCuadrilla_enc.Guardar(pBeCuadrillaEnc,
                                                        pListOpe,
                                                        pListMC) > 0
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Datos_Correctos() Then
            If MessageBox.Show("¿Guardar Cuadrilla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If MessageBox.Show("¿Eliminar cuadrilla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                'pObjBEJ.Activo = False
                'If clsLnCuadrilla.Actualizar(pObjBEJ) > 0 Then
                '    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Close()
                '    frmCuadrilla_List.Dgrid.Refresh()
                'End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtUbicacion_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If
        If e.KeyChar = "." Then
            e.Handled = True
        End If
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

    Public Class TipoCuadrillaInfo
        Public Property IdTipoCuadrilla As Integer = 0
        Public Property Nombre As String = ""
    End Class

    Private Sub Llena_Tipo_Cuadrilla()

        Try

            Dim lTipoCuadrilla As New List(Of clsBeCuadrilla_tipo)
            lTipoCuadrilla = clsLnCuadrilla_tipo.GetAll()

            If Not lTipoCuadrilla Is Nothing Then

                Dim tcds = (From p In lTipoCuadrilla
                            Select New TipoCuadrillaInfo _
                            With {.IdTipoCuadrilla = p.IdTipoCuadrilla,
                                .Nombre = p.Nombre})

                If lTipoCuadrilla.Count > 0 Then
                    cmbTipoCuadrilla.Properties.DisplayMember = "Nombre"
                    cmbTipoCuadrilla.Properties.ValueMember = "IdTipoCuadrilla"
                    cmbTipoCuadrilla.Properties.DataSource = tcds
                    cmbTipoCuadrilla.EditValue = AP.IdBodega
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Valida_Operadores()

        Try

            DTOperadores = clsLnOperador_bodega.Get_All_For_Cuadrilla_By_IdBodega(cmbBodega.EditValue)

            'ejc_18092016
            If pBeCuadrillaEnc IsNot Nothing Then
                If pBeCuadrillaEnc.IdCuadrillaEnc <> 0 Then
                    pListOpe = clsLnCuadrilla_det_operador.Get_All_By_IdCuadrillaEnc(pBeCuadrillaEnc.IdCuadrillaEnc)
                End If
            Else
                pListOpe = New List(Of clsBeCuadrilla_det_operador)
            End If

            Lista_Operadores()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Lista_Operadores()

        vTotalCostoHoraOperadores = 0

        Dim vCostoHoraOperadores As Double = 0

        Try

            DGridOperadores.BeginUpdate()

            If DTOperadores.Rows.Count > 0 Then

                Dim vIdOperadorBodega As Integer = 0

                For i As Integer = 0 To DTOperadores.Rows.Count - 1

                    vIdOperadorBodega = DTOperadores.Rows(i).Item("IdOperadorBodega")
                    vCostoHoraOperadores = IIf(IsDBNull(DTOperadores.Rows(i).Item("costo_hora")), 0.0, DTOperadores.Rows(i).Item("costo_hora"))

                    Dim lRow As DataRow = DTOperadoresGrid.NewRow
                    lRow.Item("Seleccionar") = False
                    lRow.Item("IdOperadorBodega") = vIdOperadorBodega
                    lRow.Item("Nombre") = IIf(IsDBNull(DTOperadores.Rows(i).Item("Nombres")), "", DTOperadores.Rows(i).Item("Nombres"))
                    lRow.Item("UsaHH") = IIf(IsDBNull(DTOperadores.Rows(i).Item("usa_hh")), False, DTOperadores.Rows(i).Item("usa_hh"))
                    lRow.Item("Recibe") = IIf(IsDBNull(DTOperadores.Rows(i).Item("Recibe")), False, DTOperadores.Rows(i).Item("Recibe"))
                    lRow.Item("Ubica") = IIf(IsDBNull(DTOperadores.Rows(i).Item("Ubica")), False, DTOperadores.Rows(i).Item("Ubica"))
                    lRow.Item("Transporta") = IIf(IsDBNull(DTOperadores.Rows(i).Item("Transporta")), False, DTOperadores.Rows(i).Item("Transporta"))
                    lRow.Item("Pickea") = IIf(IsDBNull(DTOperadores.Rows(i).Item("Pickea")), False, DTOperadores.Rows(i).Item("Pickea"))
                    lRow.Item("Verifica") = IIf(IsDBNull(DTOperadores.Rows(i).Item("Verifica")), False, DTOperadores.Rows(i).Item("Verifica"))
                    lRow.Item("Costo_Hora") = vCostoHoraOperadores

                    DTOperadoresGrid.Rows.Add(lRow)

                    If TipoTrans.Editar Then

                        If pListOpe IsNot Nothing AndAlso pListOpe.Count > 0 Then

                            For Each Obj As clsBeCuadrilla_det_operador In pListOpe

                                If Obj.IdOperadorBodega = vIdOperadorBodega Then
                                    lRow.Item("Seleccionar") = True
                                    vTotalCostoHoraOperadores += vCostoHoraOperadores
                                End If

                            Next

                        End If

                    End If

                Next

            End If

            DGridOperadores.DataSource = DTOperadoresGrid

            GrdOperadorBobega.Columns("Costo_Hora").SummaryItem.SummaryType = SummaryItemType.Sum
            GrdOperadorBobega.Columns("Costo_Hora").SummaryItem.DisplayFormat = "{0:n6}"
            GrdOperadorBobega.Columns("Costo_Hora").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GrdOperadorBobega.Columns("Costo_Hora").DisplayFormat.FormatString = "{0:n6}"

            Dim ritem As RepositoryItemCheckEdit = TryCast(GrdOperadorBobega.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

            DGridOperadores.EndUpdate()

            DGridOperadores.ForceInitialize()

            If GrdOperadorBobega.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GrdOperadorBobega.RowCount)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = GrdOperadorBobega.GetFocusedRow
                Dim lIndex As Integer = -1
                Dim IdOperadorBodegaGrid As Integer = Dr.Item("IdOperadorBodega")
                Dim vCostoHora As Double = IIf(IsDBNull(Dr.Item("costo_hora")), 0, Dr.Item("costo_hora"))

                lIndex = pListOpe.FindIndex(Function(b) b.IdOperadorBodega = IdOperadorBodegaGrid)

                If lIndex > -1 Then

                    If Not ritem.Checked Then

                        If (pListOpe(lIndex).IdCuadrillaDet > 0) AndAlso (pListOpe(lIndex).IdCuadrillaEnc > 0) Then

                            'clsLnCuadrilla_det_operador.Eliminar_By_IdOperadorBodega_And_IdCuadrillaEnc(IdOperadorBodegaGrid,
                            '                                                                            pBeCuadrillaEnc.IdCuadrillaEnc)

                            'Lista_Operadores()

                        End If

                        pListOpe(lIndex).Activo = False

                        vTotalCostoHoraOperadores -= vCostoHora

                        Dr.Item("Seleccionar") = False

                    Else

                        Dim Obj As New clsBeCuadrilla_det_operador()
                        Obj.IdOperadorBodega = Dr.Item("IdOperadorBodega")
                        Obj.User_agr = AP.UsuarioAp.IdUsuario
                        Obj.Fec_agr = Now
                        Obj.User_mod = AP.UsuarioAp.IdUsuario
                        Obj.Fec_mod = Now
                        Obj.IsNew = True
                        Obj.Activo = True
                        pListOpe.Add(Obj)

                        vTotalCostoHoraOperadores += vCostoHora

                        Dr.Item("Seleccionar") = True

                    End If

                Else

                    Dim Obj As New clsBeCuadrilla_det_operador()
                    Obj.IdOperadorBodega = Dr.Item("IdOperadorBodega")
                    Obj.User_agr = AP.UsuarioAp.IdUsuario
                    Obj.Fec_agr = Now
                    Obj.User_mod = AP.UsuarioAp.IdUsuario
                    Obj.Fec_mod = Now
                    Obj.IsNew = True
                    Obj.Activo = True
                    pListOpe.Add(Obj)

                    vTotalCostoHoraOperadores += vCostoHora

                    Dr.Item("Seleccionar") = True

                End If

                CheckTotals()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Valida_Montacargas()

        Try

            DTMontaCargas = clsLnMontacarga_bodega.Get_All_For_Cuadrilla_By_IdBodega(cmbBodega.EditValue)

            'ejc_18092016
            If pBeCuadrillaEnc IsNot Nothing Then
                If pBeCuadrillaEnc.IdCuadrillaEnc <> 0 Then
                    pListMC = clsLnCuadrilla_det_montacarga.Get_All_By_IdCuadrillaEnc(pBeCuadrillaEnc.IdCuadrillaEnc)
                End If
            Else
                pListMC = New List(Of clsBeCuadrilla_det_montacarga)
            End If

            Lista_Montacargas()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Lista_Montacargas()

        vTotalCostoHoraMontaCarga = 0

        Dim vCostoHoraMontaCarga As Double = 0

        Try

            DgridMontaCargas.BeginUpdate()

            If DTMontaCargas.Rows.Count > 0 Then

                Dim vIdmontacargaBodega As Integer = 0

                DTMontacargasGrid.Rows.Clear()

                For i As Integer = 0 To DTMontaCargas.Rows.Count - 1

                    vIdmontacargaBodega = DTMontaCargas.Rows(i).Item("IdMontacargaBodega")
                    vCostoHoraMontaCarga = IIf(IsDBNull(DTMontaCargas.Rows(i).Item("costo_hora")), 0.00, DTMontaCargas.Rows(i).Item("costo_hora"))

                    Dim lRow As DataRow = DTMontacargasGrid.NewRow
                    lRow.Item("Seleccionar") = False
                    lRow.Item("IdMontacargaBodega") = vIdmontacargaBodega
                    lRow.Item("Nombre") = IIf(IsDBNull(DTMontaCargas.Rows(i).Item("Nombre")), "", DTMontaCargas.Rows(i).Item("Nombre"))
                    lRow.Item("Tipo") = IIf(IsDBNull(DTMontaCargas.Rows(i).Item("tipo_montacarga")), "", DTMontaCargas.Rows(i).Item("tipo_montacarga"))
                    lRow.Item("Costo_Hora") = vCostoHoraMontaCarga
                    DTMontacargasGrid.Rows.Add(lRow)

                    If TipoTrans.Editar Then

                        If pListMC IsNot Nothing AndAlso pListMC.Count > 0 Then

                            For Each Obj As clsBeCuadrilla_det_montacarga In pListMC

                                If Obj.IdMontacargaBodega = vIdmontacargaBodega Then
                                    lRow.Item("Seleccionar") = True
                                    vTotalCostoHoraMontaCarga += vCostoHoraMontaCarga
                                End If

                            Next

                        End If

                    End If

                Next

            End If

            DgridMontaCargas.DataSource = DTMontacargasGrid

            DgridMontaCargas.EndUpdate()
            DgridMontaCargas.ForceInitialize()

            If grdviewMontaCarga.RowCount > 0 Then

                lblRegs.Caption = String.Format("Registros: {0}", grdviewMontaCarga.RowCount)

                Dim ritem As RepositoryItemCheckEdit = TryCast(grdviewMontaCarga.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
                AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged_MC

            End If

            If grdviewMontaCarga.Columns.Count > 0 Then

                grdviewMontaCarga.Columns("Costo_Hora").SummaryItem.SummaryType = SummaryItemType.Sum
                grdviewMontaCarga.Columns("Costo_Hora").SummaryItem.DisplayFormat = "{0:n6}"
                grdviewMontaCarga.Columns("Costo_Hora").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdviewMontaCarga.Columns("Costo_Hora").DisplayFormat.FormatString = "{0:n6}"

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged_MC(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = grdviewMontaCarga.GetFocusedRow
                Dim lIndex As Integer = -1
                Dim IdmontacargaBodegaGrid As Integer = Dr.Item("IdmontacargaBodega")
                Dim vCostoHora As Double = IIf(IsDBNull(Dr.Item("costo_hora")), 0, Dr.Item("costo_hora"))

                lIndex = pListMC.FindIndex(Function(b) b.IdMontacargaBodega = IdmontacargaBodegaGrid)

                If lIndex > -1 Then

                    If Not ritem.Checked Then

                        vTotalCostoHoraMontaCarga -= vCostoHora

                        pListMC(lIndex).Activo = False

                        Dr.Item("Seleccionar") = False

                    Else

                        Dr.Item("Seleccionar") = True

                        Dim Obj As New clsBeCuadrilla_det_montacarga()
                        Obj.IdMontacargaBodega = Dr.Item("IdMontacargaBodega")
                        Obj.User_agr = AP.UsuarioAp.IdUsuario
                        Obj.Fec_agr = Now
                        Obj.User_mod = AP.UsuarioAp.IdUsuario
                        Obj.Fec_mod = Now
                        Obj.IsNew = True
                        Obj.Activo = True
                        pListMC.Add(Obj)

                        vTotalCostoHoraMontaCarga += vCostoHora

                    End If

                Else

                    Dr.Item("Seleccionar") = True

                    Dim Obj As New clsBeCuadrilla_det_montacarga()
                    Obj.IdMontacargaBodega = Dr.Item("IdmontacargaBodega")
                    Obj.User_agr = AP.UsuarioAp.IdUsuario
                    Obj.Fec_agr = Now
                    Obj.User_mod = AP.UsuarioAp.IdUsuario
                    Obj.Fec_mod = Now
                    Obj.IsNew = True
                    Obj.Activo = True
                    pListMC.Add(Obj)

                    vTotalCostoHoraMontaCarga += vCostoHora

                End If

                CheckTotals()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub DGridOperadores_Click(sender As Object, e As EventArgs) Handles DGridOperadores.Click



    End Sub

    'Private Sub Procesa_Operadores()

    '    Try

    '        Dim filaseleccionada As Integer = GrdOperadorBobega.GetSelectedRows()(0)
    '        Dim lIndex As Integer = -1
    '        Dim tabla As DataTable = DGridOperadores.DataSource

    '        tabla.Rows(filaseleccionada).BeginEdit()
    '        Dim valor As Boolean = tabla.Rows(filaseleccionada)("Seleccionar")
    '        Dim IdmontacargaBodegaGrid As Integer = tabla.Rows(filaseleccionada).Item("IdmontacargaBodega")
    '        Dim vCostoHora As Double = IIf(IsDBNull(tabla.Rows(filaseleccionada).Item("costo_hora")), 0, tabla.Rows(filaseleccionada).Item("costo_hora"))
    '        Dim vSeleccionado As Boolean = IIf(valor, False, True)
    '        tabla.Rows(filaseleccionada)("Seleccionar") = IIf(valor, False, True)
    '        tabla.Rows(filaseleccionada).EndEdit()
    '        tabla.Rows(filaseleccionada).AcceptChanges()


    '        For Each row In DGridOperadores.DataSource

    '        Next

    '        lIndex = pListMC.FindIndex(Function(b) b.IdMontacargaBodega = IdmontacargaBodegaGrid)

    '        If lIndex > -1 Then

    '            If Not vSeleccionado Then

    '                If (pListMC(lIndex).IdCuadrillaDetMontaCarga > 0) AndAlso (pListMC(lIndex).IdCuadrillaEnc > 0) Then

    '                    clsLnCuadrilla_det_montacarga.Eliminar_By_IdMontacargaBodega_And_IdCuadrillaEnc(IdmontacargaBodegaGrid, pBeCuadrillaEnc.IdCuadrillaEnc)

    '                    Lista_Montacargas()

    '                    vTotalCostoHoraMontaCarga -= vCostoHora

    '                End If

    '                pListMC.RemoveAt(lIndex)

    '                CheckTotals()

    '            End If

    '        Else

    '            Dim Obj As New clsBeCuadrilla_det_montacarga()
    '            Obj.IdMontacargaBodega = IdmontacargaBodegaGrid
    '            Obj.User_agr = AP.UsuarioAp.IdUsuario
    '            Obj.Fec_agr = Now
    '            Obj.User_mod = AP.UsuarioAp.IdUsuario
    '            Obj.Fec_mod = Now
    '            Obj.IsNew = True
    '            pListMC.Add(Obj)

    '            vTotalCostoHoraMontaCarga += vCostoHora

    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub

End Class