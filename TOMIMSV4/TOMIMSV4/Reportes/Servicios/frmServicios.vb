Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmServicios

    Public ListRegistros As New List(Of clsBe_Servicio_Logistico)
    Private DT As New DataTable("Servicios")

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public pMovimiento As Boolean
    Public pIdProducto As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetDatataTable()

        DT.Columns.Add("Fecha_Servicio", GetType(Date))
        DT.Columns.Add("Almacen", GetType(Double))
        DT.Columns.Add("IdCliente", GetType(Integer))
        DT.Columns.Add("IdConsolidador", GetType(Integer))
        DT.Columns.Add("Codigo_Producto", GetType(String))
        DT.Columns.Add("Nombre_Producto", GetType(String))
        DT.Columns.Add("No_orden", GetType(String))
        DT.Columns.Add("No_Linea", GetType(String))
        DT.Columns.Add("Cantidad", GetType(Double))

    End Sub

    Private lPresentaciones As New List(Of clsBeProducto_Presentacion)

    Private Sub Cargar_Datos_Servicio()

        Dim Presentacion As New clsBeProducto_Presentacion
        Dim VCantidadUMbas As Double = 0
        Dim vCantidadPres As Double = 0
        Dim Conver As Double = 0
        Dim IdxPresentacion As Integer = -1

        Try

            ListRegistros = clsLnTrans_servicio_det.Get_Servicios_By_Rango_Fechas(dtpFechaDel.Value, dtpFechaAl.Value)

            DT.Clear()

            If ListRegistros.Count > 0 Then

                For Each Obj As clsBe_Servicio_Logistico In ListRegistros

                    DT.Rows.Add(Obj.Fecha_Servicio,
                                Obj.Almacen,
                                Obj.IdCliente,
                                Obj.IdConsolidador,
                                Obj.Codigo_producto,
                                Obj.Nombre_Producto,
                                Obj.No_orden,
                                Obj.No_Linea,
                                Obj.Cantidad)

                Next

                grdRegistroInt.DataSource = DT

            End If


            If GridView1.RowCount > 0 Then

                GridView1.OptionsView.ShowFooter = True
                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.BestFitColumns(True)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmRegistrosInterface_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetDatataTable()
        Cargar_Datos_Servicio()
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos_Servicio()
    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar_Datos_Servicio()
            '    GridView1.Focus()
            'End If

            Cargar_Datos_Servicio()
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
            '    Cargar_Datos_Servicio()
            '    GridView1.Focus()
            'End If

            Cargar_Datos_Servicio()
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