Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen
Imports TOMWMS.frmOrdenCompra

Public Class frmRegServicio

    Private pListObjT As New List(Of clsTabla)
    Public gBeOrdenCompra As New clsBeTrans_oc_enc
    Public gBePedidoEnc As New clsBeTrans_pe_enc
    Public gBeServicio As New clsBeTrans_servicio_enc
    Public lTransServiciosDet As New List(Of clsBeTrans_servicio_det)

    Private ProductoGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private ServicioGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private PropietarioGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private PropietarioGridServiciosLookUpEdit As New RepositoryItemGridLookUpEdit
    Private PresentacionGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private MotivoDevolcuionGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private txtNoLineaGrid As New RepositoryItemTextEdit
    Private txtCantidadGrid As New RepositoryItemSpinEdit
    Private txtValorFOBGrid As New RepositoryItemSpinEdit
    Private txtValorDAIGrid As New RepositoryItemSpinEdit
    Private txtTotalGrid As New RepositoryItemSpinEdit
    Private txtCostoGrid As New RepositoryItemSpinEdit
    Public Property IngresoConsolidado As eTipoTrans = 1
    Private BeConfigBodega As New clsBeI_nav_config_enc

    Private gBeServicioEnc = New clsBeTrans_servicio_enc()
    Private tipo_doc_ingreso As Boolean = False

    Private IsClosing As Boolean = False
    Private IsLoading As Boolean = False
    Private DT As New DataTable
    Public TransServiciosDet As New clsBeTrans_servicio_det
    Public Delegate Sub Listar_Servicios()
    Public Property InvokeListarServicios As Listar_Servicios

    Public Property Modo As ModoTrans
    Public Enum ModoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property TipoTransaccion As pTipoTransaccion
    Enum pTipoTransaccion
        Ingreso = 1
        Salida = 2
    End Enum

    Public Sub New(ByVal pModo As ModoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Cargar_Info_DI()

        Try

            If gBeOrdenCompra IsNot Nothing AndAlso gBeOrdenCompra.IdOrdenCompraEnc > 0 Then
                txtNombreDocIngreso.Text = String.Format("Ref: {0} - Doc: {1}", gBeOrdenCompra.Referencia, gBeOrdenCompra.No_Documento)
                pListObjOrdeCompraDet = pObjOC.DetalleOC.ToList
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

    Private Sub Cargar_Servicios_Registrados(ByVal IdServicioEnc As Integer, ByVal IdBodega As Integer)

        Try

            DTGridDetalleServicios.Clear()

            lTransServiciosDet = clsLnTrans_servicio_det.List_By_Parameter(IdServicioEnc)

            If lTransServiciosDet.Count > 0 Then

                Dim servicio As New clsBeTrans_servicio_det()
                Dim BeAcuerdoDet As New clsBeI_nav_acuerdo_det()
                Dim vIdAcuerdoDet As String = ""
                Dim vIdPropietarioBodega As Integer = 0
                Dim vIdOrdenCompraServicio As Integer = 0
                Dim vCantidad As Double = 0
                Dim vNombreServicio As String = ""

                Llena_Propietarios_Grid()
                Llena_Servicios_By_Acuerdo()

                For Each Obj As clsBeTrans_servicio_det In lTransServiciosDet

                    'GT 29062021 se obtiene el IdPropietario, el IdBodegaPropietario no se almacena
                    vIdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(Obj.IdPropietario, IdBodega)

                    DTGridDetalleServicios.Rows.Add(vIdPropietarioBodega,
                                                    Obj.Codigo_producto,
                                                    Obj.Nombre_servicio,
                                                    Obj.Cantidad,
                                                    Obj.IdServicioDet)


                Next

            Else
                XtraMessageBox.Show("No hay servicios asociados al documento!")
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

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim bePolizaAux As clsBeTrans_oc_pol = Nothing

            If gBeOrdenCompra.ObjPoliza Is Nothing Then

                bePolizaAux = clsLnTrans_oc_pol.GetSingle(gBeOrdenCompra.IdOrdenCompraEnc)

                If bePolizaAux IsNot Nothing Then
                    gBeOrdenCompra.ObjPoliza = bePolizaAux
                Else
                    gBeOrdenCompra.ObjPoliza = New clsBeTrans_oc_pol
                End If
            End If

            gBeServicio = New clsBeTrans_servicio_enc()
            gBeServicio.IdServicioEnc = clsLnTrans_servicio_enc.MaxID() + 1

            If TipoTransaccion = pTipoTransaccion.Ingreso Then
                gBeServicio.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc
                gBeServicio.IdBodega = gBeOrdenCompra.IdBodega
                gBeServicio.Es_Ingreso = True
                gBeServicio.IdPedidoEnc = 0
            Else
                gBeServicio.IdPedidoEnc = Val(txtIdPedidoEnc.Text.Trim())
                gBeServicio.IdBodega = gBePedidoEnc.IdBodega
                gBeServicio.Es_Ingreso = False
                gBeServicio.IdOrdenCompraEnc = 0
            End If


            gBeServicio.No_poliza = gBeOrdenCompra.ObjPoliza.codigo_poliza
            gBeServicio.No_orden = gBeOrdenCompra.ObjPoliza.numero_orden
            gBeServicio.Fecha_doc_ingreso = gBeOrdenCompra.Fecha_Recepcion
            gBeServicio.Fecha_servicio = dtpFechaServicio.Value
            'GT 29062021 se obtiene el IdPropietario en lugar del IdpropietarioBodega
            Dim fila As Object = cmbPropietario.GetSelectedDataRow
            Dim IdPropietario_ As Integer = fila.Item("IdPropietario")
            gBeServicio.IdPropietario = IdPropietario_
            gBeServicio.IdEmpresa = AP.IdEmpresa
            gBeServicio.Fec_agr = Now
            gBeServicio.User_agr = AP.UsuarioAp.IdUsuario
            gBeServicio.User_mod = AP.UsuarioAp.IdUsuario
            gBeServicio.Fec_mod = Now
            gBeServicio.Activo = True
            clsLnTrans_servicio_enc.Guardar_Registro(gBeServicio, lTransServiciosDet)

            Guardar = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try


            'If Datos_Correctos() Then
            'End If

            'GT 03092021: Se remueve Datos_correctos() porque ya se validó al iniciar el evento del boton actualizar.
            gBeServicio.User_mod = AP.UsuarioAp.IdUsuario
            gBeServicio.Fec_mod = Now
            gBeServicio.IsNew = False
            gBeServicio.Activo = BarCheckItem1.Checked
            Actualizar = clsLnTrans_servicio_enc.Guardar_Registro(gBeServicio, lTransServiciosDet) > 0
            Actualizar = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
            Actualizar = False

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Cerrar_Documento() As Boolean

        Cerrar_Documento = False

        Try

            If Datos_Correctos() Then
                gBeServicio.User_mod = AP.UsuarioAp.IdUsuario
                gBeServicio.Fec_mod = Now
                gBeServicio.IsNew = False
                gBeServicio.Activo = BarCheckItem1.Checked
                gBeServicio.Estado = "Cerrado"
                Cerrar_Documento = clsLnTrans_servicio_enc.Cerrar_Documento_Ingreso(gBeServicio) > 0
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

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If TipoTransaccion = pTipoTransaccion.Ingreso AndAlso String.IsNullOrEmpty(txtIdOrdenCompra.Text.Trim()) Then
                XtraMessageBox.Show("Seleccione el documento de ingreso", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdOrdenCompra.Focus()
            ElseIf TipoTransaccion = pTipoTransaccion.Ingreso AndAlso Not Documento_Ingreso_Es_Valido() Then
                XtraMessageBox.Show("El número de documento de ingreso no es válido", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdOrdenCompra.Focus()
            ElseIf TipoTransaccion = pTipoTransaccion.Salida AndAlso String.IsNullOrEmpty(txtIdPedidoEnc.Text.Trim()) Then
                XtraMessageBox.Show("Seleccione el documento de salida", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdOrdenCompra.Focus()
            ElseIf TipoTransaccion = pTipoTransaccion.Salida AndAlso Not Pedido_Cliente_Es_Valido() Then
                XtraMessageBox.Show("El número de documento de ingreso no es válido", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdOrdenCompra.Focus()
            ElseIf Not Process_Servicios() Then
                XtraMessageBox.Show("El detalle del documento tiene errores o no se ha complementado, por favor revise el detalle.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdOrdenCompra.Focus()
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

    Private Function Pedido_Cliente_Es_Valido() As Boolean

        Try

            Return clsLnTrans_pe_enc.Existe_Documento_By_IdPedidoEnc(txtIdPedidoEnc.Text)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Documento_Ingreso_Es_Valido() As Boolean

        Try

            Return clsLnTrans_oc_enc.Existe_Documento_By_IdOrdenCompraEnc(txtIdOrdenCompra.Text)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Datos_Correctos() Then
            If XtraMessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'InvokeListarServicios.Invoke()
                    If Not InvokeListarServicios Is Nothing Then InvokeListarServicios.Invoke()
                    Close()

                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Try

            If Datos_Correctos() Then
                If XtraMessageBox.Show("¿Actualizar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Actualizar() Then
                        XtraMessageBox.Show("Se actualizó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Close()
                        If Not InvokeListarServicios Is Nothing Then InvokeListarServicios.Invoke()
                    End If
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If gBeOrdenCompra.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra Anulado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf gBeOrdenCompra.Enviado_A_ERP = 1 Then

                XtraMessageBox.Show("El registro ya se envio al ERP, no se puede Anular.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else

                gBeServicio.Activo = False
                gBeServicio.Estado = "Anulado"
                gBeServicio.User_mod = AP.UsuarioAp.User_mod
                gBeServicio.Fec_mod = Now

                If XtraMessageBox.Show("¿Eliminar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If clsLnTrans_servicio_enc.Anular(gBeServicio) > 0 Then
                        XtraMessageBox.Show("Se eliminó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Close()
                        InvokeListarServicios.Invoke()
                    End If
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmRegServicio_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private DTGridDetalleServicios As New DataTable("DetalleServicios")

    Private Sub Set_Datata_Table_Grid_Detalle_Servicios()

        DTGridDetalleServicios.Columns.Clear()
        DTGridDetalleServicios.Columns.Add("IdPropietarioBodega", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("IdAcuerdoDet", GetType(String))
        DTGridDetalleServicios.Columns.Add("nombre_servicio", GetType(String))
        DTGridDetalleServicios.Columns.Add("cantidad", GetType(Double))
        DTGridDetalleServicios.Columns.Add("IdServicioDet", GetType(Integer))

    End Sub

    Private Sub Set_Columnas_Grid_Detalle_Servicios()

        Dim VisibleIndex As Integer = 1

        Try

            Dim ColIndexAux As Integer = 0

            gvDetalleServicios.OptionsView.ShowFooter = True
            gvDetalleServicios.OptionsView.ShowGroupPanel = False

#Region "Columna - Propietario"

            PropietarioGridServiciosLookUpEdit.View.Columns.Clear()

            PropietarioGridServiciosLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdPropietarioBodega", .Caption = "IdPropietarioBodega", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            PropietarioGridServiciosLookUpEdit.ValueMember = "IdPropietarioBodega"
            PropietarioGridServiciosLookUpEdit.DisplayMember = "Nombre"
            PropietarioGridServiciosLookUpEdit.NullText = ""

            If Not gBeOrdenCompra Is Nothing Then
                If gBeOrdenCompra.TipoIngreso.IdTipoIngresoOC = eTipoTrans.Consolidado Then
                    PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdBodega_And_IdDocumentoIngreso_For_Combo(cmbBodega.EditValue, gBeOrdenCompra.IdOrdenCompraEnc)
                Else
                    If Modo = ModoTrans.Editar Then
                        If TipoTransaccion = pTipoTransaccion.Ingreso Then
                            PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdPropietarioBodega_For_Combo(gBeOrdenCompra.IdPropietarioBodega)
                        Else
                            PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdPropietarioBodega_For_Combo(gBePedidoEnc.IdPropietarioBodega)
                        End If
                    Else
                        If Not cmbPropietario.EditValue Is Nothing Then
                            PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdPropietarioBodega_For_Combo(cmbPropietario.EditValue)
                        End If
                    End If
                End If
            Else
                If Modo = ModoTrans.Editar Then

                    If TipoTransaccion = pTipoTransaccion.Ingreso Then
                        PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdPropietarioBodega_For_Combo(gBeOrdenCompra.IdPropietarioBodega)
                    Else
                        PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdPropietarioBodega_For_Combo(gBePedidoEnc.IdPropietarioBodega)
                    End If

                Else
                    If Not cmbPropietario.EditValue Is Nothing Then
                        PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdPropietarioBodega_For_Combo(cmbPropietario.EditValue)
                    End If
                End If
            End If


            PropietarioGridServiciosLookUpEdit.PopupFormWidth = 700
            PropietarioGridServiciosLookUpEdit.View.BestFitColumns()

            RemoveHandler PropietarioGridServiciosLookUpEdit.Leave, AddressOf PropietarioServicioGridLookUpEditDetalleIngreso_Leave
            AddHandler PropietarioGridServiciosLookUpEdit.Leave, AddressOf PropietarioServicioGridLookUpEditDetalleIngreso_Leave

            PropietarioGridServiciosLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColPropieario As New GridColumn With {
                .FieldName = "IdPropietarioBodega",
                .Caption = "Propietario",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .ColumnEdit = PropietarioGridServiciosLookUpEdit
            }
            ColPropieario.Width = 500
            ColPropieario.OptionsColumn.AllowEdit = True
            ColPropieario.Visible = True '(TipoTrans = eTipoTrans.Consolidado)
            gvDetalleServicios.Columns.Add(ColPropieario)

            VisibleIndex += 1

#End Region

#Region "Columna - Código_Servicio"


            ServicioGridLookUpEdit.View.Columns.Clear()

            ServicioGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdAcuerdoDet", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "nombre_servicio", .Caption = "Servicio", .Visible = True}
            })

            ServicioGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True
            ServicioGridLookUpEdit.ValueMember = "IdAcuerdoDet"
            ServicioGridLookUpEdit.DisplayMember = "IdAcuerdoDet"
            ServicioGridLookUpEdit.NullText = ""

            RemoveHandler ServicioGridLookUpEdit.Leave, AddressOf servicioGridLookUpEditDetalleServicio_Leave
            AddHandler ServicioGridLookUpEdit.Leave, AddressOf servicioGridLookUpEditDetalleServicio_Leave

            Dim ColCodigoServicio As New GridColumn With {
                .FieldName = "IdAcuerdoDet",
                .Caption = "Codigo",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .ColumnEdit = ServicioGridLookUpEdit
            }

            ColCodigoServicio.Width = 200
            ServicioGridLookUpEdit.NullText = ""
            ColCodigoServicio.OptionsColumn.AllowEdit = True
            gvDetalleServicios.Columns.Add(ColCodigoServicio)
            VisibleIndex += 1

#End Region

#Region "Columna - Nombre_Servicio"

            Dim ColNombreServicio As New GridColumn With {
                .FieldName = "nombre_servicio",
                .Caption = "Servicio",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .Width = 300
            }

            ColNombreServicio.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColNombreServicio)
            VisibleIndex += 1

#End Region

#Region "Columna - Cantidad"

            Dim ColCantidad As New GridColumn With {
                .FieldName = "cantidad",
                .Caption = "Cantidad",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = VisibleIndex
            }

            ColCantidad.OptionsColumn.AllowEdit = True
            ColCantidad.DisplayFormat.FormatType = FormatType.Numeric
            ColCantidad.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleServicios.Columns.Add(ColCantidad)

            gvDetalleServicios.Columns("cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleServicios.Columns("cantidad").SummaryItem.DisplayFormat = "{0:n6}"
            gvDetalleServicios.Columns("cantidad").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleServicios.Columns("cantidad").DisplayFormat.FormatString = "{0:n6}"
            VisibleIndex += 1

#End Region

#Region "Columna - IdServicioDet"


            Dim ColIdServicioDet As New GridColumn With {
                .FieldName = "IdServicioDet",
                .Caption = "IdServicioDet",
                .Visible = False,
                .VisibleIndex = VisibleIndex,
                .Width = 100
            }

            ColIdServicioDet.Visible = False
            ColIdServicioDet.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColIdServicioDet)
            VisibleIndex += 1

#End Region

            gvDetalleServicios.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub servicioGridLookUpEditDetalleServicio_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaRequisicion As DataRow = gvDetalleServicios.GetFocusedDataRow()
            If drLineaRequisicion Is Nothing Then Return

            Dim UnaOvejota As Object = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView))

            If Not UnaOvejota Is Nothing Then

                Dim drArticulo As DataRow = UnaOvejota.Row
                If drArticulo Is Nothing Then Return

                Dim duplicados As Integer = (
                        From q In DTGridDetalleServicios.Rows
                        Where (q("IdAcuerdoDet").ToString() = drArticulo("IdAcuerdoDet").ToString() And q("IdPropietarioBodega") = drLineaRequisicion("IdPropietarioBodega"))
                        Select q).Count()

                If duplicados > 0 Then

                    XtraMessageBox.Show("Servicio duplicado al mismo propietario!",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation)

                Else

                    drLineaRequisicion("nombre_servicio") = drArticulo("nombre_servicio")
                    drLineaRequisicion("IdServicioDet") = 0
                    'drLineaRequisicion("nombre_unidad") = drArticulo("nombre_unidad")
                    'GT 06092021: Se omite el focus, el grid automaticamente lo hace
                    'gvDetalleServicios.FocusedColumn = gvDetalleServicios.Columns("Cantidad")
                    gvDetalleServicios.PostEditor()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private pObjOC As New clsBeTrans_oc_enc
    Private pListObjOrdeCompraDet As New List(Of clsBeTrans_oc_det)

    Private Sub lnk_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkDocumentoIngreso.LinkClicked
        Get_Orden_Compra_lnk()
    End Sub

    Private Sub Get_Orden_Compra_lnk()

        Dim tem_IdOrdencompra = txtIdOrdenCompra.Text
        Dim tem_OC = txtNombreDocIngreso.Text


        txtIdOrdenCompra.Text = String.Empty
        txtNombreDocIngreso.Text = String.Empty

        gBeOrdenCompra = New clsBeTrans_oc_enc()

        Try

            Dim OC As New frmOrdenCompra_List() With {
                .pIdBodega = cmbBodega.EditValue,
                .pIdPropietario = cmbPropietario.Tag,
                .Modo = frmOrdenCompra_List.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                OC.OpcionesMenu = OpcionesMenu
                OC.mnuActualizar.Enabled = OpcionesMenu.Leer
                OC.mnuNuevo.Enabled = OpcionesMenu.Modificar
                OC.mnuNuevoIngresoConsolidados.Enabled = OpcionesMenu.Modificar
            End If

            If OC.ShowDialog() = DialogResult.OK Then

                gBeOrdenCompra = OC.gBeOrdenCompra

                'Dim gBeServicioEnc = New clsBeTrans_servicio_enc()

                'GT 02072021 validar si la OC ya tiene registro previo de servicios con estado Activo/Enviado a ERP.
                'GT 03092021 valido tipo_doc_servicio para saber si consulto una OC o un PE de la lista de servicios ya guardados

                gBeServicioEnc = New clsBeTrans_servicio_enc()
                gBeServicioEnc = clsLnTrans_servicio_enc.GetSingle_By_OC(gBeOrdenCompra.IdOrdenCompraEnc, tipo_doc_ingreso)

                If gBeServicioEnc IsNot Nothing AndAlso gBeServicioEnc.Activo OrElse gBeServicioEnc.Enviado_a_erp Then

                    XtraMessageBox.Show("El número de documento de ingreso ya tiene servicios asociados!",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)

                Else

                    If gBeOrdenCompra IsNot Nothing AndAlso gBeOrdenCompra.IdOrdenCompraEnc > 0 Then

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                        ValidaOC(gBeOrdenCompra.IdOrdenCompraEnc)

                        If gBeOrdenCompra.EsDevolucion Then
                            SplashScreenManager.Default.SetWaitFormCaption("Devolución")
                        Else
                            SplashScreenManager.Default.SetWaitFormCaption("Documento de Ingreso")
                        End If

                        pObjOC = clsLnTrans_oc_enc.Get_Orden_Compra(gBeOrdenCompra.IdOrdenCompraEnc)
                        txtIdOrdenCompra.Text = pObjOC.IdOrdenCompraEnc

                        Cargar_Info_DI()

                        cmbBodega.EditValue = pObjOC.IdBodega
                        cmbPropietario.EditValue = pObjOC.PropietarioBodega.IdPropietarioBodega
                        cmbBodega.Enabled = False
                        cmbPropietario.Enabled = False

                        IngresoConsolidado = IIf(gBeOrdenCompra.TipoIngreso.Es_Poliza_Consolidada,
                                             eTipoTrans.Consolidado,
                                             eTipoTrans.Simple)

                        Llena_Propietarios_Grid()

                    Else
                        cmbBodega.Enabled = True
                        cmbPropietario.Enabled = True
                    End If

                    SplashScreenManager.CloseForm(False)

                    XtraMessageBox.Show("Se cargó el documento correctamente",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                End If

            Else
                'GT 02062021 Si se carga la modal, pero no se selecciona nada, se reestablecen los valores orginales.
                txtIdOrdenCompra.Text = tem_IdOrdencompra
                txtNombreDocIngreso.Text = tem_OC

            End If

            OC.Close()
            OC.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try


    End Sub

    Private Sub Llena_Propietarios_Grid()

        Try

            If (IngresoConsolidado = eTipoTrans.Consolidado) Then
                PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdBodega_And_IdDocumentoIngreso_For_Combo(cmbBodega.EditValue, gBeOrdenCompra.IdOrdenCompraEnc)
            Else

                If Modo = ModoTrans.Editar Then
                    If TipoTransaccion = pTipoTransaccion.Ingreso Then
                        PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdPropietarioBodega_For_Combo(gBeOrdenCompra.IdPropietarioBodega)
                    Else
                        PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdPropietarioBodega_For_Combo(gBePedidoEnc.IdPropietarioBodega)
                    End If
                Else
                    If Not cmbPropietario.EditValue Is Nothing Then
                        PropietarioGridServiciosLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdPropietarioBodega_For_Combo(cmbPropietario.EditValue)
                    End If
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private ListPC As New List(Of clsProductoCargar)
    Public OpcionesMenu As clsBeOpcionesMenuRol

    Private Sub ValidaOC(ByVal pIdOrdenCompra As Integer)

        Try

            ListPC = New List(Of clsProductoCargar)

            Dim ListaOC As List(Of clsBeTrans_oc_det) = clsLnTrans_oc_det.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompra).ToList
            Dim ListaR As List(Of clsBeTrans_re_det) = clsLnTrans_re_det.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompra).ToList

            If ListaOC IsNot Nothing AndAlso ListaR IsNot Nothing Then

                For Each bo As clsBeTrans_oc_det In ListaOC

                    For Each boR As clsBeTrans_re_det In ListaR

                        If bo.No_Linea = boR.No_Linea Then

                            If bo.Cantidad_recibida < boR.cantidad_recibida Then

                                '#EJC20180113: No_Linea agregado en clsProductoCargar
                                Dim ObjPC As New clsProductoCargar() With
                                    {.IdProductoBodega = bo.IdProductoBodega,
                                    .CantidadACargar = bo.Cantidad_recibida - boR.cantidad_recibida,
                                    .No_Linea = bo.No_Linea}

                                ListPC.Add(ObjPC)

                            End If

                        End If

                    Next

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Function Process_Servicios() As Boolean

        Process_Servicios = False

        Try

            If gvDetalleServicios.DataRowCount > 0 Then

                Dim servicio As New clsBeTrans_servicio_det()
                Dim ServicioExistente As New clsBeTrans_servicio_det()
                Dim BeAcuerdoDet As New clsBeI_nav_acuerdo_det()
                Dim vCodigoServicio As String = ""
                Dim vIdPropietarioBodega As Integer = 0
                Dim vIdOrdenCompraServicio As Integer = 0
                Dim vCantidad As Double = 0
                Dim vNombreServicio As String = ""
                Dim vIdServicioDet As Integer = 0


                For i As Integer = 0 To gvDetalleServicios.DataRowCount - 1

                    vCodigoServicio = IIf(IsDBNull(gvDetalleServicios.GetRowCellValue(i, "IdAcuerdoDet")), 0, gvDetalleServicios.GetRowCellValue(i, "IdAcuerdoDet"))
                    vCantidad = IIf(IsDBNull(gvDetalleServicios.GetRowCellValue(i, "cantidad")), 0, gvDetalleServicios.GetRowCellValue(i, "cantidad"))
                    vNombreServicio = IIf(IsDBNull(gvDetalleServicios.GetRowCellValue(i, "nombre_servicio")), 0, gvDetalleServicios.GetRowCellValue(i, "nombre_servicio"))
                    vIdPropietarioBodega = IIf(IsDBNull(gvDetalleServicios.GetRowCellValue(i, "IdPropietarioBodega")), 0, gvDetalleServicios.GetRowCellValue(i, "IdPropietarioBodega"))
                    vIdServicioDet = IIf(IsDBNull(gvDetalleServicios.GetRowCellValue(i, "IdServicioDet")), 0, gvDetalleServicios.GetRowCellValue(i, "IdServicioDet"))

                    'GT 29062021 se obtiene el IdPropietario, el IdBodegaPropietario no se almacena
                    Dim IdPropietario = clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue, vIdPropietarioBodega)

                    'GT 29062021 se omite consulta porque la descripción del servicio ya se obtiene del grid
                    'BeAcuerdoDet = clsLnI_nav_acuerdo_det.Get_Single_By_IdAcuerdoDet_And_IdAcuerdoEnc(vIdAcuerdoDet, 1)

                    'GT 29062021 se omite consulta porque la descripción del servicio ya se obtiene del grid
                    ServicioExistente = clsLnTrans_servicio_det.GetSingle(gBeServicio.IdServicioEnc, vIdServicioDet)

                    If ServicioExistente Is Nothing Then '#EJC20210420: El servicio no existe
                        servicio = New clsBeTrans_servicio_det()
                        servicio.IdPropietario = IdPropietario
                        servicio.Cantidad = vCantidad
                        servicio.Codigo_producto = vCodigoServicio
                        servicio.Nombre_servicio = vNombreServicio.Trim()
                        servicio.User_agr = AP.UsuarioAp.IdUsuario
                        servicio.User_mod = AP.UsuarioAp.IdUsuario
                        servicio.Fec_mod = Now
                        servicio.IsNew = True
                        lTransServiciosDet.Add(servicio)
                    Else
                        If ServicioExistente.Cantidad <> vCantidad Then
                            ServicioExistente.Cantidad = vCantidad
                            ServicioExistente.User_mod = AP.UsuarioAp.IdUsuario
                            ServicioExistente.Fec_mod = Now
                            lTransServiciosDet.Add(ServicioExistente)
                        End If
                    End If

                Next

                Process_Servicios = (lTransServiciosDet.Count > 0)

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub Listar_Propietarios()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(cmbBodega.EditValue)
            cmbPropietario.Properties.DataSource = DT1
            cmbPropietario.Properties.ValueMember = "IdPropietarioBodega"
            cmbPropietario.Properties.DisplayMember = "Nombre"
            cmbPropietario.Properties.PopupWidth = 700
            cmbPropietario.Properties.BestFit()
            cmbPropietario.Properties.PopulateColumns()

            If cmbPropietario.Properties.Columns.Count > 0 Then
                cmbPropietario.Properties.Columns(0).Visible = False
                cmbPropietario.Properties.Columns(1).Visible = False
            End If

            cmbPropietario.Properties.NullText = ""

            If Not DT1 Is Nothing Then
                If DT1.Rows.Count = 1 Then
                    cmbPropietario.Text = DT1.Rows(0).Item("Nombre").ToString
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

    Private Sub Llena_Servicios_By_Acuerdo()

        Try

            ServicioGridLookUpEdit.DataSource = clsLnI_nav_acuerdo_enc.Get_All_For_Grid(BeConfigBodega.IdAcuerdoEnc)
            '#EJC20210629: Ya se configuro en el set columnas.
            ServicioGridLookUpEdit.ValueMember = "IdAcuerdoDet"
            ServicioGridLookUpEdit.DisplayMember = "IdAcuerdoDet"
            ServicioGridLookUpEdit.NullText = ""
            ServicioGridLookUpEdit.View.BestFitColumns()
            ServicioGridLookUpEdit.PopupFormWidth = 700

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub PropietarioServicioGridLookUpEditDetalleIngreso_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)

            If lista.EditValue Is Nothing Then

                If cmbPropietario.EditValue Is Nothing Then
                    Return
                Else
                    gvDetalleServicios.SetFocusedRowCellValue(gvDetalleServicios.Columns("IdPropietarioBodega"), cmbPropietario.EditValue)
                End If

            End If

            Dim drLineaRequisicion As DataRow = gvDetalleServicios.GetFocusedDataRow()
            If drLineaRequisicion Is Nothing Then Return

            Dim vObjProp As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vObjProp Is Nothing Then

                Dim drPropietario As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drPropietario Is Nothing Then Return
                Dim vIdPropietario As Integer = 0
                vIdPropietario = drPropietario("IdPropietarioBodega")
                Llena_Servicios_By_Acuerdo()
                gvDetalleServicios.PostEditor()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuCerrar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCerrar.ItemClick

        Try

            If XtraMessageBox.Show("¿Cerrar el documento de servicios?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Cerrar_Documento() Then

                    XtraMessageBox.Show("Se actualizó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Close()

                    If Not InvokeListarServicios Is Nothing Then InvokeListarServicios.Invoke()

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmRegServicio_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Set_Datata_Table_Grid_Detalle_Servicios()
            Set_Columnas_Grid_Detalle_Servicios()

            dgridServiciosAsociados.DataSource = DTGridDetalleServicios

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            Listar_Propietarios()

            BeConfigBodega = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(cmbBodega.EditValue, AP.IdEmpresa)

            '#EJC20210809: Tipificación para flujo de transacción (ingreso o salida)
            Select Case TipoTransaccion

                Case pTipoTransaccion.Ingreso

                    Me.Text = "Registro de servicios de ingreso"
                    chkEsIngreso.Checked = True

                    lnkDocumentoIngreso.Visible = True
                    txtIdOrdenCompra.Visible = True
                    txtNombreDocIngreso.Visible = True

                    lnkPedidoCliente.Visible = False
                    txtIdPedidoEnc.Visible = False
                    txtNomPedidoEnc.Visible = False

                    tipo_doc_ingreso = 1

                Case pTipoTransaccion.Salida

                    Me.Text = "Registro de servicios de salida"
                    chkEsIngreso.Checked = False

                    lnkPedidoCliente.Visible = True
                    txtIdPedidoEnc.Visible = True
                    txtNomPedidoEnc.Visible = True

                    lnkDocumentoIngreso.Visible = False
                    txtIdOrdenCompra.Visible = False
                    txtNombreDocIngreso.Visible = False

                    lnkPedidoCliente.Location = lnkDocumentoIngreso.Location
                    txtIdPedidoEnc.Location = txtIdOrdenCompra.Location
                    txtNomPedidoEnc.Location = txtNombreDocIngreso.Location

                    tipo_doc_ingreso = 0

            End Select

            Select Case Modo

                Case ModoTrans.Nuevo

                    lblIdServicioEnc.Text = clsLnArancel.MaxID()
                    lblEstado.Text = "Nuevo"
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now
                    mnuGuardar.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False
                    mnuCerrar.Enabled = False

                    dtpFechaDocumento.EditValue = Date.Now

                Case ModoTrans.Editar


                    '#EJC20210715A: Validar estado cerrado en registro de servicios.
                    If gBeServicio.Enviado_a_erp OrElse Not gBeServicio.Activo OrElse gBeServicio.Estado = "Cerrado" Then

                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = False
                        mnuEliminar.Enabled = False
                        mnuEstadoEnvioERP.Enabled = False
                        BarCheckItem1.Enabled = False
                        mnuCerrar.Enabled = False
                        mnuAsignacion.Enabled = False
                        txtIdOrdenCompra.Enabled = False
                        lnkDocumentoIngreso.Enabled = False
                        txtIdPedidoEnc.Enabled = False
                        lnkPedidoCliente.Enabled = False

                    Else

                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                        mnuEliminar.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Eliminar, True)
                        mnuAsignacion.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                        txtIdOrdenCompra.Enabled = False
                        lnkDocumentoIngreso.Enabled = False

                    End If

                    If TipoTransaccion = pTipoTransaccion.Ingreso Then

                        If gBeOrdenCompra IsNot Nothing Then
                            txtIdOrdenCompra.Text = gBeOrdenCompra.IdOrdenCompraEnc
                            Cargar_Info_DI()
                        End If

                        IngresoConsolidado = IIf(gBeOrdenCompra.TipoIngreso.Es_Poliza_Consolidada,
                                                  eTipoTrans.Consolidado,
                                                  eTipoTrans.Simple)
                        cmbPropietario.EditValue = gBeOrdenCompra.IdPropietarioBodega

                    Else

                        txtIdPedidoEnc.Text = gBePedidoEnc.IdPedidoEnc
                        Set_Info_Pedido_Cliente()
                        cmbPropietario.EditValue = gBePedidoEnc.IdPropietarioBodega

                    End If


                    lblIdServicioEnc.Text = gBeServicio.IdServicioEnc

                    If TipoTransaccion = pTipoTransaccion.Ingreso Then
                        dtpFechaDocumento.EditValue = gBeOrdenCompra.Fecha_Creacion
                    Else
                        dtpFechaDocumento.EditValue = gBePedidoEnc.Fecha_Pedido
                    End If

                    cmbBodega.EditValue = gBeServicio.IdBodega

                    lblEstado.Text = gBeServicio.Estado
                    dtpFechaDocumento.EditValue = gBeServicio.Fecha_doc_ingreso
                    mnuCerrar.Enabled = (gBeServicio.Estado = "Nuevo")

                    Cargar_Servicios_Registrados(gBeServicio.IdServicioEnc, gBeServicio.IdBodega)


            End Select

            If TipoTransaccion = pTipoTransaccion.Ingreso Then
                txtIdOrdenCompra.Focus()
            Else
                txtIdPedidoEnc.Focus()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)


            IsLoading = False
            '#EJC20210428: Si se realizó una interrupción para cerrar mientras se estaba cargando, cerrar la forma al final.
            If IsClosing Then Close()

        End Try

    End Sub

    Private Sub lnkPedidoCliente_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkPedidoCliente.LinkClicked
        Get_Pedido_Cliente_lnk()
    End Sub

    Private Sub Get_Pedido_Cliente_lnk()

        Dim tem_IdOrdencompra = txtIdPedidoEnc.Text
        Dim tem_OC = txtNomPedidoEnc.Text

        txtIdPedidoEnc.Text = String.Empty
        txtNomPedidoEnc.Text = String.Empty

        gBePedidoEnc = New clsBeTrans_pe_enc()

        Try

            'Dim bo As New frmPedidoDetalleBuscador() With {.Modo = frmPedidoDetalleBuscador.ProcesoSolicitante.Despacho,
            '.IdBodega = cmbBodega.EditValue}

            'GT 03092021 1120: lista de pedidos es más practico que frmPedidoDetalleBuscador
            Dim bo As New frmPedido_List() With {.Modo = frmPedido_List.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                bo.OpcionesMenu = OpcionesMenu
                bo.mnuActualizar.Enabled = OpcionesMenu.Leer
                bo.mnuNuevo.Enabled = OpcionesMenu.Modificar
            End If

            If bo.ShowDialog() = DialogResult.OK Then

                gBePedidoEnc = bo.pBePedidoEnc

                'GT 02072021 validar si la OC o el PE tiene registro previo de servicios con estado Activo/Enviado a ERP.
                'GT 03092021 1124: parametro tipo_doc_ingreso define si busca Ingresos o Salidas en el query
                gBeServicioEnc = New clsBeTrans_servicio_enc()
                gBeServicioEnc = clsLnTrans_servicio_enc.GetSingle_By_OC(gBePedidoEnc.IdPedidoEnc, tipo_doc_ingreso)

                If gBeServicioEnc IsNot Nothing AndAlso gBeServicioEnc.Activo OrElse gBeServicioEnc.Enviado_a_erp Then

                    XtraMessageBox.Show("El número de documento de salida ya tiene servicios asociados!",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation)

                Else

                    If gBePedidoEnc IsNot Nothing AndAlso gBePedidoEnc.IdPedidoEnc > 0 Then

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Pedido")

                        Application.DoEvents()

                        txtIdPedidoEnc.Text = gBePedidoEnc.IdPedidoEnc
                        cmbPropietario.EditValue = gBePedidoEnc.IdPropietarioBodega
                        dtpFechaDocumento.EditValue = gBePedidoEnc.Fecha_Pedido

                        'cmbPropietario.EditValue = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(gBePedidoEnc.IdBodega,
                        'gBePedidoEnc.IdPropietarioBodega)

                        txtNomPedidoEnc.Text = String.Format("Ref: {0} - Doc: {1}", gBePedidoEnc.Referencia, gBePedidoEnc.No_documento)

                        Llena_Propietarios_Grid()
                    Else

                        txtIdPedidoEnc.Text = String.Empty
                        txtNomPedidoEnc.Text = String.Empty

                    End If


                End If

            Else

                txtIdPedidoEnc.Text = tem_IdOrdencompra
                txtNomPedidoEnc.Text = tem_OC

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub


    Private Sub Set_Info_Pedido_Cliente()

        Try

            If gBePedidoEnc.Referencia.ToString() = "" Then
                txtNomPedidoEnc.Text = "Cliente: " & gBePedidoEnc.Cliente.Nombre_comercial.ToString() & " - " & gBePedidoEnc.No_documento.ToString()
            Else
                txtNomPedidoEnc.Text = "Cli.: " & gBePedidoEnc.Cliente.Nombre_comercial.ToString() & " - Doc.: " & gBePedidoEnc.No_documento.ToString() & " - Ref: " & gBePedidoEnc.Referencia.ToString()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub


    'Private Sub gvDetalleServicios_ShownEditor(sender As Object, e As EventArgs) Handles gvDetalleServicios.ShownEditor

    '    Try

    '        Dim view As ColumnView = DirectCast(sender, ColumnView)

    '        If view.FocusedColumn.FieldName = "IdProductoBodega" Then
    '            Dim editor As GridLookUpEdit = CType(view.ActiveEditor, GridLookUpEdit)
    '            Dim pIdPropietarioBodega As String = Convert.ToString(view.GetFocusedRowCellValue("IdPropietarioBodega"))
    '            If pIdPropietarioBodega.Trim = "" Then pIdPropietarioBodega = cmbPropietario.EditValue
    '            editor.Properties.DataSource = clsLnProducto.Get_Lista_For_Grid_By_IdPropietario_And_IdBodega(pIdPropietarioBodega, cmbBodega.EditValue)
    '        End If

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub

End Class
