Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmListaRoadVendedor

    Dim pObjP As clsBeRoad_p_vendedor

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Nuevo_Vendedor()

        Try

            Cierra_Instancia_Previa(frmRoadxVendedor)

            With frmRoadxVendedor
                .Modo = frmRoadxVendedor.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu
                .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                .InvokeListarVendedores = AddressOf listar_Vendedores
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
        Nuevo_Vendedor()
    End Sub

    Private Sub listar_Vendedores()
        Try


            Dim lista As New List(Of clsBeRoad_p_vendedor)

            lista = clsLnRoad_p_vendedor.GetAllFiltro(chkActivos.Checked).ToList

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("road_p_vendedor")
                DT.Columns.Add("idvendedor", GetType(Integer))
                DT.Columns.Add("codigo", GetType(String))
                DT.Columns.Add("nombre", GetType(String))
                DT.Columns.Add("clave", GetType(String))
                DT.Columns.Add("ruta", GetType(String))
                DT.Columns.Add("nivel", GetType(Integer))
                DT.Columns.Add("nivelprecio", GetType(Integer))
                DT.Columns.Add("bidega", GetType(String))
                DT.Columns.Add("subbodega", GetType(String))
                DT.Columns.Add("cod_vehiculo", GetType(String))
                DT.Columns.Add("liquidando", GetType(String))
                DT.Columns.Add("ultima_fecha_liq", GetType(String))
                DT.Columns.Add("bloqueado", GetType(Integer))
                DT.Columns.Add("devolucion_sap", GetType(Integer))

                For Each Obj As clsBeRoad_p_vendedor In lista
                    DT.Rows.Add(Obj.IdVendedor, Obj.Codigo, Obj.Nombre, Obj.Clave, Obj.Ruta, Obj.Nivel, Obj.Nivelprecio, Obj.Bodega, Obj.Subbodega, Obj.Cod_vehiculo, Obj.Liquidando, Obj.Ultima_fecha_liq, Obj.Bloqueado, Obj.Devolucion_sap)
                Next

                Dgrid.DataSource = DT

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                Try

                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns()

                Catch ex As Exception

                End Try

            Else
                Dgrid.DataSource = Nothing
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Dgrid_Load(sender As Object, e As EventArgs) Handles Dgrid.Load
        Modo = 1
        listar_Vendedores()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        listar_Vendedores()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        listar_Vendedores()
    End Sub

    Private Sub Procesar_Registro()

        Try
            If (GridView1.RowCount > 0) Then
                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmRoadxVendedor)

                    With frmRoadxVendedor
                        .Modo = frmRoadxVendedor.TipoTrans.Editar
                        .limpiarFormulario()
                        .pObj.IdVendedor = CInt(Dr.Item("idVendedor"))
                        .InvokeListarVendedores = AddressOf listar_Vendedores
                        .MdiParent = MdiParent
                        .OpcionesMenu = OpcionesMenu
                        .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                        .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                        .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    pObjP = clsLnRoad_p_vendedor.GetSingle(Dr.Item("idVendedor"))
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

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub txtFiltro_EditValueChanged(sender As Object, e As EventArgs)
        listar_Vendedores()
    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick
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

        Dim reportHeader As String = vbNewLine & "Listado de Vendedores en ROAD"

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

    Private Sub frmListaRoadVendedor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class