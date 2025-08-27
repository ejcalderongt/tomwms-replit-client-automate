Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraEditors

Public Class frmZona_Picking

    Private pListObjT As New List(Of clsTabla)
    Public gBeZonaPicking As New clsBeZona_picking

    Public Delegate Sub Listar_Zona_Pickinges()
    Public Property Listar As Listar_Zona_Pickinges

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

    Private Sub frmZona_Picking_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("Zona_Picking")

            Select Case Modo

                Case TipoTrans.Nuevo

                    lbl.Text = clsLnZona_picking.MaxID() + 1
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    mnuGuardar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False
                    tabDetalle.Visible = False

                Case TipoTrans.Editar

                    lbl.Text = gBeZonaPicking.IdZonaPicking
                    txtNombre.Text = gBeZonaPicking.Nombre.Trim()
                    txtDescripcion.Text = gBeZonaPicking.Descripcion
                    chkActivo.Checked = gBeZonaPicking.Activo

                    User_agrTextEdit.Text = gBeZonaPicking.User_agr
                    Fec_agrDateEdit.Text = gBeZonaPicking.Fec_agr
                    User_modTextEdit.Text = gBeZonaPicking.User_mod
                    Fec_modDateEdit.Text = gBeZonaPicking.Fec_mod

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    mnuEliminar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Eliminar, True)
                    mnuAsignacion.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)

                    tabDetalle.Visible = True

                    Listar_Tramos_Por_Zona_Picking()

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            gBeZonaPicking = New clsBeZona_picking() With {.IdZonaPicking = clsLnZona_picking.MaxID() + 1,
            .Nombre = txtNombre.Text.Trim(),
            .Descripcion = txtDescripcion.Text,
            .Activo = True,
            .User_agr = AP.UsuarioAp.IdUsuario,
            .Fec_agr = Now,
            .User_mod = AP.UsuarioAp.IdUsuario,
            .Fec_mod = Now,
            .IdEmpresa = AP.IdEmpresa}

            Guardar = clsLnZona_picking.Insertar(gBeZonaPicking) > 0

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

                gBeZonaPicking.Nombre = txtNombre.Text.Trim()
                gBeZonaPicking.Descripcion = txtDescripcion.Text
                gBeZonaPicking.Activo = chkActivo.Checked
                gBeZonaPicking.User_mod = AP.UsuarioAp.IdUsuario
                gBeZonaPicking.Fec_mod = Now
                Actualizar = clsLnZona_picking.Actualizar(gBeZonaPicking) > 0

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

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            If Datos_Correctos() Then

                If XtraMessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then

                        XtraMessageBox.Show("Se guardó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If Listar IsNot Nothing Then
                            Listar.Invoke()
                        End If

                        Modo = TipoTrans.Editar

                        tabDetalle.Visible = True

                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = True
                        mnuEliminar.Enabled = False
                        mnuAsignacion.Enabled = False

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

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Try
            If Actualizar() Then
                XtraMessageBox.Show("Se actualizó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If
                Close()
            End If
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If gBeZonaPicking.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If XtraMessageBox.Show("¿Eliminar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If clsLnZona_picking.Eliminar(gBeZonaPicking) > 0 Then
                        XtraMessageBox.Show("Se eliminó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Listar IsNot Nothing Then
                            Listar.Invoke()
                        End If
                        Close()
                    End If
                End If
            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("Zona_Picking", gBeZonaPicking.IdZonaPicking)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmZona_Picking_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub cmdNewPR_Click(sender As Object, e As EventArgs) Handles cmdNewPR.Click

        Try

            Dim BT As New frmBodegaTramoList()
            BT.IdZonaPicking = gBeZonaPicking.IdZonaPicking

            If BT.ShowDialog() = DialogResult.OK Then

                If Not Tiene_Inconsistencias(BT.lTramos) Then

                    If clsLnZona_picking_tramo.Insertar_Lista(gBeZonaPicking.IdZonaPicking, BT.lTramos) Then

                        Listar_Tramos_Por_Zona_Picking()

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

    Public Function Tiene_Inconsistencias(ByVal pListaZonaPickingTramo As List(Of clsBeZona_picking_tramo)) As Boolean

        Tiene_Inconsistencias = False

        Try

            Dim lTramosColumnasExistentesPorZonaPicking As New List(Of clsBeZona_picking_tramo)
            Dim lTramosNivelesExistentesPorZonaPicking As New List(Of clsBeZona_picking_tramo)


            For Each ZPT In pListaZonaPickingTramo

                If Not gBeZonaPicking.Lista_Zona_Picking_Tramo Is Nothing Then

                    lTramosColumnasExistentesPorZonaPicking = gBeZonaPicking.Lista_Zona_Picking_Tramo.FindAll(Function(x) x.IdBodega = ZPT.IdBodega _
                                                                                                      AndAlso x.IdArea = ZPT.IdArea _
                                                                                                      AndAlso x.IdSector = ZPT.IdSector _
                                                                                                      AndAlso x.IdTramo = ZPT.IdTramo _
                                                                                                      AndAlso (x.Min_x >= ZPT.Min_x AndAlso x.Max_x <= ZPT.Max_x))

                    If Not lTramosColumnasExistentesPorZonaPicking Is Nothing Then

                        If lTramosColumnasExistentesPorZonaPicking.Count > 0 Then

                            lTramosNivelesExistentesPorZonaPicking = lTramosColumnasExistentesPorZonaPicking.FindAll(Function(x) x.Min_y >= ZPT.Min_y AndAlso x.Max_y <= ZPT.Max_y)

                            If Not lTramosNivelesExistentesPorZonaPicking Is Nothing Then

                                If XtraMessageBox.Show("¿Se encontró una zona de traslape entre la información. ¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then

                                    Tiene_Inconsistencias = True

                                    Exit For

                                End If

                            End If

                        End If

                    End If

                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub Listar_Tramos_Por_Zona_Picking()


        Try

            Dim DT As New DataTable
            DT = clsLnZona_picking_tramo.Get_All_VW_Zona_Picking_Tramo_By_IdZonaPicking(gBeZonaPicking.IdZonaPicking)

            dgridTramosZonaPicking.DataSource = DT

            If (gvTramosPorZona.Columns.Count <> 0) Then

                Try

                    gvTramosPorZona.Columns("IdZonaPickingTramo").SummaryItem.SummaryType = SummaryItemType.Count
                    gvTramosPorZona.Columns("IdZonaPickingTramo").SummaryItem.DisplayFormat = "Registros: {0:n2}"

                    gvTramosPorZona.BestFitColumns()

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdDesactivarPresentacion_Click(sender As Object, e As EventArgs) Handles cmdDesactivarPresentacion.Click

        Try

            If (gvTramosPorZona.RowCount > 0) Then

                Dim Dr As DataRowView = gvTramosPorZona.GetFocusedRow()

                Dim BeZonaPickingTramo As New clsBeZona_picking_tramo

                BeZonaPickingTramo = clsLnZona_picking_tramo.Get_Single_By_IdZonaPickingTramo(Dr.Item("IdZonaPickingTramo"))

                Dim lSelectionIndex As Integer = gvTramosPorZona.FocusedRowHandle

                If Not BeZonaPickingTramo Is Nothing Then

                    If XtraMessageBox.Show("¿Eliminar Registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        Dim vResult As Integer = clsLnZona_picking_tramo.Eliminar(BeZonaPickingTramo)

                        If vResult > 0 Then

                            Listar_Tramos_Por_Zona_Picking()

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class