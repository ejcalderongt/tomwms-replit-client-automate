Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmAsociacionReglaUbicacion

    Public Property Modo As pModo
    Public Property IdReglaUbicacionEnc As Integer

    Public BeBodega As New clsBeBodega

    Private IdTramo As Integer = 0
    Private tramonom As String
    Private bMostrarTodas As Boolean = False

    Private lUbicacionesBodega As New List(Of clsBeBodega_ubicacion)
    Private lUbicacionesRegla As New List(Of clsBeRegla_ubicacion)

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmTipoTarima_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            grdvUbicaciones.FocusRectStyle = Views.Grid.DrawFocusRectStyle.RowFullFocus
            grdvTramo.FocusRectStyle = Views.Grid.DrawFocusRectStyle.RowFullFocus

            Dim ritem As RepositoryItemCheckEdit = TryCast(grdvUbicaciones.Columns("Asignar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

            ListarTramos()

            Listar_Ubicaciones()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.Default.CloseWaitForm()
        End Try

    End Sub

    Public Sub ListarTramos()

        Dim lTramos As New List(Of clsBeBodega_tramo)
        Dim dr As DataRow

        DgridTramos.BeginUpdate()
        DsReglaUb.Tramo.Clear()

        IdTramo = 0

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Listando tramos...")

            BeBodega.IdBodega = AP.IdBodega

            clsLnBodega.Get_Estructura_By_IdBodega(BeBodega)

            lTramos = BeBodega.Tramos

            For Each T As clsBeBodega_tramo In lTramos

                dr = DsReglaUb.Tramo.NewRow
                dr.Item("Id") = T.IdTramo ': If i = 0 Then tramoid = lTramos(i)(0)
                dr.Item("Nombre") = String.Format("{0} - {1}", T.Codigo, T.Descripcion)
                DsReglaUb.Tramo.AddTramoRow(dr)

            Next

            DgridTramos.DataSource = DsReglaUb.Tramo

            DgridTramos.EndUpdate()
            DgridTramos.ForceInitialize()

            Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Count, "Nombre", "Tramos={0}")

            If grdvTramo.Columns("Nombre").Summary.Count = 0 Then
                grdvTramo.Columns("Nombre").Summary.Add(item)
            End If

            grdvTramo.BestFitColumns()

        Catch ex As Exception
            IdTramo = 0
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Listar_Ubicaciones()

        Dim dr As DataRow

        DgridUbicaciones.BeginUpdate()

        DsReglaUb.Ubic.Clear()

        Try
            '#MA20260519 Para listar
            'lUbicacionesBodega = BeBodega.Ubicaciones.FindAll(Function(x) x.IdTramo = IdTramo AndAlso x.IdBodega = AP.IdBodega) ' clsLnRegla_ubicacion.ListarUbicaciones(tramoid, reglaid)
            If bMostrarTodas Then

                lUbicacionesBodega = BeBodega.Ubicaciones.FindAll(
                    Function(x) x.IdBodega = AP.IdBodega)

            Else

                lUbicacionesBodega = BeBodega.Ubicaciones.FindAll(
                    Function(x) x.IdTramo = IdTramo AndAlso
                    x.IdBodega = AP.IdBodega)
            End If

            lUbicacionesRegla = clsLnRegla_Ubicacion.Get_All_By_IdReglaUbicacionEnc(IdReglaUbicacionEnc, AP.IdBodega)

            For Each U As clsBeBodega_ubicacion In lUbicacionesBodega

                dr = DsReglaUb.Ubic.NewRow
                dr.Item("Id") = U.IdUbicacion
                dr.Item("Nombre") = U.Descripcion
                dr.Item("Asignar") = (lUbicacionesRegla.FindIndex(Function(x) x.IdUbicacion = U.IdUbicacion) <> -1)
                DsReglaUb.Ubic.AddUbicRow(dr)

            Next

            DgridUbicaciones.DataSource = DsReglaUb.Ubic

            DgridUbicaciones.EndUpdate()
            DgridUbicaciones.ForceInitialize()

            Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Count, "Nombre", "Ubicaciones={0}")

            If grdvUbicaciones.Columns("Nombre").Summary.Count = 0 Then
                grdvUbicaciones.Columns("Nombre").Summary.Add(item)
            End If

            grdvUbicaciones.BestFitColumns()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = grdvUbicaciones.GetFocusedRow


                If ritem.Checked Then

                    Dim BeAsocReglaUbic As New clsBeRegla_ubicacion() With
                    {.IdReglaUbicacionEnc = IdReglaUbicacionEnc,
                    .IdUbicacion = Dr.Item("Id"),
                    .IdBodega = AP.IdBodega}
                    clsLnRegla_ubicacion.Insertar(BeAsocReglaUbic)

                Else
                    Dim BeAsocReglaUbic As New clsBeRegla_ubicacion() With
                   {.IdReglaUbicacionEnc = IdReglaUbicacionEnc,
                   .IdUbicacion = Dr.Item("Id"),
                   .IdBodega = AP.IdBodega}
                    clsLnRegla_ubicacion.Eliminar(BeAsocReglaUbic)
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
        Listar_Ubicaciones()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        cmdImprimir.Enabled = False

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")

        Try
            tramonom = grdvTramo.GetFocusedDisplayText
        Catch ex As Exception
            cmdImprimir.Enabled = False
            tramonom = ""
        End Try

        Imprimir_Vista()

        cmdImprimir.Enabled = False
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdvTramo.RowStyle

        Try

            grdvTramo.OptionsBehavior.Editable = False
            grdvTramo.OptionsSelection.EnableAppearanceFocusedCell = False

            grdvTramo.FocusRectStyle = Views.Grid.DrawFocusRectStyle.RowFocus

            grdvTramo.OptionsSelection.EnableAppearanceFocusedRow = True
            grdvTramo.OptionsSelection.EnableAppearanceHideSelection = True
            grdvTramo.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            grdvTramo.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            grdvTramo.Appearance.FocusedRow.ForeColor = Color.White
            grdvTramo.Appearance.SelectedRow.ForeColor = Color.White

            grdvTramo.Appearance.SelectedRow.Options.UseBackColor = True
            grdvTramo.Appearance.SelectedRow.Options.UseForeColor = True

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
            printLink.Component = DgridUbicaciones
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

        Dim reportHeader As String = String.Format("{0}Listado de Reglas por ubicacion{1}{1}Tramo : {2}", vbNewLine, vbCrLf, tramonom)

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 110)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub TGrid_Click(sender As Object, e As EventArgs) Handles DgridTramos.Click

        Dim selid As Integer

        Try

            selid = DsReglaUb.Tramo.Rows(grdvTramo.FocusedRowHandle).Item("ID")

            grdvTramo.SelectRow(grdvTramo.FocusedRowHandle)

        Catch ex As Exception
            Return
        End Try

        IdTramo = selid
        'Si estaba activado "mostrar todas", se apaga automáticamente
        If bMostrarTodas Then
            bMostrarTodas = False
            BarToggleSwitchItem1.Checked = False
        End If

        grdvTramo.SelectRow(grdvTramo.FocusedRowHandle)

        Listar_Ubicaciones()

    End Sub

    Private Sub mnuMarcarTodos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMarcarTodos.CheckedChanged

        Try

            prg.Properties.PercentView = True
            prg.Properties.Minimum = 0
            prg.Properties.Maximum = grdvUbicaciones.RowCount
            prg.Properties.Step = 1
            prg.Visible = True
            prg.EditValue = 0

            Dim vMarcado As Boolean = False

            For Each Prod As DataRowView In grdvUbicaciones.DataSource

                'MsgBox(Prod)

                vMarcado = Prod.Item("Asignar")

                If vMarcado Then

                    Dim BeAsocReglaUbic As New clsBeRegla_ubicacion() With
                   {.IdReglaUbicacionEnc = IdReglaUbicacionEnc,
                   .IdUbicacion = Prod.Item("Id"),
                    .IdBodega = AP.IdBodega}
                    clsLnRegla_ubicacion.Eliminar(BeAsocReglaUbic)

                Else

                    Dim BeAsocReglaUbic As New clsBeRegla_ubicacion() With
                    {.IdReglaUbicacionEnc = IdReglaUbicacionEnc,
                    .IdUbicacion = Prod.Item("Id"),
                    .IdBodega = AP.IdBodega}
                    clsLnRegla_ubicacion.Insertar(BeAsocReglaUbic)

                End If

                prg.PerformStep()
                prg.Update()
            Next

            Listar_Ubicaciones()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            prg.Visible = False
        End Try

    End Sub

    '#MA20260519 Marcar todas las ubicaciones
    Private Sub BarToggleSwitchItem1_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarToggleSwitchItem1.CheckedChanged

        Try

            bMostrarTodas = BarToggleSwitchItem1.Checked

            If bMostrarTodas Then

                IdTramo = 0
                grdvTramo.ClearSelection()

            End If

            Listar_Ubicaciones()

        Catch ex As Exception

            XtraMessageBox.Show(
            ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub
End Class