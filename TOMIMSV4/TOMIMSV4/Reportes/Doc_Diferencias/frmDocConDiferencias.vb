Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraSplashScreen

Public Class frmDocConDiferencias

    Private DT As New DataTable("DocConDiferencias")
    Public pBeListaProductos As New List(Of Integer)
    Private vNombreArchivoLayOutGrid As String = "DocIngresosConDiferencias"
    Public ListaMovimientos As New List(Of clsBeVW_Doc_Con_Diferencias)
    Public RepMovEnUnaFecha As New List(Of clsBeStockEnUnaFecha)
    Public Property Modo As pModo
    Public Property ProductoEspecifico As New clsBeProducto
    Public Property ModoDepuracion As Boolean = False
    Public Property IsLoading As Boolean = True
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum


    Private Sub SetDatataTable()

        DT.Columns.Add("Orden_Compra", GetType(String))
        DT.Columns.Add("Codigo_Producto", GetType(String))
        DT.Columns.Add("Nombre_Producto", GetType(String))
        DT.Columns.Add("Cantidad", GetType(Double))
        DT.Columns.Add("Cantidad_Recibida", GetType(Double))
        DT.Columns.Add("Diferencia", GetType(Double))
        DT.Columns.Add("Presentacion", GetType(String))
        DT.Columns.Add("Bodega", GetType(String))
        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Poliza", GetType(String))
        DT.Columns.Add("Id_Tipo_Ingreso_OC", GetType(Integer))
        DT.Columns.Add("Nombre_Ingreso_OC", GetType(String))
        DT.Columns.Add("Id_Producto_Bodega", GetType(Integer))
        DT.Columns.Add("UMBas", GetType(String))
        DT.Columns.Add("Estado", GetType(String))
        DT.Columns.Add("Fecha_Creación", GetType(Date))

    End Sub

    Private Sub Generar_Reporte()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Generando...")

            Dim BeStockEnFecha As New clsBeStockEnUnaFecha
            Dim Idx As Integer = -1
            Dim Idx1 As Integer = -1

            ListaMovimientos.Clear()

            lblPrg.Visible = True

            If Get_Lista_Movimientos() Then

                Llena_Grid2()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
            prg.Visible = False
        End Try

    End Sub

    Private Sub Llena_Grid2()

        Try

            IsLoading = True

            Dim fila As Object = cmbPropietario.GetSelectedDataRow
            Dim IdPropietario As Integer = fila.Item("IdPropietario")

            If txtIdProducto.Text.Trim <> "" AndAlso Not ProductoEspecifico Is Nothing Then
                Dim IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(ProductoEspecifico.IdProducto, cmbBodega.EditValue)
                DT = clsLnVW_Doc_con_Diferencias.Get_All_Movimientos_DT_By_IdProducto(dtpFechaDesde.Value,
                                                                                        dtpfechaHasta.Value,
                                                                                        IdProductoBodega,
                                                                                        cmbBodega.EditValue,
                                                                                        IdPropietario)
            Else
                DT = clsLnVW_Doc_con_Diferencias.Get_All_Movimientos_DT_By_IdProducto(dtpFechaDesde.Value,
                                                                                      dtpfechaHasta.Value,
                                                                                      0,
                                                                                      cmbBodega.EditValue,
                                                                                      IdPropietario)
            End If

            dgrid.DataSource = DT

            GridView1.OptionsView.ShowFooter = True

            If GridView1.RowCount > 0 Then

                GridView1.BestFitColumns(True)

                GridView1.Columns("No_Documento").Group()

                GridView1.Columns("Solicitado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Solicitado").DisplayFormat.FormatString = "{0:N6}"
                GridView1.Columns("Solicitado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Solicitado").SummaryItem.DisplayFormat = "{0:N6}"

                GridView1.Columns("Recibido").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Recibido").DisplayFormat.FormatString = "{0:N6}"
                GridView1.Columns("Recibido").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Recibido").SummaryItem.DisplayFormat = "{0:N6}"

                GridView1.Columns("Diferencia").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Diferencia").DisplayFormat.FormatString = "{0:N6}"
                GridView1.Columns("Diferencia").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Diferencia").SummaryItem.DisplayFormat = "{0:N6}"

                IsLoading = False

                Set_LayOut_Grid()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Llena_Grid()

        Try

            DT.Rows.Clear()

            If ListaMovimientos IsNot Nothing AndAlso ListaMovimientos.Count > 0 Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Obteniendo datos...")
                SplashScreenManager.Default.SetWaitFormCaption("Por favor espere.")

                prg.Properties.Maximum = ListaMovimientos.Count

                For Each Obj In ListaMovimientos

                    DT.Rows.Add(Obj.ORDENCOMPRA,
                                Obj.CODIGO_PRODUCTO,
                                Obj.NOMBRE_PRODUCTO,
                                Obj.CANTIDAD,
                                Obj.CANTIDAD_RECIBIDA,
                                Obj.DIFERENCIA,
                                Obj.PRESENTACION,
                                Obj.BODEGA,
                                Obj.PROPIETARIO,
                                Obj.POLIZA,
                                Obj.IDTIPOINGRESOOC,
                                Obj.NOMBRE_INGRESOOC,
                                Obj.IDPRODUCTOBODEGA,
                                Obj.UMBAS,
                                Obj.ESTADO,
                                Obj.FECHA_CREACION)

                    Application.DoEvents()

                    prg.PerformStep()

                Next

                GridView1.OptionsView.ShowFooter = True
                GridView1.BestFitColumns(True)

                GridView1.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad_Recibida").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Recibida").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Cantidad_Recibida").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Recibida").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Diferencia").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Diferencia").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Diferencia").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Diferencia").SummaryItem.DisplayFormat = "{0:n6}"

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Function Get_Lista_Movimientos() As Boolean


        Get_Lista_Movimientos = False
        Dim IdProductoBodega As Integer = 0

        Try


            Dim fila As Object = cmbPropietario.GetSelectedDataRow
            Dim IdPropietario As Integer = fila.Item("IdPropietario")

            If txtIdProducto.Text.Trim <> "" AndAlso Not ProductoEspecifico Is Nothing Then
                IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(ProductoEspecifico.IdProducto, cmbBodega.EditValue)
                ListaMovimientos = clsLnVW_Doc_con_Diferencias.Get_All_Movimientos_By_IdProducto(dtpFechaDesde.Value,
                                                                                                 dtpfechaHasta.Value,
                                                                                                 IdProductoBodega,
                                                                                                 cmbBodega.EditValue,
                                                                                                 IdPropietario)
            Else
                ListaMovimientos = clsLnVW_Doc_con_Diferencias.Get_All_Movimientos_By_IdPropietario_And_Bodega(dtpFechaDesde.Value,
                                                                                                               dtpfechaHasta.Value,
                                                                                                               cmbBodega.EditValue,
                                                                                                               IdPropietario)
            End If

            If Not ListaMovimientos Is Nothing Then
                Get_Lista_Movimientos = ListaMovimientos.Count > 0
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub fchDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDesde.ValueChanged

        Try

            If Me.dtpFechaDesde.Value > Me.dtpfechaHasta.Value Or Me.dtpfechaHasta.Value < Me.dtpFechaDesde.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                Generar_Reporte()
                GridView1.Focus()
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

    Private Sub fchAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpfechaHasta.ValueChanged

        Try

            If Me.dtpFechaDesde.Value > Me.dtpfechaHasta.Value Or Me.dtpfechaHasta.Value < Me.dtpFechaDesde.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                Generar_Reporte()
                GridView1.Focus()
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

    Private Sub lblProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblProducto.LinkClicked

        Try

            Dim Rec As New frmProductoList() With {
                   .Modo = frmProductoList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            dgrid.DataSource = Nothing

            If Rec.pObjProducto IsNot Nothing AndAlso Rec.pObjProducto.IdProducto <> 0 Then
                txtIdProducto.Text = Rec.pObjProducto.Codigo
                txtNombreProducto.Text = Rec.pObjProducto.Nombre
                Generar_Reporte()
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

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdProducto_Validated(sender As Object, e As EventArgs) Handles txtIdProducto.Validated

        Try

            If String.IsNullOrEmpty(txtIdProducto.Text.Trim()) = False AndAlso txtIdProducto.Text > "0" Then

                ProductoEspecifico = clsLnProducto.Get_Single_By_Codigo(txtIdProducto.Text)

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then
                    txtNombreProducto.Text = Trim(String.Format("{0}", ProductoEspecifico.Nombre))
                    Generar_Reporte()
                Else
                    XtraMessageBox.Show(String.Format("No existe producto con código {0}", txtIdProducto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdProducto.Focus()
                    txtIdProducto.SelectAll()
                    ProductoEspecifico = Nothing
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdProducto_TextChanged(sender As Object, e As EventArgs) Handles txtIdProducto.TextChanged
        txtNombreProducto.Text = ""
        ProductoEspecifico = Nothing
    End Sub

    Private Sub frmStockEnUnaFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.Control AndAlso e.KeyCode = Keys.D Then
            ModoDepuracion = True
            MsgBox("Modo depuración activado, tenga cuidado...", MsgBoxStyle.Information, Text)
        End If

    End Sub


    Private Sub frmDocOCconDiferencias_Load(sender As Object, e As EventArgs) Handles Me.Load

        AP.Listar_Bodegas_By_Usuario(cmbBodega)
        cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)
        IsLoading = True

        Try

            SetDatataTable()

            'dgrid.DataSource = DT

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            IsLoading = False
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Generar_Reporte()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        Try
            Imprimir_Vista()
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    'Private Sub Imprimir_Vista()

    '    Try

    '        Dim printingSystem1 As New PrintingSystem()
    '        Dim printLink As New PrintableComponentLink()

    '        AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

    '        Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
    '        Dim leftColumnHead As String = "Empresa: " & AP.Empresa.Nombre
    '        Dim leftColumnHead2 As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

    '        Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "
    '        Dim firmaPilotoLinea As String = "Firma Piloto: __________________________"

    '        Dim firmaOperadorLinea As String = "Firma Operador: __________________________"

    '        Dim phf As PageHeaderFooter =
    '        TryCast(printLink.PageHeaderFooter, PageHeaderFooter)

    '        phf.Header.Content.Clear()

    '        phf.Footer.Content.AddRange(New String() {leftColumnFoot, "", ""})
    '        phf.Footer.Content.AddRange(New String() {firmaOperadorLinea, "", firmaPilotoLinea})

    '        phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
    '        phf.Header.Content.AddRange(New String() {leftColumnHead2})
    '        phf.Header.LineAlignment = BrickAlignment.Far

    '        phf.Footer.Content.AddRange(New String() _
    '        {leftColumnFoot})
    '        phf.Footer.LineAlignment = BrickAlignment.Near

    '        phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
    '        phf.Header.Content.AddRange(New String() {leftColumnHead2})
    '        phf.Header.LineAlignment = BrickAlignment.Far

    '        printingSystem1.PageSettings.Landscape = True
    '        printLink.Component = dgrid
    '        printLink.Landscape = True
    '        printLink.CreateDocument(printingSystem1)
    '        printingSystem1.PreviewFormEx.ShowDialog()
    '        printingSystem1.Dispose()

    '    Catch ex As Exception

    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '        Text,
    '        MessageBoxButtons.OK,
    '        MessageBoxIcon.Error)

    '        Dim vMsgError As String = ex.Message
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)

    '    End Try

    'End Sub

    Private Sub Imprimir_Vista()

        Try

            GridView1.BestFitColumns()

            Dim printingSystem1 As New PrintingSystem()
            Dim printLink As New PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            'Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres & " " & AP.UsuarioAp.Apellidos
            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As PageHeaderFooter = TryCast(printLink.PageHeaderFooter, PageHeaderFooter)

            phf.Header.Content.Clear()
            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = BrickAlignment.Far

            phf.Footer.Content.Clear()
            'phf.Footer.Content.AddRange(New String() {leftColumnFoot})
            phf.Footer.LineAlignment = BrickAlignment.Near

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = dgrid
            printLink.Landscape = True
            printingSystem1.PageSettings.AssignDefaultPrinterSettings()
            printingSystem1.PageSettings.TopMargin = 120 ' Ajustado para asegurar espacio para el encabezado

            ' Agregar las firmas al pie de página
            AddHandler printLink.CreateMarginalFooterArea, AddressOf printLink_CreateMarginalFooterArea

            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub printLink_CreateMarginalFooterArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)

        Try

            ' Obtener el ancho de la página para calcular la posición
            Dim pageWidth As Single = e.Graph.ClientPageSize.Width

            ' Definir los textos para las firmas y la página
            Dim firmaOperador As String = "Firma Operador: __________________________"
            Dim firmaPiloto As String = "Firma Piloto: __________________________"

            ' Crear un TextBrick para la firma del operador directamente debajo del número de página
            Dim operadorBrick As New TextBrick() With {
            .Rect = New RectangleF(0, 40, pageWidth / 2, 40),
            .Text = firmaOperador,
            .StringFormat = New BrickStringFormat(StringAlignment.Near),
            .BorderWidth = 0
        }

            ' Crear un TextBrick para la firma del piloto en la esquina inferior derecha
            Dim pilotoBrick As New TextBrick() With {
            .Rect = New RectangleF(pageWidth / 2, 40, pageWidth / 2, 40),
            .Text = firmaPiloto,
            .StringFormat = New BrickStringFormat(StringAlignment.Far),
            .BorderWidth = 0
        }

            ' Dibujar los ladrillos en el área del pie de página            
            e.Graph.DrawBrick(operadorBrick)
            e.Graph.DrawBrick(pilotoBrick)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As CreateAreaEventArgs)

        Try

            Dim reportHeader As String = vbNewLine & "TOMWMS" &
                                 vbNewLine & "Mercancías pendientes de recepción" &
                                 vbNewLine & "EMPRESA: " & AP.Empresa.Nombre

            e.Graph.StringFormat = New BrickStringFormat(StringAlignment.Center)
            e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

            Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 100) ' Aumentado a 100 para dar más espacio
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None)

        Catch ex As Exception

        End Try


    End Sub


    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick

        Try

            'dgrid.ExportToXlsx()

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "WMS_Doc_Con_Diferencias_" & FormatoFechas.tFecha(dtpFechaDesde.Value) & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    dgrid.ExportToXlsx(myStream)
                    myStream.Close()
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

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Close()
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

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
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

End Class