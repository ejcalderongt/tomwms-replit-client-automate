Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmReglaUbicPrioList

    Public BeReglaUbicPrioEnc As New clsBeRegla_ubic_prio_enc

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub ListarReglas()

        Try

            Dim lReglaUbicSel As New List(Of clsBeRegla_ubic_prio_enc)
            lReglaUbicSel = clsLnRegla_ubic_prio_enc.Get_All_By_IdBodega(AP.IdBodega, chkActivos.Checked)

            Dim Data = (From P In lReglaUbicSel Select New With {.Código = P.IdReglaUbicPrioEnc, .Nombre = P.Nombre}).ToList

            Dgrid.DataSource = Data

            lblRegistro.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            Try

                GridView1.OptionsView.ColumnAutoWidth = False

                If GridView1.Columns.Count > 0 Then
                    GridView1.BestFitColumns()
                End If

            Catch ex As Exception

            End Try

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmTipoTarima_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListarReglas()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        ListarReglas()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub Nueva_Regla_Prio()

        Try

            Cierra_Instancia_Previa(frmReglaUbicPrio)

            With frmReglaUbicPrio
                .Modo = frmReglaUbicPrio.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu()
                .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                .InvokeListarReglasPrio = AddressOf ListarReglas
                .WindowState = FormWindowState.Normal
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
        Nueva_Regla_Prio()
    End Sub

    Private Sub EditItem(Byval pObjBEJ As clsBeRegla_ubic_prio_enc)

        Try

            Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

            Cierra_Instancia_Previa(frmReglaUbicPrio)

            With frmReglaUbicPrio
                .Modo = frmReglaUbicPrio.TipoTrans.Editar
                .BeReglaUbicPrioEnc = pObjBEJ
                .InvokeListarReglasPrio = AddressOf ListarReglas
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu()
                .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                .WindowState = FormWindowState.Normal
                .Show()
                .Focus()
            End With

            GridView1.FocusedRowHandle = lSelectionIndex

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As New Object
                Dr = GridView1.GetFocusedRow

                BeReglaUbicPrioEnc.IdReglaUbicPrioEnc = Dr.Código
                clsLnRegla_ubic_prio_enc.GetSingle(BeReglaUbicPrioEnc)

                If Modo = pModo.Lista Then
                    EditItem(BeReglaUbicPrioEnc)
                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If

                'dr.Dispose()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        ListarReglas()
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

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de prioridad de reglas"

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

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        'Dim Regla As New frmReglaUbicacion_List() With {.Modo = frmReglaUbicacion_List.pModo.Lista, .reglaid = 1}
        'Regla.ShowDialog()
        'Regla.Dispose()
    End Sub

    Private Sub frmReglaUbicPrioList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

End Class