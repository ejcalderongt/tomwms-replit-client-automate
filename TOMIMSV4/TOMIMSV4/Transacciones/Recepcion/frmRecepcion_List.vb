Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmRecepcion_List

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property gBeRecepcion As New clsBeTrans_re_enc
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmOrdenCompraRecepcion_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Listar_Recepciones()
    End Sub

    Private Sub Listar_Recepciones()

        Try

            Dim DT As New DataTable("Result")

            DT = clsLnTrans_re_enc.Get_All_By_IdBodega(chkActivos.Checked,
                                                       dtpFechaDel.Value,
                                                       dtpFechaAl.Value,
                                                       AP.IdBodega)

            Dgrid.DataSource = DT

            If GridView1.Columns.Count > 0 Then
                Try '#CKFK 20180411 10:33 Se colocó esta línea en un try para evitar el mensaje de error

                    Dim Obj = GridView1.Columns("Activo")
                    If Not Obj Is Nothing Then
                        GridView1.Columns("Activo").Visible = False
                    Else
                        GridView1.Columns("activo").Visible = False
                    End If

                    GridView1.Columns("IdBodega").Visible = False
                Catch ex As Exception
                    Debug.Print("Error: " & ex.Message)
                End Try
            End If

            If GridView1.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.BestFitColumns()
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Nuevo_Registro()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando recepción")

            Cierra_Instancia_Previa(frmRecepcion)

            With frmRecepcion
                .Modo = frmRecepcion.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .Listar = AddressOf Listar_Recepciones
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
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=AP.IdEmpresa,
                                                 pIdBodega:=AP.IdBodega,
                                                 pIdUsuarioAgr:=AP.UsuarioAp.IdUsuario,
                                                 pIdRecEnc:=gBeRecepcion.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        Nuevo_Registro()
    End Sub

    Public Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                gBeRecepcion = clsLnTrans_re_enc.GetSingle(Dr.Item("Código"))

                If Modo = pModo.Lista Then

                    Dgrid.Enabled = False

                    Cierra_Instancia_Previa(frmRecepcion)

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                    With frmRecepcion
                        .Modo = frmRecepcion.TipoTrans.Editar

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                        End If

                        .MdiParent = MdiParent
                        .gBeRecepcionEnc = gBeRecepcion
                        .Listar = AddressOf Listar_Recepciones
                        .Show()
                        .Focus()
                    End With

                    SplashScreenManager.CloseForm(False)

                    GridView1.FocusedRowHandle = lSelectionIndex

                    Dgrid.Enabled = True

                ElseIf Modo = pModo.Seleccion Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormCaption("Cargando...")
                    SplashScreenManager.CloseForm(False)

                    Hide()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Recepciones()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Recepciones()
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Recepciones"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If dtpFechaDel.Value > dtpFechaAl.Value Or dtpFechaAl.Value < dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            Listar_Recepciones()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=AP.IdEmpresa,
                                                 pIdBodega:=AP.IdBodega,
                                                 pIdUsuarioAgr:=AP.UsuarioAp.IdUsuario,
                                                 pIdRecEnc:=gBeRecepcion.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            Listar_Recepciones()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=AP.IdEmpresa,
                                                 pIdBodega:=AP.IdBodega,
                                                 pIdUsuarioAgr:=AP.UsuarioAp.IdUsuario,
                                                 pIdRecEnc:=gBeRecepcion.IdRecepcionEnc,
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

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=AP.IdEmpresa,
                                                 pIdBodega:=AP.IdBodega,
                                                 pIdUsuarioAgr:=AP.UsuarioAp.IdUsuario,
                                                 pIdRecEnc:=gBeRecepcion.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then
            Procesar_Registro()
        End If
    End Sub

    Private Sub frmRecepcion_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub frmRecepcion_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Listar_Recepciones()
    End Sub

    Private Sub cmdImprimirSojet_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimirSojet.ItemClick

        Try
            'With frmImpresionSojet
            '    .MdiParent = MdiParent
            '    .Show()
            '    .Focus()
            'End With
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try

    End Sub
End Class