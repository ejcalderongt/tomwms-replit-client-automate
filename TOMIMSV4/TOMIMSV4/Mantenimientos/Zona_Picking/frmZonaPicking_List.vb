Imports System.Reflection
Imports System.Threading.Tasks
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmZona_Picking_List

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public gBeZona_Picking As New clsBeZona_picking

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmZona_Picking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Listar_Zona_Pickinges()
    End Sub

    Private Sub Listar_Zona_Pickinges()

        Try

            Dgrid.DataSource = Nothing

            Dim lista As New List(Of clsBeZona_picking)

            lista = clsLnZona_picking.Get_All(chkActivos.Checked)

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("Zona_Picking")
                DT.Columns.Add("IdZonaPicking", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))
                DT.Columns.Add("Descripcion", GetType(String))

                Parallel.ForEach(lista, Sub(ByVal Obj)
                                            SyncLock DT
                                                DT.Rows.Add(Obj.IdZonaPicking,
                                                            Obj.Nombre,
                                                            Obj.Descripcion)
                                            End SyncLock
                                        End Sub)

                Dgrid.DataSource = DT

                If (GridView1.Columns.Count <> 0) Then

                    Try
                        GridView1.Columns("IdZonaPicking").Caption = "Código"
                        GridView1.BestFitColumns()
                    Catch ex As Exception
                        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End Try
                End If

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Nuevo_Registro()

        Try

            Cierra_Instancia_Previa(frmZona_Picking)

            With frmZona_Picking
                .Modo = frmZona_Picking.TipoTrans.Nuevo

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                    .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                    .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                    .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                End If

                .Listar = AddressOf Listar_Zona_Pickinges
                .MdiParent = MdiParent
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        Nuevo_Registro()
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                gBeZona_Picking = clsLnZona_picking.GetSingle(Dr.Item("IdZonaPicking"))

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmZona_Picking)

                    With frmZona_Picking
                        .Modo = frmZona_Picking.TipoTrans.Editar

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                            .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                            .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                            .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                        End If

                        .MdiParent = MdiParent
                        .gBeZonaPicking = gBeZona_Picking
                        .Listar = AddressOf Listar_Zona_Pickinges
                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Zona_Pickinges()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Zona_Pickinges()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {"Páginas: [Page # of Pages #] "})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", "Fecha: [Date Printed] [Time Printed] "})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = Dgrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            SplashScreenManager.CloseForm(False)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & Text

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
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

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmZona_Picking_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class