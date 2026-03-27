Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmImpresionRecepcion_OC

    Public pTransOC_Enc As New clsBeTrans_oc_enc
    Private pImpresoraProdSeleccionada As String = ""
    Private pImpresoraLicSeleccionada As String = ""
    Private EsPrimeraImpresion As Boolean = False

    Dim pTransOC_Det As New clsBeTrans_oc_det()
    Dim pCamasPorTarima As Integer
    Dim pCajasPorCama As Integer
    Dim pPresentacion As String
    Dim pBeBarra_Pallet As clsBeI_nav_barras_pallet
    Dim BeBodega_Origen As clsBeBodega
    Dim BeBodega_Destino As clsBeBodega

    Private Sub frmImpresionRecepcion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        BeBodega_Origen = New clsBeBodega()

        Try

            Dim vCantImpresion As Integer = 0
            vCantImpresion = txtCantidadBarras.Value
            txtLicencia.Enabled = False
            txtLicencia.Text = Genera_Licencia_BOF(AP.Bodega.IdBodega, AP.UsuarioAp.IdUsuario)

            Cargar_productos_oc()
            Cargar_Impresoras_Windows(cmbPrinterBarra)
            Cargar_Impresoras_Windows(cmbPrinterLicencia)

            BeBodega_Origen = clsLnBodega.GetSingle_By_Idbodega(pTransOC_Enc.IdBodega)

            EsPrimeraImpresion = True
            txtVencimiento.Enabled = False
            txtLicencia.Enabled = False
            txtPresentacion.Enabled = False
            txtFactor.Enabled = False

            cmbPrinterLicencia.EditValue = frmRecepcion.pImpresoraLicSeleccionada
            cmbPrinterBarra.EditValue = frmRecepcion.pImpresoraProdSeleccionada

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

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
                    ' opcional si querés forzar el refresco visual:
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

            Imprimir_Licencia(pTransOC_Det,
                              cmbPrinterLicencia.EditValue,
                              txtCantidadLicencias.Value)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Imprimir_Producto(ByVal pReDet As clsBeTrans_oc_det,
                                  ByVal PrinterName As String,
                                  ByVal pImpresiones As Integer)

        Try
            If String.IsNullOrWhiteSpace(PrinterName) Then
                DxErrorProvider1.SetError(cmbPrinterBarra, "Seleccione impresora")
                Exit Sub
            End If

            ' Copias solicitadas (cada impresión imprime 2 etiquetas iguales por ser 2-up)
            Dim vCopias As Integer
            Try
                vCopias = Convert.ToInt32(txtCopias.Value)
            Catch
                vCopias = pImpresiones
            End Try
            If vCopias <= 0 Then vCopias = 1

            ' Datos dinámicos
            Dim vEmpresa As String = AP.Empresa.Nombre
            Dim vLicencia As String = Convert.ToString(txtLicencia.EditValue)

            Dim vCodigoProducto As String = pReDet.Codigo_Producto
            Dim vNombreProducto As String =
            If(String.IsNullOrEmpty(pReDet.Nombre_producto),
               "",
               pReDet.Nombre_producto.Substring(0, Math.Min(pReDet.Nombre_producto.Length, 44))).Trim()

            Dim vLote As String = Convert.ToString(cmbLote.Text)

            ' Vence solo fecha
            Dim vVenceStr As String = ""
            If txtVencimiento.EditValue IsNot Nothing AndAlso
           Not String.IsNullOrWhiteSpace(Convert.ToString(txtVencimiento.EditValue)) Then

                Dim d As Date
                If Date.TryParse(Convert.ToString(txtVencimiento.EditValue), d) Then
                    vVenceStr = d.ToString("dd/MM/yy")
                End If
            End If

            ' Obtener ZPL desde BD (como original)
            Dim pBeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pReDet.IdProductoBodega)
            Dim pTipoEtiqueta = pBeProducto.IdTipoEtiqueta
            Dim pTipoSimbologia = pBeProducto.IdSimbologia
            Dim Tipo_Etiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(pTipoEtiqueta, pTipoSimbologia, 1)

            If Tipo_Etiqueta Is Nothing Then
                Throw New Exception("No se cargaron las propiedades de la etiqueta.")
            End If

            Dim tmpZPLString As String = Convert.ToString(Tipo_Etiqueta.codigo_zpl)
            If String.IsNullOrWhiteSpace(tmpZPLString) Then
                XtraMessageBox.Show($"{MethodBase.GetCurrentMethod.Name()} No está definido el formato de etiqueta",
                                Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' QR (placeholder {3}) - lo dejamos como código producto (ajústalo si quieres licencia)
            Dim vQR As String = vCodigoProducto

            ' Rellenar placeholders (pasa también {1} y {4} aunque no se usen, para evitar errores)
            Dim ZPLString As String = String.Format(tmpZPLString,
                                                   vNombreProducto, ' {0}
                                                   vEmpresa,       ' {1} (puede no usarse)
                                                   vCodigoProducto,      ' {2}
                                                   vQR,            ' {3}
                                                   "",             ' {4} (no se usa)
                                                   vLote,          ' {5}
                                                   vVenceStr)      ' {6}

            ' Imprimir N veces
            For i As Integer = 1 To vCopias
                RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmImpresionRecepcion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            Imprimir_Producto(pTransOC_Det, pImpresoraProdSeleccionada, txtCantidadBarras.Value)
        ElseIf e.Control AndAlso e.KeyCode = Keys.L Then
            Imprimir_Producto(pTransOC_Det, pImpresoraLicSeleccionada, txtCantidadLicencias.Value)
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

            If iList.Count > 0 Then
                'Buscar en la base de datos una impresora que coincida con el nombre y tipo para bof.
            End If

        Catch ex As Exception
            XtraMessageBox.Show("El servicio de impresión no está disponible o no se pudieron listar las impresoras disponibles.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmdImpresionBarra_Click(sender As Object, e As EventArgs) Handles cmdImpresionBarra.Click

        Try

            Imprimir_Producto(pTransOC_Det, cmbPrinterBarra.EditValue, txtCantidadBarras.Value)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbPrinterBarra_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPrinterBarra.EditValueChanged
        pImpresoraProdSeleccionada = cmbPrinterBarra.EditValue
        frmRecepcion.pImpresoraProdSeleccionada = pImpresoraProdSeleccionada
    End Sub

    Private Sub cmbPrinterLicencia_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPrinterLicencia.EditValueChanged
        pImpresoraProdSeleccionada = cmbPrinterLicencia.EditValue
        frmRecepcion.pImpresoraLicSeleccionada = pImpresoraProdSeleccionada
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
                    pTransOC_Det = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(pTransOC_Enc.IdOrdenCompraEnc, pTransOC_Det.IdOrdenCompraDet, pTransOC_Det.IdProductoBodega, pTransOC_Det.No_Linea)
                End If

                If pTransOC_Det.IdOrdenCompraDet > 0 Then
                    Cargar_Presentacion(pIdPresentacion)
                    Cargar_oc_lotes(pTransOC_Enc.IdOrdenCompraEnc, pTransOC_Det.IdOrdenCompraDet)
                End If

                Dim pBeProducto As clsBeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pTransOC_Det.IdProductoBodega)
                Dim BeUmBas As clsBeUnidad_medida = clsLnUnidad_medida.Get_Unidad_Medida_By_IdUnidadMedida(pBeProducto.IdUnidadMedidaBasica)

                lblUmbasCant.Text = BeUmBas.Nombre

                MostrarCantidadEtiquetas()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Dim pBeProductoPresentacion As New clsBeProducto_Presentacion

    Private Sub Cargar_Presentacion(pIdPresentacion As Integer)

        Try

            pBeProductoPresentacion = clsLnProducto_presentacion.Get_Single_By_IdPresentacion(pIdPresentacion)

            If pBeProductoPresentacion Is Nothing Then Exit Try

            Dim usaDetalle As Boolean =
            (pTransOC_Det IsNot Nothing) AndAlso
            (pTransOC_Det.Camas_Tarima > 0 OrElse pTransOC_Det.Cajas_Cama > 0)

            If usaDetalle Then
                ' Prioriza configuración del detalle y la “asocia” al objeto presentación
                pBeProductoPresentacion.CamasPorTarima = pTransOC_Det.Camas_Tarima
                pBeProductoPresentacion.CajasPorCama = pTransOC_Det.Cajas_Cama

                txtCamaPorTarima.ReadOnly = False
                txtCajaPorCama.ReadOnly = False
            Else
                txtCamaPorTarima.ReadOnly = True
                txtCajaPorCama.ReadOnly = True
            End If

            ' UI
            txtPresentacion.Text = pBeProductoPresentacion.Nombre
            txtCamaPorTarima.Value = pBeProductoPresentacion.CamasPorTarima
            txtCajaPorCama.Value = pBeProductoPresentacion.CajasPorCama
            txtFactor.EditValue = pBeProductoPresentacion.Factor

            ' >>> NUEVO: calcula cuántas etiquetas se necesitan según cantidad del detalle
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
                        .Columns("cantidad").FormatString = "n2" 'ajusta: n0 / n2 según aplique
                    End If

                    .NullText = ""
                    .ShowHeader = True
                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete
                    .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
                End With

                If pListaLotes IsNot Nothing AndAlso pListaLotes.Rows.Count = 1 Then
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

    Private Sub Imprimir_Licencia(ByVal pReDet As clsBeTrans_oc_det,
                                  ByVal PrinterName As String,
                                  ByVal pImpresiones As Integer)

        Dim clsTransaccion As New clsTransaccion
        pBeBarra_Pallet = New clsBeI_nav_barras_pallet()

        Try
            If String.IsNullOrWhiteSpace(PrinterName) Then
                DxErrorProvider1.SetError(cmbPrinterLicencia, "seleccione impresora")
                Exit Sub
            End If

            ' Cantidad de licencias a generar
            If pImpresiones <= 0 Then Exit Sub

            ' Cantidad de copias por licencia (NumericUpDown)
            Dim copiasPorLicencia As Integer = Convert.ToInt32(txtCopias.Value)
            If copiasPorLicencia <= 0 Then Exit Sub

            clsTransaccion.Begin_Transaction()

            Dim vEmpresa As String = AP.Empresa.Nombre
            Dim vCodigoProducto As String = pReDet.Codigo_Producto
            Dim vNombreProducto As String = pReDet.Nombre_producto.Substring(0, If(pReDet.Nombre_producto.Length < 45, pReDet.Nombre_producto.Length, 44))
            Dim vLote As String = cmbLote.Text
            Dim vFechaVence As Date = If(txtVencimiento.EditValue Is Nothing, New Date(1900, 1, 1), CDate(txtVencimiento.EditValue))

            pCajasPorCama = Convert.ToInt32(txtCajaPorCama.Value)
            pCamasPorTarima = Convert.ToInt32(txtCamaPorTarima.Value)
            pPresentacion = CStr(txtPresentacion.EditValue)

            Dim pCantidadPresentacion As Integer = pCamasPorTarima * pCajasPorCama
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

            ' Licencia actual (la que ya tenés visible)
            Dim licenciaActual As String = CStr(txtLicencia.EditValue)

            ' ====== LOOP: licencias (pImpresiones) ======
            For i As Integer = 1 To pImpresiones

                Dim ZPLString As String = String.Format(tmpZPLString,
                                                    AP.Bodega.Codigo & " - " & AP.Bodega.Nombre,
                                                    vEmpresa,
                                                    vCodigoProducto & " - " & vNombreProducto.Trim,
                                                    licenciaActual,
                                                    AP.UsuarioAp.Nombres & " " & AP.UsuarioAp.Apellidos & " / " & Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                    vLote,
                                                    vFechaVence.ToString("dd/MM/yy"),
                                                    pPresentacion,
                                                    pCantidadPresentacion)

                ' ====== LOOP: copias por licencia (txtcopias) ======
                For c As Integer = 1 To copiasPorLicencia
                    RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                Next

                ' Guarda pallet pre-impresión (solo 1 vez por licencia, NO por copia)
                Dim obj As New clsBeI_nav_barras_pallet With {
                .IdPallet = 0,
                .Codigo = pReDet.Codigo_Producto,
                .Nombre = pReDet.Nombre_producto,
                .Camas_Por_Tarima = pCamasPorTarima,
                .Cajas_Por_Cama = pCajasPorCama,
                .Cantidad_Presentacion = pCantidadPresentacion,
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

                clsLnI_nav_barras_pallet.Guardar_Pallet_PreImpresion(obj,
                                                                 clsTransaccion.lConnection,
                                                                 clsTransaccion.lTransaction)

                ' Incrementa correlativo y prepara la siguiente licencia
                Dim licenciaGenerada As String = Incrementar_Licencia_BOF_Info(AP.IdBodega,
                                                                           AP.UsuarioAp.IdUsuario,
                                                                           clsTransaccion.lConnection,
                                                                           clsTransaccion.lTransaction)

                licenciaActual = Genera_Licencia_BOF(AP.Bodega.IdBodega, AP.UsuarioAp.IdUsuario)
                txtLicencia.EditValue = licenciaActual

            Next

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Function CalcularEtiquetas(cantidad As Decimal, camas As Integer, cajas As Integer) As Integer
        Dim capacidad As Integer = camas * cajas
        If capacidad <= 0 OrElse cantidad <= 0D Then Return 0

        ' Ceil(cantidad / capacidad)
        Dim etiquetas As Integer = CInt(Math.Ceiling(cantidad / CDec(capacidad)))
        Return Math.Max(0, etiquetas)
    End Function

    Private Sub RecalcularCantidadEtiquetas()
        Dim camas As Integer = Convert.ToInt32(txtCamaPorTarima.Value)
        Dim cajas As Integer = Convert.ToInt32(txtCajaPorCama.Value)

        Dim cantidad As Decimal = 0D
        If pTransOC_Det IsNot Nothing Then cantidad = CDec(pTransOC_Det.Cantidad)

        Dim etiquetas As Integer = CalcularEtiquetas(cantidad, camas, cajas)

        If etiquetas > 0 Then
            txtCantidadLicencias.Value = etiquetas
        Else
            txtCantidadLicencias.Value = 0
        End If
    End Sub

    Private Sub MostrarCantidadEtiquetas()

        Dim camas As Integer = Convert.ToInt32(txtCamaPorTarima.Value)
        Dim cajas As Integer = Convert.ToInt32(txtCajaPorCama.Value)
        Dim capacidad As Integer = camas * cajas

        Dim cantidad As Decimal = 0D
        If pTransOC_Det IsNot Nothing Then cantidad = CDec(pTransOC_Det.Cantidad)

        Dim etiquetas As Integer = 0
        If capacidad > 0 AndAlso cantidad > 0D Then
            etiquetas = CInt(Math.Ceiling(cantidad / CDec(capacidad)))
        End If

        lblEtiquetas.Text = $"Etiquetas a imprimir: {etiquetas}"
        txtCantidadLicencias.Value = etiquetas
        txtCantidadBarras.Value = txtCamaPorTarima.Value * txtCajaPorCama.Value

        txtCantUmBas.Value = pBeProductoPresentacion.Factor * cantidad

    End Sub

End Class