Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmMovimientos_Detalle

    Private pListMovs As New List(Of clsBeVW_Movimientos)
    Private DT As New DataTable("MovimientosPorDocumento")

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum
    Dim pIdRec As Integer
    Dim pIdOC As Integer

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub lblOC_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblOC.LinkClicked

        Try

            txtIdRec.Text = ""
            txtNoFac.Text = ""
            pIdRec = 0

            Dim OC As New frmOrdenCompra_List() With {
                   .Modo = frmOrdenCompra_List.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            OC.ShowDialog()

            grdMovimientosDetalle.DataSource = Nothing

            If OC.gBeOrdenCompra IsNot Nothing AndAlso OC.gBeOrdenCompra.IdOrdenCompraEnc <> 0 Then
                pIdOC = OC.gBeOrdenCompra.IdOrdenCompraEnc
                txtIdOC.Text = OC.gBeOrdenCompra.IdOrdenCompraEnc
                txtNoDocOC.Text = OC.gBeOrdenCompra.No_Documento + " - " + OC.gBeOrdenCompra.Referencia
            End If

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

    Private Sub lblRec_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblRec.LinkClicked

        Try

            txtIdOC.Text = ""
            txtNoDocOC.Text = ""
            pIdOC = 0

            Dim Rec As New frmRecepcion_List() With {
                   .Modo = frmRecepcion_List.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            grdMovimientosDetalle.DataSource = Nothing

            If Rec.gBeRecepcion IsNot Nothing AndAlso Rec.gBeRecepcion.IdRecepcionEnc <> 0 Then

                pIdRec = Rec.gBeRecepcion.IdRecepcionEnc
                txtIdRec.Text = Rec.gBeRecepcion.IdRecepcionEnc

                Rec.gBeRecepcion = clsLnTrans_re_enc.GetSingle(Rec.gBeRecepcion.IdRecepcionEnc)

                txtNoFac.Text = Rec.gBeRecepcion.NoDocumentoOC

                Dim lFacturas As New List(Of clsBeTrans_re_fact)
                lFacturas = clsLnTrans_re_fact.GetAllByRecepcion(Rec.gBeRecepcion.IdRecepcionEnc)

                For Each F In lFacturas
                    txtNoFac.Text += F.NoFactura & " "
                Next

            End If

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

    Public Sub Cargar()

        Dim cant As Double
        Dim IdOC As Integer
        Dim IdRec As Integer

        Try

            If txtIdOC.Text <> "" Or txtIdRec.Text <> "" Then

                IdOC = IIf(txtIdOC.Text <> "", txtIdOC.Text.Trim, 0)
                IdRec = IIf(txtIdRec.Text <> "", txtIdRec.Text.Trim, 0)

                pListMovs = clsLnTrans_movimientos.GetMovimientosByOC(IdOC, IdRec, cmbBodega.EditValue)

                If pListMovs.Count > 0 Then

                    grdMovimientosDetalle.DataSource = Nothing
                    DT.Clear()

                    For Each Obj As clsBeVW_Movimientos In pListMovs

                        If Obj.TTarea = "DESP" Then
                            cant = (Obj.Cantidad * -1)
                        Else
                            cant = Obj.Cantidad
                        End If

                        DT.Rows.Add(Obj.Propietario,
                                    Obj.Poliza,
                                     Obj.Producto,
                                     Obj.Codigo,
                                     Obj.CodigoBarra,
                                     Obj.UMBas,
                                     Obj.Presentacion,
                                     Obj.EstadoOrigen,
                                     Obj.EstadoDestino,
                                     cant,
                                     Obj.Peso,
                                     Obj.Lote,
                                     Obj.Fecha_Vence,
                                     Obj.UbicOrigen,
                                     Obj.UbicDestino,
                                     Obj.TTarea,
                                     Obj.Fecha,
                                     Obj.IdBodegaOrigen)
                    Next

                    grdMovimientosDetalle.DataSource = DT

                    If GridView1.Columns.Count > 0 Then


                        GridView1.OptionsView.ShowFooter = True

                        GridView1.Columns("Código").Group()

                        Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad",
                              .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                              .DisplayFormat = "{0:n6}",
                              .ShowInGroupColumnFooter = GridView1.Columns("Cantidad")}
                        GridView1.GroupSummary.Add(item)

                        Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Peso",
                              .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                              .DisplayFormat = "{0:n6}",
                              .ShowInGroupColumnFooter = GridView1.Columns("Peso")}
                        GridView1.GroupSummary.Add(item1)

                        GridView1.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.ExpandAllGroups()
                        GridView1.BestFitColumns(True)

                    End If

                End If

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            Else
                XtraMessageBox.Show("No hay datos para mostrar", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdMovimientosDetalle, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = grdMovimientosDetalle
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

        Dim reportHeader As String = vbNewLine & "Listado de Movimientos"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub SetDatataTable()

        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Poliza", GetType(String))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Código Barra", GetType(String))
        DT.Columns.Add("UMBas", GetType(String))
        DT.Columns.Add("Presentación", GetType(String))
        DT.Columns.Add("Estado Origen", GetType(String))
        DT.Columns.Add("Estado Destino", GetType(String))
        DT.Columns.Add("Cantidad", GetType(Double))
        DT.Columns.Add("Peso", GetType(Double))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("FechaVence", GetType(Date))
        DT.Columns.Add("Ubicación Origen", GetType(String))
        DT.Columns.Add("Ubicación Destino", GetType(String))
        DT.Columns.Add("Tipo Tarea", GetType(String))
        DT.Columns.Add("Fecha", GetType(Date))
        DT.Columns.Add("IdBodegaOrigen", GetType(Integer))

    End Sub

    Private Sub frmMovimientos_Detalle_Load(sender As Object, e As EventArgs) Handles Me.Load
        clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
        SetDatataTable()
        AP.Listar_Bodegas_By_Usuario(cmbBodega)
    End Sub

    Private Sub txtIdOC_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdOC.KeyPress

        Try

            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If

            If e.KeyChar = "." Then
                e.Handled = True
            End If

            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdOC.Text.Length = 1 Then
                txtNoDocOC.Text = String.Empty
                DT.Clear()
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

    Private Sub txtIdOC_Validated(sender As Object, e As EventArgs) Handles txtIdOC.Validated

        Try

            If String.IsNullOrEmpty(txtIdOC.Text.Trim()) = False AndAlso txtIdOC.Text > "0" Then

                Dim Obj As New clsBeTrans_oc_enc
                Obj = clsLnTrans_oc_enc.GetSingleForMovs(txtIdOC.Text, cmbBodega.EditValue)

                If Obj IsNot Nothing AndAlso Obj.IdOrdenCompraEnc > 0 Then
                    txtNoDocOC.Text = Trim(String.Format("{0}", Obj.No_Documento) + " - " + Obj.Referencia)
                    txtIdRec.Clear()
                    txtNoFac.Clear()
                    Cargar()
                Else

                    XtraMessageBox.Show(String.Format("No existe orden de compra con código: {0}", txtIdOC.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdOC.Focus()
                    txtIdOC.SelectAll()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdRec_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdRec.KeyPress

        Try

            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If

            If e.KeyChar = "." Then
                e.Handled = True
            End If

            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdRec.Text.Length = 1 Then
                txtNoFac.Text = String.Empty
                DT.Clear()
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

    Private Sub txtIdRec_Validated(sender As Object, e As EventArgs) Handles txtIdRec.Validated

        Try

            If String.IsNullOrEmpty(txtIdRec.Text.Trim()) = False AndAlso txtIdRec.Text > "0" Then

                Dim Obj As New clsBeTrans_re_enc
                Obj = clsLnTrans_re_enc.GetSingleForMovs(txtIdRec.Text, cmbBodega.EditValue)

                If Obj IsNot Nothing AndAlso Obj.IdRecepcionEnc > 0 Then
                    txtNoFac.Text = Trim(String.Format("{0}", Obj.NOFactura))
                    txtIdOC.Clear()
                    txtNoDocOC.Clear()
                    Cargar()
                Else

                    XtraMessageBox.Show(String.Format("No existe recepción con código: {0}", txtIdRec.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdRec.Focus()
                    txtIdRec.SelectAll()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        Cargar()
    End Sub
End Class



