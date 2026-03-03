Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmProductoPed_List

    Public pIdProductoExcepto As Integer
    Public pObjProducto As clsBeProducto
    Public pIdBodega As Integer
    Public pIdPropietario As Integer
    Public Property Modo As pModo
    Public Property vNombreArchivoLayOutGrid As String = ""
    Private WithEvents bgWorker As New System.ComponentModel.BackgroundWorker

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Property AplicarFiltro As Boolean = False

    Sub New(Optional ByVal pAplicarFiltro As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()
        AplicarFiltro = pAplicarFiltro

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Listar()

        Try

            If Not AplicarFiltro Then
                Get_All_Producto()
            Else
                Get_Producto_By_Filter()
            End If

            Restore_LayOut_Grid()

            Try
                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.BestFitColumns()
            Catch ex As Exception
            End Try


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try


    End Sub

    Private Sub Get_All_Producto()

        Try

            Dim lista As New List(Of clsBeProducto)

            lista = clsLnProducto.Get_All_Producto(chkActivos.Checked).ToList()

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("Producto")
                DT.Columns.Add("Correlativo", GetType(Integer))
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("Clasificación", GetType(String))
                DT.Columns.Add("Familia", GetType(String))
                DT.Columns.Add("Marca", GetType(String))
                DT.Columns.Add("Tipo Producto", GetType(String))
                DT.Columns.Add("Unidad Medida", GetType(String))

                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Nombre", GetType(String))

                DT.Columns.Add("Existencia Mínima", GetType(Double))
                DT.Columns.Add("Existencia Máxima", GetType(Double))
                DT.Columns.Add("Costo", GetType(Double))
                DT.Columns.Add("Precio", GetType(Double))

                For Each Obj As clsBeProducto In lista

                    If pIdProductoExcepto <> 0 Then

                        If Obj.IdProducto <> pIdProductoExcepto Then
                            DT.Rows.Add(Obj.IdProducto, Obj.Propietario.Nombre_comercial, Obj.Clasificacion.Nombre,
                             Obj.Familia.Nombre, Obj.Marca.Nombre, Obj.TipoProducto.NombreTipoProducto,
                             Obj.UnidadMedida.Nombre, Obj.Codigo, Obj.Nombre, Obj.Existencia_min,
                             Obj.Existencia_max, Obj.Costo, Obj.Precio)
                        End If

                    Else

                        DT.Rows.Add(Obj.IdProducto, Obj.Propietario.Nombre_comercial, Obj.Clasificacion.Nombre,
                                  Obj.Familia.Nombre, Obj.Marca.Nombre, Obj.TipoProducto.NombreTipoProducto,
                                  Obj.UnidadMedida.Nombre, Obj.Codigo, Obj.Nombre, Obj.Existencia_min,
                                  Obj.Existencia_max, Obj.Costo, Obj.Precio)
                    End If

                Next

                Dgrid.DataSource = DT

                If GridView1.RowCount > 0 Then
                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                End If

            Else
                Dgrid.DataSource = Nothing
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Get_Producto_By_Filter()

        Try


            Dim lista As New List(Of clsBeProducto)

            lista = clsLnProducto.Get_All_By_Propietario(True, chkActivos.Checked, pIdBodega, pIdPropietario).ToList()

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("Producto")
                DT.Columns.Add("Correlativo", GetType(Integer))
                DT.Columns.Add("ID", GetType(Integer))
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("Clasificación", GetType(String))
                DT.Columns.Add("Familia", GetType(String))
                DT.Columns.Add("Marca", GetType(String))
                DT.Columns.Add("Tipo Producto", GetType(String))
                DT.Columns.Add("Unidad Medida", GetType(String))

                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Nombre", GetType(String))

                DT.Columns.Add("Existencia Mínima", GetType(Double))
                DT.Columns.Add("Existencia Máxima", GetType(Double))
                DT.Columns.Add("Costo", GetType(Double))
                DT.Columns.Add("Precio", GetType(Double))

                For Each Obj As clsBeProducto In lista

                    DT.Rows.Add(Obj.IdProductoBodega, Obj.IdProducto, Obj.Propietario.Nombre_comercial, Obj.Clasificacion.Nombre,
                                  Obj.Familia.Nombre, Obj.Marca.Nombre, Obj.TipoProducto.NombreTipoProducto,
                                  Obj.UnidadMedida.Nombre, Obj.Codigo, Obj.Nombre, Obj.Existencia_min,
                                  Obj.Existencia_max, Obj.Costo, Obj.Precio)
                Next

                Dgrid.DataSource = DT

                If GridView1.RowCount > 0 Then
                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                End If

            Else
                Dgrid.DataSource = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick

        Try

            Dim Producto As New frmProducto(frmProducto.TipoTrans.Nuevo)
            Producto.ShowDialog()
            Producto.Dispose()

            If Not bgWorker.IsBusy Then
                bgWorker.RunWorkerAsync()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                pObjProducto = New clsBeProducto

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Pedido")
                If pIdBodega = 0 AndAlso pIdPropietario = 0 Then
                    pObjProducto = clsLnProducto.GetSingle(Dr.Item("Correlativo"))
                Else
                    pObjProducto = clsLnProducto.GetSingle(Dr.Item("ID"))
                    pObjProducto.IdProductoBodega = CInt(Dr.Item("Correlativo"))
                End If
                SplashScreenManager.CloseForm(False)
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle
                If Modo = pModo.Lista Then
                    Dim Producto As New frmProducto(frmProducto.TipoTrans.Editar) With {.pBeProducto = pObjProducto}
                    Producto.ShowDialog()
                    Producto.Dispose()
                    If Not bgWorker.IsBusy Then
                        bgWorker.RunWorkerAsync()
                    End If
                    GridView1.FocusedRowHandle = lSelectionIndex
                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        If Not bgWorker.IsBusy Then
            bgWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Not bgWorker.IsBusy Then
            bgWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
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
            printLink.Component = Dgrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Productos"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub frmProductoPed_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            vNombreArchivoLayOutGrid = "frmProductoPed_List.xml"

            bgWorker.WorkerReportsProgress = True
            bgWorker.WorkerSupportsCancellation = True

            AddHandler bgWorker.DoWork, AddressOf bgWorker_DoWork
            AddHandler bgWorker.RunWorkerCompleted, AddressOf bgWorker_RunWorkerCompleted

            If Not bgWorker.IsBusy Then
                bgWorker.RunWorkerAsync()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub bgWorker_DoWork(sender As Object, e As DoWorkEventArgs)

        Try

            If Not AplicarFiltro Then
                Get_All_Producto()
            Else
                Get_Producto_By_Filter()
            End If

        Catch ex As Exception
            ' Manejar excepciones o registrar errores
            ' No interactúes directamente con la interfaz de usuario aquí
        End Try
    End Sub

    Private Sub bgWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)

        Try

            If e.Error IsNot Nothing Then
                ' Manejar cualquier excepción que ocurrió en DoWork
            Else
                ' Actualiza la interfaz de usuario aquí, por ejemplo, restaurando el diseño de la cuadrícula
                Restore_LayOut_Grid()
                Try
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns()
                Catch ex As Exception
                    ' Manejar excepciones
                End Try
            End If

        Catch ex As Exception

        End Try

    End Sub


    Private Sub frmProductoPed_List_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        'Listar()
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

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

    Private Sub Restore_LayOut_Grid()

        Try

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            'vNombreArchivoLayOutGrid = "frmProductoPed_List.xml"

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