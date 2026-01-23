Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.DashboardWin.Commands
Imports DevExpress.XtraEditors
Imports TOMWMS.wsTOMHH

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

            cmbPrinterLicencia.EditValue = frmRecepcion.pImpresoraLicSeleccionada
            cmbPrinterBarra.EditValue = frmRecepcion.pImpresoraProdSeleccionada

            BeBodega_Origen = clsLnBodega.GetSingle_By_Idbodega(pTransOC_Enc.IdBodega)

            EsPrimeraImpresion = True
            txtVencimiento.Enabled = False
            txtLicencia.Enabled = False
            txtPresentacion.Enabled = False
            txtFactor.Enabled = False

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
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("No_Linea", "No. Línea", 60))
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdProductoBodega", "IdProductoBodega") With {.Visible = False})
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdOrdenCompraDet", "IdOrdenCompraDet") With {.Visible = False})
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdPresentacion", "IdPresentacion") With {.Visible = False})
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Codigo_Producto", "Codigo_Producto") With {.Visible = True})
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nombre_unidad_medida_basica", "Nombre_unidad_medida_basica") With {.Visible = False})
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdUnidadMedidaBasica", "IdUnidadMedidaBasica") With {.Visible = False})
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nombre_producto", "Producto", 220))
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Cantidad", "Cantidad", 80))
                    .NullText = ""
                    .ShowHeader = True
                    .PopupWidth = 450
                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete
                    .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
                End With

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

            '#GT15022024: valores a cargar en la etiqueta ZPL
            Dim ZPLString As String = ""
            Dim vEmpresa As String = AP.Empresa.Nombre
            Dim vCodigoBarra As String = txtLicencia.EditValue
            Dim vCodigoProducto As String = pReDet.Codigo_Producto
            Dim vNombreProducto As String = pReDet.Nombre_producto.Substring(0, IIf(pReDet.Nombre_producto.Length < 45, pReDet.Nombre_producto.Length, 44)) '"PRAZOLEN 20MG CAJA X 15 CAPSULAS MUY LARGO PARA"
            Dim vLote As String = cmbLote.EditValue
            Dim vFechaVence As String = txtVencimiento.EditValue

            '#GT14022024: validamos producto y tipo etiqueta (zpl, simbologia)
            Dim pBeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pReDet.IdProductoBodega)
            Dim pTipoEtiqueta = pBeProducto.IdTipoEtiqueta
            Dim pTipoSimbologia = pBeProducto.IdSimbologia
            Dim Tipo_Etiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(pTipoEtiqueta, pTipoSimbologia, 1)

            '#GT15022024: validamos cuantas impresiones deben realizarse, considera 3 x fila (son 3 columnas)
            'Dim vColaImpresiones As Double = Math.Truncate(pImpresiones / 3)
            'im vColaFraccion As Double = pImpresiones - (vColaImpresiones * 3)

            If PrinterName <> "" Then

                If Tipo_Etiqueta IsNot Nothing Then

                    Dim tmpZPLString = Tipo_Etiqueta.codigo_zpl

                    If tmpZPLString <> "" Then
                        ZPLString = String.Format(tmpZPLString,
                                                  BeBodega_Origen.Nombre,
                                                  vEmpresa,
                                                  vNombreProducto.Trim + " " + vCodigoProducto,
                                                  vCodigoProducto,
                                                  vFechaVence)
                    End If

                    If ZPLString <> "" Then
                        Dim vColaImpresiones = pImpresiones
                        If vColaImpresiones > 0 Then
                            If vColaImpresiones = 1 Then
                                RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                            Else
                                For i = 1 To vColaImpresiones
                                    RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                                Next
                            End If
                        End If

                    Else
                        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "No está definido el formato de etiqueta"),
                                            Text,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                    End If
                Else
                    Throw New Exception("GT14022024: No se cargaron las propiedades de la etiqueta.")
                End If

            Else
                DxErrorProvider1.SetError(cmbPrinterBarra, "Seleccione impresora")
            End If

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
                    pIdPresentacion = fila.IdPresentacion
                End If

                If pTransOC_Det.IdOrdenCompraDet > 0 Then
                    Cargar_Presentacion(pIdPresentacion)
                    Cargar_oc_lotes(pTransOC_Enc.IdOrdenCompraEnc, pTransOC_Det.IdOrdenCompraDet)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Dim pBeProductoPresentacion As New clsBeProducto_Presentacion
    Private Sub Cargar_Presentacion(pIdPresentacion As Integer)
        Try
            pBeProductoPresentacion = clsLnProducto_presentacion.Get_Single_By_IdPresentacion(pIdPresentacion)

            If pBeProductoPresentacion IsNot Nothing Then

                txtPresentacion.Text = pBeProductoPresentacion.Nombre
                txtCamaPorTarima.Value = pBeProductoPresentacion.CamasPorTarima
                txtCajaPorCama.Value = pBeProductoPresentacion.CajasPorCama
                txtFactor.EditValue = pBeProductoPresentacion.Factor

            End If

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
            clsTransaccion.Begin_Transaction()

            '#GT15022024: valores a cargar en la etiqueta ZPL
            Dim ZPLString As String = ""
            Dim vEmpresa As String = AP.Empresa.Nombre
            Dim vCodigoBarra As String = txtLicencia.EditValue
            Dim vCodigoProducto As String = pReDet.Codigo_Producto
            Dim vNombreProducto As String = pReDet.Nombre_producto.Substring(0, IIf(pReDet.Nombre_producto.Length < 45, pReDet.Nombre_producto.Length, 44))
            Dim vLote As String = cmbLote.EditValue
            Dim vFechaVence As String = txtVencimiento.EditValue
            pCajasPorCama = txtCajaPorCama.Value
            pCamasPorTarima = txtCamaPorTarima.Value
            pPresentacion = txtPresentacion.EditValue
            Dim pCantidad = pCamasPorTarima * pCajasPorCama

            Dim pTipoEtiqueta As Integer = AP.Bodega.IdTipoEtiquetaLicencia
            Dim pTipoSimbologia As Integer = AP.Bodega.IdSimbologiaLicencia
            Dim pClasificacion As Integer = 2

            Dim Tipo_Etiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(pTipoEtiqueta, pTipoSimbologia, pClasificacion,
                                                                            clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If PrinterName <> "" Then

                If Tipo_Etiqueta IsNot Nothing Then

                    Dim tmpZPLString = Tipo_Etiqueta.codigo_zpl

                    If tmpZPLString <> "" Then
                        ZPLString = String.Format(tmpZPLString, AP.Bodega.Codigo + " - " + AP.Bodega.Nombre,
                                              vEmpresa,
                                              vCodigoProducto + " - " + vNombreProducto.Trim,
                                              vCodigoBarra,
                                              AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos + " / " + Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                              vLote,
                                              vFechaVence,
                                              pPresentacion,
                                              pCantidad)
                    End If

                    If ZPLString <> "" Then
                        Dim vColaImpresiones = pImpresiones
                        If vColaImpresiones = 1 Then
                            RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                        ElseIf vColaImpresiones > 1 Then
                            For i = 1 To vColaImpresiones
                                RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                            Next
                        End If
                    Else
                        Throw New Exception("GT21012026: No está definido el formato de etiqueta")
                    End If

                Else
                    Throw New Exception("GT14022024: No se cargaron las propiedades de la etiqueta.")
                End If

                Dim obj As New clsBeI_nav_barras_pallet With {
                        .IdPallet = 0,                           'se calcula al insertar
                        .Codigo = pReDet.Codigo_Producto,
                        .Nombre = pReDet.Nombre_producto,
                        .Camas_Por_Tarima = pCamasPorTarima,
                        .Cajas_Por_Cama = pCajasPorCama,
                        .Cantidad_Presentacion = pCantidad,        ' float NULL
                        .UM_Producto = pReDet.Nombre_unidad_medida_basica,
                        .Lote = cmbLote.Text,                ' nvarchar(100) NOT NULL
                        .Fecha_Agregado = Now,               ' datetime NULL
                        .Fecha_Ingreso = New Date(1990, 1, 1),                ' date NULL
                        .Fecha_Vence = vFechaVence,                  ' date NULL
                        .Fecha_Produccion = New Date(1990, 1, 1),             ' date NULL
                        .Activo = 1,                       ' bit NULL
                        .Recibido = 1,                     ' int NULL
                        .IdRecepcion = Nothing,                  ' int NULL
                        .Bodega_Origen = BeBodega_Origen.Codigo,
                        .Bodega_Destino = BeBodega_Origen.Codigo,
                        .Codigo_barra = vCodigoBarra,    'lic_plate      
                        .Cantidad_UMP = Nothing,
                        .Lote_Numerico = Nothing
                    }

                clsLnI_nav_barras_pallet.Guardar_Pallet_PreImpresion(obj, clsTransaccion.lConnection,
                                                                                      clsTransaccion.lTransaction)

                Incrementar_Licencia_BOF(AP.IdBodega,
                                     AP.UsuarioAp.IdUsuario,
                                     clsTransaccion.lConnection,
                                     clsTransaccion.lTransaction)

                clsTransaccion.Commit_Transaction()

            Else
                DxErrorProvider1.SetError(cmbPrinterLicencia, "seleccione impresora")
                clsTransaccion.RollBack_Transaction()
            End If

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub


End Class