Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmProveedor_List

    Public pBeProveedor As New clsBeProveedor_bodega

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property IdBodega As Integer = 0
    Public Property IdPropietario As Integer = 0
    Public Property Modo As pModo

    Public Property Requerir_Proveedor_Es_Bodega_WMS As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Llena_Grid_Proveedor(ByVal listaProveedor As List(Of clsBeProveedor))

        Try

            If listaProveedor IsNot Nothing AndAlso listaProveedor.Count > 0 Then

                Dim DT As New DataTable("Proveedor")
                DT.Columns.Add("Id", GetType(Integer))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Empresa", GetType(String))
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("Proveedor", GetType(String))
                DT.Columns.Add("Teléfono", GetType(String))
                DT.Columns.Add("Contacto", GetType(String))
                DT.Columns.Add("ProveedorBodega", GetType(String))
                DT.Columns.Add("Sistema", GetType(Boolean))
                DT.Columns.Add("Bodega Recepción", GetType(Boolean))
                DT.Columns.Add("Bodega Traslado", GetType(Boolean))

                For Each Obj As clsBeProveedor In listaProveedor
                    DT.Rows.Add(Obj.IdProveedor, Obj.Codigo,
                                Obj.Empresa.Nombre, Obj.Propietario.Nombre_comercial,
                                Obj.Nombre, Obj.Telefono, Obj.Contacto, 0,
                                Obj.Sistema, Obj.Es_Bodega_Recepcion, Obj.Es_Bodega_Traslado)
                Next

                Dgrid.DataSource = DT

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                Try
                    '#GT21092023: ordenar por codigo ascendente.
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns()

                    GridView1.ClearSorting()
                    GridView1.Columns("Id").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

                Catch ex As Exception

                End Try

            Else
                Dgrid.DataSource = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Grid_ProveedorBodega(ByVal listaProveedorBodega As List(Of clsBeProveedor_bodega))

        Try

            If listaProveedorBodega IsNot Nothing AndAlso listaProveedorBodega.Count > 0 Then

                Dim DT As New DataTable("Proveedor")
                DT.Columns.Add("ID", GetType(Integer))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Empresa", GetType(String))
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("Proveedor", GetType(String))
                DT.Columns.Add("Teléfono", GetType(String))
                DT.Columns.Add("Contacto", GetType(String))

                For Each Obj As clsBeProveedor_bodega In listaProveedorBodega
                    DT.Rows.Add(Obj.IdProveedor, Obj.Proveedor.Codigo, Obj.Proveedor.Empresa.Nombre, Obj.Proveedor.Propietario.Nombre_comercial, Obj.Proveedor.Nombre, Obj.Proveedor.Telefono, Obj.Proveedor.Contacto)
                Next

                Try
                    GridView1.BeginDataUpdate()
                    Dgrid.DataSource = DT

                    GridView1.ClearSorting()
                    GridView1.Columns("ID").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

                    GridView1.EndDataUpdate()
                Catch ex As Exception
                    GridView1.EndDataUpdate()
                End Try


                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            Else
                Dgrid.DataSource = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub listar_Proveedor()

        Try

            Dim listaProveedor As New List(Of clsBeProveedor)
            Dim listaProveedorBodega As New List(Of clsBeProveedor_bodega)

            Requerir_Proveedor_Es_Bodega_WMS = chkRequerir_Proveedor_Es_Bodega_WMS.Checked

            If Modo = pModo.Lista Then
                listaProveedor = clsLnProveedor.Get_All_Lista_Mantenimiento(chkActivos.Checked, AP.IdBodega)
                Llena_Grid_Proveedor(listaProveedor)
            ElseIf Modo = pModo.Seleccion Then
                listaProveedorBodega = clsLnProveedor_bodega.Get_All_By_IdBodega_And_IdPropietario(IdBodega, IdPropietario, Requerir_Proveedor_Es_Bodega_WMS)
                Llena_Grid_ProveedorBodega(listaProveedorBodega)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmProveedor_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            listar_Proveedor()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        listar_Proveedor()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        listar_Proveedor()
        mnuActualizar.Enabled = True
    End Sub

    Private Sub Nuevo_Proveedor()

        Try

            Cierra_Instancia_Previa(frmProveedor)

            With frmProveedor
                .Modo = frmProveedor.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu()
                .mnuGuardar.Enabled = OpcionesMenu.Modificar
                .mnuActualizar.Enabled = OpcionesMenu.Modificar
                .mnuEliminar.Enabled = OpcionesMenu.Eliminar
                .InvokeListarProveedor = AddressOf listar_Proveedor
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
        Nuevo_Proveedor()
        mnuNuevo.Enabled = True
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmProveedor)

                    With frmProveedor
                        .Modo = frmProveedor.TipoTrans.Editar
                        .pBeProveedor.IdProveedor = CInt(Dr.Item("ID"))
                        .InvokeListarProveedor = AddressOf listar_Proveedor
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

                ElseIf Modo = pModo.Seleccion Then
                    pBeProveedor.Proveedor.IdProveedor = Dr.Item("ID")
                    pBeProveedor.Proveedor = clsLnProveedor.GetSingle(pBeProveedor.Proveedor.IdProveedor)
                    pBeProveedor.IdProveedor = pBeProveedor.Proveedor.IdProveedor
                    pBeProveedor.IdBodega = IdBodega
                    clsLnProveedor_bodega.GetSingle_By_IdBodega_And_IdProveedor(pBeProveedor)
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

        Dim reportHeader As String = vbNewLine & "Listado de Proveedores"

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

    Private Sub frmProveedor_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub chkRequerir_Proveedor_Es_Bodega_WMS_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkRequerir_Proveedor_Es_Bodega_WMS.CheckedChanged
        listar_Proveedor()
    End Sub

End Class