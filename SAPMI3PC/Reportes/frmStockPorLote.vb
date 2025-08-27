Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen
Imports SAPbobsCOM

Public Class frmStockPorLote

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property IsLoading As Boolean = True
    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private oCompany As Company
    Dim sErrMsg As String = ""
    Dim lRetCode, lErrCode As Long

    Private Sub Cargar_Datos()

        Try


            grdStockPorLote.DataSource = Nothing

            Try

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                If Not SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                End If

                SplashScreenManager.Default.SetWaitFormDescription("Generando reporte...")

            Catch ex As Exception

            End Try


            Dim dtStockWMS As New DataTable
            dtStockWMS = clsLnStock.Get_Reporte_Stock_For_SAP(BeConfigEnc.Idbodega)

            Dim dtSAPB1 As New DataTable
            dtSAPB1 = Get_Existencias_From_SAP_AsDataTable()

            If Not dtSAPB1 Is Nothing Then

                Dim dtReporteComparativo As New DataTable
                dtReporteComparativo = CompareStockData(dtStockWMS, dtSAPB1)

                IsLoading = True

                If Not dtReporteComparativo Is Nothing Then

                    If dtReporteComparativo.Rows.Count > 0 Then

                        GridView1.Columns.Clear()

                        grdStockPorLote.DataSource = dtReporteComparativo

                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                        GridView1.PopulateColumns()

                        GridView1.OptionsView.ShowFooter = True

                        GridView1.Columns("Cantidad_TOMWMS").SummaryItem.SummaryType = SummaryItemType.Sum
                        GridView1.Columns("Cantidad_TOMWMS").SummaryItem.DisplayFormat = "{0:n6}"
                        GridView1.Columns("Cantidad_TOMWMS").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Cantidad_TOMWMS").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Cantidad_SAPB1").SummaryItem.SummaryType = SummaryItemType.Sum
                        GridView1.Columns("Cantidad_SAPB1").SummaryItem.DisplayFormat = "{0:n6}"
                        GridView1.Columns("Cantidad_SAPB1").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Cantidad_SAPB1").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Diferencia_Cantidad").SummaryItem.SummaryType = SummaryItemType.Sum
                        GridView1.Columns("Diferencia_Cantidad").SummaryItem.DisplayFormat = "{0:n6}"
                        GridView1.Columns("Diferencia_Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Diferencia_Cantidad").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Diferencia_Encontrada").Visible = False

                        GridView1.BestFitColumns()

                    End If

                End If

                IsLoading = False

                If Not chkIncluirUbicacion.Checked AndAlso Not chkIncluirIdStock.Checked Then
                    Set_LayOut_Grid()
                End If

            Else

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("No se pudo obtener la información de existencias de SAP.",
                                     Text,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error)

            End If


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            GridView1.OptionsPrint.ExpandAllDetails = True
            GridView1.OptionsPrint.PrintDetails = True

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - MI3 "

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
            printLink.Component = grdStockPorLote
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

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

        Dim reportHeader As String = vbNewLine & "Detalle de existencias por estado de producto"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdExToExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExToExcel.ItemClick
        Exportar_Grid_A_Excel(grdStockPorLote, "WMS_ExistenciasPorLote.xlsx")
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus
            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.Options.UseBackColor = True
            GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGrid As String = ""

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick

        Try

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(BeConfigEnc.Idempresa,
                                                          IdUsuario,
                                                          HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            clsLnConfiguracion_usuario_enc.Delete_Layout(BeConfigEnc.Idempresa,
                                                         IdUsuario,
                                                         HostName,
                                                         vNombreArchivoLayOutGrid)

            XtraMessageBox.Show("Diseño de grid eliminado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmStockPorLote_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Inicializando estructuras...")

            vNombreArchivoLayOutGrid = "grdStockPorLote.xml"

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            If Not BeConfigEnc Is Nothing Then

                Set_LayOut_Grid()

            Else

                XtraMessageBox.Show("ERROR_202404102212: No se pudo obtener la configuración de interface para el IdConfiguracionInterface " & BD.Instancia.IdConfiguracionInterface,
                                      Text,
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error)

                Close()

                Exit Sub

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Set_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(BeConfigEnc.Idempresa,
                                                                                  IdUsuario,
                                                                                  HostName,
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

    Private Sub GridView_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        Try

            If IsLoading Then Exit Sub

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(BeConfigEnc.Idempresa,
                                                          IdUsuario,
                                                          HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private dtExistenciasSAP As New DataTable

    Public Function Get_Existencias_From_SAP_AsDataTable() As DataTable

        Get_Existencias_From_SAP_AsDataTable = Nothing

        Try

            If Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then

                Dim oRecordset As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

                Dim query As String = "SELECT 
                                    T0.ItemCode AS 'Codigo',
                                    T0.ItemName AS 'Nombre',
                                    T1.WhsCode AS 'Codigo_Bodega',
                                    T4.WhsName AS 'Nombre_Bodega',
                                    T1.OnHand AS 'Total_Almacen',
                                    T2.DistNumber AS 'Lote',
                                    T3.Quantity AS 'Cantidad_Lote',
	                                T2.ExpDate AS 'Fecha_Vence'
                                FROM 
                                    OITM T0 
                                INNER JOIN 
                                    OITW T1 ON T0.ItemCode = T1.ItemCode 
                                INNER JOIN 
                                    OBTN T2 ON T0.ItemCode = T2.ItemCode
                                INNER JOIN 
                                    OBTQ T3 ON T2.AbsEntry = T3.MdAbsEntry AND T1.WhsCode = T3.WhsCode
                                INNER JOIN 
                                    OWHS T4 ON T1.WhsCode = T4.WhsCode
                                WHERE 
                                    T1.OnHand > 0"

                oRecordset.DoQuery(query)

                dtExistenciasSAP.Columns.Clear()
                dtExistenciasSAP.Columns.Add("Codigo", GetType(String))
                dtExistenciasSAP.Columns.Add("Nombre", GetType(String))
                dtExistenciasSAP.Columns.Add("Codigo_Bodega", GetType(String))
                dtExistenciasSAP.Columns.Add("Nombre_Bodega", GetType(String))
                dtExistenciasSAP.Columns.Add("Total_Almacen", GetType(Double))
                dtExistenciasSAP.Columns.Add("Lote", GetType(String))
                dtExistenciasSAP.Columns.Add("Cantidad_Lote", GetType(Double))
                dtExistenciasSAP.Columns.Add("Fecha_Vence", GetType(Date))

                dtExistenciasSAP.Rows.Clear()

                While Not oRecordset.EoF

                    Dim row As DataRow = dtExistenciasSAP.NewRow()
                    row("Codigo") = oRecordset.Fields.Item("Codigo").Value
                    row("Nombre") = oRecordset.Fields.Item("Nombre").Value
                    row("Codigo_Bodega") = oRecordset.Fields.Item("Codigo_Bodega").Value
                    row("Total_Almacen") = oRecordset.Fields.Item("Total_Almacen").Value
                    row("Lote") = oRecordset.Fields.Item("Lote").Value
                    row("Cantidad_Lote") = oRecordset.Fields.Item("Cantidad_Lote").Value
                    row("Fecha_Vence") = oRecordset.Fields.Item("Fecha_Vence").Value

                    dtExistenciasSAP.Rows.Add(row)

                    oRecordset.MoveNext()

                End While

                Get_Existencias_From_SAP_AsDataTable = dtExistenciasSAP

            End If

        Catch ex As Exception
            Throw ex
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    'Public Function CompareStockData(dtTOMWMS As DataTable, dtSAPB1 As DataTable) As DataTable

    '    Dim dtReport As New DataTable()
    '    ' Definir las columnas del DataTable de reporte
    '    dtReport.Columns.Add("Codigo", GetType(String))
    '    dtReport.Columns.Add("Lote", GetType(String))
    '    dtReport.Columns.Add("Fecha_Vence_TOMWMS", GetType(Date))
    '    dtReport.Columns.Add("Fecha_Vence_SAPB1", GetType(Date))
    '    dtReport.Columns.Add("Cantidad_TOMWMS", GetType(Double))
    '    dtReport.Columns.Add("Cantidad_SAPB1", GetType(Double))
    '    dtReport.Columns.Add("Diferencia_Cantidad", GetType(Double))
    '    dtReport.Columns.Add("Diferencia_Encontrada", GetType(Boolean))

    '    Try

    '        ' Agrupar y sumar las cantidades en el DataTable de SAP B1 por código de producto y lote
    '        Dim groupedSAP = (From row In dtSAPB1.AsEnumerable()
    '                          Group row By Codigo = row.Field(Of String)("Codigo"),
    '                       Lote = row.Field(Of String)("Lote")
    '          Into Group
    '                          Select New With {
    '              .Codigo = Codigo,
    '              .Lote = Lote,
    '              .Fecha_Vence_SAPB1 = Group.Max(Function(r) r.Field(Of Date)("Fecha_Vence")), ' Asume que queremos la fecha máxima en caso de múltiples entradas para un mismo lote
    '              .Cantidad_SAPB1 = Group.Sum(Function(r) r.Field(Of Double)("Cantidad_Lote"))
    '          }).ToList()

    '        ' Agrupar y sumar las cantidades en el DataTable de SAP B1 por código de producto y lote
    '        Dim groupedWMS = (From row In dtTOMWMS.AsEnumerable()
    '                          Group row By Codigo = row.Field(Of String)("Codigo"),
    '                       Lote = row.Field(Of String)("Lote")
    '          Into Group
    '                          Select New With {
    '              .Codigo = Codigo,
    '              .Lote = Lote,
    '              .Fecha_Vence = Group.Max(Function(r) r.Field(Of Date)("Fecha_Vence")), ' Asume que queremos la fecha máxima en caso de múltiples entradas para un mismo lote
    '              .CantidadUMBas = Group.Sum(Function(r) r.Field(Of Double)("CantidadUMBas"))
    '          }).ToList()

    '        ' Comparar los datos de ambos DataTables
    '        For Each rowTOMWMS As DataRow In dtTOMWMS.Rows

    '            Dim codigoProducto As String = rowTOMWMS("Codigo").ToString()
    '            Dim loteTOMWMS As String = rowTOMWMS("Lote").ToString()
    '            Dim fechaVenceTOMWMS As Date = If(IsDBNull(rowTOMWMS("Fecha_Vence")), Nothing, Convert.ToDateTime(rowTOMWMS("Fecha_Vence")))
    '            Dim cantidadTOMWMS As Double = If(IsDBNull(rowTOMWMS("CantidadUMBas")), 0, Double.Parse(rowTOMWMS("CantidadUMBas").ToString()))

    '            ' Buscar grupo correspondiente en SAP
    '            Dim sapMatch = groupedSAP.FirstOrDefault(Function(g) g.Codigo = codigoProducto And g.Lote = loteTOMWMS)

    '            ' Variables para SAP
    '            Dim fechaVenceSAPB1 As Date = Nothing
    '            Dim cantidadSAPB1 As Double = 0

    '            ' Si se encuentra un lote correspondiente en SAP, se usan sus datos
    '            If sapMatch IsNot Nothing Then
    '                cantidadSAPB1 = sapMatch.Cantidad_SAPB1
    '                fechaVenceSAPB1 = sapMatch.Fecha_Vence_SAPB1 ' Aquí asignamos la fecha de vencimiento de SAP
    '            End If

    '            ' Calcular la diferencia de cantidad
    '            Dim diferenciaCantidad As Double = cantidadTOMWMS - cantidadSAPB1
    '            Dim diferenciaEncontrada As Boolean = Not (Math.Abs(diferenciaCantidad) < 0.0001 AndAlso fechaVenceTOMWMS.Date = fechaVenceSAPB1.Date)

    '            ' Buscar si ya existe una fila en dtReport con el mismo Codigo y Lote
    '            Dim existingRow = dtReport.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("Codigo") = codigoProducto AndAlso r.Field(Of String)("Lote") = loteTOMWMS)

    '            If existingRow IsNot Nothing Then
    '                ' Si ya existe, sumar la cantidadTOMWMS a la columna Cantidad_TOMWMS
    '                existingRow("Cantidad_TOMWMS") = existingRow.Field(Of Double)("Cantidad_TOMWMS") + cantidadTOMWMS
    '                ' Recalcular la diferencia de cantidad
    '                existingRow("Diferencia_Cantidad") = existingRow.Field(Of Double)("Cantidad_TOMWMS") - existingRow.Field(Of Double)("Cantidad_SAPB1")
    '                ' Actualizar la diferencia encontrada
    '                existingRow("Diferencia_Encontrada") = Not (Math.Abs(existingRow.Field(Of Double)("Diferencia_Cantidad")) < 0.0001 AndAlso existingRow.Field(Of Date)("Fecha_Vence_TOMWMS").Date = existingRow.Field(Of Date)("Fecha_Vence_SAPB1").Date)
    '            Else
    '                ' Si no existe, agregar una nueva fila
    '                dtReport.Rows.Add(codigoProducto,
    '                              loteTOMWMS,
    '                              fechaVenceTOMWMS,
    '                              fechaVenceSAPB1,
    '                              cantidadTOMWMS,
    '                              cantidadSAPB1,
    '                              diferenciaCantidad,
    '                              diferenciaEncontrada)
    '            End If
    '        Next

    '    Catch ex As Exception
    '        Throw
    '    End Try

    '    ' Devolver el DataTable de reporte
    '    Return dtReport

    'End Function

    Public Function CompareStockData(dtTOMWMS As DataTable, dtSAPB1 As DataTable) As DataTable

        Dim dtResumen As New DataTable("Resumen")

        ' Definir las columnas del DataTable de resumen
        dtResumen.Columns.Add("Codigo", GetType(String))
        dtResumen.Columns.Add("Lote", GetType(String))
        dtResumen.Columns.Add("Fecha_Vence_TOMWMS", GetType(Date))
        dtResumen.Columns.Add("Fecha_Vence_SAPB1", GetType(Date))
        dtResumen.Columns.Add("Cantidad_TOMWMS", GetType(Double))
        dtResumen.Columns.Add("Cantidad_SAPB1", GetType(Double))
        dtResumen.Columns.Add("Diferencia_Cantidad", GetType(Double))
        dtResumen.Columns.Add("Diferencia_Encontrada", GetType(Boolean))
        If chkIncluirUbicacion.Checked Then dtResumen.Columns.Add("Ubicacion", GetType(String))
        If chkIncluirIdStock.Checked Then dtResumen.Columns.Add("IdStock", GetType(Integer))

        CompareStockData = Nothing

        Try
            ' Agrupar y sumar las cantidades en el DataTable de SAP B1 por código de producto y lote
            '  Dim groupedSAP = (From row In dtSAPB1.AsEnumerable()
            '                    Group row By Codigo = row.Field(Of String)("Codigo"),
            '             Lote = row.Field(Of String)("Lote")
            'Into Group
            '                    Select New With {
            '    .Codigo = Codigo,
            '    .Lote = Lote,
            '    .Fecha_Vence_SAPB1 = Group.Max(Function(r) r.Field(Of Date)("Fecha_Vence")), ' Asume que queremos la fecha máxima en caso de múltiples entradas para un mismo lote
            '    .Cantidad_SAPB1 = Group.Sum(Function(r) r.Field(Of Double)("Cantidad_Lote"))
            '}).ToList()

            Dim groupedSAP = (From row In dtSAPB1.AsEnumerable()
                              Group row By Codigo = row.Field(Of String)("Codigo"),
                  Lote = row.Field(Of String)("Lote"),
                  Fecha_Vence = row.Field(Of Date)("Fecha_Vence")
            Into Group
                              Select New With {
                      .Codigo = Codigo,
                      .Lote = Lote,
                      .Fecha_Vence_SAPB1 = Group.Where(Function(r) Not IsDBNull(r("Fecha_Vence"))).Max(Function(r) r.Field(Of Date)("Fecha_Vence")),
                      .Cantidad_SAPB1 = Group.Sum(Function(r) r.Field(Of Double)("Cantidad_Lote"))
                  }).ToList()


            ' Agrupar y sumar las cantidades en el DataTable de SAP B1 por código de producto y lote
            Dim groupedWMS = (From row In dtTOMWMS.AsEnumerable()
                              Group row By Codigo = row.Field(Of String)("Codigo"),
                       Lote = row.Field(Of String)("Lote"),
                       Fecha_Vence = row.Field(Of Date)("Fecha_Vence")
            Into Group
                              Select New With {
              .Codigo = Codigo,
              .Lote = Lote,
              .Fecha_Vence = Group.Max(Function(r) r.Field(Of Date)("Fecha_Vence")), ' Asume que queremos la fecha máxima en caso de múltiples entradas para un mismo lote
              .CantidadUMBas = Group.Sum(Function(r) r.Field(Of Double)("CantidadUMBas"))
          }).ToList()

            If chkIncluirUbicacion.Checked OrElse chkIncluirIdStock.Checked Then

                For Each rowTOMWMS As DataRow In dtTOMWMS.Rows

                    Dim codigoProducto As String = rowTOMWMS("Codigo").ToString()
                    Dim loteTOMWMS As String = rowTOMWMS("Lote").ToString()
                    Dim fechaVenceTOMWMS As Date = If(IsDBNull(rowTOMWMS("Fecha_Vence")), Nothing, Convert.ToDateTime(rowTOMWMS("Fecha_Vence")))
                    Dim cantidadTOMWMS As Double = If(IsDBNull(rowTOMWMS("CantidadUMBas")), 0, Double.Parse(rowTOMWMS("CantidadUMBas").ToString()))
                    Dim idUbicacion As String = rowTOMWMS("Ubicacion").ToString()
                    Dim IdStock As String = rowTOMWMS("IdStock").ToString()

                    ' Buscar grupo correspondiente en SAP
                    Dim sapMatch = groupedSAP.Find(Function(g) g.Codigo = codigoProducto And g.Lote = loteTOMWMS And g.Fecha_Vence_SAPB1 = fechaVenceTOMWMS)

                    ' Variables para SAP
                    Dim fechaVenceSAPB1 As Date = Nothing
                    Dim cantidadSAPB1 As Double = 0

                    ' Si se encuentra un lote correspondiente en SAP, se usan sus datos
                    If sapMatch IsNot Nothing Then
                        cantidadSAPB1 = sapMatch.Cantidad_SAPB1
                        fechaVenceSAPB1 = sapMatch.Fecha_Vence_SAPB1 ' Aquí asignamos la fecha de vencimiento de SAP
                    End If

                    ' Calcular la diferencia de cantidad
                    Dim diferenciaCantidad As Double = cantidadTOMWMS - cantidadSAPB1
                    Dim diferenciaEncontrada As Boolean = Not (Math.Abs(diferenciaCantidad) < 0.0001 AndAlso fechaVenceTOMWMS.Date = fechaVenceSAPB1.Date)

                    ' Buscar si ya existe una fila en dtResumen con el mismo Codigo y Lote
                    Dim existingRow = dtResumen.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("Codigo") = codigoProducto AndAlso
                                                                                 r.Field(Of String)("Lote") = loteTOMWMS AndAlso
                                                                                 r.Field(Of Date)("Fecha_Vence_TOMWMS").Date = r.Field(Of Date)("Fecha_Vence_SAPB1"))

                    If chkIncluirIdStock.Checked And Not chkIncluirUbicacion.Checked Then
                        existingRow = dtResumen.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("Codigo") = codigoProducto AndAlso
                                                                                 r.Field(Of String)("Lote") = loteTOMWMS AndAlso
                                                                                 r.Field(Of Date)("Fecha_Vence_TOMWMS").Date = r.Field(Of Date)("Fecha_Vence_SAPB1") AndAlso
                                                                                 r.Field(Of Integer)("IdStock") = IdStock)
                    ElseIf chkIncluirUbicacion.Checked And Not chkIncluirIdStock.Checked Then
                        existingRow = dtResumen.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("Codigo") = codigoProducto AndAlso
                                                                                 r.Field(Of String)("Lote") = loteTOMWMS AndAlso
                                                                                 r.Field(Of Date)("Fecha_Vence_TOMWMS").Date = r.Field(Of Date)("Fecha_Vence_SAPB1") AndAlso
                                                                                 r.Field(Of String)("Ubicacion") = idUbicacion)
                    Else
                        existingRow = dtResumen.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("Codigo") = codigoProducto AndAlso
                                                                                 r.Field(Of String)("Lote") = loteTOMWMS AndAlso
                                                                                 r.Field(Of Date)("Fecha_Vence_TOMWMS").Date = r.Field(Of Date)("Fecha_Vence_SAPB1") AndAlso
                                                                                 r.Field(Of Integer)("IdStock") = r.Field(Of Integer)("IdStock") AndAlso
                                                                                 r.Field(Of String)("Ubicacion") = idUbicacion)
                    End If

                    If existingRow IsNot Nothing Then
                        ' Si ya existe, sumar la cantidadTOMWMS a la columna Cantidad_TOMWMS
                        existingRow("Cantidad_TOMWMS") = existingRow.Field(Of Double)("Cantidad_TOMWMS") + cantidadTOMWMS
                        ' Recalcular la diferencia de cantidad
                        existingRow("Diferencia_Cantidad") = existingRow.Field(Of Double)("Cantidad_TOMWMS") - existingRow.Field(Of Double)("Cantidad_SAPB1")
                        ' Actualizar la diferencia encontrada
                        existingRow("Diferencia_Encontrada") = Not (Math.Abs(existingRow.Field(Of Double)("Diferencia_Cantidad")) < 0.0001 AndAlso existingRow.Field(Of Date)("Fecha_Vence_TOMWMS").Date = existingRow.Field(Of Date)("Fecha_Vence_SAPB1").Date)
                    Else

                        If chkIncluirUbicacion.Checked Then

                            If Not chkIncluirIdStock.Checked Then

                                ' Si no existe, agregar una nueva fila
                                dtResumen.Rows.Add(codigoProducto,
                                              loteTOMWMS,
                                              fechaVenceTOMWMS,
                                              fechaVenceSAPB1,
                                              cantidadTOMWMS,
                                              cantidadSAPB1,
                                              diferenciaCantidad,
                                              diferenciaEncontrada,
                                              idUbicacion)
                            Else

                                ' Si no existe, agregar una nueva fila
                                dtResumen.Rows.Add(codigoProducto,
                                              loteTOMWMS,
                                              fechaVenceTOMWMS,
                                              fechaVenceSAPB1,
                                              cantidadTOMWMS,
                                              cantidadSAPB1,
                                              diferenciaCantidad,
                                              diferenciaEncontrada,
                                              idUbicacion,
                                              IdStock)

                            End If

                        Else

                            If Not chkIncluirIdStock.Checked Then

                                ' Si no existe, agregar una nueva fila
                                dtResumen.Rows.Add(codigoProducto,
                                              loteTOMWMS,
                                              fechaVenceTOMWMS,
                                              fechaVenceSAPB1,
                                              cantidadTOMWMS,
                                              cantidadSAPB1,
                                              diferenciaCantidad,
                                              diferenciaEncontrada)
                            Else

                                ' Si no existe, agregar una nueva fila
                                dtResumen.Rows.Add(codigoProducto,
                                              loteTOMWMS,
                                              fechaVenceTOMWMS,
                                              fechaVenceSAPB1,
                                              cantidadTOMWMS,
                                              cantidadSAPB1,
                                              diferenciaCantidad,
                                              diferenciaEncontrada,
                                              IdStock)

                            End If

                        End If

                    End If


                Next

            Else

                For Each rowTOMWMS In groupedWMS

                    Dim codigoProducto As String = rowTOMWMS.Codigo
                    Dim loteTOMWMS As String = rowTOMWMS.Lote
                    Dim fechaVenceTOMWMS As Date = If(IsDBNull(rowTOMWMS.Fecha_Vence), New Date(1900, 1, 1), rowTOMWMS.Fecha_Vence)
                    Dim cantidadTOMWMS As Double = If(IsDBNull(rowTOMWMS.CantidadUMBas), 0, rowTOMWMS.CantidadUMBas)
                    Dim idUbicacion As String = ""
                    Dim IdStock As String = ""

                    ' Buscar grupo correspondiente en SAP
                    Dim sapMatch = groupedSAP.FirstOrDefault(Function(g) g.Codigo = codigoProducto And g.Lote = loteTOMWMS And g.Fecha_Vence_SAPB1 = fechaVenceTOMWMS)

                    ' Variables para SAP
                    Dim fechaVenceSAPB1 As Date = Nothing
                    Dim cantidadSAPB1 As Double = 0

                    ' Si se encuentra un lote correspondiente en SAP, se usan sus datos
                    If sapMatch IsNot Nothing Then
                        cantidadSAPB1 = sapMatch.Cantidad_SAPB1
                        fechaVenceSAPB1 = sapMatch.Fecha_Vence_SAPB1 ' Aquí asignamos la fecha de vencimiento de SAP
                    End If

                    ' Calcular la diferencia de cantidad
                    Dim diferenciaCantidad As Double = cantidadTOMWMS - cantidadSAPB1
                    Dim diferenciaEncontrada As Boolean = Not (Math.Abs(diferenciaCantidad) < 0.0001 AndAlso fechaVenceTOMWMS.Date = fechaVenceSAPB1.Date)

                    ' Buscar si ya existe una fila en dtResumen con el mismo Codigo y Lote
                    Dim existingRow = dtResumen.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("Codigo") = codigoProducto AndAlso r.Field(Of String)("Lote") = loteTOMWMS AndAlso r.Field(Of Date)("Fecha_Vence_TOMWMS").Date = r.Field(Of Date)("Fecha_Vence_SAPB1"))

                    If existingRow IsNot Nothing Then
                        ' Si ya existe, sumar la cantidadTOMWMS a la columna Cantidad_TOMWMS
                        existingRow("Cantidad_TOMWMS") = existingRow.Field(Of Double)("Cantidad_TOMWMS") + cantidadTOMWMS
                        ' Recalcular la diferencia de cantidad
                        existingRow("Diferencia_Cantidad") = existingRow.Field(Of Double)("Cantidad_TOMWMS") - existingRow.Field(Of Double)("Cantidad_SAPB1")
                        ' Actualizar la diferencia encontrada
                        existingRow("Diferencia_Encontrada") = Not (Math.Abs(existingRow.Field(Of Double)("Diferencia_Cantidad")) < 0.0001 AndAlso existingRow.Field(Of Date)("Fecha_Vence_TOMWMS").Date = existingRow.Field(Of Date)("Fecha_Vence_SAPB1").Date)
                    Else

                        dtResumen.Rows.Add(codigoProducto,
                                           loteTOMWMS,
                                           fechaVenceTOMWMS,
                                           fechaVenceSAPB1,
                                           cantidadTOMWMS,
                                           cantidadSAPB1,
                                           diferenciaCantidad,
                                           diferenciaEncontrada)

                    End If

                Next

            End If

            ' Comparar los datos de ambos DataTables
            'For Each rowTOMWMS As DataRow In dtTOMWMS.Rows

            '    Dim codigoProducto As String = rowTOMWMS("Codigo").ToString()
            '    Dim loteTOMWMS As String = rowTOMWMS("Lote").ToString()
            '    Dim fechaVenceTOMWMS As Date = If(IsDBNull(rowTOMWMS("Fecha_Vence")), Nothing, Convert.ToDateTime(rowTOMWMS("Fecha_Vence")))
            '    Dim cantidadTOMWMS As Double = If(IsDBNull(rowTOMWMS("CantidadUMBas")), 0, Double.Parse(rowTOMWMS("CantidadUMBas").ToString()))
            '    Dim idUbicacion As String = rowTOMWMS("Ubicacion").ToString()
            '    Dim IdStock As String = rowTOMWMS("IdStock").ToString()

            '    ' Buscar grupo correspondiente en SAP
            '    Dim sapMatch = groupedSAP.FirstOrDefault(Function(g) g.Codigo = codigoProducto And g.Lote = loteTOMWMS And g.Fecha_Vence_SAPB1 = fechaVenceTOMWMS)

            '    ' Variables para SAP
            '    Dim fechaVenceSAPB1 As Date = Nothing
            '    Dim cantidadSAPB1 As Double = 0

            '    ' Si se encuentra un lote correspondiente en SAP, se usan sus datos
            '    If sapMatch IsNot Nothing Then
            '        cantidadSAPB1 = sapMatch.Cantidad_SAPB1
            '        fechaVenceSAPB1 = sapMatch.Fecha_Vence_SAPB1 ' Aquí asignamos la fecha de vencimiento de SAP
            '    End If

            '    ' Calcular la diferencia de cantidad
            '    Dim diferenciaCantidad As Double = cantidadTOMWMS - cantidadSAPB1
            '    Dim diferenciaEncontrada As Boolean = Not (Math.Abs(diferenciaCantidad) < 0.0001 AndAlso fechaVenceTOMWMS.Date = fechaVenceSAPB1.Date)

            '    ' Buscar si ya existe una fila en dtResumen con el mismo Codigo y Lote
            '    Dim existingRow = dtResumen.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("Codigo") = codigoProducto AndAlso r.Field(Of String)("Lote") = loteTOMWMS AndAlso r.Field(Of Date)("Fecha_Vence_TOMWMS").Date = r.Field(Of Date)("Fecha_Vence_SAPB1"))

            '    If existingRow IsNot Nothing Then
            '        ' Si ya existe, sumar la cantidadTOMWMS a la columna Cantidad_TOMWMS
            '        existingRow("Cantidad_TOMWMS") = existingRow.Field(Of Double)("Cantidad_TOMWMS") + cantidadTOMWMS
            '        ' Recalcular la diferencia de cantidad
            '        existingRow("Diferencia_Cantidad") = existingRow.Field(Of Double)("Cantidad_TOMWMS") - existingRow.Field(Of Double)("Cantidad_SAPB1")
            '        ' Actualizar la diferencia encontrada
            '        existingRow("Diferencia_Encontrada") = Not (Math.Abs(existingRow.Field(Of Double)("Diferencia_Cantidad")) < 0.0001 AndAlso existingRow.Field(Of Date)("Fecha_Vence_TOMWMS").Date = existingRow.Field(Of Date)("Fecha_Vence_SAPB1").Date)
            '    Else

            '        If chkIncluirUbicacion.Checked Then

            '            If Not chkIncluirIdStock.Checked Then

            '                ' Si no existe, agregar una nueva fila
            '                dtResumen.Rows.Add(codigoProducto,
            '                                  loteTOMWMS,
            '                                  fechaVenceTOMWMS,
            '                                  fechaVenceSAPB1,
            '                                  cantidadTOMWMS,
            '                                  cantidadSAPB1,
            '                                  diferenciaCantidad,
            '                                  diferenciaEncontrada,
            '                                  idUbicacion)
            '            Else

            '                ' Si no existe, agregar una nueva fila
            '                dtResumen.Rows.Add(codigoProducto,
            '                                  loteTOMWMS,
            '                                  fechaVenceTOMWMS,
            '                                  fechaVenceSAPB1,
            '                                  cantidadTOMWMS,
            '                                  cantidadSAPB1,
            '                                  diferenciaCantidad,
            '                                  diferenciaEncontrada,
            '                                  idUbicacion,
            '                                  IdStock)

            '            End If

            '        Else

            '            dtResumen.Rows.Add(codigoProducto,
            '                                  loteTOMWMS,
            '                                  fechaVenceTOMWMS,
            '                                  fechaVenceSAPB1,
            '                                  cantidadTOMWMS,
            '                                  cantidadSAPB1,
            '                                  diferenciaCantidad,
            '                                  diferenciaEncontrada)

            '        End If

            '    End If

            'Next

            CompareStockData = dtResumen

        Catch ex As Exception
            Throw
        End Try

    End Function

    Private Sub chkIncluirIdStock_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkIncluirIdStock.CheckedChanged
        Cargar_Datos()
    End Sub

    Private Sub chkIncluirUbicacion_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkIncluirUbicacion.CheckedChanged
        Cargar_Datos()
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        Try

            Dim view As GridView = TryCast(sender, GridView)
            If view IsNot Nothing Then
                ' Comprobar si la fila actual tiene una diferencia
                Dim diferenciaEncontrada As Boolean = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Diferencia_Encontrada"))
                If diferenciaEncontrada Then
                    ' Cambiar el color de fondo de la fila a rojo claro si hay una diferencia
                    e.Appearance.BackColor = Color.LightCoral
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub



End Class