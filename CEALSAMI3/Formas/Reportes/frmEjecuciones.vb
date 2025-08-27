Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmEjecuciones

    Public Property Modo As pModo
    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum
    Public pLista As New List(Of clsBeI_nav_ejecucion_enc)
    Private DTS As New DataTable("Ejecuciones")

    Private Sub SetDatataTable()

        DTS.Columns.Add("Id Ejecución", GetType(Int32)).ReadOnly = True
        DTS.Columns.Add("Id Empresa", GetType(Integer)).ReadOnly = True
        DTS.Columns.Add("Id Bodega", GetType(Integer)).ReadOnly = True
        DTS.Columns.Add("Id Propietario", GetType(Integer)).ReadOnly = True
        DTS.Columns.Add("Empresa", GetType(String)).ReadOnly = True
        DTS.Columns.Add("Bodega", GetType(String)).ReadOnly = True
        DTS.Columns.Add("Propietario", GetType(String)).ReadOnly = True
        DTS.Columns.Add("Configuración", GetType(String)).ReadOnly = True
        DTS.Columns.Add("Interface", GetType(String)).ReadOnly = True
        DTS.Columns.Add("Registros WS", GetType(Integer)).ReadOnly = True
        DTS.Columns.Add("Registros TI", GetType(Integer)).ReadOnly = True
        DTS.Columns.Add("Registros WMS", GetType(Integer)).ReadOnly = True
        DTS.Columns.Add("Exitosa", GetType(Boolean)).ReadOnly = True
        DTS.Columns.Add("Error", GetType(String)).ReadOnly = True
        DTS.Columns.Add("Fecha", GetType(String)).ReadOnly = True
        DTS.Columns.Add("Referencia", GetType(String)).ReadOnly = True

    End Sub

    Private Sub Encabezado()

        Dim lRow As DataRow

        Try


            pLista = clsLnI_nav_ejecucion_enc.GetAllEjecuciones(fchDel.Value, fchAl.Value)

            Dim ListarEjecuciones = From i In pLista Group i By Keys = New With {Key i.IdEjecucionEnc, Key i.Empresa,
                                                                Key i.Bodega, Key i.Propietario, Key i.Configuracion, Key i.Interfaces, Key i.Fecha} Into Group
                                    Select New With {.id = Keys.IdEjecucionEnc, .empresa = Keys.Empresa, .bodega = Keys.Bodega, .propietario = Keys.Propietario, .config = Keys.Configuracion,
                                                        .interface = Keys.Interfaces, .fecha = Keys.Fecha}


            If ListarEjecuciones IsNot Nothing AndAlso ListarEjecuciones.Count > 0 Then

                DSEjecuciones.Encabezado.Clear()

                For Each Obj In ListarEjecuciones

                    lRow = DSEjecuciones.Encabezado.NewRow

                    lRow.Item("IdEjecución") = Obj.id
                    lRow.Item("Empresa") = Obj.empresa
                    lRow.Item("Bodega") = Obj.bodega
                    lRow.Item("Propietario") = Obj.propietario
                    lRow.Item("Configuración") = Obj.config
                    lRow.Item("Interface") = Obj.interface
                    lRow.Item("Fecha") = Obj.fecha

                    DSEjecuciones.Encabezado.AddEncabezadoRow(lRow)

                Next

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Detalle()

        Dim lRow As DataRow

        Try


            pLista = clsLnI_nav_ejecucion_enc.GetAllEjecucionesDetalle(fchDel.Value, fchAl.Value)

            Dim ListarEjecuciones = From i In pLista Group i By Keys = New With {Key i.IdEjecucionEnc, Key i.registros_ti,
                                                                Key i.registros_ws, Key i.registros_wms, Key i.Exitosa_res, Key i.Errores, Key i.Referencia, Key i.Configuracion, Key i.Interfaces, Key i.Fecha_error} Into Group
                                    Select New With {.id = Keys.IdEjecucionEnc, .reg_it = Keys.registros_ti, .reg_ws = Keys.registros_ws, .reg_wms = Keys.registros_wms, .exitosa = Keys.Exitosa_res, .error = Keys.Exitosa_res, .ref = Keys.Referencia, .config = Keys.Configuracion,
                                                        .interface = Keys.Interfaces, .fecha_error = Keys.Fecha_error}

            If ListarEjecuciones IsNot Nothing AndAlso ListarEjecuciones.Count > 0 Then

                DSEjecuciones.Detalle.Clear()

                For Each Obj In ListarEjecuciones

                    lRow = DSEjecuciones.Detalle.NewRow

                    lRow.Item("idejecucionenc") = Obj.id
                    lRow.Item("Configuracion") = Obj.config
                    lRow.Item("Interface") = Obj.interface
                    lRow.Item("registros_ws") = Obj.reg_ws
                    lRow.Item("registros_ti") = Obj.reg_it
                    lRow.Item("registros_wms") = Obj.reg_wms
                    lRow.Item("exitosa") = Obj.exitosa
                    lRow.Item("error") = Obj.error
                    lRow.Item("fecha_error") = Obj.fecha_error
                    lRow.Item("referencia") = Obj.ref

                    DSEjecuciones.Detalle.AddDetalleRow(lRow)

                Next

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub frmEjecuciones_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cargar()
    End Sub

    Private Sub Cargar()

        Try

            grdEjecuciones.BeginUpdate()

            Encabezado()

            GridView1.OptionsView.ColumnAutoWidth = False
            GridView1.BestFitColumns()

            Detalle()

            grdEjecuciones.EndUpdate()

            grdEjecuciones.ForceInitialize()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub fchAl_ValueChanged(sender As Object, e As EventArgs) Handles fchAl.ValueChanged

        Try

            If Me.fchDel.Value > Me.fchAl.Value Or Me.fchAl.Value < Me.fchDel.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                Cargar()
                GridView1.Focus()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub fchDel_ValueChanged(sender As Object, e As EventArgs) Handles fchDel.ValueChanged

        Try

            If Me.fchDel.Value > Me.fchAl.Value Or Me.fchAl.Value < Me.fchDel.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                Cargar()
                GridView1.Focus()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
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

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Imprimir_Vista()

        Try

            GridView1.OptionsPrint.ExpandAllDetails = True
            GridView1.OptionsPrint.PrintDetails = True

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdEjecuciones
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Reporte de Ejecuciones"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub grdEjecuciones_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdEjecuciones.ViewRegistered

        Try

            Dim gridView As GridView = e.View
            gridView.OptionsView.ColumnAutoWidth = False
            gridView.BestFitColumns()

            gridView.Columns("idejecucionenc").Visible = False
            gridView.Columns("Configuracion").Caption = "Configuración"
            gridView.Columns("registros_ws").Caption = "Registros WS"
            gridView.Columns("registros_ti").Caption = "Registros TI"
            gridView.Columns("registros_wms").Caption = "Registros WMS"
            gridView.Columns("exitosa").Caption = "Exitosa"
            gridView.Columns("error").Caption = "Error"
            gridView.Columns("fecha_error").Caption = "Fecha Error"
            gridView.Columns("referencia").Caption = "Referencia"

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
End Class