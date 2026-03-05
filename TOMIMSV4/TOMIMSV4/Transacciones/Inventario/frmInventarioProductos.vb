Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraTreeList

Public Class frmInventarioProductos

    Private ListStock As New List(Of clsBeVW_stock_res)
    Private ListProductos As New List(Of clsBeProducto)
    Private gBeInventarioCiclico As New clsBeTrans_inv_ciclico
    Public FechasCongelacion As DateTime
    Public Propietario As String = ""
    Public Property IdPropietario As Integer = 0
    Public Property IdBodega As Integer = 0
    Public IdInventario As Integer = 0
    Public IdOperador As Integer = 0

    Private DTProductos As New DataTable

    Private Sub Cargar()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando productos...")

            twExistencias.Enabled = False

            'dgridProductos.ClearNodes()

            'Encabezado(dgridProductos)
            Detalle()

            SplashScreenManager.CloseForm()

        Catch ex As Exception
            SplashScreenManager.CloseForm()
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Encabezado(ByRef tl As TreeList)

        Try

            tl.BeginUpdate()
            tl.Columns.Add()
            tl.Columns(0).Caption = "IdStock"
            tl.Columns(0).VisibleIndex = 0
            tl.Columns(0).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(1).Caption = "Código"
            tl.Columns(1).VisibleIndex = 1
            tl.Columns(1).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(2).Caption = "Producto"
            tl.Columns(2).VisibleIndex = 2
            tl.Columns(2).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(3).Caption = "UMBas"
            tl.Columns(3).VisibleIndex = 3
            tl.Columns(3).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(4).Caption = "Cantidad UMBas"
            tl.Columns(4).VisibleIndex = 4
            tl.Columns(4).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(5).Caption = "Presentación"
            tl.Columns(5).VisibleIndex = 5
            tl.Columns(5).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(6).Caption = "Cantidad Pres"
            tl.Columns(6).VisibleIndex = 6
            tl.Columns(6).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(7).Caption = "IdProductoBodega"
            tl.Columns(7).VisibleIndex = 7
            tl.Columns(7).Visible = False
            tl.Columns(7).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(8).Caption = "IdProductoEstado"
            tl.Columns(8).VisibleIndex = 8
            tl.Columns(8).Visible = False
            tl.Columns(8).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(9).Caption = "IdPresentacion"
            tl.Columns(9).VisibleIndex = 9
            tl.Columns(9).Visible = False
            tl.Columns(9).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(10).Caption = "IdUbicacion"
            tl.Columns(10).VisibleIndex = 10
            tl.Columns(10).Visible = False
            tl.Columns(10).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(11).Caption = "lote"
            tl.Columns(11).VisibleIndex = 11
            tl.Columns(11).Visible = False
            tl.Columns(11).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(12).Caption = "fecha"
            tl.Columns(12).VisibleIndex = 12
            tl.Columns(12).Visible = False
            tl.Columns(12).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(13).Caption = "Peso"
            tl.Columns(13).VisibleIndex = 13
            tl.Columns(13).Visible = False
            tl.Columns(13).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(14).Caption = "Cantidad_Final"
            tl.Columns(14).VisibleIndex = 14
            tl.Columns(14).Visible = False
            tl.Columns(14).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(15).Caption = "EsPallet"
            tl.Columns(15).VisibleIndex = 15
            tl.Columns(15).Visible = False
            tl.Columns(15).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(16).Caption = "lic_plate"
            tl.Columns(16).VisibleIndex = 16
            tl.Columns(16).Visible = False
            tl.Columns(16).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.EndUpdate()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Detalle()

        Try

            Dim vCantidad As Double = 0.0
            Dim vCantidad_final As Double = 0.0
            Dim vCantidad_factor As Double = 0.0
            Dim EsPallet As Boolean = False

            '#EJC20180806: Evitar sobrecargar al inicio de lista en productos para inventario
            If IdInventario = 0 Then Exit Sub

            DTProductos = clsLnProducto.Get_All_By_IdPropietario_And_Bodega_Para_Inventario(IdPropietario,
                                                                                            IdBodega,
                                                                                            IdInventario,
                                                                                            twExistencias.Checked)
            Dgrid.DataSource = DTProductos

            Try

                If DTProductos.Rows.Count > 0 Then
                    Dim ritem As RepositoryItemCheckEdit = TryCast(gvProductos.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
                    AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            gvProductos.BestFitColumns(True)

            '#EJC20180806: Ocultar algunas columnas y mostrarlas en el columnchoser del grid para el enduser

            gvProductos.Columns("IndiceRotacion").Visible = False
            gvProductos.Columns("IndiceRotacion").OptionsColumn.ShowInCustomizationForm = True

            gvProductos.Columns("IndiceRotacion").Visible = False
            gvProductos.Columns("IndiceRotacion").OptionsColumn.ShowInCustomizationForm = True

            gvProductos.Columns("IdProductoBodega").Visible = False
            gvProductos.Columns("IdProductoBodega").OptionsColumn.ShowInCustomizationForm = True

            gvProductos.Columns("IdProducto").Visible = False
            gvProductos.Columns("IdProducto").OptionsColumn.ShowInCustomizationForm = True

            gvProductos.Columns("IdFamilia").Visible = False
            gvProductos.Columns("IdFamilia").OptionsColumn.ShowInCustomizationForm = True

            gvProductos.Columns("IdClasificacion").Visible = False
            gvProductos.Columns("IdClasificacion").OptionsColumn.ShowInCustomizationForm = True

            lblRegistros.Caption = String.Format("Registros: {0}", gvProductos.RowCount)
            lblRegistrosSeleccionados.Caption = String.Format("Regs. Seleccionados: {0}", 0)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = gvProductos.GetFocusedRow
                Dim lIndex As Integer = -1

                '#EJC20180809: Marcar el registro en el campo seleccionar (checkbox)
                Dr.Item("Seleccionar") = Not Dr.Item("Seleccionar")

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmInventarioProductos_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cargar()
    End Sub

    'Private Sub tlProductos_AfterCheckNode(sender As Object, e As NodeEventArgs)

    '    dgridProductos.SetFocusedNode(e.Node)

    '    Try

    '        Dim ND = dgridProductos.FocusedNode

    '        If ND.Checked Then

    '            ND.CheckAll()

    '            If twExistencias.IsOn = False Then
    '                Dim IdProductoBodega = ND.Item("IdProductoBodega")
    '                Dim Producto As New clsBeProducto
    '                Producto = clsLnProducto.Get_Single_ProductoBodega(IdProductoBodega)
    '                If Producto IsNot Nothing Then
    '                    With frmDatosProducto
    '                        .Modo = frmDatosProducto.TipoTrans.Nuevo
    '                        .Prod = Producto
    '                        Producto = .Prod
    '                        .ShowDialog()
    '                        .Focus()
    '                    End With

    '                    If Producto.Lote <> "" Then
    '                        ND.Item("lote") = Producto.Lote
    '                    End If

    '                    If Not Producto.FechaVence = Nothing Then '#CKFK 20180624 Modifiqué esto  Producto.FechaVence = "" ya que la fecha no es de tipo string y por eso daba el error
    '                        ND.Item("fecha") = Producto.FechaVence
    '                    End If

    '                    If Producto.Cantidad > 0 Then
    '                        ND.Item("Cantidad UMBas") = Producto.Cantidad
    '                        ND.Item("Cantidad_Final") = Producto.Cantidad
    '                    End If

    '                End If
    '            End If
    '        Else
    '            ND.UncheckAll()

    '        End If

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    Private Sub twExistencias_Toggled(sender As Object, e As EventArgs)
        Cargar()
    End Sub

    Private Function Guardar_Productos() As Boolean

        Dim ContadorExistentes As Integer = 0

        Guardar_Productos = False

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Asignando productos...")

            '#EJC20220119: validar si se definieron previamente ubicaciones para conteo.
            Dim lTransInvUbic As New List(Of clsBeTrans_inv_ciclico_ubic)
            lTransInvUbic = clsLnTrans_inv_ciclico_ubic.Get_All_By_IdInventarioEnc(IdInventario, AP.IdBodega)

            '#EJC20220119: si no se definieron, al agregar los productos se insertan todas las posiciones en las que se encuentra el producto.
            If lTransInvUbic.Count = 0 Then

                Guardar_Productos = clsLnTrans_inv_stock.Insertar_Inventario_Congelado(DTProductos,
                                                                                       IdInventario,
                                                                                       AP.UsuarioAp.IdUsuario,
                                                                                       IdOperador)

            Else
                '#EJC20220119: Agregar el producto solo en las ubicaciones que se definieron a priori.

                Guardar_Productos = clsLnTrans_inv_stock.Insertar_Inventario_Congelado(DTProductos,
                                                                                       IdInventario,
                                                                                       AP.UsuarioAp.IdUsuario,
                                                                                       IdOperador,
                                                                                       lTransInvUbic)

            End If

            If Not Guardar_Productos Then
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("No se agregó Producto(s) a la lista", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub cmdAgregar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdAgregar.ItemClick

        Try

            If Guardar_Productos() Then
                XtraMessageBox.Show("Producto(s) adicionado(s)", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            Else
                Cargar()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub tlProductos_MouseClick(sender As Object, e As MouseEventArgs)
    '    dgridProductos.SetFocusedNode(dgridProductos.GetNodeAt(e.X, e.Y))
    'End Sub

    Private Sub mnuMarcarTodos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMarcarTodos.CheckedChanged

        Try

            pgbProductos.Properties.PercentView = True
            pgbProductos.Properties.Minimum = 0
            pgbProductos.Properties.Step = 1
            pgbProductos.Visible = True
            pgbProductos.EditValue = 0

            If twExistencias.Checked Then

                Dim vValorActual As Boolean = False

                For i As Integer = 0 To gvProductos.DataRowCount - 1
                    vValorActual = gvProductos.GetRowCellValue(i, "Seleccionar")
                    gvProductos.SetRowCellValue(i, "Seleccionar", Not vValorActual)
                    pgbProductos.PerformStep()
                    pgbProductos.Update()
                Next i

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            pgbProductos.Visible = False
        End Try

    End Sub

    Private Sub gvProductos_ColumnFilterChanged(sender As Object, e As EventArgs) Handles gvProductos.ColumnFilterChanged

        Try

            lblRegistros.Caption = String.Format("Registros: {0}", gvProductos.RowCount)
            lblRegistrosSeleccionados.Caption = String.Format("Regs. Seleccionados: {0}", 0)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub twExistencias_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles twExistencias.CheckedChanged
        Cargar()
    End Sub
End Class