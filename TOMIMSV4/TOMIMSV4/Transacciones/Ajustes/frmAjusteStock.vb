Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen

Public Class frmAjusteStock

    Public pBeTransAjustEnc As New clsBeTrans_ajuste_enc
    Public Modo As TipoTrans

    Private dtm, dtt As New DataTable
    Private BeAjusteDet As New clsBeTrans_ajuste_det
    Private lBeTransAjusteDet As New List(Of clsBeTrans_ajuste_det)
    Private lBeTransMovimientos As New List(Of clsBeTrans_movimientos)

    Public Delegate Sub Listar_Ajustes()
    Public Property InvokeListarAjustes As Listar_Ajustes
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private Stock As FrmStock_List
    Private oDateTimePicker As DateTimePicker
    Private DgComboTipo As New DataGridViewComboBoxCell()

    Private IdTipoAjuste, IdMotivoAjuste, IdStockRes, IdTipoTarea As Integer
    Private TipoAjuste_Por_lote, TipoAjuste_Por_Fecha_Vence,
        TipoAjuste_Por_Cantidad, TipoAjuste_Por_Peso, Guardado As Boolean

    Private LastEventHandlerTipo As EventHandler = AddressOf combotipo_SelectedIndexChanged
    Private LastEventHandlerMotivo As EventHandler = AddressOf combomotivo_SelectedIndexChanged

    Dim DgComboBodega As New DataGridViewComboBoxCell()

    Private Es_Ajuste_Positivo As Boolean = False

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Sub New()

        InitializeComponent()

        Try

            Stock = New FrmStock_List(AP.IdBodega, cmbPropietarioBodega.EditValue) With {
            .Modo = FrmStock_List.pModo.Seleccion,
            .WindowState = FormWindowState.Maximized}

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Deshabilita_Grid()

        Try

            '#EJC20180924_1048PM: Si es un ajuste por inventario, se debe habilitar en el grid la bodega para que sea editable, para que se modifique, previo a envío a ERP.
            For Each Col As DataGridViewColumn In dgrid.Columns
                If Col.Name = "ColEnviadoAErp" Then
                    Col.ReadOnly = False
                ElseIf (Col.Name = "ColBodega") AndAlso pBeTransAjustEnc.Ajuste_Por_Inventario > 0 Then
                    Col.ReadOnly = False
                    Col.Visible = True
                ElseIf (Col.Name = "ColBodega") AndAlso Not pBeTransAjustEnc.Ajuste_Por_Inventario > 0 Then
                    Col.Visible = False
                Else
                    Col.ReadOnly = False
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private BeConfig As New clsBeI_nav_config_enc

    Private Sub Inserta_Encabezado_Ajuste()

        Try

            pBeTransAjustEnc.Idajusteenc = clsLnTrans_ajuste_enc.MaxID() + 1
            pBeTransAjustEnc.Referencia = txtReferencia.Text
            pBeTransAjustEnc.Fecha = dtpFecha.EditValue
            pBeTransAjustEnc.Fec_agr = dtpFecha.EditValue
            pBeTransAjustEnc.Fec_mod = dtpFecha.EditValue
            pBeTransAjustEnc.Idusuario = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.User_agr = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.User_mod = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.IdBodega = AP.IdBodega
            pBeTransAjustEnc.IdPropietarioBodega = cmbPropietarioBodega.EditValue

            clsLnTrans_ajuste_enc.Insertar(pBeTransAjustEnc)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Cargar_Datos()

        Try


            txtReferencia.Text = pBeTransAjustEnc.Referencia
            dtpFecha.EditValue = pBeTransAjustEnc.Fecha

            User_agrTextEdit.Text = pBeTransAjustEnc.User_agr
            Fec_agrDateEdit.Text = pBeTransAjustEnc.Fec_agr
            User_modTextEdit.Text = pBeTransAjustEnc.User_mod
            Fec_modDateEdit.Text = pBeTransAjustEnc.Fec_mod

            Dim BeCliente As New clsBeCliente
            BeCliente = clsLnCliente.Get_Single_By_Codigo(pBeTransAjustEnc.IdBodega)

            If Not BeCliente Is Nothing Then
                cmbBodegaERP.EditValue = BeCliente.IdCliente
            End If

            If pBeTransAjustEnc.IdPropietarioBodega <> 0 Then
                cmbPropietarioBodega.EditValue = pBeTransAjustEnc.IdPropietarioBodega
            End If

            cmbProductoFamilia.EditValue = pBeTransAjustEnc.IdProductoFamilia
            lcmbCentroCosto.EditValue = pBeTransAjustEnc.IdCentroCosto

            '#CKFK20220704 Cambié el clsLnTrans_ajuste_det.Get_All porque primero cargaba todos los ajustes para devolver el seleccionado
            lBeTransAjusteDet = clsLnTrans_ajuste_det.Get_By_IdAjusteEnc(pBeTransAjustEnc.Idajusteenc)

            Dim tipoajuste = 0
            If lBeTransAjusteDet IsNot Nothing Then
                If lBeTransAjusteDet.Count > 0 Then
                    '#GT13062022_0933: como este método es usado para abrir un registro existente,
                    'queda set en el cmbtipoajuste con el tipoajuste guardado en el detalle, sino es el tipo 3 (+/-) entonces puede ser cualquier otro
                    tipoajuste = lBeTransAjusteDet.FirstOrDefault().Idtipoajuste
                End If
            End If

            If tipoajuste = 3 OrElse tipoajuste = 5 Then
                cmbTipoAjuste.EditValue = 3
            Else
                cmbTipoAjuste.EditValue = tipoajuste
            End If

            Cargar_Detalle()

            Carga_Documentos_Asociados()

            chkAuditado.Checked = pBeTransAjustEnc.Auditado

            chkAuditado.Enabled = Not pBeTransAjustEnc.Enviado_A_ERP

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Cargar_Detalle()

        Dim rc As Integer
        Dim Ubic, Codigo As String
        Dim vIdProducto As Integer
        Dim clsTrans As New clsTransaccion

        dgrid.SuspendLayout() : dgrid.Rows.Clear()

        Try

            clsTrans.Begin_Transaction()

            Try

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Cargando detalle...")

            Catch ex As Exception

            End Try

            For Each vBeAjustDet As clsBeTrans_ajuste_det In lBeTransAjusteDet

                '#CKFK 20210223 Agregué la bodega a la funciónGet_Nombre_Completo_By_IdUbicacion
                Ubic = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(vBeAjustDet.IdUbicacion, AP.IdBodega, clsTrans.lConnection, clsTrans.lTransaction)

                vIdProducto = clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(vBeAjustDet.IdProductoBodega, clsTrans.lConnection, clsTrans.lTransaction)

                If vIdProducto <> 0 Then

                    Codigo = clsLnProducto.Get_Single_By_IdProducto(clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(vBeAjustDet.IdProductoBodega, clsTrans.lConnection, clsTrans.lTransaction)).Codigo

                    vBeAjustDet.UmBas = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(vBeAjustDet.IdUnidadMedida, clsTrans.lConnection, clsTrans.lTransaction)

                    rc = dgrid.Rows.Add(Codigo, vBeAjustDet.Nombre_producto, vBeAjustDet.UmBas, vBeAjustDet.Nombre_Presentacion, Ubic)

                    dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image
                    dgrid.Rows(rc).Cells("ColLote").Value = vBeAjustDet.Lote_original

                    Llenar_Motivo(rc, vBeAjustDet.IdMotivoAjuste, clsTrans.lConnection, clsTrans.lTransaction)
                    Llenar_Tipo(rc, vBeAjustDet.Idtipoajuste, clsTrans.lConnection, clsTrans.lTransaction)
                    Llena_Bodegas_ERP_Grid(rc, vBeAjustDet.IdBodegaERP, clsTrans.lConnection, clsTrans.lTransaction)

                    If vBeAjustDet.Idtipoajuste = 3 Then
                        If vBeAjustDet.IdPresentacion <> 0 Then
                            dgrid.Rows(rc).Cells("CantidadP").Value = vBeAjustDet.Cantidad_original / vBeAjustDet.Factor
                            dgrid.Rows(rc).Cells("ColCantidad").Value = (vBeAjustDet.Cantidad_nueva - vBeAjustDet.Cantidad_original) / vBeAjustDet.Factor
                        Else
                            dgrid.Rows(rc).Cells("CantidadP").Value = vBeAjustDet.Cantidad_original
                            dgrid.Rows(rc).Cells("ColCantidad").Value = vBeAjustDet.Cantidad_nueva - vBeAjustDet.Cantidad_original
                        End If
                    ElseIf vBeAjustDet.Idtipoajuste = 5 Then
                        If vBeAjustDet.IdPresentacion <> 0 Then
                            dgrid.Rows(rc).Cells("CantidadP").Value = vBeAjustDet.Cantidad_original / vBeAjustDet.Factor
                            dgrid.Rows(rc).Cells("ColCantidad").Value = (vBeAjustDet.Cantidad_original - vBeAjustDet.Cantidad_nueva) / vBeAjustDet.Factor
                        Else
                            dgrid.Rows(rc).Cells("CantidadP").Value = vBeAjustDet.Cantidad_original
                            dgrid.Rows(rc).Cells("ColCantidad").Value = vBeAjustDet.Cantidad_original - vBeAjustDet.Cantidad_nueva
                        End If
                    ElseIf vBeAjustDet.Idtipoajuste = 1 Then 'Ajuste Lote
                        '#EJC20180726: Desplegar lote anterior y nuevo lote
                        '#CKFK 20211214 Cambié el orden de los valores en ColCantidad siempre voy a colocar el nuevo valor
                        dgrid.Rows(rc).Cells("ColLote").Value = vBeAjustDet.Lote_original
                        dgrid.Rows(rc).Cells("ColCantidad").Value = vBeAjustDet.Lote_nuevo
                    ElseIf vBeAjustDet.Idtipoajuste = 2 Then 'Ajuste vencimiento
                        '#CKFK 20211214 La fecha vence nueva se estaba colocando en el lote, lo modifique
                        '#CKFK 20211214 Cambié el orden de los valores en ColCantidad siempre voy a colocar el nuevo valor
                        dgrid.Rows(rc).Cells("CantidadP").Value = vBeAjustDet.Fecha_vence_original
                        dgrid.Rows(rc).Cells("ColCantidad").Value = vBeAjustDet.Fecha_vence_nueva
                    ElseIf vBeAjustDet.Idtipoajuste = 4 Then ' Ajuste Peso
                        dgrid.Rows(rc).Cells("ColCantidad").Value = vBeAjustDet.Peso_nuevo
                    End If

                    '#CKFK 20211214 Quité esta columna porque estaba repetida dos veces
                    'dgrid.Rows(rc).Cells("ColObservacion").Value = vBeAjustDet.Observacion

                    dgrid.Rows(rc).Cells("colUbicacion").Value = Ubic
                    dgrid.Rows(rc).Cells("colUbicacion").ReadOnly = True
                    If vBeAjustDet.IdPresentacion <> 0 Then
                        dgrid.Rows(rc).Cells("colPresentacion").Value = vBeAjustDet.Nombre_Presentacion
                        dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
                    Else
                        dgrid.Rows(rc).Cells("colPresentacion").Value = Nothing
                        dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
                    End If

                    dgrid.Rows(rc).Cells("ColEnviadoAErp").Value = vBeAjustDet.Enviado
                    dgrid.Rows(rc).Cells("ColIdAjusteDEt").Value = vBeAjustDet.IdAjusteDet 'IdAjusteDet by EJC
                    dgrid.Rows(rc).Cells("UmBas").Value = vBeAjustDet.UmBas
                    dgrid.Rows(rc).Cells("ColObservacion").Value = vBeAjustDet.Observacion
                    dgrid.Rows(rc).Cells("ColLicPlate").Value = vBeAjustDet.lic_plate
                End If

                Application.DoEvents()

            Next

            clsTrans.Commit_Transaction()

            If Not lBeTransAjusteDet Is Nothing Then
                lblRegs.Caption = "Registros: " & lBeTransAjusteDet.Count
            Else
                lblRegs.Caption = "Registros: 0"
            End If

            RibbonStatusBar.Refresh()
            RibbonStatusBar.Update()

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            clsTrans.RollBack_Transaction()

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally

            clsTrans.Close_Conection()

            SplashScreenManager.CloseForm(False)

        End Try

        dgrid.ResumeLayout()

        'lblRegs.Caption = "Registros: " & dgrid.Rows.Count

    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click

        Dim st As New clsBeVW_stock_res
        Dim ubic, codigo As String
        Dim rc As Integer
        '#GT16062022_1511: set a 0
        Dim pTipoAjuste As Integer = 0

        Es_Ajuste_Positivo = False

        Try
            dgrid.EndEdit()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

        Try
            If dgrid.IsCurrentCellInEditMode Then commitValue()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

        Try

            Try

                Stock.IdPropietarioBodega = cmbPropietarioBodega.EditValue
                '#GT21112022_0900: envio tipo ajuste para validar si producto tiene la propiedad.
                Stock.varTipoAjuste = cmbTipoAjuste.EditValue

                If Stock.ShowDialog() <> DialogResult.OK Then
                    Try
                        Stock.Hide()
                    Catch ex As Exception
                    End Try
                    Return
                End If
            Catch ex As Exception
                MsgBox("No se puede mostrar el stock")
            End Try


            'GT21042022: iterar la selección multiple
            If Stock.SeleccionMultiple Then

                lBeTransAjusteDet = New List(Of clsBeTrans_ajuste_det)
                If Not cmbBodegaERP.EditValue Is Nothing AndAlso Not cmbBodegaERP.EditValue Is Nothing Then
                    pTipoAjuste = cmbTipoAjuste.EditValue
                End If

                For Each StockEspecificoSeleccionado In Stock.listaStockSeleccionado

                    Reservar_Stock(StockEspecificoSeleccionado.IdStock)

                    Llenar_Grid_Detalle(StockEspecificoSeleccionado, pTipoAjuste)

                    '#GT13062022_2004: si tiene mas de un registro en grid, bloquear el combo tipo ajuste
                    If dgrid.Rows.Count > 0 Then
                        cmbTipoAjuste.Enabled = False
                    End If

                Next



            Else

                Reservar_Stock(Stock.pObjStock.IdStock)

                BeAjusteDet = New clsBeTrans_ajuste_det
                BeAjusteDet.IdAjusteDet = 0
                BeAjusteDet.IdAjusteEnc = pBeTransAjustEnc.Idajusteenc
                BeAjusteDet.IdStock = Stock.pObjStock.IdStock
                BeAjusteDet.IdPropietarioBodega = Stock.pObjStock.IdPropietarioBodega
                BeAjusteDet.IdProductoBodega = Stock.pObjStock.IdProductoBodega
                BeAjusteDet.IdProductoEstado = Stock.pObjStock.IdProductoEstado
                BeAjusteDet.IdPresentacion = Stock.pObjStock.IdPresentacion
                BeAjusteDet.IdUnidadMedida = Stock.pObjStock.IdUnidadMedida
                BeAjusteDet.IdUbicacion = Stock.pObjStock.IdUbicacion

                If BeAjusteDet.IdPresentacion <> 0 Then
                    BeAjusteDet.Presentacion = clsLnProducto_presentacion.GetSingle(BeAjusteDet.IdPresentacion)
                End If

                BeAjusteDet.Lote_original = Stock.pObjStock.Lote
                BeAjusteDet.Lote_nuevo = Stock.pObjStock.Lote
                BeAjusteDet.Fecha_vence_original = Stock.pObjStock.Fecha_Vence
                BeAjusteDet.Fecha_vence_nueva = Stock.pObjStock.Fecha_Vence
                BeAjusteDet.Peso_original = Stock.pObjStock.Peso
                BeAjusteDet.Peso_nuevo = Stock.pObjStock.Peso
                BeAjusteDet.Cantidad_original = Stock.pObjStock.CantidadUmBas - Stock.pObjStock.CantidadReservadaUMBas
                BeAjusteDet.Cantidad_nueva = Stock.pObjStock.CantidadUmBas - Stock.pObjStock.CantidadReservadaUMBas
                BeAjusteDet.CantReservada = Stock.pObjStock.CantidadReservadaUMBas

                If BeAjusteDet.IdPresentacion <> 0 Then
                    BeAjusteDet.Cantidad_original = Math.Round(BeAjusteDet.Cantidad_original / BeAjusteDet.Presentacion.Factor, 6)
                    BeAjusteDet.Cantidad_nueva = Math.Round(BeAjusteDet.Cantidad_nueva / BeAjusteDet.Presentacion.Factor, 6)
                    BeAjusteDet.CantReservada = Math.Round(BeAjusteDet.CantReservada / BeAjusteDet.Presentacion.Factor, 6)
                End If

                BeAjusteDet.UmBas = Stock.pObjStock.UMBas
                BeAjusteDet.Codigo_producto = Stock.pObjStock.Codigo_Producto
                BeAjusteDet.Nombre_producto = Stock.pObjStock.Nombre_Producto 'clsLnProducto.GetSingle(Stock.pObjStock.IdProducto).Nombre
                BeAjusteDet.Idtipoajuste = 0
                BeAjusteDet.IdMotivoAjuste = 0
                BeAjusteDet.Observacion = ""
                BeAjusteDet.Codigo_ajuste = 0
                BeAjusteDet.Enviado = False
                BeAjusteDet.lic_plate = Stock.pObjStock.Lic_plate

                BeAjusteDet.idstockres = IdStockRes
                BeAjusteDet.idstocklink = 0
                BeAjusteDet.esnuevolink = 0
                lBeTransAjusteDet.Add(BeAjusteDet)

                ubic = clsLnBodega_ubicacion.GetSingle(BeAjusteDet.IdUbicacion, AP.IdBodega).NombreCompleto
                codigo = clsLnProducto.Get_Single_By_IdProducto(clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(BeAjusteDet.IdProductoBodega)).Codigo

                rc = dgrid.Rows.Add(codigo, BeAjusteDet.Nombre_producto, BeAjusteDet.UmBas, ubic)
                dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image
                dgrid.Rows(rc).Cells("ColLote").Value = BeAjusteDet.Lote_original

                dgrid.Rows(rc).Cells("UmBas").Value = BeAjusteDet.UmBas
                dgrid.Rows(rc).Cells("UmBas").ReadOnly = True

                dgrid.Rows(rc).Cells("colUbicacion").Value = ubic
                dgrid.Rows(rc).Cells("colUbicacion").ReadOnly = True

                '#CKFK 20211214 Agregué esta condición
                If dgrid.Columns("ColCantidad").HeaderText = "Vence Anterior" Then
                    dgrid.Rows(rc).Cells("ColC antidad").Value = BeAjusteDet.Fecha_vence_original
                ElseIf dgrid.Columns("ColCantidad").HeaderText = "Existencia" Then
                    dgrid.Rows(rc).Cells("ColCantidad").Value = BeAjusteDet.Cantidad_original
                ElseIf dgrid.Columns("ColCantidad").HeaderText = "Lote Anterior" Then
                    dgrid.Rows(rc).Cells("ColCantidad").Value = BeAjusteDet.Lote_original
                End If

                If BeAjusteDet.IdPresentacion <> 0 Then
                    dgrid.Rows(rc).Cells("colPresentacion").Value = BeAjusteDet.Presentacion.Nombre
                    dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
                Else
                    dgrid.Rows(rc).Cells("colPresentacion").Value = Nothing
                    dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
                End If

                If BeAjusteDet.lic_plate <> "" Then
                    dgrid.Rows(rc).Cells("ColLicPlate").Value = BeAjusteDet.lic_plate
                    dgrid.Rows(rc).Cells("ColLicPlate").ReadOnly = True
                Else
                    dgrid.Rows(rc).Cells("ColLicPlate").Value = Nothing
                    dgrid.Rows(rc).Cells("ColLicPlate").ReadOnly = True
                End If

                Llenar_Motivo(rc, -1)

                '#GT13062022_16_50: si pTipoAjuste es 0, entonces se deja set de cmbTipoAjuste, para que el grid obtenga las columnas adecuadas
                Dim cmbIdTipoAjuste = cmbTipoAjuste.EditValue
                If pTipoAjuste = 0 Then
                    Llenar_Tipo(rc, cmbIdTipoAjuste)
                Else
                    Llenar_Tipo(rc, pTipoAjuste)
                End If

                Llena_Bodegas_ERP_Grid(rc, -1)

                dgrid.Rows(rc).Selected = True

                '#GT13062022_2004: si tiene mas de un registro en grid, bloquear el combo tipo ajuste
                If dgrid.Rows.Count > 0 Then
                    cmbTipoAjuste.Enabled = False
                End If

            End If

            Stock.Hide()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End Try

        lblRegs.Caption = "Registros: " & dgrid.Rows.Count

    End Sub

    Private Sub Llenar_Grid_Detalle(stockEspecificoSeleccionado As clsBeVW_stock_res, pTipoAjuste As Integer)

        Dim ubic, codigo As String
        Dim rc As Integer

        Try

            BeAjusteDet = New clsBeTrans_ajuste_det
            BeAjusteDet.IdAjusteDet = 0
            BeAjusteDet.IdAjusteEnc = pBeTransAjustEnc.Idajusteenc
            BeAjusteDet.IdStock = stockEspecificoSeleccionado.IdStock
            BeAjusteDet.IdPropietarioBodega = stockEspecificoSeleccionado.IdPropietarioBodega
            BeAjusteDet.IdProductoBodega = stockEspecificoSeleccionado.IdProductoBodega
            BeAjusteDet.IdProductoEstado = stockEspecificoSeleccionado.IdProductoEstado
            BeAjusteDet.IdPresentacion = stockEspecificoSeleccionado.IdPresentacion
            BeAjusteDet.IdUnidadMedida = stockEspecificoSeleccionado.IdUnidadMedida
            BeAjusteDet.IdUbicacion = stockEspecificoSeleccionado.IdUbicacion

            If BeAjusteDet.IdPresentacion <> 0 Then
                BeAjusteDet.Presentacion = clsLnProducto_presentacion.GetSingle(BeAjusteDet.IdPresentacion)
            End If

            BeAjusteDet.Lote_original = stockEspecificoSeleccionado.Lote
            BeAjusteDet.Lote_nuevo = stockEspecificoSeleccionado.Lote
            BeAjusteDet.Fecha_vence_original = stockEspecificoSeleccionado.Fecha_Vence
            BeAjusteDet.Fecha_vence_nueva = stockEspecificoSeleccionado.Fecha_Vence
            BeAjusteDet.Peso_original = stockEspecificoSeleccionado.Peso
            BeAjusteDet.Peso_nuevo = stockEspecificoSeleccionado.Peso
            BeAjusteDet.Cantidad_original = stockEspecificoSeleccionado.CantidadUmBas - stockEspecificoSeleccionado.CantidadReservadaUMBas
            BeAjusteDet.Cantidad_nueva = stockEspecificoSeleccionado.CantidadUmBas - stockEspecificoSeleccionado.CantidadReservadaUMBas
            BeAjusteDet.CantReservada = stockEspecificoSeleccionado.CantidadReservadaUMBas

            If BeAjusteDet.IdPresentacion <> 0 Then
                BeAjusteDet.Cantidad_original = Math.Round(BeAjusteDet.Cantidad_original / BeAjusteDet.Presentacion.Factor, 6)
                BeAjusteDet.Cantidad_nueva = Math.Round(BeAjusteDet.Cantidad_nueva / BeAjusteDet.Presentacion.Factor, 6)
                BeAjusteDet.CantReservada = Math.Round(BeAjusteDet.CantReservada / BeAjusteDet.Presentacion.Factor, 6)
            End If

            BeAjusteDet.UmBas = stockEspecificoSeleccionado.UMBas
            BeAjusteDet.Codigo_producto = stockEspecificoSeleccionado.Codigo_Producto
            BeAjusteDet.Nombre_producto = stockEspecificoSeleccionado.Nombre_Producto 'clsLnProducto.GetSingle(Stock.pObjStock.IdProducto).Nombre
            BeAjusteDet.Idtipoajuste = 0
            BeAjusteDet.IdMotivoAjuste = 0
            BeAjusteDet.Observacion = ""
            BeAjusteDet.Codigo_ajuste = 0
            BeAjusteDet.Enviado = False
            BeAjusteDet.lic_plate = stockEspecificoSeleccionado.Lic_plate

            BeAjusteDet.idstockres = IdStockRes
            BeAjusteDet.idstocklink = 0
            BeAjusteDet.esnuevolink = 0
            lBeTransAjusteDet.Add(BeAjusteDet)

            ubic = clsLnBodega_ubicacion.GetSingle(BeAjusteDet.IdUbicacion, AP.IdBodega).NombreCompleto
            codigo = clsLnProducto.Get_Single_By_IdProducto(clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(BeAjusteDet.IdProductoBodega)).Codigo

            rc = dgrid.Rows.Add(codigo, BeAjusteDet.Nombre_producto, BeAjusteDet.UmBas, ubic)
            dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image
            dgrid.Rows(rc).Cells("ColLote").Value = BeAjusteDet.Lote_original

            dgrid.Rows(rc).Cells("UmBas").Value = BeAjusteDet.UmBas
            dgrid.Rows(rc).Cells("UmBas").ReadOnly = True

            dgrid.Rows(rc).Cells("colUbicacion").Value = ubic
            dgrid.Rows(rc).Cells("colUbicacion").ReadOnly = True

            '#CKFK 20211214 Agregué esta condición
            If dgrid.Columns("ColCantidad").HeaderText = "Vence Anterior" Then
                dgrid.Rows(rc).Cells("ColCantidad").Value = BeAjusteDet.Fecha_vence_original
            ElseIf dgrid.Columns("ColCantidad").HeaderText = "Existencia" Then
                dgrid.Rows(rc).Cells("ColCantidad").Value = BeAjusteDet.Cantidad_original
            ElseIf dgrid.Columns("ColCantidad").HeaderText = "Lote Anterior" Then
                dgrid.Rows(rc).Cells("ColCantidad").Value = BeAjusteDet.Lote_original
            End If

            If BeAjusteDet.IdPresentacion <> 0 Then
                dgrid.Rows(rc).Cells("colPresentacion").Value = BeAjusteDet.Presentacion.Nombre
                dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
            Else
                dgrid.Rows(rc).Cells("colPresentacion").Value = Nothing
                dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
            End If

            If BeAjusteDet.lic_plate <> "" Then
                dgrid.Rows(rc).Cells("ColLicPlate").Value = BeAjusteDet.lic_plate
                dgrid.Rows(rc).Cells("ColLicPlate").ReadOnly = True
            Else
                dgrid.Rows(rc).Cells("ColLicPlate").Value = Nothing
                dgrid.Rows(rc).Cells("ColLicPlate").ReadOnly = True
            End If

            Llenar_Motivo(rc, -1)
            'Llenar_Tipo(rc, -1)
            Llenar_Tipo(rc, pTipoAjuste)
            Llena_Bodegas_ERP_Grid(rc, -1)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub Reservar_Stock(idstock As Integer)

        Dim rs As New clsBeStock_res
        Dim st As New clsBeStock

        Try

            IdStockRes = clsLnStock_res.MaxID() + 1

            st.IdStock = idstock
            clsLnStock.GetSingle(st)

            rs.IdStockRes = IdStockRes
            rs.IdTransaccion = pBeTransAjustEnc.Idajusteenc
            rs.Indicador = "ajuste_stock" '#EJC20180613 ajuste_stock aplicado.
            rs.IdPedidoDet = 0
            rs.IdStock = st.IdStock
            rs.IdPropietarioBodega = st.IdPropietarioBodega
            rs.IdProductoBodega = st.IdProductoBodega
            rs.IdUbicacion = st.IdUbicacion
            rs.IdProductoEstado = st.IdProductoEstado
            rs.IdPresentacion = st.IdPresentacion
            rs.IdUnidadMedida = st.IdUnidadMedida
            rs.Lote = st.Lote
            rs.Lic_plate = st.Lic_plate
            rs.Serial = st.Serial
            rs.Cantidad = st.Cantidad
            rs.Peso = st.Peso
            rs.Estado = ""
            rs.Fecha_ingreso = st.Fecha_Ingreso
            rs.Fecha_vence = st.Fecha_vence
            rs.Uds_lic_plate = st.Uds_lic_plate
            rs.Ubicacion_ant = st.UbicacionAnterior
            rs.No_bulto = st.No_bulto
            rs.IdRecepcion = st.IdRecepcionEnc
            rs.IdPicking = st.IdPickingEnc
            rs.IdPedido = st.IdPedidoEnc
            rs.IdDespacho = st.IdDespachoEnc
            rs.User_agr = st.User_agr
            rs.Fec_agr = st.Fec_agr
            rs.User_mod = st.User_mod
            rs.Fec_mod = st.Fec_mod
            rs.Host = AP.HostName
            rs.añada = st.Añada
            rs.Fecha_manufactura = st.Fecha_Manufactura
            rs.Atributo_Variante_1 = st.Atributo_Variante_1
            rs.IdBodega = st.IdBodega

            clsLnStock_res.Insertar(rs)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    '#GT29112024: reserva para ajuste positivo de producto sin stock
    Private Function Reservar_Stock(idstock As Integer, ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Boolean

        Dim rs As New clsBeStock_res
        Dim st As New clsBeStock

        Reservar_Stock = False

        Try

            IdStockRes = clsLnStock_res.MaxID(lConnection, lTransaction) + 1

            st.IdStock = idstock
            st = clsLnStock.GetSingle(st.IdStock, lConnection, lTransaction)

            rs.IdStockRes = IdStockRes
            rs.IdTransaccion = pBeTransAjustEnc.Idajusteenc
            rs.Indicador = "ajuste_stock" '#EJC20180613 ajuste_stock aplicado.
            rs.IdPedidoDet = 0
            rs.IdStock = st.IdStock
            rs.IdPropietarioBodega = st.IdPropietarioBodega
            rs.IdProductoBodega = st.IdProductoBodega
            rs.IdUbicacion = st.IdUbicacion
            rs.IdProductoEstado = st.IdProductoEstado
            rs.IdPresentacion = st.IdPresentacion
            rs.IdUnidadMedida = st.IdUnidadMedida
            rs.Lote = st.Lote
            rs.Lic_plate = st.Lic_plate
            rs.Serial = st.Serial
            rs.Cantidad = st.Cantidad
            rs.Peso = st.Peso
            rs.Estado = ""
            rs.Fecha_ingreso = st.Fecha_Ingreso
            rs.Fecha_vence = st.Fecha_vence
            rs.Uds_lic_plate = st.Uds_lic_plate
            rs.Ubicacion_ant = st.UbicacionAnterior
            rs.No_bulto = st.No_bulto
            rs.IdRecepcion = st.IdRecepcionEnc
            rs.IdPicking = st.IdPickingEnc
            rs.IdPedido = st.IdPedidoEnc
            rs.IdDespacho = st.IdDespachoEnc
            rs.User_agr = st.User_agr
            rs.Fec_agr = st.Fec_agr
            rs.User_mod = st.User_mod
            rs.Fec_mod = st.Fec_mod
            rs.Host = AP.HostName
            rs.añada = st.Añada
            rs.Fecha_manufactura = st.Fecha_Manufactura
            rs.Atributo_Variante_1 = st.Atributo_Variante_1
            rs.IdBodega = st.IdBodega

            clsLnStock_res.Insertar(rs, lConnection, lTransaction)
            Reservar_Stock = True

        Catch ex As Exception

            Reservar_Stock = False

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub Llenar_Motivo(ByVal pIndex As Integer,
                              Optional pidmotivo As Integer = 0,
                              Optional ByVal lConnection As SqlConnection = Nothing,
                              Optional ByVal lTransaction As SqlTransaction = Nothing)

        Dim DgCombo As New DataGridViewComboBoxCell()
        Dim dt As New DataTable
        Dim vTransaccionRemota As Boolean = (lConnection IsNot Nothing AndAlso lTransaction IsNot Nothing)

        Try

            If vTransaccionRemota Then
                dt = clsLnAjuste_motivo.Listar(lConnection, lTransaction)
            Else
                dt = clsLnAjuste_motivo.Listar()
            End If


            DgCombo = TryCast(dgrid.Rows(pIndex).Cells("motivoajuste"), DataGridViewComboBoxCell)
            DgCombo.DataSource = dt
            DgCombo.ValueMember = "Idmotivoajuste"
            DgCombo.DisplayMember = "Nombre"

            If pidmotivo <> -1 Then
                DgCombo.Value = pidmotivo
            Else
                If dt.Rows.Count = 1 Then
                    pidmotivo = 1
                    DgCombo.Value = 1
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Llenar_Tipo(ByVal pIndex As Integer, Optional pidtipo As Integer = 0,
                              Optional ByVal lConnection As SqlConnection = Nothing,
                              Optional ByVal lTransaction As SqlTransaction = Nothing)

        Dim dt As New List(Of clsBeAjuste_tipo)

        Try


            'Se carga el combo del grid solo con los ajustes positivos/negativos

            '#GT10062022: removi el pidtipo y deje el IdTipoAjuste porque es el que se obtiene del grid.
            'If pidtipo = 3 Then --se cumple cuando del grid cmbTipoAjuste esta seleccionado el +/', pero cuando se selecciona del grid
            'entonces deja de ser 3 y podria ser 5 (3 y 5 son los ajustes por cantidad).
            '#GT10062022_1640

            If IdTipoAjuste = 3 OrElse IdTipoAjuste = 5 Then

                Dim vTransaccionRemota As Boolean = (lConnection IsNot Nothing AndAlso lTransaction IsNot Nothing)

                If vTransaccionRemota Then
                    dt = clsLnAjuste_tipo.Get_by_Cantidad(lConnection, lTransaction)
                Else
                    dt = clsLnAjuste_tipo.Get_by_Cantidad()
                End If


                '#13062022_1138: validamos si es nuevo o es un registro existente para cargar el tipo en el combo del grid.
                Select Case Modo

                    Case TipoTrans.Editar

                        If pidtipo = 3 OrElse pidtipo = 5 Then

                            DgComboTipo = TryCast(dgrid.Rows(pIndex).Cells("tipoajuste"), DataGridViewComboBoxCell)
                            DgComboTipo.DataSource = dt
                            DgComboTipo.DisplayMember = "Nombre"
                            DgComboTipo.ValueMember = "Idtipoajuste"

                            DgComboTipo.Value = pidtipo

                            dgrid.Rows(pIndex).Cells("tipoajuste").ReadOnly = True
                            Valor_Tipo_Ajuste(pIndex)

                        End If

                    Case TipoTrans.Nuevo

                        DgComboTipo = TryCast(dgrid.Rows(pIndex).Cells("tipoajuste"), DataGridViewComboBoxCell)
                        DgComboTipo.DataSource = dt
                        DgComboTipo.DisplayMember = "Nombre"
                        DgComboTipo.ValueMember = "Idtipoajuste"
                        dgrid.Rows(pIndex).Cells("tipoajuste").ReadOnly = False
                        Valor_Tipo_Ajuste(pIndex)

                End Select

            Else

                'GT22042022_1459: sino es ajuste por cantidad, cargamos los demas tipos, y se deja en set el del combo en el enc.
                If dgrid.Rows.Count = 0 Then
                    dt = clsLnAjuste_tipo.GetAll()
                Else
                    dt = clsLnAjuste_tipo.GetAll()
                End If

                DgComboTipo = TryCast(dgrid.Rows(pIndex).Cells("tipoajuste"), DataGridViewComboBoxCell)

                DgComboTipo.DataSource = dt
                DgComboTipo.DisplayMember = "Nombre"
                DgComboTipo.ValueMember = "Idtipoajuste"

                If pidtipo <> -1 Then
                    dgrid.Rows(pIndex).Cells("tipoajuste").ReadOnly = True
                    DgComboTipo.Value = pidtipo
                    IdTipoAjuste = pidtipo
                    Valor_Tipo_Ajuste(pIndex)
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
    Private Sub RemoveValueChangedHandlerTipo(ByVal sender As Object, ByVal e As EventArgs)

        If TypeOf (sender) Is DataGridViewComboBoxEditingControl Then
            Dim cboThisCombobox = DirectCast(sender, DataGridViewComboBoxEditingControl)

            RemoveHandler cboThisCombobox.SelectedValueChanged, LastEventHandlerTipo
        End If

    End Sub

    Private Sub RemoveValueChangedHandlerMotivo(ByVal sender As Object, ByVal e As EventArgs)

        If TypeOf (sender) Is DataGridViewComboBoxEditingControl Then
            Dim cboThisCombobox = DirectCast(sender, DataGridViewComboBoxEditingControl)

            RemoveHandler cboThisCombobox.SelectedValueChanged, LastEventHandlerMotivo
        End If

    End Sub

    Private Sub grdData_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgrid.EditingControlShowing

        Try

            If TypeOf e.Control Is System.Windows.Forms.ComboBox Then

                If dgrid.CurrentCell.OwningColumn.Name = "tipoajuste" Then

                    If TypeOf (e.Control) Is DataGridViewComboBoxEditingControl Then

                        Dim cboThisComboBox = DirectCast(e.Control, DataGridViewComboBoxEditingControl)

                        AddHandler cboThisComboBox.SelectedValueChanged, LastEventHandlerTipo
                        AddHandler cboThisComboBox.Leave, AddressOf RemoveValueChangedHandlerTipo

                    End If

                End If

                If dgrid.CurrentCell.OwningColumn.Name = "motivoajuste" Then

                    If TypeOf (e.Control) Is DataGridViewComboBoxEditingControl Then

                        Dim cboThisComboBox = DirectCast(e.Control, DataGridViewComboBoxEditingControl)

                        AddHandler cboThisComboBox.SelectedValueChanged, LastEventHandlerMotivo
                        AddHandler cboThisComboBox.Leave, AddressOf RemoveValueChangedHandlerMotivo

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

    Private Sub combotipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim DgCombo As New DataGridViewComboBoxCell()
        Dim sr As Integer

        Try

            If dgrid.Rows.Count > 0 Then

                sr = dgrid.SelectedRows(0).Index

                dgrid.EndEdit()

                DgCombo = TryCast(dgrid.Rows(sr).Cells("tipoajuste"), DataGridViewComboBoxCell)

                Dim vNombreTipo As String = DgCombo.EditedFormattedValue

                If vNombreTipo.Trim <> "" AndAlso vNombreTipo <> "System.Data.DataRowView" Then

                    Get_IdTipo_By_Nombre(DgCombo.EditedFormattedValue)

                    If IdTipoAjuste <= 0 Then Return

                    lBeTransAjusteDet(sr).Idtipoajuste = IdTipoAjuste

                    dgrid.Rows(sr).Cells("ColCantidad").Value = Nothing

                    If TipoAjuste_Por_Fecha_Vence Then

                        oDateTimePicker.Location = dgrid.GetCellDisplayRectangle((8), sr, False).Location
                        oDateTimePicker.Visible = True

                        If dgrid.CurrentCell.Value IsNot DBNull.Value AndAlso IsDate(dgrid.CurrentCell.Value) Then
                            oDateTimePicker.Value = dgrid.CurrentCell.Value
                        Else
                            oDateTimePicker.Value = Today
                        End If

                    Else
                        oDateTimePicker.Visible = False
                    End If

                    If TipoAjuste_Por_lote Then
                        Set_Valores_Ajuste_Lote(sr)
                    ElseIf TipoAjuste_Por_Fecha_Vence Then
                        Set_Valores_Ajuste_Vence(sr)
                    ElseIf TipoAjuste_Por_Cantidad Then
                        Set_Valores_Ajuste_Cantidad(sr)
                    ElseIf TipoAjuste_Por_Peso Then
                        Set_Valores_Ajuste_Peso(sr)
                    End If

                    Valor_Tipo_Ajuste(sr)

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

    Private Sub Valor_Tipo_Ajuste(sr As Integer)

        Dim dr() As DataRow

        TipoAjuste_Por_lote = False : TipoAjuste_Por_Fecha_Vence = False : TipoAjuste_Por_Cantidad = False : TipoAjuste_Por_Peso = False

        Try

            If IdTipoAjuste <> -1 Then

                dr = dtt.Select("idtipoajuste=" & IdTipoAjuste)

                If dr.Count = 0 Then Return

                If dr(0).Item("modifica_lote") Then TipoAjuste_Por_lote = True
                If dr(0).Item("momdifica_vencimiento") Then TipoAjuste_Por_Fecha_Vence = True
                If dr(0).Item("modifica_cantidad") Then TipoAjuste_Por_Cantidad = True
                If dr(0).Item("modifica_peso") Then TipoAjuste_Por_Peso = True

                If TipoAjuste_Por_lote Then 'colLote

                    dgrid.Columns("CantidadP").HeaderText = "Existencia"
                    dgrid.Rows(sr).Cells("CantidadP").Value = lBeTransAjusteDet(sr).Cantidad_original

                    dgrid.Columns("ColCantidad").HeaderText = "Nuevo Lote"
                    dgrid.Rows(sr).Cells("ColCantidad").ReadOnly = False
                    dgrid.Columns("colLote").HeaderText = "Lote Actual"
                    dgrid.Columns("UmBas").ReadOnly = True
                    dgrid.Columns("colLote").ReadOnly = True

                    Dim currencyCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle()
                    currencyCellStyle.Format = "N6"

                    dgrid.Columns("ColCantidad").DefaultCellStyle = currencyCellStyle

                    dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDet(sr).Lote_original

                    dgrid.Columns("ColEnviadoAErp").Visible = True

                    If Not BeConfig Is Nothing Then
                        If BeConfig.Interface_SAP Then
                            dgrid.Rows(sr).Cells("ColEnviadoAErp").Value = False
                        Else
                            dgrid.Rows(sr).Cells("ColEnviadoAErp").Value = True
                        End If
                    Else
                        dgrid.Rows(sr).Cells("ColEnviadoAErp").Value = True
                    End If

                    Set_Valores_Ajuste_Lote(sr) : Return

                End If

                If TipoAjuste_Por_Fecha_Vence Then

                    dgrid.Columns("ColCantidad").HeaderText = "Vence Actual"
                    dgrid.Columns("CantidadP").HeaderText = "Vence Anterior"

                    dgrid.Columns("CantidadP").ReadOnly = True
                    dgrid.Columns("ColCantidad").ReadOnly = False
                    dgrid.Columns("colLote").ReadOnly = True

                    Dim currencyCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle()
                    currencyCellStyle.Format = "d"

                    dgrid.Columns("ColCantidad").DefaultCellStyle = currencyCellStyle
                    dgrid.Columns("CantidadP").DefaultCellStyle = currencyCellStyle

                    dgrid.Rows(sr).Cells("CantidadP").Value = lBeTransAjusteDet(sr).Fecha_vence_original
                    dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDet(sr).Fecha_vence_original

                    dgrid.Columns("ColEnviadoAErp").Visible = True

                    If Not BeConfig Is Nothing Then
                        If BeConfig.Interface_SAP Then
                            dgrid.Rows(sr).Cells("ColEnviadoAErp").Value = False
                        Else
                            dgrid.Rows(sr).Cells("ColEnviadoAErp").Value = True
                        End If
                    Else
                        dgrid.Rows(sr).Cells("ColEnviadoAErp").Value = True
                    End If

                    Set_Valores_Ajuste_Vence(sr) : Return

                End If

                If TipoAjuste_Por_Cantidad Then

                    dgrid.Rows(sr).Cells("colLote").ReadOnly = True
                    dgrid.Columns("CantidadP").ReadOnly = True

                    dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDet(sr).Cantidad_original
                    dgrid.Rows(sr).Cells("CantidadP").Value = lBeTransAjusteDet(sr).Cantidad_original

                    dgrid.Columns("ColCantidad").HeaderText = "Cantidad"

                    Dim currencyCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle()
                    currencyCellStyle.Format = "N6"

                    dgrid.Columns("ColCantidad").DefaultCellStyle = currencyCellStyle
                    dgrid.Columns("CantidadP").DefaultCellStyle = currencyCellStyle

                    dgrid.Columns("ColEnviadoAErp").Visible = True

                    If Not BeConfig Is Nothing Then
                        If BeConfig.Interface_SAP Then
                            dgrid.Rows(sr).Cells("ColEnviadoAErp").Value = False
                        Else
                            dgrid.Rows(sr).Cells("ColEnviadoAErp").Value = True
                        End If
                    Else
                        dgrid.Rows(sr).Cells("ColEnviadoAErp").Value = True
                    End If

                    Set_Valores_Ajuste_Cantidad(sr) : Return

                End If

                If TipoAjuste_Por_Peso Then

                    dgrid.Columns("ColCantidad").ReadOnly = False

                    Dim currencyCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle()
                    currencyCellStyle.Format = "N6"

                    dgrid.Columns("ColCantidad").DefaultCellStyle = currencyCellStyle

                    Set_Valores_Ajuste_Peso(sr) : Return

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

    Private Sub Set_Valores_Ajuste_Lote(sr As Integer)

        Try

            dgrid.Rows(sr).Cells("LoteOrig").Value = lBeTransAjusteDet(sr).Lote_original

            If Modo = TipoTrans.Editar Then
                '#CKFK 20211214 Cambié la información porque en ColCantidad va el lote nuevo y en ColLote el original
                dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDet(sr).Lote_original
                dgrid.Rows(sr).Cells("ColLote").Value = lBeTransAjusteDet(sr).Lote_nuevo
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

    Private Sub Set_Valores_Ajuste_Vence(sr As Integer)
        Try
            'dgrid.Rows(sr).Cells("CantidadP").Value = Format(lBeTransAjusteDet(sr).Fecha_vence_original, "dd-MM-yyyy")
            'If Modo = TipoTrans.Editar Then dgrid.Rows(sr).Cells("ColCantidad").Value = Format(lBeTransAjusteDet(sr).Fecha_vence_nueva, "dd-MM-yyyy")
            dgrid.Rows(sr).Cells("CantidadP").Value = lBeTransAjusteDet(sr).Fecha_vence_original
            If Modo = TipoTrans.Editar Then dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDet(sr).Fecha_vence_nueva
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Set_Valores_Ajuste_Cantidad(sr As Integer)

        Dim vNuevaCant As Double = 0

        Try

            dgrid.Columns("ColCantidad").ReadOnly = False

            If Modo = TipoTrans.Editar Then dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDet(sr).Cantidad_original - lBeTransAjusteDet(sr).Cantidad_nueva

            If lBeTransAjusteDet(sr).Idtipoajuste = 3 Then
                vNuevaCant = lBeTransAjusteDet(sr).Cantidad_original + dgrid.Rows(sr).Cells("ColCantidad").Value
            ElseIf lBeTransAjusteDet(sr).Idtipoajuste = 5 Then
                vNuevaCant = lBeTransAjusteDet(sr).Cantidad_original - dgrid.Rows(sr).Cells("ColCantidad").Value
            End If

            If lBeTransAjusteDet(sr).Cantidad_original < vNuevaCant Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
            If lBeTransAjusteDet(sr).Cantidad_original > vNuevaCant Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Set_Valores_Ajuste_Peso(sr As Integer)
        Try
            dgrid.Rows(sr).Cells("CantidadP").Value = lBeTransAjusteDet(sr).Peso_original
            If Modo = TipoTrans.Editar Then dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDet(sr).Peso_nuevo

            If lBeTransAjusteDet(sr).Peso_original < lBeTransAjusteDet(sr).Peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
            If lBeTransAjusteDet(sr).Peso_original > lBeTransAjusteDet(sr).Peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub mnuDel_Click(sender As Object, e As EventArgs) Handles mnuDel.Click

        Dim sr, stocklink, ii As Integer
        Dim str As New clsBeStock_res

        dgrid.EndEdit()

        Try
            sr = dgrid.SelectedRows(0).Index
            If MessageBox.Show("Eliminar registro ?", "", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

        Try
            str.IdStockRes = lBeTransAjusteDet(sr).idstockres
            stocklink = lBeTransAjusteDet(sr).idstocklink

            If stocklink = 0 Then
                clsLnStock_res.Eliminar(str)

                lBeTransAjusteDet.RemoveAt(sr)
                dgrid.Rows.RemoveAt(sr)
            Else
                For ii = lBeTransAjusteDet.Count - 1 To 0 Step -1

                    If Not lBeTransAjusteDet(ii).esnuevolink Then
                        str.IdStockRes = lBeTransAjusteDet(ii).idstockres
                        clsLnStock_res.Eliminar(str)
                    End If

                    lBeTransAjusteDet.RemoveAt(ii)
                    dgrid.Rows.RemoveAt(ii)
                Next
            End If

            '#GT13062022_2025: si al caso el grid se queda sin registros, habilitamos el combo del tipo ajuste
            If dgrid.Rows.Count = 0 Then
                cmbTipoAjuste.Enabled = True
            End If

            lblRegs.Caption = "Registros: " & dgrid.Rows.Count
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuDividir_Click(sender As Object, e As EventArgs) Handles mnuDividir.Click
        Dim Item, It As clsBeTrans_ajuste_det
        Dim ubic, codigo, sval As String
        Dim sr, rc As Integer
        Dim tval, nval, oval, tpes, npes, opes As Double

        dgrid.EndEdit()

        Try
            sr = dgrid.SelectedRows(0).Index
            It = lBeTransAjusteDet(sr)
            tval = It.Cantidad_original
            tpes = It.Peso_original
            If It.idstocklink <> 0 Then
                XtraMessageBox.Show("No se puede dividir un registro dividido anteriormente ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        Try
            sval = XtraInputBox.Show("Ingrese la nueva cantidad. Max. : " & tval, "Division de cantidad", "1")
            If ((sval >= tval) Or (sval <= 0)) Then Throw New Exception

            nval = sval
            npes = nval * tpes / tval
            oval = tval - nval
            opes = tpes - npes
        Catch ex As Exception
            XtraMessageBox.Show("Cantidad incorrecta", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

        Try
            Item = New clsBeTrans_ajuste_det
            It = lBeTransAjusteDet(sr)

            Item.IdAjusteDet = 0
            Item.IdAjusteEnc = It.IdAjusteEnc
            Item.IdStock = It.IdStock
            Item.IdPropietarioBodega = It.IdPropietarioBodega
            Item.IdProductoBodega = It.IdProductoBodega
            Item.IdProductoEstado = It.IdProductoEstado
            Item.IdPresentacion = It.IdPresentacion
            Item.IdUnidadMedida = It.IdUnidadMedida
            Item.IdUbicacion = It.IdUbicacion

            Item.Lote_original = It.Lote_original
            Item.Lote_nuevo = It.Lote_original
            Item.Fecha_vence_original = It.Fecha_vence_original
            Item.Fecha_vence_nueva = It.Fecha_vence_original
            Item.Peso_original = 0
            Item.Peso_nuevo = npes
            Item.Cantidad_original = 0
            Item.Cantidad_nueva = nval

            Item.Codigo_producto = It.Codigo_producto
            Item.Nombre_producto = It.Nombre_producto
            Item.Idtipoajuste = 0
            Item.IdMotivoAjuste = 0
            Item.Observacion = ""
            Item.Codigo_ajuste = 0
            Item.Enviado = False

            Item.idstockres = IdStockRes
            Item.idstocklink = It.IdStock
            Item.esnuevolink = 1

            lBeTransAjusteDet.Add(Item)

            It.idstocklink = It.IdStock
            It.Cantidad_original = oval + nval
            It.Cantidad_nueva = oval
            It.Peso_original = opes + npes
            It.Peso_nuevo = opes

            ubic = clsLnBodega_ubicacion.GetSingle(Item.IdUbicacion, AP.IdBodega).NombreCompleto
            codigo = clsLnProducto.Get_Single_By_IdProducto(clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(Item.IdProductoBodega)).Codigo

            rc = dgrid.Rows.Add(codigo, Item.Nombre_producto, ubic)
            dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image

            Llenar_Motivo(rc, Item.IdMotivoAjuste)
            Llenar_Tipo(rc, Item.Idtipoajuste)

            lblRegs.Caption = "Registros: " & dgrid.Rows.Count
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Function commitValue() As Boolean

        Dim sr As Integer
        Dim ov As Object
        Dim val, v1, v2 As Double
        Dim f As DateTime

        'grdData.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image

        Try
            sr = dgrid.SelectedRows(0).Index
            v1 = dgrid.Rows(sr).Cells("CantidadP").Value
            v2 = dgrid.Rows(sr).Cells("ColCantidad").Value
            If v1 < v2 Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
            If v1 > v2 Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image
            If v1 = v2 Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image
        Catch ex As Exception
            dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image
        End Try

        Return True

        Try
            sr = dgrid.SelectedRows(0).Index
            Get_IdTipo_By_Nombre(dgrid.Rows(sr).Cells("tipoajuste").EditedFormattedValue)
            Valor_Tipo_Ajuste(sr)

            If lBeTransAjusteDet(sr).IdMotivoAjuste = 0 Or lBeTransAjusteDet(sr).Idtipoajuste = 0 Then
                XtraMessageBox.Show("Debe definir motivo y tipo de ajuste.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            If dgrid.IsCurrentCellInEditMode Then
                lBeTransAjusteDet(sr).Observacion = dgrid.Rows(sr).Cells(8).EditedFormattedValue
                ov = dgrid.Rows(sr).Cells(6).EditedFormattedValue
            Else
                lBeTransAjusteDet(sr).Observacion = dgrid.Rows(sr).Cells(8).Value
                ov = dgrid.Rows(sr).Cells("ColCantidad").Value
            End If

            dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image

            If TipoAjuste_Por_lote Then
                lBeTransAjusteDet(sr).Lote_nuevo = CStr(ov)
                lBeTransAjusteDet(sr).Codigo_ajuste = 16

                lBeTransAjusteDet(sr).Fecha_vence_nueva = lBeTransAjusteDet(sr).Fecha_vence_original
                lBeTransAjusteDet(sr).Cantidad_nueva = lBeTransAjusteDet(sr).Cantidad_original
                lBeTransAjusteDet(sr).Peso_nuevo = lBeTransAjusteDet(sr).Peso_original

                Return True
            End If

            If TipoAjuste_Por_Fecha_Vence Then
                Try
                    f = ov
                    lBeTransAjusteDet(sr).Fecha_vence_nueva = f
                    'grdData.CurrentCell.Value = f.ToShortDateString
                    lBeTransAjusteDet(sr).Codigo_ajuste = 15

                    lBeTransAjusteDet(sr).Lote_nuevo = lBeTransAjusteDet(sr).Lote_original
                    lBeTransAjusteDet(sr).Cantidad_nueva = lBeTransAjusteDet(sr).Cantidad_original
                    lBeTransAjusteDet(sr).Peso_nuevo = lBeTransAjusteDet(sr).Peso_original

                    Return True
                Catch ex As Exception
                    XtraMessageBox.Show("Valor fecha vencimiento incorrecto", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End Try

            End If

            If TipoAjuste_Por_Cantidad Then
                Try
                    val = ov
                    If val < 0 Then Throw New Exception

                    lBeTransAjusteDet(sr).Cantidad_nueva = val

                    If lBeTransAjusteDet(sr).Idtipoajuste = 3 Then
                        lBeTransAjusteDet(sr).Codigo_ajuste = 13
                    ElseIf lBeTransAjusteDet(sr).Idtipoajuste = 5 Then
                        lBeTransAjusteDet(sr).Codigo_ajuste = 17
                    End If

                    lBeTransAjusteDet(sr).Lote_nuevo = lBeTransAjusteDet(sr).Lote_original
                    lBeTransAjusteDet(sr).Fecha_vence_nueva = lBeTransAjusteDet(sr).Fecha_vence_original
                    lBeTransAjusteDet(sr).Peso_nuevo = lBeTransAjusteDet(sr).Peso_original

                    If lBeTransAjusteDet(sr).Cantidad_original < lBeTransAjusteDet(sr).Cantidad_nueva Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
                    If lBeTransAjusteDet(sr).Cantidad_original > lBeTransAjusteDet(sr).Cantidad_nueva Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image
                    If lBeTransAjusteDet(sr).Cantidad_original = lBeTransAjusteDet(sr).Cantidad_nueva Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image

                    Return True
                Catch ex As Exception
                    XtraMessageBox.Show("Valor cantidad incorrecto", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End Try
            End If

            If TipoAjuste_Por_Peso Then

                Try

                    val = ov

                    If val < 0 Then Throw New Exception

                    lBeTransAjusteDet(sr).Peso_nuevo = val
                    lBeTransAjusteDet(sr).Codigo_ajuste = 14

                    lBeTransAjusteDet(sr).Lote_nuevo = lBeTransAjusteDet(sr).Lote_original
                    lBeTransAjusteDet(sr).Fecha_vence_nueva = lBeTransAjusteDet(sr).Fecha_vence_original
                    lBeTransAjusteDet(sr).Cantidad_nueva = lBeTransAjusteDet(sr).Cantidad_original

                    If lBeTransAjusteDet(sr).Peso_original < lBeTransAjusteDet(sr).Peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
                    If lBeTransAjusteDet(sr).Peso_original > lBeTransAjusteDet(sr).Peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image
                    If lBeTransAjusteDet(sr).Peso_original = lBeTransAjusteDet(sr).Peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image

                    Return True
                Catch ex As Exception
                    XtraMessageBox.Show("Valor peso incorrecto", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End Try
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

    Private Sub mnuImprimir1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir1.ItemClick
        Llenar_DS_Rep()
    End Sub

    Private Sub combomotivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim DgCombo As New DataGridViewComboBoxCell()
        Dim sr As Integer
        Dim vNombreMotivo As String = ""

        Try

            If dgrid.Rows.Count > 0 Then

                sr = dgrid.SelectedRows(0).Index

                dgrid.EndEdit()

                DgCombo = TryCast(dgrid.Rows(sr).Cells("motivoajuste"), DataGridViewComboBoxCell)

                vNombreMotivo = DgCombo.EditedFormattedValue

                If vNombreMotivo.Trim <> "" AndAlso vNombreMotivo <> "System.Data.DataRowView" Then

                    Get_IdMotivo_By_Nombre(vNombreMotivo)

                    If IdMotivoAjuste > 0 Then
                        lBeTransAjusteDet(sr).IdMotivoAjuste = IdMotivoAjuste
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

    Private Sub Get_IdTipo_By_Nombre(val As String)

        Dim dr() As DataRow

        TipoAjuste_Por_lote = False : TipoAjuste_Por_Fecha_Vence = False : TipoAjuste_Por_Cantidad = False : TipoAjuste_Por_Peso = False

        IdTipoAjuste = -1

        Try

            dr = dtt.Select("Nombre='" & val & "'")

            IdTipoAjuste = dr(0).Item("idtipoajuste")

            If dr(0).Item("modifica_lote") Then TipoAjuste_Por_lote = True
            If dr(0).Item("momdifica_vencimiento") Then TipoAjuste_Por_Fecha_Vence = True
            If dr(0).Item("modifica_cantidad") Then TipoAjuste_Por_Cantidad = True
            If dr(0).Item("modifica_peso") Then TipoAjuste_Por_Peso = True

        Catch ex As Exception
            IdTipoAjuste = -1
        End Try

    End Sub

    Private Sub Get_IdMotivo_By_Nombre(ByVal NombreMotivo As String)

        Try

            If NombreMotivo.Trim <> "" Then

                Dim dr() As DataRow

                IdMotivoAjuste = -1

                dr = dtm.Select("Nombre='" & NombreMotivo & "'")

                If dr(0).Item("Idmotivoajuste") > 0 Then
                    IdMotivoAjuste = IIf(IsDBNull(dr(0).Item("Idmotivoajuste")), "-1", dr(0).Item("Idmotivoajuste"))
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdData_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgrid.CellEndEdit
        Try
            'commitValue()
            oDateTimePicker.Visible = False
        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Private Sub grdData_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgrid.CellBeginEdit

        If dgrid.Focused AndAlso dgrid.CurrentCell.ColumnIndex = 7 Then

            If TipoAjuste_Por_Fecha_Vence Then
                oDateTimePicker.Location = dgrid.GetCellDisplayRectangle((8), e.RowIndex, False).Location
                oDateTimePicker.Visible = True

                If dgrid.CurrentCell.Value IsNot DBNull.Value AndAlso IsDate(dgrid.CurrentCell.Value) Then
                    oDateTimePicker.Value = dgrid.CurrentCell.Value
                Else
                    oDateTimePicker.Value = Today
                End If
            Else
                oDateTimePicker.Visible = False
            End If

        End If

    End Sub

    Private Sub grdData_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgrid.CellEnter

        Try

            If e.ColumnIndex = 5 Then

                oDateTimePicker.Visible = False

                Dim vNomTipo As String = IIf(IsDBNull(dgrid.Rows(e.RowIndex).Cells("tipoajuste").EditedFormattedValue), "", dgrid.Rows(e.RowIndex).Cells("tipoajuste").EditedFormattedValue)

                If vNomTipo <> "" Then
                    Get_IdTipo_By_Nombre(vNomTipo)
                    Valor_Tipo_Ajuste(e.RowIndex)
                    If IdTipoAjuste > 0 Then oDateTimePicker.Visible = TipoAjuste_Por_Fecha_Vence
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            If XtraMessageBox.Show("¿Guardar ajuste?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If dgrid.IsCurrentCellInEditMode Then
                    If Not commitValue() Then Return
                End If

                dgrid.EndEdit()

                If dgrid.Rows.Count = 0 Then
                    XtraMessageBox.Show("La transaccion debe contener al menos 1 ajuste", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If

                If Not Validar_Datos() Then Return

                '#GT27112024: ajuste normal debe entrar por aca
                If Not Es_Ajuste_Positivo Then

                    If pBeTransAjustEnc.Ajuste_Por_Inventario > 0 Then
                        Actualizar_Ajuste()
                    Else
                        Guardar_Ajuste()
                    End If
                Else
                    '#GT27112024: ajuste positivo por este proceso
                    Guardar_Stock_por_Ajuste_Positivo()
                End If

                If Not InvokeListarAjustes Is Nothing Then
                    InvokeListarAjustes.Invoke
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Function Validar_Datos() As Boolean

        Dim FechaVenceNueva, FechaOriginalVence As DateTime
        Dim sr As Integer
        Dim val As Double
        Dim vNomTipoAjuste As String = ""
        Dim vNomMotivo As String = ""

        For sr = 0 To dgrid.Rows.Count - 1

            vNomTipoAjuste = IIf(IsDBNull(dgrid.Rows(sr).Cells("tipoajuste").EditedFormattedValue), "", dgrid.Rows(sr).Cells("tipoajuste").EditedFormattedValue)

            '#CM_18062018: Puse en comentario este metodo porque llama de nuevo a comprobar el IdMotivo pero ya viene en una variable publica,
            'entonces coloca el IdMotivoAjuste en -1.
            '#EJC20180924: Que gracioso, quedaba en -1 porque estaba buscando el nombre del tipo en el motivo por eso nunca lo encontraba.
            'Get_IdMotivo_By_Nombre(vNomMotivo)
            Get_IdTipo_By_Nombre(vNomTipoAjuste)

            If IdTipoAjuste < 1 Then
                dgrid.Rows(sr).Cells(0).Selected = True
                XtraMessageBox.Show("Linea : " & sr + 1 & " Debe definir el tipo de ajuste ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            vNomMotivo = dgrid.Rows(sr).Cells("motivoajuste").EditedFormattedValue
            Get_IdMotivo_By_Nombre(vNomMotivo)

            If IdMotivoAjuste < 1 Then
                dgrid.Rows(sr).Cells(0).Selected = True
                XtraMessageBox.Show("Línea : " & sr + 1 & " Debe definir el motivo del ajuste ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            '#EJC20240715: No mandatorio, cumbre.
            'If dgrid.Rows(sr).Cells("ColObservacion").Value = "" AndAlso Not pBeTransAjustEnc.Ajuste_Por_Inventario Then
            '    dgrid.Rows(sr).Cells(0).Selected = True
            '    XtraMessageBox.Show("Línea : " & CInt(sr + 1) & " Falta la observación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return False
            'End If

            lBeTransAjusteDet(sr).IdMotivoAjuste = IdMotivoAjuste
            lBeTransAjusteDet(sr).Observacion = dgrid.Rows(sr).Cells("ColObservacion").Value

            If TipoAjuste_Por_lote Then
                If lBeTransAjusteDet(sr).idstocklink = 0 Then
                    '#GT27112024: un ajuste positivo podria llevar los mismos datos tanto en origen como destino
                    If Not Es_Ajuste_Positivo Then
                        If (dgrid.Rows(sr).Cells("LoteOrig").Value = dgrid.Rows(sr).Cells("ColCantidad").Value) Then
                            XtraMessageBox.Show("Linea : " & sr + 1 & " Valor original y nuevo deben ser distintos !", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If
                    End If
                End If

                lBeTransAjusteDet(sr).Lote_nuevo = dgrid.Rows(sr).Cells("ColCantidad").Value
                lBeTransAjusteDet(sr).Codigo_ajuste = 16

                dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image

            End If

            If TipoAjuste_Por_Fecha_Vence Then

                Try

                    If oDateTimePicker.Visible Then
                        FechaVenceNueva = oDateTimePicker.Value
                    Else

                        Dim vValor As String = dgrid.Rows(sr).Cells("ColCantidad").Value

                        If vValor = "" Then
                            Throw New Exception("No se ha ingresado el valor")
                        Else
                            FechaVenceNueva = vValor
                        End If

                    End If

                    FechaOriginalVence = dgrid.Rows(sr).Cells("CantidadP").Value

                    If lBeTransAjusteDet(sr).idstocklink = 0 Then
                        If (FechaOriginalVence = FechaVenceNueva) Then
                            XtraMessageBox.Show("Linea : " & sr + 1 & " Valor original y nuevo deben ser distintos !", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If
                    End If

                    lBeTransAjusteDet(sr).Fecha_vence_nueva = FechaVenceNueva
                    lBeTransAjusteDet(sr).Codigo_ajuste = 15

                    dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image

                Catch ex As Exception
                    dgrid.Rows(sr).Cells(0).Selected = True
                    XtraMessageBox.Show("Linea : " & sr + 1 & " Valor fecha vencimiento incorrecto", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End Try

            End If

            If TipoAjuste_Por_Cantidad Then

                Try

                    Dim vNuevaCantidad As Double = 0

                    If (dgrid.Rows(sr).Cells("tipoajuste").Value = Nothing) Then
                        XtraMessageBox.Show("Linea : " & sr + 1 & " No ha seleccionado correctamente el tipo de ajuste !", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    If dgrid.Rows(sr).Cells("tipoajuste").Value = 3 Then
                        vNuevaCantidad = dgrid.Rows(sr).Cells("CantidadP").Value + dgrid.Rows(sr).Cells("ColCantidad").Value
                    ElseIf dgrid.Rows(sr).Cells("tipoajuste").Value = 5 Then
                        vNuevaCantidad = dgrid.Rows(sr).Cells("CantidadP").Value - dgrid.Rows(sr).Cells("ColCantidad").Value
                    End If

                    If lBeTransAjusteDet(sr).idstocklink = 0 Then
                        If (dgrid.Rows(sr).Cells("CantidadP").Value = vNuevaCantidad) Then
                            XtraMessageBox.Show("Linea : " & sr + 1 & " Valor original y nuevo deben ser distintos !", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If
                    End If

                    If dgrid.Rows(sr).Cells("ColCantidad").Value.ToString = "" Then Throw New Exception
                    val = vNuevaCantidad

                    If val < 0 Then Throw New Exception

                    lBeTransAjusteDet(sr).Cantidad_nueva = val

                    If lBeTransAjusteDet(sr).Idtipoajuste = 3 Then
                        lBeTransAjusteDet(sr).Codigo_ajuste = 13
                    ElseIf lBeTransAjusteDet(sr).Idtipoajuste = 5 Then
                        lBeTransAjusteDet(sr).Codigo_ajuste = 17
                    Else
                    End If

                    If lBeTransAjusteDet(sr).Cantidad_original < lBeTransAjusteDet(sr).Cantidad_nueva Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
                    If lBeTransAjusteDet(sr).Cantidad_original > lBeTransAjusteDet(sr).Cantidad_nueva Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image
                    If lBeTransAjusteDet(sr).Cantidad_original = lBeTransAjusteDet(sr).Cantidad_nueva Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image

                Catch ex As Exception
                    XtraMessageBox.Show("Linea : " & sr + 1 & " el valor ingresado en el campo cantidad es incorrecto", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End Try

            End If

            If TipoAjuste_Por_Peso Then

                Try

                    If lBeTransAjusteDet(sr).idstocklink = 0 Then
                        If (dgrid.Rows(sr).Cells("CantidadP").Value = dgrid.Rows(sr).Cells("ColCantidad").Value) Then
                            XtraMessageBox.Show("Linea : " & sr + 1 & " Valor original y nuevo deben ser distinctos !", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If
                    End If

                    If dgrid.Rows(sr).Cells("ColCantidad").Value = "" Then Throw New Exception
                    val = dgrid.Rows(sr).Cells("ColCantidad").Value
                    If val < 0 Then Throw New Exception

                    lBeTransAjusteDet(sr).Peso_nuevo = val
                    lBeTransAjusteDet(sr).Codigo_ajuste = 14

                    If lBeTransAjusteDet(sr).Peso_original < lBeTransAjusteDet(sr).Peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
                    If lBeTransAjusteDet(sr).Peso_original > lBeTransAjusteDet(sr).Peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image
                    If lBeTransAjusteDet(sr).Peso_original = lBeTransAjusteDet(sr).Peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image

                Catch ex As Exception
                    XtraMessageBox.Show("Linea : " & sr + 1 & " Valor peso incorrecto", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End Try
            End If

        Next

        Return True

    End Function

    Private Sub Guardar_Ajuste()

        Dim ss As String = ""
        Dim cc, ic As Integer
        Dim Codigo As String, Lote As String, IdAjusteDet As Integer
        Dim Enviar As Boolean = False
        Dim IdBodegaERP As Integer
        Dim CantidadRegistrosEnviados As Integer = 0

        Try

            pBeTransAjustEnc.Referencia = txtReferencia.Text
            pBeTransAjustEnc.Fecha = dtpFecha.EditValue
            pBeTransAjustEnc.Fec_agr = dtpFecha.EditValue
            pBeTransAjustEnc.Fec_mod = dtpFecha.EditValue
            pBeTransAjustEnc.Idusuario = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.User_agr = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.User_mod = AP.UsuarioAp.IdUsuario
            '#CM_20200219: Se cambio el IdBodega en lugar de IdBodegaErp 
            pBeTransAjustEnc.IdBodega = AP.IdBodega
            pBeTransAjustEnc.IdProductoFamilia = cmbProductoFamilia.EditValue
            pBeTransAjustEnc.IdPropietarioBodega = cmbPropietarioBodega.EditValue
            pBeTransAjustEnc.Auditado = chkAuditado.Checked

            '#EJC20220330_2355_BYB: Guardar el centro de costo.
            If Not lcmbCentroCosto.EditValue Is Nothing Then
                pBeTransAjustEnc.IdCentroCosto = lcmbCentroCosto.EditValue
            End If

            cc = 0

            For I = 0 To lBeTransAjusteDet.Count - 1

                lBeTransAjusteDet(I).IdAjusteEnc = pBeTransAjustEnc.Idajusteenc : ic = 0

                If lBeTransAjusteDet(I).Lote_original <> lBeTransAjusteDet(I).Lote_nuevo Then ic += 1
                If lBeTransAjusteDet(I).Fecha_vence_original <> lBeTransAjusteDet(I).Fecha_vence_nueva Then ic += 1
                If lBeTransAjusteDet(I).Cantidad_original <> lBeTransAjusteDet(I).Cantidad_nueva Then ic += 1
                If lBeTransAjusteDet(I).Peso_original <> lBeTransAjusteDet(I).Peso_nuevo Then ic += 1
                If ic > 0 Then cc += 1

                '#EJC20180924: Asignación de bodega de ERP.                
                DgComboBodega = TryCast(dgrid.CurrentRow.Cells("ColBodega"), DataGridViewComboBoxCell)
                '#CM_20200219: Se cambio el IdBodega en lugar de IdBodegaErp
                IdBodegaERP = cmbBodegaERP.EditValue
                lBeTransAjusteDet(I).IdBodegaERP = IdBodegaERP

                '#GT13062022_1906: Asignación del tipoajuste seteado en el grid.     
                lBeTransAjusteDet(I).Idtipoajuste = dgrid.Rows(I).Cells("tipoajuste").Value

            Next

            For i = 0 To dgrid.Rows.Count - 1

                Codigo = IIf(IsDBNull(dgrid.Rows(i).Cells("ColCodigoProducto").Value), "", dgrid.Rows(i).Cells("ColCodigoProducto").Value)
                Lote = IIf(IsDBNull(dgrid.Rows(i).Cells("ColLote").Value), "", dgrid.Rows(i).Cells("ColLote").Value)
                IdAjusteDet = IIf(IsDBNull(dgrid.Rows(i).Cells("ColIdAjusteDEt").Value), 0, dgrid.Rows(i).Cells("ColIdAjusteDEt").Value)
                Enviar = IIf(IsDBNull(dgrid.Rows(i).Cells("ColEnviadoAErp").Value), False, dgrid.Rows(i).Cells("ColEnviadoAErp").Value)

                BeAjusteDet = lBeTransAjusteDet.Find(Function(x) x.Codigo_producto = Codigo _
                                                    AndAlso x.Lote_nuevo = Lote _
                                                    AndAlso x.IdAjusteDet = IdAjusteDet)

                '#GT28112024: al ser ajuste positivo, el idstock se obtiene hasta que se presiona guardar
                If Es_Ajuste_Positivo Then
                    BeAjusteDet.IdStock = pStock_Sin_Existencia_Previa.IdStock
                End If

                If Not BeAjusteDet Is Nothing Then
                    BeAjusteDet.Enviado = Enviar
                    If Enviar Then
                        CantidadRegistrosEnviados += 1
                    End If
                End If

            Next

            Crear_Movimientos()

            clsLnTrans_ajuste_enc.Aplicar_Ajuste(pBeTransAjustEnc,
                                                 lBeTransAjusteDet,
                                                 lBeTransMovimientos)

            If CantidadRegistrosEnviados = lBeTransAjusteDet.Count Then
                If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.Idajusteenc, True) > 0 Then
                    XtraMessageBox.Show("Se guardó el ajuste y se actualizó a enviado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                XtraMessageBox.Show("Ajuste aplicado correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Guardado = True

            mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuImprimir1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            txtReferencia.Enabled = False
            cmbBodegaERP.Enabled = False
            cmbProductoFamilia.Enabled = False
            cmbPropietarioBodega.Enabled = False
            dtpFecha.Enabled = Modo = False
            cmdAdd.Visible = Modo = False
            mnuDel.Visible = Modo = False
            mnuDividir.Visible = Modo = False
            lcmbCentroCosto.Enabled = False

            dgrid.Enabled = (Modo = True)

            Set_Estado_Envio_A_ERP()

            Deshabilita_Grid()

            Llenar_DS_Rep()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Guardar_Ajuste_Positivo(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction)

        Dim ss As String = ""
        Dim cc, ic As Integer
        Dim Codigo As String, Lote As String, IdAjusteDet As Integer
        Dim Enviar As Boolean = False
        Dim IdBodegaERP As Integer
        Dim CantidadRegistrosEnviados As Integer = 0

        Try

            pBeTransAjustEnc.Referencia = txtReferencia.Text
            pBeTransAjustEnc.Fecha = dtpFecha.EditValue
            pBeTransAjustEnc.Fec_agr = dtpFecha.EditValue
            pBeTransAjustEnc.Fec_mod = dtpFecha.EditValue
            pBeTransAjustEnc.Idusuario = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.User_agr = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.User_mod = AP.UsuarioAp.IdUsuario
            '#CM_20200219: Se cambio el IdBodega en lugar de IdBodegaErp 
            pBeTransAjustEnc.IdBodega = AP.IdBodega
            pBeTransAjustEnc.IdProductoFamilia = cmbProductoFamilia.EditValue
            pBeTransAjustEnc.IdPropietarioBodega = cmbPropietarioBodega.EditValue
            pBeTransAjustEnc.Auditado = chkAuditado.Checked

            '#EJC20220330_2355_BYB: Guardar el centro de costo.
            If Not lcmbCentroCosto.EditValue Is Nothing Then
                pBeTransAjustEnc.IdCentroCosto = lcmbCentroCosto.EditValue
            End If

            cc = 0

            For I = 0 To lBeTransAjusteDet.Count - 1

                lBeTransAjusteDet(I).IdAjusteEnc = pBeTransAjustEnc.Idajusteenc : ic = 0

                If lBeTransAjusteDet(I).Lote_original <> lBeTransAjusteDet(I).Lote_nuevo Then ic += 1
                If lBeTransAjusteDet(I).Fecha_vence_original <> lBeTransAjusteDet(I).Fecha_vence_nueva Then ic += 1
                If lBeTransAjusteDet(I).Cantidad_original <> lBeTransAjusteDet(I).Cantidad_nueva Then ic += 1
                If lBeTransAjusteDet(I).Peso_original <> lBeTransAjusteDet(I).Peso_nuevo Then ic += 1
                If ic > 0 Then cc += 1

                '#EJC20180924: Asignación de bodega de ERP.                
                DgComboBodega = TryCast(dgrid.CurrentRow.Cells("ColBodega"), DataGridViewComboBoxCell)
                '#CM_20200219: Se cambio el IdBodega en lugar de IdBodegaErp
                IdBodegaERP = cmbBodegaERP.EditValue
                lBeTransAjusteDet(I).IdBodegaERP = IdBodegaERP

                '#GT13062022_1906: Asignación del tipoajuste seteado en el grid.     
                lBeTransAjusteDet(I).Idtipoajuste = dgrid.Rows(I).Cells("tipoajuste").Value

            Next

            For i = 0 To dgrid.Rows.Count - 1

                Codigo = IIf(IsDBNull(dgrid.Rows(i).Cells("ColCodigoProducto").Value), "", dgrid.Rows(i).Cells("ColCodigoProducto").Value)
                Lote = IIf(IsDBNull(dgrid.Rows(i).Cells("ColLote").Value), "", dgrid.Rows(i).Cells("ColLote").Value)
                IdAjusteDet = IIf(IsDBNull(dgrid.Rows(i).Cells("ColIdAjusteDEt").Value), 0, dgrid.Rows(i).Cells("ColIdAjusteDEt").Value)
                Enviar = IIf(IsDBNull(dgrid.Rows(i).Cells("ColEnviadoAErp").Value), False, dgrid.Rows(i).Cells("ColEnviadoAErp").Value)

                BeAjusteDet = lBeTransAjusteDet.Find(Function(x) x.Codigo_producto = Codigo _
                                                    AndAlso x.Lote_nuevo = Lote _
                                                    AndAlso x.IdAjusteDet = IdAjusteDet)

                '#GT28112024: al ser ajuste positivo, el idstock se obtiene hasta que se presiona guardar
                If Es_Ajuste_Positivo Then
                    BeAjusteDet.IdStock = pStock_Sin_Existencia_Previa.IdStock
                End If

                If Not BeAjusteDet Is Nothing Then
                    BeAjusteDet.Enviado = Enviar
                    If Enviar Then
                        CantidadRegistrosEnviados += 1
                    End If
                End If

            Next

            Crear_Movimientos_Positivos(lConnection, lTransaction)

            clsLnTrans_ajuste_enc.Aplicar_Ajuste(pBeTransAjustEnc, lBeTransAjusteDet, lBeTransMovimientos,
                                                                                      lConnection,
                                                                                      lTransaction)


            If CantidadRegistrosEnviados = lBeTransAjusteDet.Count Then
                If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.Idajusteenc, True, lConnection, lTransaction) > 0 Then
                    XtraMessageBox.Show("Se guardó el ajuste y se actualizó a enviado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                XtraMessageBox.Show("Ajuste aplicado correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Guardado = True

            mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuImprimir1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            txtReferencia.Enabled = False
            cmbBodegaERP.Enabled = False
            cmbProductoFamilia.Enabled = False
            cmbPropietarioBodega.Enabled = False
            dtpFecha.Enabled = Modo = False
            cmdAdd.Visible = Modo = False
            mnuDel.Visible = Modo = False
            mnuDividir.Visible = Modo = False
            lcmbCentroCosto.Enabled = False

            dgrid.Enabled = (Modo = True)

            Set_Estado_Envio_A_ERP()

            Deshabilita_Grid()

            Llenar_DS_Rep()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Actualizar_Ajuste()

        Dim ss As String = ""
        Dim cc, ic As Integer
        Dim Codigo As String, Lote As String, IdAjusteDet As Integer
        Dim Enviar As Boolean = False
        Dim IdBodegaERP As Integer
        Dim CantidadRegistrosEnviados As Integer = 0

        Try

            pBeTransAjustEnc.Referencia = txtReferencia.Text
            pBeTransAjustEnc.Fecha = dtpFecha.EditValue
            pBeTransAjustEnc.Fec_agr = dtpFecha.EditValue
            pBeTransAjustEnc.Fec_mod = dtpFecha.EditValue
            pBeTransAjustEnc.Idusuario = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.User_agr = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.User_mod = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.IdBodega = AP.IdBodega
            pBeTransAjustEnc.IdProductoFamilia = cmbProductoFamilia.EditValue
            pBeTransAjustEnc.IdPropietarioBodega = cmbPropietarioBodega.EditValue
            pBeTransAjustEnc.Auditado = chkAuditado.Checked

            '#EJC20220330_2355_BYB: Guardar el centro de costo.
            If Not lcmbCentroCosto.EditValue Is Nothing Then
                pBeTransAjustEnc.IdCentroCosto = lcmbCentroCosto.EditValue
            End If

            cc = 0

            For I = 0 To lBeTransAjusteDet.Count - 1

                lBeTransAjusteDet(I).IdAjusteEnc = pBeTransAjustEnc.Idajusteenc : ic = 0

                If lBeTransAjusteDet(I).Lote_original <> lBeTransAjusteDet(I).Lote_nuevo Then ic += 1
                If lBeTransAjusteDet(I).Fecha_vence_original <> lBeTransAjusteDet(I).Fecha_vence_nueva Then ic += 1
                If lBeTransAjusteDet(I).Cantidad_original <> lBeTransAjusteDet(I).Cantidad_nueva Then ic += 1
                If lBeTransAjusteDet(I).Peso_original <> lBeTransAjusteDet(I).Peso_nuevo Then ic += 1
                If ic > 0 Then cc += 1

                '#GT28112024: ejecutar código normal sino es positivo el ajuste.
                If Not Es_Ajuste_Positivo Then
                    '#EJC20180924: Asignación de bodega de ERP.                
                    DgComboBodega = TryCast(dgrid.Rows(I).Cells("ColBodega"), DataGridViewComboBoxCell)
                    IdBodegaERP = cmbBodegaERP.EditValue.Value
                    lBeTransAjusteDet(I).IdBodegaERP = IdBodegaERP
                End If



            Next

            For i = 0 To dgrid.Rows.Count - 1

                Codigo = IIf(IsDBNull(dgrid.Rows(i).Cells("ColCodigoProducto").Value), "", dgrid.Rows(i).Cells("ColCodigoProducto").Value)
                Lote = IIf(IsDBNull(dgrid.Rows(i).Cells("ColLote").Value), "", dgrid.Rows(i).Cells("ColLote").Value)
                IdAjusteDet = IIf(IsDBNull(dgrid.Rows(i).Cells("ColIdAjusteDEt").Value), 0, dgrid.Rows(i).Cells("ColIdAjusteDEt").Value)
                Enviar = IIf(IsDBNull(dgrid.Rows(i).Cells("ColEnviadoAErp").Value), False, dgrid.Rows(i).Cells("ColEnviadoAErp").Value)

                BeAjusteDet = lBeTransAjusteDet.Find(Function(x) x.Codigo_producto = Codigo _
                                                    AndAlso x.Lote_nuevo = Lote _
                                                    AndAlso x.IdAjusteDet = IdAjusteDet)

                '#GT28112024: al ser ajuste positivo, el idstock se obtiene hasta que se presiona guardar
                If Es_Ajuste_Positivo Then
                    BeAjusteDet.IdStock = pStock_Sin_Existencia_Previa.IdStock
                End If

                If Not BeAjusteDet Is Nothing Then

                    If Not BeConfig Is Nothing Then
                        If BeConfig.Interface_SAP Then
                            BeAjusteDet.Enviado = False
                        Else
                            BeAjusteDet.Enviado = Enviar
                        End If
                    Else
                        BeAjusteDet.Enviado = Enviar
                    End If

                    If Enviar Then
                        CantidadRegistrosEnviados += 1
                    End If
                End If

            Next

            clsLnTrans_ajuste_enc.Actualizar_Ajuste(lBeTransAjusteDet,
                                                     lBeTransMovimientos,
                                                     AP.IdEmpresa,
                                                     cmbBodegaERP.EditValue,
                                                     AP.UsuarioAp.IdUsuario)

            If CantidadRegistrosEnviados = lBeTransAjusteDet.Count Then
                If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.Idajusteenc, True) > 0 Then
                    XtraMessageBox.Show("Se actualizó el ajuste correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                XtraMessageBox.Show("Ajuste actualizado correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Guardado = True

            mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuImprimir1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            txtReferencia.Enabled = False
            cmbBodegaERP.Enabled = False
            cmbProductoFamilia.Enabled = False
            cmbPropietarioBodega.Enabled = False
            dtpFecha.Enabled = Modo = False
            cmdAdd.Visible = Modo = False
            mnuDel.Visible = Modo = False
            mnuDividir.Visible = Modo = False

            dgrid.Enabled = (Modo = True)

            Set_Estado_Envio_A_ERP()

            Deshabilita_Grid()

            Llenar_DS_Rep()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private pStock_Sin_Existencia_Previa As New clsBeStock
    Private Sub Guardar_Stock_por_Ajuste_Positivo()

        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            pStock_Sin_Existencia_Previa = New clsBeStock
            pStock_Sin_Existencia_Previa.IsNew = 1
            pStock_Sin_Existencia_Previa.IdPropietarioBodega = BeAjusteDet.IdPropietarioBodega
            pStock_Sin_Existencia_Previa.IdProductoBodega = BeAjusteDet.IdProductoBodega
            pStock_Sin_Existencia_Previa.IdUnidadMedida = BeAjusteDet.IdUnidadMedida
            pStock_Sin_Existencia_Previa.Fecha_Ingreso = Now
            pStock_Sin_Existencia_Previa.Fecha_vence = BeAjusteDet.Fecha_vence_nueva
            pStock_Sin_Existencia_Previa.IdPresentacion = BeAjusteDet.IdPresentacion
            pStock_Sin_Existencia_Previa.IdProductoEstado = BeAjusteDet.IdProductoEstado
            pStock_Sin_Existencia_Previa.ProductoEstado = New clsBeProducto_estado
            'pStock_Sin_Existencia_Previa.ProductoEstado = clsLnProducto_estado.Get_Single_By_IdEstado(BeAjusteDet.IdProductoEstado)
            pStock_Sin_Existencia_Previa.ProductoEstado = clsLnProducto_estado.GetSingleByIdEstado(BeAjusteDet.IdProductoEstado, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            pStock_Sin_Existencia_Previa.IdUbicacion = BeAjusteDet.IdUbicacion
            pStock_Sin_Existencia_Previa.IdUbicacion_anterior = BeAjusteDet.IdUbicacion
            pStock_Sin_Existencia_Previa.Cantidad = 1 '#GT28112024: al no existir stock se inserta con 1 por defecto
            pStock_Sin_Existencia_Previa.Lic_plate = BeAjusteDet.lic_plate
            pStock_Sin_Existencia_Previa.Lote = BeAjusteDet.Lote_nuevo
            pStock_Sin_Existencia_Previa.Peso = BeAjusteDet.Peso_nuevo
            pStock_Sin_Existencia_Previa.User_agr = AP.UsuarioAp.IdUsuario
            pStock_Sin_Existencia_Previa.Fec_agr = Now
            pStock_Sin_Existencia_Previa.User_mod = AP.UsuarioAp.IdUsuario
            pStock_Sin_Existencia_Previa.Fec_mod = Now
            pStock_Sin_Existencia_Previa.IdBodega = AP.IdBodega
            pStock_Sin_Existencia_Previa.Activo = 1

            '#GT02122024: se inserta primero el stock antes de reservarlo, y hacer el ajuste positivo
            If clsLnStock.Guardar_Stock_Ajuste_Positivo(pStock_Sin_Existencia_Previa,
                                                        clsTransaccion.lConnection,
                                                        clsTransaccion.lTransaction) Then

                If Reservar_Stock(pStock_Sin_Existencia_Previa.IdStock, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then

                    Guardar_Ajuste_Positivo(clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    Incrementar_Licencia_BOF_By_Ajuste_Positivo(AP.IdBodega, AP.UsuarioAp.IdUsuario, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                End If

            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            clsTransaccion.Close_Conection()
        End Try
    End Sub

    Private Sub Llena_Bodegas_ERP_Grid(ByVal pIndex As Integer, Optional ByVal pIdBodegaERP As Integer = 0,
                              Optional ByVal lConnection As SqlConnection = Nothing,
                              Optional ByVal lTransaction As SqlTransaction = Nothing)

        Dim vTransaccionRemota As Boolean = (lConnection IsNot Nothing AndAlso lTransaction IsNot Nothing)

        Try

            DgComboBodega = TryCast(dgrid.Rows(pIndex).Cells("ColBodega"), DataGridViewComboBoxCell)
            DgComboBodega.DropDownWidth = 200

            Dim DT As New DataTable

            If vTransaccionRemota Then
                DT = clsLnCliente.Get_All_By_IdEmpresa_For_Combo_Trans(AP.IdEmpresa, False, lConnection, lTransaction)
            Else
                DT = clsLnCliente.Get_All_By_IdEmpresa_For_Combo(AP.IdEmpresa)
            End If


            If DT.Rows.Count > 0 Then
                DgComboBodega.DisplayMember = "Nombre"
                DgComboBodega.ValueMember = "IdCliente"
                DgComboBodega.DataSource = DT
            End If

            If DgComboBodega.Items.Count > 0 Then
                DgComboBodega.Value = DT.Rows(0).Item("IdCliente")
            Else
                '#EJC20171024_1136PM:Corrección para cuando se cambia a un código de producto que no tiene stock y por lo tanto no tiene estado.
                DgComboBodega.Value = Nothing
            End If

            If pIdBodegaERP <> 0 Then
                If DgComboBodega.Items.Count > 0 Then
                    If Not pIdBodegaERP = -1 Then
                        DgComboBodega.Value = pIdBodegaERP
                    Else
                        DgComboBodega.Value = Nothing
                    End If
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Get_Cantidad_No_Enviados() As Integer

        Get_Cantidad_No_Enviados = 0

        Try

            Dim Codigo As String, Lote As String, IdAjusteDet As Integer
            Dim BeAjusteDet As New clsBeTrans_ajuste_det
            Dim Enviar As Boolean = False

            dgrid.EndEdit(DataGridViewDataErrorContexts.Commit)

            For i = 0 To dgrid.Rows.Count - 1

                Codigo = IIf(IsDBNull(dgrid.Rows(i).Cells("ColCodigoProducto").Value), "", dgrid.Rows(i).Cells("ColCodigoProducto").Value)
                Lote = IIf(IsDBNull(dgrid.Rows(i).Cells("ColLote").Value), "", dgrid.Rows(i).Cells("ColLote").Value)
                IdAjusteDet = IIf(IsDBNull(dgrid.Rows(i).Cells("ColIdAjusteDEt").Value), 0, dgrid.Rows(i).Cells("ColIdAjusteDEt").Value)
                Enviar = IIf(IsDBNull(dgrid.Rows(i).Cells("ColEnviadoAErp").Value), False, dgrid.Rows(i).Cells("ColEnviadoAErp").Value)

                If Not Enviar Then
                    Get_Cantidad_No_Enviados += 1
                End If

            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub mnuEstadoEnviadoAERP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEstadoEnviadoAERP.ItemClick
        Marcar_Ajuste_Enviado_A_ERP()
    End Sub

    Private Sub Marcar_Ajuste_Enviado_A_ERP()

        Try

            If pBeTransAjustEnc.Enviado_A_ERP Then

                Dim MarcadosParaReenviar As Integer = Get_Cantidad_No_Enviados()

                If MarcadosParaReenviar > 0 Then

                    If XtraMessageBox.Show(String.Format("¿Está seguro de cambiar a pendiente de envío {0} registros del ajuste?", MarcadosParaReenviar), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        pBeTransAjustEnc.Enviado_A_ERP = False

                        Dim Codigo As String, Lote As String, IdAjusteDet As Integer
                        Dim BeAjusteDet As New clsBeTrans_ajuste_det
                        Dim Enviar As Boolean = False

                        For i = 0 To dgrid.Rows.Count - 1

                            Codigo = IIf(IsDBNull(dgrid.Rows(i).Cells("ColCodigoProducto").Value), "", dgrid.Rows(i).Cells("ColCodigoProducto").Value)
                            Lote = IIf(IsDBNull(dgrid.Rows(i).Cells("ColLote").Value), "", dgrid.Rows(i).Cells("ColLote").Value)
                            IdAjusteDet = IIf(IsDBNull(dgrid.Rows(i).Cells("ColIdAjusteDEt").Value), 0, dgrid.Rows(i).Cells("ColIdAjusteDEt").Value)
                            Enviar = IIf(IsDBNull(dgrid.Rows(i).Cells("ColEnviadoAErp").Value), False, dgrid.Rows(i).Cells("ColEnviadoAErp").Value)

                            BeAjusteDet = lBeTransAjusteDet.Find(Function(x) x.Codigo_producto = Codigo _
                                                                 AndAlso x.Lote_nuevo = Lote AndAlso x.IdAjusteDet = IdAjusteDet)

                            If Not BeAjusteDet Is Nothing Then
                                If BeAjusteDet.Enviado Then
                                    clsLnTrans_ajuste_det.Actualizar_Estado_Enviado_A_ERP(BeAjusteDet.IdAjusteDet, Enviar)
                                End If
                            End If

                        Next

                        If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.Idajusteenc, pBeTransAjustEnc.Enviado_A_ERP) > 0 Then

                            Cargar_Datos()

                            XtraMessageBox.Show("Se actualizó el ajuste a pendiente de envío", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        End If

                    End If

                Else
                    XtraMessageBox.Show("No se ha desmarcado ningún registro para reenvío", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Else

                If XtraMessageBox.Show("¿Está seguro de cambiar el estado del ajuste a enviado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    pBeTransAjustEnc.Enviado_A_ERP = True

                    Dim Codigo As String, Lote As String, IdAjusteDet As Integer
                    Dim BeAjusteDet As New clsBeTrans_ajuste_det

                    For i = 0 To dgrid.Rows.Count - 1

                        Codigo = dgrid.Rows(i).Cells("ColCodigoProducto").Value
                        Lote = dgrid.Rows(i).Cells("ColLote").Value
                        IdAjusteDet = dgrid.Rows(i).Cells("ColIdAjusteDEt").Value

                        BeAjusteDet = lBeTransAjusteDet.Find(Function(x) x.Codigo_producto = Codigo _
                                                   AndAlso x.Lote_nuevo = Lote AndAlso x.IdAjusteDet = IdAjusteDet)

                        If Not BeAjusteDet Is Nothing Then

                            If Not BeAjusteDet.Enviado Then
                                clsLnTrans_ajuste_det.Actualizar_Estado_Enviado_A_ERP(BeAjusteDet.IdAjusteDet, True)
                            End If
                        End If

                    Next

                    If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.Idajusteenc, pBeTransAjustEnc.Enviado_A_ERP) > 0 Then

                        Cargar_Datos()

                        XtraMessageBox.Show("Se actualizó el ajuste a pendiente de envío", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub Crear_Movimientos()

        Try

            Dim BeStock As New clsBeStock
            Dim BeMov As clsBeTrans_movimientos
            Dim IdMovimiento As Integer

            lBeTransMovimientos.Clear()

            IdMovimiento = clsLnTrans_movimientos.MaxID()

            For Each item As clsBeTrans_ajuste_det In lBeTransAjusteDet

                'If (item.Cantidad_original <> item.Cantidad_nueva) Or (item.Peso_original <> item.Peso_nuevo) Then

                BeStock.IdStock = item.IdStock

                clsLnStock.GetSingle(BeStock)

                BeMov = New clsBeTrans_movimientos
                BeMov.IdMovimiento = IdMovimiento
                BeMov.IdEmpresa = AP.IdEmpresa
                BeMov.IdBodegaOrigen = AP.IdBodega
                BeMov.IdTransaccion = item.IdAjusteEnc
                BeMov.IdPropietarioBodega = BeStock.IdPropietarioBodega
                BeMov.IdProductoBodega = BeStock.IdProductoBodega
                BeMov.IdUbicacionOrigen = BeStock.IdUbicacion
                BeMov.IdUbicacionDestino = BeStock.IdUbicacion
                BeMov.IdPresentacion = BeStock.IdPresentacion
                BeMov.IdEstadoOrigen = BeStock.IdProductoEstado
                BeMov.IdEstadoDestino = BeStock.IdProductoEstado

                BeMov.IdUnidadMedida = BeStock.IdUnidadMedida

                '#CKFK 20180618 Se habilitó esta opción porque ahora esta tarea ya no se guarda como AJUS sino como AJUSN y AJUSP
                If item.Idtipoajuste = 3 Then 'Ajuste positivo
                    BeMov.IdTipoTarea = 13
                ElseIf item.Idtipoajuste = 5 Then 'Ajuste negativo
                    BeMov.IdTipoTarea = 17
                ElseIf item.Idtipoajuste = 4 Then 'Ajuste peso
                    BeMov.IdTipoTarea = 14
                ElseIf item.Idtipoajuste = 2 Then 'Ajuste vencimiento
                    BeMov.IdTipoTarea = 15
                    BeMov.IdTipoTarea = 13 '-> Se ajustó por lote, pero debe generar un mov. positivo. #EJC20180622
                ElseIf item.Idtipoajuste = 1 Then 'Ajuste lote
                    BeMov.IdTipoTarea = 16
                    BeMov.IdTipoTarea = 13 '-> Se ajustó por lote, pero debe generar un mov. positivo. #EJC20180622
                Else
                    BeMov.IdTipoTarea = 7
                End If

                BeMov.IdBodegaDestino = AP.IdBodega
                BeMov.IdRecepcion = BeStock.IdRecepcionEnc
                BeMov.IdRecepcionDet = BeStock.IdRecepcionDet
                BeMov.Serie = BeStock.Serial
                BeMov.Lote = item.Lote_nuevo
                BeMov.Fecha_vence = item.Fecha_vence_nueva
                BeMov.Fecha = Now
                BeMov.Barra_pallet = ""
                BeMov.Hora_ini = Now
                BeMov.Hora_fin = Now
                BeMov.Fecha_agr = Now
                BeMov.Usuario_agr = AP.UsuarioAp.IdUsuario
                BeMov.Barra_pallet = BeStock.Lic_plate

                If (item.Cantidad_original <> item.Cantidad_nueva) Then

                    If item.Idtipoajuste = 3 Then
                        BeMov.Cantidad = item.Cantidad_nueva - item.Cantidad_original
                    ElseIf item.Idtipoajuste = 5 Then
                        BeMov.Cantidad = item.Cantidad_original - item.Cantidad_nueva
                    End If

                    BeMov.Cantidad_hist = item.Cantidad_original
                Else
                    BeMov.Cantidad = item.Cantidad_original
                    BeMov.Cantidad_hist = item.Cantidad_original
                End If

                If (item.Peso_original <> item.Peso_nuevo) Then
                    BeMov.Peso = item.Peso_nuevo
                    BeMov.Peso_hist = item.Peso_original
                End If

                '#EJC20211220: Si el ajuste es en presentación, enviar el movimiento en la cantidad multiplicado por el factor, es decir en umbas.
                Dim BePresentacion As New clsBeProducto_Presentacion

                If BeMov.IdPresentacion <> 0 Then

                    BePresentacion = clsLnProducto_presentacion.GetSingle(BeMov.IdPresentacion)

                    If Not BePresentacion Is Nothing Then
                        BeMov.Cantidad = Math.Round(BeMov.Cantidad * BePresentacion.Factor, 6)
                    End If

                End If

                lBeTransMovimientos.Add(BeMov) : IdMovimiento += 1

                If item.Idtipoajuste = 1 OrElse item.Idtipoajuste = 2 Then 'Ajuste lote
                    Crear_Movimiento_Salida_Lote_Origen(IdMovimiento, item.IdAjusteEnc, item.IdStock, BeMov.Cantidad, BeMov.Peso)
                    IdMovimiento += 1
                End If

            Next

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Crear_Movimientos_Positivos(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction)

        Try

            Dim BeStock As New clsBeStock
            Dim BeMov As clsBeTrans_movimientos
            Dim IdMovimiento As Integer

            lBeTransMovimientos.Clear()

            IdMovimiento = clsLnTrans_movimientos.MaxID(lConnection, lTransaction)

            For Each item As clsBeTrans_ajuste_det In lBeTransAjusteDet

                'If (item.Cantidad_original <> item.Cantidad_nueva) Or (item.Peso_original <> item.Peso_nuevo) Then

                BeStock.IdStock = item.IdStock

                BeStock = clsLnStock.GetSingle(BeStock.IdStock,
                                     lConnection,
                                     lTransaction)

                BeMov = New clsBeTrans_movimientos
                BeMov.IdMovimiento = IdMovimiento
                BeMov.IdEmpresa = AP.IdEmpresa
                BeMov.IdBodegaOrigen = AP.IdBodega
                BeMov.IdTransaccion = item.IdAjusteEnc
                BeMov.IdPropietarioBodega = BeStock.IdPropietarioBodega
                BeMov.IdProductoBodega = BeStock.IdProductoBodega
                BeMov.IdUbicacionOrigen = BeStock.IdUbicacion
                BeMov.IdUbicacionDestino = BeStock.IdUbicacion
                BeMov.IdPresentacion = BeStock.IdPresentacion
                BeMov.IdEstadoOrigen = BeStock.IdProductoEstado
                BeMov.IdEstadoDestino = BeStock.IdProductoEstado

                BeMov.IdUnidadMedida = BeStock.IdUnidadMedida

                '#CKFK 20180618 Se habilitó esta opción porque ahora esta tarea ya no se guarda como AJUS sino como AJUSN y AJUSP
                If item.Idtipoajuste = 3 Then 'Ajuste positivo
                    BeMov.IdTipoTarea = 13
                ElseIf item.Idtipoajuste = 5 Then 'Ajuste negativo
                    BeMov.IdTipoTarea = 17
                ElseIf item.Idtipoajuste = 4 Then 'Ajuste peso
                    BeMov.IdTipoTarea = 14
                ElseIf item.Idtipoajuste = 2 Then 'Ajuste vencimiento
                    BeMov.IdTipoTarea = 15
                    BeMov.IdTipoTarea = 13 '-> Se ajustó por lote, pero debe generar un mov. positivo. #EJC20180622
                ElseIf item.Idtipoajuste = 1 Then 'Ajuste lote
                    BeMov.IdTipoTarea = 16
                    BeMov.IdTipoTarea = 13 '-> Se ajustó por lote, pero debe generar un mov. positivo. #EJC20180622
                Else
                    BeMov.IdTipoTarea = 7
                End If

                BeMov.IdBodegaDestino = AP.IdBodega
                BeMov.IdRecepcion = BeStock.IdRecepcionEnc
                BeMov.IdRecepcionDet = BeStock.IdRecepcionDet
                BeMov.Serie = BeStock.Serial
                BeMov.Lote = item.Lote_nuevo
                BeMov.Fecha_vence = item.Fecha_vence_nueva
                BeMov.Fecha = Now
                BeMov.Barra_pallet = ""
                BeMov.Hora_ini = Now
                BeMov.Hora_fin = Now
                BeMov.Fecha_agr = Now
                BeMov.Usuario_agr = AP.UsuarioAp.IdUsuario
                BeMov.Barra_pallet = BeStock.Lic_plate

                If (item.Cantidad_original <> item.Cantidad_nueva) Then

                    If item.Idtipoajuste = 3 Then
                        BeMov.Cantidad = item.Cantidad_nueva - item.Cantidad_original
                    ElseIf item.Idtipoajuste = 5 Then
                        BeMov.Cantidad = item.Cantidad_original - item.Cantidad_nueva
                    End If

                    BeMov.Cantidad_hist = item.Cantidad_original
                Else
                    BeMov.Cantidad = item.Cantidad_original
                    BeMov.Cantidad_hist = item.Cantidad_original
                End If

                If (item.Peso_original <> item.Peso_nuevo) Then
                    BeMov.Peso = item.Peso_nuevo
                    BeMov.Peso_hist = item.Peso_original
                End If

                '#EJC20211220: Si el ajuste es en presentación, enviar el movimiento en la cantidad multiplicado por el factor, es decir en umbas.
                Dim BePresentacion As New clsBeProducto_Presentacion

                If BeMov.IdPresentacion <> 0 Then

                    BePresentacion = clsLnProducto_presentacion.GetSingle(BeMov.IdPresentacion, lConnection, lTransaction)

                    If Not BePresentacion Is Nothing Then
                        BeMov.Cantidad = Math.Round(BeMov.Cantidad * BePresentacion.Factor, 6)
                    End If

                End If

                lBeTransMovimientos.Add(BeMov) : IdMovimiento += 1

                If item.Idtipoajuste = 1 OrElse item.Idtipoajuste = 2 Then 'Ajuste lote
                    Crear_Movimiento_Salida_Lote_Origen_Ajuste_Positivo(IdMovimiento, item.IdAjusteEnc, item.IdStock, BeMov.Cantidad, BeMov.Peso, lConnection, lTransaction)
                    IdMovimiento += 1
                End If

            Next

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Crear_Movimiento_Salida_Lote_Origen(ByVal IdMovimiento As Integer,
                                                    ByVal IdAjusteEnc As Integer,
                                                    ByVal IdStock As Integer,
                                                    ByVal Cantidad As Double,
                                                    ByVal Peso As Double)

        Try

            Dim BeStock As New clsBeStock
            Dim BeMov As clsBeTrans_movimientos

            BeStock.IdStock = IdStock
            clsLnStock.GetSingle(BeStock)

            BeMov = New clsBeTrans_movimientos
            BeMov.IdMovimiento = IdMovimiento
            BeMov.IdEmpresa = AP.IdEmpresa
            BeMov.IdBodegaOrigen = AP.IdBodega
            BeMov.IdTransaccion = IdAjusteEnc
            BeMov.IdPropietarioBodega = BeStock.IdPropietarioBodega
            BeMov.IdProductoBodega = BeStock.IdProductoBodega
            BeMov.IdUbicacionOrigen = BeStock.IdUbicacion
            BeMov.IdUbicacionDestino = BeStock.IdUbicacion
            BeMov.IdPresentacion = BeStock.IdPresentacion
            BeMov.IdEstadoOrigen = BeStock.IdProductoEstado
            BeMov.IdEstadoDestino = BeStock.IdProductoEstado

            BeMov.IdUnidadMedida = BeStock.IdUnidadMedida
            BeMov.IdTipoTarea = 17 'Ajuste negativo

            BeMov.IdBodegaDestino = AP.IdBodega
            BeMov.IdRecepcion = BeStock.IdRecepcionEnc
            BeMov.IdRecepcionDet = BeStock.IdRecepcionDet
            BeMov.Serie = BeStock.Serial
            BeMov.Lote = BeStock.Lote
            BeMov.Fecha_vence = BeStock.Fecha_vence
            BeMov.Fecha = Now
            BeMov.Barra_pallet = ""
            BeMov.Hora_ini = Now
            BeMov.Hora_fin = Now
            BeMov.Fecha_agr = Now
            BeMov.Usuario_agr = AP.UsuarioAp.IdUsuario
            BeMov.Cantidad = Cantidad
            BeMov.Peso = Peso
            lBeTransMovimientos.Add(BeMov)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#GT02122024: copia del metodo para guardar ajuste positivo para producto sin stock 
    Private Sub Crear_Movimiento_Salida_Lote_Origen_Ajuste_Positivo(ByVal IdMovimiento As Integer,
                                                                    ByVal IdAjusteEnc As Integer,
                                                                    ByVal IdStock As Integer,
                                                                    ByVal Cantidad As Double,
                                                                    ByVal Peso As Double,
                                                                    ByRef lConnection As SqlConnection,
                                                                    ByRef lTransaction As SqlTransaction)

        Try

            Dim BeStock As New clsBeStock
            Dim BeMov As clsBeTrans_movimientos

            BeStock.IdStock = IdStock
            BeStock = clsLnStock.GetSingle(BeStock.IdStock,
                                           lConnection,
                                           lTransaction)

            BeMov = New clsBeTrans_movimientos
            BeMov.IdMovimiento = IdMovimiento
            BeMov.IdEmpresa = AP.IdEmpresa
            BeMov.IdBodegaOrigen = AP.IdBodega
            BeMov.IdTransaccion = IdAjusteEnc
            BeMov.IdPropietarioBodega = BeStock.IdPropietarioBodega
            BeMov.IdProductoBodega = BeStock.IdProductoBodega
            BeMov.IdUbicacionOrigen = BeStock.IdUbicacion
            BeMov.IdUbicacionDestino = BeStock.IdUbicacion
            BeMov.IdPresentacion = BeStock.IdPresentacion
            BeMov.IdEstadoOrigen = BeStock.IdProductoEstado
            BeMov.IdEstadoDestino = BeStock.IdProductoEstado

            BeMov.IdUnidadMedida = BeStock.IdUnidadMedida
            BeMov.IdTipoTarea = 17 'Ajuste negativo

            BeMov.IdBodegaDestino = AP.IdBodega
            BeMov.IdRecepcion = BeStock.IdRecepcionEnc
            BeMov.IdRecepcionDet = BeStock.IdRecepcionDet
            BeMov.Serie = BeStock.Serial
            BeMov.Lote = BeStock.Lote
            BeMov.Fecha_vence = BeStock.Fecha_vence
            BeMov.Fecha = Now
            BeMov.Barra_pallet = ""
            BeMov.Hora_ini = Now
            BeMov.Hora_fin = Now
            BeMov.Fecha_agr = Now
            BeMov.Usuario_agr = AP.UsuarioAp.IdUsuario
            BeMov.Cantidad = Cantidad
            BeMov.Peso = Peso
            lBeTransMovimientos.Add(BeMov)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub


    Private Sub cmbBodegaERP_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodegaERP.EditValueChanged

        Try

            If Not cmbBodegaERP.EditValue Is Nothing AndAlso Not cmbBodegaERP.EditValue Is Nothing Then
                txtSerie.Text = clsLnCliente.Get_Referencia_By_IdCliente(cmbBodegaERP.EditValue)
            Else
                txtSerie.Text = ""
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

    Private Sub mnuReimpresionEtiquetas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReimpresionEtiquetas.ItemClick

        Try

            If dgrid.Rows.Count > 0 Then

                If dgrid.SelectedRows.Count > 0 Then

                    Dim Fila As DataGridViewRow = dgrid.SelectedRows(0)

                    If Fila IsNot Nothing Then

                        Dim ObjTransReDet As New clsBeTrans_re_det

                        ObjTransReDet.Codigo_Producto = lBeTransAjusteDet(Fila.Index).Codigo_producto
                        ObjTransReDet.cantidad_recibida = lBeTransAjusteDet(Fila.Index).Cantidad_nueva

                        ObjTransReDet.Lic_plate = lBeTransAjusteDet(Fila.Index).lic_plate
                        ObjTransReDet.Nombre_producto = lBeTransAjusteDet(Fila.Index).Nombre_producto
                        ObjTransReDet.Lote = lBeTransAjusteDet(Fila.Index).Lote_nuevo
                        ObjTransReDet.Fecha_vence = lBeTransAjusteDet(Fila.Index).Fecha_vence_nueva
                        ObjTransReDet.IdPresentacion = lBeTransAjusteDet(Fila.Index).IdPresentacion

                        If ObjTransReDet.IdPresentacion <> 0 Then

                            ObjTransReDet.Presentacion.IdPresentacion = ObjTransReDet.IdPresentacion
                            clsLnProducto_presentacion.GetSingle(ObjTransReDet.Presentacion)

                            If Not ObjTransReDet.Presentacion Is Nothing Then
                                If Not ObjTransReDet.Presentacion.Factor = 0 Then
                                    ObjTransReDet.cantidad_recibida = lBeTransAjusteDet(Fila.Index).Cantidad_nueva / ObjTransReDet.Presentacion.Factor
                                End If
                            End If

                        End If

                        Dim pd As PrintDialog = New PrintDialog()
                        pd.PrinterSettings = New PrinterSettings()

                        If ObjTransReDet IsNot Nothing Then
                            Dim Impresion As New frmImpresionRecepcion
                            Impresion.pTransReDet = ObjTransReDet
                            Impresion.ShowDialog()
                            Impresion.Dispose()
                        Else
                            XtraMessageBox.Show("No se cargó el producto para reimpresión.", "Impresión BOF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    End If

                Else
                    XtraMessageBox.Show("No hay una fila seleccionada para reimpresión.", "Impresión BOF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                XtraMessageBox.Show("No hay registros para reimpresión.", "Impresión BOF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub RibbonControl_Click(sender As Object, e As EventArgs) Handles RibbonControl.Click

    End Sub

    Private Sub Llenar_DS_Rep()

        Dim lRow As DataRow
        Dim repAjuste As New rptAjuste
        Dim DsRepAjustes As New dsRepAjustes

        Try

            DsRepAjustes.trans_ajuste_det.Clear()

            For i = 0 To dgrid.Rows.Count - 1

                lRow = DsRepAjustes.trans_ajuste_det.NewRow

                With lRow

                    .Item("Codigo") = dgrid.Rows(i).Cells("ColCodigoProducto").Value
                    .Item("Producto") = dgrid.Rows(i).Cells("ColNombreProducto").Value

                    If dgrid.Rows(i).Cells("colUbicacion").Value IsNot Nothing Then
                        .Item("Ubicacion") = dgrid.Rows(i).Cells("colUbicacion").Value.ToString.Substring(IIf(dgrid.Rows(i).Cells("colUbicacion").Value.ToString.IndexOf("#") = -1, 0, dgrid.Rows(i).Cells("colUbicacion").Value.ToString.IndexOf("#") + 1))
                    End If

                    .Item("Motivo") = dgrid.Rows(i).Cells("motivoajuste").EditedFormattedValue
                    .Item("Tipo") = dgrid.Rows(i).Cells("tipoajuste").EditedFormattedValue

                    Select Case .Item("Tipo")
                        Case "Ajuste Lote."
                            .Item("ValorAnterior") = dgrid.Rows(i).Cells("ColLote").Value
                            .Item("ValorActual") = dgrid.Rows(i).Cells("ColCantidad").Value
                        Case "Ajuste Vencimiento"
                            .Item("ValorAnterior") = Format(dgrid.Rows(i).Cells("CantidadP").Value, "dd-MM-yyyy")
                            .Item("ValorActual") = Format(dgrid.Rows(i).Cells("ColCantidad").Value, "dd-MM-yyyy")

                            '#CKFK 20211217 Corrección para evitar que ponga esto "dd-MM-yyyy" 
                            If .Item("ValorActual") = "dd-MM-yyyy" Then
                                .Item("ValorActual") = dgrid.Rows(i).Cells("ColCantidad").Value
                            End If
                        Case "Ajuste Positivo"
                            .Item("ValorAnterior") = dgrid.Rows(i).Cells("CantidadP").Value
                            .Item("ValorActual") = dgrid.Rows(i).Cells("CantidadP").Value + dgrid.Rows(i).Cells("ColCantidad").Value
                        Case "Ajuste Peso"
                            .Item("ValorAnterior") = dgrid.Rows(i).Cells("CantidadP").Value
                            .Item("ValorActual") = dgrid.Rows(i).Cells("ColCantidad").Value
                        Case "Ajuste Negativo"
                            .Item("ValorAnterior") = dgrid.Rows(i).Cells("CantidadP").Value
                            .Item("ValorActual") = dgrid.Rows(i).Cells("CantidadP").Value - dgrid.Rows(i).Cells("ColCantidad").Value
                    End Select

                    .Item("Observacion") = dgrid.Rows(i).Cells("ColObservacion").Value
                    .Item("Lote") = dgrid.Rows(i).Cells(10).Value
                    .Item("LicPlate") = dgrid.Rows(i).Cells("ColLicPlate").Value

                End With

                DsRepAjustes.trans_ajuste_det.Addtrans_ajuste_detRow(lRow)

            Next

            repAjuste.DataSource = DsRepAjustes
            repAjuste.Parameters("Referencia").Value = pBeTransAjustEnc.Referencia
            repAjuste.Parameters("Bodega").Value = cmbBodegaERP.Text
            repAjuste.Parameters("Documento").Value = pBeTransAjustEnc.Idajusteenc
            repAjuste.Parameters("Fecha").Value = pBeTransAjustEnc.Fecha
            repAjuste.Parameters("Usuario").Value = String.Format("{0} - {1} {2}", AP.UsuarioAp.Codigo, AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
            repAjuste.RequestParameters = False
            Dim tool As ReportPrintTool = New ReportPrintTool(repAjuste)
            tool.PreviewForm.WindowState = FormWindowState.Maximized
            tool.ShowPreview()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private IsLoading As Boolean = False

    'Private Sub chkAuditado_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkAuditado.CheckedChanged

    '    Try

    '        If IsLoading Then Exit Sub

    '        If Not pBeTransAjustEnc.Enviado_A_ERP Then

    '            If chkAuditado.Checked Then

    '                If XtraMessageBox.Show("¿El ajuste está listo para ser enviado a ERP?", Text,
    '                                        MessageBoxButtons.YesNo,
    '                                        MessageBoxIcon.Information) = DialogResult.Yes Then

    '                    Dim vResult As Integer = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(pBeTransAjustEnc.Idajusteenc, True)

    '                    If Not vResult = 0 Then
    '                        XtraMessageBox.Show("El ajuste fue auditado y está listo para enviarse a ERP.",
    '                                            Text,
    '                                            MessageBoxButtons.OK,
    '                                            MessageBoxIcon.Information)

    '                        If AP.IdConfiguracionInterface <> -1 Then

    '                            Dim BeINavConfig As New clsBeI_nav_config_enc
    '                            BeINavConfig = clsLnI_nav_config_enc.GetSingle(AP.IdConfiguracionInterface)

    '                            Dim vArgumentosAEnviarAInterface As String = ""

    '                            If Not BeINavConfig Is Nothing Then

    '                                vArgumentosAEnviarAInterface = "20-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBeTransAjustEnc.Idajusteenc & "-0" & "-" & clsBD.Instancia.NombreInstancia
    '                                Ejecutar_Interface(vArgumentosAEnviarAInterface, Me)

    '                            End If

    '                        End If

    '                        If Not InvokeListarAjustes Is Nothing Then
    '                            InvokeListarAjustes.Invoke
    '                        End If

    '                        Close()

    '                    End If

    '                End If

    '            Else

    '                If Not pBeTransAjustEnc.Enviado_A_ERP Then

    '                    If chkAuditado.Checked Then

    '                        If XtraMessageBox.Show("¿El ajuste está listo para ser enviado a ERP?", Text,
    '                                        MessageBoxButtons.YesNo,
    '                                        MessageBoxIcon.Information) = DialogResult.Yes Then

    '                            Dim vResult As Integer = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(pBeTransAjustEnc.Idajusteenc, True)

    '                            If Not vResult = 0 Then
    '                                XtraMessageBox.Show("El ajuste fue auditado y está listo para enviarse a ERP.",
    '                                                Text,
    '                                                MessageBoxButtons.OK,
    '                                                MessageBoxIcon.Information)

    '                                If Not InvokeListarAjustes Is Nothing Then
    '                                    InvokeListarAjustes.Invoke
    '                                End If

    '                                Close()

    '                            End If

    '                        End If

    '                    Else

    '                        Dim vResult As Integer = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(pBeTransAjustEnc.Idajusteenc, False)

    '                        If Not vResult = 0 Then

    '                            XtraMessageBox.Show("El ajuste se marchó como no auditado ya no podrá enviarse a ERP.",
    '                                                Text,
    '                                                MessageBoxButtons.OK,
    '                                                MessageBoxIcon.Information)

    '                            If Not InvokeListarAjustes Is Nothing Then
    '                                InvokeListarAjustes.Invoke
    '                            End If

    '                            Close()

    '                        End If

    '                    End If

    '                Else
    '                    XtraMessageBox.Show("El ajuste ya fue enviado al ERP, no se puede deshabilitar su auditoría.",
    '                                Text,
    '                                MessageBoxButtons.OK,
    '                                MessageBoxIcon.Exclamation)
    '                End If


    '            End If

    '        End If

    '    Catch ex As Exception

    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '                            Text,
    '                            MessageBoxButtons.OK,
    '                            MessageBoxIcon.Error)

    '        Dim vMsgError As String = ex.Message
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)

    '    End Try

    'End Sub


    Private Sub chkAuditado_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkAuditado.CheckedChanged

        Try

            If IsLoading Then Exit Sub

            If Not pBeTransAjustEnc.Enviado_A_ERP Then

                If chkAuditado.Checked Then

                    If XtraMessageBox.Show("¿El ajuste está listo para ser enviado a ERP?", Text,
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Information) = DialogResult.Yes Then

                        Dim vResult As Integer

                        If pBeTransAjustEnc.Ajuste_Por_Inventario > 0 Then

                            Dim vListaAjustesInv As New List(Of Integer)
                            vListaAjustesInv = clsLnTrans_ajuste_enc.Get_All_By_IdInventarioEnc(pBeTransAjustEnc.Ajuste_Por_Inventario)

                            For Each inv In vListaAjustesInv
                                vResult = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(inv, True)
                            Next

                        Else
                            vResult = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(pBeTransAjustEnc.Idajusteenc, True)
                        End If

                        If Not vResult = 0 Then
                            XtraMessageBox.Show("El ajuste fue auditado y está listo para enviarse a ERP.",
                                                Text,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information)

                            If AP.IdConfiguracionInterface <> -1 Then

                                Dim BeINavConfig As New clsBeI_nav_config_enc
                                BeINavConfig = clsLnI_nav_config_enc.GetSingle(AP.IdConfiguracionInterface)

                                Dim vArgumentosAEnviarAInterface As String = ""

                                If Not BeINavConfig Is Nothing Then

                                    vArgumentosAEnviarAInterface = "20-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBeTransAjustEnc.Idajusteenc & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                    Ejecutar_Interface(vArgumentosAEnviarAInterface, Me)

                                End If

                            End If

                            If Not InvokeListarAjustes Is Nothing Then
                                InvokeListarAjustes.Invoke
                            End If

                            Close()

                        End If

                    End If

                Else

                    If Not pBeTransAjustEnc.Enviado_A_ERP Then

                        If chkAuditado.Checked Then

                            If XtraMessageBox.Show("¿El ajuste está listo para ser enviado a ERP?", Text,
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Information) = DialogResult.Yes Then

                                Dim vResult As Integer
                                If pBeTransAjustEnc.Ajuste_Por_Inventario > 0 Then

                                    Dim vListaAjustesInv As New List(Of Integer)
                                    vListaAjustesInv = clsLnTrans_ajuste_enc.Get_All_By_IdInventarioEnc(pBeTransAjustEnc.Ajuste_Por_Inventario)

                                    For Each inv In vListaAjustesInv
                                        vResult = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(inv, True)
                                    Next

                                Else
                                    vResult = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(pBeTransAjustEnc.Idajusteenc, True)
                                End If

                                If Not vResult = 0 Then
                                    XtraMessageBox.Show("El ajuste fue auditado y está listo para enviarse a ERP.",
                                                    Text,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information)

                                    If Not InvokeListarAjustes Is Nothing Then
                                        InvokeListarAjustes.Invoke
                                    End If

                                    Close()

                                End If

                            End If

                        Else

                            Dim vResult As Integer
                            If pBeTransAjustEnc.Ajuste_Por_Inventario > 0 Then

                                Dim vListaAjustesInv As New List(Of Integer)
                                vListaAjustesInv = clsLnTrans_ajuste_enc.Get_All_By_IdInventarioEnc(pBeTransAjustEnc.Ajuste_Por_Inventario)

                                For Each inv In vListaAjustesInv
                                    vResult = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(inv, False)
                                Next

                            Else
                                vResult = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(pBeTransAjustEnc.Idajusteenc, False)
                            End If

                            If Not vResult = 0 Then

                                XtraMessageBox.Show("El ajuste se marcó como no auditado ya no podrá enviarse a ERP.",
                                                    Text,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information)

                                If Not InvokeListarAjustes Is Nothing Then
                                    InvokeListarAjustes.Invoke
                                End If

                                Close()

                            End If

                        End If

                    Else
                        XtraMessageBox.Show("El ajuste ya fue enviado al ERP, no se puede deshabilitar su auditoría.",
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                    End If


                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmAjusteStock_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not Guardado Then
                If XtraMessageBox.Show("No ha guardado el ajuste. ¿Salir sin guardar?", Text,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Error) = DialogResult.Yes Then
                    clsLnTrans_ajuste_enc.RollBackStockRes(pBeTransAjustEnc)
                Else
                    e.Cancel = True
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

    Private Sub mnuAjustePositivo_Click(sender As Object, e As EventArgs) Handles mnuAjustePositivo.Click

        Try
            dgrid.EndEdit()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

        Try
            If dgrid.IsCurrentCellInEditMode Then commitValue()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

        Try
            Es_Ajuste_Positivo = True

            If pIdPropietarioBodega <= 0 Then
                Throw New Exception("Debe seleccionar un propietario.")
            End If

            '#GT28112024: cargar producto sin stock asociado
            Dim frmAjuste As New frmAjustePositivo() With {
                     .pStockIdPropietarioBodega = pIdPropietarioBodega,
                     .pStockIdPropietario = pIdPropietario,
                     .pStockBodegaFiltro = AP.IdBodega,
                     .StartPosition = FormStartPosition.CenterParent,
                     .WindowState = FormWindowState.Normal
                }
            frmAjuste.ShowDialog()

            If frmAjuste.DialogResult = DialogResult.OK Then

                If frmAjuste.vProducto IsNot Nothing AndAlso frmAjuste.pStockTemporal IsNot Nothing Then
                    Cargar_producto_Sin_Stock(frmAjuste.vProducto, frmAjuste.pStockTemporal)
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub Cargar_producto_Sin_Stock(ByVal vProductoSinStock As clsBeProducto, ByVal pStockTemporal As clsBeStock)
        Try

            '#GT22112024: se valida al final porque aun no tenemos stock
            'Reservar_Stock(Stock.pObjStock.IdStock)

            Dim st As New clsBeVW_stock_res
            Dim ubic, codigo As String
            Dim rc As Integer
            Dim pTipoAjuste As Integer = 0
            Dim IdProducto As Integer = 0
            Dim BeProducto As New clsBeProducto

            IdProducto = vProductoSinStock.IdProducto
            codigo = vProductoSinStock.Codigo
            BeProducto = vProductoSinStock

            '#GT22112024: obtener ubicacion por defecto recepcion
            ubic = AP.Bodega.Ubic_recepcion

            BeAjusteDet = New clsBeTrans_ajuste_det
            BeAjusteDet.IdAjusteDet = 0
            BeAjusteDet.IdAjusteEnc = pBeTransAjustEnc.Idajusteenc
            BeAjusteDet.IdStock = 0 'pendiente
            BeAjusteDet.IdPropietarioBodega = pIdPropietarioBodega
            BeAjusteDet.IdProductoBodega = vProductoSinStock.IdProductoBodega
            BeAjusteDet.IdProductoEstado = pStockTemporal.IdProductoEstado
            BeAjusteDet.IdPresentacion = pStockTemporal.IdPresentacion
            BeAjusteDet.IdUnidadMedida = vProductoSinStock.IdUnidadMedidaBasica
            BeAjusteDet.IdUbicacion = pStockTemporal.IdUbicacion

            If BeAjusteDet.IdPresentacion <> 0 Then
                BeAjusteDet.Presentacion = clsLnProducto_presentacion.GetSingle(BeAjusteDet.IdPresentacion)
            End If

            BeAjusteDet.Lote_original = pStockTemporal.Lote
            BeAjusteDet.Lote_nuevo = pStockTemporal.Lote
            BeAjusteDet.Fecha_vence_original = pStockTemporal.Fecha_vence
            BeAjusteDet.Fecha_vence_nueva = pStockTemporal.Fecha_vence
            BeAjusteDet.Peso_original = pStockTemporal.Peso
            BeAjusteDet.Peso_nuevo = pStockTemporal.Peso
            BeAjusteDet.Cantidad_original = pStockTemporal.Cantidad
            BeAjusteDet.Cantidad_nueva = pStockTemporal.Cantidad
            BeAjusteDet.CantReservada = pStockTemporal.Cantidad

            If BeAjusteDet.IdPresentacion <> 0 Then
                BeAjusteDet.Cantidad_original = Math.Round(BeAjusteDet.Cantidad_original / BeAjusteDet.Presentacion.Factor, 6)
                BeAjusteDet.Cantidad_nueva = Math.Round(BeAjusteDet.Cantidad_nueva / BeAjusteDet.Presentacion.Factor, 6)
                BeAjusteDet.CantReservada = Math.Round(BeAjusteDet.CantReservada / BeAjusteDet.Presentacion.Factor, 6)
            End If

            BeAjusteDet.UmBas = vProductoSinStock.UnidadMedida.Codigo
            BeAjusteDet.Codigo_producto = vProductoSinStock.Codigo
            BeAjusteDet.Nombre_producto = vProductoSinStock.Nombre
            BeAjusteDet.Idtipoajuste = 0
            BeAjusteDet.IdMotivoAjuste = 0
            BeAjusteDet.Observacion = "WMS Ajuste positivo por BOF."
            BeAjusteDet.Codigo_ajuste = 0
            BeAjusteDet.Enviado = False
            BeAjusteDet.lic_plate = pStockTemporal.Lic_plate

            BeAjusteDet.idstockres = IdStockRes
            BeAjusteDet.idstocklink = 0
            BeAjusteDet.esnuevolink = 0
            lBeTransAjusteDet.Add(BeAjusteDet)


            rc = dgrid.Rows.Add(codigo, BeAjusteDet.Nombre_producto, BeAjusteDet.UmBas, ubic)
            dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image
            dgrid.Rows(rc).Cells("ColLote").Value = BeAjusteDet.Lote_original

            dgrid.Rows(rc).Cells("UmBas").Value = BeAjusteDet.UmBas
            dgrid.Rows(rc).Cells("UmBas").ReadOnly = True

            dgrid.Rows(rc).Cells("colUbicacion").Value = ubic
            dgrid.Rows(rc).Cells("colUbicacion").ReadOnly = True

            '#CKFK 20211214 Agregué esta condición
            If dgrid.Columns("ColCantidad").HeaderText = "Vence Anterior" Then
                dgrid.Rows(rc).Cells("ColC antidad").Value = BeAjusteDet.Fecha_vence_original
            ElseIf dgrid.Columns("ColCantidad").HeaderText = "Existencia" Then
                dgrid.Rows(rc).Cells("ColCantidad").Value = BeAjusteDet.Cantidad_original
            ElseIf dgrid.Columns("ColCantidad").HeaderText = "Lote Anterior" Then
                dgrid.Rows(rc).Cells("ColCantidad").Value = BeAjusteDet.Lote_original
            End If

            If BeAjusteDet.IdPresentacion <> 0 Then
                dgrid.Rows(rc).Cells("colPresentacion").Value = BeAjusteDet.Presentacion.Nombre
                dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
            Else
                dgrid.Rows(rc).Cells("colPresentacion").Value = Nothing
                dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
            End If

            If BeAjusteDet.lic_plate <> "" Then
                dgrid.Rows(rc).Cells("ColLicPlate").Value = BeAjusteDet.lic_plate
                dgrid.Rows(rc).Cells("ColLicPlate").ReadOnly = True
            Else
                dgrid.Rows(rc).Cells("ColLicPlate").Value = Nothing
                dgrid.Rows(rc).Cells("ColLicPlate").ReadOnly = True
            End If

            Llenar_Motivo(rc, -1)

            '#GT13062022_16_50: si pTipoAjuste es 0, entonces se deja set de cmbTipoAjuste, para que el grid obtenga las columnas adecuadas
            Dim cmbIdTipoAjuste = cmbTipoAjuste.EditValue
            If pTipoAjuste = 0 Then
                Llenar_Tipo(rc, cmbIdTipoAjuste)
            Else
                Llenar_Tipo(rc, pTipoAjuste)
            End If

            Llena_Bodegas_ERP_Grid(rc, -1)

            dgrid.Rows(rc).Selected = True

            '#GT13062022_2004: si tiene mas de un registro en grid, bloquear el combo tipo ajuste
            If dgrid.Rows.Count > 0 Then
                cmbTipoAjuste.Enabled = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub oDateTimePicker_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)

        If oDateTimePicker.Visible Then
            'dgrid.CurrentRow.Cells("ColCantidad").Value = Format(oDateTimePicker.Value.Date, "dd-MM-yyyy")
            dgrid.CurrentRow.Cells("ColCantidad").Value = oDateTimePicker.Value
        End If

    End Sub

    Private Sub frmAjusteStock_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Set_Estado_Envio_A_ERP()

        Try

            If pBeTransAjustEnc.Enviado_A_ERP Then
                mnuEstadoEnviadoAERP.Caption = "Enviado"
                mnuEstadoEnviadoAERP.LargeGlyph = My.Resources.green_ball
            Else
                mnuEstadoEnviadoAERP.Caption = "No Enviado"
                mnuEstadoEnviadoAERP.LargeGlyph = My.Resources.red_ball
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

    Private Sub Carga_Documentos_Asociados()

        Try

            Dim DT As New DataTable

            DT = clsLnTrans_ajuste_det_doc.GetAll_ByIdAjusteEnc(pBeTransAjustEnc.Idajusteenc)

            grdDocsAsociados.DataSource = DT

            If GridView1.RowCount > 0 Then

                GridView1.Columns("idajustedoc").Caption = "Código"
                GridView1.Columns("idajusteenc").Caption = "IdAjusteEnc"
                GridView1.Columns("documento").Caption = "Documento"

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

    Private Sub dgrid_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgrid.DataError

        Try

        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub

    Private Sub cmbTipoAjuste_EditValueChanged(sender As Object, e As EventArgs) Handles cmbTipoAjuste.EditValueChanged
        Try

            'GT22042022_1612: obtener el tipo de ajuste por defecto, si en caso no se usa seleccionMultiple
            IdTipoAjuste = cmbTipoAjuste.EditValue

            If IdTipoAjuste = 3 Then
                mnuAjustePositivo.Enabled = True
                Es_Ajuste_Positivo = True
            Else
                mnuAjustePositivo.Enabled = False
                Es_Ajuste_Positivo = False
            End If

            'GT22042022: si cambia el tipo de ajuste en el combo del enc, se actualiza el combo del grid
            If dgrid.Rows.Count > 0 Then

                Dim fila As Object = cmbTipoAjuste.GetSelectedDataRow
                Dim pIdTipoAjuste = fila.Item("nombre")

                For sr = 0 To dgrid.Rows.Count - 1
                    Llenar_Tipo(sr, IdTipoAjuste)
                Next

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

    Private Sub Marcar_Ajuste_Para_Enviar_A_ERP()

        Try

            If pBeTransAjustEnc.Enviado_A_ERP Then

                Dim MarcadosParaReenviar As Integer = Get_Cantidad_No_Enviados()

                If MarcadosParaReenviar > 0 Then

                    If XtraMessageBox.Show(String.Format("¿Está seguro de cambiar a pendiente de envío {0} registros del ajuste?", MarcadosParaReenviar), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        pBeTransAjustEnc.Enviado_A_ERP = False

                        Dim Codigo As String, Lote As String, IdAjusteDet As Integer
                        Dim BeAjusteDet As New clsBeTrans_ajuste_det
                        Dim Enviar As Boolean = False

                        For i = 0 To dgrid.Rows.Count - 1

                            Codigo = IIf(IsDBNull(dgrid.Rows(i).Cells("ColCodigoProducto").Value), "", dgrid.Rows(i).Cells("ColCodigoProducto").Value)
                            Lote = IIf(IsDBNull(dgrid.Rows(i).Cells("ColLote").Value), "", dgrid.Rows(i).Cells("ColLote").Value)
                            IdAjusteDet = IIf(IsDBNull(dgrid.Rows(i).Cells("ColIdAjusteDEt").Value), 0, dgrid.Rows(i).Cells("ColIdAjusteDEt").Value)
                            Enviar = IIf(IsDBNull(dgrid.Rows(i).Cells("ColEnviadoAErp").Value), True, dgrid.Rows(i).Cells("ColEnviadoAErp").Value)

                            BeAjusteDet = lBeTransAjusteDet.Find(Function(x) x.Codigo_producto = Codigo _
                                                                 AndAlso x.Lote_nuevo = Lote AndAlso x.IdAjusteDet = IdAjusteDet)

                            If Not BeAjusteDet Is Nothing Then
                                If BeAjusteDet.Enviado Then
                                    clsLnTrans_ajuste_det.Actualizar_Estado_Enviado_A_ERP(BeAjusteDet.IdAjusteDet, Enviar)
                                End If
                            End If

                        Next

                        If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.Idajusteenc, pBeTransAjustEnc.Enviado_A_ERP) > 0 Then

                            Cargar_Datos()

                            XtraMessageBox.Show("Se actualizó el ajuste a pendiente de envío", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        End If

                    End If

                Else
                    XtraMessageBox.Show("No se ha desmarcado ningún registro para reenvío", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Else

                If XtraMessageBox.Show("¿Está seguro de cambiar el estado del ajuste a enviado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    pBeTransAjustEnc.Enviado_A_ERP = True

                    Dim Codigo As String, Lote As String, IdAjusteDet As Integer
                    Dim BeAjusteDet As New clsBeTrans_ajuste_det

                    For i = 0 To dgrid.Rows.Count - 1

                        Codigo = dgrid.Rows(i).Cells("ColCodigoProducto").Value
                        Lote = dgrid.Rows(i).Cells("ColLote").Value
                        IdAjusteDet = dgrid.Rows(i).Cells("ColIdAjusteDEt").Value

                        BeAjusteDet = lBeTransAjusteDet.Find(Function(x) x.Codigo_producto = Codigo _
                                                   AndAlso x.Lote_nuevo = Lote AndAlso x.IdAjusteDet = IdAjusteDet)

                        If Not BeAjusteDet Is Nothing Then

                            If Not BeAjusteDet.Enviado Then
                                clsLnTrans_ajuste_det.Actualizar_Estado_Enviado_A_ERP(BeAjusteDet.IdAjusteDet, True)
                            End If
                        End If

                    Next

                    If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.Idajusteenc, pBeTransAjustEnc.Enviado_A_ERP) > 0 Then

                        Cargar_Datos()

                        XtraMessageBox.Show("Se actualizó el ajuste a pendiente de envío", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub frmAjusteStock_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        IsLoading = True

        Try

            Try

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Cargando ajuste...")

            Catch ex As Exception

            End Try

            Try
                dtt = clsLnAjuste_tipo.Listar(True)
                dtm = clsLnAjuste_motivo.Listar()
            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

            oDateTimePicker = New DateTimePicker
            oDateTimePicker.Format = DateTimePickerFormat.Short
            oDateTimePicker.Visible = False
            oDateTimePicker.Width = 100
            dgrid.Controls.Add(oDateTimePicker)

            AddHandler oDateTimePicker.ValueChanged, AddressOf oDateTimePicker_ValueChanged

            txtReferencia.Enabled = Modo = TipoTrans.Nuevo
            dtpFecha.Enabled = Modo = TipoTrans.Nuevo
            cmdAdd.Visible = Modo = TipoTrans.Nuevo
            mnuDel.Visible = Modo = TipoTrans.Nuevo

            'grdData.ReadOnly = Not (Modo = TipoTrans.Nuevo)
            '#EJC20180613: Habilitar checkbox para reenviar datos a interface.
            'If Not (Modo = TipoTrans.Nuevo) Then
            Deshabilita_Grid()
            'End If

            cmbBodegaERP.Enabled = (Modo = TipoTrans.Nuevo)
            cmbProductoFamilia.Enabled = (Modo = TipoTrans.Nuevo)
            cmbPropietarioBodega.Enabled = (Modo = TipoTrans.Nuevo)
            lcmbCentroCosto.Enabled = (Modo = TipoTrans.Nuevo)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, AP.IdBodega, True)

            IMS.Listar_Producto_Familia(cmbProductoFamilia, AP.IdEmpresa)

            '#EJC20180924_0846PM: La bodega del ERP se registra por línea de detalle, 
            'En encabezado guardar la bodega de WMS. 
            IMS.Listar_Clientes_By_IdEmpresa(cmbBodegaERP, AP.IdEmpresa)

            IMS.Listar_Centro_Costo_By_IdEmpresa(lcmbCentroCosto, AP.IdEmpresa)

            'GT22042022_1034: Carga los tipos de ajuste activos para el combo del encabezado.
            IMS.Listar_Tipo_Ajuste_Activo(cmbTipoAjuste)

            BeConfig = clsLnI_nav_config_enc.GetSingle(AP.IdConfiguracionInterface)

            Application.DoEvents()

            Select Case Modo

                Case TipoTrans.Nuevo

                    Guardado = False

                    Inserta_Encabezado_Ajuste()

                    mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    mnuImprimir1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    lBeTransAjusteDet.Clear()
                    txtReferencia.Text = "" : dtpFecha.EditValue = Now

                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now
                    cmbBodegaERP.EditValue = clsLnCliente.Get_IdBodega_By_Codigo(AP.Bodega.Codigo)

                Case TipoTrans.Editar

                    Guardado = True

                    cmbTipoAjuste.Enabled = False

                    If pBeTransAjustEnc.Ajuste_Por_Inventario > 0 Then
                        mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                        mnuGuardar.Enabled = False
                        dgrid.ReadOnly = True
                    Else
                        mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    End If

                    mnuImprimir1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                    cmbBodegaERP.EditValue = clsLnCliente.Get_IdBodega_By_Codigo(AP.Bodega.Codigo)

                    Cargar_Datos()

                    Set_Estado_Envio_A_ERP()

            End Select

            'Cargar_Detalle()

            Try
                oDateTimePicker.Visible = False
                oDateTimePicker.Top = -50
            Catch ex As Exception
            End Try

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            IsLoading = False
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Dim pIdPropietarioBodega As Integer
    Dim pIdPropietario As Integer
    Private Sub cmbPropietarioBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietarioBodega.EditValueChanged
        Try

            If cmbPropietarioBodega.EditValue > 0 Then

                Dim fila As Object = cmbPropietarioBodega.GetSelectedDataRow

                If fila Is Nothing Then
                    'Throw New Exception("Error_09042024_1220: No se pudo obtener un propietario correctamente.")
                Else
                    pIdPropietario = fila.Item("IdPropietario")
                End If

                pIdPropietarioBodega = cmbPropietarioBodega.EditValue
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class