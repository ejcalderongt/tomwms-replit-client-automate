Imports System.Threading.Tasks
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmPendientesRequisicion

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Listar_Productos_Pendientes_Requisicion()

        Try

            DgridProdPendRequi.DataSource = Nothing

            Dim lista As New List(Of clsBeVW_stock_res)

            lista = clsLnStock.GetProductsPendientesRequisicion(0).ToList

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("Productos")
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Nombre", GetType(String))
                DT.Columns.Add("Barra", GetType(String))
                DT.Columns.Add("UM", GetType(String))
                DT.Columns.Add("Presentación", GetType(String))
                DT.Columns.Add("Cant_Presentación", GetType(Double))
                DT.Columns.Add("Cant_U.M_Bas", GetType(Double))
                DT.Columns.Add("Estado", GetType(String))
                DT.Columns.Add("Min", GetType(Double))
                DT.Columns.Add("Max", GetType(Double))
                DT.Columns.Add("Rotacion", GetType(String))

                Parallel.ForEach(lista, Sub(ByVal Obj)
                                            SyncLock DT
                                                DT.Rows.Add(Obj.Propietario, Obj.Codigo_Producto, Obj.Nombre_Producto, Obj.Codigo_Barra, Obj.UMBas, Obj.Nombre_Presentacion, Obj.CantidadPresentacion, Obj.CantidadUmBas, Obj.NomEstado, Obj.Existencia_min_umbas, Obj.Existencia_max_umbas, Obj.IndiceRotacion)
                                            End SyncLock
                                        End Sub)

                DgridProdPendRequi.DataSource = DT
                DgridProdPendRequi.DefaultView.RefreshData()

                Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Count, "Propietario", "Count={0}")

                grdvProductosPendientesRequi.BestFitColumns()

                If grdvProductosPendientesRequi.Columns("Propietario").Summary.Count = 0 Then
                    grdvProductosPendientesRequi.Columns("Propietario").Summary.Add(item)
                End If

                Dim item1 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Cant_Presentación", "Sum={0:n6}")

                If grdvProductosPendientesRequi.Columns("Cant_Presentación").Summary.Count = 0 Then
                    grdvProductosPendientesRequi.Columns("Cant_Presentación").Summary.Add(item1)
                End If

                Dim item2 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Cant_U.M_Bas", "Sum={0:n6}")

                If grdvProductosPendientesRequi.Columns("Cant_U.M_Bas").Summary.Count = 0 Then
                    grdvProductosPendientesRequi.Columns("Cant_U.M_Bas").Summary.Add(item2)
                End If

                grdvProductosPendientesRequi.Columns("Cant_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvProductosPendientesRequi.Columns("Cant_Presentación").DisplayFormat.FormatString = "{0:n6}"

                grdvProductosPendientesRequi.Columns("Cant_U.M_Bas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvProductosPendientesRequi.Columns("Cant_U.M_Bas").DisplayFormat.FormatString = "{0:n6}"

                grdvProductosPendientesRequi.Columns("Min").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvProductosPendientesRequi.Columns("Min").DisplayFormat.FormatString = "{0:n6}"

                grdvProductosPendientesRequi.Columns("Max").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvProductosPendientesRequi.Columns("Max").DisplayFormat.FormatString = "{0:n6}"

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

    Private Sub frmPendientesRequisicion_Load(sender As Object, e As EventArgs) Handles Me.Load
        Listar_Productos_Pendientes_Requisicion()
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Listar_Productos_Pendientes_Requisicion()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            grdvProductosPendientesRequi.OptionsPrint.ExpandAllDetails = True
            grdvProductosPendientesRequi.OptionsPrint.PrintDetails = True

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
            printLink.Component = DgridProdPendRequi
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

        Dim reportHeader As String = vbNewLine & "Pendientes de Requisición"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub grdvProductosPendientesRequi_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles grdvProductosPendientesRequi.RowCellStyle

        Try

            If e.RowHandle > -1 Then

                Dim View As GridView = sender

                If Not View.Columns("Rotacion") Is Nothing Then

                    Dim Rotacion As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Rotacion"))

                    If e.Column.FieldName = "Rotacion" Then

                        If Rotacion = "A" Then

                            e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)

                            e.Appearance.BackColor = Color.Salmon
                            e.Appearance.BackColor2 = Color.SeaShell

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
End Class