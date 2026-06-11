Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmImpresionRecepcion_OC

    Public Event NotificarActualizacionLotesOC()

    Private Enum TipoProcesoLicencia
        SoloLicencia = 1
        LicenciaBulto = 2
    End Enum

    Public pTransOC_Enc As New clsBeTrans_oc_enc

    Private pImpresoraProdSeleccionada As String = ""
    Private pImpresoraLicSeleccionada As String = ""
    Private EsPrimeraImpresion As Boolean = False

    Private pBeTransOcDet As New clsBeTrans_oc_det()
    Private pCamasPorTarima As Integer
    Private pCajasPorCama As Integer
    Private pPresentacion As String
    Private BeBodega_Origen As clsBeBodega
    Private BeBodega_Destino As clsBeBodega
    Private pBeProductoPresentacion As New clsBeProducto_Presentacion

    ' =========================
    ' Estado operativo del flujo
    ' =========================
    Private pModoProcesoActual As TipoProcesoLicencia = TipoProcesoLicencia.SoloLicencia
    Private pBultosPendientesLicenciaActual As Integer = 0
    Private pLicenciaActualCerrada As Boolean = False
    Private pCapacidadObjetivoLicenciaActual As Integer = 0
    Private pUltimoBultoPuedeVariar As Boolean = True
    Private pTotalTarimasProducto As Integer = 0
    Private pCorrelativoTarimaActual As Integer = 0
    Private pTarimasImpresasAcumuladas As Integer = 0
    Private pModoReimpresion As Boolean = False
    ' #EJC20260610_FLOW_IMP_OC_FASEADA:
    ' Estado para trabajar fardos sobre una licencia ya generada (madre existente).
    Private pModoLicenciaExistenteBultos As Boolean = False
    Private pCapacidadObjetivoLicenciaSeleccionada As Integer = 0
    ' #EJC20260604_FIX_IMP_OC_LIC_MADRE_FARDO: ancla de licencia madre para fardos (evita saltos accidentales).
    Private pLicenciaMadreBulto As String = ""
    '#EJC20260605_FIX_IMP_OC_CONTADOR_ETIQUETAS_PRESENTACION:
    'Contador en memoria por licencia activa. Se persiste al cerrar licencia.
    Private pEtiquetasPresentacionImpresasLicenciaActual As Integer = 0

    Private Sub frmImpresionRecepcion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        BeBodega_Origen = New clsBeBodega()

        Try
            txtLicencia.Enabled = False
            txtVencimiento.Enabled = False
            txtPresentacion.Enabled = False
            txtFactor.Enabled = False
            txtPesoTarima.Visible = False

            Cargar_productos_oc()
            Cargar_Impresoras_Windows(cmbPrinterBarra)
            Cargar_Impresoras_Windows(cmbPrinterLicencia)

            BeBodega_Origen = clsLnBodega.GetSingle_By_Idbodega(pTransOC_Enc.IdBodega)

            EsPrimeraImpresion = True

            AsignarImpresoraInicial(cmbPrinterLicencia, frmRecepcion.pImpresoraLicSeleccionada)
            AsignarImpresoraInicial(cmbPrinterBarra, frmRecepcion.pImpresoraProdSeleccionada)

            If chkSoloLicencia IsNot Nothing Then chkSoloLicencia.Checked = True

            ReservarNuevaLicencia()
            AplicarModoProceso()
            ActualizarEstadoPantalla()
            ListarBarrasPallet()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ListarBarrasPallet(Optional ByVal pNotificar As Boolean = False)
        Dim dt As DataTable = clsLnTrans_oc_det_lote.Get_Barras_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(pTransOC_Enc.IdOrdenCompraEnc, pBeTransOcDet.IdOrdenCompraDet)
        dgridBarrasPallet.DataSource = dt
        ConfigurarGridLicencias()
        ActualizarPanelUltimaEtiqueta(dt)
        If pNotificar Then
            RaiseEvent NotificarActualizacionLotesOC()
        End If
    End Sub

    ' #EJC20260610_FIX_IMP_OC_REFRESH_CONTADOR_PRESENTACION:
    ' En modo Licencia+Bulto, persiste el contador de fardos en la licencia madre tras cada impresión.
    Private Sub PersistirContadorEtiquetasPresentacionLicenciaActual()
        Try
            If pModoProcesoActual <> TipoProcesoLicencia.LicenciaBulto Then Exit Sub
            If pTransOC_Enc Is Nothing OrElse pBeTransOcDet Is Nothing Then Exit Sub
            If pTransOC_Enc.IdOrdenCompraEnc <= 0 OrElse pBeTransOcDet.IdOrdenCompraDet <= 0 Then Exit Sub

            Dim licenciaMadre As String = ObtenerLicenciaMadreActiva()
            If String.IsNullOrWhiteSpace(licenciaMadre) Then Exit Sub

            Dim filasAfectadas As Integer = clsLnI_nav_barras_pallet.Actualizar_Cant_Etiquetas_Presentacion_Impresas(
                pTransOC_Enc.IdOrdenCompraEnc,
                pBeTransOcDet.IdOrdenCompraDet,
                licenciaMadre.Trim(),
                pEtiquetasPresentacionImpresasLicenciaActual)
            If filasAfectadas <= 0 Then
                Throw New Exception("No se actualizó el contador de fardos impresos para la licencia madre " & licenciaMadre.Trim())
            End If
        Catch
            Throw
        End Try
    End Sub

    Private Sub chkSoloLicencia_CheckedChanged(sender As Object, e As EventArgs) Handles chkSoloLicencia.CheckedChanged
        If chkSoloLicencia.Checked Then
            If chkLicenciaBulto IsNot Nothing Then chkLicenciaBulto.Checked = False
            AplicarModoProceso()
        End If
    End Sub

    Private Sub chkLicenciaBulto_CheckedChanged(sender As Object, e As EventArgs) Handles chkLicenciaBulto.CheckedChanged
        If chkLicenciaBulto.Checked Then
            If chkSoloLicencia IsNot Nothing Then chkSoloLicencia.Checked = False
            AplicarModoProceso()
        End If
    End Sub

    Private Sub AplicarModoProceso()
        If chkLicenciaBulto IsNot Nothing AndAlso chkLicenciaBulto.Checked Then
            pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto
        Else
            pModoProcesoActual = TipoProcesoLicencia.SoloLicencia
        End If

        Select Case pModoProcesoActual
            Case TipoProcesoLicencia.SoloLicencia
                txtCantidadLicencias.Enabled = True
                txtCantidadBarras.Enabled = False
                txtCopias.Enabled = True
                cmdImpresionBarra.Enabled = False
                cmdUnidad.Enabled = False

            Case TipoProcesoLicencia.LicenciaBulto
                txtCantidadLicencias.Value = 1
                txtCantidadLicencias.Enabled = False
                txtCantidadBarras.Enabled = True
                txtCopias.Enabled = True
                cmdImpresionBarra.Enabled = True
                cmdUnidad.Enabled = True
        End Select

        RecalcularCapacidadLicenciaActual()
        ActualizarEstadoPantalla()
    End Sub

    Private Sub ReservarNuevaLicencia()
        txtLicencia.Text = Genera_Licencia_BOF(AP.Bodega.IdBodega, AP.UsuarioAp.IdUsuario)
        ReiniciarEstadoLicenciaActual()
    End Sub

    Private Sub ReiniciarEstadoLicenciaActual()
        RecalcularCapacidadLicenciaActual()
        pBultosPendientesLicenciaActual = pCapacidadObjetivoLicenciaActual
        pLicenciaActualCerrada = False
        pEtiquetasPresentacionImpresasLicenciaActual = 0
        pModoLicenciaExistenteBultos = False
        pCapacidadObjetivoLicenciaSeleccionada = 0
        ' #EJC20260604_FIX_IMP_OC_LIC_MADRE_FARDO: al abrir nueva licencia de trabajo, queda anclada como licencia madre de fardos.
        pLicenciaMadreBulto = Convert.ToString(txtLicencia.Text).Trim()
        ActualizarEstadoPantalla()
    End Sub

    Private Function ObtenerLicenciaMadreActiva() As String
        If pModoProcesoActual <> TipoProcesoLicencia.LicenciaBulto Then
            Return Convert.ToString(txtLicencia.Text).Trim()
        End If

        If String.IsNullOrWhiteSpace(pLicenciaMadreBulto) Then
            ' #EJC20260604_FIX_IMP_OC_LIC_MADRE_FARDO: fallback defensivo si el estado fue reiniciado por flujo de UI.
            pLicenciaMadreBulto = Convert.ToString(txtLicencia.Text).Trim()
        End If

        Return pLicenciaMadreBulto
    End Function

    Private Sub RecalcularCapacidadLicenciaActual()
        pCamasPorTarima = Convert.ToInt32(txtCamaPorTarima.Value)
        pCajasPorCama = Convert.ToInt32(txtCajaPorCama.Value)
        pPresentacion = CStr(txtPresentacion.EditValue)
        ' #EJC20260610_FLOW_IMP_OC_FASEADA:
        ' Si el operador seleccionó una licencia ya generada, la capacidad real es la persistida en esa licencia.
        If pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto AndAlso pModoLicenciaExistenteBultos AndAlso pCapacidadObjetivoLicenciaSeleccionada > 0 Then
            pCapacidadObjetivoLicenciaActual = pCapacidadObjetivoLicenciaSeleccionada
        Else
            pCapacidadObjetivoLicenciaActual = Math.Max(0, pCamasPorTarima * pCajasPorCama)
        End If
    End Sub

    ' #EJC20260610_FLOW_IMP_OC_FASEADA:
    ' Carga estado operativo (capacidad/impresas/pendientes) desde una licencia madre ya generada.
    Private Sub CargarEstadoLicenciaSeleccionadaParaBultos(ByVal licencia As String)
        If String.IsNullOrWhiteSpace(licencia) Then Exit Sub
        If pTransOC_Enc Is Nothing OrElse pBeTransOcDet Is Nothing Then Exit Sub
        If pTransOC_Enc.IdOrdenCompraEnc <= 0 OrElse pBeTransOcDet.IdOrdenCompraDet <= 0 Then Exit Sub

        Dim dt As DataTable = clsLnTrans_oc_det_lote.Get_Barras_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(
            pTransOC_Enc.IdOrdenCompraEnc,
            pBeTransOcDet.IdOrdenCompraDet)

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then Exit Sub

        Dim dr As DataRow = dt.AsEnumerable().
            FirstOrDefault(Function(r) String.Equals(Convert.ToString(r("Licencia")).Trim(),
                                                     licencia.Trim(),
                                                     StringComparison.OrdinalIgnoreCase))

        If dr Is Nothing Then Exit Sub

        Dim capacidad As Integer = 0
        If dt.Columns.Contains("Cantidad_Presentacion") Then
            capacidad = ValorEntero(dr("Cantidad_Presentacion"))
        End If

        Dim impresas As Integer = 0
        If dt.Columns.Contains("cant_etiquetas_presentacion_impresas") Then
            impresas = ValorEntero(dr("cant_etiquetas_presentacion_impresas"))
        End If

        If capacidad < 0 Then capacidad = 0
        If impresas < 0 Then impresas = 0

        pModoLicenciaExistenteBultos = True
        pCapacidadObjetivoLicenciaSeleccionada = capacidad
        pLicenciaMadreBulto = licencia.Trim()
        pEtiquetasPresentacionImpresasLicenciaActual = impresas
        pBultosPendientesLicenciaActual = Math.Max(0, capacidad - impresas)
        pLicenciaActualCerrada = (pBultosPendientesLicenciaActual <= 0)

        txtLicencia.Text = pLicenciaMadreBulto
        txtCantidadBarras.Value = pBultosPendientesLicenciaActual
        ActualizarEstadoPantalla()
    End Sub

    Private Sub AvanzarALaSiguienteLicencia(ByVal clsTransaccion As clsTransaccion)
        Incrementar_Licencia_BOF_Info(AP.IdBodega,
                                      AP.UsuarioAp.IdUsuario,
                                      clsTransaccion.lConnection,
                                      clsTransaccion.lTransaction)

        txtLicencia.EditValue = Genera_Licencia_BOF(AP.Bodega.IdBodega, AP.UsuarioAp.IdUsuario)
        ReiniciarEstadoLicenciaActual()
    End Sub

    Private Function ValidarDatosBasicosProducto() As Boolean
        DxErrorProvider1.ClearErrors()

        If pBeTransOcDet Is Nothing OrElse pBeTransOcDet.IdProductoBodega <= 0 Then
            XtraMessageBox.Show("Seleccione un producto válido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If String.IsNullOrWhiteSpace(Convert.ToString(cmbLote.Text)) Then
            XtraMessageBox.Show("Seleccione un lote.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If String.IsNullOrWhiteSpace(Convert.ToString(txtLicencia.Text)) Then
            XtraMessageBox.Show("No existe una licencia activa para imprimir.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        RecalcularCapacidadLicenciaActual()

        If pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto AndAlso pCapacidadObjetivoLicenciaActual <= 0 Then
            XtraMessageBox.Show("La capacidad de la licencia debe ser mayor a cero.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If Not ValidarPesoTarimaContraLinea() Then
            Return False
        End If

        Return True

    End Function

    Private Function ValidarImpresora(ByVal control As BaseEdit, ByVal printerName As String, ByVal mensaje As String) As Boolean
        If String.IsNullOrWhiteSpace(printerName) Then
            DxErrorProvider1.SetError(control, mensaje)
            Return False
        End If

        Return True
    End Function

    Private Function PuedeImprimirBultos(ByVal cantidadSolicitada As Integer) As Boolean
        If pModoProcesoActual <> TipoProcesoLicencia.LicenciaBulto Then Return True

        If pLicenciaActualCerrada Then
            XtraMessageBox.Show("La licencia actual ya fue cerrada. Debe trabajar con la siguiente licencia.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If pBultosPendientesLicenciaActual <= 0 Then
            XtraMessageBox.Show("Ya se imprimió la cantidad máxima de bultos para esta licencia. Debe imprimir/cerrar la licencia antes de continuar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If cantidadSolicitada <= 0 Then
            XtraMessageBox.Show("La cantidad de bultos a imprimir debe ser mayor a cero.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If cantidadSolicitada > pBultosPendientesLicenciaActual Then
            XtraMessageBox.Show($"No puede imprimir {cantidadSolicitada} bultos. Pendientes para esta licencia: {pBultosPendientesLicenciaActual}.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        Return True
    End Function

    '#EJC20260605_FIX_IMP_OC_VALIDA_LIC_MADRE_PREVIA:
    'En flujo Licencia+Bulto, solo permitir fardos si la licencia madre ya existe/impresa en i_nav_barras_pallet.
    Private Function ValidarLicenciaMadreImpresaParaFardos(ByVal licenciaMadre As String) As Boolean

        If pModoProcesoActual <> TipoProcesoLicencia.LicenciaBulto Then Return True

        If String.IsNullOrWhiteSpace(licenciaMadre) Then
            XtraMessageBox.Show("No existe una licencia madre activa para imprimir fardos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If pTransOC_Enc Is Nothing OrElse pBeTransOcDet Is Nothing Then
            XtraMessageBox.Show("No se pudo validar la licencia madre. Recargue el producto y vuelva a intentar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If pTransOC_Enc.IdOrdenCompraEnc <= 0 OrElse pBeTransOcDet.IdOrdenCompraDet <= 0 Then
            XtraMessageBox.Show("No se pudo validar la licencia madre por falta de referencia de documento/línea.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        Dim existeLicenciaMadreImpresa As Boolean = clsLnI_nav_barras_pallet.Existe_Licencia_Madre_Impresa(pTransOC_Enc.IdOrdenCompraEnc,
                                                                                                             pBeTransOcDet.IdOrdenCompraDet,
                                                                                                             licenciaMadre.Trim())

        If Not existeLicenciaMadreImpresa Then
            XtraMessageBox.Show("Debe imprimir primero la licencia madre [" & licenciaMadre & "] para habilitar impresión de etiquetas de presentación/fardos.",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
            Return False
        End If

        Return True
    End Function

    Private Function DebeForzarCierreLicenciaAntesDeSeguir() As Boolean
        If pModoProcesoActual <> TipoProcesoLicencia.LicenciaBulto Then Return False
        Return pBultosPendientesLicenciaActual <= 0 AndAlso Not pLicenciaActualCerrada
    End Function

    Private Function PuedeImprimirLicenciaSolo() As Boolean
        If pModoProcesoActual <> TipoProcesoLicencia.SoloLicencia Then Return True

        Dim cantidad As Integer = Convert.ToInt32(txtCantidadLicencias.Value)
        If cantidad <= 0 Then
            XtraMessageBox.Show("La cantidad de licencias debe ser mayor a cero.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        Return True
    End Function

    Private Function PuedeCerrarEImprimirLicenciaConBultos() As Boolean

        If pModoProcesoActual <> TipoProcesoLicencia.LicenciaBulto Then Return True

        If pLicenciaActualCerrada Then
            XtraMessageBox.Show("La licencia actual ya fue impresa/cerrada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If pBultosPendientesLicenciaActual = pCapacidadObjetivoLicenciaActual Then
            If XtraMessageBox.Show("Esta licencia no tiene bultos impresos. ¿Desea imprimir/cerrar la licencia de todas formas?",
                                   Text,
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question) = DialogResult.No Then
                Return False
            End If
        End If

        pCorrelativoTarimaActual += 1

        Return True

    End Function

    Private Function ObtenerNombreProductoCorto() As String
        If pBeTransOcDet Is Nothing OrElse String.IsNullOrWhiteSpace(pBeTransOcDet.Nombre_producto) Then Return ""
        Return pBeTransOcDet.Nombre_producto.Substring(0, Math.Min(pBeTransOcDet.Nombre_producto.Length, 44)).Trim()
    End Function

    Private Function ObtenerFechaVence() As Date
        If txtVencimiento.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(Convert.ToString(txtVencimiento.EditValue)) Then
            Return New Date(1900, 1, 1)
        End If

        Dim d As Date
        If Date.TryParse(Convert.ToString(txtVencimiento.EditValue), d) Then
            Return d
        End If

        Return New Date(1900, 1, 1)
    End Function

    Private Function ObtenerCopiasSolicitadas() As Integer
        Dim vCopias As Integer

        Try
            vCopias = Convert.ToInt32(txtCopias.Value)
        Catch
            vCopias = 1
        End Try

        If vCopias <= 0 Then vCopias = 1
        Return vCopias
    End Function

    Private Function ObtenerTextoBodegaEtiqueta() As String
        Dim bodegaEtiqueta As clsBeBodega = Nothing

        ' #EJC20260610_FIX_IMP_OC_BODEGA_NOMBRE_OCULTO_CACHE:
        ' Fuerza lectura fresca de bodega para evitar valores stale en sesión/AP.Bodega.
        Try
            Dim idBodegaActual As Integer = 0

            If BeBodega_Origen IsNot Nothing AndAlso BeBodega_Origen.IdBodega > 0 Then
                idBodegaActual = BeBodega_Origen.IdBodega
            ElseIf AP IsNot Nothing AndAlso AP.Bodega IsNot Nothing AndAlso AP.Bodega.IdBodega > 0 Then
                idBodegaActual = AP.Bodega.IdBodega
            End If

            If idBodegaActual > 0 Then
                bodegaEtiqueta = clsLnBodega.GetSingle_By_Idbodega(idBodegaActual)
            End If
        Catch
            ' No interrumpir impresión por fallback de lectura de bodega.
        End Try

        If bodegaEtiqueta Is Nothing Then
            If BeBodega_Origen IsNot Nothing AndAlso BeBodega_Origen.IdBodega > 0 Then
                bodegaEtiqueta = BeBodega_Origen
            Else
                bodegaEtiqueta = AP.Bodega
            End If
        End If

        If bodegaEtiqueta Is Nothing Then Return ""

        Dim codigo As String = Convert.ToString(bodegaEtiqueta.Codigo).Trim()
        Dim nombre As String = Convert.ToString(bodegaEtiqueta.Nombre).Trim()

        If bodegaEtiqueta.ocultar_nombre_etiquetas_impresas Then
            Return codigo
        End If

        If String.IsNullOrWhiteSpace(nombre) Then Return codigo

        Return codigo & " - " & nombre
    End Function

    Private Function ConstruirZplProducto(ByVal pReDet As clsBeTrans_oc_det,
                                          Optional ByVal licenciaForzada As String = "") As String

        Dim vFechaVence As Date = ObtenerFechaVence()
        Dim vEmpresa As String = AP.Empresa.Nombre
        Dim vCodigoProducto As String = pReDet.Codigo_Producto
        Dim vNombreProducto As String = ObtenerNombreProductoCorto()
        Dim vLote As String = Convert.ToString(cmbLote.Text)

        If pBeProductoPresentacion Is Nothing OrElse pBeProductoPresentacion.IdTipoEtiqueta <= 0 Then
            Throw New Exception("No está definido el tipo de etiqueta para la presentación del producto.")
        End If

        Dim pTipoEtiqueta As Integer = pBeProductoPresentacion.IdTipoEtiqueta

        Dim Tipo_Etiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(pTipoEtiqueta)

        If Tipo_Etiqueta Is Nothing Then
            Throw New Exception("No se cargaron las propiedades de la etiqueta.")
        End If

        Dim tmpZPLString As String = Convert.ToString(Tipo_Etiqueta.codigo_zpl)

        If String.IsNullOrWhiteSpace(tmpZPLString) Then
            Throw New Exception($"{MethodBase.GetCurrentMethod.Name()} No está definido el formato de etiqueta")
        End If

        Dim licenciaImpresion As String = If(String.IsNullOrWhiteSpace(licenciaForzada),
                                             Convert.ToString(txtLicencia.Text).Trim(),
                                             licenciaForzada.Trim())

        Return String.Format(tmpZPLString,
                         ObtenerTextoBodegaEtiqueta(),
                         vEmpresa,
                         vCodigoProducto & " - " & vNombreProducto,
                         licenciaImpresion,
                         AP.UsuarioAp.Nombres & " " & AP.UsuarioAp.Apellidos & " / " & Now.ToString("yyyy-MM-dd HH:mm:ss"),
                         vLote,
                         vFechaVence.ToString("dd/MM/yy"),
                         pPresentacion,
                         1)

    End Function

    Private Function ConstruirZplLicencia(ByVal pReDet As clsBeTrans_oc_det,
                                      ByVal licenciaActual As String,
                                      ByVal cantidadPresentacion As Integer,
                                      ByVal clsTransaccion As clsTransaccion) As String

        Dim vEmpresa As String = AP.Empresa.Nombre
        Dim vCodigoProducto As String = pReDet.Codigo_Producto
        Dim vNombreProducto As String = If(String.IsNullOrWhiteSpace(pReDet.Nombre_producto),
                                       "",
                                       pReDet.Nombre_producto.Substring(0, Math.Min(pReDet.Nombre_producto.Length, 44)))

        Dim vLote As String = cmbLote.Text
        Dim vFechaVence As Date = ObtenerFechaVence()

        Dim pTipoEtiqueta As Integer = AP.Bodega.IdTipoEtiquetaLicencia
        Dim pTipoSimbologia As Integer = AP.Bodega.IdSimbologiaLicencia
        Dim pClasificacion As Integer = 2

        Dim Tipo_Etiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(pTipoEtiqueta,
                                                                        pTipoSimbologia,
                                                                        pClasificacion,
                                                                        clsTransaccion.lConnection,
                                                                        clsTransaccion.lTransaction)

        If Tipo_Etiqueta Is Nothing OrElse String.IsNullOrWhiteSpace(Tipo_Etiqueta.codigo_zpl) Then
            Throw New Exception("GT21012026: No está definido el formato de etiqueta")
        End If

        Dim tmpZPLString As String = Tipo_Etiqueta.codigo_zpl

        Dim vLicenciaQr As String = "$" & licenciaActual
        Dim vPesoOLicencia As String = ""

        If txtPesoTarima.Value > 0 Then
            vPesoOLicencia = "PESO:" & txtPesoTarima.Value
        Else
            vPesoOLicencia = "LIC:" & vLicenciaQr
        End If

        Dim args() As Object = {
        ObtenerTextoBodegaEtiqueta(),                                                                  ' {0}
        vEmpresa,                                                                                      ' {1}
        vCodigoProducto & " - " & vNombreProducto.Trim(),                                              ' {2}
        vLicenciaQr,                                                                                   ' {3}
        AP.UsuarioAp.Nombres & " " & AP.UsuarioAp.Apellidos & " / " & Now.ToString("yyyy-MM-dd HH:mm:ss"), ' {4}
        vLote,                                                                                         ' {5}
        vFechaVence.ToString("dd/MM/yy"),                                                              ' {6}
        pPresentacion,                                                                                 ' {7}
        cantidadPresentacion,                                                                          ' {8}
        If(txtPesoTarima.Value > 0, "PESO:" & txtPesoTarima.Value, ""),                                ' {9}
        vPesoOLicencia                                                                                 ' {10}
    }

        Dim regex As New Text.RegularExpressions.Regex("\{(\d+)\}")
        Dim matches = regex.Matches(tmpZPLString)

        If matches.Count > 0 Then
            Dim maxPlaceholderIndex As Integer = matches.Cast(Of Text.RegularExpressions.Match)().
            Select(Function(m) CInt(m.Groups(1).Value)).
            Max()

            If maxPlaceholderIndex >= args.Length Then
                Throw New InvalidOperationException(
                "El diseño de la etiqueta no coincide con la cantidad de parámetros. " &
                "Mayor índice en etiqueta: {" & maxPlaceholderIndex & "}, " &
                "parámetros enviados: " & args.Length & ". " &
                "Revise la configuración de la etiqueta.")
            End If
        End If

        Return String.Format(tmpZPLString, args)

    End Function

    Private Function CrearPalletPreImpresion(ByVal BeTransOcDetLote As clsBeTrans_oc_det,
                                             ByVal licenciaActual As String,
                                             ByVal cantidadPresentacion As Integer) As clsBeI_nav_barras_pallet

        Dim vFechaVence As Date = ObtenerFechaVence()
        Dim idOrdenCompraDetLote As Integer = ObtenerIdOrdenCompraDetLoteSeleccionado()

        '#EJC20260605_FIX_MHS_FK_LOTE_BARRA:
        'Persistir vínculo fuerte barra->lote para trazabilidad robusta (evita inferencia por texto de lote).
        Return New clsBeI_nav_barras_pallet With {
            .IdPallet = 0,
            .Codigo = BeTransOcDetLote.Codigo_Producto,
            .Nombre = BeTransOcDetLote.Nombre_producto,
            .Camas_Por_Tarima = pCamasPorTarima,
            .Cajas_Por_Cama = pCajasPorCama,
            .Cantidad_Presentacion = cantidadPresentacion,
            .UM_Producto = IIf(BeTransOcDetLote.Nombre_presentacion <> "", BeTransOcDetLote.Presentacion.Codigo, BeTransOcDetLote.Nombre_unidad_medida_basica),
            .Lote = cmbLote.Text,
            .Fecha_Agregado = Now,
            .Fecha_Ingreso = New Date(1900, 1, 1),
            .Fecha_Vence = vFechaVence,
            .Fecha_Produccion = New Date(1900, 1, 1),
            .Activo = 1,
            .Recibido = 0,
            .IdRecepcion = Nothing,
            .Bodega_Origen = BeBodega_Origen.Codigo,
            .Bodega_Destino = BeBodega_Origen.Codigo,
            .Codigo_barra = licenciaActual,
            .Cantidad_UMP = BeTransOcDetLote.Cantidad,
            .Lote_Numerico = Nothing,
            .IdOrdenCompraEnc = BeTransOcDetLote.IdOrdenCompraEnc,
            .IdOrdenCompraDet = BeTransOcDetLote.IdOrdenCompraDet,
            .IdOrdenCompraDetLote = idOrdenCompraDetLote,
            .Impreso = True
        }
    End Function

    Private Function ObtenerIdOrdenCompraDetLoteSeleccionado() As Integer
        Try
            If cmbLote Is Nothing OrElse cmbLote.EditValue Is Nothing Then Return 0
            If IsDBNull(cmbLote.EditValue) Then Return 0
            Return Convert.ToInt32(cmbLote.EditValue)
        Catch
            Return 0
        End Try
    End Function

    Private Sub ActualizarEstadoPantalla()
        RecalcularCapacidadLicenciaActual()

        Dim impresos As Integer = pCapacidadObjetivoLicenciaActual - pBultosPendientesLicenciaActual
        If impresos < 0 Then impresos = 0

        Select Case pModoProcesoActual
            Case TipoProcesoLicencia.SoloLicencia
                lblEtiquetas.Text = $"Licencias sugeridas: {CalcularEtiquetasDocumento()} | Para fardos cambie a 'Licencias y Presentación' y seleccione licencia madre."
            Case TipoProcesoLicencia.LicenciaBulto
                lblEtiquetas.Text = $"Licencia actual: impresos {impresos} / capacidad {pCapacidadObjetivoLicenciaActual} / pendientes {pBultosPendientesLicenciaActual}"
        End Select

        If pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto Then
            txtCantidadBarras.Value = Math.Max(0, pBultosPendientesLicenciaActual)
        Else
            txtCantidadBarras.Value = pCapacidadObjetivoLicenciaActual
        End If

        Dim cantidad As Decimal = 0D
        If pBeTransOcDet IsNot Nothing Then cantidad = CDec(pBeTransOcDet.Cantidad)
        If Not pBeProductoPresentacion Is Nothing Then
            txtCantUmBas.Value = pBeProductoPresentacion.Factor * cantidad
        Else
            txtCantUmBas.Value = cantidad
        End If

    End Sub

    Private Function CalcularEtiquetasDocumento() As Integer
        Dim camas As Integer = Convert.ToInt32(txtCamaPorTarima.Value)
        Dim cajas As Integer = Convert.ToInt32(txtCajaPorCama.Value)
        Dim cantidad As Decimal = 0D

        If pBeTransOcDet IsNot Nothing Then cantidad = CDec(pBeTransOcDet.Cantidad)
        Return CalcularEtiquetas(cantidad, camas, cajas)
    End Function

    Private Sub Cargar_productos_oc()
        Try
            Dim pListaProductos = clsLnTrans_oc_det.Get_Detalle_By_IdOrdenCompraEnc(pTransOC_Enc.IdOrdenCompraEnc)

            If pListaProductos IsNot Nothing AndAlso pListaProductos.Count > 0 Then
                With cmbProducto.Properties
                    .DataSource = pListaProductos
                    .ValueMember = "IdProductoBodega"
                    .DisplayMember = "Nombre_producto"
                    .Columns.Clear()
                    .Columns.Add(New Controls.LookUpColumnInfo("No_Linea", "No. Línea", 60))
                    .Columns.Add(New Controls.LookUpColumnInfo("IdProductoBodega", "IdProductoBodega") With {.Visible = False})
                    .Columns.Add(New Controls.LookUpColumnInfo("IdOrdenCompraDet", "IdOrdenCompraDet") With {.Visible = False})
                    .Columns.Add(New Controls.LookUpColumnInfo("IdPresentacion", "IdPresentacion") With {.Visible = False})
                    .Columns.Add(New Controls.LookUpColumnInfo("Codigo_Producto", "Codigo_Producto") With {.Visible = True})
                    .Columns.Add(New Controls.LookUpColumnInfo("Nombre_unidad_medida_basica", "Nombre_unidad_medida_basica") With {.Visible = False})
                    .Columns.Add(New Controls.LookUpColumnInfo("IdUnidadMedidaBasica", "IdUnidadMedidaBasica") With {.Visible = False})
                    .Columns.Add(New Controls.LookUpColumnInfo("Nombre_producto", "Producto", 220))
                    .Columns.Add(New Controls.LookUpColumnInfo("Cantidad", "Cantidad", 80))
                    .NullText = ""
                    .ShowHeader = True
                    .PopupWidth = 450
                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete
                    .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
                End With

                If pListaProductos.Count = 1 Then
                    cmbProducto.EditValue = pListaProductos(0).IdProductoBodega
                    cmbProducto.Properties.ForceInitialize()
                End If
            Else
                cmbProducto.Properties.DataSource = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdImpresionLicencia_Click(sender As Object, e As EventArgs) Handles cmdImpresionLicencia.Click

        cmdImpresionLicencia.Enabled = False

        Try

            If Not ValidarDatosBasicosProducto() Then Exit Sub
            If Not ValidarImpresora(cmbPrinterLicencia, Convert.ToString(cmbPrinterLicencia.EditValue), "Seleccione impresora") Then Exit Sub

            ' #EJC20260610_FLOW_IMP_OC_FASEADA:
            ' Si se está trabajando sobre licencia existente, el cierre/generación de licencia no aplica.
            If pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto AndAlso pModoLicenciaExistenteBultos Then
                XtraMessageBox.Show("Está trabajando sobre una licencia ya generada. Use Fardo/Unidad para imprimir presentaciones pendientes.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If pModoReimpresion Then
                ReimprimirLicencia(pBeTransOcDet, Convert.ToString(cmbPrinterLicencia.EditValue))
                pModoReimpresion = False
                cmdImpresionLicencia.BackColor = Color.Transparent
                ' #EJC20260610_FIX_IMP_OC_REIMPRESION_HABILITA_FARDO:
                ' Tras reimprimir licencia, habilitar flujo de hijas (fardos/unidad) automáticamente.
                If chkLicenciaBulto IsNot Nothing Then chkLicenciaBulto.Checked = True
                CargarEstadoLicenciaSeleccionadaParaBultos(Convert.ToString(txtLicencia.Text).Trim())
                ListarBarrasPallet(True)
                Exit Sub
            End If

            If DebeForzarCierreLicenciaAntesDeSeguir() Then
                CerrarEImprimirLicenciaConBultos(pBeTransOcDet, Convert.ToString(cmbPrinterLicencia.EditValue))
                ListarBarrasPallet(True)
                Exit Sub
            End If

            Select Case pModoProcesoActual
                Case TipoProcesoLicencia.SoloLicencia
                    If Not PuedeImprimirLicenciaSolo() Then Exit Sub
                    ImprimirLicencias_SoloLicencia(pBeTransOcDet,
                                               Convert.ToString(cmbPrinterLicencia.EditValue),
                                               Convert.ToInt32(txtCantidadLicencias.Value))

                Case TipoProcesoLicencia.LicenciaBulto
                    If Not PuedeCerrarEImprimirLicenciaConBultos() Then Exit Sub
                    CerrarEImprimirLicenciaConBultos(pBeTransOcDet,
                                                 Convert.ToString(cmbPrinterLicencia.EditValue))
            End Select

            ListarBarrasPallet(True)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            cmdImpresionLicencia.Enabled = True
        End Try

    End Sub

    Private Sub cmdImpresionBarra_Click(sender As Object, e As EventArgs) Handles cmdImpresionBarra.Click
        Try
            If pModoProcesoActual <> TipoProcesoLicencia.LicenciaBulto Then
                XtraMessageBox.Show("No se permite imprimir fardos en 'Solo Licencias'." &
                                    Environment.NewLine & Environment.NewLine &
                                    "Pasos:" &
                                    Environment.NewLine &
                                    "1. Cambie a 'Licencias y Presentación'." &
                                    Environment.NewLine &
                                    "2. Seleccione la licencia madre en la pestaña 'Licencias' (doble clic)." &
                                    Environment.NewLine &
                                    "3. Luego imprima Fardo/Unidad.",
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Not ValidarDatosBasicosProducto() Then Exit Sub
            If Not ValidarImpresora(cmbPrinterBarra, Convert.ToString(cmbPrinterBarra.EditValue), "Seleccione impresora") Then Exit Sub

            Dim cantidadSolicitada As Integer = Convert.ToInt32(txtCantidadBarras.Value)
            Dim licenciaMadre As String = Convert.ToString(txtLicencia.Text).Trim()

            If pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto Then
                ' #EJC20260604_FIX_IMP_OC_LIC_MADRE_FARDO: reimpresión de licencia no puede mezclarse con impresión de fardos.
                If pModoReimpresion AndAlso Not pModoLicenciaExistenteBultos Then
                    XtraMessageBox.Show("No puede imprimir fardos en modo reimpresión de licencia. Finalice la reimpresión o seleccione una licencia activa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                licenciaMadre = ObtenerLicenciaMadreActiva()
                Dim licenciaPantalla As String = Convert.ToString(txtLicencia.Text).Trim()

                ' #EJC20260604_FIX_IMP_OC_LIC_MADRE_FARDO: evita salto de licencia por cambios visuales/selección accidental.
                If Not String.IsNullOrWhiteSpace(licenciaMadre) AndAlso
                   Not String.IsNullOrWhiteSpace(licenciaPantalla) AndAlso
                   Not String.Equals(licenciaMadre, licenciaPantalla, StringComparison.OrdinalIgnoreCase) Then

                    txtLicencia.Text = licenciaMadre
                    XtraMessageBox.Show("Se detectó cambio de licencia durante el proceso de fardos. Se restauró la licencia madre para evitar mezcla de bultos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If Not ValidarLicenciaMadreImpresaParaFardos(licenciaMadre) Then Exit Sub
            End If

            If DebeForzarCierreLicenciaAntesDeSeguir() Then
                XtraMessageBox.Show("Ya completó la capacidad de esta licencia. Debe imprimir/cerrar la licencia antes de continuar con otra impresión de fardos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Not PuedeImprimirBultos(cantidadSolicitada) Then Exit Sub

            If XtraMessageBox.Show("Está por imprimir " & cantidadSolicitada & " etiquetas. ¿Continuar?",
                           Text,
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning) = DialogResult.No Then Exit Sub

            ' #EJC20260604_FIX_IMP_OC_LIC_MADRE_FARDO: imprime fardos con licencia explícita (madre), no dependiente del control UI.
            Imprimir_Producto(pBeTransOcDet,
                              Convert.ToString(cmbPrinterBarra.EditValue),
                              cantidadSolicitada,
                              licenciaMadre)

            PersistirContadorEtiquetasPresentacionLicenciaActual()
            ListarBarrasPallet(True)
            If pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto AndAlso Not String.IsNullOrWhiteSpace(licenciaMadre) Then
                CargarEstadoLicenciaSeleccionadaParaBultos(licenciaMadre)
            End If

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdUnidad_Click(sender As Object, e As EventArgs) Handles cmdUnidad.Click
        cmdImpresionBarra.PerformClick()
    End Sub

    Private Sub Imprimir_Producto(ByVal pReDet As clsBeTrans_oc_det,
                                  ByVal PrinterName As String,
                                  ByVal pImpresiones As Integer,
                                  Optional ByVal licenciaForzada As String = "")
        Try
            If pImpresiones <= 0 Then Exit Sub

            If pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto Then
                If Not ValidarLicenciaMadreImpresaParaFardos(licenciaForzada) Then Exit Sub
            End If

            ' #EJC20260604_FIX_IMP_OC_LIC_MADRE_FARDO: licencia forzada en ZPL para mantener trazabilidad madre->fardos.
            Dim zplString As String = ConstruirZplProducto(pReDet, licenciaForzada)
            Dim vCopias As Integer = ObtenerCopiasSolicitadas()

            For i As Integer = 1 To pImpresiones
                For c As Integer = 1 To vCopias
                    RawPrinterHelper.SendStringToPrinter(PrinterName, zplString)
                Next
            Next

            If pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto Then
                pBultosPendientesLicenciaActual -= pImpresiones
                If pBultosPendientesLicenciaActual < 0 Then pBultosPendientesLicenciaActual = 0
                pEtiquetasPresentacionImpresasLicenciaActual += (pImpresiones * vCopias)
            End If

            ActualizarEstadoPantalla()

            If DebeForzarCierreLicenciaAntesDeSeguir() Then
                XtraMessageBox.Show("Se completó la cantidad de bultos de la licencia actual. Ahora debe imprimir/cerrar la licencia antes de continuar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ImprimirLicencias_SoloLicencia(ByVal pBeTransOcDet As clsBeTrans_oc_det,
                                               ByVal PrinterName As String,
                                               ByVal pImpresiones As Integer)

        Dim clsTransaccion As New clsTransaccion

        Try
            If pImpresiones <= 0 Then Exit Sub

            Dim licenciasGeneradas As Integer = ObtenerLicenciasGeneradasLinea()

            Dim pendientes As Integer = pTotalTarimasProducto - licenciasGeneradas

            If pendientes < 0 Then pendientes = 0

            If pImpresiones > pendientes Then

                Dim result = XtraMessageBox.Show($"Está intentando imprimir más tarimas de las pendientes." &
                                                Environment.NewLine & Environment.NewLine &
                                                $"Total tarimas documento: {pTotalTarimasProducto}" &
                                                Environment.NewLine &
                                                $"Ya generadas: {licenciasGeneradas}" &
                                                Environment.NewLine &
                                                $"Pendientes: {pendientes}" &
                                                Environment.NewLine &
                                                $"Solicitadas: {pImpresiones}" &
                                                Environment.NewLine & Environment.NewLine &
                                                "¿Continuar?",
                                                Text,
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning)

                If result = DialogResult.No Then Exit Sub

            End If

            clsTransaccion.Begin_Transaction()

            If Not ValidarSobreImpresionTarimas(pImpresiones) Then
                clsTransaccion.RollBack_Transaction()
                Exit Sub
            End If

            pCajasPorCama = Convert.ToInt32(txtCajaPorCama.Value)
            pCamasPorTarima = Convert.ToInt32(txtCamaPorTarima.Value)
            pPresentacion = CStr(txtPresentacion.EditValue)

            Dim copiasPorLicencia As Integer = ObtenerCopiasSolicitadas()
            Dim licenciaActual As String = CStr(txtLicencia.EditValue)

            For i As Integer = 1 To pImpresiones

                Dim numeroTarima As Integer = pCorrelativoTarimaActual + 1
                Dim cantidadPresentacion As Integer = ObtenerCantidadPresentacionPorTarima(numeroTarima)

                If cantidadPresentacion <= 0 Then

                    Dim capacidadTarima As Integer = Convert.ToInt32(txtCamaPorTarima.Value) *
                                         Convert.ToInt32(txtCajaPorCama.Value)

                    If XtraMessageBox.Show("No se pudo calcular la cantidad real de presentación para la tarima." &
                                                                        Environment.NewLine &
                                                                        $"¿Imprimir con: {capacidadTarima}?",
                                                                        "Error",
                                                                        MessageBoxButtons.YesNo,
                                                                        MessageBoxIcon.Question) = DialogResult.No Then

                        Return

                    Else
                        cantidadPresentacion = capacidadTarima
                    End If

                End If


                If cantidadPresentacion <= 0 Then
                    Throw New Exception("No se pudo calcular la cantidad real de presentación para la tarima.")
                End If

                Dim zplString As String = ConstruirZplLicencia(pBeTransOcDet,
                                                               licenciaActual,
                                                               cantidadPresentacion,
                                                               clsTransaccion)

                For c As Integer = 1 To copiasPorLicencia
                    RawPrinterHelper.SendStringToPrinter(PrinterName, zplString)
                Next

                Dim BeInavBarraPalletTMP As clsBeI_nav_barras_pallet =
                CrearPalletPreImpresion(pBeTransOcDet,
                                         licenciaActual,
                                         cantidadPresentacion)

                BeInavBarraPalletTMP.Peso = txtPesoTarima.Value

                If clsLnI_nav_barras_pallet.Get_Single_By_Licencia(licenciaActual,
                                                                  clsTransaccion.lConnection,
                                                                  clsTransaccion.lTransaction) Then

                    ListarBarrasPallet(True)

                    Throw New Exception($"La licencia [{licenciaActual}] ya existe.")

                End If

                clsLnI_nav_barras_pallet.Guardar_Pallet_PreImpresion(BeInavBarraPalletTMP,
                                                                     clsTransaccion.lConnection,
                                                                     clsTransaccion.lTransaction)

                pCorrelativoTarimaActual += 1
                pTarimasImpresasAcumuladas += 1

                AvanzarALaSiguienteLicencia(clsTransaccion)

                licenciaActual = CStr(txtLicencia.EditValue)

            Next

            clsTransaccion.Commit_Transaction()

            ActualizarEstadoPantalla()
            ListarBarrasPallet(True)

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub CerrarEImprimirLicenciaConBultos(ByVal pReDet As clsBeTrans_oc_det,
                                                 ByVal PrinterName As String)

        Dim clsTransaccion As New clsTransaccion

        Try
            clsTransaccion.Begin_Transaction()

            If Not ValidarSobreImpresionTarimas(1) Then
                clsTransaccion.RollBack_Transaction()
                Exit Sub
            End If

            pCajasPorCama = Convert.ToInt32(txtCajaPorCama.Value)
            pCamasPorTarima = Convert.ToInt32(txtCamaPorTarima.Value)
            pPresentacion = CStr(txtPresentacion.EditValue)

            Dim cantidadPresentacion As Integer = pCamasPorTarima * pCajasPorCama
            Dim vPeso As Double = txtPesoTarima.Value
            Dim licenciaActual As String = CStr(txtLicencia.EditValue)
            Dim copiasPorLicencia As Integer = ObtenerCopiasSolicitadas()
            Dim zplString As String = ConstruirZplLicencia(pReDet,
                                                           licenciaActual,
                                                           cantidadPresentacion,
                                                           clsTransaccion)

            For c As Integer = 1 To copiasPorLicencia
                RawPrinterHelper.SendStringToPrinter(PrinterName, zplString)
            Next

            Dim BeInavBarraPallet As clsBeI_nav_barras_pallet = CrearPalletPreImpresion(pReDet,
                                                                                      licenciaActual,
                                                                                      cantidadPresentacion)

            BeInavBarraPallet.Peso = vPeso
            ' #EJC20260605_FIX_IMP_OC_CONTADOR_ETIQUETAS_PRESENTACION:
            'Persistir en la licencia cerrada cuántas etiquetas de presentación se imprimieron realmente.
            BeInavBarraPallet.Cant_Etiquetas_Presentacion_Impresas = pEtiquetasPresentacionImpresasLicenciaActual
            BeInavBarraPallet.Cantidad_Presentacion = pEtiquetasPresentacionImpresasLicenciaActual

            Dim factorPresentacion As Double = ObtenerFactorPresentacionActual()
            BeInavBarraPallet.Cantidad_UMP = pEtiquetasPresentacionImpresasLicenciaActual * factorPresentacion

            If clsLnI_nav_barras_pallet.Get_Single_By_Licencia(licenciaActual,
                                                                  clsTransaccion.lConnection,
                                                                  clsTransaccion.lTransaction) Then

                ListarBarrasPallet(True)
                Throw New Exception($"La licencia [{licenciaActual}] ya existe. (Verifique concurrencia)")

            End If

            clsLnI_nav_barras_pallet.Guardar_Pallet_PreImpresion(BeInavBarraPallet,
                                                                 clsTransaccion.lConnection,
                                                                 clsTransaccion.lTransaction)
            clsLnI_nav_barras_pallet.Actualizar_Cant_Etiquetas_Presentacion_Impresas(BeInavBarraPallet.IdOrdenCompraEnc,
                                                                                      BeInavBarraPallet.IdOrdenCompraDet,
                                                                                      BeInavBarraPallet.Codigo_barra,
                                                                                      BeInavBarraPallet.Cant_Etiquetas_Presentacion_Impresas,
                                                                                      clsTransaccion.lConnection,
                                                                                      clsTransaccion.lTransaction)

            pLicenciaActualCerrada = True
            AvanzarALaSiguienteLicencia(clsTransaccion)

            clsTransaccion.Commit_Transaction()

            ActualizarEstadoPantalla()

            pTarimasImpresasAcumuladas += 1

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw
        Finally
            clsTransaccion.Close_Conection()
        End Try
    End Sub

    Private Sub frmImpresionRecepcion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            cmdImpresionBarra.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.L Then
            cmdImpresionLicencia.PerformClick()
        End If
    End Sub

    Public Function Cargar_Impresoras_Windows(ByRef Cmb As LookUpEdit) As Boolean
        Cargar_Impresoras_Windows = False

        Try
            Dim i As Integer
            Dim iList As New ArrayList
            Dim pkInstalledPrinters As String

            For i = 0 To PrinterSettings.InstalledPrinters.Count - 1
                pkInstalledPrinters = PrinterSettings.InstalledPrinters.Item(i)
                iList.Add(pkInstalledPrinters)
            Next

            Cmb.Properties.DataSource = iList
            Return iList.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show("El servicio de impresión no está disponible o no se pudieron listar las impresoras disponibles.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try
    End Function

    '#EJC20260605_FIX_IMP_OC_DEFAULT_PRINTER:
    'Carga impresora inicial por prioridad: guardada -> Zebra -> predeterminada Windows -> primera disponible.
    Private Sub AsignarImpresoraInicial(ByRef cmb As LookUpEdit, ByVal impresoraGuardada As String)
        Try
            If cmb Is Nothing OrElse cmb.Properties Is Nothing OrElse cmb.Properties.DataSource Is Nothing Then Exit Sub

            Dim lista As New List(Of String)
            For Each item In CType(cmb.Properties.DataSource, System.Collections.IEnumerable)
                Dim nombre As String = Convert.ToString(item).Trim()
                If Not String.IsNullOrWhiteSpace(nombre) Then lista.Add(nombre)
            Next

            If lista.Count <= 0 Then Exit Sub

            Dim seleccion As String = ""

            If Not String.IsNullOrWhiteSpace(impresoraGuardada) Then
                For Each p As String In lista
                    If String.Equals(p.Trim(), impresoraGuardada.Trim(), StringComparison.OrdinalIgnoreCase) Then
                        seleccion = p
                        Exit For
                    End If
                Next
            End If

            If String.IsNullOrWhiteSpace(seleccion) Then
                For Each p As String In lista
                    If p.IndexOf("zebra", StringComparison.OrdinalIgnoreCase) >= 0 Then
                        seleccion = p
                        Exit For
                    End If
                Next
            End If

            If String.IsNullOrWhiteSpace(seleccion) Then
                Dim windowsDefault As String = ""
                Try
                    Dim ps As New PrinterSettings()
                    If ps IsNot Nothing AndAlso ps.IsValid Then
                        windowsDefault = Convert.ToString(ps.PrinterName).Trim()
                    End If
                Catch
                End Try

                If Not String.IsNullOrWhiteSpace(windowsDefault) Then
                    For Each p As String In lista
                        If String.Equals(p.Trim(), windowsDefault, StringComparison.OrdinalIgnoreCase) Then
                            seleccion = p
                            Exit For
                        End If
                    Next
                End If
            End If

            If String.IsNullOrWhiteSpace(seleccion) Then
                seleccion = lista(0)
            End If

            cmb.EditValue = seleccion

        Catch
        End Try
    End Sub

    Private Sub cmbPrinterBarra_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPrinterBarra.EditValueChanged
        pImpresoraProdSeleccionada = Convert.ToString(cmbPrinterBarra.EditValue)
        frmRecepcion.pImpresoraProdSeleccionada = pImpresoraProdSeleccionada
    End Sub

    Private Sub cmbPrinterLicencia_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPrinterLicencia.EditValueChanged
        pImpresoraLicSeleccionada = Convert.ToString(cmbPrinterLicencia.EditValue)
        frmRecepcion.pImpresoraLicSeleccionada = pImpresoraLicSeleccionada
    End Sub

    Private Sub cmbProducto_EditValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.EditValueChanged

        Try

            If cmbProducto.EditValue > 0 Then

                Dim fila As Object = cmbProducto.GetSelectedDataRow
                Dim pIdPresentacion As Integer

                If fila Is Nothing Then
                    Throw New Exception("Error_20220208_1204: el producto no es valido.")
                Else
                    pBeTransOcDet.IdProductoBodega = fila.IdProductoBodega
                    pBeTransOcDet.Codigo_Producto = fila.Codigo_Producto
                    pBeTransOcDet.Nombre_producto = fila.Nombre_producto
                    pBeTransOcDet.IdOrdenCompraDet = fila.IdOrdenCompraDet
                    pBeTransOcDet.Nombre_unidad_medida_basica = fila.Nombre_unidad_medida_basica
                    pBeTransOcDet.Cantidad = fila.Cantidad
                    pBeTransOcDet.No_Linea = fila.No_Linea
                    pIdPresentacion = fila.IdPresentacion
                    pBeTransOcDet = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(pTransOC_Enc.IdOrdenCompraEnc,
                                                                                                          pBeTransOcDet.IdOrdenCompraDet,
                                                                                                          pBeTransOcDet.IdProductoBodega,
                                                                                                          pBeTransOcDet.No_Linea)
                End If

                If pBeTransOcDet.IdOrdenCompraDet > 0 Then
                    Cargar_Presentacion(pIdPresentacion)
                    Cargar_oc_lotes(pTransOC_Enc.IdOrdenCompraEnc, pBeTransOcDet.IdOrdenCompraDet)
                End If

                Dim pBeProducto As clsBeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pBeTransOcDet.IdProductoBodega)
                Dim BeUmBas As clsBeUnidad_medida = clsLnUnidad_medida.Get_Unidad_Medida_By_IdUnidadMedida(pBeProducto.IdUnidadMedidaBasica)

                lblUmbasCant.Text = BeUmBas.Nombre

                lblPesoTarima.Visible = pBeProducto.Control_peso
                txtPesoTarima.Visible = pBeProducto.Control_peso
                txtPesoTotal.Visible = pBeProducto.Control_peso

                txtPesoTotal.Value = pBeTransOcDet.Peso

                Dim pListaLotes = clsLnTrans_oc_det_lote.Get_Lotes_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(pTransOC_Enc.IdOrdenCompraEnc, pBeTransOcDet.IdOrdenCompraDet)
                Dim pesoTotalLotes As Decimal = If(pListaLotes IsNot Nothing AndAlso pListaLotes.Rows.Count > 0, pListaLotes.AsEnumerable().Sum(Function(r) If(IsDBNull(r("peso_licencia")), 0D, Convert.ToDecimal(r("peso_licencia")))), 0D)

                ' Agrupar por tarima y sumar peso
                Dim pesoPorTarima =
                If(pListaLotes IsNot Nothing AndAlso pListaLotes.Rows.Count > 0,
                   pListaLotes.AsEnumerable().
                       GroupBy(Function(r) r.Field(Of Integer)("IdLote")).
                       Select(Function(g) New With {
                           Key .IdTarima = g.Key,
                           Key .PesoTotal = g.Sum(Function(r) If(IsDBNull(r("peso_licencia")), 0D, Convert.ToDecimal(r("peso_licencia"))))
                       }).ToList(),
                   New List(Of Object))

                MostrarCantidadEtiquetas()
                ReiniciarEstadoLicenciaActual()

                pTotalTarimasProducto = CalcularEtiquetasDocumento()
                pCorrelativoTarimaActual = ObtenerCorrelativoTarimaActual()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Function ObtenerCorrelativoTarimaActual() As Integer

        If pTransOC_Enc Is Nothing OrElse pBeTransOcDet Is Nothing Then Return 0

        If pTransOC_Enc.IdOrdenCompraEnc <= 0 OrElse pBeTransOcDet.IdOrdenCompraDet <= 0 Then
            Return 0
        End If

        Return clsLnTrans_oc_det_lote.Get_Correlativo_Inferido_Tarima_Actual(pTransOC_Enc.IdOrdenCompraEnc, pBeTransOcDet.IdOrdenCompraDet)

    End Function
    Private Sub Cargar_Presentacion(pIdPresentacion As Integer)
        Try
            pBeProductoPresentacion = clsLnProducto_presentacion.Get_Single_By_IdPresentacion(pIdPresentacion)
            If pBeProductoPresentacion Is Nothing Then Exit Sub

            Dim usaDetalle As Boolean =
                (pBeTransOcDet IsNot Nothing) AndAlso
                (pBeTransOcDet.Camas_Tarima > 0 OrElse pBeTransOcDet.Cajas_Cama > 0)

            If usaDetalle Then
                pBeProductoPresentacion.CamasPorTarima = pBeTransOcDet.Camas_Tarima
                pBeProductoPresentacion.CajasPorCama = pBeTransOcDet.Cajas_Cama
                txtCamaPorTarima.ReadOnly = False
                txtCajaPorCama.ReadOnly = False
            Else
                txtCamaPorTarima.ReadOnly = True
                txtCajaPorCama.ReadOnly = True
            End If

            txtPresentacion.Text = pBeProductoPresentacion.Nombre
            txtCamaPorTarima.Value = pBeProductoPresentacion.CamasPorTarima
            txtCajaPorCama.Value = pBeProductoPresentacion.CajasPorCama
            txtFactor.EditValue = pBeProductoPresentacion.Factor

            RecalcularCantidadEtiquetas()
            MostrarCantidadEtiquetas()
            lblProductoPres.Text = pBeProductoPresentacion.Nombre

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Cargar_oc_lotes(pIdOrdenCompraEnc As Integer, IdOrdenCompraDet As Integer)
        Try
            Dim pListaLotes = clsLnTrans_oc_det_lote.Get_Lotes_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(pIdOrdenCompraEnc, IdOrdenCompraDet)

            If pListaLotes IsNot Nothing AndAlso pListaLotes.Rows.Count > 0 Then
                With cmbLote.Properties
                    .DataSource = pListaLotes
                    .ForceInitialize()
                    .PopulateColumns()
                    .ValueMember = "IdLote"
                    .DisplayMember = "lote"

                    For Each c As Controls.LookUpColumnInfo In .Columns
                        c.Visible = False
                    Next

                    If .Columns("lote") IsNot Nothing Then
                        .Columns("lote").Visible = True
                        .Columns("lote").Caption = "Lote"
                        .Columns("lote").Width = 160
                    End If

                    If .Columns("fecha_vence") IsNot Nothing Then
                        .Columns("fecha_vence").Visible = True
                        .Columns("fecha_vence").Caption = "Vence"
                        .Columns("fecha_vence").Width = 110
                        .Columns("fecha_vence").FormatType = DevExpress.Utils.FormatType.DateTime
                        .Columns("fecha_vence").FormatString = "dd/MM/yyyy"
                    End If

                    If .Columns("cantidad") IsNot Nothing Then
                        .Columns("cantidad").Visible = True
                        .Columns("cantidad").Caption = "Cantidad"
                        .Columns("cantidad").Width = 90
                        .Columns("cantidad").FormatType = DevExpress.Utils.FormatType.Numeric
                        .Columns("cantidad").FormatString = "n2"
                    End If

                    .NullText = ""
                    .ShowHeader = True
                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete
                    .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor

                End With

                If pListaLotes.Rows.Count = 1 Then
                    cmbLote.EditValue = pListaLotes.Rows(0)("IdLote")
                    cmbLote.Properties.ForceInitialize()
                End If
            Else
                cmbLote.Properties.DataSource = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmbLote_EditValueChanged(sender As Object, e As EventArgs) Handles cmbLote.EditValueChanged
        Try
            If cmbLote.EditValue > 0 Then
                Dim fila As Object = cmbLote.GetSelectedDataRow
                If fila Is Nothing Then
                    Throw New Exception("Error_20220208_1204: el lote no es valido.")
                Else
                    Dim pFechaVence As String = CDate(fila.Item("fecha_vence")).ToString("dd/MM/yyyy")
                    Dim pPeso As Double = fila.Item("peso_licencia")
                    txtVencimiento.EditValue = pFechaVence
                    txtPesoTarima.Value = pPeso
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function CalcularEtiquetas(cantidad As Decimal, camas As Integer, cajas As Integer) As Integer
        Dim capacidad As Integer = camas * cajas
        If capacidad <= 0 OrElse cantidad <= 0D Then Return 0
        Return Math.Max(0, CInt(Math.Ceiling(cantidad / CDec(capacidad))))
    End Function

    Private Sub RecalcularCantidadEtiquetas()
        Dim etiquetas As Integer = CalcularEtiquetasDocumento()
        If etiquetas > 0 Then
            txtCantidadLicencias.Value = etiquetas
        Else
            txtCantidadLicencias.Value = 0
        End If
    End Sub

    Private Sub MostrarCantidadEtiquetas()
        RecalcularCantidadEtiquetas()
        ActualizarEstadoPantalla()
    End Sub

    Private Sub txtCamaPorTarima_ValueChanged(sender As Object, e As EventArgs) Handles txtCamaPorTarima.ValueChanged
        RecalcularCantidadEtiquetas()
        ReiniciarEstadoLicenciaActual()
    End Sub

    Private Sub txtCajaPorCama_ValueChanged(sender As Object, e As EventArgs) Handles txtCajaPorCama.ValueChanged
        RecalcularCantidadEtiquetas()
        ReiniciarEstadoLicenciaActual()
    End Sub

#Region "Control peso, ejc."

    Private Function ObtenerPesoTotalLinea() As Decimal
        If pBeTransOcDet Is Nothing Then Return 0D

        Return CDec(pBeTransOcDet.Peso_Neto)
    End Function

    Private Function ValidarPesoTarimaContraLinea() As Boolean
        If pBeTransOcDet Is Nothing OrElse pBeTransOcDet.IdProductoBodega <= 0 Then
            Return True
        End If

        Dim pesoTarima As Decimal = CDec(txtPesoTarima.Value)
        Dim pesoTotalLinea As Decimal = ObtenerPesoTotalLinea()

        Dim pesoYaImpreso As Decimal = ObtenerResumenPesoImpreso()
        Dim pesoProyectado As Decimal = pesoYaImpreso + pesoTarima

        ' Si ambos pesos son cero, no aplica validación.
        If pesoTarima <= 0D AndAlso pesoTotalLinea <= 0D Then
            Return True
        End If

        ' Si el lote tiene peso, pero la línea no tiene peso, es inconsistencia.
        If pesoTarima > 0D AndAlso pesoTotalLinea <= 0D Then
            XtraMessageBox.Show("El lote seleccionado tiene peso registrado, pero la línea del documento no tiene peso neto." &
                        Environment.NewLine &
                        "No se puede validar el peso de la tarima.",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation)
            Return False
        End If

        ' Si la línea tiene peso, pero el lote no tiene peso, es inconsistencia.
        If pesoTarima <= 0D AndAlso pesoTotalLinea > 0D Then
            XtraMessageBox.Show("La línea del documento tiene peso neto registrado, pero el lote seleccionado no tiene peso de tarima." &
                        Environment.NewLine &
                        "No se puede validar el peso de la tarima.",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation)
            Return False
        End If

        Dim cantidadTarimas As Integer = CalcularEtiquetasDocumento()

        If cantidadTarimas <= 0 Then
            XtraMessageBox.Show("No se pudo calcular la cantidad de tarimas para validar el peso.",
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
            Return False
        End If

        Dim pBeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pBeTransOcDet.IdProductoBodega)

        If pBeProducto Is Nothing Then
            XtraMessageBox.Show("No se pudo obtener la información del producto para validar la tolerancia de peso.",
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
            Return False
        End If

        Dim porcentajeTolerancia As Decimal = CDec(pBeProducto.Peso_tolerancia)

        If porcentajeTolerancia < 0D Then porcentajeTolerancia = 0D

        Dim pesoEstadisticoPorTarima As Decimal = pesoTotalLinea / CDec(cantidadTarimas)
        Dim margenTolerancia As Decimal = pesoEstadisticoPorTarima * (porcentajeTolerancia / 100D)

        Dim limiteGlobal As Decimal = pesoTotalLinea + (pesoTotalLinea * (porcentajeTolerancia / 100D))

        Dim pesoMinimoPermitido As Decimal = pesoEstadisticoPorTarima - margenTolerancia
        Dim pesoMaximoPermitido As Decimal = pesoEstadisticoPorTarima + margenTolerancia

        If pesoTarima < pesoMinimoPermitido OrElse pesoTarima > pesoMaximoPermitido Then
            XtraMessageBox.Show("El peso de la tarima está fuera del rango permitido." &
                            Environment.NewLine &
                            Environment.NewLine &
                            $"Peso ingresado: {pesoTarima:N3} lbs" &
                            Environment.NewLine &
                            $"Peso estadístico por tarima: {pesoEstadisticoPorTarima:N3} lbs" &
                            Environment.NewLine &
                            $"Tolerancia producto: {porcentajeTolerancia:N2}%" &
                            Environment.NewLine &
                            $"Rango permitido: {pesoMinimoPermitido:N3} lbs - {pesoMaximoPermitido:N3} lbs",
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
            Return False
        End If

        If pesoProyectado > limiteGlobal Then
            XtraMessageBox.Show("Se excede el peso total permitido considerando lo ya recibido." &
                    Environment.NewLine &
                    Environment.NewLine &
                    $"Peso línea: {pesoTotalLinea:N2} lbs" &
                    Environment.NewLine &
                    $"Ya recibido: {pesoYaImpreso:N2} lbs" &
                    Environment.NewLine &
                    $"Nueva tarima: {pesoTarima:N2} lbs" &
                    Environment.NewLine &
                    $"Total proyectado: {pesoProyectado:N2} lbs" &
                    Environment.NewLine &
                    $"Límite permitido: {limiteGlobal:N2} lbs",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)
            Return False
        End If

        Return True

    End Function


    Private Function ObtenerResumenPesoImpreso() As Decimal

        Try

            If pBeTransOcDet Is Nothing Then Return 0D

            Return clsLnTrans_oc_det.Get_Peso_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(pBeTransOcDet.IdOrdenCompraEnc, pBeTransOcDet.IdOrdenCompraDet)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

    Private Function ValidarSobreImpresionTarimas(cantidadAImprimir As Integer) As Boolean

        If pTotalTarimasProducto <= 0 Then Return True

        Dim totalDespues As Integer = pTarimasImpresasAcumuladas + cantidadAImprimir

        If totalDespues > pTotalTarimasProducto Then

            Dim result = XtraMessageBox.Show(
                $"Ya se alcanzó o se superará la cantidad de tarimas de la línea." &
                Environment.NewLine &
                $"Permitidas: {pTotalTarimasProducto}" &
                Environment.NewLine &
                $"Ya impresas: {pTarimasImpresasAcumuladas}" &
                Environment.NewLine &
                $"Intentando imprimir: {cantidadAImprimir}" &
                Environment.NewLine &
                $"Total resultante: {totalDespues}" &
                Environment.NewLine & Environment.NewLine &
                "¿Desea continuar?",
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )

            If result = DialogResult.No Then
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub dgridBarrasPallet_DoubleClick(sender As Object, e As EventArgs) Handles dgridBarrasPallet.DoubleClick

        Try

            If (gviewlicencias.RowCount > 0) Then

                Dim Dr As DataRowView = gviewlicencias.GetFocusedRow

                If Dr Is Nothing Then Exit Sub

                Dim IdPallet As Integer = Integer.Parse(Dr.Item("IdPallet"))

                Dim lSelectionIndex As Integer = gviewlicencias.FocusedRowHandle
                gviewlicencias.FocusedRowHandle = lSelectionIndex

                Dim pBeI_nav_barras_pallet = clsLnI_nav_barras_pallet.GetSingle(IdPallet)

                If pBeI_nav_barras_pallet IsNot Nothing Then

                    Dim vCodigoProducto As String = pBeI_nav_barras_pallet.Codigo
                    Dim BeProducto = clsLnProducto.Get_Single_By_Codigo(vCodigoProducto)

                    If BeProducto IsNot Nothing Then
                        Dim vIdProductoBodega As Integer = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BeProducto.IdProducto, pTransOC_Enc.IdBodega)
                        cmbProducto.EditValue = vIdProductoBodega
                        SeleccionarLotePorCodigo(pBeI_nav_barras_pallet.Lote)
                        txtVencimiento.EditValue = pBeI_nav_barras_pallet.Fecha_Vence
                        txtLicencia.Text = pBeI_nav_barras_pallet.Codigo_barra
                        lblIdPallet.Visible = True
                        txtIdPallet.Visible = True
                        txtIdPallet.Text = pBeI_nav_barras_pallet.IdPallet.ToString()
                        tabImp.SelectedTab = tabImpresion

                        ' #EJC20260610_FIX_IMP_OC_AUTO_MODO_BULTO:
                        ' Si el operador selecciona una licencia ya impresa desde la grilla,
                        ' habilitar automáticamente flujo Licencia+Bulto para imprimir hijas (fardos).
                        Dim licenciaImpresa As Boolean = False
                        Try
                            If Dr.Row.Table.Columns.Contains("Impreso") Then
                                licenciaImpresa = (ValorEntero(Dr.Item("Impreso")) > 0)
                            End If
                        Catch
                        End Try

                        If licenciaImpresa Then
                            If chkLicenciaBulto IsNot Nothing Then chkLicenciaBulto.Checked = True
                            pModoReimpresion = False
                            cmdImpresionLicencia.BackColor = Color.Transparent
                            CargarEstadoLicenciaSeleccionadaParaBultos(pBeI_nav_barras_pallet.Codigo_barra)
                        ElseIf pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto Then
                            pModoReimpresion = False
                            cmdImpresionLicencia.BackColor = Color.Transparent
                            CargarEstadoLicenciaSeleccionadaParaBultos(pBeI_nav_barras_pallet.Codigo_barra)
                        Else
                            pModoReimpresion = True
                            cmdImpresionLicencia.BackColor = Color.MistyRose
                        End If
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

    Private Sub SeleccionarLotePorCodigo(lote As String)

        Dim dtLotes As DataTable = TryCast(cmbLote.Properties.DataSource, DataTable)

        If dtLotes Is Nothing Then Exit Sub

        Dim drLote As DataRow = dtLotes.AsEnumerable().
            FirstOrDefault(Function(r) r.Field(Of String)("lote").Trim().ToUpper() =
                                       lote.Trim().ToUpper())

        If drLote IsNot Nothing Then
            cmbLote.EditValue = drLote.Field(Of Integer)("IdLote")
        End If

    End Sub

    Private Sub ReimprimirLicencia(ByVal pReDet As clsBeTrans_oc_det,
                                   ByVal PrinterName As String)

        Dim clsTransaccion As New clsTransaccion

        Try

            If pModoReimpresion Then
                If XtraMessageBox.Show("Está por reimprimir una licencia existente. ¿Continuar?",
                           Text,
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning) = DialogResult.No Then Exit Sub
            End If

            clsTransaccion.Begin_Transaction()

            pCajasPorCama = Convert.ToInt32(txtCajaPorCama.Value)
            pCamasPorTarima = Convert.ToInt32(txtCamaPorTarima.Value)
            pPresentacion = CStr(txtPresentacion.EditValue)

            Dim cantidadPresentacion As Integer = pCamasPorTarima * pCajasPorCama
            Dim licenciaActual As String = CStr(txtLicencia.EditValue)
            Dim copiasPorLicencia As Integer = ObtenerCopiasSolicitadas()

            Dim zplString As String = ConstruirZplLicencia(pReDet,
                                                           licenciaActual,
                                                           cantidadPresentacion,
                                                           clsTransaccion)

            For c As Integer = 1 To copiasPorLicencia
                RawPrinterHelper.SendStringToPrinter(PrinterName, zplString)
            Next

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Function ObtenerCantidadPresentacionPorTarima(ByVal numeroTarima As Integer) As Integer

        Dim cantidadTotal As Decimal = CDec(pBeTransOcDet.Cantidad)
        Dim capacidadTarima As Integer = Convert.ToInt32(txtCamaPorTarima.Value) *
                                         Convert.ToInt32(txtCajaPorCama.Value)

        If capacidadTarima <= 0 Then Return 0
        If cantidadTotal <= 0D Then Return 0

        Dim cantidadConsumida As Decimal = CDec(numeroTarima - 1) * CDec(capacidadTarima)
        Dim cantidadRestante As Decimal = cantidadTotal - cantidadConsumida

        If cantidadRestante <= 0D Then Return 0

        Return CInt(Math.Min(capacidadTarima, cantidadRestante))

    End Function

    Private Function ObtenerLicenciasGeneradasLinea() As Integer

        If pTransOC_Enc Is Nothing OrElse pBeTransOcDet Is Nothing Then Return 0

        If pTransOC_Enc.IdOrdenCompraEnc <= 0 OrElse pBeTransOcDet.IdOrdenCompraDet <= 0 Then
            Return 0
        End If

        Return clsLnI_nav_barras_pallet.Get_Count_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(pTransOC_Enc.IdOrdenCompraEnc,
                                                                                           pBeTransOcDet.IdOrdenCompraDet)

    End Function


    Private Function ObtenerFactorPresentacionActual() As Double
        Try
            If txtFactor.EditValue IsNot Nothing Then
                Return Convert.ToDouble(txtFactor.EditValue)
            End If
        Catch
        End Try
        Return 0
    End Function

    Private Sub ConfigurarGridLicencias()
        Try
            If gviewlicencias Is Nothing Then Exit Sub
            If gviewlicencias.Columns Is Nothing Then Exit Sub

            If gviewlicencias.Columns.ColumnByFieldName("Impreso") IsNot Nothing Then
                gviewlicencias.Columns("Impreso").Caption = "Licencia Impresa"
                gviewlicencias.Columns("Impreso").Width = 120
            End If

            If gviewlicencias.Columns.ColumnByFieldName("cant_etiquetas_presentacion_impresas") IsNot Nothing Then
                gviewlicencias.Columns("cant_etiquetas_presentacion_impresas").Caption = "Etiquetas Pres. Impresas"
                gviewlicencias.Columns("cant_etiquetas_presentacion_impresas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gviewlicencias.Columns("cant_etiquetas_presentacion_impresas").DisplayFormat.FormatString = "n0"
                gviewlicencias.Columns("cant_etiquetas_presentacion_impresas").Summary.Clear()
                gviewlicencias.Columns("cant_etiquetas_presentacion_impresas").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "cant_etiquetas_presentacion_impresas", "{0:n0}")
            End If
        Catch
        End Try
    End Sub

    Private Sub ActualizarPanelUltimaEtiqueta(ByVal dt As DataTable)
        Try
            txtUltimaLicencia.Text = ""
            TextEdit2.Text = "0"
            txtFechaUltimaImpresion.Text = ""

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then Exit Sub

            Dim dr As DataRow = dt.Rows(0)

            If dt.Columns.Contains("Licencia") Then
                txtUltimaLicencia.Text = Convert.ToString(dr("Licencia"))
            End If

            If dt.Columns.Contains("cant_etiquetas_presentacion_impresas") Then
                TextEdit2.Text = Convert.ToString(ValorEntero(dr("cant_etiquetas_presentacion_impresas")))
            ElseIf dt.Columns.Contains("Cantidad_Presentacion") Then
                TextEdit2.Text = Convert.ToString(ValorEntero(dr("Cantidad_Presentacion")))
            End If

            If dt.Columns.Contains("Fecha_Agregado") AndAlso Not IsDBNull(dr("Fecha_Agregado")) Then
                txtFechaUltimaImpresion.Text = CDate(dr("Fecha_Agregado")).ToString("dd/MM/yyyy HH:mm")
            ElseIf dt.Columns.Contains("Fecha_Vence") AndAlso Not IsDBNull(dr("Fecha_Vence")) Then
                txtFechaUltimaImpresion.Text = CDate(dr("Fecha_Vence")).ToString("dd/MM/yyyy")
            End If

        Catch
        End Try
    End Sub

    Private Function ValorEntero(ByVal value As Object) As Integer
        Try
            If IsDBNull(value) Then Return 0
            Return Convert.ToInt32(value)
        Catch
            Return 0
        End Try
    End Function

    Private Function ValorBooleano(ByVal value As Object) As Boolean
        Try
            If IsDBNull(value) Then Return False
            Return Convert.ToBoolean(value)
        Catch
            Return False
        End Try
    End Function

    Private Sub gviewlicencias_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gviewlicencias.RowStyle
        Try
            If e.RowHandle < 0 Then Exit Sub

            Dim impreso As Boolean = False
            Dim etiquetas As Integer = 0

            If gviewlicencias.Columns.ColumnByFieldName("Impreso") IsNot Nothing Then
                impreso = ValorBooleano(gviewlicencias.GetRowCellValue(e.RowHandle, "Impreso"))
            End If

            If gviewlicencias.Columns.ColumnByFieldName("cant_etiquetas_presentacion_impresas") IsNot Nothing Then
                etiquetas = ValorEntero(gviewlicencias.GetRowCellValue(e.RowHandle, "cant_etiquetas_presentacion_impresas"))
            ElseIf gviewlicencias.Columns.ColumnByFieldName("Cantidad_Presentacion") IsNot Nothing Then
                etiquetas = ValorEntero(gviewlicencias.GetRowCellValue(e.RowHandle, "Cantidad_Presentacion"))
            End If

            If impreso AndAlso etiquetas > 0 Then
                e.Appearance.BackColor = Color.Honeydew
                e.Appearance.ForeColor = Color.DarkGreen
            ElseIf impreso AndAlso etiquetas <= 0 Then
                e.Appearance.BackColor = Color.Moccasin
                e.Appearance.ForeColor = Color.DarkGoldenrod
            Else
                e.Appearance.BackColor = Color.MistyRose
                e.Appearance.ForeColor = Color.DarkRed
            End If
        Catch
        End Try
    End Sub

    Private Sub gviewlicencias_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles gviewlicencias.CustomColumnDisplayText
        Try
            If e.Column Is Nothing Then Exit Sub
            If Not String.Equals(e.Column.FieldName, "Impreso", StringComparison.OrdinalIgnoreCase) Then Exit Sub

            e.DisplayText = If(ValorBooleano(e.Value), "SI", "NO")
        Catch
        End Try
    End Sub
    Private Sub tabImpresion_Click(sender As Object, e As EventArgs) Handles tabImpresion.Click

    End Sub
End Class
