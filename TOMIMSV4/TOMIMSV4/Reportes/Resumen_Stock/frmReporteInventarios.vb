Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Public Class frmReporteInventarios

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Dt As New DataTable("Stock")

    Private Sub Set_DataTable()

        Dt = New DataTable("Stock")

        Dt.Columns.Add(("Código"), GetType(String))
        Dt.Columns.Add(("Producto"), GetType(String))
        Dt.Columns.Add(("Existencias UMBas"), GetType(Double))
        Dt.Columns.Add(("U.M.Bas"), GetType(String))
        Dt.Columns.Add(("Presentación"), GetType(String))
        Dt.Columns.Add(("Estado"), GetType(String))
        Dt.Columns.Add(("Fecha Ingreso"), GetType(Date))
        Dt.Columns.Add(("Fecha Vence"), GetType(Date))
        Dt.Columns.Add(("IdUbicación"), GetType(Integer))
        Dt.Columns.Add(("Rack"), GetType(Integer))
        Dt.Columns.Add(("Ubicación"), GetType(String))
        Dt.Columns.Add(("Recepción"), GetType(Integer))
        Dt.Columns.Add(("Motivo Devolución"), GetType(String))
        Dt.Columns.Add(("Póliza"), GetType(String))
        Dt.Columns.Add(("Referencia"), GetType(String))
        Dt.Columns.Add(("No_Docto_Rec"), GetType(String))

    End Sub

    Private Sub frmReporteInventarios_Load(sender As Object, e As EventArgs) Handles Me.Load
        Set_DataTable()

        AP.Listar_Bodegas_By_Usuario(cmbBodega)

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietarios, cmbBodega.EditValue)

    End Sub

    Private Sub Cargar_Datos()

        Try

            Dt.Clear()

            Dt = clsLnStock.Get_All_Stock_DT_By_IdBodega_And_IdPropietarioBodega(cmbBodega.EditValue, cmbPropietarios.EditValue, dtpFechaDel.Value, dtpFechaAl.Value, chkSinFechas.Checked, chkConsolidaFechas.Checked)

            grdStock.DataSource = Dt

            If GridView1.RowCount > 0 Then

                GridView1.OptionsView.ShowFooter = True
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                GridView1.BestFitColumns(True)

                GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Disponible_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietarios, cmbBodega.EditValue)

        Cargar_Datos()

    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar_Datos()
            '    GridView1.Focus()
            'End If

            Cargar_Datos()
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

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar_Datos()
            '    GridView1.Focus()
            'End If

            Cargar_Datos()
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

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        Cargar_Datos()

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
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

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Exportar_Grid_A_Excel(grdStock, "WMS_DetalleExistencias.xlsx")
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
            printLink.Component = grdStock
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

        Dim reportHeader As String = vbNewLine & "Detalle de inventario"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub chkSinFechas_CheckedChanged(sender As Object, e As EventArgs) Handles chkSinFechas.CheckedChanged

        If chkSinFechas.Checked Then
            GroupControl1.Enabled = False
        Else
            GroupControl1.Enabled = True
        End If

        Cargar_Datos()

    End Sub

    Private Sub CheckEdit1_CheckedChanged(sender As Object, e As EventArgs)
        Cargar_Datos()
    End Sub

    Private Sub chkConsolidaFechas_CheckedChanged(sender As Object, e As EventArgs) Handles chkConsolidaFechas.CheckedChanged
        Cargar_Datos()
    End Sub
End Class