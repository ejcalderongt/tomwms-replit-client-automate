Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class fmrUbicaciones_Picking

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private DT As New DataTable("ExistenciasPorEstado")

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGrid As String = ""

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Cargar()

        Try

            DT.Clear()

            If txtIdPicking.EditValue <> 0 Then
                DT = clsLnTrans_picking_ubic.Get_Ubicacion_Picking_By_IdPicking_And_IdPedidoEnc(IIf(txtIdPicking.Text = "", 0, txtIdPicking.Text), 0)
            End If

            If DT.Rows.Count > 0 Then

                grdUbicPick.DataSource = DT

                Restore_LayOut_Grid()

                GridView1.BestFitColumns(True)

                GridView1.Columns(0).GroupIndex = 0
                GridView1.OptionsView.ShowFooter = True

                GridView1.OptionsBehavior.AutoExpandAllGroups = True

                GridView1.Columns("Solicitado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Solicitado").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("Solicitado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Solicitado").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("Picking").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Picking").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("Picking").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Picking").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("Verificado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Verificado").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("Verificado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Verificado").DisplayFormat.FormatString = "{0:n2}"

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

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Cargar()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdUbicPick, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = grdUbicPick
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

        Dim reportHeader As String = vbNewLine & "Listado de Ubicación Picking"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub txtIdPicking_Validated(sender As Object, e As EventArgs) Handles txtIdPicking.Validated

        Try

            If txtIdPicking.Text > "0" Then

                Cargar()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdPicking_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdPicking.KeyPress

        Try

            If txtIdPicking.Text.Length = 1 Then
                Cargar()
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

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Close()
    End Sub

    Private Sub cmdConsultar_Click(sender As Object, e As EventArgs) Handles cmdConsultar.Click

        If txtIdPicking.Text = "" Then
            XtraMessageBox.Show("Debe ingresar un código de picking para consultar", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Cargar()
        End If

    End Sub

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

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            'vNombreArchivoLayOutGrid = "grdUbicacionesPicking.xml"

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub fmrUbicaciones_Picking_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            vNombreArchivoLayOutGrid = "grdUbicacionesPicking.xml"

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick
        Try


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

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub
End Class



