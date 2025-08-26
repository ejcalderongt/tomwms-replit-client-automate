Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmCliente_List

    Public gCliente As clsBeCliente

    Public Property Propietario As New clsBePropietarios

    Public Property Modo As pModo

    Public Property Requerir_Cliente_Es_Bodega_WMS As Boolean = False
    Public Property EsProveedor As Boolean = False

    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public pIdCliente As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Listar_Clientes()

        Try

            Dim lista As New DataTable

            '#EJC20190312: Se modificaron procedimientos para cargar por datatable por rendimiento.
            If Modo = pModo.Seleccion Then
                '#CKFK Cambié el método para que liste los clientes del propietario
                'lista = clsLnCliente.Get_All_Filtro_Seleccion_DT(chkActivos.Checked, AP.IdBodega)
                lista = clsLnCliente.Get_All_Clientes_By_IdPropietario_And_IdBodega(chkActivos.Checked,
                                                                                    Propietario.IdPropietario,
                                                                                    AP.IdBodega,
                                                                                    Requerir_Cliente_Es_Bodega_WMS, EsProveedor)

            Else
                lista = clsLnCliente.Get_All_Filtro_DT(chkActivos.Checked, AP.IdBodega)
            End If

            If lista IsNot Nothing AndAlso lista.Rows.Count > 0 Then

                Dgrid.DataSource = lista

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                'Dim DT As New DataTable("Cliente")
                'DT.Columns.Add("Correlativo", GetType(Integer))
                'DT.Columns.Add("Empresa", GetType(String))
                'DT.Columns.Add("Propietario", GetType(String))
                'DT.Columns.Add("Tipo Cliente", GetType(String))
                'DT.Columns.Add("Código", GetType(String))
                'DT.Columns.Add("Nombre Comercial", GetType(String))
                'DT.Columns.Add("Teléfono", GetType(String))
                'DT.Columns.Add("Activo", GetType(Boolean))
                'DT.Columns.Add("Sistema", GetType(Boolean))
                'DT.Columns.Add("EsBodegaRecepción", GetType(Boolean))
                'DT.Columns.Add("EsBodegaTraslado", GetType(Boolean))
                'DT.Columns.Add("User_agr", GetType(String))
                'DT.Columns.Add("Fec_agr", GetType(Date))
                'DT.Columns.Add("User_mod", GetType(String))
                'DT.Columns.Add("Fec_mod", GetType(Date))

                'For Each Obj As clsBeCliente In lista
                '    DT.Rows.Add(Obj.IdCliente, Obj.Empresa.Nombre, Obj.Propietario.Nombre_comercial, Obj.ClienteTipo.NombreTipoCliente, Obj.Codigo, Obj.Nombre_comercial, Obj.Telefono, Obj.Activo, Obj.Sistema, Obj.Es_bodega_recepcion, Obj.Es_Bodega_Traslado, Obj.User_agr, Obj.Fec_agr, Obj.User_mod, Obj.Fec_mod)
                'Next

                'Dgrid.DataSource = DT

                lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView1.RowCount))

            Else
                Dgrid.DataSource = Nothing
            End If

            If (GridView1.Columns.Count <> 0) Then

                Try

                    'GridView1.Columns("Activo").Visible = False
                    'GridView1.Columns("User_agr").Visible = False
                    'GridView1.Columns("Fec_agr").Visible = False
                    'GridView1.Columns("User_mod").Visible = False
                    'GridView1.Columns("Fec_mod").Visible = False

                    GridView1.BestFitColumns()

                    GridView1.Columns("Correlativo").SummaryItem.SummaryType = SummaryItemType.Count
                    GridView1.Columns("Correlativo").SummaryItem.DisplayFormat = "{0:n2}"

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub frmCliente_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Listar_Clientes()
    End Sub

    Private Sub Nuevo_Cliente()

        Try

            Cierra_Instancia_Previa(frmCliente)

            With frmCliente
                .Modo = frmCliente.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .InvokeListarClientes = AddressOf Listar_Clientes
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
        Nuevo_Cliente()
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim BeCliente As New clsBeCliente

                BeCliente = clsLnCliente.GetSingle(Dr.Item("Correlativo"))

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmCliente)

                    With frmCliente
                        .Modo = frmCliente.TipoTrans.Editar
                        .OpcionesMenu = OpcionesMenu
                        .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                        .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                        .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                        .gBeCliente = BeCliente
                        .InvokeListarClientes = AddressOf Listar_Clientes
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    gCliente = BeCliente
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
        Listar_Clientes()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")
        Imprimir_Vista()
        SplashScreenManager.CloseForm(False)
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

        Dim reportHeader As String = vbNewLine & "Listado de Clientes"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Clientes()
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

    Private Sub frmCliente_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub mnuMI3Sync_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMI3Sync.ItemClick

        Try

            If AP.IdConfiguracionInterface <> -1 Then
                If Ejecutar_Interface("3-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me) Then
                    Listar_Clientes()
                End If
            Else
                XtraMessageBox.Show("El archivo de configuración .ini no tiene un identificador de configuración para interface",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
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

    Private Sub cmdTiemposClientes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdTiemposClientes.ItemClick

        Try

            With frmTiemposClientes_List
                .Modo = frmTiemposClientes_List.pModo.Lista
                .MdiParent = Me.MdiParent
                .WindowState = FormWindowState.Normal
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub cmdImportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImportarExcel.ItemClick

    End Sub
End Class