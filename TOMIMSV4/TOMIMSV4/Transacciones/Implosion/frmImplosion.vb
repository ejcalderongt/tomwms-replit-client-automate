Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen

Public Class frmImplosion

    Public ListRegistros As New List(Of clsBeVW_Stock_Res_Pedido)
    Public Property SeleccionMultiple As Boolean = False
    Public listaStockSeleccionado As New List(Of clsBeVW_stock_res)
    Public pObjStock As clsBeVW_stock_res
    'Private DT As New DataTable("StockReservado")
    Private IsLoading As Boolean = False
    Private pTrans_ubic_hh_enc As New clsBeTrans_ubic_hh_enc
    Public pListUbicHHDet As New List(Of clsBeTrans_ubic_hh_det)
    Private pIdPropietarioBodega As Integer
    Private pListMovimiento As New List(Of clsBeTrans_movimientos)
    Private pListStockMov As New List(Of clsBeStock)
    Private pUbicHHDet As New clsBeTrans_ubic_hh_det
    Private pListjOperador As New List(Of clsBeTrans_ubic_hh_op)

    Private ReadOnly pListTransUbicTarimaDisponibles As New List(Of clsBeTrans_ubic_tarima)
    Private ReadOnly pListTransUbicTarimaUsadas As New List(Of clsBeTrans_ubic_tarima)

    Dim IdPropietario_ As Integer = 0
    Dim IdOperadorBodega As Integer = 0
    Dim IdOperador As Integer
    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Cargar_Datos()

        IsLoading = True

        Try

            Dim DT As New DataTable
            DT.Clear()

            grdStockRes.DataSource = Nothing

            Dim vIdBodega = cmbBodega.EditValue
            Dim vIdPropietarioBodega = cmbPropietarioBodega.EditValue

            DT = clsLnStock.Get_Reporte_Stock_By_IdBodega_and_IdPropietario_For_Implosion(vIdBodega, vIdPropietarioBodega)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    grdStockRes.DataSource = DT

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.OptionsView.ShowFooter = True

                    GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("CantidadUMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("CantidadPresentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Disponible_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Disponible_Presentación").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Disponible_Presentación").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatString = "{0:n6}"

                    Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Reservada_UMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Cantidad_Reservada")}
                    GridView1.GroupSummary.Add(item)

                    Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_UMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Cantidad_UMBas")}
                    GridView1.GroupSummary.Add(item1)

                    Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Presentacion", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Cantidad_Presentacion")}
                    GridView1.GroupSummary.Add(item2)

                    Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_UMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Disponible_UMBas")}
                    GridView1.GroupSummary.Add(item3)


                    Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_Presentación", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Disponible_Presentación")}
                    GridView1.GroupSummary.Add(item4)


                    GridView1.Columns("IdPresentacion").Visible = False

                    GridView1.ExpandAllGroups()

                End If

            End If

            Restore_LayOut_Grid()

            GridView1.BestFitColumns()

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

    Private Sub Cargar_CambiosEstado()

        Try

            Dgrid.DataSource = Nothing

            Dim lista As New List(Of clsBeTrans_ubic_hh_enc)

            lista = clsLnTrans_ubic_hh_enc.Get_All_Filtro(1, dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date, 3).ToList()

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("TransaccionUbicacionHhEnc")
                DT.Columns.Add(("Código"), GetType(Integer))
                DT.Columns.Add(("UbicacionConHh"), GetType(Boolean))
                DT.Columns.Add(("OperadorPorLinea"), GetType(Boolean))
                DT.Columns.Add(("MotivoUbicacion"), GetType(String))
                DT.Columns.Add(("Observacion"), GetType(String))
                DT.Columns.Add(("FechaInicio"), GetType(Date))
                DT.Columns.Add(("HoraInicio"), GetType(TimeSpan))
                DT.Columns.Add(("FechaFin"), GetType(Date))
                DT.Columns.Add(("HoraFin"), GetType(TimeSpan))
                DT.Columns.Add(("Estado"), GetType(String))
                DT.Columns.Add(("Nombre_Operador"), GetType(String))
                DT.Columns.Add(("Usuario"), GetType(String))
                DT.Columns.Add(("Rol"), GetType(String))

                For Each Obj As clsBeTrans_ubic_hh_enc In lista
                    DT.Rows.Add(Obj.IdTareaUbicacionEnc,
                                Obj.Ubicacion_con_hh,
                                Obj.Operador_por_linea,
                                Obj.DescripcionMotivo,
                                Obj.Observacion,
                                Obj.FechaInicio,
                                Obj.HoraInicio.TimeOfDay,
                                Obj.FechaFin,
                                Obj.HoraFin.TimeOfDay,
                                Obj.Estado,
                                Obj.Nombre_Operador,
                                Obj.Usuario,
                                Obj.Rol)
                Next

                Dgrid.DataSource = DT
                'lblRegs.Caption = String.Format("Registros: {0}", GridView2.RowCount)

                GridView2.BestFitColumns()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmListaStock_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Text = "Implosión"
            GroupBox1.Text = "Lista de Implosiones"

            vNombreArchivoLayOutGrid = "frmImplosion.xml"
            mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            'mnuTomarSeleccionados.Enabled = False

            If Not AP.Listar_Bodegas_By_Usuario(cmbBodega) Then
                XtraMessageBox.Show("No hay bodegas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            '#CKFK20181001: Colocar bodega por defecto.
            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
            cmbBodega.RefreshEditValue()

            If Not IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue) Then
                XtraMessageBox.Show("No hay propietarios definidos para la bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not IMS.Listar_Operadores(cmbOperador) Then
                XtraMessageBox.Show("No hay operadores definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Cargar_Datos()

            Cargar_CambiosEstado()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs)

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
    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos()
        Cargar_CambiosEstado()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportarExcel.ItemClick
        Exportar_Grid_A_Excel(grdStockRes, "TOMWMS_Lista_Stock_Control_Calidad.xlsx")
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

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick
        Imprimir_Vista()
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
            printLink.Component = grdStockRes
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

        Dim reportHeader As String = vbNewLine & "Detalle de stock reservado"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub chkSeleccionMultiple_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkSeleccionMultiple.CheckedChanged


        Try

            If chkSeleccionMultiple.Checked Then

                If XtraMessageBox.Show("Si habilita la selección múltiple se tomarán las cantidades completas de cada línea. ¿Continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    GridView1.OptionsSelection.MultiSelect = True
                    GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

                Else
                    chkSeleccionMultiple.Checked = False
                End If

            Else
                SeleccionMultiple = False
                chkSeleccionMultiple.Checked = False
                GridView1.OptionsSelection.MultiSelect = False
                GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
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

    Private Sub Procesar_Registros()


        Try

            pListStockMov.Clear()

            If Llenar_Lista() Then

                IdOperador = cmbOperador.EditValue
                IdOperadorBodega = clsLnOperador_bodega.Get_IdOperadorBodega_By_IdOperador(IdOperador, cmbBodega.EditValue)

                If listaStockSeleccionado.Count > 0 Then
                    IdPropietario_ = listaStockSeleccionado(0).IdPropietario
                    pIdPropietarioBodega = cmbPropietarioBodega.EditValue
                Else
                    Throw New Exception("Error_20231227_01: No se puede cargar Estados, sobre una lista vacia.")
                End If

                If IdPropietario_ = 0 Then
                    Throw New Exception("Error_20231227_02: El propietario no es valido.")
                End If

                Dim diferentesEstados As Boolean = listaStockSeleccionado.Select(Function(x) x.IdProductoEstado).Distinct().Count() > 1
                If diferentesEstados Then
                    Throw New Exception("Error_20231227_03: No se pueden agrupar estados para su procesamiento")
                End If

                ' Nueva lista resultado
                Dim nuevaLista As New List(Of clsBeVW_stock_res)

                ' Agrupar por producto
                Dim grupos = listaStockSeleccionado.GroupBy(Function(s) s.IdProducto)

#Region "Reproceso"

                Dim pListStockMov As New List(Of clsBeStock)
                Dim listaFinal = ImplosionarStockSeleccionado(listaStockSeleccionado, AP.UsuarioAp.IdUsuario, pListStockMov)

                ActualizarTablaStockTrasImplosion(listaStockSeleccionado, pListStockMov)

#End Region

                For Each Stock As clsBeVW_stock_res In listaStockSeleccionado

                    Dim pObjStockMov As New clsBeStock() With {.IdStockOrigen = Stock.IdStock, .IdStock = Stock.IdStock}
                    clsLnStock.GetSingle(pObjStockMov)
                    pObjStockMov.IdUbicacion_anterior = Stock.IdUbicacion
                    pObjStockMov.IdProductoEstado = Stock.IdProductoEstado
                    pListStockMov.Add(pObjStockMov)

                    pUbicHHDet = New clsBeTrans_ubic_hh_det
                    pUbicHHDet.IdStock = Stock.IdStock
                    pUbicHHDet.Producto = New clsBeProducto
                    pUbicHHDet.Stock = New clsBeStock
                    pUbicHHDet.ProductoEstado = New clsBeProducto_estado
                    pUbicHHDet.ProductoPresentacion = New clsBeProducto_Presentacion
                    pUbicHHDet.UnidadMedida = New clsBeUnidad_medida
                    pUbicHHDet.Producto.Nombre = Stock.Nombre_Producto
                    pUbicHHDet.Producto.Codigo = Stock.Codigo_Producto
                    pUbicHHDet.Stock.Fecha_vence = Stock.Fecha_Vence
                    pUbicHHDet.Stock.Serial = Stock.Serial
                    pUbicHHDet.Stock.Añada = 0
                    pUbicHHDet.Stock.Lote = Stock.Lote
                    pUbicHHDet.Stock.Fecha_Ingreso = Stock.Fecha_ingreso
                    pUbicHHDet.ProductoPresentacion.IdPresentacion = Stock.IdPresentacion
                    pUbicHHDet.ProductoPresentacion.Nombre = Stock.Nombre_Presentacion
                    pUbicHHDet.IdEstadoOrigen = Stock.IdProductoEstado
                    pUbicHHDet.IdEstadoDestino = Stock.IdProductoEstado
                    pUbicHHDet.ProductoEstado.Nombre = pObjStockMov.ProductoEstado.Nombre
                    pUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
                    pUbicHHDet.UbicacionDestino.IdUbicacion = Stock.IdUbicacion
                    pUbicHHDet.IdUbicacionOrigen = Stock.IdUbicacion
                    pUbicHHDet.IdUbicacionDestino = Stock.IdUbicacion
                    Dim Obj As New clsBeTrans_ubic_hh_op() _
                        With {.IdOperadorBodega = IdOperadorBodega,
                              .User_agr = AP.UsuarioAp.IdUsuario,
                              .Fec_agr = Now,
                              .User_mod = AP.UsuarioAp.IdUsuario,
                              .Fec_mod = Now}
                    pUbicHHDet.Operador = New clsBeOperador
                    pUbicHHDet.IdOperadorBodega = Obj.IdOperadorBodega
                    pListjOperador.Add(Obj)

                    pUbicHHDet.HoraInicio = New DateTime(1900, 1, 1, 0, 0, 0)
                    pUbicHHDet.HoraFin = New DateTime(1900, 1, 1, 0, 0, 0)
                    pUbicHHDet.Realizado = False

                    If Stock.IdPresentacion <> 0 Then

                        Dim BePresentacion As New clsBeProducto_Presentacion
                        BePresentacion = clsLnProducto_presentacion.GetSingle(Stock.IdPresentacion)

                        If Not BePresentacion Is Nothing Then
                            If BePresentacion.Factor = 0 Then
                                Throw New Exception("ERR20220202_1458: El factor de la presentación es 0. esto crearía un movimiento no válido para el sistema, valide el factor de la presentación. Identificador de presentación: " & Stock.IdPresentacion)
                            Else
                                pUbicHHDet.Cantidad = Math.Round(Stock.CantidadUmBas / BePresentacion.Factor, 6)
                            End If
                        Else
                            Throw New Exception("ERR20220202_1458: No se encontró el objeto de presentación para el identificador: " & Stock.IdPresentacion)
                        End If

                    End If

                    pUbicHHDet.Activo = 1
                    pUbicHHDet.IdBodega = AP.IdBodega
                    pListUbicHHDet.Add(pUbicHHDet)

                    Dim mov As New clsBeTrans_movimientos()
                    mov.IdEmpresa = AP.IdEmpresa
                    mov.IdBodegaOrigen = AP.IdBodega
                    mov.IdTransaccion = pUbicHHDet.IdTareaUbicacionEnc
                    mov.IdPropietarioBodega = pObjStock.IdPropietarioBodega
                    mov.IdProductoBodega = pObjStock.IdProductoBodega
                    mov.IdUbicacionOrigen = Stock.IdUbicacion_Anterior
                    mov.IdUbicacionDestino = pUbicHHDet.IdUbicacionDestino
                    mov.IdPresentacion = pObjStock.IdPresentacion
                    mov.IdEstadoOrigen = Stock.IdProductoEstado
                    mov.IdEstadoDestino = pObjStockMov.ProductoEstado.Nombre
                    mov.IdUnidadMedida = pObjStock.IdUnidadMedida
                    mov.IdTipoTarea = clsDataContractDI.tTipoTarea.CEST
                    mov.IdBodegaDestino = AP.IdBodega
                    mov.IdRecepcion = pObjStock.IdRecepcionEnc
                    mov.IdRecepcionDet = pObjStock.IdRecepcionDet
                    mov.Cantidad = pObjStockMov.Cantidad
                    mov.Serie = Stock.Serial
                    mov.Peso = Stock.Peso
                    mov.Lote = Stock.Lote
                    mov.Fecha_vence = pObjStock.Fecha_Vence
                    mov.Fecha = pObjStock.Fecha_ingreso
                    mov.Barra_pallet = pObjStock.Lic_plate
                    mov.Hora_ini = Now
                    mov.Hora_fin = Now
                    mov.Fecha_agr = Now
                    mov.Usuario_agr = AP.IdRol
                    mov.Cantidad_hist = Stock.CantidadUmBas
                    mov.Cantidad = Stock.CantidadUmBas
                    mov.Peso_hist = Stock.Peso

                    '#CKFK20251117 Puse esto en comentario porque lo correcto es que se quede en unidad de medida básica
                    'If Stock.IdPresentacion <> 0 Then

                    '    Dim BePresentacion As New clsBeProducto_Presentacion
                    '    BePresentacion = clsLnProducto_presentacion.GetSingle(Stock.IdPresentacion)

                    '    If Not BePresentacion Is Nothing Then
                    '        If BePresentacion.Factor = 0 Then
                    '            Throw New Exception("ERR20220202_1458: El factor de la presentación es 0. esto crearía un movimiento no válido para el sistema, valide el factor de la presentación. Identificador de presentación: " & mov.IdPresentacion)
                    '        Else
                    '            mov.Cantidad = Math.Round(mov.Cantidad / BePresentacion.Factor, 6)
                    '            mov.Cantidad_hist = Math.Round(mov.Cantidad_hist / BePresentacion.Factor, 6)
                    '        End If
                    '    Else
                    '        Throw New Exception("ERR20220202_1458: No se encontró el objeto de presentación para el identificador: " & mov.IdPresentacion)
                    '    End If

                    'End If

                    pListMovimiento.Add(mov)

                Next

                mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuTomarSeleccionados.Enabled = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub mnuTomarSeleccionados_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTomarSeleccionados.ItemClick

        Try


            Procesar_Registros()

            If Datos_Correctos() Then

                If XtraMessageBox.Show("¿Guardar transacción?",
                                       Text,
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question) = DialogResult.Yes Then


                    If Guardar() Then

                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        mnuTomarSeleccionados.Enabled = False

                        '#EJC20250325:
                        Imprimir_Reporte_Control_Calidad_Cambio_Estado(pTrans_ubic_hh_enc.IdTareaUbicacionEnc)

                        Cargar_Datos() : Cargar_CambiosEstado()

                    End If

                End If

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

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If listaStockSeleccionado.Count = 0 Then
                XtraMessageBox.Show("Transaccion no tiene detalle.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
            End If

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            pTrans_ubic_hh_enc = New clsBeTrans_ubic_hh_enc
            pTrans_ubic_hh_enc.IdPropietarioBodega = pIdPropietarioBodega
            pTrans_ubic_hh_enc.IdMotivoUbicacion = 1 'PROCEDIMIENTO
            pTrans_ubic_hh_enc.FechaInicio = Now
            pTrans_ubic_hh_enc.HoraInicio = Now
            pTrans_ubic_hh_enc.FechaFin = Now
            pTrans_ubic_hh_enc.HoraFin = Now
            pTrans_ubic_hh_enc.Activo = 1
            pTrans_ubic_hh_enc.Observacion = "Proceso: Cambio Estado Masivo."
            pTrans_ubic_hh_enc.User_agr = AP.UsuarioAp.IdUsuario
            pTrans_ubic_hh_enc.Fec_agr = Now
            pTrans_ubic_hh_enc.User_mod = AP.UsuarioAp.IdUsuario
            pTrans_ubic_hh_enc.Fec_mod = Now
            pTrans_ubic_hh_enc.Operador_por_linea = 0 'por ser masivo.
            pTrans_ubic_hh_enc.Ubicacion_con_hh = 0 'False, es dirigida.
            pTrans_ubic_hh_enc.Cambio_estado = 1
            pTrans_ubic_hh_enc.Asunto = "Cambio de Estado Masivo"
            pTrans_ubic_hh_enc.IdPrioridad = 1
            pTrans_ubic_hh_enc.IdTipoTarea = 3 'cambio de estado
            pTrans_ubic_hh_enc.IdBodega = AP.IdBodega
            pTrans_ubic_hh_enc.Estado = "Finalizado"
            pTrans_ubic_hh_enc.IsNew = 1

            clsLnTrans_ubic_hh_enc.Guardar_Transaccion(pTrans_ubic_hh_enc,
                                                      pListUbicHHDet,
                                                      pListjOperador,
                                                      pListMovimiento,
                                                      0,
                                                      IdPropietario_,
                                                      pListStockMov,
                                                      pListTransUbicTarimaDisponibles,
                                                      pListTransUbicTarimaUsadas,
                                                      pTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                      AP.HostName
                                                      )

            Guardar = True


        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                           Text,
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error)
        End Try


    End Function

    Private vNombreArchivoLayOutGrid As String = ""

    Private Sub GridView1_Layout(sender As Object, e As EventArgs)
        Guardar_Layout()
    End Sub

    Private Sub Guardar_Layout()

        If IsLoading Then Exit Sub

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

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try


    End Sub

    Private ExisteLayOut As Boolean = False
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

    Private Sub Restore_LayOut_Grid()

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

    Private Sub mnuCambiarEstado_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)

        Try

            pListStockMov.Clear()

            '#GT27120223: llenar la lista con las filas seleccionadas del grid, para luego cargar Estados
            '#GT27122023: mostrar estados de un único propietario.
            If Llenar_Lista() Then

                IdOperador = cmbOperador.EditValue
                IdOperadorBodega = clsLnOperador_bodega.Get_IdOperadorBodega_By_IdOperador(IdOperador, cmbBodega.EditValue)

                If listaStockSeleccionado.Count > 0 Then
                    IdPropietario_ = listaStockSeleccionado(0).IdPropietario
                    pIdPropietarioBodega = cmbPropietarioBodega.EditValue
                Else
                    Throw New Exception("Error_20231227_01: No se puede cargar Estados, sobre una lista vacia.")
                End If

                If IdPropietario_ = 0 Then
                    Throw New Exception("Error_20231227_02: El propietario no es valido.")
                End If

                '#EJC20241022
                Dim diferentesEstados As Boolean = listaStockSeleccionado.Select(Function(x) x.IdProductoEstado).Distinct().Count() > 1
                If diferentesEstados Then
                    Throw New Exception("Error_20231227_03: No se pueden agrupar estados para su procesamiento")
                End If

                Dim CambioEstado As New frmProducto_EstadoList() With
                {
                .Modo = frmProducto_EstadoList.pModo.Seleccion_Masiva,
                .pIdPropietario = IdPropietario_,
                .EstadoOrigen = listaStockSeleccionado(0).IdProductoEstado
                }
                CambioEstado.ShowDialog()

                If CambioEstado.pObj IsNot Nothing AndAlso CambioEstado.pObj.IdEstado <> 0 Then

                    'Dim IdTareaUbicacionDet As Integer = 1

                    For Each Stock As clsBeVW_stock_res In listaStockSeleccionado

                        '#GT03022023: Llenamos el objeto Stock
                        Dim pObjStockMov As New clsBeStock() With {.IdStockOrigen = Stock.IdStock, .IdStock = Stock.IdStock}
                        clsLnStock.GetSingle(pObjStockMov)
                        pObjStockMov.IdUbicacion_anterior = Stock.IdUbicacion
                        pObjStockMov.IdProductoEstado = Stock.IdProductoEstado
                        pListStockMov.Add(pObjStockMov)

                        '#GT02012023: creamos la tarea detalle de la HH
                        pUbicHHDet = New clsBeTrans_ubic_hh_det
                        'pUbicHHDet.IdTareaUbicacionDet = IdTareaUbicacionDet
                        pUbicHHDet.IdStock = Stock.IdStock
                        pUbicHHDet.Producto = New clsBeProducto
                        pUbicHHDet.Stock = New clsBeStock
                        pUbicHHDet.ProductoEstado = New clsBeProducto_estado
                        pUbicHHDet.ProductoPresentacion = New clsBeProducto_Presentacion
                        pUbicHHDet.UnidadMedida = New clsBeUnidad_medida
                        'pUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
                        pUbicHHDet.Producto.Nombre = Stock.Nombre_Producto
                        pUbicHHDet.Producto.Codigo = Stock.Codigo_Producto
                        'pUbicHHDet.Stock.IdUbicacion_anterior = Stock.IdUbicacion_Anterior
                        pUbicHHDet.Stock.Fecha_vence = Stock.Fecha_Vence
                        pUbicHHDet.Stock.Serial = Stock.Serial
                        'pUbicHHDet.ProductoEstado.Nombre = CambioEstado.pObj.Nombre
                        pUbicHHDet.Stock.Añada = 0
                        pUbicHHDet.Stock.Lote = Stock.Lote
                        pUbicHHDet.Stock.Fecha_Ingreso = Stock.Fecha_ingreso
                        pUbicHHDet.ProductoPresentacion.IdPresentacion = Stock.IdPresentacion
                        pUbicHHDet.ProductoPresentacion.Nombre = Stock.Nombre_Presentacion
                        '#GT02012023: es cambio de estado
                        pUbicHHDet.IdEstadoOrigen = Stock.IdProductoEstado
                        pUbicHHDet.IdEstadoDestino = CambioEstado.pObj.IdEstado
                        pUbicHHDet.ProductoEstado.Nombre = CambioEstado.pObj.Nombre
                        '#GT02012023: se requieren datos de la ubicación destino, aunque sea la misma
                        pUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
                        pUbicHHDet.UbicacionDestino.IdUbicacion = Stock.IdUbicacion
                        pUbicHHDet.IdUbicacionOrigen = Stock.IdUbicacion
                        pUbicHHDet.IdUbicacionDestino = Stock.IdUbicacion
                        '#EJC20220211: Agregar Operador por Línea. (Mercopan)
                        Dim Obj As New clsBeTrans_ubic_hh_op() _
                        With {.IdOperadorBodega = IdOperadorBodega,
                              .User_agr = AP.UsuarioAp.IdUsuario,
                              .Fec_agr = Now,
                              .User_mod = AP.UsuarioAp.IdUsuario,
                              .Fec_mod = Now}

                        '#GT02012023: asignación del operador
                        pUbicHHDet.Operador = New clsBeOperador
                        pUbicHHDet.IdOperadorBodega = Obj.IdOperadorBodega
                        'pUbicHHDet.Operador.Nombres = cmbOperadores.Text.Trim()
                        pListjOperador.Add(Obj)

                        pUbicHHDet.HoraInicio = New DateTime(1900, 1, 1, 0, 0, 0)
                        pUbicHHDet.HoraFin = New DateTime(1900, 1, 1, 0, 0, 0)
                        pUbicHHDet.Realizado = False
                        '#GT02012023: el obj stock si tiene campo cantidad, pero lo intenta asociar a cantidadPresentacion ??
                        pUbicHHDet.Cantidad = pObjStockMov.Cantidad
                        pUbicHHDet.Activo = 1
                        pUbicHHDet.IdBodega = AP.IdBodega
                        pListUbicHHDet.Add(pUbicHHDet)

                        '#GT02012023: llenamos el movimimiento
                        Dim mov As New clsBeTrans_movimientos()
                        mov.IdEmpresa = AP.IdEmpresa
                        mov.IdBodegaOrigen = AP.IdBodega
                        mov.IdTransaccion = pUbicHHDet.IdTareaUbicacionEnc
                        mov.IdPropietarioBodega = pObjStock.IdPropietarioBodega
                        mov.IdProductoBodega = pObjStock.IdProductoBodega
                        mov.IdUbicacionOrigen = Stock.IdUbicacion_Anterior
                        mov.IdUbicacionDestino = pUbicHHDet.IdUbicacionDestino
                        mov.IdPresentacion = pObjStock.IdPresentacion
                        mov.IdEstadoOrigen = Stock.IdProductoEstado 'estado original según stock
                        mov.IdEstadoDestino = CambioEstado.pObj.IdEstado 'nuevo estado según selección de modal
                        mov.IdUnidadMedida = pObjStock.IdUnidadMedida
                        mov.IdTipoTarea = clsDataContractDI.tTipoTarea.CEST 'Cambio de Estado
                        mov.IdBodegaDestino = AP.IdBodega
                        mov.IdRecepcion = pObjStock.IdRecepcionEnc
                        mov.IdRecepcionDet = pObjStock.IdRecepcionDet
                        mov.Cantidad = pObjStockMov.Cantidad
                        mov.Serie = Stock.Serial
                        mov.Peso = Stock.Peso
                        mov.Lote = Stock.Lote
                        mov.Fecha_vence = pObjStock.Fecha_Vence
                        mov.Fecha = pObjStock.Fecha_ingreso
                        mov.Barra_pallet = pObjStock.Lic_plate
                        mov.Hora_ini = Now
                        mov.Hora_fin = Now
                        mov.Fecha_agr = Now
                        mov.Usuario_agr = AP.IdRol
                        mov.Cantidad_hist = Stock.CantidadUmBas
                        mov.Peso_hist = Stock.Peso

                        If Stock.IdPresentacion <> 0 Then

                            Dim BePresentacion As New clsBeProducto_Presentacion
                            BePresentacion = clsLnProducto_presentacion.GetSingle(Stock.IdPresentacion)

                            If Not BePresentacion Is Nothing Then
                                If BePresentacion.Factor = 0 Then
                                    Throw New Exception("ERR20220202_1458: El factor de la presentación es 0. esto crearía un movimiento no válido para el sistema, valide el factor de la presentación. Identificador de presentación: " & mov.IdPresentacion)
                                Else
                                    mov.Cantidad = Math.Round(mov.Cantidad * BePresentacion.Factor, 6)
                                    mov.Cantidad_hist = Math.Round(mov.Cantidad_hist * BePresentacion.Factor, 6)
                                End If
                            Else
                                Throw New Exception("ERR20220202_1458: No se encontró el objeto de presentación para el identificador: " & mov.IdPresentacion)
                            End If

                        End If

                        pListMovimiento.Add(mov)

                    Next

                    mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    mnuTomarSeleccionados.Enabled = True

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Llenar_Lista() As Boolean

        Llenar_Lista = False
        Dim lBeStockRes As New List(Of clsBeStock_res)

        Try

            Dim selectedRowHandles As Integer() = GridView1.GetSelectedRows()

            If selectedRowHandles.Length = 0 Then
                XtraMessageBox.Show("Seleccione al menos un registro",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Else

                Dim IdStock As Integer = 0
                Dim pExistePropietario As New clsBeVW_stock_res


                listaStockSeleccionado = New List(Of clsBeVW_stock_res)

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Llenando lista...")

                For i As Integer = 0 To selectedRowHandles.Length - 1

                    IdStock = IIf(IsDBNull(GridView1.GetRowCellValue(selectedRowHandles(i), "IdStock")), 0, GridView1.GetRowCellValue(selectedRowHandles(i), "IdStock"))
                    pObjStock = New clsBeVW_stock_res()
                    pObjStock = clsLnStock.Get_Single_By_IdStock(IdStock)

                    If listaStockSeleccionado.Count > 0 Then
                        pExistePropietario = listaStockSeleccionado.Find(Function(x) x.IdPropietario = pObjStock.IdPropietario And x.IdPropietarioBodega = pObjStock.IdPropietarioBodega)

                        If pExistePropietario Is Nothing Then
                            Throw New Exception("Error_20231227_03: No se puede seleccionar producto de distinto propietario.")
                        Else
                            listaStockSeleccionado.Add(pObjStock)
                        End If

                    Else
                        listaStockSeleccionado.Add(pObjStock)
                    End If

                    SplashScreenManager.Default.SetWaitFormCaption("Stock procesado: " & IdStock)

                    Application.DoEvents()

                Next

                If listaStockSeleccionado.Count > 0 Then
                    Llenar_Lista = True
                End If

            End If


        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                             Text,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try
    End Function

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        Try
            Cargar_Datos()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbPropietarioBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietarioBodega.EditValueChanged
        Try
            Cargar_Datos()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView2.RowCount > 0) Then

                Dim Dr As DataRowView = GridView2.GetFocusedRow
                Dim gBeTransubicacionHHEnc As New clsBeTrans_ubic_hh_enc
                gBeTransubicacionHHEnc = clsLnTrans_ubic_hh_enc.GetSingle(Dr.Item("Código"))

                With frmCambioUbicacion
                    .tipoOperacion = 1
                    .Modo = frmCambioUbicacion.TipoTrans.Editar
                    .gBeTransubicacionHHEnc = gBeTransubicacionHHEnc
                    .MdiParent = MdiParent

                    If OpcionesMenu IsNot Nothing Then
                        .OpcionesMenu = OpcionesMenu
                        .mnuGuardar.Enabled = OpcionesMenu.Modificar
                        .mnuActualizar.Enabled = OpcionesMenu.Modificar
                        .mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    .Show()
                    .Focus()
                End With

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub mnuImprimirDocumento_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimirDocumento.ItemClick

        Try

            If (GridView2.RowCount > 0) Then

                Dim Dr As DataRowView = GridView2.GetFocusedRow

                If Dr Is Nothing Then Exit Sub

                Dim IdTareaUbicacionEnc As Integer = Integer.Parse(Dr.Item("Código"))

                Imprimir_Reporte_Control_Calidad_Cambio_Estado(IdTareaUbicacionEnc)

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

    Public Sub Imprimir_Reporte_Control_Calidad_Cambio_Estado(ByVal IdTareaUbicacionEnc As Integer)

        Try

            If (GridView2.RowCount > 0) Then

                Dim DT As New DataTable("Result")
                DT = clsLnTrans_ubic_hh_enc.Get_rpt_Control_Calidad_Cambio_Estado(IdTareaUbicacionEnc)

                Dim idBodega As Integer = (From row In DT.AsEnumerable()
                                           Select row.Field(Of Integer)("IdBodega")).FirstOrDefault()

                Dim vNombreBodega As String = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(idBodega)
                Dim IdEmpresa As Integer = clsLnBodega.Get_IdEmpresa_By_IdBodega(idBodega)
                Dim vNombreEmpresa As String = clsLnEmpresa.Get_Nombre_Empresa_By_IdEmpresa(IdEmpresa)

                Dim Rep As New rptControlCalidadCambioEstado
                Rep.DataSource = DT
                Rep.DataMember = "Result"
                Rep.Parameters("Empresa").Value = vNombreEmpresa
                Rep.Parameters("Empresa").Visible = False
                Rep.Parameters("Bodega").Value = vNombreBodega
                Rep.Parameters("Bodega").Visible = False
                Rep.RequestParameters = False

                If clsLnEmpresa.GetImagen(IdEmpresa) Is Nothing Then
                    Rep.XrLogo.Image = Nothing
                Else
                    Rep.XrLogo.Image = clsPublic.ByteArrayToImage(clsLnEmpresa.GetImagen(IdEmpresa))
                End If

                Rep.ShowPreview()

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

    Public Function ImplosionarStockSeleccionado(ByVal listaStockSeleccionado As List(Of clsBeVW_stock_res),
                                                 ByVal usuario As String,
                                                 ByRef pListStockMov As List(Of clsBeStock)) As List(Of clsBeStock)

        Dim nuevaLista As New List(Of clsBeStock)
        Dim grupos = listaStockSeleccionado.
                    GroupBy(Function(s) New With {
                        Key s.IdProducto,
                        Key s.Lote,
                        Key s.IdUbicacion
                    })

        For Each grupo In grupos
            Dim clave = grupo.Key

            ' Obtener presentación por defecto
            Dim presentacionDefecto As clsBeProducto_Presentacion = clsLnProducto_presentacion.Get_Presentacion_Defecto_By_IdProducto(clave.IdProducto)
            If presentacionDefecto Is Nothing OrElse presentacionDefecto.Factor <= 0 Then
                For Each stock In grupo
                    Dim pObjStockMov As New clsBeStock() With {
                        .IdStockOrigen = stock.IdStock,
                        .IdStock = stock.IdStock
                    }
                    clsLnStock.GetSingle(pObjStockMov)
                    nuevaLista.Add(pObjStockMov)
                    pListStockMov.Add(pObjStockMov)
                Next
                Continue For
            End If

            Dim factor As Double = presentacionDefecto.Factor
            Dim acumulado As Double = 0
            Dim idStocksAcumulados As New List(Of clsBeVW_stock_res)
            Dim stocksOrdenados = grupo.OrderByDescending(Function(s) s.CantidadUmBas).ToList()

            For Each stock In stocksOrdenados
                acumulado += stock.CantidadUmBas
                idStocksAcumulados.Add(stock)

                While acumulado >= factor
                    Dim stockBase = idStocksAcumulados.First()
                    Dim pObjStockMov As New clsBeStock() With {
                        .IdStockOrigen = stockBase.IdStock,
                        .IdStock = stockBase.IdStock
                    }
                    clsLnStock.GetSingle(pObjStockMov)

                    pObjStockMov.Cantidad = factor
                    pObjStockMov.IdPresentacion = presentacionDefecto.IdPresentacion
                    pObjStockMov.Fec_agr = Now
                    pObjStockMov.Fec_mod = Now
                    pObjStockMov.User_agr = usuario
                    pObjStockMov.User_mod = usuario
                    pObjStockMov.IdUbicacion_anterior = stockBase.IdUbicacion
                    pObjStockMov.IdProductoEstado = stockBase.IdProductoEstado

                    nuevaLista.Add(pObjStockMov)
                    pListStockMov.Add(pObjStockMov)

                    acumulado -= factor

                    ' Restar del conjunto de stocks acumulados
                    Dim cantidadRestante = factor
                    For i = 0 To idStocksAcumulados.Count - 1
                        Dim s = idStocksAcumulados(i)
                        If s.CantidadUmBas <= cantidadRestante Then
                            cantidadRestante -= s.CantidadUmBas
                            s.CantidadUmBas = 0
                        Else
                            s.CantidadUmBas -= cantidadRestante
                            cantidadRestante = 0
                        End If
                        If cantidadRestante <= 0 Then Exit For
                    Next
                    idStocksAcumulados = idStocksAcumulados.Where(Function(s) s.CantidadUmBas > 0).ToList()
                End While
            Next

            ' Actualizar el stock sobrante en su mismo IdStock
            If acumulado > 0 AndAlso idStocksAcumulados.Count > 0 Then
                Dim stockSobrante = idStocksAcumulados.First()
                Dim pObjStockSobrante As New clsBeStock() With {
                    .IdStockOrigen = stockSobrante.IdStock,
                    .IdStock = stockSobrante.IdStock
                }
                clsLnStock.GetSingle(pObjStockSobrante)

                pObjStockSobrante.Cantidad = stockSobrante.CantidadUmBas
                pObjStockSobrante.IdPresentacion = 0
                pObjStockSobrante.Fec_mod = Now
                pObjStockSobrante.User_mod = usuario
                pObjStockSobrante.IdUbicacion_anterior = stockSobrante.IdUbicacion
                pObjStockSobrante.IdProductoEstado = stockSobrante.IdProductoEstado

                pListStockMov.Add(pObjStockSobrante)
                nuevaLista.Add(pObjStockSobrante)
            End If
        Next

        Return nuevaLista
    End Function

    Public Sub ActualizarTablaStockTrasImplosion(ByVal listaOriginal As List(Of clsBeVW_stock_res),
                                                 ByVal listaMovimientos As List(Of clsBeStock))

        Dim idsOriginales = listaOriginal.Select(Function(x) x.IdStock).Distinct().ToList()
        Dim idsEnMovimientos = listaMovimientos.Select(Function(x) x.IdStock).Distinct().ToList()
        Dim idsParaEliminar = idsOriginales.Except(idsEnMovimientos).ToList()

        ' Eliminar
        For Each id In idsParaEliminar
            Try
                clsLnStock.Eliminar_By_IdStock(id)
            Catch ex As Exception
                MsgBox($"Error al eliminar IdStock {id}: {ex.Message}", MsgBoxStyle.Critical)
            End Try
        Next

        ' Insertar o actualizar
        For Each stock In listaMovimientos
            Try
                If stock.Fec_agr <> Date.MinValue Then
                    ' Es un nuevo registro creado en esta sesión
                    clsLnStock.Insertar(stock)
                Else
                    ' Es una actualización del stock original (sobrante)
                    clsLnStock.Actualizar(stock)
                End If
            Catch ex As Exception
                MsgBox($"Error al insertar/actualizar stock del producto {stock.IdProductoBodega}, IdStock {stock.IdStock}: {ex.Message}", MsgBoxStyle.Critical)
            End Try
        Next
    End Sub

End Class