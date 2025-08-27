Imports System.IO
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmIngreso_List

    Public Property IsLoading As Boolean = True

    Public Sub New()
        InitializeComponent()
        IsLoading = False
    End Sub

    Public Property Modo As pModo
    Public Property gBeRecepcion As New clsBeTrans_re_enc

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub mnuSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub


    Private DT As New DataTable("Ingreso")

    Private Sub Init_DataTable_Productos()

        DT = New DataTable("Ingreso")

        DT.Columns.Add("IdBodega", GetType(Integer))
        DT.Columns.Add("IdRecepcionEnc", GetType(Integer))
        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Poliza", GetType(String))
        DT.Columns.Add("Codigo_Bodega", GetType(String))
        DT.Columns.Add("IdCompraEnc", GetType(Integer))
        DT.Columns.Add("Codigo_Proveedor", GetType(String))
        DT.Columns.Add("Nombre_Proveedor", GetType(String))
        DT.Columns.Add("No_DocumentoOC", GetType(String))
        DT.Columns.Add("ReferenciaOC", GetType(String))
        DT.Columns.Add("Fecha", GetType(DateTime))
        DT.Columns.Add("Estado", GetType(String))
        DT.Columns.Add("TipoTrans", GetType(String))
        DT.Columns.Add("Descripcion", GetType(String))
        DT.Columns.Add("activo", GetType(Boolean))
        DT.Columns.Add("Usuario_Agrego", GetType(String))
        DT.Columns.Add("Operador_HH", GetType(String))
        DT.Columns.Add("Fecha_Agrego", GetType(DateTime))
        DT.Columns.Add("CodigoProd", GetType(String))
        DT.Columns.Add("BarraProd", GetType(String))
        DT.Columns.Add("NombreProd", GetType(String))
        DT.Columns.Add("Recibido", GetType(Boolean))
        DT.Columns.Add("UM", GetType(String))
        DT.Columns.Add("EstadoProd", GetType(String))
        DT.Columns.Add("PresProd", GetType(String))
        DT.Columns.Add("Licencia", GetType(String))
        DT.Columns.Add("lote", GetType(String))
        DT.Columns.Add("fecha_vence", GetType(DateTime))
        DT.Columns.Add("IdPedidoEncDevolucion", GetType(Integer))
        DT.Columns.Add("No_Documento_Devolucion", GetType(String))
        DT.Columns.Add("Nombre_Cliente_Devolucion", GetType(String))
        DT.Columns.Add("Clasificacion", GetType(Integer))
        DT.Columns.Add("IdUbicacionRecepcion", GetType(Integer))
        DT.Columns.Add("Area", GetType(String))
        DT.Columns.Add("Familia", GetType(String))
        DT.Columns.Add("Parametro_A", GetType(String))
        DT.Columns.Add("Parametro_B", GetType(String))
        DT.Columns.Add("Marca", GetType(String))
        DT.Columns.Add("Codigo_Regimen_Ingreso", GetType(String))
        DT.Columns.Add("Carta_Cupo", GetType(String))
        DT.Columns.Add("Numero_Orden_Ingreso", GetType(String))
        DT.Columns.Add("No_Contenedor", GetType(String))
        DT.Columns.Add("Placa_Contenedor_Ingreso", GetType(String))
        DT.Columns.Add("Poliza_Activa", GetType(String))
        DT.Columns.Add("Dua_Ingreso", GetType(String))

    End Sub


    Private Sub Listar_Ingresos()

        Dim pBodega As clsBeBodega

        Try

            If IsLoading Then Exit Sub

            Init_DataTable_Productos()
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            DT.Clear()

            pBodega = AP.Bodega

            DT = clsLnTrans_re_enc.Get_All_Ingreso_By_RangoFechas_and_IdBodega(chkActivos.Checked,
                                                                               dtpFechaDel.Value,
                                                                               dtpFechaAl.Value,
                                                                               pBodega)


            IsLoading = True

            Dgrid.DataSource = DT

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ShowFooter = True

                'Set_LayOut_Grid()

                GridView1.ExpandAllGroups()

                GridView1.Columns("Poliza_Activa").Visible = False

                GridView1.Columns("Recibido").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Recibido").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Recibido").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Recibido").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Fecha_Agrego").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView1.Columns("Fecha_Agrego").DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"

                GridView1.Columns("Fecha").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView1.Columns("Fecha").DisplayFormat.FormatString = "G"

                GridView1.OptionsView.ColumnAutoWidth = False

                GridView1.ExpandAllGroups()
                GridView1.BestFitColumns(True)

            End If


            Set_Label_Personalizados()

            If GridView1.RowCount > 0 Then
                BarStaticItem1.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            IsLoading = False
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            'Listar_Ingresos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            ' Listar_Ingresos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    'Private Sub GridView1_RowStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowStyle

    '    Try

    '        GridView1.OptionsBehavior.Editable = False
    '        GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
    '        GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
    '        GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
    '        GridView1.OptionsSelection.EnableAppearanceHideSelection = True
    '        GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
    '        GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
    '        GridView1.Appearance.FocusedRow.ForeColor = Color.White
    '        GridView1.Appearance.SelectedRow.ForeColor = Color.White
    '        GridView1.Appearance.SelectedRow.Options.UseBackColor = True
    '        GridView1.Appearance.SelectedRow.Options.UseForeColor = True

    '    Catch ex As Exception

    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '        Text,
    '        MessageBoxButtons.OK,
    '        MessageBoxIcon.Error)

    '        Dim vMsgError As String = ex.Message
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)

    '    End Try

    'End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

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

        If e.Column.FieldName = "cantidad" Then
            e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
            e.Appearance.BackColor = Color.Green
            e.Appearance.BackColor2 = Color.White
        End If

    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Ingresos()
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

        Dim reportHeader As String = vbNewLine & "Listado de Ingresos"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        Listar_Ingresos()
        mnuActualizar.Enabled = True
    End Sub

    Private vNombreArchivoLayOutGrid As String = ""
    Private Sub frmIngreso_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try
            vNombreArchivoLayOutGrid = "frmIngreso_List.xml"

            Init_DataTable_Productos()

            lblInfo.Caption = "Mostrando solo registros para bodega: " & AP.NomBodega

            Listar_Ingresos()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Public Sub Set_Label_Personalizados()

        Try

            Dim BeConfiguracion As New clsBeConfiguracion_alias_campos
            Dim TheColumnToChange As GridColumn = Nothing

            If Not lConfiguracionAliasCampos Is Nothing Then

                If lConfiguracionAliasCampos.Count > 0 Then

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_a")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView1.Columns.ColumnByFieldName("parametro_a")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_b")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView1.Columns.ColumnByFieldName("parametro_b")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "familia")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView1.Columns.ColumnByFieldName("familia")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "clasificacion")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView1.Columns.ColumnByFieldName("Clasificacion")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

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


    Private Sub GridView_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        Try

            If IsLoading Then Exit Sub

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

            'mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Set_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuExportarExcel.ItemClick
        Exportar_Grid_A_Excel(Dgrid, "WMS_Ingresos.xlsx")
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

End Class