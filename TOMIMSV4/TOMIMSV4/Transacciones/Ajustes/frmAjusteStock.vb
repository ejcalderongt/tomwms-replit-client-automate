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
    Private lBeTransAjusteDetBorrador As New List(Of clsBeTrans_ajuste_det_borrador)
    Private lBeTransMovimientos As New List(Of clsBeTrans_movimientos)

    Public Delegate Sub Listar_Ajustes()
    Public Property InvokeListarAjustes As Listar_Ajustes
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private frmStockList As FrmStock_List
    Private oDateTimePicker As DateTimePicker
    Private DgComboTipo As New DataGridViewComboBoxCell()

    Private DgComboTalla As New DataGridViewComboBoxCell()
    Private DgComboColor As New DataGridViewComboBoxCell()

    Private IdTipoAjuste, IdMotivoAjuste, IdStockRes, IdTipoTarea As Integer
    Private TipoAjuste_Por_lote, TipoAjuste_Por_Fecha_Vence,
        TipoAjuste_Por_Cantidad, TipoAjuste_Por_Peso, Guardado, TipoAjuste_Por_Talla, TipoAjuste_Por_Color As Boolean

    Private LastEventHandlerTipo As EventHandler = AddressOf combotipo_SelectedIndexChanged
    Private LastEventHandlerMotivo As EventHandler = AddressOf combomotivo_SelectedIndexChanged

    Private LastEventHandlerTalla As EventHandler = AddressOf comboTalla_SelectedIndexChanged
    Private LastEventHandlerColor As EventHandler = AddressOf comboColor_SelectedIndexChanged

    Dim DgComboBodega As New DataGridViewComboBoxCell()
    Private Es_Ajuste_Positivo_Sin_Stock As Boolean = False

    Private BeBodega As clsBeBodega

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Sub New()

        InitializeComponent()

        Try

            frmStockList = New FrmStock_List(AP.IdBodega, cmbPropietarioBodega.EditValue) With {
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

            pBeTransAjustEnc.IdAjusteenc = clsLnTrans_ajuste_enc.MaxID() + 1

            txtNoAjuste.Text = pBeTransAjustEnc.IdAjusteenc

            pBeTransAjustEnc.Referencia = txtReferencia.Text
            pBeTransAjustEnc.Fecha = dtpFecha.EditValue
            pBeTransAjustEnc.Fec_agr = dtpFecha.EditValue
            pBeTransAjustEnc.Fec_mod = dtpFecha.EditValue
            pBeTransAjustEnc.Idusuario = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.User_agr = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.User_mod = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.IdBodega = AP.IdBodega
            pBeTransAjustEnc.IdPropietarioBodega = cmbPropietarioBodega.EditValue
            pBeTransAjustEnc.Centro_Costo_Erp = clsLnCentro_costo.Get_IdCentroCosto_By_Codigo(txtCentroCostoERP.Text)
            pBeTransAjustEnc.Centro_Costo_Dep_Erp = clsLnCentro_costo.Get_IdCentroCosto_By_Codigo(txtCentroCostoDepERP.Text)
            pBeTransAjustEnc.Centro_Costo_Dir_Erp = clsLnCentro_costo.Get_IdCentroCosto_By_Codigo(txtCentroCostoDirERP.Text)
            pBeTransAjustEnc.IdCentroCosto = lcmbCentroCosto.EditValue

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

            txtNoAjuste.Text = pBeTransAjustEnc.IdAjusteenc
            txtReferencia.Text = pBeTransAjustEnc.Referencia
            dtpFecha.EditValue = pBeTransAjustEnc.Fecha

            User_agrTextEdit.Text = pBeTransAjustEnc.User_agr
            Fec_agrDateEdit.Text = pBeTransAjustEnc.Fec_agr
            User_modTextEdit.Text = pBeTransAjustEnc.User_mod
            Fec_modDateEdit.Text = pBeTransAjustEnc.Fec_mod

            If BeBodega Is Nothing Then
                BeBodega = clsLnBodega.GetSingle_By_Idbodega(AP.IdBodega)
            End If

            If BeBodega.Bodega_Cliente_Ajuste_ByB Then

                Dim BeCliente As New clsBeCliente
                BeCliente = clsLnCliente.Get_Single_By_Codigo(pBeTransAjustEnc.IdBodega)

                If Not BeCliente Is Nothing Then
                    cmbBodegaERP.EditValue = BeCliente.IdCliente
                End If
            Else
                cmbBodegaERP.EditValue = 0
            End If

            If pBeTransAjustEnc.IdPropietarioBodega <> 0 Then
                cmbPropietarioBodega.EditValue = pBeTransAjustEnc.IdPropietarioBodega
            End If

            cmbProductoFamilia.EditValue = pBeTransAjustEnc.IdProductoFamilia
            lcmbCentroCosto.EditValue = pBeTransAjustEnc.IdCentroCosto

            txtCentroCostoERP.Text = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(Val(pBeTransAjustEnc.Centro_Costo_Erp))
            txtCentroCostoDirERP.Text = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(Val(pBeTransAjustEnc.Centro_Costo_Dir_Erp))
            txtCentroCostoDepERP.Text = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(Val(pBeTransAjustEnc.Centro_Costo_Dep_Erp))

            chkBorrador.Checked = pBeTransAjustEnc.Borrador

            lBeTransAjusteDet.Clear()
            lBeTransAjusteDetBorrador.Clear()

            lBeTransAjusteDetBorrador = clsLnTrans_ajuste_det_borrador.Get_By_IdAjusteEnc(pBeTransAjustEnc.IdAjusteenc)
            lBeTransAjusteDet = clsLnTrans_ajuste_det.Get_By_IdAjusteEnc(pBeTransAjustEnc.IdAjusteenc)

            Dim tipoajuste As Integer = 0

            If chkBorrador.Checked Then

                '#FIX_v10_BORRADOR_RESERVA_2026-04-25 (C3):
                'Validar que las reservas en stock_res asociadas al borrador sigan vivas.
                'Si algún IdStock fue consumido o liberado por terceros entre sesiones,
                'recrear la reserva. Si no se puede recrear (stock agotado), advertir
                'al operador antes de aplicar.
                Validar_Reservas_Borrador_Al_Cargar(lBeTransAjusteDetBorrador,
                                                    pBeTransAjustEnc.IdAjusteenc)

                If lBeTransAjusteDetBorrador IsNot Nothing AndAlso lBeTransAjusteDetBorrador.Count > 0 Then
                    tipoajuste = lBeTransAjusteDetBorrador.FirstOrDefault().idtipoajuste
                End If
            Else
                If lBeTransAjusteDet IsNot Nothing AndAlso lBeTransAjusteDet.Count > 0 Then
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

            Dim lDetalleCargar As List(Of clsBeTrans_ajuste_det) = Obtener_Detalle_A_Cargar()

            For Each vBeAjustDet As clsBeTrans_ajuste_det In lDetalleCargar

                '#CKFK 20210223 Agregué la bodega a la funciónGet_Nombre_Completo_By_IdUbicacion
                Ubic = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(vBeAjustDet.IdUbicacion, AP.IdBodega, clsTrans.lConnection, clsTrans.lTransaction)

                vIdProducto = clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(vBeAjustDet.IdProductoBodega, clsTrans.lConnection, clsTrans.lTransaction)

                Dim vProveedor As clsBeProveedor = Nothing
                Dim sProveedorTexto As String = ""

                If vIdProducto <> 0 Then

                    Codigo = clsLnProducto.Get_Single_By_IdProducto(clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(vBeAjustDet.IdProductoBodega, clsTrans.lConnection, clsTrans.lTransaction)).Codigo

                    vBeAjustDet.UmBas = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(vBeAjustDet.IdUnidadMedida, clsTrans.lConnection, clsTrans.lTransaction)

                    vProveedor = clsLnProveedor.Get_Single_By_IdStock(vBeAjustDet.IdStock, clsTrans.lConnection, clsTrans.lTransaction)

                    If vProveedor IsNot Nothing Then
                        If Not String.IsNullOrWhiteSpace(vProveedor.Codigo) Then
                            sProveedorTexto = $"{vProveedor.Codigo} - {vProveedor.Nombre}"
                        Else
                            sProveedorTexto = vProveedor.Nombre
                        End If
                    Else
                        Debug.WriteLine($"No se encontró proveedor para IdStock: {vBeAjustDet.IdStock}")
                    End If

                    ' #EJCRP 21042026: Estandarización del Rows.Add posicional. Aunque aquí
                    ' las 5 columnas pasadas SÍ coinciden con el orden actual del Designer
                    ' (ColCodigoProducto, colNombreProducto, UmBas, colPresentacion,
                    ' colUbicacion), se unifica el patrón con el resto del archivo para
                    ' protegerse de futuros reordenamientos en el Designer.
                    rc = dgrid.Rows.Add(Codigo, vBeAjustDet.Nombre_producto)
                    dgrid.Rows(rc).Cells("UmBas").Value = vBeAjustDet.UmBas
                    dgrid.Rows(rc).Cells("colPresentacion").Value = vBeAjustDet.Nombre_Presentacion
                    dgrid.Rows(rc).Cells("colUbicacion").Value = Ubic

                    ' #EJCRP 21042026: (consolida #ProveedorFix previo) ColProveedor está al
                    ' final del grid; se asigna por nombre para no caer en 'motivoajuste'
                    ' por el orden posicional. sProveedorTexto se calcula líneas arriba.
                    dgrid.Rows(rc).Cells("ColProveedor").Value = sProveedorTexto

                    dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image
                    dgrid.Rows(rc).Cells("ColLote").Value = vBeAjustDet.Lote_original

                    Llenar_Motivo(rc, vBeAjustDet.IdMotivoAjuste, clsTrans.lConnection, clsTrans.lTransaction)
                    Llenar_Tipo(rc, vBeAjustDet.Idtipoajuste, clsTrans.lConnection, clsTrans.lTransaction)
                    Llena_Bodegas_ERP_Grid(rc, vBeAjustDet.IdBodegaERP, clsTrans.lConnection, clsTrans.lTransaction)
                    Llenar_Talla(rc, -1)
                    Llenar_Color(rc, -1)

                    '#FIX_v18_CARGAR_DISPLAY_2026-04-25 (D2): existencia live solo para display.
                    Dim existenciaLive As Decimal = vBeAjustDet.Cantidad_original
                    If vBeAjustDet.IdStock > 0 AndAlso
                       (vBeAjustDet.Idtipoajuste = 3 OrElse vBeAjustDet.Idtipoajuste = 5) Then
                        Try
                            Dim stockActual As clsBeStock = clsLnStock.GetSingle(
                                vBeAjustDet.IdStock,
                                clsTrans.lConnection, clsTrans.lTransaction)
                            If stockActual IsNot Nothing AndAlso stockActual.IdStock > 0 Then
                                existenciaLive = stockActual.Cantidad
                            End If
                        Catch ex As Exception
                            clsLnLog_error_wms.Agregar_Error(
                                "Refrescar existencia IdStock=" & vBeAjustDet.IdStock & ": " & ex.Message)
                        End Try
                    End If

                    If vBeAjustDet.Idtipoajuste = 3 Then
                        If vBeAjustDet.IdPresentacion <> 0 Then
                            dgrid.Rows(rc).Cells("CantidadP").Value = existenciaLive / vBeAjustDet.Factor
                            dgrid.Rows(rc).Cells("ColCantidad").Value = (vBeAjustDet.Cantidad_nueva - vBeAjustDet.Cantidad_original) / vBeAjustDet.Factor
                        Else
                            dgrid.Rows(rc).Cells("CantidadP").Value = existenciaLive
                            dgrid.Rows(rc).Cells("ColCantidad").Value = vBeAjustDet.Cantidad_nueva - vBeAjustDet.Cantidad_original
                        End If
                    ElseIf vBeAjustDet.Idtipoajuste = 5 Then
                        If vBeAjustDet.IdPresentacion <> 0 Then
                            dgrid.Rows(rc).Cells("CantidadP").Value = existenciaLive / vBeAjustDet.Factor
                            dgrid.Rows(rc).Cells("ColCantidad").Value = (vBeAjustDet.Cantidad_original - vBeAjustDet.Cantidad_nueva) / vBeAjustDet.Factor
                        Else
                            dgrid.Rows(rc).Cells("CantidadP").Value = existenciaLive
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

                    If BeBodega.Control_Talla_Color Then

                        Dim tmpProductoTallaColor = clsLnProducto_talla_color.GetSingle(vBeAjustDet.IdProductoTallaColor_origen)

                        If tmpProductoTallaColor IsNot Nothing Then

                            Dim tmpTalla = clsLnTalla.GetSingle_By_IdTalla(tmpProductoTallaColor.IdTalla)
                            Dim tmpColor = clsLnColor.GetSingle_By_IdColor(tmpProductoTallaColor.IdColor)
                            dgrid.Rows(rc).Cells("colIdProductoTallaColor").Value = tmpProductoTallaColor.IdProductoTallaColor
                            dgrid.Rows(rc).Cells("colTalla").Value = tmpTalla.Codigo
                            dgrid.Rows(rc).Cells("colColor").Value = tmpColor.Codigo

                        End If

                    End If

                End If

                Application.DoEvents()
            Next

            clsTrans.Commit_Transaction()

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

        lblRegs.Caption = "Registros: " & dgrid.Rows.Count

    End Sub

    ' #EJCRP 21042026: Helper centralizado para resolver el texto del Proveedor a partir
    ' de un IdStock. Devuelve "" cuando no hay IdStock (>0), no proviene de una
    ' recepción contra OC, o si la consulta falla. Nunca lanza excepción para no
    ' bloquear la carga del grid. Sobrecargas:
    '   - Sin transacción: abre/cierra conexión propia (uso fuera de transacciones).
    '   - Con conn + trans: para llamarlo dentro de una transacción ya abierta.
    Private Function Get_Proveedor_Texto(ByVal pIdStock As Integer) As String
        If pIdStock <= 0 Then Return ""
        Try
            Dim be As clsBeProveedor = clsLnProveedor.Get_Single_By_IdStock(pIdStock)
            If be Is Nothing Then Return ""
            If Not String.IsNullOrWhiteSpace(be.Codigo) Then
                Return $"{be.Codigo} - {be.Nombre}"
            End If
            Return be.Nombre
        Catch
            Return ""
        End Try
    End Function

    Private Function Get_Proveedor_Texto(ByVal pIdStock As Integer,
                                         ByVal pConn As SqlClient.SqlConnection,
                                         ByVal pTrans As SqlClient.SqlTransaction) As String
        If pIdStock <= 0 Then Return ""
        Try
            Dim be As clsBeProveedor = clsLnProveedor.Get_Single_By_IdStock(pIdStock, pConn, pTrans)
            If be Is Nothing Then Return ""
            If Not String.IsNullOrWhiteSpace(be.Codigo) Then
                Return $"{be.Codigo} - {be.Nombre}"
            End If
            Return be.Nombre
        Catch
            Return ""
        End Try
    End Function

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click

        Dim st As New clsBeVW_stock_res
        Dim ubic, codigo As String
        Dim rc As Integer
        Dim pTipoAjuste As Integer = 0

        Es_Ajuste_Positivo_Sin_Stock = False

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

                frmStockList.IdPropietarioBodega = cmbPropietarioBodega.EditValue
                frmStockList.varTipoAjuste = cmbTipoAjuste.EditValue

                If frmStockList.ShowDialog() <> DialogResult.OK Then
                    Try
                        frmStockList.Hide()
                    Catch ex As Exception
                    End Try
                    Return
                End If

            Catch ex As Exception
                MsgBox("No se puede mostrar el stock")
            End Try

            'GT21042022: iterar la selección multiple
            If frmStockList.SeleccionMultiple Then

                If chkBorrador.Checked Then
                    lBeTransAjusteDetBorrador = New List(Of clsBeTrans_ajuste_det_borrador)
                Else
                    lBeTransAjusteDet = New List(Of clsBeTrans_ajuste_det)
                End If

                If Not cmbBodegaERP.EditValue Is Nothing AndAlso Not cmbBodegaERP.EditValue Is Nothing Then
                    pTipoAjuste = cmbTipoAjuste.EditValue
                End If

                For Each StockEspecificoSeleccionado In frmStockList.listSeleccionObjVWStockRes

                    Reservar_Stock(StockEspecificoSeleccionado.IdStock)

                    Llenar_Grid_Detalle(StockEspecificoSeleccionado, pTipoAjuste)

                    If dgrid.Rows.Count > 0 Then
                        cmbTipoAjuste.Enabled = False
                    End If

                Next

            Else

                Reservar_Stock(frmStockList.pSingleBEVWStockRes.IdStock)

                If chkBorrador.Checked Then

                    Dim BeAjusteDetBorrador As New clsBeTrans_ajuste_det_borrador
                    Dim pProductoTallaColor As New DataTable

                    BeAjusteDetBorrador.idajustedet = 0
                    BeAjusteDetBorrador.idajusteenc = pBeTransAjustEnc.IdAjusteenc
                    BeAjusteDetBorrador.IdStock = frmStockList.pSingleBEVWStockRes.IdStock
                    BeAjusteDetBorrador.IdPropietarioBodega = frmStockList.pSingleBEVWStockRes.IdPropietarioBodega
                    BeAjusteDetBorrador.IdProductoBodega = frmStockList.pSingleBEVWStockRes.IdProductoBodega
                    BeAjusteDetBorrador.IdProductoEstado = frmStockList.pSingleBEVWStockRes.IdProductoEstado
                    BeAjusteDetBorrador.IdPresentacion = frmStockList.pSingleBEVWStockRes.IdPresentacion
                    BeAjusteDetBorrador.IdUnidadMedida = frmStockList.pSingleBEVWStockRes.IdUnidadMedida
                    BeAjusteDetBorrador.IdUbicacion = frmStockList.pSingleBEVWStockRes.IdUbicacion

                    If BeAjusteDetBorrador.IdPresentacion <> 0 Then
                        BeAjusteDetBorrador.Presentacion = clsLnProducto_presentacion.GetSingle(BeAjusteDetBorrador.IdPresentacion)
                    End If

                    BeAjusteDetBorrador.lote_original = frmStockList.pSingleBEVWStockRes.Lote
                    BeAjusteDetBorrador.lote_nuevo = frmStockList.pSingleBEVWStockRes.Lote
                    BeAjusteDetBorrador.fecha_vence_original = frmStockList.pSingleBEVWStockRes.Fecha_Vence
                    BeAjusteDetBorrador.fecha_vence_nueva = frmStockList.pSingleBEVWStockRes.Fecha_Vence
                    BeAjusteDetBorrador.peso_original = frmStockList.pSingleBEVWStockRes.Peso
                    BeAjusteDetBorrador.peso_nuevo = frmStockList.pSingleBEVWStockRes.Peso
                    BeAjusteDetBorrador.cantidad_original = frmStockList.pSingleBEVWStockRes.CantidadUmBas - frmStockList.pSingleBEVWStockRes.CantidadReservadaUMBas
                    BeAjusteDetBorrador.cantidad_nueva = frmStockList.pSingleBEVWStockRes.CantidadUmBas - frmStockList.pSingleBEVWStockRes.CantidadReservadaUMBas
                    BeAjusteDetBorrador.CantReservada = frmStockList.pSingleBEVWStockRes.CantidadReservadaUMBas

                    If BeAjusteDetBorrador.IdPresentacion <> 0 Then
                        BeAjusteDetBorrador.cantidad_original = Math.Round(BeAjusteDetBorrador.cantidad_original / BeAjusteDetBorrador.Presentacion.Factor, 6)
                        BeAjusteDetBorrador.cantidad_nueva = Math.Round(BeAjusteDetBorrador.cantidad_nueva / BeAjusteDetBorrador.Presentacion.Factor, 6)
                        BeAjusteDetBorrador.CantReservada = Math.Round(BeAjusteDetBorrador.CantReservada / BeAjusteDetBorrador.Presentacion.Factor, 6)
                    End If

                    BeAjusteDetBorrador.UmBas = frmStockList.pSingleBEVWStockRes.UMBas
                    BeAjusteDetBorrador.codigo_producto = frmStockList.pSingleBEVWStockRes.Codigo_Producto
                    BeAjusteDetBorrador.nombre_producto = frmStockList.pSingleBEVWStockRes.Nombre_Producto
                    BeAjusteDetBorrador.idtipoajuste = 0
                    BeAjusteDetBorrador.idmotivoajuste = 0
                    BeAjusteDetBorrador.observacion = ""
                    BeAjusteDetBorrador.codigo_ajuste = 0
                    BeAjusteDetBorrador.enviado = False
                    BeAjusteDetBorrador.lic_plate = frmStockList.pSingleBEVWStockRes.Lic_plate
                    BeAjusteDetBorrador.idstockres = IdStockRes
                    BeAjusteDetBorrador.idstocklink = 0
                    BeAjusteDetBorrador.esnuevolink = 0

                    If BeBodega.Control_Talla_Color Then

                        BeAjusteDetBorrador.IdProductoTallaColor_origen = frmStockList.pSingleBEVWStockRes.IdProductoTallaColor
                        pProductoTallaColor = clsLnProducto_talla_color.Get_Single_Dt_By_IdProductoTallaColor(BeAjusteDetBorrador.IdProductoTallaColor_origen)

                        If pProductoTallaColor IsNot Nothing Then
                            BeAjusteDetBorrador.Talla_origen = IIf(IsDBNull(pProductoTallaColor.Rows(0).Item("Talla")), "", pProductoTallaColor.Rows(0).Item("Talla"))
                            BeAjusteDetBorrador.Color_origen = IIf(IsDBNull(pProductoTallaColor.Rows(0).Item("Color")), "", pProductoTallaColor.Rows(0).Item("Color"))
                        Else
                            Throw New Exception("No se encontró talla y color para el producto (id): " & BeAjusteDetBorrador.IdProductoBodega)
                        End If

                    End If

                    lBeTransAjusteDetBorrador.Add(BeAjusteDetBorrador)

                    ubic = clsLnBodega_ubicacion.GetSingle(BeAjusteDetBorrador.IdUbicacion, AP.IdBodega).NombreCompleto
                    codigo = clsLnProducto.Get_Single_By_IdProducto(clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(BeAjusteDetBorrador.IdProductoBodega)).Codigo

                    ' #EJCRP 21042026: Estandarización del Rows.Add posicional. Antes se
                    ' pasaban (codigo, nombre, UmBas, ubic) como argumentos posicionales,
                    ' lo que era frágil ante cambios de orden de columnas en el Designer
                    ' (las posiciones 3 y 4 NO siempre corresponden a UmBas/colUbicacion;
                    ' realmente eran UmBas/colPresentacion en este grid). Ahora pasamos
                    ' solo las 2 primeras (que sí son ColCodigoProducto/colNombreProducto)
                    ' y el resto se asigna por nombre justo abajo (UmBas, colUbicacion).
                    rc = dgrid.Rows.Add(codigo, BeAjusteDetBorrador.nombre_producto)

                    dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image
                    dgrid.Rows(rc).Cells("ColLote").Value = BeAjusteDetBorrador.lote_original
                    ' #EJCRP 21042026: poblar Proveedor desde el IdStock (alta desde borrador).
                    dgrid.Rows(rc).Cells("ColProveedor").Value = Get_Proveedor_Texto(BeAjusteDetBorrador.IdStock)

                    dgrid.Rows(rc).Cells("UmBas").Value = BeAjusteDetBorrador.UmBas
                    dgrid.Rows(rc).Cells("UmBas").ReadOnly = True

                    dgrid.Rows(rc).Cells("colUbicacion").Value = ubic
                    dgrid.Rows(rc).Cells("colUbicacion").ReadOnly = True

                    If dgrid.Columns("ColCantidad").HeaderText = "Vence Anterior" Then
                        dgrid.Rows(rc).Cells("ColCantidad").Value = BeAjusteDetBorrador.fecha_vence_original
                    ElseIf dgrid.Columns("ColCantidad").HeaderText = "Existencia" Then
                        dgrid.Rows(rc).Cells("ColCantidad").Value = BeAjusteDetBorrador.cantidad_original
                    ElseIf dgrid.Columns("ColCantidad").HeaderText = "Lote Anterior" Then
                        dgrid.Rows(rc).Cells("ColCantidad").Value = BeAjusteDetBorrador.lote_original
                    End If

                    If BeAjusteDetBorrador.IdPresentacion <> 0 Then
                        dgrid.Rows(rc).Cells("colPresentacion").Value = BeAjusteDetBorrador.Presentacion.Nombre
                        dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
                    Else
                        dgrid.Rows(rc).Cells("colPresentacion").Value = Nothing
                        dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
                    End If

                    If BeAjusteDetBorrador.lic_plate <> "" Then
                        dgrid.Rows(rc).Cells("ColLicPlate").Value = BeAjusteDetBorrador.lic_plate
                        dgrid.Rows(rc).Cells("ColLicPlate").ReadOnly = True
                    Else
                        dgrid.Rows(rc).Cells("ColLicPlate").Value = Nothing
                        dgrid.Rows(rc).Cells("ColLicPlate").ReadOnly = True
                    End If

                    Llenar_Motivo(rc, -1)

                    Dim cmbIdTipoAjuste = cmbTipoAjuste.EditValue
                    If pTipoAjuste = 0 Then
                        Llenar_Tipo(rc, cmbIdTipoAjuste)
                    Else
                        Llenar_Tipo(rc, pTipoAjuste)
                    End If

                    Llena_Bodegas_ERP_Grid(rc, -1)

                    dgrid.Rows(rc).Selected = True

                    If dgrid.Rows.Count > 0 Then
                        cmbTipoAjuste.Enabled = False
                    End If

                    If BeBodega.Control_Talla_Color Then

                        Llenar_Talla(rc, -1)
                        Llenar_Color(rc, -1)

                        If pProductoTallaColor IsNot Nothing Then
                            dgrid.Rows(rc).Cells("colIdProductoTallaColor").Value = BeAjusteDetBorrador.IdProductoTallaColor_origen
                            dgrid.Rows(rc).Cells("colTalla").Value = IIf(IsDBNull(pProductoTallaColor.Rows(0).Item("Talla")), "", pProductoTallaColor.Rows(0).Item("Talla"))
                            dgrid.Rows(rc).Cells("colColor").Value = IIf(IsDBNull(pProductoTallaColor.Rows(0).Item("Color")), "", pProductoTallaColor.Rows(0).Item("Color"))
                        Else
                            Throw New Exception("No se encontró talla y color para el producto (id): " & BeAjusteDetBorrador.IdProductoBodega)
                        End If

                        dgrid.Rows(rc).Cells("colTalla").ReadOnly = True
                        dgrid.Rows(rc).Cells("colColor").ReadOnly = True

                    End If

                Else

                    BeAjusteDet = New clsBeTrans_ajuste_det
                    BeAjusteDet.IdAjusteDet = 0
                    BeAjusteDet.IdAjusteEnc = pBeTransAjustEnc.IdAjusteenc
                    BeAjusteDet.IdStock = frmStockList.pSingleBEVWStockRes.IdStock
                    BeAjusteDet.IdPropietarioBodega = frmStockList.pSingleBEVWStockRes.IdPropietarioBodega
                    BeAjusteDet.IdProductoBodega = frmStockList.pSingleBEVWStockRes.IdProductoBodega
                    BeAjusteDet.IdProductoEstado = frmStockList.pSingleBEVWStockRes.IdProductoEstado
                    BeAjusteDet.IdPresentacion = frmStockList.pSingleBEVWStockRes.IdPresentacion
                    BeAjusteDet.IdUnidadMedida = frmStockList.pSingleBEVWStockRes.IdUnidadMedida
                    BeAjusteDet.IdUbicacion = frmStockList.pSingleBEVWStockRes.IdUbicacion

                    If BeAjusteDet.IdPresentacion <> 0 Then
                        BeAjusteDet.Presentacion = clsLnProducto_presentacion.GetSingle(BeAjusteDet.IdPresentacion)
                    End If

                    BeAjusteDet.Lote_original = frmStockList.pSingleBEVWStockRes.Lote
                    BeAjusteDet.Lote_nuevo = frmStockList.pSingleBEVWStockRes.Lote
                    BeAjusteDet.Fecha_vence_original = frmStockList.pSingleBEVWStockRes.Fecha_Vence
                    BeAjusteDet.Fecha_vence_nueva = frmStockList.pSingleBEVWStockRes.Fecha_Vence
                    BeAjusteDet.Peso_original = frmStockList.pSingleBEVWStockRes.Peso
                    BeAjusteDet.Peso_nuevo = frmStockList.pSingleBEVWStockRes.Peso
                    BeAjusteDet.Cantidad_original = frmStockList.pSingleBEVWStockRes.CantidadUmBas - frmStockList.pSingleBEVWStockRes.CantidadReservadaUMBas
                    BeAjusteDet.Cantidad_nueva = frmStockList.pSingleBEVWStockRes.CantidadUmBas - frmStockList.pSingleBEVWStockRes.CantidadReservadaUMBas
                    BeAjusteDet.CantReservada = frmStockList.pSingleBEVWStockRes.CantidadReservadaUMBas

                    If BeAjusteDet.IdPresentacion <> 0 Then
                        BeAjusteDet.Cantidad_original = Math.Round(BeAjusteDet.Cantidad_original / BeAjusteDet.Presentacion.Factor, 6)
                        BeAjusteDet.Cantidad_nueva = Math.Round(BeAjusteDet.Cantidad_nueva / BeAjusteDet.Presentacion.Factor, 6)
                        BeAjusteDet.CantReservada = Math.Round(BeAjusteDet.CantReservada / BeAjusteDet.Presentacion.Factor, 6)
                    End If

                    BeAjusteDet.UmBas = frmStockList.pSingleBEVWStockRes.UMBas
                    BeAjusteDet.Codigo_producto = frmStockList.pSingleBEVWStockRes.Codigo_Producto
                    BeAjusteDet.Nombre_producto = frmStockList.pSingleBEVWStockRes.Nombre_Producto
                    BeAjusteDet.Idtipoajuste = 0
                    BeAjusteDet.IdMotivoAjuste = 0
                    BeAjusteDet.Observacion = ""
                    BeAjusteDet.Codigo_ajuste = 0
                    BeAjusteDet.Enviado = False
                    BeAjusteDet.lic_plate = frmStockList.pSingleBEVWStockRes.Lic_plate
                    BeAjusteDet.idstockres = IdStockRes
                    BeAjusteDet.idstocklink = 0
                    BeAjusteDet.esnuevolink = 0

                    Dim pProductoTallaColor As New DataTable

                    If BeBodega.Control_Talla_Color Then

                        BeAjusteDet.IdProductoTallaColor_origen = frmStockList.pSingleBEVWStockRes.IdProductoTallaColor
                        pProductoTallaColor = clsLnProducto_talla_color.Get_Single_Dt_By_IdProductoTallaColor(BeAjusteDet.IdProductoTallaColor_origen)

                        If pProductoTallaColor IsNot Nothing Then
                            BeAjusteDet.Talla_origen = IIf(IsDBNull(pProductoTallaColor.Rows(0).Item("Talla")), "", pProductoTallaColor.Rows(0).Item("Talla"))
                            BeAjusteDet.Color_origen = IIf(IsDBNull(pProductoTallaColor.Rows(0).Item("Color")), "", pProductoTallaColor.Rows(0).Item("Color"))
                        Else
                            Throw New Exception("No se encontró talla y color para el producto (id): " & BeAjusteDet.IdProductoBodega)
                        End If

                    End If

                    lBeTransAjusteDet.Add(BeAjusteDet)

                    ubic = clsLnBodega_ubicacion.GetSingle(BeAjusteDet.IdUbicacion, AP.IdBodega).NombreCompleto
                    codigo = clsLnProducto.Get_Single_By_IdProducto(clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(BeAjusteDet.IdProductoBodega)).Codigo

                    ' #EJCRP 21042026: Estandarización del Rows.Add posicional. Mismo
                    ' criterio que en el alta desde borrador: solo Codigo+Nombre por
                    ' posición; UmBas/colUbicacion ya se asignan por nombre abajo.
                    rc = dgrid.Rows.Add(codigo, BeAjusteDet.Nombre_producto)

                    dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image
                    dgrid.Rows(rc).Cells("ColLote").Value = BeAjusteDet.Lote_original
                    ' #EJCRP 21042026: poblar Proveedor desde el IdStock (alta directa).
                    dgrid.Rows(rc).Cells("ColProveedor").Value = Get_Proveedor_Texto(BeAjusteDet.IdStock)

                    dgrid.Rows(rc).Cells("UmBas").Value = BeAjusteDet.UmBas
                    dgrid.Rows(rc).Cells("UmBas").ReadOnly = True

                    dgrid.Rows(rc).Cells("colUbicacion").Value = ubic
                    dgrid.Rows(rc).Cells("colUbicacion").ReadOnly = True

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

                    Dim cmbIdTipoAjuste = cmbTipoAjuste.EditValue
                    If pTipoAjuste = 0 Then
                        Llenar_Tipo(rc, cmbIdTipoAjuste)
                    Else
                        Llenar_Tipo(rc, pTipoAjuste)
                    End If

                    Llena_Bodegas_ERP_Grid(rc, -1)

                    dgrid.Rows(rc).Selected = True

                    If dgrid.Rows.Count > 0 Then
                        cmbTipoAjuste.Enabled = False
                    End If

                    If BeBodega.Control_Talla_Color Then

                        Llenar_Talla(rc, -1)
                        Llenar_Color(rc, -1)

                        If pProductoTallaColor IsNot Nothing Then
                            dgrid.Rows(rc).Cells("colIdProductoTallaColor").Value = BeAjusteDet.IdProductoTallaColor_origen
                            dgrid.Rows(rc).Cells("colTalla").Value = IIf(IsDBNull(pProductoTallaColor.Rows(0).Item("Talla")), "", pProductoTallaColor.Rows(0).Item("Talla"))
                            dgrid.Rows(rc).Cells("colColor").Value = IIf(IsDBNull(pProductoTallaColor.Rows(0).Item("Color")), "", pProductoTallaColor.Rows(0).Item("Color"))
                        Else
                            Throw New Exception("No se encontró talla y color para el producto (id): " & BeAjusteDet.IdProductoBodega)
                        End If

                        dgrid.Rows(rc).Cells("colTalla").ReadOnly = True
                        dgrid.Rows(rc).Cells("colColor").ReadOnly = True

                    End If

                End If

            End If

            frmStockList.Hide()

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
            BeAjusteDet.IdAjusteEnc = pBeTransAjustEnc.IdAjusteenc
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
            BeAjusteDet.IdProductoTallaColor_origen = stockEspecificoSeleccionado.IdProductoTallaColor
            BeAjusteDet.Talla_origen = stockEspecificoSeleccionado.Codigo_Talla
            BeAjusteDet.Color_origen = stockEspecificoSeleccionado.Codigo_Color
            lBeTransAjusteDet.Add(BeAjusteDet)

            ubic = clsLnBodega_ubicacion.GetSingle(BeAjusteDet.IdUbicacion, AP.IdBodega).NombreCompleto
            codigo = clsLnProducto.Get_Single_By_IdProducto(clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(BeAjusteDet.IdProductoBodega)).Codigo

            ' #EJCRP 21042026: Estandarización del Rows.Add posicional. UmBas y ubic se
            ' reasignan por nombre justo abajo, así que no hace falta pasarlos por
            ' posición. ColProveedor se llena un poco más abajo desde stockEspecificoSeleccionado.Proveedor.
            rc = dgrid.Rows.Add(codigo, BeAjusteDet.Nombre_producto)
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

            dgrid.Rows(rc).Cells("ColProveedor").Value = stockEspecificoSeleccionado.Proveedor

            '#GT28082025: si hay control talla color, mostrar los codigos porque no se manejan las columnas como combos (no hay que llenar id´s)
            If BeBodega.Control_Talla_Color Then

                Llenar_Talla(rc, -1)
                Llenar_Color(rc, -1)

                Dim pProductoTallaColor = clsLnProducto_talla_color.Get_Single_Dt_By_IdProductoTallaColor(BeAjusteDet.IdProductoTallaColor_origen)

                If pProductoTallaColor IsNot Nothing Then

                    dgrid.Rows(rc).Cells("ColIdProductoTallaColor").Value = BeAjusteDet.IdProductoTallaColor_origen
                    dgrid.Rows(rc).Cells("colTalla").Value = IIf(IsDBNull(pProductoTallaColor.Rows(0).Item("Talla")), "", pProductoTallaColor.Rows(0).Item("Talla"))
                    dgrid.Rows(rc).Cells("colColor").Value = IIf(IsDBNull(pProductoTallaColor.Rows(0).Item("Color")), "", pProductoTallaColor.Rows(0).Item("Color"))

                End If

            End If

            Llenar_Motivo(rc, -1)
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

        Dim pStock_Reservado As New clsBeStock_res
        Dim pStock As New clsBeStock
        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            pStock.IdStock = idstock
            clsLnStock.GetSingle(pStock, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If BeBodega.Control_Talla_Color Then
                Dim pProductoTallaColor = clsLnProducto_talla_color.Get_Single_Dt_By_IdProductoTallaColor(pStock.IdProductoTallaColor,
                                                                                                          clsTransaccion.lConnection,
                                                                                                          clsTransaccion.lTransaction)

                If pProductoTallaColor IsNot Nothing Then
                    pStock_Reservado.IdProductoTallaColor = pStock.IdProductoTallaColor
                    pStock_Reservado.Talla = pProductoTallaColor.Rows(0).Item("Talla")
                    pStock_Reservado.Color = pProductoTallaColor.Rows(0).Item("Color")
                Else
                    Throw New Exception("No se encontró talla y color para el producto (id): " & pStock.IdProductoBodega)
                End If

            End If

            pStock_Reservado.IdStockRes = IdStockRes
            pStock_Reservado.IdTransaccion = pBeTransAjustEnc.IdAjusteenc
            pStock_Reservado.Indicador = "ajuste_stock" '#EJC20180613 ajuste_stock aplicado.
            pStock_Reservado.IdPedidoDet = 0
            pStock_Reservado.IdStock = pStock.IdStock
            pStock_Reservado.IdPropietarioBodega = pStock.IdPropietarioBodega
            pStock_Reservado.IdProductoBodega = pStock.IdProductoBodega
            pStock_Reservado.IdUbicacion = pStock.IdUbicacion
            pStock_Reservado.IdProductoEstado = pStock.IdProductoEstado
            pStock_Reservado.IdPresentacion = pStock.IdPresentacion
            pStock_Reservado.IdUnidadMedida = pStock.IdUnidadMedida
            pStock_Reservado.Lote = pStock.Lote
            pStock_Reservado.Lic_plate = pStock.Lic_plate
            pStock_Reservado.Serial = pStock.Serial
            pStock_Reservado.Cantidad = pStock.Cantidad
            pStock_Reservado.Peso = pStock.Peso
            pStock_Reservado.Estado = ""
            pStock_Reservado.Fecha_ingreso = pStock.Fecha_Ingreso
            pStock_Reservado.Fecha_vence = pStock.Fecha_vence
            pStock_Reservado.Uds_lic_plate = pStock.Uds_lic_plate
            pStock_Reservado.Ubicacion_ant = pStock.UbicacionAnterior
            pStock_Reservado.No_bulto = pStock.No_bulto
            pStock_Reservado.IdRecepcion = pStock.IdRecepcionEnc
            pStock_Reservado.IdPicking = pStock.IdPickingEnc
            pStock_Reservado.IdPedido = pStock.IdPedidoEnc
            pStock_Reservado.IdDespacho = pStock.IdDespachoEnc
            pStock_Reservado.User_agr = pStock.User_agr
            pStock_Reservado.Fec_agr = pStock.Fec_agr
            pStock_Reservado.User_mod = pStock.User_mod
            pStock_Reservado.Fec_mod = pStock.Fec_mod
            pStock_Reservado.Host = AP.HostName
            pStock_Reservado.añada = pStock.Añada
            pStock_Reservado.Fecha_manufactura = pStock.Fecha_Manufactura
            pStock_Reservado.Atributo_Variante_1 = pStock.Atributo_Variante_1
            pStock_Reservado.IdBodega = pStock.IdBodega
            pStock_Reservado.Talla = pStock.Talla
            pStock_Reservado.Color = pStock.Color
            pStock_Reservado.IdProductoTallaColor = pStock.IdProductoTallaColor

            clsLnStock_res.Insertar(pStock_Reservado, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    '#GT29112024: reserva para ajuste positivo de producto sin stock
    Private Function Reservar_Stock(idstock As Integer, ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Boolean

        Dim rs As New clsBeStock_res
        Dim st As New clsBeStock

        Reservar_Stock = False

        Try

            st.IdStock = idstock
            st = clsLnStock.GetSingle(st.IdStock, lConnection, lTransaction)
            rs.IdStockRes = IdStockRes
            rs.IdTransaccion = pBeTransAjustEnc.IdAjusteenc
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

    Private Sub Llenar_Talla(ByVal pIndex As Integer,
                              Optional pIdTalla As Integer = 0,
                              Optional ByVal lConnection As SqlConnection = Nothing,
                              Optional ByVal lTransaction As SqlTransaction = Nothing)


        Dim DgCombo As New DataGridViewComboBoxCell()
        Dim dt As New DataTable
        Dim vTransaccionRemota As Boolean = (lConnection IsNot Nothing AndAlso lTransaction IsNot Nothing)

        Try

            If vTransaccionRemota Then
                dt = clsLnTalla.Listar(True, lConnection, lTransaction)
            Else
                dt = clsLnTalla.Listar(True)
            End If

            DgCombo = TryCast(dgrid.Rows(pIndex).Cells("ColTalla"), DataGridViewComboBoxCell)
            DgCombo.DataSource = dt
            DgCombo.ValueMember = "IdTalla"
            DgCombo.DisplayMember = "Codigo"

            If pIdTalla <> -1 Then
                DgCombo.Value = pIdTalla
            Else
                If dt.Rows.Count = 1 Then
                    pIdTalla = 1
                    DgCombo.Value = 1
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Llenar_Color(ByVal pIndex As Integer,
                              Optional pIdColor As Integer = 0,
                              Optional ByVal lConnection As SqlConnection = Nothing,
                              Optional ByVal lTransaction As SqlTransaction = Nothing)

        Dim DgCombo As New DataGridViewComboBoxCell()
        Dim dt As New DataTable
        Dim vTransaccionRemota As Boolean = (lConnection IsNot Nothing AndAlso lTransaction IsNot Nothing)

        Try

            If vTransaccionRemota Then
                dt = clsLnColor.Listar(True, lConnection, lTransaction)
            Else
                dt = clsLnColor.Listar(True)
            End If

            DgCombo = TryCast(dgrid.Rows(pIndex).Cells("ColColor"), DataGridViewComboBoxCell)
            DgCombo.DataSource = dt
            DgCombo.ValueMember = "IdColor"
            DgCombo.DisplayMember = "Codigo"

            If pIdColor <> -1 Then
                DgCombo.Value = pIdColor
            Else
                If dt.Rows.Count = 1 Then
                    pIdColor = 1
                    DgCombo.Value = 1
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

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

    '#FIX_v10_BORRADOR_RESERVA_2026-04-25 (C3):
    'Helper invocado al cargar un ajuste borrador existente. Verifica que cada
    'detalle tenga su correspondiente reserva en stock_res; si no, intenta
    'recrearla. Devuelve la cantidad de detalles que NO se pudieron re-reservar
    '(stock agotado/eliminado por terceros).
    '
    'Implementación: usa clsLnStock_res.Get_All_By_IdStock(IdStock) que SÍ existe
    '(DAL clsLnStock_res_Partial.vb línea 5149) y filtra in-memory por
    'IdTransaccion + Indicador. Más lento que un SP dedicado pero evita agregar
    'función nueva al DAL.
    Private Function Validar_Reservas_Borrador_Al_Cargar(
            ByVal lDetallesBorrador As List(Of clsBeTrans_ajuste_det_borrador),
            ByVal pIdAjusteEnc As Integer) As Integer

        Dim cntFaltantes As Integer = 0
        Dim cntRecreados As Integer = 0
        Dim warnings As New System.Text.StringBuilder()

        Try
            If lDetallesBorrador Is Nothing OrElse lDetallesBorrador.Count = 0 Then Return 0

            For Each det As clsBeTrans_ajuste_det_borrador In lDetallesBorrador
                If det.IdStock <= 0 Then Continue For

                Dim reservasDelStock As List(Of clsBeStock_res) =
                    clsLnStock_res.Get_All_By_IdStock(det.IdStock)

                Dim tieneReservaPropia As Boolean = False
                If reservasDelStock IsNot Nothing Then
                    For Each rsv In reservasDelStock
                        If rsv.IdTransaccion = pIdAjusteEnc AndAlso
                           rsv.Indicador = "ajuste_stock" Then
                            tieneReservaPropia = True
                            Exit For
                        End If
                    Next
                End If

                If tieneReservaPropia Then Continue For

                'Reserva faltante: intentar recrear
                Dim ok As Boolean = Reservar_Stock_Borrador_Standalone(det.IdStock, pIdAjusteEnc)
                If ok Then
                    cntRecreados += 1
                Else
                    cntFaltantes += 1
                    warnings.AppendLine(String.Format(
                        "  - IdStock {0}, lote {1}, producto {2}: stock no disponible.",
                        det.IdStock, det.lote_original, det.codigo_producto))
                End If
            Next

            If cntFaltantes > 0 Then
                XtraMessageBox.Show(
                    String.Format(
                        "Se reanudó el borrador pero {0} detalle(s) tienen stock " &
                        "consumido por otros procesos:" & vbCrLf & vbCrLf & "{1}" & vbCrLf &
                        "Revise estos detalles antes de aplicar el ajuste.",
                        cntFaltantes, warnings.ToString()),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error(
                "Validar_Reservas_Borrador_Al_Cargar: " & ex.Message)
        End Try

        Return cntFaltantes
    End Function

    '#FIX_v10_BORRADOR_RESERVA_2026-04-25 (C3 helper):
    'Wrap defensivo de Reservar_Stock_Para_Borrador_Standalone con su propia tx,
    'devuelve True/False sin mostrar MessageBox individual (el agregado lo hace
    'Validar_Reservas_Borrador_Al_Cargar).
    Private Function Reservar_Stock_Borrador_Standalone(ByVal idstock As Integer,
                                                         ByVal pIdAjusteEnc As Integer) As Boolean
        Dim ct As New clsTransaccion()
        Try
            ct.Begin_Transaction()
            Dim ok = clsLnTrans_ajuste_enc.Reservar_Stock_Para_Borrador_Standalone(
                idstock, pIdAjusteEnc, AP.HostName, AP.UsuarioAp.IdUsuario,
                ct.lConnection, ct.lTransaction)
            ct.Commit_Transaction()
            Return ok
        Catch ex As Exception
            ct.RollBack_Transaction()
            clsLnLog_error_wms.Agregar_Error("Reservar_Stock_Borrador_Standalone: " & ex.Message)
            Return False
        Finally
            ct.Close_Conection()
        End Try
    End Function

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

                            If Not chkBorrador.Checked Then
                                dgrid.Rows(pIndex).Cells("tipoajuste").ReadOnly = True
                            End If

                            Valor_Tipo_Ajuste(pIndex)

                        End If

                    Case TipoTrans.Nuevo

                        DgComboTipo = TryCast(dgrid.Rows(pIndex).Cells("tipoajuste"), DataGridViewComboBoxCell)
                        DgComboTipo.DataSource = dt
                        DgComboTipo.DisplayMember = "Nombre"
                        DgComboTipo.ValueMember = "Idtipoajuste"
                        dgrid.Rows(pIndex).Cells("tipoajuste").ReadOnly = False
                        Valor_Tipo_Ajuste(pIndex)

                        If pidtipo = 3 OrElse pidtipo = 5 Then
                            DgComboTipo.Value = pidtipo
                        End If

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

    Private Sub comboTalla_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim DgCombo As New DataGridViewComboBoxCell()
        Dim sr As Integer

        Try

            If dgrid.Rows.Count > 0 Then

                sr = dgrid.SelectedRows(0).Index

                dgrid.EndEdit()

                DgCombo = TryCast(dgrid.Rows(sr).Cells("ColTalla"), DataGridViewComboBoxCell)

                Dim vNombreTipo As String = DgCombo.EditedFormattedValue

                If vNombreTipo.Trim <> "" AndAlso vNombreTipo <> "System.Data.DataRowView" Then

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

    Private Sub comboColor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim DgCombo As New DataGridViewComboBoxCell()
        Dim sr As Integer

        Try

            If dgrid.Rows.Count > 0 Then

                sr = dgrid.SelectedRows(0).Index

                dgrid.EndEdit()

                DgCombo = TryCast(dgrid.Rows(sr).Cells("ColColor"), DataGridViewComboBoxCell)

                Dim vNombreTipo As String = DgCombo.EditedFormattedValue

                If vNombreTipo.Trim <> "" AndAlso vNombreTipo <> "System.Data.DataRowView" Then

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

                    If chkBorrador.Checked Then
                        lBeTransAjusteDetBorrador(sr).idtipoajuste = IdTipoAjuste
                    Else
                        lBeTransAjusteDet(sr).Idtipoajuste = IdTipoAjuste
                    End If


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

        TipoAjuste_Por_lote = False
        TipoAjuste_Por_Fecha_Vence = False
        TipoAjuste_Por_Cantidad = False
        TipoAjuste_Por_Peso = False

        Try

            If IdTipoAjuste = -1 Then Return

            dr = dtt.Select("idtipoajuste=" & IdTipoAjuste)

            If dr.Count = 0 Then Return

            If dr(0).Item("modifica_lote") Then TipoAjuste_Por_lote = True
            If dr(0).Item("momdifica_vencimiento") Then TipoAjuste_Por_Fecha_Vence = True
            If dr(0).Item("modifica_cantidad") Then TipoAjuste_Por_Cantidad = True
            If dr(0).Item("modifica_peso") Then TipoAjuste_Por_Peso = True

            If TipoAjuste_Por_lote Then

                Dim vCantidadOriginal As Double = 0
                Dim vLoteOriginal As String = ""

                If chkBorrador.Checked Then
                    vCantidadOriginal = lBeTransAjusteDetBorrador(sr).cantidad_original
                    vLoteOriginal = lBeTransAjusteDetBorrador(sr).lote_original
                Else
                    vCantidadOriginal = lBeTransAjusteDet(sr).Cantidad_original
                    vLoteOriginal = lBeTransAjusteDet(sr).Lote_original
                End If

                dgrid.Columns("CantidadP").HeaderText = "Existencia"
                dgrid.Rows(sr).Cells("CantidadP").Value = vCantidadOriginal

                dgrid.Columns("ColCantidad").HeaderText = "Nuevo Lote"
                dgrid.Rows(sr).Cells("ColCantidad").ReadOnly = False
                dgrid.Columns("colLote").HeaderText = "Lote Actual"
                dgrid.Columns("UmBas").ReadOnly = True
                dgrid.Columns("colLote").ReadOnly = True

                Dim currencyCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle()
                currencyCellStyle.Format = "N6"

                dgrid.Columns("ColCantidad").DefaultCellStyle = currencyCellStyle
                dgrid.Rows(sr).Cells("ColCantidad").Value = vLoteOriginal

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

                Set_Valores_Ajuste_Lote(sr)
                Return

            End If

            If TipoAjuste_Por_Fecha_Vence Then

                Dim vFechaVenceOriginal As Date

                If chkBorrador.Checked Then
                    vFechaVenceOriginal = lBeTransAjusteDetBorrador(sr).fecha_vence_original
                Else
                    vFechaVenceOriginal = lBeTransAjusteDet(sr).Fecha_vence_original
                End If

                dgrid.Columns("ColCantidad").HeaderText = "Vence Actual"
                dgrid.Columns("CantidadP").HeaderText = "Vence Anterior"

                dgrid.Columns("CantidadP").ReadOnly = True
                dgrid.Columns("ColCantidad").ReadOnly = False
                dgrid.Columns("colLote").ReadOnly = True

                Dim currencyCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle()
                currencyCellStyle.Format = "d"

                dgrid.Columns("ColCantidad").DefaultCellStyle = currencyCellStyle
                dgrid.Columns("CantidadP").DefaultCellStyle = currencyCellStyle

                dgrid.Rows(sr).Cells("CantidadP").Value = vFechaVenceOriginal
                dgrid.Rows(sr).Cells("ColCantidad").Value = vFechaVenceOriginal

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

                Set_Valores_Ajuste_Vence(sr)
                Return

            End If

            If TipoAjuste_Por_Cantidad Then

                Dim vCantidadOriginal As Double = 0

                If chkBorrador.Checked Then
                    vCantidadOriginal = lBeTransAjusteDetBorrador(sr).cantidad_original
                Else
                    vCantidadOriginal = lBeTransAjusteDet(sr).Cantidad_original
                End If

                dgrid.Rows(sr).Cells("colLote").ReadOnly = True
                dgrid.Columns("CantidadP").ReadOnly = True

                dgrid.Rows(sr).Cells("ColCantidad").Value = vCantidadOriginal
                dgrid.Rows(sr).Cells("CantidadP").Value = vCantidadOriginal

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

                Set_Valores_Ajuste_Cantidad(sr)
                Return

            End If

            If TipoAjuste_Por_Peso Then

                dgrid.Columns("ColCantidad").ReadOnly = False

                Dim currencyCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle()
                currencyCellStyle.Format = "N6"

                dgrid.Columns("ColCantidad").DefaultCellStyle = currencyCellStyle

                Set_Valores_Ajuste_Peso(sr)
                Return

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

            Dim vLoteOriginal As String = ""
            Dim vLoteNuevo As String = ""

            If chkBorrador.Checked Then
                vLoteOriginal = lBeTransAjusteDetBorrador(sr).lote_original
                vLoteNuevo = lBeTransAjusteDetBorrador(sr).lote_nuevo
            Else
                vLoteOriginal = lBeTransAjusteDet(sr).Lote_original
                vLoteNuevo = lBeTransAjusteDet(sr).Lote_nuevo
            End If

            dgrid.Rows(sr).Cells("LoteOrig").Value = vLoteOriginal

            If Modo = TipoTrans.Editar Then
                '#CKFK 20211214 Cambié la información porque en ColCantidad va el lote nuevo y en ColLote el original
                dgrid.Rows(sr).Cells("ColCantidad").Value = vLoteOriginal
                dgrid.Rows(sr).Cells("ColLote").Value = vLoteNuevo
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

            If chkBorrador.Checked Then
                dgrid.Rows(sr).Cells("CantidadP").Value = lBeTransAjusteDetBorrador(sr).fecha_vence_original
                If Modo = TipoTrans.Editar Then dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDetBorrador(sr).fecha_vence_nueva
            Else
                dgrid.Rows(sr).Cells("CantidadP").Value = lBeTransAjusteDet(sr).Fecha_vence_original
                If Modo = TipoTrans.Editar Then dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDet(sr).Fecha_vence_nueva
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

    Private Sub Set_Valores_Ajuste_Cantidad(sr As Integer)

        Dim vNuevaCant As Double = 0
        Dim vCantidadOriginal As Double = 0
        Dim vCantidadNueva As Double = 0
        Dim vIdTipoAjuste As Integer = 0

        Try

            dgrid.Columns("ColCantidad").ReadOnly = False

            If chkBorrador.Checked Then
                vCantidadOriginal = lBeTransAjusteDetBorrador(sr).cantidad_original
                vCantidadNueva = lBeTransAjusteDetBorrador(sr).cantidad_nueva
                vIdTipoAjuste = lBeTransAjusteDetBorrador(sr).idtipoajuste
            Else
                vCantidadOriginal = lBeTransAjusteDet(sr).Cantidad_original
                vCantidadNueva = lBeTransAjusteDet(sr).Cantidad_nueva
                vIdTipoAjuste = lBeTransAjusteDet(sr).Idtipoajuste
            End If

            If Modo = TipoTrans.Editar Then
                If Not chkBorrador.Checked Then
                    dgrid.Rows(sr).Cells("ColCantidad").Value = vCantidadOriginal - vCantidadNueva
                Else
                    dgrid.Rows(sr).Cells("ColCantidad").Value = vCantidadOriginal
                End If
            End If

            If vIdTipoAjuste = 3 Then
                vNuevaCant = vCantidadOriginal + dgrid.Rows(sr).Cells("ColCantidad").Value
            ElseIf vIdTipoAjuste = 5 Then
                vNuevaCant = vCantidadOriginal - dgrid.Rows(sr).Cells("ColCantidad").Value
            End If

            If vCantidadOriginal < vNuevaCant Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
            If vCantidadOriginal > vNuevaCant Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image

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

            If chkBorrador.Checked Then
                dgrid.Rows(sr).Cells("CantidadP").Value = lBeTransAjusteDetBorrador(sr).peso_original
                If Modo = TipoTrans.Editar Then dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDetBorrador(sr).peso_nuevo

                If lBeTransAjusteDetBorrador(sr).peso_original < lBeTransAjusteDetBorrador(sr).peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
                If lBeTransAjusteDetBorrador(sr).peso_original > lBeTransAjusteDetBorrador(sr).peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image
            Else
                dgrid.Rows(sr).Cells("CantidadP").Value = lBeTransAjusteDet(sr).Peso_original
                If Modo = TipoTrans.Editar Then dgrid.Rows(sr).Cells("ColCantidad").Value = lBeTransAjusteDet(sr).Peso_nuevo

                If lBeTransAjusteDet(sr).Peso_original < lBeTransAjusteDet(sr).Peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
                If lBeTransAjusteDet(sr).Peso_original > lBeTransAjusteDet(sr).Peso_nuevo Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image
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
            Return
        End Try

        Try

            If chkBorrador.Checked Then

                stocklink = lBeTransAjusteDetBorrador(sr).idstocklink

                If stocklink = 0 Then
                    lBeTransAjusteDetBorrador.RemoveAt(sr)
                    dgrid.Rows.RemoveAt(sr)
                Else
                    '#FIX_v13_DEL_STOCKLINK_2026-04-25 (G1):
                    'Borrar solo filas hermanas del mismo idstocklink.
                    For ii = lBeTransAjusteDetBorrador.Count - 1 To 0 Step -1
                        If lBeTransAjusteDetBorrador(ii).idstocklink = stocklink Then
                            lBeTransAjusteDetBorrador.RemoveAt(ii)
                            If ii < dgrid.Rows.Count Then
                                dgrid.Rows.RemoveAt(ii)
                            End If
                        End If
                    Next
                End If

            Else

                str.IdStockRes = lBeTransAjusteDet(sr).idstockres
                stocklink = lBeTransAjusteDet(sr).idstocklink

                If stocklink = 0 Then
                    clsLnStock_res.Eliminar(str)

                    lBeTransAjusteDet.RemoveAt(sr)
                    dgrid.Rows.RemoveAt(sr)
                Else
                    '#FIX_v13_DEL_STOCKLINK_2026-04-25 (G1):
                    'Borrar solo filas hermanas del mismo idstocklink.
                    For ii = lBeTransAjusteDet.Count - 1 To 0 Step -1

                        If lBeTransAjusteDet(ii).idstocklink = stocklink Then

                            If Not lBeTransAjusteDet(ii).esnuevolink Then
                                str.IdStockRes = lBeTransAjusteDet(ii).idstockres
                                clsLnStock_res.Eliminar(str)
                            End If

                            lBeTransAjusteDet.RemoveAt(ii)
                            If ii < dgrid.Rows.Count Then
                                dgrid.Rows.RemoveAt(ii)
                            End If

                        End If
                    Next
                End If

            End If

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

            ' #EJCRP 21042026: (consolida #ProveedorFix previo) pasaba 'ubic' como 3er
            ' argumento posicional y caía en la columna UmBas. Estandarizado: solo
            ' Codigo+Nombre por posición; el resto por nombre.
            rc = dgrid.Rows.Add(codigo, Item.Nombre_producto)
            dgrid.Rows(rc).Cells("UmBas").Value = Item.UmBas
            dgrid.Rows(rc).Cells("colUbicacion").Value = ubic
            dgrid.Rows(rc).Cells("colUbicacion").ReadOnly = True
            dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image
            ' #EJCRP 21042026: poblar Proveedor para el detalle del link de quiebre de lote.
            ' Item.IdStock representa el stock origen del nuevo link.
            dgrid.Rows(rc).Cells("ColProveedor").Value = Get_Proveedor_Texto(Item.IdStock)

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
        Try
            If dgrid Is Nothing OrElse dgrid.Rows.Count = 0 Then
                XtraMessageBox.Show("No hay lineas para imprimir.",
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Imprimir_Detalle_Ajuste_RC2026()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Try : clsLnLog_error_wms.Agregar_Error("mnuImprimir1_ItemClick: " & ex.Message) : Catch : End Try
        End Try
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
                        If chkBorrador.Checked Then
                            lBeTransAjusteDetBorrador(sr).idmotivoajuste = IdMotivoAjuste
                        Else
                            lBeTransAjusteDet(sr).IdMotivoAjuste = IdMotivoAjuste
                        End If
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
                '#FIX_v8_2026-04-25: defensa contra bug duplicado ajuste positivo.
                'La bandera Es_Ajuste_Positivo_Sin_Stock se prendÃ­a indebidamente desde
                'cmbTipoAjuste_EditValueChanged (linea 4795) al elegir IdTipoAjuste=3,
                'incluso cuando todos los detalles tenÃ­an stock previo (IdStock > 0).
                'Resultado: el flujo entraba en Guardar_Ajuste_Positivo_Sin_Stock que
                'usa Guardar_Stock_Ajuste_Positivo (DAL 16375) que SOLO hace INSERT,
                'creando un IdStock nuevo en lugar de actualizar el origen.
                '
                'Defensa: Ruta B (Guardar_Ajuste_Positivo_Sin_Stock) solo si REALMENTE
                'hay al menos un detalle SIN stock previo (IdStock = 0). En caso
                'contrario forzar Ruta A para que Procesar_Ajuste actualice el origen.
                Dim hayDetalleSinStockPrevio As Boolean =
                    lBeTransAjusteDet IsNot Nothing AndAlso
                    lBeTransAjusteDet.Any(Function(d) d.IdStock = 0)

                If Not Es_Ajuste_Positivo_Sin_Stock OrElse Not hayDetalleSinStockPrevio Then

                    If pBeTransAjustEnc.Ajuste_Por_Inventario > 0 Then
                        Actualizar_Ajuste()
                    Else
                        Guardar_Ajuste()
                    End If
                Else
                    '#GT27112024: ajuste positivo por este proceso
                    Guardar_Ajuste_Positivo_Sin_Stock()
                End If

                If Not InvokeListarAjustes Is Nothing Then
                    InvokeListarAjustes.Invoke
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function Validar_Datos() As Boolean

        Dim FechaVenceNueva, FechaOriginalVence As DateTime
        Dim sr As Integer
        Dim ValorGrd As Double
        Dim vNomTipoAjuste As String = ""
        Dim vNomMotivo As String = ""
        Dim vTalla As String = ""
        Dim vColor As String = ""

        Try

            'Si el usuario inició el ajuste en firme y luego marcó borrador,
            'sincronizar la lista borrador desde la lista principal.
            If chkBorrador.Checked Then

                If (lBeTransAjusteDetBorrador Is Nothing OrElse lBeTransAjusteDetBorrador.Count = 0) AndAlso
               (lBeTransAjusteDet IsNot Nothing AndAlso lBeTransAjusteDet.Count > 0) Then

                    lBeTransAjusteDetBorrador = New List(Of clsBeTrans_ajuste_det_borrador)

                    For Each item As clsBeTrans_ajuste_det In lBeTransAjusteDet

                        Dim oBorrador As New clsBeTrans_ajuste_det_borrador

                        With oBorrador
                            .idajustedet = item.IdAjusteDet
                            .idajusteenc = item.IdAjusteEnc
                            .IdStock = item.IdStock
                            .IdPropietarioBodega = item.IdPropietarioBodega
                            .IdProductoBodega = item.IdProductoBodega
                            .IdProductoEstado = item.IdProductoEstado
                            .IdPresentacion = item.IdPresentacion
                            .IdUnidadMedida = item.IdUnidadMedida
                            .IdUbicacion = item.IdUbicacion

                            .lote_original = item.Lote_original
                            .lote_nuevo = item.Lote_nuevo
                            .fecha_vence_original = item.Fecha_vence_original
                            .fecha_vence_nueva = item.Fecha_vence_nueva
                            .peso_original = item.Peso_original
                            .peso_nuevo = item.Peso_nuevo
                            .cantidad_original = item.Cantidad_original
                            .cantidad_nueva = item.Cantidad_nueva

                            .codigo_producto = item.Codigo_producto
                            .nombre_producto = item.Nombre_producto
                            .idtipoajuste = item.Idtipoajuste
                            .idmotivoajuste = item.IdMotivoAjuste
                            .observacion = item.Observacion
                            .codigo_ajuste = item.Codigo_ajuste
                            .enviado = item.Enviado
                            .IdBodegaERP = item.IdBodegaERP
                            .lic_plate = item.lic_plate
                            .referencia_ajuste_erp = item.referencia_ajuste_erp
                            .estado_ajuste_erp = item.estado_ajuste_erp

                            .idstockres = item.idstockres
                            .idstocklink = item.idstocklink
                            .esnuevolink = item.esnuevolink

                            .IdProductoTallaColor_origen = item.IdProductoTallaColor_origen
                            .Talla_origen = item.Talla_origen
                            .Color_origen = item.Color_origen
                            .IdProductoTallaColor_destino = item.IdProductoTallaColor_destino
                            .Talla_destino = item.Talla_destino
                            .Color_destino = item.Color_destino

                            .UmBas = item.UmBas
                            .Factor = item.Factor
                            .Nombre_Presentacion = item.Nombre_Presentacion
                            .CantReservada = item.CantReservada
                            .Presentacion = item.Presentacion
                        End With

                        lBeTransAjusteDetBorrador.Add(oBorrador)

                    Next

                End If

            End If

            For sr = 0 To dgrid.Rows.Count - 1

                Dim esBorrador As Boolean = chkBorrador.Checked

                Dim vIdStockLink As Integer = If(esBorrador, lBeTransAjusteDetBorrador(sr).idstocklink, lBeTransAjusteDet(sr).idstocklink)
                Dim vIdTipoAjusteActual As Integer = If(esBorrador, lBeTransAjusteDetBorrador(sr).idtipoajuste, lBeTransAjusteDet(sr).Idtipoajuste)
                Dim vCantidadOriginal As Double = If(esBorrador, lBeTransAjusteDetBorrador(sr).cantidad_original, lBeTransAjusteDet(sr).Cantidad_original)
                Dim vPesoOriginal As Double = If(esBorrador, lBeTransAjusteDetBorrador(sr).peso_original, lBeTransAjusteDet(sr).Peso_original)
                Dim vTallaOrigen As String = If(esBorrador, lBeTransAjusteDetBorrador(sr).Talla_origen, lBeTransAjusteDet(sr).Talla_origen)
                Dim vColorOrigen As String = If(esBorrador, lBeTransAjusteDetBorrador(sr).Color_origen, lBeTransAjusteDet(sr).Color_origen)

                vNomTipoAjuste = IIf(IsDBNull(dgrid.Rows(sr).Cells("tipoajuste").EditedFormattedValue), "", dgrid.Rows(sr).Cells("tipoajuste").EditedFormattedValue)

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

                If esBorrador Then
                    lBeTransAjusteDetBorrador(sr).idmotivoajuste = IdMotivoAjuste
                    lBeTransAjusteDetBorrador(sr).observacion = dgrid.Rows(sr).Cells("ColObservacion").Value
                Else
                    lBeTransAjusteDet(sr).IdMotivoAjuste = IdMotivoAjuste
                    lBeTransAjusteDet(sr).Observacion = dgrid.Rows(sr).Cells("ColObservacion").Value
                End If

                If TipoAjuste_Por_lote Then

                    If vIdStockLink = 0 Then
                        If Not Es_Ajuste_Positivo_Sin_Stock Then
                            If (dgrid.Rows(sr).Cells("LoteOrig").Value = dgrid.Rows(sr).Cells("ColCantidad").Value) Then
                                XtraMessageBox.Show("Linea : " & sr + 1 & " Valor original y nuevo deben ser distintos !", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return False
                            End If
                        End If
                    End If

                    If esBorrador Then
                        lBeTransAjusteDetBorrador(sr).lote_nuevo = dgrid.Rows(sr).Cells("ColCantidad").Value
                        lBeTransAjusteDetBorrador(sr).codigo_ajuste = 16
                    Else
                        lBeTransAjusteDet(sr).Lote_nuevo = dgrid.Rows(sr).Cells("ColCantidad").Value
                        lBeTransAjusteDet(sr).Codigo_ajuste = 16
                    End If

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

                        If vIdStockLink = 0 Then
                            If (FechaOriginalVence = FechaVenceNueva) Then
                                XtraMessageBox.Show("Linea : " & sr + 1 & " Valor original y nuevo deben ser distintos !", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return False
                            End If
                        End If

                        If esBorrador Then
                            lBeTransAjusteDetBorrador(sr).fecha_vence_nueva = FechaVenceNueva
                            lBeTransAjusteDetBorrador(sr).codigo_ajuste = 15
                        Else
                            lBeTransAjusteDet(sr).Fecha_vence_nueva = FechaVenceNueva
                            lBeTransAjusteDet(sr).Codigo_ajuste = 15
                        End If

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
                            vNuevaCantidad = Val(dgrid.Rows(sr).Cells("CantidadP").Value) + Val(dgrid.Rows(sr).Cells("ColCantidad").Value)
                        ElseIf dgrid.Rows(sr).Cells("tipoajuste").Value = 5 Then
                            vNuevaCantidad = Val(dgrid.Rows(sr).Cells("CantidadP").Value) - Val(dgrid.Rows(sr).Cells("ColCantidad").Value)
                        End If

                        If vIdStockLink = 0 Then
                            If (dgrid.Rows(sr).Cells("CantidadP").Value = vNuevaCantidad) Then
                                XtraMessageBox.Show("Linea : " & sr + 1 & " Valor original y nuevo deben ser distintos !", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return False
                            End If
                        End If

                        If dgrid.Rows(sr).Cells("ColCantidad").Value.ToString = "" Then Throw New Exception
                        ValorGrd = vNuevaCantidad

                        If ValorGrd < 0 Then Throw New Exception

                        If esBorrador Then
                            lBeTransAjusteDetBorrador(sr).cantidad_nueva = ValorGrd

                            If vIdTipoAjusteActual = 3 Then
                                lBeTransAjusteDetBorrador(sr).codigo_ajuste = 13
                            ElseIf vIdTipoAjusteActual = 5 Then
                                lBeTransAjusteDetBorrador(sr).codigo_ajuste = 17
                            End If
                        Else
                            lBeTransAjusteDet(sr).Cantidad_nueva = ValorGrd

                            If vIdTipoAjusteActual = 3 Then
                                lBeTransAjusteDet(sr).Codigo_ajuste = 13
                            ElseIf vIdTipoAjusteActual = 5 Then
                                lBeTransAjusteDet(sr).Codigo_ajuste = 17
                            End If
                        End If

                        If vCantidadOriginal < ValorGrd Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
                        If vCantidadOriginal > ValorGrd Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image
                        If vCantidadOriginal = ValorGrd Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image

                    Catch ex As Exception
                        XtraMessageBox.Show("Linea : " & sr + 1 & " el valor ingresado en el campo cantidad es incorrecto", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End Try

                End If

                If TipoAjuste_Por_Peso Then

                    Try

                        If vIdStockLink = 0 Then
                            If (dgrid.Rows(sr).Cells("CantidadP").Value = dgrid.Rows(sr).Cells("ColCantidad").Value) Then
                                XtraMessageBox.Show("Linea : " & sr + 1 & " Valor original y nuevo deben ser distinctos !", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return False
                            End If
                        End If

                        If dgrid.Rows(sr).Cells("ColCantidad").Value = "" Then Throw New Exception
                        ValorGrd = dgrid.Rows(sr).Cells("ColCantidad").Value
                        If ValorGrd < 0 Then Throw New Exception

                        If esBorrador Then
                            lBeTransAjusteDetBorrador(sr).peso_nuevo = ValorGrd
                            lBeTransAjusteDetBorrador(sr).codigo_ajuste = 14
                        Else
                            lBeTransAjusteDet(sr).Peso_nuevo = ValorGrd
                            lBeTransAjusteDet(sr).Codigo_ajuste = 14
                        End If

                        If vPesoOriginal < ValorGrd Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox2.Image
                        If vPesoOriginal > ValorGrd Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox3.Image
                        If vPesoOriginal = ValorGrd Then dgrid.Rows(sr).Cells("ColDiferencia").Value = PictureBox1.Image

                    Catch ex As Exception
                        XtraMessageBox.Show("Linea : " & sr + 1 & " Valor peso incorrecto", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End Try
                End If

                If BeBodega.Control_Talla_Color Then

                    vTalla = dgrid.Rows(sr).Cells("ColTalla").FormattedValue
                    vColor = dgrid.Rows(sr).Cells("ColColor").FormattedValue

                    If vTallaOrigen = "" Then
                        XtraMessageBox.Show("Linea : " & sr + 1 & " debe seleccionar una talla.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    If vColorOrigen = "" Then
                        XtraMessageBox.Show("Linea : " & sr + 1 & " debe seleccionar un color.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    If esBorrador Then
                        If vTalla <> vTallaOrigen Then
                            lBeTransAjusteDetBorrador(sr).Talla_destino = vTalla
                        End If

                        If vColor <> vColorOrigen Then
                            lBeTransAjusteDetBorrador(sr).Color_destino = vColor
                        End If
                    Else
                        If vTalla <> vTallaOrigen Then
                            lBeTransAjusteDet(sr).Talla_destino = vTalla
                        End If

                        If vColor <> vColorOrigen Then
                            lBeTransAjusteDet(sr).Color_destino = vColor
                        End If
                    End If

                End If

            Next

            Return True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Return False

        End Try

    End Function

    '#FIX_v14_ELIMINAR_AJUSTE_2026-04-25 (H1):
    'Elimina el ajuste actual solo si no tiene detalle.
    Public Sub Eliminar_Ajuste_Si_Sin_Detalle()
        Try
            If pBeTransAjustEnc Is Nothing OrElse
               pBeTransAjustEnc.IdAjusteenc <= 0 Then
                XtraMessageBox.Show(
                    "No hay un ajuste cargado para eliminar.",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim filasGrid As Integer = 0
            For Each row As DataGridViewRow In dgrid.Rows
                If Not row.IsNewRow Then filasGrid += 1
            Next
            If filasGrid > 0 Then
                XtraMessageBox.Show(
                    "El ajuste tiene " & filasGrid & " fila(s) en el grid. Elimine los detalles antes de eliminar el ajuste.",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim detalles As List(Of clsBeTrans_ajuste_det) =
                clsLnTrans_ajuste_det.Get_By_IdAjusteEnc(pBeTransAjustEnc.IdAjusteenc)
            If detalles IsNot Nothing AndAlso detalles.Count > 0 Then
                XtraMessageBox.Show(
                    "El ajuste tiene " & detalles.Count & " detalle(s) persistido(s) en Trans_ajuste_det. Elimine los detalles antes de eliminar el ajuste.",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim detallesBorr As List(Of clsBeTrans_ajuste_det_borrador) =
                clsLnTrans_ajuste_det_borrador.Get_By_IdAjusteEnc(pBeTransAjustEnc.IdAjusteenc)
            If detallesBorr IsNot Nothing AndAlso detallesBorr.Count > 0 Then
                XtraMessageBox.Show(
                    "El ajuste tiene " & detallesBorr.Count & " detalle(s) en estado borrador. Elimine los detalles antes.",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim resp As DialogResult = XtraMessageBox.Show(
                "Confirma eliminar el ajuste #" & pBeTransAjustEnc.IdAjusteenc &
                "?" & vbCrLf & vbCrLf & "Esta operación es irreversible.",
                "Eliminar ajuste sin detalle",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2)
            If resp <> DialogResult.Yes Then Return

            clsLnTrans_ajuste_enc.RollBackStockRes(pBeTransAjustEnc)

            XtraMessageBox.Show(
                "Ajuste eliminado correctamente.",
                Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Guardado = True
            Me.Close()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            clsLnLog_error_wms.Agregar_Error(
                "Eliminar_Ajuste_Si_Sin_Detalle: " & ex.Message)
        End Try
    End Sub

    Private Sub Guardar_Ajuste()

        Dim cc, ic As Integer
        Dim Codigo As String, Lote As String, IdAjusteDet As Integer
        Dim Enviar As Boolean = False
        Dim IdBodegaERP As Integer
        Dim CantidadRegistrosEnviados As Integer = 0
        Dim EsBorrador As Boolean = chkBorrador.Checked

        Try

            'Si por alguna razón la lista principal está vacía pero existe la de borrador,
            'la reconstruimos para trabajar siempre con una sola lista base.
            If (lBeTransAjusteDet Is Nothing OrElse lBeTransAjusteDet.Count = 0) AndAlso
           (lBeTransAjusteDetBorrador IsNot Nothing AndAlso lBeTransAjusteDetBorrador.Count > 0) Then

                lBeTransAjusteDet = New List(Of clsBeTrans_ajuste_det)

                For Each itemBorrador As clsBeTrans_ajuste_det_borrador In lBeTransAjusteDetBorrador

                    Dim oDet As New clsBeTrans_ajuste_det

                    With oDet
                        .IdAjusteDet = itemBorrador.idajustedet
                        .IdAjusteEnc = itemBorrador.idajusteenc
                        .IdStock = itemBorrador.IdStock
                        .IdPropietarioBodega = itemBorrador.IdPropietarioBodega
                        .IdProductoBodega = itemBorrador.IdProductoBodega
                        .IdProductoEstado = itemBorrador.IdProductoEstado
                        .IdPresentacion = itemBorrador.IdPresentacion
                        .IdUnidadMedida = itemBorrador.IdUnidadMedida
                        .IdUbicacion = itemBorrador.IdUbicacion
                        .Lote_original = itemBorrador.lote_original
                        .Lote_nuevo = itemBorrador.lote_nuevo
                        .Fecha_vence_original = itemBorrador.fecha_vence_original
                        .Fecha_vence_nueva = itemBorrador.fecha_vence_nueva
                        .Peso_original = itemBorrador.peso_original
                        .Peso_nuevo = itemBorrador.peso_nuevo
                        .Cantidad_original = itemBorrador.cantidad_original
                        .Cantidad_nueva = itemBorrador.cantidad_nueva
                        .Codigo_producto = itemBorrador.codigo_producto
                        .Nombre_producto = itemBorrador.nombre_producto
                        .Idtipoajuste = itemBorrador.idtipoajuste
                        .IdMotivoAjuste = itemBorrador.idmotivoajuste
                        .Observacion = itemBorrador.observacion
                        .Codigo_ajuste = itemBorrador.codigo_ajuste
                        .Enviado = itemBorrador.enviado
                        .IdBodegaERP = itemBorrador.IdBodegaERP
                        .lic_plate = itemBorrador.lic_plate
                        .referencia_ajuste_erp = itemBorrador.referencia_ajuste_erp
                        .estado_ajuste_erp = itemBorrador.estado_ajuste_erp
                        .idstockres = itemBorrador.idstockres
                        .idstocklink = itemBorrador.idstocklink
                        .esnuevolink = itemBorrador.esnuevolink
                        .IdProductoTallaColor_origen = itemBorrador.IdProductoTallaColor_origen
                        .Talla_origen = itemBorrador.Talla_origen
                        .Color_origen = itemBorrador.Color_origen
                        .IdProductoTallaColor_destino = itemBorrador.IdProductoTallaColor_destino
                        .Talla_destino = itemBorrador.Talla_destino
                        .Color_destino = itemBorrador.Color_destino
                        .UmBas = itemBorrador.UmBas
                        .Factor = itemBorrador.Factor
                        .Nombre_Presentacion = itemBorrador.Nombre_Presentacion

                        If .IdPresentacion <> 0 Then
                            .Presentacion = clsLnProducto_presentacion.GetSingle(.IdPresentacion)
                        End If
                    End With

                    lBeTransAjusteDet.Add(oDet)

                Next

            End If

            '#FIX_v13_SYNC_GUARDAR_2026-04-25 (G2):
            'Reconciliar lista vs grid antes de iterar por índice.
            Sincronizar_Lista_Con_Grid()

            If lBeTransAjusteDet Is Nothing OrElse lBeTransAjusteDet.Count = 0 Then
                Throw New Exception("No hay registros para guardar.")
            End If

            '#FIX_v15_RECUPERAR_PRODUCTO_2026-04-25 (I2): completar código/nombre faltantes.
            For Each detItem As clsBeTrans_ajuste_det In lBeTransAjusteDet
                If String.IsNullOrWhiteSpace(detItem.Codigo_producto) OrElse
                   String.IsNullOrWhiteSpace(detItem.Nombre_producto) Then
                    Try
                        If detItem.IdProductoBodega > 0 Then
                            Dim idProd As Integer =
                                clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(
                                    detItem.IdProductoBodega)
                            If idProd > 0 Then
                                Dim prod = clsLnProducto.Get_Single_By_IdProducto(idProd)
                                If prod IsNot Nothing Then
                                    If String.IsNullOrWhiteSpace(detItem.Codigo_producto) Then
                                        detItem.Codigo_producto = prod.Codigo
                                    End If
                                    If String.IsNullOrWhiteSpace(detItem.Nombre_producto) Then
                                        detItem.Nombre_producto = prod.Nombre
                                    End If
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        clsLnLog_error_wms.Agregar_Error(
                            "v15.I2 Recuperar producto IdProductoBodega=" &
                            detItem.IdProductoBodega & ": " & ex.Message)
                    End Try
                End If
            Next

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
            pBeTransAjustEnc.Borrador = EsBorrador

            If Not lcmbCentroCosto.EditValue Is Nothing Then
                pBeTransAjustEnc.IdCentroCosto = lcmbCentroCosto.EditValue
            End If

            cc = 0

            For i = 0 To lBeTransAjusteDet.Count - 1

                lBeTransAjusteDet(i).IdAjusteEnc = pBeTransAjustEnc.IdAjusteenc
                lBeTransAjusteDet(i).IdBodegaERP = IdBodegaERP
                lBeTransAjusteDet(i).Idtipoajuste = Val(dgrid.Rows(i).Cells("tipoajuste").Value)

                ic = 0
                If lBeTransAjusteDet(i).Lote_original <> lBeTransAjusteDet(i).Lote_nuevo Then ic += 1
                If lBeTransAjusteDet(i).Fecha_vence_original <> lBeTransAjusteDet(i).Fecha_vence_nueva Then ic += 1
                If lBeTransAjusteDet(i).Cantidad_original <> lBeTransAjusteDet(i).Cantidad_nueva Then ic += 1
                If lBeTransAjusteDet(i).Peso_original <> lBeTransAjusteDet(i).Peso_nuevo Then ic += 1
                If ic > 0 Then cc += 1

                '#EJC20180924: Asignación de bodega de ERP.
                '#FIX_v13_SYNC_GUARDAR_2026-04-25 (G3): usar la fila del bucle.
                DgComboBodega = TryCast(dgrid.Rows(i).Cells("ColBodega"), DataGridViewComboBoxCell)
                '#CM_20200219: Se cambio el IdBodega en lugar de IdBodegaErp
                If BeBodega.Bodega_Cliente_Ajuste_ByB Then
                    IdBodegaERP = cmbBodegaERP.EditValue
                Else
                    IdBodegaERP = 0
                End If
                lBeTransAjusteDet(i).IdBodegaERP = IdBodegaERP

                '#GT13062022_1906: Asignación del tipoajuste seteado en el grid.     
                lBeTransAjusteDet(i).Idtipoajuste = dgrid.Rows(i).Cells("tipoajuste").Value

            Next

            For i = 0 To dgrid.Rows.Count - 1

                Codigo = IIf(IsDBNull(dgrid.Rows(i).Cells("ColCodigoProducto").Value), "", dgrid.Rows(i).Cells("ColCodigoProducto").Value)
                Lote = IIf(IsDBNull(dgrid.Rows(i).Cells("ColLote").Value), "", dgrid.Rows(i).Cells("ColLote").Value)
                IdAjusteDet = IIf(IsDBNull(dgrid.Rows(i).Cells("ColIdAjusteDEt").Value), 0, dgrid.Rows(i).Cells("ColIdAjusteDEt").Value)
                Enviar = IIf(IsDBNull(dgrid.Rows(i).Cells("ColEnviadoAErp").Value), False, dgrid.Rows(i).Cells("ColEnviadoAErp").Value)

                BeAjusteDet = lBeTransAjusteDet.Find(Function(x) x.Codigo_producto = Codigo _
                                                AndAlso x.Lote_nuevo = Lote _
                                                AndAlso x.IdAjusteDet = IdAjusteDet)

                ''#GT28112024: al ser ajuste positivo, el idstock se obtiene hasta que se presiona guardar
                'If Es_Ajuste_Positivo_Sin_Stock Then
                '    BeAjusteDet.IdStock = pStock_Sin_Existencia_Previa.IdStock
                'End If

                'If Es_Ajuste_Positivo_Sin_Stock AndAlso Not BeAjusteDet Is Nothing Then
                '    BeAjusteDet.IdStock = pStock_Sin_Existencia_Previa.IdStock
                'End If

                If Not BeAjusteDet Is Nothing Then
                    BeAjusteDet.Enviado = Enviar
                    If Enviar Then
                        CantidadRegistrosEnviados += 1
                    End If
                End If

            Next

            Validar_Talla_Color()
            If EsBorrador Then

                clsLnTrans_ajuste_enc.Guardar_Borrador_Ajuste(pBeTransAjustEnc,
                                                              lBeTransAjusteDet)

            Else

                Crear_Movimientos()

                clsLnTrans_ajuste_enc.Aplicar_Ajuste(pBeTransAjustEnc,
                                                     lBeTransAjusteDet,
                                                     lBeTransMovimientos)

            End If

            If CantidadRegistrosEnviados = lBeTransAjusteDet.Count Then

                If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.IdAjusteenc, True) > 0 Then
                    XtraMessageBox.Show("Se guardó el ajuste y se actualizó a enviado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Else

                If EsBorrador Then
                    XtraMessageBox.Show("Se guardó el borrador correctamente, puede reanudar mas tarde.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    XtraMessageBox.Show("Ajuste aplicado correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If

            Guardado = True

            mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuImprimir1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            txtReferencia.Enabled = False
            cmbBodegaERP.Enabled = False
            cmbProductoFamilia.Enabled = False
            cmbPropietarioBodega.Enabled = False
            dtpFecha.Enabled = False
            cmdAdd.Visible = False
            mnuDel.Visible = False
            mnuDividir.Visible = False
            lcmbCentroCosto.Enabled = False

            dgrid.Enabled = True

            Set_Estado_Envio_A_ERP()

            Deshabilita_Grid()

            If Not EsBorrador Then
                'Llenar_DS_Rep()
                Imprimir_Detalle_Ajuste_RC2026()
            End If

            If EsBorrador Then Close()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Validar_Talla_Color()

        Dim clsTransaccion As New clsTransaccion

        Try

            clsTransaccion.Begin_Transaction()

            If chkBorrador.Checked Then

                For Each pAjusteDet As clsBeTrans_ajuste_det_borrador In lBeTransAjusteDetBorrador

                    If pAjusteDet.IdProductoTallaColor_destino = 0 Then

                        If pAjusteDet.Talla_origen <> pAjusteDet.Talla_destino OrElse pAjusteDet.Color_origen <> pAjusteDet.Color_destino Then

                            Dim pExisteProductoConTallaColor As New clsBeProducto_talla_color()
                            Dim vIdProductoBodega = pAjusteDet.IdProductoBodega

                            Dim BeProducto = clsLnProducto_bodega.Get_BeProducto_By_IdProductoBodega(vIdProductoBodega,
                                                                                                 clsTransaccion.lConnection,
                                                                                                 clsTransaccion.lTransaction)

                            Dim vTalla As String = IIf(Not String.IsNullOrEmpty(pAjusteDet.Talla_destino), pAjusteDet.Talla_destino, pAjusteDet.Talla_origen)
                            Dim vColor As String = IIf(Not String.IsNullOrEmpty(pAjusteDet.Color_destino), pAjusteDet.Color_destino, pAjusteDet.Color_origen)

                            pExisteProductoConTallaColor = clsLnProducto_talla_color.Get_Single_By_IdColor_IdTalla(BeProducto.IdProducto,
                                                                                                               vTalla,
                                                                                                               vColor,
                                                                                                               clsTransaccion.lConnection,
                                                                                                               clsTransaccion.lTransaction)

                            If pExisteProductoConTallaColor IsNot Nothing Then
                                pAjusteDet.IdProductoTallaColor_destino = pExisteProductoConTallaColor.IdProductoTallaColor
                            Else
                                Dim pProducto_Talla_color As New clsBeProducto_talla_color()
                                Dim pTalla As New clsBeTalla()
                                Dim pColor As New clsBeColor()

                                pTalla = clsLnTalla.Get_Single_By_Codigo(vTalla, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                pColor = clsLnColor.Get_Single_By_Codigo(vColor, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                                pProducto_Talla_color.IdProductoTallaColor = clsLnProducto_talla_color.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
                                pProducto_Talla_color.IdColor = pColor.IdColor
                                pProducto_Talla_color.IdTalla = pTalla.IdTalla
                                pProducto_Talla_color.IdProducto = BeProducto.IdProducto
                                pProducto_Talla_color.Activo = 1
                                pProducto_Talla_color.Fec_mod = Now
                                pProducto_Talla_color.Fec_agr = Now
                                pProducto_Talla_color.IdCampaña = 0
                                pProducto_Talla_color.User_agr = 1
                                pProducto_Talla_color.User_mod = 1
                                pProducto_Talla_color.CodigoSKU = BeProducto.Codigo + pColor.Codigo + pTalla.Codigo

                                clsLnProducto_talla_color.Insertar(pProducto_Talla_color, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                                pAjusteDet.IdProductoTallaColor_destino = pProducto_Talla_color.IdProductoTallaColor
                                pAjusteDet.Talla_destino = pTalla.Codigo
                                pAjusteDet.Color_destino = pColor.Codigo
                            End If

                        End If

                    End If

                Next

            Else

                For Each pAjusteDet As clsBeTrans_ajuste_det In lBeTransAjusteDet

                    If pAjusteDet.IdProductoTallaColor_destino = 0 Then

                        If pAjusteDet.Talla_origen <> pAjusteDet.Talla_destino OrElse pAjusteDet.Color_origen <> pAjusteDet.Color_destino Then

                            Dim pExisteProductoConTallaColor As New clsBeProducto_talla_color()
                            Dim vIdProductoBodega = pAjusteDet.IdProductoBodega

                            Dim BeProducto = clsLnProducto_bodega.Get_BeProducto_By_IdProductoBodega(vIdProductoBodega,
                                                                                                 clsTransaccion.lConnection,
                                                                                                 clsTransaccion.lTransaction)

                            Dim vTalla As String = IIf(Not String.IsNullOrEmpty(pAjusteDet.Talla_destino), pAjusteDet.Talla_destino, pAjusteDet.Talla_origen)
                            Dim vColor As String = IIf(Not String.IsNullOrEmpty(pAjusteDet.Color_destino), pAjusteDet.Color_destino, pAjusteDet.Color_origen)

                            pExisteProductoConTallaColor = clsLnProducto_talla_color.Get_Single_By_IdColor_IdTalla(BeProducto.IdProducto,
                                                                                                               vTalla,
                                                                                                               vColor,
                                                                                                               clsTransaccion.lConnection,
                                                                                                               clsTransaccion.lTransaction)

                            If pExisteProductoConTallaColor IsNot Nothing Then
                                pAjusteDet.IdProductoTallaColor_destino = pExisteProductoConTallaColor.IdProductoTallaColor
                            Else
                                Dim pProducto_Talla_color As New clsBeProducto_talla_color()
                                Dim pTalla As New clsBeTalla()
                                Dim pColor As New clsBeColor()

                                pTalla = clsLnTalla.Get_Single_By_Codigo(vTalla, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                pColor = clsLnColor.Get_Single_By_Codigo(vColor, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                                pProducto_Talla_color.IdProductoTallaColor = clsLnProducto_talla_color.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
                                pProducto_Talla_color.IdColor = pColor.IdColor
                                pProducto_Talla_color.IdTalla = pTalla.IdTalla
                                pProducto_Talla_color.IdProducto = BeProducto.IdProducto
                                pProducto_Talla_color.Activo = 1
                                pProducto_Talla_color.Fec_mod = Now
                                pProducto_Talla_color.Fec_agr = Now
                                pProducto_Talla_color.IdCampaña = 0
                                pProducto_Talla_color.User_agr = 1
                                pProducto_Talla_color.User_mod = 1
                                pProducto_Talla_color.CodigoSKU = BeProducto.Codigo + pColor.Codigo + pTalla.Codigo

                                clsLnProducto_talla_color.Insertar(pProducto_Talla_color, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                                pAjusteDet.IdProductoTallaColor_destino = pProducto_Talla_color.IdProductoTallaColor
                                pAjusteDet.Talla_destino = pTalla.Codigo
                                pAjusteDet.Color_destino = pColor.Codigo
                            End If

                        End If

                    End If

                Next

            End If

            clsTransaccion.Commit_Transaction()
            clsTransaccion.Close_Conection()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            clsTransaccion.Close_Conection()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '#FIX_v13_SYNC_GUARDAR_2026-04-25 (G2 helper) — REWRITE v16:
    'Reconstruye lBeTransAjusteDet para que refleje exactamente el grid.
    Private Sub Sincronizar_Lista_Con_Grid()
        Dim lNueva As New List(Of clsBeTrans_ajuste_det)
        Dim lOriginal As List(Of clsBeTrans_ajuste_det) =
            If(lBeTransAjusteDet, New List(Of clsBeTrans_ajuste_det))

        Dim filasGrid As New List(Of DataGridViewRow)
        For iRow As Integer = 0 To dgrid.Rows.Count - 1
            If Not dgrid.Rows(iRow).IsNewRow Then filasGrid.Add(dgrid.Rows(iRow))
        Next

        Dim modoPosicional As Boolean = (filasGrid.Count = lOriginal.Count)

        For i As Integer = 0 To filasGrid.Count - 1

            Dim row As DataGridViewRow = filasGrid(i)

            Dim Codigo As String = If(
                IsDBNull(row.Cells("ColCodigoProducto").Value),
                "", CStr(row.Cells("ColCodigoProducto").Value))
            Dim Lote As String = If(
                IsDBNull(row.Cells("ColLote").Value),
                "", CStr(row.Cells("ColLote").Value))
            Dim IdAjusteDet As Integer = If(
                IsDBNull(row.Cells("ColIdAjusteDEt").Value),
                0, CInt(row.Cells("ColIdAjusteDEt").Value))

            Dim found As clsBeTrans_ajuste_det = Nothing

            '1) Match nominal en lista principal
            found = lOriginal.Find(Function(x) x.Codigo_producto = Codigo _
                                    AndAlso x.Lote_nuevo = Lote _
                                    AndAlso x.IdAjusteDet = IdAjusteDet)

            '2) Fallback nominal en borrador
            If found Is Nothing AndAlso
               lBeTransAjusteDetBorrador IsNot Nothing Then
                Dim foundBorr As clsBeTrans_ajuste_det_borrador =
                    lBeTransAjusteDetBorrador.Find(
                        Function(x) x.codigo_producto = Codigo _
                          AndAlso x.lote_nuevo = Lote _
                          AndAlso x.idajustedet = IdAjusteDet)
                If foundBorr IsNot Nothing Then
                    found = Convertir_Borrador_A_Detalle(foundBorr)
                End If
            End If

            '3) FIX_v16_J1: fallback posicional + backfill de campos vacíos
            If found Is Nothing AndAlso modoPosicional AndAlso i < lOriginal.Count Then
                found = lOriginal(i)

                If String.IsNullOrWhiteSpace(found.Codigo_producto) Then
                    found.Codigo_producto = Codigo
                End If

                Try
                    Dim nombreGrid As String = If(
                        IsDBNull(row.Cells("ColNombreProducto").Value),
                        "", CStr(row.Cells("ColNombreProducto").Value))
                    If String.IsNullOrWhiteSpace(found.Nombre_producto) AndAlso
                       Not String.IsNullOrWhiteSpace(nombreGrid) Then
                        found.Nombre_producto = nombreGrid
                    End If
                Catch
                End Try

                If String.IsNullOrWhiteSpace(found.Lote_nuevo) AndAlso
                   Not String.IsNullOrWhiteSpace(Lote) Then
                    found.Lote_nuevo = Lote
                End If
            End If

            If found IsNot Nothing Then
                lNueva.Add(found)
            Else
                Throw New Exception(String.Format(
                    "Fila {0} del grid (Código={1}, Lote={2}, IdAjusteDet={3}) no tiene detalle asociado en memoria. Cierre el ajuste y vuelva a abrirlo para refrescar.",
                    i + 1, Codigo, Lote, IdAjusteDet))
            End If
        Next

        lBeTransAjusteDet = lNueva

        'FIX_v16_J2: usar mapeo explícito al espejo borrador
        If chkBorrador.Checked AndAlso lBeTransAjusteDetBorrador IsNot Nothing Then
            Dim lNuevoBorr As New List(Of clsBeTrans_ajuste_det_borrador)
            Dim fechaActual As Date = Now
            Dim usuarioActual As String = AP.UsuarioAp.IdUsuario.ToString()
            For Each d As clsBeTrans_ajuste_det In lNueva
                lNuevoBorr.Add(MapearDetalleABorrador(d, fechaActual, usuarioActual))
            Next
            lBeTransAjusteDetBorrador = lNuevoBorr
        End If
    End Sub

    Private Sub Guardar_Ajuste_Positivo(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction)

        Dim ss As String = ""
        Dim cc, ic As Integer
        Dim Codigo As String, Lote As String, IdAjusteDet As Integer
        Dim Enviar As Boolean = False
        Dim IdBodegaERP As Integer
        Dim CantidadRegistrosEnviados As Integer = 0
        Dim pStock_Sin_Existencia_Previa As New clsBeStock

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

                pStock_Sin_Existencia_Previa = New clsBeStock
                pStock_Sin_Existencia_Previa.IsNew = 1
                pStock_Sin_Existencia_Previa.IdPropietarioBodega = lBeTransAjusteDet(I).IdPropietarioBodega
                pStock_Sin_Existencia_Previa.IdProductoBodega = lBeTransAjusteDet(I).IdProductoBodega
                pStock_Sin_Existencia_Previa.IdUnidadMedida = lBeTransAjusteDet(I).IdUnidadMedida
                pStock_Sin_Existencia_Previa.Fecha_Ingreso = Now
                pStock_Sin_Existencia_Previa.Fecha_vence = lBeTransAjusteDet(I).Fecha_vence_nueva
                pStock_Sin_Existencia_Previa.IdPresentacion = lBeTransAjusteDet(I).IdPresentacion
                pStock_Sin_Existencia_Previa.IdProductoEstado = lBeTransAjusteDet(I).IdProductoEstado
                pStock_Sin_Existencia_Previa.ProductoEstado = New clsBeProducto_estado
                'pStock_Sin_Existencia_Previa.ProductoEstado = clsLnProducto_estado.Get_Single_By_IdEstado(BeAjusteDet.IdProductoEstado)
                pStock_Sin_Existencia_Previa.ProductoEstado = clsLnProducto_estado.GetSingleByIdEstado(lBeTransAjusteDet(I).IdProductoEstado, lConnection, lTransaction)
                pStock_Sin_Existencia_Previa.IdUbicacion = lBeTransAjusteDet(I).IdUbicacion
                pStock_Sin_Existencia_Previa.IdUbicacion_anterior = lBeTransAjusteDet(I).IdUbicacion
                pStock_Sin_Existencia_Previa.Cantidad = 1 '#GT28112024: al no existir stock se inserta con 1 por defecto
                pStock_Sin_Existencia_Previa.Lic_plate = lBeTransAjusteDet(I).lic_plate
                pStock_Sin_Existencia_Previa.Lote = lBeTransAjusteDet(I).Lote_nuevo
                pStock_Sin_Existencia_Previa.Peso = lBeTransAjusteDet(I).Peso_nuevo
                pStock_Sin_Existencia_Previa.User_agr = AP.UsuarioAp.IdUsuario
                pStock_Sin_Existencia_Previa.Fec_agr = Now
                pStock_Sin_Existencia_Previa.User_mod = AP.UsuarioAp.IdUsuario
                pStock_Sin_Existencia_Previa.Fec_mod = Now
                pStock_Sin_Existencia_Previa.IdBodega = AP.IdBodega
                pStock_Sin_Existencia_Previa.Activo = 1
                '#GT28082025: talla/color    
                pStock_Sin_Existencia_Previa.IdProductoTallaColor = lBeTransAjusteDet(I).IdProductoTallaColor_destino

                '#GT02122024: se inserta primero el stock antes de reservarlo, y hacer el ajuste positivo
                If clsLnStock.Guardar_Stock_Ajuste_Positivo(pStock_Sin_Existencia_Previa,
                                                            lConnection,
                                                            lTransaction) Then

                    lBeTransAjusteDet(I).IdStock = pStock_Sin_Existencia_Previa.IdStock

                    If Reservar_Stock(pStock_Sin_Existencia_Previa.IdStock, lConnection, lTransaction) Then

                        Try
                            Incrementar_Licencia_BOF_By_Ajuste_Positivo(AP.IdBodega, AP.UsuarioAp.IdUsuario, lConnection, lTransaction)
                        Catch ex As Exception
                        End Try

                    End If

                End If

                lBeTransAjusteDet(I).IdAjusteEnc = pBeTransAjustEnc.IdAjusteenc : ic = 0

                If lBeTransAjusteDet(I).Lote_original <> lBeTransAjusteDet(I).Lote_nuevo Then ic += 1
                If lBeTransAjusteDet(I).Fecha_vence_original <> lBeTransAjusteDet(I).Fecha_vence_nueva Then ic += 1
                If lBeTransAjusteDet(I).Cantidad_original <> lBeTransAjusteDet(I).Cantidad_nueva Then ic += 1
                If lBeTransAjusteDet(I).Peso_original <> lBeTransAjusteDet(I).Peso_nuevo Then ic += 1
                If ic > 0 Then cc += 1

                '#EJC20180924: Asignación de bodega de ERP.                
                DgComboBodega = TryCast(dgrid.CurrentRow.Cells("ColBodega"), DataGridViewComboBoxCell)
                '#CM_20200219: Se cambio el IdBodega en lugar de IdBodegaErp

                If BeBodega.Bodega_Cliente_Ajuste_ByB Then
                    IdBodegaERP = cmbBodegaERP.EditValue
                Else
                    IdBodegaERP = 0
                End If

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
                If Es_Ajuste_Positivo_Sin_Stock Then
                    'BeAjusteDet.IdStock = vIdStock
                    'vIdStock += 1
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
                If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.IdAjusteenc, True, lConnection, lTransaction) > 0 Then
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

                lBeTransAjusteDet(I).IdAjusteEnc = pBeTransAjustEnc.IdAjusteenc : ic = 0

                If lBeTransAjusteDet(I).Lote_original <> lBeTransAjusteDet(I).Lote_nuevo Then ic += 1
                If lBeTransAjusteDet(I).Fecha_vence_original <> lBeTransAjusteDet(I).Fecha_vence_nueva Then ic += 1
                If lBeTransAjusteDet(I).Cantidad_original <> lBeTransAjusteDet(I).Cantidad_nueva Then ic += 1
                If lBeTransAjusteDet(I).Peso_original <> lBeTransAjusteDet(I).Peso_nuevo Then ic += 1
                If ic > 0 Then cc += 1

                '#GT28112024: ejecutar código normal sino es positivo el ajuste.
                If Not Es_Ajuste_Positivo_Sin_Stock Then
                    '#EJC20180924: Asignación de bodega de ERP.                
                    DgComboBodega = TryCast(dgrid.Rows(I).Cells("ColBodega"), DataGridViewComboBoxCell)

                    If BeBodega.Bodega_Cliente_Ajuste_ByB Then
                        IdBodegaERP = cmbBodegaERP.EditValue.Value
                    End If
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

                ''#GT28112024: al ser ajuste positivo, el idstock se obtiene hasta que se presiona guardar
                'If Es_Ajuste_Positivo_Sin_Stock Then
                '    BeAjusteDet.IdStock = pStock_Sin_Existencia_Previa.IdStock
                'End If

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
                If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.IdAjusteenc, True) > 0 Then
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

    Private Sub Guardar_Ajuste_Positivo_Sin_Stock()

        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            Guardar_Ajuste_Positivo(clsTransaccion.lConnection, clsTransaccion.lTransaction)

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

                        If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.IdAjusteenc, pBeTransAjustEnc.Enviado_A_ERP) > 0 Then

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

                    If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.IdAjusteenc, pBeTransAjustEnc.Enviado_A_ERP) > 0 Then

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

                '#GT18122025: considerar que solo talla o color fue actualizado y no ambos.
                Dim vTalla As String = IIf(Not String.IsNullOrEmpty(item.Talla_destino), item.Talla_destino, item.Talla_origen)
                Dim vColor As String = IIf(Not String.IsNullOrEmpty(item.Color_destino), item.Color_destino, item.Color_origen)
                Dim vIdProductoTallaColor As Integer = IIf(item.IdProductoTallaColor_destino > 0, item.IdProductoTallaColor_destino, item.IdProductoTallaColor_origen)

                BeMov.Talla = vTalla
                BeMov.Color = vColor
                BeMov.IdProductoTallaColor = vIdProductoTallaColor

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

                BeMov.IdProductoTallaColor = item.IdProductoTallaColor_origen
                BeMov.Talla = item.Talla_origen
                BeMov.Color = item.Color_origen

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
            BeMov.IdProductoTallaColor = BeStock.IdProductoTallaColor

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

    Private Sub Llenar_DS_Rep()

        Dim lRow As DataRow
        Dim repAjuste As New rptAjuste
        Dim repAjusteTallaColor As New rptAjuste_TallaColor
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


                    If BeBodega.Control_Talla_Color Then
                        .Item("talla_destino") = dgrid.Rows(i).Cells("ColTalla").Value.ToString()
                        .Item("color_destino") = dgrid.Rows(i).Cells("ColColor").Value.ToString()
                    End If

                End With

                DsRepAjustes.trans_ajuste_det.Addtrans_ajuste_detRow(lRow)

            Next

            Dim tool As ReportPrintTool

            If BeBodega.Control_Talla_Color Then
                repAjusteTallaColor.DataSource = DsRepAjustes
                repAjusteTallaColor.Parameters("Referencia").Value = pBeTransAjustEnc.Referencia
                repAjusteTallaColor.Parameters("Bodega").Value = BeBodega.IdBodega
                repAjusteTallaColor.Parameters("Documento").Value = pBeTransAjustEnc.IdAjusteenc
                repAjusteTallaColor.Parameters("Fecha").Value = pBeTransAjustEnc.Fecha
                repAjusteTallaColor.Parameters("Usuario").Value = String.Format("{0} - {1} {2}", AP.UsuarioAp.Codigo, AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                repAjusteTallaColor.RequestParameters = False

                tool = New ReportPrintTool(repAjusteTallaColor)
            Else
                repAjuste.DataSource = DsRepAjustes
                repAjuste.Parameters("Referencia").Value = pBeTransAjustEnc.Referencia
                repAjuste.Parameters("Bodega").Value = BeBodega.IdBodega
                repAjuste.Parameters("Documento").Value = pBeTransAjustEnc.IdAjusteenc
                repAjuste.Parameters("Fecha").Value = pBeTransAjustEnc.Fecha
                repAjuste.Parameters("Usuario").Value = String.Format("{0} - {1} {2}", AP.UsuarioAp.Codigo, AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)


                If chkBorrador.Checked Then
                    repAjuste.Parameters("EsBorrador").Value = "Este documento es un Borrador"
                    repAjuste.RequestParameters = False
                Else
                    repAjuste.Parameters("EsBorrador").Value = ""
                End If

                repAjuste.Parameters("EsBorrador").Visible = False
                repAjuste.RequestParameters = False

                tool = New ReportPrintTool(repAjuste)
            End If

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
                            vResult = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(pBeTransAjustEnc.IdAjusteenc, True)
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

                                    vArgumentosAEnviarAInterface = "20-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBeTransAjustEnc.IdAjusteenc & "-0" & "-" & clsBD.Instancia.NombreInstancia
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
                                    vResult = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(pBeTransAjustEnc.IdAjusteenc, True)
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
                                vResult = clsLnTrans_ajuste_enc.Actualizar_Estado_Auditado(pBeTransAjustEnc.IdAjusteenc, False)
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
            Es_Ajuste_Positivo_Sin_Stock = True

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

            Dim vStock As clsBeStock = frmAjuste.pStockTemporal

            If frmAjuste.DialogResult = DialogResult.OK Then

                cmdAdd.Enabled = False

                If frmAjuste.vProducto IsNot Nothing AndAlso vStock IsNot Nothing Then
                    Cargar_producto_Sin_Stock(frmAjuste.vProducto, vStock)
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
            BeAjusteDet.IdAjusteEnc = pBeTransAjustEnc.IdAjusteenc
            BeAjusteDet.IdStock = 0 'pendiente
            BeAjusteDet.IdPropietarioBodega = pIdPropietarioBodega
            BeAjusteDet.IdProductoBodega = vProductoSinStock.IdProductoBodega
            BeAjusteDet.IdProductoEstado = pStockTemporal.IdProductoEstado
            BeAjusteDet.IdPresentacion = pStockTemporal.IdPresentacion
            BeAjusteDet.IdUnidadMedida = vProductoSinStock.IdUnidadMedidaBasica
            BeAjusteDet.IdUbicacion = IIf(pStockTemporal.IdUbicacion = 0, ubic, pStockTemporal.IdUbicacion)

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

            Dim pTalla As New clsBeTalla()
            Dim pColor As New clsBeColor()
            Dim pProducto As New clsBeProducto()
            Dim pProductoTallaExiste As New clsBeProducto_talla_color()

            If BeBodega.Control_Talla_Color Then
                '#validar si la combinacion talla/color ya existe, sino se debe guardar y luego asociar al ajuste
                pTalla = clsLnTalla.Get_Single_By_Codigo(pStockTemporal.Talla)
                If pTalla Is Nothing Then Throw New Exception("La talla no puede estar vacia")
                pColor = clsLnColor.GetSingle_By_CodigoColor(pStockTemporal.Color)
                If pColor Is Nothing Then Throw New Exception("El color no puede estar vacio")
                pProducto = clsLnProducto.Get_BeProducto_By_IdProductoBodega(pStockTemporal.IdProductoBodega, pStockTemporal.IdBodega)
                pProductoTallaExiste = clsLnProducto_talla_color.Get_Single_By_IdProducto(pProducto.IdProducto, pStockTemporal.Talla, pStockTemporal.Color)

                If pProductoTallaExiste Is Nothing Then

                    Dim pProductoTallaNuevo As New clsBeProducto_talla_color()
                    pProductoTallaNuevo = New clsBeProducto_talla_color()
                    pProductoTallaNuevo.IdProductoTallaColor = clsLnProducto_talla_color.MaxID() + 1
                    pProductoTallaNuevo.IdProducto = pProducto.IdProducto
                    pProductoTallaNuevo.IdTalla = pTalla.IdTalla
                    pProductoTallaNuevo.IdColor = pColor.IdColor
                    pProductoTallaNuevo.CodigoSKU = pProducto.Codigo & pColor.Codigo & pTalla.Codigo
                    pProductoTallaNuevo.Fec_agr = Now
                    pProductoTallaNuevo.Fec_mod = Now
                    pProductoTallaNuevo.User_agr = AP.UsuarioAp.IdUsuario
                    pProductoTallaNuevo.User_mod = AP.UsuarioAp.IdUsuario
                    clsLnProducto_talla_color.Insertar(pProductoTallaNuevo) '#EJC20260105: Crear la combinación.

                    '#GT06012025: un ajuste sin existencia no tiene color/talla origen :)
                    BeAjusteDet.Talla_origen = pTalla.Codigo
                    BeAjusteDet.Color_origen = pColor.Codigo
                    BeAjusteDet.Talla_destino = pTalla.Codigo
                    BeAjusteDet.Color_destino = pColor.Codigo
                    BeAjusteDet.IdProductoTallaColor_origen = pProductoTallaNuevo.IdProductoTallaColor

                ElseIf pProductoTallaExiste IsNot Nothing Then
                    BeAjusteDet.Talla_origen = pStockTemporal.Talla
                    BeAjusteDet.Color_origen = pStockTemporal.Color

                    BeAjusteDet.Talla_destino = pStockTemporal.Talla
                    BeAjusteDet.Color_destino = pStockTemporal.Color

                    BeAjusteDet.IdProductoTallaColor_origen = pProductoTallaExiste.IdProductoTallaColor
                End If
            End If

            '#GT16122025: aqui guarda el ajuste en memoria
            lBeTransAjusteDet.Add(BeAjusteDet)

            ' #EJCRP 21042026: Estandarización del Rows.Add posicional. Antes pasaba
            ' (codigo, nombre, UmBas, BeAjusteDet.IdUbicacion) — el IdUbicacion (numérico)
            ' caía en la columna colPresentacion. UmBas y colUbicacion ya se asignan por
            ' nombre abajo. NOTA: no se llena ColProveedor aquí porque este flujo es
            ' "alta de producto sin existencia" donde IdStock = 0 (línea ~4434, "pendiente"):
            ' al no haber stock previo, no hay proveedor de recepción asociado.
            rc = dgrid.Rows.Add(codigo, BeAjusteDet.Nombre_producto)
            dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image
            dgrid.Rows(rc).Cells("ColLote").Value = BeAjusteDet.Lote_original

            dgrid.Rows(rc).Cells("UmBas").Value = BeAjusteDet.UmBas
            dgrid.Rows(rc).Cells("UmBas").ReadOnly = True

            Dim pUbicacion As clsBeBodega_ubicacion = clsLnBodega_ubicacion.GetSingle(BeAjusteDet.IdUbicacion, AP.IdBodega)

            dgrid.Rows(rc).Cells("colUbicacion").Value = pUbicacion.NombreCompleto ' BeAjusteDet.IdUbicacion
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

            '#GT15122025: llenar los combos para talla/color
            If BeBodega.Control_Talla_Color Then

                Llenar_Talla(rc, -1)
                Llenar_Color(rc, -1)

                If pProductoTallaExiste IsNot Nothing Then
                    dgrid.Rows(rc).Cells("colIdProductoTallaColor").Value = pProductoTallaExiste.IdProductoTallaColor
                    dgrid.Rows(rc).Cells("colTalla").Value = pTalla.Codigo
                    dgrid.Rows(rc).Cells("colColor").Value = pColor.Codigo
                Else
                    dgrid.Rows(rc).Cells("colIdProductoTallaColor").Value = 0
                    dgrid.Rows(rc).Cells("colTalla").Value = pTalla.Codigo
                    dgrid.Rows(rc).Cells("colColor").Value = pColor.Codigo
                End If

            End If

            Llenar_Motivo(rc, -1)

            '#GT13062022_16_50: si pTipoAjuste es 0, entonces se deja set de cmbTipoAjuste, para que el grid obtenga las columnas adecuadas
            Dim cmbIdTipoAjuste = cmbTipoAjuste.EditValue
            If pTipoAjuste = 0 Then
                Llenar_Tipo(rc, cmbIdTipoAjuste)
            Else
                Llenar_Tipo(rc, pTipoAjuste)
            End If

            If BeBodega.Bodega_Cliente_Ajuste_ByB Then
                Llena_Bodegas_ERP_Grid(rc, -1)
            End If

            dgrid.Rows(rc).Cells("ColEnviadoAErp").Value = BeAjusteDet.Enviado
            dgrid.Rows(rc).Cells("ColEnviadoAErp").ReadOnly = False

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

            DT = clsLnTrans_ajuste_det_doc.GetAll_ByIdAjusteEnc(pBeTransAjustEnc.IdAjusteenc)

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

    Private Sub chkBorrador_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkBorrador.CheckedChanged

        If IsLoading Then Exit Sub
        Try
            Dim fechaActual As Date = Now
            Dim usuarioActual As String = AP.UsuarioAp.IdUsuario.ToString()

            If chkBorrador.Checked Then
                '=========================================================
                ' ESCENARIO 1: NORMAL -> BORRADOR
                ' Solo copiar si la lista borrador no existe o está vacía
                '=========================================================
                If (lBeTransAjusteDetBorrador Is Nothing OrElse lBeTransAjusteDetBorrador.Count = 0) AndAlso
               (lBeTransAjusteDet IsNot Nothing AndAlso lBeTransAjusteDet.Count > 0) Then

                    lBeTransAjusteDetBorrador = New List(Of clsBeTrans_ajuste_det_borrador)

                    For Each item As clsBeTrans_ajuste_det In lBeTransAjusteDet
                        lBeTransAjusteDetBorrador.Add(MapearDetalleABorrador(item, fechaActual, usuarioActual))
                    Next

                End If

            Else
                '=========================================================
                ' ESCENARIO 2: BORRADOR -> NORMAL
                ' Solo copiar si la lista normal no existe o está vacía
                '=========================================================
                If (lBeTransAjusteDet Is Nothing OrElse lBeTransAjusteDet.Count = 0) AndAlso
               (lBeTransAjusteDetBorrador IsNot Nothing AndAlso lBeTransAjusteDetBorrador.Count > 0) Then

                    lBeTransAjusteDet = New List(Of clsBeTrans_ajuste_det)

                    For Each item As clsBeTrans_ajuste_det_borrador In lBeTransAjusteDetBorrador
                        lBeTransAjusteDet.Add(MapearBorradorADetalle(item))
                    Next

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

    Private Sub dgrid_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgrid.DataError

        Try

        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub

    Private Sub cmbTipoAjuste_EditValueChanged(sender As Object, e As EventArgs) Handles cmbTipoAjuste.EditValueChanged

        If IsLoading Then Exit Sub

        Try

            'GT22042022_1612: obtener el tipo de ajuste por defecto, si en caso no se usa seleccionMultiple
            IdTipoAjuste = cmbTipoAjuste.EditValue

            If IdTipoAjuste = 3 Then
                mnuAjustePositivo.Enabled = True
                '#FIX_v8_2026-04-25: NO prender la bandera acÃ¡. La bandera
                'Es_Ajuste_Positivo_Sin_Stock SOLO debe activarse cuando el operador
                'usa explÃ­citamente mnuAjustePositivo_Click (agregar producto SIN stock previo).
                'Antes: Es_Ajuste_Positivo_Sin_Stock = True
                'Esa lÃ­nea provocaba que ajustes positivos masivos vÃ­a Excel (con IdStock previo)
                'crearan un IdStock nuevo en lugar de actualizar el origen.
            Else
                mnuAjustePositivo.Enabled = False
                Es_Ajuste_Positivo_Sin_Stock = False
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

                        If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.IdAjusteenc, pBeTransAjustEnc.Enviado_A_ERP) > 0 Then

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

                    If clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pBeTransAjustEnc.IdAjusteenc, pBeTransAjustEnc.Enviado_A_ERP) > 0 Then

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

        Dim vPermitirEdicion As Boolean = False

        IsLoading = True

        Try

            Try
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Cargando ajuste...")
            Catch ex As Exception
            End Try

            '#GT28082025: si bodega controla talla/color mostrar columnas de lo contrario, ocultarlas.
            BeBodega = AP.Bodega

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

            cmbBodegaERP.Visible = BeBodega.Bodega_Cliente_Ajuste_ByB

            If BeBodega.Bodega_Cliente_Ajuste_ByB Then
                cmbBodegaERP.Enabled = (Modo = TipoTrans.Nuevo)
            End If
            cmbProductoFamilia.Enabled = (Modo = TipoTrans.Nuevo)
            cmbPropietarioBodega.Enabled = (Modo = TipoTrans.Nuevo)
            lcmbCentroCosto.Enabled = (Modo = TipoTrans.Nuevo)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, AP.IdBodega, True)

            IMS.Listar_Producto_Familia(cmbProductoFamilia, AP.IdEmpresa)

            '#EJC20180924_0846PM: La bodega del ERP se registra por línea de detalle, 
            'En encabezado guardar la bodega de WMS. 
            If BeBodega.Bodega_Cliente_Ajuste_ByB Then
                cmbBodegaERP.EditValue = clsLnCliente.Get_IdBodega_By_Codigo(AP.Bodega.Codigo)

                IMS.Listar_Clientes_By_IdEmpresa(cmbBodegaERP, AP.IdEmpresa)
            Else
                cmbBodegaERP.EditValue = 0
            End If

            If BeBodega.Centro_Costo_Dep_Erp = "" AndAlso BeBodega.Centro_Costo_Dir_Erp = "" AndAlso BeBodega.Centro_Costo_Erp = "" Then

                gcCentroCosto.Visible = False
                lcmbCentroCosto.Visible = True
                lblCentroCosto.Visible = True

                IMS.Listar_Centro_Costo_By_IdEmpresa(lcmbCentroCosto, AP.IdEmpresa)
                txtCentroCostoERP.Text = ""
                txtCentroCostoDirERP.Text = ""
                txtCentroCostoDepERP.Text = ""

            Else

                gcCentroCosto.Visible = True
                lcmbCentroCosto.Visible = False
                lblCentroCosto.Visible = False

                txtCentroCostoERP.Text = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(BeBodega.Centro_Costo_Erp)
                txtCentroCostoDirERP.Text = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(BeBodega.Centro_Costo_Dir_Erp)
                txtCentroCostoDepERP.Text = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(BeBodega.Centro_Costo_Dep_Erp)

            End If

            'Dim BeConfig As clsBeI_nav_config_enc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(BeBodega.IdBodega)

            BeConfig = clsLnI_nav_config_enc.GetSingle(AP.IdConfiguracionInterface)

            If BeConfig IsNot Nothing Then
                If BeConfig.Requerir_Centro_Costo_Obligatorio Then
                    If Not gcCentroCosto.Visible Then
                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("La bodega debe tener definidos los centros de costo para los ajustes, no es posible generar un ajuste ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Guardado = True
                        Me.Close()
                    End If
                End If
            End If

            'GT22042022_1034: Carga los tipos de ajuste activos para el combo del encabezado.
            IMS.Listar_Tipo_Ajuste_Activo(cmbTipoAjuste, BeBodega.Control_Talla_Color)

            cmbTipoAjuste.EditValue = 3

            '#GT29082025: mostrar u ocultar las columnas de talla/color
            Mostrar_Columnas_Talla_Color(BeBodega.Control_Talla_Color)

            Application.DoEvents()

            Select Case Modo

                Case TipoTrans.Nuevo

                    Guardado = False

                    Inserta_Encabezado_Ajuste()

                    mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    mnuImprimir1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                    lBeTransAjusteDet.Clear()

                    txtReferencia.Text = ""
                    dtpFecha.EditValue = Now

                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now
                    cmbBodegaERP.EditValue = clsLnCliente.Get_IdBodega_By_Codigo(AP.Bodega.Codigo)

                    If BeBodega.Bodega_Cliente_Ajuste_ByB Then
                        cmbBodegaERP.EditValue = clsLnCliente.Get_IdBodega_By_Codigo(AP.Bodega.Codigo)
                    Else
                        cmbBodegaERP.EditValue = 0
                    End If

                    vPermitirEdicion = True

                Case TipoTrans.Editar

                    Guardado = True
                    mnuImprimir1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    cmbBodegaERP.EditValue = clsLnCliente.Get_IdBodega_By_Codigo(AP.Bodega.Codigo)

                    Cargar_Datos()

                    Set_Estado_Envio_A_ERP()
                    'Si es borrador, debe permitirse editar para luego trasladar a tablas finales
                    If chkBorrador.Checked Then
                        vPermitirEdicion = True
                        mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                        mnuGuardar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    Else
                        vPermitirEdicion = False

                        If pBeTransAjustEnc.Ajuste_Por_Inventario > 0 Then
                            mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            mnuGuardar.Enabled = False
                        Else
                            mnuGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                        End If
                    End If

            End Select

            If BeBodega.Bodega_Cliente_Ajuste_ByB Then
                cmbBodegaERP.EditValue = clsLnCliente.Get_IdBodega_By_Codigo(AP.Bodega.Codigo)
            Else
                cmbBodegaERP.EditValue = 0
            End If

            'Aplicar estado final de habilitación
            txtReferencia.Enabled = vPermitirEdicion
            dtpFecha.Enabled = vPermitirEdicion
            cmdAdd.Visible = vPermitirEdicion
            mnuDel.Visible = vPermitirEdicion

            cmbBodegaERP.Enabled = vPermitirEdicion
            cmbProductoFamilia.Enabled = vPermitirEdicion
            cmbPropietarioBodega.Enabled = vPermitirEdicion
            lcmbCentroCosto.Enabled = vPermitirEdicion

            'cmbTipoAjuste: en editar normalmente queda deshabilitado, salvo si es borrador
            If Modo = TipoTrans.Nuevo Then
                cmbTipoAjuste.Enabled = True
            Else
                cmbTipoAjuste.Enabled = chkBorrador.Checked
            End If

            If vPermitirEdicion Then
                dgrid.ReadOnly = False
            Else
                Deshabilita_Grid()
                dgrid.ReadOnly = True
            End If

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
            ' #RC2026 INIT - Inicializa el resumen consolidado por (Codigo|Talla|Color).
            ' Idempotente: solo arma el datasource y las columnas la primera vez.
            Try : Hook_Init_Resumen_Consolidado_2026() : Catch : End Try
            IsLoading = False
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub btnImportarExcel_Click(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnImportarExcel.ItemClick

    End Sub

    Private Sub Mostrar_Columnas_Talla_Color(ByVal mostrar_talla_color As Boolean)
        Try

            '#EJC20180924_1048PM: Si es un ajuste por inventario, se debe habilitar en el grid la bodega para que sea editable, para que se modifique, previo a envío a ERP.
            For Each Col As DataGridViewColumn In dgrid.Columns
                If Col.Name = "ColTalla" AndAlso Not mostrar_talla_color Then
                    Col.Visible = False
                ElseIf (Col.Name = "ColColor") AndAlso Not mostrar_talla_color Then
                    Col.Visible = False
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

    Private Function Obtener_Detalle_A_Cargar() As List(Of clsBeTrans_ajuste_det)

        If lBeTransAjusteDet IsNot Nothing AndAlso lBeTransAjusteDet.Count > 0 Then
            Return lBeTransAjusteDet
        End If

        Dim lReturn As New List(Of clsBeTrans_ajuste_det)

        If lBeTransAjusteDetBorrador IsNot Nothing Then
            For Each itemBorrador As clsBeTrans_ajuste_det_borrador In lBeTransAjusteDetBorrador
                lReturn.Add(Convertir_Borrador_A_Detalle(itemBorrador))
            Next
        End If

        Return lReturn

    End Function

    Private Function Convertir_Borrador_A_Detalle(ByVal item As clsBeTrans_ajuste_det_borrador) As clsBeTrans_ajuste_det

        Dim oDet As New clsBeTrans_ajuste_det

        With oDet
            .IdAjusteDet = item.idajustedet
            .IdAjusteEnc = item.idajusteenc
            .IdStock = item.IdStock
            .IdPropietarioBodega = item.IdPropietarioBodega
            .IdProductoBodega = item.IdProductoBodega
            .IdProductoEstado = item.IdProductoEstado
            .IdPresentacion = item.IdPresentacion
            .IdUnidadMedida = item.IdUnidadMedida
            .IdUbicacion = item.IdUbicacion
            .Lote_original = item.lote_original
            .Lote_nuevo = item.lote_nuevo
            .Fecha_vence_original = item.fecha_vence_original
            .Fecha_vence_nueva = item.fecha_vence_nueva
            .Peso_original = item.peso_original
            .Peso_nuevo = item.peso_nuevo
            .Cantidad_original = item.cantidad_original
            .Cantidad_nueva = item.cantidad_nueva
            .Codigo_producto = item.codigo_producto
            .Nombre_producto = item.nombre_producto
            .Idtipoajuste = item.idtipoajuste
            .IdMotivoAjuste = item.idmotivoajuste
            .Observacion = item.observacion
            .Codigo_ajuste = item.codigo_ajuste
            .Enviado = item.enviado
            .IdBodegaERP = item.IdBodegaERP
            .lic_plate = item.lic_plate
            .referencia_ajuste_erp = item.referencia_ajuste_erp
            .estado_ajuste_erp = item.estado_ajuste_erp
            .UmBas = item.UmBas
            .Factor = item.Factor
            .Nombre_Presentacion = item.Nombre_Presentacion

            .IdProductoTallaColor_origen = item.IdProductoTallaColor_origen
            .Talla_origen = item.Talla_origen
            .Color_origen = item.Color_origen

            .IdProductoTallaColor_destino = item.IdProductoTallaColor_destino
            .Talla_destino = item.Talla_destino
            .Color_destino = item.Color_destino
        End With

        Return oDet

    End Function

    '#FIX_v12_HASH_EXCEL_2026-04-25 (F1):
    'HashSet de hashes SHA-256 de los lotes ya importados en la sesión actual.
    Private pHashesExcelImportadosSesion As New HashSet(Of String)

    '#FIX_v12_HASH_EXCEL_2026-04-25 (F1 helper):
    Private Function CalcularHashImportacion(
            ByVal lote As List(Of clsBeTrans_ajuste_det)) As String
        If lote Is Nothing OrElse lote.Count = 0 Then Return ""
        Try
            Dim sb As New System.Text.StringBuilder()
            Dim ordenado = lote.OrderBy(Function(x) x.Codigo_producto) _
                               .ThenBy(Function(x) x.Lote_original) _
                               .ThenBy(Function(x) x.IdStock)
            For Each d As clsBeTrans_ajuste_det In ordenado
                sb.Append(If(d.Codigo_producto, "")).Append("|"c)
                sb.Append(If(d.Lote_original, "")).Append("|"c)
                sb.Append(d.IdStock).Append("|"c)
                sb.Append(d.Cantidad_original.ToString("R", System.Globalization.CultureInfo.InvariantCulture)).Append("|"c)
                sb.Append(d.Cantidad_nueva.ToString("R", System.Globalization.CultureInfo.InvariantCulture)).Append("|"c)
                sb.Append(d.Idtipoajuste).Append(";"c)
            Next
            Using sha As System.Security.Cryptography.SHA256 = System.Security.Cryptography.SHA256.Create()
                Dim bytes() As Byte = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sb.ToString()))
                Return BitConverter.ToString(bytes).Replace("-", "")
            End Using
        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error("CalcularHashImportacion: " & ex.Message)
            Return ""
        End Try
    End Function

    Private Sub btnImportarExcel_Click(sender As Object, e As EventArgs) Handles btnImportarExcel.ItemClick
        Try
            ' El tipo de ajuste es obligatorio antes de importar
            If cmbTipoAjuste.EditValue Is Nothing OrElse CInt(cmbTipoAjuste.EditValue) = 0 Then
                XtraMessageBox.Show("Seleccione el Tipo de Ajuste antes de importar.",
                                    "Configuración incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim idTipoSel As Integer = CInt(cmbTipoAjuste.EditValue)

            Using frm As New frmImportarAjusteExcel(AP.IdBodega,
                                                    cmbPropietarioBodega.EditValue,
                                                    idTipoSel,
                                                    BeBodega IsNot Nothing AndAlso BeBodega.Control_Talla_Color)
                frm.ShowDialog(Me)

                If frm.DialogResult <> DialogResult.OK OrElse frm.AjustesParaCargar.Count = 0 Then Return

                '#FIX_v12_HASH_EXCEL_2026-04-25 (F1): evitar doble import accidental.
                Dim hashLote As String = CalcularHashImportacion(frm.AjustesParaCargar)
                If Not String.IsNullOrEmpty(hashLote) Then
                    If pHashesExcelImportadosSesion.Contains(hashLote) Then
                        Dim resp As DialogResult = XtraMessageBox.Show(
                            "El archivo Excel que está intentando importar coincide exactamente con uno ya importado en esta sesión del ajuste." & vbCrLf & vbCrLf &
                            "¿Desea importarlo de todas formas? Continuar generará filas duplicadas en el grid.",
                            "Archivo duplicado",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2)
                        If resp <> DialogResult.Yes Then Return
                    Else
                        pHashesExcelImportadosSesion.Add(hashLote)
                    End If
                End If

                Dim BeAjusteDetBorrador As clsBeTrans_ajuste_det_borrador = Nothing

                ' Asignar IdAjusteEnc y cargar al grid
                For Each det As clsBeTrans_ajuste_det In frm.AjustesParaCargar

                    det.IdAjusteEnc = pBeTransAjustEnc.IdAjusteenc

                    '#FIX_v15_IMPORTER_BORRADOR_2026-04-25 (I1): mapeo explícito al borrador.
                    If chkBorrador.Checked Then
                        BeAjusteDetBorrador = MapearDetalleABorrador(
                            det, Now, AP.UsuarioAp.IdUsuario.ToString())
                        lBeTransAjusteDetBorrador.Add(BeAjusteDetBorrador)
                    Else
                        lBeTransAjusteDet.Add(det)
                    End If


                    Dim ubic As String = clsLnBodega_ubicacion.GetSingle(det.IdUbicacion, AP.IdBodega).NombreCompleto

                    Dim vIdProducto = clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(det.IdProductoBodega)

                    Dim vProveedor As clsBeProveedor = Nothing
                    Dim sProveedorTexto As String = ""
                    vProveedor = clsLnProveedor.Get_Single_By_IdStock(det.IdStock)

                    If vProveedor IsNot Nothing Then
                        If Not String.IsNullOrWhiteSpace(vProveedor.Codigo) Then
                            sProveedorTexto = $"{vProveedor.Codigo} - {vProveedor.Nombre}"
                        Else
                            sProveedorTexto = vProveedor.Nombre
                        End If
                    Else
                        Debug.WriteLine($"No se encontró proveedor para IdStock: {det.IdStock}")
                    End If

                    '#FIX_v12_PROVEEDOR_GRID_2026-04-25 (F1): asignación nominal de proveedor.
                    Dim rc As Integer = dgrid.Rows.Add(det.Codigo_producto, det.Nombre_producto, det.UmBas,
                                                       If(det.IdPresentacion <> 0, det.Presentacion?.Nombre, ""),
                                                       ubic)

                    Llenar_Motivo(rc, det.IdMotivoAjuste)
                    Llenar_Tipo(rc, det.Idtipoajuste)

                    ' Cinturón defensivo: forzar el valor del combo aunque
                    ' Llenar_Tipo no lo haya seteado (el fix raíz va en
                    ' Llenar_Tipo, branch Case TipoTrans.Nuevo).
                    Try
                        dgrid.Rows(rc).Cells("tipoajuste").Value = det.Idtipoajuste
                    Catch
                    End Try

                    ' Talla/Color: solo cuando la bodega controla. El importador
                    ' resolvió IdProductoTallaColor desde el IdStock; aquí se
                    ' propaga al dgrid usando el patrón canónico (líneas 921-934
                    ' del flujo manual: Llenar_Talla/Color cargan los DataSource).
                    If BeBodega IsNot Nothing AndAlso BeBodega.Control_Talla_Color _
                       AndAlso det.IdProductoTallaColor_origen > 0 Then
                        Try
                            Llenar_Talla(rc, -1)
                            Llenar_Color(rc, -1)
                            dgrid.Rows(rc).Cells("ColIdProductoTallaColor").Value = det.IdProductoTallaColor_origen
                            dgrid.Rows(rc).Cells("colTalla").Value = det.Talla_origen
                            dgrid.Rows(rc).Cells("colColor").Value = det.Color_origen
                            dgrid.Rows(rc).Cells("colTalla").ReadOnly = True
                            dgrid.Rows(rc).Cells("colColor").ReadOnly = True
                        Catch ex As Exception
                            ' No bloquea la importación si el lookup falla.
                            clsLnLog_error_wms.Agregar_Error(
                                "btnImportarExcel_Click talla/color row " & rc & ": " & ex.Message)
                        End Try
                    End If

                    Llena_Bodegas_ERP_Grid(rc, -1)

                    '#FIX_v12_PROVEEDOR_GRID_2026-04-25 (F1):
                    dgrid.Rows(rc).Cells("ColProveedor").Value = sProveedorTexto

                    dgrid.Rows(rc).Cells("ColDiferencia").Value = PictureBox1.Image
                    dgrid.Rows(rc).Cells("ColLote").Value = det.Lote_original
                    dgrid.Rows(rc).Cells("colUbicacion").Value = ubic
                    dgrid.Rows(rc).Cells("colUbicacion").ReadOnly = True
                    dgrid.Rows(rc).Cells("UmBas").Value = det.UmBas
                    dgrid.Rows(rc).Cells("UmBas").ReadOnly = True
                    dgrid.Rows(rc).Cells("ColObservacion").Value = det.Observacion
                    dgrid.Rows(rc).Cells("ColLicPlate").Value = det.lic_plate

                    ' Mismo patrón que Cargar_Detalle: Cantidad_original/nueva
                    ' viajan en UM base; si hay presentación se divide por Factor
                    ' solo para visualización en el grid.
                    Dim usarPres As Boolean = (det.IdPresentacion <> 0 AndAlso det.Factor > 0)
                    Dim divisor As Double = If(usarPres, det.Factor, 1.0)

                    Select Case det.Idtipoajuste
                        Case 3 ' Positivo
                            dgrid.Rows(rc).Cells("CantidadP").Value = det.Cantidad_original / divisor
                            dgrid.Rows(rc).Cells("ColCantidad").Value = (det.Cantidad_nueva - det.Cantidad_original) / divisor
                        Case 5 ' Negativo
                            dgrid.Rows(rc).Cells("CantidadP").Value = det.Cantidad_original / divisor
                            dgrid.Rows(rc).Cells("ColCantidad").Value = (det.Cantidad_original - det.Cantidad_nueva) / divisor
                        Case 1 ' Lote
                            dgrid.Rows(rc).Cells("ColLote").Value = det.Lote_original
                            dgrid.Rows(rc).Cells("ColCantidad").Value = det.Lote_nuevo
                    End Select

                    If usarPres Then
                        dgrid.Rows(rc).Cells("colPresentacion").Value = det.Nombre_Presentacion
                        dgrid.Rows(rc).Cells("colPresentacion").ReadOnly = True
                    End If



                Next

                If dgrid.Rows.Count > 0 Then cmbTipoAjuste.Enabled = False
                lblRegs.Caption = "Registros: " & dgrid.Rows.Count

                XtraMessageBox.Show(frm.AjustesParaCargar.Count & " fila(s) importada(s). Revise y presione Guardar.",
                                    "Importación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error al importar", MessageBoxButtons.OK, MessageBoxIcon.Error)
            clsLnLog_error_wms.Agregar_Error("btnImportarExcel_Click: " & ex.Message)
        End Try
    End Sub

    Private Sub SincronizarDetalleAjusteSegunModoBorrador()
        Try
            Dim fechaActual As Date = Now
            Dim usuarioActual As String = AP.UsuarioAp.IdUsuario.ToString()

            '------------------------------------------------------------
            ' CASO 1:
            ' Si está en modo borrador y la lista borrador está vacía,
            ' pero la lista normal tiene datos, copiar de normal -> borrador
            '------------------------------------------------------------
            If chkBorrador.Checked Then

                If (lBeTransAjusteDetBorrador Is Nothing OrElse lBeTransAjusteDetBorrador.Count = 0) AndAlso
                   (lBeTransAjusteDet IsNot Nothing AndAlso lBeTransAjusteDet.Count > 0) Then

                    lBeTransAjusteDetBorrador = lBeTransAjusteDet.
                        Select(Function(item) MapearDetalleABorrador(item, fechaActual, usuarioActual)).
                        ToList()

                End If

            Else
                '------------------------------------------------------------
                ' CASO 2:
                ' Si ya NO está en modo borrador y la lista normal está vacía,
                ' pero la lista borrador tiene datos, copiar de borrador -> normal
                '------------------------------------------------------------
                If (lBeTransAjusteDet Is Nothing OrElse lBeTransAjusteDet.Count = 0) AndAlso
                   (lBeTransAjusteDetBorrador IsNot Nothing AndAlso lBeTransAjusteDetBorrador.Count > 0) Then

                    lBeTransAjusteDet = lBeTransAjusteDetBorrador.
                        Select(Function(item) MapearBorradorADetalle(item)).
                        ToList()

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

    Private Function MapearDetalleABorrador(item As clsBeTrans_ajuste_det,
                                            fechaActual As Date,
                                            usuarioActual As String) As clsBeTrans_ajuste_det_borrador

        Return New clsBeTrans_ajuste_det_borrador With {
            .idajustedet = item.IdAjusteDet,
            .idajusteenc = item.IdAjusteEnc,
            .IdStock = item.IdStock,
            .IdPropietarioBodega = item.IdPropietarioBodega,
            .IdProductoBodega = item.IdProductoBodega,
            .IdProductoEstado = item.IdProductoEstado,
            .IdPresentacion = item.IdPresentacion,
            .IdUnidadMedida = item.IdUnidadMedida,
            .IdUbicacion = item.IdUbicacion,
        .lote_original = item.Lote_original,
        .lote_nuevo = item.Lote_nuevo,
        .fecha_vence_original = item.Fecha_vence_original,
        .fecha_vence_nueva = item.Fecha_vence_nueva,
        .peso_original = item.Peso_original,
        .peso_nuevo = item.Peso_nuevo,
        .cantidad_original = item.Cantidad_original,
        .cantidad_nueva = item.Cantidad_nueva,
        .codigo_producto = item.Codigo_producto,
        .nombre_producto = item.Nombre_producto,
        .idtipoajuste = item.Idtipoajuste,
        .idmotivoajuste = item.IdMotivoAjuste,
        .observacion = item.Observacion,
        .codigo_ajuste = item.Codigo_ajuste,
        .enviado = item.Enviado,
        .IdBodegaERP = item.IdBodegaERP,
        .lic_plate = item.lic_plate,
        .referencia_ajuste_erp = item.referencia_ajuste_erp,
        .estado_ajuste_erp = item.estado_ajuste_erp,
        .idstockres = item.idstockres,
        .idstocklink = item.idstocklink,
        .esnuevolink = item.esnuevolink,
        .IdProductoTallaColor_origen = item.IdProductoTallaColor_origen,
        .Talla_origen = item.Talla_origen,
        .Color_origen = item.Color_origen,
        .IdProductoTallaColor_destino = item.IdProductoTallaColor_destino,
        .Talla_destino = item.Talla_destino,
        .Color_destino = item.Color_destino,
        .UmBas = item.UmBas,
        .Factor = item.Factor,
        .Nombre_Presentacion = item.Nombre_Presentacion,
        .CantReservada = item.CantReservada,
        .Presentacion = item.Presentacion,
        .estado_borrador = "BORRADOR",
        .confirmado = False,
        .procesado = False,
        .fecha_creacion = fechaActual,
        .usuario_creacion = usuarioActual,
        .fecha_modificacion = fechaActual,
        .usuario_modificacion = usuarioActual
    }
    End Function

    Private Function MapearBorradorADetalle(item As clsBeTrans_ajuste_det_borrador) As clsBeTrans_ajuste_det

        Return New clsBeTrans_ajuste_det With {
            .IdAjusteDet = item.idajustedet,
            .IdAjusteEnc = item.idajusteenc,
            .IdStock = item.IdStock,
            .IdPropietarioBodega = item.IdPropietarioBodega,
            .IdProductoBodega = item.IdProductoBodega,
            .IdProductoEstado = item.IdProductoEstado,
            .IdPresentacion = item.IdPresentacion,
            .IdUnidadMedida = item.IdUnidadMedida,
            .IdUbicacion = item.IdUbicacion,
        .Lote_original = item.lote_original,
        .Lote_nuevo = item.lote_nuevo,
        .Fecha_vence_original = item.fecha_vence_original,
        .Fecha_vence_nueva = item.fecha_vence_nueva,
        .Peso_original = item.peso_original,
        .Peso_nuevo = item.peso_nuevo,
        .Cantidad_original = item.cantidad_original,
        .Cantidad_nueva = item.cantidad_nueva,
        .Codigo_producto = item.codigo_producto,
        .Nombre_producto = item.nombre_producto,
        .Idtipoajuste = item.idtipoajuste,
        .IdMotivoAjuste = item.idmotivoajuste,
        .Observacion = item.observacion,
        .Codigo_ajuste = item.codigo_ajuste,
        .Enviado = item.enviado,
        .IdBodegaERP = item.IdBodegaERP,
        .lic_plate = item.lic_plate,
        .referencia_ajuste_erp = item.referencia_ajuste_erp,
        .estado_ajuste_erp = item.estado_ajuste_erp,
        .idstockres = item.idstockres,
        .idstocklink = item.idstocklink,
        .esnuevolink = item.esnuevolink,
        .IdProductoTallaColor_origen = item.IdProductoTallaColor_origen,
        .Talla_origen = item.Talla_origen,
        .Color_origen = item.Color_origen,
        .IdProductoTallaColor_destino = item.IdProductoTallaColor_destino,
        .Talla_destino = item.Talla_destino,
        .Color_destino = item.Color_destino,
        .UmBas = item.UmBas,
        .Factor = item.Factor,
        .Nombre_Presentacion = item.Nombre_Presentacion,
        .CantReservada = item.CantReservada,
        .Presentacion = item.Presentacion
    }
    End Function


#Region "Resumen_Consolidado_2026"
    ' ============================================================================
    '  RESUMEN CONSOLIDADO 2026 — agrupa el detalle por (Codigo|Talla|Color)
    '  y lo muestra en el grid DevExpress `dgridProductosConsolidados`
    '  (MainView = GridView3, declarado read-only en Designer).
    '
    '  Una sola vista, una sola responsabilidad:
    '    - 1 fila por (Codigo | Talla | Color)
    '    - sumas: # lineas, Existencia, Cantidad ajuste, Diferencia
    '    - sets: Bodegas, Ubicaciones (texto separado por coma / pipe)
    '    - resalta filas con Diferencia <> 0 en rosa palido
    '    - footer con totales (sumas y conteo de productos distintos)
    '    - autofilter row activado para inspeccion rapida
    '
    '  No modifica el dgrid clasico ni su logica de carga / edicion / guardado.
    '  Se refresca solo cuando el dgrid clasico cambia o cuando el usuario
    '  entra a la pestana del resumen.
    '
    '  Hook a invocar UNA vez desde frmAjusteStock_Shown (al final, en Finally):
    '    Try : Hook_Init_Resumen_Consolidado_2026() : Catch : End Try
    '  (Esa linea ya quedo agregada en este archivo - buscar "#RC2026 INIT".)
    ' ============================================================================

    Private DT_RC2026_Consolidado As DataTable
    Private RC2026_Inicializado As Boolean = False

    Private Sub mnuImprimirResumen_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimirResumen.ItemClick
        Try
            ' Aseguro que el consolidado esta al dia antes de imprimir.
            Refrescar_Resumen_Consolidado_RC2026()
            If DT_RC2026_Consolidado Is Nothing OrElse DT_RC2026_Consolidado.Rows.Count = 0 Then
                XtraMessageBox.Show("No hay datos para imprimir en el resumen consolidado.",
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Imprimir_Resumen_Consolidado_RC2026()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Try : clsLnLog_error_wms.Agregar_Error("mnuImprimirResumen_ItemClick: " & ex.Message) : Catch : End Try
        End Try
    End Sub

    Private RC2026_Refrescando As Boolean = False  ' guard re-entrancy

    Private Sub Hook_Init_Resumen_Consolidado_2026()
        If RC2026_Inicializado Then Return

        Try
            Build_DT_Consolidado_RC2026()
            Set_Columnas_GridView3_RC2026()

            ' Auto-refresh: NO reemplaza handlers, agrega en paralelo.
            ' Cualquier cambio en el dgrid clasico dispara recalculo si la
            ' pestana del resumen esta activa (ahorra trabajo si no se ve).
            AddHandler dgrid.RowsAdded, AddressOf dgrid_AnyChange_RC2026
            AddHandler dgrid.RowsRemoved, AddressOf dgrid_AnyChange_RC2026
            AddHandler dgrid.CellValueChanged, AddressOf dgrid_AnyChange_RC2026
            AddHandler dgrid.RowValidated, AddressOf dgrid_AnyChange_RC2026

            If XtraTabControl1 IsNot Nothing Then
                AddHandler XtraTabControl1.SelectedPageChanged,
                    Sub(s, e2) Refrescar_Resumen_Consolidado_RC2026()
            End If

            RC2026_Inicializado = True
            Refrescar_Resumen_Consolidado_RC2026()
        Catch ex As Exception
            Try
                clsLnLog_error_wms.Agregar_Error(
                    "Hook_Init_Resumen_Consolidado_2026: " & ex.Message)
            Catch
            End Try
        End Try
    End Sub

    Private Sub mnuEliminarAjusteBorrador_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarAjusteBorrador.ItemClick
        Eliminar_Ajuste_Si_Sin_Detalle()
    End Sub

    Private Sub dgrid_AnyChange_RC2026(sender As Object, e As EventArgs)
        ' Solo recalcula si la pestana del resumen esta visible.
        If XtraTabControl1 Is Nothing OrElse
           XtraTabControl1.SelectedTabPage Is Nothing Then Return
        If XtraTabControl1.SelectedTabPage IsNot tabResumenAjuste Then Return
        Refrescar_Resumen_Consolidado_RC2026()
    End Sub

    Private Sub Build_DT_Consolidado_RC2026()
        DT_RC2026_Consolidado = New DataTable("ResumenConsolidado")
        With DT_RC2026_Consolidado.Columns
            .Add("Codigo", GetType(String))
            .Add("Producto", GetType(String))
            .Add("UmBas", GetType(String))
            .Add("Talla", GetType(String))
            .Add("Color", GetType(String))
            .Add("Lineas", GetType(Integer))
            .Add("Existencia", GetType(Decimal))
            .Add("Cantidad", GetType(Decimal))
            .Add("Diferencia", GetType(Decimal))
            .Add("Bodegas", GetType(String))
            .Add("Ubicaciones", GetType(String))
        End With
    End Sub

    ' --- Helpers de lectura segura sobre el dgrid clasico --------------------
    Private Function CellRC_Obj(rowIdx As Integer, name As String) As Object
        Try
            If rowIdx < 0 OrElse rowIdx >= dgrid.Rows.Count Then Return Nothing
            Dim r = dgrid.Rows(rowIdx)
            If r.IsNewRow Then Return Nothing
            If Not dgrid.Columns.Contains(name) Then Return Nothing
            Dim v = r.Cells(name).Value
            If v Is Nothing OrElse IsDBNull(v) Then Return Nothing
            Return v
        Catch
            Return Nothing
        End Try
    End Function

    Private Function CellRC_Str(rowIdx As Integer, name As String) As String
        Dim v = CellRC_Obj(rowIdx, name)
        If v Is Nothing Then Return ""
        Return v.ToString()
    End Function

    Private Function CellRC_Dec(rowIdx As Integer, name As String) As Decimal
        Dim v = CellRC_Obj(rowIdx, name)
        If v Is Nothing Then Return 0D
        Dim d As Decimal
        If Decimal.TryParse(v.ToString(),
                            Globalization.NumberStyles.Any,
                            Globalization.CultureInfo.CurrentCulture, d) Then Return d
        Return 0D
    End Function

    ' --- Refresh principal ---------------------------------------------------
    Public Sub Refrescar_Resumen_Consolidado_RC2026()
        If Not RC2026_Inicializado Then Return
        If RC2026_Refrescando Then Return
        RC2026_Refrescando = True
        Try
            ' Acumulador en memoria: clave = "Codigo|Talla|Color"
            Dim acc As New Dictionary(Of String, Object())

            For i As Integer = 0 To dgrid.Rows.Count - 1
                If dgrid.Rows(i).IsNewRow Then Continue For

                Dim cod = CellRC_Str(i, "ColCodigoProducto")
                Dim prod = CellRC_Str(i, "colNombreProducto")
                Dim umb = CellRC_Str(i, "UmBas")
                Dim tal = CellRC_Str(i, "colTalla")
                Dim col = CellRC_Str(i, "colColor")
                Dim exi = CellRC_Dec(i, "CantidadP")
                Dim can = CellRC_Dec(i, "ColCantidad")
                ' Diferencia se calcula SIEMPRE como (Cantidad - Existencia)
                ' para que el consolidado sea correcto en borrador, antes de
                ' que el flujo principal recalcule la columna ColDiferencia
                ' del dgrid clasico (que puede estar vacia / desactualizada).
                Dim dif = can - exi
                Dim bod = CellRC_Str(i, "ColBodega").Trim()
                Dim ubi = CellRC_Str(i, "colUbicacion").Trim()

                ' Si la fila no tiene codigo, no aporta al consolidado.
                If cod Is Nothing OrElse cod.Trim() = "" Then Continue For

                Dim key = cod & "|" & tal & "|" & col
                If Not acc.ContainsKey(key) Then
                    acc(key) = New Object() {
                        cod, prod, umb, tal, col,
                        0, 0D, 0D, 0D,
                        New HashSet(Of String)(StringComparer.OrdinalIgnoreCase),
                        New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
                    }
                End If
                Dim row = acc(key)
                row(5) = CInt(row(5)) + 1
                row(6) = CDec(row(6)) + exi
                row(7) = CDec(row(7)) + can
                row(8) = CDec(row(8)) + dif
                Dim setBod = DirectCast(row(9), HashSet(Of String))
                Dim setUbi = DirectCast(row(10), HashSet(Of String))
                If bod <> "" Then setBod.Add(bod)
                If ubi <> "" Then setUbi.Add(ubi)
            Next

            DT_RC2026_Consolidado.BeginLoadData()
            DT_RC2026_Consolidado.Rows.Clear()
            For Each kv In acc
                Dim row = kv.Value
                Dim setBod = DirectCast(row(9), HashSet(Of String))
                Dim setUbi = DirectCast(row(10), HashSet(Of String))
                DT_RC2026_Consolidado.Rows.Add(
                    row(0), row(1), row(2), row(3), row(4),
                    CInt(row(5)), CDec(row(6)), CDec(row(7)), CDec(row(8)),
                    String.Join(", ", setBod),
                    String.Join(" | ", setUbi))
            Next
            DT_RC2026_Consolidado.EndLoadData()

            ' Bind idempotente
            If dgridProductosConsolidados.DataSource IsNot DT_RC2026_Consolidado Then
                dgridProductosConsolidados.DataSource = DT_RC2026_Consolidado
            End If
        Catch ex As Exception
            Try
                clsLnLog_error_wms.Agregar_Error(
                    "Refrescar_Resumen_Consolidado_RC2026: " & ex.Message)
            Catch
            End Try
        Finally
            RC2026_Refrescando = False
        End Try
    End Sub

    ' --- Setup de GridView3 (la vista del dgridProductosConsolidados) --------
    Private Sub Set_Columnas_GridView3_RC2026()
        Try
            If GridView3 Is Nothing Then Return

            GridView3.OptionsView.ShowFooter = True
            GridView3.OptionsView.ShowGroupPanel = False
            GridView3.OptionsView.ColumnAutoWidth = False
            GridView3.OptionsView.ShowAutoFilterRow = True
            GridView3.OptionsBehavior.Editable = False
            GridView3.OptionsBehavior.ReadOnly = True
            GridView3.OptionsSelection.MultiSelect = False
            GridView3.OptionsCustomization.AllowFilter = True
            GridView3.OptionsFind.AlwaysVisible = True
            GridView3.Columns.Clear()

            Dim idx As Integer = 0
            Dim addCol = Sub(field As String, caption As String,
                             width As Integer, fmt As String)
                             Dim c As New DevExpress.XtraGrid.Columns.GridColumn With {
                                 .FieldName = field,
                                 .Caption = caption,
                                 .Visible = True,
                                 .VisibleIndex = idx,
                                 .Width = width
                             }
                             c.OptionsColumn.AllowEdit = False
                             c.OptionsColumn.ReadOnly = True
                             If fmt <> "" Then
                                 c.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                 c.DisplayFormat.FormatString = fmt
                             End If
                             GridView3.Columns.Add(c)
                             idx += 1
                         End Sub

            addCol("Codigo", "Codigo", 100, "")
            addCol("Producto", "Producto", 260, "")
            addCol("UmBas", "UmBas", 70, "")
            addCol("Talla", "Talla", 70, "")
            addCol("Color", "Color", 80, "")
            addCol("Lineas", "# Lineas", 70, "{0:n0}")
            addCol("Existencia", "Existencia", 110, "{0:n6}")
            addCol("Cantidad", "Cantidad ajuste", 120, "{0:n6}")
            addCol("Diferencia", "Diferencia", 110, "{0:n6}")
            addCol("Bodegas", "Bodegas", 150, "")
            addCol("Ubicaciones", "Ubicaciones", 350, "")

            With GridView3.Columns("Lineas").SummaryItem
                .SummaryType = DevExpress.Data.SummaryItemType.Sum
                .DisplayFormat = "Total lineas: {0:n0}"
            End With
            With GridView3.Columns("Existencia").SummaryItem
                .SummaryType = DevExpress.Data.SummaryItemType.Sum
                .DisplayFormat = "Sum Exist: {0:n6}"
            End With
            With GridView3.Columns("Cantidad").SummaryItem
                .SummaryType = DevExpress.Data.SummaryItemType.Sum
                .DisplayFormat = "Sum Cant: {0:n6}"
            End With
            With GridView3.Columns("Diferencia").SummaryItem
                .SummaryType = DevExpress.Data.SummaryItemType.Sum
                .DisplayFormat = "Sum Dif: {0:n6}"
            End With
            With GridView3.Columns("Codigo").SummaryItem
                .SummaryType = DevExpress.Data.SummaryItemType.Count
                .DisplayFormat = "Productos distintos: {0:n0}"
            End With

            ' Ajuste visual: alinear numericos a la derecha
            For Each fn In {"Lineas", "Existencia", "Cantidad", "Diferencia"}
                GridView3.Columns(fn).AppearanceCell.TextOptions.HAlignment =
                GridView3.Columns(fn).AppearanceCell.TextOptions.HAlignment =
                    DevExpress.Utils.HorzAlignment.Far
                GridView3.Columns(fn).AppearanceHeader.TextOptions.HAlignment =
                    DevExpress.Utils.HorzAlignment.Far
            Next

            ' Resaltar filas con Diferencia <> 0
            AddHandler GridView3.RowStyle, AddressOf GridView3_RowStyle_RC2026
        Catch ex As Exception
            Try
                clsLnLog_error_wms.Agregar_Error(
                    "Set_Columnas_GridView3_RC2026: " & ex.Message)
            Catch
            End Try
        End Try
    End Sub

    Private Sub GridView3_RowStyle_RC2026(
            sender As Object,
            e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        Try
            Dim gv = DirectCast(sender,
                                DevExpress.XtraGrid.Views.Grid.GridView)
            If e.RowHandle < 0 Then Return
            Dim difObj = gv.GetRowCellValue(e.RowHandle, "Diferencia")
            If difObj Is Nothing OrElse IsDBNull(difObj) Then Return
            Dim dif As Decimal = 0D
            If Decimal.TryParse(difObj.ToString(),
                                Globalization.NumberStyles.Any,
                                Globalization.CultureInfo.CurrentCulture,
                                dif) Then
                If dif <> 0D Then
                    e.Appearance.BackColor =
                        System.Drawing.Color.FromArgb(255, 252, 232, 232)
                End If
            End If
        Catch
        End Try
    End Sub

    ' ========================================================================
    '  IMPRESION del Resumen Consolidado 2026
    '  Construye un XtraReport en codigo (sin .repx, sin typed dataset),
    '  con QR JSON compacto del documento en el header y filas con
    '  Diferencia <> 0 resaltadas en rosa palido.
    ' ========================================================================

    Private Sub Imprimir_Resumen_Consolidado_RC2026()

        ' --- 1) Datos cabecera (defensivo: el form puede no tenerlos todavia)
        Dim docId As Long = 0L
        Dim refer As String = ""
        Dim fecDoc As DateTime = DateTime.Now
        Dim idBod As Integer = 0
        Dim nomBod As String = ""
        Dim usuarioTxt As String = ""
        Dim esBorrador As Boolean = False

        Try
            If pBeTransAjustEnc IsNot Nothing Then
                docId = CLng(pBeTransAjustEnc.IdAjusteenc)
                Try : refer = If(pBeTransAjustEnc.Referencia, "") : Catch : End Try
                Try : fecDoc = pBeTransAjustEnc.Fecha : Catch : End Try
            End If
        Catch
        End Try

        Try
            If BeBodega IsNot Nothing Then
                idBod = CInt(BeBodega.IdBodega)
                Try : nomBod = If(BeBodega.Nombre, "") : Catch : End Try
            End If
        Catch
        End Try

        Try
            usuarioTxt = String.Format("{0} - {1} {2}",
                AP.UsuarioAp.Codigo, AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
        Catch
        End Try

        Try : esBorrador = chkBorrador.Checked : Catch : End Try

        ' --- 2) Sumas y conteos para el footer + el QR
        Dim totLineas As Integer = 0
        Dim totProd As Integer = DT_RC2026_Consolidado.Rows.Count
        Dim sumExi As Decimal = 0D
        Dim sumCan As Decimal = 0D
        Dim sumDif As Decimal = 0D
        For Each r As DataRow In DT_RC2026_Consolidado.Rows
            totLineas += CInt(r("Lineas"))
            sumExi += CDec(r("Existencia"))
            sumCan += CDec(r("Cantidad"))
            sumDif += CDec(r("Diferencia"))
        Next

        ' --- 3) JSON compacto para el QR (sin librerias externas)
        Dim qrJson As String = String.Format(
            Globalization.CultureInfo.InvariantCulture,
            "{{""t"":""AJ"",""id"":{0},""bod"":{1},""f"":""{2}"",""b"":{3},""ln"":{4},""pr"":{5},""se"":{6},""sc"":{7},""sd"":{8}}}",
            docId, idBod,
            fecDoc.ToString("yyyy-MM-ddTHH:mm:ss",
                            Globalization.CultureInfo.InvariantCulture),
            If(esBorrador, "true", "false"),
            totLineas, totProd, sumExi, sumCan, sumDif)

        ' --- 4) Construccion del reporte
        Dim rpt As New DevExpress.XtraReports.UI.XtraReport()
        rpt.PaperKind = System.Drawing.Printing.PaperKind.Letter
        rpt.Landscape = True
        rpt.Margins = New System.Drawing.Printing.Margins(40, 40, 40, 40)
        rpt.DataSource = DT_RC2026_Consolidado
        rpt.DataMember = ""
        rpt.Font = New System.Drawing.Font("Segoe UI", 8)
        rpt.DisplayName = $"Resumen Ajuste #{docId}"

        ' --- 4a) Page Header
        Dim phb As New DevExpress.XtraReports.UI.PageHeaderBand()
        phb.HeightF = 130
        rpt.Bands.Add(phb)

        ' Titulo
        Dim lblTitulo As New DevExpress.XtraReports.UI.XRLabel() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F),
            .SizeF = New System.Drawing.SizeF(720.0F, 28.0F),
            .Font = New System.Drawing.Font("Segoe UI Semibold", 16,
                                            System.Drawing.FontStyle.Bold),
            .Text = "AJUSTE DE STOCK - RESUMEN CONSOLIDADO",
            .ForeColor = System.Drawing.Color.FromArgb(33, 64, 95)
        }
        phb.Controls.Add(lblTitulo)

        ' Linea separadora
        Dim line1 As New DevExpress.XtraReports.UI.XRLine() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 30.0F),
            .SizeF = New System.Drawing.SizeF(720.0F, 2.0F),
            .ForeColor = System.Drawing.Color.FromArgb(33, 64, 95)
        }
        phb.Controls.Add(line1)

        ' Datos cabecera (lado izquierdo)
        Dim hdrFont As New System.Drawing.Font("Segoe UI", 9)
        Dim addHdr = Sub(label As String, value As String, top As Single)
                         Dim lblK As New DevExpress.XtraReports.UI.XRLabel() With {
                             .LocationFloat = New DevExpress.Utils.PointFloat(0F, top),
                             .SizeF = New System.Drawing.SizeF(80.0F, 14.0F),
                             .Font = New System.Drawing.Font("Segoe UI Semibold", 9,
                                                             System.Drawing.FontStyle.Bold),
                             .Text = label
                         }
                         Dim lblV As New DevExpress.XtraReports.UI.XRLabel() With {
                             .LocationFloat = New DevExpress.Utils.PointFloat(82.0F, top),
                             .SizeF = New System.Drawing.SizeF(540.0F, 14.0F),
                             .Font = hdrFont,
                             .Text = value
                         }
                         phb.Controls.Add(lblK)
                         phb.Controls.Add(lblV)
                     End Sub

        addHdr("Documento:", If(docId > 0, "AJ-" & docId.ToString(), "(nuevo)"), 38.0F)
        addHdr("Fecha:", fecDoc.ToString("dd/MM/yyyy HH:mm"), 54.0F)
        addHdr("Bodega:",
               If(idBod > 0, idBod.ToString() & " - " & nomBod, nomBod), 70.0F)
        addHdr("Usuario:", usuarioTxt, 86.0F)
        addHdr("Referencia:", refer, 102.0F)

        ' Watermark "BORRADOR" si aplica
        If esBorrador Then
            Dim lblBorr As New DevExpress.XtraReports.UI.XRLabel() With {
                .LocationFloat = New DevExpress.Utils.PointFloat(300.0F, 38.0F),
                .SizeF = New System.Drawing.SizeF(160.0F, 24.0F),
                .Font = New System.Drawing.Font("Segoe UI Black", 14,
                                                System.Drawing.FontStyle.Bold),
                .Text = "* BORRADOR *",
                .ForeColor = System.Drawing.Color.FromArgb(190, 60, 60),
                .TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                .Borders = DevExpress.XtraPrinting.BorderSide.All,
                .BorderColor = System.Drawing.Color.FromArgb(190, 60, 60),
                .BorderWidth = 2
            }
            phb.Controls.Add(lblBorr)
        End If

        ' QR (lado derecho)
        Dim qr As New DevExpress.XtraReports.UI.XRBarCode() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(640.0F, 38.0F),
            .SizeF = New System.Drawing.SizeF(80.0F, 80.0F),
            .Text = qrJson,
            .ShowText = False,
            .AutoModule = True,
            .Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100.0F)
        }
        Dim qrGen As New DevExpress.XtraPrinting.BarCode.QRCodeGenerator()
        qrGen.CompactionMode =
            DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte
        qrGen.ErrorCorrectionLevel =
            DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel.M
        qr.Symbology = qrGen
        phb.Controls.Add(qr)

        ' Etiqueta "Doc QR" debajo del QR
        Dim lblQrCap As New DevExpress.XtraReports.UI.XRLabel() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(640.0F, 118.0F),
            .SizeF = New System.Drawing.SizeF(80.0F, 10.0F),
            .Font = New System.Drawing.Font("Segoe UI", 7),
            .TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
            .Text = "Doc QR"
        }
        phb.Controls.Add(lblQrCap)

        ' --- 4b) Encabezado de columnas (XRTable como header pegado abajo del PH)
        Dim colDefs = New Object()() {
            New Object() {"Codigo", "Codigo", 70.0F, False},
            New Object() {"Producto", "Producto", 200.0F, False},
            New Object() {"UmBas", "UmBas", 50.0F, False},
            New Object() {"Talla", "Talla", 45.0F, False},
            New Object() {"Color", "Color", 55.0F, False},
            New Object() {"Lineas", "# Lineas", 55.0F, True},
            New Object() {"Existencia", "Existencia", 75.0F, True},
            New Object() {"Cantidad", "Cantidad", 75.0F, True},
            New Object() {"Diferencia", "Diferencia", 70.0F, True},
            New Object() {"Bodegas", "Bodegas", 80.0F, False},
            New Object() {"Ubicaciones", "Ubicaciones", 195.0F, False}
        }

        Dim hdrTbl As New DevExpress.XtraReports.UI.XRTable() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 130.0F - 18.0F),
            .SizeF = New System.Drawing.SizeF(970.0F, 18.0F),
            .Font = New System.Drawing.Font("Segoe UI Semibold", 8,
                                            System.Drawing.FontStyle.Bold),
            .Borders = DevExpress.XtraPrinting.BorderSide.All,
            .BackColor = System.Drawing.Color.FromArgb(33, 64, 95),
            .ForeColor = System.Drawing.Color.White
        }
        Dim hdrRow As New DevExpress.XtraReports.UI.XRTableRow()
        For Each cd In colDefs
            Dim caption = CStr(cd(1))
            Dim w = CSng(cd(2))
            Dim alignRight = CBool(cd(3))
            Dim cell As New DevExpress.XtraReports.UI.XRTableCell() With {
                .Text = caption,
                .WidthF = w,
                .TextAlignment = If(alignRight,
                    DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    DevExpress.XtraPrinting.TextAlignment.MiddleLeft),
                .Padding = New DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100.0F)
            }
            hdrRow.Cells.Add(cell)
        Next
        hdrTbl.Rows.Add(hdrRow)
        phb.Controls.Add(hdrTbl)

        ' --- 4c) Detail Band con XRTable bindeada al DataTable
        Dim detail As New DevExpress.XtraReports.UI.DetailBand()
        detail.HeightF = 16
        rpt.Bands.Add(detail)

        Dim detTbl As New DevExpress.XtraReports.UI.XRTable() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F),
            .SizeF = New System.Drawing.SizeF(970.0F, 16.0F),
            .Font = New System.Drawing.Font("Segoe UI", 8),
            .Borders = DevExpress.XtraPrinting.BorderSide.All,
            .BorderColor = System.Drawing.Color.FromArgb(220, 220, 220)
        }
        Dim detRow As New DevExpress.XtraReports.UI.XRTableRow()
        detRow.Name = "rowDetail"
        For Each cd In colDefs
            Dim field = CStr(cd(0))
            Dim w = CSng(cd(2))
            Dim alignRight = CBool(cd(3))
            Dim cell As New DevExpress.XtraReports.UI.XRTableCell() With {
                .WidthF = w,
                .TextAlignment = If(alignRight,
                    DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    DevExpress.XtraPrinting.TextAlignment.MiddleLeft),
                .Padding = New DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100.0F)
            }
            cell.ExpressionBindings.Add(
                New DevExpress.XtraReports.UI.ExpressionBinding(
                    "BeforePrint", "Text",
                    If(field = "Lineas", "[Lineas]",
                        If(alignRight, "FormatString('{0:n6}', [" & field & "])",
                                       "[" & field & "]"))))
            If field = "Lineas" Then
                cell.ExpressionBindings.Clear()
                cell.ExpressionBindings.Add(
                    New DevExpress.XtraReports.UI.ExpressionBinding(
                        "BeforePrint", "Text", "FormatString('{0:n0}', [Lineas])"))
            End If
            detRow.Cells.Add(cell)
        Next
        detTbl.Rows.Add(detRow)
        detail.Controls.Add(detTbl)

        ' Resaltar filas con Diferencia <> 0
        AddHandler detRow.BeforePrint,
            Sub(s, eArgs)
                Try
                    Dim row = DirectCast(s, DevExpress.XtraReports.UI.XRTableRow)
                    Dim difCell = row.Cells(8) ' indice de Diferencia segun colDefs
                    Dim difTxt = If(difCell.Text, "")
                    Dim dif As Decimal = 0D
                    If Decimal.TryParse(difTxt,
                            Globalization.NumberStyles.Any,
                            Globalization.CultureInfo.CurrentCulture, dif) Then
                        If dif <> 0D Then
                            row.BackColor = System.Drawing.Color.FromArgb(252, 232, 232)
                        Else
                            row.BackColor = System.Drawing.Color.White
                        End If
                    End If
                Catch
                End Try
            End Sub

        ' --- 4d) Report Footer: totales + firmas
        Dim rfb As New DevExpress.XtraReports.UI.ReportFooterBand()
        rfb.HeightF = 110
        rpt.Bands.Add(rfb)

        Dim line2 As New DevExpress.XtraReports.UI.XRLine() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 4.0F),
            .SizeF = New System.Drawing.SizeF(970.0F, 2.0F),
            .ForeColor = System.Drawing.Color.FromArgb(33, 64, 95)
        }
        rfb.Controls.Add(line2)

        Dim totFont As New System.Drawing.Font("Segoe UI Semibold", 9,
                                               System.Drawing.FontStyle.Bold)
        Dim addTot = Sub(label As String, value As String, top As Single)
                         Dim lblK As New DevExpress.XtraReports.UI.XRLabel() With {
                             .LocationFloat = New DevExpress.Utils.PointFloat(0F, top),
                             .SizeF = New System.Drawing.SizeF(160.0F, 14.0F),
                             .Font = totFont,
                             .Text = label
                         }
                         Dim lblV As New DevExpress.XtraReports.UI.XRLabel() With {
                             .LocationFloat = New DevExpress.Utils.PointFloat(160.0F, top),
                             .SizeF = New System.Drawing.SizeF(180.0F, 14.0F),
                             .Font = New System.Drawing.Font("Segoe UI", 9),
                             .TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                             .Text = value
                         }
                         rfb.Controls.Add(lblK)
                         rfb.Controls.Add(lblV)
                     End Sub

        addTot("Productos distintos:", totProd.ToString("n0"), 12.0F)
        addTot("Total lineas detalle:", totLineas.ToString("n0"), 28.0F)
        addTot("Sum Existencia:", sumExi.ToString("n6"), 44.0F)
        addTot("Sum Cantidad ajuste:", sumCan.ToString("n6"), 60.0F)
        addTot("Sum Diferencia:", sumDif.ToString("n6"), 76.0F)

        ' Firmas
        Dim sigFont As New System.Drawing.Font("Segoe UI", 8)
        Dim addSig = Sub(label As String, leftPos As Single)
                         Dim lnSig As New DevExpress.XtraReports.UI.XRLine() With {
                             .LocationFloat = New DevExpress.Utils.PointFloat(leftPos, 70.0F),
                             .SizeF = New System.Drawing.SizeF(220.0F, 1.0F),
                             .ForeColor = System.Drawing.Color.Black
                         }
                         Dim lblS As New DevExpress.XtraReports.UI.XRLabel() With {
                             .LocationFloat = New DevExpress.Utils.PointFloat(leftPos, 72.0F),
                             .SizeF = New System.Drawing.SizeF(220.0F, 14.0F),
                             .Font = sigFont,
                             .TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                             .Text = label
                         }
                         rfb.Controls.Add(lnSig)
                         rfb.Controls.Add(lblS)
                     End Sub
        addSig("Preparo", 510.0F)
        addSig("Aprobo", 750.0F)

        ' --- 4e) Page Footer: paginacion + fecha de impresion
        Dim pfb As New DevExpress.XtraReports.UI.PageFooterBand()
        pfb.HeightF = 24
        rpt.Bands.Add(pfb)

        Dim lnFoot As New DevExpress.XtraReports.UI.XRLine() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F),
            .SizeF = New System.Drawing.SizeF(970.0F, 1.0F),
            .ForeColor = System.Drawing.Color.FromArgb(180, 180, 180)
        }
        pfb.Controls.Add(lnFoot)

        Dim lblImpr As New DevExpress.XtraReports.UI.XRLabel() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 6.0F),
            .SizeF = New System.Drawing.SizeF(400.0F, 14.0F),
            .Font = sigFont,
            .Text = "Impreso: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm") &
                    "   |   Usuario impresion: " & usuarioTxt
        }
        pfb.Controls.Add(lblImpr)

        Dim pageInfo As New DevExpress.XtraReports.UI.XRPageInfo() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(800.0F, 6.0F),
            .SizeF = New System.Drawing.SizeF(170.0F, 14.0F),
            .Font = sigFont,
            .TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
            .PageInfo = DevExpress.XtraPrinting.PageInfo.NumberOfTotal,
            .Format = "Pag {0} de {1}"
        }
        pfb.Controls.Add(pageInfo)

        ' --- 5) Mostrar preview
        Dim tool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        tool.PreviewForm.WindowState = FormWindowState.Maximized
        tool.ShowPreview()
    End Sub

    '#EJC20260424
    ' ========================================================================
    '  IMPRESION del DETALLE del Ajuste (reemplaza Llenar_DS_Rep legacy)
    '  - Reporte XtraReport construido en codigo (sin .repx, sin typed dataset).
    '  - Columnas dinamicas segun BeBodega.Control_Talla_Color y
    '    BeBodega.Bodega_Cliente_Ajuste_ByB.
    '  - Columna UM unificada: Presentacion si existe, sino UmBas.
    '  - Chip de color en la celda Tipo segun categoria del ajuste.
    '  - Footer con resumen por Tipo + 3 firmas + QR JSON compacto.
    ' ========================================================================
    Private Sub Imprimir_Detalle_Ajuste_RC2026()

        ' --- 1) Datos cabecera (defensivo)
        Dim docId As Long = 0L
        Dim refer As String = ""
        Dim fecDoc As DateTime = DateTime.Now
        Dim idBod As Integer = 0
        Dim nomBod As String = ""
        Dim usuarioTxt As String = ""
        Dim esBorrador As Boolean = False

        Try
            If pBeTransAjustEnc IsNot Nothing Then
                docId = CLng(pBeTransAjustEnc.IdAjusteenc)
                Try : refer = If(pBeTransAjustEnc.Referencia, "") : Catch : End Try
                Try : fecDoc = pBeTransAjustEnc.Fecha : Catch : End Try
            End If
        Catch
        End Try

        Try
            If BeBodega IsNot Nothing Then
                idBod = CInt(BeBodega.IdBodega)
                Try : nomBod = If(BeBodega.Nombre, "") : Catch : End Try
            End If
        Catch
        End Try

        Try
            usuarioTxt = String.Format("{0} - {1} {2}",
                AP.UsuarioAp.Codigo, AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
        Catch
        End Try

        Try : esBorrador = chkBorrador.Checked : Catch : End Try

        ' --- 2) Flags de bodega (controlan que columnas aparecen)
        Dim incTallaColor As Boolean = False
        Try : incTallaColor = BeBodega.Control_Talla_Color : Catch : End Try
        Dim incBodegaByB As Boolean = False
        Try : incBodegaByB = BeBodega.Bodega_Cliente_Ajuste_ByB : Catch : End Try

        ' --- 3) DataTable dinamico (orden = orden de columnas en el reporte)
        Dim dt As New DataTable("AjusteDetalle")
        dt.Columns.Add("NumLinea", GetType(Integer))
        dt.Columns.Add("Codigo", GetType(String))
        dt.Columns.Add("Producto", GetType(String))
        dt.Columns.Add("UM", GetType(String))
        dt.Columns.Add("Ubicacion", GetType(String))
        dt.Columns.Add("Lote", GetType(String))
        dt.Columns.Add("LicPlate", GetType(String))
        dt.Columns.Add("Proveedor", GetType(String))
        If incTallaColor Then
            dt.Columns.Add("Talla", GetType(String))
            dt.Columns.Add("Color", GetType(String))
        End If
        If incBodegaByB Then
            dt.Columns.Add("Bodega", GetType(String))
        End If
        dt.Columns.Add("Motivo", GetType(String))
        dt.Columns.Add("Tipo", GetType(String))
        dt.Columns.Add("ValorAnterior", GetType(String))
        dt.Columns.Add("ValorActual", GetType(String))
        dt.Columns.Add("Observacion", GetType(String))

        ' --- 4) Helper local para leer celda como string seguro
        Dim cellStr = Function(rowIx As Integer, name As String) As String
                          Try
                              Dim v = dgrid.Rows(rowIx).Cells(name).Value
                              If v Is Nothing Then Return ""
                              Return v.ToString().Trim()
                          Catch
                              Return ""
                          End Try
                      End Function

        Dim cellFmt = Function(rowIx As Integer, name As String) As String
                          Try
                              Dim v = dgrid.Rows(rowIx).Cells(name).EditedFormattedValue
                              If v Is Nothing Then Return ""
                              Return v.ToString().Trim()
                          Catch
                              Return ""
                          End Try
                      End Function

        ' --- 5) Popular DT y acumular conteo por Tipo
        Dim countByTipo As New Dictionary(Of String, Integer)

        For i = 0 To dgrid.Rows.Count - 1
            Dim r = dt.NewRow()

            r("NumLinea") = i + 1
            r("Codigo") = cellStr(i, "ColCodigoProducto")
            r("Producto") = cellStr(i, "ColNombreProducto")

            ' UM unificada: Presentacion si existe, sino UmBas
            Dim presStr = cellStr(i, "colPresentacion")
            Dim umBasStr = cellStr(i, "UmBas")
            r("UM") = If(presStr <> "", presStr, umBasStr)

            ' Ubicacion: parseo igual que el reporte legacy (substring despues de "#")
            Dim ubic = cellStr(i, "colUbicacion")
            If ubic <> "" Then
                Dim ix = ubic.IndexOf("#"c)
                If ix >= 0 Then ubic = ubic.Substring(ix + 1)
            End If
            r("Ubicacion") = ubic

            r("Lote") = cellStr(i, "ColLote")
            r("LicPlate") = cellStr(i, "ColLicPlate")
            r("Proveedor") = cellStr(i, "ColProveedor")

            If incTallaColor Then
                r("Talla") = cellStr(i, "ColTalla")
                r("Color") = cellStr(i, "ColColor")
            End If

            If incBodegaByB Then
                ' ColBodega es DataGridViewComboBoxCell -> EditedFormattedValue
                Dim bodStr = cellFmt(i, "ColBodega")
                If bodStr = "" Then bodStr = If(idBod > 0, idBod.ToString(), "")
                r("Bodega") = bodStr
            End If

            Dim motivoStr = cellFmt(i, "motivoajuste")
            Dim tipoStr = cellFmt(i, "tipoajuste")
            r("Motivo") = motivoStr
            r("Tipo") = tipoStr

            ' ValorAnterior / ValorActual: misma logica que Llenar_DS_Rep legacy
            Dim cantP As Object = Nothing
            Try : cantP = dgrid.Rows(i).Cells("CantidadP").Value : Catch : End Try
            Dim cantC As Object = Nothing
            Try : cantC = dgrid.Rows(i).Cells("ColCantidad").Value : Catch : End Try

            Dim vAnt As String = ""
            Dim vAct As String = ""

            Select Case tipoStr
                Case "Ajuste Lote."
                    vAnt = cellStr(i, "ColLote")
                    vAct = If(cantC, "").ToString()
                Case "Ajuste Vencimiento"
                    Try : vAnt = Format(cantP, "dd-MM-yyyy") : Catch : vAnt = If(cantP, "").ToString() : End Try
                    Try : vAct = Format(cantC, "dd-MM-yyyy") : Catch : vAct = If(cantC, "").ToString() : End Try
                    If vAct = "dd-MM-yyyy" Then vAct = If(cantC, "").ToString()
                Case "Ajuste Positivo"
                    Try
                        vAnt = CDec(cantP).ToString("n6")
                        vAct = (CDec(cantP) + CDec(cantC)).ToString("n6")
                    Catch
                        vAnt = If(cantP, "").ToString()
                        vAct = If(cantC, "").ToString()
                    End Try
                Case "Ajuste Peso"
                    vAnt = If(cantP, "").ToString()
                    vAct = If(cantC, "").ToString()
                Case "Ajuste Negativo"
                    Try
                        vAnt = CDec(cantP).ToString("n6")
                        vAct = (CDec(cantP) - CDec(cantC)).ToString("n6")
                    Catch
                        vAnt = If(cantP, "").ToString()
                        vAct = If(cantC, "").ToString()
                    End Try
                Case Else
                    vAnt = If(cantP, "").ToString()
                    vAct = If(cantC, "").ToString()
            End Select

            r("ValorAnterior") = vAnt
            r("ValorActual") = vAct
            r("Observacion") = cellStr(i, "ColObservacion")

            dt.Rows.Add(r)

            ' Conteo por tipo
            Dim k = If(tipoStr = "", "(sin tipo)", tipoStr)
            If Not countByTipo.ContainsKey(k) Then countByTipo(k) = 0
            countByTipo(k) += 1
        Next

        ' --- 6) QR JSON compacto (tipo AJD = Ajuste Detalle)
        Dim qrJson As String = String.Format(
            Globalization.CultureInfo.InvariantCulture,
            "{{""t"":""AJD"",""id"":{0},""bod"":{1},""f"":""{2}"",""b"":{3},""ln"":{4},""tc"":{5},""bb"":{6}}}",
            docId, idBod,
            fecDoc.ToString("yyyy-MM-ddTHH:mm:ss",
                            Globalization.CultureInfo.InvariantCulture),
            If(esBorrador, "true", "false"),
            dt.Rows.Count,
            If(incTallaColor, 1, 0),
            If(incBodegaByB, 1, 0))

        ' --- 7) Construccion del XtraReport
        Dim rpt As New DevExpress.XtraReports.UI.XtraReport()
        rpt.PaperKind = System.Drawing.Printing.PaperKind.Letter
        rpt.Landscape = True
        rpt.Margins = New System.Drawing.Printing.Margins(40, 40, 40, 40)
        rpt.DataSource = dt
        rpt.DataMember = ""
        rpt.Font = New System.Drawing.Font("Segoe UI", 8)
        rpt.DisplayName = $"Detalle Ajuste #{docId}"

        Const PAGE_W As Single = 970.0F   ' Letter horizontal usable

        ' --- 7a) Page Header (140 px)
        Dim phb As New DevExpress.XtraReports.UI.PageHeaderBand()
        phb.HeightF = 140
        rpt.Bands.Add(phb)

        Dim lblTitulo As New DevExpress.XtraReports.UI.XRLabel() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F),
            .SizeF = New System.Drawing.SizeF(720.0F, 28.0F),
            .Font = New System.Drawing.Font("Segoe UI Semibold", 16,
                                            System.Drawing.FontStyle.Bold),
            .Text = "AJUSTE DE STOCK - DETALLE",
            .ForeColor = System.Drawing.Color.FromArgb(33, 64, 95)
        }
        phb.Controls.Add(lblTitulo)

        Dim line1 As New DevExpress.XtraReports.UI.XRLine() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 30.0F),
            .SizeF = New System.Drawing.SizeF(PAGE_W, 2.0F),
            .ForeColor = System.Drawing.Color.FromArgb(33, 64, 95)
        }
        phb.Controls.Add(line1)

        ' Datos cabecera en 2 columnas
        Dim hdrFont As New System.Drawing.Font("Segoe UI", 9)
        Dim hdrBold As New System.Drawing.Font("Segoe UI Semibold", 9,
                                               System.Drawing.FontStyle.Bold)

        Dim addHdr2 = Sub(label As String, value As String,
                          col As Integer, top As Single)
                          Dim leftK As Single = If(col = 0, 0F, 320.0F)
                          Dim leftV As Single = If(col = 0, 82.0F, 410.0F)
                          Dim wV As Single = If(col = 0, 230.0F, 220.0F)
                          Dim lblK As New DevExpress.XtraReports.UI.XRLabel() With {
                              .LocationFloat = New DevExpress.Utils.PointFloat(leftK, top),
                              .SizeF = New System.Drawing.SizeF(80.0F, 14.0F),
                              .Font = hdrBold,
                              .Text = label
                          }
                          Dim lblV As New DevExpress.XtraReports.UI.XRLabel() With {
                              .LocationFloat = New DevExpress.Utils.PointFloat(leftV, top),
                              .SizeF = New System.Drawing.SizeF(wV, 14.0F),
                              .Font = hdrFont,
                              .Text = value
                          }
                          phb.Controls.Add(lblK)
                          phb.Controls.Add(lblV)
                      End Sub

        addHdr2("Documento:", If(docId > 0, "AJ-" & docId.ToString(), "(nuevo)"), 0, 38.0F)
        addHdr2("Fecha:", fecDoc.ToString("dd/MM/yyyy HH:mm"), 0, 54.0F)
        addHdr2("Bodega:",
                If(idBod > 0, idBod.ToString() & " - " & nomBod, nomBod), 0, 70.0F)
        addHdr2("Referencia:", refer, 0, 86.0F)

        addHdr2("Usuario:", usuarioTxt, 1, 38.0F)
        addHdr2("Estado:", If(esBorrador, "BORRADOR", "Definitivo"), 1, 54.0F)
        addHdr2("Total lineas:", dt.Rows.Count.ToString("n0"), 1, 70.0F)
        addHdr2("Talla/Color:", If(incTallaColor, "Si", "No") &
                "    Bodega ByB: " & If(incBodegaByB, "Si", "No"), 1, 86.0F)

        ' Watermark BORRADOR
        If esBorrador Then
            Dim lblBorr As New DevExpress.XtraReports.UI.XRLabel() With {
                .LocationFloat = New DevExpress.Utils.PointFloat(580.0F, 4.0F),
                .SizeF = New System.Drawing.SizeF(160.0F, 24.0F),
                .Font = New System.Drawing.Font("Segoe UI Black", 14,
                                                System.Drawing.FontStyle.Bold),
                .Text = "* BORRADOR *",
                .ForeColor = System.Drawing.Color.FromArgb(190, 60, 60),
                .TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                .Borders = DevExpress.XtraPrinting.BorderSide.All,
                .BorderColor = System.Drawing.Color.FromArgb(190, 60, 60),
                .BorderWidth = 2
            }
            phb.Controls.Add(lblBorr)
        End If

        ' QR
        Dim qr As New DevExpress.XtraReports.UI.XRBarCode() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(880.0F, 38.0F),
            .SizeF = New System.Drawing.SizeF(90.0F, 90.0F),
            .Text = qrJson,
            .ShowText = False,
            .AutoModule = True,
            .Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100.0F)
        }
        Dim qrGen As New DevExpress.XtraPrinting.BarCode.QRCodeGenerator()
        qrGen.CompactionMode =
            DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte
        qrGen.ErrorCorrectionLevel =
            DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel.M
        qr.Symbology = qrGen
        phb.Controls.Add(qr)

        Dim lblQrCap As New DevExpress.XtraReports.UI.XRLabel() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(880.0F, 128.0F),
            .SizeF = New System.Drawing.SizeF(90.0F, 10.0F),
            .Font = New System.Drawing.Font("Segoe UI", 7),
            .TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
            .Text = "Doc QR (AJD)"
        }
        phb.Controls.Add(lblQrCap)

        ' --- 7b) Definicion dinamica de columnas
        '  {fieldName, captionTxt, widthF, alignRight, wrap}
        Dim cols As New List(Of Object())()
        cols.Add(New Object() {"NumLinea", "#", 28.0F, True, False})
        cols.Add(New Object() {"Codigo", "Codigo", 60.0F, False, False})
        cols.Add(New Object() {"Producto", "Producto", 175.0F, False, True})
        cols.Add(New Object() {"UM", "UM", 55.0F, False, False})
        cols.Add(New Object() {"Ubicacion", "Ubicacion", 75.0F, False, False})
        cols.Add(New Object() {"Lote", "Lote", 60.0F, False, False})
        cols.Add(New Object() {"LicPlate", "LicPlate", 65.0F, False, False})
        cols.Add(New Object() {"Proveedor", "Proveedor", 110.0F, False, True})
        If incTallaColor Then
            cols.Add(New Object() {"Talla", "Talla", 38.0F, False, False})
            cols.Add(New Object() {"Color", "Color", 50.0F, False, False})
        End If
        If incBodegaByB Then
            cols.Add(New Object() {"Bodega", "Bod.", 40.0F, False, False})
        End If
        cols.Add(New Object() {"Motivo", "Motivo", 80.0F, False, False})
        cols.Add(New Object() {"Tipo", "Tipo", 75.0F, False, False})
        cols.Add(New Object() {"ValorAnterior", "V. Anterior", 65.0F, True, False})
        cols.Add(New Object() {"ValorActual", "V. Actual", 65.0F, True, False})
        cols.Add(New Object() {"Observacion", "Observacion", 130.0F, False, True})

        ' Reescalar para que sumen exactamente PAGE_W
        Dim sumW As Single = 0F
        For Each cd In cols
            sumW += CSng(cd(2))
        Next
        If sumW > 0F Then
            Dim factor As Single = PAGE_W / sumW
            For Each cd In cols
                cd(2) = CSng(cd(2)) * factor
            Next
        End If

        ' Tipo column index (para chip de color)
        Dim tipoColIdx As Integer = -1
        For ix = 0 To cols.Count - 1
            If CStr(cols(ix)(0)) = "Tipo" Then
                tipoColIdx = ix
                Exit For
            End If
        Next

        ' --- 7c) Encabezado de columnas (ultima banda del PageHeader)
        Dim hdrTbl As New DevExpress.XtraReports.UI.XRTable() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 122.0F),
            .SizeF = New System.Drawing.SizeF(PAGE_W, 18.0F),
            .Font = New System.Drawing.Font("Segoe UI Semibold", 8,
                                            System.Drawing.FontStyle.Bold),
            .Borders = DevExpress.XtraPrinting.BorderSide.All,
            .BackColor = System.Drawing.Color.FromArgb(33, 64, 95),
            .ForeColor = System.Drawing.Color.White
        }
        Dim hdrRow As New DevExpress.XtraReports.UI.XRTableRow()
        For Each cd In cols
            Dim caption = CStr(cd(1))
            Dim w = CSng(cd(2))
            Dim alignRight = CBool(cd(3))
            Dim cell As New DevExpress.XtraReports.UI.XRTableCell() With {
                .Text = caption,
                .WidthF = w,
                .TextAlignment = If(alignRight,
                    DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    DevExpress.XtraPrinting.TextAlignment.MiddleLeft),
                .Padding = New DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100.0F)
            }
            hdrRow.Cells.Add(cell)
        Next
        hdrTbl.Rows.Add(hdrRow)
        phb.Controls.Add(hdrTbl)

        ' --- 7d) Detail Band con XRTable bindeada al DT
        Dim detail As New DevExpress.XtraReports.UI.DetailBand()
        detail.HeightF = 18
        rpt.Bands.Add(detail)

        Dim detTbl As New DevExpress.XtraReports.UI.XRTable() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F),
            .SizeF = New System.Drawing.SizeF(PAGE_W, 18.0F),
            .Font = New System.Drawing.Font("Segoe UI", 8),
            .Borders = DevExpress.XtraPrinting.BorderSide.All,
            .BorderColor = System.Drawing.Color.FromArgb(220, 220, 220)
        }
        Dim detRow As New DevExpress.XtraReports.UI.XRTableRow()
        detRow.Name = "rowDetail"
        For Each cd In cols
            Dim field = CStr(cd(0))
            Dim w = CSng(cd(2))
            Dim alignRight = CBool(cd(3))
            Dim wrap = CBool(cd(4))
            Dim cell As New DevExpress.XtraReports.UI.XRTableCell() With {
                .WidthF = w,
                .TextAlignment = If(alignRight,
                    DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                    DevExpress.XtraPrinting.TextAlignment.MiddleLeft),
                .Padding = New DevExpress.XtraPrinting.PaddingInfo(3, 3, 1, 1, 100.0F),
                .Multiline = wrap,
                .CanGrow = wrap
            }
            cell.ExpressionBindings.Add(
                New DevExpress.XtraReports.UI.ExpressionBinding(
                    "BeforePrint", "Text", "[" & field & "]"))
            detRow.Cells.Add(cell)
        Next
        detRow.CanGrow = True
        detTbl.Rows.Add(detRow)
        detail.CanGrow = True
        detail.Controls.Add(detTbl)

        ' Zebra + chip de color en la celda Tipo
        AddHandler detRow.BeforePrint,
            Sub(s, eArgs)
                Try
                    Dim row = DirectCast(s, DevExpress.XtraReports.UI.XRTableRow)
                    Dim ix As Integer = 0
                    Try : ix = CInt(rpt.GetCurrentColumnValue("NumLinea")) : Catch : End Try

                    ' Zebra
                    If (ix Mod 2) = 0 Then
                        row.BackColor = System.Drawing.Color.FromArgb(247, 249, 252)
                    Else
                        row.BackColor = System.Drawing.Color.White
                    End If

                    ' Chip de color en celda Tipo
                    If tipoColIdx >= 0 AndAlso tipoColIdx < row.Cells.Count Then
                        Dim tipoValObj As Object = Nothing
                        Try : tipoValObj = rpt.GetCurrentColumnValue("Tipo") : Catch : End Try
                        Dim tipoVal = If(tipoValObj, "").ToString().Trim()
                        Dim chip = row.Cells(tipoColIdx)
                        Select Case tipoVal
                            Case "Ajuste Positivo"
                                chip.BackColor = System.Drawing.Color.FromArgb(220, 245, 220)
                                chip.ForeColor = System.Drawing.Color.FromArgb(20, 90, 30)
                            Case "Ajuste Negativo"
                                chip.BackColor = System.Drawing.Color.FromArgb(252, 224, 224)
                                chip.ForeColor = System.Drawing.Color.FromArgb(140, 30, 30)
                            Case "Ajuste Lote."
                                chip.BackColor = System.Drawing.Color.FromArgb(220, 234, 252)
                                chip.ForeColor = System.Drawing.Color.FromArgb(30, 60, 140)
                            Case "Ajuste Vencimiento"
                                chip.BackColor = System.Drawing.Color.FromArgb(255, 234, 200)
                                chip.ForeColor = System.Drawing.Color.FromArgb(150, 80, 0)
                            Case "Ajuste Peso"
                                chip.BackColor = System.Drawing.Color.FromArgb(232, 220, 245)
                                chip.ForeColor = System.Drawing.Color.FromArgb(80, 30, 130)
                            Case Else
                                chip.BackColor = System.Drawing.Color.FromArgb(235, 235, 235)
                                chip.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80)
                        End Select
                        chip.Font = New System.Drawing.Font(
                            "Segoe UI Semibold", 8, System.Drawing.FontStyle.Bold)
                    End If
                Catch
                End Try
            End Sub

        ' --- 7e) Report Footer: resumen por tipo + firmas
        Dim rfb As New DevExpress.XtraReports.UI.ReportFooterBand()
        rfb.HeightF = 130
        rpt.Bands.Add(rfb)

        Dim line2 As New DevExpress.XtraReports.UI.XRLine() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 4.0F),
            .SizeF = New System.Drawing.SizeF(PAGE_W, 2.0F),
            .ForeColor = System.Drawing.Color.FromArgb(33, 64, 95)
        }
        rfb.Controls.Add(line2)

        Dim lblTotHdr As New DevExpress.XtraReports.UI.XRLabel() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 10.0F),
            .SizeF = New System.Drawing.SizeF(300.0F, 14.0F),
            .Font = New System.Drawing.Font("Segoe UI Semibold", 10,
                                            System.Drawing.FontStyle.Bold),
            .Text = "Resumen por Tipo de ajuste",
            .ForeColor = System.Drawing.Color.FromArgb(33, 64, 95)
        }
        rfb.Controls.Add(lblTotHdr)

        ' Mini tabla por tipo (max 6 filas, ordenadas por tipo)
        Dim topY As Single = 28.0F
        Dim totFont As New System.Drawing.Font("Segoe UI", 9)
        Dim totFontB As New System.Drawing.Font("Segoe UI Semibold", 9,
                                                System.Drawing.FontStyle.Bold)

        Dim ixT As Integer = 0
        For Each kv In countByTipo.OrderBy(Function(x) x.Key)
            Dim lblK As New DevExpress.XtraReports.UI.XRLabel() With {
                .LocationFloat = New DevExpress.Utils.PointFloat(0F, topY),
                .SizeF = New System.Drawing.SizeF(180.0F, 14.0F),
                .Font = totFontB,
                .Text = kv.Key
            }
            Dim lblV As New DevExpress.XtraReports.UI.XRLabel() With {
                .LocationFloat = New DevExpress.Utils.PointFloat(180.0F, topY),
                .SizeF = New System.Drawing.SizeF(70.0F, 14.0F),
                .Font = totFont,
                .TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                .Text = kv.Value.ToString("n0") & " linea(s)"
            }
            rfb.Controls.Add(lblK)
            rfb.Controls.Add(lblV)
            topY += 16.0F
            ixT += 1
            If ixT >= 6 Then Exit For
        Next

        ' Firmas: 3 (Preparo / Aprobo / Recibido)
        Dim sigFont As New System.Drawing.Font("Segoe UI", 8)
        Dim addSig = Sub(label As String, leftPos As Single)
                         Dim lnSig As New DevExpress.XtraReports.UI.XRLine() With {
                             .LocationFloat = New DevExpress.Utils.PointFloat(leftPos, 90.0F),
                             .SizeF = New System.Drawing.SizeF(180.0F, 1.0F),
                             .ForeColor = System.Drawing.Color.Black
                         }
                         Dim lblS As New DevExpress.XtraReports.UI.XRLabel() With {
                             .LocationFloat = New DevExpress.Utils.PointFloat(leftPos, 92.0F),
                             .SizeF = New System.Drawing.SizeF(180.0F, 14.0F),
                             .Font = sigFont,
                             .TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                             .Text = label
                         }
                         rfb.Controls.Add(lnSig)
                         rfb.Controls.Add(lblS)
                     End Sub
        addSig("Preparo", 400.0F)
        addSig("Aprobo", 600.0F)
        addSig("Recibido", 800.0F)

        ' --- 7f) Page Footer: paginacion + fecha
        Dim pfb As New DevExpress.XtraReports.UI.PageFooterBand()
        pfb.HeightF = 24
        rpt.Bands.Add(pfb)

        Dim lnFoot As New DevExpress.XtraReports.UI.XRLine() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F),
            .SizeF = New System.Drawing.SizeF(PAGE_W, 1.0F),
            .ForeColor = System.Drawing.Color.FromArgb(180, 180, 180)
        }
        pfb.Controls.Add(lnFoot)

        Dim lblImpr As New DevExpress.XtraReports.UI.XRLabel() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(0F, 6.0F),
            .SizeF = New System.Drawing.SizeF(500.0F, 14.0F),
            .Font = sigFont,
            .Text = "Impreso: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm") &
                    "   |   Usuario impresion: " & usuarioTxt
        }
        pfb.Controls.Add(lblImpr)

        Dim pageInfo As New DevExpress.XtraReports.UI.XRPageInfo() With {
            .LocationFloat = New DevExpress.Utils.PointFloat(800.0F, 6.0F),
            .SizeF = New System.Drawing.SizeF(170.0F, 14.0F),
            .Font = sigFont,
            .TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
            .PageInfo = DevExpress.XtraPrinting.PageInfo.NumberOfTotal,
            .Format = "Pag {0} de {1}"
        }
        pfb.Controls.Add(pageInfo)

        ' --- 8) Mostrar preview
        Dim tool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        tool.PreviewForm.WindowState = FormWindowState.Maximized
        tool.ShowPreview()
    End Sub

#End Region

End Class