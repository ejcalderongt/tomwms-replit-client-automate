Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraPrinting

Public Class frmMontacargaList

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmMontacargaList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Lista de Montacargas en " + AP.NomEmpresa
        'Cargar unicamente las montacargas pertenecientes a la empresa en la que se encuentra registrado
        cargarListadeMontacargaxEmpresa()
    End Sub

    Private Sub cargarListadeMontacargaxEmpresa()
        Try

            Dim lista As New List(Of clsBeMontacarga)
            lista = clsLnMontacarga.GetMontaCargas(AP.IdEmpresa, txtFiltro.Text)

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("Montacarga")
                DT.Columns.Add("ID", GetType(Integer))
                DT.Columns.Add("IDEMPRESA", GetType(Integer))
                DT.Columns.Add("NOMBRE", GetType(String))
                DT.Columns.Add("MODELO", GetType(String))
                DT.Columns.Add("SERIE", GetType(String))
                DT.Columns.Add("CAPACIDAD BASICA", GetType(Double))
                DT.Columns.Add("DESPLAZAMIENTO MOTOR", GetType(Double))
                DT.Columns.Add("TIPO COMBUSTIBLE", GetType(String))
                DT.Columns.Add("TIPO MONTACARGA", GetType(String))

                DT.Columns.Add("FECHA COMPRA", GetType(DateTime))
                DT.Columns.Add("INICIA OPERACION", GetType(DateTime))
                DT.Columns.Add("PROXIMO MANTENIMIENTO", GetType(DateTime))

                For Each Obj As clsBeMontacarga In lista
                    DT.Rows.Add(Obj.IdMontacarga, Obj.IdEmpresa, Obj.Nombre, Obj.Modelo, Obj.Serie, Obj.Capacidad_basica, Obj.Desplazamiento_motor, Obj.Tipo_combustible, Obj.Tipo_montacarga, Obj.Fecha_compra, Obj.Fecha_inicio_operaciones, Obj.Proximo_mantenimiento)
                Next

                Dgrid.DataSource = DT

                If GridView1.RowCount > 0 Then
                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns()
                End If

            Else
                Dgrid.DataSource = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtFiltro_EditValueChanged(sender As Object, e As EventArgs) Handles txtFiltro.EditValueChanged
        cargarListadeMontacargaxEmpresa()
    End Sub

    Private Sub Nuevo_Montacarga()

        Try

            Cierra_Instancia_Previa(frmMontacarga)

            With frmMontacarga
                .Modo = frmMontacarga.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu
                .btnGuardar.Enabled = .OpcionesMenu.Modificar
                .btnActualizar.Enabled = .OpcionesMenu.Modificar
                .btnEliminar.Enabled = .OpcionesMenu.Eliminar
                .InvokeListarMontacarga = AddressOf cargarListadeMontacargaxEmpresa
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
        Nuevo_Montacarga()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New PrintingSystem()
            Dim printLink As New PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = Dgrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As CreateAreaEventArgs)

        Dim reportHeader As String = String.Format("{0}Listado de Montacarga de {1}", vbNewLine, AP.NomEmpresa)

        e.Graph.StringFormat = New BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None)

    End Sub

    Private Sub cmdImportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImportarExcel.ItemClick
        Dim Carga As New frmCargaExcel() With {.pNombreMantenimiento = "Montacarga",
            .pTipoMantenimiento = "Montacarga",
            .Listar = New frmCargaExcel.Operar(AddressOf cargarListadeMontacargaxEmpresa)}
        Carga.ShowDialog()
        Carga.Dispose()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        cargarListadeMontacargaxEmpresa()
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmMontacarga)

                    With frmMontacarga
                        .Modo = frmMontacarga.TipoTrans.Editar
                        .BeMontacarga.IdMontacarga = Dr.Item("ID")
                        .InvokeListarMontacarga = AddressOf cargarListadeMontacargaxEmpresa
                        .MdiParent = MdiParent
                        .OpcionesMenu = OpcionesMenu
                        .btnGuardar.Enabled = .OpcionesMenu.Modificar
                        .btnActualizar.Enabled = .OpcionesMenu.Modificar
                        .btnEliminar.Enabled = .OpcionesMenu.Eliminar
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
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

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
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

    Private Sub frmMontacargaList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub Dgrid_Click(sender As Object, e As EventArgs) Handles Dgrid.Click

    End Sub
End Class