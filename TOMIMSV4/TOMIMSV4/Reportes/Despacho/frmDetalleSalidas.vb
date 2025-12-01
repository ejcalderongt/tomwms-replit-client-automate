Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraSplashScreen

Public Class frmDetalleSalidas

    Public Property IsLoading As Boolean = True

    Public Sub New()
        InitializeComponent()
        IsLoading = False
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private DT As New DataTable("Despachos")
    Public listarDespacho As New List(Of clsBeVW_Despacho_Rep)

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub SetDatataTable()

        Try

            DT.Columns.Add("Propietario", GetType(String))
            DT.Columns.Add("Código", GetType(String))
            DT.Columns.Add("Nombre", GetType(String))
            DT.Columns.Add("UM", GetType(String))
            DT.Columns.Add("Presentación", GetType(String))
            DT.Columns.Add("Factor", GetType(Double))
            DT.Columns.Add("Estado", GetType(String))
            DT.Columns.Add("Lote", GetType(String))
            DT.Columns.Add("Licencia", GetType(String))
            DT.Columns.Add("Vence", GetType(Date))
            DT.Columns.Add("Fecha_Despacho", GetType(Date))
            DT.Columns.Add("Ubicación_Origen", GetType(String))
            DT.Columns.Add("Cant_Pick", GetType(Double))
            DT.Columns.Add("Cant_Veri", GetType(Double))
            DT.Columns.Add("Peso_Pick", GetType(Double))
            DT.Columns.Add("Peso_Veri", GetType(Double))
            DT.Columns.Add("Cant_Despachada", GetType(Double))
            DT.Columns.Add("Peso_Despachado", GetType(Double))
            DT.Columns.Add("Encontrado", GetType(Boolean))
            DT.Columns.Add("Acepto", GetType(Boolean))
            DT.Columns.Add("Documento_WMS", GetType(String))
            DT.Columns.Add("Referencia", GetType(String))
            DT.Columns.Add("Cliente", GetType(String))
            DT.Columns.Add("Bodega_Recepción", GetType(Boolean))
            DT.Columns.Add("Bodega_Traslado", GetType(Boolean))
            DT.Columns.Add("No_Pase", GetType(String))
            DT.Columns.Add("Observación", GetType(String))
            DT.Columns.Add("Número", GetType(Integer))
            DT.Columns.Add("Marchamo", GetType(String))
            DT.Columns.Add("Ruta", GetType(String))
            DT.Columns.Add("Placas Vehículo", GetType(String))
            DT.Columns.Add("Piloto", GetType(String))
            '#GT23112022_1400: campos DyD
            DT.Columns.Add("clasificacion", GetType(String))
            DT.Columns.Add("marca", GetType(String))
            DT.Columns.Add("familia", GetType(String))
            DT.Columns.Add("parametro_a", GetType(String))
            DT.Columns.Add("parametro_b", GetType(String))

            '#GT18082023: campos poliza ingreso
            DT.Columns.Add("numero_orden_ingreso", GetType(String))
            DT.Columns.Add("codigo_poliza_ingreso", GetType(String))

            '#GT16082023: referencias al pedido y despacho
            DT.Columns.Add("IdPedidoEnc", GetType(String))
            DT.Columns.Add("IdDespachoEnc", GetType(String))

            '#GT16082023: campos poliza salida
            DT.Columns.Add("numero_orden_pedido", GetType(String))
            DT.Columns.Add("codigo_poliza_pedido", GetType(String))
            DT.Columns.Add("codigo_regimen_salida", GetType(String))
            DT.Columns.Add("placa_contenedor_salida", GetType(String))
            DT.Columns.Add("dua_salida", GetType(String))

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Cargar()

        Dim pBodega As clsBeBodega

        Try

            If IsLoading Then Exit Sub
            IsLoading = True

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            grdDetalleSalidas.DataSource = Nothing
            DT.Clear()
            listarDespacho = New List(Of clsBeVW_Despacho_Rep)

            pBodega = AP.Bodega

            listarDespacho = clsLnVW_Despacho_Rep.Get_All_By_Rango_Fechas(dtpFechaDel.Value, dtpFechaAl.Value, pBodega)

            If listarDespacho.Count > 0 Then

                For Each Obj As clsBeVW_Despacho_Rep In listarDespacho

                    DT.Rows.Add(Obj.Propietario,
                                Obj.Codigo_Producto,
                                Obj.Nombre_Producto,
                                Obj.UM,
                                Obj.Presentacion,
                                Obj.Factor,
                                Obj.Estado,
                                Obj.Lote,
                                Obj.Licencia,
                                Obj.Vence,
                                Obj.Fecha,
                                Obj.Ubicacion_Origen,
                                Obj.Cantidad_pickeada,
                                Obj.Cantidad_verificada,
                                Obj.Peso_Pickeado,
                                Obj.Peso_Verificado,
                                Obj.CantidadDespachada,
                                Obj.PesoDespachado,
                                Obj.Encontrado,
                                Obj.Acepto,
                                Obj.No_Documento_WMS,
                                Obj.No_Referencia,
                                Obj.Nombre_Cliente,
                                Obj.Es_bodega_recepcion,
                                Obj.Es_bodega_traslado,
                                Obj.No_pase,
                                Obj.Observacion,
                                Obj.Numero,
                                Obj.Marchamo,
                                Obj.Nombre_Ruta,
                                Obj.Placa_Vehiculo,
                                Obj.Nombre_Piloto,
                                Obj.clasificacion,
                                Obj.marca,
                                Obj.familia,
                                Obj.parametro_a,
                                Obj.parametro_b,
                                Obj.numero_orden_ingreso,
                                Obj.codigo_poliza_ingreso,
                                Obj.IdPedidoEnc,
                                Obj.IdDespachoEnc,
                                Obj.numero_orden_pedido,
                                Obj.codigo_poliza_pedido,
                                Obj.codigo_regimen_salida,
                                Obj.placa_contenedor_salida,
                                Obj.Dua_salida)


                Next

                grdDetalleSalidas.DataSource = DT

                If GridView1.RowCount > 0 Then

                    '#GT04012023: primero aca, porque luego incluye las filas de las agrupaciones y esos no son regitros.
                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    Set_LayOut_Grid()

                    GridView1.OptionsView.ShowFooter = True
                    GridView1.Columns("Propietario").Group()
                    GridView1.Columns("Factor").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Factor").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Factor").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Factor").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Cant_Pick").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cant_Pick").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cant_Pick").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cant_Pick").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Cant_Veri").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cant_Veri").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cant_Veri").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cant_Veri").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Peso_Pick").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Peso_Pick").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Peso_Pick").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Peso_Pick").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Peso_Veri").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Peso_Veri").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Peso_Veri").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Peso_Veri").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Cant_Despachada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cant_Despachada").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cant_Despachada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cant_Despachada").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Peso_Despachado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Peso_Despachado").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Peso_Despachado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Peso_Despachado").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Fecha_Despacho").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView1.Columns("Fecha_Despacho").DisplayFormat.FormatString = "G"

                    GridView1.ExpandAllGroups()
                Else
                    lblRegs.Caption = String.Format("Registros: {0}", 0)
                End If

                Set_Label_Personalizados()

                GridView1.BestFitColumns(True)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

            IsLoading = False

            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

        Application.DoEvents()

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        cmdActualizar.Enabled = False
        Cargar()
        cmdActualizar.Enabled = True
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
            printLink.Component = grdDetalleSalidas
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

        Dim reportHeader As String = vbNewLine & "Salidas"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If dtpFechaDel.Value > dtpFechaAl.Value Or dtpFechaAl.Value < dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar()
            '    GridView1.Focus()
            'End If

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

            '#EJC20200114: Deshabilitado por rendimiento.
            'If dtpFechaDel.Value > dtpFechaAl.Value Or dtpFechaAl.Value < dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar()
            '    GridView1.Focus()
            'End If

            '#GT04012023: hace muy lenta la carga cuando fecha del es bastante antigúa.
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

    Private vNombreArchivoLayOutGrid As String = ""
    Private Sub frmDetalleSalidas_Shown(sender As Object, e As EventArgs) Handles Me.Shown


        Try

            vNombreArchivoLayOutGrid = "frmSalidas_List.xml"
            lblRegs.Caption = String.Format("Registros: 0")

            SetDatataTable()

            Cargar()

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

                        TheColumnToChange = GridView1.Columns.ColumnByFieldName("clasificacion")

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

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportarExcel.ItemClick
        Exportar_Grid_A_Excel(grdDetalleSalidas, "WMS_Salidas.xlsx")
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