Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmMotivo_CorrecionList

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public BeMotivoCorreccion As New clsBeTrans_oc_pol_motivo_correccion_bodega

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmMotivo_CorrecionList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Listar_Motivos_Correccione_Poliza()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Motivos_Correccione_Poliza()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Motivos_Correccione_Poliza()
    End Sub

    Private Sub Listar_Motivos_Correccione_Poliza()

        Try

            Dim lista As New List(Of clsBeTrans_oc_pol_motivo_correccion)

            If Modo = pModo.Seleccion Then
                lista = clsLnTrans_oc_pol_motivo_correccion.Get_All_By_IdBodega(BeMotivoCorreccion.IdBodega).ToList()
            Else
                lista = clsLnTrans_oc_pol_motivo_correccion.Get_All_Filtro(chkActivos.Checked).ToList()
            End If

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("MotivoCorreccion")
                DT.Columns.Add("Código", GetType(Integer))
                DT.Columns.Add("Empresa", GetType(String))
                DT.Columns.Add("Motivo Corrección", GetType(String))

                For Each BeTransOcPolMotivoCorreccion As clsBeTrans_oc_pol_motivo_correccion In lista
                    DT.Rows.Add(BeTransOcPolMotivoCorreccion.IdMotivoCorreccion,
                                BeTransOcPolMotivoCorreccion.Empresa.Nombre,
                                BeTransOcPolMotivoCorreccion.Nombre)
                Next

                Dgrid.DataSource = DT

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                Try
                    GridView1.OptionsView.ColumnAutoWidth = False
                    If GridView1.Columns.Count > 0 Then
                        GridView1.BestFitColumns()
                    End If
                Catch ex As Exception

                End Try

            Else
                Dgrid.DataSource = Nothing
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick
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
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
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

    Private Sub mnuExportar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportar.ItemClick

        Try

            Exportar_Grid_A_Excel(Dgrid, "WMS_Lista_Motivos_Correccion.xlsx")

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Exportar_Grid_A_Excel(ByRef dGrid As GridControl, ByVal NomArchivo As String)

        Try

            Try

                Dim myStream As Stream
                Dim saveFileDialog1 As New SaveFileDialog()

                saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
                saveFileDialog1.FilterIndex = 1
                saveFileDialog1.RestoreDirectory = True
                saveFileDialog1.FileName = NomArchivo

                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    myStream = saveFileDialog1.OpenFile()
                    If (myStream IsNot Nothing) Then
                        ' Code to write the stream goes here.
                        dGrid.ExportToXlsx(myStream)
                        myStream.Close()
                    End If
                End If

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

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

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        Nuevo_Motivo_Correccion()
    End Sub

    Private Sub Nuevo_Motivo_Correccion()

        Try

            Cierra_Instancia_Previa(frmMotivo_Correccion)

            With frmMotivo_Correccion
                .Modo = frmMotivo_Correccion.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu()
                .mnuGuardar.Enabled = OpcionesMenu.Modificar
                .mnuActualizar.Enabled = OpcionesMenu.Modificar
                .mnuEliminar.Enabled = OpcionesMenu.Eliminar
                .Listar = AddressOf Listar_Motivos_Correccione_Poliza
                .Show()
                .Focus()
            End With

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle
                Dim IdMotivoCorreccion As Integer = Dr.Item("Código")

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmMotivo_Correccion)

                    With frmMotivo_Correccion
                        .Modo = frmMotivo_Correccion.TipoTrans.Editar
                        .MdiParent = MdiParent
                        .pBeMotivoCorreccion.IdMotivoCorreccion = IdMotivoCorreccion
                        .Listar = AddressOf Listar_Motivos_Correccione_Poliza
                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    BeMotivoCorreccion.IdMotivoCorreccion = IdMotivoCorreccion
                    clsLnTrans_oc_pol_motivo_correccion.GetIdMotivoCorreccionBodega(BeMotivoCorreccion)
                    Me.DialogResult = DialogResult.OK

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

End Class