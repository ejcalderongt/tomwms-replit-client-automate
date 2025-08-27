Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen

Public Class frmReglaUbicSelProd

    Public Property Modo As pModo
    Public Property IdReglaUbicPrioEnc As Integer

    Private IdPropietario As Integer = -1
    Private propnom As String

    Public pListBEProductosSeleccion As New List(Of clsBeProducto_SelectionList)
    Public pListBEPropietarioSeleccion As New List(Of clsBePropietarioBodegaSeleccion)
    Public pListBeReglaUbicProd As New List(Of clsBeRegla_ubic_prio_producto)

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmReglaUbicSelProd_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            grdvProductos.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
            grdvPropietarios.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus

            Listar_Propietarios()

            Listar_Productos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Listar_Propietarios()

        DgridPropietarios.BeginUpdate()

        Try

            pListBEPropietarioSeleccion = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Seleccion(AP.IdBodega)
            DgridPropietarios.DataSource = pListBEPropietarioSeleccion

            DgridPropietarios.EndUpdate()
            DgridPropietarios.ForceInitialize()

            grdvPropietarios.Columns(0).Visible = False
            grdvPropietarios.Columns(1).Caption = "CódigoPorBodega"

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Listar_Productos()

        Try

            Dim pListBEProductosSeleccion As DataTable = clsLnProducto.Get_All_By_Bodega_For_Seleccion_DT(AP.IdBodega, IdPropietario)
            pListBeReglaUbicProd = clsLnRegla_ubic_prio_producto.Get_All_By_IdRegla_Ubic_Prio_Enc(IdReglaUbicPrioEnc)

            Dim vIndice As Integer = -1

            For Each ProdSel As DataRow In pListBEProductosSeleccion.Rows
                vIndice = pListBeReglaUbicProd.FindIndex(Function(x) x.IdProducto = ProdSel.Item("IdProducto"))
                If vIndice <> -1 Then
                    ProdSel.Item("Seleccionar") = True
                    ProdSel.Item("IdReglaUbicPrioProd") = pListBeReglaUbicProd(vIndice).IdReglaUbicPrioProd
                End If
            Next

            DgridProductos.DataSource = pListBEProductosSeleccion

            grdvProductos.BestFitColumns()
            DgridProductos.EndUpdate()
            DgridProductos.ForceInitialize()

            If pListBEProductosSeleccion.Rows.Count > 0 Then
                Dim ritem As RepositoryItemCheckEdit = TryCast(grdvProductos.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
                RemoveHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged
                AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = grdvProductos.GetFocusedRow

                If ritem.Checked Then

                    Dim BeReglaUbicPrioProd As New clsBeRegla_ubic_prio_producto() With
                    {.IdReglaUbicPrioProd = clsLnRegla_ubic_prio_producto.MaxID() + 1,
                    .IdProducto = Dr.Item("IdProducto"),
                    .IdReglaUbicPrioEnc = IdReglaUbicPrioEnc}
                    clsLnRegla_ubic_prio_producto.Insertar(BeReglaUbicPrioProd)
                    Dr.Item("IdReglaUbicPrioProd") = BeReglaUbicPrioProd.IdReglaUbicPrioProd
                Else

                    Dim BeReglaUbicPrioProd As New clsBeRegla_ubic_prio_producto() With
                    {.IdProducto = Dr.Item("IdProducto"),
                    .IdReglaUbicPrioEnc = IdReglaUbicPrioEnc,
                    .IdReglaUbicPrioProd = Dr.Item("IdReglaUbicPrioProd")}
                    clsLnRegla_ubic_prio_producto.Eliminar(BeReglaUbicPrioProd)
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

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Productos()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")

        Try
            propnom = grdvPropietarios.GetFocusedDisplayText
        Catch ex As Exception
            propnom = ""
        End Try

        Imprimir_Vista()

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdvPropietarios.RowStyle

        Try

            grdvPropietarios.OptionsBehavior.Editable = False
            grdvPropietarios.OptionsSelection.EnableAppearanceFocusedCell = False

            grdvPropietarios.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            grdvPropietarios.OptionsSelection.EnableAppearanceFocusedRow = True
            grdvPropietarios.OptionsSelection.EnableAppearanceHideSelection = True
            grdvPropietarios.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            grdvPropietarios.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            grdvPropietarios.Appearance.FocusedRow.ForeColor = Color.White
            grdvPropietarios.Appearance.SelectedRow.ForeColor = Color.White

            grdvPropietarios.Appearance.SelectedRow.Options.UseBackColor = True
            grdvPropietarios.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

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
            printLink.Component = DgridProductos
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = String.Format("{0}Listado de Reglas de ubicacion{1}{1}Propietario : {2}", vbNewLine, vbCrLf, propnom)

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 110)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub TGrid_Click(sender As Object, e As EventArgs) Handles DgridPropietarios.Click

        Try

            Dim Dr As clsBePropietarioBodegaSeleccion = grdvPropietarios.GetFocusedRow
            IdPropietario = Dr.IdPropietarioBodega
            Listar_Productos()
            grdvPropietarios.SelectRow(grdvPropietarios.FocusedRowHandle)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuMarcarTodos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMarcarTodos.CheckedChanged

        Try

            prg.Properties.PercentView = True
            prg.Properties.Minimum = 0
            prg.Properties.Maximum = grdvProductos.RowCount
            prg.Properties.Step = 1
            prg.Visible = True

            Dim vMarcado As Boolean = False
            Dim vIdProducto As Integer = 0
            Dim vIdReglaUbicPrioProd As Integer = 0
            Dim vIndiceDT As Integer = -1

            For i As Integer = 0 To grdvProductos.DataRowCount - 1

                vIdProducto = Val(grdvProductos.GetRowCellValue(i, "IdProducto").ToString())
                vIdReglaUbicPrioProd = Val(grdvProductos.GetRowCellValue(i, "IdReglaUbicPrioProd").ToString())
                vMarcado = CBool(grdvProductos.GetRowCellValue(i, "Seleccionar"))

                'vMarcado = pListBEProductosSeleccion.Find(Function(x) x.IdProducto = vIdProducto).Seleccionar

                If vMarcado Then

                    Dim BeReglaUbicPrioProd As New clsBeRegla_ubic_prio_producto() With
                        {.IdReglaUbicPrioProd = vIdReglaUbicPrioProd,
                        .IdProducto = vIdProducto,
                        .IdReglaUbicPrioEnc = IdReglaUbicPrioEnc}
                    clsLnRegla_ubic_prio_producto.Eliminar(BeReglaUbicPrioProd)

                    vIndiceDT = pListBEProductosSeleccion.FindIndex(Function(x) x.IdProducto = vIdProducto)

                    '.Seleccionar = Not vMarcado

                Else
                    Dim BeReglaUbicPrioProd As New clsBeRegla_ubic_prio_producto() With
                        {.IdReglaUbicPrioProd = clsLnRegla_ubic_prio_producto.MaxID() + 1,
                        .IdProducto = vIdProducto,
                        .IdReglaUbicPrioEnc = IdReglaUbicPrioEnc}
                    clsLnRegla_ubic_prio_producto.Insertar(BeReglaUbicPrioProd)

                    vIndiceDT = pListBEProductosSeleccion.FindIndex(Function(x) x.IdProducto = vIdProducto)

                    If vIndiceDT <> -1 Then

                    End If
                    '.Seleccionar = Not vMarcado

                End If

                prg.PerformStep()

            Next

            Listar_Productos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            prg.Visible = False
        End Try

    End Sub

End Class