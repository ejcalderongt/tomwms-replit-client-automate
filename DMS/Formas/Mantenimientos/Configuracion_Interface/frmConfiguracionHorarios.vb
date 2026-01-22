Imports System.Reflection
Imports System.Security.Cryptography
Imports DevExpress.XtraEditors
Imports DevExpress.XtraTab

Public Class frmConfiguracionHorarios

    Public BeConfigEnc As New clsBeI_nav_config_enc

    Public pBeINavConfigEnt As New clsBeI_nav_config_ent
    'Public pBeINavConfigDet As New clsBeI_nav_config_det

    Dim pBeINavConfigDet As New clsBeI_nav_config_det()

    Public ListNavConfigDet As New List(Of clsBeI_nav_config_det)

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
        checBoxMartes.Checked = False
        checBoxMiercoles.Checked = False
        checBoxJueves.Checked = False
        checBoxViernes.Checked = False
        checBoxSabado.Checked = False
        checBoxDomingo.Checked = False
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

    Private Sub Opciones_menu()
        cmdAdd.Enabled = False
    End Sub

    Private Sub Limpiar_Inputs()
        cmbInterfaces.EditValue = -1
        cmbEntidades.EditValue = -1
        txtEndpoint.Text = ""
        txtHoraInicio.EditValue = 0
        txtHoraFin.EditValue = 0
        txtFrecuencia.Value = 0
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

                '#EJC20171107_REF21_1127PM: clsLnI_nav_config_enc.Actualizar con transaccionalidad y encabezado de configuración
                If clsLnI_nav_config_enc.Actualizar(BeConfigEnc, ListNavConfigDet, pBeINavConfigEnt) Then

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

                '#EJC20171107_REF21_1127PM: clsLnI_nav_config_enc.Actualizar con transaccionalidad y encabezado de configuración
                '#GT12012023_2000: agregue el guardar, porque el Actualizar no agrega nuevos registros.
                'If clsLnI_nav_config_enc.Actualizar(BeConfigEnc, lDet, pBeINavConfigEnt) Then
                If clsLnI_nav_config_enc.Guardar(BeConfigEnc, ListNavConfigDet, pBeINavConfigEnt) Then

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


    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Actualizar()
    End Sub

    Private Sub Listar_Entidades_Por_Dia()

        Try

            ListNavConfigDet = clsLnI_nav_config_det.GetAllFiltro(pBeINavConfigEnt.IdNavEnt, True)

            '#EJC20171107_REF19_0924PM: Optimización de código Listar_Entidades en interface reemplazar ciclo por LinQ
            'Antes se utilizaba un For creado por Cuscul

            If Not ListNavConfigDet Is Nothing Then

                If ListNavConfigDet.Count > 0 Then

                    Dim vConfig As New clsBeI_nav_config_det
                    vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 1).FirstOrDefault

                    If vConfig Is Nothing Then
                        checBoxLunes.Checked = False
                    Else
                        checBoxLunes.Checked = vConfig.Activo
                    End If

                    vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 2).FirstOrDefault

                    If vConfig Is Nothing Then
                        checBoxMartes.Checked = False
                    Else
                        checBoxMartes.Checked = vConfig.Activo
                    End If

                    vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 3).FirstOrDefault

                    If vConfig Is Nothing Then
                        checBoxMiercoles.Checked = False
                    Else
                        checBoxMiercoles.Checked = vConfig.Activo
                    End If

                    vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 4).FirstOrDefault

                    If vConfig Is Nothing Then
                        checBoxJueves.Checked = False
                    Else
                        checBoxJueves.Checked = vConfig.Activo
                    End If

                    vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 5).FirstOrDefault

                    If vConfig Is Nothing Then
                        checBoxViernes.Checked = False
                    Else
                        checBoxViernes.Checked = vConfig.Activo
                    End If

                    vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 6).FirstOrDefault

                    If vConfig Is Nothing Then
                        checBoxSabado.Checked = False
                    Else
                        checBoxSabado.Checked = vConfig.Activo
                    End If

                    vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 7).FirstOrDefault

                    If vConfig Is Nothing Then
                        checBoxDomingo.Checked = False
                    Else
                        checBoxDomingo.Checked = vConfig.Activo
                    End If

                    Dim BeNavEnt As New clsBeI_nav_ent() With {.Idnavent = ListNavConfigDet.FirstOrDefault.IdNavEnt}
                    clsLnI_nav_ent.Get_Single(BeNavEnt)

                    txtEntidad.Text = BeNavEnt.Nombre
                    'cmbEntidades.EditValue = BeNavEnt.Idnavent
                    txtEndpoint.Text = BeNavEnt.Endpoint
                    txtHoraInicio.EditValue = ListNavConfigDet.FirstOrDefault.HoraInicio
                    txtHoraFin.EditValue = ListNavConfigDet.FirstOrDefault.HoraFin
                    txtFrecuencia.Value = ListNavConfigDet.FirstOrDefault.Frecuencia

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub configuracion_Dia_Seleccionad(pBeINavConfigDet As clsBeI_nav_config_det)
        Try

            'ListNavConfigDet = clsLnI_nav_config_det.GetAllFiltro(pBeINavConfigEnt.IdNavEnt, True)
            ListNavConfigDet = clsLnI_nav_config_det.GetList(pBeINavConfigDet)

            If Not ListNavConfigDet Is Nothing AndAlso ListNavConfigDet.Count > 0 Then

                Dim vConfig As New clsBeI_nav_config_det
                vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 1).FirstOrDefault

                If vConfig Is Nothing Then
                    checBoxLunes.Checked = False
                Else
                    checBoxLunes.Checked = vConfig.Activo
                End If

                vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 2).FirstOrDefault

                If vConfig Is Nothing Then
                    checBoxMartes.Checked = False
                Else
                    checBoxMartes.Checked = vConfig.Activo
                End If

                vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 3).FirstOrDefault

                If vConfig Is Nothing Then
                    checBoxMiercoles.Checked = False
                Else
                    checBoxMiercoles.Checked = vConfig.Activo
                End If

                vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 4).FirstOrDefault

                If vConfig Is Nothing Then
                    checBoxJueves.Checked = False
                Else
                    checBoxJueves.Checked = vConfig.Activo
                End If

                vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 5).FirstOrDefault

                If vConfig Is Nothing Then
                    checBoxViernes.Checked = False
                Else
                    checBoxViernes.Checked = vConfig.Activo
                End If

                vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 6).FirstOrDefault

                If vConfig Is Nothing Then
                    checBoxSabado.Checked = False
                Else
                    checBoxSabado.Checked = vConfig.Activo
                End If

                vConfig = ListNavConfigDet.Where(Function(x) x.Dia = 7).FirstOrDefault

                If vConfig Is Nothing Then
                    checBoxDomingo.Checked = False
                Else
                    checBoxDomingo.Checked = vConfig.Activo
                End If


                txtHoraInicio.EditValue = ListNavConfigDet.FirstOrDefault.HoraInicio
                txtHoraFin.EditValue = ListNavConfigDet.FirstOrDefault.HoraFin
                txtFrecuencia.Value = ListNavConfigDet.FirstOrDefault.Frecuencia
                chkActivo.Checked = ListNavConfigDet.FirstOrDefault.Activo

                cmdAdd.Enabled = False

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                    IndiceEntExistente = ListNavConfigDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    ListNavConfigDet(IndiceEntExistente).Activo = checBoxLunes.Checked
                    ListNavConfigDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    ListNavConfigDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    ListNavConfigDet(IndiceEntExistente).Dia = 1
                    ListNavConfigDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    ListNavConfigDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    ListNavConfigDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checBoxLunes.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 1,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    ListNavConfigDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checBoxMartes.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 2) Then
                    IndiceEntExistente = ListNavConfigDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    ListNavConfigDet(IndiceEntExistente).Activo = checBoxMartes.Checked
                    ListNavConfigDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    ListNavConfigDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    ListNavConfigDet(IndiceEntExistente).Dia = 2
                    ListNavConfigDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    ListNavConfigDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    ListNavConfigDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checBoxMartes.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 2,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    ListNavConfigDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checBoxMiercoles.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 3) Then
                    IndiceEntExistente = ListNavConfigDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    ListNavConfigDet(IndiceEntExistente).Activo = checBoxMiercoles.Checked
                    ListNavConfigDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    ListNavConfigDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    ListNavConfigDet(IndiceEntExistente).Dia = 3
                    ListNavConfigDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    ListNavConfigDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    ListNavConfigDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checBoxMiercoles.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 3,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    ListNavConfigDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checBoxJueves.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 4) Then
                    IndiceEntExistente = ListNavConfigDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    ListNavConfigDet(IndiceEntExistente).Activo = checBoxJueves.Checked
                    ListNavConfigDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    ListNavConfigDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    ListNavConfigDet(IndiceEntExistente).Dia = 4
                    ListNavConfigDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    ListNavConfigDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    ListNavConfigDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checBoxJueves.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 4,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    ListNavConfigDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checBoxViernes.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 5) Then
                    IndiceEntExistente = ListNavConfigDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    ListNavConfigDet(IndiceEntExistente).Activo = checBoxViernes.Checked
                    ListNavConfigDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    ListNavConfigDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    ListNavConfigDet(IndiceEntExistente).Dia = 5
                    ListNavConfigDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    ListNavConfigDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    ListNavConfigDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checBoxViernes.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 5,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    ListNavConfigDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checBoxSabado.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 6) Then
                    IndiceEntExistente = ListNavConfigDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    ListNavConfigDet(IndiceEntExistente).Activo = checBoxSabado.Checked
                    ListNavConfigDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    ListNavConfigDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    ListNavConfigDet(IndiceEntExistente).Dia = 6
                    ListNavConfigDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    ListNavConfigDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    ListNavConfigDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checBoxSabado.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 6,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    ListNavConfigDet.Add(pBeINavConfigDet)
                End If
            End If

            If (checBoxDomingo.Checked) Then
                If clsLnI_nav_config_det.Exists(pBeINavConfigEnt.IdNavEnt, 7) Then
                    IndiceEntExistente = ListNavConfigDet.FindIndex(Function(x) x.IdNavEnt = pBeINavConfigEnt.IdNavEnt)
                    ListNavConfigDet(IndiceEntExistente).Activo = checBoxDomingo.Checked
                    ListNavConfigDet(IndiceEntExistente).IdNavEnt = pBeINavConfigEnt.IdNavEnt
                    ListNavConfigDet(IndiceEntExistente).IdNavConfigEnc = BeConfigEnc.Idnavconfigenc
                    ListNavConfigDet(IndiceEntExistente).Dia = 6
                    ListNavConfigDet(IndiceEntExistente).HoraInicio = Convert.ToDateTime(txtHoraInicio.Text)
                    ListNavConfigDet(IndiceEntExistente).HoraFin = Convert.ToDateTime(txtHoraFin.Text)
                    ListNavConfigDet(IndiceEntExistente).Frecuencia = Integer.Parse(txtFrecuencia.Text)
                Else
                    pBeINavConfigDet = New clsBeI_nav_config_det() With {.Activo = checBoxDomingo.Checked,
                        .IdNavEnt = pBeINavConfigEnt.IdNavEnt,
                        .IdNavConfigEnc = BeConfigEnc.Idnavconfigenc,
                        .Dia = 7,
                        .HoraInicio = Convert.ToDateTime(txtHoraInicio.Text),
                        .HoraFin = Convert.ToDateTime(txtHoraFin.Text),
                        .Frecuencia = Integer.Parse(txtFrecuencia.Text)}
                    ListNavConfigDet.Add(pBeINavConfigDet)
                End If
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

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
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

            XtraTabControl.TabPages(0).PageVisible = False
            XtraTabControl.TabPages(2).PageVisible = False
            XtraTabControl.TabPages(3).PageVisible = False

            'Listar_Estados_Producto()
            'Listar_Estados_Producto_NC()
            'CargaComboEtiquetas()
            'CargaComboTipoRotacion()
            'CargaComboIndiceRotacion()
            '#GT21072025: se renombro porque se usa para mostrar la entidad guardada con el dia de la semana.
            'Listar_Entidades_Por_Dia()
            '#GT22072025: no es necesario mostrar los ejecutables en el grid, se cargan en un combo
            'Llenar_Grid_Entidades()
            '#GT21072025: listar las entidadades de la tabla en el combo.

            Listar_Interfaces()

            Listar_Entidades()

            Select Case Modo
                Case TipoTrans.Editar

                    cmdAdd.Enabled = True
                    cmdActualizar.Enabled = True
                    cmdDelete.Enabled = True
                    cmdNuevo.Enabled = False

            End Select

            Set_Helpers()

            cmbEntidades.Enabled = False
            txtEndpoint.Enabled = False

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Listar_Interfaces()
        Try
            cmbInterfaces.Properties.Columns.Clear()
            cmbInterfaces.Properties.DisplayMember = "descripcion"
            cmbInterfaces.Properties.ValueMember = "Id"
            cmbInterfaces.Properties.DataSource = clsLnI_nav_config_enc.Get_All_By_DMS()
            'cmbInterfaces.ItemIndex = -1
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Listar_Entidades()
        Try
            cmbEntidades.Properties.Columns.Clear()
            cmbEntidades.Properties.DisplayMember = "nombre"     ' La columna que quieres mostrar
            cmbEntidades.Properties.ValueMember = "idnavent"     ' La columna que representa el valor
            cmbEntidades.Properties.DataSource = clsLnI_nav_ent.Listar()
            cmbEntidades.ItemIndex = -1

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbInterfaces_EditValueChanged(sender As Object, e As EventArgs) Handles cmbInterfaces.EditValueChanged
        Try
            If cmbInterfaces.EditValue <> -1 Then
                pBeINavConfigDet.IdNavConfigEnc = cmbInterfaces.EditValue

                Dim fila As Object = cmbInterfaces.GetSelectedDataRow

                If fila IsNot Nothing Then
                    cmbEntidades.Enabled = True
                    cmbEntidades.Text = fila.Item("entidad")
                    cmbEntidades.Enabled = False
                End If

                If cmbEntidades.Text = "" Then
                    txtEndpoint.Enabled = False
                End If

            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private Sub cmbEntidades_EditValueChanged(sender As Object, e As EventArgs) Handles cmbEntidades.EditValueChanged
        Try
            If cmbEntidades.EditValue <> -1 Then
                Dim fila As Object = cmbEntidades.GetSelectedDataRow
                If fila IsNot Nothing Then
                    txtEndpoint.Enabled = True
                    txtEndpoint.Text = fila.Item("endpoint")
                    txtEndpoint.Enabled = False
                End If

                If Not Existe_InterfaceEnc(cmbEntidades.EditValue, pBeINavConfigDet.IdNavConfigEnc) Then
                    XtraMessageBox.Show("El ejecutable no es valido para la interface seleccionada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Limpiar()
                    cmdAdd.Enabled = False
                    cmdActualizar.Enabled = False
                    cmdDelete.Enabled = False
                    cmbEntidades.EditValue = -1
                    cmbInterfaces.Focus()
                Else
                    pBeINavConfigDet.IdNavEnt = cmbEntidades.EditValue
                    'txtHoraInicio.Focus()
                    cargarDetalleHorarios(False)

                    GridView1.Focus()

                End If



            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function Existe_InterfaceEnc(pIdNavEnt As Integer, pINavConfigEnc As Integer) As Boolean
        Existe_InterfaceEnc = False
        Try

            Dim pNavEntidad = clsLnI_nav_config_ent.GetSingle_BY_IdNavEnt(pIdNavEnt, pINavConfigEnc)

            If pNavEntidad.IdNavConfigEnt > 1 Then
                Existe_InterfaceEnc = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Sub Dgrid_DoubleClick_(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Try


            Opciones_menu()

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle
                pBeINavConfigDet.IdNavEnt = Dr.Item("Ejecutable")
                pBeINavConfigDet.Idnavconfigdet = Dr.Item("Id")
                pBeINavConfigDet.IdNavConfigEnc = Dr.Item("Interface")
                '#GT22072025: obtener la parte numerica del día
                Dim valorCompleto As String = Dr.Item("Dia").ToString()
                Dim numeroDia As Integer = CInt(valorCompleto.Split("-"c)(0).Trim())
                pBeINavConfigDet.Dia = numeroDia
                pBeINavConfigDet.Activo = Dr.Item("activo")

                GridView1.FocusedRowHandle = lSelectionIndex

                'Listar_Entidades_Por_Dia()

                configuracion_Dia_Seleccionad(pBeINavConfigDet)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        guardarHorario()
        cargarDetalleHorarios(False)
    End Sub

    Private Sub guardarHorario()

        Try

            If checBoxLunes.Checked Then
                GuardarHorarioLaboral(1)
            End If
            If checBoxMartes.Checked Then
                GuardarHorarioLaboral(2)
            End If
            If checBoxMiercoles.Checked Then
                GuardarHorarioLaboral(3)
            End If
            If checBoxJueves.Checked Then
                GuardarHorarioLaboral(4)
            End If
            If checBoxViernes.Checked Then
                GuardarHorarioLaboral(5)
            End If
            If checBoxSabado.Checked Then
                GuardarHorarioLaboral(6)
            End If
            If checBoxDomingo.Checked Then
                GuardarHorarioLaboral(7)
                'GuardarHorarioLaboral(0)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function GuardarHorarioLaboral(ByVal dia As Integer) As Boolean

        GuardarHorarioLaboral = False
        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            Dim pBeINavConfigDet As New clsBeI_nav_config_det() With {.Idnavconfigdet = clsLnI_nav_config_det.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1,
            .IdNavEnt = cmbEntidades.EditValue,
            .IdNavConfigEnc = cmbInterfaces.EditValue,
            .Dia = dia,
            .HoraInicio = txtHoraInicio.EditValue,
            .HoraFin = txtHoraFin.EditValue,
            .Frecuencia = txtFrecuencia.Value,
            .Activo = True,
            .Fec_agr = Now,
            .User_agr = AP.UsuarioAp.IdUsuario,
            .Fec_mod = Now,
            .User_mod = AP.UsuarioAp.IdUsuario
                }

            If clsLnI_nav_config_det.Exists(pBeINavConfigDet, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                XtraMessageBox.Show("Ya existe un horario con este dia", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                GuardarHorarioLaboral = clsLnI_nav_config_det.Insertar(pBeINavConfigDet, clsTransaccion.lConnection, clsTransaccion.lTransaction) > 0
            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Private Sub cargarDetalleHorarios(ByVal pGuardo As Boolean)

        Try

            If pGuardo = False Then

                ListNavConfigDet = clsLnI_nav_config_det.Get_All_By_IdEnc(pBeINavConfigDet.IdNavConfigEnc, cmbEntidades.Text)

            End If


            Dim DT As New DataTable("Horarios")
            DT.Columns.Add("Id", GetType(Integer))
            DT.Columns.Add("Ejecutable", GetType(Integer))
            DT.Columns.Add("Interface", GetType(Integer))
            DT.Columns.Add("Dia", GetType(String))
            DT.Columns.Add("Inicio", GetType(String))
            DT.Columns.Add("Fin", GetType(String))
            DT.Columns.Add("Frecuencia", GetType(Integer))
            DT.Columns.Add("Activo", GetType(Boolean))
            Dgrid.DataSource = Nothing

            For Each registro As clsBeI_nav_config_det In ListNavConfigDet.OrderBy(Function(x) x.Dia)


                Dim dia = ObtenerDiaDeSemana(registro.Dia)

                Dim lRow As DataRow = DT.NewRow
                lRow(0) = registro.Idnavconfigdet
                lRow(1) = registro.IdNavEnt
                lRow(2) = registro.IdNavConfigEnc
                lRow(3) = registro.Dia & " - " & dia
                lRow(4) = registro.HoraInicio
                lRow(5) = registro.HoraFin
                lRow(6) = registro.Frecuencia
                lRow(7) = registro.Activo
                DT.Rows.Add(lRow)
            Next

            Dgrid.DataSource = DT

            Dgrid.Refresh()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub cmdNuevo_Click(sender As Object, e As EventArgs) Handles cmdNuevo.Click
        Try
            Limpiar_Inputs()
            Dgrid.DataSource = Nothing
            cmbInterfaces.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdActualizar_Click(sender As Object, e As EventArgs) Handles cmdActualizar.Click
        Try

            For Each configuracion_dia In ListNavConfigDet
                configuracion_dia.HoraInicio = txtHoraInicio.EditValue
                configuracion_dia.HoraFin = txtHoraFin.EditValue
                configuracion_dia.Frecuencia = txtFrecuencia.Value
                configuracion_dia.Fec_mod = Now
                configuracion_dia.Activo = chkActivo.Checked

                clsLnI_nav_config_det.Actualizar(configuracion_dia)

            Next

            cargarDetalleHorarios(False)

            txtHoraInicio.EditValue = 0
            txtHoraFin.EditValue = 0
            txtFrecuencia.Value = 0

        Catch ex As Exception

        End Try
    End Sub


    Private Function ObtenerDiaDeSemana(numero As Integer) As String
        Select Case numero
            Case 1
                Return "Lunes"
            Case 2
                Return "Martes"
            Case 3
                Return "Miércoles"
            Case 4
                Return "Jueves"
            Case 5
                Return "Viernes"
            Case 6
                Return "Sábado"
            Case 7
                Return "Domingo"
            Case Else
                Return "Número inválido"
        End Select
    End Function

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Try
            For Each configuracion_dia In ListNavConfigDet

                configuracion_dia.Fec_mod = Now
                configuracion_dia.Activo = False

                clsLnI_nav_config_det.Actualizar(configuracion_dia)

            Next

            cargarDetalleHorarios(False)

            txtHoraInicio.EditValue = 0
            txtHoraFin.EditValue = 0
            txtFrecuencia.Value = 0
        Catch ex As Exception

        End Try
    End Sub
End Class