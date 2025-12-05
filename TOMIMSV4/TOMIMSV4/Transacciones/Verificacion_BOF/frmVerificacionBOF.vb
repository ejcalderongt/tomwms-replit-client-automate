Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen
Imports TOMWMS.frmVerificacionBOF

Public Class frmVerificacionBOF
    Public pBePedidoEnc As New clsBeTrans_pe_enc
    Public Property InvokeListarPedidos As ListarPedidos
    Public Delegate Sub ListarPedidos()
    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public Delegate Sub Cargar_Datos_Pedido()

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    'variables privadas
    Private pBeCliente As clsBeCliente
    Private pBeProducto As New clsBeProducto
    Private pBeStock As New clsBeStock
    Private pBeStockRes As New clsBeStock_res
    Private pClienteTiemposList As New List(Of clsBeCliente_tiempos)
    Private pBePedidoDet As New clsBeTrans_pe_det
    Private pBePedidoDetList As New List(Of clsBeTrans_pe_det)
    Private Propietario As New clsBePropietarios
    Public listarPicking As New List(Of clsBeTrans_picking_enc)
    Private EsKit As Boolean = False
    Private PedidoGuardadoPorUsuario As Boolean = False
    Private plistPickingUbic As New List(Of clsBeTrans_picking_ubic)
    Private BePickingUbicList As New List(Of clsBeTrans_picking_ubic)
    Private gBePedidoDetVerif As New clsBeDetallePedidoAVerificar

    '#GT27112025: Guardar en memoria el producto seleccionado del pedido y marcarlo como Ok o Pausa
    Private pBeTransPickingUbicTemp As New clsBeTrans_picking_ubic

    '#CKFK20220325 Agregué estas dos variables para cuando el cliente se maneje en el detalle del pedido
    Private Cliente_Detalle_Ultimo_Lote As Integer
    Private Cliente_Detalle_Control_Calidad As Integer
    Private BeBodega As New clsBeBodega()
    Private BeConfigBodega As New clsBeI_nav_config_enc()
    '#GT27112025: patron para mejorar consulta de imagenes
    Private vRutaCDN As String = ""
    Private _listaRutasPng As List(Of String)

    Private Confirmar_SKU As Boolean = False
    Private pMotivo As Integer = 0
    Private pEstado As Integer = 0

    Private BeLogVeficacion As New clsBeLog_verificacion_bof()

    Private Sub frmVerificacionBOF_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        pBeTransPickingUbicTemp = New clsBeTrans_picking_ubic()
        plistPickingUbic = New List(Of clsBeTrans_picking_ubic)
        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            vRutaCDN = clsLnBodega.GetRutaCDN_By_Idbodega(AP.IdBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)


            If String.IsNullOrEmpty(vRutaCDN) Then
                XtraMessageBox.Show("No esta definida la ruta hacia la galeria de imagenes.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            'Dim archivosPng() As String = Directory.GetFiles(vRutaCDN, "*.png")
            '_listaRutasPng = archivosPng.ToList()

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pBePedidoEnc.IdBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando datos...")

            If pBePedidoEnc Is Nothing Then Exit Sub

            LimpiarControlesGrupo()

            Cargar_Datos(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            AplicarEstiloScanner()

            Cargar_Estados()

            txtScanner.SelectAll()
            txtScanner.Focus()

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub Cargar_Estados()
        Try

            Dim _listaEstados = clsLnVerificacion_estado.Get_All()

            With cmbEstado.Properties
                .DataSource = _listaEstados
                .ValueMember = "IdEstado"
                .DisplayMember = "Descripcion"
                .NullText = ""
                .ShowHeader = False
                .Columns.Clear()
                .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", "Estado"))
            End With

            cmbEstado.EditValue = Nothing
            cmbMotivo.Properties.DataSource = Nothing
            cmbMotivo.EditValue = Nothing

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
        End Try
    End Sub

    Private Sub Cargar_Motivos(idEstado As Integer)
        Try

            'Dim _listaMotivos = Motivo.ObtenerDemo()
            Dim _listaMotivos = clsLnVerificacion_motivo.GetSingle_By_IdEstado(idEstado)

            With cmbMotivo.Properties
                .DataSource = _listaMotivos
                .ValueMember = "IdMotivo"
                .DisplayMember = "Descripcion"
                .NullText = ""
                .ShowHeader = False
                .Columns.Clear()
                .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", "Motivo"))
            End With
            cmbMotivo.EditValue = Nothing

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
        End Try
    End Sub

    Private Sub Cargar_Datos(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            If Not pBePedidoEnc Is Nothing Then

                pBePedidoEnc.IsNew = False

                txtIdPedidoEnc.Text = pBePedidoEnc.IdPedidoEnc
                txtReferencia.Text = pBePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino
                txtIdPedidoEnc.Enabled = False
                txtReferencia.Enabled = False

                Cargar_Detalle_Pedido(lConnection,
                                      lTransaction)

                BePickingUbicList = New List(Of clsBeTrans_picking_ubic)
                BePickingUbicList = pBePedidoEnc.Picking.ListaPickingUbic

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

    Private Function Set_DataTable() As DataTable

        Dim dt As New DataTable()
        dt.Columns.Add("SKU", GetType(String))
        dt.Columns.Add("No_Linea", GetType(Integer))
        dt.Columns.Add("CodigoProducto", GetType(String))
        dt.Columns.Add("NombreProducto", GetType(String))
        dt.Columns.Add("Talla", GetType(String))
        dt.Columns.Add("Color", GetType(String))
        dt.Columns.Add("UmBas", GetType(String))
        dt.Columns.Add("CantidadPickeada", GetType(Double))
        dt.Columns.Add("CantidadVerificada", GetType(Double))
        dt.Columns.Add("IdPedidoEnc", GetType(Integer))
        dt.Columns.Add("IdPedidoDet", GetType(Integer))
        dt.Columns.Add("IdStockEspecifico", GetType(Integer))
        dt.Columns.Add("Licencia", GetType(String))
        dt.Columns.Add("IdPickingUbic", GetType(Integer))
        dt.Columns.Add("IdProductoTallaColor", GetType(Integer))
        dgridListaPedido.DataSource = dt
        gvListaPedido.RefreshData()

        Return dt

    End Function

    Private Sub Cargar_Detalle_Pedido(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            dgridListaPedido.DataSource = Nothing

            ' Limpia filas existentes del GridView
            gvListaPedido.BeginUpdate()
            gvListaPedido.RefreshData()

            Set_DataTable()

            Dim i As Integer = -1
            Dim vCantidadPickeada As Double = 0
            Dim vCantidadVerificada As Double = 0
            Dim IndicePadre As Integer = -1
            Dim vCodigoPadre As String = ""
            Dim vClienteTiempo As New clsBeCliente_tiempos

            If Not pClienteTiemposList Is Nothing Then
                vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                            x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                            And x.IdFamilia = pBeProducto.Familia.IdFamilia)
            End If

            Dim vDiasVencimientoCliente As Integer = 0

            vDiasVencimientoCliente = 1
            Cliente_Detalle_Ultimo_Lote = 0
            Cliente_Detalle_Control_Calidad = 0


            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            Application.DoEvents()

            If Not pBePedidoEnc Is Nothing Then

                For Each pDet As clsBeTrans_pe_det In pBePedidoEnc.Detalle.OrderBy(Function(x) x.No_linea)

                    pBeStock = New clsBeStock
                    pBeProducto = New clsBeProducto
                    pBeProducto.IdProducto = pDet.Producto.IdProducto

                    If SplashScreenManager.Default Is Nothing Then
                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormDescription("Obteniendo código: " & pDet.Codigo_Producto)
                    End If

                    If Not pDet.EsPadre AndAlso Not pDet.IdPedidoDetPadre > 0 Then

                        ' ====== ADD NEW ROW EN GRIDVIEW ======
                        gvListaPedido.AddNewRow()
                        i = gvListaPedido.FocusedRowHandle

                        ' Setear valores base (equivalente a Rows.Add(...))
                        gvListaPedido.SetRowCellValue(i, "No_Linea", pDet.No_linea)
                        gvListaPedido.SetRowCellValue(i, "CodigoProducto", pDet.Codigo_Producto)
                        gvListaPedido.SetRowCellValue(i, "NombreProducto", pDet.Nombre_producto)

                        Dim pTempPickingUbic = pDet.ListaPickingUbic.Find(Function(x) x.IdPedidoEnc = pDet.IdPedidoEnc AndAlso x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdProductoBodega = pDet.IdProductoBodega)

                        If pTempPickingUbic IsNot Nothing Then
                            gvListaPedido.SetRowCellValue(i, "Licencia", pTempPickingUbic.Lic_plate)
                            gvListaPedido.SetRowCellValue(i, "IdPickingUbic", pTempPickingUbic.IdPickingUbic)
                        End If

                        pBeProducto.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBeProducto.IdProducto,
                                                                                                                            pBePedidoEnc.IdBodega,
                                                                                                                            lConnection,
                                                                                                                            lTransaction)


                        If BeBodega.Control_Talla_Color Then

                            Dim BeProductoTc = clsLnProducto_talla_color.GetSingle(pDet.IdProductoTallaColor, lConnection, lTransaction)

                            If BeProductoTc IsNot Nothing Then

                                gvListaPedido.SetRowCellValue(i, "SKU", BeProductoTc.CodigoSKU)

                                Dim BeTalla = clsLnTalla.GetSingle(BeProductoTc.IdTalla, lConnection, lTransaction)
                                Dim BeColor = clsLnColor.GetSingle(BeProductoTc.IdColor, lConnection, lTransaction)

                                gvListaPedido.SetRowCellValue(i, "Talla", BeTalla.Codigo)
                                gvListaPedido.SetRowCellValue(i, "Color", BeColor.Codigo)
                                gvListaPedido.SetRowCellValue(i, "IdProductoTallaColor", BeProductoTc.IdProductoTallaColor)

                            Else
                                Throw New Exception("No se encontró la talla y color asociada al IdProductoTallaColor " & BeProductoTc.IdProductoTallaColor)
                            End If

                        End If

                        gvListaPedido.SetRowCellValue(i, "UmBas", pDet.Nom_unid_med)

                        pBeStock.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega
                        pBeStock.ProductoEstado.IdEstado = pDet.IdEstado
                        pBeStock.Presentacion.IdPresentacion = pDet.IdPresentacion
                        pBeStock.IdPresentacion = pDet.IdPresentacion
                        pBeStock.IdBodega = BeBodega.IdBodega

                        ''#EJC20171025_0217PM: Si no se manda la unidad de medida no devuelve el stock disponible en el pedido.
                        pBeStock.IdUnidadMedida = pDet.IdUnidadMedidaBasica

                        If pBeStock.IdProductoBodega <> 0 AndAlso pBeStock.ProductoEstado.IdEstado <> 0 Then

                            pBeStock.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega
                            pDet.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega

                            'Obtiene la cantidad disponible restando la cantidad reservada.
                            clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStock,
                                                                     BeBodega.IdBodega,
                                                                     True,
                                                                     False,
                                                                     vDiasVencimientoCliente,
                                                                     True,
                                                                     lConnection,
                                                                     lTransaction)

                            pDet.CantidadReservada = clsLnStock.Get_Cantidad_Reservada_By_IdPedidoDet(pBeStock,
                                                                                                      pDet.IdPedidoDet,
                                                                                                      lConnection,
                                                                                                      lTransaction,
                                                                                                      True)
                            'GT 270720210843: para un pedido, si se edita, es porque ya se guardo, y no se debe sumar lo reservado más la existencia
                            If Modo = TipoTrans.Editar Then

                            Else
                                '#EJC20171021_1108AM: Obtiene la cantidad reservada por detalle de pedido para considerarla como disponible.
                                pBeStock.Cantidad += pDet.CantidadReservada
                            End If

                            pDet.PesoReservado = clsLnStock.Get_Peso_Reservado(pBeStock,
                                                                           pDet.IdPedidoDet,
                                                                           lConnection,
                                                                           lTransaction,
                                                                           True)

                            '#EJC20171021_1108AM: Obtiene el peso reservado por detalle de pedido para considerarlo como disponible.
                            pBeStock.Peso += pDet.PesoReservado

                            ''#EJC20171025_0221PM: Desplegar cantidad disponible en base a presentación cuando se edita un pedido.
                            If Not pBeStock.Presentacion Is Nothing Then

                                If pBeStock.Presentacion.IdPresentacion <> 0 Then
                                    gvListaPedido.SetRowCellValue(i, "CantidadExistencia", pBeStock.Cantidad)
                                Else
                                    gvListaPedido.SetRowCellValue(i, "CantidadExistencia", pBeStock.Cantidad)
                                End If

                            Else
                                gvListaPedido.SetRowCellValue(i, "CantidadExistencia", pBeStock.Cantidad)
                            End If

                            '#EJC20171021_0527PM: Obtener la cantidad pickeada.
                            If Not pDet.ListaPickingUbic Is Nothing Then

                                Try

                                    Dim vCantidadRecUMBas As Double = 0
                                    Dim vCantidadVerUMBas As Double = 0
                                    vCantidadPickeada = 0
                                    vCantidadVerificada = 0

                                    If pDet.IdPresentacion > 0 Then
                                        '#CM_20191128: Esta fue la única manera que encontré para sumar bien la cantidad del pedido cuando se tiene reservada 
                                        'X cantidad del pedido en presentación y la otra parte en UMBas.
                                        vCantidadRecUMBas = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Recibida)
                                        vCantidadVerUMBas = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Verificada)
                                    End If

                                    If vCantidadRecUMBas > 0 OrElse vCantidadVerUMBas > 0 Then
                                        '#CM_20191128: Busco de primero la cantidad total con presentación

                                        vCantidadPickeada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Recibida)
                                        vCantidadVerificada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Verificada)

                                        '#CM_20191128: Busco el factor de la presentación
                                        pDet.Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pDet.IdProductoBodega, pDet.IdPresentacion, lConnection, lTransaction)

                                        '#CM_20191128: Divido la cantidad UMBas entre el factor
                                        vCantidadRecUMBas = Math.Round(vCantidadRecUMBas / pDet.Factor, 6)
                                        vCantidadVerUMBas = Math.Round(vCantidadVerUMBas / pDet.Factor, 6)

                                        '#CM_20191128: Sumo las cantidades.
                                        vCantidadPickeada = Math.Round(vCantidadPickeada + vCantidadRecUMBas, 6)
                                        vCantidadVerificada = Math.Round(vCantidadVerificada + vCantidadVerUMBas, 6)
                                    Else

                                        If pDet.IdPresentacion = 0 Then

                                            Dim vFactor As Integer = 0

                                            For Each ubic In pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet).ToList
                                                If ubic.IdPresentacion <> 0 Then

                                                    vFactor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pDet.IdProductoBodega, ubic.IdPresentacion, lConnection, lTransaction)

                                                    vCantidadPickeada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = ubic.IdPresentacion).Sum(Function(y) y.Cantidad_Recibida) * vFactor
                                                    vCantidadVerificada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = ubic.IdPresentacion).Sum(Function(y) y.Cantidad_Verificada) * vFactor

                                                Else

                                                    vCantidadPickeada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Recibida)
                                                    vCantidadVerificada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Verificada)

                                                End If
                                            Next

                                        Else

                                            vCantidadPickeada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Recibida)
                                            vCantidadVerificada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Verificada)

                                        End If

                                    End If


                                Catch ex As Exception
                                    '#EJC201710210531PM: No se pudo obtener la cantidad pickeada de la lista, podr?a pasar pero aun no se porqu? ;) 
                                End Try

                                gvListaPedido.SetRowCellValue(i, "CantidadPickeada", vCantidadPickeada)
                                gvListaPedido.SetRowCellValue(i, "CantidadVerificada", vCantidadVerificada)

                                Dim vDif As Double = (pDet.Cantidad - Math.Round(vCantidadPickeada, 6))

                            End If

                        End If

                        gvListaPedido.SetRowCellValue(i, "IdPedidoEnc", pDet.IdPedidoEnc)
                        gvListaPedido.SetRowCellValue(i, "IdPedidoDet", pDet.IdPedidoDet)

                        If pDet.IdStockEspecifico > 0 Then
                            gvListaPedido.SetRowCellValue(i, "IdStockEspecifico", pDet.IdStockEspecifico)
                        End If

                        gvListaPedido.UpdateCurrentRow()

                    Else
                        If pDet.EsPadre Then

                            gvListaPedido.AddNewRow()
                            i = gvListaPedido.FocusedRowHandle

                            gvListaPedido.SetRowCellValue(i, "No_Linea", pDet.No_linea)
                            gvListaPedido.SetRowCellValue(i, "CodigoProducto", pDet.Codigo_Producto)
                            gvListaPedido.SetRowCellValue(i, "NombreProducto", pDet.Nombre_producto)

                            IndicePadre = i
                            vCodigoPadre = pDet.Codigo_Producto

                            gvListaPedido.SetRowCellValue(i, "IdPedidoDet", pDet.IdPedidoDet)

                            If pDet.IdStockEspecifico > 0 Then
                                gvListaPedido.SetRowCellValue(i, "IdStockEspecifico", pDet.IdStockEspecifico)
                            End If

                            gvListaPedido.UpdateCurrentRow()
                        End If

                        pDet.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega

                        If pDet.IdPedidoDetPadre <> 0 Then

                            pBeStock.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega

                        End If

                    End If

                    i += 1

                    pBePedidoDetList.Add(pDet)

                    Application.DoEvents()

                Next

                gvListaPedido.BestFitColumns()

            End If

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)

        Finally
            SplashScreenManager.CloseForm(False)
            gvListaPedido.EndUpdate()
        End Try

    End Sub

    Private Sub txtScanner_KeyDown(sender As Object, e As KeyEventArgs) Handles txtScanner.KeyDown

        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then
            e.SuppressKeyPress = True   ' evita beep / salto raro
            ProcesarScan()
        End If

    End Sub

    Private Sub ProcesarScan()

        Dim sku As String = txtScanner.Text.Trim()

        Dim estado As String = sku.ToUpperInvariant()

        Select Case estado

            Case "OK"
                If ProcesarLinea() Then
                    txtScanner.Text = ""
                    Confirmar_SKU = False
                End If

            Case "PAUSA"
                ' Poner en pausa: bloquear controles para impedir cerrar o escanear otro producto
                'ProcesarEstadoPausa()

            Case Else

                If Confirmar_SKU Then
                    MessageBox.Show(
                           $"Hay un SKU en cola y requiere confirmación de 'OK' o 'Pausa' ",
                           "Estado inválido",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Warning
                       )
                    txtScanner.Text = ""
                End If

                If Not BuscarSKU_Y_Cargar(estado) Then
                    'MessageBox.Show(
                    '        $"Estado no reconocido: [{sku}]. Escanee un SKU, o un estado para cerrar o pausar la tarea.",
                    '        "Estado inválido",
                    '        MessageBoxButtons.OK,
                    '        MessageBoxIcon.Warning
                    '    )
                    Confirmar_SKU = False
                Else
                    Confirmar_SKU = True
                    txtScanner.Text = ""
                End If

        End Select

        txtScanner.SelectAll()
        txtScanner.Focus()

    End Sub

    Private Function BuscarSKU_Y_Cargar(ByVal sku As String) As Boolean

        BuscarSKU_Y_Cargar = False
        BeLogVeficacion = New clsBeLog_verificacion_bof()
        Try

            If String.IsNullOrWhiteSpace(sku) Then Exit Function

            Dim dt As DataTable = TryCast(dgridListaPedido.DataSource, DataTable)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then Exit Function

            ' Buscar en DataTable
            Dim filas() As DataRow = dt.Select("[SKU] = '" & sku.Replace("'", "''") & "'")

            If filas.Length = 0 Then
                XtraMessageBox.Show("No se encontró el SKU, por favor reintente: " & sku, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                LimpiarControlesGrupo()
                Exit Function
            End If

            Dim row As DataRow = filas(0)

            ' --- FOCUS EN GRID: ubicar fila por SKU ---
            Dim handle As Integer = gvListaPedido.LocateByValue("SKU", sku)
            If handle >= 0 Then
                gvListaPedido.FocusedRowHandle = handle
                gvListaPedido.MakeRowVisible(handle)
            End If

            ' --- CARGAR INPUTS ---
            txtScanner.Text = sku
            txtDescripcionProducto.Text = CStr(row("NombreProducto"))
            txtTalla.Text = If(dt.Columns.Contains("Talla"), CStr(row("Talla")), "")
            txtColor.Text = If(dt.Columns.Contains("Color"), CStr(row("Color")), "")
            txtCantidad.Text = If(dt.Columns.Contains("CantidadPickeada"), CInt(row("CantidadPickeada")), "")

            '-- Cargar objeto picking_ubicTemp y al leer la barra OK, se asignará a la lista para enviar a verificar
            Dim pIdPickingUbic = If(dt.Columns.Contains("IdPickingUbic"), CInt(row("IdPickingUbic")), 0)
            Dim pLicPlate = If(dt.Columns.Contains("Licencia"), CStr(row("Licencia")), "")
            Dim pIdPedidoEnc = If(dt.Columns.Contains("IdPedidoEnc"), CInt(row("IdPedidoEnc")), 0)
            Dim pIdPedidoDet = If(dt.Columns.Contains("IdPedidoDet"), CInt(row("IdPedidoDet")), 0)
            Dim pSku = If(dt.Columns.Contains("SKU"), CStr(row("SKU")), "")
            Dim pIdProductoTallaColor = If(dt.Columns.Contains("IdProductoTallaColor"), CInt(row("IdProductoTallaColor")), 0)

            pBeTransPickingUbicTemp = New clsBeTrans_picking_ubic()
            pBeTransPickingUbicTemp = BePickingUbicList.Find(Function(x) x.IdPickingUbic = pIdPickingUbic _
                                                                           AndAlso x.IdPedidoEnc = pIdPedidoEnc _
                                                                           AndAlso x.IdPedidoDet = pIdPedidoDet _
                                                                           AndAlso x.Lic_plate = pLicPlate)

            If pBeTransPickingUbicTemp IsNot Nothing Then

                '#GT04122025: se llena el objeto de log cada vez que se escanea un SKU
                BeLogVeficacion.IdPickingEnc = pBeTransPickingUbicTemp.IdPickingEnc
                BeLogVeficacion.IdPickingDet = pBeTransPickingUbicTemp.IdPickingDet
                BeLogVeficacion.IdPickingUbic = pBeTransPickingUbicTemp.IdPickingUbic
                BeLogVeficacion.IdPedidoEnc = pBeTransPickingUbicTemp.IdPedidoEnc
                BeLogVeficacion.IdPedidoDet = pBeTransPickingUbicTemp.IdPedidoDet
                BeLogVeficacion.IdBodega = pBeTransPickingUbicTemp.IdBodega
                BeLogVeficacion.Fec_agr = Now
                BeLogVeficacion.User_agr = AP.UsuarioAp.IdUsuario
                BeLogVeficacion.Sku = pSku
                BeLogVeficacion.Cantidad = Convert.ToInt16(txtCantidad.Text)
                BeLogVeficacion.IdProductoTallaColor = pIdProductoTallaColor


                pBeTransPickingUbicTemp.CodigoSKU = pSku.ToString()

                '#GT02122025: si el SKU exite, validar que no se haya verificado antes
                Dim Existe = plistPickingUbic.Find(Function(x) x.IdPickingUbic = pBeTransPickingUbicTemp.IdPickingUbic AndAlso
                                                               x.IdPedidoEnc = pBeTransPickingUbicTemp.IdPedidoEnc AndAlso
                                                               x.IdPedidoDet = pBeTransPickingUbicTemp.IdPedidoDet AndAlso
                                                               x.Lic_plate = pBeTransPickingUbicTemp.Lic_plate)

                If Existe IsNot Nothing Then
                    XtraMessageBox.Show("El SKU ya fue verificado." & sku, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LimpiarControlesGrupo()
                    Exit Function
                End If

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                If SplashScreenManager.Default IsNot Nothing Then
                    SplashScreenManager.Default.SetWaitFormCaption("Cargando imagen...")
                End If

                Application.DoEvents()
                CargarImagenProducto(sku)

                BuscarSKU_Y_Cargar = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            If SplashScreenManager.Default IsNot Nothing Then
                SplashScreenManager.CloseForm(False)
            End If
        End Try
    End Function

    Private Sub CargarImagenProducto(ByVal sku As String)


        Try

            Dim img As Image
            Dim codigoSKU As String = sku
            Dim productoBase As String = codigoSKU
            Dim talla As String = ""
            Dim color As String = ""

            If codigoSKU.Length >= 13 Then
                productoBase = codigoSKU.Substring(0, 10)
                talla = codigoSKU.Substring(10, 3)
                If codigoSKU.Length > 13 Then
                    color = codigoSKU.Substring(13)
                End If
            ElseIf codigoSKU.Length >= 10 Then
                productoBase = codigoSKU.Substring(0, 10)
            End If

            ' --- PATRONES COMO PREFIJOS (SIN "*.png") ---
            Dim patrones As New List(Of String)

            If talla <> "" AndAlso color <> "" Then
                patrones.Add("._" & productoBase & "-" & talla & "-" & color)
                patrones.Add(productoBase & "-" & talla & "-" & color)
            End If

            If talla <> "" Then
                patrones.Add("._" & productoBase & "-" & talla)
                patrones.Add(productoBase & "-" & talla)
            End If

            patrones.Add("._" & productoBase)
            patrones.Add(productoBase)

            ' --- BÚSQUEDA EN LA LISTA EN MEMORIA (_listaRutasPng) ---
            Dim archivoEncontrado As String = Nothing

            If _listaRutasPng IsNot Nothing AndAlso _listaRutasPng.Count > 0 Then
                For Each prefijo In patrones
                    archivoEncontrado = _listaRutasPng _
                    .FirstOrDefault(Function(ruta)
                                        Dim nombre = Path.GetFileName(ruta)
                                        Return nombre.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase)
                                    End Function)

                    If Not String.IsNullOrEmpty(archivoEncontrado) Then
                        Exit For
                    End If
                Next
            End If


            If Not String.IsNullOrEmpty(archivoEncontrado) AndAlso File.Exists(archivoEncontrado) Then
                peProducto.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
                peProducto.Image = Nothing

                Using fs As New FileStream(archivoEncontrado, FileMode.Open, FileAccess.Read)
                    'peProducto.Image = Image.FromStream(fs)
                    img = Image.FromStream(fs)
                End Using

                '#GT27112025: mejora para redimensionar hasta un 200% como máximo
                Dim imgEscalada = EscalarImagen(img, 2.0)
                peProducto.Properties.SizeMode = PictureSizeMode.Squeeze
                peProducto.Image = imgEscalada


            Else
                peProducto.Image = Nothing
            End If

        Catch ex As Exception
            peProducto.Image = Nothing
        End Try

    End Sub

    Private Sub CargarImagenProducto_temp(ByVal sku As String)

        Try

            'Dim vRutaCDN As String = clsLnBodega.GetRutaCDN_By_Idbodega(AP.IdBodega)

            Dim codigoSKU As String = sku
            Dim productoBase As String = codigoSKU
            Dim talla As String = ""
            Dim color As String = ""

            If codigoSKU.Length >= 13 Then
                productoBase = codigoSKU.Substring(0, 10)
                talla = codigoSKU.Substring(10, 3)
                If codigoSKU.Length > 13 Then
                    color = codigoSKU.Substring(13)
                End If
            ElseIf codigoSKU.Length >= 10 Then
                productoBase = codigoSKU.Substring(0, 10)
            End If

            Dim patrones As New List(Of String)

            If talla <> "" AndAlso color <> "" Then
                patrones.Add("._" & productoBase & "-" & talla & "-" & color & "*.png")
                patrones.Add(productoBase & "-" & talla & "-" & color & "*.png")
            End If

            If talla <> "" Then
                patrones.Add("._" & productoBase & "-" & talla & "*.png")
                patrones.Add(productoBase & "-" & talla & "*.png")
            End If

            patrones.Add("._" & productoBase & "*.png")
            patrones.Add(productoBase & "*.png")

            Dim archivoEncontrado As String = Nothing
            For Each patron In patrones
                Dim archivos() As String = Directory.GetFiles(vRutaCDN, patron)
                If archivos.Length > 0 Then
                    archivoEncontrado = archivos(0)
                    Exit For
                End If
            Next

            ' --- ASIGNACIÓN CORRECTA AL PictureEdit ---
            If Not String.IsNullOrEmpty(archivoEncontrado) AndAlso File.Exists(archivoEncontrado) Then

                peProducto.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
                ' Libera imagen anterior para evitar bloqueo de archivo
                peProducto.Image = Nothing

                ' Carga la imagen al PictureEdit
                Using fs As New FileStream(archivoEncontrado, FileMode.Open, FileAccess.Read)
                    peProducto.Image = Image.FromStream(fs)
                End Using

            Else
                peProducto.Image = Nothing
            End If

        Catch ex As Exception
            peProducto.Image = Nothing
        End Try

    End Sub

    Private Sub AplicarEstiloScanner()

        With txtScanner.Properties.AppearanceFocused
            .BackColor = Color.LightYellow
            .Font = New Font(txtScanner.Font, FontStyle.Bold)
            .Options.UseBackColor = True
            .Options.UseFont = True
        End With

        ' opcional: también estilo normal cuando NO tiene foco
        With txtScanner.Properties.Appearance
            .BackColor = Color.White
            .Font = New Font(txtScanner.Font, FontStyle.Regular)
            .Options.UseBackColor = True
            .Options.UseFont = True
        End With

    End Sub

    'Private Sub txtEstado_KeyDown(sender As Object, e As KeyEventArgs) Handles txtEstado.KeyDown
    '    Try

    '        If e.KeyCode <> Keys.Enter Then Return

    '        Dim valorLeido As String = txtEstado.Text.Trim()
    '        txtEstado.Clear()

    '        ' Normalizamos para comparar (ignorando mayúsculas/minúsculas y espacios)
    '        Dim estado As String = valorLeido.ToUpperInvariant()

    '        Select Case estado
    '            Case "OK"
    '                ' Producto confirmado correctamente
    '                ProcesarLinea()

    '            Case "PAUSA"
    '                ' Poner en pausa: bloquear controles para impedir cerrar o escanear otro producto
    '                'ProcesarEstadoPausa()

    '            Case Else
    '                ' Cualquier otra cosa se considera no válida
    '                MessageBox.Show(
    '                    $"Estado no reconocido: [{valorLeido}]. Escanee un código 'OK' o 'Pausa'.",
    '                    "Estado inválido",
    '                    MessageBoxButtons.OK,
    '                    MessageBoxIcon.Warning
    '                )

    '                ' Vuelve a esperar un estado correcto
    '                txtEstado.Focus()
    '        End Select

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try
    'End Sub

    Private Function EscalarImagen(ByVal imgOriginal As Image, ByVal factor As Double) As Image
        Dim nuevoAncho As Integer = CInt(imgOriginal.Width * factor)
        Dim nuevoAlto As Integer = CInt(imgOriginal.Height * factor)

        Dim bmp As New Bitmap(nuevoAncho, nuevoAlto)

        Using g As Graphics = Graphics.FromImage(bmp)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

            g.DrawImage(imgOriginal, 0, 0, nuevoAncho, nuevoAlto)
        End Using

        Return bmp
    End Function


    Private Function ProcesarLinea() As Boolean
        ProcesarLinea = False

        Try
            '--- marcar que este picking es nuevo (confirmado) y agregar a la lista ---
            pBeTransPickingUbicTemp.IsNew = True
            plistPickingUbic.Add(pBeTransPickingUbicTemp)

            '--- UBICAR FILA EN EL GRID PARA REFRESCARLA ---
            Dim dt As DataTable = TryCast(dgridListaPedido.DataSource, DataTable)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso pBeTransPickingUbicTemp IsNot Nothing Then

                Dim idPicking As Integer = pBeTransPickingUbicTemp.IdPickingUbic
                Dim idPedidoEnc As Integer = pBeTransPickingUbicTemp.IdPedidoEnc
                Dim idPedidoDet As Integer = pBeTransPickingUbicTemp.IdPedidoDet

                Dim filtro As String = String.Format("IdPickingUbic = {0} AND IdPedidoEnc = {1} AND IdPedidoDet = {2}",
                                                 idPicking, idPedidoEnc, idPedidoDet)

                Dim filas() As DataRow = dt.Select(filtro)

                If filas IsNot Nothing AndAlso filas.Length > 0 Then
                    Dim row As DataRow = filas(0)
                    Dim index As Integer = dt.Rows.IndexOf(row)

                    If index >= 0 Then
                        Dim handle As Integer = gvListaPedido.GetRowHandle(index)

                        If handle >= 0 Then
                            ' Opcional: enfocar la fila confirmada
                            'gvListaPedido.FocusedRowHandle = handle
                            'gvListaPedido.MakeRowVisible(handle)

                            ' Forzar repintado para que RowStyle aplique el color
                            gvListaPedido.RefreshRow(handle)
                        End If
                    End If
                End If
            End If

            LimpiarControlesGrupo()

            'txtScanner.SelectAll()
            'txtScanner.Focus()

            ProcesarLinea = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub gvListaPedido_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gvListaPedido.RowStyle
        Try
            Dim view As GridView = TryCast(sender, GridView)
            If view Is Nothing Then Exit Sub
            If e.RowHandle < 0 Then Exit Sub
            If plistPickingUbic Is Nothing OrElse plistPickingUbic.Count = 0 Then Exit Sub

            ' Tomar los valores de la fila actual
            Dim idPickingObj As Object = view.GetRowCellValue(e.RowHandle, "IdPickingUbic")
            Dim idEncObj As Object = view.GetRowCellValue(e.RowHandle, "IdPedidoEnc")
            Dim idDetObj As Object = view.GetRowCellValue(e.RowHandle, "IdPedidoDet")

            If idPickingObj Is Nothing OrElse idEncObj Is Nothing OrElse idDetObj Is Nothing _
            OrElse idPickingObj Is DBNull.Value OrElse idEncObj Is DBNull.Value OrElse idDetObj Is DBNull.Value Then
                Exit Sub
            End If

            Dim idPicking As Integer = Convert.ToInt32(idPickingObj)
            Dim idEnc As Integer = Convert.ToInt32(idEncObj)
            Dim idDet As Integer = Convert.ToInt32(idDetObj)

            ' Verificar si esta combinación ya está confirmada en plistPickingUbic
            Dim estaConfirmado As Boolean =
            plistPickingUbic.Any(Function(x) x.IdPickingUbic = idPicking _
                                      AndAlso x.IdPedidoEnc = idEnc _
                                      AndAlso x.IdPedidoDet = idDet)

            If estaConfirmado Then
                e.Appearance.BackColor = Color.LightGreen
                e.Appearance.ForeColor = Color.Black
                e.Appearance.Options.UseBackColor = True
                e.Appearance.Options.UseForeColor = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LimpiarControlesGrupo()
        txtScanner.Text = ""
        txtDescripcionProducto.Text = ""
        txtTalla.Text = ""
        txtColor.Text = ""
        txtCantidad.Text = ""
    End Sub

    Private Sub cmdEnviar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdEnviar.ItemClick
        Try

            cmdEnviar.Enabled = False
            Guardar_Verificacion()
            cmdEnviar.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Guardar_Verificacion()

        Try

            If pMotivo > 0 Then

                If plistPickingUbic.Count > 0 Then
                    XtraMessageBox.Show("Proceso detenido por motivo: " & cmbMotivo.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    '#GT03122025: aqui se guarda en la tabla la razón de la pausa, y se retorna a la lista de pedidos
                    BeLogVeficacion.IdEstado = cmbEstado.EditValue
                    BeLogVeficacion.IdMotivo = cmbMotivo.EditValue
                    clsLnLog_verificacion_bof.Guardar_Log(BeLogVeficacion)

                    If InvokeListarPedidos IsNot Nothing Then
                        InvokeListarPedidos.Invoke()
                    End If

                    Me.DialogResult = DialogResult.OK
                Else
                    XtraMessageBox.Show("No se ha fiscalizado un producto para asociarlo al motivo seleccionado", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If


            Else
                If plistPickingUbic.Count > 0 Then

                    If Not clsLnTrans_picking_enc.Guardar_Verificacion_Bof(plistPickingUbic,
                                                                          AP.UsuarioAp.IdUsuario,
                                                                          pBePedidoEnc) Then

                        XtraMessageBox.Show("No se fiscalizó el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        XtraMessageBox.Show("Pedido fiscalizado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        If InvokeListarPedidos IsNot Nothing Then
                            InvokeListarPedidos.Invoke()
                        End If

                        Me.DialogResult = DialogResult.OK

                    End If

                Else
                    XtraMessageBox.Show("No se ha fiscalizado ninguna linea del pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub cmbMotivo_EditValueChanged(sender As Object, e As EventArgs) Handles cmbMotivo.EditValueChanged
        If cmbMotivo.EditValue > 0 Then
            pMotivo = cmbMotivo.EditValue
        End If
    End Sub

    Private Sub cmbEstado_EditValueChanged(sender As Object, e As EventArgs) Handles cmbEstado.EditValueChanged
        If cmbEstado.EditValue > 0 Then
            pEstado = cmbEstado.EditValue
            Cargar_Motivos(pEstado)

        End If
    End Sub
End Class