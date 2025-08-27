Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository

Public Class frmEmpresa_Transporte

    Private DT As DataTable
    Private pListObjT As New List(Of clsTabla)
    Public pListObjPB As List(Of clsBeEmpresa_transporte_bodega)
    Public pIdEmpresaTransporte As Integer
    Public pObjBEJ As New clsBeEmpresa_transporte
    Public Delegate Sub listar_EmpresaTransporte()
    Public Property InvokeListarEmpresaTransporte As listar_EmpresaTransporte

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

            If DT.Rows.Count > 0 Then

                Dim vIdBodega As Integer = 0

                For i As Integer = 0 To DT.Rows.Count - 1

                    vIdBodega = DT(i)(0)
                    Dim lRow As DataRow = DsEmpresaTransporteBodega.Data.NewRow
                    lRow.Item("IdBodega") = vIdBodega
                    lRow.Item("Bodega") = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)
                    lRow.Item("Selección") = False
                    If TipoTrans.Editar Then
                        If pListObjPB IsNot Nothing AndAlso pListObjPB.Count > 0 Then
                            For Each Obj As clsBeEmpresa_transporte_bodega In pListObjPB
                                If Obj.IdBodega = CInt(DT(i)(0)) AndAlso Obj.Activo Then
                                    lRow.Item("Selección") = True
                                    lRow.Item("IdAsignacion") = Obj.IdAsignacion
                                End If
                                lRow.Item("IdInterno") = Obj.IdAsignacion
                            Next
                        End If
                    End If
                    DsEmpresaTransporteBodega.Data.AddDataRow(lRow)
                Next
            End If

            Grid.EndUpdate()
            Grid.ForceInitialize()
            Dim ritem As RepositoryItemCheckEdit = TryCast(gridView1.Columns("Selección").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = gridView1.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = pListObjPB.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                                  And b.IdEmpresaTransporte = pObjBEJ.IdEmpresaTransporte)
                If lIndex > -1 Then
                    If ritem.Checked Then
                        pListObjPB(lIndex).Activo = True
                    Else
                        pListObjPB(lIndex).Activo = False
                    End If
                    pListObjPB(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pListObjPB(lIndex).Fec_mod = Now
                Else
                    Dim Obj As New clsBeEmpresa_transporte_bodega() With {.IdBodega = Dr.Item("IdBodega"),
                        .IdEmpresaTransporte = pObjBEJ.IdEmpresaTransporte,
                        .User_agr = AP.UsuarioAp.IdUsuario,
                        .Fec_agr = Now,
                        .User_mod = AP.UsuarioAp.IdUsuario,
                        .Fec_mod = Now,
                        .Activo = True}
                    pListObjPB.Add(Obj)
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function ActualizarDatos()

        Dim lConnection As New SqlClient.SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlClient.SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction()

            clsLnEmpresa_transporte.Actualizar(pObjBEJ, lConnection, lTransaction)

            Dim lMax As Integer = clsLnEmpresa_transporte_bodega.MaxID()
            For Each Obj As clsBeEmpresa_transporte_bodega In pListObjPB
                If Obj.IdAsignacion = 0 Then
                    lMax += 1
                    Obj.IdAsignacion = lMax
                    clsLnEmpresa_transporte_bodega.Insertar(Obj, lConnection, lTransaction)
                Else
                    clsLnEmpresa_transporte_bodega.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()
            lConnection.Close()
            Return True

        Catch ex As Exception
            lTransaction.Rollback()
            lConnection.Close()
            Throw ex
        End Try

    End Function

    Private Sub ValidaBodegas()

        Try

            DsEmpresaTransporteBodega.Clear()
            DT = IMS.Listar_Bodegas()
            pListObjPB = New List(Of clsBeEmpresa_transporte_bodega)
            pListObjPB = clsLnEmpresa_transporte_bodega.GetAllByEmpresaTransporte(pObjBEJ.IdEmpresaTransporte)
            ListaBodegas()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmEmpresa_Transporte_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("empresa_transporte")

            If Not IMS.Listar_Empresas(cmbEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            Select Case Modo
                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnEmpresa_transporte.MaxID
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

                    TabDatos.TabPages.Remove(TabEmpresaTransporteBodega)

                Case TipoTrans.Editar

                    clsLnEmpresa_transporte.Obtener(pObjBEJ)
                    lblCodigo.Text = pObjBEJ.IdEmpresaTransporte
                    cmbEmpresa.EditValue = pObjBEJ.IdEmpresa
                    cmbEmpresa.Enabled = False
                    txtNombre.Text = pObjBEJ.Nombre
                    chkActivo.Checked = pObjBEJ.Activo

                    User_agrTextEdit.Text = pObjBEJ.User_agr
                    Fec_agrDateEdit.Text = pObjBEJ.Fec_agr
                    User_modTextEdit.Text = pObjBEJ.User_mod
                    Fec_modDateEdit.Text = pObjBEJ.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    ValidaBodegas()

            End Select

            Application.DoEvents()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Focus()
        txtNombre.Focus()

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        mnuGuardar.Enabled = False
        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar la Empresa Transporte?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If MessageBox.Show("¿Desea asignar Bodegas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        TabDatos.TabPages.Add(TabEmpresaTransporteBodega)
                        ValidaBodegas()
                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = True
                        mnuEliminar.Enabled = True

                    Else
                        InvokeListarEmpresaTransporte.Invoke
                        Close()

                    End If

                End If

            End If

        End If
        mnuGuardar.Enabled = True
    End Sub

    Private Function Guardar() As Boolean
        Guardar = False
        Try

            Dim ObjN As New clsBeEmpresa_transporte() With {.IdEmpresa = cmbEmpresa.EditValue,
                .IdEmpresaTransporte = clsLnEmpresa_transporte.MaxID(),
                .Nombre = txtNombre.Text.Trim(),
                .Activo = True,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now}

            Guardar = clsLnEmpresa_transporte.Insertar(ObjN) > 0
            pObjBEJ = ObjN

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Function Actualizar() As Boolean
        Actualizar = False
        Try
            If Datos_Correctos() Then

                pObjBEJ.Nombre = txtNombre.Text.Trim()
                pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
                pObjBEJ.Fec_mod = Now
                pObjBEJ.Activo = chkActivo.Checked

                Return ActualizarDatos()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbEmpresa.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarEmpresaTransporte.Invoke
            Close()
        End If
        mnuActualizar.Enabled = True
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False
            If MessageBox.Show("¿Desactivar la Empresa Transporte?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False

                If clsLnEmpresa_transporte.Actualizar(pObjBEJ) > 0 Then

                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarEmpresaTransporte.Invoke
                    Close()
                    frmEmpresa_TransporteList.Dgrid.Refresh()

                End If

            End If
            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("empresa_transporte", pObjBEJ.IdEmpresaTransporte)
        End Try

    End Sub

    Private Sub gridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridView1.RowStyle

        Try

            gridView1.OptionsBehavior.Editable = True
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = True

            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            gridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            gridView1.OptionsSelection.EnableAppearanceHideSelection = True
            gridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            gridView1.Appearance.FocusedRow.ForeColor = Color.White
            gridView1.Appearance.SelectedRow.ForeColor = Color.White

            gridView1.Appearance.SelectedRow.Options.UseBackColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub RibbonControl_Click(sender As Object, e As EventArgs) Handles RibbonControl.Click

    End Sub

    Private Sub frmEmpresa_Transporte_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class