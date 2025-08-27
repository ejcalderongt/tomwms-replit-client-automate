Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmRegistrosInterface

    Public ListRegistros As New List(Of clsBeI_nav_transacciones_out)
    Private DT As New DataTable("RegistrosInterface")

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

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

        DT.Columns.Add("IdTransaccion", GetType(Integer))
        DT.Columns.Add("No_OrdenCompraEnc", GetType(Integer))
        DT.Columns.Add("No_RecepciónEnc", GetType(Integer))
        DT.Columns.Add("No_PedidoEnc", GetType(Integer))
        DT.Columns.Add("No_DespachoEnc", GetType(Integer))
        DT.Columns.Add("No_Linea", GetType(Integer))
        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("Unidad_Medida", GetType(String))
        ' DT.Columns.Add("Presentacion", GetType(String))
        DT.Columns.Add("Tipo_Transacción", GetType(String))
        DT.Columns.Add("No_Pedido", GetType(String))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("Vence", GetType(Date))
        DT.Columns.Add("Licencia", GetType(String))
        DT.Columns.Add("CantidadUMBas", GetType(Double))
        DT.Columns.Add("CantidadPresentacion", GetType(Double))
        DT.Columns.Add("Peso", GetType(Double))
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

            ListRegistros = clsLnI_nav_transacciones_out.Get_All_By_Fecha(dtpFechaDel.Value, dtpFechaAl.Value)

            DT.Clear()

            If ListRegistros.Count > 0 Then

                For Each Obj As clsBeI_nav_transacciones_out In ListRegistros

                    vCantidadPres = 0
                    VCantidadUMbas = Obj.Cantidad
                    vNombreUnidad = Obj.Unidad_medida

                    If Obj.Idpresentacion > 0 Then

                        vCantidadPres = Obj.Cantidad

                        Presentacion.IdPresentacion = Obj.Idpresentacion

                        IdxPresentacion = lPresentaciones.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                        If IdxPresentacion = -1 Then
                            clsLnProducto_presentacion.GetSingle(Presentacion)
                            lPresentaciones.Add(Presentacion)
                        Else
                            Presentacion = lPresentaciones(IdxPresentacion)
                        End If

                        VCantidadUMbas = Presentacion.Factor * Obj.Cantidad
                    End If

                    Dim vFechaTransaccion As Date = Now

                    If Obj.Tipo_transaccion = "INGRESO" Then
                        vFechaTransaccion = Obj.Fecha_recepcion
                    Else
                        vFechaTransaccion = Obj.fecha_despacho
                    End If

                    '#CKFK20221026 Quité la Presentacion porque en la i_nav_transacciones_out solo se guarda la descripción
                    'de la unidad de medida de la transacción, no importa si es UMBas o Presentación
                    DT.Rows.Add(Obj.Idtransaccion,
                                Obj.Idordencompra,
                                Obj.Idrecepcionenc,
                                Obj.Idpedidoenc,
                                Obj.Iddespachoenc,
                                Obj.No_linea,
                                Obj.Codigo_producto,
                                Obj.Nombre_producto,
                                vNombreUnidad,
                                Obj.Tipo_transaccion,
                                Obj.No_pedido,
                                Obj.Lote,
                                Obj.Fecha_vence,
                                Obj.Lic_Plate,
                                VCantidadUMbas,
                                vCantidadPres,
                                Obj.Peso,
                                vFechaTransaccion,
                                Obj.Enviado)

                Next

                grdRegistroInt.DataSource = DT

                If GridView1.RowCount > 0 Then

                    GridView1.OptionsView.ShowFooter = True
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns(True)

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.Columns("CantidadUMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("CantidadPresentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmRegistrosInterface_Load(sender As Object, e As EventArgs) Handles Me.Load
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

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar_Datos_RegistrosInterface()
            '    GridView1.Focus()
            'End If

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

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar_Datos_RegistrosInterface()
            '    GridView1.Focus()
            'End If

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
                        '.MdiParent = MdiParent
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

End Class