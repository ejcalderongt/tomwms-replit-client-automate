Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen

Public Class frmInventarioProductosRFID

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

    Public Es_Inventario_RFID As Boolean = False

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

    Private Function Guardar_Productos() As Boolean

        Dim ContadorExistentes As Integer = 0

        Guardar_Productos = False

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Asignando productos...")

            '#EJC20220119: validar si se definieron previamente ubicaciones para conteo.

            Dim lTransInvUbic As New List(Of clsBeTrans_inv_ciclico_ubic)
            lTransInvUbic = clsLnTrans_inv_ciclico_ubic.Get_All_By_IdInventarioEnc(IdInventario, AP.IdBodega)

            '#GT27052026: actualmente no se manejan ubicaciones, se agrega todo el producto seleccionado.
            If lTransInvUbic.Count = 0 Then

                Guardar_Productos = clsLnTrans_inv_stock.Agregar_Producto_A_Inventario_Ciclico_RFID(DTProductos,
                                                                                            IdInventario,
                                                                                            AP.UsuarioAp.IdUsuario,
                                                                                            IdOperador,
                                                                                            AP.IdBodega)

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

    Private Sub Cargar()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando productos...")

            chkExistencias.Enabled = False

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

    Private Sub Detalle()

        Try

            Dim vCantidad As Double = 0.0
            Dim vCantidad_final As Double = 0.0
            Dim vCantidad_factor As Double = 0.0
            Dim EsPallet As Boolean = False

            '#EJC20180806: Evitar sobrecargar al inicio de lista en productos para inventario
            If IdInventario = 0 Then Exit Sub

            '#GT28052026: listar productos sin barra_epc porque el mismo producto puede existir con varios SSCC
            DTProductos = clsLnProducto.Get_All_By_IdPropietario_And_Bodega_Para_Inventario_RFID(IdPropietario,
                                                                                                 IdBodega,
                                                                                                 IdInventario)

            Dgrid.DataSource = DTProductos

            Try

                If DTProductos.Rows.Count > 0 Then
                    Dim ritem As RepositoryItemCheckEdit = TryCast(gvProductos.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
                    AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


            'gvProductos.Columns("Barra_epc").Caption = "Etiqueta RFID"

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
            'lblRegistrosSeleccionados.Caption = String.Format("Regs. Seleccionados: {0}", 0)

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

    Private Sub frmInventarioProductosRFID_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Es_Inventario_RFID Then
                chkExistencias.Checked = True
            End If

            Cargar()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub mnuMarcarTodos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMarcarTodos.CheckedChanged
        Try

            pgbProductos.Properties.PercentView = True
            pgbProductos.Properties.Minimum = 0
            pgbProductos.Properties.Step = 1
            pgbProductos.Visible = True
            pgbProductos.EditValue = 0

            Dim vValorActual As Boolean = False

            For i As Integer = 0 To gvProductos.DataRowCount - 1
                vValorActual = gvProductos.GetRowCellValue(i, "Seleccionar")
                gvProductos.SetRowCellValue(i, "Seleccionar", Not vValorActual)
                pgbProductos.PerformStep()
                pgbProductos.Update()
            Next i

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            pgbProductos.Visible = False
        End Try
    End Sub
End Class