Imports DevExpress.XtraEditors

Public Class frmConfiguracion

    Public BeConfigEnc As New clsBeI_nav_config_enc

    Public pBeINavConfigEnt As New clsBeI_nav_config_ent
    Public pBeINavConfigDet As New clsBeI_nav_config_det

    Public lDet As New List(Of clsBeI_nav_config_det)

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Private Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Listar_Estados_Producto()

        Try

            Dim lEstados As New List(Of clsBeProducto_estado)

            lEstados = clsLnProducto_estado.GetAll()

            Dim DataSource = lEstados.Where(Function(x) x.Activo = True AndAlso x.Dañado = 0 AndAlso x.Utilizable = True).ToList()

            cmbProductoEstado.DisplayMember = "Nombre"
            cmbProductoEstado.ValueMember = "IdEstado"
            cmbProductoEstado.DataSource = DataSource

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Listar_Estados_Producto_NC()

        Try

            Dim lEstados As New List(Of clsBeProducto_estado)

            lEstados = clsLnProducto_estado.GetAll()

            Dim DataSource = lEstados.Where(Function(x) x.Activo = True AndAlso x.Dañado = 0 AndAlso x.Utilizable = True).ToList()

            cmbIdEstadoProductoNC.DisplayMember = "Nombre"
            cmbIdEstadoProductoNC.ValueMember = "IdEstado"
            cmbIdEstadoProductoNC.DataSource = DataSource

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Set_Helpers()

        Try

            HelpProvider1.SetShowHelp(Me.txtIdAcuerdoEnc, True)
            HelpProvider1.SetHelpString(txtIdAcuerdoEnc, "Indica el código de Acuerdo Comercial por defecto con el que se asocian los clientes y servicios #ASLAEC")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Limpiar()
        checBoxLunes.Checked = False
        checkBoxMartes.Checked = False
        checkBoxMiercoles.Checked = False
        checkBoxJueves.Checked = False
        checkBoxViernes.Checked = False
        checkBoxSabado.Checked = False
        checkBoxDomingo.Checked = False
        chkControlLote.Checked = False
        chkControlFechaVencimiento.Checked = False
        chkGeneraLP.Checked = False
        chkGenerarPedidoIngresoBD.Checked = False
        chkGenerarRecAutoBD.Checked = False
        chkCrearRecCompraNAV.Checked = False
        chkCrearRecTransfNAV.Checked = False
        seConvertirDecUMB.Value = 0
        seDespacharExiParc.Value = 0
        chkRechazarPedidoIncompleto.Checked = False
        chkValidaSoloCodigo.Checked = False
        txtArchivo.Text = ""
    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If (Datos_Correctos()) Then

                Guardar_Lista_Horarios_Por_Dia()

                BeConfigEnc.Idempresa = cmbEmpresa.SelectedValue
                BeConfigEnc.Idbodega = cmbBodega.SelectedValue
                BeConfigEnc.IdPropietario = cmbPropietario.SelectedValue
                BeConfigEnc.IdProductoEstado = cmbProductoEstado.SelectedValue
                BeConfigEnc.IdUsuario = cmbUsuario.SelectedValue
                BeConfigEnc.Fec_mod = Now
                BeConfigEnc.IdFamilia = cmbFamilia.SelectedValue
                BeConfigEnc.Idclasificacion = cmbClasificación.SelectedValue
                BeConfigEnc.IdMarca = cmbMarca.SelectedValue
                BeConfigEnc.IdTipoProducto = cmbTipoProducto.SelectedValue
                BeConfigEnc.Codigo_proveedor_produccion = txtCodigoProvProd.Text
                BeConfigEnc.Nombre_ejecutable = txtArchivo.Text
                BeConfigEnc.Control_lote = chkControlLote.Checked
                BeConfigEnc.Control_vencimiento = chkControlFechaVencimiento.Checked
                BeConfigEnc.Control_peso = chkControlPeso.Checked
                BeConfigEnc.Genera_lp = chkGeneraLP.Checked
                BeConfigEnc.Generar_pedido_ingreso_bodega_destino = chkGenerarPedidoIngresoBD.Checked
                BeConfigEnc.Generar_Recepcion_Auto_Bodega_Destino = chkGenerarRecAutoBD.Checked
                BeConfigEnc.Crear_Recepcion_De_Transferencia_NAV = chkCrearRecTransfNAV.Checked
                BeConfigEnc.Crear_Recepcion_De_Compra_NAV = chkCrearRecCompraNAV.Checked
                BeConfigEnc.Convertir_decimales_a_umbas = seConvertirDecUMB.Value
                BeConfigEnc.Despachar_existencia_parcial = seDespacharExiParc.Value
                BeConfigEnc.Rechazar_pedido_incompleto = IIf(Not chkRechazarPedidoIncompleto.Checked, clsBeI_nav_config_enc.tRechazarPedidoIncompleto.No, clsBeI_nav_config_enc.tRechazarPedidoIncompleto.Si)
                BeConfigEnc.IdTipoEtiqueta = cmbEtiqueta.EditValue
                BeConfigEnc.IdTipoRotacion = cmbTipoRotacion.EditValue
                BeConfigEnc.Push_Ingreso_NAV_Desde_HH = chkpush_ingreso_nav_desde_hh.Checked
                BeConfigEnc.IdAcuerdoEnc = Val(txtIdAcuerdoEnc.Text)
                BeConfigEnc.Implosion_Automatica = chkImplosionAutomaticaEnInterface.Checked
                BeConfigEnc.Explosion_Automatica = chkExplosionAutomaticaInterface.Checked
                BeConfigEnc.Ejecutar_En_Despacho_Automaticamente = chkEjecutarEnDespachoAuotmaticamente.Checked
                BeConfigEnc.Explosion_Automatica_Desde_Ubicacion_Picking = chkExplosionAutomaticaUbicacionPicking.Checked
                BeConfigEnc.Explosion_Automatica_Nivel_Max = txtNivelMaximoExplosionAuto.EditValue
                BeConfigEnc.Conservar_Zona_Picking_Clavaud = chkmantener_zona_picking_clavaud.Checked
                BeConfigEnc.Excluir_Ubicaciones_Reabasto = chkExcluirUbicacionesReabasto.Checked
                BeConfigEnc.considerar_paletizado_en_reabasto = chkConsiderar_Paletizado_En_Reabasto.Checked
                BeConfigEnc.Dias_Vida_Defecto_Perecederos = txtdias_vida_defecto_perecederos.Value
                BeConfigEnc.Codigo_Bodega_ERP_NC = txtCodigoBodegaERPNC.Text
                BeConfigEnc.Lote_Defecto_Entrada_NC = txtLoteDefectoEntradaNC.Text
                BeConfigEnc.Vence_Defecto_NC = dtpVenceDefectoNC.Value
                BeConfigEnc.IdProductoEstado_NC = cmbIdEstadoProductoNC.SelectedValue
                BeConfigEnc.Interface_SAP = chkInterfaceSAP.Checked
                BeConfigEnc.SAP_Control_Draft_Ajustes = chkSAP_Control_Draft_Ajustes.Checked
                BeConfigEnc.SAP_Control_Draft_Traslados = chkSAP_Control_Draft_Traslados.Checked
                BeConfigEnc.Rango_Dias_Importacion = nudRangoDiasImportacion.Value
                BeConfigEnc.IdIndiceRotacion = cmbIndiceRotacion.EditValue
                BeConfigEnc.Inferir_Bonificacion_Pedido_SAP = chkInferirBonificacionPedidoSAP.Checked
                BeConfigEnc.Rechazar_Bonificacion_Incompleta = chkRechazarBonificacionIncompleta.Checked
                BeConfigEnc.Valida_Solo_Codigo = chkValidaSoloCodigo.Checked
                BeConfigEnc.Excluir_Recepcion_Picking = chkExcluirRececpionPicking.Checked
                BeConfigEnc.Bodega_Facturacion = txtCodigoBodegaFacturacion.Text
                BeConfigEnc.Bodega_Prorrateo = txtCodigoBodegaProrrateo.Text
                BeConfigEnc.Bodega_Prorrateo1 = txtCodigoBodegaProrrateo1.Text

                BeConfigEnc.Centro_Costo_Dep_Erp = nuCentroCostoDepERP.Value
                BeConfigEnc.Centro_Costo_Dir_Erp = nuCentroCostoDirERP.Value
                BeConfigEnc.Centro_Costo_Erp = nuCentroCostoERP.Value

                '#EJC20171107_REF21_1127PM: clsLnI_nav_config_enc.Actualizar con transaccionalidad y encabezado de configuración
                If clsLnI_nav_config_enc.Actualizar(BeConfigEnc, lDet, pBeINavConfigEnt) Then

                    XtraMessageBox.Show("Configuración actualizada",
                                        Text,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information)

                    DialogResult = DialogResult.OK

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            If (Datos_Correctos()) Then

                Guardar_Lista_Horarios_Por_Dia()

                BeConfigEnc = New clsBeI_nav_config_enc()

                BeConfigEnc.Idempresa = cmbEmpresa.SelectedValue
                BeConfigEnc.Idbodega = cmbBodega.SelectedValue
                BeConfigEnc.IdPropietario = cmbPropietario.SelectedValue
                BeConfigEnc.IdProductoEstado = cmbProductoEstado.SelectedValue
                BeConfigEnc.IdUsuario = cmbUsuario.SelectedValue
                BeConfigEnc.Fec_mod = Now
                BeConfigEnc.IdFamilia = cmbFamilia.SelectedValue
                BeConfigEnc.Idclasificacion = cmbClasificación.SelectedValue
                BeConfigEnc.IdMarca = cmbMarca.SelectedValue
                BeConfigEnc.IdTipoProducto = cmbTipoProducto.SelectedValue
                BeConfigEnc.Codigo_proveedor_produccion = txtCodigoProvProd.Text
                BeConfigEnc.Nombre_ejecutable = txtArchivo.Text
                BeConfigEnc.Control_lote = chkControlLote.Checked
                BeConfigEnc.Control_vencimiento = chkControlFechaVencimiento.Checked
                BeConfigEnc.Control_peso = chkControlPeso.Checked
                BeConfigEnc.Genera_lp = chkGeneraLP.Checked
                BeConfigEnc.Generar_pedido_ingreso_bodega_destino = chkGenerarPedidoIngresoBD.Checked
                BeConfigEnc.Generar_Recepcion_Auto_Bodega_Destino = chkGenerarRecAutoBD.Checked
                BeConfigEnc.Crear_Recepcion_De_Transferencia_NAV = chkCrearRecTransfNAV.Checked
                BeConfigEnc.Crear_Recepcion_De_Compra_NAV = chkCrearRecCompraNAV.Checked
                BeConfigEnc.Convertir_decimales_a_umbas = seConvertirDecUMB.Value
                BeConfigEnc.Despachar_existencia_parcial = seDespacharExiParc.Value
                BeConfigEnc.Rechazar_pedido_incompleto = IIf(Not chkRechazarPedidoIncompleto.Checked, clsBeI_nav_config_enc.tRechazarPedidoIncompleto.No, clsBeI_nav_config_enc.tRechazarPedidoIncompleto.Si)
                BeConfigEnc.IdTipoEtiqueta = cmbEtiqueta.EditValue
                BeConfigEnc.IdTipoRotacion = cmbTipoRotacion.EditValue
                BeConfigEnc.Push_Ingreso_NAV_Desde_HH = chkpush_ingreso_nav_desde_hh.Checked
                BeConfigEnc.IdAcuerdoEnc = Val(txtIdAcuerdoEnc.Text)
                BeConfigEnc.Implosion_Automatica = chkImplosionAutomaticaEnInterface.Checked
                BeConfigEnc.Explosion_Automatica = chkExplosionAutomaticaInterface.Checked
                BeConfigEnc.Ejecutar_En_Despacho_Automaticamente = chkEjecutarEnDespachoAuotmaticamente.Checked
                BeConfigEnc.Explosion_Automatica_Desde_Ubicacion_Picking = chkExplosionAutomaticaUbicacionPicking.Checked
                BeConfigEnc.Explosion_Automatica_Nivel_Max = txtNivelMaximoExplosionAuto.EditValue
                BeConfigEnc.Conservar_Zona_Picking_Clavaud = chkmantener_zona_picking_clavaud.Checked
                BeConfigEnc.Excluir_Ubicaciones_Reabasto = chkExcluirUbicacionesReabasto.Checked
                BeConfigEnc.considerar_paletizado_en_reabasto = chkConsiderar_Paletizado_En_Reabasto.Checked
                BeConfigEnc.Considerar_Disponibilidad_Ubicacion_Reabasto = chkConsiderar_Disponibilidad_Ubicacion_Reabasto.Checked
                BeConfigEnc.Dias_Vida_Defecto_Perecederos = txtdias_vida_defecto_perecederos.Value
                BeConfigEnc.Codigo_Bodega_ERP_NC = txtdias_vida_defecto_perecederos.Value
                BeConfigEnc.Lote_Defecto_Entrada_NC = txtdias_vida_defecto_perecederos.Value
                BeConfigEnc.Codigo_Bodega_ERP_NC = txtCodigoBodegaERPNC.Text
                BeConfigEnc.Lote_Defecto_Entrada_NC = txtLoteDefectoEntradaNC.Text
                BeConfigEnc.Vence_Defecto_NC = dtpVenceDefectoNC.Value
                BeConfigEnc.IdProductoEstado_NC = cmbIdEstadoProductoNC.SelectedValue
                BeConfigEnc.Interface_SAP = chkInterfaceSAP.Checked
                BeConfigEnc.SAP_Control_Draft_Ajustes = chkSAP_Control_Draft_Ajustes.Checked
                BeConfigEnc.SAP_Control_Draft_Traslados = chkSAP_Control_Draft_Traslados.Checked
                BeConfigEnc.Rango_Dias_Importacion = nudRangoDiasImportacion.Value
                BeConfigEnc.IdIndiceRotacion = cmbIndiceRotacion.EditValue
                BeConfigEnc.Inferir_Bonificacion_Pedido_SAP = chkInferirBonificacionPedidoSAP.Checked
                BeConfigEnc.Rechazar_Bonificacion_Incompleta = chkRechazarBonificacionIncompleta.Checked
                BeConfigEnc.Valida_Solo_Codigo = chkValidaSoloCodigo.Checked
                BeConfigEnc.Excluir_Recepcion_Picking = chkExcluirRececpionPicking.Checked

                BeConfigEnc.Bodega_Facturacion = txtCodigoBodegaFacturacion.Text
                BeConfigEnc.Bodega_Prorrateo = txtCodigoBodegaProrrateo.Text
                BeConfigEnc.Bodega_Prorrateo1 = txtCodigoBodegaProrrateo1.Text

                BeConfigEnc.Centro_Costo_Dep_Erp = nuCentroCostoDepERP.Value
                BeConfigEnc.Centro_Costo_Dir_Erp = nuCentroCostoDirERP.Value
                BeConfigEnc.Centro_Costo_Erp = nuCentroCostoERP.Value

                '#EJC20171107_REF21_1127PM: clsLnI_nav_config_enc.Actualizar con transaccionalidad y encabezado de configuración
                '#GT12012023_2000: agregue el guardar, porque el Actualizar no agrega nuevos registros.
                'If clsLnI_nav_config_enc.Actualizar(BeConfigEnc, lDet, pBeINavConfigEnt) Then
                If clsLnI_nav_config_enc.Guardar(BeConfigEnc, lDet, pBeINavConfigEnt) Then

                    XtraMessageBox.Show("Configuración guardada",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                    DialogResult = DialogResult.OK

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function


    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Actualizar()
    End Sub

    Private Sub Listar_Entidades()

        Try

            lDet = clsLnI_nav_config_det.GetAllFiltro(pBeINavConfigEnt.IdNavEnt, True)

            '#EJC20171107_REF19_0924PM: Optimización de código Listar_Entidades en interface reemplazar ciclo por LinQ
            'Antes se utilizaba un For creado por Cuscul

            If Not lDet Is Nothing Then

                If lDet.Count > 0 Then

                    Dim vConfig As New clsBeI_nav_config_det
                    vConfig = lDet.Where(Function(x) x.Dia = 1).FirstOrDefault

                    If vConfig Is Nothing Then
                        checBoxLunes.Checked = False
                    Else
                        checBoxLunes.Checked = vConfig.Activo
                    End If

                    vConfig = lDet.Where(Function(x) x.Dia = 2).FirstOrDefault

                    If vConfig Is Nothing Then
                        checkBoxMartes.Checked = False
                    Else
                        checkBoxMartes.Checked = vConfig.Activo
                    End If

                    vConfig = lDet.Where(Function(x) x.Dia = 3).FirstOrDefault

                    If vConfig Is Nothing Then
                        checkBoxMiercoles.Checked = False
                    Else
                        checkBoxMiercoles.Checked = vConfig.Activo
                    End If

                    vConfig = lDet.Where(Function(x) x.Dia = 4).FirstOrDefault

                    If vConfig Is Nothing Then
                        checkBoxJueves.Checked = False
                    Else
                        checkBoxJueves.Checked = vConfig.Activo
                    End If

                    vConfig = lDet.Where(Function(x) x.Dia = 5).FirstOrDefault

                    If vConfig Is Nothing Then
                        checkBoxViernes.Checked = False
                    Else
                        checkBoxViernes.Checked = vConfig.Activo
                    End If

                    vConfig = lDet.Where(Function(x) x.Dia = 6).FirstOrDefault

                    If vConfig Is Nothing Then
                        checkBoxSabado.Checked = False
                    Else
                        checkBoxSabado.Checked = vConfig.Activo
                    End If

                    vConfig = lDet.Where(Function(x) x.Dia = 7).FirstOrDefault

                    If vConfig Is Nothing Then
                        checkBoxDomingo.Checked = False
                    Else
                        checkBoxDomingo.Checked = vConfig.Activo
                    End If

                    Dim BeNavEnt As New clsBeI_nav_ent() With {.Idnavent = lDet.FirstOrDefault.IdNavEnt}
                    clsLnI_nav_ent.Get_Single(BeNavEnt)

                    txtEntidad.Text = BeNavEnt.Nombre
                    txtEndpoint.Text = BeNavEnt.Endpoint
                    txtHoraInicio.EditValue = lDet.FirstOrDefault.HoraInicio
                    txtHoraFin.EditValue = lDet.FirstOrDefault.HoraFin
                    txtFrecuencia.Value = lDet.FirstOrDefault.Frecuencia

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Llenar_Grid_Entidades()

        Try

            Dgrid.DataSource = clsLnI_nav_config_ent.ListarEnt()

            If (GridView1.Columns.Count <> 0) Then

                Try
                    GridView1.Columns("idnavent").Visible = False
                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try
            If cmbEmpresa.SelectedIndex = -1 Then
                XtraMessageBox.Show("Seleccione una empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf cmbBodega.SelectedIndex = -1 Then
                XtraMessageBox.Show("Seleccione una bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese un nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    '#EJC20171107_REF18_0924PM: Optimización de código Guardar_Horarios_Por_Dia en interface
    Private Sub Guardar_Lista_Horarios_Por_Dia()

        Try

            BeConfigEnc.Nombre = txtNombre.Text
            pBeINavConfigEnt.Endpoint = txtEndpoint.Text
            pBeINavConfigEnt.Activo = True

            Dim IndiceEntExistente As Integer = 0

            If (checBoxLunes.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 1) Then
                    IndiceEntExistente = lDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    lDet(IndiceEntExistente).Activo = checBoxLunes.Checked
                    lDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    lDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    lDet(IndiceEntExistente).Dia = 1
                    lDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    lDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    lDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checBoxLunes.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 1,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    lDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checkBoxMartes.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 2) Then
                    IndiceEntExistente = lDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    lDet(IndiceEntExistente).Activo = checkBoxMartes.Checked
                    lDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    lDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    lDet(IndiceEntExistente).Dia = 2
                    lDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    lDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    lDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checkBoxMartes.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 2,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    lDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checkBoxMiercoles.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 3) Then
                    IndiceEntExistente = lDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    lDet(IndiceEntExistente).Activo = checkBoxMiercoles.Checked
                    lDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    lDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    lDet(IndiceEntExistente).Dia = 3
                    lDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    lDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    lDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checkBoxMiercoles.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 3,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    lDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checkBoxJueves.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 4) Then
                    IndiceEntExistente = lDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    lDet(IndiceEntExistente).Activo = checkBoxJueves.Checked
                    lDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    lDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    lDet(IndiceEntExistente).Dia = 4
                    lDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    lDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    lDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checkBoxJueves.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 4,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    lDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checkBoxViernes.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 5) Then
                    IndiceEntExistente = lDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    lDet(IndiceEntExistente).Activo = checkBoxViernes.Checked
                    lDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    lDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    lDet(IndiceEntExistente).Dia = 5
                    lDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    lDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    lDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checkBoxViernes.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 5,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    lDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checkBoxSabado.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 6) Then
                    IndiceEntExistente = lDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    lDet(IndiceEntExistente).Activo = checkBoxSabado.Checked
                    lDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    lDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    lDet(IndiceEntExistente).Dia = 6
                    lDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    lDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    lDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checkBoxSabado.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 6,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    lDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checkBoxDomingo.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 7) Then
                    IndiceEntExistente = lDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    lDet(IndiceEntExistente).Activo = checkBoxDomingo.Checked
                    lDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    lDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    lDet(IndiceEntExistente).Dia = 6
                    lDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    lDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    lDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checkBoxDomingo.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 7,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    lDet.Add(pBeINavConfigDet)
                End If
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs)

        Try

            Limpiar()

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                txtEndpoint.Text = Dr.Item("endpoint")
                pBeINavConfigEnt.IdNavEnt = Dr.Item("idnavent")
                txtEntidad.Text = Dr.Item("nombre")

                GridView1.FocusedRowHandle = lSelectionIndex

                Listar_Entidades()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmpresa.SelectedIndexChanged

        If cmbEmpresa.SelectedIndex <> -1 Then

            If Not IMS.Listar_BodegasPorEmpresa(cmbBodega, cmbEmpresa.SelectedValue) Then
                XtraMessageBox.Show("No hay Bodegas definidas para esta empresa", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbBodega.SelectedIndex = -1
            End If

            If Not IMS.Listar_PropietariosByEmpresa(cmbPropietario, cmbEmpresa.SelectedValue) Then
                XtraMessageBox.Show("No hay Propietarios definidos para esta empresa", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbPropietario.SelectedIndex = -1
            End If

        End If

    End Sub

    Private Sub ButtonEdit1_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtArchivo.Properties.ButtonClick

        Try

            Dim ObjO As New OpenFileDialog() With {.InitialDirectory = CurDir(),
                .Filter = "Ejecutables(*.exe)|*.exe"}

            If ObjO.ShowDialog() = DialogResult.OK AndAlso ObjO.FileName.Length <> 0 Then
                txtArchivo.Text = ObjO.FileName.Substring(CurDir.Length + 1)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbPropietario_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.SelectedValueChanged

        Try

            IMS.Listar_FamiliasByPropietario(cmbFamilia, cmbPropietario.SelectedValue)

            Application.DoEvents()

            IMS.Listar_ClasificacionesByPropietario(cmbClasificación, cmbEmpresa.SelectedValue)

            Application.DoEvents()

            IMS.Listar_MarcasByPropietario(cmbMarca, cmbEmpresa.SelectedValue)

            Application.DoEvents()

            IMS.Listar_TipoProductoByPropietario(cmbTipoProducto, cmbEmpresa.SelectedValue)

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        Guardar()
    End Sub

    Private Sub CargaComboEtiquetas()

        Try

            cmbEtiqueta.Properties.DisplayMember = "Nombre"
            cmbEtiqueta.Properties.ValueMember = "IdTipoEtiqueta"
            cmbEtiqueta.Properties.DataSource = clsLnTipo_etiqueta.GetAllForCombo()
            cmbEtiqueta.ItemIndex = 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub CargaComboTipoRotacion()

        Try

            cmbTipoRotacion.Properties.DisplayMember = "Nombre"
            cmbTipoRotacion.Properties.ValueMember = "IdTipoRotacion"
            cmbTipoRotacion.Properties.DataSource = clsLnTipo_rotacion.GetAllForCombo(True)
            cmbTipoRotacion.ItemIndex = 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
    Private Sub CargaComboIndiceRotacion()

        Try

            cmbIndiceRotacion.Properties.DisplayMember = "Nombre"
            cmbIndiceRotacion.Properties.ValueMember = "IdIndiceRotacion"
            cmbIndiceRotacion.Properties.DataSource = clsLnIndice_rotacion.GetAllForCombo(True)
            cmbIndiceRotacion.ItemIndex = 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
    Private Sub frmConfiguracion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Listar_Estados_Producto()

            Listar_Estados_Producto_NC()

            Llenar_Grid_Entidades()

            CargaComboEtiquetas()

            CargaComboTipoRotacion()

            CargaComboIndiceRotacion()

            If Not IMS.Listar_Empresas(cmbEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not IMS.Listar_BodegasPorEmpresa(cmbBodega, cmbEmpresa.SelectedValue) Then
                XtraMessageBox.Show("No hay Bodegas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            IMS.Listar_PropietariosByEmpresa(cmbPropietario, cmbEmpresa.SelectedValue)

            IMS.Listar_UsuariosSistemas(cmbUsuario, cmbEmpresa.SelectedValue)

            Select Case Modo

                Case TipoTrans.Editar

                    clsLnI_nav_config_enc.GetSingle(BeConfigEnc)

                    Me.Text &= " " & BeConfigEnc.Idnavconfigenc
                    cmbEmpresa.SelectedValue = BeConfigEnc.Idempresa
                    cmbBodega.SelectedValue = BeConfigEnc.Idbodega
                    cmbProductoEstado.SelectedValue = BeConfigEnc.IdProductoEstado
                    cmbPropietario.SelectedValue = BeConfigEnc.IdPropietario

                    cmbFamilia.SelectedValue = BeConfigEnc.IdFamilia
                    cmbClasificación.SelectedValue = BeConfigEnc.Idclasificacion
                    cmbMarca.SelectedValue = BeConfigEnc.IdMarca
                    cmbTipoProducto.SelectedValue = BeConfigEnc.IdTipoProducto

                    txtNombre.Text = BeConfigEnc.Nombre

                    txtArchivo.Text = BeConfigEnc.Nombre_ejecutable
                    txtCodigoProvProd.Text = BeConfigEnc.Codigo_proveedor_produccion

                    User_agrTextEdit.Text = BeConfigEnc.User_agr
                    Fec_agrTextEdit.Text = BeConfigEnc.Fec_agr
                    User_modTextEdit.Text = BeConfigEnc.User_mod
                    Fec_modTextEdit.Text = BeConfigEnc.Fec_mod

                    chkRechazarPedidoIncompleto.Checked = BeConfigEnc.Rechazar_pedido_incompleto
                    seConvertirDecUMB.Value = BeConfigEnc.Convertir_decimales_a_umbas
                    seDespacharExiParc.Value = BeConfigEnc.Despachar_existencia_parcial
                    chkValidaSoloCodigo.Checked = BeConfigEnc.Valida_Solo_Codigo
                    chkExcluirRececpionPicking.Checked = BeConfigEnc.Excluir_Recepcion_Picking

                    txtCodigoBodegaFacturacion.Text = BeConfigEnc.Bodega_Facturacion
                    txtCodigoBodegaProrrateo.Text = BeConfigEnc.Bodega_Prorrateo
                    txtCodigoBodegaProrrateo1.Text = BeConfigEnc.Bodega_Prorrateo1

                    chkGenerarPedidoIngresoBD.Checked = BeConfigEnc.Generar_pedido_ingreso_bodega_destino
                    chkGenerarRecAutoBD.Checked = BeConfigEnc.Generar_Recepcion_Auto_Bodega_Destino
                    chkCrearRecCompraNAV.Checked = BeConfigEnc.Crear_Recepcion_De_Compra_NAV
                    chkCrearRecTransfNAV.Checked = BeConfigEnc.Crear_Recepcion_De_Transferencia_NAV

                    chkControlFechaVencimiento.Checked = BeConfigEnc.Control_vencimiento
                    chkControlLote.Checked = BeConfigEnc.Control_lote
                    chkControlPeso.Checked = BeConfigEnc.Control_peso
                    chkGeneraLP.Checked = BeConfigEnc.Genera_lp

                    txtIdAcuerdoEnc.Text = BeConfigEnc.IdAcuerdoEnc

                    cmbEtiqueta.EditValue = BeConfigEnc.IdTipoEtiqueta
                    cmbTipoRotacion.EditValue = BeConfigEnc.IdTipoRotacion
                    cmbIndiceRotacion.EditValue = BeConfigEnc.IdIndiceRotacion

                    chkImplosionAutomaticaEnInterface.Checked = BeConfigEnc.Implosion_Automatica
                    chkExplosionAutomaticaInterface.Checked = BeConfigEnc.Explosion_Automatica

                    chkExplosionAutomaticaUbicacionPicking.Checked = BeConfigEnc.Explosion_Automatica_Desde_Ubicacion_Picking
                    txtNivelMaximoExplosionAuto.EditValue = BeConfigEnc.Explosion_Automatica_Nivel_Max

                    chkmantener_zona_picking_clavaud.Checked = BeConfigEnc.Conservar_Zona_Picking_Clavaud

                    chkExcluirUbicacionesReabasto.Checked = BeConfigEnc.Excluir_Ubicaciones_Reabasto

                    chkConsiderar_Paletizado_En_Reabasto.Checked = BeConfigEnc.considerar_paletizado_en_reabasto

                    chkConsiderar_Disponibilidad_Ubicacion_Reabasto.Checked = BeConfigEnc.Considerar_Disponibilidad_Ubicacion_Reabasto

                    txtdias_vida_defecto_perecederos.Value = BeConfigEnc.Dias_Vida_Defecto_Perecederos

                    txtCodigoBodegaERPNC.Text = BeConfigEnc.Codigo_Bodega_ERP_NC
                    txtLoteDefectoEntradaNC.Text = BeConfigEnc.Lote_Defecto_Entrada_NC
                    dtpVenceDefectoNC.Value = BeConfigEnc.Vence_Defecto_NC

                    cmbIdEstadoProductoNC.SelectedValue = BeConfigEnc.IdProductoEstado_NC

                    chkInterfaceSAP.Checked = BeConfigEnc.Interface_SAP

                    chkSAP_Control_Draft_Ajustes.Checked = BeConfigEnc.SAP_Control_Draft_Ajustes
                    chkSAP_Control_Draft_Traslados.Checked = BeConfigEnc.SAP_Control_Draft_Traslados

                    nudRangoDiasImportacion.Value = BeConfigEnc.Rango_Dias_Importacion

                    chkEjecutarEnDespachoAuotmaticamente.Checked = BeConfigEnc.Ejecutar_En_Despacho_Automaticamente

                    chkInferirBonificacionPedidoSAP.Checked = BeConfigEnc.Inferir_Bonificacion_Pedido_SAP
                    chkRechazarBonificacionIncompleta.Checked = BeConfigEnc.Rechazar_Bonificacion_Incompleta

                    nuCentroCostoDepERP.Value = BeConfigEnc.Centro_Costo_Dep_Erp
                    nuCentroCostoDirERP.Value = BeConfigEnc.Centro_Costo_Dir_Erp
                    nuCentroCostoERP.Value = BeConfigEnc.Centro_Costo_Erp

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                Case TipoTrans.Nuevo

                    cmbMarca.SelectedIndex = -1
                    cmbFamilia.SelectedIndex = -1
                    cmbClasificación.SelectedIndex = -1
                    cmbTipoProducto.SelectedIndex = -1
                    cmbTipoRotacion.EditValue = ""
                    cmbIndiceRotacion.EditValue = ""

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                    Exit Select

            End Select

            Set_Helpers()

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class