Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class FrmStock_Fiscal

    Private DT As New DataTable("DocStockFiscal")
    Public pBeListaProductos As New List(Of Integer)
    Public ListaMovimientos As New List(Of clsBeVW_Existencia_Valores_Fiscales)
    Public RepMovEnUnaFecha As New List(Of clsBeStockEnUnaFecha)
    Public Property ProductoEspecifico As New clsBeProducto
    Public Property Modo As pModo
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub SetDatataTable()

        DT.Columns.Add("IdRecepcionEnc", GetType(Integer))
        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Proveedor", GetType(String))
        DT.Columns.Add("Bodega", GetType(String))
        DT.Columns.Add("IdOrdenCompraEnc", GetType(Integer))
        DT.Columns.Add("No_DocumentoOC", GetType(String))
        DT.Columns.Add("No_DocumentoRec", GetType(String))
        DT.Columns.Add("ReferenciaOC", GetType(String))
        DT.Columns.Add("Fecha", GetType(Date))
        DT.Columns.Add("Estado", GetType(String))
        DT.Columns.Add("TipoTrans", GetType(String))
        DT.Columns.Add("Descripcion", GetType(String))
        DT.Columns.Add("Muelle", GetType(String))
        DT.Columns.Add("Activo", GetType(Boolean))
        DT.Columns.Add("Fecha_Agrego", GetType(Date))
        DT.Columns.Add("CodigoProd", GetType(String))
        DT.Columns.Add("BarraProd", GetType(String))
        DT.Columns.Add("NombreProd", GetType(String))
        DT.Columns.Add("Recibido", GetType(Double))
        DT.Columns.Add("Existencia_Actual_UMBas", GetType(Double))
        DT.Columns.Add("Existencia_Actual_Pres", GetType(Double))
        DT.Columns.Add("UM", GetType(String))
        DT.Columns.Add("EstadoProd", GetType(String))
        DT.Columns.Add("PresProd", GetType(String))
        DT.Columns.Add("Lic_plate", GetType(String))
        DT.Columns.Add("Factor", GetType(Double))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("Vence", GetType(Date))
        DT.Columns.Add("IdStock", GetType(Integer))
        DT.Columns.Add("Ubicacion_Origen", GetType(String))
        DT.Columns.Add("codigo_poliza", GetType(String))
        DT.Columns.Add("numero_orden", GetType(String))
        'GT 17082021 1605: La vw devuelve codigo y numero en sustitucion de nopoliza
        'DT.Columns.Add("NoPoliza", GetType(String))
        DT.Columns.Add("Valor_aduana", GetType(Double))
        DT.Columns.Add("Valor_fob", GetType(Double))
        DT.Columns.Add("Valor_iva", GetType(Double))
        DT.Columns.Add("Valor_dai", GetType(Double))
        DT.Columns.Add("Valor_seguro", GetType(Double))
        DT.Columns.Add("Valor_flete", GetType(Double))
        DT.Columns.Add("Peso_neto", GetType(Double))


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
            clsUiPrintHelper.PrintGridPreview(dgrid, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
                              vbNewLine & "Existencia valores fiscales " &
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
            saveFileDialog1.FileName = "VW_Existencia_Valores_Fiscales_" & FormatoFechas.tFecha(dtpFechaDesde.Value) & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

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

                    For Each ObjM In ListaMovimientos.OrderBy(Function(x) x.IdRecepcionEnc)

                        lblPrg.Text = "Procesando movimiento para producto: " & ObjM.CodigoProd
                        lblPrg.Refresh()

                        If ObjM.Fecha = TheGoalDate Then
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

                ListaMovimientos = clsLnVW_Existencia_Valores_Fiscales.Get_All_Movimientos_By_IdProducto(dtpFechaDesde.Value, dtpfechaHasta.Value, IdProductoBodega, cmbBodega.EditValue, IdPropietario)

            Else

                ListaMovimientos = clsLnVW_Existencia_Valores_Fiscales.Get_All_Movimientos_By_IdPropietario_And_Bodega(dtpFechaDesde.Value, dtpfechaHasta.Value, cmbBodega.EditValue, IdPropietario)

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

        Try

            dgrid.DataSource = Nothing

            DT.Rows.Clear()

            If ListaMovimientos IsNot Nothing AndAlso ListaMovimientos.Count > 0 Then

                For Each BeclsBeVW_Existencia_Valores_Fiscales In ListaMovimientos


                    DT.Rows.Add(BeclsBeVW_Existencia_Valores_Fiscales.IdRecepcionEnc,
                                BeclsBeVW_Existencia_Valores_Fiscales.Propietario,
                                BeclsBeVW_Existencia_Valores_Fiscales.Proveedor,
                                BeclsBeVW_Existencia_Valores_Fiscales.Bodega,
                                BeclsBeVW_Existencia_Valores_Fiscales.IdOrdenCompraEnc,
                                BeclsBeVW_Existencia_Valores_Fiscales.No_DocumentoOC,
                                BeclsBeVW_Existencia_Valores_Fiscales.No_DocumentoRec,
                                BeclsBeVW_Existencia_Valores_Fiscales.ReferenciaOC,
                                BeclsBeVW_Existencia_Valores_Fiscales.Fecha,
                                BeclsBeVW_Existencia_Valores_Fiscales.Estado,
                                BeclsBeVW_Existencia_Valores_Fiscales.TipoTrans,
                                BeclsBeVW_Existencia_Valores_Fiscales.Descripcion,
                                BeclsBeVW_Existencia_Valores_Fiscales.Muelle,
                                BeclsBeVW_Existencia_Valores_Fiscales.Activo,
                                BeclsBeVW_Existencia_Valores_Fiscales.Fecha_Agrego,
                                BeclsBeVW_Existencia_Valores_Fiscales.CodigoProd,
                                BeclsBeVW_Existencia_Valores_Fiscales.BarraProd,
                                BeclsBeVW_Existencia_Valores_Fiscales.NombreProd,
                                BeclsBeVW_Existencia_Valores_Fiscales.Recibido,
                                BeclsBeVW_Existencia_Valores_Fiscales.Existencia_Actual_UMBas,
                                BeclsBeVW_Existencia_Valores_Fiscales.Existencia_Actual_Pres,
                                BeclsBeVW_Existencia_Valores_Fiscales.UM,
                                BeclsBeVW_Existencia_Valores_Fiscales.EstadoProd,
                                BeclsBeVW_Existencia_Valores_Fiscales.PresProd,
                                BeclsBeVW_Existencia_Valores_Fiscales.Lic_plate,
                                BeclsBeVW_Existencia_Valores_Fiscales.Factor,
                                BeclsBeVW_Existencia_Valores_Fiscales.Lote,
                                BeclsBeVW_Existencia_Valores_Fiscales.Vence,
                                BeclsBeVW_Existencia_Valores_Fiscales.IdStock,
                                BeclsBeVW_Existencia_Valores_Fiscales.Ubicacion_Origen,
                                BeclsBeVW_Existencia_Valores_Fiscales.codigo_poliza,
                                BeclsBeVW_Existencia_Valores_Fiscales.numero_orden,
                                BeclsBeVW_Existencia_Valores_Fiscales.Valor_aduana,
                                BeclsBeVW_Existencia_Valores_Fiscales.Valor_fob,
                                BeclsBeVW_Existencia_Valores_Fiscales.Valor_iva,
                                BeclsBeVW_Existencia_Valores_Fiscales.Valor_dai,
                                BeclsBeVW_Existencia_Valores_Fiscales.Valor_seguro,
                                BeclsBeVW_Existencia_Valores_Fiscales.Valor_flete,
                                BeclsBeVW_Existencia_Valores_Fiscales.Peso_neto
                                )


                    Application.DoEvents()

                    prg.PerformStep()

                Next

                dgrid.DataSource = DT

                Restore_LayOut_Grid()

                GridView1.OptionsView.ShowFooter = True

                GridView1.BestFitColumns(True)

                GridView1.Columns("Recibido").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Recibido").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Recibido").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Recibido").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Existencia_Actual_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Existencia_Actual_UMBas").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Existencia_Actual_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Existencia_Actual_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Existencia_Actual_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Existencia_Actual_Pres").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Existencia_Actual_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Existencia_Actual_Pres").SummaryItem.DisplayFormat = "{0:n6}"


                GridView1.Columns("Valor_aduana").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Valor_aduana").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Valor_aduana").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Valor_aduana").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Valor_fob").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Valor_fob").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Valor_fob").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Valor_fob").SummaryItem.DisplayFormat = "{0:n6}"


                GridView1.Columns("Valor_iva").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Valor_iva").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Valor_iva").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Valor_iva").SummaryItem.DisplayFormat = "{0:n6}"


                GridView1.Columns("Valor_dai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Valor_dai").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Valor_dai").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Valor_dai").SummaryItem.DisplayFormat = "{0:n6}"


                GridView1.Columns("Valor_seguro").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Valor_seguro").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Valor_seguro").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Valor_seguro").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Valor_flete").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Valor_flete").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Valor_flete").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Valor_flete").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Peso_neto").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Peso_neto").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Peso_neto").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Peso_neto").SummaryItem.DisplayFormat = "{0:n6}"

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    '#EJC20210716FRMSTOCKFISCAL:Guardar LayoutGrid en STOCK_FISCAL
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


            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub FrmStock_Fiscal_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            '#EJC20210716:Restaurar LayoutGrid en stockParametro.
            'vNombreArchivoLayOutGrid = dgrid.Name & ".xml"
            vNombreArchivoLayOutGrid = "FrmStock_Fiscal.xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

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

    Private Sub Restore_LayOut_Grid()

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

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout
        Guardar_Layout()
    End Sub

End Class



