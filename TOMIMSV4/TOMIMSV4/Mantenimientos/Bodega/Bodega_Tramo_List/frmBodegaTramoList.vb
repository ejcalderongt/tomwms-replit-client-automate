Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmBodegaTramoList

    Public lTramos As New List(Of clsBeZona_picking_tramo)
    Public Property IdZonaPicking As Integer = 0
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Listar_Tramos()

        Try

            Dgrid.DataSource = clsLnBodega_tramo.Get_All_VW_By_IdBodega(cmbBodega.EditValue)

            If (GridView1.Columns.Count <> 0) Then

                Try

                    GridView1.Columns("IdBodega").Visible = False
                    GridView1.Columns("IdArea").Visible = False
                    GridView1.Columns("IdSector").Visible = False

                    GridView1.BestFitColumns()
                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try

            End If

            lblRegs.Caption = "Registros: " & GridView1.RowCount

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmBodegaTramoList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            Listar_Tramos()

            lTramos = New List(Of clsBeZona_picking_tramo)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        Listar_Tramos()
        mnuActualizar.Enabled = True
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub Nuevo_Muelle()

        Try

            Cierra_Instancia_Previa(frmBodega_Muelles)

            With frmBodega_Muelles
                .Modo = frmBodega_Muelles.TipoTrans.Nuevo
                .Listar = AddressOf Listar_Tramos
                .MdiParent = MdiParent
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
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

        mnuNuevo.Enabled = False

        Procesar_Tramos()

        mnuNuevo.Enabled = True

    End Sub

    Private Sub Procesar_Tramos()

        Try

            Dim selectedRowHandles As Integer() = GridView1.GetSelectedRows()

            If Not selectedRowHandles.Count = 0 Then

                If XtraMessageBox.Show("¿Agregar Tramos?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim I As Integer
                    Dim Dr As DataRow

                    Dim vIdBodega As Integer = 0
                    Dim vIdArea As Integer = 0
                    Dim vIdSector As Integer = 0
                    Dim vIdTramo As Integer = 0

                    Dim BeTramoZonaPicking As New clsBeZona_picking_tramo

                    For I = 0 To selectedRowHandles.Length - 1

                        Dim selectedRowHandle As Integer = selectedRowHandles(I)

                        If (selectedRowHandle >= 0) Then

                            Dr = GridView1.GetDataRow(selectedRowHandle)

                            If Not Dr Is Nothing Then

                                vIdBodega = Dr.Item("IdBodega")
                                vIdArea = Dr.Item("IdArea")
                                vIdSector = Dr.Item("IdSector")
                                vIdTramo = Dr.Item("IdTramo")

                                BeTramoZonaPicking = New clsBeZona_picking_tramo()
                                BeTramoZonaPicking.IdBodega = vIdBodega
                                BeTramoZonaPicking.IdArea = vIdArea
                                BeTramoZonaPicking.IdSector = vIdSector
                                BeTramoZonaPicking.IdTramo = vIdTramo
                                BeTramoZonaPicking.Min_x = txtColumnaDesde.EditValue
                                BeTramoZonaPicking.Max_x = txtColumnaHasta.EditValue
                                BeTramoZonaPicking.Min_y = txtNivelDesde.EditValue
                                BeTramoZonaPicking.Max_y = txtNivelHasta.EditValue
                                BeTramoZonaPicking.IdZonaPicking = IdZonaPicking

                                lTramos.Add(BeTramoZonaPicking)

                                Debug.Write("si")

                            End If

                        End If

                    Next

                    DialogResult = DialogResult.OK

                End If

            Else
                If XtraMessageBox.Show("No hay registros seleccionados. ¿Cerrar lista?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Close()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Procesa_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmBodega_Muelles)

                    With frmBodega_Muelles
                        .Modo = frmBodega_Muelles.TipoTrans.Editar
                        .Listar = AddressOf Listar_Tramos
                        .Muelle.IdMuelle = Dr.Item("Código")
                        .MdiParent = MdiParent
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
        Procesa_Registro()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Tramos()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        cmdImprimir.Enabled = False
        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")
        Imprimir_Vista()
        cmdImprimir.Enabled = True
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Muelles"

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

    Private Sub frmBodegaTramoList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Procesa_Registro()
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        Listar_Tramos()
    End Sub

End Class