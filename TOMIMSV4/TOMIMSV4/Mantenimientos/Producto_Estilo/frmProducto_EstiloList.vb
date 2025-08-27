Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmProducto_EstiloList

    Public objEstilo As New clsBeEstilo
    Public Property Modo As pModo
    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub frmProducto_EstiloList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Cargar_Datos()
        Catch ex As Exception
            SplashScreenManager.CloseForm()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Cargar_Datos()

        Try

            Dim table As New DataTable
            table = clsLnEstilo.Listar(chkActivos.Checked)

            If table IsNot Nothing AndAlso table.Rows.Count > 0 Then

                Dgrid.DataSource = table

                If (GridView1.Columns.Count <> 0) Then
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns()
                End If

                lblRegistros.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        cmdActualizar.Enabled = False
        Cargar_Datos()
        cmdActualizar.Enabled = True
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

        Dim reportHeader As String = vbNewLine & "Listado de Estilos"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle
                Dim objEstilo As New clsBeEstilo
                objEstilo = clsLnEstilo.GetSingle_By_IdEstilo(Dr.Item("IdEstilo"))

                Cierra_Instancia_Previa(frmProducto_Estilo)

                With frmProducto_Estilo
                    .Modo = frmProducto_Estilo.TipoTrans.Editar
                    .objEstilo = objEstilo
                    .InvokeListarEstilos = AddressOf Cargar_Datos
                    .MdiParent = MdiParent
                    .OpcionesMenu = OpcionesMenu()
                    .cmdGuardar.Enabled = OpcionesMenu.Modificar
                    .cmdActualizar.Enabled = OpcionesMenu.Modificar
                    .cmdEliminar.Enabled = OpcionesMenu.Eliminar
                    .WindowState = FormWindowState.Normal
                    .Show()
                    .Focus()
                End With

                GridView1.FocusedRowHandle = lSelectionIndex

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        Close()
    End Sub

    Private Sub cmdNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdNuevo.ItemClick
        Try
            Nuevo_Estilo()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Nuevo_Estilo()
        Try
            Cierra_Instancia_Previa(frmProducto_Estilo)

            With frmProducto_Estilo
                .Modo = frmProducto_Estilo.TipoTrans.Nuevo
                .InvokeListarEstilos = AddressOf Cargar_Datos
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu()
                .cmdGuardar.Enabled = OpcionesMenu.Modificar
                .cmdActualizar.Enabled = OpcionesMenu.Modificar
                .cmdEliminar.Enabled = OpcionesMenu.Eliminar
                .WindowState = FormWindowState.Normal
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

End Class