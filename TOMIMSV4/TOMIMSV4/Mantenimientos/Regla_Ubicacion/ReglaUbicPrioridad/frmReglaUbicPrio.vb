Imports System.ComponentModel
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmReglaUbicPrio

    Private pListObjT As New List(Of clsTabla)
    Public BeReglaUbicPrioEnc As New clsBeRegla_ubic_prio_enc
    Public pListBeReglaUbicProd As New List(Of clsBeRegla_ubic_prio_producto)
    Public Delegate Sub ListarReglas()
    Public Property InvokeListarReglasPrio As ListarReglas

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

    Public Sub New()
        InitializeComponent()
    End Sub

    Public pListBEReglaUbicPrioParamSeleccion As New List(Of clsBeRegla_ubic_prio_param_SelectionList)

    Private Sub Get_Listas()

        Try

            pListBEReglaUbicPrioParamSeleccion = clsLnRegla_ubic_prio_param.GetAllForSelection()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Actualizar_Grid_Detalle_Param_Prio()

        DgridDet.DataSource = pListBEReglaUbicPrioParamSeleccion
        grdvDet.LayoutChanged()

        Dim gridView As GridView = DgridDet.FocusedView
        gridView.BeginSort()

        Try

            gridView.ClearSorting()
            gridView.Columns("Orden").SortOrder = ColumnSortOrder.Ascending
            gridView.Columns("Activo").Visible = False
            gridView.Columns("Tipo").Visible = False
            gridView.Columns(1).Visible = False

            Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Count, "Seleccionar", "Count={0}")

            If gridView.Columns("Seleccionar").Summary.Count = 0 Then
                gridView.Columns("Seleccionar").Summary.Add(item)
            End If

        Finally
            gridView.EndSort()
        End Try

    End Sub

    Private Sub frmTipoTarima_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Get_Listas()

            Actualizar_Grid_Detalle_Param_Prio()

            pListObjT = clsBD.GetLongitudByTabla("regla_ubic_prio_enc")

            Inicializa_Lista_Default()

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnRegla_ubic_prio_enc.MaxID() + 1

                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                    BeReglaUbicPrioEnc.IsNew = True

                Case TipoTrans.Editar

                    clsLnRegla_ubic_prio_enc.Get_Single_With_Details(BeReglaUbicPrioEnc)

                    lblCodigo.Text = BeReglaUbicPrioEnc.IdReglaUbicPrioEnc
                    txtNombre.Text = BeReglaUbicPrioEnc.Nombre
                    chkActivo.Checked = BeReglaUbicPrioEnc.Activo

                    User_agrTextEdit.Text = BeReglaUbicPrioEnc.User_agr
                    Fec_agrDateEdit.Text = BeReglaUbicPrioEnc.Fec_agr
                    User_modTextEdit.Text = BeReglaUbicPrioEnc.User_mod
                    Fec_modDateEdit.Text = BeReglaUbicPrioEnc.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    If (BeReglaUbicPrioEnc.IdReglaUbicPrioEnc <> 1) Then mnuEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Else mnuEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                    Listar_Parametros_Prioridades()

                    Listar_Productos()

            End Select

            Dim ritem As RepositoryItemCheckEdit = TryCast(grdvDet.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritemDet_CheckedChanged

            Dim OrdenItem As RepositoryItemSpinEdit = TryCast(grdvDet.Columns("Orden").RealColumnEdit, RepositoryItemSpinEdit)
            AddHandler OrdenItem.Validating, AddressOf Orden_Properties_Validating

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        Focus()
        txtNombre.Focus()

    End Sub

    Private Sub Listar_Productos()

        Try

            pListBeReglaUbicProd = clsLnRegla_ubic_prio_producto.Get_All_By_IdRegla_Ubic_Prio_Enc(BeReglaUbicPrioEnc.IdReglaUbicPrioEnc, True)

            If Not pListBeReglaUbicProd Is Nothing

                Dim DT As New DataTable("Productos")
                DT.Columns.Add("Asignación", GetType(Integer))
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Nombre", GetType(String))
                DT.Columns.Add("Barra", GetType(String))

                For Each Obj As clsBeRegla_ubic_prio_producto In pListBeReglaUbicProd
                    DT.Rows.Add(Obj.IdReglaUbicPrioProd, Obj.Producto.Propietario.Nombre_comercial, Obj.Producto.Codigo, Obj.Producto.Nombre, Obj.Producto.Codigo_barra)
                Next

                DgridProd.DataSource = DT

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Inicializa_Lista_Default()

        Dim BeParamPrio As clsBeRegla_ubic_prio_det

        Try

            For Each Prio As clsBeRegla_ubic_prio_param_SelectionList In pListBEReglaUbicPrioParamSeleccion
                BeParamPrio = New clsBeRegla_ubic_prio_det With {.IdReglaUbicPrioParam = Prio.IdReglaUbicPrioParam, .Activo = False, .IsNew = True, .Orden = Prio.Orden}
                BeReglaUbicPrioEnc.lReglaUbicPrioDet.Add(BeParamPrio)
            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Listar_Parametros_Prioridades()

        Try

            If Not BeReglaUbicPrioEnc.lReglaUbicPrioDet Is Nothing

                Dim lindex As Integer = -1
                Dim lindex1 As Integer = -1

                For Each Prio As clsBeRegla_ubic_prio_det In BeReglaUbicPrioEnc.lReglaUbicPrioDet

                    lindex = BeReglaUbicPrioEnc.lReglaUbicPrioDet.FindIndex(Function(x) x.IdReglaUbicPrioParam = Prio.IdReglaUbicPrioParam)
                    lindex1 = pListBEReglaUbicPrioParamSeleccion.FindIndex(Function(x) x.IdReglaUbicPrioParam = Prio.IdReglaUbicPrioParam)

                    If lindex <> -1 AndAlso lindex1 <> -1 Then
                        pListBEReglaUbicPrioParamSeleccion(lindex1).Seleccionar = BeReglaUbicPrioEnc.lReglaUbicPrioDet(lindex).Activo
                    End If

                Next

                Actualizar_Grid_Detalle_Param_Prio()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Private Sub ritemDet_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As New clsBeRegla_ubic_prio_param_SelectionList
                Dr = grdvDet.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = BeReglaUbicPrioEnc.lReglaUbicPrioDet.FindIndex(Function(b) b.IdReglaUbicPrioParam = Dr.IdReglaUbicPrioParam)

                If lIndex > -1 Then

                    If ritem.Checked Then
                        BeReglaUbicPrioEnc.lReglaUbicPrioDet(lIndex).Activo = True
                    Else
                        BeReglaUbicPrioEnc.lReglaUbicPrioDet(lIndex).Activo = False
                    End If

                Else

                    Dim Obj As New clsBeRegla_ubic_prio_det() With {.IdReglaUbicPrioParam = Dr.IdReglaUbicPrioParam, .Activo = True, .IsNew = True, .Orden = Dr.Orden}
                    BeReglaUbicPrioEnc.lReglaUbicPrioDet.Add(Obj)

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

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            BeReglaUbicPrioEnc.IdReglaUbicPrioEnc = lblCodigo.Text
            BeReglaUbicPrioEnc.Nombre = txtNombre.Text.Trim()
            BeReglaUbicPrioEnc.IdEmpresa = AP.IdEmpresa
            BeReglaUbicPrioEnc.IdBodega = AP.IdBodega
            BeReglaUbicPrioEnc.Activo = True

            If Modo = TipoTrans.Nuevo
                BeReglaUbicPrioEnc.User_agr = AP.UsuarioAp.IdUsuario
                BeReglaUbicPrioEnc.Fec_agr = Now
            End If

            BeReglaUbicPrioEnc.User_mod = AP.UsuarioAp.IdUsuario
            BeReglaUbicPrioEnc.Fec_mod = Now

            Guardar = clsLnRegla_ubic_prio_enc.Guardar_Regla_Prioridad(BeReglaUbicPrioEnc)

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

            If Datos_Correctos() Then

                BeReglaUbicPrioEnc.Nombre = txtNombre.Text.Trim()
                BeReglaUbicPrioEnc.Activo = chkActivo.Checked
                BeReglaUbicPrioEnc.User_mod = AP.UsuarioAp.IdUsuario
                BeReglaUbicPrioEnc.Fec_mod = Now

                Actualizar = clsLnRegla_ubic_prio_enc.Guardar_Regla_Prioridad(BeReglaUbicPrioEnc)

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

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Datos_Correctos() Then
            If XtraMessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarReglasPrio IsNot Nothing Then
                        InvokeListarReglasPrio.Invoke()
                    End If
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarReglasPrio IsNot Nothing Then
                InvokeListarReglasPrio.Invoke()
            End If
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Desactivar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                BeReglaUbicPrioEnc.Activo = False
                If clsLnRegla_ubic_prio_enc.Eliminar(BeReglaUbicPrioEnc) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarReglasPrio IsNot Nothing Then
                        InvokeListarReglasPrio.Invoke()
                    End If
                    Close()
                    frmTipoTarima_List.Dgrid.Refresh()
                End If
            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("regla_ubic_sel_enc", BeReglaUbicPrioEnc.IdReglaUbicPrioEnc)
        End Try

    End Sub

#Region " Detail "

    Private Sub btnProd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnProd.ItemClick

        Try

            Dim Regla As New frmReglaUbicSelProd() With _
            {.Modo = frmReglaUbicSelProd.pModo.Lista, .IdReglaUbicPrioEnc = BeReglaUbicPrioEnc.IdReglaUbicPrioEnc}
            Regla.ShowDialog()
            Regla.Dispose()
            Listar_Productos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Orden_Properties_Validating(ByVal sender As Object, ByVal e As CancelEventArgs)

        Try

            Dim Dr As New clsBeRegla_ubic_prio_param_SelectionList
            Dr = grdvDet.GetFocusedRow

            If (TryCast(sender, SpinEdit)).IsModified Then

                Dim lIndex As Integer = -1

                lIndex = BeReglaUbicPrioEnc.lReglaUbicPrioDet.FindIndex(Function(b) b.IdReglaUbicPrioParam = Dr.IdReglaUbicPrioParam)

                If lIndex > -1 Then
                    BeReglaUbicPrioEnc.lReglaUbicPrioDet(lIndex).Orden = sender.value
                Else

                    'Actualiza el listado maestro
                    lIndex = pListBEReglaUbicPrioParamSeleccion.FindIndex(Function(x) x.IdReglaUbicPrioParam = Dr.IdReglaUbicPrioParam)

                    If lIndex <> -1 Then
                        pListBEReglaUbicPrioParamSeleccion(lIndex).Orden = sender.value
                    End If

                    clsLnRegla_ubic_prio_param.Actualizar(pListBEReglaUbicPrioParamSeleccion(lIndex))

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try


    End Sub

    Private Sub frmReglaUbicPrio_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

#End Region

End Class