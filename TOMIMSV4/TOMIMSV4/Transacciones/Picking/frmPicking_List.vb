Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmPicking_List

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property IsLoading As Boolean = False
    Public Property vNombreArchivoLayOutGrid As String = "frmPicking_List.vb"
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmPicking_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ListarPicking()

    End Sub

    Private Sub ListarPicking()

        Try

            IsLoading = True

            Dgrid.DataSource = clsLnTrans_picking_enc.Get_All_By_IdBodega(chkActivos.Checked,
                                                                          dtpFechaDel.Value,
                                                                          dtpFechaAl.Value,
                                                                          AP.IdBodega)

            If GridView1.Columns.Count > 0 Then

                GridView1.Columns("activo").Visible = False
                GridView1.Columns("IdBodega").Visible = False

            End If

            If GridView1.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

            Try

                GridView1.OptionsView.ShowFooter = False
                GridView1.OptionsView.ColumnAutoWidth = False

                If GridView1.Columns.Count > 0 Then
                    If GridView1.RowCount > 0 Then
                        GridView1.BestFitColumns()
                    End If
                End If

            Catch ex As Exception

            End Try

            Set_LayOut_Grid()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            IsLoading = False
        End Try

    End Sub

    Private Sub Nuevo_Picking()

        Try

            Cierra_Instancia_Previa(frmPicking)

            With frmPicking
                .Modo = frmPicking.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .InvokeListarPicking = AddressOf ListarPicking
                .WindowState = FormWindowState.Maximized

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                    .mnuGuardar.Enabled = OpcionesMenu.Modificar
                    .mnuActualizar.Enabled = OpcionesMenu.Modificar
                    .mnuEliminar.Enabled = OpcionesMenu.Eliminar
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

            '#MECR24102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        Nuevo_Picking()
    End Sub

    Private Sub Procesar_Registro()

        Try

            If GridView1.RowCount > 0 Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Picking")

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmPicking)

                    Dim Pick As New frmPicking
                    Pick.Text = "Picking " & Dr.Item("Código")
                    Pick.Modo = frmPicking.TipoTrans.Editar
                    Pick.BePickingEnc = clsLnTrans_picking_enc.GetSingle(Dr.Item("Código"))
                    Pick.InvokeListarPicking = AddressOf ListarPicking
                    Pick.MdiParent = MdiParent
                    Pick.WindowState = FormWindowState.Normal

                    If OpcionesMenu IsNot Nothing Then
                        Pick.OpcionesMenu = OpcionesMenu
                        Pick.mnuGuardar.Enabled = OpcionesMenu.Modificar
                        Pick.mnuActualizar.Enabled = OpcionesMenu.Modificar
                        Pick.mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    Pick.Show()
                    Pick.Focus()

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
        ListarPicking()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        ListarPicking()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Picking"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            ListarPicking()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR24102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            ListarPicking()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR24102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pStackTrace:=ex.StackTrace)

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

            '#MECR24102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmPicking_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub GridView_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        Try

            If IsLoading Then Exit Sub

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR24102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            clsLnConfiguracion_usuario_enc.Delete_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid)

            XtraMessageBox.Show("Diseño de grid eliminado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            '#MECR24102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Set_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                 vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuExportarExcel.ItemClick


        Try

            Exportar_Grid_A_Excel(Dgrid, "WMS_ListaPickings_" & DateTime.Now.ToString("yyyy_MM_dd") & ".xlsx")

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
            '#MECR24102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pUserAgr:=AP.UsuarioAp.IdUsuario,
                                                  pStackTrace:=ex.StackTrace)
        End Try

    End Sub

End Class