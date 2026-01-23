Imports DevExpress.Utils
Imports DevExpress.Xpf.Editors
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen
Imports Newtonsoft.Json

Public Class frmPreFactura

    Private AcuerdosEncGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private AcuerdosDetalleGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private txtTotalGrid As New RepositoryItemSpinEdit
    Private tipo_doc_ingreso As Boolean = False
    Private pListObjT As New List(Of clsTabla)
    Public BePrefacturaEnc As New clsBeTrans_prefactura_enc
    Private BePrefacturaDet As New clsBeTrans_prefactura_det
    Private listBePrefacturaDet As New List(Of clsBeTrans_prefactura_det)
    Private BeprefacturaMov As New clsBeTrans_prefactura_mov
    Private listBePrefacturaMov As New List(Of clsBeTrans_prefactura_mov)
    Private pBodega As New clsBeBodega
    Private BeOCEnc As New clsBeTrans_oc_enc
    Dim pPolizaIngresoExiste As New clsBeTrans_oc_pol
    Dim pPolizaSalidaExiste As New clsBeTrans_pe_pol
    Dim pPolizaIngreso As Boolean
    Dim pPolizaSalida As Boolean
    Dim pCalculoProcesado As Boolean
    Dim oc_pol_numero_orden As String = ""
    Dim pe_pol_numero_orden As String = ""
    Private IdPropietario As Integer = 0
    Private pIdOrdenCompraEnc_pol As Integer = 0
    '#Private Es_Cobro_Minimo As Boolean = False

    '#GT26062024: variables para almacenar el acuerdo y el valor por cada servicio a cobrar.
    Private vIdAcuerdoEnc As Integer = 0
    Private vIdAcuerdoDet As Integer = 0
    Private vCodigoproducto As String = ""
    Private vCodacuerdo As Integer = 0
    Private vMoneda As String = ""
    Private vDescripcion As String = ""
    Private vCorrelativo As Integer = 0
    Private vServicio As String = ""
    Private vNumero_unidades As Integer = 0
    Private vDias_eventos As Integer = 0
    Private vMonto As Double = 0.00
    Private vPorcentaje As Double = 0.00
    Private vCobro_por_linea As Double = 0.00
    Private vCobroTotal As Double = 0.00

    Private DTAcuerdos As New DataTable
    Public Delegate Sub ListarPrefacturas()
    Public Property InvokeListarPrefacturas As ListarPrefacturas
    Public Property TasaCambioBanGuat As Double = 0
    '#GT30082024: mostrar el nombre del cliente en la impresión del detalle
    Private vClienteFacturar As String = ""
    '#GT07102024: identificar si el ingreso es devolución
    Private pTrans_oc_ti As New clsBeTrans_oc_ti

    Private Es_Seleccion_Multiple As Boolean

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub


    Private DTGridDetalleServicios As New DataTable("DetalleServicios")
    Private Sub Set_Datata_Table_Grid_Detalle_Servicios()

        DTGridDetalleServicios.Columns.Clear()
        DTGridDetalleServicios.Columns.Add("IdAcuerdoEnc", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("IdAcuerdoDet", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("codigo_producto", GetType(String))
        DTGridDetalleServicios.Columns.Add("moneda", GetType(String))
        DTGridDetalleServicios.Columns.Add("codigo_acuerdo", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("nombre_acuerdo", GetType(String))
        DTGridDetalleServicios.Columns.Add("correlativo_detalleacuerdo", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("servicio", GetType(String))
        DTGridDetalleServicios.Columns.Add("numero_unidades", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("monto", GetType(Double))
        DTGridDetalleServicios.Columns.Add("porcentaje", GetType(Double))
        DTGridDetalleServicios.Columns.Add("total", GetType(Double))
        DTGridDetalleServicios.Columns.Add("erp", GetType(Double))
        DTGridDetalleServicios.Columns.Add("dias_eventos", GetType(Integer))



    End Sub

    Private DTGridDetallePreCuenta As New DataTable("DetalleMovimientos")
    Private Sub Set_Data_Table_Grid_Detalle_Movimientos()

        DTGridDetallePreCuenta.Columns.Clear()
        DTGridDetallePreCuenta.Columns.Add("idordencompraenc", GetType(Integer))
        DTGridDetallePreCuenta.Columns.Add("numero_orden", GetType(String))
        DTGridDetallePreCuenta.Columns.Add("codigo_producto", GetType(String))
        DTGridDetallePreCuenta.Columns.Add("nombre", GetType(String))
        DTGridDetallePreCuenta.Columns.Add("fecha", GetType(DateTime))
        DTGridDetallePreCuenta.Columns.Add("unidades", GetType(Integer))
        DTGridDetallePreCuenta.Columns.Add("valor_total", GetType(Double))
        DTGridDetallePreCuenta.Columns.Add("almacenaje", GetType(Double))
        DTGridDetallePreCuenta.Columns.Add("manejo", GetType(Double))

    End Sub


    Private Sub frmPreFactura_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            pPolizaIngreso = False
            pPolizaSalida = False

            DTGridDetalleServicios.Clear()
            DTGridDetallePreCuenta.Clear()

            AP.Listar_Bodegas_By_Usuario(cmbBodega)
            Set_Datata_Table_Grid_Detalle_Servicios()
            Set_Columnas_Grid_Detalle_Servicios()
            Set_Data_Table_Grid_Detalle_Movimientos()

            dgridServiciosAsociados.DataSource = DTGridDetalleServicios
            dgriDetallePreCuenta.DataSource = DTGridDetallePreCuenta

            gvdetalleprecuenta.BestFitColumns()
            cmbBodega.Enabled = False
            txtAlmacenajeDiario.Enabled = False
            dtFechaIngreso.Enabled = False
            dtFechaSalida.Enabled = False

            '#GT21082024: ocultar, a futuro podria ser utilizada la funcionalidad.
            '#GT04102024: removi los objetos para hacer espacio al log
            'lblProductoPoliza.Visible = False
            'cmbProductosPoliza.Visible = False

            txtPesoTotal.Properties.MaskSettings.MaskExpression = "n2"

            Select Case Modo

                Case TipoTrans.Nuevo

                    pBodega = AP.Bodega
                    Listar_Propietarios()
                    Listar_Clientes()
                    txtNoDocumento.Text = clsLnTrans_prefactura_enc.MaxID() + 1
                    pPolizaIngresoExiste = New clsBeTrans_oc_pol
                    pPolizaSalidaExiste = New clsBeTrans_pe_pol
                    BePrefacturaEnc = New clsBeTrans_prefactura_enc()
                    BePrefacturaEnc.IsNew = True
                    dtpFechaDocumento.EditValue = Now
                    dtFechaIngreso.EditValue = Now
                    dtFechaSalida.EditValue = Now
                    '#GT26092024: se remueve ya que el operador finalmente digita el tipo de cambio o se obtiene de la póliza.
                    'ObtenerTipoCambioDia()
                    txtDiasAlmacenaje.EditValue = ""
                    cmdProcesar.Visibility = BarItemVisibility.Always
                    chkMantenerDolares.Enabled = True
                    lblScanPoliza.Visible = False
                    txtScanPoliza.Visible = False

                    If Not pBodega.Es_Bodega_Fiscal Then
                        chkConsolidados.Checked = False
                        chkConsolidados.Enabled = False
                    End If


                    If chkConsolidados.Checked Then

                        gbErrores.Visible = False

                        lblConsolidador.Text = "Consolidador"
                        lblFechaInicial.Visible = False
                        lblFechaFinal.Visible = False
                        dtpFechaDesde.Visible = False
                        dtpfechaHasta.Visible = False
                        lblPolizaSalida.Visible = True
                        cmbPolizasOC.Visible = True
                        cmbPolizasPE.Visible = True
                        pnValores.Visible = True
                        txtValorGeneral.Enabled = False
                        txtValorAduana.Enabled = False
                        txtPesoTotal.Enabled = False
                        cmdCorreccionPoliza.Enabled = True
                        cmbPropietario.Focus()
                        chkVarianteCobro.Enabled = False
                        'chkAgruparPorProducto.Enabled = False
                        chkEstimacionCobro.Enabled = True
                        chkAgruparPorProducto.Enabled = False

                    Else
                        gbErrores.Visible = True
                        lblConsolidador.Text = "Propietario"
                        lblFechaInicial.Visible = True
                        lblFechaFinal.Visible = True
                        dtpFechaDesde.Visible = True
                        dtpfechaHasta.Visible = True
                        lblPolizaSalida.Visible = False
                        pnValores.Visible = False
                        cmbPolizasOC.Visible = True
                        cmbPolizasPE.Visible = False
                        cmdCorreccionPoliza.Enabled = False
                        cmbPropietario.Focus()
                        chkVarianteCobro.Enabled = True
                        'chkAgruparPorProducto.Enabled = True
                        chkEstimacionCobro.Enabled = False

                        chkAgruparPorProducto.Enabled = True
                    End If

                    If chkVarianteCobro.Checked Then
                        'lblProductoPoliza.Visible = True
                        'cmbProductosPoliza.Visible = True
                        'chkAgruparPorProducto.Enabled = True
                    Else
                        'lblProductoPoliza.Visible = False
                        'cmbProductosPoliza.Visible = False
                        'chkAgruparPorProducto.Enabled = False
                    End If

                    '#GT16102025: inferencia para asignar el rango de fecha a cobrar
                    AsignarFechasInferidas()

                Case TipoTrans.Editar

                    pBodega = clsLnBodega.GetSingle_By_Idbodega(BePrefacturaEnc.IdBodega)

                    cmdGuardar.Enabled = False
                    cmdCorreccionPoliza.Enabled = False
                    If BePrefacturaEnc.Anulada Then
                        cmdAnularPrefactura.Enabled = False
                    End If

                    cmbBodega.EditValue = BePrefacturaEnc.IdBodega
                    Listar_Propietarios()
                    Listar_Clientes()

                    '#GT23052024: campos indistintos de si es consolidador o no
                    chkConsolidados.Checked = BePrefacturaEnc.Es_consolidador
                    chkConsolidados.Enabled = False
                    chkControlPesoBruto.Checked = BePrefacturaEnc.Cobro_peso_bruto
                    chkControlPesoBruto.Enabled = False
                    chkVarianteCobro.Checked = BePrefacturaEnc.Variante_cobro
                    chkAgruparPorProducto.Checked = BePrefacturaEnc.Agrupar_producto

                    chkEstimacionCobro.Enabled = False
                    lblScanPoliza.Visible = False
                    txtScanPoliza.Visible = False

                    txtNoDocumento.Text = BePrefacturaEnc.IdTransPrefacturaEnc
                    dtpFechaDocumento.EditValue = BePrefacturaEnc.Fec_agr
                    cmbPropietario.EditValue = BePrefacturaEnc.IdPropietarioBodega
                    txtObservacion.Text = BePrefacturaEnc.Observacion
                    txtTipoCambio.Value = BePrefacturaEnc.Tipo_Cambio
                    chkVarianteCobro.Enabled = False
                    chkAgruparPorProducto.Enabled = False
                    chkMantenerDolares.Enabled = False

                    If BePrefacturaEnc.Es_consolidador Then
                        pnValores.Visible = True
                        cmbPolizasOC.EditValue = BePrefacturaEnc.IdOrdenCompraEnc

                        If BePrefacturaEnc.IdOrdenPedidoEnc > 0 Then
                            cmbPolizasPE.EditValue = BePrefacturaEnc.IdOrdenPedidoEnc
                        End If

                        dtFechaIngreso.EditValue = BePrefacturaEnc.Fecha_desde
                        dtFechaSalida.EditValue = BePrefacturaEnc.Fecha_hasta
                        txtPesoTotal.EditValue = BePrefacturaEnc.Valor_Peso
                        txtValorAduana.EditValue = BePrefacturaEnc.Valor_Aduana
                        txtValorGeneral.EditValue = BePrefacturaEnc.Valor_General
                        '#GT05062024: validamos fechas sin hora, porque causa diferencia en calculo.
                        Dim fechainicial = BePrefacturaEnc.Fecha_desde.Date
                        Dim fechafinal = BePrefacturaEnc.Fecha_hasta.Date
                        'txtDiasAlmacenaje.EditValue = DateDiff(DateInterval.Day, fechainicial.Date, fechafinal.Date) + 1
                        txtDiasAlmacenaje.EditValue = DateDiff(DateInterval.Day, fechainicial.Date, fechafinal.Date)
                        txtValorGeneral.Enabled = False
                        txtValorAduana.Enabled = False
                        txtPesoTotal.Enabled = False
                        txtDiasAlmacenaje.Enabled = False

                    Else

                        '#GT24052024: cargar cliente sino se facturo al consolidador
                        If BePrefacturaEnc.IdClienteBodega > 0 Then
                            cmbCliente.EditValue = BePrefacturaEnc.IdClienteBodega
                        End If

                        pnValores.Visible = False
                        dtpFechaDesde.Value = BePrefacturaEnc.Fecha_desde
                        dtpfechaHasta.Value = BePrefacturaEnc.Fecha_hasta
                        txtTipoCambio.EditValue = BePrefacturaEnc.Tipo_Cambio

                        cmbPolizasOC.Enabled = False
                        cmbPolizasPE.Enabled = False
                        txtTipoCambio.Enabled = False
                        txtObservacion.Enabled = False
                        cmbPropietario.Enabled = False
                        dtpFechaDesde.Enabled = False
                        dtpfechaHasta.Enabled = False

                    End If

                    '#GT24052024: cargar detalle de los acuerdos calculados y sus valores
                    Cargar_Detalle_Prefactura()

                    '#GT25052024: cargar dtalle precuenta para acuerdos con corte mensual
                    Cargar_Detalle_Movimientos()

                    cmdProcesar.Enabled = False
                    cmdCorreccionPoliza.Enabled = False
                    cmbPropietario.Focus()
                    cmbCliente.Enabled = False

                    Cargar_NombreCliente_Impresion()

            End Select

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub AsignarFechasInferidas()
        Try
            '#GT16102025: se infiere el mes a cobrar para facilitar el rango de fechas
            Dim fechaActual As Date = Date.Now
            Dim mesBase As Integer
            Dim anioBase As Integer

            If fechaActual.Day < 15 Then
                mesBase = fechaActual.Month - 1
                anioBase = fechaActual.Year
                If mesBase = 0 Then
                    mesBase = 12
                    anioBase -= 1
                End If
            Else
                mesBase = fechaActual.Month
                anioBase = fechaActual.Year
            End If

            Dim primerDia As New Date(anioBase, mesBase, 1)
            Dim ultimoDia As Date = primerDia.AddMonths(1).AddDays(-1)

            dtpFechaDesde.Value = primerDia
            dtpfechaHasta.Value = ultimoDia

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub


    Private Sub AjustarFechaInferida(fechaSeleccionada As Date)
        Try

            Dim primerDia As New Date(fechaSeleccionada.Year, fechaSeleccionada.Month, 1)
            Dim ultimoDia As Date = primerDia.AddMonths(1).AddDays(-1)

            ' Para evitar bucles infinitos de eventos, deshabilitamos temporalmente los handlers
            RemoveHandler dtpFechaDesde.ValueChanged, AddressOf dtpFechaDesde_ValueChanged
            RemoveHandler dtpfechaHasta.ValueChanged, AddressOf dtpfechaHasta_ValueChanged

            dtpFechaDesde.Value = primerDia
            dtpfechaHasta.Value = ultimoDia

            ' Volvemos a habilitar los handlers
            AddHandler dtpFechaDesde.ValueChanged, AddressOf dtpFechaDesde_ValueChanged
            AddHandler dtpfechaHasta.ValueChanged, AddressOf dtpfechaHasta_ValueChanged

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub dtpFechaDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDesde.ValueChanged
        Try
            'AjustarFechaInferida(dtpFechaDesde.Value)
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub dtpfechaHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpfechaHasta.ValueChanged
        Try
            AjustarFechaInferida(dtpfechaHasta.Value)
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Cargar_NombreCliente_Impresion()

        '#GT07082024: cargar al propietario
        Dim Propietario_facturar As New clsBePropietarios
        Dim pIdPropietario = clsLnPropietarios.Get_IdPropietario(BePrefacturaEnc.IdBodega, BePrefacturaEnc.IdPropietarioBodega)
        Dim Propietario = clsLnPropietarios.Get_Single_By_IdEmpresa(AP.Empresa.IdEmpresa, pIdPropietario)

        If BePrefacturaEnc.IdClienteBodega > 0 Then
            Dim pIdCliente = clsLnCliente.Get_IdPropietario(pBodega.IdBodega, BePrefacturaEnc.IdClienteBodega)
            Dim Cliente = clsLnCliente.Get_Single_By_IdEmpresa(AP.Empresa.IdEmpresa, pIdCliente)

            If Cliente IsNot Nothing Then
                Propietario_facturar = clsLnPropietarios.GetSingle(Cliente.IdPropietario)
                vClienteFacturar = Propietario.Nombre_comercial & " / " & Propietario_facturar.Nombre_comercial
            End If
        Else
            vClienteFacturar = Propietario.Nombre_comercial
        End If

    End Sub

    Private Sub Cargar_Detalle_Movimientos()

        Try
            dgriDetallePreCuenta.DataSource = Nothing
            DTGridDetallePreCuenta.Clear()
            Dim vNombreProducto As String = ""

            If BePrefacturaEnc.lDetalle_Mov IsNot Nothing Then
                If BePrefacturaEnc.lDetalle_Mov.Count > 0 Then
                    For Each mov As clsBeTrans_prefactura_mov In BePrefacturaEnc.lDetalle_Mov
                        If Not String.IsNullOrEmpty(mov.Codigo_producto) Then
                            Dim vProducto As New clsBeProducto()
                            vProducto = clsLnProducto.Get_BeProducto_By_Codigo(mov.Codigo_producto)
                            If vProducto IsNot Nothing Then
                                vNombreProducto = vProducto.Nombre
                            End If
                        End If

                        DTGridDetallePreCuenta.Rows.Add(mov.IdOrdenCompraEnc,
                                                          mov.Poliza_oc_numero_orden,
                                                          mov.Codigo_producto,
                                                          vNombreProducto,
                                                          mov.Fecha_cobro,
                                                          mov.Unidades,
                                                          mov.Valor_total,
                                                          mov.Almacenaje,
                                                          mov.Manejo)

                    Next

                    dgriDetallePreCuenta.DataSource = DTGridDetallePreCuenta

                    If gvdetalleprecuenta.RowCount > 0 Then

                        gvdetalleprecuenta.OptionsView.ShowFooter = True
                        gvdetalleprecuenta.Columns("numero_orden").Group()

                        gvdetalleprecuenta.Columns("idordencompraenc").Visible = False

                        If chkVarianteCobro.Checked AndAlso chkAgruparPorProducto.Checked Then
                            gvdetalleprecuenta.Columns("codigo_producto").Group()
                        End If

                        Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "almacenaje",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:N4}",
                        .ShowInGroupColumnFooter = gvdetalleprecuenta.Columns("almacenaje")}
                        gvdetalleprecuenta.GroupSummary.Add(item)

                        Dim item_manejo As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "manejo",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:N4}",
                        .ShowInGroupColumnFooter = gvdetalleprecuenta.Columns("manejo")}
                        gvdetalleprecuenta.GroupSummary.Add(item_manejo)


                        gvdetalleprecuenta.Columns("almacenaje").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        gvdetalleprecuenta.Columns("almacenaje").SummaryItem.DisplayFormat = "{0:n4}"
                        gvdetalleprecuenta.Columns("almacenaje").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        gvdetalleprecuenta.Columns("almacenaje").DisplayFormat.FormatString = "{0:n4}"

                        gvdetalleprecuenta.Columns("manejo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        gvdetalleprecuenta.Columns("manejo").SummaryItem.DisplayFormat = "{0:n4}"
                        gvdetalleprecuenta.Columns("manejo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        gvdetalleprecuenta.Columns("manejo").DisplayFormat.FormatString = "{0:n4}"

                        gvdetalleprecuenta.BestFitColumns(True)
                        gvdetalleprecuenta.ExpandAllGroups()
                        gvdetalleprecuenta.OptionsBehavior.Editable = False

                        'xtratabPrecuenta.SelectedTabPageIndex = 1
                        gvdetalleprecuenta.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Cargar_Detalle_Prefactura()
        Try

            Dim vIdAcuerdoEnc As Integer = 0
            Dim vIdAcuerdoDet As Integer = 0

            Dim vCodigoproducto As String = ""
            Dim vCodacuerdo As Integer = 0
            Dim vDescripcion As String = ""
            Dim vCorrelativo As Integer = 0
            Dim vServicio As String = ""
            Dim vNumero_unidades As Integer = 0
            Dim vDias_eventos As Integer = 0
            Dim vMonto As Double = 0.00
            Dim vPorcentaje As Double = 0.00
            Dim vCobro_por_linea As Double = 0.00
            Dim vCobroAlmacenajeDiario As Double = 0.00
            Dim vCobroTotal As Double = 0.00
            Dim vERP As Double = 0.00

            dgridServiciosAsociados.DataSource = Nothing
            DTGridDetalleServicios.Clear()

            If BePrefacturaEnc.lDetalle_Det IsNot Nothing Then
                If BePrefacturaEnc.lDetalle_Det.Count > 0 Then


                    Llena_Rubros_By_CodAcuerdo(BePrefacturaEnc.lDetalle_Det(0).Codigo_acuerdo_enc)

                    ''#GT04062024: validar que es consolidador para mostrar polizas.
                    'If chkConsolidados.Checked Then
                    '    cmbPolizasOC.EditValue = BePrefacturaEnc.IdOrdenCompraEnc
                    '    cmbPolizasPE.EditValue = BePrefacturaEnc.IdOrdenPedidoEnc
                    'End If

                    For Each detalle As clsBeTrans_prefactura_det In BePrefacturaEnc.lDetalle_Det

                        Dim pAcuerdoDet As New clsBeTrans_acuerdoscomerciales_det
                        Dim pAcuerdo As New clsBeTrans_Acuerdoscomerciales_enc
                        pAcuerdo.IdAcuerdoEnc = detalle.IdAcuerdoEnc
                        pAcuerdoDet.IdAcuerdoDet = detalle.IdAcuerdoDet

                        clsLnTrans_acuerdoscomerciales_enc.GetSingle(pAcuerdo)
                        clsLnTrans_acuerdoscomerciales_det.GetSingle(pAcuerdoDet)

                        vIdAcuerdoEnc = detalle.IdAcuerdoEnc
                        vIdAcuerdoDet = detalle.IdAcuerdoDet
                        vCodigoproducto = detalle.Codigo_producto_acuerdo_det
                        vMoneda = pAcuerdo.Moneda
                        vCodacuerdo = detalle.Codigo_acuerdo_enc
                        vCorrelativo = detalle.Correlativo_detalle_acuerdo
                        vDias_eventos = detalle.Dias_eventos
                        vNumero_unidades = detalle.Numero_unidades_acuerdo_det
                        vDescripcion = detalle.Descripcion
                        vServicio = detalle.Servicio
                        vMonto = detalle.Monto
                        vPorcentaje = detalle.Porcentaje
                        'vCobro_por_linea = RedondearMultiplo05(detalle.Valor)
                        vCobro_por_linea = Math.Round(detalle.Valor, 4)
                        vERP = Math.Round(detalle.Monto_Erp, 4)

                        'If pAcuerdoDet.Prioridad > 0 Then

                        '    If vPorcentaje > 0 Then
                        '        vERP = (vCobro_por_linea / vPorcentaje) * 100
                        '    Else
                        '        vERP = (vCobro_por_linea / vMonto) * 1000
                        '    End If
                        'Else
                        '    vERP = 1
                        'End If


                        If detalle.Porcentaje > 0 Then
                            Dim tasa As Double = detalle.Porcentaje / 100
                            vCobroAlmacenajeDiario = Math.Round((((txtValorAduana.EditValue + txtValorGeneral.EditValue) * tasa) / 30), 4)
                            txtAlmacenajeDiario.EditValue = vCobroAlmacenajeDiario
                        End If


                        DTGridDetalleServicios.Rows.Add(vIdAcuerdoEnc,
                                                        vIdAcuerdoDet,
                                                        vCodigoproducto,
                                                        vMoneda,
                                                        vCodacuerdo,
                                                        vDescripcion,
                                                        vCorrelativo,
                                                        vServicio,
                                                        vNumero_unidades,
                                                        vMonto,
                                                        vPorcentaje,
                                                        vCobro_por_linea,
                                                        vERP,
                                                        vDias_eventos)

                        vCobroTotal += vCobro_por_linea

                    Next

                    dgridServiciosAsociados.DataSource = DTGridDetalleServicios
                    'txtTotalFacturacion.EditValue = RedondearMultiplo05(vCobroTotal)
                    txtTotalFacturacion.EditValue = Math.Round(vCobroTotal, 4)

                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            If cmbBodega.EditValue <> 0 Then

                Listar_Propietarios()

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Listar_Propietarios()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnPropietario_bodega.Get_All_Prefactura_By_IdBodega_For_Combo(pBodega.IdBodega)

            cmbPropietario.Properties.DataSource = DT1
            cmbPropietario.Properties.ValueMember = "IdPropietarioBodega"
            cmbPropietario.Properties.DisplayMember = "Nombre"
            cmbPropietario.Properties.PopupFormSize = New Size(900, 200)
            cmbPropietario.Properties.BestFitMode = BestFitMode.BestFit
            cmbPropietario.Properties.NullText = String.Empty
            cmbPropietario.Properties.TextEditStyle = TextEditStyles.Standard
            cmbPropietario.Properties.SearchMode = SearchMode.AutoSuggest
            cmbPropietario.Properties.View.OptionsFind.AlwaysVisible = True
            cmbPropietario.Properties.View.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always
            cmbPropietario.Properties.View.OptionsFind.SearchInPreview = False
            cmbPropietario.Properties.PopupView.OptionsFind.FindFilterColumns = "NIT;Nombre;Codigo"
            cmbPropietario.Properties.PopulateViewColumns()


            If cmbPropietario.Properties.View.Columns.Count > 0 Then
                cmbPropietario.Properties.View.Columns(0).Visible = False
                cmbPropietario.Properties.View.Columns(1).Visible = False
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

    '#GT27082024: lista tambien propietario pero que sirve sino se emite factura al consolidador.
    Private Sub Listar_Clientes()

        Try
            Dim DT1 As New DataTable

            'DT1 = clsLnPropietario_bodega.Get_All_Prefactura_By_IdBodega_For_Combo(pBodega.IdBodega)
            DT1 = clsLnCliente_bodega.Get_All_Prefactura_By_IdBodega_For_Combo(pBodega.IdBodega)

            cmbCliente.Properties.DataSource = DT1
            cmbCliente.Properties.ValueMember = "IdClienteBodega"
            cmbCliente.Properties.DisplayMember = "Nombre"
            cmbCliente.Properties.PopupFormSize = New Size(900, 200)
            cmbCliente.Properties.BestFitMode = BestFitMode.BestFit
            cmbCliente.Properties.NullText = String.Empty
            cmbCliente.Properties.TextEditStyle = TextEditStyles.Standard
            'cmbCliente.Properties.SearchMode = SearchMode.AutoSuggest
            cmbCliente.Properties.View.OptionsFind.AlwaysVisible = True
            cmbCliente.Properties.View.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always
            cmbCliente.Properties.View.OptionsFind.SearchInPreview = False
            cmbCliente.Properties.PopupView.OptionsFind.FindFilterColumns = "NIT;Nombre;Codigo"
            cmbCliente.Properties.PopulateViewColumns()


            'If cmbCliente.Properties.View.Columns.Count > 0 Then
            '    cmbCliente.Properties.View.Columns(0).Visible = False
            '    cmbCliente.Properties.View.Columns(1).Visible = False
            'End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Set_Columnas_Grid_Detalle_Servicios()

        Dim VisibleIndex As Integer = 1

        Try

            dgridServiciosAsociados.DataSource = DTGridDetalleServicios

            gvDetalleServicios.OptionsView.ShowFooter = True
            gvDetalleServicios.OptionsView.ShowGroupPanel = False

            gvDetalleServicios.Columns.Clear()

#Region "Columna - Acuerdo"


            AcuerdosEncGridLookUpEdit.View.Columns.Clear()

            AcuerdosEncGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "codigo_acuerdo", .Caption = "Acuerdo", .Visible = True},
                New GridColumn With {.FieldName = "descripcion", .Caption = "Descripcion", .Visible = True},
                New GridColumn With {.FieldName = "IdAcuerdoEnc", .Caption = "IdAcuerdo", .Visible = False},
                New GridColumn With {.FieldName = "Moneda", .Caption = "Moneda", .Visible = True}
            })


            AcuerdosEncGridLookUpEdit.ValueMember = "codigo_acuerdo"
            AcuerdosEncGridLookUpEdit.DisplayMember = "descripcion"
            AcuerdosEncGridLookUpEdit.NullText = ""
            'AcuerdosEncGridLookUpEdit.BestFitMode = True
            AcuerdosEncGridLookUpEdit.PopupFormSize = New Size(600, 400)


            Select Case Modo

                Case TipoTrans.Nuevo

                    AcuerdosEncGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_enc.Get_All_For_Combo(IdPropietario)

                Case TipoTrans.Editar

                    Dim pPropietario = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(BePrefacturaEnc.IdBodega,
                                                                                                                 BePrefacturaEnc.IdPropietarioBodega)

                    AcuerdosEncGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_enc.Get_All_For_Combo(pPropietario)

            End Select


            RemoveHandler AcuerdosEncGridLookUpEdit.Leave, AddressOf AcuerdosEncGridLookUpEditDetalleServicio_Leave
            AddHandler AcuerdosEncGridLookUpEdit.Leave, AddressOf AcuerdosEncGridLookUpEditDetalleServicio_Leave

            AcuerdosEncGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True


            Dim ColCodigoServicio As New GridColumn With {
                .FieldName = "codigo_acuerdo",
                .Caption = "Acuerdo",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .ColumnEdit = AcuerdosEncGridLookUpEdit
            }

            ColCodigoServicio.Width = 300
            AcuerdosEncGridLookUpEdit.NullText = ""
            ColCodigoServicio.OptionsColumn.AllowEdit = True
            gvDetalleServicios.Columns.Add(ColCodigoServicio)

            VisibleIndex += 1

#End Region

#Region "Columna - Moneda"

            Dim ColMoneda As New GridColumn With {
                .FieldName = "moneda",
                .Caption = "Moneda",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .Width = 200
            }

            ColMoneda.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColMoneda)
            VisibleIndex += 1

#End Region

#Region "Columna Servicio"


            AcuerdosDetalleGridLookUpEdit.View.Columns.Clear()

            AcuerdosDetalleGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "correlativo_detalleacuerdo", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "servicio", .Caption = "servicio", .Visible = True},
                New GridColumn With {.FieldName = "monto", .Caption = "Valor por monto", .Visible = True},
                New GridColumn With {.FieldName = "porcentaje", .Caption = "Valor por %", .Visible = True}
            })

            AcuerdosDetalleGridLookUpEdit.ValueMember = "correlativo_detalleacuerdo"
            AcuerdosDetalleGridLookUpEdit.DisplayMember = "servicio"
            AcuerdosDetalleGridLookUpEdit.NullText = ""
            AcuerdosDetalleGridLookUpEdit.BestFitMode = True



            RemoveHandler AcuerdosDetalleGridLookUpEdit.Leave, AddressOf AcuerdosDetalleGridLookUpEditDetalleServicio_Leave
            AddHandler AcuerdosDetalleGridLookUpEdit.Leave, AddressOf AcuerdosDetalleGridLookUpEditDetalleServicio_Leave

            Dim ColCorrelativo As New GridColumn With {
                .FieldName = "correlativo_detalleacuerdo",
                .Caption = "Servicio",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .ColumnEdit = AcuerdosDetalleGridLookUpEdit
            }

            ColCorrelativo.Width = 300
            AcuerdosDetalleGridLookUpEdit.NullText = ""
            ColCorrelativo.OptionsColumn.AllowEdit = True
            gvDetalleServicios.Columns.Add(ColCorrelativo)

            VisibleIndex += 1

#End Region

#Region "Columna - Codigo Producto"

            Dim ColCodigoProducto As New GridColumn With {
                .FieldName = "codigo_producto",
                .Caption = "codigo producto",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .Width = 150
            }

            ColCodigoProducto.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColCodigoProducto)
            VisibleIndex += 1

#End Region

#Region "Columna monto"

            Dim ColMontoBase As New GridColumn With {
                .FieldName = "monto",
                .Caption = "Monto Base",
                .Width = 125,
                .VisibleIndex = VisibleIndex
            }

            ColMontoBase.DisplayFormat.FormatType = FormatType.Numeric
            ColMontoBase.DisplayFormat.FormatString = "{0:n4}"

            ColMontoBase.Visible = True
            ColMontoBase.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColMontoBase)

            VisibleIndex += 1

#End Region

#Region "Columna Porcentaje"

            Dim ColPorcentaje As New GridColumn With {
                .FieldName = "porcentaje",
                .Caption = "Porcentaje",
                .Width = 125,
                .VisibleIndex = VisibleIndex
            }

            ColPorcentaje.DisplayFormat.FormatType = FormatType.Custom
            ColPorcentaje.DisplayFormat.FormatString = "{0:n4}"

            ColPorcentaje.Visible = True
            ColPorcentaje.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColPorcentaje)

            VisibleIndex += 1

#End Region

#Region "Columna Total"

            Dim ColTotal As New GridColumn With {
                .FieldName = "total",
                .Caption = "Total",
                .Width = 175,
                 .ColumnEdit = txtTotalGrid,
                .VisibleIndex = VisibleIndex
            }


            ColTotal.DisplayFormat.FormatType = FormatType.Numeric
            ColTotal.DisplayFormat.FormatString = "{0:n4}"

            ColTotal.Visible = True
            ColTotal.OptionsColumn.AllowEdit = True
            gvDetalleServicios.Columns.Add(ColTotal)
            VisibleIndex += 1


            gvDetalleServicios.Columns("total").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleServicios.Columns("total").SummaryItem.DisplayFormat = "Total: {0:n4}"
            gvDetalleServicios.Columns("total").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleServicios.Columns("total").DisplayFormat.FormatString = "{0:n4}"


#End Region

#Region "Columna ERP"
            Dim ColERP As New GridColumn With {
                .FieldName = "erp",
                .Caption = "Monto ERP",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .Width = 135
            }

            ColERP.DisplayFormat.FormatType = FormatType.Numeric
            ColERP.DisplayFormat.FormatString = "{0:n4}"

            ColERP.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColERP)
            VisibleIndex += 1
#End Region

#Region "Columna dias_eventos"
            Dim ColDiasEvento As New GridColumn With {
                .FieldName = "dias_eventos",
                .Caption = "Dias",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .Width = 75
            }

            ColDiasEvento.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColDiasEvento)
            VisibleIndex += 1
#End Region

            gvDetalleServicios.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub AcuerdosEncGridLookUpEditDetalleServicio_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaRequisicion As DataRow = gvDetalleServicios.GetFocusedDataRow()
            If drLineaRequisicion Is Nothing Then Return

            Dim UnaOvejota As Object = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView))

            If Not UnaOvejota Is Nothing Then

                Dim drArticulo As DataRow = UnaOvejota.Row
                If drArticulo Is Nothing Then Return

                drLineaRequisicion("IdAcuerdoEnc") = drArticulo("IdAcuerdoEnc")
                drLineaRequisicion("codigo_acuerdo") = drArticulo("codigo_acuerdo")
                drLineaRequisicion("moneda") = drArticulo("Moneda")


                'GT 08022021 se obtiene el IdCliente del combo
                'Llena_Acuerdos_By_IdCliente()

                Dim pListaAcuerdosEnc As New List(Of clsBeTrans_Acuerdoscomerciales_enc)

                pListaAcuerdosEnc = clsLnTrans_acuerdoscomerciales_enc.Get_AcuerdosEnc_And_Detalle_By_IdCliente(IdPropietario, pBodega.IdBodega)

                If pListaAcuerdosEnc IsNot Nothing Then
                    If pListaAcuerdosEnc.Count > 0 Then
                        drLineaRequisicion("codigo_acuerdo") = pListaAcuerdosEnc(0).Codigo_acuerdo
                        '#GT10042024: llenar los rubros por el acuerdo si tuviera.
                        Llena_Rubros_By_CodAcuerdo(pListaAcuerdosEnc(0).Codigo_acuerdo)
                    End If
                End If



            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub AcuerdosDetalleGridLookUpEditDetalleServicio_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim pMonto As Double = 0.00
            Dim pPorcentaje As Double = 0.00
            Dim pPeso As Double = 0.00

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return

            Dim drLineaRequisicion As DataRow = gvDetalleServicios.GetFocusedDataRow()
            If drLineaRequisicion Is Nothing Then Return

            Dim Servicio As Object = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView))

            If Not Servicio Is Nothing Then

                Dim drArticulo As DataRow = Servicio.Row
                If drArticulo Is Nothing Then Return

                'Dim duplicados As Integer = (
                '        From q In DTGridDetalleServicios.Rows
                '        Where (q("servicio").ToString() = drArticulo("servicio").ToString() And q("IdPropietarioBodega") = drLineaRequisicion("IdPropietarioBodega"))
                '        Select q).Count()

                Dim duplicados As Integer = (
                        From q In DTGridDetalleServicios.Rows
                        Where (q("servicio").ToString() = drArticulo("servicio").ToString())
                        Select q).Count()

                If duplicados > 0 Then

                    XtraMessageBox.Show("Servicio duplicado al mismo propietario!",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation)

                Else

                    pMonto = drArticulo("monto")
                    pPorcentaje = drArticulo("porcentaje")

                    drLineaRequisicion("codigo_producto") = drArticulo("codigo_producto")
                    drLineaRequisicion("servicio") = drArticulo("servicio")
                    drLineaRequisicion("monto") = pMonto
                    drLineaRequisicion("porcentaje") = pPorcentaje


                    If pPorcentaje > 0 Then

                        Dim tasa As Double = pPorcentaje / 100
                        Dim vCobroAlmacenajeDiario As Double = 0.00
                        Dim vCobroAlmacenajeTotal As Double = 0.00

                        vCobroAlmacenajeDiario = Math.Round((((txtValorAduana.EditValue + txtValorGeneral.EditValue) * tasa) / 30), 2)
                        vCobroAlmacenajeTotal = Math.Round((vCobroAlmacenajeDiario * txtDiasAlmacenaje.EditValue), 2)
                        drLineaRequisicion("total") = Math.Round(vCobroAlmacenajeTotal, 2)

                        txtAlmacenajeDiario.EditValue = vCobroAlmacenajeDiario
                        gvDetalleServicios.FocusedColumn = gvDetalleServicios.Columns("total")

                    Else

                        pPeso = txtPesoTotal.EditValue
                        If pPeso < 1000 Then
                            drLineaRequisicion("total") = pMonto
                        Else
                            drLineaRequisicion("total") = Math.Round((pPeso / 1000)) * 50
                        End If

                        gvDetalleServicios.FocusedColumn = gvDetalleServicios.Columns("total")

                    End If

                End If


            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub Llena_Acuerdos_By_IdCliente()

        Try

            DTAcuerdos = New DataTable
            DTAcuerdos.Clear()

            DTAcuerdos = clsLnTrans_acuerdoscomerciales_enc.Get_All_For_Combo(IdPropietario)

            If DTAcuerdos IsNot Nothing AndAlso DTAcuerdos.Rows.Count > 0 Then

                AcuerdosEncGridLookUpEdit.DataSource = DTAcuerdos
            Else
                Throw New Exception("No se encontraron servicios al consolidador.")
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Rubros_By_CodAcuerdo(ByVal Codigo_acuerdo As Integer)

        Try

            If String.IsNullOrEmpty(Codigo_acuerdo) Then
                Throw New Exception("Error_10042024_2000: No se pudo obtener el acuerdo para cargar su detalle.")
            Else

                AcuerdosDetalleGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_All_For_Combo(Codigo_acuerdo)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Dim EsIngreso As Boolean

    Private Sub Scan_Poliza(ByVal pNumeroOrden As String, Optional ByVal pIdOrdenCompraEnc As Integer = 0)

        Try

            If EsIngreso Then

                If String.IsNullOrEmpty(pNumeroOrden) Then
                    cmbPolizasOC.Focus()
                    Throw New Exception("No hay póliza de ingreso para validar")
                End If

                BeOCEnc = New clsBeTrans_oc_enc
                Llenar_Datos_Ingreso(pNumeroOrden, pIdOrdenCompraEnc)

            Else

                If cmbPolizasOC.EditValue < 1 Then
                    cmbPolizasOC.Focus()
                    Throw New Exception("No hay póliza de ingreso para validar")
                End If

                If String.IsNullOrEmpty(pNumeroOrden) Then
                    cmbPolizasPE.Focus()
                    Throw New Exception("No hay póliza de salida para validar.")
                End If

                Llenar_Datos_Salida(pNumeroOrden)

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

    Private Sub Llenar_Datos_Salida(ByVal pNumeroOrden As String)
        Try
            Dim barra_poliza As String = ""
            Dim fechainicial As Date
            Dim Fechafinal As Date

            barra_poliza = pNumeroOrden.Replace("-", String.Empty).Trim

            '#GT30052024: limpiar grid si digitan otra poliza de salida.
            dgridServiciosAsociados.DataSource = Nothing
            DTGridDetalleServicios.Clear()

            pPolizaSalidaExiste = clsLnTrans_pe_pol.Get_Single_By_No_Orden(barra_poliza)

            If pPolizaSalidaExiste IsNot Nothing Then

                txtValorAduana.EditValue = pPolizaSalidaExiste.Total_valoraduana
                txtValorGeneral.EditValue = pPolizaSalidaExiste.total_general
                dtFechaSalida.EditValue = pPolizaSalidaExiste.Fecha_abordaje

                txtTipoCambio.EditValue = pPolizaSalidaExiste.Tipo_cambio

                '#GT05062024: validamos fechas sin hora, porque causa diferencia en calculo.
                fechainicial = CDate(dtFechaIngreso.EditValue)
                Fechafinal = CDate(dtFechaSalida.EditValue)
                txtDiasAlmacenaje.EditValue = DateDiff(DateInterval.Day, fechainicial.Date, Fechafinal.Date) + 1
                txtAlmacenajeDiario.Enabled = False
                pPolizaSalida = True
                txtAlmacenajeDiario.EditValue = 0
                txtTotalFacturacion.EditValue = 0

                If chkControlPesoBruto.Checked Then
                    txtPesoTotal.EditValue = pPolizaSalidaExiste.Total_bultos_Peso
                Else
                    txtPesoTotal.EditValue = pPolizaSalidaExiste.Total_bultos_Peso_Neto
                End If


            Else
                XtraMessageBox.Show("No se encontró la póliza de salida en TOMWMS.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Llenar_Datos_Ingreso(ByVal barra_poliza As String, Optional ByVal pIdOrdenCompraEnc As Integer = 0)
        Try

            Dim TMSTicket As New clsBeTms_ticket
            Dim pNumeroOrden As String = ""
            Dim fechainicial As Date
            Dim Fechafinal As Date

            If pBodega.Es_Bodega_Fiscal Then

                pNumeroOrden = barra_poliza.Replace("-", String.Empty).Trim
                '#GT11092024: existe caso donde la misma poliza se maneja en fiscal y posteriormente en general por traslado, buscar la que pertenece a fiscal
                '#GT24102024: si es seleccion de poliza especifica, no se requiere listar todos los ingresos asociados, solamente un registro.
                'Dim pListaPolizas As New List(Of clsBeTrans_oc_pol)
                'pListaPolizas = clsLnTrans_oc_pol.Get_All_By_Numero_Orden(pNumeroOrden)

                Dim pPoliza = clsLnTrans_oc_pol.GetSingle_By_Numero_Orden_And_IdOrdenCompraEnc(pNumeroOrden, pIdOrdenCompraEnc)

                If pPoliza IsNot Nothing Then
                    BeOCEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc_And_IdBodega(pPoliza.IdOrdenCompraEnc, pBodega.IdBodega)
                    If BeOCEnc IsNot Nothing Then
                        pPolizaIngresoExiste = pPoliza
                    End If
                Else
                    pPolizaIngresoExiste = Nothing
                End If


                'If pListaPolizas IsNot Nothing Then

                '    For Each vPoliza In pListaPolizas
                '        BeOCEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc_And_IdBodega(vPoliza.IdOrdenCompraEnc, pBodega.IdBodega)

                '        If BeOCEnc IsNot Nothing Then
                '            pPolizaIngresoExiste = vPoliza
                '            Exit For
                '        End If

                '    Next
                'Else
                '    pPolizaIngresoExiste = Nothing
                'End If

                If pPolizaIngresoExiste Is Nothing Then
                    XtraMessageBox.Show("No se encontró la póliza de ingreso en TOMWMS.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Else
                '#GT11072024: busqueda por IdCompra y referencia.
                'pPolizaIngresoExiste = Nothing
                BeOCEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc_And_Referencia(pIdOrdenCompraEnc, barra_poliza)
            End If

            If BeOCEnc IsNot Nothing Then

                TMSTicket = Nothing

                If Not String.IsNullOrEmpty(BeOCEnc.No_Ticket_TMS) Then

                    If BeOCEnc.No_Ticket_TMS > 0 Then
                        TMSTicket = New clsBeTms_ticket
                        TMSTicket = clsLnTms_ticket.Get_Ticket_By_Id(BeOCEnc.No_Ticket_TMS)
                    End If

                End If

                If TMSTicket IsNot Nothing Then
                    dtFechaIngreso.EditValue = TMSTicket.Fecha_Ingreso
                Else
                    dtFechaIngreso.EditValue = BeOCEnc.Fecha_Creacion
                End If

                '#GT05062024: validamos fechas sin hora, porque causa diferencia en calculo.
                fechainicial = dtFechaIngreso.EditValue
                Fechafinal = dtFechaSalida.EditValue
                txtDiasAlmacenaje.EditValue = DateDiff(DateInterval.Day, fechainicial.Date, Fechafinal.Date) + 1

                '#GT11092024: validaciones redundantes? si hay propietario en combo porque reasignar y recargar?
                '#tampoco hace sentido recagar la bodega!!
                'cmbBodega.EditValue = BeOCEnc.IdBodega
                'cmbBodega.Enabled = False
                'Listar_Propietarios()

                'If BeOCEnc.PropietarioBodega.IdPropietarioBodega > 0 Then
                '    cmbPropietario.EditValue = BeOCEnc.PropietarioBodega.IdPropietarioBodega
                'Else
                '    cmbPropietario.EditValue = BeOCEnc.IdPropietarioBodega
                'End If


                pPolizaIngreso = True

                '#GT18062024: esto parece redundante, al cambiar de propietario se llenan los acuerdos encabezado
                'Llena_Acuerdos_By_IdCliente()
                txtPesoTotal.Focus()

            End If


        Catch ex As Exception
            'SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private Sub txtScanPoliza_KeyDown(sender As Object, e As KeyEventArgs) Handles txtScanPoliza.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtScanPoliza.Text.Trim() <> String.Empty Then
                Scan_Poliza_Salida()
            End If
        End If
    End Sub

    Dim encabezado_duca As New clsBeCEALSA_DUCA_ENC
    Private Sub Scan_Poliza_Salida()

        Try

            encabezado_duca = New clsBeCEALSA_DUCA_ENC
            Dim barra_poliza As String = txtScanPoliza.Text
            Dim pPolizaExiste As New clsBeTrans_oc_pol
            Dim pNumero_OrdenOriginal As String = String.Empty

            If String.IsNullOrEmpty(barra_poliza) Then
                XtraMessageBox.Show("No hay póliza para leer", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                '#GT31082023: por seguridad, guardamos el scan de poliza
                Dim pPoliza = "AVISO: se guarda duca de salida " & txtScanPoliza.Text
                clsLnLog_error_wms.Agregar_Error(pPoliza)
                encabezado_duca.Numero_Orden = barra_poliza.Substring(0, 10)
                encabezado_duca.Numero_DUCA = barra_poliza.Substring(10, 20)
                encabezado_duca.Clave_aduana_despacho_destino = barra_poliza.Substring(38, 7)
                encabezado_duca.NIT_Importador = barra_poliza.Substring(45, 25).Trim()
                'GT 29042021 se convierte a mayuscula el regimen.
                encabezado_duca.Regimen = barra_poliza.Substring(70, 5).ToUpper()
                encabezado_duca.Clase = barra_poliza.Substring(75, 3).Trim()
                encabezado_duca.Pais_procedencia = barra_poliza.Substring(78, 2).ToUpper()
                encabezado_duca.Modo_transporte = barra_poliza.Substring(80, 1)
                encabezado_duca.Tipo_cambio = Convert.ToDouble(barra_poliza.Substring(81, 7))
                encabezado_duca.Total_valor_aduana = Convert.ToDouble(barra_poliza.Substring(88, 16))
                encabezado_duca.Total_bultos_Peso_Bruto = Convert.ToDouble(barra_poliza.Substring(104, 15))
                encabezado_duca.TotalFOBUSD = Convert.ToDouble(barra_poliza.Substring(119, 16))
                encabezado_duca.Total_Flete_USD = Convert.ToDouble(barra_poliza.Substring(135, 15))
                encabezado_duca.Total_Seguro_USD = Convert.ToDouble(barra_poliza.Substring(150, 15))
                encabezado_duca.TotalOtrosgastosUSD = Convert.ToDouble(barra_poliza.Substring(165, 15))
                encabezado_duca.Total_Liquidar = Convert.ToDouble(barra_poliza.Substring(180, 15))
                encabezado_duca.Total_General = Convert.ToDouble(barra_poliza.Substring(195, 15))
                encabezado_duca.Codigo_Poliza = barra_poliza.Substring(210, 9)
                Dim Fecha_string = barra_poliza.Substring(30, 8)
                'concatenación para fecha dd/mm/yyyy
                Dim comodin As String = "/"
                Dim dd As String = String.Empty
                Dim mm As String = String.Empty
                Dim anio As String = String.Empty
                dd = Fecha_string.ToString.Substring(0, 2)
                mm = Fecha_string.ToString.Substring(2, 2)
                anio = Fecha_string.ToString.Substring(4, 4)
                encabezado_duca.Fecha_Aceptacion = dd & comodin & mm & comodin & anio

                'GT 22012021 Set de los inputs en el formulario desde la clase encabezado_duca
                'txtNumeroOrden.Text = encabezado_duca.Numero_Orden

                Dim BeRegimen As New clsBeRegimen_fiscal()
                BeRegimen = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(encabezado_duca.Regimen.Trim)
                If BeRegimen Is Nothing Then
                    txtScanPoliza.Text = String.Empty
                    Throw New Exception("El régimen: " & encabezado_duca.Regimen & " no esta registrado en Régimen Fiscal, o no es legible desde el archivo de importación, re-intente!.")
                End If

                txtTipoCambio.Value = encabezado_duca.Tipo_cambio
                dtFechaSalida.EditValue = encabezado_duca.Fecha_Aceptacion
                txtValorAduana.Value = encabezado_duca.Total_valor_aduana
                txtPesoTotal.Value = encabezado_duca.Total_bultos_Peso_Bruto
                txtValorGeneral.Text = encabezado_duca.Total_General

            End If

            '#GT05092024: no hay poliza de salida, se obtuvo de un scan directo al documento.
            pPolizaSalidaExiste = Nothing

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Function RedondearMultiplo05(valor As Decimal) As Decimal
        ' Redondeamos la parte decimal a múltiplos de 0.05
        Dim parteEntera As Integer = Math.Floor(valor)
        Dim parteDecimal As Decimal = valor - parteEntera

        ' Redondear parte decimal a múltiplos de 0.05
        Dim parteDecimalRedondeada As Decimal = Math.Round(parteDecimal / 0.05D) * 0.05D

        ' Construir el valor redondeado
        Return parteEntera + parteDecimalRedondeada
    End Function

    Private Sub Llenar_grid_cobro_automatico()
        Try

            '#GT17072024: limpiar objetos
            Dim pListaAcuerdosEnc As New List(Of clsBeTrans_Acuerdoscomerciales_enc)
            Dim pAcuerdoComercial As New clsBeTrans_Acuerdoscomerciales_enc
            Dim pMonto As Double = 0.00
            Dim pPorcentaje As Double = 0.00
            Dim pPeso As Double = 0.00
            Dim vCobroAlmacenajeDiario As Double = 0.00
            Dim vCobroAlmacenajeTotal As Double = 0.00
            Dim vErp As Double = 0.00
            vCobroTotal = 0.00
            '#Es_Cobro_Minimo = False
            Dim pMontoDolares As Double = 0.00

            dgridServiciosAsociados.DataSource = Nothing
            DTGridDetalleServicios.Clear()

            '#GT10062024: si tiene varios acuerdos, validar el acuerdo en el doc de ingreso.
            If clsLnTrans_acuerdoscomerciales_enc.Get_All_By_IdPropietario(IdPropietario) > 1 Then

                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("El propietario tiene varios acuerdos comerciales, el sistema no puede determinar cuál se debe aplicar a la póliza.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else

                pAcuerdoComercial = clsLnTrans_acuerdoscomerciales_enc.Get_AcuerdoEnc_And_Detalle_By_IdCliente(IdPropietario, pBodega.IdBodega)

                '#GT03062024: si existe un acuerdo comercial activo iteramos los servicios asociados.
                If pAcuerdoComercial IsNot Nothing Then

                    '#GT10042024: llenar los servicios del acuerdo
                    Llena_Rubros_By_CodAcuerdo(pAcuerdoComercial.Codigo_acuerdo)

                    If pAcuerdoComercial.lDetalle IsNot Nothing Then
                        '#GT24042024: buscar si existe rubro con prioridad 1
                        Dim listAcuerdosPrioritarios As New List(Of clsBeTrans_acuerdoscomerciales_det)
                        listAcuerdosPrioritarios = pAcuerdoComercial.lDetalle.Where(Function(x) x.Prioridad = 1).ToList()

                        If listAcuerdosPrioritarios IsNot Nothing Then

                            If listAcuerdosPrioritarios.Count > 0 Then

                                For Each pAcuerdoPrimario As clsBeTrans_acuerdoscomerciales_det In listAcuerdosPrioritarios

                                    '**************************************************************************************************
                                    '#GT23052024: Almacenaje para consolidadores primero validar cobro por porcentaje
                                    If pAcuerdoPrimario.Porcentaje > 0.00 AndAlso pAcuerdoPrimario.Monto = 0.00 Then

                                        SplashScreenManager.Default.SetWaitFormDescription("Validando acuerdo prioridad 1...")

                                        Dim tasa As Double = pAcuerdoPrimario.Porcentaje / 100

                                        Dim tmpCobroAlmacenaje = Math.Round(txtValorAduana.EditValue + txtValorGeneral.EditValue, 4)
                                        Dim tmpCobroAlmacenaje2 = Math.Round(tmpCobroAlmacenaje * tasa, 4)
                                        'vCobroAlmacenajeDiario = Math.Round((((txtValorAduana.EditValue + txtValorGeneral.EditValue) * tasa) / 30), 4)
                                        vCobroAlmacenajeDiario = Math.Round(tmpCobroAlmacenaje2 / 30, 2)
                                        txtAlmacenajeDiario.EditValue = vCobroAlmacenajeDiario
                                        vCobroAlmacenajeTotal = (vCobroAlmacenajeDiario * txtDiasAlmacenaje.EditValue)


                                        vIdAcuerdoEnc = pAcuerdoPrimario.IdAcuerdoEnc
                                        vIdAcuerdoDet = pAcuerdoPrimario.IdAcuerdoDet
                                        vCodigoproducto = pAcuerdoPrimario.Codigo_producto
                                        vCodacuerdo = pAcuerdoPrimario.Codigo_acuerdo
                                        vDescripcion = pAcuerdoPrimario.Descripcion
                                        vCorrelativo = pAcuerdoPrimario.Correlativo_detalleacuerdo
                                        vDias_eventos = pAcuerdoPrimario.Dias_eventos
                                        vNumero_unidades = pAcuerdoPrimario.Numero_unidades
                                        vServicio = pAcuerdoPrimario.Servicio
                                        vMonto = pAcuerdoPrimario.Monto
                                        vPorcentaje = pAcuerdoPrimario.Porcentaje
                                        vCobro_por_linea = vCobroAlmacenajeTotal
                                        vMoneda = pAcuerdoComercial.Moneda


                                        ''#GT12082024: si el acuerdo esta en dolares y queremos enviar dolares al ERP
                                        If pAcuerdoComercial.Moneda = "DOLAR AMERICANO" Then
                                            If chkMantenerDolares.Checked Then
                                                If String.IsNullOrEmpty(txtTipoCambio.EditValue) Then
                                                    SplashScreenManager.CloseForm(False)
                                                    Throw New Exception("El acuerdo comercial en dolares requiere un tipo de cambio.")
                                                End If

                                                Dim tmpTipoCambio = Math.Round(txtTipoCambio.EditValue, 4)

                                                If tmpTipoCambio = 0 Then
                                                    SplashScreenManager.CloseForm(False)
                                                    Throw New Exception("El acuerdo comercial en dolares requiere un tipo de cambio superior a 0.")
                                                End If

                                                vCobro_por_linea = Math.Round(vCobro_por_linea / tmpTipoCambio, 2)

                                            End If
                                        End If

                                        If pAcuerdoPrimario.Prioridad > 0 Then

                                            If vPorcentaje > 0 Then
                                                vErp = RedondearMultiplo05((vCobro_por_linea / vPorcentaje) * 100)
                                            Else
                                                vErp = RedondearMultiplo05((vCobro_por_linea / vMonto) * 1000)
                                            End If
                                        Else
                                            vErp = 1
                                        End If

                                        '#GT24042024: validar si se llega a monto minimo, o usar el acuerdo con prioridad 2 que tiene el monto minimo a cobrar
                                        Dim pAcuerdoSecundario As New clsBeTrans_acuerdoscomerciales_det
                                        pAcuerdoSecundario = pAcuerdoComercial.lDetalle.Find(Function(x) x.Prioridad = 2 AndAlso
                                                                                                       x.Codigo_producto = pAcuerdoPrimario.Codigo_producto AndAlso
                                                                                                       x.Correlativo_detalleacuerdo <> pAcuerdoPrimario.Correlativo_detalleacuerdo)

                                        If pAcuerdoSecundario IsNot Nothing Then

                                            Dim tmpMontoMinimo As Double = 0.00
                                            If pAcuerdoComercial.Moneda = "DOLAR AMERICANO" Then
                                                tmpMontoMinimo = Math.Round(pAcuerdoSecundario.Monto * txtTipoCambio.EditValue, 4)
                                            Else
                                                tmpMontoMinimo = Math.Round(pAcuerdoSecundario.Monto, 4)
                                            End If

                                            '#GT24092024: validar monto minimo sea en Q o USD
                                            'If vCobroAlmacenajeTotal < pAcuerdoSecundario.Monto Then
                                            If vCobroAlmacenajeTotal < tmpMontoMinimo Then

                                                SplashScreenManager.Default.SetWaitFormDescription("Validando acuerdo prioridad 2...")

                                                vIdAcuerdoEnc = pAcuerdoSecundario.IdAcuerdoEnc
                                                vIdAcuerdoDet = pAcuerdoSecundario.IdAcuerdoDet
                                                vCodigoproducto = pAcuerdoSecundario.Codigo_producto
                                                vCodacuerdo = pAcuerdoSecundario.Codigo_acuerdo
                                                vDescripcion = pAcuerdoSecundario.Descripcion
                                                vCorrelativo = pAcuerdoSecundario.Correlativo_detalleacuerdo
                                                vServicio = pAcuerdoSecundario.Servicio
                                                vDias_eventos = pAcuerdoSecundario.Dias_eventos
                                                vNumero_unidades = pAcuerdoSecundario.Numero_unidades
                                                vMonto = pAcuerdoSecundario.Monto
                                                'vMonto = tmpMontoMinimo
                                                vPorcentaje = pAcuerdoSecundario.Porcentaje
                                                vCobro_por_linea = pAcuerdoSecundario.Monto
                                                'vCobro_por_linea = tmpMontoMinimo

                                                If pAcuerdoComercial.Moneda = "DOLAR AMERICANO" Then

                                                    Dim tmpTipoCambio = Math.Round(txtTipoCambio.EditValue, 4)
                                                    If String.IsNullOrEmpty(txtTipoCambio.EditValue) Then
                                                        SplashScreenManager.CloseForm(False)
                                                        Throw New Exception("El acuerdo comercial en dolares requiere un tipo de cambio.")
                                                    End If

                                                    If tmpTipoCambio = 0 Then
                                                        SplashScreenManager.CloseForm(False)
                                                        Throw New Exception("El acuerdo comercial en dolares requiere un tipo de cambio superior a 0.")
                                                    End If


                                                    If chkMantenerDolares.Checked Then
                                                    Else
                                                        vCobro_por_linea = Math.Round(vCobro_por_linea * tmpTipoCambio, 2)
                                                    End If
                                                End If


                                                '#GT17072024: validar que valor se enviará al ERP
                                                If pAcuerdoSecundario.Prioridad > 0 Then

                                                    If vPorcentaje > 0 Then
                                                        vErp = RedondearMultiplo05((vCobro_por_linea / vPorcentaje) * 100)
                                                    Else
                                                        vErp = 1
                                                    End If
                                                Else
                                                    vErp = 1
                                                End If

                                                '#Es_Cobro_Minimo = True

                                            End If
                                        End If

                                        '**************************************************************************************************
                                        '#GT24042024: calculo cobro por manejo
                                        '#GT02052024: los valores se obtienen del acuerdo
                                    ElseIf pAcuerdoPrimario.Prioridad = 1 And pAcuerdoPrimario.Monto > 0.00 AndAlso pAcuerdoPrimario.Porcentaje = 0.00 Then

                                        SplashScreenManager.Default.SetWaitFormDescription("Validando manejo...")

                                        vIdAcuerdoEnc = pAcuerdoPrimario.IdAcuerdoEnc
                                        vIdAcuerdoDet = pAcuerdoPrimario.IdAcuerdoDet
                                        vCodigoproducto = pAcuerdoPrimario.Codigo_producto
                                        vCodacuerdo = pAcuerdoPrimario.Codigo_acuerdo
                                        vDescripcion = pAcuerdoPrimario.Descripcion
                                        vCorrelativo = pAcuerdoPrimario.Correlativo_detalleacuerdo
                                        vServicio = pAcuerdoPrimario.Servicio
                                        vDias_eventos = pAcuerdoPrimario.Dias_eventos
                                        vNumero_unidades = pAcuerdoPrimario.Numero_unidades
                                        vMonto = pAcuerdoPrimario.Monto
                                        vPorcentaje = pAcuerdoPrimario.Porcentaje
                                        pPeso = Math.Round(CDbl(txtPesoTotal.EditValue), 4)
                                        vMoneda = pAcuerdoComercial.Moneda

                                        '#GT23072024: validar si aplica conversion dolares a quetzales.
                                        If pAcuerdoComercial.Moneda = "DOLAR AMERICANO" Then
                                            pMontoDolares = Math.Round(vMonto, 2)

                                            '#GT23052024: si peso es menor a lo indicado por numero unidades, cobrar monto del acuerdo
                                            If pPeso < pAcuerdoPrimario.Numero_unidades Then
                                                vCobro_por_linea = pMontoDolares
                                            Else
                                                vCobro_por_linea = RedondearMultiplo05((Math.Round(pPeso / vNumero_unidades, 4)) * pMontoDolares)
                                            End If

                                            If Not chkMantenerDolares.Checked Then
                                                vCobro_por_linea = Math.Round(vCobro_por_linea * txtTipoCambio.EditValue, 2)
                                            End If

                                        Else

                                            '#GT23052024: si peso es menor a lo indicado por numero unidades, cobrar monto del acuerdo
                                            If pPeso < pAcuerdoPrimario.Numero_unidades Then
                                                vCobro_por_linea = vMonto
                                            Else
                                                vCobro_por_linea = RedondearMultiplo05((Math.Round(pPeso / vNumero_unidades, 5)) * vMonto)
                                            End If

                                        End If



                                        If pAcuerdoPrimario.Prioridad > 0 Then

                                            If vPorcentaje > 0 Then
                                                vErp = RedondearMultiplo05((vCobro_por_linea / vPorcentaje) * 100)
                                            Else
                                                vErp = RedondearMultiplo05((vCobro_por_linea / vMonto) * 1000)
                                            End If
                                        Else
                                            vErp = 1
                                        End If


                                    End If

                                    vCobro_por_linea = Math.Round(vCobro_por_linea, 2)
                                    vErp = Math.Round(vErp, 2)

                                    DTGridDetalleServicios.Rows.Add(vIdAcuerdoEnc,
                                                                vIdAcuerdoDet,
                                                                vCodigoproducto,
                                                                vMoneda,
                                                                vCodacuerdo,
                                                                vDescripcion,
                                                                vCorrelativo,
                                                                vServicio,
                                                                vNumero_unidades,
                                                                vMonto,
                                                                vPorcentaje,
                                                                vCobro_por_linea,
                                                                vErp,
                                                                vDias_eventos)


                                    vCobroTotal += vCobro_por_linea


                                Next

                                txtTotalFacturacion.EditValue = RedondearMultiplo05(vCobroTotal)
                                dgridServiciosAsociados.DataSource = DTGridDetalleServicios

                                pCalculoProcesado = True

                            End If

                        End If



                        SplashScreenManager.Default.SetWaitFormDescription("Validando servicios en ingresos...")
                        '#GT03062024:validamos si existen servicios asociados a la póliza de ingreso
                        Dim pListaServiciosOC As New List(Of clsBeTrans_oc_servicios)
                        '#GT17062024:validar si la poliza existe en mas de un doc. de ingreso
                        Dim pListaIngresos = clsLnTrans_oc_pol.Get_All_By_Numero_Orden(pPolizaIngresoExiste.numero_orden)

                        If pListaIngresos IsNot Nothing Then

                            For Each ingreso In pListaIngresos
                                pListaServiciosOC = clsLnTrans_oc_servicios.Get_All_By_IdOrdenCompraEnc(ingreso.IdOrdenCompraEnc, pAcuerdoComercial.IdAcuerdoEnc)

                                '#GT17062024: por cada OC iteramos sus servicios y los agregamos al grid.
                                If pListaServiciosOC IsNot Nothing Then

                                    If pListaServiciosOC.Count > 0 Then

                                        vCobro_por_linea = 0

                                        For Each ServicioOC In pListaServiciosOC

                                            Dim pServicio = pAcuerdoComercial.lDetalle.Find(Function(x) x.IdAcuerdoEnc = ServicioOC.IdAcuerdo AndAlso
                                                                                                 x.Correlativo_detalleacuerdo = ServicioOC.Corre_detalleacuerdo)

                                            If pServicio IsNot Nothing Then

                                                vCobro_por_linea = pServicio.Monto

                                                'vErp = RedondearMultiplo05((vCobro_por_linea / vMonto) * 1000)
                                                vErp = 1 '#GT02072024: cuando son servicios, monto es 1, y dias es 1

                                                DTGridDetalleServicios.Rows.Add(pServicio.IdAcuerdoEnc,
                                                                       pServicio.IdAcuerdoDet,
                                                                       pServicio.Codigo_producto,
                                                                       vMoneda,
                                                                       pServicio.Codigo_acuerdo,
                                                                       pServicio.Descripcion,
                                                                       pServicio.Correlativo_detalleacuerdo,
                                                                       pServicio.Servicio,
                                                                       pServicio.Numero_unidades,
                                                                       pServicio.Monto,
                                                                       pServicio.Porcentaje,
                                                                       vCobro_por_linea,
                                                                       vErp,
                                                                       pServicio.Dias_eventos)

                                                vCobroTotal += vCobro_por_linea

                                            End If

                                        Next

                                        txtTotalFacturacion.EditValue = RedondearMultiplo05(vCobroTotal)

                                    End If

                                End If


                            Next

                        End If


                        '#GT04062024:validamos si existen servicios asociados a la póliza de salida, sino es cotización, ya que la poliza aun no esta registrada.
                        If Not chkEstimacionCobro.Checked Then
                            Dim pListaServiciosPE As New List(Of clsBeTrans_pe_servicios)
                            SplashScreenManager.Default.SetWaitFormDescription("Validando servicios en salidas...")

                            If pPolizaSalidaExiste IsNot Nothing Then
                                '#GT17062024:validar si la poliza existe en mas de un pedido
                                Dim pListaPedidos = clsLnTrans_pe_pol.Get_All_By_Numero_Orden(pPolizaSalidaExiste.numero_orden)

                                If pListaPedidos IsNot Nothing Then

                                    For Each salida In pListaPedidos

                                        pListaServiciosPE = clsLnTrans_pe_servicios.Get_All_By_IdOrdenPedidoEnc(salida.IdOrdenPedidoEnc, pAcuerdoComercial.IdAcuerdoEnc)

                                        If pListaServiciosPE IsNot Nothing Then
                                            If pListaServiciosPE.Count > 0 Then

                                                vCobro_por_linea = 0

                                                For Each ServicioPE In pListaServiciosPE
                                                    Dim pServicio = pAcuerdoComercial.lDetalle.Find(Function(x) x.IdAcuerdoEnc = ServicioPE.IdAcuerdo AndAlso
                                                                                                         x.Correlativo_detalleacuerdo = ServicioPE.Corre_detalleacuerdo)

                                                    If pServicio IsNot Nothing Then

                                                        vCobro_por_linea = pServicio.Monto

                                                        'vErp = RedondearMultiplo05((vCobro_por_linea / vMonto) * 1000)
                                                        vErp = 1 '#GT02072024: cuando son servicios, monto es 1, y dias es 1

                                                        DTGridDetalleServicios.Rows.Add(pServicio.IdAcuerdoEnc,
                                                                               pServicio.IdAcuerdoDet,
                                                                               pServicio.Codigo_producto,
                                                                               vMoneda,
                                                                               pServicio.Codigo_acuerdo,
                                                                               pServicio.Descripcion,
                                                                               pServicio.Correlativo_detalleacuerdo,
                                                                               pServicio.Servicio,
                                                                               pServicio.Numero_unidades,
                                                                               pServicio.Monto,
                                                                               pServicio.Porcentaje,
                                                                               vCobro_por_linea,
                                                                               vErp,
                                                                               pServicio.Dias_eventos)

                                                        vCobroTotal += vCobro_por_linea

                                                    End If

                                                Next

                                                txtTotalFacturacion.EditValue = RedondearMultiplo05(vCobroTotal)

                                            End If
                                        End If

                                    Next


                                End If
                            End If

                        End If



                    Else
                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("No se encontraron servicios para cálcular cobro!.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                Else
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("No se encontró un acuerdo comercial asociado!.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub gvDetalleServicios_InvalidRowException(sender As Object, e As InvalidRowExceptionEventArgs) Handles gvDetalleServicios.InvalidRowException

        Try

            e.ExceptionMode = ExceptionMode.NoAction

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub gvDetalleServicios_ValidateRow(sender As Object, e As ValidateRowEventArgs) Handles gvDetalleServicios.ValidateRow
        Try

            Grid_Tiene_Error = False

            Dim View As GridView = CType(sender, GridView)
            Dim colIdPropietarioBodega As GridColumn = View.Columns("IdPropietarioBodega")
            Dim colCodigoProducto As GridColumn = View.Columns("codigoproducto")
            Dim colCodAcuerdo As GridColumn = View.Columns("codacuerdo")
            Dim colNombreAcuerdo As GridColumn = View.Columns("nombre_acuerdo")
            Dim colCorrelativoAcuerdo As GridColumn = View.Columns("correlativo")
            Dim colServicio As GridColumn = View.Columns("servicio")
            Dim colMonto As GridColumn = View.Columns("monto")
            Dim colPorcentaje As GridColumn = View.Columns("porcentaje")
            Dim colTotal As GridColumn = View.Columns("total")

            e.Valid = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub dtFechaSalida_EditValueChanged(sender As Object, e As EventArgs) Handles dtFechaSalida.EditValueChanged
        Try

            '#GT26062024: obtenemos fecha sin hora, y permitimos editar hora salida
            Dim fechainicial = dtFechaIngreso.EditValue
            Dim fechafinal = dtFechaSalida.EditValue

            txtDiasAlmacenaje.EditValue = DateDiff(DateInterval.Day, fechainicial.date, fechafinal.date) + 1

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbCliente.KeyDown

        Try

            If e.KeyCode = Keys.Back Then
                cmbCliente.EditValue = 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub txtPesoTotal_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPesoTotal.KeyDown

        If e.KeyCode = Keys.Enter Then

            If txtPesoTotal.EditValue = 0 Then
                XtraMessageBox.Show("Aviso: El peso se estimará en 0 para calculo de manejo.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'txtScanPolizaSalida.Focus()
            Else
                'txtScanPolizaSalida.Focus()
            End If

        End If

    End Sub

    Dim pBuscarPolizaEspecifica As Boolean
    Dim pBuscarProductoEspecifico As Boolean
    Dim pDecimalesEnCobro As Integer
    Private Sub cmdProcesar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdProcesar.ItemClick

        Try

            cmdProcesar.Enabled = False

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cálculando cobro")
            SplashScreenManager.Default.SetWaitFormDescription("Validando parametros...")

            txtTotalFacturacion.EditValue = 0
            pDecimalesEnCobro = 0

            'GT02012025: Aqui calcula no consolidados (Luis)
            If chkConsolidados.Checked Then

                If txtPesoTotal.EditValue = 0 Then

                    SplashScreenManager.CloseForm(False)
                    If XtraMessageBox.Show("No se ha registrado un peso, desea procesar de todas formas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormDescription("Cálculando cobro...")

                        Llenar_grid_cobro_automatico()
                        Bloquear_grid_acuerdos()

                    Else
                        txtPesoTotal.Focus()
                    End If

                Else

                    Llenar_grid_cobro_automatico()
                    Bloquear_grid_acuerdos()

                End If

            Else
                'GT02012025: Aqui calcula consolidado (Don Alberto)
                If cmbPropietario.EditValue <> 0 Then
                    calcular_movimientos_precuenta()
                Else
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Aviso: debe seleccionar un propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If

            pPolizaIngreso = False
            pPolizaSalida = False
            cmdProcesar.Enabled = True

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            cmdProcesar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Bloquear_grid_acuerdos()
        Try

            gvDetalleServicios.OptionsBehavior.Editable = True
            gvDetalleServicios.Columns("codigo_acuerdo").OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns("correlativo_detalleacuerdo").OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns("monto").OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns("porcentaje").OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns("total").OptionsColumn.AllowEdit = True

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub calcular_movimientos_precuenta()
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Obteniendo registros...")

            pCalculoProcesado = False
            Dim pMonto As Double = 0.00
            Dim pPorcentaje As Double = 0.00
            Dim pPeso As Double = 0.00
            Dim vCobroAlmacenajeDiario As Double = 0.00
            Dim vCobroAlmacenajeTotal As Double = 0.00
            Dim pCalculomanejo As Double = 0.00
            Dim pIntegracionCalculoManejo As Double = 0.00
            Dim pIntegracionCalculoAlmacenaje As Double = 0.00
            Dim vErp As Double = 0.00
            Dim pFechaDesde As Date = Now
            Dim pFechaHasta As Date = Now
            Dim pBeProducto As New clsBeProducto()
            Dim vNombreProducto As String = ""

            'GT20112024: manejo de la moneda de cobro
            Dim vMoneda As String = ""
            Dim vUsarMonedaDolar As Boolean = False
            Dim vTipoCambioEnQuetzales As Double = 0.00


            '#GT06052024: time parametrizable por la densidad de datos en el reporte histórico.
            Dim pTimeOut = clsBD.Instancia.TimeOutConBD
            Dim pBodega = clsLnBodega.GetSingle_By_Idbodega(AP.IdBodega)
            Dim DTListaIngresos As New DataTable
            Dim pServicioConAcuerdo As New clsBeTrans_oc_servicios
            Dim pListaServiciosOC As New List(Of clsBeTrans_oc_servicios)
            Dim pAcuerdoComercial As New clsBeTrans_Acuerdoscomerciales_enc
            Dim pListaServiciosPE As New List(Of clsBeTrans_pe_servicios)

            '#GT27062024: lista de polizas para validar que no existan duplicadas
            Dim lPolizasOC As New List(Of clsBeTrans_oc_pol)
            Dim ListaTranOcEncReferencias As New List(Of clsBeTrans_oc_enc)
            Dim listPolizas As New List(Of String)
            Dim listReferencias As New List(Of String)
            Dim pPolizaManejo As New clsBeTrans_oc_pol
            Dim pOCManejo As New clsBeTrans_oc_enc
            Dim pTransOcEncReferencia As New clsBeTrans_oc_enc
            Dim pListaProductoConVariante As New List(Of clsBeProducto)

            DTGridDetallePreCuenta.Clear()
            dgriDetallePreCuenta.DataSource = Nothing
            dgridServiciosAsociados.DataSource = Nothing
            DTGridDetalleServicios.Clear()

            pServicioConAcuerdo = Nothing
            pAcuerdoComercial = Nothing
            pFechaDesde = dtpFechaDesde.Value
            pFechaHasta = dtpfechaHasta.Value
            vCobro_por_linea = 0.00

            pDecimalesEnCobro = txtDecimales.Value

            '#GT04102024: Log de ingresos sin histórico.
            lblPrg.Text = ""
            lblPrg.Refresh()
            lblPrg.AppendText(vbNewLine)
            lblPrg.AppendText("Inicia proceso..." & Now)
            lblPrg.AppendText(vbNewLine)
            lblPrg.Refresh()
            lblPrg.SelectionStart = lblPrg.TextLength
            lblPrg.ScrollToCaret()
            lblPrg.AppendText(vbNewLine)

            Dim IngresoSinHistorico As New List(Of Integer)
            pTrans_oc_ti = New clsBeTrans_oc_ti

            '#GT20112024: obtener un valor de tipo de cambio, podria ser 0
            vTipoCambioEnQuetzales = txtTipoCambio.EditValue

            '#GT31072024: consultar datos y validar si incluimos producto
            If pBuscarPolizaEspecifica Then

                If pBodega.Es_Bodega_Fiscal Then

                    '#GT21012025: mejora para buscar por idordencompraEnc y no solo la referencia.
                    DTListaIngresos = clsLnReportesFiscales.Get_Fiscal_Facturacion_By_IdOrdenCompra(dtpFechaDesde.Value.Date,
                                                                                                    dtpfechaHasta.Value.Date,
                                                                                                    pBodega.Es_Bodega_Fiscal,
                                                                                                    IdPropietario,
                                                                                                    chkAgruparPorProducto.Checked,
                                                                                                    BeOCEnc.IdOrdenCompraEnc,
                                                                                                    BeOCEnc.Referencia,
                                                                                                    pTimeOut)


                    '#GT23102024: Identificar si el ingreso es por devolucion
                    Dim tmpOC = clsLnTrans_oc_enc.GetSingle(pPolizaIngresoExiste.IdOrdenCompraEnc)
                    pTrans_oc_ti = clsLnTrans_oc_ti.GetSingle(tmpOC.IdTipoIngresoOC)

                Else

                    '#GT21012025: mejora para buscar por idordencompraEnc y no solo la referencia.
                    DTListaIngresos = clsLnReportesFiscales.Get_Fiscal_Facturacion_By_IdOrdenCompra(dtpFechaDesde.Value.Date,
                                                                                                    dtpfechaHasta.Value.Date,
                                                                                                    pBodega.Es_Bodega_Fiscal,
                                                                                                    IdPropietario,
                                                                                                    chkAgruparPorProducto.Checked,
                                                                                                    BeOCEnc.IdOrdenCompraEnc,
                                                                                                    BeOCEnc.Referencia,
                                                                                                    pTimeOut)




                    pTrans_oc_ti = clsLnTrans_oc_ti.GetSingle(BeOCEnc.IdTipoIngresoOC)

                End If
            Else

                If Es_Seleccion_Multiple Then

                    DTListaIngresos = Nothing

                    For Each OC In ListaSeleccionMultiple

                        Dim dt As New DataTable
                        dt = clsLnReportesFiscales.Get_Fiscal_Facturacion_By_IdOrdenCompra(dtpFechaDesde.Value.Date,
                                                                                                    dtpfechaHasta.Value.Date,
                                                                                                    pBodega.Es_Bodega_Fiscal,
                                                                                                    IdPropietario,
                                                                                                    chkAgruparPorProducto.Checked,
                                                                                                    OC.IdOrdenCompraEnc,
                                                                                                    OC.Referencia,
                                                                                                    pTimeOut)

                        If dt IsNot Nothing Then
                            If DTListaIngresos Is Nothing Then
                                DTListaIngresos = dt.Copy()
                            Else
                                For Each row As DataRow In dt.Rows
                                    ' Crea una nueva fila en dtDestino con los datos de dtOriginal
                                    Dim newRow As DataRow = DTListaIngresos.NewRow()
                                    newRow.ItemArray = row.ItemArray.Clone() ' Copia todos los datos de la fila
                                    DTListaIngresos.Rows.Add(newRow) ' Agrega la nueva fila a dtDestino
                                Next
                            End If
                        End If

                    Next

                Else

                    '#GT21012025: mejora para buscar por idordencompraEnc y no solo la referencia.
                    DTListaIngresos = clsLnReportesFiscales.Get_Fiscal_Facturacion_By_IdOrdenCompra(dtpFechaDesde.Value.Date,
                                                                                                    dtpfechaHasta.Value.Date,
                                                                                                    pBodega.Es_Bodega_Fiscal,
                                                                                                    IdPropietario,
                                                                                                    chkAgruparPorProducto.Checked,
                                                                                                    0,
                                                                                                    "",
                                                                                                    pTimeOut)

                End If


            End If


            If DTListaIngresos Is Nothing OrElse DTListaIngresos.Rows.Count = 0 Then
                SplashScreenManager.CloseForm(False)
                Throw New Exception("La consulta no ha retornado ningún registro!")
            End If


            '#GT31072024: inicia proceso de cálculo si usamos poliza con producto o no
            If pBuscarPolizaEspecifica Then

                SplashScreenManager.Default.SetWaitFormDescription("Validando poliza seleccionada...")

                '#GT20062024: propietario con muchos acuerdos y ninguno asociado a la póliza.
                If DTAcuerdos.Rows.Count > 1 AndAlso pAcuerdoComercial Is Nothing Then
                    SplashScreenManager.CloseForm(False)
                    Throw New Exception("Error: El propietario tiene varios acuerdos comerciales activos, el sistema no puede inferir cuál utilizar.")
                End If

                If DTAcuerdos.Rows.Count = 1 Then

                    If pAcuerdoComercial Is Nothing Then
                        pAcuerdoComercial = clsLnTrans_acuerdoscomerciales_enc.Get_AcuerdoEnc_And_Detalle_By_IdCliente(IdPropietario, pBodega.IdBodega)
                    End If

                    If pAcuerdoComercial IsNot Nothing Then

                        Llena_Rubros_By_CodAcuerdo(pAcuerdoComercial.Codigo_acuerdo)

                        If pAcuerdoComercial.lDetalle IsNot Nothing Then

                            Dim listAcuerdosPrioritarios As New List(Of clsBeTrans_acuerdoscomerciales_det)
                            listAcuerdosPrioritarios = pAcuerdoComercial.lDetalle.Where(Function(x) x.Prioridad = 1 AndAlso x.IdBodega = pBodega.IdBodega).ToList()

                            If listAcuerdosPrioritarios IsNot Nothing Then

                                If listAcuerdosPrioritarios.Count > 0 Then

                                    For Each pAcuerdoPrimario As clsBeTrans_acuerdoscomerciales_det In listAcuerdosPrioritarios

                                        '#GT26062024: Llenar el acuerdo comercial encabezado
                                        vIdAcuerdoEnc = pAcuerdoPrimario.IdAcuerdoEnc
                                        vIdAcuerdoDet = pAcuerdoPrimario.IdAcuerdoDet
                                        vCodigoproducto = pAcuerdoPrimario.Codigo_producto
                                        vCodacuerdo = pAcuerdoPrimario.Codigo_acuerdo
                                        vDescripcion = pAcuerdoPrimario.Descripcion
                                        vCorrelativo = pAcuerdoPrimario.Correlativo_detalleacuerdo
                                        vDias_eventos = pAcuerdoPrimario.Dias_eventos
                                        vNumero_unidades = pAcuerdoPrimario.Numero_unidades
                                        vServicio = pAcuerdoPrimario.Servicio
                                        vMonto = pAcuerdoPrimario.Monto
                                        vPorcentaje = pAcuerdoPrimario.Porcentaje
                                        vMoneda = pAcuerdoComercial.Moneda
                                        pListaProductoConVariante = New List(Of clsBeProducto)

                                        '#GT16102025: cargar la oc y pe y determinar si es transferencia, para no cobrar el primer día obtenido de la lista.
                                        Dim BeOrdenCompraEnc As New clsBeTrans_oc_enc()
                                        Dim BePedidoEnc As New clsBeTrans_pe_enc()
                                        Dim pPosicionesOcupadas As Integer = 0

                                        '#GT20112024: validar si requerimos tipo cambio antes de realizar cualquier cálculo.
                                        If clsDataContractDI.tTipoMonedaPrefacturacion.Dolar = pAcuerdoComercial.Cod_moneda Then
                                            If vTipoCambioEnQuetzales = 0 Then
                                                SplashScreenManager.CloseForm(False)
                                                txtTipoCambio.Focus()
                                                Throw New Exception("El acuerdo comercial en dolares requiere, tipo de cambio superior a 0.")
                                            Else
                                                vUsarMonedaDolar = True
                                            End If
                                        Else
                                            vUsarMonedaDolar = False
                                        End If

                                        '#****************************************************************************
                                        '#GT20062024: validar cobro almacenaje por unidad (tipo 1 con posibles variantes)
                                        If pAcuerdoPrimario.Prioridad > 0 AndAlso pAcuerdoPrimario.IdTipoCobro = 1 AndAlso pAcuerdoPrimario.Monto > 0 Then

                                            SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por unidad...")

                                            vCobroAlmacenajeDiario = 0
                                            BeOrdenCompraEnc = New clsBeTrans_oc_enc()
                                            BePedidoEnc = New clsBeTrans_pe_enc()

                                            If DTListaIngresos IsNot Nothing AndAlso DTListaIngresos.Rows.Count > 0 Then

                                                pBeProducto = Nothing
                                                'BeOrdenCompraEnc = Nothing

                                                For i As Integer = 0 To DTListaIngresos.Rows.Count - 1
                                                    Dim vNumero_orden = DTListaIngresos.Rows(i)(0).ToString()
                                                    Dim vIdOrdenCompraEnc = DTListaIngresos.Rows(i)(1).ToString()
                                                    Dim vFecha = CDate(DTListaIngresos.Rows(i)(2).ToString())
                                                    Dim vUnidades = CDbl(DTListaIngresos.Rows(i)(3).ToString())
                                                    Dim vValor_Total = Math.Round(CDbl(DTListaIngresos.Rows(i)(4).ToString()), 4)

                                                    Dim vCodigo_Producto As String = ""
                                                    If chkAgruparPorProducto.Checked Then
                                                        vCodigo_Producto = DTListaIngresos.Rows(i)(5).ToString()
                                                    End If

                                                    '#GT16102025: validar si el ingreso es por transferencia, para que el primer día sea cobro 0
                                                    '#GT16102025: solo hacer la consulta una vez, para no sobrecargar el proceso.
                                                    If BeOrdenCompraEnc.IdOrdenCompraEnc <> vIdOrdenCompraEnc Then
                                                        BeOrdenCompraEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc(vIdOrdenCompraEnc)
                                                        If BeOrdenCompraEnc IsNot Nothing Then
                                                            BePedidoEnc = clsLnTrans_pe_enc.GetPedido_By_IdDespachoEnc(BeOrdenCompraEnc.IdDespachoEnc)
                                                        End If
                                                    End If

                                                    '#GT25072024: validar propiedades del producto especifico o uno perteneciente a la poliza
                                                    If chkVarianteCobro.Checked Then

                                                        If chkAgruparPorProducto.Checked Then
                                                            pBeProducto = New clsBeProducto
                                                            pBeProducto = clsLnProducto.Get_BeProducto_Cobranza_By_Codigo(vCodigo_Producto, pBodega.IdBodega, IdPropietario)
                                                            If pBeProducto IsNot Nothing Then
                                                                '#GT29082024: validar que no tenga factor y dimensiones al mismo tiempo.
                                                                If pBeProducto.IdUnidadMedidaCobro > 0 AndAlso (pBeProducto.Largo > 0 OrElse pBeProducto.Ancho OrElse pBeProducto.Alto) Then
                                                                    SplashScreenManager.CloseForm(False)
                                                                    Throw New Exception("El producto " & pBeProducto.Nombre & " tiene factor y dimensiones de cobro, no se puede determinar cual se debe aplicar.")
                                                                End If

                                                                '#GT06082024: aplicar cobro variante por factor
                                                                If pBeProducto.IdUnidadMedidaCobro > 0 Then
                                                                    Dim UmbasCobro = clsLnUnidad_medida.Get_Unidad_Medida_By_IdUnidadMedida(pBeProducto.IdUnidadMedidaCobro)
                                                                    If UmbasCobro IsNot Nothing Then
                                                                        Dim tmpCobro = Math.Round(vUnidades * UmbasCobro.factor, pDecimalesEnCobro)
                                                                        Dim tmpCobro2 = Math.Round(tmpCobro / vDias_eventos, pDecimalesEnCobro)
                                                                        vCobroAlmacenajeDiario = Math.Round(tmpCobro2, pDecimalesEnCobro)
                                                                    End If
                                                                End If

                                                                '#GT06082024: aplicar cobro variante por dimensiones
                                                                If pBeProducto.Largo AndAlso pBeProducto.Ancho Then
                                                                    Dim Mt As Decimal = 0.00
                                                                    If pBeProducto.Alto > 0 Then
                                                                        Mt = pBeProducto.Alto * pBeProducto.Ancho * pBeProducto.Largo
                                                                    Else
                                                                        Mt = pBeProducto.Largo * pBeProducto.Ancho
                                                                    End If

                                                                    Dim tmpCobro = Math.Round(vUnidades * Mt, pDecimalesEnCobro)
                                                                    Dim tmpCobro2 = Math.Round(tmpCobro * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                    Dim tmpMTS = Math.Round(tmpCobro2 / vDias_eventos, pDecimalesEnCobro)
                                                                    vCobroAlmacenajeDiario = Math.Round(tmpMTS, pDecimalesEnCobro)

                                                                End If

                                                                vNombreProducto = pBeProducto.Nombre

                                                            Else
                                                                XtraMessageBox.Show("El producto no tiene configuracion para cobro por variante", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                            End If

                                                        Else

                                                            SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por unidad y variante...")

                                                            '#GT29082024: buscar productos unicos asociados a la poliza especifica
                                                            '#GT17102025: aqui también traer las lic_plates para saber cuantas posicines ocupan.
                                                            Dim tableProductos As New DataTable
                                                            tableProductos = clsLnStock_jornada.Get_Productos_By_IdOrdenCompra_And_Rango_Fechas(vIdOrdenCompraEnc,
                                                                                                                                                vFecha.Date,
                                                                                                                                                pTimeOut)

                                                            '#GT03092024: validar que los productos tengan configuracion por variante (umbas cobro o medidas)
                                                            If tableProductos IsNot Nothing AndAlso tableProductos.Rows.Count > 0 Then
                                                                For j As Integer = 0 To tableProductos.Rows.Count - 1
                                                                    Dim pdProducto As String = tableProductos.Rows(j)(0)
                                                                    Dim pProducto = clsLnProducto.Get_BeProducto_Cobranza_By_Codigo(pdProducto, pBodega.IdBodega, IdPropietario)

                                                                    '#GT29082024: si producto no tiene variante cobro, salir del proceso y alertar
                                                                    If pProducto Is Nothing Then
                                                                        SplashScreenManager.CloseForm(False)
                                                                        Throw New Exception("No hay configuracion de cobro por variante, producto: " & pdProducto)
                                                                        Exit For
                                                                    Else
                                                                        '#GT21102024: si el producto ya existe no agregar a lista, eso duplicará el cobro.
                                                                        Dim tmpProducto = pListaProductoConVariante.Find(Function(x) x.Codigo = pProducto.Codigo)
                                                                        If tmpProducto Is Nothing Then
                                                                            pListaProductoConVariante.Add(pProducto)
                                                                        End If

                                                                    End If
                                                                Next
                                                            Else
                                                                SplashScreenManager.CloseForm(False)
                                                                Throw New Exception("No se encontró historico para el ingreso con referencia: " & vNumero_orden & " e ingreso " & vIdOrdenCompraEnc)
                                                            End If

                                                            '#GT03092024: iterar la lista de todos los productos y cálcular el cobro total de la poliza.
                                                            If pListaProductoConVariante.Count > 0 Then
                                                                Dim tmpCobroAlmacenajeDiario As Double = 0.00
                                                                For Each pBeProducto In pListaProductoConVariante

                                                                    '#GT06082024: aplicar cobro variante por factor
                                                                    If pBeProducto.IdUnidadMedidaCobro > 0 Then

                                                                        SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por unidad y posición...")

                                                                        'Dim UmbasCobro = clsLnUnidad_medida.Get_Unidad_Medida_By_IdUnidadMedida(pBeProducto.IdUnidadMedidaCobro)
                                                                        'If UmbasCobro IsNot Nothing Then
                                                                        '    Dim tmpCobro = Math.Round(vUnidades * UmbasCobro.factor, pDecimalesEnCobro)
                                                                        '    Dim tmpCobro2 = Math.Round(tmpCobro / vDias_eventos, pDecimalesEnCobro)
                                                                        '    tmpCobroAlmacenajeDiario += Math.Round(tmpCobro2, pDecimalesEnCobro)
                                                                        '    Dim tmpCobro = Math.Round(vUnidades * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                        '    tmpCobroAlmacenajeDiario += Math.Round(tmpCobro, pDecimalesEnCobro)
                                                                        'End If

                                                                        '#Consultar el # de licencias asociadas al producto para inferir el # de posiciones ocupadas.
                                                                        pPosicionesOcupadas = clsLnStock_jornada.Get_Posiciones_By_IdOrdenCompra_And_Fecha_And_IdProducto(vIdOrdenCompraEnc, vFecha, pBeProducto.Codigo, pTimeOut)

                                                                        If pPosicionesOcupadas > 0 Then
                                                                            Dim tmpCobro = Math.Round(pPosicionesOcupadas * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                            tmpCobroAlmacenajeDiario += Math.Round(tmpCobro, pDecimalesEnCobro)
                                                                        End If

                                                                    End If

                                                                    '#GT06082024: aplicar cobro variante por dimensiones
                                                                    If pBeProducto.Largo AndAlso pBeProducto.Ancho Then

                                                                        SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por unidad y dimensiónes...")
                                                                        Dim Mt As Decimal = 0.00
                                                                        If pBeProducto.Alto > 0 Then
                                                                            Mt = pBeProducto.Alto * pBeProducto.Ancho * pBeProducto.Largo
                                                                        Else
                                                                            Mt = pBeProducto.Largo * pBeProducto.Ancho
                                                                        End If
                                                                        Dim tmpCobro = Math.Round(vUnidades * Mt, pDecimalesEnCobro)
                                                                        Dim tmpCobro2 = Math.Round(tmpCobro * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                        Dim tmpMTS = Math.Round(tmpCobro2 / vDias_eventos, pDecimalesEnCobro)
                                                                        tmpCobroAlmacenajeDiario += Math.Round(tmpMTS, pDecimalesEnCobro)
                                                                    End If
                                                                Next

                                                                vCobroAlmacenajeDiario = tmpCobroAlmacenajeDiario

                                                            End If

                                                        End If

                                                    Else

                                                        If chkAgruparPorProducto.Checked Then
                                                            pBeProducto = New clsBeProducto
                                                            pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vCodigo_Producto, pBodega.IdBodega, IdPropietario)
                                                            vNombreProducto = pBeProducto.Nombre
                                                        End If

                                                        vCobroAlmacenajeDiario = Math.Round((vUnidades * pAcuerdoPrimario.Monto) / vDias_eventos, pDecimalesEnCobro)
                                                    End If

                                                    '#GT12082024: si el acuerdo esta en dolares pero se requiere conversion a quetzales
                                                    If vUsarMonedaDolar Then
                                                        If Not chkMantenerDolares.Checked Then
                                                            'vCobroAlmacenajeDiario = Math.Round(vCobroAlmacenajeDiario * 1, pDecimalesEnCobro)
                                                            vCobroAlmacenajeDiario = Math.Round(vCobroAlmacenajeDiario * vTipoCambioEnQuetzales, pDecimalesEnCobro)
                                                        Else

                                                        End If
                                                    End If

                                                    '#GT16102025: si es transferencia, y generó ingreso auto por transfer y es el día uno de cobro
                                                    If BePedidoEnc.IdTipoPedido = 1 AndAlso BePedidoEnc.TipoPedido.Generar_pedido_ingreso_bodega_destino AndAlso i = 0 Then
                                                        vCobroAlmacenajeDiario = 0
                                                    End If

                                                    vCobro_por_linea += vCobroAlmacenajeDiario

                                                    DTGridDetallePreCuenta.Rows.Add(vIdOrdenCompraEnc,
                                                                                    vNumero_orden,
                                                                                    vCodigo_Producto,
                                                                                    vNombreProducto,
                                                                                    vFecha,
                                                                                    IIf(pPosicionesOcupadas > 0, pPosicionesOcupadas, vUnidades),
                                                                                    vValor_Total,
                                                                                    vCobroAlmacenajeDiario)

                                                Next

                                                If pAcuerdoPrimario.Prioridad > 0 Then
                                                    '#GT05092024: según correo 09092024
                                                    vErp = Math.Round(vCobro_por_linea / vMonto, pDecimalesEnCobro)
                                                Else
                                                    vErp = 1
                                                End If

                                                If chkVarianteCobro.Checked Then

                                                    If DTGridDetalleServicios.Rows.Count = 0 Then

                                                        DTGridDetalleServicios.Rows.Add(vIdAcuerdoEnc,
                                                                                        vIdAcuerdoDet,
                                                                                        vCodigoproducto,
                                                                                        vMoneda,
                                                                                        vCodacuerdo,
                                                                                        vDescripcion,
                                                                                        vCorrelativo,
                                                                                        vServicio,
                                                                                        vNumero_unidades,
                                                                                        vMonto,
                                                                                        vPorcentaje,
                                                                                        vCobro_por_linea,
                                                                                        vErp,
                                                                                        vDias_eventos)

                                                    Else

                                                        Dim pcobro = CDbl(DTGridDetalleServicios.Rows(0)("total").ToString())
                                                        pcobro += vCobro_por_linea

                                                        For Each row In DTGridDetalleServicios.Rows
                                                            row.Item("total") = pcobro
                                                        Next

                                                    End If

                                                Else

                                                    DTGridDetalleServicios.Rows.Add(vIdAcuerdoEnc,
                                                                                   vIdAcuerdoDet,
                                                                                   vCodigoproducto,
                                                                                   vMoneda,
                                                                                   vCodacuerdo,
                                                                                   vDescripcion,
                                                                                   vCorrelativo,
                                                                                   vServicio,
                                                                                   vNumero_unidades,
                                                                                   vMonto,
                                                                                   vPorcentaje,
                                                                                   vCobro_por_linea,
                                                                                   vErp,
                                                                                   vDias_eventos)

                                                End If

                                            End If

                                        End If

                                        '#****************************************************************************
                                        '#GT20062024: validar cobro almacenaje por valor mercaderia (tipo 2)
                                        If pAcuerdoPrimario.Prioridad > 0 AndAlso pAcuerdoPrimario.IdTipoCobro = 2 And pAcuerdoPrimario.Porcentaje > 0 Then

                                            SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por valor...")

                                            Dim tasa As Double = Math.Round(pAcuerdoPrimario.Porcentaje / 100, pDecimalesEnCobro)
                                            vCobroAlmacenajeDiario = 0

                                            If DTListaIngresos IsNot Nothing AndAlso DTListaIngresos.Rows.Count > 0 Then
                                                For i As Integer = 0 To DTListaIngresos.Rows.Count - 1
                                                    Dim vNumero_orden = DTListaIngresos.Rows(i)(0).ToString()
                                                    Dim vIdOrdenCompraEnc = CInt(DTListaIngresos.Rows(i)(1).ToString())
                                                    Dim vFecha = CDate(DTListaIngresos.Rows(i)(2).ToString())
                                                    Dim vUnidades = CDbl(DTListaIngresos.Rows(i)(3).ToString())
                                                    Dim vValor_Total = Math.Round(CDbl(DTListaIngresos.Rows(i)(4).ToString()), 4)
                                                    Dim vCodigo_Producto As String = ""
                                                    If chkAgruparPorProducto.Checked Then
                                                        vCodigo_Producto = DTListaIngresos.Rows(i)(5).ToString()
                                                    End If

                                                    If vUsarMonedaDolar Then
                                                        Dim tmpCobro = Math.Round(vValor_Total * tasa, 4)
                                                        Dim tmpcobro2 = Math.Round(tmpCobro / vTipoCambioEnQuetzales, pDecimalesEnCobro)
                                                        Dim tmpCobro3 = Math.Round(tmpcobro2 / pAcuerdoPrimario.Dias_eventos, pDecimalesEnCobro)
                                                        vCobroAlmacenajeDiario = Math.Round(tmpCobro3, pDecimalesEnCobro)
                                                        '#GT20112024: calculo en dolares, pero se requiere convertir a quetzales.
                                                        If Not chkMantenerDolares.Checked Then
                                                            vCobroAlmacenajeDiario = Math.Round(vCobroAlmacenajeDiario * vTipoCambioEnQuetzales, pDecimalesEnCobro)
                                                        End If
                                                    Else
                                                        Dim tmpCobro = Math.Round(vValor_Total * tasa, pDecimalesEnCobro)
                                                        vCobroAlmacenajeDiario = Math.Round(tmpCobro / pAcuerdoPrimario.Dias_eventos, pDecimalesEnCobro)
                                                    End If

                                                    If chkAgruparPorProducto.Checked Then
                                                        pBeProducto = New clsBeProducto
                                                        pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vCodigo_Producto, AP.Bodega.IdBodega, IdPropietario)
                                                        If pBeProducto IsNot Nothing Then
                                                            vNombreProducto = pBeProducto.Nombre
                                                        End If
                                                    End If

                                                    vCobro_por_linea += vCobroAlmacenajeDiario
                                                    DTGridDetallePreCuenta.Rows.Add(vIdOrdenCompraEnc,
                                                                                    vNumero_orden,
                                                                                    vCodigo_Producto,
                                                                                    vNombreProducto,
                                                                                    vFecha,
                                                                                    vUnidades,
                                                                                    vValor_Total,
                                                                                    vCobroAlmacenajeDiario)
                                                Next

                                                If pAcuerdoPrimario.Prioridad > 0 Then
                                                    vErp = Math.Round((vCobro_por_linea / vPorcentaje) * 100, pDecimalesEnCobro)
                                                End If

                                                DTGridDetalleServicios.Rows.Add(vIdAcuerdoEnc,
                                                                                vIdAcuerdoDet,
                                                                                vCodigoproducto,
                                                                                vMoneda,
                                                                                vCodacuerdo,
                                                                                vDescripcion,
                                                                                vCorrelativo,
                                                                                vServicio,
                                                                                vNumero_unidades,
                                                                                vMonto,
                                                                                vPorcentaje,
                                                                                vCobro_por_linea,
                                                                                vErp,
                                                                                vDias_eventos)

                                            End If

                                        End If


                                        '#*****************************************************************************
                                        '#GT08072024: validar cobro por manejo si la póliza pertenece al mes de corte.
                                        '#GT07102024: si es devolucion, no se calcula manejo

                                        If Not pTrans_oc_ti.Es_devolucion Then
                                            If pAcuerdoPrimario.Prioridad > 0 AndAlso pAcuerdoPrimario.IdTipoCobro = 0 And pAcuerdoPrimario.Monto > 0 Then
                                                SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por manejo...")

                                                If pBodega.Es_Bodega_Fiscal Then

                                                    If pPolizaIngresoExiste.Fecha_abordaje.Date >= pFechaDesde.Date AndAlso pPolizaIngresoExiste.Fecha_abordaje.Date <= pFechaHasta.Date Then

                                                        Dim lastRow As DataRow
                                                        Dim result() As DataRow = DTListaIngresos.Select("numero_orden = " & pPolizaIngresoExiste.numero_orden)
                                                        lastRow = result.LastOrDefault()
                                                        Dim tmpFecha = CDate(lastRow.Item("fecha"))

                                                        If chkControlPesoBruto.Checked Then
                                                            '#GT10042025: si peso es menor a 1k de unidades, se utiliza el cobro por el monto del acuerdo
                                                            If pPolizaIngresoExiste.Total_bultos_Peso_Bruto < pAcuerdoPrimario.Numero_unidades Then
                                                                pCalculomanejo = pAcuerdoPrimario.Monto
                                                            Else
                                                                Dim tmpCobro = Math.Round(pPolizaIngresoExiste.Total_bultos_Peso_Bruto / 1000, pDecimalesEnCobro)
                                                                Dim tmpCobro2 = Math.Round(tmpCobro * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                pCalculomanejo = Math.Round(tmpCobro2, pDecimalesEnCobro)
                                                            End If

                                                        Else
                                                            '#GT10042025: si peso es menor a 1k de unidades, se utiliza el cobro por el monto del acuerdo
                                                            If pPolizaIngresoExiste.Total_bultos_Peso_Bruto < pAcuerdoPrimario.Numero_unidades Then
                                                                pCalculomanejo = pAcuerdoPrimario.Monto
                                                            Else
                                                                Dim tmpCobro = Math.Round(pPolizaIngresoExiste.Total_bultos_Peso_Neto / 1000, pDecimalesEnCobro)
                                                                Dim tmpCobro2 = Math.Round(tmpCobro * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                pCalculomanejo = Math.Round(tmpCobro2, pDecimalesEnCobro)
                                                            End If

                                                        End If

                                                        If vUsarMonedaDolar Then
                                                            If chkMantenerDolares.Checked Then
                                                                pCalculomanejo = Math.Round(pCalculomanejo * 1, pDecimalesEnCobro)
                                                            Else
                                                                pCalculomanejo = Math.Round(pCalculomanejo * vTipoCambioEnQuetzales, pDecimalesEnCobro)
                                                            End If
                                                        Else

                                                        End If

                                                        pIntegracionCalculoManejo += pCalculomanejo

                                                        If pCalculomanejo > 0 Then
                                                            DTGridDetallePreCuenta.Rows.Add(pPolizaIngresoExiste.IdOrdenCompraEnc,
                                                                                            pPolizaIngresoExiste.numero_orden,
                                                                                            "",
                                                                                            vNombreProducto,
                                                                                            tmpFecha,
                                                                                            0,
                                                                                            0,
                                                                                            0,
                                                                                            pCalculomanejo)
                                                        End If


                                                        If pIntegracionCalculoManejo > 0 Then
                                                            vErp = Math.Round((pIntegracionCalculoManejo / vMonto) * 1000, pDecimalesEnCobro)

                                                            DTGridDetalleServicios.Rows.Add(vIdAcuerdoEnc,
                                                                                            vIdAcuerdoDet,
                                                                                            vCodigoproducto,
                                                                                            vMoneda,
                                                                                            vCodacuerdo,
                                                                                            vDescripcion,
                                                                                            vCorrelativo,
                                                                                            vServicio,
                                                                                            vNumero_unidades,
                                                                                            vMonto,
                                                                                            vPorcentaje,
                                                                                            pIntegracionCalculoManejo,
                                                                                            vErp,
                                                                                            vDias_eventos)
                                                        End If

                                                    End If

                                                Else

                                                    If BeOCEnc.Fecha_Creacion.Date >= pFechaDesde.Date AndAlso BeOCEnc.Fecha_Creacion.Date <= pFechaHasta.Date Then

                                                        '#GT26082024: aqui filtramos si traemos peso bruto o neto.
                                                        Dim pPesoCobro = clsLnTrans_oc_det.Get_Peso_OC_By_IdOrdenCompraEnc_HH(BeOCEnc.IdOrdenCompraEnc, chkControlPesoBruto.Checked)
                                                        If pPesoCobro = 0 Then
                                                            SplashScreenManager.CloseForm(False)
                                                            Throw New Exception("Error: No se encontró un peso para cálcular manejo!.")
                                                        End If

                                                        Dim lastRow As DataRow
                                                        Dim result() As DataRow = DTListaIngresos.Select("referencia = " & "'" & BeOCEnc.Referencia & "'")
                                                        lastRow = result.LastOrDefault()
                                                        Dim tmpFecha = CDate(lastRow.Item("fecha"))

                                                        '#GT12092024: calculo por partes para mantener 4 decimales en cobro
                                                        '#GT10042025: si peso es menor a 1k de unidades, se utiliza el cobro por el monto del acuerdo
                                                        If pPesoCobro < pAcuerdoPrimario.Numero_unidades Then
                                                            pCalculomanejo = pAcuerdoPrimario.Monto
                                                        Else
                                                            Dim tmpPeso = Math.Round(pPesoCobro / 1000, pDecimalesEnCobro)
                                                            Dim tmpPeso2 = Math.Round(tmpPeso * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                            pCalculomanejo = Math.Round(tmpPeso2, pDecimalesEnCobro)
                                                        End If

                                                        If vUsarMonedaDolar Then
                                                            If chkMantenerDolares.Checked Then
                                                                pCalculomanejo = Math.Round(pCalculomanejo * 1, pDecimalesEnCobro)
                                                            Else
                                                                pCalculomanejo = Math.Round(pCalculomanejo * vTipoCambioEnQuetzales, pDecimalesEnCobro)
                                                            End If
                                                        End If

                                                        pIntegracionCalculoManejo += pCalculomanejo
                                                        If pCalculomanejo > 0 Then
                                                            DTGridDetallePreCuenta.Rows.Add(BeOCEnc.IdOrdenCompraEnc,
                                                                                                  BeOCEnc.Referencia,
                                                                                                  "",
                                                                                                  vNombreProducto,
                                                                                                  tmpFecha,
                                                                                                  0,
                                                                                                  0,
                                                                                                  0,
                                                                                                  pCalculomanejo)
                                                        End If

                                                        If pIntegracionCalculoManejo > 0 Then
                                                            vErp = Math.Round((pIntegracionCalculoManejo / vMonto) * 1000, pDecimalesEnCobro)

                                                            DTGridDetalleServicios.Rows.Add(vIdAcuerdoEnc,
                                                                                        vIdAcuerdoDet,
                                                                                        vCodigoproducto,
                                                                                        vMoneda,
                                                                                        vCodacuerdo,
                                                                                        vDescripcion,
                                                                                        vCorrelativo,
                                                                                        vServicio,
                                                                                        vNumero_unidades,
                                                                                        vMonto,
                                                                                        vPorcentaje,
                                                                                        pIntegracionCalculoManejo,
                                                                                        vErp,
                                                                                        vDias_eventos)
                                                        End If

                                                    End If

                                                End If

                                            End If
                                        End If

                                    Next

                                    '#********************************************************************************
                                    '#GT03092024: Buscar servicios a cobrar en los documentos de ingreso y de salida
                                    SplashScreenManager.Default.SetWaitFormDescription("Validando servicios en OC...")
                                    Dim vIdOdenCompraEnc As Integer = 0
                                    If pBodega.Es_Bodega_Fiscal Then
                                        vIdOdenCompraEnc = pPolizaIngresoExiste.IdOrdenCompraEnc
                                    Else
                                        vIdOdenCompraEnc = BeOCEnc.IdOrdenCompraEnc
                                    End If
                                    pListaServiciosOC = clsLnTrans_oc_servicios.Get_All_By_IdOrdenCompraEnc(vIdOdenCompraEnc, pAcuerdoComercial.IdAcuerdoEnc)
                                    '#GT17062024: por cada OC iteramos sus servicios y los agregamos al grid.
                                    If pListaServiciosOC IsNot Nothing Then
                                        If pListaServiciosOC.Count > 0 Then
                                            vCobro_por_linea = 0
                                            For Each ServicioOC In pListaServiciosOC

                                                Dim pServicio = pAcuerdoComercial.lDetalle.Find(Function(x) x.IdAcuerdoEnc = ServicioOC.IdAcuerdo AndAlso
                                                                                                 x.Correlativo_detalleacuerdo = ServicioOC.Corre_detalleacuerdo)

                                                If pServicio IsNot Nothing Then
                                                    If ServicioOC.Fecha_Servicio.Date >= pFechaDesde.Date AndAlso ServicioOC.Fecha_Servicio.Date <= pFechaHasta.Date Then
                                                        vCobro_por_linea = Math.Round((pServicio.Monto * ServicioOC.Cantidad), pDecimalesEnCobro)
                                                        '#GT20112024: validar si servicios en dolar requieren conversion a quetzales o no.
                                                        If vUsarMonedaDolar Then
                                                            If Not chkMantenerDolares.Checked Then
                                                                vCobro_por_linea = Math.Round((pServicio.Monto * ServicioOC.Cantidad * vTipoCambioEnQuetzales), pDecimalesEnCobro)
                                                            End If
                                                        End If

                                                        vErp = ServicioOC.Cantidad
                                                        DTGridDetalleServicios.Rows.Add(pServicio.IdAcuerdoEnc,
                                                                           pServicio.IdAcuerdoDet,
                                                                           pServicio.Codigo_producto,
                                                                           vMoneda,
                                                                           pServicio.Codigo_acuerdo,
                                                                           pServicio.Descripcion,
                                                                           pServicio.Correlativo_detalleacuerdo,
                                                                           pServicio.Servicio,
                                                                           pServicio.Numero_unidades,
                                                                           pServicio.Monto,
                                                                           pServicio.Porcentaje,
                                                                           vCobro_por_linea,
                                                                           vErp,
                                                                           pServicio.Dias_eventos)
                                                    End If

                                                End If

                                            Next

                                        End If

                                    End If

                                    '#GT03092024: Llenar los grid con las tablas
                                    dgriDetallePreCuenta.DataSource = DTGridDetallePreCuenta
                                    dgridServiciosAsociados.DataSource = DTGridDetalleServicios

                                    If gvdetalleprecuenta.RowCount > 0 Then

                                        gvdetalleprecuenta.Columns("numero_orden").Group()
                                        gvdetalleprecuenta.Columns("idordencompraenc").Visible = False

                                        '#GT12092024: agrupar producto no es funcion exclusiva de la variante de cobro, por requerimiento Cealsa
                                        If chkVarianteCobro.Checked AndAlso chkAgruparPorProducto.Checked Then
                                            gvdetalleprecuenta.Columns("codigo_producto").Group()
                                        ElseIf chkAgruparPorProducto.Checked Then
                                            gvdetalleprecuenta.Columns("codigo_producto").Group()
                                        End If

                                        Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                                            With {.FieldName = "almacenaje",
                                            .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                                            .DisplayFormat = "Total: {0:N4}",
                                            .ShowInGroupColumnFooter = gvdetalleprecuenta.Columns("almacenaje")}
                                        gvdetalleprecuenta.GroupSummary.Add(item)

                                        gvdetalleprecuenta.BestFitColumns(True)
                                        gvdetalleprecuenta.ExpandAllGroups()
                                        gvdetalleprecuenta.OptionsBehavior.Editable = False

                                        xtratabPrecuenta.SelectedTabPageIndex = 1
                                        gvdetalleprecuenta.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways
                                        gvdetalleprecuenta.OptionsBehavior.Editable = False

                                        gvdetalleprecuenta.Columns("almacenaje").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                                        gvdetalleprecuenta.Columns("almacenaje").SummaryItem.DisplayFormat = "{0:n4}"
                                        gvdetalleprecuenta.Columns("almacenaje").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                        gvdetalleprecuenta.Columns("almacenaje").DisplayFormat.FormatString = "{0:n4}"

                                        gvdetalleprecuenta.Columns("manejo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                                        gvdetalleprecuenta.Columns("manejo").SummaryItem.DisplayFormat = "{0:n4}"
                                        gvdetalleprecuenta.Columns("manejo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                        gvdetalleprecuenta.Columns("manejo").DisplayFormat.FormatString = "{0:n4}"

                                    End If

                                    pCalculoProcesado = True

                                Else
                                    SplashScreenManager.CloseForm(False)
                                    Throw New Exception("El acuerdo comercial no tiene servicios con prioridad, para aplicar cobro automatico.")
                                End If
                            End If

                        Else
                            SplashScreenManager.CloseForm(False)
                            Throw New Exception("Error: El acuerdo comercial no tiene servicios para cobranza. Valide la lista de acuerdos comerciales.")
                        End If

                    Else
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("Error: No se encontró un acuerdo comercial.")
                    End If

                End If

            Else


                SplashScreenManager.Default.SetWaitFormDescription("Validando todas las polizas...")
                '#GT05082024: validar que solo exista un acuerdo, o si existen varios, debera utilizar cobro con póliza especifica
                If DTAcuerdos.Rows.Count > 1 Then
                    SplashScreenManager.CloseForm(False)
                    Throw New Exception("Error: El propietario tiene varios comerciales activos, el sistema no puede determinar cuál se debe aplicar a todas las pólizas.")
                Else

                    '#GT20062024: si existe solo un acuerdo, tomarlo por defecto
                    pAcuerdoComercial = clsLnTrans_acuerdoscomerciales_enc.Get_AcuerdoEnc_And_Detalle_By_IdCliente(IdPropietario, pBodega.IdBodega)

                    If pAcuerdoComercial IsNot Nothing Then
                        Llena_Rubros_By_CodAcuerdo(pAcuerdoComercial.Codigo_acuerdo)

                        If pAcuerdoComercial.lDetalle IsNot Nothing Then

                            Dim listAcuerdosPrioritarios As New List(Of clsBeTrans_acuerdoscomerciales_det)
                            listAcuerdosPrioritarios = pAcuerdoComercial.lDetalle.Where(Function(x) x.Prioridad = 1 AndAlso x.IdBodega = pBodega.IdBodega).ToList()

                            If listAcuerdosPrioritarios IsNot Nothing Then

                                If listAcuerdosPrioritarios.Count > 0 Then

                                    lPolizasOC = New List(Of clsBeTrans_oc_pol)
                                    ListaTranOcEncReferencias = New List(Of clsBeTrans_oc_enc)
                                    '#GT16102025: cargar la oc y pe y determinar si es transferencia, para no cobrar el primer día obtenido de la lista.
                                    Dim BeOrdenCompraEnc As New clsBeTrans_oc_enc()
                                    Dim BePedidoEnc As New clsBeTrans_pe_enc()


                                    For i As Integer = 0 To DTListaIngresos.Rows.Count - 1



                                        Dim vNumero_orden = DTListaIngresos.Rows(i)(0).ToString()
                                        Debug.WriteLine("numero_orden" & vNumero_orden)
                                        Dim vIdOrdenCompraEnc = DTListaIngresos.Rows(i)(1).ToString()
                                        Debug.WriteLine("OrdenCompra" & vIdOrdenCompraEnc)

                                        'If vIdOrdenCompraEnc = 1214 Then
                                        '    Debug.WriteLine("aqui va")
                                        'End If

                                        If Not String.IsNullOrEmpty(vNumero_orden) Then
                                            Dim pListaPolizas As New List(Of clsBeTrans_oc_pol)
                                            Dim pOcPoliza = clsLnTrans_oc_pol.GetSingle_By_Numero_Orden_And_IdOrdenCompraEnc(vNumero_orden, vIdOrdenCompraEnc)
                                            If pOcPoliza IsNot Nothing Then
                                                Dim existe = lPolizasOC.Any(Function(x) x.numero_orden = vNumero_orden AndAlso x.IdOrdenCompraEnc = vIdOrdenCompraEnc)
                                                If Not existe Then
                                                    lPolizasOC.Add(pOcPoliza)
                                                End If
                                            End If

                                            If Not String.IsNullOrEmpty(vNumero_orden) Then
                                                pTransOcEncReferencia = New clsBeTrans_oc_enc
                                                pTransOcEncReferencia = clsLnTrans_oc_enc.Get_Single_By_Referencia(vNumero_orden, vIdOrdenCompraEnc, pBodega.IdBodega)

                                                If pTransOcEncReferencia IsNot Nothing Then
                                                    ' Verificar si ya existe en la lista
                                                    Dim existe = ListaTranOcEncReferencias.Any(Function(x) x.Referencia = vNumero_orden AndAlso x.IdOrdenCompraEnc = vIdOrdenCompraEnc)
                                                    ' Solo agregar si no existe
                                                    If Not existe Then
                                                        ListaTranOcEncReferencias.Add(pTransOcEncReferencia)
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Next

                                    '#GT06082024: iteramos los detalles del acuerdo
                                    For Each pAcuerdoPrimario As clsBeTrans_acuerdoscomerciales_det In listAcuerdosPrioritarios
                                        vIdAcuerdoEnc = pAcuerdoPrimario.IdAcuerdoEnc
                                        vIdAcuerdoDet = pAcuerdoPrimario.IdAcuerdoDet
                                        vCodigoproducto = pAcuerdoPrimario.Codigo_producto
                                        vCodacuerdo = pAcuerdoPrimario.Codigo_acuerdo
                                        vDescripcion = pAcuerdoPrimario.Descripcion
                                        vCorrelativo = pAcuerdoPrimario.Correlativo_detalleacuerdo
                                        vDias_eventos = pAcuerdoPrimario.Dias_eventos
                                        vNumero_unidades = pAcuerdoPrimario.Numero_unidades
                                        vServicio = pAcuerdoPrimario.Servicio
                                        vMonto = pAcuerdoPrimario.Monto
                                        vPorcentaje = pAcuerdoPrimario.Porcentaje
                                        vMoneda = pAcuerdoComercial.Moneda
                                        pListaProductoConVariante = New List(Of clsBeProducto)


                                        '#GT20112024: validar si requerimos tipo cambio antes de realizar cualquier cálculo.
                                        If clsDataContractDI.tTipoMonedaPrefacturacion.Dolar = pAcuerdoComercial.Cod_moneda Then
                                            If vTipoCambioEnQuetzales = 0 Then
                                                SplashScreenManager.CloseForm(False)
                                                txtTipoCambio.Focus()
                                                Throw New Exception("El acuerdo comercial en dolares requiere, tipo de cambio superior a 0.")
                                            Else
                                                vUsarMonedaDolar = True
                                            End If
                                        Else
                                            vUsarMonedaDolar = False
                                        End If


                                        '*************************************************************************************************************
                                        '#GT20062024: validar si es cobro por unidad (tipo 1)
                                        '#GT03092024: validar si tiene variante de cobro (el producto tiene factor de cobro o dimensiones registradas)
                                        If pAcuerdoPrimario.IdTipoCobro = 1 And pAcuerdoPrimario.Monto > 0 Then

                                            SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por unidad...")

                                            vCobroAlmacenajeDiario = 0
                                            BeOrdenCompraEnc = New clsBeTrans_oc_enc()
                                            BePedidoEnc = New clsBeTrans_pe_enc()
                                            Dim pPosicionesOcupadas As Integer = 0
                                            '#GT23102025: Resetar las posiciones contadas si cambiamos de día, para que la cantidad no aumente exponencialmente.
                                            Dim fechaAnterior As Date? = Nothing


                                            If DTListaIngresos IsNot Nothing AndAlso DTListaIngresos.Rows.Count > 0 Then
                                                pBeProducto = Nothing
                                                For i As Integer = 0 To DTListaIngresos.Rows.Count - 1
                                                    Dim vCodigo_Producto As String = ""
                                                    Dim vNumero_orden = DTListaIngresos.Rows(i)(0).ToString()
                                                    Dim vIdOrdenCompraEnc = CInt(DTListaIngresos.Rows(i)(1).ToString())
                                                    Dim vFecha = CDate(DTListaIngresos.Rows(i)(2).ToString())
                                                    Dim vUnidades = CDbl(DTListaIngresos.Rows(i)(3).ToString())
                                                    Dim vValor_Total = Math.Round(CDbl(DTListaIngresos.Rows(i)(4).ToString()), 4)
                                                    If chkAgruparPorProducto.Checked Then
                                                        vCodigo_Producto = DTListaIngresos.Rows(i)(5).ToString()
                                                    End If


                                                    If fechaAnterior Is Nothing OrElse vFecha <> fechaAnterior.Value Then
                                                        pPosicionesOcupadas = 0   'Reiniciar al cambiar de día
                                                        fechaAnterior = vFecha    'Actualizar la referencia
                                                    End If


                                                    '#GT16102025: validar si el ingreso es por transferencia, para que el primer día sea cobro 0
                                                    If vIdOrdenCompraEnc = 15221 Then
                                                        Debug.WriteLine("aqui")
                                                    End If

                                                    If BeOrdenCompraEnc.IdOrdenCompraEnc <> vIdOrdenCompraEnc Then
                                                        BeOrdenCompraEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc(vIdOrdenCompraEnc)
                                                        If BeOrdenCompraEnc IsNot Nothing Then
                                                            BePedidoEnc = clsLnTrans_pe_enc.GetPedido_By_IdDespachoEnc(BeOrdenCompraEnc.IdDespachoEnc)
                                                        End If
                                                    End If

                                                    '#GT25072024: Validamos si hay variantes de cobro.
                                                    If chkVarianteCobro.Checked Then
                                                        SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por unidad y variante...")

                                                        '#GT07082024: validar si se agrupa por producto
                                                        If chkAgruparPorProducto.Checked Then

                                                            pBeProducto = New clsBeProducto
                                                            pBeProducto = clsLnProducto.Get_BeProducto_Cobranza_By_Codigo(vCodigo_Producto, AP.Bodega.IdBodega, IdPropietario)

                                                            If pBeProducto IsNot Nothing Then
                                                                '#GT06082024: Validar cobro variante por factor
                                                                If pBeProducto.IdUnidadMedidaCobro > 0 Then
                                                                    Dim UmbasCobro = clsLnUnidad_medida.Get_Unidad_Medida_By_IdUnidadMedida(pBeProducto.IdUnidadMedidaCobro)
                                                                    If UmbasCobro IsNot Nothing Then
                                                                        '#GT12092024: calculo por partes con 4 cifras y valor final redondeo a 2. 
                                                                        'vCobroAlmacenajeDiario = Math.Round((vUnidades * UmbasCobro.factor) / vDias_eventos, 2)
                                                                        Dim tmpCobro = Math.Round(vUnidades * UmbasCobro.factor, pDecimalesEnCobro)
                                                                        Dim tmpCobro2 = Math.Round(tmpCobro / vDias_eventos, pDecimalesEnCobro)
                                                                        vCobroAlmacenajeDiario = Math.Round(tmpCobro2, pDecimalesEnCobro)

                                                                    End If
                                                                End If

                                                                '#GT06082024: Validar cobro variante por dimensiones
                                                                If pBeProducto.Largo AndAlso pBeProducto.Ancho Then

                                                                    Dim Mt As Decimal

                                                                    If pBeProducto.Alto > 0 Then
                                                                        Mt = pBeProducto.Alto * pBeProducto.Ancho * pBeProducto.Largo
                                                                    Else
                                                                        Mt = pBeProducto.Largo * pBeProducto.Ancho
                                                                    End If

                                                                    '#GT12092024: calculo por partes con 4 cifras y valor final redondeo a 2.
                                                                    Dim tmpCobro = Math.Round(vUnidades * Mt, pDecimalesEnCobro)
                                                                    Dim tmpCobro2 = Math.Round(tmpCobro * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                    Dim tmpMTS = Math.Round(tmpCobro2 / vDias_eventos, pDecimalesEnCobro)
                                                                    vCobroAlmacenajeDiario = Math.Round(tmpMTS, pDecimalesEnCobro)

                                                                End If

                                                                vNombreProducto = pBeProducto.Nombre

                                                            Else
                                                                XtraMessageBox.Show("El producto no tiene configuracion para cobro por variante", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                            End If

                                                        Else

                                                            Dim pIdOrdenCompraEnc As Integer = 0
                                                            Dim vListaSinVariante As Integer = 0
                                                            Dim Detalle As New List(Of clsBeTrans_re_det)

                                                            '#GT29082024: buscar productos unicos asociados a la poliza especifica
                                                            Dim tableProductos As New DataTable
                                                            tableProductos = clsLnStock_jornada.Get_Productos_By_IdOrdenCompra_And_Rango_Fechas(vIdOrdenCompraEnc,
                                                                                                                                                vFecha.Date,
                                                                                                                                                pTimeOut)

                                                            '#GT03092024: validar que los productos tengan configuracion por variante (umbas cobro o medidas)
                                                            If tableProductos IsNot Nothing AndAlso tableProductos.Rows.Count > 0 Then
                                                                For j As Integer = 0 To tableProductos.Rows.Count - 1
                                                                    Dim pdProducto As String = tableProductos.Rows(j)(0)
                                                                    Dim pProducto = clsLnProducto.Get_BeProducto_Cobranza_By_Codigo(pdProducto, pBodega.IdBodega, IdPropietario)
                                                                    '#GT29082024: si producto no tiene variante cobro, salir del proceso y alertar
                                                                    If pProducto Is Nothing Then
                                                                        SplashScreenManager.CloseForm(False)
                                                                        Throw New Exception("No hay configuracion de cobro por variante, producto: " & pdProducto)
                                                                        Exit For
                                                                    Else
                                                                        '#GT21102024: si el producto ya existe no agregar a lista, eso duplicará el cobro.
                                                                        Dim tmpProducto = pListaProductoConVariante.Find(Function(x) x.Codigo = pProducto.Codigo)
                                                                        If tmpProducto Is Nothing Then
                                                                            pListaProductoConVariante.Add(pProducto)
                                                                        End If
                                                                    End If
                                                                Next
                                                            Else

                                                                If Not IngresoSinHistorico.Contains(vIdOrdenCompraEnc) Then
                                                                    IngresoSinHistorico.Add(vIdOrdenCompraEnc)
                                                                    lblPrg.AppendText("No se encontró historico con referencia: " & vNumero_orden & " e ingreso" & vIdOrdenCompraEnc)
                                                                    lblPrg.AppendText(vbNewLine)
                                                                    lblPrg.Refresh()
                                                                    lblPrg.SelectionStart = lblPrg.TextLength
                                                                    lblPrg.ScrollToCaret()
                                                                End If

                                                            End If

                                                            '#GT03092024: iterar la lista de todos los productos y cálcular el cobro total de la poliza.
                                                            If pListaProductoConVariante.Count > 0 Then

                                                                Dim tmpCobroAlmacenajeDiario As Double = 0.00

                                                                For Each pBeProducto In pListaProductoConVariante

                                                                    '#GT06082024: aplicar cobro variante por factor
                                                                    If pBeProducto.IdUnidadMedidaCobro > 0 Then

                                                                        SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por unidad y posición...")

                                                                        Dim tmpPosicionesOcupadas = clsLnStock_jornada.Get_Posiciones_By_IdOrdenCompra_And_Fecha_And_IdProducto(vIdOrdenCompraEnc, vFecha, pBeProducto.Codigo, pTimeOut)
                                                                        If tmpPosicionesOcupadas > 0 Then
                                                                            Dim tmpCobro = Math.Round(tmpPosicionesOcupadas * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                            tmpCobroAlmacenajeDiario += Math.Round(tmpCobro, pDecimalesEnCobro)
                                                                            pPosicionesOcupadas += tmpPosicionesOcupadas
                                                                        End If

                                                                    End If


                                                                    '#GT06082024: aplicar cobro variante por factor
                                                                    'If pBeProducto.IdUnidadMedidaCobro > 0 Then
                                                                    '    Dim UmbasCobro = clsLnUnidad_medida.Get_Unidad_Medida_By_IdUnidadMedida(pBeProducto.IdUnidadMedidaCobro)
                                                                    '    If UmbasCobro IsNot Nothing Then
                                                                    '        '#GT12092024: calculo por partes con 4 cifras y valor final redondeo. 
                                                                    '        'Dim tmpCobro = Math.Round(vUnidades * UmbasCobro.factor, pDecimalesEnCobro)
                                                                    '        'Dim tmpCobro2 = Math.Round(tmpCobro / vDias_eventos, pDecimalesEnCobro)
                                                                    '        'tmpCobroAlmacenajeDiario = Math.Round(tmpCobro2, pDecimalesEnCobro)

                                                                    '        Dim tmppPosicionesOcupadas = clsLnStock_jornada.Get_Posiciones_By_IdOrdenCompra_And_Fecha_And_IdProducto(vIdOrdenCompraEnc, vFecha, pBeProducto.Codigo, pTimeOut)

                                                                    '        If tmppPosicionesOcupadas > 0 Then
                                                                    '            pPosicionesOcupadas += tmppPosicionesOcupadas
                                                                    '            Dim tmpCobro = Math.Round(pPosicionesOcupadas * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                    '            tmpCobroAlmacenajeDiario += Math.Round(tmpCobro, pDecimalesEnCobro)
                                                                    '        End If
                                                                    '    End If
                                                                    'End If

                                                                    '#GT06082024: aplicar cobro variante por dimensiones
                                                                    If pBeProducto.Largo AndAlso pBeProducto.Ancho Then
                                                                        Dim Mt As Decimal = 0.00
                                                                        If pBeProducto.Alto > 0 Then
                                                                            Mt = pBeProducto.Alto * pBeProducto.Ancho * pBeProducto.Largo
                                                                        Else
                                                                            Mt = pBeProducto.Largo * pBeProducto.Ancho
                                                                        End If

                                                                        '#GT12092024: calculo por partes con 4 cifras y valor final redondeo a 2.
                                                                        'Dim tmpMt = (vUnidades * Mt) * pAcuerdoPrimario.Monto
                                                                        'tmpCobroAlmacenajeDiario = Math.Round(tmpMt / vDias_eventos, 2)
                                                                        Dim tmpMT = Math.Round(vUnidades * Mt, 4)
                                                                        Dim tmpMT2 = Math.Round(tmpMT * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                        Dim tmpMTS = Math.Round(tmpMT2 / vDias_eventos, pDecimalesEnCobro)
                                                                        tmpCobroAlmacenajeDiario = Math.Round(tmpMTS, pDecimalesEnCobro)
                                                                    End If
                                                                Next

                                                                vCobroAlmacenajeDiario = tmpCobroAlmacenajeDiario

                                                            End If

                                                        End If

                                                    Else
                                                        SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por unidad sin variante...")
                                                        pBeProducto = New clsBeProducto
                                                        pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vCodigo_Producto, AP.Bodega.IdBodega, IdPropietario)

                                                        If pBeProducto IsNot Nothing Then
                                                            vNombreProducto = pBeProducto.Nombre
                                                        End If

                                                        vCobroAlmacenajeDiario = Math.Round(((vUnidades * pAcuerdoPrimario.Monto) / vDias_eventos), pDecimalesEnCobro)

                                                    End If

                                                    If vUsarMonedaDolar Then
                                                        If Not chkMantenerDolares.Checked Then
                                                            vCobroAlmacenajeDiario = Math.Round(vCobroAlmacenajeDiario * vTipoCambioEnQuetzales, pDecimalesEnCobro)
                                                        End If
                                                    End If

                                                    '#GT16102025: si es transferencia, y generó ingreso auto por transfer y es el día uno de cobro
                                                    If BePedidoEnc.IdTipoPedido = 1 AndAlso BePedidoEnc.TipoPedido.Generar_pedido_ingreso_bodega_destino Then
                                                        Dim firstRow As DataRow
                                                        Dim result() As DataRow = DTListaIngresos.Select("IdOrdenCompraEnc = '" + vIdOrdenCompraEnc.ToString() + "'")
                                                        firstRow = result.FirstOrDefault()
                                                        Dim tmpFecha = CDate(firstRow.Item("fecha"))

                                                        If tmpFecha = DTListaIngresos(i).Item("fecha") Then
                                                            vCobroAlmacenajeDiario = 0
                                                        End If

                                                    End If

                                                    vCobro_por_linea += vCobroAlmacenajeDiario

                                                    DTGridDetallePreCuenta.Rows.Add(vIdOrdenCompraEnc,
                                                                                      vNumero_orden,
                                                                                      vCodigo_Producto,
                                                                                      vNombreProducto,
                                                                                      vFecha,
                                                                                      IIf(pPosicionesOcupadas > 0, pPosicionesOcupadas, vUnidades),
                                                                                      vValor_Total,
                                                                                      vCobroAlmacenajeDiario,
                                                                                      0)

                                                Next

                                                '#GT07082024: creamos el valor ERP a enviar
                                                If pAcuerdoPrimario.Prioridad > 0 Then
                                                    vErp = Math.Round(vCobro_por_linea / vMonto, pDecimalesEnCobro)
                                                Else
                                                    vErp = 1
                                                End If

                                                '#GT07082024: llenamos grid con acuerdo a cobrar
                                                DTGridDetalleServicios.Rows.Add(vIdAcuerdoEnc,
                                                                                vIdAcuerdoDet,
                                                                                vCodigoproducto,
                                                                                vMoneda,
                                                                                vCodacuerdo,
                                                                                vDescripcion,
                                                                                vCorrelativo,
                                                                                vServicio,
                                                                                vNumero_unidades,
                                                                                vMonto,
                                                                                vPorcentaje,
                                                                                vCobro_por_linea,
                                                                                vErp,
                                                                                vDias_eventos)

                                            End If

                                        End If

                                        '****************************************************************************
                                        '#GT20062024: validar si es cobro por valor mercaderia (tipo 2)
                                        If pAcuerdoPrimario.IdTipoCobro = 2 And pAcuerdoPrimario.Porcentaje > 0 Then
                                            SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por valor...")
                                            Dim tasa As Double = pAcuerdoPrimario.Porcentaje / 100
                                            vCobroAlmacenajeDiario = 0

                                            If DTListaIngresos IsNot Nothing AndAlso DTListaIngresos.Rows.Count > 0 Then
                                                For i As Integer = 0 To DTListaIngresos.Rows.Count - 1
                                                    Dim vNumero_orden = DTListaIngresos.Rows(i)(0).ToString()
                                                    Dim vIdOrdenCompraEnc = CInt(DTListaIngresos.Rows(i)(1).ToString())
                                                    Dim vFecha = CDate(DTListaIngresos.Rows(i)(2).ToString())
                                                    Dim vUnidades = CDbl(DTListaIngresos.Rows(i)(3).ToString())
                                                    Dim vValor_Total = Math.Round(CDbl(DTListaIngresos.Rows(i)(4).ToString()), 4)
                                                    Dim vCodigo_Producto As String = ""
                                                    If chkAgruparPorProducto.Checked Then
                                                        vCodigo_Producto = DTListaIngresos.Rows(i)(5).ToString()
                                                    End If


                                                    If vUsarMonedaDolar Then
                                                        Dim tmpCobro = Math.Round(vValor_Total * tasa, pDecimalesEnCobro)
                                                        Dim tmpcobro2 = Math.Round(tmpCobro / vTipoCambioEnQuetzales, pDecimalesEnCobro)
                                                        Dim tmpCobro3 = Math.Round(tmpcobro2 / pAcuerdoPrimario.Dias_eventos, pDecimalesEnCobro)
                                                        vCobroAlmacenajeDiario = Math.Round(tmpCobro3, pDecimalesEnCobro)
                                                        '#GT20112024: calculo en dolares, pero se requiere convertir a quetzales.
                                                        If Not chkMantenerDolares.Checked Then
                                                            vCobroAlmacenajeDiario = Math.Round(vCobroAlmacenajeDiario * vTipoCambioEnQuetzales, pDecimalesEnCobro)
                                                        End If
                                                    Else
                                                        Dim tmpCobro = Math.Round(vValor_Total * tasa, pDecimalesEnCobro)
                                                        vCobroAlmacenajeDiario = Math.Round(tmpCobro / pAcuerdoPrimario.Dias_eventos, pDecimalesEnCobro)
                                                    End If

                                                    If chkAgruparPorProducto.Checked Then
                                                        pBeProducto = New clsBeProducto
                                                        pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vCodigo_Producto, AP.Bodega.IdBodega, IdPropietario)
                                                        If pBeProducto IsNot Nothing Then
                                                            vNombreProducto = pBeProducto.Nombre
                                                        End If
                                                    End If

                                                    vCobro_por_linea += vCobroAlmacenajeDiario

                                                    DTGridDetallePreCuenta.Rows.Add(vIdOrdenCompraEnc,
                                                                                      vNumero_orden,
                                                                                      vCodigo_Producto,
                                                                                      vNombreProducto,
                                                                                      vFecha,
                                                                                      vUnidades,
                                                                                      vValor_Total,
                                                                                      vCobroAlmacenajeDiario, 0)


                                                Next


                                                If pAcuerdoPrimario.Prioridad > 0 Then
                                                    'vErp = RedondearMultiplo05((vCobro_por_linea / vPorcentaje) * 100)
                                                    '#GT18112024: redondear a 4 decimales para que ERP no descuadre en reconversión.
                                                    'Dim tmpvCobro_por_linea = Math.Round(vCobro_por_linea, 4)
                                                    vErp = Math.Round((vCobro_por_linea / vPorcentaje) * 100, pDecimalesEnCobro)

                                                Else
                                                    vErp = 1
                                                End If

                                                DTGridDetalleServicios.Rows.Add(vIdAcuerdoEnc,
                                                                                vIdAcuerdoDet,
                                                                                vCodigoproducto,
                                                                                vMoneda,
                                                                                vCodacuerdo,
                                                                                vDescripcion,
                                                                                vCorrelativo,
                                                                                vServicio,
                                                                                vNumero_unidades,
                                                                                vMonto,
                                                                                vPorcentaje,
                                                                                vCobro_por_linea, vErp, vDias_eventos)

                                            End If

                                        End If

                                        '****************************************************
                                        '#GT05072024: validar cobro manejo si la poliza pertenece al mes de corte.
                                        If pAcuerdoPrimario.IdTipoCobro = 0 And pAcuerdoPrimario.Monto > 0 Then

                                            'SplashScreenManager.Default.SetWaitFormCaption("Validando cobro manejo")
                                            SplashScreenManager.Default.SetWaitFormDescription("Validando cobro por manejo...")

                                            If pBodega.Es_Bodega_Fiscal Then

                                                For Each pPoliza In lPolizasOC

                                                    Dim tmpOC = ListaTranOcEncReferencias.Find(Function(x) x.IdOrdenCompraEnc = pPoliza.IdOrdenCompraEnc)

                                                    If tmpOC Is Nothing Then
                                                        SplashScreenManager.CloseForm(False)
                                                        Throw New Exception("Error: No se encontró la relación entre la poliza " & pPoliza.numero_orden & " y el ingreso " & pPoliza.IdOrdenCompraEnc)
                                                    End If

                                                    pTrans_oc_ti = New clsBeTrans_oc_ti()
                                                    pTrans_oc_ti = clsLnTrans_oc_ti.GetSingle(tmpOC.IdTipoIngresoOC)

                                                    If pTrans_oc_ti Is Nothing Then
                                                        SplashScreenManager.CloseForm(False)
                                                        Throw New Exception("Error Critico: No se encontró el tipo de documento asociado a la poliza " & pPoliza.numero_orden & " e ingreso " & pPoliza.IdOrdenCompraEnc)
                                                    End If


                                                    '#GT07102024: no calcular manejo si es ingreso por devolucion
                                                    If Not pTrans_oc_ti.Es_devolucion Then
                                                        If pPoliza.Fecha_abordaje.Date >= pFechaDesde.Date AndAlso pPoliza.Fecha_abordaje.Date <= pFechaHasta.Date Then

                                                            Dim lastRow As DataRow
                                                            Dim result() As DataRow = DTListaIngresos.Select("numero_orden = '" + pPoliza.numero_orden.ToString() + "'")
                                                            lastRow = result.LastOrDefault()
                                                            Dim tmpFecha = CDate(lastRow.Item("fecha"))

                                                            If chkControlPesoBruto.Checked Then
                                                                If pPoliza.Total_bultos_Peso_Bruto < pAcuerdoPrimario.Numero_unidades Then
                                                                    pCalculomanejo = pAcuerdoPrimario.Monto
                                                                Else
                                                                    Dim tmpPeso = Math.Round(pPoliza.Total_bultos_Peso_Bruto / 1000, pDecimalesEnCobro)
                                                                    Dim tmpPeso2 = Math.Round(tmpPeso * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                    pCalculomanejo = Math.Round(tmpPeso2, pDecimalesEnCobro)
                                                                End If
                                                            Else
                                                                If pPoliza.Total_bultos_Peso_Neto < pAcuerdoPrimario.Numero_unidades Then
                                                                    pCalculomanejo = pAcuerdoPrimario.Monto
                                                                Else
                                                                    Dim tmpPeso = Math.Round(pPoliza.Total_bultos_Peso_Neto / 1000, pDecimalesEnCobro)
                                                                    Dim tmpPeso2 = Math.Round(tmpPeso * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                    pCalculomanejo = Math.Round(tmpPeso2, pDecimalesEnCobro)
                                                                End If
                                                            End If

                                                            If vUsarMonedaDolar Then
                                                                If Not chkMantenerDolares.Checked Then
                                                                    pCalculomanejo = Math.Round(pCalculomanejo * vTipoCambioEnQuetzales, pDecimalesEnCobro)
                                                                End If
                                                            Else

                                                            End If

                                                            pIntegracionCalculoManejo += pCalculomanejo

                                                            If pCalculomanejo > 0 Then
                                                                DTGridDetallePreCuenta.Rows.Add(pPoliza.IdOrdenCompraEnc,
                                                                                              pPoliza.numero_orden,
                                                                                              "",
                                                                                              vNombreProducto,
                                                                                              tmpFecha,
                                                                                              0,
                                                                                              0,
                                                                                              0,
                                                                                              pCalculomanejo)
                                                            End If

                                                        End If
                                                    End If

                                                Next


                                            Else

                                                For Each OC In ListaTranOcEncReferencias
                                                    pTrans_oc_ti = New clsBeTrans_oc_ti()
                                                    pTrans_oc_ti = clsLnTrans_oc_ti.GetSingle(OC.IdTipoIngresoOC)
                                                    '#GT07102024: no calcular manejo si es ingreso por devolucion
                                                    If Not pTrans_oc_ti.Es_devolucion Then
                                                        If OC.Fecha_Creacion.Date >= pFechaDesde.Date AndAlso OC.Fecha_Creacion.Date <= pFechaHasta.Date Then

                                                            '#GT26082024: aqui filtramos si traemos peso bruto o neto.
                                                            Dim pPesoCobro = clsLnTrans_oc_det.Get_Peso_OC_By_IdOrdenCompraEnc_HH(OC.IdOrdenCompraEnc, chkControlPesoBruto.Checked)

                                                            If pPesoCobro = 0 Then
                                                                SplashScreenManager.CloseForm(False)
                                                                Throw New Exception("Error: No se encontró un peso para cálcular manejo en el ingreso." & OC.IdOrdenCompraEnc)
                                                            End If

                                                            Dim lastRow As DataRow
                                                            Dim result() As DataRow = DTListaIngresos.Select("referencia = '" + OC.Referencia.ToString() + "'")
                                                            lastRow = result.LastOrDefault()
                                                            Dim tmpFecha = CDate(lastRow.Item("fecha"))

                                                            '#GT26082024: no se requiere validar si es peso bruto o neto, el query ya hizo la operación
                                                            If pPesoCobro < pAcuerdoPrimario.Numero_unidades Then
                                                                pCalculomanejo = pAcuerdoPrimario.Monto
                                                            Else
                                                                Dim tmpPeso = Math.Round(pPesoCobro / 1000, pDecimalesEnCobro)
                                                                Dim tmPeso2 = Math.Round(tmpPeso * pAcuerdoPrimario.Monto, pDecimalesEnCobro)
                                                                pCalculomanejo = Math.Round(tmPeso2, pDecimalesEnCobro)
                                                            End If

                                                            If vUsarMonedaDolar Then
                                                                If Not chkMantenerDolares.Checked Then
                                                                    pCalculomanejo = Math.Round(pCalculomanejo * vTipoCambioEnQuetzales, pDecimalesEnCobro)
                                                                End If

                                                            End If

                                                            pIntegracionCalculoManejo += pCalculomanejo

                                                            If pCalculomanejo > 0 Then
                                                                DTGridDetallePreCuenta.Rows.Add(OC.IdOrdenCompraEnc,
                                                                                              OC.Referencia,
                                                                                              "",
                                                                                              vNombreProducto,
                                                                                              tmpFecha,
                                                                                              0,
                                                                                              0,
                                                                                              0,
                                                                                              pCalculomanejo)
                                                            End If
                                                        End If
                                                    End If
                                                Next



                                            End If


                                            If pIntegracionCalculoManejo > 0 Then
                                                'vErp = RedondearMultiplo05((pIntegracionCalculoManejo / vMonto) * 1000)
                                                vErp = Math.Round((pIntegracionCalculoManejo / vMonto) * 1000, pDecimalesEnCobro)

                                                DTGridDetalleServicios.Rows.Add(vIdAcuerdoEnc,
                                                                                vIdAcuerdoDet,
                                                                                vCodigoproducto,
                                                                                vMoneda,
                                                                                vCodacuerdo,
                                                                                vDescripcion,
                                                                                vCorrelativo,
                                                                                vServicio,
                                                                                vNumero_unidades,
                                                                                vMonto,
                                                                                vPorcentaje,
                                                                                pIntegracionCalculoManejo,
                                                                                vErp,
                                                                                vDias_eventos)
                                            End If

                                        End If

                                    Next


                                    '#GT09092024: validar servicios en los ingresos, dependiendo de si es fiscal o general.
                                    '#********************************************************************************
                                    '#GT03092024: Buscar servicios a cobrar en los documentos de ingreso y de salida
                                    SplashScreenManager.Default.SetWaitFormDescription("Validando servicios en OC...")

                                    If pBodega.Es_Bodega_Fiscal Then
                                        For Each ingreso In lPolizasOC
                                            pListaServiciosOC = clsLnTrans_oc_servicios.Get_All_By_IdOrdenCompraEnc(ingreso.IdOrdenCompraEnc, pAcuerdoComercial.IdAcuerdoEnc)
                                            'pListaServiciosOC = clsLnTrans_oc_servicios.Get_All_By_IdOrdenCompraEnc(ingreso.IdOrdenCompraEnc, pAcuerdoComercial.IdAcuerdoEnc)
                                            '#GT17062024: por cada OC iteramos sus servicios y los agregamos al grid.
                                            If pListaServiciosOC IsNot Nothing Then
                                                If pListaServiciosOC.Count > 0 Then
                                                    vCobro_por_linea = 0
                                                    For Each ServicioOC In pListaServiciosOC

                                                        Dim pServicio = pAcuerdoComercial.lDetalle.Find(Function(x) x.IdAcuerdoEnc = ServicioOC.IdAcuerdo AndAlso
                                                                                                 x.Correlativo_detalleacuerdo = ServicioOC.Corre_detalleacuerdo)

                                                        If pServicio IsNot Nothing Then
                                                            If ServicioOC.Fecha_Servicio.Date >= pFechaDesde.Date AndAlso ServicioOC.Fecha_Servicio <= pFechaHasta.Date Then
                                                                '#GT06092024: en correo 06092024, Otto indica el valor del acuerdo * la cantidad de servicios que puede ser superior a 1
                                                                vCobro_por_linea = Math.Round((pServicio.Monto * ServicioOC.Cantidad), pDecimalesEnCobro)
                                                                'vErp = 1 '#GT02072024: cuando son servicios, monto es 1, y dias es 1
                                                                vErp = ServicioOC.Cantidad
                                                                DTGridDetalleServicios.Rows.Add(pServicio.IdAcuerdoEnc,
                                                                                               pServicio.IdAcuerdoDet,
                                                                                               pServicio.Codigo_producto,
                                                                                               vMoneda,
                                                                                               pServicio.Codigo_acuerdo,
                                                                                               pServicio.Descripcion,
                                                                                               pServicio.Correlativo_detalleacuerdo,
                                                                                               pServicio.Servicio,
                                                                                               pServicio.Numero_unidades,
                                                                                               pServicio.Monto,
                                                                                               pServicio.Porcentaje,
                                                                                               vCobro_por_linea,
                                                                                               vErp,
                                                                                               pServicio.Dias_eventos)
                                                            End If

                                                        End If

                                                    Next

                                                End If

                                            End If
                                        Next

                                    Else

                                        For Each ingreso In ListaTranOcEncReferencias
                                            pListaServiciosOC = clsLnTrans_oc_servicios.Get_All_By_IdOrdenCompraEnc(ingreso.IdOrdenCompraEnc, pAcuerdoComercial.IdAcuerdoEnc)
                                            If pListaServiciosOC IsNot Nothing Then
                                                If pListaServiciosOC.Count > 0 Then
                                                    vCobro_por_linea = 0
                                                    For Each ServicioOC In pListaServiciosOC

                                                        Dim pServicio = pAcuerdoComercial.lDetalle.Find(Function(x) x.IdAcuerdoEnc = ServicioOC.IdAcuerdo AndAlso
                                                                                                 x.Correlativo_detalleacuerdo = ServicioOC.Corre_detalleacuerdo)

                                                        If pServicio IsNot Nothing Then
                                                            '#GT07102024: los servicios deben pertenecer al mes de corte para cobrarse.
                                                            If ServicioOC.Fecha_Servicio.Date >= pFechaDesde.Date AndAlso ServicioOC.Fecha_Servicio.Date <= pFechaHasta.Date Then
                                                                vCobro_por_linea = Math.Round((pServicio.Monto * ServicioOC.Cantidad), pDecimalesEnCobro)
                                                                vErp = ServicioOC.Cantidad

                                                                DTGridDetalleServicios.Rows.Add(pServicio.IdAcuerdoEnc,
                                                                                               pServicio.IdAcuerdoDet,
                                                                                               pServicio.Codigo_producto,
                                                                                               vMoneda,
                                                                                               pServicio.Codigo_acuerdo,
                                                                                               pServicio.Descripcion,
                                                                                               pServicio.Correlativo_detalleacuerdo,
                                                                                               pServicio.Servicio,
                                                                                               pServicio.Numero_unidades,
                                                                                               pServicio.Monto,
                                                                                               pServicio.Porcentaje,
                                                                                               vCobro_por_linea,
                                                                                               vErp,
                                                                                               pServicio.Dias_eventos)

                                                            End If

                                                        End If

                                                    Next

                                                End If

                                            End If
                                        Next

                                    End If

                                    dgriDetallePreCuenta.DataSource = DTGridDetallePreCuenta
                                    dgridServiciosAsociados.DataSource = DTGridDetalleServicios

                                    If gvdetalleprecuenta.RowCount > 0 Then

                                        gvdetalleprecuenta.Columns("numero_orden").Group()
                                        gvdetalleprecuenta.Columns("idordencompraenc").Visible = False

                                        If chkVarianteCobro.Checked AndAlso chkAgruparPorProducto.Checked Then
                                            gvdetalleprecuenta.Columns("codigo_producto").Group()
                                        End If

                                        Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                                        With {.FieldName = "almacenaje",
                                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                                        .DisplayFormat = "Total: {0:N4}",
                                        .ShowInGroupColumnFooter = gvdetalleprecuenta.Columns("almacenaje")}
                                        gvdetalleprecuenta.GroupSummary.Add(item)

                                        Dim item_manejo As GridGroupSummaryItem = New GridGroupSummaryItem() _
                                        With {.FieldName = "manejo",
                                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                                        .DisplayFormat = "Total: {0:N4}",
                                        .ShowInGroupColumnFooter = gvdetalleprecuenta.Columns("manejo")}
                                        gvdetalleprecuenta.GroupSummary.Add(item_manejo)

                                        gvdetalleprecuenta.Columns("manejo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                                        gvdetalleprecuenta.Columns("manejo").SummaryItem.DisplayFormat = "{0:n4}"
                                        gvdetalleprecuenta.Columns("manejo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                        gvdetalleprecuenta.Columns("manejo").DisplayFormat.FormatString = "{0:n4}"

                                        gvdetalleprecuenta.Columns("almacenaje").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                                        gvdetalleprecuenta.Columns("almacenaje").SummaryItem.DisplayFormat = "{0:n4}"
                                        gvdetalleprecuenta.Columns("almacenaje").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                        gvdetalleprecuenta.Columns("almacenaje").DisplayFormat.FormatString = "{0:n4}"

                                        gvdetalleprecuenta.BestFitColumns(True)
                                        gvdetalleprecuenta.ExpandAllGroups()
                                        gvdetalleprecuenta.OptionsBehavior.Editable = False
                                        xtratabPrecuenta.SelectedTabPageIndex = 1
                                        gvdetalleprecuenta.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways

                                    End If

                                    pCalculoProcesado = True
                                Else
                                    SplashScreenManager.CloseForm(False)
                                    Throw New Exception("El acuerdo comercial no tiene servicios con prioridad, para aplicar cobro automatico.")
                                End If

                            End If

                        Else
                            SplashScreenManager.CloseForm(False)
                            Throw New Exception("Error: El acuerdo comercial no tiene servicios para cobranza. Valide la lista de acuerdos comerciales.")
                        End If

                    Else
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("Error: No se encontró un acuerdo comercial.")
                    End If

                End If

            End If

        Catch ex As Exception

            pCalculoProcesado = False
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try
    End Sub


    Private Sub chkConsolidador_CheckedChanged(sender As Object, e As EventArgs) Handles chkConsolidados.CheckedChanged

        Try

            If chkConsolidados.Checked Then

                gbErrores.Visible = False

                lblConsolidador.Text = "Consolidador"
                chkControlPesoBruto.Enabled = False
                chkVarianteCobro.Enabled = False

                lblFechaInicial.Visible = False
                lblFechaFinal.Visible = False
                lblPolizaSalida.Visible = True
                dtpFechaDesde.Visible = False
                dtpfechaHasta.Visible = False
                cmbPolizasOC.Visible = True
                cmbPolizasPE.Visible = True


                txtValorGeneral.Enabled = False
                txtValorAduana.Enabled = False
                txtPesoTotal.Enabled = False

                dtFechaIngreso.Enabled = False
                dtFechaSalida.Enabled = False

                '#GT24072024: cobro con variante activo para no consolidadores
                chkVarianteCobro.Checked = False
                chkVarianteCobro.Enabled = False
                'cmdCerrarVarianteCobro.Enabled = False

                chkAgruparPorProducto.Enabled = False

                Select Case Modo

                    Case TipoTrans.Nuevo

                        cmdCorreccionPoliza.Enabled = True
                        cmbCliente.EditValue = 0
                        cmbPropietario.EditValue = 0
                        cmbPolizasOC.EditValue = 0
                        chkEstimacionCobro.Enabled = True
                        chkEstimacionCobro.Checked = False

                        Listar_Propietarios()
                        Listar_Clientes()
                        cmbPropietario.Focus()

                    Case TipoTrans.Editar
                        cmdCorreccionPoliza.Enabled = False
                        txtNoDocumento.Focus()
                        cmbPropietario.Enabled = False
                        cmbCliente.Enabled = False
                        cmbPolizasOC.Enabled = False
                        cmbPolizasPE.Enabled = False
                        chkEstimacionCobro.Enabled = False
                        Listar_Propietarios()
                        Listar_Clientes()

                        gvDetalleServicios.OptionsBehavior.Editable = False
                        gvdetalleprecuenta.OptionsBehavior.Editable = False

                End Select

                pnValores.Visible = True
                DTGridDetalleServicios.Clear()
                dgridServiciosAsociados.DataSource = DTGridDetalleServicios
                dgriDetallePreCuenta.DataSource = DTGridDetallePreCuenta

            Else

                gbErrores.Visible = True

                lblConsolidador.Text = "Propietario"
                chkControlPesoBruto.Enabled = True

                cmdCorreccionPoliza.Enabled = False
                lblFechaInicial.Visible = True
                lblFechaFinal.Visible = True
                lblPolizaSalida.Visible = False
                dtpFechaDesde.Visible = True
                dtpfechaHasta.Visible = True
                '#GT21082024: no ocultar el combo del cliente, 
                'lblCliente.Visible = False
                'cmbCliente.Visible = False
                '#GT18062024: mostrar polizas de ingreso cuando se factura directamente a un ingreso
                cmbPolizasOC.Visible = True
                cmbPolizasPE.Visible = False
                pnValores.Visible = False

                dgridServiciosAsociados.DataSource = DTGridDetalleServicios
                dgriDetallePreCuenta.DataSource = DTGridDetallePreCuenta
                cmbPropietario.Focus()
                chkEstimacionCobro.Enabled = False

                chkAgruparPorProducto.Enabled = True

                Select Case Modo

                    Case TipoTrans.Nuevo

                        chkVarianteCobro.Enabled = True
                        chkVarianteCobro.Checked = False

                    Case TipoTrans.Editar

                        chkVarianteCobro.Checked = BePrefacturaEnc.Variante_cobro
                        chkVarianteCobro.Enabled = False

                End Select

            End If

            txtTotalFacturacion.Enabled = False

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdGuardar.ItemClick
        Try
            cmdGuardar.Enabled = False
            If pCalculoProcesado Then
                Grabar()
            Else
                XtraMessageBox.Show("No ha realizado el cálculo de cobro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            cmdGuardar.Enabled = True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Grabar()
        Try

            If Datos_Correctos() Then

                SplashScreenManager.CloseForm(False)

                If XtraMessageBox.Show("¿Guardar PreFactura?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

                    If Guardar(False) Then

                        SplashScreenManager.CloseForm(False)

                        '#GT05092024: si es una cotización no se requiere enviar al ERP.
                        If Not chkEstimacionCobro.Checked Then

                            If XtraMessageBox.Show("Se guardó la prefactura. ¿Desea enviar para facturación?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                If Enviar_Prefactura_a_ERP() Then

                                    If Not InvokeListarPrefacturas Is Nothing Then InvokeListarPrefacturas.Invoke()
                                    DialogResult = DialogResult.OK
                                    Close()
                                Else
                                    If Not InvokeListarPrefacturas Is Nothing Then InvokeListarPrefacturas.Invoke()
                                    Close()

                                End If
                            Else
                                If Not InvokeListarPrefacturas Is Nothing Then InvokeListarPrefacturas.Invoke()
                                Close()
                            End If

                        Else
                            If Not InvokeListarPrefacturas Is Nothing Then InvokeListarPrefacturas.Invoke()
                            Close()
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function Enviar_Prefactura_a_ERP() As Boolean

        Try

            Enviar_Prefactura_a_ERP = False

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Enviando data a ERP.")

            Dim CobroERP As New clsBeTrans_prefactura_erp
            Dim DetalleERP As New clsBeTrans_prefactura_erp_det

            Dim Propietario As New clsBePropietarios
            Dim Cliente As New clsBeCliente
            Dim Propietario_facturar As New clsBePropietarios


            '#GT07082024: cargar al propietario
            Dim pIdPropietario = clsLnPropietarios.Get_IdPropietario(BePrefacturaEnc.IdBodega, BePrefacturaEnc.IdPropietarioBodega)
            Propietario = clsLnPropietarios.Get_Single_By_IdEmpresa(AP.Empresa.IdEmpresa, pIdPropietario)

            If BePrefacturaEnc.IdClienteBodega > 0 Then
                Dim pIdCliente = clsLnCliente.Get_IdPropietario(pBodega.IdBodega, BePrefacturaEnc.IdClienteBodega)
                Cliente = clsLnCliente.Get_Single_By_IdEmpresa(AP.Empresa.IdEmpresa, pIdCliente)

                If Cliente IsNot Nothing Then
                    Propietario_facturar = clsLnPropietarios.GetSingle(Cliente.IdPropietario)
                End If

            Else
                Propietario_facturar = Nothing
            End If

            Dim vFechaInicial As Date = BePrefacturaEnc.Fecha_desde.Date
            Dim vFechaFinal As Date = BePrefacturaEnc.Fecha_hasta.Date

            CobroERP.IdPrefacturaEnc = BePrefacturaEnc.IdTransPrefacturaEnc

            If Propietario_facturar IsNot Nothing Then
                CobroERP.Nit = Propietario_facturar.NIT
                CobroERP.IdCliente_facturar = Propietario_facturar.IdPropietario
            Else
                CobroERP.IdCliente_facturar = Propietario.IdPropietario
                CobroERP.Nit = Propietario.NIT
            End If

            CobroERP.Codigo_acuerdo = listBePrefacturaDet(0).Codigo_acuerdo_enc
            CobroERP.IdCliente = Propietario.IdPropietario
            CobroERP.Moneda = listBePrefacturaDet(0).Moneda
            CobroERP.Periodo = "Del " & vFechaInicial + " Al " + vFechaFinal

            If BePrefacturaEnc.Es_consolidador Then
                CobroERP.Mercaderia = "TO " & BePrefacturaEnc.Poliza_oc_numero_orden + " -ID " + BePrefacturaEnc.Poliza_pe_numero_orden
            Else
                CobroERP.Mercaderia = "NOMBRE DEL ACUERDO COMERCIAL"
            End If

            CobroERP.TipoCambio = IIf(BePrefacturaEnc.Tipo_Cambio > 0, BePrefacturaEnc.Tipo_Cambio, 1.0)
            CobroERP.Observaciones = BePrefacturaEnc.Observacion


            CobroERP.Detalle = New List(Of clsBeTrans_prefactura_erp_det)

            For Each Detalle In listBePrefacturaDet

                DetalleERP = New clsBeTrans_prefactura_erp_det
                'DetalleERP.IdPrefacturaDet = Detalle.IdTransPrefacturaDet
                DetalleERP.codigoproducto = Detalle.Codigo_producto_acuerdo_det
                DetalleERP.corre_cbdetacuerdosservicios = Detalle.Correlativo_detalle_acuerdo
                DetalleERP.monto = Math.Round(Detalle.Monto_Erp, pDecimalesEnCobro)
                DetalleERP.dias = Detalle.Dias_eventos
                CobroERP.Detalle.Add(DetalleERP)

            Next

            Dim strserialize As String = JsonConvert.SerializeObject(CobroERP)
            'Debug.Write(strserialize)

            Dim pTimeOut = clsBD.Instancia.TimeOutConBD

            If clsLnTrans_prefactura_enc.Insertar_Prefactura_ERP(CobroERP, pTimeOut, BePrefacturaEnc.User_agr) Then
                Enviar_Prefactura_a_ERP = True
                clsLnTrans_prefactura_enc.Actualizar_by_Proceso_ERP(CobroERP)
            End If


            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function


    Dim Grid_Tiene_Error As Boolean
    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            Dim vDetalleCorrecto As Boolean = False


            If chkConsolidados.Checked Then

                If cmbPropietario.EditValue = -1 Then
                    XtraMessageBox.Show("Seleccione un propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cmbPropietario.Focus()
                ElseIf cmbPolizasOC.EditValue = -1 Then
                    XtraMessageBox.Show("Ingrese número de orden póliza ingreso.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cmbPolizasOC.Focus()
                ElseIf cmbPolizasPE.EditValue = -1 Then
                    XtraMessageBox.Show("Ingrese número de orden póliza salida.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cmbPolizasPE.Focus()
                ElseIf gvDetalleServicios.RowCount = 0 Then
                    XtraMessageBox.Show("No hay servicio agregado para la prefactura.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf Grid_Tiene_Error Then
                    XtraMessageBox.Show("El detalle del documento contiene errores, debe corregirlos antes de guardar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    Datos_Correctos = True
                End If

            Else

                If cmbPropietario.EditValue = -1 Then
                    XtraMessageBox.Show("Seleccione un propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cmbPropietario.Focus()
                ElseIf dtpFechaDesde.Value = Now Then
                    XtraMessageBox.Show("Seleccione una fecha inicial.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    dtpFechaDesde.Focus()
                ElseIf dtpfechaHasta.Value = Now Then
                    XtraMessageBox.Show("Seleccione una fecha final.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    dtpfechaHasta.Focus()
                ElseIf gvDetalleServicios.RowCount = 0 Then
                    XtraMessageBox.Show("No hay servicio agregado para la prefactura.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf gvdetalleprecuenta.RowCount = 0 Then
                    XtraMessageBox.Show("No hay detalle mensual para la prefactura.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf Grid_Tiene_Error Then
                    XtraMessageBox.Show("El detalle del documento contiene errores, debe corregirlos antes de guardar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    Datos_Correctos = True
                End If

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Function Guardar(ByVal EsActualizacion As Boolean) As Boolean

        Guardar = False

        Try

            Dim vContador As Integer = 0
            BePrefacturaEnc.IdBodega = cmbBodega.EditValue
            BePrefacturaEnc.IdPropietarioBodega = cmbPropietario.EditValue
            BePrefacturaEnc.IdClienteBodega = IdClienteBodega 'cmbCliente.EditValue
            BePrefacturaEnc.IdOrdenCompraEnc = BeOCEnc.IdOrdenCompraEnc
            '#GT07052024: precuenta=0, prefactura=1
            '#GT05092024: al guardar si se envia al ERP, se determina que es prefactura.
            BePrefacturaEnc.Cobro_peso_bruto = chkControlPesoBruto.Checked
            BePrefacturaEnc.Tipo_Cambio = txtTipoCambio.EditValue
            BePrefacturaEnc.IdOrdenCompraPol = pPolizaIngresoExiste.IdOrdenCompraPol
            BePrefacturaEnc.Poliza_oc_numero_orden = pPolizaIngresoExiste.numero_orden

            If chkEstimacionCobro.Checked Then

                BePrefacturaEnc.Valor_Aduana = txtValorAduana.Value
                BePrefacturaEnc.Valor_General = txtValorGeneral.Value

                BePrefacturaEnc.IdOrdenPedidoEnc = 0
                BePrefacturaEnc.IdOrdenPedidoPol = 0
                BePrefacturaEnc.Poliza_pe_numero_orden = encabezado_duca.Numero_Orden
            Else
                BePrefacturaEnc.IdOrdenPedidoEnc = pPolizaSalidaExiste.IdOrdenPedidoEnc
                BePrefacturaEnc.IdOrdenPedidoPol = pPolizaSalidaExiste.IdOrdenPedidoPol
                BePrefacturaEnc.Poliza_pe_numero_orden = pPolizaSalidaExiste.numero_orden

                BePrefacturaEnc.Valor_Aduana = pPolizaSalidaExiste.Total_valoraduana
                BePrefacturaEnc.Valor_General = pPolizaSalidaExiste.total_general

            End If


            BePrefacturaEnc.User_agr = AP.UsuarioAp.IdUsuario
            BePrefacturaEnc.Fec_agr = Now
            BePrefacturaEnc.User_mod = AP.UsuarioAp.IdUsuario
            BePrefacturaEnc.Fec_mod = Now
            BePrefacturaEnc.Anulada = 0
            BePrefacturaEnc.Observacion = txtObservacion.Text

            If chkConsolidados.Checked Then
                BePrefacturaEnc.Fecha_desde = dtFechaIngreso.EditValue
                BePrefacturaEnc.Fecha_hasta = dtFechaSalida.EditValue
                BePrefacturaEnc.Es_consolidador = True
            Else
                BePrefacturaEnc.Fecha_desde = dtpFechaDesde.Value
                BePrefacturaEnc.Fecha_hasta = dtpfechaHasta.Value
                BePrefacturaEnc.Es_consolidador = False
            End If

            '#GT05092024: se guardan los valores de poliza propensos a ser modificados del original
            BePrefacturaEnc.Valor_Peso = txtPesoTotal.Value
            BePrefacturaEnc.Variante_cobro = chkVarianteCobro.Checked
            BePrefacturaEnc.Agrupar_producto = chkAgruparPorProducto.Checked

            '#GT07052024: tabla con los acuerdos, servicios y valores a facturar
            If gvDetalleServicios.RowCount > 0 Then

                listBePrefacturaDet = New List(Of clsBeTrans_prefactura_det)

                For i As Integer = 0 To gvDetalleServicios.RowCount - 1

                    BePrefacturaDet = New clsBeTrans_prefactura_det
                    BePrefacturaDet.IdAcuerdoEnc = CInt(gvDetalleServicios.GetRowCellValue(i, "IdAcuerdoEnc"))

                    If BePrefacturaDet.IdAcuerdoEnc > 0 Then
                        BePrefacturaDet.Codigo_acuerdo_enc = CInt(gvDetalleServicios.GetRowCellValue(i, "codigo_acuerdo"))
                        BePrefacturaDet.Codigo_producto_acuerdo_det = (gvDetalleServicios.GetRowCellValue(i, "codigo_producto"))
                        BePrefacturaDet.IdAcuerdoDet = CInt(gvDetalleServicios.GetRowCellValue(i, "IdAcuerdoDet"))
                        BePrefacturaDet.Moneda = (gvDetalleServicios.GetRowCellValue(i, "moneda"))
                        BePrefacturaDet.Correlativo_detalle_acuerdo = CInt(gvDetalleServicios.GetRowCellValue(i, "correlativo_detalleacuerdo"))
                        BePrefacturaDet.Numero_unidades_acuerdo_det = CInt(gvDetalleServicios.GetRowCellValue(i, "numero_unidades"))
                        BePrefacturaDet.Servicio = (gvDetalleServicios.GetRowCellValue(i, "servicio"))
                        BePrefacturaDet.Descripcion = (gvDetalleServicios.GetRowCellValue(i, "nombre_acuerdo"))
                        BePrefacturaDet.Monto = CDec(gvDetalleServicios.GetRowCellValue(i, "monto"))
                        BePrefacturaDet.Porcentaje = CDec(gvDetalleServicios.GetRowCellValue(i, "porcentaje"))
                        BePrefacturaDet.Dias_eventos = CInt(gvDetalleServicios.GetRowCellValue(i, "dias_eventos"))
                        BePrefacturaDet.Valor = Math.Round(CDec(gvDetalleServicios.GetRowCellValue(i, "total")), pDecimalesEnCobro)
                        BePrefacturaDet.Monto_Erp = Math.Round(CDec(gvDetalleServicios.GetRowCellValue(i, "erp")), pDecimalesEnCobro)
                        BePrefacturaDet.User_agr = AP.UsuarioAp.IdUsuario
                        BePrefacturaDet.Fec_agr = Now
                        BePrefacturaDet.User_mod = AP.UsuarioAp.IdUsuario
                        BePrefacturaDet.Fec_mod = Now
                        BePrefacturaDet.Activo = 1


                        listBePrefacturaDet.Add(BePrefacturaDet)
                    End If

                Next

            End If

            If Not chkConsolidados.Checked Then

                '#GT07052024: tabla con el detalle de pólizas calculadas para clientes mensuales.
                If gvdetalleprecuenta.RowCount > 0 Then

                    listBePrefacturaMov = New List(Of clsBeTrans_prefactura_mov)

                    For i As Integer = 0 To gvdetalleprecuenta.RowCount - 1

                        Dim BePrefacturaMov As New clsBeTrans_prefactura_mov

                        BePrefacturaMov.IdOrdenCompraEnc = CInt(gvdetalleprecuenta.GetRowCellValue(i, "idordencompraenc"))

                        If Not (IsDBNull(gvdetalleprecuenta.GetRowCellValue(i, "numero_orden"))) Then
                            BePrefacturaMov.Poliza_oc_numero_orden = gvdetalleprecuenta.GetRowCellValue(i, "numero_orden")
                        End If

                        If chkVarianteCobro.Checked AndAlso chkAgruparPorProducto.Checked Then
                            If Not (IsDBNull(gvdetalleprecuenta.GetRowCellValue(i, "codigo_producto"))) Then
                                BePrefacturaMov.Codigo_producto = gvdetalleprecuenta.GetRowCellValue(i, "codigo_producto")
                            End If
                        End If

                        If Not (IsDBNull(gvdetalleprecuenta.GetRowCellValue(i, "almacenaje"))) Then
                            BePrefacturaMov.Almacenaje = Math.Round(CDbl(gvdetalleprecuenta.GetRowCellValue(i, "almacenaje")), pDecimalesEnCobro)
                        End If

                        If Not (IsDBNull(gvdetalleprecuenta.GetRowCellValue(i, "manejo"))) Then
                            BePrefacturaMov.Manejo = Math.Round(CDbl(gvdetalleprecuenta.GetRowCellValue(i, "manejo")), pDecimalesEnCobro)
                        End If

                        If Not (IsDBNull(gvdetalleprecuenta.GetRowCellValue(i, "unidades"))) Then
                            BePrefacturaMov.Unidades = CDbl(gvdetalleprecuenta.GetRowCellValue(i, "unidades"))
                        End If


                        If Not (IsDBNull(gvdetalleprecuenta.GetRowCellValue(i, "valor_total"))) Then
                            BePrefacturaMov.Valor_total = Math.Round(CDbl(gvdetalleprecuenta.GetRowCellValue(i, "valor_total")), pDecimalesEnCobro)
                        End If

                        BePrefacturaMov.Fecha_cobro = CDate(gvdetalleprecuenta.GetRowCellValue(i, "fecha"))

                        BePrefacturaMov.Fec_agr = Now
                        BePrefacturaMov.User_agr = AP.UsuarioAp.IdUsuario
                        BePrefacturaMov.Fec_mod = Now
                        BePrefacturaMov.User_mod = AP.UsuarioAp.IdUsuario
                        BePrefacturaMov.Activo = 1


                        '#GT30082024: existe la ultima linea que viene vacia, aunque el grid maneja el -1
                        If BePrefacturaMov.IdOrdenCompraEnc > 0 Then
                            listBePrefacturaMov.Add(BePrefacturaMov)

                        End If


                    Next

                End If

            End If

            clsLnTrans_prefactura_enc.Guardar_Datos(BePrefacturaEnc, listBePrefacturaDet, listBePrefacturaMov)

            'If Not EsActualizacion Then
            '    clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231656: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " guardó el IdOrdenCompraEnc: " & gBeOrdenCompra.IdOrdenCompraEnc)
            'Else
            '    clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231656A: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " actualizó el IdOrdenCompraEnc: " & gBeOrdenCompra.IdOrdenCompraEnc)
            'End If

            Guardar = True

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function
    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged

        Try

            If cmbPropietario.EditValue > 0 Then

                Dim fila As Object = cmbPropietario.GetSelectedDataRow
                If fila IsNot Nothing Then
                    IdPropietario = fila.Item("IdPropietario")
                End If

                Llena_Acuerdos_By_IdCliente()
                Llenar_Lista_Polizas_OC()
                If chkConsolidados.Checked Then
                    Llenar_Lista_Polizas_PE()
                End If

                cmbCliente.Focus()

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llenar_Lista_Polizas_OC()
        Try
            Dim DT As New DataTable

            Select Case Modo

                '#GT10072024: validar si bodega es fiscal o general para listar polizas o docs. de ingreso.
                Case TipoTrans.Nuevo

                    If pBodega.Es_Bodega_Fiscal Then
                        DT = clsLnTrans_oc_pol.Get_All_By_IdPropietarioBodega_And_IdBodega(cmbPropietario.EditValue, AP.Bodega.IdBodega)
                    Else
                        DT = clsLnTrans_oc_enc.Get_All_By_IdPropietarioBodega_And_IdBodega(cmbPropietario.EditValue, AP.Bodega.IdBodega)
                    End If


                Case TipoTrans.Editar

                    If pBodega.Es_Bodega_Fiscal Then
                        DT = clsLnTrans_oc_pol.Get_All_By_IdPropietarioBodega_And_IdBodega(cmbPropietario.EditValue, BePrefacturaEnc.IdBodega)
                    Else
                        DT = clsLnTrans_oc_enc.Get_All_By_IdPropietarioBodega_And_IdBodega(cmbPropietario.EditValue, BePrefacturaEnc.IdBodega)
                    End If


            End Select

            cmbPolizasOC.Properties.ValueMember = "IdOrdenCompraEnc"
            cmbPolizasOC.Properties.DisplayMember = "numero_orden"
            cmbPolizasOC.Properties.DataSource = DT
            cmbPolizasOC.Properties.PopupFormSize = New Size(900, 200)
            cmbPolizasOC.Properties.BestFitMode = BestFitMode.BestFit
            cmbPolizasOC.Properties.NullText = String.Empty
            cmbPolizasOC.Properties.TextEditStyle = TextEditStyles.Standard
            cmbPolizasOC.Properties.View.OptionsFind.AlwaysVisible = True
            cmbPolizasOC.Properties.View.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always
            cmbPolizasOC.Properties.View.OptionsFind.SearchInPreview = False
            cmbPolizasOC.Properties.PopupView.OptionsFind.FindFilterColumns = "numero_orden;codigo_poliza"
            cmbPolizasOC.Properties.PopulateViewColumns()


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llenar_Lista_Polizas_PE()
        Try
            Dim DT As New DataTable


            Select Case Modo

                Case TipoTrans.Nuevo

                    If AP.Bodega.Es_Bodega_Fiscal AndAlso chkConsolidados.Checked Then
                        DT = clsLnTrans_pe_pol.Get_All_By_IdPropietarioBodega_And_IdBodega(cmbPropietario.EditValue, AP.Bodega.IdBodega)
                    End If

                Case TipoTrans.Editar
                    DT = clsLnTrans_pe_pol.Get_All_By_IdPropietarioBodega_And_IdBodega(cmbPropietario.EditValue, BePrefacturaEnc.IdBodega)
            End Select

            cmbPolizasPE.Properties.ValueMember = "IdOrdenPedidoEnc"
            cmbPolizasPE.Properties.DisplayMember = "numero_orden"
            cmbPolizasPE.Properties.DataSource = DT
            cmbPolizasPE.Properties.PopupFormSize = New Size(900, 200)
            cmbPolizasPE.Properties.BestFitMode = BestFitMode.BestFit
            cmbPolizasPE.Properties.NullText = String.Empty
            cmbPolizasPE.Properties.TextEditStyle = TextEditStyles.Standard
            cmbPolizasPE.Properties.View.OptionsFind.AlwaysVisible = True
            cmbPolizasPE.Properties.View.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always
            cmbPolizasPE.Properties.View.OptionsFind.SearchInPreview = False
            cmbPolizasPE.Properties.PopupView.OptionsFind.FindFilterColumns = "numero_orden;codigo_poliza"
            cmbPolizasPE.Properties.PopulateViewColumns()


            If cmbPolizasPE.Properties.View.Columns.Count > 0 Then
                cmbPolizasPE.Properties.View.Columns(0).Visible = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbPolizasOC_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPolizasOC.EditValueChanged
        Try
            If cmbPolizasOC.EditValue > 0 Then

                Dim fila As Object = cmbPolizasOC.GetSelectedDataRow

                If fila Is Nothing Then
                    Throw New Exception("Error_09042024_1220: No se pudo obtener el numero de orden correctamente.")
                Else

                    EsIngreso = True
                    pIdOrdenCompraEnc_pol = 0
                    pIdOrdenCompraEnc_pol = fila.Item("IdOrdenCompraEnc")
                    oc_pol_numero_orden = fila.Item("numero_orden")
                    Scan_Poliza(oc_pol_numero_orden, pIdOrdenCompraEnc_pol)
                    pBuscarPolizaEspecifica = True

                    If chkConsolidados.Checked Then
                        cmbPolizasPE.Focus()
                    End If

                    '#GT23072024: si hay variante, mostrar los productos que pertenezcan a la oc
                    If chkVarianteCobro.Checked Then
                        DTProductos.Clear()
                        'Listar_Poliza_Productos()
                        '#GT07082024: no mostrar el combo de productos.
                    End If

                End If
            Else
                pBuscarPolizaEspecifica = False
                pPolizaIngresoExiste = Nothing
                BeOCEnc = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Dim DTProductos As New DataTable
    'Private Sub Listar_Poliza_Productos()

    '    Try

    '        If cmbPolizasOC.EditValue > 0 Then

    '            DTProductos = clsLnTrans_re_det.Get_Productos_By_IdOrdenCompraEnc(pIdOrdenCompraEnc_pol)


    '            If DTProductos IsNot Nothing AndAlso DTProductos.Rows.Count > 0 Then


    '                cmbProductosPoliza.Properties.ValueMember = "IdProductoBodega"
    '                cmbProductosPoliza.Properties.DisplayMember = "codigo_producto"
    '                cmbProductosPoliza.Properties.DataSource = DTProductos
    '                cmbProductosPoliza.Properties.PopupFormSize = New Size(900, 200)
    '                cmbProductosPoliza.Properties.BestFitMode = BestFitMode.BestFit
    '                cmbProductosPoliza.Properties.NullText = String.Empty
    '                cmbProductosPoliza.Properties.TextEditStyle = TextEditStyles.Standard
    '                cmbProductosPoliza.Properties.View.OptionsFind.AlwaysVisible = True
    '                cmbProductosPoliza.Properties.View.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always
    '                cmbProductosPoliza.Properties.View.OptionsFind.SearchInPreview = False
    '                cmbProductosPoliza.Properties.PopupView.OptionsFind.FindFilterColumns = "codigo_producto;nombre_producto"
    '                cmbProductosPoliza.Properties.PopulateViewColumns()


    '                If cmbProductosPoliza.Properties.View.Columns.Count > 0 Then
    '                    cmbProductosPoliza.Properties.View.Columns(0).Visible = False
    '                End If

    '            Else
    '                Throw New Exception("No se encontraron productos asociados a la póliza seleccionada.")
    '            End If

    '        End If

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub

    Private Sub cmbPolizasPE_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPolizasPE.EditValueChanged
        Try

            Select Case Modo

                Case TipoTrans.Nuevo

                    If cmbPolizasPE.EditValue > 0 Then
                        Dim fila As Object = cmbPolizasPE.GetSelectedDataRow
                        If fila Is Nothing Then
                            Throw New Exception("Error_09042024_1220: No se pudo obtener el numero de orden correctamente.")
                        Else
                            EsIngreso = False
                            pe_pol_numero_orden = fila.Item("numero_orden")
                            Scan_Poliza(pe_pol_numero_orden)
                            cmbCliente.Focus()

                        End If
                    End If
            End Select


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdImpresionPrecuenta_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImpresionPrecuenta.ItemClick

        Try

            Imprimir_Precuenta()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub cmdImpresionDetallePrecuenta_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImpresionDetallePrecuenta.ItemClick
        Try

            Imprimir_Detalle_Precuenta()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Imprimir_Precuenta()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = dgridServiciosAsociados
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Imprimir_Detalle_Precuenta()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = dgriDetallePreCuenta
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "TOMWMS" &
                              vbNewLine & "Precuenta " &
                              vbNewLine & vClienteFacturar &
                                vbNewLine & "BODEGA: " & AP.NomBodega

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    '#GT06062024: al hacer clic seleccionar todo para facilitar edición.
    Private Sub txtPesoTotal_Click(sender As Object, e As EventArgs) Handles txtPesoTotal.Click
        txtPesoTotal.SelectAll()
    End Sub

    Private Sub txtValorAduana_Click(sender As Object, e As EventArgs) Handles txtValorAduana.Click
        txtValorAduana.SelectAll()
    End Sub

    Private Sub txtValorGeneral_Click(sender As Object, e As EventArgs) Handles txtValorGeneral.Click
        txtValorGeneral.SelectAll()
    End Sub

    Private Function cmdCorreccionPoliza_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdCorreccionPoliza.ItemClick

        Dim us As New clsBeUsuario
        Dim ms As New clsBeMenu_sistema
        Dim clave As String
        Dim BeMoticoCorreccionBodega As New clsBeTrans_oc_pol_motivo_correccion_bodega

        Try

            BeMoticoCorreccionBodega.IdBodega = cmbBodega.EditValue

            ms.IdMenu = e.Link.KeyTip
            clsLnMenu_sistema.GetSingle(ms)

            If (ms.Solicitar_clave_autorizacion) Then

                us.IdUsuario = AP.UsuarioAp.IdUsuario
                clsLnUsuario.GetSingle(us)

                Try

                    clave = clsPublic.Desencriptar(us.Clave_autorizacion)

                    If (clave = String.Empty) Then Throw New Exception

                Catch ex As Exception
                    MsgBox("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.") : Return False
                End Try

                Dim frmlog As New frmAjusteLogin() With {.clave = clave}

                If frmlog.ShowDialog() <> DialogResult.Yes Then
                    frmlog.Dispose() : Return False
                Else
                    'Return True
                End If

                frmlog.Dispose()

                Using MA As New frmMotivo_CorrecionList()

                    With MA

                        .Modo = frmMotivo_CorrecionList.pModo.Seleccion
                        .BeMotivoCorreccion.IdBodega = BeMoticoCorreccionBodega.IdBodega

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                        End If

                        If .ShowDialog() = DialogResult.OK Then
                            clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202410060900: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " corrige prefactura de póliza con el idmotivo: " & MA.BeMotivoCorreccion.IdMotivoCorreccion & " para numero_orden: " & pPolizaSalidaExiste.numero_orden)

                            txtValorAduana.Enabled = True
                            txtValorGeneral.Enabled = True
                            txtPesoTotal.Enabled = True
                            dtFechaSalida.Enabled = True
                            txtPesoTotal.Focus()
                        Else
                            txtValorAduana.Enabled = False
                            txtValorGeneral.Enabled = False
                            txtPesoTotal.Enabled = False
                            dtFechaSalida.Enabled = False
                        End If

                    End With

                End Using

                SplashScreenManager.CloseForm(False)

                Return True

            Else
                Return True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try

    End Function

    Private Sub txtTipoCambio_Click(sender As Object, e As EventArgs) Handles txtTipoCambio.Click
        txtTipoCambio.SelectAll()
    End Sub

    Public Sub ObtenerTipoCambioDia()

        Try
            ' Crear una instancia del servicio web
            Dim tipoCambioService As New srvTipoCambio.TipoCambioSoapClient()

            ' Llamar al método TipoCambioDia
            Dim resultado As srvTipoCambio.InfoVariable = tipoCambioService.TipoCambioDia()
            TasaCambioBanGuat = Math.Round(resultado.CambioDolar(0).referencia(), 2)
            txtTipoCambio.Text = Math.Round(resultado.CambioDolar(0).referencia(), 2)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtTipoCambio_EditValueChanged(sender As Object, e As EventArgs) Handles txtTipoCambio.EditValueChanged
        lblTipoCambioOrigen.Visible = True
        If txtTipoCambio.EditValue = TasaCambioBanGuat Then
            lblTipoCambioOrigen.Text = "Banguat"
            lblTipoCambioOrigen.ForeColor = Color.DarkGreen
        Else
            lblTipoCambioOrigen.Text = "Manual"
            lblTipoCambioOrigen.ForeColor = Color.Firebrick
        End If
    End Sub

    'Class clsBeErp

    '    Public IdPrefacturaEnc As Integer
    '    Public Nit As String
    '    Public IdCliente_facturar As Integer
    '    Public Codigo_acuerdo As String
    '    Public IdCliente As Integer
    '    Public Moneda As String
    '    Public Periodo As String
    '    Public Mercaderia As String
    '    Public TipoCambio As Double
    '    Public Observaciones As String
    '    Public Detalle As List(Of clsBeDetalle)
    'End Class

    'Class clsBeDetalle
    '    Public IdPrefacturaDet As Integer
    '    Public Codigo_producto As String
    '    Public Correlativo As Integer
    '    Public Monto As Double
    '    Public Dias_eventos As Integer
    'End Class

    Private Sub chkVarianteCobro_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkVarianteCobro.CheckedChanged
        Try

            If chkVarianteCobro.Checked Then

                'chkAgruparPorProducto.Enabled = True

                chkMantenerDolares.Enabled = True
                '#GT07082024: por ahora no mostrar el combo ni cargarlo
                'cmbProductosPoliza.Visible = False
                DTProductos.Clear()

                '#GT07082024: por ahora no mostrar el combo ni cargarlo
                'Listar_Poliza_Productos()
                '#GT25072024: activar el grid temporal
                'Set_Data_Table_Grid_Temporal()
                'dgridTemporal.DataSource = DTGridTemporal
                'xtratabPrecuenta.TabPages.Item(2).PageVisible = True
                'xtratabPrecuenta.SelectedTabPageIndex = 2

            Else
                'chkAgruparPorProducto.Checked = False
                'chkAgruparPorProducto.Enabled = False
                'lblProductoPoliza.Visible = False
                'cmbProductosPoliza.Visible = False
                DTProductos.Clear()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Dim pCodigo_Barra As String = ""
    'Private Sub cmbProductosPoliza_EditValueChanged(sender As Object, e As EventArgs)

    '    Try

    '        If cmbProductosPoliza.EditValue <> 0 Then

    '            pBuscarProductoEspecifico = True
    '            Dim fila As Object = cmbProductosPoliza.GetSelectedDataRow

    '            If fila Is Nothing Then
    '                Throw New Exception("Error_23072024: No se pudo obtener el producto.")
    '            Else
    '                pCodigo_Barra = fila.Item("codigo_producto")
    '            End If

    '        Else
    '            pCodigo_Barra = ""
    '            pBuscarProductoEspecifico = False
    '        End If

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub

    Private Sub mnuPrefactura_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuPrefactura.ItemClick

        Try

            Dim Rep As New rptPrefactura
            Rep.DataSource = clsLnTrans_prefactura_enc.Get_Reporte_By_IdPrefactura(BePrefacturaEnc.IdTransPrefacturaEnc)
            Rep.DataMember = "Result"
            Rep.RequestParameters = False
            If clsLnEmpresa.GetImagen(AP.IdEmpresa) Is Nothing Then
                Rep.XrLogo.Image = Nothing
            Else
                Rep.XrLogo.Image = clsPublic.ByteArrayToImage(clsLnEmpresa.GetImagen(AP.IdEmpresa))
            End If

            Rep.ShowPreview()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function cmdAnularPrefactura_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdAnularPrefactura.ItemClick
        Dim us As New clsBeUsuario
        Dim ms As New clsBeMenu_sistema
        Dim clave As String
        'Dim BeMoticoCorreccionBodega As New clsBeTrans_oc_pol_motivo_correccion_bodega

        Try

            'BeMoticoCorreccionBodega.IdBodega = cmbBodega.EditValue

            ms.IdMenu = e.Link.KeyTip
            clsLnMenu_sistema.GetSingle(ms)

            If (ms.Solicitar_clave_autorizacion) Then

                us.IdUsuario = AP.UsuarioAp.IdUsuario
                clsLnUsuario.GetSingle(us)

                Try

                    clave = clsPublic.Desencriptar(us.Clave_autorizacion)

                    If (clave = String.Empty) Then Throw New Exception

                Catch ex As Exception
                    MsgBox("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.") : Return False
                End Try

                Dim frmlog As New frmAjusteLogin() With {.clave = clave}

                If frmlog.ShowDialog() <> DialogResult.Yes Then
                    frmlog.Dispose() : Return False
                Else
                    'Return True
                End If

                frmlog.Dispose()

                Using MA As New frmMotivo_AnulacionList()

                    With MA

                        .Modo = frmMotivo_AnulacionList.pModo.Seleccion
                        .BeMotivoAnulacionBodega.IdBodega = pBodega.IdBodega

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                        End If

                        If .ShowDialog() = DialogResult.OK Then

                            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                            SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                            If clsLnTrans_prefactura_enc.Anular_Prefactura_By_IdTransPrefacturaEnc(BePrefacturaEnc.IdTransPrefacturaEnc) Then
                                clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202408281031: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " anuló la prefactura: " & BePrefacturaEnc.IdTransPrefacturaEnc)
                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show("Prefactura anulada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Close()
                                InvokeListarPrefacturas.Invoke()
                            Else
                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show("No se pudo anular el documento de ingreso.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If
                        End If

                    End With

                End Using

                SplashScreenManager.CloseForm(False)

                Return True

            Else
                Return True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try

    End Function

    Dim IdClienteBodega As Integer
    Private Sub cmbCliente_EditValueChanged(sender As Object, e As EventArgs) Handles cmbCliente.EditValueChanged
        Try

            If cmbCliente.EditValue > 0 Then
                Dim fila As Object = cmbCliente.GetSelectedDataRow
                If fila IsNot Nothing Then
                    IdClienteBodega = fila.Item("IdClienteBodega")
                End If
            Else
                IdClienteBodega = 0
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub chkEstimacionCobro_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkEstimacionCobro.CheckedChanged

        If chkEstimacionCobro.Checked Then

            lblScanPoliza.Visible = True
            txtScanPoliza.Visible = True

            cmbPolizasPE.Enabled = False
            txtScanPoliza.Enabled = True

            txtPesoTotal.Enabled = True
            dtFechaSalida.Enabled = True
            txtValorAduana.Enabled = True
            txtValorGeneral.Enabled = True
        Else

            lblScanPoliza.Visible = False
            txtScanPoliza.Visible = False

            cmbPolizasPE.Enabled = True
            txtScanPoliza.Enabled = False

            txtPesoTotal.Enabled = False
            dtFechaSalida.Enabled = False
            txtValorAduana.Enabled = False
            txtValorGeneral.Enabled = False
        End If

    End Sub

    Dim ListaSeleccionMultiple As New List(Of clsBeTrans_oc_enc)

    Private Sub cmdListadoIngresos_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdListadoIngresos.ItemClick

        Try

            If IdPropietario > 0 Then
                Dim frmIngresos = New frmIngresos_List

                frmIngresos.Modo = frmIngresos_List.pModo.Seleccion
                frmIngresos.WindowState = FormWindowState.Normal
                frmIngresos.pIdPropietario = cmbPropietario.EditValue
                frmIngresos.pBodega = pBodega

                If frmIngresos.ShowDialog() = DialogResult.OK Then

                    If frmIngresos.Es_Seleccion_Multiple Then
                        ListaSeleccionMultiple = frmIngresos.ListaSeleccionMultiple
                        Es_Seleccion_Multiple = True
                        pBuscarPolizaEspecifica = False


                        lblPrg.Text = ""
                        lblPrg.Refresh()
                        lblPrg.AppendText(vbNewLine)
                        lblPrg.AppendText("selección múltiple..." & Now)
                        lblPrg.AppendText(vbNewLine)
                        lblPrg.Refresh()
                        lblPrg.SelectionStart = lblPrg.TextLength
                        lblPrg.ScrollToCaret()

                        For Each ingreso In ListaSeleccionMultiple

                            lblPrg.AppendText("numero de orden: " & ingreso.Referencia & ", ingreso: " & ingreso.IdOrdenCompraEnc)
                            lblPrg.AppendText(vbNewLine)
                            lblPrg.Refresh()
                            lblPrg.SelectionStart = lblPrg.TextLength
                            lblPrg.ScrollToCaret()

                        Next


                    Else
                        'BeOCEnc = frmIngresos.ListaSeleccionMultiple(0)
                        Es_Seleccion_Multiple = False
                        'pBuscarPolizaEspecifica = True
                        XtraMessageBox.Show("No esta definido procesar un registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                End If

                frmIngresos.Hide()

            Else
                XtraMessageBox.Show("Debe seleccionar un propietario primero.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


End Class