Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS.WSOrdenesProduccion

Public Class clsSyncNavOrdenProduccion : Inherits clsInterfaceBase
    Implements IDisposable

    Property pBodega As String = ""

    Private fichaOrdenProduccion() As OP_Lanzadas
    Dim VContadorBitacoraTomims As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Private wsOrdenProduccionService As New OP_Lanzadas_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing
    Private disposedValue As Boolean

    Public Function Importar_Ordenes_Produccion_Desde_WSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                              ByRef prg As System.Windows.Forms.ProgressBar,
                                                                              ByRef cnnLog As SqlConnection) As Boolean

        Importar_Ordenes_Produccion_Desde_WSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Consultando registros en ERP..")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim lPedidosCompra As New List(Of OP_Lanzadas)

            lPedidosCompra = Get_Ordenes_Produccion_FromWS(True)

            BeNavEjecucionRes.Registros_ws = fichaOrdenProduccion.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeI_nav_OrdenProduccion As clsBeI_nav_ped_compra_enc
            Dim BeI_nav_OrdenProduccionDet As clsBeI_nav_ped_compra_det
            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim vCantidadEntera As Integer = 0
            Dim vCantidadDecimal As Double = 0
            Dim vCantidadConversionCajas As Double = 0

            lblprg.AppendText(String.Format("Órdenes de producción en WS: {0} ", fichaOrdenProduccion.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()
            lblprg.Refresh()

            prg.Maximum = lPedidosCompra.Count

            Dim vContador As Integer = 0
            Dim vNoLInea As Integer = 1

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Borrar tablas intermedias.
            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                For Each PC As OP_Lanzadas In lPedidosCompra

                    BeI_nav_OrdenProduccion = New clsBeI_nav_ped_compra_enc

                    CopyObject(PC, BeI_nav_OrdenProduccion)

                    '#EJC20210201: Proveedor por defecto.
                    If PC.Bin_Code Is Nothing Then
                        If Not PC.ProdOrderLines Is Nothing Then
                            If PC.ProdOrderLines.Length > 0 Then
                                PC.Bin_Code = PC.ProdOrderLines.FirstOrDefault.Bin_Code()
                            Else
                                PC.Bin_Code = "PROD"
                            End If
                        End If
                    End If

                    'Proveedor
                    BeI_nav_OrdenProduccion.Buy_From_Vendor_Name = "PRODUCCIÓN"
                    BeI_nav_OrdenProduccion.Buy_From_Vendor_No = PC.Bin_Code
                    BeI_nav_OrdenProduccion.Posting_Description = PC.Description
                    BeI_nav_OrdenProduccion.Vendor_Invoice_No = PC.Routing_No
                    BeI_nav_OrdenProduccion.Status = "1"
                    BeI_nav_OrdenProduccion.Document_Date = PC.Starting_Date
                    BeI_nav_OrdenProduccion.Posting_Date = PC.Starting_Date
                    BeI_nav_OrdenProduccion.Order_Date = Now

                    If PC.No = "OP-00040372" Then
                        Debug.Print("Espera")
                    End If


                    lblprg.AppendText(String.Format("{0} Procesando Órden de Producción: {1} ", vbTab, BeI_nav_OrdenProduccion.No, vbNewLine))
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Try
                        '#EJC20180503: Es un documento de compra de proveedor
                        BeI_nav_OrdenProduccion.Is_Internal_Transfer = False

                        'Insertar encabezado
                        clsLnI_nav_ped_compra_enc.Insertar(BeI_nav_OrdenProduccion, lConnection, lTransaction)

                        VContadorBitacoraIntermedia += 1

                        prg.Value = vContador

                        vContador += 1

                        Application.DoEvents()

                        Dim BePresentacion As New clsBeProducto_Presentacion

                        'Insertar detalle
                        If Not PC.ProdOrderLines Is Nothing Then

                            If PC.ProdOrderLines.Length > 0 Then

                                vNoLInea = 1

                                For Each L As Released_Prod_Order_Lines In PC.ProdOrderLines

                                    BeI_nav_OrdenProduccionDet = New clsBeI_nav_ped_compra_det

                                    If Not L.Item_No Is Nothing Then

                                        Try

                                            Try

                                                CopyObject(L, BeI_nav_OrdenProduccionDet)

                                            Catch ex As Exception
                                                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                            End Try

                                            BeI_nav_OrdenProduccionDet.NoEnc = PC.No
                                            BeI_nav_OrdenProduccionDet.Type = "2" '#EJC20210114: Artículo
                                            BeI_nav_OrdenProduccionDet.No = L.Item_No
                                            '#EJC20210324: De momento no está el no de línea en el ws de órdenes de prd.
                                            BeI_nav_OrdenProduccionDet.Line_No = 10000

                                            '#CKFK 20211126 Las OP siempre se van a recibir en Presentación,
                                            'se debe validar de igual forma en los lotes
                                            If Not BeI_nav_OrdenProduccionDet.No Is Nothing Then

                                                BePresentacion = clsLnProducto_presentacion.Get_Single_By_Codigo(BeI_nav_OrdenProduccionDet.No, lConnection, lTransaction)

                                                If Not BePresentacion Is Nothing Then

                                                    BeI_nav_OrdenProduccionDet.Variant_Code = BePresentacion.Nombre
                                                    vCantidadConversionCajas = Math.Round(BeI_nav_OrdenProduccionDet.Quantity / BePresentacion.Factor, 6)
                                                    BeI_nav_OrdenProduccionDet.Quantity = vCantidadConversionCajas
                                                    Debug.Print("Cantidad es ->" & vCantidadConversionCajas)
                                                    vCantidadDecimal = vCantidadConversionCajas - BeI_nav_OrdenProduccionDet.Quantity

                                                    If vCantidadDecimal <> 0 Then
                                                        Debug.WriteLine("En un futuro cercano.... INSERTAR LINEA DE UNIDADES CON ESTA CANTIDAD")
                                                    End If

                                                End If


                                            End If

                                            vNoLInea += 1

                                            If Not L.Location_Code Is Nothing Then


                                                BeProductoBodega = clsLnProducto_bodega.Existe(L.Item_No,
                                                                                               BeConfigEnc.Idbodega,
                                                                                               lConnection,
                                                                                               lTransaction)

                                                'Existe el producto en el maestro?
                                                If Not BeProductoBodega Is Nothing Then

                                                    '#CKFK 20211201 Quité esta condición porque siempre se debe recibir
                                                    ' If (L.Remaining_Quantity <> 0) Then
                                                    If clsLnI_nav_ped_compra_det.Exist(BeI_nav_OrdenProduccionDet, lConnection, lTransaction) Then
                                                        clsLnI_nav_ped_compra_det.Actualizar(BeI_nav_OrdenProduccionDet, lConnection, lTransaction)
                                                        VContadorBitacoraIntermedia += 1
                                                    Else
                                                        clsLnI_nav_ped_compra_det.Insertar(BeI_nav_OrdenProduccionDet, lConnection, lTransaction)
                                                        VContadorBitacoraIntermedia += 1
                                                    End If

                                                Else

                                                    Try


                                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Producto no pertenece a lista de bodegas válidas para recepción: Prod: {0} Bod: {1}{2}", L.Item_No, L.Location_Code, vbNewLine),
                                                                                L.Item_No,
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet, cnnLog)

                                                        lblprg.AppendText(vbTab & vbTab & String.Format("Producto no pertenece a lista de bodegas válidas para recepción: Prod: {0} Bod: {1}{2}", L.Item_No, L.Location_Code, vbNewLine))
                                                        lblprg.AppendText(vbNewLine)
                                                        lblprg.Refresh()
                                                        lblprg.SelectionStart = lblprg.TextLength
                                                        lblprg.ScrollToCaret()

                                                    Catch ex As Exception
                                                        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                                    End Try

                                                End If 'Fin 'Existe el producto en el maestro?

                                            End If 'Fin location code is nothing    

                                        Catch ex As Exception

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                "Sin informacion",
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet, cnnLog)

                                            lblprg.AppendText(String.Format("Error al insertar Linea desde el ws a intermedia en órden de producción: {0}{1}{2}", BeI_nav_OrdenProduccionDet.No, vbNewLine, ex.Message))
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        End Try

                                    End If

                                Next

                            Else
                                Console.WriteLine("Orden de producción sin lineas de detalle?")
                            End If

                        Else
                            Console.WriteLine("Orden de producción sin lineas de detalle?")
                        End If

                    Catch ex As Exception

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                          BeI_nav_OrdenProduccion.No,
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet, cnnLog)

                        lblprg.AppendText(String.Format("Error al insertar Encabezado de documento de ingreso desde ws a intermedia: {0}{1}{2}", BeI_nav_OrdenProduccion.No, vbNewLine, ex.Message))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End Try

                    Application.DoEvents()

                Next

            End If

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vbTab & "Fin de procesamiento a tabla intermendia: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Ordenes_Produccion_Desde_WSNav_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            lblprg.AppendText(String.Format("Error al insertar Ordenes de Compra desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

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

    Private Function Asigna_Unidad_De_Medida(ByRef BeOrdenProduccionDet As clsBeTrans_oc_det,
                                             ByRef navOrdenProduccionDet As clsBeI_nav_ped_compra_det,
                                             ByRef BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                             ByRef BeProductoBodega As clsBeProducto_bodega,
                                             ByRef lblprg As RichTextBox,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction,
                                             ByRef lConnectionLog As SqlConnection) As Boolean

        Asigna_Unidad_De_Medida = False

        Try

            'Existe el producto con U.M.Bas = U.M. de pedido de compra.
            If Not clsLnProducto.Existe(navOrdenProduccionDet.No,
                                        BeUnidadMedidaPedCompra.IdUnidadMedida,
                                        lConnection,
                                        lTransaction) Then

                Dim BePresentacion As New clsBeProducto_Presentacion

                BePresentacion = clsLnProducto_presentacion.
                Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                            navOrdenProduccionDet.Unit_of_Measure_Code,
                                                            lConnection,
                                                            lTransaction)

                If Not BePresentacion Is Nothing Then
                    'La presentación ya existe
                    BeOrdenProduccionDet.IdPresentacion = BePresentacion.IdPresentacion
                    BeOrdenProduccionDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                    BeOrdenProduccionDet.IdUnidadMedidaBasica = BeProductoBodega.Producto.IdUnidadMedidaBasica
                    BeOrdenProduccionDet.UnidadMedida.IdUnidadMedida = BeProductoBodega.Producto.IdUnidadMedidaBasica
                    BeOrdenProduccionDet.Nombre_unidad_medida_basica = BeProductoBodega.Producto.UnidadMedida.Nombre
                Else

                    Dim vFactorConv As Double = clsLnUnidad_medida_conversion.Get_Factor(BeUnidadMedidaPedCompra.IdUnidadMedida,
                                                                                         BeProductoBodega.Producto.UnidadMedida.IdUnidadMedida,
                                                                                         lConnection,
                                                                                         lTransaction)

                    'Existe factor para crear la presentación con la unidad de medida del pedido de compra.
                    If vFactorConv > 0 Then

                        BePresentacion = New clsBeProducto_Presentacion
                        BePresentacion.IdPresentacion = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1
                        BePresentacion.IdProducto = BeProductoBodega.IdProducto
                        BePresentacion.Codigo_barra = BeProductoBodega.Producto.Codigo_barra + navOrdenProduccionDet.Unit_of_Measure_Code
                        BePresentacion.Nombre = navOrdenProduccionDet.Unit_of_Measure_Code
                        BePresentacion.Imprime_barra = True
                        BePresentacion.Peso = 0
                        BePresentacion.Alto = 0
                        BePresentacion.Largo = 0
                        BePresentacion.Ancho = 0
                        BePresentacion.Factor = vFactorConv
                        BePresentacion.MinimoExistencia = 0
                        BePresentacion.MaximoExistencia = 0
                        BePresentacion.User_agr = BeConfigEnc.IdUsuario
                        BePresentacion.User_mod = BeConfigEnc.IdUsuario
                        BePresentacion.Fec_agr = Now
                        BePresentacion.Fec_mod = Now
                        BePresentacion.Activo = True
                        BePresentacion.EsPallet = False
                        BePresentacion.Precio = 0
                        BePresentacion.MinimoPeso = 0
                        BePresentacion.MaximoPeso = 0
                        BePresentacion.Costo = 0
                        BePresentacion.CamasPorTarima = 0
                        BePresentacion.CajasPorCama = 0
                        BePresentacion.Genera_lp_auto = False
                        BePresentacion.Permitir_paletizar = False
                        BePresentacion.Sistema = True
                        BePresentacion.Codigo = BeProductoBodega.Producto.Codigo

                        Try

                            clsLnProducto_presentacion.Insertar(BePresentacion, lConnection, lTransaction)

                            BeOrdenProduccionDet.IdPresentacion = BePresentacion.IdPresentacion
                            BeOrdenProduccionDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                            BeOrdenProduccionDet.IdUnidadMedidaBasica = BeProductoBodega.Producto.IdUnidadMedidaBasica
                            BeOrdenProduccionDet.UnidadMedida.IdUnidadMedida = BeProductoBodega.Producto.IdUnidadMedidaBasica
                            BeOrdenProduccionDet.Nombre_unidad_medida_basica = BeProductoBodega.Producto.UnidadMedida.Nombre

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                        BeOrdenProduccionDet.Codigo_Producto,
                                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                                        BeConfigDet.Idnavconfigdet, lConnectionLog)

                            lblprg.AppendText(String.Format("Error al insertar presentación: {0}{1}", ex.Message, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                    Else

                        Throw New Exception(String.Format("Error: No existe factor en unidad_medida_conversion 
                                            para Producto: {0} UnidMedBas {1} <> UnidMed Ped. Compra {2} ",
                                            navOrdenProduccionDet.No,
                                            BeProductoBodega.Producto.UnidadMedida.Nombre,
                                            navOrdenProduccionDet.Unit_of_Measure_Code))

                    End If 'Fin Sí: 'Existe factor para crear la presentación con la unidad de medida del pedido de compra.                   

                End If 'Fin sí: Existe presentación.              

            Else
                'La unidad de medida básica del producto es = a la unidad de medida del pedido de compra.
                'Se utiliza la UM del pedido de compra aunque la básica del maestro sea otra porque existe factor de conversión
                BeOrdenProduccionDet.IdUnidadMedidaBasica = BeUnidadMedidaPedCompra.IdUnidadMedida
                BeOrdenProduccionDet.UnidadMedida.IdUnidadMedida = BeUnidadMedidaPedCompra.IdUnidadMedida
                BeOrdenProduccionDet.Nombre_unidad_medida_basica = navOrdenProduccionDet.Unit_of_Measure_Code
            End If

            Asigna_Unidad_De_Medida = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Insertar_Ordenes_Produccion_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByRef lblprg As RichTextBox,
                                                                                      ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                      Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                      Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Ordenes_Produccion_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim DifCant As Double = 0

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Pedido compra") Then

                    lblprg.AppendText("La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Exit Function

                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                                        CnnInterface, lTransInterface)

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            'lblprg.AppendText(String.Format("Conectando a BD: {0} Sever: {1}", BD.Instancia.NombreBD, BD.Instancia.Server))
            'lblprg.AppendText(vbNewLine)

            'lblprg.Refresh()

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

            'lblprg.AppendText("Iniciando transacción a BD: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText("Consultando WebService de bodega en: " & My.MySettings.Default.NavSync_WSOrdenesProduccion_OP_Lanzadas_Service)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Ordenes_Produccion_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde WS?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Ordenes_Produccion_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            Dim lOrdenProduccionEnc As New List(Of clsBeI_nav_ped_compra_enc)

            lblprg.AppendText("Consultando pedidos de compra en tabla intermedia ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lOrdenProduccionEnc = clsLnI_nav_ped_compra_enc.GetAll(CnnInterface, lTransInterface, lblprg, prg)

            If lOrdenProduccionEnc.Count > 0 Then

                Dim gBeOrdenCompra As clsBeTrans_oc_enc = Nothing
                Dim OrdenProduccionExistente As clsBeTrans_oc_enc = Nothing
                Dim vContador As Integer = 0
                Dim vContadorLineasDet As Integer = 0
                Dim BeProveedorBodega As New clsBeProveedor_bodega
                Dim BeProductoBodega As New clsBeProducto_bodega
                Dim BePresentacion As New clsBeProducto_Presentacion

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                                        CnnInterface, lTransInterface)

                If BeConfigEnc Is Nothing Then
                    If BD.Instancia.IdConfiguracionInterface = 0 Then
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                    Else
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                    End If
                End If

                prg.Maximum = lOrdenProduccionEnc.Count

                prg.Value = 0

                VContadorBitacoraTomims = 0

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("Procesando en tablas de WMS: " & Now)
                lblprg.AppendText(vbNewLine)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Dim BeUnidadMedidaPedCompra As New clsBeUnidad_medida

                Dim vIdPropietarioBodega As Integer = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.Idbodega,
                                                                                                                              BeConfigEnc.IdPropietario,
                                                                                                                              CnnInterface,
                                                                                                                              lTransInterface)

                If vIdPropietarioBodega = 0 Then
                    Throw New Exception(String.Format("El Id de propietario: {0} No está asociado al Id de bodega: {1}: ", BeConfigEnc.IdPropietario, BeConfigEnc.Idbodega))
                End If

                For Each navOrdenProduccionEnc As clsBeI_nav_ped_compra_enc In lOrdenProduccionEnc

                    If navOrdenProduccionEnc.Status <> 0 Then

                        If navOrdenProduccionEnc.No = "OP-00069724" Then
                            Debug.Print("ESPERA")
                            'Continue For
                        End If

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(vbTab & String.Format("Procesando O.P.: {0} ", navOrdenProduccionEnc.No, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        '#CKFK 20211117 Estoy poniendo esto en comentario porque me dice Ricardo que debe hacerse contra el número de la OP
                        'gBeOrdenCompra = New clsBeTrans_oc_enc() With {.Referencia = navOrdenProduccionEnc.Vendor_Invoice_No, .IsNew = True}
                        gBeOrdenCompra = New clsBeTrans_oc_enc() With {.No_Documento = navOrdenProduccionEnc.No, .IsNew = True}

                        '#CKFK 20211117 Puse esto en comentario porque me dice Ricardo que debe hacerse contra el número de la OP
                        'OrdenProduccionExistente = clsLnTrans_oc_enc.Get_Single_By_Referencia(gBeOrdenCompra, CnnInterface, lTransInterface)
                        OrdenProduccionExistente = clsLnTrans_oc_enc.Get_Single_By_NoDocumento(gBeOrdenCompra.No_Documento, CnnInterface, lTransInterface)

                        prg.Value = vContador

                        vContador += 1
                        vContadorLineasDet = 0

                        'El pedido de compra existe y debe ser actualizado.
                        If Not OrdenProduccionExistente Is Nothing Then

                            gBeOrdenCompra.Activo = True

                            BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navOrdenProduccionEnc.Buy_From_Vendor_No,
                                                                                                       BeConfigEnc.Idbodega,
                                                                                                       CnnInterface,
                                                                                                       lTransInterface)

                            If BeProveedorBodega Is Nothing Then
                                Throw New Exception("No existe el proveedor: " & navOrdenProduccionEnc.Buy_From_Vendor_No & " para la bodega con identificador: " & BeConfigEnc.Idbodega)
                            End If

                            If gBeOrdenCompra.ProveedorBodega Is Nothing Then
                                gBeOrdenCompra.ProveedorBodega = New clsBeProveedor_bodega
                            End If

                            gBeOrdenCompra.IdProveedorBodega = BeProveedorBodega.IdAsignacion
                            gBeOrdenCompra.IdTipoIngresoOC = 6 'OPM - Orden de producción.
                            gBeOrdenCompra.No_Documento = navOrdenProduccionEnc.No
                            gBeOrdenCompra.User_Mod = BeConfigEnc.IdUsuario
                            gBeOrdenCompra.Fec_Mod = Now
                            gBeOrdenCompra.Procedencia = ""
                            gBeOrdenCompra.No_Marchamo = ""
                            gBeOrdenCompra.Referencia = navOrdenProduccionEnc.No
                            gBeOrdenCompra.Observacion = navOrdenProduccionEnc.Posting_Description
                            gBeOrdenCompra.Control_Poliza = False
                            gBeOrdenCompra.Push_To_NAV = True

                            If gBeOrdenCompra.IsNew Then
                                gBeOrdenCompra.ObjPoliza = Nothing
                            End If

                            clsLnTrans_oc_enc.Actualizar(gBeOrdenCompra, CnnInterface, lTransInterface)

                            lblprg.AppendText(vbTab & String.Format("Procesando# : {0}{1}", navOrdenProduccionEnc.No, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            VContadorBitacoraTomims += 1

                            If navOrdenProduccionEnc.Lineas_Detalle.Count > 0 Then

                                Dim BeOrdenProduccionDet As New clsBeTrans_oc_det

                                For Each navOrdenProduccionDet As clsBeI_nav_ped_compra_det In navOrdenProduccionEnc.Lineas_Detalle

                                    vContadorLineasDet += 1

                                    Try

                                        BePresentacion = New clsBeProducto_Presentacion

                                        BeProductoBodega = clsLnProducto_bodega.Existe(navOrdenProduccionDet.No,
                                                                                       BeConfigEnc.Idbodega,
                                                                                       CnnInterface,
                                                                                       lTransInterface)

                                        'Existe el producto en el maestro?
                                        If BeProductoBodega IsNot Nothing Then

                                            'Existe el producto en el detalle de la orden de compra en la tabla DE TOMWMS?
                                            BeOrdenProduccionDet = clsLnTrans_oc_det.Exist(OrdenProduccionExistente.IdOrdenCompraEnc,
                                                                                           navOrdenProduccionDet.Line_No,
                                                                                           CnnInterface,
                                                                                           lTransInterface)

                                            '#CKFK 20180725 17:45 coloqué esto en comentario, porque la instancia BeUnidadMedidaPedCompra era nothing y no se le podía asignar valor a la property Nombre
                                            'BeUnidadMedidaPedCompra.Nombre = navOrdenProduccionDet.Unit_of_Measure_Code
                                            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Nombre(navOrdenProduccionDet.Unit_of_Measure_Code,
                                                                                                          CnnInterface,
                                                                                                          lTransInterface)

                                            If BeUnidadMedidaPedCompra Is Nothing Then
                                                BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navOrdenProduccionDet.Unit_of_Measure_Code,
                                                                                                                                BeConfigEnc.IdPropietario,
                                                                                                                                CnnInterface,
                                                                                                                                lTransInterface)
                                            End If

                                            'La unidad de medida existe?
                                            If BeUnidadMedidaPedCompra Is Nothing Then
                                                'unidad de medida no existe en tabla UNIDAD_MEDIDA
                                                Throw New Exception(
                                            String.Format("Producto: {0} UnidMedBas {1} No existe ",
                                                          navOrdenProduccionDet.No,
                                                          BeProductoBodega.Producto.UnidadMedida.Nombre))
                                            End If 'Fin sí: unidad de medida no existe.

#Region "Cod_Variante_A_Presentacion"
                                            If navOrdenProduccionDet.NoEnc = "OP-00040372" Then
                                                Debug.Print("Espera")
                                            End If

                                            If navOrdenProduccionDet.Variant_Code <> "" Then

                                                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                                                                                                        navOrdenProduccionDet.Variant_Code,
                                                                                                                                        CnnInterface,
                                                                                                                                        lTransInterface)
                                                If BePresentacion Is Nothing Then
                                                    Throw New Exception("ERROR_202303031404C: La presentación: " & navOrdenProduccionDet.Variant_Code & " no existe para el código de producto " & navOrdenProduccionDet.No)
                                                End If 'Fin sí: BePresentacion IsNothing (Presentación no existe y se insertó)

                                            End If 'Fin sí: Cod_Variante <> ""

#End Region

                                            'Producto No existe en la tabla de detalle DE TOMWMS. trans_oc_det
                                            If BeOrdenProduccionDet Is Nothing Then

                                                Try

                                                    BeOrdenProduccionDet = New clsBeTrans_oc_det
                                                    BeOrdenProduccionDet.IdOrdenCompraEnc = OrdenProduccionExistente.IdOrdenCompraEnc
                                                    BeOrdenProduccionDet.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(BeOrdenProduccionDet.IdOrdenCompraEnc, CnnInterface, lTransInterface) + 1
                                                    BeOrdenProduccionDet.IdProductoBodega = BeProductoBodega.IdProductoBodega

                                                    If Not BePresentacion Is Nothing Then
                                                        BeOrdenProduccionDet.IdPresentacion = BePresentacion.IdPresentacion
                                                        BeOrdenProduccionDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                                                    Else
                                                        BeOrdenProduccionDet.IdPresentacion = 0
                                                    End If

                                                    BeOrdenProduccionDet.Nombre_producto = navOrdenProduccionDet.Description.Replace("ñ", "").Replace("&", "")
                                                    BeOrdenProduccionDet.Nombre_unidad_medida_basica = navOrdenProduccionDet.Unit_of_Measure_Code
                                                    BeOrdenProduccionDet.Cantidad = navOrdenProduccionDet.Quantity
                                                    BeOrdenProduccionDet.Cantidad_recibida = navOrdenProduccionDet.Quantity_Received
                                                    BeOrdenProduccionDet.Costo = navOrdenProduccionDet.Direct_Unit_Cost
                                                    BeOrdenProduccionDet.Total_linea = navOrdenProduccionDet.Line_Amount
                                                    BeOrdenProduccionDet.No_Linea = navOrdenProduccionDet.Line_No
                                                    BeOrdenProduccionDet.Activo = True
                                                    BeOrdenProduccionDet.Porcentaje_arancel = 0
                                                    BeOrdenProduccionDet.User_agr = BeConfigEnc.IdUsuario
                                                    BeOrdenProduccionDet.User_mod = BeConfigEnc.IdUsuario
                                                    BeOrdenProduccionDet.Atributo_variante_1 = navOrdenProduccionDet.Variant_Code

                                                    If Asigna_Unidad_De_Medida(BeOrdenProduccionDet,
                                                                           navOrdenProduccionDet,
                                                                           BeUnidadMedidaPedCompra,
                                                                           BeProductoBodega,
                                                                           lblprg,
                                                                           CnnInterface,
                                                                           lTransInterface,
                                                                           CnnLog) Then

                                                        clsLnTrans_oc_det.Insertar(BeOrdenProduccionDet, CnnInterface, lTransInterface)

                                                        'VContadorBitacoraTomims += 1

                                                    End If

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                            BeOrdenProduccionDet.Nombre_producto,
                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                            BeConfigDet.Idnavconfigdet, CnnLog)

                                                    lblprg.AppendText(String.Format("Error al insertar Detalle en : {0}{1}", ex.Message, vbNewLine))
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.Refresh()
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()

                                                End Try

                                            Else 'Producto sí existe en tabla trans_oc_det

                                                Try

                                                    BeOrdenProduccionDet.IdOrdenCompraEnc = BeOrdenProduccionDet.IdOrdenCompraEnc
                                                    BeOrdenProduccionDet.IdOrdenCompraDet = BeOrdenProduccionDet.IdOrdenCompraDet
                                                    BeOrdenProduccionDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                    BeOrdenProduccionDet.Codigo_Producto = navOrdenProduccionDet.No
                                                    BeOrdenProduccionDet.Nombre_producto = navOrdenProduccionDet.Description.Replace("ñ", "").Replace("&", "")
                                                    BeOrdenProduccionDet.Nombre_unidad_medida_basica = navOrdenProduccionDet.Unit_of_Measure_Code

                                                    If BeOrdenProduccionDet.Cantidad = 0 Then
                                                        BeOrdenProduccionDet.Cantidad = navOrdenProduccionDet.Quantity
                                                    Else

                                                        DifCant = navOrdenProduccionDet.Quantity - BeOrdenProduccionDet.Cantidad

                                                        lblprg.AppendText(vbNewLine)

                                                        Select Case DifCant

                                                            Case 0
                                                                lblprg.AppendText(String.Format("La cantidad no se modificó para pedido {0} producto {1} ", navOrdenProduccionEnc.No, navOrdenProduccionDet.No))
                                                            Case Is > 0
                                                                lblprg.AppendText(String.Format("La cantidad incrementó respecto a TOM para pedido {0} producto {1} ", navOrdenProduccionEnc.No, navOrdenProduccionDet.No))
                                                            Case Is < 0
                                                                lblprg.AppendText(String.Format("La cantidad disminuyó respecto al original en WMS  para pedido {0} producto {1} ", navOrdenProduccionEnc.No, navOrdenProduccionDet.No))
                                                            Case Else
                                                                Exit Select
                                                        End Select

                                                        BeOrdenProduccionDet.Cantidad = navOrdenProduccionDet.Quantity

                                                    End If

                                                    '#EJC20220404: Hotfix, actualizar solo si lo recibido en el ERP es mayor que 0.
                                                    If (navOrdenProduccionDet.Quantity_Received > 0) Then

                                                        '#EJC20220404: Hotfix, actualizar solo si lo recibido en el ERP es mayor que lo que tiene WMS.
                                                        If (navOrdenProduccionDet.Quantity_Received > BeOrdenProduccionDet.Cantidad_recibida) Then

                                                            BeOrdenProduccionDet.Cantidad_recibida = navOrdenProduccionDet.Quantity_Received
                                                            BeOrdenProduccionDet.Costo = navOrdenProduccionDet.Direct_Unit_Cost
                                                            BeOrdenProduccionDet.Total_linea = navOrdenProduccionDet.Line_Amount
                                                            BeOrdenProduccionDet.No_Linea = navOrdenProduccionDet.Line_No
                                                            BeOrdenProduccionDet.Activo = True
                                                            BeOrdenProduccionDet.Porcentaje_arancel = 0
                                                            BeOrdenProduccionDet.User_agr = BeConfigEnc.IdUsuario
                                                            BeOrdenProduccionDet.User_mod = BeConfigEnc.IdUsuario
                                                            BeOrdenProduccionDet.Atributo_variante_1 = navOrdenProduccionDet.Variant_Code

                                                            clsLnTrans_oc_det.Actualizar_Desde_Interface(BeOrdenProduccionDet, CnnInterface, lTransInterface)

                                                        End If

                                                    End If

                                                    'VContadorBitacoraTomims += 1

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                             BeOrdenProduccionDet.Nombre_producto,
                                                                             BeNavEjecucionEnc.IdEjecucionEnc,
                                                                             BeConfigDet.Idnavconfigdet, CnnLog)

                                                    lblprg.AppendText(String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine))
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.Refresh()
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()

                                                End Try

                                            End If

                                        End If 'Fin sí: producto existe.

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                            "Pedido Sin Detalle",
                                                                             BeNavEjecucionEnc.IdEjecucionEnc,
                                                                             BeConfigDet.Idnavconfigdet, CnnLog)

                                        lblprg.AppendText(String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                    End Try

                                Next

                            End If

                        Else

                            '#EJC20180108: Se agregó validación porque el detalle de la O.C. puede tener líneas no válidas a recibir en el WMS.
                            'Si la O.C. tiene detalle en la tabla intermedia
                            If navOrdenProduccionEnc.Lineas_Detalle.Count = 0 Then
                                lblprg.AppendText(vbTab & vbTab & String.Format("Pedido #:{0} Sin Detalle {1}", navOrdenProduccionEnc.No, vbNewLine))
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else

                                gBeOrdenCompra.IdOrdenCompraEnc = clsLnTrans_oc_enc.MaxID(CnnInterface, lTransInterface) + 1
                                gBeOrdenCompra.PropietarioBodega = New clsBePropietario_bodega
                                gBeOrdenCompra.PropietarioBodega.IdPropietarioBodega = vIdPropietarioBodega
                                gBeOrdenCompra.IdEstadoOC = 1
                                gBeOrdenCompra.Hora_Creacion = Now
                                gBeOrdenCompra.User_Agr = BeConfigEnc.IdUsuario
                                gBeOrdenCompra.Fec_Agr = navOrdenProduccionEnc.Document_Date
                                gBeOrdenCompra.Fecha_Creacion = Now
                                gBeOrdenCompra.Activo = True
                                gBeOrdenCompra.IdBodega = BeConfigEnc.Idbodega

                                BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navOrdenProduccionEnc.Buy_From_Vendor_No,
                                                                                                       BeConfigEnc.Idbodega,
                                                                                                       CnnInterface, lTransInterface)

                                If BeProveedorBodega Is Nothing Then

                                    BeProveedorBodega = clsSyncNavProveedor.Insertar_Proveedor_Single(navOrdenProduccionEnc.Buy_From_Vendor_No, CnnInterface, lTransInterface, CnnLog, lblprg, prg)

                                    If BeProveedorBodega Is Nothing Then

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(
                                        String.Format("El proveedor: {0} no existe, no se puede importar la órden de producción: {1}",
                                                      navOrdenProduccionEnc.Buy_From_Vendor_No,
                                                      navOrdenProduccionEnc.No),
                                                      navOrdenProduccionEnc.Buy_From_Vendor_No,
                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                      BeConfigDet.Idnavconfigdet, CnnLog)

                                        lblprg.AppendText(String.Format("Error al insertar órden de producción: {0} El proveedor: {1} no existe, ¿Ya se actualizó maestro de proveedores?", navOrdenProduccionEnc.Buy_From_Vendor_No, navOrdenProduccionEnc.No, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        Throw New Exception("No logramos insertar el proveedor asociado a la órden de producción, lamentamos el inconveniente")

                                    Else

                                        lblprg.AppendText(String.Format("El proveedor: {1} no existía pero se insertó para el la O.P.: {0}. Nada de que preocuparse :) ", navOrdenProduccionEnc.Buy_From_Vendor_No, navOrdenProduccionEnc.No, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                    End If

                                End If

                                If gBeOrdenCompra.ProveedorBodega Is Nothing Then
                                    gBeOrdenCompra.ProveedorBodega = New clsBeProveedor_bodega
                                End If

                                gBeOrdenCompra.IdProveedorBodega = BeProveedorBodega.IdAsignacion
                                gBeOrdenCompra.IdTipoIngresoOC = 6 'P.C. REC NAV
                                gBeOrdenCompra.No_Documento = navOrdenProduccionEnc.No
                                gBeOrdenCompra.User_Mod = BeConfigEnc.IdUsuario
                                gBeOrdenCompra.Fec_Mod = Now
                                gBeOrdenCompra.Procedencia = ""
                                gBeOrdenCompra.No_Marchamo = ""
                                gBeOrdenCompra.Referencia = navOrdenProduccionEnc.No
                                gBeOrdenCompra.Observacion = navOrdenProduccionEnc.Posting_Description
                                gBeOrdenCompra.Control_Poliza = False
                                gBeOrdenCompra.Push_To_NAV = True

                                If gBeOrdenCompra.IsNew Then
                                    gBeOrdenCompra.ObjPoliza = Nothing
                                End If

                                gBeOrdenCompra.Enviado_A_ERP = False

                                Try

                                    clsLnTrans_oc_enc.Insertar(gBeOrdenCompra, CnnInterface, lTransInterface)

                                    Application.DoEvents()
                                    VContadorBitacoraTomims += 1

                                    If navOrdenProduccionEnc.Lineas_Detalle.Count > 0 Then

                                        For Each navOrdenProduccionDet As clsBeI_nav_ped_compra_det In navOrdenProduccionEnc.Lineas_Detalle

                                            vContadorLineasDet += 1

                                            Dim BeOrdenProduccionDet As New clsBeTrans_oc_det() With {.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc,
                                                .IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompra.IdOrdenCompraEnc, CnnInterface, lTransInterface) + 1}

                                            '#20180101_1203:Línea agregada para actulización en envío.
                                            'BeOrdenProduccionDet.No_Linea = navOrdenProduccionDet.No

                                            BePresentacion = New clsBeProducto_Presentacion()

                                            BeProductoBodega = clsLnProducto_bodega.Existe(navOrdenProduccionDet.No, BeConfigEnc.Idbodega, CnnInterface, lTransInterface)

                                            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navOrdenProduccionDet.Unit_of_Measure_Code,
                                                                                                                            BeConfigEnc.IdPropietario,
                                                                                                                            CnnInterface,
                                                                                                                            lTransInterface)

#Region "COD_VARIANTE_A_PRESENTACION"

                                            If navOrdenProduccionDet.Variant_Code <> "" Then

                                                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                                                                                          navOrdenProduccionDet.Variant_Code,
                                                                                                                          CnnInterface,
                                                                                                                          lTransInterface)

                                                If BePresentacion Is Nothing Then
                                                    Throw New Exception("ERROR_202303031404D: La presentación: " & navOrdenProduccionDet.Variant_Code & " no existe para el código de producto " & navOrdenProduccionDet.No)
                                                End If

                                            End If

#End Region

                                            If BeProductoBodega IsNot Nothing Then

                                                Try

                                                    BeOrdenProduccionDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                    BeOrdenProduccionDet.Codigo_Producto = navOrdenProduccionDet.No
                                                    BeOrdenProduccionDet.Nombre_producto = navOrdenProduccionDet.Description.Replace("ñ", "").Replace("&", "")
                                                    BeOrdenProduccionDet.Nombre_unidad_medida_basica = navOrdenProduccionDet.Unit_of_Measure_Code
                                                    BeOrdenProduccionDet.Cantidad = navOrdenProduccionDet.Quantity
                                                    BeOrdenProduccionDet.Cantidad_recibida = navOrdenProduccionDet.Quantity_Received
                                                    BeOrdenProduccionDet.Costo = navOrdenProduccionDet.Direct_Unit_Cost
                                                    BeOrdenProduccionDet.Total_linea = navOrdenProduccionDet.Line_Amount
                                                    BeOrdenProduccionDet.No_Linea = navOrdenProduccionDet.Line_No
                                                    BeOrdenProduccionDet.Activo = True
                                                    BeOrdenProduccionDet.Porcentaje_arancel = 0
                                                    BeOrdenProduccionDet.User_agr = BeConfigEnc.IdUsuario
                                                    BeOrdenProduccionDet.User_mod = BeConfigEnc.IdUsuario
                                                    BeOrdenProduccionDet.Atributo_variante_1 = navOrdenProduccionDet.Variant_Code

                                                    If Not BePresentacion Is Nothing Then
                                                        BeOrdenProduccionDet.IdPresentacion = BePresentacion.IdPresentacion
                                                        BeOrdenProduccionDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                                                    Else
                                                        BeOrdenProduccionDet.IdPresentacion = 0
                                                    End If

                                                    If Asigna_Unidad_De_Medida(BeOrdenProduccionDet,
                                                                               navOrdenProduccionDet,
                                                                               BeUnidadMedidaPedCompra,
                                                                               BeProductoBodega,
                                                                               lblprg,
                                                                               CnnInterface,
                                                                               lTransInterface,
                                                                               CnnLog) Then

                                                        clsLnTrans_oc_det.Insertar(BeOrdenProduccionDet,
                                                                                   CnnInterface,
                                                                                   lTransInterface)

                                                        'VContadorBitacoraTomims += 1

                                                    End If

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                              BeOrdenProduccionDet.Nombre_producto,
                                                                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                              BeConfigDet.Idnavconfigdet, CnnLog)

                                                    lblprg.AppendText(String.Format("Error al insertar desde ws a intermedia: {0}{1}{2}", BeOrdenProduccionDet.Nombre_producto, ex.Message, vbNewLine))
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.Refresh()
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()

                                                End Try

                                            Else

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro ",
                                                                      navOrdenProduccionDet.No,
                                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                                      BeConfigDet.Idnavconfigdet, CnnLog)

                                                lblprg.AppendText(String.Format("No existe Producto Bodega: {0}{1}", navOrdenProduccionDet.No, vbNewLine))
                                                lblprg.AppendText(vbNewLine)
                                                lblprg.Refresh()
                                                lblprg.SelectionStart = lblprg.TextLength
                                                lblprg.ScrollToCaret()

                                            End If

                                        Next

                                    End If


                                    '#EJC20230124: Actualizar a importado (creo que 2 es otra cosa)
                                    navOrdenProduccionEnc.Status = 3 'Importado

                                    clsLnI_nav_ped_compra_enc.Actualizar_Estado(navOrdenProduccionEnc, CnnInterface, lTransInterface)

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                            navOrdenProduccionEnc.No,
                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                            BeConfigDet.Idnavconfigdet, CnnLog)

                                    lblprg.AppendText(String.Format("Error al insertar la OC con Referencia: {0}{1}{2}", navOrdenProduccionEnc.No, vbNewLine, ex.Message))
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                End Try

                                Application.DoEvents()

                            End If

                        End If

                    Else

                        lblprg.AppendText(String.Format("OC Inactiva {0} ", navOrdenProduccionEnc.No, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                Next

            End If

            lTransInterface.Commit()

            '#EJC20171107_REF04_0250AM: Desplegar cantidad de registros de pedidos de compra procesados
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vbNewLine)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            lblprg.AppendText("Fin de procesamiento en WMS: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Órdenes de producción procesadas correctamente: {0}", VContadorBitacoraTomims))
            lblprg.AppendText(vbNewLine)
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

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception

            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()


            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                              "",
                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                              BeConfigDet.Idnavconfigdet, CnnLog)

            lblprg.AppendText(String.Format("Error al insertar pedido de compra a tabla DE TOMWMS: {0} {1}", ex.Message, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Public Function Get_Ordenes_Produccion_FromWS(Optional ByVal AplicarFiltros As Boolean = True) As List(Of OP_Lanzadas)

        Try

            Dim lPedidosCompra As New List(Of OP_Lanzadas)
            Dim TieneFiltros As Boolean = False
            Dim vCriteria As String = ""
            Dim vContador As Integer = 0
            Dim StartDate As String = "01092021D"

            If AplicarFiltros Then

                Dim BeNavEnt As New clsBeI_nav_ent
                BeNavEnt = clsLnI_nav_ent.Get_Single_By_Nombre("Órdenes Producción")

                If Not BeNavEnt Is Nothing Then

                    If Not BeNavEnt.lDetalleFiltros Is Nothing Then

                        If BeNavEnt.lDetalleFiltros.Count > 0 Then

                            TieneFiltros = True

                            Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
                            lFiltros = BeNavEnt.lDetalleFiltros

                            For Each FiltroCategoria In lFiltros

                                If FiltroCategoria.Tipo_Filtro = "" OrElse FiltroCategoria.Tipo_Filtro = "BODEGA" Then

                                    If vContador = 0 Then
                                        vCriteria = FiltroCategoria.Valor
                                    Else
                                        vCriteria += "|" & FiltroCategoria.Valor
                                    End If

                                ElseIf FiltroCategoria.Tipo_Filtro = "FECHA_INICIO" Then
                                    StartDate = FiltroCategoria.Valor
                                End If

                                vContador += 1

                            Next

                        End If

                    End If

                End If

            End If

            If TieneFiltros Then

                If vCriteria <> "" AndAlso pBodega <> "" Then
                    If pBodega <> vCriteria Then
                        Throw New Exception(String.Format("La Bodega del filtro: {0} no se corresponde con la Bodega de la interface: {1}", vCriteria, pBodega))
                    End If
                End If

                Dim vFechaFiltro As String = FormatoFechas.sFechaM(New Date(2022, 3, 31))
                Dim vFiltro1 As New OP_Lanzadas_Filter() With {.Field = OP_Lanzadas_Fields.Location_Code, .Criteria = vCriteria}
                Dim vFiltro2 As New OP_Lanzadas_Filter() With {.Field = OP_Lanzadas_Fields.Blocked, .Criteria = "0"}
                'Dim vFiltro3 As New OP_Lanzadas_Filter() With {.Field = OP_Lanzadas_Fields.Starting_Date, .Criteria = vFechaFiltro} '"07/23/2021.."

                '#EJC20211112: No aplicar filtro de fecha, instrucción de Ricardo 11/11/2021, la bandeja de NAV, debería tener solo lo activo.
                Dim vFiltro3 As New OP_Lanzadas_Filter() With {.Field = OP_Lanzadas_Fields.Starting_Date, .Criteria = StartDate} '"07/23/2021.."
                Dim vFiltros As OP_Lanzadas_Filter() = New OP_Lanzadas_Filter() {vFiltro1, vFiltro2, vFiltro3}
                'Dim vFiltros As OP_Lanzadas_Filter() = New OP_Lanzadas_Filter() {vFiltro1, vFiltro2}

                wsOrdenProduccionService.Url = My.MySettings.Default.NavSync_WSOrdenesProduccion_OP_Lanzadas_Service

                fichaOrdenProduccion = wsOrdenProduccionService.ReadMultiple(vFiltros, Nothing, 0)
            Else
                fichaOrdenProduccion = wsOrdenProduccionService.ReadMultiple(Nothing, Nothing, 0)
            End If

            lPedidosCompra.AddRange(fichaOrdenProduccion)

            Return lPedidosCompra

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Sub Enviar_Transacciones_De_Ingreso(ByRef lblprg As RichTextBox,
                                               ByRef prg As System.Windows.Forms.ProgressBar)

        Dim lTransaccionesIngreso As New List(Of clsBeI_nav_transacciones_out)
        Dim TipoOrdenProduccion As Integer = 0
        Dim Enviado_A_Erp As Boolean = False

        Try

            lTransaccionesIngreso = clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio_By_IdBodega(BeConfigEnc.Idbodega)

            If Not lTransaccionesIngreso Is Nothing AndAlso lTransaccionesIngreso.Count > 0 Then

                Dim ListaPedidosCompra = (From i In lTransaccionesIngreso
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idordencompra, Key i.Idrecepcionenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idordencompra, Key Keys.Idrecepcionenc})

                lblprg.AppendText(String.Format("Transacciones a enviar: {0}", lTransaccionesIngreso.Count))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Dim BeReOC As New clsBeTrans_re_oc

                For Each PC In ListaPedidosCompra

                    TipoOrdenProduccion = clsLnTrans_oc_enc.Get_Tipo_Documento(PC.No_pedido)

                    BeReOC = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(PC.Idordencompra, PC.Idrecepcionenc)

                    Enviado_A_Erp = (BeReOC.No_docto <> "")

                    Select Case TipoOrdenProduccion

                        Case 1 'Es un pedido de compra de proveedor.

                            If Not Enviado_A_Erp Then


                            Else

                                Try

                                Catch ex As Exception

                                    If Not ex.Message = "There is nothing to post." Then 'Pedido sin nada que registrar
                                        lblprg.AppendText(String.Format("Nada que registrar para pedido: {0} en NAV.", PC.No_pedido))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                    Else
                                        lblprg.AppendText(String.Format("Error al enviar pedido a NAV: {0}", ex.Message))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                    End If

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar pedido a NAV: {0}", ex.Message),
                                      PC.No_pedido,
                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                      BeConfigDet.Idnavconfigdet)

                                End Try

                            End If

                        Case 2 'Es un pedido de transferencia desde una bodega X hacia la bodega de WMS.

                            If Not Enviado_A_Erp Then

                            End If

                    End Select

                Next

            Else

                '#EJC20180521: Tratar de registrar pedidos de ingreso que no se registraron en NAV.
                Dim lTransOcPendienteRegistroEnNav As New List(Of clsBeTrans_oc_enc)
                lTransOcPendienteRegistroEnNav = clsLnTrans_oc_enc.Get_All_Pendiente_Registro_MI3()

                If lTransOcPendienteRegistroEnNav.Count > 0 Then

                    For Each PC In lTransOcPendienteRegistroEnNav

                        'Cerrada ó en backorder?
                        If PC.IdEstadoOC = 4 OrElse PC.IdEstadoOC = 6 Then

                            'Transf de recepción o pedido de compra?
                            TipoOrdenProduccion = clsLnTrans_oc_enc.Get_Tipo_Documento(PC.Referencia)

                            If PC.Referencia = "PC-043933" Then
                                Debug.Print("Espera")
                            Else
                                Debug.Print("Sigue")
                            End If

                            Select Case TipoOrdenProduccion

                                Case 1 'Es un pedido de compra de proveedor.

                                    'lTransaccionesIngreso = clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio()

                                    Dim lRecs As New List(Of clsBeTrans_re_enc)
                                    lRecs = clsLnTrans_re_enc.Get_All_By_IdOrdenRecEnc(PC.IdOrdenCompraEnc)

                                    prg.Maximum = lRecs.Count

                                    lblprg.Text = ""

                                    Try

                                        'wsRegistraRecepcionOrdenProduccion.RegistrarRecCompra(PC.Referencia)

                                        lblprg.AppendText(String.Format("Se registró el pedido:{0} correctamente en el ERP.", PC.Referencia))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(PC.IdOrdenCompraEnc, True)

                                    Catch ex As Exception
                                        lblprg.AppendText(String.Format("Error al registrar pedido {0} en NAV: {1}", PC.Referencia, ex.Message))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar pedido {0} en NAV: {1}", PC.Referencia, ex.Message),
                                        PC.Referencia,
                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                        BeConfigDet.Idnavconfigdet)
                                    End Try

                                Case 2 'Es un pedido de transferencia desde una bodega X hacia la bodega de WMS.

                                    'WsRegistra_Transfer_Recepcion.RegistrarRecepTransfer(PC.Referencia)

                                    clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(PC.IdOrdenCompraEnc, True)

                            End Select

                        End If

                    Next

                End If

                lblprg.AppendText("No hay transacciones para enviar.")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
        End Try

    End Sub

    '#CKFK20220125 Agregué el Push en la clase correspondiente
    Public Shared Function Push_Recepcion_Produccion_To_NAV_For_BYB(ByVal DocumentoUbicacion As String,
                                                                    ByVal CodigoProducto As String,
                                                                    ByVal Cantidad As Double,
                                                                    ByVal IdRecepcionEnc As Integer,
                                                                    ByVal IdRecepcionDet As Integer,
                                                                    ByVal pIdUsuario As Integer,
                                                                    ByRef pRespuesta As String) As Boolean

        Dim vResultadoPutAwayCreate As String = ""

        Dim vDocumentoUbicacion As String = DocumentoUbicacion
        Dim vRecepcionAlmacen As String = ""
        Dim vTipoPush As String = "Push_Recepcion_Produccion_To_NAV_For_BYB"

        Push_Recepcion_Produccion_To_NAV_For_BYB = False


        Dim vUrl As String = My.MySettings.Default.NavSync_CUWMS_CUWMS

        Dim ws2 As New CUWMS.CUWMS() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrl
        }

        vUrl = My.MySettings.Default.NavSync_WSUbicarAlmacen_Ubicar_Almacen_Service

        Dim ws3 As New WSUbicarAlmacen.Ubicar_Almacen_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrl
        }

        Try

            Dim UbicarAlmacen As New WSUbicarAlmacen.Ubicar_Almacen()
            UbicarAlmacen = ws3.Read(DocumentoUbicacion)

            '#EJC20220405: Algunas veces NAV no devuelve la ubicación..
            If Not UbicarAlmacen Is Nothing Then

                '#EJC20210324: Modificar cantidad a tomar/colocar si es diferente.
                For Each Lu In UbicarAlmacen.WhseActivityLines
                    If Lu.Item_No = CodigoProducto Then
                        If Not (Lu.Qty_Handled = Cantidad) Then
                            Lu.Qty_to_Handle = Cantidad
                        End If
                    End If
                Next

                '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                ws3.Update(UbicarAlmacen)

                ws2.RegisterPutAway(DocumentoUbicacion,
                                    vResultadoPutAwayCreate)

                If vResultadoPutAwayCreate = "Successfully Created" Then
                    pRespuesta = vResultadoPutAwayCreate
                    Push_Recepcion_Produccion_To_NAV_For_BYB = True
                Else
                    Throw New Exception(vResultadoPutAwayCreate)
                End If

            Else
                Throw New Exception("NAV_ERROR_20220405: No se obtuvo la ubicación: " & DocumentoUbicacion)
            End If

        Catch ex As Exception

            pRespuesta = ex.Message
            Return False

        End Try

    End Function


    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class