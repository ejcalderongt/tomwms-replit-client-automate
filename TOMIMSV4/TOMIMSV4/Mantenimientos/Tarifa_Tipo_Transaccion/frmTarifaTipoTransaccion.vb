Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmTarifaTipoTransaccion

    Public pObjBEJ As New clsBeTarifa_tipo_transaccion

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Private Sub frmTarifaTipoTransaccion_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Try


            Set_Columnas_Grid_Detalle() : Set_Datata_Table_Grid_Detalle()

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnTarifa_tipo_transaccion.MaxID() + 1

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                    dgridServiciosAsociados.DataSource = DTGridDetalle


                Case TipoTrans.Editar

                    clsLnTarifa_tipo_transaccion.GetSingle(pObjBEJ)

                    DTGridDetalle = clsLnTarifa_tipo_transaccion_det.Get_All_By_IdTipoTransaccion_DT(pObjBEJ.IdTipoTransaccion)

                    If DTGridDetalle Is Nothing Then
                        Set_Datata_Table_Grid_Detalle()
                    End If

                    dgridServiciosAsociados.DataSource = DTGridDetalle

                    lblCodigo.Text = pObjBEJ.IdTipoTransaccion
                    txtCodigo.Text = pObjBEJ.Codigo
                    txtNombre.Text = pObjBEJ.Nombre
                    chkActivo.Checked = pObjBEJ.Activo

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

            gvDetalleServicios.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        txtCodigo.Focus()

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            pObjBEJ = New clsBeTarifa_tipo_transaccion() With {.IdTipoTransaccion = clsLnTarifa_tipo_transaccion.MaxID(),
                .Codigo = txtCodigo.Text.Trim(),
                .Nombre = txtNombre.Text.Trim(),
                .Activo = True}

            Llena_Lista_Detalle_Servicios()

            Guardar = clsLnTarifa_tipo_transaccion.Guardar_Transaccion(pObjBEJ, True) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub Llena_Lista_Detalle_Servicios()

        Try

            Dim vIdServicio As String = ""
            Dim BeTarifaTipoTransaccionDet As New clsBeTarifa_tipo_transaccion_det

            If Modo = TipoTrans.Nuevo Then

                For i As Integer = 0 To gvDetalleServicios.DataRowCount - 1
                    vIdServicio = gvDetalleServicios.GetRowCellValue(i, "IdServicio")
                    BeTarifaTipoTransaccionDet = New clsBeTarifa_tipo_transaccion_det()
                    BeTarifaTipoTransaccionDet.IdTipoTransaccion = pObjBEJ.IdTipoTransaccion
                    BeTarifaTipoTransaccionDet.IdServicio = Val(vIdServicio)
                    BeTarifaTipoTransaccionDet.Activo = True
                    pObjBEJ.lDetalleServicios.Add(BeTarifaTipoTransaccionDet)
                Next

            ElseIf Modo = TipoTrans.Editar Then

                Dim Idx As Integer = -1
                Dim vActivo As Boolean = True

                For i As Integer = 0 To gvDetalleServicios.DataRowCount - 1

                    vIdServicio = gvDetalleServicios.GetRowCellValue(i, "IdServicio")
                    Idx = pObjBEJ.lDetalleServicios.FindIndex(Function(x) x.IdServicio = vIdServicio)

                    If Idx = -1 Then
                        BeTarifaTipoTransaccionDet = New clsBeTarifa_tipo_transaccion_det()
                        BeTarifaTipoTransaccionDet.IdTipoTransaccion = pObjBEJ.IdTipoTransaccion
                        BeTarifaTipoTransaccionDet.IdServicio = Val(vIdServicio)
                        BeTarifaTipoTransaccionDet.Activo = vActivo
                        pObjBEJ.lDetalleServicios.Add(BeTarifaTipoTransaccionDet)
                    End If

                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjBEJ.Codigo = txtCodigo.Text.Trim()
                pObjBEJ.Nombre = txtNombre.Text.Trim()
                pObjBEJ.Activo = chkActivo.Checked

                Llena_Lista_Detalle_Servicios()

                Actualizar = clsLnTarifa_tipo_transaccion.Guardar_Transaccion(pObjBEJ, False) > 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Datos_Correctos() Then
            If MessageBox.Show("¿Guardar TarifaTipoTransaccion?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If MessageBox.Show("¿Desactivar TarifaTipoTransaccion?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False
                If clsLnTarifa_tipo_transaccion.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                    frmTarifaTipoTransaccion_List.Dgrid.Refresh()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtUbicacion_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If
        If e.KeyChar = "." Then
            e.Handled = True
        End If
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

    Private ProductoGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private MotivoDevolcuionGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private txtNoLineaGrid As New RepositoryItemTextEdit
    Private txtCantidadGrid As New RepositoryItemSpinEdit
    Private DTGridDetalle As New DataTable

    Private Sub Set_Datata_Table_Grid_Detalle()

        DTGridDetalle.Columns.Clear()
        DTGridDetalle.Columns.Add("IdServicio", GetType(String))
        DTGridDetalle.Columns.Add("Codigo", GetType(String))
        DTGridDetalle.Columns.Add("Nombre", GetType(String))
        DTGridDetalle.Columns.Add("Nemonico", GetType(String))

    End Sub

    Private Sub Set_Columnas_Grid_Detalle()


        Try


            Dim ColIndexAux As Integer = 0

            gvDetalleServicios.OptionsView.ShowFooter = True
            gvDetalleServicios.OptionsView.ShowGroupPanel = False

#Region "Columna - Código_Servicio"


            ProductoGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdServicio", .Caption = "IdServicio", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True},
                New GridColumn With {.FieldName = "Nemonico", .Caption = "Nemonico", .Visible = True}
            })

            ProductoGridLookUpEdit.ValueMember = "IdServicio"
            ProductoGridLookUpEdit.DisplayMember = "Codigo"
            ProductoGridLookUpEdit.NullText = ""
            ProductoGridLookUpEdit.DataSource = clsLnI_nav_servicio.Get_All_For_Grid()
            ProductoGridLookUpEdit.View.BestFitColumns()
            ProductoGridLookUpEdit.PopupFormWidth = 700

            AddHandler ProductoGridLookUpEdit.Leave, AddressOf articuloGridLookUpEdit_Leave

            ProductoGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True


            Dim ColCodigoProducto As New GridColumn With {
                .FieldName = "IdServicio",
                .Caption = "Codigo",
                .Visible = True,
                .VisibleIndex = 1,
                .ColumnEdit = ProductoGridLookUpEdit
            }

            ColCodigoProducto.OptionsColumn.AllowEdit = True
            gvDetalleServicios.Columns.Add(ColCodigoProducto)

#End Region

#Region "Columna - Nombre_Producto"

            Dim ColNombreProducto As New GridColumn With {
                .FieldName = "Nombre",
                .Caption = "Nombre",
                .Visible = True,
                .VisibleIndex = 2,
                .Width = 150
            }

            ColNombreProducto.OptionsColumn.AllowEdit = False

            gvDetalleServicios.Columns.Add(ColNombreProducto)

#End Region

#Region "Columna - Nemonico"


            Dim ColNemonico As New GridColumn With {
                .FieldName = "Nemonico",
                .Caption = "Nemónico",
                .Visible = True,
                .VisibleIndex = 3,
                .Width = 75
            }

            ColNemonico.OptionsColumn.AllowEdit = False

            gvDetalleServicios.Columns.Add(ColNemonico)

#End Region

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub articuloGridLookUpEdit_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaRequisicion As DataRow = gvDetalleServicios.GetFocusedDataRow()
            If drLineaRequisicion Is Nothing Then Return
            Dim drArticulo As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
            If drArticulo Is Nothing Then Return
            drLineaRequisicion("Nombre") = drArticulo("Nombre")
            drLineaRequisicion("Nemonico") = drArticulo("Nemonico")
            drLineaRequisicion("Codigo") = drArticulo("Codigo")
            gvDetalleServicios.FocusedColumn = gvDetalleServicios.Columns("Nombre")
            gvDetalleServicios.PostEditor()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub gridView1_ValidatingEditor(ByVal sender As Object, ByVal e As BaseContainerValidateEditorEventArgs) Handles gvDetalleServicios.ValidatingEditor

        Try

            Dim view As GridView = TryCast(sender, GridView)

            If view.FocusedColumn.FieldName = "Cantidad" Then

                Dim Cantidad As Double = Val(e.Value)

                If Cantidad = 0 Then
                    e.Valid = False
                    e.ErrorText = "Cantidad > 0"
                Else
                    e.Valid = True
                End If

            End If

            If view.FocusedColumn.FieldName = "Codigo" Then

                Dim CodigoProducto As String = (e.Value)

                If CodigoProducto = "" Then
                    e.Valid = False
                    e.ErrorText = "Servicio?"
                Else
                    e.Valid = True
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub gvDetalleServicios_ValidateRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles gvDetalleServicios.ValidateRow

        Try

            Dim view As GridView = TryCast(sender, GridView)

            If view.FocusedColumn.FieldName = "Cantidad" Then
            End If


            Dim CodigoProducto As String = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "Codigo")), "", view.GetRowCellValue(e.RowHandle, "Codigo"))

            Dim ColCodProd As GridColumn = view.Columns().ColumnByFieldName("Codigo")

            If CodigoProducto = "" Then
                e.Valid = False
                'e.ErrorText = "Producto no definido."                
                view.SetColumnError(ColCodProd, "Producto?")
                Exit Sub
            Else
                If Not Existe_Codigo(CodigoProducto) Then
                    view.SetColumnError(ColCodProd, "")
                    e.Valid = True
                Else
                    e.Valid = False
                    'e.ErrorText = "Producto no definido."                
                    view.SetColumnError(ColCodProd, "Ya existe!")
                End If
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Existe_Codigo(ByVal pCodigo As String) As Boolean

        Existe_Codigo = False

        Try

            Dim vCodigoServicio As String = ""


            For i As Integer = 0 To gvDetalleServicios.DataRowCount - 1
                vCodigoServicio = gvDetalleServicios.GetRowCellValue(i, "Codigo")
                If vCodigoServicio = pCodigo Then
                    Existe_Codigo = True
                    Exit For
                End If
            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub gvDetalleServicios_KeyDown(sender As Object, e As KeyEventArgs) Handles gvDetalleServicios.KeyDown

        Try

            If (e.KeyCode = Keys.Delete And e.Modifiers = Keys.Control) Then

                Dim view As GridView = CType(sender, GridView)

                Dim IdServicio As String = IIf(IsDBNull(view.GetRowCellValue(view.FocusedRowHandle, "IdServicio")), "", view.GetRowCellValue(view.FocusedRowHandle, "IdServicio"))
                Dim CodigoServicio As String = IIf(IsDBNull(view.GetRowCellValue(view.FocusedRowHandle, "Codigo")), "", view.GetRowCellValue(view.FocusedRowHandle, "Codigo"))

                If (MessageBox.Show("¿Eliminar Servicio: " & CodigoServicio & "?", "Confirmación",
                  MessageBoxButtons.YesNo) <> DialogResult.Yes) Then Return

                Dim Idx As Integer = pObjBEJ.lDetalleServicios.FindIndex(Function(x) x.IdServicio = IdServicio)

                If Idx <> -1 Then
                    pObjBEJ.lDetalleServicios(Idx).Activo = False
                End If

                view.DeleteRow(view.FocusedRowHandle)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class