Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmRegularizarInventarioRFID


    Public SinMovs As Integer = 0
    Public ConMovs As Integer = 0
    Public NoRecal As Integer = 0
    Private ListCiclico As New List(Of clsBeTrans_inv_ciclico_rfid)
    Private ListMovimientos As New List(Of clsBeTrans_movimientos)
    Private ListStock As New List(Of clsBeStock)
    Public gBeInventario As New clsBeTrans_inv_enc
    Public pBeTransAjustEnc As New clsBeTrans_ajuste_enc
    Private lBeTransAjusteDet As New List(Of clsBeTrans_ajuste_det)
    Private ListMovs As New List(Of clsBeI_nav_barras_rfid_mov)
    Private pBeMovs As New clsBeTrans_movimientos
    Private ListStockNuevo As New List(Of clsBeStock)
    Private pBeAjusteDet As New clsBeTrans_ajuste_det
    Private pBeStock As New clsBeStock
    Private vCantidad As Double = 0.0
    Private vPeso As Double = 0.0
    Private Diferencia_Stock_Vrs_Conteo As Double = 0
    Private IdTipoTarea As Integer = 0
    Private vLote As String = ""
    Private Producto As New clsBeProducto
    Private IdAjusteDet As Integer = 0
    Private IdAjusteEnc As Integer = 0
    Private lOperaciones As New List(Of KeyValuePair(Of Integer, Integer))
    Private ReadOnly mProductoByProductoBodegaCache As New Dictionary(Of Integer, clsBeProducto)
    'Private ReadOnly mTallaColorCache As New Dictionary(Of Integer, clsBeProducto_talla_color)
    'Private ReadOnly mTallaCodigoCache As New Dictionary(Of Integer, String)
    'Private ReadOnly mColorCodigoCache As New Dictionary(Of Integer, String)
    Private ReadOnly mBodegaERPCache As New Dictionary(Of String, Integer)
    Private mIdMotivoAjusteCache As Integer = -1
    Private mRegularizacionUltimoTick As Integer = 0
    Private mRegularizacionTotalAjustes As Integer = 0
    Private mRegularizacionProcesados As Integer = 0

    Dim ListBeStockNuevo As New List(Of clsBeStock)

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Limpiar_Caches_Regularizacion()

        mProductoByProductoBodegaCache.Clear()
        'mTallaColorCache.Clear()
        'mTallaCodigoCache.Clear()
        'mColorCodigoCache.Clear()
        mBodegaERPCache.Clear()
        mIdMotivoAjusteCache = -1
        mRegularizacionUltimoTick = 0

    End Sub

    Private Sub frmRegularizarInventarioRFID_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            dtFecha.EditValue = gBeInventario.Fec_agr
            dtHora.EditValue = gBeInventario.Fec_agr
            IMS.Listar_BodegasPorPropietario(cmbBodega, gBeInventario.Idpropietario)

            cmbBodega.EditValue = gBeInventario.IdBodega

            Select Case Modo

                Case TipoTrans.Nuevo

                    clsLnTrans_inv_ciclico_rfid.Actualizar_Regularizar_By_IdInventarioEnc(gBeInventario.Idinventarioenc, True)

                    Cargar_Datos_RFID()

                Case TipoTrans.Editar

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Cargar_Datos_RFID()

        Dim clsTransaccion As New clsTransaccion
        Dim vRegularizarUpdateSuspendido As Boolean = False
        Dim vSalidaUpdateSuspendido As Boolean = False

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Cargando datos RFID...")

        Try

            Dim DT As New DataTable

            clsTransaccion.Begin_Transaction()

            lblPrg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            '/*************************************************************************************************/
            'No fueron leídos por la HH,todavía están en stock, y no hay movimiento que justifique su ausencia.

            lblPrg.Caption = "Cargando candidatos RFID..."
            lblPrg.Refresh()

            'Grid 1: candidatos a ajuste negativo
            GridView1.BeginDataUpdate()
            vRegularizarUpdateSuspendido = True
            GridView1.GroupSummary.Clear()


            grdRegularizar.DataSource = clsLnTrans_inv_ciclico_rfid.Get_All_RFID_Candidatos_Ajuste_Negativo(
                                        gBeInventario.Idinventarioenc,
                                        gBeInventario.Fec_agr,
                                        Now,
                                        gBeInventario.IdBodega,
                                        clsTransaccion.lConnection,
                                        clsTransaccion.lTransaction
                                    )


            If GridView1.RowCount > 0 Then

                GridView1.Columns("IdStock").Visible = False
                GridView1.Columns("IdProductoBodega").Visible = False

                GridView1.OptionsView.ShowFooter = True
                GridView1.BestFitColumns(True)

                If GridView1.Columns.ColumnByFieldName("barra_epc") IsNot Nothing Then
                    GridView1.Columns("barra_epc").Caption = "EPC-SSCC"
                End If

                If GridView1.Columns.ColumnByFieldName("cantidad") IsNot Nothing Then
                    GridView1.Columns("cantidad").Caption = "Cantidad"
                End If

                If GridView1.Columns.ColumnByFieldName("Observacion") IsNot Nothing Then
                    GridView1.Columns("Observacion").Caption = "Observación"
                End If
            End If


            '/***************************************************************************************/
            'HH reporta cantidad = 0, No existen en stock RFID, SÍ tienen salida confirmada

            lblPrg.Caption = "Cargando salidas confirmadas RFID..."
            lblPrg.Refresh()

            'Grid 2: tags no leídos pero con salida confirmada

            GridView2.BeginDataUpdate()
            vSalidaUpdateSuspendido = True
            GridView2.GroupSummary.Clear()

            dgridMovimientos.DataSource = clsLnTrans_inv_ciclico_rfid.Get_All_RFID_Salidas_Confirmadas(
                                            gBeInventario.Idinventarioenc,
                                            gBeInventario.Fec_agr,
                                            Now,
                                            gBeInventario.IdBodega,
                                            clsTransaccion.lConnection,
                                            clsTransaccion.lTransaction)



            If GridView2.RowCount > 0 Then
                GridView2.OptionsView.ShowFooter = True
                GridView2.BestFitColumns(True)

                If GridView2.Columns.ColumnByFieldName("barra_epc") IsNot Nothing Then
                    GridView2.Columns("barra_epc").Caption = "EPC-SSCC"
                End If

                If GridView2.Columns.ColumnByFieldName("Fecha") IsNot Nothing Then
                    GridView2.Columns("Fecha").DisplayFormat.FormatString = "G"
                End If

                If GridView2.Columns.ColumnByFieldName("Observacion") IsNot Nothing Then
                    GridView2.Columns("Observacion").Caption = "Observación"
                End If
            End If

            '#GT02062026: si no hay faltantes, mostrar aviso y deshabilitar botones.
            Dim vTotalCandidatos As Integer = GridView1.RowCount
            Dim vTotalSalidasConfirmadas As Integer = GridView2.RowCount

            If vTotalCandidatos = 0 AndAlso vTotalSalidasConfirmadas = 0 Then

                Dim ajustesNegativos As New List(Of clsBeTrans_inv_ciclico_rfid)

                cmdRegularizar.Enabled = False

                Try
                    SplashScreenManager.CloseForm(False)
                Catch
                End Try

                If XtraMessageBox.Show("No hay diferencias." & vbCrLf & vbCrLf &
                           "¿Desea cerrar el inventario?",
                           Text,
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Cerrar_Inventario_RFID(gBeInventario, ajustesNegativos,
                                              AP.UsuarioAp,
                                              clsTransaccion.lConnection,
                                              clsTransaccion.lTransaction) Then

                        XtraMessageBox.Show("El inventario fue cerrado correctamente.",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                        Close()
                    Else
                        XtraMessageBox.Show("No se pudo cerrar el inventario.",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
                    End If

                Else

                    Close()

                End If

            Else

                cmdRegularizar.Enabled = True

            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

            SplashScreenManager.CloseForm(False)

            If vRegularizarUpdateSuspendido Then
                GridView1.EndDataUpdate()
            End If

            If vSalidaUpdateSuspendido Then
                GridView2.EndDataUpdate()
            End If
            prg.Visible = False
            lblPrg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Function Cerrar_Inventario_RFID(ByVal BeTransInvEnc As clsBeTrans_inv_enc,
                                            ByVal ListBeStockNuevo As List(Of clsBeTrans_inv_ciclico_rfid),
                                            ByVal Usuario As clsBeUsuario,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction) As Boolean



        Try

            clsLnTrans_inv_ciclico.Cerrar_Inventario_RFID(BeTransInvEnc, ListBeStockNuevo, Usuario, lConnection, lTransaction)

            Return True

        Catch ex As Exception


            XtraMessageBox.Show("Error al cerrar inventario RFID: " & ex.Message,
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)


        End Try

    End Function

    Private Sub cmdRegularizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdRegularizar.ItemClick
        Try

            If XtraMessageBox.Show("¿Iniciar proceso de regularizacion?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
            If XtraMessageBox.Show("¡Este proceso no se puede revertir !" & vbCrLf & "¿Está seguro de continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

            If clsLnEmpresa.Get_Id_Motivo_Ajuste(AP.IdEmpresa) = 0 Then
                XtraMessageBox.Show("No tiene definido el IdMotivoAjuste por defecto.",
                   Text,
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation)
                Return
            End If

            If Aplica_Inventario() Then
                XtraMessageBox.Show("El inventario ha sido aplicado correctamente.",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
                Close()
            Else
                XtraMessageBox.Show("No se pudo aplicar la regularización",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Aplica_Inventario() As Boolean

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Procesando ajustes de inventario...")

        Aplica_Inventario = False

        Dim clsTrans As New clsTransaccion()

        Try

            clsTrans.Open_Connection()
            clsTrans.Begin_Transaction()

            Limpiar_Caches_Regularizacion()
            lOperaciones.Clear()
            lBeTransAjusteDet.Clear()
            ListMovs.Clear()
            ListStockNuevo.Clear()

            Actualizar_Progreso_Regularizacion("Cargando inventario ciclico RFID", 0, 1, True)

            'Universo RFID del inventario.
            ListCiclico = clsLnTrans_inv_ciclico_rfid.Get_All_By_IdInventarioEnc(gBeInventario.Idinventarioenc,
                                                                             clsTrans.lConnection,
                                                                             clsTrans.lTransaction)

            'Universo real a validar: solo registros cantidad = 0 y marcados para regularizar.
            Dim universoCantidadCero As List(Of clsBeTrans_inv_ciclico_rfid) =
            ListCiclico.Where(Function(x) x IsNot Nothing AndAlso
                                          x.Cantidad = 0 AndAlso
                                          x.Regularizar = True).ToList()

            If Not universoCantidadCero.Any() Then

                clsTrans.RollBack_Transaction()

                XtraMessageBox.Show("No existen registros RFID pendientes de regularización.",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)

                Return False

            End If

            Actualizar_Progreso_Regularizacion("Clasificando ajustes RFID", 0, Math.Max(universoCantidadCero.Count, 1), True)

            Dim vIdPropietarioBodega As Integer =
            clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(gBeInventario.IdBodega,
                                                                                    gBeInventario.Idpropietario,
                                                                                    clsTrans.lConnection,
                                                                                    clsTrans.lTransaction)

            'Lista 1: candidatos reales a ajuste negativo.
            Dim DTCandidatos As DataTable =
            clsLnTrans_inv_ciclico_rfid.Get_All_RFID_Candidatos_Ajuste_Negativo(gBeInventario.Idinventarioenc,
                                                                                gBeInventario.Fec_agr,
                                                                                Now,
                                                                                gBeInventario.IdBodega,
                                                                                clsTrans.lConnection,
                                                                                clsTrans.lTransaction)

            'Lista 2: salidas confirmadas. Informativa / no acción.
            Dim DTSalidasConfirmadas As DataTable =
            clsLnTrans_inv_ciclico_rfid.Get_All_RFID_Salidas_Confirmadas(gBeInventario.Idinventarioenc,
                                                                         gBeInventario.Fec_agr,
                                                                         Now,
                                                                         gBeInventario.IdBodega,
                                                                         clsTrans.lConnection,
                                                                         clsTrans.lTransaction)

            Dim hsCandidatos As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            Dim hsSalidasConfirmadas As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

            If DTCandidatos IsNot Nothing AndAlso DTCandidatos.Columns.Contains("tag") Then
                For Each row As DataRow In DTCandidatos.Rows
                    If row IsNot Nothing AndAlso Not IsDBNull(row("tag")) Then

                        Dim vTag As String = row("tag").ToString().Trim()

                        If vTag <> "" Then
                            hsCandidatos.Add(vTag)
                        End If

                    End If
                Next
            End If

            If DTSalidasConfirmadas IsNot Nothing AndAlso DTSalidasConfirmadas.Columns.Contains("tag") Then
                For Each row As DataRow In DTSalidasConfirmadas.Rows
                    If row IsNot Nothing AndAlso Not IsDBNull(row("tag")) Then

                        Dim vTag As String = row("tag").ToString().Trim()

                        If vTag <> "" Then
                            hsSalidasConfirmadas.Add(vTag)
                        End If

                    End If
                Next
            End If

            'Candidatos que sí se darán de baja.
            Dim ajustesNegativos As List(Of clsBeTrans_inv_ciclico_rfid) =
            universoCantidadCero.Where(Function(x)
                                           Dim vSSCC As String = If(x.SSCC, "").Trim()
                                           Return vSSCC <> "" AndAlso hsCandidatos.Contains(vSSCC)
                                       End Function).ToList()

            'Registros cantidad = 0 que no están clasificados ni como candidato ni como salida confirmada.
            Dim listaInconsistencias As List(Of clsBeTrans_inv_ciclico_rfid) =
            universoCantidadCero.Where(Function(x)
                                           Dim vSSCC As String = If(x.SSCC, "").Trim()
                                           Return vSSCC = "" OrElse
                                                  (Not hsCandidatos.Contains(vSSCC) AndAlso
                                                   Not hsSalidasConfirmadas.Contains(vSSCC))
                                       End Function).ToList()

            If listaInconsistencias.Any() Then

                clsTrans.RollBack_Transaction()

                XtraMessageBox.Show("Existen registros RFID con cantidad = 0 que no pudieron clasificarse como candidato a ajuste negativo ni como salida confirmada." &
                                vbCrLf & vbCrLf &
                                "Registros inconsistentes: " & listaInconsistencias.Count.ToString() &
                                vbCrLf &
                                "Revise la información antes de aplicar la regularización.",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)

                Return False

            End If

            If Not ajustesNegativos.Any() Then

                clsTrans.RollBack_Transaction()

                XtraMessageBox.Show("No existen candidatos RFID para ajuste negativo. Los registros pendientes corresponden a salidas confirmadas o no requieren ajuste.",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)

                Return False

            End If

            mRegularizacionTotalAjustes = ajustesNegativos.Count
            mRegularizacionProcesados = 0

            Actualizar_Progreso_Regularizacion("Preparando ajustes RFID", 0, Math.Max(mRegularizacionTotalAjustes, 1), True)

            ProcesarAjustePorTipo(ajustesNegativos,
                                  clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo,
                                  vIdPropietarioBodega,
                                  "Negativo",
                                  clsTrans)


            Actualizar_Progreso_Regularizacion("Cerrando Inventario RFID", 0, Math.Max(mRegularizacionTotalAjustes, 1), True)

            Cerrar_Inventario_RFID(gBeInventario,
                                   ajustesNegativos,
                                   AP.UsuarioAp,
                                   clsTrans.lConnection,
                                   clsTrans.lTransaction)

            clsTrans.Commit_Transaction()

            Actualizar_Progreso_Regularizacion("Regularizacion RFID finalizada",
                                           Math.Max(mRegularizacionTotalAjustes, 1),
                                           Math.Max(mRegularizacionTotalAjustes, 1),
                                           True)

            Aplica_Inventario = True

        Catch ex As Exception

            Try
                clsTrans.RollBack_Transaction()
            Catch
            End Try

            Throw New Exception("Error en Aplica_Inventario: " & ex.Message, ex)

        Finally

            clsTrans.Close_Conection()
            SplashScreenManager.CloseForm(False)

        End Try

        Return Aplica_Inventario

    End Function

    Private Sub Actualizar_Progreso_Regularizacion(ByVal pMensaje As String,
                                                   ByVal pActual As Integer,
                                                   ByVal pTotal As Integer,
                                                   Optional ByVal pForzar As Boolean = False)

        Dim vAhora As Integer = Environment.TickCount

        If Not pForzar AndAlso pActual < pTotal AndAlso Math.Abs(vAhora - mRegularizacionUltimoTick) < 250 Then
            Return
        End If

        mRegularizacionUltimoTick = vAhora

        Try
            Dim vTotal As Integer = Math.Max(pTotal, 1)
            Dim vActual As Integer = Math.Min(Math.Max(pActual, 0), vTotal)
            Dim vTexto As String = String.Format("{0} ({1}/{2})", pMensaje, vActual, vTotal)

            If SplashScreenManager.Default IsNot Nothing Then
                SplashScreenManager.Default.SetWaitFormDescription(vTexto)
            End If

            prg.Visible = True
            prg.Minimum = 0
            prg.Maximum = vTotal
            prg.Value = vActual
            lblPrg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            lblPrg.Caption = vTexto
            lblPrg.Refresh()

            Application.DoEvents()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ProcesarAjustePorTipoPositivo(ajustes As List(Of clsBeTrans_inv_ciclico),
                                      tipoAjuste As clsDataContractDI.tTipoAjusteWMS,
                                      clsTrans As clsTransaccion,
                                      vIdPropietarioBodega As Integer,
                                      pReferencia As String)

        Dim vIdStock As Integer = 0
        Dim vIdStockLista As Integer = 0
        Dim vIdMotivoAjuste As Integer = 0

        Try

            ' Crear encabezado para el ajuste
            pBeTransAjustEnc = New clsBeTrans_ajuste_enc()
            pBeTransAjustEnc.IdAjusteenc = clsLnTrans_ajuste_enc.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
            pBeTransAjustEnc.IdBodega = gBeInventario.IdBodega
            pBeTransAjustEnc.Idusuario = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.Fecha = Date.Now
            pBeTransAjustEnc.IdCentroCosto = gBeInventario.IdCentroCosto
            pBeTransAjustEnc.IdPropietarioBodega = vIdPropietarioBodega
            pBeTransAjustEnc.User_agr = AP.UsuarioAp.Nombres
            pBeTransAjustEnc.User_mod = AP.UsuarioAp.Nombres
            pBeTransAjustEnc.Referencia = $"Ajuste {pReferencia} generado por inventario No. {gBeInventario.Idinventarioenc}"
            pBeTransAjustEnc.Ajuste_Por_Inventario = gBeInventario.Idinventarioenc
            pBeTransAjustEnc.Enviado_A_ERP = True
            pBeTransAjustEnc.Centro_Costo_Erp = AP.Bodega.Centro_Costo_Erp
            pBeTransAjustEnc.Centro_Costo_Dir_Erp = AP.Bodega.Centro_Costo_Dir_Erp
            pBeTransAjustEnc.Centro_Costo_Dep_Erp = AP.Bodega.Centro_Costo_Dep_Erp

            pBeAjusteDet.IdMotivoAjuste = 0
            vIdMotivoAjuste = Get_IdMotivoAjuste_Regularizacion(clsTrans.lConnection, clsTrans.lTransaction)

            ' Insertar encabezado
            clsLnTrans_ajuste_enc.Insertar(pBeTransAjustEnc, clsTrans.lConnection, clsTrans.lTransaction)

            ' Procesar los detalles de los ajustes
            For Each BeTransInvCiclico In ajustes
                mRegularizacionProcesados += 1
                Actualizar_Progreso_Regularizacion("Generando ajuste " & pReferencia,
                                                   mRegularizacionProcesados,
                                                   Math.Max(mRegularizacionTotalAjustes, 1))

                ' Crear detalle del ajuste
                Dim pBeAjusteDet As New clsBeTrans_ajuste_det()
                pBeAjusteDet.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                pBeAjusteDet.IdAjusteEnc = pBeTransAjustEnc.IdAjusteenc
                pBeAjusteDet.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                pBeAjusteDet.IdProductoEstado = BeTransInvCiclico.IdProductoEst_nuevo
                pBeAjusteDet.IdPresentacion = BeTransInvCiclico.IdPresentacion
                pBeAjusteDet.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                pBeAjusteDet.IdUbicacion = BeTransInvCiclico.IdUbicacion
                pBeAjusteDet.Cantidad_original = BeTransInvCiclico.Cant_stock
                pBeAjusteDet.Cantidad_nueva = BeTransInvCiclico.Cantidad
                pBeAjusteDet.Lote_original = BeTransInvCiclico.Lote_stock
                pBeAjusteDet.Lote_nuevo = BeTransInvCiclico.Lote
                pBeAjusteDet.Fecha_vence_original = BeTransInvCiclico.Fecha_vence_stock
                pBeAjusteDet.Fecha_vence_nueva = BeTransInvCiclico.Fecha_vence
                pBeAjusteDet.Idtipoajuste = tipoAjuste
                pBeAjusteDet.lic_plate = BeTransInvCiclico.lic_plate
                pBeAjusteDet.Enviado = True

                If clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado = tipoAjuste Then
                    pBeAjusteDet.Codigo_ajuste = clsDataContractDI.tTipoTarea.CESTI
                ElseIf clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote = tipoAjuste Then
                    pBeAjusteDet.Codigo_ajuste = clsDataContractDI.tTipoTarea.AJLOTEPI
                ElseIf clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento = tipoAjuste Then
                    pBeAjusteDet.Codigo_ajuste = clsDataContractDI.tTipoTarea.AJVENCEPI
                ElseIf clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo = tipoAjuste Then
                    pBeAjusteDet.Codigo_ajuste = clsDataContractDI.tTipoTarea.AJCANTPI
                ElseIf clsDataContractDI.tTipoAjusteWMS.Ajuste_TallaColor = tipoAjuste Then
                    pBeAjusteDet.Codigo_ajuste = clsDataContractDI.tTipoTarea.AJCANTPI
                End If

                pBeAjusteDet.Peso_original = BeTransInvCiclico.Peso
                pBeAjusteDet.Peso_nuevo = BeTransInvCiclico.Peso
                pBeAjusteDet.lic_plate = BeTransInvCiclico.lic_plate
                pBeAjusteDet.IdStock = BeTransInvCiclico.IdStock

                'Dim vProducto As clsBeProducto = Get_Producto_Regularizacion(BeTransInvCiclico.IdProductoBodega,
                '                                                             clsTrans.lConnection,
                '                                                             clsTrans.lTransaction)

                'pBeAjusteDet.Codigo_producto = vProducto.IdProducto
                'pBeAjusteDet.Codigo_producto = vProducto.Codigo
                'pBeAjusteDet.Nombre_producto = vProducto.Nombre
                'pBeAjusteDet.IdMotivoAjuste = vIdMotivoAjuste

                'Dim BodegaERP As Integer = Get_IdBodegaERP_Regularizacion(AP.Bodega.codigo_bodega_erp,
                '                                                          clsTrans.lConnection,
                '                                                          clsTrans.lTransaction)


                'pBeAjusteDet.IdBodegaERP = Val(BodegaERP)

                ' Insertar detalle
                If BeTransInvCiclico.Fecha_vence <> BeTransInvCiclico.Fecha_vence_stock Then
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento
                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDet, clsTrans.lConnection, clsTrans.lTransaction)
                End If

                If BeTransInvCiclico.Lote_stock <> BeTransInvCiclico.Lote Then

                    Dim pBeAjusteDetLote As New clsBeTrans_ajuste_det

                    clsPublic.CopyObject(pBeAjusteDet, pBeAjusteDetLote)

                    pBeAjusteDetLote.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeAjusteDetLote.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote

                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDetLote, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If BeTransInvCiclico.IdProductoEstado <> BeTransInvCiclico.IdProductoEst_nuevo Then

                    Dim pBeAjusteDetEstado As New clsBeTrans_ajuste_det

                    clsPublic.CopyObject(pBeAjusteDet, pBeAjusteDetEstado)

                    pBeAjusteDetEstado.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeAjusteDetEstado.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado

                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDetEstado, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                '#AT20260213 Ajuste talla y color deber ir positivo
                If BeTransInvCiclico.IdProductoTallaColor <> BeTransInvCiclico.IdProductoTallaColor_nuevo Then

                    Dim pBeAjusteDetEstado As New clsBeTrans_ajuste_det

                    clsPublic.CopyObject(pBeAjusteDet, pBeAjusteDetEstado)

                    pBeAjusteDetEstado.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeAjusteDetEstado.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo

                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDetEstado, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If tipoAjuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo Then

                    Dim pBeAjusteDetNegativo As New clsBeTrans_ajuste_det

                    clsPublic.CopyObject(pBeAjusteDet, pBeAjusteDetNegativo)

                    pBeAjusteDetNegativo.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeAjusteDetNegativo.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo

                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDetNegativo, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If tipoAjuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo Then

                    Dim pBeAjusteDetPositivo As New clsBeTrans_ajuste_det

                    clsPublic.CopyObject(pBeAjusteDet, pBeAjusteDetPositivo)

                    pBeAjusteDetPositivo.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeAjusteDetPositivo.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo

                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDetPositivo, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                ' Insertar movimiento directamente
                Dim pBeMovs As New clsBeTrans_movimientos()
                pBeMovs = New clsBeTrans_movimientos()
                pBeMovs.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                pBeMovs.IdEmpresa = AP.IdEmpresa
                pBeMovs.IdBodegaOrigen = AP.IdBodega
                pBeMovs.IdTransaccion = gBeInventario.Idinventarioenc
                pBeMovs.IdPropietarioBodega = vIdPropietarioBodega
                pBeMovs.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                pBeMovs.IdUbicacionOrigen = BeTransInvCiclico.IdUbicacion
                pBeMovs.IdUbicacionDestino = BeTransInvCiclico.IdUbicacion
                pBeMovs.IdPresentacion = BeTransInvCiclico.IdPresentacion
                pBeMovs.IdEstadoOrigen = BeTransInvCiclico.IdProductoEstado
                pBeMovs.IdEstadoDestino = BeTransInvCiclico.IdProductoEst_nuevo
                pBeMovs.IdUnidadMedida = Producto.IdUnidadMedidaBasica
                pBeMovs.IdTipoTarea = IdTipoTarea
                pBeMovs.IdBodegaDestino = AP.IdBodega
                pBeMovs.IdRecepcion = 0
                pBeMovs.IdRecepcionDet = 0
                pBeMovs.Serie = ""
                pBeMovs.Lote = BeTransInvCiclico.Lote_stock
                pBeMovs.Fecha_vence = BeTransInvCiclico.Fecha_vence_stock
                pBeMovs.Fecha = Now
                pBeMovs.Barra_pallet = BeTransInvCiclico.lic_plate
                pBeMovs.Hora_ini = Now
                pBeMovs.Hora_fin = Now
                pBeMovs.Fecha_agr = Now
                pBeMovs.Usuario_agr = AP.UsuarioAp.IdUsuario

                '#CM_20180821_426PM: Cantidad correcta para movimientos de ajustes. 
                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo Then
                    If BeTransInvCiclico.Cantidad < BeTransInvCiclico.Cant_stock Then
                        pBeMovs.Cantidad = BeTransInvCiclico.Cantidad
                    Else
                        pBeMovs.Cantidad = Math.Round((BeTransInvCiclico.Cantidad - BeTransInvCiclico.Cant_stock), 6)
                    End If
                ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo Then
                    pBeMovs.Cantidad = Math.Round((BeTransInvCiclico.Cant_stock - BeTransInvCiclico.Cantidad), 6)
                ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_TallaColor Then
                    pBeMovs.Cantidad = BeTransInvCiclico.Cantidad

                    If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote Then
                        pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.AJLOTEPI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento Then
                        pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.AJVENCEPI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then
                        pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.CESTI
                    End If

                End If

                pBeMovs.Cantidad_hist = BeTransInvCiclico.Cant_stock
                pBeMovs.Peso = vPeso
                pBeMovs.Peso_hist = BeTransInvCiclico.Peso_stock
                pBeMovs.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                pBeMovs.IdPresentacion = BeTransInvCiclico.IdPresentacion

                If BeTransInvCiclico.Fecha_vence <> BeTransInvCiclico.Fecha_vence_stock Then
                    pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.AJVENCEPI
                    clsLnTrans_movimientos.Insertar(pBeMovs, clsTrans.lConnection, clsTrans.lTransaction)
                End If

                '#CKFK20241202
                '****************************
                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then

                    Dim pBeMovsInverso As New clsBeTrans_movimientos()
                    pBeMovsInverso = New clsBeTrans_movimientos()
                    pBeMovsInverso.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                    pBeMovsInverso.IdEmpresa = AP.IdEmpresa
                    pBeMovsInverso.IdBodegaOrigen = AP.IdBodega
                    pBeMovsInverso.IdTransaccion = gBeInventario.Idinventarioenc
                    pBeMovsInverso.IdPropietarioBodega = vIdPropietarioBodega
                    pBeMovsInverso.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                    pBeMovsInverso.IdUbicacionOrigen = BeTransInvCiclico.IdUbicacion
                    pBeMovsInverso.IdUbicacionDestino = BeTransInvCiclico.IdUbicacion
                    pBeMovsInverso.IdPresentacion = BeTransInvCiclico.IdPresentacion
                    pBeMovsInverso.IdEstadoOrigen = BeTransInvCiclico.IdProductoEstado
                    pBeMovsInverso.IdEstadoDestino = BeTransInvCiclico.IdProductoEst_nuevo
                    pBeMovsInverso.IdUnidadMedida = Producto.IdUnidadMedidaBasica
                    pBeMovsInverso.IdTipoTarea = IdTipoTarea
                    pBeMovsInverso.IdBodegaDestino = AP.IdBodega
                    pBeMovsInverso.IdRecepcion = 0
                    pBeMovsInverso.IdRecepcionDet = 0
                    pBeMovsInverso.Serie = ""
                    pBeMovsInverso.Lote = BeTransInvCiclico.Lote_stock
                    pBeMovsInverso.Fecha_vence = BeTransInvCiclico.Fecha_vence_stock
                    pBeMovsInverso.Fecha = Now
                    pBeMovsInverso.Barra_pallet = BeTransInvCiclico.lic_plate
                    pBeMovsInverso.Hora_ini = Now
                    pBeMovsInverso.Hora_fin = Now
                    pBeMovsInverso.Fecha_agr = Now
                    pBeMovsInverso.Usuario_agr = AP.UsuarioAp.IdUsuario

                    pBeMovsInverso.Cantidad = BeTransInvCiclico.Cantidad

                    If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote Then
                        pBeMovsInverso.IdTipoTarea = clsDataContractDI.tTipoTarea.AJLOTENI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento Then
                        pBeMovsInverso.IdTipoTarea = clsDataContractDI.tTipoTarea.AJVENCENI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then
                        pBeMovsInverso.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then
                        pBeMovsInverso.IdTipoTarea = clsDataContractDI.tTipoTarea.TALLACOLOR
                    End If

                    pBeMovsInverso.Cantidad_hist = BeTransInvCiclico.Cant_stock
                    pBeMovsInverso.Peso = vPeso
                    pBeMovsInverso.Peso_hist = BeTransInvCiclico.Peso_stock
                    pBeMovsInverso.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                    pBeMovsInverso.IdPresentacion = BeTransInvCiclico.IdPresentacion

                    clsLnTrans_movimientos.Insertar(pBeMovsInverso, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                '****************************

                If BeTransInvCiclico.Lote_stock <> BeTransInvCiclico.Lote AndAlso BeTransInvCiclico.Lote <> "" Then

                    Dim pBeMovsLote As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsLote)

                    pBeMovsLote.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsLote.IdTipoTarea = clsDataContractDI.tTipoTarea.AJLOTEPI

                    clsLnTrans_movimientos.Insertar(pBeMovsLote, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If BeTransInvCiclico.IdProductoEstado <> BeTransInvCiclico.IdProductoEst_nuevo AndAlso BeTransInvCiclico.IdProductoEst_nuevo <> 0 Then

                    Dim pBeMovsEstado As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsEstado)

                    pBeMovsEstado.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsEstado.IdTipoTarea = clsDataContractDI.tTipoTarea.CESTI

                    clsLnTrans_movimientos.Insertar(pBeMovsEstado, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                '#AT20260213 Talla Color
                If BeTransInvCiclico.IdProductoTallaColor <> BeTransInvCiclico.IdProductoTallaColor_nuevo Then

                    Dim pBeMovsEstado As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsEstado)

                    pBeMovsEstado.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsEstado.IdTipoTarea = clsDataContractDI.tTipoTarea.TALLACOLOR

                    clsLnTrans_movimientos.Insertar(pBeMovsEstado, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If BeTransInvCiclico.IdUbicacion <> BeTransInvCiclico.IdUbicacion_nuevo AndAlso
                    BeTransInvCiclico.IdUbicacion_nuevo <> 0 Then

                    Dim pBeMovsUbicacion As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsUbicacion)

                    pBeMovsUbicacion.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsUbicacion.IdTipoTarea = clsDataContractDI.tTipoTarea.CUBII

                    pBeMovsUbicacion.IdUbicacionDestino = BeTransInvCiclico.IdUbicacion_nuevo

                    clsLnTrans_movimientos.Insertar(pBeMovsUbicacion, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo Then

                    Dim pBeMovsPositivos As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsPositivos)

                    pBeMovsPositivos.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsPositivos.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTPI

                    clsLnTrans_movimientos.Insertar(pBeMovsPositivos, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo Then

                    Dim pBeMovsNegativos As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsNegativos)

                    pBeMovsNegativos.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsNegativos.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI

                    clsLnTrans_movimientos.Insertar(pBeMovsNegativos, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                ' Llenar lista de stock
                Dim pBeStock As New clsBeStock()
                pBeStock = clsLnStock.GetSingle(BeTransInvCiclico.IdStock, clsTrans.lConnection, clsTrans.lTransaction)

                If pBeStock IsNot Nothing Then

                    pBeStock.IdStock = BeTransInvCiclico.IdStock
                    pBeStock.IdProductoBodega = BeTransInvCiclico.IdProductoBodega

                    pBeStock.Cantidad = BeTransInvCiclico.Cantidad

                    If BeTransInvCiclico.IdProductoEst_nuevo <> 0 Then
                        pBeStock.ProductoEstado.IdEstado = BeTransInvCiclico.IdProductoEst_nuevo
                        pBeStock.IdProductoEstado = BeTransInvCiclico.IdProductoEst_nuevo
                    Else
                        pBeStock.ProductoEstado.IdEstado = BeTransInvCiclico.IdProductoEstado
                        pBeStock.IdProductoEstado = BeTransInvCiclico.IdProductoEstado
                    End If

                    If BeTransInvCiclico.IdUbicacion_nuevo <> 0 Then
                        pBeStock.IdUbicacion = BeTransInvCiclico.IdUbicacion_nuevo
                    Else
                        pBeStock.IdUbicacion = BeTransInvCiclico.IdUbicacion
                    End If

                    pBeStock.Fecha_vence = BeTransInvCiclico.Fecha_vence

                    If BeTransInvCiclico.Lote <> BeTransInvCiclico.Lote_stock AndAlso BeTransInvCiclico.Lote <> "" Then
                        pBeStock.Lote = BeTransInvCiclico.Lote
                    End If

                    '#AT20260128 Actualizar talla color en stock
                    If BeTransInvCiclico.IdProductoTallaColor <> BeTransInvCiclico.IdProductoTallaColor_nuevo Then
                        pBeStock.IdProductoTallaColor = BeTransInvCiclico.IdProductoTallaColor_nuevo
                    Else
                        pBeStock.IdProductoTallaColor = BeTransInvCiclico.IdProductoTallaColor
                    End If

                    lOperaciones.Add(New KeyValuePair(Of Integer, Integer)(pBeStock.IdStock, tipoAjuste))

                Else

                    pBeStock = New clsBeStock()
                    pBeStock.IdBodega = BeTransInvCiclico.IdBodega
                    pBeStock.IdStock = 0
                    pBeStock.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                    pBeStock.Cantidad = BeTransInvCiclico.Cantidad
                    pBeStock.IdUbicacion = BeTransInvCiclico.IdUbicacion
                    pBeStock.Fecha_vence = BeTransInvCiclico.Fecha_vence
                    pBeStock.ProductoEstado.IdEstado = BeTransInvCiclico.IdProductoEst_nuevo
                    pBeStock.IdProductoEstado = BeTransInvCiclico.IdProductoEst_nuevo
                    pBeStock.Lote = BeTransInvCiclico.Lote
                    pBeStock.IdUbicacion = BeTransInvCiclico.IdUbicacion

                    pBeStock.IdPropietarioBodega = vIdPropietarioBodega
                    pBeStock.Presentacion.IdPresentacion = BeTransInvCiclico.IdPresentacion
                    pBeStock.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                    pBeStock.IdUbicacion = BeTransInvCiclico.IdUbicacion
                    pBeStock.IdUbicacion_anterior = BeTransInvCiclico.IdUbicacion
                    pBeStock.Fecha_Ingreso = Now
                    pBeStock.Activo = True
                    pBeStock.Peso = 0
                    pBeStock.Temperatura = 0
                    pBeStock.Fec_agr = Now
                    pBeStock.Fec_mod = Now
                    pBeStock.User_agr = BeTransInvCiclico.User_agr
                    pBeStock.User_mod = BeTransInvCiclico.User_agr
                    pBeStock.Pallet_No_Estandar = False
                    pBeStock.Lic_plate = BeTransInvCiclico.lic_plate
                    pBeStock.IdProductoTallaColor = BeTransInvCiclico.IdProductoTallaColor_nuevo

                    clsLnStock.Insertar(pBeStock, clsTrans.lConnection, clsTrans.lTransaction)

                    lOperaciones.Add(New KeyValuePair(Of Integer, Integer)(pBeStock.IdStock, tipoAjuste))

                End If

                ListStockNuevo.Add(pBeStock)

            Next

        Catch ex As Exception
            Throw New Exception($"Error en ProcesarAjustePorTipo: {ex.Message}")
        End Try

    End Sub

    Private Sub ProcesarAjustePorTipo(ByVal ajustes As List(Of clsBeTrans_inv_ciclico_rfid),
                                      ByVal tipoAjuste As Integer,
                                      ByVal IdPropietarioBodega As Integer,
                                      ByVal pReferencia As String,
                                      ByVal clsTrans As clsTransaccion)


        Try

            ' Crear encabezado de ajuste
            Dim idAjusteEnc As Integer = 0

            idAjusteEnc = clsLnTrans_ajuste_enc.Inserta_Encabezado_Ajuste(gBeInventario.Idinventarioenc,
                                                                          AP.UsuarioAp,
                                                                          gBeInventario.IdBodega,
                                                                          IdPropietarioBodega,
                                                                          gBeInventario.Idinventarioenc,
                                                                          gBeInventario.IdCentroCosto,
                                                                          pReferencia,
                                                                          clsTrans.lConnection,
                                                                          clsTrans.lTransaction)

            ' Procesar cada ajuste
            For Each item In ajustes
                mRegularizacionProcesados += 1
                Actualizar_Progreso_Regularizacion("Generando ajuste " & pReferencia,
                                                   mRegularizacionProcesados,
                                                   Math.Max(mRegularizacionTotalAjustes, 1))

                LLena_Objetos_Detalle_Ajuste(item,
                                             IdPropietarioBodega,
                                             clsTrans.lConnection,
                                             clsTrans.lTransaction,
                                             lBeTransAjusteDet,
                                             ListMovs,
                                             idAjusteEnc,
                                             tipoAjuste)
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LLena_Objetos_Detalle_Ajuste(ByVal BeTransInvCiclico As clsBeTrans_inv_ciclico_rfid,
                                             ByVal IdPropietarioBodega As Integer,
                                             ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction,
                                             ByRef lBeTransAjusteDet As List(Of clsBeTrans_ajuste_det),
                                             ByRef ListMovs As List(Of clsBeI_nav_barras_rfid_mov),
                                             ByVal IdAjusteEnc As Integer,
                                             ByVal TipoAjuste As Integer)

        Try
            ' Inicializar IDs y objetos necesarios
            Dim IdAjusteDet As Integer = clsLnTrans_ajuste_det.MaxID(lConnection, lTransaction) + 1
            Dim pBeAjusteDet As New clsBeTrans_ajuste_det
            Dim pBeMovs As New clsBeI_nav_barras_rfid_mov
            Dim vIdMotivoAjuste As Integer = Get_IdMotivoAjuste_Regularizacion(lConnection, lTransaction)

            ' Configuración de tareas según el tipo de ajuste
            Dim IdTipoTarea As Integer
            Select Case TipoAjuste
                Case clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote
                    IdTipoTarea = clsDataContractDI.tTipoTarea.AJLOTE
                Case clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento
                    IdTipoTarea = clsDataContractDI.tTipoTarea.AJVENC
                Case clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo
                    IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTPI
                Case clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo
                    '#AJUSTE NEGATIVO Y MERMA DETECTADA POR RFID.
                    IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTNIRFID
                    pBeMovs.IdRfidTipoMov = 6
                Case clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado
                    IdTipoTarea = clsDataContractDI.tTipoTarea.CESTI
                Case Else
                    Throw New Exception("Tipo de ajuste desconocido.")
            End Select

            ' Llenar los detalles del ajuste
            pBeAjusteDet.IdAjusteDet = IdAjusteDet
            pBeAjusteDet.IdAjusteEnc = IdAjusteEnc
            pBeAjusteDet.IdPropietarioBodega = IdPropietarioBodega
            pBeAjusteDet.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
            pBeAjusteDet.Lote_original = BeTransInvCiclico.Lote
            pBeAjusteDet.Cantidad_original = BeTransInvCiclico.Cantidad
            'pBeAjusteDet.Cantidad_nueva = BeTransInvCiclico.Cantidad
            pBeAjusteDet.Idtipoajuste = TipoAjuste
            pBeAjusteDet.Codigo_ajuste = IdTipoTarea
            pBeAjusteDet.Enviado = True
            pBeAjusteDet.lic_plate = BeTransInvCiclico.SSCC

            '#CKFK20250130 Cambié que esnuevolink solo sea pora los ajustes que no sean por cantidad
            If TipoAjuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo OrElse TipoAjuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo Then
                pBeAjusteDet.esnuevolink = 0
            Else
                pBeAjusteDet.esnuevolink = 1
            End If

            pBeAjusteDet.IdMotivoAjuste = vIdMotivoAjuste

            pBeAjusteDet.Codigo_Proveedor = Nothing
            pBeAjusteDet.Nombre_Proveedor = Nothing
            pBeAjusteDet.IdProveedor = 0

            lBeTransAjusteDet.Add(pBeAjusteDet)
            clsLnTrans_ajuste_det.Insertar(pBeAjusteDet, lConnection, lTransaction)

            ' Llenar los movimientos asociados
            pBeMovs.IdRfidMovimiento = clsLnI_nav_barras_rfid_mov.MaxID(lConnection, lTransaction)
            pBeMovs.IdBodega = AP.IdBodega
            pBeMovs.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
            pBeMovs.Lote = BeTransInvCiclico.Lote
            pBeMovs.Cantidad = BeTransInvCiclico.Cantidad


            'pBeMovs.IdTipoTarea = IdTipoTarea
            pBeMovs.Fec_agr = DateTime.Now
            pBeMovs.User_agr = AP.UsuarioAp.IdUsuario
            pBeMovs.Barra_epc = BeTransInvCiclico.SSCC

            ListMovs.Add(pBeMovs)
            clsLnI_nav_barras_rfid_mov.Insertar(pBeMovs, lConnection, lTransaction)
            'clsLnTrans_movimientos.Insertar(pBeMovs, lConnection, lTransaction)

        Catch ex As Exception
            Throw New Exception($"Error en LLena_Objetos_Detalle_Ajuste: {ex.Message}")
        End Try

    End Sub

    Private Function Get_IdMotivoAjuste_Regularizacion(ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As Integer

        If mIdMotivoAjusteCache = -1 Then
            Dim DT As DataTable = clsLnAjuste_motivo.Listar()
            mIdMotivoAjusteCache = 0

            If DT IsNot Nothing Then
                If DT.Rows.Count > 0 Then
                    mIdMotivoAjusteCache = DT.Rows(0).Item("IdMotivoAjuste")
                End If
                DT.Dispose()
            End If
        End If

        Return mIdMotivoAjusteCache

    End Function


End Class