Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmValorizacionStock

    Public DTValoracionStockEnc As New DataTable

    Private Is_Loading As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Close()
    End Sub

    Private Sub frmValorizacionStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            '#EJC20210716:Restaurar LayoutGrid en frmstock_especifico_list.vb
            vNombreArchivoLayOutGrid = "frmValorizacionStock.xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Sub Cargar_Datos()

        Try

            Is_Loading = True

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Consultando datos...")

            grdValorizacionStock.BeginUpdate()

            DTValoracionStockEnc = clsLnStock.Get_Reporte_Valoracion_Stock_DataTable(cmbBodega.EditValue,
                                                                                     cmbPropietario.EditValue)

            grdValorizacionStock.DataSource = DTValoracionStockEnc

            grdValorizacionStock.EndUpdate()

            grdValorizacionStock.ForceInitialize()

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ShowFooter = True

                Try

                    GridView1.Columns("Cantidad_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cantidad_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_UMBas").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Cantidad_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Pres").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cantidad_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Pres").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Disponible_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Disponible_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Disponible_Presentacion").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Disponible_Presentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Disponible_Presentacion").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Costo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Costo").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Total").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Total").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Total").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Total").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try

            End If

            lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            GridView1.OptionsView.ColumnAutoWidth = False

            GridView1.BestFitColumns()

            Restore_LayOut()

            Set_Label_Personalizados()

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            Is_Loading = False
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Imprimir_Vista()

        Try

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
            printLink.Component = grdValorizacionStock
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

        Dim reportHeader As String = vbNewLine & "Reporte de Stock"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        BarButtonItem2.Enabled = False
        Cargar_Datos()
        BarButtonItem2.Enabled = True
    End Sub

    Private Sub grdValorizacionStock_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdValorizacionStock.ViewRegistered
        Try

            Dim gridView As GridView = e.View

            If gridView.IsDetailView Then

                gridView.Columns("codigo").Caption = "Código"
                gridView.Columns("nombre").Caption = "Nombre"
                gridView.Columns("fecha_ingreso").Caption = "Fecha Ingreso"
                gridView.Columns("CantidadSF").Caption = "Cantidad UMBAS"
                gridView.Columns("fecha_vence").Caption = "Fecha Vence"
                gridView.Columns("costo").Caption = "Costo"

            End If

            If gridView.Columns.Count > 0 Then

                gridView.OptionsView.ShowFooter = True

                gridView.Columns("CantidadSF").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("CantidadSF").SummaryItem.DisplayFormat = "{0:n6}"
                gridView.Columns("CantidadSF").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("CantidadSF").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("CantidadReservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("CantidadReservada").DisplayFormat.FormatString = "{0:n6}"
                gridView.Columns("CantidadReservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("CantidadReservada").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("costo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("costo").DisplayFormat.FormatString = "{0:n6}"
                gridView.Columns("costo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("costo").SummaryItem.DisplayFormat = "{0:n6}"

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

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

            'GT15032022_1707: se omite, porque al cambiar de bodega, no mantiene al propietario y lanza una consulta sin uso
            'Cargar_Datos()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmValorizacionStock_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            'GT15032022_1709: se omite porque al cargar, lanza una consulta no útil a la primera bodega con el primer
            'propietario seteado
            'Cargar_Datos()
            mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    '#EJC20210716_1159AM:Guardar LayoutGrid en frmStock_Especifico_List.vb
    Private vNombreArchivoLayOutGrid As String = ""
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

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

            'If File.Exists(vNombreArchivoLayOutGrid) Then
            '    File.Delete(vNombreArchivoLayOutGrid)
            '    mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            'End If

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

        If Is_Loading Then Exit Sub

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