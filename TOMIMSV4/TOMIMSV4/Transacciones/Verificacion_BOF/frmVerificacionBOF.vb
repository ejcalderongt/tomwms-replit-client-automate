Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmVerificacionBOF
    'Public pBePedidoEnc As New clsBeTrans_pe_enc
    Public pBeListaPedidos As New List(Of clsBeTrans_pe_enc)()

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
    Private BePickingUbicList_Origen As New List(Of clsBeTrans_picking_ubic)
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

    Private Sku_en_proceso As Boolean = False
    Private pMotivo As Integer = 0
    Private pEstado As Integer = 0

    Private BeLogVeficacion As New clsBeLog_verificacion_bof()

    Private Sub frmVerificacionBOF_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        pBeTransPickingUbicTemp = New clsBeTrans_picking_ubic()
        plistPickingUbic = New List(Of clsBeTrans_picking_ubic)

        Dim clsTransaccion As New clsTransaccion()

        Try

            Sku_en_proceso = False

            clsTransaccion.Begin_Transaction()

            vRutaCDN = clsLnBodega.GetRutaCDN_By_Idbodega(AP.IdBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)


            If String.IsNullOrEmpty(vRutaCDN) Then
                XtraMessageBox.Show("No esta definida la ruta hacia la galeria de imagenes.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando rutas...")

            'Dim archivosPng() As String = Directory.GetFiles(vRutaCDN, "*.png")
            '_listaRutasPng = archivosPng.ToList()

            SplashScreenManager.Default.SetWaitFormCaption("Cargando datos...")

            'If pBePedidoEnc Is Nothing Then Exit Sub
            If pBeListaPedidos Is Nothing OrElse pBeListaPedidos.Count = 0 Then Exit Sub

            lbPedidos.Text = "Pedidos: " & pBeListaPedidos.Count

            'GroupControl2.Text = "Lista pedidos: " & pBeListaPedidos.Count

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
        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                SplashScreenManager.Default.CloseWaitForm()
            End If
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
                .Columns.Add(New LookUpColumnInfo("Descripcion", "Estado"))
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

            Cargar_Detalle_Pedido(lConnection, lTransaction)

            Cargar_Detalle_Picking()

            With gvListaPedido
                .Columns("IdPedidoEnc").Visible = False
                .Columns("IdPedidoDet").Visible = False
                .Columns("IdStockEspecifico").Visible = False
                .Columns("IdPickingUbic").Visible = False
                .Columns("IdProductoTallaColor").Visible = False
            End With

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Cargar_Detalle_Picking()
        Try
            ' #GT08122025: obtener la lista de picking para validar los datos de cada producto a verificar.
            BePickingUbicList_Origen = New List(Of clsBeTrans_picking_ubic)

            For Each PedidoEnc As clsBeTrans_pe_enc In pBeListaPedidos

                If PedidoEnc IsNot Nothing AndAlso
               PedidoEnc.Picking IsNot Nothing AndAlso
               PedidoEnc.Picking.ListaPickingUbic IsNot Nothing Then

                    ' En lugar de reemplazar, acumulamos:
                    BePickingUbicList_Origen.AddRange(PedidoEnc.Picking.ListaPickingUbic)

                End If

            Next

        Catch ex As Exception
            ' Manejo de error (si ya lo tienes centralizado, puedes dejarlo vacío aquí)
        End Try
    End Sub

    Private Function Set_DataTable() As DataTable

        Dim dt As New DataTable()
        dt.Columns.Add("SKU", GetType(String))
        dt.Columns.Add("CodigoProducto", GetType(String))
        dt.Columns.Add("NombreProducto", GetType(String))
        dt.Columns.Add("Talla", GetType(String))
        dt.Columns.Add("Color", GetType(String))
        dt.Columns.Add("UmBas", GetType(String))
        'dt.Columns.Add("CantidadPickeada", GetType(Double))
        'dt.Columns.Add("CantidadVerificada", GetType(Double))
        dt.Columns.Add("Pickeada", GetType(Double))
        dt.Columns.Add("Verificada", GetType(Double))
        dt.Columns.Add("No_Linea", GetType(Integer))

        dt.Columns.Add("IdPedidoEnc", GetType(Integer))
        dt.Columns.Add("IdPedidoDet", GetType(Integer))
        dt.Columns.Add("IdStockEspecifico", GetType(Integer))
        dt.Columns.Add("Licencia", GetType(String))
        dt.Columns.Add("IdPickingUbic", GetType(Integer))
        dt.Columns.Add("IdProductoTallaColor", GetType(Integer))

        dt.Columns.Add("Pedido", GetType(Integer))
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Cliente", GetType(String))
        dt.Columns.Add("Bodega", GetType(String))
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

            For Each pPedidoEnc As clsBeTrans_pe_enc In pBeListaPedidos

                BeBodega = New clsBeBodega()
                BeBodega = clsLnBodega.GetSingle_By_Idbodega(pPedidoEnc.IdBodega, lConnection, lTransaction)

                For Each pDet As clsBeTrans_pe_det In pPedidoEnc.Detalle.OrderBy(Function(x) x.No_linea)

                    pBeStock = New clsBeStock
                    pBeProducto = New clsBeProducto
                    pBeProducto.IdProducto = pDet.Producto.IdProducto

                    If SplashScreenManager.Default Is Nothing Then
                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormDescription("Obteniendo código: " & pDet.Codigo_Producto)
                    End If

                    If Not pDet.EsPadre AndAlso Not pDet.IdPedidoDetPadre > 0 Then

                        gvListaPedido.AddNewRow()
                        i = gvListaPedido.FocusedRowHandle

                        '#GT10122025: mostrar pedido, fecha, cliente y bodega
                        gvListaPedido.SetRowCellValue(i, "Pedido", pPedidoEnc.IdPedidoEnc)
                        gvListaPedido.SetRowCellValue(i, "Fecha", pPedidoEnc.Fecha_Pedido)

                        Dim pBeCliente = clsLnCliente.GetSingle(pPedidoEnc.IdCliente, lConnection, lTransaction)
                        If pBeCliente IsNot Nothing Then
                            gvListaPedido.SetRowCellValue(i, "Cliente", pBeCliente.Nombre_comercial)
                        Else
                            gvListaPedido.SetRowCellValue(i, "Cliente", pPedidoEnc.IdCliente)
                        End If
                        gvListaPedido.SetRowCellValue(i, "Bodega", BeBodega.Nombre)
                        gvListaPedido.SetRowCellValue(i, "No_Linea", pDet.No_linea)
                        gvListaPedido.SetRowCellValue(i, "CodigoProducto", pDet.Codigo_Producto)
                        gvListaPedido.SetRowCellValue(i, "NombreProducto", pDet.Nombre_producto)

                        Dim pTempPickingUbic = pDet.ListaPickingUbic.Find(Function(x) x.IdPedidoEnc = pDet.IdPedidoEnc AndAlso x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdProductoBodega = pDet.IdProductoBodega)

                        If pTempPickingUbic IsNot Nothing Then
                            gvListaPedido.SetRowCellValue(i, "Licencia", pTempPickingUbic.Lic_plate)
                            gvListaPedido.SetRowCellValue(i, "IdPickingUbic", pTempPickingUbic.IdPickingUbic)
                        End If

                        pBeProducto.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBeProducto.IdProducto,
                                                                                                                            pPedidoEnc.IdBodega,
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

                                gvListaPedido.SetRowCellValue(i, "Pickeada", vCantidadPickeada)
                                gvListaPedido.SetRowCellValue(i, "Verificada", vCantidadVerificada)

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

            Next

            gvListaPedido.BestFitColumns()


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

                ' No hay SKU en proceso y el objeto indica que no se ha leído ningún SKU (IdPickingUbic = 0)
                If Not Sku_en_proceso Then

                    If pBeTransPickingUbicTemp Is Nothing OrElse pBeTransPickingUbicTemp.IdPickingUbic = 0 Then
                        ' ESTADO 1: Sin SKU asignado
                        ' No hay nada que confirmar con OK
                        XtraMessageBox.Show(
                            "Debe leer un SKU antes de leer la barra 'OK'.",
                            "Estado inválido",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        )

                        LimpiarControlesGrupo()
                        txtScanner.SelectAll()
                        txtScanner.Focus()
                        Exit Sub

                    Else

                        If ProcesarLinea() Then
                            txtScanner.Text = ""
                            Sku_en_proceso = False
                        End If

                    End If

                    LimpiarControlesGrupo()
                    txtScanner.SelectAll()
                    txtScanner.Focus()
                    Exit Sub

                Else

                    XtraMessageBox.Show(
                       "La cantidad del SKU aún no está completa. No puede confirmar con 'OK' todavía.",
                       "Estado inválido",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information
                   )

                    txtScanner.Text = ""
                    txtScanner.SelectAll()
                    txtScanner.Focus()
                    Exit Sub



                End If

            Case Else

                If Not Sku_en_proceso AndAlso (pBeTransPickingUbicTemp Is Nothing _
                               OrElse pBeTransPickingUbicTemp.IdPickingUbic = 0) Then


                    '#GT: Procesar lectura del SKU (permitido si es el mismo SKU o no hay ninguno en cola) ===
                    If BuscarSKU_Y_Cargar(estado) Then
                        Sku_en_proceso = True
                        txtScanner.Text = ""
                    Else
                        Sku_en_proceso = False
                        txtScanner.Text = ""
                    End If

                Else
                    ' Ya existe un SKU en contexto (cantidad completa), aquí se espera un OK,
                    ' pero se está leyendo nuevamente un SKU (mismo u otro).

                    Dim skuLeido As String = estado


                    If Sku_en_proceso AndAlso
                        pBeTransPickingUbicTemp IsNot Nothing AndAlso
                        pBeTransPickingUbicTemp.IdPickingUbic > 0 Then

                        Dim skuActual As String = Convert.ToString(pBeTransPickingUbicTemp.CodigoSKU)

                        If String.Equals(skuLeido, skuActual, StringComparison.InvariantCultureIgnoreCase) Then
                            ' Mismo SKU: continuar verificando este SKU
                            If BuscarSKU_Y_Cargar(skuLeido) Then
                                ' Sigue pendiente
                                Sku_en_proceso = True
                                txtScanner.Text = ""
                            Else
                                ' Con esta lectura se completó la cantidad:
                                ' ahora se espera OK / Motivo
                                Sku_en_proceso = False
                                txtScanner.Text = ""
                            End If

                            txtScanner.Text = ""
                            txtScanner.SelectAll()
                            txtScanner.Focus()
                            Exit Sub

                        Else
                            ' SKU distinto mientras hay uno en proceso
                            XtraMessageBox.Show(
                                "Hay un SKU en proceso y no se puede leer otro hasta completar la cantidad requerida." & Environment.NewLine &
                                "SKU en proceso: (" & skuActual & ")",
                                "Estado inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            )

                            txtScanner.Text = ""
                            txtScanner.SelectAll()
                            txtScanner.Focus()
                            Exit Sub
                        End If

                        ' ESCENARIO 2: cantidad completa, esperando OK / Motivo
                    ElseIf Not Sku_en_proceso AndAlso
                           pBeTransPickingUbicTemp IsNot Nothing AndAlso
                           pBeTransPickingUbicTemp.IdPickingUbic > 0 Then

                        Dim skuActual As String = Convert.ToString(pBeTransPickingUbicTemp.CodigoSKU)

                        ' ¿Volvieron a leer el mismo SKU que ya está completo?
                        If String.Equals(skuLeido, skuActual, StringComparison.InvariantCultureIgnoreCase) Then

                            XtraMessageBox.Show(
                                "La cantidad de este SKU ya está completa." & Environment.NewLine &
                                "Debe confirmar con 'OK' o registrar un motivo para continuar.",
                                "Estado inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            )

                        Else
                            ' Están intentando leer otro SKU cuando aún hay uno pendiente de OK/motivo
                            XtraMessageBox.Show(
                                "Hay un SKU en cola que requiere un 'OK' o un Motivo para continuar." & Environment.NewLine &
                                "SKU en cola: (" & skuActual & ")",
                                "Estado inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            )

                        End If

                        txtScanner.Text = ""
                        txtScanner.SelectAll()
                        txtScanner.Focus()
                        Exit Sub

                    End If


                End If

        End Select

    End Sub

    Private Function BuscarSKU_Y_Cargar(ByVal sku As String) As Boolean

        BuscarSKU_Y_Cargar = False
        BeLogVeficacion = New clsBeLog_verificacion_bof()

        Try

            If String.IsNullOrWhiteSpace(sku) Then Exit Function

            Dim dt As DataTable = TryCast(dgridListaPedido.DataSource, DataTable)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then Exit Function

            ' Buscar todas las filas que tengan el SKU (ya vienen ordenadas por fecha + IdPedidoEnc desde SQL)
            Dim filas() As DataRow = dt.Select("[SKU] = '" & sku.Replace("'", "''") & "'")

            If filas Is Nothing OrElse filas.Length = 0 Then
                XtraMessageBox.Show("No se encontró el SKU, por favor reintente: " & sku,
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                LimpiarControlesGrupo()
                Exit Function
            End If

            ' Buscar la PRIMERA fila pendiente: Verificada < Pickeada
            Dim rowPendiente As DataRow = Nothing
            Dim pickeada As Integer = 0
            Dim verificada As Integer = 0

            For Each r As DataRow In filas

                pickeada = 0
                verificada = 0

                If dt.Columns.Contains("Pickeada") AndAlso Not IsDBNull(r("Pickeada")) Then
                    pickeada = Convert.ToInt32(r("Pickeada"))
                End If

                If dt.Columns.Contains("Verificada") AndAlso Not IsDBNull(r("Verificada")) Then
                    verificada = Convert.ToInt32(r("Verificada"))
                End If

                ' Solo nos interesan las líneas con cantidad pendiente
                If pickeada > 0 AndAlso verificada < pickeada Then
                    rowPendiente = r
                    Exit For
                End If
            Next

            ' Si no hay ninguna fila pendiente para este SKU, ya se completó en un pedido especifico.
            If rowPendiente Is Nothing Then

                XtraMessageBox.Show(
                    "El SKU ya fue escaneado con la cantidad requerida." & Environment.NewLine &
                                "SKU: " & sku & Environment.NewLine &
                                "Pickeado: " & pickeada & ", Verificado: " & verificada,
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            )

                BuscarSKU_Y_Cargar = False
                Return False
            End If

            Dim row As DataRow = rowPendiente

            ' --- UBICAR LA FILA CORRECTA EN EL GRID ---
            Dim index As Integer = dt.Rows.IndexOf(row)
            Dim handle As Integer = -1

            If index >= 0 Then
                handle = gvListaPedido.GetRowHandle(index)
                If handle >= 0 Then
                    gvListaPedido.FocusedRowHandle = handle
                    gvListaPedido.MakeRowVisible(handle)
                End If
            End If

            ' --- ACTUALIZAR VERIFICADA (una unidad por scan) ---
            pickeada = 0
            verificada = 0

            If dt.Columns.Contains("Pickeada") AndAlso Not IsDBNull(row("Pickeada")) Then
                pickeada = Convert.ToInt32(row("Pickeada"))
            End If

            If dt.Columns.Contains("Verificada") AndAlso Not IsDBNull(row("Verificada")) Then
                verificada = Convert.ToInt32(row("Verificada"))
            End If

            Dim nuevaVerificada As Integer = verificada + 1
            row("Verificada") = nuevaVerificada

            ' NUEVO: calcular estado después de esta lectura
            Dim siguePendiente As Boolean = (pickeada > 0 AndAlso nuevaVerificada < pickeada)
            'Dim lineaCompleta As Boolean = (pickeada > 0 AndAlso nuevaVerificada >= pickeada)

            If handle >= 0 Then
                Dim siguienteHandle As Integer = handle + 1
                If siguienteHandle >= gvListaPedido.RowCount Then
                    siguienteHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle
                End If

                gvListaPedido.ClearSelection()
                gvListaPedido.FocusedRowHandle = siguienteHandle
                gvListaPedido.FocusedColumn = Nothing
            End If

            ' --- CARGAR INPUTS DE LA FILA PENDIENTE SELECCIONADA ---
            txtScanner.Text = sku
            txtDescripcionProducto.Text = CStr(row("NombreProducto"))
            txtTalla.Text = If(dt.Columns.Contains("Talla"), CStr(row("Talla")), "")
            txtColor.Text = If(dt.Columns.Contains("Color"), CStr(row("Color")), "")
            txtCantidad.Text = If(dt.Columns.Contains("Pickeada"), CInt(row("Pickeada")), "")

            '-- Cargar objeto picking_ubicTemp y al leer la barra OK, se asignará a la lista para enviar a verificar
            Dim pIdPickingUbic = If(dt.Columns.Contains("IdPickingUbic"), CInt(row("IdPickingUbic")), 0)
            Dim pLicPlate = If(dt.Columns.Contains("Licencia"), CStr(row("Licencia")), "")
            Dim pIdPedidoEnc = If(dt.Columns.Contains("IdPedidoEnc"), CInt(row("IdPedidoEnc")), 0)
            Dim pIdPedidoDet = If(dt.Columns.Contains("IdPedidoDet"), CInt(row("IdPedidoDet")), 0)
            Dim pSku = If(dt.Columns.Contains("SKU"), CStr(row("SKU")), "")
            Dim pIdProductoTallaColor = If(dt.Columns.Contains("IdProductoTallaColor"), CInt(row("IdProductoTallaColor")), 0)

            '#GT10122025: con los campos obtenidos de la tabla validamos que existan en la lista.
            'pBeTransPickingUbicTemp = New clsBeTrans_picking_ubic()
            pBeTransPickingUbicTemp = BePickingUbicList_Origen.Find(Function(x) x.IdPickingUbic = pIdPickingUbic _
                                                                   AndAlso x.IdPedidoEnc = pIdPedidoEnc _
                                                                   AndAlso x.IdPedidoDet = pIdPedidoDet _
                                                                   AndAlso x.Lic_plate = pLicPlate)

            If pBeTransPickingUbicTemp IsNot Nothing Then

                '#GT11122025: incremento por cada lectura.
                pBeTransPickingUbicTemp.Cantidad_Verificada = nuevaVerificada
                pBeTransPickingUbicTemp.CodigoSKU = pSku.ToString()

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
                BeLogVeficacion.Cantidad = verificada
                BeLogVeficacion.IdProductoTallaColor = pIdProductoTallaColor

                ' Carga de imagen
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                If SplashScreenManager.Default IsNot Nothing Then
                    SplashScreenManager.Default.SetWaitFormCaption("Cargando imagen...")
                End If

                Application.DoEvents()
                CargarImagenProducto(sku)

                ' True  -> después de esta lectura TODAVÍA hay lecturas pendientes (nuevaVerificada < Pickeada)
                ' False -> después de esta lectura la línea quedó completa (nuevaVerificada >= Pickeada)
                BuscarSKU_Y_Cargar = siguePendiente

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
                peProducto.Properties.SizeMode = PictureSizeMode.Zoom
                peProducto.Image = Nothing

                Using fs As New FileStream(archivoEncontrado, FileMode.Open, FileAccess.Read)
                    'peProducto.Image = Image.FromStream(fs)
                    img = Image.FromStream(fs)
                End Using

                '#GT27112025: mejora para redimensionar hasta un 200% como máximo
                Dim imgEscalada = EscalarImagen(img, 2.0)

                Dim factor As Double = 2.5R
                Dim nuevoAncho As Integer = CInt(img.Width * factor)
                Dim nuevoAlto As Integer = CInt(img.Height * factor)
                Dim imgProcesada As Image = RedimensionarAltaCalidad(imgEscalada, nuevoAncho, nuevoAlto)

                peProducto.Properties.SizeMode = PictureSizeMode.Squeeze
                peProducto.Image = imgProcesada


            Else
                peProducto.Image = Nothing
            End If

        Catch ex As Exception
            peProducto.Image = Nothing
        End Try

    End Sub

    Private Function RedimensionarAltaCalidad(
    ByVal imgOriginal As Image,
    ByVal nuevoAncho As Integer,
    ByVal nuevoAlto As Integer) As Image

        Dim bmp As New Bitmap(nuevoAncho, nuevoAlto)

        bmp.SetResolution(imgOriginal.HorizontalResolution, imgOriginal.VerticalResolution)

        Using g As Graphics = Graphics.FromImage(bmp)
            g.CompositingMode = CompositingMode.SourceCopy
            g.CompositingQuality = CompositingQuality.HighQuality
            g.SmoothingMode = SmoothingMode.HighQuality
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = PixelOffsetMode.HighQuality

            Using attrs As New ImageAttributes()
                attrs.SetWrapMode(WrapMode.TileFlipXY)

                g.DrawImage(
                imgOriginal,
                New Rectangle(0, 0, nuevoAncho, nuevoAlto),
                0, 0, imgOriginal.Width, imgOriginal.Height,
                GraphicsUnit.Pixel,
                attrs
            )
            End Using
        End Using

        Return bmp
    End Function

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

            pBeTransPickingUbicTemp = New clsBeTrans_picking_ubic()

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
                            ' Forzar repintado para que RowStyle aplique el color
                            gvListaPedido.RefreshRow(handle)
                        End If
                    End If
                End If
            End If

            LimpiarControlesGrupo()

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

            ' ==========================
            ' 1) COLOR POR CANTIDADES
            ' ==========================
            Dim pickeadaObj As Object = view.GetRowCellValue(e.RowHandle, "Pickeada")
            Dim verificadaObj As Object = view.GetRowCellValue(e.RowHandle, "Verificada")

            Dim cantidadPickeada As Integer = 0
            Dim cantidadVerificada As Integer = 0

            If pickeadaObj IsNot Nothing AndAlso pickeadaObj IsNot DBNull.Value Then
                cantidadPickeada = Convert.ToInt32(pickeadaObj)
            End If

            If verificadaObj IsNot Nothing AndAlso verificadaObj IsNot DBNull.Value Then
                cantidadVerificada = Convert.ToInt32(verificadaObj)
            End If

            If cantidadPickeada > 0 AndAlso cantidadVerificada > 0 Then
                If cantidadVerificada < cantidadPickeada Then
                    ' En proceso: escaneado parcial
                    e.Appearance.BackColor = Color.Yellow
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.Options.UseBackColor = True
                    e.Appearance.Options.UseForeColor = True
                ElseIf cantidadVerificada >= cantidadPickeada Then
                    ' Completo por cantidades
                    e.Appearance.BackColor = Color.LightGreen
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.Options.UseBackColor = True
                    e.Appearance.Options.UseForeColor = True
                End If
            End If

            ' =========================================
            ' 2) OVERRIDE POR LÍNEA CONFIRMADA (OK)
            ' =========================================
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
                ' Si está confirmada, siempre verde (aunque antes estuviera amarillo)
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

    Private Sub cmdEnviar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdGuardar.ItemClick
        Try

            cmdGuardar.Enabled = False
            Guardar_Verificacion()
            cmdGuardar.Enabled = True

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

                '#GT03122025: aqui se guarda en la tabla la razón de la pausa, y se retorna a la lista de pedidos
                XtraMessageBox.Show("Proceso detenido por motivo: " & cmbMotivo.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                BeLogVeficacion.IdEstado = cmbEstado.EditValue
                BeLogVeficacion.IdMotivo = cmbMotivo.EditValue
                clsLnLog_verificacion_bof.Guardar_Log(BeLogVeficacion)

                If InvokeListarPedidos IsNot Nothing Then
                    InvokeListarPedidos.Invoke()
                End If

                Me.DialogResult = DialogResult.OK

            Else

                If plistPickingUbic.Count > 0 Then

                    ' Validar líneas no verificadas (cantidad_verificada = 0), agrupadas por pedido
                    Dim pendientesPorPedido = plistPickingUbic _
                                            .Where(Function(x) x.Cantidad_Verificada = 0) _
                                            .GroupBy(Function(x) x.IdPedidoEnc) _
                                            .ToList()


                    If pendientesPorPedido IsNot Nothing AndAlso pendientesPorPedido.Count > 0 Then

                        Dim sb As New StringBuilder()
                        sb.AppendLine("Existen líneas sin verificar en los siguientes pedidos:")

                        For Each grupo In pendientesPorPedido
                            sb.AppendLine(String.Format(" - Pedido {0}: {1} líneas sin verificar",
                                    grupo.Key,
                                    grupo.Count()))
                        Next

                        sb.AppendLine()
                        sb.AppendLine("¿Desea continuar y guardar solo las líneas verificadas?")

                        Dim r = XtraMessageBox.Show(
                                                    sb.ToString(),
                                                    "Líneas pendientes",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning
                                                )


                        If r = DialogResult.No Then
                            Exit Sub
                        End If

                    End If



                    If Not clsLnTrans_picking_enc.Guardar_Verificacion_Bof(plistPickingUbic,
                                                                          AP.UsuarioAp.IdUsuario,
                                                                          pBeListaPedidos) Then

                        XtraMessageBox.Show("No se fiscalizó el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        XtraMessageBox.Show("Pedido fiscalizado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        If InvokeListarPedidos IsNot Nothing Then
                            InvokeListarPedidos.Invoke()
                        End If

                        Me.DialogResult = DialogResult.OK

                    End If

                Else
                    XtraMessageBox.Show("Primero fiscalice las lineas del pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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