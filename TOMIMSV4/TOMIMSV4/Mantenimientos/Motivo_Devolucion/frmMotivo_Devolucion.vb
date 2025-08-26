Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository

Public Class frmMotivo_Devolucion

    Private DT As DataTable
    Private pListObjT As New List(Of clsTabla)

    Public pBeMotivoDevolucion As New clsBeMotivo_devolucion
    Public pBeMotivoDevolucionBodega As New clsBeMotivo_devolucion_bodega
    Public pBeMotivoDevolucionBodegaList As List(Of clsBeMotivo_devolucion_bodega)
    Public pIdMotivoDevolucion As Integer
    Public Delegate Sub Listar_Motivos_Devolucion()
    Public Property Listar As Listar_Motivos_Devolucion

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

    Private Sub Listar_Bodegas()

        Try

            Grid.BeginUpdate()

            If DT.Rows.Count > 0 Then

                Dim vIdBodega As Integer = 0

                For i As Integer = 0 To DT.Rows.Count - 1

                    vIdBodega = DT(i)(0)
                    Dim lRow As DataRow = DsMotivoDev.Data.NewRow
                    lRow.Item("IdBodega") = vIdBodega
                    lRow.Item("Bodega") = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)
                    lRow.Item("Asignar") = False

                    If TipoTrans.Editar Then

                        If pBeMotivoDevolucionBodegaList IsNot Nothing AndAlso pBeMotivoDevolucionBodegaList.Count > 0 Then

                            For Each Obj As clsBeMotivo_devolucion_bodega In pBeMotivoDevolucionBodegaList

                                If Obj.IdBodega = CInt(DT(i)(0)) AndAlso Obj.Activo Then
                                    lRow.Item("Asignar") = True
                                    lRow.Item("IdMotivoDevolucionBodega") = Obj.IdMotivoDevolucionBodega
                                End If

                                lRow.Item("IdInterno") = Obj.IdMotivoDevolucionBodega

                            Next

                        End If

                    End If

                    DsMotivoDev.Data.AddDataRow(lRow)

                Next

            End If

            Grid.EndUpdate()
            Grid.ForceInitialize()

            Dim ritem As RepositoryItemCheckEdit = TryCast(gridView1.Columns("Asignar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = gridView1.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = pBeMotivoDevolucionBodegaList.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                                  And b.IdMotivoDevolucion = pBeMotivoDevolucion.IdMotivoDevolucion)
                If lIndex > -1 Then

                    If ritem.Checked Then
                        pBeMotivoDevolucionBodegaList(lIndex).Activo = True
                    Else
                        pBeMotivoDevolucionBodegaList(lIndex).Activo = False
                    End If

                    pBeMotivoDevolucionBodegaList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pBeMotivoDevolucionBodegaList(lIndex).Fec_mod = Now

                Else

                    Dim Obj As New clsBeMotivo_devolucion_bodega() With {.IdBodega = Dr.Item("IdBodega"), .IdMotivoDevolucion = pBeMotivoDevolucion.IdMotivoDevolucion, .User_agr = AP.UsuarioAp.IdUsuario, .Fec_agr = Now, .User_mod = AP.UsuarioAp.IdUsuario, .Fec_mod = Now, .Activo = True}
                    pBeMotivoDevolucionBodegaList.Add(Obj)

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


    Private Sub ValidaBodegas()

        Try

            DsMotivoDev.Clear()

            DT = IMS.Listar_Bodegas()

            pBeMotivoDevolucionBodegaList = New List(Of clsBeMotivo_devolucion_bodega)
            pBeMotivoDevolucionBodegaList = clsLnMotivo_devolucion_bodega.GetAllByMotivoDevolucion(pBeMotivoDevolucion.IdMotivoDevolucion).ToList()

            Listar_Bodegas()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmMotivoDevolucion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("motivo_devolucion")

            If Not IMS.Listar_Empresas(cmbEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, cmbEmpresa.EditValue)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnMotivo_devolucion.MAXID
                    cmbEmpresa.Enabled = True

                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    cmbEmpresa.Enabled = True

                    TabDatos.TabPages.Remove(TabMotivoDevolucionBodega)

                Case TipoTrans.Editar

                    pBeMotivoDevolucion = clsLnMotivo_devolucion.GetSingle(pBeMotivoDevolucion.IdMotivoDevolucion)

                    lblCodigo.Text = pBeMotivoDevolucion.IdMotivoDevolucion
                    cmbEmpresa.EditValue = pBeMotivoDevolucion.IdEmpresa
                    cmbPropietario.EditValue = pBeMotivoDevolucion.IdPropietario
                    NombreTextEdit.Text = pBeMotivoDevolucion.Nombre
                    chkEsDetalle.Checked = pBeMotivoDevolucion.Es_detalle

                    User_agrTextEdit.Text = pBeMotivoDevolucion.User_agr
                    Fec_agrDateEdit.Text = pBeMotivoDevolucion.Fec_agr
                    User_modTextEdit.Text = pBeMotivoDevolucion.User_mod
                    Fec_modDateEdit.Text = pBeMotivoDevolucion.Fec_mod

                    chkActivo.Checked = pBeMotivoDevolucion.Activo

                    cmbEmpresa.Enabled = False
                    cmbPropietario.Enabled = False
                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    ValidaBodegas()

            End Select

            NombreTextEdit.Focus()

            Application.DoEvents()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            If Datos_Correctos() Then

                If XtraMessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Guardar()

                    If XtraMessageBox.Show("Se guardó el registro, ¿asignar bodegas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        TabDatos.TabPages.Add(TabMotivoDevolucionBodega)

                        ValidaBodegas()

                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = True
                        mnuEliminar.Enabled = True

                    Else
                        Close()
                    End If

                    If Listar IsNot Nothing Then
                        Listar.Invoke()
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


    Private Sub Guardar()

        Try

            Dim ObjN As New clsBeMotivo_devolucion() With {.IdMotivoDevolucion = clsLnMotivo_devolucion.MAXID(), .Empresa = New clsBeEmpresa()}
            ObjN.Empresa.IdEmpresa = cmbEmpresa.EditValue
            ObjN.IdEmpresa = cmbEmpresa.EditValue
            ObjN.Propietario = New clsBePropietarios
            ObjN.IdPropietario = cmbPropietario.EditValue
            ObjN.Propietario.IdPropietario = cmbPropietario.EditValue
            ObjN.Nombre = NombreTextEdit.Text
            ObjN.Activo = True
            ObjN.Es_detalle = chkEsDetalle.Checked
            ObjN.User_agr = AP.UsuarioAp.IdUsuario
            ObjN.Fec_agr = Now
            ObjN.User_mod = AP.UsuarioAp.IdUsuario
            ObjN.Fec_mod = Now

            pBeMotivoDevolucion.IdMotivoDevolucion = clsLnMotivo_devolucion.MAXID()
            pBeMotivoDevolucion.Empresa = New clsBeEmpresa
            pBeMotivoDevolucion.IdEmpresa = cmbEmpresa.EditValue
            pBeMotivoDevolucion.Empresa.IdEmpresa = cmbEmpresa.EditValue
            pBeMotivoDevolucion.Propietario = New clsBePropietarios
            pBeMotivoDevolucion.IdPropietario = cmbPropietario.EditValue
            pBeMotivoDevolucion.Propietario.IdPropietario = cmbPropietario.EditValue
            pBeMotivoDevolucion.Nombre = NombreTextEdit.Text
            pBeMotivoDevolucion.Activo = True
            pBeMotivoDevolucion.Es_detalle = chkEsDetalle.Checked
            pBeMotivoDevolucion.User_agr = AP.UsuarioAp.IdUsuario
            pBeMotivoDevolucion.Fec_agr = Now
            pBeMotivoDevolucion.User_mod = AP.UsuarioAp.IdUsuario
            pBeMotivoDevolucion.Fec_mod = Now

            clsLnMotivo_devolucion.Insertar(ObjN)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub


    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try
            If cmbEmpresa.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(NombreTextEdit.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()
            ElseIf NombreTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()
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


    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pBeMotivoDevolucion.Nombre = NombreTextEdit.Text.Trim()
                pBeMotivoDevolucion.Es_detalle = chkEsDetalle.Checked
                pBeMotivoDevolucion.Activo = chkActivo.Checked
                pBeMotivoDevolucion.User_mod = AP.UsuarioAp.IdUsuario
                pBeMotivoDevolucion.Fec_mod = Now

                pBeMotivoDevolucion.Activo = chkActivo.Checked

                Return clsLnMotivo_devolucion_bodega.ActualizarDatos(pBeMotivoDevolucion, pBeMotivoDevolucionBodegaList)

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


    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If Listar IsNot Nothing Then
                Listar.Invoke()
            End If
            Close()
        End If

    End Sub



    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Eliminar el motivo de devolución?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim ObjMD As New clsBeMotivo_devolucion() With {.IdMotivoDevolucion = pBeMotivoDevolucion.IdMotivoDevolucion, .Empresa = New clsBeEmpresa()}
                ObjMD.Empresa.IdEmpresa = pBeMotivoDevolucion.Empresa.IdEmpresa
                ObjMD.Propietario = New clsBePropietarios
                ObjMD.Propietario.IdPropietario = pBeMotivoDevolucion.Propietario.IdPropietario
                ObjMD.Nombre = pBeMotivoDevolucion.Nombre
                ObjMD.Es_detalle = pBeMotivoDevolucion.Es_detalle
                ObjMD.User_agr = pBeMotivoDevolucion.User_agr
                ObjMD.Fec_agr = pBeMotivoDevolucion.Fec_agr
                ObjMD.User_mod = AP.UsuarioAp.IdUsuario
                ObjMD.Fec_mod = Now

                If clsLnMotivo_devolucion.Eliminar(ObjMD) > 0 Then
                    XtraMessageBox.Show("Se ha eliminado el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If
                    Close()
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

    Private Sub cmdImportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImportarExcel.ItemClick
        Dim Carga As New frmCargaExcel() With {.pNombreMantenimiento = "Motivo Devolución", .pTipoMantenimiento = "MotivoDevolucion"}
        Carga.ShowDialog()
        Carga.Dispose()
    End Sub

    Private Sub gridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridView1.RowStyle

        Try

            gridView1.OptionsBehavior.Editable = True
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            gridView1.OptionsSelection.EnableAppearanceHideSelection = True
            gridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gridView1.Appearance.FocusedRow.ForeColor = Color.White
            gridView1.Appearance.SelectedRow.ForeColor = Color.White
            gridView1.Appearance.SelectedRow.Options.UseBackColor = True
            gridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmMotivo_Devolucion_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged

        If cmbPropietario.EditValue > 0 Then
            'IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario.EditValue, cmbEmpresa.EditValue)
        End If


    End Sub

End Class