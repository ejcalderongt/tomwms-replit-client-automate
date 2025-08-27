Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmDocPeConDiferencias

    Private DT As New DataTable("DocPeConDiferencias")
    Public pBeListaProductos As New List(Of Integer)
    Public ListaMovimientos As New List(Of clsBeVW_Pe_Con_Diferencias)
    Public RepMovEnUnaFecha As New List(Of clsBeStockEnUnaFecha)
    Public Property Modo As pModo
    Public Property ProductoEspecifico As New clsBeProducto
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum


    Private Sub SetDatataTable()

        DT.Columns.Add("Orden_Pedido", GetType(String))
        DT.Columns.Add("Codigo_Producto", GetType(String))
        DT.Columns.Add("Nombre_Producto", GetType(String))
        DT.Columns.Add("Cantidad", GetType(Double))
        DT.Columns.Add("Cantidad_Despachada", GetType(Double))
        DT.Columns.Add("Diferencia", GetType(Double))
        DT.Columns.Add("Presentacion", GetType(String))
        DT.Columns.Add("Id_Propietario_Bodega", GetType(String))
        DT.Columns.Add("Bodega", GetType(String))
        DT.Columns.Add("Propietario", GetType(String))
        'DT.Columns.Add("Id_Proveedor_Bodega", GetType(Integer))
        DT.Columns.Add("Id_Tipo_Pedido", GetType(Integer))
        DT.Columns.Add("Nombre_Pedido", GetType(String))
        DT.Columns.Add("Id_Producto_Bodega", GetType(Integer))
        'DT.Columns.Add("Id_Presentación", GetType(Integer))
        'DT.Columns.Add("Id_Unidad_Medida_Basica", GetType(Integer))
        DT.Columns.Add("UMBas", GetType(String))
        DT.Columns.Add("Estado", GetType(String))
        'DT.Columns.Add("Activo", GetType(Integer))
        DT.Columns.Add("Fecha_Pedido", GetType(Date))

    End Sub


    Private Sub frmDocPeconDiferencias_Load(sender As Object, e As EventArgs) Handles Me.Load

        AP.Listar_Bodegas_By_Usuario(cmbBodega)
        cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

        Try

            SetDatataTable()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Generar_Reporte()
    End Sub

    Private Sub Generar_Reporte()


        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Generando...")

            Dim BeStockEnFecha As New clsBeStockEnUnaFecha
            Dim Idx As Integer = -1
            Dim Idx1 As Integer = -1

            ListaMovimientos.Clear()
            RepMovEnUnaFecha.Clear()
            lblPrg.Visible = True

            If Get_Lista_Movimientos() Then

                RepMovEnUnaFecha.Clear()

                prg.Visible = True
                prg.Properties.Step = 1
                prg.Properties.PercentView = True
                prg.Properties.Maximum = ListaMovimientos.Count
                prg.Properties.Minimum = 0

                If Not ListaMovimientos Is Nothing Then

                    Dim TheGoalDate As Date = New Date(2019, 8, 30)

                    For Each ObjM In ListaMovimientos.OrderBy(Function(x) x.ORDENPEDIDO)

                        lblPrg.Text = "Procesando movimiento para producto: " & ObjM.Codigo_producto
                        lblPrg.Refresh()

                        If ObjM.Fecha_Pedido = TheGoalDate Then
                            Debug.Print("Wait a second!")
                        End If

                        prg.PerformStep()

                        Application.DoEvents()

                    Next

                End If

                Llena_Grid()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
            prg.Visible = False
        End Try

    End Sub

    Private Function Get_Lista_Movimientos() As Boolean

        Dim clsTransaccion As New clsTransaccion
        Get_Lista_Movimientos = False
        Dim IdProductoBodega As Integer = 0

        Try

            clsTransaccion.Begin_Transaction()

            dgrid.DataSource = Nothing
            DT.Rows.Clear()

            'GT 08022021 se obtiene id propietario del combo en lugar de idpropietariobodega
            Dim fila As Object = cmbPropietario.GetSelectedDataRow
            Dim IdPropietario As Integer = fila.Item("IdPropietario")

            If txtIdProducto.Text.Trim <> "" AndAlso Not ProductoEspecifico Is Nothing Then

                IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(ProductoEspecifico.IdProducto, cmbBodega.EditValue)

                ListaMovimientos = clsLnVW_Pe_Con_Diferencias.Get_All_Movimientos_By_IdProducto(dtpFechaDesde.Value, dtpfechaHasta.Value, IdProductoBodega, cmbBodega.EditValue, IdPropietario)

            Else

                ListaMovimientos = clsLnVW_Pe_Con_Diferencias.Get_All_Movimientos_By_IdPropietario_And_Bodega(dtpFechaDesde.Value, dtpfechaHasta.Value, cmbBodega.EditValue, IdPropietario)

            End If

            If Not ListaMovimientos Is Nothing Then
                Get_Lista_Movimientos = ListaMovimientos.Count > 0
            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Private Sub Llena_Grid()

        dgrid.DataSource = Nothing
        DT.Rows.Clear()

        If ListaMovimientos IsNot Nothing AndAlso ListaMovimientos.Count > 0 Then

            For Each BeclsBeVW_Pe_Con_Diferencias In ListaMovimientos

                DT.Rows.Add(BeclsBeVW_Pe_Con_Diferencias.ORDENPEDIDO,
                            BeclsBeVW_Pe_Con_Diferencias.Codigo_producto,
                            BeclsBeVW_Pe_Con_Diferencias.Nombre_producto,
                            BeclsBeVW_Pe_Con_Diferencias.Cantidad,
                            BeclsBeVW_Pe_Con_Diferencias.Cant_despachada,
                            BeclsBeVW_Pe_Con_Diferencias.DIFERENCIA,
                            BeclsBeVW_Pe_Con_Diferencias.PRESENTACION,
                            BeclsBeVW_Pe_Con_Diferencias.IdPropietarioBodega,
                            BeclsBeVW_Pe_Con_Diferencias.BODEGA,
                            BeclsBeVW_Pe_Con_Diferencias.PROPIETARIO,
                            BeclsBeVW_Pe_Con_Diferencias.IdTipoPedido,
                            BeclsBeVW_Pe_Con_Diferencias.NOMBRE_PEDIDO,
                            BeclsBeVW_Pe_Con_Diferencias.IdProductoBodega,
                            BeclsBeVW_Pe_Con_Diferencias.UMBas,
                            BeclsBeVW_Pe_Con_Diferencias.Estado,
                            BeclsBeVW_Pe_Con_Diferencias.Fecha_Pedido)


                Application.DoEvents()

                prg.PerformStep()

            Next

            dgrid.DataSource = DT

            GridView1.OptionsView.ShowFooter = True
            GridView1.BestFitColumns(True)

            GridView1.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridView1.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"
            GridView1.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            GridView1.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

            GridView1.Columns("Cantidad_Despachada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridView1.Columns("Cantidad_Despachada").DisplayFormat.FormatString = "{0:n6}"
            GridView1.Columns("Cantidad_Despachada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            GridView1.Columns("Cantidad_Despachada").SummaryItem.DisplayFormat = "{0:n6}"

            GridView1.Columns("Diferencia").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridView1.Columns("Diferencia").DisplayFormat.FormatString = "{0:n6}"
            GridView1.Columns("Diferencia").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            GridView1.Columns("Diferencia").SummaryItem.DisplayFormat = "{0:n6}"


        End If

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
            printLink.Component = dgrid
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

        Dim reportHeader As String = vbNewLine & "TOM, WMS" &
                              vbNewLine & "Doc Pe con diferencias " &
                              vbNewLine & "BODEGA: " & AP.NomBodega

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Close()
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick

        Try

            'dgrid.ExportToXlsx()

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "WMS_Doc_Pe_Con_Diferencias_" & FormatoFechas.tFecha(dtpFechaDesde.Value) & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

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


    Private Sub fchDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDesde.ValueChanged

        Try

            If Me.dtpFechaDesde.Value > Me.dtpfechaHasta.Value Or Me.dtpfechaHasta.Value < Me.dtpFechaDesde.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                'Generar_Reporte()
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
                'Generar_Reporte()
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
            End If

            'Generar_Reporte()

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

End Class