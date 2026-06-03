Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmProductividadPicking

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Inicializar_Layout_Dashboard()

        If mDashboardLayoutInicializado Then Return
        mDashboardLayoutInicializado = True

        Configurar_Identidad_Kpi_Resumen()

        Dim split As New SplitContainerControl With {
            .Dock = DockStyle.Fill,
            .SplitterPosition = CInt(XtraTabPage2.Width * 0.58),
            .FixedPanel = SplitFixedPanel.None
        }
        XtraTabPage2.Controls.Add(split)

        grdEficienciaOperadores = New GridControl With {.Dock = DockStyle.Fill}
        viewEficienciaOperadores = New GridView(grdEficienciaOperadores)
        grdEficienciaOperadores.MainView = viewEficienciaOperadores
        grdEficienciaOperadores.ViewCollection.Add(viewEficienciaOperadores)
        viewEficienciaOperadores.OptionsBehavior.ReadOnly = True
        viewEficienciaOperadores.OptionsView.ShowFooter = True
        viewEficienciaOperadores.OptionsFind.AlwaysVisible = True
        split.Panel1.Controls.Add(grdEficienciaOperadores)

        Dim splitRight As New SplitContainerControl With {
            .Dock = DockStyle.Fill,
            .Horizontal = False,
            .SplitterPosition = CInt(XtraTabPage2.Height * 0.5)
        }
        split.Panel2.Controls.Add(splitRight)

        grdTendencia = New GridControl With {.Dock = DockStyle.Fill}
        viewTendencia = New GridView(grdTendencia)
        grdTendencia.MainView = viewTendencia
        grdTendencia.ViewCollection.Add(viewTendencia)
        viewTendencia.OptionsBehavior.ReadOnly = True
        viewTendencia.OptionsView.ShowFooter = True
        splitRight.Panel1.Controls.Add(grdTendencia)

        grdTipoDocumento = New GridControl With {.Dock = DockStyle.Fill}
        viewTipoDocumento = New GridView(grdTipoDocumento)
        grdTipoDocumento.MainView = viewTipoDocumento
        grdTipoDocumento.ViewCollection.Add(viewTipoDocumento)
        viewTipoDocumento.OptionsBehavior.ReadOnly = True
        viewTipoDocumento.OptionsView.ShowFooter = True
        splitRight.Panel2.Controls.Add(grdTipoDocumento)

    End Sub

    ' #EJC20260603_KPI_LABELS: mapea labels del designer a nombres/lectura funcional para soporte y operación.
    Private Sub Configurar_Identidad_Kpi_Resumen()
        If Not Ensure_Kpi_Labels() Then Exit Sub

        lblKpiCumplimiento.Name = "lblKpiCumplimiento"
        lblKpiSolicitado.Name = "lblKpiSolicitado"
        lblKpiPickeado.Name = "lblKpiPickeado"
        lblKpiBrecha.Name = "lblKpiBrecha"
        LabelControl5.Name = "lblKpiOperadores"
        LabelControl6.Name = "lblKpiPicksHora"

        lblKpiCumplimiento.Text = "KPI 1 - Cumplimiento (%): --"
        lblKpiSolicitado.Text = "KPI 2 - Cantidad solicitada: --"
        lblKpiPickeado.Text = "KPI 3 - Cantidad pickeada: --"
        lblKpiBrecha.Text = "KPI 4 - Brecha (sol - pick): --"
        LabelControl5.Text = "KPI 5 - Operadores activos: --"
        LabelControl6.Text = "KPI 6 - Picks por hora: --"
    End Sub

    Private Sub Inicializar_Filtros_Dinamicos()

        If cmbOperadorFiltro IsNot Nothing Then Return

        cmbOperadorFiltro = New LookUpEdit With {
            .Name = "cmbOperadorFiltro",
            .Location = New Point(10, 130),
            .Width = 340
        }
        cmbOperadorFiltro.Properties.NullText = "(Todos)"
        cmbOperadorFiltro.Properties.DisplayMember = "Descripcion"
        cmbOperadorFiltro.Properties.ValueMember = "Codigo"
        AddHandler cmbOperadorFiltro.EditValueChanged, AddressOf Filtro_Dashboard_EditValueChanged

        cmbTipoDocumentoFiltro = New LookUpEdit With {
            .Name = "cmbTipoDocumentoFiltro",
            .Location = New Point(10, 165),
            .Width = 340
        }
        cmbTipoDocumentoFiltro.Properties.NullText = "(Todos)"
        cmbTipoDocumentoFiltro.Properties.DisplayMember = "Descripcion"
        cmbTipoDocumentoFiltro.Properties.ValueMember = "Codigo"
        AddHandler cmbTipoDocumentoFiltro.EditValueChanged, AddressOf Filtro_Dashboard_EditValueChanged

        chkSoloConDiferencia = New CheckEdit With {
            .Name = "chkSoloConDiferencia",
            .Location = New Point(10, 200),
            .Width = 340,
            .Text = "Solo líneas con diferencia"
        }
        AddHandler chkSoloConDiferencia.CheckedChanged, AddressOf Filtro_Dashboard_EditValueChanged

        GroupControl1.Controls.Add(New LabelControl With {.Text = "Operador:", .Location = New Point(10, 114)})
        GroupControl1.Controls.Add(cmbOperadorFiltro)
        GroupControl1.Controls.Add(New LabelControl With {.Text = "Tipo documento:", .Location = New Point(10, 149)})
        GroupControl1.Controls.Add(cmbTipoDocumentoFiltro)
        GroupControl1.Controls.Add(chkSoloConDiferencia)

    End Sub

    Private Sub Bind_Filtros_Dinamicos()

        If cmbOperadorFiltro Is Nothing OrElse cmbTipoDocumentoFiltro Is Nothing Then Return
        If mDtDetalleBase Is Nothing Then Return

        mIniciandoFiltros = True
        Try

            Dim dtOperador As New DataTable()
            dtOperador.Columns.Add("Codigo", GetType(String))
            dtOperador.Columns.Add("Descripcion", GetType(String))

            For Each r In mDtDetalleBase.AsEnumerable().
                OrderBy(Function(x) x.Field(Of String)("Descripción_Operador")).
                GroupBy(Function(x) New With {
                    Key .Cod = If(x.Field(Of String)("Código_Operador"), ""),
                    Key .Nom = If(x.Field(Of String)("Descripción_Operador"), "")
                })
                dtOperador.Rows.Add(r.Key.Cod, r.Key.Cod & " - " & r.Key.Nom)
            Next

            Dim dtTipo As New DataTable()
            dtTipo.Columns.Add("Codigo", GetType(String))
            dtTipo.Columns.Add("Descripcion", GetType(String))

            For Each r In mDtDetalleBase.AsEnumerable().
                OrderBy(Function(x) x.Field(Of String)("Tipo_Documento_Pedido")).
                GroupBy(Function(x) If(x.Field(Of String)("Tipo_Documento_Pedido"), ""))
                dtTipo.Rows.Add(r.Key, r.Key)
            Next

            cmbOperadorFiltro.Properties.DataSource = dtOperador
            cmbOperadorFiltro.EditValue = Nothing

            cmbTipoDocumentoFiltro.Properties.DataSource = dtTipo
            cmbTipoDocumentoFiltro.EditValue = Nothing

            chkSoloConDiferencia.Checked = False

        Finally
            mIniciandoFiltros = False
        End Try

    End Sub

    Private Sub Filtro_Dashboard_EditValueChanged(sender As Object, e As EventArgs)
        If mIniciandoFiltros Then Return
        Aplicar_Filtros_Dashboard()
    End Sub

    Private Sub Aplicar_Filtros_Dashboard()

        If mDtDetalleBase Is Nothing Then
            DgridProductividadPicking.DataSource = Nothing
            lblRegs.Caption = "Registros: 0"
            Exit Sub
        End If

        Dim vOperador As String = If(cmbOperadorFiltro Is Nothing OrElse cmbOperadorFiltro.EditValue Is Nothing, "", cmbOperadorFiltro.EditValue.ToString())
        Dim vTipoDoc As String = If(cmbTipoDocumentoFiltro Is Nothing OrElse cmbTipoDocumentoFiltro.EditValue Is Nothing, "", cmbTipoDocumentoFiltro.EditValue.ToString())
        Dim vSoloDif As Boolean = (chkSoloConDiferencia IsNot Nothing AndAlso chkSoloConDiferencia.Checked)

        Dim lData = mDtDetalleBase.AsEnumerable()

        If vOperador <> "" Then
            lData = lData.Where(Function(x) String.Equals(If(x.Field(Of String)("Código_Operador"), ""), vOperador, StringComparison.OrdinalIgnoreCase))
        End If

        If vTipoDoc <> "" Then
            lData = lData.Where(Function(x) String.Equals(If(x.Field(Of String)("Tipo_Documento_Pedido"), ""), vTipoDoc, StringComparison.OrdinalIgnoreCase))
        End If

        If vSoloDif Then
            lData = lData.Where(Function(x) Convert.ToDecimal(If(IsDBNull(x("Diferencia")), 0D, x("Diferencia"))) <> 0D)
        End If

        Dim dtDetalle As DataTable = mDtDetalleBase.Clone()
        For Each row In lData
            dtDetalle.ImportRow(row)
        Next

        DgridProductividadPicking.DataSource = dtDetalle
        lblRegs.Caption = String.Format("Registros: {0}", dtDetalle.Rows.Count)
        Configurar_Grid_Detalle()

        Bind_Kpi(dtDetalle)
        Bind_Operadores(dtDetalle)
        Bind_Tendencia(dtDetalle)
        Bind_TipoDocumento(dtDetalle)

    End Sub

    Private Sub Configurar_Grid_Detalle()
        If GridView1.Columns.Count = 0 Then Return

        GridView1.OptionsView.ShowFooter = True
        GridView1.GroupSummary.Clear()

        If GridView1.Columns("Código_Producto") IsNot Nothing Then
            GridView1.Columns("Código_Producto").Group()
        End If

        For Each cName In New String() {"Cantidad_Solicitada", "Cantidad_Pickeada", "Diferencia"}
            If GridView1.Columns(cName) IsNot Nothing Then
                GridView1.Columns(cName).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns(cName).SummaryItem.DisplayFormat = "{0:n2}"
                GridView1.Columns(cName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns(cName).DisplayFormat.FormatString = "{0:n2}"
            End If
        Next

        If GridView1.Columns("Fecha_Por_Línea") IsNot Nothing Then
            GridView1.Columns("Fecha_Por_Línea").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            GridView1.Columns("Fecha_Por_Línea").DisplayFormat.FormatString = "G"
        End If

        Restore_LayOut()
    End Sub

    ' #EJC20260603_KPI_GUARD: evita NullReference cuando eventos disparan Cargar antes de que el layout dinámico esté listo.
    Private Function Ensure_Kpi_Labels() As Boolean
        If lblKpiCumplimiento Is Nothing OrElse
           lblKpiSolicitado Is Nothing OrElse
           lblKpiPickeado Is Nothing OrElse
           lblKpiBrecha Is Nothing OrElse
           LabelControl5 Is Nothing OrElse
           LabelControl6 Is Nothing Then
            Return False
        End If

        Return True
    End Function

    Private Sub Bind_Kpi(ByVal dtDetalle As DataTable)
        If dtDetalle Is Nothing Then Exit Sub
        If Not Ensure_Kpi_Labels() Then Exit Sub

        Dim totalSolicitado As Decimal = dtDetalle.AsEnumerable().Sum(Function(x) Convert.ToDecimal(If(IsDBNull(x("Cantidad_Solicitada")), 0D, x("Cantidad_Solicitada"))))
        Dim totalPickeado As Decimal = dtDetalle.AsEnumerable().Sum(Function(x) Convert.ToDecimal(If(IsDBNull(x("Cantidad_Pickeada")), 0D, x("Cantidad_Pickeada"))))
        Dim totalBrecha As Decimal = totalSolicitado - totalPickeado

        Dim cumplimiento As Decimal = 0D
        If totalSolicitado > 0 Then
            cumplimiento = Math.Round((totalPickeado / totalSolicitado) * 100D, 2)
        End If

        Dim operadoresActivos As Integer = dtDetalle.AsEnumerable().
            Select(Function(x) If(x.Field(Of String)("Código_Operador"), "")).
            Where(Function(x) x <> "").
            Distinct().
            Count()

        Dim horas As Decimal = Math.Max((dtpFechaAl.Value.Date.AddDays(1) - dtpFechaDel.Value.Date).TotalHours, 1)
        Dim picksHora As Decimal = Math.Round(totalPickeado / Convert.ToDecimal(horas), 2)

        lblKpiCumplimiento.Text = String.Format("KPI 1 - Cumplimiento (%): {0:n2}%", cumplimiento)
        lblKpiSolicitado.Text = String.Format("KPI 2 - Cantidad solicitada: {0:n2}", totalSolicitado)
        lblKpiPickeado.Text = String.Format("KPI 3 - Cantidad pickeada: {0:n2}", totalPickeado)
        lblKpiBrecha.Text = String.Format("KPI 4 - Brecha (sol - pick): {0:n2}", totalBrecha)
        LabelControl5.Text = String.Format("KPI 5 - Operadores activos: {0:n0}", operadoresActivos)
        LabelControl6.Text = String.Format("KPI 6 - Picks por hora (rango): {0:n2}", picksHora)
    End Sub

    Private Sub Bind_Operadores(ByVal dtDetalle As DataTable)
        If grdEficienciaOperadores Is Nothing Then Return

        Dim dt As New DataTable()
        dt.Columns.Add("Código_Operador", GetType(String))
        dt.Columns.Add("Descripción_Operador", GetType(String))
        dt.Columns.Add("Líneas", GetType(Integer))
        dt.Columns.Add("Cantidad_Solicitada", GetType(Decimal))
        dt.Columns.Add("Cantidad_Pickeada", GetType(Decimal))
        dt.Columns.Add("Cumplimiento_Pct", GetType(Decimal))

        For Each g In dtDetalle.AsEnumerable().GroupBy(Function(x) New With {
            Key .Cod = If(x.Field(Of String)("Código_Operador"), ""),
            Key .Nom = If(x.Field(Of String)("Descripción_Operador"), "")
        }).OrderByDescending(Function(x) x.Sum(Function(r) Convert.ToDecimal(If(IsDBNull(r("Cantidad_Pickeada")), 0D, r("Cantidad_Pickeada")))))

            Dim sol As Decimal = g.Sum(Function(r) Convert.ToDecimal(If(IsDBNull(r("Cantidad_Solicitada")), 0D, r("Cantidad_Solicitada"))))
            Dim pic As Decimal = g.Sum(Function(r) Convert.ToDecimal(If(IsDBNull(r("Cantidad_Pickeada")), 0D, r("Cantidad_Pickeada"))))
            Dim pct As Decimal = If(sol > 0D, Math.Round((pic / sol) * 100D, 2), 0D)
            dt.Rows.Add(g.Key.Cod, g.Key.Nom, g.Count(), sol, pic, pct)
        Next

        grdEficienciaOperadores.DataSource = dt
    End Sub

    Private Sub Bind_Tendencia(ByVal dtDetalle As DataTable)
        If grdTendencia Is Nothing Then Return

        Dim dt As New DataTable()
        dt.Columns.Add("Fecha", GetType(Date))
        dt.Columns.Add("Líneas", GetType(Integer))
        dt.Columns.Add("Cantidad_Solicitada", GetType(Decimal))
        dt.Columns.Add("Cantidad_Pickeada", GetType(Decimal))
        dt.Columns.Add("Cumplimiento_Pct", GetType(Decimal))

        For Each g In dtDetalle.AsEnumerable().
            GroupBy(Function(x) Convert.ToDateTime(x("Fecha_Por_Línea")).Date).
            OrderBy(Function(x) x.Key)

            Dim sol As Decimal = g.Sum(Function(r) Convert.ToDecimal(If(IsDBNull(r("Cantidad_Solicitada")), 0D, r("Cantidad_Solicitada"))))
            Dim pic As Decimal = g.Sum(Function(r) Convert.ToDecimal(If(IsDBNull(r("Cantidad_Pickeada")), 0D, r("Cantidad_Pickeada"))))
            Dim pct As Decimal = If(sol > 0D, Math.Round((pic / sol) * 100D, 2), 0D)
            dt.Rows.Add(g.Key, g.Count(), sol, pic, pct)
        Next

        grdTendencia.DataSource = dt
    End Sub

    Private Sub Bind_TipoDocumento(ByVal dtDetalle As DataTable)
        If grdTipoDocumento Is Nothing Then Return

        Dim dt As New DataTable()
        dt.Columns.Add("Tipo_Documento_Pedido", GetType(String))
        dt.Columns.Add("Líneas", GetType(Integer))
        dt.Columns.Add("Cantidad_Solicitada", GetType(Decimal))
        dt.Columns.Add("Cantidad_Pickeada", GetType(Decimal))
        dt.Columns.Add("Cumplimiento_Pct", GetType(Decimal))

        For Each g In dtDetalle.AsEnumerable().
            GroupBy(Function(x) If(x.Field(Of String)("Tipo_Documento_Pedido"), "")).
            OrderByDescending(Function(x) x.Count())

            Dim sol As Decimal = g.Sum(Function(r) Convert.ToDecimal(If(IsDBNull(r("Cantidad_Solicitada")), 0D, r("Cantidad_Solicitada"))))
            Dim pic As Decimal = g.Sum(Function(r) Convert.ToDecimal(If(IsDBNull(r("Cantidad_Pickeada")), 0D, r("Cantidad_Pickeada"))))
            Dim pct As Decimal = If(sol > 0D, Math.Round((pic / sol) * 100D, 2), 0D)
            dt.Rows.Add(g.Key, g.Count(), sol, pic, pct)
        Next

        grdTipoDocumento.DataSource = dt
    End Sub

    Public Property Skip_Saving_Layout As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private mDtDetalleBase As DataTable
    Private mIniciandoFiltros As Boolean = False

    Private mDashboardLayoutInicializado As Boolean = False

    Private cmbOperadorFiltro As LookUpEdit
    Private cmbTipoDocumentoFiltro As LookUpEdit
    Private chkSoloConDiferencia As CheckEdit

    Private grdEficienciaOperadores As GridControl
    Private viewEficienciaOperadores As GridView
    Private grdTendencia As GridControl
    Private viewTendencia As GridView
    Private grdTipoDocumento As GridControl
    Private viewTipoDocumento As GridView

    Private Sub Cargar()

        Skip_Saving_Layout = True

        Try
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando...")

            Dim ds As DataSet = Nothing

            Try
                ds = clsLnTrans_picking_enc.Get_Rpt_Productividad_Picking_Dashboard_By_IdBodega(dtpFechaDel.Value, dtpFechaAl.Value, cmbBodega.EditValue)
            Catch exDashboard As Exception
                Dim vMsgWarn As String = "#EJC20260602_PRODUCTIVIDAD_PICKING_DASHBOARD: fallback a vista legacy. " & exDashboard.Message
                clsLnLog_error_wms.Agregar_Error(vMsgWarn)
            End Try

            If ds IsNot Nothing AndAlso ds.Tables.Count >= 5 Then

                mDtDetalleBase = ds.Tables(4).Copy()
                Bind_Filtros_Dinamicos()
                Aplicar_Filtros_Dashboard()

            Else

                Dim dt As DataTable = clsLnTrans_picking_enc.Get_Rpt_Productividad_Picking_By_IdBodega(dtpFechaDel.Value,
                                                                                                        dtpFechaAl.Value,
                                                                                                        cmbBodega.EditValue)
                mDtDetalleBase = If(dt Is Nothing, New DataTable(), dt.Copy())
                Bind_Filtros_Dinamicos()
                Aplicar_Filtros_Dashboard()

            End If

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            Skip_Saving_Layout = False
        End Try
    End Sub


    Private Sub cmbBodega_EditValueChanging(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        If Skip_Saving_Layout Then Return
        If cmbBodega.EditValue Is Nothing Then Return
        Cargar()
        GridView1.Focus()
    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            Cargar()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            Cargar()

            GridView1.Focus()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmMovimientosCardex_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            '#EJC20210719:Restaurar LayoutGrid en grdExistDoc.
            vNombreArchivoLayOutGrid = "frmProductividadPicking.xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            Inicializar_Layout_Dashboard()
            Inicializar_Filtros_Dinamicos()

            Cargar()


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Restore_LayOut()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                 vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(DgridProductividadPicking, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True, True, 12)
            Exit Sub
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
            printLink.Component = DgridProductividadPicking
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Productividad Picking"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi en frmStockConDocumento.
    Private vNombreArchivoLayOutGrid As String = ""

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick

        Try

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

            XtraMessageBox.Show("Diseño de grid guardado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdExToExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExportarExcel.ItemClick
        Exportar_Grid_A_Excel(DgridProductividadPicking, "WMS_ProductividadPicking.xlsx")
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
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub gviewEncabezadoPedido_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView1.ColumnFilterChanged
        lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView1.DataRowCount.ToString()))
    End Sub

End Class



