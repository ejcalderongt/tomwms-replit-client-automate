Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmProductosSinMovimientos

    Public pBeListaStock As New List(Of clsBeVW_stock_res)
    Public pBeListaProductos As New List(Of clsBeProducto)
    Public pBeListaMovimientos As New List(Of clsBeTrans_movimientos)
    Public pBeListaProductosNuevos As New List(Of clsBeProducto)
    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub Productos()

        Try

            Dim UltimoMovimiento As Integer

            For Each BeProducto As clsBeProducto In pBeListaProductos

                If BeProducto.Ciclo_vida > 0 Then

                    If clsLnProducto.Existe_Stock_By_IdProducto(BeProducto.IdProducto) Then

                        UltimoMovimiento = clsLnTrans_movimientos.Get_Dias_Despues_Ultimo_Despacho(BeProducto.IdProducto)

                        If (UltimoMovimiento) > BeProducto.Ciclo_vida Then

                            clsLnPropietarios.GetSingle(BeProducto.Propietario)
                            BeProducto.UnidadMedida = clsLnUnidad_medida.GetSingle(BeProducto.IdUnidadMedidaBasica)
                            BeProducto.Stock = clsLnVW_stock_res.Get_Single_By_IdProducto(BeProducto.IdProducto, AP.IdBodega)
                            BeProducto.Stock.UbicacionActual.IdUbicacion = BeProducto.Stock.IdUbicacion
                            clsLnBodega_ubicacion.Obtener(BeProducto.Stock.UbicacionActual)

                            BeProducto.UnidadMedida.Nombre = BeProducto.UnidadMedida.Nombre
                            BeProducto.Propietario.Nombre_comercial = BeProducto.Propietario.Nombre_comercial
                            BeProducto.FechaVence = BeProducto.Stock.Fecha_Vence
                            BeProducto.ExistenciaUMBas = BeProducto.Stock.CantidadUmBas
                            BeProducto.Cantidad = BeProducto.Stock.CantidadPresentacion
                            ' BeProducto.Estado = BeProducto.Stock.NomEstado
                            BeProducto.Presentacion.Nombre = BeProducto.Stock.Nombre_Presentacion
                            'BeProducto.Ubicacion = BeProducto.Stock.UbicacionActual.NombreCompleto
                            BeProducto.Lote = BeProducto.Stock.Lote
                            BeProducto.Noserie = BeProducto.Stock.No_Serie

                            pBeListaProductosNuevos.Add(BeProducto)

                        End If

                    End If

                End If

            Next

            LlenaGrid()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub LlenaGrid()

        Try

            If pBeListaProductosNuevos IsNot Nothing AndAlso pBeListaProductosNuevos.Count > 0 Then


                Dim DT As New DataTable("Productos")
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("UM", GetType(String))
                DT.Columns.Add("Presentación", GetType(String))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Barra", GetType(String))
                DT.Columns.Add("Nombre", GetType(String))
                DT.Columns.Add("Lote", GetType(String))
                DT.Columns.Add("Serie", GetType(String))
                DT.Columns.Add("Cant. Presentación", GetType(Double))
                DT.Columns.Add("Cant U.M Bas", GetType(Double))
                DT.Columns.Add("Ubicación", GetType(String))
                DT.Columns.Add("Estado", GetType(String))
                DT.Columns.Add("Vence", GetType(Date))

                For Each ObjP As clsBeProducto In pBeListaProductosNuevos

                    DT.Rows.Add(ObjP.Propietario.Nombre_comercial,
                                ObjP.UnidadMedida.Nombre,
                                ObjP.Presentacion.Nombre,
                                ObjP.Codigo,
                                ObjP.Codigo_barra,
                                ObjP.Nombre,
                                ObjP.Stock.Lote,
                                ObjP.Stock.No_Serie,
                                ObjP.Stock.CantidadPresentacion,
                                ObjP.Stock.CantidadUmBas,
                                ObjP.Stock.Ubicacion_Nombre,
                                ObjP.Stock.NomEstado,
                                ObjP.Stock.Fecha_Vence)

                Next

                grdSinMov.DataSource = DT


                GridView1.OptionsView.ColumnAutoWidth = False

                GridView1.BestFitColumns()

                If GridView1.RowCount > 0 Then

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.OptionsView.ShowFooter = True

                    GridView1.Columns("Cant U.M Bas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cant U.M Bas").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Cant U.M Bas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cant U.M Bas").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Cant. Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cant. Presentación").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Cant. Presentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cant. Presentación").SummaryItem.DisplayFormat = "{0:n6}"

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

    Private Function TieneMovimientos(ByVal pIdProducto As Integer) As List(Of clsBeTrans_movimientos)

        TieneMovimientos = Nothing

        Dim ReturnList As New List(Of clsBeTrans_movimientos)

        Try

            Dim ObjM As New clsBeTrans_movimientos

            pBeListaMovimientos = clsLnTrans_movimientos.TieneMovimiento(pIdProducto)

            For Each Obj As clsBeTrans_movimientos In pBeListaMovimientos

                If Obj.TipoTarea.ToUpper = "DESP" Then

                    ObjM.IdProducto = Obj.IdProducto
                    ObjM.Producto = Obj.Producto
                    ObjM.Codigo = Obj.Codigo
                    ObjM.CodigoBarra = Obj.CodigoBarra
                    ObjM.Fecha = Obj.Fecha

                    ReturnList.Add(ObjM)

                End If

            Next

            Return ReturnList

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdSinMov, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = grdSinMov
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

        Dim reportHeader As String = vbNewLine & "Productos Sin Movimiento"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub Cargar()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Generando...")

            pBeListaMovimientos.Clear()
            pBeListaProductos.Clear()
            pBeListaProductosNuevos.Clear()
            pBeListaStock.Clear()

            pBeListaProductos = clsLnProducto.GetAll_Tiene_Cliclo_Vida()
            'pBeListaStock = clsLnVW_stock_res.GetAll()

            Productos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            SplashScreenManager.CloseForm(False)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub frmProductosSinMovimientos_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            'Cargar()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

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
End Class



