Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraSplashScreen

Public Class frmExistenciasPorEstado

    Private IsLoading As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public listaStock As New List(Of clsBeVW_stock_res)
    Private DT As New DataTable("ExistenciasPorEstado")

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub SetDatataTable()

        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Código Barra", GetType(String))
        DT.Columns.Add("Cantidad UMBas", GetType(Double))
        DT.Columns.Add("Unidad Medida", GetType(String))
        DT.Columns.Add("Cantidad Presentación", GetType(Double))
        DT.Columns.Add("Presentación", GetType(String))
        DT.Columns.Add("Estado", GetType(String))
        DT.Columns.Add("Cantidad Reservada", GetType(Double))
        DT.Columns.Add("Disponible UMBas", GetType(Double))
        DT.Columns.Add("Disponible Presentación", GetType(Double))
        DT.Columns.Add("Lic Plate", GetType(String))
        DT.Columns.Add("Ubicación", GetType(String))

        DT.Columns.Add("codigo_poliza", GetType(String))
        DT.Columns.Add("numero_orden", GetType(String))

    End Sub

    Private Sub Cargar_Datos()

        Try

            If Not IsLoading Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            End If

            DT = clsLnStock.Get_Reporte_Stock_DataTable(cmbBodega.EditValue, cmbPropietarioBodega.EditValue)

            grdExistenciasEstado.DataSource = DT

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("Estado").Group()

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "CantidadUMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("CantidadUMBas")}
                GridView1.GroupSummary.Add(item)

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "CantidadPresentacion", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("CantidadPresentacion")}
                GridView1.GroupSummary.Add(item1)


                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_UMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Disponible_UMBas")}
                GridView1.GroupSummary.Add(item2)

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_Presentación", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Disponible_Presentación")}
                GridView1.GroupSummary.Add(item3)


                GridView1.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("CantidadUMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("CantidadPresentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Disponible_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Disponible_Presentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Disponible_Presentación").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "G"

                GridView1.ExpandAllGroups()
                GridView1.BestFitColumns(True)

                Restore_LayOut()

            End If

            Set_Label_Personalizados()

            lblReg.Caption = String.Format("Registros: {0}", GridView1.RowCount)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

        Application.DoEvents()

    End Sub

    Private Sub frmExistenciasPorEstado_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            '#EJC20210716:Restaurar LayoutGrid en frmstock_especifico_list.vb
            vNombreArchivoLayOutGrid = "frmExistenciasPorEstado.xml"

            IsLoading = True

            SetDatataTable()

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsLoading = False
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdExistenciasEstado, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True, True, 12)
            Exit Sub
            GridView1.OptionsPrint.ExpandAllDetails = True
            GridView1.OptionsPrint.PrintDetails = True

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
            printLink.Component = grdExistenciasEstado
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Detalle de existencias por estado de producto"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)

            Cargar_Datos()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

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

    Private Sub cmdExportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExportarExcel.ItemClick
        Exportar_Grid_A_Excel(grdExistenciasEstado, "WMS_ExistenciasConEstado.xlsx")
    End Sub

    Private Sub frmExistenciasPorEstado_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            Cargar_Datos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    '#EJC20210716_1159AM:Guardar LayoutGrid en frmStock_Especifico_List.vb
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

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
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

    Private Sub Restore_LayOut()

        '#EJC20210716:Restaurar LayoutGrid en frmstock_especifico_list.vb
        'vNombreArchivoLayOutGrid = "frmExistenciasUbicacion.xml"

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

                        TheColumnToChange = GridView1.Columns.ColumnByFieldName("Familia")

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

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        If IsLoading Then Exit Sub

        Guardar_Layout()

    End Sub

    Private Sub Guardar_Layout()

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

End Class



