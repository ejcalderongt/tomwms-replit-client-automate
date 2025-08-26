Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraSplashScreen

Public Class frmLicenciasPorUbicacion

    Public pIdPropietarioBodega As Integer
    Public Property Termino_Carga_De_Datos As Boolean = False

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGridStock As String = "frmLicenciasPorUbicacion.xml"
    Private vNombreArchivoLayOutGridLicenciasInconsistentes As String = "frmLicenciasInconsistentes.xml"

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Dim DTStock As New DataTable
    Dim DTLicenciasInconsistentes As New DataTable

    Private Is_Loading As Boolean = True

    Private Sub Listar_Stock_With_DT()

        Try

            grdStock.DataSource = Nothing

            DTStock = clsLnStock.Get_Licencias_Por_Ubicacion(cmbBodega.EditValue)

            If DTStock IsNot Nothing Then

                If DTStock.Rows.Count > 0 Then

                    grdStock.DataSource = DTStock
                    grdvStock.Columns("Cant_Lic").SummaryItem.SummaryType = SummaryItemType.Sum
                    grdvStock.Columns("Cant_Lic").SummaryItem.DisplayFormat = "Suma = {0}"
                    grdvStock.Columns("Ubicacion").SummaryItem.SummaryType = SummaryItemType.Count
                    grdvStock.Columns("Ubicacion").SummaryItem.DisplayFormat = "Cant = {0}"
                    grdvStock.OptionsView.ShowFooter = True
                    grdvStock.BestFitColumns()

                End If

            End If

            Restore_LayOut()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Try
                SplashScreenManager.CloseForm(False)
            Catch ex As Exception
            End Try
        End Try

    End Sub

    Private Sub Listar_Licencias_Inconsistentes_With_DT()

        Try

            grdLicenciasInconsistentes.DataSource = Nothing

            DTLicenciasInconsistentes = clsLnStock.Get_Licencias_Inconsistente_Por_Ubicacion(cmbBodega.EditValue)

            If DTLicenciasInconsistentes IsNot Nothing Then

                If DTLicenciasInconsistentes.Rows.Count > 0 Then

                    grdLicenciasInconsistentes.DataSource = DTLicenciasInconsistentes

                    grdvLicenciasInconsistentes.Columns("Licencia").SummaryItem.SummaryType = SummaryItemType.Count
                    grdvLicenciasInconsistentes.Columns("Licencia").SummaryItem.DisplayFormat = "Count = {0}"
                    grdvLicenciasInconsistentes.OptionsView.ShowFooter = True
                    grdvLicenciasInconsistentes.BestFitColumns()

                End If

            End If

            Restore_LayOut()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Try
                SplashScreenManager.CloseForm(False)
            Catch ex As Exception
            End Try
        End Try

    End Sub

    Private Sub Imprimir_Vista_Licencias_Por_Ubicacion()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf grdvLicenciasPorStockPrintableComponentLink_CreateReportHeaderArea

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
            printLink.Component = grdStock
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Imprimir_Vista_Licencias_Inconsistentes()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf grdvLIcenciasInconsistentesPrintableComponentLink_CreateReportHeaderArea

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
            printLink.Component = grdLicenciasInconsistentes
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub grdvLicenciasPorStockPrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Stock"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub grdvLIcenciasInconsistentesPrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Licencias Inconsistentes"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub frmLicenciasPorUbicacion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            mnuGuardarLayoutGrid.Visibility = BarItemVisibility.Never

            vNombreArchivoLayOutGridStock = "frmLicenciasPorUbicacion.xml"
            vNombreArchivoLayOutGridLicenciasInconsistentes = "frmLicenciasInconsistentes.xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            Actualizar_Cosas()

        Catch ex As Exception

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Actualizar_Cosas()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
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

    Private Sub cmdExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExcel.ItemClick
        Exportar_Grid_A_Excel(grdStock, "WMS_ExistenciasConUbicacion.xlsx")
    End Sub

    Private Sub Restore_LayOut()

        '#EJC20210716:Restaurar LayoutGrid en frmstock_especifico_list.vb
        'vNombreArchivoLayOutGrid = "frmLicenciasPorUbicacion.xml"

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGridStock)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                grdvStock.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
            End If


            Dim BeConfiguracionUsuarioDet1 As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet1 = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGridLicenciasInconsistentes)

            If Not BeConfiguracionUsuarioDet1 Is Nothing Then
                grdvLicenciasInconsistentes.RestoreLayoutFromStream(BeConfiguracionUsuarioDet1.Stream_Template)
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub GridView1_Layout(sender As Object, e As EventArgs)

        If Is_Loading Then Exit Sub

        Guardar_Layout()

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            clsLnConfiguracion_usuario_enc.Delete_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGridStock)


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

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick

        Try

            mnuGuardarLayoutGrid.Enabled = False


            Dim Ms As New MemoryStream
            grdvStock.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGridStock,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            mnuGuardarLayoutGrid.Enabled = True
        End Try

    End Sub

    Private Sub Set_Label_Personalizados()


        Try

            Dim BeConfiguracion As New clsBeConfiguracion_alias_campos
            Dim TheColumnToChange As GridColumn = Nothing

            If Not lConfiguracionAliasCampos Is Nothing Then

                If lConfiguracionAliasCampos.Count > 0 Then

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_a")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = grdvStock.Columns.ColumnByFieldName("parametro_a")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_b")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = grdvStock.Columns.ColumnByFieldName("parametro_b")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "familia")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = grdvStock.Columns.ColumnByFieldName("Familia")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "clasificacion")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = grdvStock.Columns.ColumnByFieldName("Clasificacion")

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

    Private Sub Guardar_Layout()

        Try

            Dim Ms As New MemoryStream
            grdvStock.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGridStock,
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

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        Actualizar_Cosas()
    End Sub

    Private Sub Actualizar_Cosas()

        mnuActualizar.Enabled = False

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Buscando...")

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)

            DTStock.Clear()
            DTLicenciasInconsistentes.Clear()

            Is_Loading = True

            Listar_Stock_With_DT()

            Listar_Licencias_Inconsistentes_With_DT()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            mnuActualizar.Enabled = True
            SplashScreenManager.CloseForm(False)
            Is_Loading = False
        End Try

    End Sub

    Private Sub grdvLicenciasInconsistentes_Layout(sender As Object, e As EventArgs) Handles grdvLicenciasInconsistentes.Layout

        Try

            Dim Ms As New MemoryStream
            grdvLicenciasInconsistentes.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGridLicenciasInconsistentes,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub grdvStock_Layout(sender As Object, e As EventArgs) Handles grdvStock.Layout

        Try

            If Not Is_Loading Then

                Dim Ms As New MemoryStream
                grdvStock.SaveLayoutToStream(Ms)
                Ms.Seek(0, SeekOrigin.Begin)
                Dim MsReader As New StreamReader(Ms)
                Dim LayoutToText As String = MsReader.ReadToEnd()

                clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                              AP.UsuarioAp.IdUsuario,
                                                              AP.HostName,
                                                              vNombreArchivoLayOutGridStock,
                                                              LayoutToText)

                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always


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

    Private Sub grdLicenciasInconsistentes_Layout(sender As Object, e As EventArgs) Handles grdLicenciasInconsistentes.Layout

        Try

            If Not Is_Loading Then

                Dim Ms As New MemoryStream
                grdvLicenciasInconsistentes.SaveLayoutToStream(Ms)
                Ms.Seek(0, SeekOrigin.Begin)
                Dim MsReader As New StreamReader(Ms)
                Dim LayoutToText As String = MsReader.ReadToEnd()

                clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                              AP.UsuarioAp.IdUsuario,
                                                              AP.HostName,
                                                              vNombreArchivoLayOutGridLicenciasInconsistentes,
                                                              LayoutToText)

                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always


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

    Private Sub mnuImprimirLicenciasPorUbicaicon_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuImprimirLicenciasPorUbicaicon.ItemClick
        Imprimir_Vista_Licencias_Por_Ubicacion()
    End Sub

    Private Sub mnuLicenciasInconsistentes_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuLicenciasInconsistentes.ItemClick
        Imprimir_Vista_Licencias_Inconsistentes()
    End Sub

End Class