Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class clsSyncERPAcuerdosComerciales : Inherits clsInterfaceBase
    Implements IDisposable

    Private VContadorBitacoraTomims As Integer = 0
    Private VContadorBitacoraIntermedia As Integer = 0

    Public Sub Dispose() Implements IDisposable.Dispose

    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Function Importar_Acuerdos_Comerciales_Desde_ERP(ByVal lblprg As RichTextBox,
                                                            ByRef prg As Windows.Forms.ProgressBar,
                                                            ByRef cnnLog As SqlConnection) As Boolean

        Importar_Acuerdos_Comerciales_Desde_ERP = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vCodigoBase As String = ""

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim lAcuerdosComerciales As New List(Of clsBeCEALSA_acuerdoscomerciales)

            '#GT02052024: obtener todos los acuerdos desde ERP
            lAcuerdosComerciales = clsLnCEALSA_acuerdoscomerciales.Get_All_With_Detail()


            BeNavEjecucionRes.Registros_ws = lAcuerdosComerciales.Count()
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            lblprg.AppendText(String.Format("Acuerdos comerciales en ERP: {0} ", lAcuerdosComerciales.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()
            lblprg.Refresh()

            prg.Maximum = lAcuerdosComerciales.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BePedidoCompraEnc As New clsBeTrans_oc_enc

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            VContadorBitacoraTomims = 0

            If lAcuerdosComerciales.Count > 0 Then

                prg.Visible = True

                '#GT30042024 limpiar las tablas intermedias i_nav
                clsLnI_nav_acuerdo_enc.Eliminar(lConnection, lTransaction)
                clsLnI_nav_acuerdo_det.Eliminar(lConnection, lTransaction)

                Dim BeINavAcuerdo As New clsBeI_nav_acuerdo_enc()


                Dim contador As Integer = lAcuerdosComerciales.Count

                For Each AC In lAcuerdosComerciales

                    lblprg.AppendText(String.Format("Procesando Acuerdo Comercial No.: {0} ", AC.Codacuerdo, vbNewLine))
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    BeINavAcuerdo = New clsBeI_nav_acuerdo_enc
                    BeINavAcuerdo.IdAcuerdo = clsLnI_nav_acuerdo_enc.MaxID(lConnection, lTransaction) + 1
                    BeINavAcuerdo.Idcliente = AC.Codcliente
                    BeINavAcuerdo.Codigo_acuerdo = AC.Codacuerdo
                    BeINavAcuerdo.Descripcion = AC.Descrip
                    BeINavAcuerdo.Tipo_cobro = AC.Tipocobro
                    BeINavAcuerdo.Cod_moneda = AC.Codmoneda
                    BeINavAcuerdo.Nom_moneda = AC.Moneda
                    BeINavAcuerdo.Procesado_wms = AC.Procesado_wms 'IIf(AC.Procesado_wms = "False", 0, 1)
                    BeINavAcuerdo.Estado = IIf(AC.Estado = "A", 1, 0)

                    '#GT02052024: antes de insertar en i_nav validamos que el cliente exista con codigo propietario.
                    If clsLnPropietarios.Existe_By_IdPropietario(BeINavAcuerdo.Idcliente, lConnection, lTransaction) Then

                        '#GT13052024: acuerdo nuevo se inserta
                        If BeINavAcuerdo.Procesado_wms = 0 Then

                            clsLnI_nav_acuerdo_enc.Insertar(BeINavAcuerdo, lConnection, lTransaction)

                            '#GT13052024: validar si acuerdo tiene detalle
                            If AC.lDetalle IsNot Nothing Then

                                If AC.lDetalle.Count > 0 Then

                                    Dim BeINavAcuerdoDet As New clsBeI_nav_acuerdo_det()

                                    For Each detalle In AC.lDetalle

                                        lblprg.AppendText(String.Format("Procesando Acuerdo Comercial No: {0} con Detalle: {1} ", detalle.Corre_cbmaeacuerdosservicios, detalle.Correlativo, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        BeINavAcuerdoDet = New clsBeI_nav_acuerdo_det
                                        BeINavAcuerdoDet.IdAcuerdoDet = clsLnI_nav_acuerdo_det.MaxID(lConnection, lTransaction) + 1
                                        BeINavAcuerdoDet.IdAcuerdo = BeINavAcuerdo.IdAcuerdo
                                        BeINavAcuerdoDet.Codigo_producto = detalle.Codigoproducto
                                        BeINavAcuerdoDet.Codigo_acuerdo = detalle.Corre_cbmaeacuerdosservicios
                                        BeINavAcuerdoDet.Servicio = detalle.Servicio
                                        BeINavAcuerdoDet.Nemonico = detalle.Nemonico
                                        BeINavAcuerdoDet.Correlativo_detalleacuerdo = detalle.Correlativo
                                        BeINavAcuerdoDet.Descripcion = detalle.Descripcion
                                        BeINavAcuerdoDet.Numero_unidades = detalle.Numero_unidades
                                        BeINavAcuerdoDet.Monto = detalle.Monto
                                        BeINavAcuerdoDet.Porcentaje = detalle.Porcentaje
                                        BeINavAcuerdoDet.Dias_eventos = detalle.Dias_eventos
                                        BeINavAcuerdoDet.corre_cbcatalogoproductos = detalle.Corre_cbcatalogoproductos
                                        BeINavAcuerdoDet.Procesado_wms = detalle.Procesado_wms
                                        BeINavAcuerdoDet.Estado = detalle.Estado
                                        BeINavAcuerdoDet.Prioridad = detalle.Prioridad
                                        clsLnI_nav_acuerdo_det.Insertar(BeINavAcuerdoDet, lConnection, lTransaction)

                                    Next

                                End If

                            Else

                                lblprg.AppendText(String.Format("Procesando Acuerdo Comercial No.: {0} {1} ", AC.Codacuerdo, " Sin Detalle ", vbNewLine))
                                lblprg.AppendText(vbNewLine)
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            End If

                            prg.Value = vContador
                            vContador += 1
                            VContadorBitacoraTomims += 1
                        Else

                            '#GT13052024: si esta procesado, pero tiene detalle sin procesado, agregar.
                            If AC.lDetalle IsNot Nothing Then

                                If AC.lDetalle.Count > 0 Then

                                    '#GT13052024: si esta procesado, pero tiene nuevo detalle, requerimos el encabezado en i_nav_acuerdos_enc
                                    clsLnI_nav_acuerdo_enc.Insertar(BeINavAcuerdo, lConnection, lTransaction)

                                    Dim BeINavAcuerdoDet As New clsBeI_nav_acuerdo_det()

                                    For Each detalle In AC.lDetalle

                                        lblprg.AppendText(String.Format("Procesando Acuerdo Comercial Existente No: {0} con nuevo detalle: {1} ", detalle.Corre_cbmaeacuerdosservicios, detalle.Correlativo, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        BeINavAcuerdoDet.IdAcuerdoDet = clsLnI_nav_acuerdo_det.MaxID(lConnection, lTransaction) + 1
                                        BeINavAcuerdoDet.IdAcuerdo = BeINavAcuerdo.IdAcuerdo
                                        BeINavAcuerdoDet.Codigo_producto = detalle.Codigoproducto
                                        BeINavAcuerdoDet.Codigo_acuerdo = detalle.Corre_cbmaeacuerdosservicios
                                        BeINavAcuerdoDet.Servicio = detalle.Servicio
                                        BeINavAcuerdoDet.Nemonico = detalle.Nemonico
                                        BeINavAcuerdoDet.Correlativo_detalleacuerdo = detalle.Correlativo
                                        BeINavAcuerdoDet.Descripcion = detalle.Descripcion
                                        BeINavAcuerdoDet.Numero_unidades = detalle.Numero_unidades
                                        BeINavAcuerdoDet.Monto = detalle.Monto
                                        BeINavAcuerdoDet.Porcentaje = detalle.Porcentaje
                                        BeINavAcuerdoDet.Dias_eventos = detalle.Dias_eventos
                                        BeINavAcuerdoDet.corre_cbcatalogoproductos = detalle.Corre_cbcatalogoproductos
                                        BeINavAcuerdoDet.Procesado_wms = detalle.Procesado_wms
                                        BeINavAcuerdoDet.Estado = detalle.Estado
                                        BeINavAcuerdoDet.Prioridad = detalle.Prioridad
                                        clsLnI_nav_acuerdo_det.Insertar(BeINavAcuerdoDet, lConnection, lTransaction)

                                    Next

                                End If

                            End If

                            prg.Value = vContador
                            vContador += 1
                            VContadorBitacoraTomims += 1

                        End If

                        contador = contador - 1
                    Else
                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("No se encontró el IdCliente para el código de cliente: " & BeINavAcuerdo.Idcliente & " Ya acutalizó maestro de clientes?")
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        'Throw New Exception("No se encontró el IdCliente para el código de cliente: " & BeINavAcuerdo.IdCliente)
                    End If

                Next


                '#GT02052024: YA NO SE VALIDA SI CLIENTE EXISTE EN I_NAV_CLIENTES.

                'Dim lAcuerdosComercialesForEnc As New List(Of clsBeCEALSA_acuerdoscomerciales)
                'lAcuerdosComercialesForEnc = clsLnCEALSA_acuerdoscomerciales.Get_All()
                'Dim BeINavAcuerdoEnc As New clsBeI_nav_acuerdo_enc

                'Dim lAcuerdosComercialesForEnc As New List(Of clsBeI_nav_acuerdo_enc)
                'lAcuerdosComercialesForEnc = clsLnI_nav_acuerdo_enc.Get_All()

                'If lAcuerdosComercialesForEnc.Count > 0 Then

                '    prg.Maximum = lAcuerdosComercialesForEnc.Count
                '    prg.Value = 0 : vContador = 0

                '    For Each AC In lAcuerdosComercialesForEnc

                '        lblprg.AppendText(vbNewLine)
                '        lblprg.AppendText(String.Format("Asociando Acuerdo No.:{0} con Cliente:{1} {2} ", AC.Codacuerdo, AC.Codcliente, vbNewLine))
                '        lblprg.AppendText(vbNewLine)
                '        lblprg.SelectionStart = lblprg.TextLength
                '        lblprg.ScrollToCaret()


                '        'BeINavAcuerdoEnc.IdContrato = clsLnI_nav_acuerdo_enc.MaxID(lConnection, lTransaction) + 1
                '        BeINavAcuerdoEnc.IdCliente = clsLni_nav_cliente.Get_IdCliente_By_Codigo(AC.Codcliente, lConnection, lTransaction)
                '        BeINavAcuerdoEnc.IdAcuerdo = clsLnI_nav_acuerdo_enc.Get_IdAcuerdo_By_Codigo(AC.Codacuerdo, lConnection, lTransaction)

                '        If BeINavAcuerdoEnc.IdCliente = -1 Then
                '            lblprg.AppendText(vbNewLine)
                '            lblprg.AppendText("No se encontró el IdCliente para el código de cliente: " & AC.Codcliente & " Ya acutalizó maestro de clientes?")
                '            lblprg.AppendText(vbNewLine)
                '            lblprg.SelectionStart = lblprg.TextLength
                '            lblprg.ScrollToCaret()
                '            Throw New Exception("No se encontró el IdCliente para el código de cliente: " & AC.Codcliente)
                '        End If

                '        If BeINavAcuerdoEnc.IdAcuerdo = -1 Then


                '            BeINavAcuerdoEnc.IdAcuerdo = clsLnI_nav_acuerdo_enc.Get_IdAcuerdo_By_Nombre(AC.Descrip, vCodigoBase, lConnection, lTransaction)

                '            If BeINavAcuerdoEnc.IdAcuerdo = -1 Then
                '                lblprg.AppendText("No se encontró el IdAcuerdo de WMS para el código de acuerdo: " & AC.Codcliente)
                '                lblprg.AppendText(vbNewLine)
                '                lblprg.SelectionStart = lblprg.TextLength
                '                lblprg.ScrollToCaret()
                '                Throw New Exception("No se encontró el IdAcuerdo de WMS para el código de acuerdo: " & AC.Codcliente)
                '            Else
                '                lblprg.AppendText(vbNewLine)
                '                lblprg.AppendText("Se encontró el IdAcuerdo bajo el nombre del servicio: " & AC.Descrip & " Se asociará al cliente con el acuerdo base No.:" & vCodigoBase)
                '                lblprg.SelectionStart = lblprg.TextLength
                '                lblprg.ScrollToCaret()
                '            End If

                '        End If

                '        If Not clsLnI_nav_acuerdo_enc.Existe_Acuerdo(BeINavAcuerdoEnc.IdCliente, BeINavAcuerdoEnc.IdAcuerdo, lConnection, lTransaction) Then
                '            clsLnI_nav_acuerdo_enc.Insertar(BeINavAcuerdoEnc, lConnection, lTransaction)
                '        Else
                '            lblprg.AppendText(vbNewLine)
                '            lblprg.AppendText("El acuerdo comercial bajo el nombre del servicio: " & AC.Descrip & " Se encuentra duplicado para el cliente No.:" & AC.Codcliente)
                '            lblprg.SelectionStart = lblprg.TextLength
                '            lblprg.ScrollToCaret()
                '        End If

                '        prg.Value = vContador

                '        vContador += 1

                '        VContadorBitacoraTomims += 1

                '    Next

                'End If


            End If

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText(String.Format("Acuerdos comerciales procesados  correctamente: {0}", VContadorBitacoraTomims))
            lblprg.AppendText(vbNewLine)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            lblprg.AppendText(String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Acuerdos_Comerciales_Desde_ERP = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            lblprg.AppendText(String.Format("Error al insertar acuerdos comerciales desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
        End Try

    End Function

    Public Function Insertar_Acuerdos_Comerciales_Desde_ERP(ByRef lblprg As RichTextBox,
                                                                                   ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                   Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                   Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Acuerdos_Comerciales_Desde_ERP = False


        Dim lConnectionWMS As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransactionWMS As SqlTransaction = Nothing
        Dim CnnLogWMS As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

        'Dim lConnectionWMS As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        'Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        'Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing

        Dim DifCant As Double = 0


        Dim lConnectionERP As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransactionERP As SqlTransaction = Nothing

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Acuerdo_Comerciales") Then

                    lblprg.AppendText("La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Exit Function

                End If

            End If

            CnnLogWMS.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = 0 'clsLnI_nav_ejecucion_enc.MaxID(CnnLogWMS)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLogWMS)

            BeNavEjecucionRes.IdEjecucionRes = 0 'clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLogWMS) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLogWMS)

            BeNavEjecRes = BeNavEjecucionRes

            '#EJC20210306: Abrir conexión a BD WMS
            lConnectionWMS.Open() : lTransactionWMS = lConnectionWMS.BeginTransaction(IsolationLevel.ReadCommitted)

            '#EJC20210306: Abrir conexión a BD ERP
            lConnectionERP.Open() : lTransactionERP = lConnectionERP.BeginTransaction(IsolationLevel.ReadCommitted)


            'lblprg.AppendText("Iniciando transacción a BD: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Acuerdos_Comerciales_Desde_ERP(lblprg, prg, CnnLogWMS) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde WS?", "Interface acuerdos comerciales.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Acuerdos_Comerciales_Desde_ERP(lblprg, prg, CnnLogWMS) Then
                        Exit Function
                    End If
                End If

            End If

            '---------------------------------------------------------------------------------
            '#GT02052024: Trasladamos de Intermedia hacia TOMWMS

            Dim listAcuerdosNav_NoProcesados As New List(Of clsBeI_nav_acuerdo_enc)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Consultando acuerdos en tabla intermedia ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            '#GT12052024: traemos los acuerdos no procesados almacenados temporalmente en i_nav
            listAcuerdosNav_NoProcesados = clsLnI_nav_acuerdo_enc.Get_All_With_Detail(lConnectionWMS, lTransactionWMS)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Acuerdos en tabla intermedia: {0}", listAcuerdosNav_NoProcesados.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()


            If listAcuerdosNav_NoProcesados.Count > 0 Then

                Dim wmsTransAcuerdoEnc As New clsBeTrans_acuerdoscomerciales_enc
                Dim wmsTransAcuerdoDet As New clsBeTrans_acuerdoscomerciales_det

                prg.Maximum = listAcuerdosNav_NoProcesados.Count
                Dim vContador As Integer = 0
                prg.Value = 0

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("********** INICIO DE INSERCIÓN EN TABLA DE TOMWMS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()


                For Each Acuerdo As clsBeI_nav_acuerdo_enc In listAcuerdosNav_NoProcesados

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText("**********************************************************")
                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText(String.Format("Procesando Acuerdo: {0} y cliente: {1} ({2} de {3})", Acuerdo.Codigo_acuerdo, Acuerdo.Idcliente, vContador + 1, listAcuerdosNav_NoProcesados.Count))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    vContador += 1
                    prg.Value = vContador

                    Dim ExisteAcuerdoEnc As New clsBeTrans_acuerdoscomerciales_enc
                    ExisteAcuerdoEnc = clsLnTrans_acuerdoscomerciales_enc.Existe(Acuerdo.Codigo_acuerdo, lConnectionWMS, lTransactionWMS)

                    '#GT13052024: si ya existe el encabezado, validamos detalle
                    If ExisteAcuerdoEnc IsNot Nothing Then

                        If Acuerdo.lDetalle IsNot Nothing Then

                            If Acuerdo.lDetalle.Count > 0 Then

                                Dim BeTransAcuerdoDet As New clsBeTrans_acuerdoscomerciales_det
                                For Each detalle As clsBeI_nav_acuerdo_det In Acuerdo.lDetalle


                                    '#GT13052024: validar que el detalle no exista en WMS antes de insertar.
                                    If Not clsLnTrans_acuerdoscomerciales_det.Existe_by_AcuerdoEnc(detalle.Codigo_acuerdo, detalle.Correlativo_detalleacuerdo, lConnectionWMS, lTransactionWMS) Then

                                        BeTransAcuerdoDet = New clsBeTrans_acuerdoscomerciales_det
                                        BeTransAcuerdoDet.IdAcuerdoDet = clsLnTrans_acuerdoscomerciales_det.MaxID(lConnectionWMS, lTransactionWMS) + 1
                                        BeTransAcuerdoDet.IdAcuerdoEnc = ExisteAcuerdoEnc.IdAcuerdoEnc
                                        BeTransAcuerdoDet.Codigo_producto = detalle.Codigo_producto
                                        BeTransAcuerdoDet.Servicio = detalle.Servicio
                                        BeTransAcuerdoDet.Nemonico = detalle.Nemonico
                                        BeTransAcuerdoDet.Codigo_acuerdo = detalle.Codigo_acuerdo
                                        BeTransAcuerdoDet.Correlativo_detalleacuerdo = detalle.Correlativo_detalleacuerdo
                                        BeTransAcuerdoDet.Descripcion = detalle.Descripcion
                                        BeTransAcuerdoDet.Numero_unidades = detalle.Numero_unidades
                                        BeTransAcuerdoDet.Monto = detalle.Monto
                                        BeTransAcuerdoDet.Porcentaje = detalle.Porcentaje
                                        BeTransAcuerdoDet.Dias_eventos = detalle.Dias_eventos
                                        BeTransAcuerdoDet.Corre_cbcatalogoproductos = detalle.corre_cbcatalogoproductos
                                        BeTransAcuerdoDet.Estado = 0
                                        BeTransAcuerdoDet.Prioridad = 0
                                        BeTransAcuerdoDet.User_agr = 1
                                        BeTransAcuerdoDet.Fec_agr = Now
                                        BeTransAcuerdoDet.User_mod = 1
                                        BeTransAcuerdoDet.Fec_mod = Now
                                        clsLnTrans_acuerdoscomerciales_det.Insertar(BeTransAcuerdoDet, lConnectionWMS, lTransactionWMS)

                                        lblprg.AppendText(vbNewLine)
                                        lblprg.AppendText(String.Format("Procesando Acuerdo Existente: {0}  Inserta Nuevo Detalle: {1} ({2} de {3})", detalle.Codigo_acuerdo, BeTransAcuerdoDet.Correlativo_detalleacuerdo, vContador, listAcuerdosNav_NoProcesados.Count))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                    Else


                                        lblprg.AppendText(vbNewLine)
                                        lblprg.AppendText(String.Format("Omitiendo Acuerdo Existente: {0} con Detalle: {1} ({2} de {3})", detalle.Codigo_acuerdo, detalle.Correlativo_detalleacuerdo, vContador, listAcuerdosNav_NoProcesados.Count))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                    End If

                                Next

                            End If

                        End If

                    Else

                        wmsTransAcuerdoEnc = New clsBeTrans_acuerdoscomerciales_enc
                        wmsTransAcuerdoEnc.IdAcuerdoEnc = clsLnTrans_acuerdoscomerciales_enc.MaxID(lConnectionWMS, lTransactionWMS) + 1
                        wmsTransAcuerdoEnc.IdCliente = Acuerdo.Idcliente
                        wmsTransAcuerdoEnc.Codigo_acuerdo = Acuerdo.Codigo_acuerdo
                        wmsTransAcuerdoEnc.Descripcion = Acuerdo.Descripcion
                        wmsTransAcuerdoEnc.Tipo_cobro = Acuerdo.Tipo_cobro
                        wmsTransAcuerdoEnc.Cod_moneda = Acuerdo.Cod_moneda
                        wmsTransAcuerdoEnc.Moneda = Acuerdo.Nom_moneda
                        wmsTransAcuerdoEnc.Estado = 1
                        wmsTransAcuerdoEnc.user_agr = 1
                        wmsTransAcuerdoEnc.fec_agr = Now
                        wmsTransAcuerdoEnc.user_mod = 1
                        wmsTransAcuerdoEnc.fec_mod = Now
                        wmsTransAcuerdoEnc.fec_erp = Now
                        clsLnTrans_acuerdoscomerciales_enc.Insertar(wmsTransAcuerdoEnc, lConnectionWMS, lTransactionWMS)

                        If Acuerdo.lDetalle IsNot Nothing Then

                            If Acuerdo.lDetalle.Count > 0 Then

                                Dim BeTransAcuerdoDet As New clsBeTrans_acuerdoscomerciales_det

                                For Each detalle As clsBeI_nav_acuerdo_det In Acuerdo.lDetalle

                                    BeTransAcuerdoDet = New clsBeTrans_acuerdoscomerciales_det
                                    BeTransAcuerdoDet.IdAcuerdoDet = clsLnTrans_acuerdoscomerciales_det.MaxID(lConnectionWMS, lTransactionWMS) + 1
                                    BeTransAcuerdoDet.IdAcuerdoEnc = wmsTransAcuerdoEnc.IdAcuerdoEnc
                                    BeTransAcuerdoDet.Codigo_producto = detalle.Codigo_producto
                                    BeTransAcuerdoDet.Servicio = detalle.Servicio
                                    BeTransAcuerdoDet.Nemonico = detalle.Nemonico
                                    BeTransAcuerdoDet.Codigo_acuerdo = detalle.Codigo_acuerdo
                                    BeTransAcuerdoDet.Correlativo_detalleacuerdo = detalle.Correlativo_detalleacuerdo
                                    BeTransAcuerdoDet.Descripcion = detalle.Descripcion
                                    BeTransAcuerdoDet.Numero_unidades = detalle.Numero_unidades
                                    BeTransAcuerdoDet.Monto = detalle.Monto
                                    BeTransAcuerdoDet.Porcentaje = detalle.Porcentaje
                                    BeTransAcuerdoDet.Dias_eventos = detalle.Dias_eventos
                                    BeTransAcuerdoDet.Corre_cbcatalogoproductos = detalle.corre_cbcatalogoproductos
                                    BeTransAcuerdoDet.Estado = 0
                                    BeTransAcuerdoDet.Prioridad = 0
                                    BeTransAcuerdoDet.User_agr = 1
                                    BeTransAcuerdoDet.Fec_agr = Now
                                    BeTransAcuerdoDet.User_mod = 1
                                    BeTransAcuerdoDet.Fec_mod = Now
                                    clsLnTrans_acuerdoscomerciales_det.Insertar(BeTransAcuerdoDet, lConnectionWMS, lTransactionWMS)

                                    'lblprg.AppendText(vbNewLine)
                                    lblprg.AppendText(String.Format("Procesando Nuevo Acuerdo: {0}  Inserta Nuevo Detalle: {1} ({2} de {3})", detalle.Codigo_acuerdo, BeTransAcuerdoDet.Correlativo_detalleacuerdo, vContador, listAcuerdosNav_NoProcesados.Count))
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                Next

                            End If

                        End If


                    End If

                    Acuerdo.Procesado_wms = True

                    clsLnI_nav_acuerdo_enc.Actualizar_Procesado_WMS(Acuerdo, lConnectionWMS, lTransactionWMS)
                    clsLnCEALSA_acuerdoscomerciales.Actualizar_Procesado_WMS(Acuerdo, lConnectionERP, lTransactionERP)

                Next

            End If


            lTransactionWMS.Commit() : lTransactionERP.Commit()


            lblprg.AppendText(vbNewLine)
            '#EJC20171107_REF04_0254AM: Desplegar cantidad de registros de acuerdos procesados
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA DE TOMIMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Acuerdos/Detalles procesados correctamente: {0}", VContadorBitacoraTomims))
            lblprg.AppendText(vbNewLine)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            lblprg.AppendText(String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTomims

            If VContadorBitacoraIntermedia = VContadorBitacoraTomims Then
                BeNavEjecucionRes.Exitosa = True
            Else
                BeNavEjecucionRes.Exitosa = False
            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLogWMS)

        Catch ex As Exception

            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            prg.Value = 0

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                              "",
                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                              BeConfigDet.Idnavconfigdet, CnnLogWMS)

            lblprg.AppendText(String.Format("Error al insertar acuerdo comercial a tabla de TOMIMS: {0} {1}", ex.Message, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnectionERP.State = ConnectionState.Open Then lConnectionERP.Close()
            If CnnLogWMS.State = ConnectionState.Open Then CnnLogWMS.Close()
        End Try

    End Function

    Public Function Marcar_AcuerdoComercial_Sincronizado_En_ERP(ByVal pNoDocumento As String) As Boolean

        Marcar_AcuerdoComercial_Sincronizado_En_ERP = False

        Try



        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class