Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository

Public Class frmMotivo_Anulacion

    Private DTMotivosAnulacion As DataTable
    Private pListObjT As New List(Of clsTabla)

    Public pBeMotivoAnulacionBodega As New clsBeMotivo_anulacion
    Public pBeMotivoAnulacion As New clsBeMotivo_anulacion
    Public pBeMotivoAnulacionBodegaList As List(Of clsBeMotivo_anulacion_bodega)
    Public pIdMotivoDevolucion As Integer
    Public Delegate Sub Listar_Motivos_Anulacion()
    Public Property Listar As Listar_Motivos_Anulacion

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

    Private Sub ListaBodegas()

        Try

            Grid.BeginUpdate()

            If DTMotivosAnulacion.Rows.Count > 0 Then

                Dim vIdBodega As Integer = 0

                For i As Integer = 0 To DTMotivosAnulacion.Rows.Count - 1

                    vIdBodega = DTMotivosAnulacion(i)(0)
                    Dim lRow As DataRow = DsMotivoAnulacion.Data.NewRow
                    lRow.Item("IdBodega") = vIdBodega
                    lRow.Item("Bodega") = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)
                    lRow.Item("Asignar") = False

                    If TipoTrans.Editar Then

                        If pBeMotivoAnulacionBodegaList IsNot Nothing AndAlso pBeMotivoAnulacionBodegaList.Count > 0 Then

                            For Each Obj As clsBeMotivo_anulacion_bodega In pBeMotivoAnulacionBodegaList

                                If Obj.IdBodega = CInt(DTMotivosAnulacion(i)(0)) AndAlso Obj.Activo Then
                                    lRow.Item("Asignar") = True
                                    lRow.Item("IdMotivoAnulacionBodega") = Obj.IdMotivoAnulacionBodega
                                End If

                                lRow.Item("IdInterno") = Obj.IdMotivoAnulacionBodega

                            Next

                        End If

                    End If

                    DsMotivoAnulacion.Data.AddDataRow(lRow)

                Next

            End If

            Grid.EndUpdate()
            Grid.ForceInitialize()

            Dim ritem As RepositoryItemCheckEdit = TryCast(GridView1.Columns("Asignar").RealColumnEdit, RepositoryItemCheckEdit)
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

            If ritem IsNot Nothing Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = pBeMotivoAnulacionBodegaList.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                                  And b.IdMotivoAnulacion = pBeMotivoAnulacionBodega.IdMotivoAnulacion)
                If lIndex > -1 Then

                    If ritem.Checked Then
                        pBeMotivoAnulacionBodegaList(lIndex).Activo = True
                    Else
                        pBeMotivoAnulacionBodegaList(lIndex).Activo = False
                    End If

                    pBeMotivoAnulacionBodegaList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pBeMotivoAnulacionBodegaList(lIndex).Fec_mod = Now

                Else

                    Dim Obj As New clsBeMotivo_anulacion_bodega() With {.IdBodega = Dr.Item("IdBodega"), .IdMotivoAnulacion = pBeMotivoAnulacionBodega.IdMotivoAnulacion, .User_agr = AP.UsuarioAp.IdUsuario, .Fec_agr = Now, .User_mod = AP.UsuarioAp.IdUsuario, .Fec_mod = Now, .Activo = True}
                    pBeMotivoAnulacionBodegaList.Add(Obj)

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

            DsMotivoAnulacion.Clear()

            DTMotivosAnulacion = IMS.Listar_Bodegas()

            pBeMotivoAnulacionBodegaList = New List(Of clsBeMotivo_anulacion_bodega)
            pBeMotivoAnulacionBodegaList = clsLnMotivo_anulacion_bodega.GetAllByMotivoAnulacion(pBeMotivoAnulacionBodega.IdMotivoAnulacion).ToList()

            ListaBodegas()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmMotivoAnulacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("motivo_anulacion")

            If Not IMS.Listar_Empresas(cmbEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnMotivo_anulacion.MaxIdMotivoAnulacion
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

                    TabDatos.TabPages.Remove(TabMotivoAnulacionBodega)

                Case TipoTrans.Editar

                    pBeMotivoAnulacion = clsLnMotivo_anulacion.GetSingle(pBeMotivoAnulacionBodega.IdMotivoAnulacion)

                    lblCodigo.Text = pBeMotivoAnulacion.IdMotivoAnulacion
                    cmbEmpresa.EditValue = pBeMotivoAnulacion.IdEmpresa
                    NombreTextEdit.Text = pBeMotivoAnulacion.Nombre

                    User_agrTextEdit.Text = pBeMotivoAnulacion.User_agr
                    Fec_agrDateEdit.Text = pBeMotivoAnulacion.Fec_agr
                    User_modTextEdit.Text = pBeMotivoAnulacion.User_mod
                    Fec_modDateEdit.Text = pBeMotivoAnulacion.Fec_mod

                    chkActivo.Checked = pBeMotivoAnulacion.Activo

                    pBeMotivoAnulacionBodega.IdMotivoAnulacion = pBeMotivoAnulacion.IdMotivoAnulacion
                    pBeMotivoAnulacionBodega.IdEmpresa = pBeMotivoAnulacion.IdEmpresa
                    pBeMotivoAnulacionBodega.Nombre = pBeMotivoAnulacion.Nombre

                    pBeMotivoAnulacionBodega.User_agr = pBeMotivoAnulacion.User_agr
                    pBeMotivoAnulacionBodega.Fec_agr = pBeMotivoAnulacion.Fec_agr
                    pBeMotivoAnulacionBodega.User_mod = pBeMotivoAnulacion.User_mod
                    pBeMotivoAnulacionBodega.Fec_mod = pBeMotivoAnulacion.Fec_mod
                    pBeMotivoAnulacionBodega.Activo = pBeMotivoAnulacion.Activo

                    cmbEmpresa.Enabled = False
                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    ValidaBodegas()

            End Select

            NombreTextEdit.Focus()

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

                If XtraMessageBox.Show("¿Guardar Motivo Anulación?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then

                        If XtraMessageBox.Show("Se guardó el registro, ¿asignar bodegas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            TabDatos.TabPages.Add(TabMotivoAnulacionBodega)

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

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim ObjN As New clsBeMotivo_anulacion() With {.IdMotivoAnulacion = clsLnMotivo_anulacion.MaxIdMotivoAnulacion(),
                .IdEmpresa = cmbEmpresa.EditValue,
                .Nombre = NombreTextEdit.Text,
                .Activo = True,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now}

            pBeMotivoAnulacionBodega.IdMotivoAnulacion = ObjN.IdMotivoAnulacion

            Guardar = clsLnMotivo_anulacion.Insertar(ObjN) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbEmpresa.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(NombreTextEdit.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()
            ElseIf NombreTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

                pBeMotivoAnulacionBodega.IdEmpresa = cmbEmpresa.EditValue
                pBeMotivoAnulacionBodega.Nombre = NombreTextEdit.Text.Trim()

                pBeMotivoAnulacionBodega.User_mod = AP.UsuarioAp.IdUsuario
                If pBeMotivoAnulacionBodega.User_agr Is Nothing Then pBeMotivoAnulacionBodega.User_agr = AP.UsuarioAp.IdUsuario
                If pBeMotivoAnulacionBodega.Fec_agr = Nothing Then pBeMotivoAnulacionBodega.Fec_agr = Now

                pBeMotivoAnulacionBodega.Fec_mod = Now
                pBeMotivoAnulacionBodega.Activo = chkActivo.Checked

                Actualizar = clsLnMotivo_anulacion_bodega.ActualizarDatos(pBeMotivoAnulacionBodega, pBeMotivoAnulacionBodegaList)

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
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If Listar IsNot Nothing Then
                Listar.Invoke()
            End If
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Eliminiar el motivo anulación?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim ObjMD As New clsBeMotivo_anulacion() With {.IdMotivoAnulacion = pBeMotivoAnulacionBodega.IdMotivoAnulacion, .IdEmpresa = pBeMotivoAnulacionBodega.IdEmpresa, .Nombre = pBeMotivoAnulacionBodega.Nombre, .User_agr = pBeMotivoAnulacionBodega.User_agr, .Fec_agr = pBeMotivoAnulacionBodega.Fec_agr, .User_mod = pBeMotivoAnulacionBodega.User_mod, .Fec_mod = pBeMotivoAnulacionBodega.Fec_mod}

                If clsLnMotivo_anulacion.Eliminar(ObjMD) > 0 Then

                    XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = True
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = True
            GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.Options.UseBackColor = True
            GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmMotivo_Anulacion_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class