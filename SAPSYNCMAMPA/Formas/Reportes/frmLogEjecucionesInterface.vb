Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Reflection

Public Class frmLogEjecucionesInterface

    Private Const FILTRO_TRANSAC_WMS As String = "TRANSAC_WMS"
    Private mDataSet As DataSet
    Private mAplicarFiltroTransacWmsDefault As Boolean = True

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal pAplicarFiltroTransacWmsDefault As Boolean)
        Me.New()
        mAplicarFiltroTransacWmsDefault = pAplicarFiltroTransacWmsDefault
    End Sub

    Private Sub frmLogEjecucionesInterface_Load(sender As Object, e As EventArgs) Handles Me.Load
        InicializarFiltros(mAplicarFiltroTransacWmsDefault)
        Cargar()
    End Sub

    Private Sub InicializarFiltros(Optional ByVal pAplicarFiltroTransacWms As Boolean = True)
        dtDesde.EditValue = Date.Today.AddDays(-7)
        dtHasta.EditValue = Date.Today
        txtTexto.EditValue = String.Empty
        txtTransaccion.EditValue = If(pAplicarFiltroTransacWms, FILTRO_TRANSAC_WMS, String.Empty)
        txtProceso.EditValue = String.Empty
    End Sub

    Private Sub Cargar()

        Dim lBeginUpdate As Boolean = False

        Try
            ValidarFiltros()
            UseWaitCursor = True
            grdLog.BeginUpdate()
            lBeginUpdate = True

            mDataSet = clsLnI_nav_ejecucion_enc.GetLogEjecucionesDataSet(CDate(dtDesde.EditValue),
                                                                          CDate(dtHasta.EditValue),
                                                                          txtTexto.Text,
                                                                          txtTransaccion.Text,
                                                                          txtProceso.Text)

            grdLog.DataSource = mDataSet.Tables("Encabezado")
            grdLog.ForceInitialize()

            ConfigurarVistas()
            ActualizarResumen()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            If lBeginUpdate Then grdLog.EndUpdate()
            UseWaitCursor = False
        End Try

    End Sub

    Private Sub ValidarFiltros()

        If dtDesde.EditValue Is Nothing OrElse dtHasta.EditValue Is Nothing Then
            Throw New Exception("Seleccione el rango de fechas.")
        End If

        If CDate(dtDesde.EditValue).Date > CDate(dtHasta.EditValue).Date Then
            Throw New Exception("Seleccione un rango de fechas valido.")
        End If

    End Sub

    Private Sub ConfigurarVistas()

        ConfigurarVistaBase(gvEjecuciones)
        ConfigurarVistaBase(gvResultados)
        ConfigurarVistaBase(gvErrores)

        gvEjecuciones.OptionsFind.AlwaysVisible = True
        gvResultados.OptionsFind.AlwaysVisible = True
        gvErrores.OptionsFind.AlwaysVisible = True

        ConfigurarColumnasEncabezado()
        ConfigurarColumnasResultados()
        ConfigurarColumnasErrores()

        gvEjecuciones.BestFitColumns()
        gvResultados.BestFitColumns()
        gvErrores.BestFitColumns()

    End Sub

    Private Sub ConfigurarVistaBase(ByVal pView As GridView)

        pView.OptionsBehavior.Editable = False
        pView.OptionsSelection.EnableAppearanceFocusedCell = False
        pView.FocusRectStyle = DrawFocusRectStyle.RowFocus
        pView.OptionsView.ColumnAutoWidth = False
        pView.OptionsView.ShowGroupPanel = False
        pView.OptionsDetail.EnableMasterViewMode = True
        pView.OptionsDetail.ShowDetailTabs = True
        pView.OptionsView.RowAutoHeight = True

    End Sub

    Private Sub ConfigurarColumnasEncabezado()

        SetCaption(gvEjecuciones, "IdEjecucionEnc", "Id Ejecucion")
        SetCaption(gvEjecuciones, "IdNavConfigEnc", "Id Config")
        SetCaption(gvEjecuciones, "Fecha", "Fecha")
        SetCaption(gvEjecuciones, "Exitosa", "Exitosa")
        SetCaption(gvEjecuciones, "Configuracion", "Configuracion")
        SetCaption(gvEjecuciones, "Empresa", "Empresa")
        SetCaption(gvEjecuciones, "Bodega", "Bodega")
        SetCaption(gvEjecuciones, "Propietario", "Propietario")
        SetCaption(gvEjecuciones, "Origen", "Origen")
        SetCaption(gvEjecuciones, "Sentido", "Sentido")
        SetCaption(gvEjecuciones, "Procesos", "Procesos")
        SetCaption(gvEjecuciones, "CantidadResultados", "Resultados")
        SetCaption(gvEjecuciones, "CantidadErrores", "Errores")

        SetVisible(gvEjecuciones, "IdNavConfigEnc", False)

    End Sub

    Private Sub ConfigurarColumnasResultados()

        SetCaption(gvResultados, "IdEjecucionRes", "Id Resultado")
        SetCaption(gvResultados, "IdEjecucionEnc", "Id Ejecucion")
        SetCaption(gvResultados, "IdNavConfigDet", "Id Det Config")
        SetCaption(gvResultados, "Proceso", "Proceso")
        SetCaption(gvResultados, "Origen", "Origen")
        SetCaption(gvResultados, "Sentido", "Sentido")
        SetCaption(gvResultados, "RegistrosWS", "Registros WS")
        SetCaption(gvResultados, "RegistrosTI", "Registros TI")
        SetCaption(gvResultados, "RegistrosWMS", "Registros WMS")
        SetCaption(gvResultados, "Exitosa", "Exitosa")

        SetVisible(gvResultados, "IdEjecucionEnc", False)

    End Sub

    Private Sub ConfigurarColumnasErrores()

        SetCaption(gvErrores, "IdEjecucionDet", "Id Error")
        SetCaption(gvErrores, "IdEjecucionEnc", "Id Ejecucion")
        SetCaption(gvErrores, "IdNavConfigDet", "Id Det Config")
        SetCaption(gvErrores, "Proceso", "Proceso")
        SetCaption(gvErrores, "Origen", "Origen")
        SetCaption(gvErrores, "Sentido", "Sentido")
        SetCaption(gvErrores, "Fecha", "Fecha")
        SetCaption(gvErrores, "Referencia", "Documento / Referencia")
        SetCaption(gvErrores, "Error", "Error")
        SetCaption(gvErrores, "NoLinea", "No Linea")
        SetCaption(gvErrores, "CodigoProducto", "Codigo Producto")
        SetCaption(gvErrores, "UMBas", "UMBas")
        SetCaption(gvErrores, "CodigoPresentacion", "Presentacion")

        SetVisible(gvErrores, "IdEjecucionEnc", False)

    End Sub

    Private Sub SetCaption(ByVal pView As GridView, ByVal pFieldName As String, ByVal pCaption As String)
        If pView.Columns.ColumnByFieldName(pFieldName) IsNot Nothing Then
            pView.Columns(pFieldName).Caption = pCaption
        End If
    End Sub

    Private Sub SetVisible(ByVal pView As GridView, ByVal pFieldName As String, ByVal pVisible As Boolean)
        If pView.Columns.ColumnByFieldName(pFieldName) IsNot Nothing Then
            pView.Columns(pFieldName).Visible = pVisible
        End If
    End Sub

    Private Sub ActualizarResumen()

        Dim lEjecuciones As Integer = 0
        Dim lResultados As Integer = 0
        Dim lErrores As Integer = 0

        If mDataSet IsNot Nothing Then
            If mDataSet.Tables.Contains("Encabezado") Then lEjecuciones = mDataSet.Tables("Encabezado").Rows.Count
            If mDataSet.Tables.Contains("Resultados") Then lResultados = mDataSet.Tables("Resultados").Rows.Count
            If mDataSet.Tables.Contains("Errores") Then lErrores = mDataSet.Tables("Errores").Rows.Count
        End If

        lblResumen.Caption = String.Format("Ejecuciones: {0} | Resultados: {1} | Errores: {2}", lEjecuciones, lResultados, lErrores)

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub

    Private Sub cmdLimpiar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdLimpiar.ItemClick
        InicializarFiltros(False)
        gvEjecuciones.ActiveFilter.Clear()
        gvResultados.ActiveFilter.Clear()
        gvErrores.ActiveFilter.Clear()
        Cargar()
    End Sub

    Private Sub cmdExpandir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExpandir.ItemClick
        ExpandirDetalles(True)
    End Sub

    Private Sub cmdContraer_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdContraer.ItemClick
        ExpandirDetalles(False)
    End Sub

    Private Sub ExpandirDetalles(ByVal pExpandir As Boolean)

        For i As Integer = 0 To gvEjecuciones.DataRowCount - 1
            gvEjecuciones.SetMasterRowExpanded(i, pExpandir)
        Next

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        ImprimirVista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Cargar()
    End Sub

    Private Sub Filtro_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTexto.KeyDown, txtTransaccion.KeyDown, txtProceso.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Cargar()
        End If
    End Sub

    Private Sub ImprimirVista()

        Try
            gvEjecuciones.OptionsPrint.ExpandAllDetails = True
            gvEjecuciones.OptionsPrint.PrintDetails = True

            Dim printingSystem As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            printLink.Component = grdLog
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem)
            printingSystem.PreviewFormEx.ShowDialog()
            printingSystem.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Log de Ejecuciones de Interface"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub gvEjecuciones_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gvEjecuciones.RowStyle

        If e.RowHandle < 0 Then Exit Sub

        Dim lExitosa As Object = gvEjecuciones.GetRowCellValue(e.RowHandle, "Exitosa")
        Dim lErrores As Object = gvEjecuciones.GetRowCellValue(e.RowHandle, "CantidadErrores")

        If (lExitosa IsNot DBNull.Value AndAlso lExitosa IsNot Nothing AndAlso Not CBool(lExitosa)) OrElse
           (lErrores IsNot DBNull.Value AndAlso lErrores IsNot Nothing AndAlso CInt(lErrores) > 0) Then

            e.Appearance.BackColor = Color.MistyRose
            e.Appearance.ForeColor = Color.Maroon
        End If

    End Sub

    Private Sub gvErrores_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gvErrores.RowStyle
        If e.RowHandle >= 0 Then
            e.Appearance.BackColor = Color.MistyRose
            e.Appearance.ForeColor = Color.Maroon
        End If
    End Sub

End Class
