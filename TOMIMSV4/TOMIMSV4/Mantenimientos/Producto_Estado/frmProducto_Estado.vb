Imports DevExpress.XtraEditors

Public Class frmProducto_Estado

    Public pObjPE As New clsBeProducto_estado
    Private pListObjT As New List(Of clsTabla)
    Private pListEstadoProductoUbicacion As New List(Of clsBeProducto_estado_ubic)
    Private ReadOnly pObjEstadoProductoUbicacion As New clsBeProducto_estado_ubic
    Public gBeProductoEstado As New clsBeProducto_estado
    Public Delegate Sub listarProductoEstado()
    Public Property InvokeListarProductoEstado As listarProductoEstado

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

    Private Sub frmProducto_Estado_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("producto_estado")

            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, AP.IdEmpresa)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnProducto_Estado.MaxID()
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now
                    mnuGuardar.Enabled = OpcionesMenu.Modificar
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False
                    cmbPropietario.Enabled = True

                Case TipoTrans.Editar

                    lblCodigo.Text = gBeProductoEstado.IdEstado
                    cmbPropietario.EditValue = gBeProductoEstado.IdPropietario
                    cmbPropietario.Enabled = False
                    chkSistema.Checked = gBeProductoEstado.Sistema

                    txtNombre.Text = gBeProductoEstado.Nombre
                    'txtNombreUbicación.Text = gBeProductoEstado.Descripcion

                    If (gBeProductoEstado.Dañado) Then
                        rdMalo.Checked = True
                    Else
                        rdBueno.Checked = True
                    End If

                    chkUtilizable.Checked = gBeProductoEstado.Utilizable
                    chkActivo.Checked = gBeProductoEstado.Activo
                    chkSistema.Checked = gBeProductoEstado.Sistema

                    User_agrTextEdit.Text = gBeProductoEstado.User_agr
                    Fec_agrDateEdit.Text = gBeProductoEstado.Fec_agr
                    User_modTextEdit.Text = gBeProductoEstado.User_mod
                    Fec_modDateEdit.Text = gBeProductoEstado.Fec_mod

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = OpcionesMenu.Modificar
                    mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    mnuAsignacion.Enabled = OpcionesMenu.Modificar
                    pObjPE.IdEstado = gBeProductoEstado.IdEstado
                    txtIdUbicacion.Text = gBeProductoEstado.IdUbicacionDefecto
                    txtCodigoBodegaERP.Text = gBeProductoEstado.Codigo_Bodega_ERP

                    nudDiasVencimientoClasificacion.Value = gBeProductoEstado.Dias_Vencimiento_Clasificacion
                    nudToleranciaDiasVencimiento.Value = gBeProductoEstado.Tolerancia_Dias_Vencimiento

                    chkReservaRequiereUMBas.EditValue = gBeProductoEstado.Reservar_En_UmBas

                    txtIdUbicacion_LostFocus(Nothing, Nothing)
                    Cargar_Estados_Ubicaciones(False)

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Focus()
        txtNombre.Focus()

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim BeProductoEstado As New clsBeProducto_estado() With {.IdPropietario = cmbPropietario.EditValue, .Nombre = txtNombre.Text}

            BeProductoEstado.Dañado = IIf(rdMalo.Checked, 1, 0)
            BeProductoEstado.Utilizable = chkUtilizable.Checked
            BeProductoEstado.Activo = chkActivo.Checked
            BeProductoEstado.IsNew = True
            BeProductoEstado.User_agr = AP.UsuarioAp.IdUsuario
            BeProductoEstado.Fec_agr = Now
            BeProductoEstado.User_mod = AP.UsuarioAp.IdUsuario
            BeProductoEstado.Fec_mod = Now
            BeProductoEstado.Sistema = chkSistema.Checked
            BeProductoEstado.Codigo_Bodega_ERP = txtCodigoBodegaERP.Text.Trim
            BeProductoEstado.Dias_Vencimiento_Clasificacion = nudDiasVencimientoClasificacion.Value
            BeProductoEstado.Tolerancia_Dias_Vencimiento = nudToleranciaDiasVencimiento.Value
            BeProductoEstado.Reservar_En_UmBas = chkReservaRequiereUMBas.EditValue
            clsLnProducto_estado.Insert_Producto_Estado_With_Ubic(BeProductoEstado, pListEstadoProductoUbicacion)

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                gBeProductoEstado.Nombre = txtNombre.Text.Trim()
                gBeProductoEstado.User_mod = AP.UsuarioAp.IdUsuario
                gBeProductoEstado.Fec_mod = Now
                gBeProductoEstado.Activo = chkActivo.Checked 'aaa

                gBeProductoEstado.Dañado = IIf(rdMalo.Checked, 1, 0)
                gBeProductoEstado.Utilizable = chkUtilizable.Checked
                gBeProductoEstado.Sistema = chkSistema.Checked

                gBeProductoEstado.Dias_Vencimiento_Clasificacion = nudDiasVencimientoClasificacion.Value
                gBeProductoEstado.Tolerancia_Dias_Vencimiento = nudToleranciaDiasVencimiento.Value

                gBeProductoEstado.Reservar_En_UmBas = chkReservaRequiereUMBas.EditValue

                If txtIdUbicacion.Text.Trim <> "" Then
                    gBeProductoEstado.IdUbicacionDefecto = txtIdUbicacion.Text
                Else
                    gBeProductoEstado.IdUbicacionDefecto = 0 '?
                End If

                gBeProductoEstado.Codigo_Bodega_ERP = txtCodigoBodegaERP.Text.Trim

                Actualizar = clsLnProducto_estado.Insert_Producto_Estado_With_Ubic(gBeProductoEstado, pListEstadoProductoUbicacion)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPropietario.Focus()
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function


    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        mnuGuardar.Enabled = False
        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar el Estado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If InvokeListarProductoEstado IsNot Nothing Then
                    InvokeListarProductoEstado.Invoke()
                End If
                Close()
            End If

        End If
        mnuGuardar.Enabled = True
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        mnuActualizar.Enabled = False
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarProductoEstado IsNot Nothing Then
                InvokeListarProductoEstado.Invoke()
            End If
            Close()
        End If
        mnuActualizar.Enabled = True
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False

            If pObjPE.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If MessageBox.Show("¿Desactivar el Estado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If clsLnProducto_estado.Eliminar(pObjPE) > 0 Then
                        If InvokeListarProductoEstado IsNot Nothing Then
                            InvokeListarProductoEstado.Invoke()
                        End If
                        Close()
                        frmProducto_EstadoList.Dgrid.Refresh()
                    End If
                End If
            End If

            mnuEliminar.Enabled = True
        Catch ex As Exception
            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("producto_estado", pObjPE.IdEstado)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub lnkClasificacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkClasificacion.LinkClicked

        Try

            Dim Ubicacion As New frmBodegaT() With {.Modo = frmBodegaT.pModo.Seleccion}
            Ubicacion.pObjBeB.IdBodega = AP.IdBodega
            Ubicacion.ShowDialog()

            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion Then
                txtIdUbicacion.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicación.Text = Ubicacion.pObj.Descripcion
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdUbicacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdUbicacion.KeyPress
        Try
            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If
            If e.KeyChar = "." Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdUbicacion.Text.Length = 1 Then
                txtNombreUbicación.Text = String.Empty
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtIdUbicacion_LostFocus(sender As Object, e As EventArgs) Handles txtIdUbicacion.LostFocus

        Try

            If String.IsNullOrEmpty(txtIdUbicacion.Text.Trim()) = False Then

                Dim Obj As New clsBeBodega_ubicacion

                Obj = clsLnBodega_ubicacion.GetSingle(txtIdUbicacion.Text.Trim(), AP.IdBodega)

                If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then
                    txtIdUbicacion.Text = Obj.IdUbicacion
                    txtNombreUbicación.Text = Obj.Descripcion
                Else
                    'XtraMessageBox.Show(String.Format("No existe Ubicación con código {0}", txtIdUbicacion.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdUbicacion.Text = String.Empty
                    txtNombreUbicación.Text = String.Empty
                    txtIdUbicacion.Focus()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Estados_Ubicaciones(ByVal pGuardo As Boolean)

        Try

            If pGuardo = False And pObjPE.IdEstado > 0 Then
                pListEstadoProductoUbicacion = clsLnProducto_estado_ubic.Get_All_By_IdEstado(pObjPE.IdEstado, chkEstadoUbicActivo.Checked).ToList()
            End If

            Dim DT As New DataTable("EstadosUbicaciones")
            DT.Columns.Add("IdBodega", GetType(Integer))
            DT.Columns.Add("Bodega", GetType(String))
            DT.Columns.Add("Correlativo", GetType(Integer))
            DT.Columns.Add("Ubicación", GetType(String))

            Dgrid.DataSource = Nothing

            For Each Obj As clsBeProducto_estado_ubic In pListEstadoProductoUbicacion.OrderBy(Function(o) o.IdEstado)
                Dim lRow As DataRow = DT.NewRow
                lRow(0) = Obj.IdBodega
                lRow(1) = Obj.Bodega
                lRow(2) = Obj.IdProductoEstadUbic
                lRow(3) = Obj.Ubicacion
                DT.Rows.Add(lRow)
            Next

            Dgrid.DataSource = DT
            Dgrid.Refresh()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Limpiar()
        txtIdUbicacion.Text = String.Empty
        txtNombreUbicación.Text = String.Empty
        txtIdUbicacion.Focus()
        'txtNombre.Text = String.Empty
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click

        If String.IsNullOrEmpty(txtIdUbicacion.Text) Then
            XtraMessageBox.Show("Seleccione Ubicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtIdUbicacion.Focus()
            txtIdUbicacion.SelectAll()
            Return
        End If

        Try

            Dim pIndex As Integer = -1

            If pIndex > -1 Then

                pListEstadoProductoUbicacion(pIndex).IdUbicacionDefecto = CInt(txtIdUbicacion.Text)
                cmdAdd.Tag = Nothing

            Else

                Dim ObjEstadoProductoUbicaion As New clsBeProducto_estado_ubic

                If pListEstadoProductoUbicacion IsNot Nothing AndAlso pListEstadoProductoUbicacion.Count > 0 Then
                    Dim find As Integer = -1
                    find = pListEstadoProductoUbicacion.FindIndex(Function(b) b.IdUbicacionDefecto = CInt(txtIdUbicacion.Text) And
                                                                              b.IdBodega = AP.IdBodega)
                    If find > -1 Then
                        XtraMessageBox.Show("La Ubicación ya existe para este estado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Limpiar()
                        Return
                    End If
                End If

                If pListEstadoProductoUbicacion IsNot Nothing AndAlso pListEstadoProductoUbicacion.Count > 0 Then
                    ObjEstadoProductoUbicaion.IdProductoEstadUbic = pListEstadoProductoUbicacion.Max(Function(b) b.IdProductoEstadUbic) + 1
                Else
                    ObjEstadoProductoUbicaion.IdProductoEstadUbic = 1
                End If

                'ObjEstadoProductoUbicaion.IdEstado = pObjPE.IdEstado
                ObjEstadoProductoUbicaion.IdBodega = AP.IdBodega
                ObjEstadoProductoUbicaion.Bodega = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(AP.IdBodega)
                ObjEstadoProductoUbicaion.IdUbicacionDefecto = CInt(txtIdUbicacion.Text)
                ObjEstadoProductoUbicaion.Fec_agr = Now
                ObjEstadoProductoUbicaion.User_agr = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                ObjEstadoProductoUbicaion.Fec_mod = Now
                ObjEstadoProductoUbicaion.User_mod = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                ObjEstadoProductoUbicaion.Activo = True

                'ObjEstadoProductoUbicaion.Estado = txtNombre.Text
                ObjEstadoProductoUbicaion.Ubicacion = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(txtIdUbicacion.Text, AP.IdBodega)
                ObjEstadoProductoUbicaion.IsNew = True

                pListEstadoProductoUbicacion.Add(ObjEstadoProductoUbicaion)

            End If

            Cargar_Estados_Ubicaciones(True)
            Limpiar()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick

        Try

            If GridView1.RowCount > 0 Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                Dim lIndex As Integer = -1

                If Dr.Item("IdBodega") = AP.IdBodega Then

                    lIndex = pListEstadoProductoUbicacion.FindIndex(Function(b) b.IdProductoEstadUbic = CInt(Dr.Item("Correlativo")))

                    If lIndex > -1 Then
                        cmdAdd.Tag = pListEstadoProductoUbicacion(lIndex).IdProductoEstadUbic
                        txtIdUbicacion.Text = pListEstadoProductoUbicacion(lIndex).IdUbicacionDefecto
                        txtNombreUbicación.Text = pListEstadoProductoUbicacion(lIndex).Ubicacion
                    End If
                Else
                    XtraMessageBox.Show(String.Format("No se puede editar el estado: {0} porque la ubicación no pertence a la bodega: {1}", Dr.Item("Correlativo"), AP.IdBodega), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click

        Try

            If MessageBox.Show("¿Desactivar la Ubicación?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjEstadoProductoUbicacion.IdProductoEstadUbic = CInt(cmdAdd.Tag)
                Dim lIndex As Integer
                If pObjEstadoProductoUbicacion.IdProductoEstadUbic > 0 Then
                    lIndex = pListEstadoProductoUbicacion.FindIndex(Function(ByVal b) b.IdProductoEstadUbic = pObjEstadoProductoUbicacion.IdProductoEstadUbic)
                Else
                    lIndex = -1
                End If

                If lIndex > -1 Then
                    clsLnProducto_estado.Eliminar_Producto_Estado_By_IdProductoEstadUbic(pObjEstadoProductoUbicacion.IdProductoEstadUbic)
                    pListEstadoProductoUbicacion.RemoveAt(lIndex)
                    Cargar_Estados_Ubicaciones(False)
                    Limpiar()
                End If

            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
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

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdUbicacion_Validated(sender As Object, e As EventArgs) Handles txtIdUbicacion.Validated

        Try

            If String.IsNullOrEmpty(txtIdUbicacion.Text.Trim()) = False AndAlso txtIdUbicacion.Text > 0 Then

                Dim Obj As New clsBeBodega_ubicacion
                Obj = clsLnBodega_ubicacion.GetSingle(txtIdUbicacion.Text.Trim(), AP.IdBodega)

                If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then
                    txtIdUbicacion.Text = Obj.IdUbicacion
                    txtNombreUbicación.Text = Obj.Descripcion
                Else
                    XtraMessageBox.Show(String.Format("No existe ubicación con código {0}", txtIdUbicacion.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdUbicacion.Focus()
                    txtIdUbicacion.SelectAll()

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub hideContainerBottom_Click(sender As Object, e As EventArgs) Handles hideContainerBottom.Click

    End Sub

    Private Sub frmProducto_Estado_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class