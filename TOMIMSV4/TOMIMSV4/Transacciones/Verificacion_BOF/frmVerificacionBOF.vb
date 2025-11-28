Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq.Expressions
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen

Public Class frmVerificacionBOF
    Public pBePedidoEnc As New clsBeTrans_pe_enc
    Public Delegate Sub ListarPedidos()
    Public Property InvokeListarPedidos As ListarPedidos
    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public Delegate Sub Cargar_Datos_Pedido()
    ReadOnly Property InvokeCargarPedido As Cargar_Datos_Pedido()
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


    Private Sub frmVerificacionBOF_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            vRutaCDN = clsLnBodega.GetRutaCDN_By_Idbodega(AP.IdBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If String.IsNullOrEmpty(vRutaCDN) Then
                XtraMessageBox.Show("No esta definida la ruta hacia la galeria de imagenes")
            End If


            'Dim archivosPng() As String = Directory.GetFiles(vRutaCDN, "*.png")
            '_listaRutasPng = archivosPng.ToList()

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pBePedidoEnc.IdBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando datos...")

            If pBePedidoEnc Is Nothing Then Exit Sub

            Cargar_Datos(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            AplicarEstiloScanner()

            txtScanner.SelectAll()
            txtScanner.Focus()

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Cargar_Datos(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            If Not pBePedidoEnc Is Nothing Then

                pBePedidoEnc.IsNew = False

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

        ' Define SOLO las columnas que usas en SetRowCellValue
        dt.Columns.Add("SKU", GetType(String))
        dt.Columns.Add("No_Linea", GetType(Integer))
        'dt.Columns.Add("colIdProducto", GetType(Integer))
        'dt.Columns.Add("colIsNew", GetType(Boolean))
        dt.Columns.Add("CodigoProducto", GetType(String))
        dt.Columns.Add("NombreProducto", GetType(String))
        dt.Columns.Add("Talla", GetType(String))
        dt.Columns.Add("Color", GetType(String))
        dt.Columns.Add("UmBas", GetType(String))
        'dt.Columns.Add("Atributo_Variante_1", GetType(String))
        'dt.Columns.Add("colIdProductoBodega", GetType(Integer))
        'dt.Columns.Add("colCantidadExistencia", GetType(Double))
        'dt.Columns.Add("colPesoExistencia", GetType(Double))
        'dt.Columns.Add("colPesoUnitario", GetType(Double))
        dt.Columns.Add("CantidadPickeada", GetType(Double))
        dt.Columns.Add("CantidadVerificada", GetType(Double))
        'dt.Columns.Add("colEstadoColor", GetType(String))
        'dt.Columns.Add("colCantidad", GetType(Double))
        'dt.Columns.Add("colPeso", GetType(Double))
        'dt.Columns.Add("colPrecio", GetType(Double))
        'dt.Columns.Add("colTotal", GetType(Double))
        dt.Columns.Add("IdPedidoEnc", GetType(Integer))
        dt.Columns.Add("IdPedidoDet", GetType(Integer))
        'dt.Columns.Add("colNoDias", GetType(Integer))
        'dt.Columns.Add("ColFechaEspecifica", GetType(Boolean))
        dt.Columns.Add("IdStockEspecifico", GetType(Integer))
        dt.Columns.Add("Licencia", GetType(String))
        dt.Columns.Add("IdPickingUbic", GetType(Integer))

        ' Asigna el DataSource UNA sola vez
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

            'ltrans.Begin_Transaction()

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
                        'gvListaPedido.SetRowCellValue(i, "colIdProducto", pDet.Producto.IdProducto)
                        'gvListaPedido.SetRowCellValue(i, "colIsNew", pDet.IsNew)
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

                            End If

                            'gvListaPedido.Columns("colTalla").OptionsColumn.ReadOnly = True
                            'gvListaPedido.Columns("colColor").OptionsColumn.ReadOnly = True

                            '#GT21082025: a futuro si cliente lo requiere se muestran
                            'gvListaPedido.Columns("colIdProductoTallaColor").Visible = False
                            'gvListaPedido.Columns("colSKU").Visible = False

                        End If

                        gvListaPedido.SetRowCellValue(i, "UmBas", pDet.Nom_unid_med)

                        '#GT OMITIR4 ATRIBURTO VARIANTE
                        '#EJC20180114: Agregu? No_Linea y Atributo_Variante_1 en Cargar_Detalle_Pedido
                        'gvListaPedido.SetRowCellValue(i, "ColNo_Linea", pDet.No_linea)
                        'gvListaPedido.SetRowCellValue(i, "Atributo_Variante_1", pDet.Atributo_Variante_1)


                        '#GT OMITIR EN EL GRID
                        '#EJC20180606: Para reservar stock a posteriori.
                        'gvListaPedido.SetRowCellValue(i, "colIdProductoBodega", pDet.ProductoBodega.IdProductoBodega)

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
                                    'gvListaPedido.SetRowCellValue(i, "colPesoExistencia", pBeStock.Peso)

                                Else

                                    'DgComboPresentacion = TryCast(dgrid.Rows(i).Cells("colPresentacion"), DataGridViewComboBoxCell)
                                    'DgComboPresentacion.Value = Nothing

                                    gvListaPedido.SetRowCellValue(i, "CantidadExistencia", pBeStock.Cantidad)
                                    'gvListaPedido.SetRowCellValue(i, "colPesoExistencia", pBeStock.Peso)

                                End If

                            Else

                                gvListaPedido.SetRowCellValue(i, "CantidadExistencia", pBeStock.Cantidad)
                                'gvListaPedido.SetRowCellValue(i, "colPesoExistencia", pBeStock.Peso)

                            End If

                            'If pBeStock.Cantidad > 0 Then
                            '    gvListaPedido.SetRowCellValue(i, "colPesoUnitario", pBeStock.Peso / pBeStock.Cantidad)
                            'ElseIf pBeStock.Peso > 0 Then
                            '    gvListaPedido.SetRowCellValue(i, "colPesoUnitario", pBeStock.Peso)
                            'Else
                            '    gvListaPedido.SetRowCellValue(i, "colPesoUnitario", 0)
                            'End If

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

                                ' En DevExpress el color de fila se maneja por RowStyle.
                                ' Guardamos un "estado" en una columna (por ejemplo colEstadoColor).
                                'Select Case vDif

                                '    Case pDet.Cantidad
                                '        'No se ha pickeado nada.
                                '        gvListaPedido.SetRowCellValue(i, "colEstadoColor", "WHITE")

                                '    Case Is > 0
                                '        'Falta pickear producto 
                                '        gvListaPedido.SetRowCellValue(i, "colEstadoColor", "MISTYROSE")

                                '    Case Is < 0
                                '        'Sobra producto en el picking, esto no debería pasar nunca.
                                '        gvListaPedido.SetRowCellValue(i, "colEstadoColor", "LIGHTYELLOW")

                                '    Case 0
                                '        'Se pickeó completa la cantidad solicitada en el pedido.
                                '        gvListaPedido.SetRowCellValue(i, "colEstadoColor", "LIGHTGREEN")

                                '    Case Else
                                '        Exit Select

                                'End Select

                            End If

                        End If

                        'gvListaPedido.SetRowCellValue(i, "colCantidad", pDet.Cantidad)
                        'If Modo = TipoTrans.Editar Then
                        '    gvListaPedido.SetRowCellValue(i, "colCantidadExistencia",
                        '    pDet.Cantidad + CDbl(gvListaPedido.GetRowCellValue(i, "colCantidadExistencia")))
                        'End If
                        'gvListaPedido.SetRowCellValue(i, "colPeso", pDet.Peso)
                        'gvListaPedido.SetRowCellValue(i, "colPrecio", pDet.Precio)
                        'gvListaPedido.SetRowCellValue(i, "colTotal", pDet.RoadTotal)
                        gvListaPedido.SetRowCellValue(i, "IdPedidoEnc", pDet.IdPedidoEnc)
                        gvListaPedido.SetRowCellValue(i, "IdPedidoDet", pDet.IdPedidoDet)
                        'gvListaPedido.SetRowCellValue(i, "colNoDias", pDet.Ndias)
                        'gvListaPedido.SetRowCellValue(i, "ColFechaEspecifica", pDet.Fecha_especifica)

                        If pDet.IdStockEspecifico > 0 Then
                            gvListaPedido.SetRowCellValue(i, "IdStockEspecifico", pDet.IdStockEspecifico)
                        End If

                        gvListaPedido.UpdateCurrentRow()

                    Else
                        If pDet.EsPadre Then

                            gvListaPedido.AddNewRow()
                            i = gvListaPedido.FocusedRowHandle

                            gvListaPedido.SetRowCellValue(i, "No_Linea", pDet.No_linea)
                            'gvListaPedido.SetRowCellValue(i, "colIdProducto", pDet.Producto.IdProducto)
                            'gvListaPedido.SetRowCellValue(i, "colIsNew", pDet.IsNew)
                            gvListaPedido.SetRowCellValue(i, "CodigoProducto", pDet.Codigo_Producto)
                            gvListaPedido.SetRowCellValue(i, "NombreProducto", pDet.Nombre_producto)

                            IndicePadre = i
                            vCodigoPadre = pDet.Codigo_Producto

                            'gvListaPedido.SetRowCellValue(i, "colCantidad", pDet.Cantidad)
                            'If Modo = TipoTrans.Editar Then
                            '    gvListaPedido.SetRowCellValue(i, "colCantidadExistencia",
                            '    pDet.Cantidad + CDbl(gvListaPedido.GetRowCellValue(i, "colCantidadExistencia")))
                            'End If
                            'gvListaPedido.SetRowCellValue(i, "colPeso", pDet.Peso)
                            'gvListaPedido.SetRowCellValue(i, "colPrecio", pDet.Precio)
                            'gvListaPedido.SetRowCellValue(i, "colTotal", pDet.RoadTotal)
                            gvListaPedido.SetRowCellValue(i, "IdPedidoDet", pDet.IdPedidoDet)



                            '#GT OMITIR COLUMNA EN GRID
                            'gvListaPedido.SetRowCellValue(i, "colNoDias", pDet.Ndias)
                            'gvListaPedido.SetRowCellValue(i, "ColFechaEspecifica", pDet.Fecha_especifica)

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
        If sku <> "" Then
            If BuscarSKU_Y_Cargar(sku) Then
                AplicarEstiloScanner()
                txtEstado.SelectAll()
                txtEstado.Focus()
            End If
        Else
            txtScanner.SelectAll()
            txtScanner.Focus()
        End If

    End Sub

    Private Function BuscarSKU_Y_Cargar(ByVal sku As String) As Boolean

        BuscarSKU_Y_Cargar = False

        Try

            If String.IsNullOrWhiteSpace(sku) Then Exit Function

            Dim dt As DataTable = TryCast(dgridListaPedido.DataSource, DataTable)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then Exit Function

            ' Buscar en DataTable
            Dim filas() As DataRow = dt.Select("[SKU] = '" & sku.Replace("'", "''") & "'")

            If filas.Length = 0 Then
                XtraMessageBox.Show("No se encontró el SKU: " & sku, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

            pBeTransPickingUbicTemp = New clsBeTrans_picking_ubic()
            pBeTransPickingUbicTemp = BePickingUbicList.Find(Function(x) x.IdPickingUbic = pIdPickingUbic _
                                                                           AndAlso x.IdPedidoEnc = pIdPedidoEnc _
                                                                           AndAlso x.IdPedidoDet = pIdPedidoDet _
                                                                           AndAlso x.Lic_plate = pLicPlate)

            '---cuando se lea la barra de OK, será true para poder enviar la lista final al WS
            pBeTransPickingUbicTemp.IsNew = False

            ' --- Aviso de cargar imagen si por alguna razón existiera Lag ---
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            If SplashScreenManager.Default IsNot Nothing Then
                SplashScreenManager.Default.SetWaitFormCaption("Cargando imagen...")
            End If

            Application.DoEvents()

            CargarImagenProducto(sku)

            BuscarSKU_Y_Cargar = True

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

    Private Sub txtEstado_KeyDown(sender As Object, e As KeyEventArgs) Handles txtEstado.KeyDown
        Try

            If e.KeyCode <> Keys.Enter Then Return

            Dim valorLeido As String = txtEstado.Text.Trim()
            txtEstado.Clear()

            ' Normalizamos para comparar (ignorando mayúsculas/minúsculas y espacios)
            Dim estado As String = valorLeido.ToUpperInvariant()

            Select Case estado
                Case "OK"
                    ' Producto confirmado correctamente
                    ProcesarEstadoOK()

                Case "PAUSA"
                    ' Poner en pausa: bloquear controles para impedir cerrar o escanear otro producto
                    'ProcesarEstadoPausa()

                Case Else
                    ' Cualquier otra cosa se considera no válida
                    MessageBox.Show(
                        $"Estado no reconocido: [{valorLeido}]. Escanee un código 'OK' o 'Pausa'.",
                        "Estado inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )

                    ' Vuelve a esperar un estado correcto
                    txtEstado.Focus()
            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
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

    Private Function ProcesarEstadoOK() As Boolean
        Try

            '#GT27112025: cada vez que se escanea un SKU, se llena el objeto pBeTransPickingUbicTemp, y al leer la barra OK, se asigna a la lista con IsNew=true
            '#GT27112025: al terminar de escanear todos los productos, enviar la lista a verificar y cerrar la tarea
            pBeTransPickingUbicTemp.IsNew = True
            plistPickingUbic.Add(pBeTransPickingUbicTemp)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Sub cmdEnviar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdEnviar.ItemClick
        Try

            cmdEnviar.Enabled = False
            Guardar_Verificacion()
            cmdEnviar.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guardar_Verificacion()
        Try

            Dim ListaVerificada As Boolean = False

            If plistPickingUbic.Count > 0 Then

                For Each BePickingUbic As clsBeTrans_picking_ubic In plistPickingUbic

                    '#GT28112025: parece redundante, pero la verificación recibe una lista de un registro
                    Dim tmpListaPickingUbic = New List(Of clsBeTrans_picking_ubic)
                    tmpListaPickingUbic.Add(BePickingUbic)

                    If clsLnTrans_picking_ubic.Actualiza_Cant_Peso_Verificacion(tmpListaPickingUbic,
                                                                              AP.UsuarioAp.IdUsuario,
                                                                              BePickingUbic.Cantidad_Recibida,
                                                                              BePickingUbic.Peso_recibido,
                                                                              0,
                                                                              pBePedidoEnc.IdPedidoEnc) Then

                        ListaVerificada = True
                    Else
                        ListaVerificada = False
                    End If

                Next

                'clsLnTrans_picking_enc.Actualizar_PickingEnc_Verificado(pBePedidoEnc.Picking)
                'clsLnTrans_pe_enc.Actualizar_Estado_Verificado(pBePedidoEnc)

                '#GT28112025: si la lista se itero completa no será false de lo contrario, algun registro lanzo excepción
                If ListaVerificada Then clsLnTrans_picking_enc.Actualizar_PickingEnc_Verificado(pBePedidoEnc.Picking)

            Else
                    XtraMessageBox.Show("No se ha fiscalizado ningun producto.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception

        End Try

    End Sub

    'Private Sub Llena_Presentacion_Grid(ByVal pIndex As Integer,
    '                                    ByVal lConnection As SqlConnection,
    '                                    ByVal lTransaction As SqlTransaction,
    '                                    Optional ByVal pIdPresentacion As Integer = 0)

    '    Try

    '        DgComboPresentacion = TryCast(dgrid.Rows(pIndex).Cells("colPresentacion"), DataGridViewComboBoxCell)
    '        DgComboPresentacion.DropDownWidth = 200

    '        Dim lPres As New List(Of clsBeProducto_Presentacion)

    '        lPres = New List(Of clsBeProducto_Presentacion)

    '        If Modo = TipoTrans.Nuevo Then
    '            lPres = clsLnProducto_presentacion.Get_All_Presentacion_By_IdProductoBodega(pBeProducto.IdProductoBodega, lConnection, lTransaction).ToList()
    '        Else
    '            lPres = clsLnProducto_presentacion.Get_All_Presentaciones_By_IdProductoBodega(pBeProducto.IdProductoBodega, True, lConnection, lTransaction).ToList()
    '        End If

    '        DgComboPresentacion.DataSource = lPres
    '        DgComboPresentacion.ValueMember = "IdPresentacion"
    '        DgComboPresentacion.DisplayMember = "Nombre"

    '        If DgComboPresentacion.Items.Count > 0 AndAlso (Modo = TipoTrans.Nuevo Or Modo = TipoTrans.Editar) Then
    '            Dim vIdPresentacion As Integer = lPres(0).IdPresentacion
    '            DgComboPresentacion.Value = vIdPresentacion
    '            If Not DgComboPresentacion.Value = lPres(0).IdPresentacion Then
    '                DgComboPresentacion.Value = Nothing
    '            End If
    '            Exit Sub
    '        Else
    '            DgComboPresentacion.Value = Nothing
    '        End If

    '        If pIdPresentacion <> 0 AndAlso DgComboPresentacion.Items.Count > 0 Then
    '            DgComboPresentacion.Value = pIdPresentacion
    '        Else
    '            DgComboPresentacion.Value = Nothing
    '        End If

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub


    'Private Sub Cargar_Detalle_Pedido(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

    '    Try

    '        'gvListaPedido.Rows.Clear()
    '        dgridListaPedido.DataSource = Nothing

    '        Dim i As Integer = -1
    '        Dim vCantidadPickeada As Double = 0
    '        Dim vCantidadVerificada As Double = 0
    '        Dim IndicePadre As Integer = -1
    '        Dim vCodigoPadre As String = ""
    '        Dim vClienteTiempo As New clsBeCliente_tiempos

    '        If Not pClienteTiemposList Is Nothing Then
    '            vClienteTiempo = pClienteTiemposList.Find(Function(x) _
    '                            x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
    '                            And x.IdFamilia = pBeProducto.Familia.IdFamilia)
    '        End If

    '        Dim vDiasVencimientoCliente As Integer = 0

    '        If Not vClienteTiempo Is Nothing Then
    '            If chkPedidoLocal.Checked Then
    '                vDiasVencimientoCliente = vClienteTiempo.Dias_Local
    '            Else
    '                vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
    '            End If
    '        End If

    '        'ltrans.Begin_Transaction()

    '        Cliente_Detalle_Ultimo_Lote = 0
    '        Cliente_Detalle_Control_Calidad = 0


    '        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

    '        Application.DoEvents()

    '        If Not pBePedidoEnc Is Nothing Then

    '            For Each pDet As clsBeTrans_pe_det In pBePedidoEnc.Detalle.OrderBy(Function(x) x.No_linea)

    '                pBeStock = New clsBeStock
    '                pBeProducto = New clsBeProducto
    '                pBeProducto.IdProducto = pDet.Producto.IdProducto

    '                If SplashScreenManager.Default Is Nothing Then
    '                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
    '                    SplashScreenManager.Default.SetWaitFormDescription("Obteniendo código: " & pDet.Codigo_Producto)
    '                End If

    '                If Not pDet.EsPadre AndAlso Not pDet.IdPedidoDetPadre > 0 Then

    '                    i = dgrid.Rows.Add(pDet.No_linea,
    '                                       pDet.Producto.IdProducto,
    '                                       pDet.IsNew,
    '                                       pDet.Codigo_Producto,
    '                                       pDet.Nombre_producto)

    '                    pBeProducto.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBeProducto.IdProducto,
    '                                                                                                                        cmbBodega.EditValue,
    '                                                                                                                        lConnection,
    '                                                                                                                        lTransaction)


    '                    If BeBodega.Control_Talla_Color Then

    '                        Dim BeProductoTc = clsLnProducto_talla_color.GetSingle(pDet.IdProductoTallaColor, lConnection, lTransaction)

    '                        If BeProductoTc IsNot Nothing Then
    '                            dgrid.Rows(i).Cells("colTalla").Value = BeProductoTc.IdTalla
    '                            dgrid.Rows(i).Cells("colColor").Value = BeProductoTc.IdColor
    '                        End If

    '                        dgrid.Columns("colTalla").ReadOnly = True
    '                        dgrid.Columns("colColor").ReadOnly = True

    '                        '#GT21082025: a futuro si cliente lo requiere se muestran
    '                        dgrid.Columns("colIdProductoTallaColor").Visible = False
    '                        'dgrid.Columns("colSKU").Visible = False

    '                    End If

    '                    Llena_Presentacion_Grid(i, lConnection, lTransaction, pDet.IdPresentacion)

    '                    If BeTipoDoc.Generar_pedido_ingreso_bodega_destino Then
    '                        Llena_Cliente_Grid(i, lConnection, lTransaction, pDet.IdCliente)
    '                    End If

    '                    If pDet.IdCliente <> 0 Then

    '                        If Cliente_Control_Calidad(pDet.IdCliente, lConnection, lTransaction) Then
    '                            Cliente_Detalle_Control_Calidad += 1
    '                        End If

    '                        If Cliente_Control_Ultimo_Lote(pDet.IdCliente, lConnection, lTransaction) Then
    '                            Cliente_Detalle_Ultimo_Lote += 1
    '                        End If

    '                    End If

    '                    dgrid.Rows(i).Cells("colUnidadMedida").Value = pDet.Nom_unid_med

    '                    '#EJC20180614: Se agregó validación para que cuando no haya existencia, no se trate de desplegar el estado
    '                    Llena_Estados_Grid(i,
    '                                       lConnection,
    '                                       lTransaction,
    '                                       pDet.IdEstado)

    '                    dgrid.Rows(i).Cells("colUnidadMedida").Value = pDet.Nom_unid_med

    '                    '#EJC20180114: Agregu? No_Linea y Atributo_Variante_1 en Cargar_Detalle_Pedido
    '                    dgrid.Rows(i).Cells("ColNo_Linea").Value = pDet.No_linea
    '                    dgrid.Rows(i).Cells("Atributo_Variante_1").Value = pDet.Atributo_Variante_1

    '                    '#EJC20180606: Para reservar stock a posteriori.
    '                    dgrid.Rows(i).Cells("colIdProductoBodega").Value = pDet.ProductoBodega.IdProductoBodega

    '                    pBeStock.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega
    '                    pBeStock.ProductoEstado.IdEstado = pDet.IdEstado
    '                    pBeStock.Presentacion.IdPresentacion = pDet.IdPresentacion
    '                    pBeStock.IdPresentacion = pDet.IdPresentacion
    '                    pBeStock.IdBodega = cmbBodega.EditValue

    '                    ''#EJC20171025_0217PM: Si no se manda la unidad de medida no devuelve el stock disponible en el pedido.
    '                    pBeStock.IdUnidadMedida = pDet.IdUnidadMedidaBasica

    '                    If pBeStock.IdProductoBodega <> 0 AndAlso pBeStock.ProductoEstado.IdEstado <> 0 Then

    '                        pBeStock.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega
    '                        pDet.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega

    '                        '#EJC20220720_1357:Abastecer desde ubicación específica de cliente.
    '                        pBeStock.IdUbicacion = Val(txtIdUbicacionAbastecimiento.Text)

    '                        'Obtiene la cantidad disponible restando la cantidad reservada.
    '                        clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStock,
    '                                                                     cmbBodega.EditValue,
    '                                                                     True,
    '                                                                     False,
    '                                                                     vDiasVencimientoCliente,
    '                                                                     True,
    '                                                                     lConnection,
    '                                                                     lTransaction)

    '                        pDet.CantidadReservada = clsLnStock.Get_Cantidad_Reservada_By_IdPedidoDet(pBeStock,
    '                                                                                                  pDet.IdPedidoDet,
    '                                                                                                  lConnection,
    '                                                                                                  lTransaction,
    '                                                                                                  True)
    '                        'GT 270720210843: para un pedido, si se edita, es porque ya se guardo, y no se debe sumar lo reservado más la existencia
    '                        If Modo = TipoTrans.Editar Then

    '                        Else
    '                            '#EJC20171021_1108AM: Obtiene la cantidad reservada por detalle de pedido para considerarla como disponible.
    '                            pBeStock.Cantidad += pDet.CantidadReservada
    '                        End If

    '                        pDet.PesoReservado = clsLnStock.Get_Peso_Reservado(pBeStock,
    '                                                                           pDet.IdPedidoDet,
    '                                                                           lConnection,
    '                                                                           lTransaction,
    '                                                                           True)

    '                        '#EJC20171021_1108AM: Obtiene el peso reservado por detalle de pedido para considerarlo como disponible.
    '                        pBeStock.Peso += pDet.PesoReservado

    '                        ''#EJC20171025_0221PM: Desplegar cantidad disponible en base a presentación cuando se edita un pedido.
    '                        If Not pBeStock.Presentacion Is Nothing Then

    '                            If pBeStock.Presentacion.IdPresentacion <> 0 Then

    '                                dgrid.Rows(i).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
    '                                dgrid.Rows(i).Cells("colPesoExistencia").Value = pBeStock.Peso

    '                            Else

    '                                DgComboPresentacion = TryCast(dgrid.Rows(i).Cells("colPresentacion"), DataGridViewComboBoxCell)
    '                                DgComboPresentacion.Value = Nothing

    '                                dgrid.Rows(i).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
    '                                dgrid.Rows(i).Cells("colPesoExistencia").Value = pBeStock.Peso

    '                            End If

    '                        Else

    '                            dgrid.Rows(i).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
    '                            dgrid.Rows(i).Cells("colPesoExistencia").Value = pBeStock.Peso

    '                        End If

    '                        If pBeStock.Cantidad > 0 Then
    '                            dgrid.Rows(i).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
    '                        ElseIf pBeStock.Peso > 0 Then
    '                            dgrid.Rows(i).Cells("colPesoUnitario").Value = pBeStock.Peso
    '                        Else
    '                            dgrid.Rows(i).Cells("colPesoUnitario").Value = 0
    '                        End If

    '                        '#EJC20171021_0527PM: Obtener la cantidad pickeada.
    '                        If Not pDet.ListaPickingUbic Is Nothing Then

    '                            Try

    '                                Dim vCantidadRecUMBas As Double = 0
    '                                Dim vCantidadVerUMBas As Double = 0
    '                                vCantidadPickeada = 0
    '                                vCantidadVerificada = 0

    '                                If pDet.IdPresentacion > 0 Then
    '                                    '#CM_20191128: Esta fue la única manera que encontré para sumar bien la cantidad del pedido cuando se tiene reservada 
    '                                    'X cantidad del pedido en presentación y la otra parte en UMBas.
    '                                    vCantidadRecUMBas = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Recibida)
    '                                    vCantidadVerUMBas = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Verificada)
    '                                End If

    '                                If vCantidadRecUMBas > 0 OrElse vCantidadVerUMBas > 0 Then
    '                                    '#CM_20191128: Busco de primero la cantidad total con presentación

    '                                    vCantidadPickeada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Recibida)
    '                                    vCantidadVerificada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Verificada)

    '                                    '#CM_20191128: Busco el factor de la presentación
    '                                    pDet.Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pDet.IdProductoBodega, pDet.IdPresentacion, lConnection, lTransaction)

    '                                    '#CM_20191128: Divido la cantidad UMBas entre el factor
    '                                    vCantidadRecUMBas = Math.Round(vCantidadRecUMBas / pDet.Factor, 6)
    '                                    vCantidadVerUMBas = Math.Round(vCantidadVerUMBas / pDet.Factor, 6)

    '                                    '#CM_20191128: Sumo las cantidades.
    '                                    vCantidadPickeada = Math.Round(vCantidadPickeada + vCantidadRecUMBas, 6)
    '                                    vCantidadVerificada = Math.Round(vCantidadVerificada + vCantidadVerUMBas, 6)
    '                                Else

    '                                    '#CKFK20221211 Modifiqué la forma de obtener lo pickeado y lo verificado
    '                                    'vCantidadPickeada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Recibida)
    '                                    'vCantidadVerificada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Verificada)

    '                                    If pDet.IdPresentacion = 0 Then

    '                                        Dim vFactor As Integer = 0

    '                                        For Each ubic In pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet).ToList
    '                                            If ubic.IdPresentacion <> 0 Then

    '                                                vFactor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pDet.IdProductoBodega, ubic.IdPresentacion, lConnection, lTransaction)

    '                                                vCantidadPickeada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = ubic.IdPresentacion).Sum(Function(y) y.Cantidad_Recibida) * vFactor
    '                                                vCantidadVerificada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = ubic.IdPresentacion).Sum(Function(y) y.Cantidad_Verificada) * vFactor

    '                                            Else

    '                                                vCantidadPickeada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Recibida)
    '                                                vCantidadVerificada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Verificada)

    '                                            End If
    '                                        Next

    '                                    Else

    '                                        vCantidadPickeada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Recibida)
    '                                        vCantidadVerificada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Verificada)

    '                                    End If

    '                                End If


    '                            Catch ex As Exception
    '                                '#EJC201710210531PM: No se pudo obtener la cantidad pickeada de la lista, podr?a pasar pero aun no se porqu? ;) 
    '                            End Try

    '                            dgrid.Rows(i).Cells("CantidadPickeada").Value = vCantidadPickeada
    '                            dgrid.Rows(i).Cells("CantidadVerificada").Value = vCantidadVerificada

    '                            Dim vDif As Double = (pDet.Cantidad - Math.Round(vCantidadPickeada, 6))
    '                            '#EJC20171021_0534OM: Formateo condicional de color, cantidad pedido vrs. cantidad_picking
    '                            Select Case vDif

    '                                Case pDet.Cantidad

    '                                    'No se ha pickeado nada.
    '                                    dgrid.Rows(i).DefaultCellStyle.BackColor = Color.White

    '                                Case Is > 0

    '                                    'Falta pickear producto 
    '                                    dgrid.Rows(i).DefaultCellStyle.BackColor = Color.MistyRose

    '                                Case Is < 0

    '                                    'Sobra producto en el picking, esto no debería pasar nunca.
    '                                    dgrid.Rows(i).DefaultCellStyle.BackColor = Color.LightYellow

    '                                Case 0

    '                                    'Se pickeó completa la cantidad solicitada en el pedido.
    '                                    dgrid.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

    '                                Case Else
    '                                    Exit Select

    '                            End Select

    '                        End If

    '                    End If

    '                    dgrid.Rows(i).Cells("colCantidad").Value = pDet.Cantidad
    '                    If Modo = TipoTrans.Editar Then
    '                        dgrid.Rows(i).Cells("colCantidadExistencia").Value = pDet.Cantidad + dgrid.Rows(i).Cells("colCantidadExistencia").Value
    '                    End If
    '                    dgrid.Rows(i).Cells("colPeso").Value = pDet.Peso
    '                    dgrid.Rows(i).Cells("colPrecio").Value = pDet.Precio
    '                    dgrid.Rows(i).Cells("colTotal").Value = pDet.RoadTotal
    '                    dgrid.Rows(i).Cells("colIdPedidoDet").Value = pDet.IdPedidoDet
    '                    dgrid.Rows(i).Cells("colNoDias").Value = pDet.Ndias
    '                    dgrid.Rows(i).Cells("ColFechaEspecifica").Value = pDet.Fecha_especifica

    '                    If pDet.IdStockEspecifico > 0 Then
    '                        dgrid.Rows(i).Cells("IdStockEspecifico").Value = pDet.IdStockEspecifico
    '                    End If

    '                Else
    '                    If pDet.EsPadre Then

    '                        i = dgrid.Rows.Add(pDet.No_linea,
    '                                           pDet.Producto.IdProducto,
    '                                           pDet.IsNew,
    '                                           pDet.Codigo_Producto,
    '                                           pDet.Nombre_producto)

    '                        IndicePadre = i
    '                        vCodigoPadre = pDet.Codigo_Producto

    '                        Set_Producto_Padre_Kit(pBeProducto,
    '                                               i,
    '                                               lConnection,
    '                                               lTransaction)

    '                        dgrid.Rows(i).Cells("colCantidad").Value = pDet.Cantidad
    '                        If Modo = TipoTrans.Editar Then
    '                            dgrid.Rows(i).Cells("colCantidadExistencia").Value = pDet.Cantidad + dgrid.Rows(i).Cells("colCantidadExistencia").Value
    '                        End If
    '                        dgrid.Rows(i).Cells("colPeso").Value = pDet.Peso
    '                        dgrid.Rows(i).Cells("colPrecio").Value = pDet.Precio
    '                        dgrid.Rows(i).Cells("colTotal").Value = pDet.RoadTotal
    '                        dgrid.Rows(i).Cells("colIdPedidoDet").Value = pDet.IdPedidoDet
    '                        dgrid.Rows(i).Cells("colNoDias").Value = pDet.Ndias
    '                        dgrid.Rows(i).Cells("ColFechaEspecifica").Value = pDet.Fecha_especifica

    '                        If pDet.IdStockEspecifico > 0 Then
    '                            dgrid.Rows(i).Cells("IdStockEspecifico").Value = pDet.IdStockEspecifico
    '                        End If

    '                    End If

    '                    pDet.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega

    '                    If pDet.IdPedidoDetPadre <> 0 Then

    '                        pBeStock.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega

    '                        Set_Productos_Hijos_Kit(pBeProducto,
    '                                                IndicePadre,
    '                                                vCodigoPadre)

    '                    End If

    '                End If

    '                i += 1

    '                pBePedidoDetList.Add(pDet)

    '                Application.DoEvents()

    '            Next

    '        End If

    '        txtControlUltimoLote.Text = IIf(Cliente_Detalle_Ultimo_Lote > 0, "Si", "No")

    '        If txtControlUltimoLote.Text = "Si" Then
    '            txtControlUltimoLote.BackColor = Color.PaleGreen
    '        Else
    '            txtControlUltimoLote.BackColor = Color.Firebrick
    '        End If

    '        txtCertificadoCalidad.Text = IIf(Cliente_Detalle_Control_Calidad > 0, "Si", "No")

    '        If txtCertificadoCalidad.Text = "Si" Then
    '            txtCertificadoCalidad.BackColor = Color.PaleGreen
    '        Else
    '            txtCertificadoCalidad.BackColor = Color.Firebrick
    '        End If

    '    Catch ex As Exception

    '        SplashScreenManager.CloseForm(False)

    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '                            Text,
    '                            MessageBoxButtons.OK,
    '                            MessageBoxIcon.Exclamation)

    '    Finally
    '        SplashScreenManager.CloseForm(False)
    '    End Try

    'End Sub

End Class