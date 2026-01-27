Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmInventarioList

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public Call_Bind_Listar_Inventarios As New MethodInvoker(AddressOf Cargar)

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Public Sub Cargar()

        Try

            grdInventario.DataSource = Nothing

            Dim lista As New List(Of clsBeTrans_inv_enc)
            Dim gBeTransInvEnc As New clsBeTrans_inv_enc
            Dim listaInvDet As New List(Of clsBeTrans_inv_detalle)

            lista = clsLnTrans_inv_enc.Get_All_By_Rango_Fechas(dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date, AP.IdBodega, chkActivo.Checked)

            For Each objP As clsBeTrans_inv_enc In lista

                listaInvDet = clsLnTrans_inv_detalle.Get_All_By_IdInventarioEnc(objP.Idinventarioenc)

                If listaInvDet.Count > 0 And objP.Estado = "Nuevo" Then

                    objP.Estado = "Procesando"
                    clsLnTrans_inv_enc.Actualizar(objP)

                End If

            Next

            '#EJC20180813_0929AM: Porqué se vuelve a llenar la lista de inventario por fecha si arriba se llena?
            'lista = clsLnTrans_inv_enc.Get_All_By_Rango_Fechas(dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date)

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("InventarioLista")
                DT.Columns.Add(("Código"), GetType(Integer))
                DT.Columns.Add(("Propietario"), GetType(String))
                DT.Columns.Add(("Bodega"), GetType(String))
                DT.Columns.Add(("Tipo Inventario"), GetType(String))
                DT.Columns.Add(("Tipo Conteo"), GetType(String))
                DT.Columns.Add(("Doble Verificación"), GetType(Boolean))
                DT.Columns.Add(("Incial"), GetType(Boolean))
                DT.Columns.Add(("Regularizado"), GetType(Boolean))
                DT.Columns.Add(("Estado"), GetType(String))
                DT.Columns.Add(("Fecha"), GetType(Date))
                DT.Columns.Add(("HoraInicio"), GetType(TimeSpan))
                DT.Columns.Add(("HoraFin"), GetType(TimeSpan))

                For Each Obj As clsBeTrans_inv_enc In lista

                    DT.Rows.Add(Obj.Idinventarioenc, Obj.Propietario.Nombre_comercial, Obj.Bodega.Nombre, Obj.TipoInv.Descripcion, Obj.TipoConteo.Descripcion, Obj.Doble_verificacion,
                                Obj.Inicial, Obj.Regularizado, Obj.Estado, Obj.Fecha, Obj.Hora_ini.TimeOfDay, Obj.Hora_fin.TimeOfDay)
                Next

                grdInventario.DataSource = DT

            End If

            lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            Try
                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.BestFitColumns()
            Catch ex As Exception
            End Try

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Nuevo_Inventario()

        Try

            Cierra_Instancia_Previa(frmInventario)

            With frmInventario
                .Modo = frmInventario.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .InvokeListarInventario = AddressOf Cargar
                .WindowState = FormWindowState.Maximized

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                    .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                    .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                    .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                End If

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

    Private Sub cmdNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdNuevo.ItemClick
        Nuevo_Inventario()
    End Sub

    Private Sub dtpFechaFin_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaFin.ValueChanged

        Try

            If dtpFechaFin.Value < dtpFechaInicio.Value Then
                XtraMessageBox.Show("La fecha fin debe de ser mayor a fecha inicio", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpFechaFin.Value = Now
            End If

            If Me.dtpFechaInicio.Value > Me.dtpFechaFin.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            End If

            Cargar()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub dtpFechaInicio_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaInicio.ValueChanged

        Try

            Cargar()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

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

    Private Sub Procesar_Registro()

        Try

            If GridView1.RowCount > 0 Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim Obj As New clsBeTrans_inv_enc

                Obj = clsLnTrans_inv_enc.Get_Single_By_IdInventarioEnc(Dr.Item("Código"))
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmInventario)

                    With frmInventario
                        .Modo = frmInventario.TipoTrans.Editar
                        .gBeTransInvEnc = Obj
                        .InvokeListarInventario = AddressOf Cargar
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Maximized

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                            .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                            .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                            .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                        End If

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

    Private Sub grdInventario_DoubleClick(sender As Object, e As EventArgs) Handles grdInventario.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick
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
            printLink.Component = grdInventario
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Inventarios"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub grdInventario_KeyDown(sender As Object, e As KeyEventArgs) Handles grdInventario.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmInventarioList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub cmdPlantilla_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdPlantilla.ItemClick

        Dim vRutaArchivo As String = CurDir() & "\Mantenimientos\plantillas\WMS_plantilla_Inventario_Inicial.xlsx"

        Try
            If File.Exists(vRutaArchivo) Then
                ' Crear un nuevo SaveFileDialog
                Using saveDialog As New SaveFileDialog()
                    saveDialog.Title = "Guardar plantilla de inventario inicial"
                    saveDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx"
                    saveDialog.FileName = "WMS_plantilla_Inventario_Inicial.xlsx"
                    ' Mostrar el cuadro de diálogo de guardado
                    If saveDialog.ShowDialog() = DialogResult.OK Then
                        ' Copiar el archivo a la ruta seleccionada por el usuario
                        File.Copy(vRutaArchivo, saveDialog.FileName, True)
                        XtraMessageBox.Show("Archivo guardado exitosamente en: " & saveDialog.FileName, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            Else
                XtraMessageBox.Show("No existe el formato en: " & vRutaArchivo, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmInventarioList_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Cargar()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
End Class