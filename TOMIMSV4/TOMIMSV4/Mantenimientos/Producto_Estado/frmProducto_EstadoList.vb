Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmProducto_EstadoList

    Public pObj As New clsBeProducto_estado
    Public gBeProductoEstado As New clsBeProducto_estado
    Public pIdPropietario As Integer = 0

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public Property EstadoOrigen As Integer = 0

    Public Enum pModo
        Lista = 1
        Seleccion = 2
        Seleccion_Masiva = 3
    End Enum

    Public Sub New(ByVal pModo As pModo)
        InitializeComponent()
        Modo = pModo
    End Sub

    Private Sub frmProducto_ClasificacionList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Listar_Producto_Estado()

        Try

            Dim lBeProductoEstado As New List(Of clsBeProducto_estado)
            Dim lBeProductoEstadoUsuario As New List(Of clsBeRol_usuario_estado)


            Select Case Modo

                Case pModo.Lista, pModo.Seleccion

                    lBeProductoEstado = clsLnProducto_estado.Get_All_Filtro(chkActivos.Checked)

                Case pModo.Seleccion_Masiva

                    lBeProductoEstado = clsLnProducto_estado.Get_All_Filtro(chkActivos.Checked, pIdPropietario)

                    '#EJC202410221122AM:
                    lBeProductoEstadoUsuario = clsLnRol_usuario_estado.Get_All_By_IdRol_And_IdEstadoOrigen(AP.UsuarioAp.IdRol, EstadoOrigen)
                    If lBeProductoEstadoUsuario.Any() Then
                        ' Filtrar lBeProductoEstado basado en los IdEstado de lBeProductoEstadoUsuario
                        lBeProductoEstado = lBeProductoEstado.Where(Function(x) lBeProductoEstadoUsuario.Any(Function(usr) usr.IdEstadoDestino = x.IdEstado AndAlso usr.Permitir = True)).ToList()

                        ' Excluir los elementos que coincidan con el EstadoOrigen de lBeProductoEstadoUsuario
                        lBeProductoEstado = lBeProductoEstado.Where(Function(x) x.IdEstado <> EstadoOrigen).ToList()
                    Else
                        lBeProductoEstado = Nothing
                    End If

            End Select

            If lBeProductoEstado IsNot Nothing AndAlso lBeProductoEstado.Count > 0 Then

                Dim DT As New DataTable("Estado")
                DT.Columns.Add("IdEstado", GetType(Integer))
                DT.Columns.Add("IdPropietario", GetType(Integer))
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("Estado", GetType(String))

                For Each BeProductoEstado As clsBeProducto_estado In lBeProductoEstado
                    DT.Rows.Add(BeProductoEstado.IdEstado,
                                BeProductoEstado.Propietario.IdPropietario,
                                BeProductoEstado.Propietario.Nombre_comercial,
                                BeProductoEstado.Nombre)
                Next

                Dgrid.DataSource = DT

                If (GridView1.Columns.Count <> 0) Then

                    Try

                        GridView1.Columns("IdEstado").Caption = "Código"
                        GridView1.Columns("IdPropietario").Visible = False
                        GridView1.OptionsView.ColumnAutoWidth = False
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

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        Listar_Producto_Estado()
        mnuActualizar.Enabled = True
    End Sub

    Private Sub Nuevo_ProductoEstado()

        Try

            Cierra_Instancia_Previa(frmProducto_Estado)

            With frmProducto_Estado
                .Modo = frmProducto_Estado.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu()
                .mnuGuardar.Enabled = OpcionesMenu.Modificar
                .mnuActualizar.Enabled = OpcionesMenu.Modificar
                .mnuEliminar.Enabled = OpcionesMenu.Eliminar
                .InvokeListarProductoEstado = AddressOf Listar_Producto_Estado
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

        mnuNuevo.Enabled = False
        Nuevo_ProductoEstado()
        mnuNuevo.Enabled = True
    End Sub

    Private Sub Producto_Estado_Editar()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                gBeProductoEstado = clsLnProducto_estado.Get_Single_By_IdEstado(Dr.Item("IdEstado"))
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmProducto_Estado)

                    With frmProducto_Estado
                        .Modo = frmProducto_Estado.TipoTrans.Editar
                        .gBeProductoEstado = gBeProductoEstado
                        .InvokeListarProductoEstado = AddressOf Listar_Producto_Estado
                        .MdiParent = MdiParent
                        .OpcionesMenu = OpcionesMenu()
                        .mnuGuardar.Enabled = OpcionesMenu.Modificar
                        .mnuActualizar.Enabled = OpcionesMenu.Modificar
                        .mnuEliminar.Enabled = OpcionesMenu.Eliminar
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Or Modo = pModo.Seleccion_Masiva Then
                    pObj = gBeProductoEstado
                    Hide()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Producto_Estado_Editar()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Producto_Estado()
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Estados"

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

    Private Sub cmdImportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImportarExcel.ItemClick

        cmdImportarExcel.Enabled = False
        Dim Carga As New frmCargaExcel()
        Carga.pPropietario = True
        Carga.pNombreMantenimiento = "Producto Estado"
        Carga.pTipoMantenimiento = "ProductoEstado"
        Carga.Listar = New frmCargaExcel.Operar(AddressOf Listar_Producto_Estado)
        Carga.ShowDialog()
        Carga.Dispose()
        cmdImportarExcel.Enabled = True

    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Producto_Estado_Editar()
    End Sub

    Private Sub frmProducto_EstadoList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub frmProducto_EstadoList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Listar_Producto_Estado()
    End Sub
End Class