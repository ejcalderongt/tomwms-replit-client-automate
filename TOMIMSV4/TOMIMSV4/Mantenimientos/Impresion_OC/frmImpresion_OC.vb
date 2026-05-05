Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmImpresionRecepcion_OC

    Private Enum TipoProcesoLicencia
        SoloLicencia = 1
        LicenciaBulto = 2
    End Enum

    Public pTransOC_Enc As New clsBeTrans_oc_enc

    Private pImpresoraProdSeleccionada As String = ""
    Private pImpresoraLicSeleccionada As String = ""
    Private EsPrimeraImpresion As Boolean = False

    Private pTransOC_Det As New clsBeTrans_oc_det()
    Private pCamasPorTarima As Integer
    Private pCajasPorCama As Integer
    Private pPresentacion As String
    Private pBeBarra_Pallet As clsBeI_nav_barras_pallet
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

    Private Sub frmImpresionRecepcion_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        BeBodega_Origen = New clsBeBodega()

        Try
            txtLicencia.Enabled = False
            txtVencimiento.Enabled = False
            txtPresentacion.Enabled = False
            txtFactor.Enabled = False

            Cargar_productos_oc()
            Cargar_Impresoras_Windows(cmbPrinterBarra)
            Cargar_Impresoras_Windows(cmbPrinterLicencia)

            BeBodega_Origen = clsLnBodega.GetSingle_By_Idbodega(pTransOC_Enc.IdBodega)

            EsPrimeraImpresion = True

            cmbPrinterLicencia.EditValue = frmRecepcion.pImpresoraLicSeleccionada
            cmbPrinterBarra.EditValue = frmRecepcion.pImpresoraProdSeleccionada

            If chkSoloLicencia IsNot Nothing Then chkSoloLicencia.Checked = True

            ReservarNuevaLicencia()
            AplicarModoProceso()
            ActualizarEstadoPantalla()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            Case TipoProcesoLicencia.LicenciaBulto
                txtCantidadLicencias.Value = 1
                txtCantidadLicencias.Enabled = False
                txtCantidadBarras.Enabled = True
                txtCopias.Enabled = True
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
        ActualizarEstadoPantalla()
    End Sub

    Private Sub RecalcularCapacidadLicenciaActual()
        pCamasPorTarima = Convert.ToInt32(txtCamaPorTarima.Value)
        pCajasPorCama = Convert.ToInt32(txtCajaPorCama.Value)
        pPresentacion = CStr(txtPresentacion.EditValue)
        pCapacidadObjetivoLicenciaActual = Math.Max(0, pCamasPorTarima * pCajasPorCama)
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

        If pTransOC_Det Is Nothing OrElse pTransOC_Det.IdProductoBodega <= 0 Then
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

        Return True
    End Function

    Private Function ObtenerNombreProductoCorto() As String
        If pTransOC_Det Is Nothing OrElse String.IsNullOrWhiteSpace(pTransOC_Det.Nombre_producto) Then Return ""
        Return pTransOC_Det.Nombre_producto.Substring(0, Math.Min(pTransOC_Det.Nombre_producto.Length, 44)).Trim()
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

    Private Function ConstruirZplProducto(ByVal pReDet As clsBeTrans_oc_det) As String
        Dim vFechaVence As Date = ObtenerFechaVence()
        Dim vEmpresa As String = AP.Empresa.Nombre
        Dim vCodigoProducto As String = pReDet.Codigo_Producto
        Dim vNombreProducto As String = ObtenerNombreProductoCorto()
        Dim vLote As String = Convert.ToString(cmbLote.Text)

        Dim pBeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pReDet.IdProductoBodega)
        Dim pTipoEtiqueta = pBeProducto.IdTipoEtiqueta
        Dim pTipoSimbologia = pBeProducto.IdSimbologia
        Dim Tipo_Etiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(pTipoEtiqueta, pTipoSimbologia, 1)

        If Tipo_Etiqueta Is Nothing Then
            Throw New Exception("No se cargaron las propiedades de la etiqueta.")
        End If

        Dim tmpZPLString As String = Convert.ToString(Tipo_Etiqueta.codigo_zpl)
        If String.IsNullOrWhiteSpace(tmpZPLString) Then
            Throw New Exception($"{MethodBase.GetCurrentMethod.Name()} No está definido el formato de etiqueta")
        End If

        Return String.Format(tmpZPLString,
                             AP.Bodega.Codigo & " - " & AP.Bodega.Nombre,
                             vEmpresa,
                             vCodigoProducto & " - " & vNombreProducto,
                             txtLicencia.Text,
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
        Dim vNombreProducto As String = If(String.IsNullOrWhiteSpace(pReDet.Nombre_producto), "", pReDet.Nombre_producto.Substring(0, Math.Min(pReDet.Nombre_producto.Length, 44)))
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

        Return String.Format(tmpZPLString,
                             AP.Bodega.Codigo & " - " & AP.Bodega.Nombre,
                             vEmpresa,
                             vCodigoProducto & " - " & vNombreProducto.Trim(),
                             licenciaActual,
                             AP.UsuarioAp.Nombres & " " & AP.UsuarioAp.Apellidos & " / " & Now.ToString("yyyy-MM-dd HH:mm:ss"),
                             vLote,
                             vFechaVence.ToString("dd/MM/yy"),
                             pPresentacion,
                             cantidadPresentacion)
    End Function

    Private Function CrearPalletPreImpresion(ByVal pReDet As clsBeTrans_oc_det,
                                             ByVal licenciaActual As String,
                                             ByVal cantidadPresentacion As Integer) As clsBeI_nav_barras_pallet

        Dim vFechaVence As Date = ObtenerFechaVence()

        Return New clsBeI_nav_barras_pallet With {
            .IdPallet = 0,
            .Codigo = pReDet.Codigo_Producto,
            .Nombre = pReDet.Nombre_producto,
            .Camas_Por_Tarima = pCamasPorTarima,
            .Cajas_Por_Cama = pCajasPorCama,
            .Cantidad_Presentacion = cantidadPresentacion,
            .UM_Producto = pReDet.Nombre_unidad_medida_basica,
            .Lote = cmbLote.Text,
            .Fecha_Agregado = Now,
            .Fecha_Ingreso = New Date(1900, 1, 1),
            .Fecha_Vence = vFechaVence,
            .Fecha_Produccion = New Date(1900, 1, 1),
            .Activo = 1,
            .Recibido = 1,
            .IdRecepcion = Nothing,
            .Bodega_Origen = BeBodega_Origen.Codigo,
            .Bodega_Destino = BeBodega_Origen.Codigo,
            .Codigo_barra = licenciaActual,
            .Cantidad_UMP = Nothing,
            .Lote_Numerico = Nothing
        }
    End Function

    Private Sub ActualizarEstadoPantalla()
        RecalcularCapacidadLicenciaActual()

        Dim impresos As Integer = pCapacidadObjetivoLicenciaActual - pBultosPendientesLicenciaActual
        If impresos < 0 Then impresos = 0

        Select Case pModoProcesoActual
            Case TipoProcesoLicencia.SoloLicencia
                lblEtiquetas.Text = $"Licencias sugeridas: {CalcularEtiquetasDocumento()}"

            Case TipoProcesoLicencia.LicenciaBulto
                lblEtiquetas.Text = $"Licencia actual: impresos {impresos} / capacidad {pCapacidadObjetivoLicenciaActual} / pendientes {pBultosPendientesLicenciaActual}"
        End Select

        txtCantidadBarras.Value = pCapacidadObjetivoLicenciaActual

        Dim cantidad As Decimal = 0D
        If pTransOC_Det IsNot Nothing Then cantidad = CDec(pTransOC_Det.Cantidad)
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

        If pTransOC_Det IsNot Nothing Then cantidad = CDec(pTransOC_Det.Cantidad)
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
        Try
            If Not ValidarDatosBasicosProducto() Then Exit Sub
            If Not ValidarImpresora(cmbPrinterLicencia, Convert.ToString(cmbPrinterLicencia.EditValue), "Seleccione impresora") Then Exit Sub

            If DebeForzarCierreLicenciaAntesDeSeguir() Then
                CerrarEImprimirLicenciaConBultos(pTransOC_Det, Convert.ToString(cmbPrinterLicencia.EditValue))
                Exit Sub
            End If

            Select Case pModoProcesoActual
                Case TipoProcesoLicencia.SoloLicencia
                    If Not PuedeImprimirLicenciaSolo() Then Exit Sub
                    ImprimirLicencias_SoloLicencia(pTransOC_Det,
                                                   Convert.ToString(cmbPrinterLicencia.EditValue),
                                                   Convert.ToInt32(txtCantidadLicencias.Value))

                Case TipoProcesoLicencia.LicenciaBulto
                    If Not PuedeCerrarEImprimirLicenciaConBultos() Then Exit Sub
                    CerrarEImprimirLicenciaConBultos(pTransOC_Det,
                                                     Convert.ToString(cmbPrinterLicencia.EditValue))
            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdImpresionBarra_Click(sender As Object, e As EventArgs) Handles cmdImpresionBarra.Click
        Try
            If Not ValidarDatosBasicosProducto() Then Exit Sub
            If Not ValidarImpresora(cmbPrinterBarra, Convert.ToString(cmbPrinterBarra.EditValue), "Seleccione impresora") Then Exit Sub

            Dim cantidadSolicitada As Integer = Convert.ToInt32(txtCantidadBarras.Value)

            If DebeForzarCierreLicenciaAntesDeSeguir() Then
                XtraMessageBox.Show("Ya completó la capacidad de esta licencia. Debe imprimir/cerrar la licencia antes de continuar con otra impresión de fardos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Not PuedeImprimirBultos(cantidadSolicitada) Then Exit Sub

            Imprimir_Producto(pTransOC_Det,
                              Convert.ToString(cmbPrinterBarra.EditValue),
                              cantidadSolicitada)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdUnidad_Click(sender As Object, e As EventArgs) Handles cmdUnidad.Click
        cmdImpresionBarra.PerformClick()
    End Sub

    Private Sub Imprimir_Producto(ByVal pReDet As clsBeTrans_oc_det,
                                  ByVal PrinterName As String,
                                  ByVal pImpresiones As Integer)
        Try
            If pImpresiones <= 0 Then Exit Sub

            Dim zplString As String = ConstruirZplProducto(pReDet)
            Dim vCopias As Integer = ObtenerCopiasSolicitadas()

            For i As Integer = 1 To pImpresiones
                For c As Integer = 1 To vCopias
                    RawPrinterHelper.SendStringToPrinter(PrinterName, zplString)
                Next
            Next

            If pModoProcesoActual = TipoProcesoLicencia.LicenciaBulto Then
                pBultosPendientesLicenciaActual -= pImpresiones
                If pBultosPendientesLicenciaActual < 0 Then pBultosPendientesLicenciaActual = 0
            End If

            ActualizarEstadoPantalla()

            If DebeForzarCierreLicenciaAntesDeSeguir() Then
                XtraMessageBox.Show("Se completó la cantidad de bultos de la licencia actual. Ahora debe imprimir/cerrar la licencia antes de continuar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ImprimirLicencias_SoloLicencia(ByVal pReDet As clsBeTrans_oc_det,
                                               ByVal PrinterName As String,
                                               ByVal pImpresiones As Integer)

        Dim clsTransaccion As New clsTransaccion

        Try
            If pImpresiones <= 0 Then Exit Sub

            clsTransaccion.Begin_Transaction()

            pCajasPorCama = Convert.ToInt32(txtCajaPorCama.Value)
            pCamasPorTarima = Convert.ToInt32(txtCamaPorTarima.Value)
            pPresentacion = CStr(txtPresentacion.EditValue)

            Dim pCantidadPresentacion As Integer = pCamasPorTarima * pCajasPorCama
            Dim copiasPorLicencia As Integer = ObtenerCopiasSolicitadas()
            Dim licenciaActual As String = CStr(txtLicencia.EditValue)

            For i As Integer = 1 To pImpresiones
                Dim zplString As String = ConstruirZplLicencia(pReDet,
                                                               licenciaActual,
                                                               pCantidadPresentacion,
                                                               clsTransaccion)

                For c As Integer = 1 To copiasPorLicencia
                    RawPrinterHelper.SendStringToPrinter(PrinterName, zplString)
                Next

                Dim obj As clsBeI_nav_barras_pallet = CrearPalletPreImpresion(pReDet,
                                                                              licenciaActual,
                                                                              pCantidadPresentacion)

                clsLnI_nav_barras_pallet.Guardar_Pallet_PreImpresion(obj,
                                                                     clsTransaccion.lConnection,
                                                                     clsTransaccion.lTransaction)

                AvanzarALaSiguienteLicencia(clsTransaccion)
                licenciaActual = CStr(txtLicencia.EditValue)
            Next

            clsTransaccion.Commit_Transaction()
            ActualizarEstadoPantalla()

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

            Dim obj As clsBeI_nav_barras_pallet = CrearPalletPreImpresion(pReDet,
                                                                          licenciaActual,
                                                                          cantidadPresentacion)

            clsLnI_nav_barras_pallet.Guardar_Pallet_PreImpresion(obj,
                                                                 clsTransaccion.lConnection,
                                                                 clsTransaccion.lTransaction)

            pLicenciaActualCerrada = True
            AvanzarALaSiguienteLicencia(clsTransaccion)

            clsTransaccion.Commit_Transaction()
            ActualizarEstadoPantalla()

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
                    pTransOC_Det.IdProductoBodega = fila.IdProductoBodega
                    pTransOC_Det.Codigo_Producto = fila.Codigo_Producto
                    pTransOC_Det.Nombre_producto = fila.Nombre_producto
                    pTransOC_Det.IdOrdenCompraDet = fila.IdOrdenCompraDet
                    pTransOC_Det.Nombre_unidad_medida_basica = fila.Nombre_unidad_medida_basica
                    pTransOC_Det.Cantidad = fila.Cantidad
                    pTransOC_Det.No_Linea = fila.No_Linea
                    pIdPresentacion = fila.IdPresentacion
                    pTransOC_Det = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(pTransOC_Enc.IdOrdenCompraEnc,
                                                                                                           pTransOC_Det.IdOrdenCompraDet,
                                                                                                           pTransOC_Det.IdProductoBodega,
                                                                                                           pTransOC_Det.No_Linea)
                End If

                If pTransOC_Det.IdOrdenCompraDet > 0 Then
                    Cargar_Presentacion(pIdPresentacion)
                    Cargar_oc_lotes(pTransOC_Enc.IdOrdenCompraEnc, pTransOC_Det.IdOrdenCompraDet)
                End If

                Dim pBeProducto As clsBeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pTransOC_Det.IdProductoBodega)
                Dim BeUmBas As clsBeUnidad_medida = clsLnUnidad_medida.Get_Unidad_Medida_By_IdUnidadMedida(pBeProducto.IdUnidadMedidaBasica)

                lblUmbasCant.Text = BeUmBas.Nombre
                MostrarCantidadEtiquetas()
                ReiniciarEstadoLicenciaActual()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Cargar_Presentacion(pIdPresentacion As Integer)
        Try
            pBeProductoPresentacion = clsLnProducto_presentacion.Get_Single_By_IdPresentacion(pIdPresentacion)
            If pBeProductoPresentacion Is Nothing Then Exit Sub

            Dim usaDetalle As Boolean =
                (pTransOC_Det IsNot Nothing) AndAlso
                (pTransOC_Det.Camas_Tarima > 0 OrElse pTransOC_Det.Cajas_Cama > 0)

            If usaDetalle Then
                pBeProductoPresentacion.CamasPorTarima = pTransOC_Det.Camas_Tarima
                pBeProductoPresentacion.CajasPorCama = pTransOC_Det.Cajas_Cama
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

                    For Each c As DevExpress.XtraEditors.Controls.LookUpColumnInfo In .Columns
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
                    .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
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
                    txtVencimiento.EditValue = pFechaVence
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

End Class
