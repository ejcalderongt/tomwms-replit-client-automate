Imports DevExpress.XtraEditors

Public Class frmTraslado_Producto

    Public pObjetoTraslado As New clsBeTrans_tras_enc
    Private pProducto As New clsBeProducto
    Dim DgComboPres As New DataGridViewComboBoxCell()
    Dim DgComboEstado As New DataGridViewComboBoxCell()
    Private pStock As New clsBeStock
    Private txtCodigoProductoGrid As TextBox
    Private LastEventHandlerPres As EventHandler = AddressOf Me.cmbPresentacion_SelectedIndexChanged
    Private LastEventHandlerEstado As EventHandler = AddressOf Me.cmbEstado_SelectedIndexChanged
    Private wcfProducto As New clsLnProducto

    Public gBeRoadRuta As New clsBeRoad_ruta

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

    Private Sub limpiarFomulario()
        IMS.Listar_Empresas(cmbEmpresa)
        cmbEmpresa.EditValue = AP.IdEmpresa
    End Sub

    Private Sub frmPropietario_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'vStock.Open()
        'wcfProducto.Open()
        'lnProducto.Open()


        Try

            limpiarFomulario()

            Select Case Modo
                Case TipoTrans.Nuevo
                    txtID.Text = clsLnTrans_tras_enc.MaxID()
                    User_agrTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    dtpFechaTraslado.DateTime = Today
                    dtpFechaEntrega.DateTime = Today

                Case TipoTrans.Editar
                    txtID.Text = pObjetoTraslado.IdTrasladoEnc
                    User_agrTextEdit.Text = pObjetoTraslado.User_agr
                    Fec_agrDateEdit.Text = pObjetoTraslado.Fec_agr
                    User_modTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_modDateEdit.Text = Now

                    cmbEmpresa.Enabled = False
                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    cmbEmpresa.Enabled = False
                    'CargaTrasladoProducto()
                    'ValidaBodegas()

            End Select

            Application.DoEvents()


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmbEmpresa_SelectedValueChanged(sender As Object, e As EventArgs)
        If cmbEmpresa.ItemIndex > 0 Then
            'cmbPropietario.DataSource = Nothing 'Borrar datos anterior en el campo
            'AP.Listar_PropietariosPorEmpresa(cmbPropietario, cmbEmpresa.SelectedValue)
            'If (cmbPropietario.Items.Count > 0) Then
            '    cmbPropietario.SelectedIndex = 0    'Seleccionar el primer item
            'End If
            cmbBodegaOrigen.Properties.DataSource = Nothing
            IMS.Listar_Bodegas_Por_Empresa(cmbBodegaOrigen, cmbEmpresa.EditValue)
            IMS.Listar_Bodegas_Por_Empresa(cmbBodegaDesti, AP.IdEmpresa)

            If (cmbBodegaOrigen.ItemIndex > 0) Then
                cmbBodegaOrigen.ItemIndex = 0
            End If
            If (cmbBodegaDesti.ItemIndex > 0) Then
                cmbBodegaDesti.ItemIndex = 0
            End If

        End If
    End Sub

    Private Sub cmbPropietario_SelectedValueChanged(sender As Object, e As EventArgs)
        If cmbPropietario.Text <> "" Then

            IMS.Listar_BodegasPorPropietario(cmbBodegaDesti, cmbPropietario.EditValue, cmbBodegaOrigen.EditValue)
            'cmbBodegaOrigen.DataSource = Nothing
            'cmbBodegaDestino.DataSource = Nothing
            'AP.Listar_BodegasPorPropietario(cmbBodegaOrigen, cmbPropietario.SelectedValue, -1)
        End If
    End Sub

    Private Sub cmbBodegaOrigen_SelectedValueChanged(sender As Object, e As EventArgs)
        If (cmbBodegaOrigen.ItemIndex > 0) Then

            cmbPropietario.Properties.DataSource = Nothing
            cmbMuelle.Properties.DataSource = Nothing
            Try
                'AP.Listar_PropietariosPorEmpresa(cmbBodegaOrigen, cmbEmpresa.SelectedValue)
                IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodegaOrigen.EditValue, False)
                If (cmbPropietario.Text <> "") Then
                    cmbPropietario.ItemIndex = 0
                End If

            Catch ex As Exception

            End Try

            Try
                IMS.Listar_Muelles(cmbMuelle, cmbBodegaOrigen.EditValue)
            Catch ex As Exception
                cmbMuelle.Properties.DataSource = Nothing
                ' MsgBox("La bodega " + cmbBodegaOrigen.ValueMember + "No tiene Muelle asignados")
            End Try

        End If
    End Sub

    Private Sub lnkidVehiculo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkidVehiculo.LinkClicked
        Try
            Dim frmVehiculo As New frmEmpresa_Transporte_VehiculoList
            frmVehiculo.Modo = frmEmpresa_Transporte_VehiculoList.pModo.Seleccion
            frmVehiculo.ShowDialog()

            Dim Dr As DataRowView = frmVehiculo.GridView1.GetFocusedRow
            If Dr.Item("Código") IsNot Nothing Then
                txtIdVehiculo.Text = Dr.Item("Código")
                txtNombreVehiculo.Text = Dr.Item("Placa")
            Else
                txtIdVehiculo.Text = ""
                txtNombreVehiculo.Text = ""
            End If

            frmVehiculo.Close()
            frmVehiculo.Dispose()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub lnkidPiloto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkidPiloto.LinkClicked
        Try
            Dim frmPiloto As New frmEmpresa_Transporte_PilotoList
            frmPiloto.Modo = frmEmpresa_Transporte_PilotoList.pModo.Seleccion
            frmPiloto.ShowDialog()

            Dim Dr As DataRowView = frmPiloto.GridView1.GetFocusedRow
            If Dr.Item("Código") IsNot Nothing Then
                txtIdPiloto.Text = Dr.Item("Código")
                txtNombrePiloto.Text = Dr.Item("Nombres") + " " + Dr.Item("Apellidos")
            Else
                txtIdPiloto.Text = ""
                txtNombrePiloto.Text = ""
            End If

            frmPiloto.Close()
            frmPiloto.Dispose()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub lnkidRuta_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkidRuta.LinkClicked
        Try
            Dim frmRuta As New frmListaRoadRuta
            frmRuta.Modo = frmListaRoadRuta.pModo.Seleccion
            frmRuta.ShowDialog()

            Dim Dr As DataRowView = frmRuta.GridView1.GetFocusedRow
            If Dr.Item("ID") IsNot Nothing Then
                txtIdRuta.Text = Dr.Item("ID")
                txtNombreRuta.Text = Dr.Item("Nombre")
            Else
                txtIdRuta.Text = ""
                txtNombreRuta.Text = ""
            End If

            frmRuta.Close()
            frmRuta.Dispose()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub dtpFechaTraslado_ValueChanged(sender As Object, e As EventArgs)
        dtpFechaEntrega.Properties.MinValue = dtpFechaTraslado.EditValue
    End Sub

    Private Sub dtpHoraInicialTraslado_ValueChanged(sender As Object, e As EventArgs) Handles dtpHoraInicialTraslado.ValueChanged
        dtpHoraFinalTraslado.MinDate = dtpHoraInicialTraslado.Value
        dtpHoraFinalTraslado.Value = dtpHoraInicialTraslado.Value
    End Sub

    Private Sub dtpHoraFinalTraslado_ValueChanged(sender As Object, e As EventArgs) Handles dtpHoraFinalTraslado.ValueChanged
        dtpHoraInicialEntrega.MinDate = dtpHoraFinalTraslado.Value
    End Sub

    Private Sub dtpHoraInicialEntrega_ValueChanged(sender As Object, e As EventArgs) Handles dtpHoraInicialEntrega.ValueChanged
        dtpHoraMaximaEntrega.MinDate = dtpHoraInicialEntrega.Value
    End Sub


    Private Sub lnkAgregarProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAgregarProducto.LinkClicked
        Try

            Dim Producto As New frmProductoList(True)
            Producto.cmdImportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Producto.chkActivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Producto.pIdBodega = cmbBodegaOrigen.EditValue
            Producto.pIdPropietarioBodega = cmbPropietario.EditValue
            Producto.Modo = frmProductoList.pModo.Seleccion
            Producto.WindowState = FormWindowState.Maximized
            Producto.ShowDialog()

            If Producto.pObjProducto IsNot Nothing AndAlso Producto.pObjProducto.IdProducto <> 0 Then
                setProducto(Producto.pObjProducto)
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

    Private Sub setProducto(ByVal ObjP As clsBeProducto)

        Try

            Dim i As Integer = dgrid.Rows.Count - 1

            If ObjP.IdProducto > 0 Then

                'If Not ExisteProducto(ObjP.IdProducto) Then



                'Else
                '    Throw New Exception("Producto ya existe en la lista.")
                'End If

                Dim IdProductoCell As DataGridViewCell = dgrid.Rows(i).Cells(dgrid.Columns("colIdProducto").Index)
                Dim CodProductoCell As DataGridViewCell = dgrid.Rows(i).Cells(dgrid.Columns("ColCodProducto").Index)
                Dim NomProductoCell As DataGridViewCell = dgrid.Rows(i).Cells(dgrid.Columns("ColNomProducto").Index)
                Dim UniMedCell As DataGridViewCell = dgrid.Rows(i).Cells(dgrid.Columns("colUnidadMedida").Index)
                'Dim colPresentacion As DataGridViewCell = dgrid.Rows(i).Cells(dgrid.Columns("colPresentacion").Index)
                'Dim colUnidMed As DataGridViewCell = dgrid.Rows(i).Cells(dgrid.Columns("colUnidadMedida").Index)
                'Dim colCantidad As DataGridViewCell = dgrid.Rows(i).Cells(dgrid.Columns("colCantidad").Index)
                Dim colPrecio As DataGridViewCell = dgrid.Rows(i).Cells(dgrid.Columns("colPrecio").Index)
                'Dim colTotal As DataGridViewCell = dgrid.Rows(i).Cells(dgrid.Columns("colTotal").Index)

                IdProductoCell.Value = ObjP.IdProducto
                CodProductoCell.Value = ObjP.Codigo
                NomProductoCell.Value = ObjP.Nombre
                colPrecio.Value = ObjP.Precio

                pProducto.Codigo = CodProductoCell.Value
                UniMedCell.Value = pProducto.UnidadMedida.Nombre
                colPrecio.Value = pProducto.Precio

                'pProducto = lnProducto.GetByCodigoAndBodega(pProducto.Codigo, cmbBodega.SelectedValue)

                LlenaPresentacionGrid(i)

                LlenaEstadosGrid(i)

                dgrid.Rows(i).Cells("ColIsNew").Value = True

            End If



        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub LlenaPresentacionGrid(ByVal pIndex As Integer, Optional ByVal pIdPresentacion As Integer = 0)

        Try

            DgComboPres = TryCast(dgrid.Rows(pIndex).Cells("colPresentacion"), DataGridViewComboBoxCell)

            DgComboPres.DropDownWidth = 200

            Dim lPres As New List(Of clsBeProducto_Presentacion)

            lPres = clsLnProducto_presentacion.Get_All_Stock_Con_Presentacion_By_IdProducto(pProducto.IdProducto).ToList()

            DgComboPres.DataSource = lPres
            DgComboPres.ValueMember = "IdPresentacion"
            DgComboPres.DisplayMember = "Nombre"

            If DgComboPres.Items.Count > 0 Then
                DgComboPres.Value = lPres(0).IdPresentacion
            End If

            If pIdPresentacion <> 0 Then
                DgComboPres.Value = pIdPresentacion
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub LlenaEstadosGrid(ByVal pIndex As Integer, Optional ByVal pIdEstado As Integer = 0)

        Try

            DgComboEstado = TryCast(dgrid.Rows(pIndex).Cells("colEstadoProducto"), DataGridViewComboBoxCell)

            DgComboEstado.DropDownWidth = 200

            Dim lEstado As New List(Of clsBeProducto_estado)
            lEstado = clsLnProducto_estado.Get_All_Stock_Con_Estado_By_IdProducto(pProducto.IdProducto).ToList()

            DgComboEstado.DataSource = lEstado
            DgComboEstado.ValueMember = "IdEstado"
            DgComboEstado.DisplayMember = "Nombre"

            If DgComboEstado.Items.Count > 0 Then
                DgComboEstado.Value = lEstado(0).IdEstado
            End If

            If pIdEstado <> 0 Then
                DgComboEstado.Value = pIdEstado
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub frmTraslado_Producto_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            lnkAgregarProducto_LinkClicked(Nothing, Nothing)
        End If
    End Sub


    Private Sub dgrid_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgrid.CellValueChanged

        Try


            Dim row As DataGridViewRow = dgrid.CurrentRow
            Dim vCodProdGrid As String = ""
            Dim IdProductoBodega As Integer = 0
            'Dim vPesoDisponible As Double = 0
            'Dim vCantidadDisponible As Double = 0

            If row Is Nothing OrElse (row.IsNewRow) Then Exit Sub

            vCodProdGrid = IIf(IsDBNull(dgrid.Item("ColCodProducto", e.RowIndex).Value), "", dgrid.Item("ColCodProducto", e.RowIndex).Value)
            IdProductoBodega = IIf(IsDBNull(dgrid.Item("ColIdProducto", e.RowIndex).Value), "", dgrid.Item("ColIdProducto", e.RowIndex).Value)

            If vCodProdGrid Is Nothing OrElse vCodProdGrid = "" Then Exit Sub

            Dim colUnidMed As DataGridViewCell

            Try
                colUnidMed = row.Cells(dgrid.Columns("colUnidadMedida").Index) 'IIf(IsDBNull(row.Cells(dgrid.Columns("colUnidMed").Index)), "", row.Cells(dgrid.Columns("colUnidMed").Index))
            Catch ex As Exception
            End Try

            Dim colEstadoProducto As DataGridViewCell

            Try
                colEstadoProducto = row.Cells(dgrid.Columns("colEstadoProducto").Index) 'IIf(IsDBNull(row.Cells(dgrid.Columns("colUnidMed").Index)), "", row.Cells(dgrid.Columns("colUnidMed").Index))
            Catch ex As Exception
            End Try


            Dim vCantidad As Double = IIf(IsDBNull(row.Cells("colCantidadExistencia").Value), 0, row.Cells("colCantidadExistencia").Value)
            Dim vPrecio As Double = IIf(IsDBNull(row.Cells("colPrecio").Value), 0, row.Cells("colPrecio").Value)

            'vPeso = Val(IIf(IsDBNull(dgvDetalleFactura.Item(3, e.RowIndex).Value), 0, dgvDetalleFactura.Item(3, e.RowIndex).Value))
            'vPrecio = Val(IIf(IsDBNull(dgvDetalleFactura.Item(4, e.RowIndex).Value), 0, dgvDetalleFactura.Item(4, e.RowIndex).Value))
            'vTotal = Val(IIf(IsDBNull(dgvDetalleFactura.Item(5, e.RowIndex).Value), 0, dgvDetalleFactura.Item(5, e.RowIndex).Value))

            'If vIdLinea = 0 Then vIdLinea = -1

            If pProducto.IdProductoBodega <> IdProductoBodega AndAlso vCodProdGrid <> "" OrElse IdProductoBodega = 0 Then

                pStock.IdProductoBodega = clsLnProducto.Get_IdProducto_By_Codigo(vCodProdGrid)

            End If

            pStock.ProductoEstado = New clsBeProducto_estado
            pStock.ProductoEstado.IdEstado = Convert.ToInt32(row.Cells("colEstadoProducto").Value)

            pStock.Presentacion = New clsBeProducto_Presentacion
            pStock.Presentacion.IdPresentacion = Convert.ToInt32(row.Cells("colPresentacion").Value)

            If Not IsNothing(pProducto.UnidadMedida) Then
                pStock.IdUnidadMedida = pProducto.UnidadMedida.IdUnidadMedida
            End If

            If pStock.IdProductoBodega <> 0 AndAlso pStock.ProductoEstado.IdEstado <> 0 Then

                pStock.IdProductoBodega = pProducto.IdProductoBodega
                clsLnStock.Get_Existencia_Disp_By_IdProducto(pStock, AP.IdBodega)

                row.Cells("colCantidadExistencia").Value = pStock.Cantidad
                row.Cells("colPesoExistencia").Value = pStock.Peso

            End If

            row.Cells("colTotal").Value = Math.Round(vCantidad * vPrecio, 2)

            'vEsProdBarra = (vProdFactMan.EsProdBarra OrElse vProdFactMan.Es_Rosti OrElse vProdFactMan.Es_VentaPorKg)

            'If e.ColumnIndex <> 5 Then

            '    'vPesoDisponible = Get_Peso_Temporal_Disponible(vCodProdGrid, vEsProdBarra)
            '    'vCantidadDisponible = Get_Inventario_Temporal_Disponible(vCodProdGrid, vEsProdBarra)

            '    If vProdFactMan.Es_VentaPorKg OrElse vProdFactMan.Es_Embutido_Peso_Variable Then
            '        If vIdLinea = -1 Then dgvDetalleFactura.Item(5, e.RowIndex).Value = vPesoFM * vPrecioFM
            '        dgvDetalleFactura.Item(8, e.RowIndex).Value = vPesoFM * vPrecioFM
            '    ElseIf vProdFactMan.Es_Embutido_Peso_Fijo Then
            '        If vIdLinea = -1 Then dgvDetalleFactura.Item(5, e.RowIndex).Value = vCantidadFM * vPrecioFM
            '        dgvDetalleFactura.Item(8, e.RowIndex).Value = vCantidadFM * vPrecioFM
            '    ElseIf vProdFactMan.Es_Huevo Then
            '        If vIdLinea = -1 Then dgvDetalleFactura.Item(5, e.RowIndex).Value = vCantidadFM * vPrecioFM
            '        dgvDetalleFactura.Item(8, e.RowIndex).Value = vCantidadFM * vPrecioFM
            '    ElseIf vProdFactMan.Es_Rosti Then

            '        Dim vCantidadPollos As Double = 0
            '        Dim vPesoPromedio As Double = 0
            '        Dim vPrecioPorUnidad As Double = 0

            '        Get_Peso_Promedio(vCodProdGrid, vCantidadPollos, vPesoPromedio)

            '        vPrecioPorUnidad = Math.Round((vPrecioFM), 2)

            '        If vIdLinea = -1 Then dgvDetalleFactura.Item(5, e.RowIndex).Value = vCantidadFM * vPrecioFM * vCantidadPollos
            '        dgvDetalleFactura.Item(8, e.RowIndex).Value = vCantidadFM * vPrecioFM * vCantidadPollos

            '        'Recálculo de peso
            '        'dgvDetalleFactura.Item(3, e.RowIndex).Value = Math.Round(vPesoDisponible / IIf(vCantidadFM = 0, 1, vCantidadFM), 2)

            '    ElseIf vProdFactMan.EsCongelado Then
            '        If vIdLinea = -1 Then dgvDetalleFactura.Item(5, e.RowIndex).Value = vPrecioFM * vPesoFM
            '        dgvDetalleFactura.Item(8, e.RowIndex).Value = vPrecioFM * vPesoFM
            '    Else
            '        If vIdLinea = -1 Then dgvDetalleFactura.Item(5, e.RowIndex).Value = vPrecioFM * vCantidadFM
            '        dgvDetalleFactura.Item(8, e.RowIndex).Value = vPrecioFM * vCantidadFM
            '    End If

            'Recálculo de peso.
            'If vProdFactMan.Es_VentaPorKg OrElse vProdFactMan.EsProdBarra OrElse vProdFactMan.Es_Rosti Then
            '    Dim vPesoPromedioBarra As Double = Math.Round(vPesoDisponible / IIf(vCantidadDisponible = 0, 1, vCantidadDisponible), 2)
            '    dgvDetalleFactura.Item(3, e.RowIndex).Value = vPesoPromedioBarra * vCantidadFM
            'End If

            'End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub dgrid_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgrid.CellValidating

        'Codigo - ColCodProducto
        'Desc - ColNomProducto
        'Cant - ColCantidad	
        'Peso - ColPeso
        'Precio - ColPrecioReal
        'Total - ColTotal
        'PesoR - ColPesoRoad
        'PrecioR - ColPrecioRoad
        'TotalR - ColTotalRoad
        'Boni - ColBonificacion
        'Consecutivo - colConsecutivo
        'IdLInea - IdLinea
        'Can. Disp - ColPesoDisp
        'Peso. disp - ColPesoDisp

        Try

            If Not dgrid.Rows(e.RowIndex) Is Nothing AndAlso Not dgrid.Rows(e.RowIndex).IsNewRow AndAlso dgrid.IsCurrentRowDirty Then

                dgrid.EndEdit()

                Dim row As DataGridViewRow = dgrid.Rows(e.RowIndex)

                Dim CodProductoCell As DataGridViewCell = row.Cells(dgrid.Columns("ColCodProducto").Index)
                'Dim NomProductoCell As DataGridViewCell = row.Cells(dgrid.Columns("ColNomProducto").Index)
                'Dim UniMedCell As DataGridViewCell = row.Cells(dgrid.Columns("colUnidadMedida").Index)

                'Dim colPresentacion As DataGridViewCell = row.Cells(dgrid.Columns("colPresentacion").Index)

                Try
                    'Dim colUnidMed As DataGridViewCell = row.Cells(dgrid.Columns("colUnidadMedida").Index)
                Catch ex As Exception
                End Try

                'Dim colCantidad As DataGridViewCell = row.Cells(dgrid.Columns("colCantidad").Index)

                Try
                    'Dim colPrecio As DataGridViewCell = row.Cells(dgrid.Columns("colPrecio").Index)
                Catch ex As Exception
                End Try

                'Dim colTotal As DataGridViewCell = row.Cells(dgrid.Columns("colTotal").Index)

                Dim vCodProdGrid = IIf(IsDBNull(CodProductoCell.Value), "", CodProductoCell.Value)

                If Not IsNothing(pProducto.UnidadMedida) Then
                    pStock.IdUnidadMedida = pProducto.UnidadMedida.IdUnidadMedida
                End If

                Dim HoraIni As DateTime = Now

                If CodProductoCell.Value Is Nothing Then Exit Sub

                Select Case dgrid.Columns(e.ColumnIndex).Name

                    Case "ColCodProducto"

                        If vCodProdGrid.ToString <> "" Then

                            pProducto.Codigo = CodProductoCell.Value

                            pProducto = clsLnProducto.Get_BeProducto_By_Codigo(pProducto.Codigo, cmbBodegaOrigen.EditValue)

                            dgrid.Item("ColNomProducto", e.RowIndex).Value = pProducto.Nombre

                            LlenaPresentacionGrid(e.RowIndex)

                            dgrid.Rows(e.RowIndex).Cells("colUnidadMedida").Value = pProducto.UnidadMedida.Nombre
                            dgrid.Rows(e.RowIndex).Cells("colPrecio").Value = pProducto.Precio

                            LlenaEstadosGrid(e.RowIndex)

                            Dim TiempoS As Integer = DateDiff(DateInterval.Second, HoraIni, Now)

                            XtraMessageBox.Show("Validatin: " & TiempoS, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            'setUnidadMedida(

                        End If

                    Case 2 'Cantidad


                    Case 3 'Peso de producto en factura manual


                    Case 4 'Precio de producto en factura manual


                    Case 5 'Total de línea


                End Select

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


    Private Sub dgvDetalleFactura_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgrid.EditingControlShowing

        Dim Dt As New List(Of String)

        Try

            dgrid.EndEdit()

            If txtCodigoProductoGrid IsNot Nothing Then txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()

            Dim vProducto As String = ""

            If dgrid.CurrentCell.ColumnIndex = 0 Then
                txtCodigoProductoGrid = TryCast(e.Control, TextBox)
                txtCodigoProductoGrid.BackColor = Color.White
            End If

            vProducto = IIf(IsDBNull(dgrid.Item(0, dgrid.CurrentCell.RowIndex).Value), "", dgrid.Item(0, dgrid.CurrentCell.RowIndex).Value)

            txtCodigoProductoGrid = TryCast(e.Control, TextBox)

            Select Case dgrid.CurrentCell.OwningColumn.Name

                Case "ColCodProducto"

                    If txtCodigoProductoGrid IsNot Nothing Then

                        txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()
                        txtCodigoProductoGrid.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                        txtCodigoProductoGrid.AutoCompleteSource = AutoCompleteSource.CustomSource

                        Dt = GetCodigosSugeridos()

                        For Each Cod As String In Dt
                            txtCodigoProductoGrid.AutoCompleteCustomSource.Add(Cod)
                        Next

                    Else
                        If Not IsNothing(txtCodigoProductoGrid) Then txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()
                    End If


                Case "colPresentacion"

                    If TypeOf (e.Control) Is DataGridViewComboBoxEditingControl Then

                        Dim cboThisComboBox = DirectCast(e.Control, DataGridViewComboBoxEditingControl)

                        AddHandler cboThisComboBox.SelectedValueChanged, LastEventHandlerPres

                        AddHandler cboThisComboBox.Leave, AddressOf RemoveValueChangedHandlerPres

                    End If

                Case "colEstadoProducto"

                    If TypeOf (e.Control) Is DataGridViewComboBoxEditingControl Then

                        Dim cboThisComboBox = DirectCast(e.Control, DataGridViewComboBoxEditingControl)

                        AddHandler cboThisComboBox.SelectedValueChanged, LastEventHandlerEstado

                        AddHandler cboThisComboBox.Leave, AddressOf RemoveValueChangedHandlerEstado

                    End If

                Case Else

                    txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()

            End Select


            'AddHandler txtCodigoProductoGrid.KeyPress, AddressOf Validar_Keypress

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try


    End Sub

    Private Sub dgrid_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgrid.RowValidating

        Try

            If Not dgrid.Rows(e.RowIndex) Is Nothing AndAlso Not dgrid.Rows(e.RowIndex).IsNewRow AndAlso dgrid.IsCurrentRowDirty Then

                Dim row As DataGridViewRow = dgrid.Rows(e.RowIndex)
                Dim CodProductoCell As DataGridViewCell = row.Cells(dgrid.Columns("colCodProducto").Index)
                Dim NomProductoCell As DataGridViewCell = row.Cells(dgrid.Columns("ColNomProducto").Index)
                Dim CantidadCell As DataGridViewCell = row.Cells(dgrid.Columns("ColCantidad").Index)
                Dim PesoCell As DataGridViewCell = row.Cells(dgrid.Columns("ColPeso").Index)
                Dim PrecioCell As DataGridViewCell = row.Cells(dgrid.Columns("ColPrecio").Index)
                Dim TotalCell As DataGridViewCell = row.Cells(dgrid.Columns("ColTotal").Index)
                Dim CantDisp As DataGridViewCell = row.Cells(dgrid.Columns("colCantidadExistencia").Index)


                Dim vCodigoProducto As String = ""
                Dim vNomProducto As String = ""
                Dim vCantidad As Double = 0
                Dim vPeso As Double = 0
                Dim vPrecio As Double = 0
                'Dim vLote As String = ""
                'Dim vBarra As String = ""
                Dim vTotal As Double = 0
                'Dim vBonificacion As Boolean = False
                Dim vCantidadDisponible As Double = 0
                'Dim vPesoDisponible As Double = 0
                'Dim vEsProdBarra As Boolean = False

                Try

                    vCodigoProducto = (IIf(IsDBNull(CodProductoCell.Value), "", CodProductoCell.Value))
                    vNomProducto = IIf(IsDBNull(NomProductoCell.Value), "", NomProductoCell.Value)
                    vCantidad = Val(IIf(IsDBNull(CantidadCell.Value), "0", CantidadCell.Value))
                    vPeso = Val(IIf(IsDBNull(PesoCell.Value), "0", PesoCell.Value))
                    vPrecio = Val(IIf(IsDBNull(PrecioCell.Value), "0", PrecioCell.Value))
                    vTotal = Val(IIf(IsDBNull(TotalCell.Value), "0", TotalCell.Value))
                    vCantidadDisponible = Val(IIf(IsDBNull(CantDisp.Value), "0", CantDisp.Value))

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try


                If vCodigoProducto <> "" Then

                    If vNomProducto = "" Then
                        CodProductoCell.ErrorText = "Código de producto no válido"
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = "Código de producto no válido"
                        e.Cancel = True
                    ElseIf vCantidad <= 0 Then
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                        CantidadCell.ErrorText = "Ingrese cantidad > 0"
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = "Ingrese cantidad > 0"
                        e.Cancel = True
                    ElseIf vPeso = 0 And (pProducto.Control_peso) Then
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = ""
                        PesoCell.ErrorText = "Ingrese peso > 0"
                        dgrid.Rows(PesoCell.RowIndex).ErrorText = "Ingrese peso > 0"
                        e.Cancel = True
                    ElseIf Not IsNumeric(vCantidad) AndAlso (dgrid.Item(0, e.RowIndex).Value <> "") Then
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = ""
                        dgrid.Rows(PesoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(PrecioCell.RowIndex).ErrorText = ""
                        CantidadCell.ErrorText = "Ingrese valor numérico"
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = "Ingrese valor numérico"
                        e.Cancel = True
                    ElseIf Val(vCantidad) > vCantidadDisponible Then 'Se vende por lote
                        Dim result As String = String.Format("La cantidad ingresada: {0} es mayor que la cantidad disponible: {1} ", vCantidad, vCantidadDisponible)
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = ""
                        dgrid.Rows(PesoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(PrecioCell.RowIndex).ErrorText = ""
                        CantidadCell.ErrorText = result
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = result
                        e.Cancel = True
                    Else
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                        CodProductoCell.ErrorText = ""

                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = ""
                        CantidadCell.ErrorText = ""

                        dgrid.Rows(PesoCell.RowIndex).ErrorText = ""
                        PesoCell.ErrorText = ""

                        dgrid.Rows(PrecioCell.RowIndex).ErrorText = ""
                        PrecioCell.ErrorText = ""

                        dgrid.Rows(TotalCell.RowIndex).ErrorText = ""
                        TotalCell.ErrorText = ""

                    End If


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


    Private Sub RemoveValueChangedHandlerPres(ByVal sender As Object, ByVal e As System.EventArgs)

        If TypeOf (sender) Is DataGridViewComboBoxEditingControl Then
            Dim cboThisCombobox = DirectCast(sender, DataGridViewComboBoxEditingControl)

            RemoveHandler cboThisCombobox.SelectedValueChanged, LastEventHandlerPres
        End If

    End Sub

    Private Sub RemoveValueChangedHandlerEstado(ByVal sender As Object, ByVal e As System.EventArgs)

        If TypeOf (sender) Is DataGridViewComboBoxEditingControl Then
            Dim cboThisCombobox = DirectCast(sender, DataGridViewComboBoxEditingControl)

            RemoveHandler cboThisCombobox.SelectedValueChanged, LastEventHandlerEstado
        End If

    End Sub

    Private Sub cmbPresentacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim combo As DataGridViewComboBoxEditingControl = TryCast(sender, DataGridViewComboBoxEditingControl)

        Dim IdPresentacion As Integer

        If (combo.SelectedItem IsNot Nothing) Then

            If Integer.TryParse(combo.SelectedValue, IdPresentacion) Then

                pStock.Presentacion.IdPresentacion = IdPresentacion

                If pStock.IdProductoBodega <> 0 AndAlso pStock.ProductoEstado.IdEstado <> 0 Then

                    pStock.IdProductoBodega = pProducto.IdProductoBodega
                    clsLnStock.Get_Existencia_Disp_By_IdProducto(pStock, AP.IdBodega)

                    dgrid.CurrentRow.Cells("colCantidadExistencia").Value = pStock.Cantidad
                    dgrid.CurrentRow.Cells("colPesoExistencia").Value = pStock.Peso

                End If

                'dgrid.CurrentRow.Cells("colTotal").Value = Math.Round(vCantidad * vPrecio, 2)

            End If

        End If

    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim combo As DataGridViewComboBoxEditingControl = TryCast(sender, DataGridViewComboBoxEditingControl)
            Dim IdEstado As Integer

            If (combo.SelectedItem IsNot Nothing) Then

                If Integer.TryParse(combo.SelectedValue, IdEstado) Then

                    pStock.ProductoEstado.IdEstado = IdEstado

                    If pStock.IdProductoBodega <> 0 AndAlso pStock.ProductoEstado.IdEstado <> 0 Then

                        pStock.IdProductoBodega = pProducto.IdProductoBodega
                        clsLnStock.Get_Existencia_Disp_By_IdProducto(pStock, AP.IdBodega)

                        dgrid.CurrentRow.Cells("colCantidadExistencia").Value = pStock.Cantidad
                        dgrid.CurrentRow.Cells("colPesoExistencia").Value = pStock.Peso

                        dgrid.EndEdit()

                    End If

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

    Private Function GetCodigosSugeridos() As List(Of String)

        GetCodigosSugeridos = Nothing

        Try

            Return clsLnProducto.Get_Codigos_Sugeridos_By_IdPropietario_And_IdBodega(cmbPropietario.EditValue, cmbBodegaOrigen.EditValue)

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
        Try

            mnuGuardar.Enabled = False

            XtraMessageBox.Show(cmbBodegaDesti.EditValue.ToString + " - " + cmbPropietario.EditValue.ToString, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            pObjetoTraslado.IdTrasladoEnc = clsLnTrans_tras_enc.MaxID
            pObjetoTraslado.IdBodegaOrigen = cmbBodegaOrigen.EditValue
            pObjetoTraslado.IdBodegaDestino = cmbBodegaDesti.EditValue
            pObjetoTraslado.IdPropietarioBodega = cmbPropietario.EditValue
            pObjetoTraslado.IdMuelleOrigen = cmbMuelle.EditValue
            pObjetoTraslado.IdPiloto = txtIdPiloto.Text
            pObjetoTraslado.IdVehiculo = txtIdVehiculo.Text
            pObjetoTraslado.IdRuta = txtIdRuta.Text
            pObjetoTraslado.FechaTraslado = dtpFechaTraslado.EditValue
            pObjetoTraslado.Hora_ini = dtpHoraInicialTraslado.Value
            pObjetoTraslado.Hora_fin = dtpHoraFinalTraslado.Value
            pObjetoTraslado.Ubicacion = ""   'Vacio segun indicacion de Erick
            pObjetoTraslado.Estado = "NUEVO"
            pObjetoTraslado.Activo = 1
            pObjetoTraslado.User_agr = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
            pObjetoTraslado.Fec_agr = Date.Now
            pObjetoTraslado.User_mod = ""
            pObjetoTraslado.Fec_mod = Date.Now
            pObjetoTraslado.No_documento = 1                     'Recuperar de alguna manera
            pObjetoTraslado.Local = cbxLocal.Checked
            pObjetoTraslado.Pallet_primero = True                                 ' Sin definir
            pObjetoTraslado.Anulado = False
            pObjetoTraslado.FechaEntrega = dtpFechaEntrega.EditValue
            pObjetoTraslado.HoraEntregaDesde = dtpHoraInicialEntrega.Value
            pObjetoTraslado.HoraEntregaHasta = dtpHoraMaximaEntrega.Value
            pObjetoTraslado.Observacion = txtObservacion.Text
            pObjetoTraslado.NoGuia = txtNoGuia.Text

            clsLnTrans_tras_enc.Insertar(pObjetoTraslado)

            XtraMessageBox.Show("Listo", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            mnuGuardar.Enabled = True

        Catch ex As Exception
            mnuGuardar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Public Sub CargaTrasladoProducto()

        cmbBodegaOrigen.EditValue = pObjetoTraslado.IdBodegaOrigen
        cmbBodegaDesti.EditValue = pObjetoTraslado.IdBodegaDestino
        cmbPropietario.EditValue = pObjetoTraslado.IdPropietarioBodega
        cmbMuelle.EditValue = pObjetoTraslado.IdMuelleOrigen
        txtIdPiloto.Text = pObjetoTraslado.IdPiloto
        txtIdVehiculo.Text = pObjetoTraslado.IdVehiculo
        txtIdRuta.Text = pObjetoTraslado.IdRuta
        dtpFechaTraslado.EditValue = pObjetoTraslado.FechaTraslado
        dtpHoraInicialTraslado.Value = pObjetoTraslado.Hora_ini
        dtpHoraFinalTraslado.Value = pObjetoTraslado.Hora_fin
        txtNoDocumento.Text = pObjetoTraslado.No_documento
        cbxLocal.Checked = pObjetoTraslado.Local
        dtpFechaEntrega.EditValue = pObjetoTraslado.FechaEntrega
        dtpHoraInicialEntrega.Value = pObjetoTraslado.HoraEntregaDesde
        dtpHoraMaximaEntrega.Value = pObjetoTraslado.HoraEntregaHasta
        txtObservacion.Text = pObjetoTraslado.Observacion
        txtNoGuia.Text = pObjetoTraslado.NoGuia

    End Sub

    Private Sub txtIdRuta_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdRuta.KeyDown
        Try
            If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
                cargarRuta()
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cargarRuta()

        Dim resultado As Boolean = False

        If String.IsNullOrEmpty(txtIdRuta.Text.Trim()) = False Then
            gBeRoadRuta.IdRuta = CInt(txtIdRuta.Text.Trim())
            If (gBeRoadRuta.IdRuta > 0) Then

                Dim lista As List(Of clsBeRoad_ruta) = clsLnRoad_ruta.GetAllFiltro(True).ToList()
                gBeRoadRuta = clsLnRoad_ruta.GetSingle(gBeRoadRuta.IdRuta)

                For it = 0 To lista.Count
                    If (gBeRoadRuta.IdRuta = lista.ElementAt(it).IdRuta) Then
                        resultado = True
                    End If
                    it += 1
                Next

                If (resultado) Then
                    txtIdRuta.Text = gBeRoadRuta.IdRuta
                    txtNombreRuta.Text = gBeRoadRuta.NOMBRE
                Else
                    txtIdRuta.Text = gBeRoadRuta.IdRuta
                    txtNombreRuta.Text = gBeRoadRuta.NOMBRE
                    txtIdRuta.Focus()
                End If
            Else
                txtIdRuta.Text = ""
                txtNombreRuta.Text = ""
                txtIdRuta.Focus()

            End If
        Else
            txtIdRuta.Text = ""
            txtNombreRuta.Text = ""
        End If
    End Sub

    Private Sub txtIdRuta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdRuta.KeyPress
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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdRuta.Text.Length = 1 Then
                txtIdRuta.Text = String.Empty
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    'Implementación de comboBodegaDestino
    'CM - 20170711
    Private Sub cmbBodegaDestino_SelectedValueChanged(sender As Object, e As EventArgs)
        If (cmbBodegaDesti.ItemIndex > 0) Then

            Try
                IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodegaDesti.EditValue, False)
                If (cmbPropietario.Text <> "") Then
                    cmbPropietario.ItemIndex = 0
                End If

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

    End Sub
End Class
