Imports System.IO
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmRegistrosInterfaceRes

    Public ListRegistros As New List(Of clsBeI_nav_transacciones_out)
    Private DT As New DataTable("RegistrosInterfaceRes")

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private vNombreArchivoLayOutGrid As String = "GridRegistrosInterfaceRes"
    Public Property IsLoading As Boolean = True
    Enum pModo
        Lista = 1
        Seleccion = 2
        ''' <summary>
        ''' #EJC20240917: Listar las que tengan cantidad pendiente de ajuste en SAP.
        ''' </summary>
        Transacciones_Reenvio = 3
    End Enum

    Public pMovimiento As Boolean
    Public pIdProducto As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetDatataTable()

        DT.Columns.Add("IdBodega", GetType(Integer))
        DT.Columns.Add("IdTransaccion", GetType(Integer))
        DT.Columns.Add("No_PedidoEnc", GetType(Integer))
        DT.Columns.Add("No_DespachoEnc", GetType(Integer))
        DT.Columns.Add("No_Pedido", GetType(String))
        DT.Columns.Add("No_Linea", GetType(Integer))
        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("Unidad_Medida", GetType(String))
        DT.Columns.Add("Tipo_Transacción", GetType(String))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("Vence", GetType(Date))
        DT.Columns.Add("Licencia", GetType(String))
        DT.Columns.Add("Cantidad", GetType(Double))
        DT.Columns.Add("Cantidad_enviada", GetType(Double))
        DT.Columns.Add("Cantidad_pendiente", GetType(Double))
        DT.Columns.Add("Fecha_Transaccion", GetType(Date))
        DT.Columns.Add("Enviado", GetType(Boolean))

    End Sub

    Private lPresentaciones As New List(Of clsBeProducto_Presentacion)

    Private Sub Cargar_Datos_RegistrosInterface()

        Dim Presentacion As New clsBeProducto_Presentacion
        Dim VCantidadUMbas As Double = 0
        Dim vCantidadPres As Double = 0
        Dim Conver As Double = 0
        Dim IdxPresentacion As Integer = -1
        Dim vNombrePresentacion As String = ""
        Dim vIdPresentacion As Integer = 0
        Dim vNombreUnidad As String = ""

        Try

            ListRegistros = clsLnI_nav_transacciones_out.Get_All_Lotes_Pendientes_De_Envio(dtpFechaDel.Value, dtpFechaAl.Value)

            DT.Clear()

            If ListRegistros.Count > 0 Then

                For Each BeINavTransaccionOut As clsBeI_nav_transacciones_out In ListRegistros

                    vCantidadPres = 0
                    VCantidadUMbas = BeINavTransaccionOut.Cantidad
                    vNombreUnidad = BeINavTransaccionOut.Unidad_medida

                    If BeINavTransaccionOut.Idpresentacion > 0 Then

                        vCantidadPres = BeINavTransaccionOut.Cantidad

                        Presentacion.IdPresentacion = BeINavTransaccionOut.Idpresentacion

                        IdxPresentacion = lPresentaciones.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                        If IdxPresentacion = -1 Then
                            clsLnProducto_presentacion.GetSingle(Presentacion)
                            lPresentaciones.Add(Presentacion)
                        Else
                            Presentacion = lPresentaciones(IdxPresentacion)
                        End If

                        VCantidadUMbas = Presentacion.Factor * BeINavTransaccionOut.Cantidad
                    End If

                    Dim vFechaTransaccion As Date = Now

                    If BeINavTransaccionOut.Tipo_transaccion = "INGRESO" Then
                        vFechaTransaccion = BeINavTransaccionOut.Fecha_recepcion
                    Else
                        vFechaTransaccion = BeINavTransaccionOut.fecha_despacho
                    End If

                    DT.Rows.Add(BeINavTransaccionOut.Idbodega,
                                BeINavTransaccionOut.Idtransaccion,
                                BeINavTransaccionOut.Idpedidoenc,
                                BeINavTransaccionOut.Iddespachoenc,
                                BeINavTransaccionOut.No_pedido,
                                BeINavTransaccionOut.No_linea,
                                BeINavTransaccionOut.Codigo_producto,
                                BeINavTransaccionOut.Nombre_producto,
                                vNombreUnidad,
                                BeINavTransaccionOut.Tipo_transaccion,
                                BeINavTransaccionOut.Lote,
                                BeINavTransaccionOut.Fecha_vence,
                                BeINavTransaccionOut.Lic_Plate,
                                BeINavTransaccionOut.Cantidad,
                                BeINavTransaccionOut.Cantidad_Enviada,
                                BeINavTransaccionOut.Cantidad_Pendiente,
                                vFechaTransaccion,
                                BeINavTransaccionOut.Enviado)

                Next

                grdRegistroInt.DataSource = DT

                If GridView1.RowCount > 0 Then

                    GridView1.OptionsView.ShowFooter = True
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns(True)

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.Columns("Cantidad_enviada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_enviada").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Cantidad_enviada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_enviada").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Cantidad_pendiente").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_pendiente").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Cantidad_pendiente").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_pendiente").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                End If

            End If

            IsLoading = False

            Set_LayOut_Grid()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmRegistrosInterfaceRes_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetDatataTable()
        Cargar_Datos_RegistrosInterface()
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

        Try

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
        Cargar_Datos_RegistrosInterface()
    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            Cargar_Datos_RegistrosInterface()

            GridView1.Focus()

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

            Cargar_Datos_RegistrosInterface()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub grdRegistroInt_DoubleClick(sender As Object, e As EventArgs) Handles grdRegistroInt.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim Obj As New clsBeI_nav_transacciones_out
                Obj.Idtransaccion = Dr.Item("IdTransaccion")
                clsLnI_nav_transacciones_out.GetSingle(Obj)

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    With frmRegistroInter
                        .Modo = frmRegistroInter.TipoTrans.Editar
                        .pBeINavTransOut = Obj
                        .ShowDialog()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If

                Cargar_Datos_RegistrosInterface()

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {"Páginas: [Page # of Pages #] "})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", "Fecha: [Date Printed] [Time Printed] "})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdRegistroInt
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            SplashScreenManager.CloseForm(False)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & Text

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

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