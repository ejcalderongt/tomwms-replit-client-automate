Imports System.IO
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmResumenHistorico

    'Private DT As New DataTable("DocResHistorico")
    Private pBeCliente As clsBeCliente
    Private IsLoading As Boolean = False


    Public Property Modo As pModo
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    'Private Sub SetDatataTable()

    '    DT.Columns.Add("REGIMEN", GetType(String))
    '    DT.Columns.Add("CLIENTE", GetType(String))
    '    DT.Columns.Add("NUMERO_ORDEN", GetType(String))
    '    DT.Columns.Add("CODIGO_POLIZA", GetType(String))
    '    DT.Columns.Add("MATERIAL_NAME", GetType(String))
    '    DT.Columns.Add("FECHA", GetType(DateTime))
    '    DT.Columns.Add("SHORT_NAME", GetType(String))
    '    DT.Columns.Add("LICENCIA", GetType(String))
    '    DT.Columns.Add("CANTIDAD", GetType(Double))
    '    DT.Columns.Add("CIF", GetType(Double))
    '    DT.Columns.Add("DAI", GetType(Double))
    '    DT.Columns.Add("IVA", GetType(Double))
    '    DT.Columns.Add("TOTAL_VALOR", GetType(Double))

    'End Sub

    Private Sub frmResumenHistorico_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            IsLoading = True
            IMS.ListarRegimen(cmbRegimen)
            AP.Listar_Bodegas_By_Usuario(cmbBodega)
            Listar_Propietarios()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsLoading = False
        End Try

    End Sub

    'Private Sub lnkProveedor_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkProveedor.LinkClicked

    '    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
    '    SplashScreenManager.Default.SetWaitFormCaption("Clientes")

    '    Try



    '        Dim Proveedor As New frmProveedor_List() With {.IdBodega = cmbBodega.EditValue,
    '                .Modo = frmProveedor_List.pModo.Seleccion,
    '                .StartPosition = FormStartPosition.CenterParent,
    '                .WindowState = FormWindowState.Maximized}
    '        SplashScreenManager.CloseForm(False)
    '        Proveedor.ShowDialog()

    '        If Proveedor.pObjP IsNot Nothing AndAlso Proveedor.pObjP.IdProveedor <> 0 Then
    '            txtIdProveedor.Tag = Proveedor.pObjP.IdAsignacion
    '            txtIdProveedor.Text = Proveedor.pObjP.IdProveedor
    '            txtNombreProveedor.Text = Proveedor.pObjP.Proveedor.Nombre
    '        End If

    '        Proveedor.Close()
    '        Proveedor.Dispose()



    '    Catch ex As Exception
    '        SplashScreenManager.CloseForm(False)
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try

    'End Sub


    Private Sub Listar_Propietarios()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(cmbBodega.EditValue)
            lcmbPropietario.Properties.DataSource = DT1
            lcmbPropietario.Properties.ValueMember = "IdPropietarioBodega"
            lcmbPropietario.Properties.DisplayMember = "Nombre"
            lcmbPropietario.Properties.PopupWidth = 700
            lcmbPropietario.Properties.BestFit()
            lcmbPropietario.Properties.PopulateColumns()
            lcmbPropietario.Properties.Columns(0).Visible = False
            lcmbPropietario.Properties.Columns(1).Visible = False
            lcmbPropietario.Properties.NullText = ""

            If DT1.Rows.Count = 1 Then
                lcmbPropietario.Text = DT1.Rows(0).Item("Nombre").ToString
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



    Private Sub cmdActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdActualizar.ItemClick

        cmdActualizar.Enabled = False
        Generar_Reporte()
        cmdActualizar.Enabled = True

    End Sub

    Private Sub fchDel_ValueChanged(sender As Object, e As EventArgs)

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

    Private Sub fchAl_ValueChanged(sender As Object, e As EventArgs)

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

    Private Sub Generar_Reporte()

        Dim IdPropietario As Integer
        Dim DT As New DataTable
        grdExistenciasConLp.DataSource = Nothing

        Try

            If Not IsLoading Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            End If

            If lcmbPropietario.EditValue <> 0 Then
                'GT 16032021 se obtiene id propietario del combo en lugar de idpropietariobodega
                Dim fila As Object = lcmbPropietario.GetSelectedDataRow
                IdPropietario = fila.Item("IdPropietario")
            Else
                IdPropietario = 0

            End If

            Dim pTimeOut = clsBD.Instancia.TimeOutConBD

            DT.Clear()

            DT = clsLnReportesFiscales.Get_Fiscal_historico(dtpFechaDesde.Value, dtpfechaHasta.Value, cmbRegimen.EditValue, IdPropietario, pTimeOut)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    grdExistenciasConLp.DataSource = Nothing
                    grdExistenciasConLp.DataSource = DT

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    GridView1.OptionsView.ShowFooter = True


                    GridView1.Columns("CANTIDAD").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CANTIDAD").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("CANTIDAD").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CANTIDAD").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("CIF").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CIF").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("CIF").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CIF").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("DAI").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("DAI").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("DAI").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("DAI").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("IVA").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("IVA").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("IVA").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("IVA").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("TOTAL_VALOR").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("TOTAL_VALOR").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("TOTAL_VALOR").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("TOTAL_VALOR").SummaryItem.DisplayFormat = "{0:n6}"



                End If

            End If

            GridView1.BestFitColumns(True)


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

        Application.DoEvents()

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick


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
            clsUiPrintHelper.PrintGridPreview(grdExistenciasConLp, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = grdExistenciasConLp
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
                              vbNewLine & "Resumen histórico " &
                              vbNewLine & "BODEGA: " & AP.NomBodega

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdSalir.ItemClick

        Close()

    End Sub

    Private Sub cmdImpExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImpExcel.ItemClick

        Try

            'dgrid.ExportToXlsx()

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "VW_Resumen_Historico_" & FormatoFechas.tFecha(dtpFechaDesde.Value) & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    grdExistenciasConLp.ExportToXlsx(myStream)
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

    Private Sub lcmbPropietario_KeyDown(sender As Object, e As KeyEventArgs) Handles lcmbPropietario.KeyDown

        Try

            If e.KeyCode = Keys.Back Then
                lcmbPropietario.EditValue = 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub
End Class



