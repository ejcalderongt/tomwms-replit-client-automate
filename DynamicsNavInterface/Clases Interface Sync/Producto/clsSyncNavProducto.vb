Imports System.Data.SqlClient
Imports System.Net
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS.WSCategoriasProductos
Imports TOMWMS.WSGruposProductos
Imports TOMWMS.WSProductos
Imports TOMWMS.wsTablaConversiones

Public Class clsSyncNavProducto : Inherits clsInterfaceBase
    Implements IDisposable

    Dim VContadorBitacoraTomims As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Private wsProdService As New Productos_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Public Sub Dispose() Implements IDisposable.Dispose
        If wsProdService IsNot Nothing Then
            wsProdService.Dispose()
            wsProdService = Nothing
        End If
    End Sub

    Public Function Get_Productos_FromWS(Optional ByVal AplicarFiltros As Boolean = True) As List(Of Productos)

        Try

            Dim fichaProductos() As Productos
            Dim lfichaProductos As New List(Of Productos)

            wsProdService.Url = My.Settings.DynamicsNavInterface_wsProductos_Productos_Service

            If AplicarFiltros Then

                Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
                lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Producto)

                Dim vCriteria As String = ""
                Dim vContador As Integer = 0

                For Each FiltroCategoria In lFiltros

                    If vContador = 0 Then
                        vCriteria = FiltroCategoria.Valor
                    Else
                        vCriteria += "|" & FiltroCategoria.Valor
                    End If

                    vContador += 1

                Next

                Dim vFiltro1 As New Productos_Filter() With {.Field = Productos_Fields.Item_Category_Code, .Criteria = vCriteria}
                Dim vFiltro2 As New Productos_Filter() With {.Field = Productos_Fields.Blocked, .Criteria = 0}
                Dim vFiltros As Productos_Filter() = New Productos_Filter() {vFiltro1, vFiltro2}

                fichaProductos = wsProdService.ReadMultiple(vFiltros, Nothing, 0)

                For Each P As Productos In fichaProductos
                    lfichaProductos.Add(P)
                Next

            Else

                fichaProductos = wsProdService.ReadMultiple(Nothing, Nothing, 0)

                For Each P As Productos In fichaProductos
                    lfichaProductos.Add(P)
                Next

            End If

            Return lfichaProductos

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Private Function Importar_Productos_DesdeWSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                     ByRef prg As Windows.Forms.ProgressBar,
                                                                     ByRef cnnLog As SqlConnection) As Boolean

        Importar_Productos_DesdeWSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim lfichaProductos As New List(Of Productos)
            Dim lfichaUnidadesMedida As New List(Of Tabla_Conversiones)
            Dim BeINavConversion As New clsBeI_nav_conversion
            Dim lINavConversion As New List(Of clsBeI_nav_conversion)

            lfichaProductos = Get_Productos_FromWS(True)

            Application.DoEvents()

            lblprg.AppendText(String.Format("Productos encontrados en WS: {0} {1}", lfichaProductos.Count, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            prg.Maximum = lfichaProductos.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_producto.EliminarTodos(lConnection, lTransaction)

            BeNavEjecucionRes.Registros_ws = lfichaProductos.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim BeI_nav_Producto As clsBeI_nav_producto

            Dim beprodr As Productos

            lblprg.AppendText(String.Format("Consultando unidades de medida en WS: {0} {1}", clsSyncNavTablaConversion.wsTablaConv.Url, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            lfichaUnidadesMedida = clsSyncNavTablaConversion.Get_Lista_Tabla_Conversiones_FromWS()

            lblprg.AppendText(String.Format("UM encontradas en WS: {0} {1}", lfichaUnidadesMedida.Count, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Application.DoEvents()

            For Each ProductoNav As Productos In lfichaProductos

                'Debug.Assert(Prod.No <> "01007103", "Encontrado", "")

                beprodr = ProductoNav

                Try

                    BeI_nav_Producto = New clsBeI_nav_producto
                    BeI_nav_Producto.No = ProductoNav.No
                    BeI_nav_Producto.Description = ProductoNav.Description
                    BeI_nav_Producto.Description_2 = ProductoNav.Description_2
                    BeI_nav_Producto.Inventory = ProductoNav.Inventory
                    BeI_nav_Producto.Base_Unit_Of_Measure = ProductoNav.Base_Unit_of_Measure
                    BeI_nav_Producto.Unit_Cost = ProductoNav.Unit_Cost
                    BeI_nav_Producto.Inventory_Posting_Group = ProductoNav.Inventory_Posting_Group
                    BeI_nav_Producto.Gen_Prod_Posting_Group = ProductoNav.Gen_Prod_Posting_Group
                    BeI_nav_Producto.Search_Description = ProductoNav.Search_Description
                    BeI_nav_Producto.Item_Category_Code = ProductoNav.Item_Category_Code
                    BeI_nav_Producto.Product_Group_Code = ProductoNav.Product_Group_Code
                    BeI_nav_Producto.Sales_Unit = ProductoNav.Sales_Unit_of_Measure
                    BeI_nav_Producto.Item_Tracking_Code = ProductoNav.Item_Tracking_Code
                    '#EJC20210119: Obtener tabla de conversiones de NAV - Para convertirlas posteriormente en presentaciones.

                    lINavConversion = New List(Of clsBeI_nav_conversion)

                    If Not lfichaUnidadesMedida Is Nothing Then

                        For Each UM In lfichaUnidadesMedida.FindAll(Function(x) x.Item_No = BeI_nav_Producto.No)
                            BeINavConversion = New clsBeI_nav_conversion()
                            CopyObject(UM, BeINavConversion)
                            If BeINavConversion.Qty_per_Unit_of_Measure <> 1 Then
                                lINavConversion.Add(BeINavConversion)
                            End If
                        Next

                        BeI_nav_Producto.lINavConversion = lINavConversion

                    End If

                    lblprg.AppendText(String.Format("Procesando Producto: {0} {1} ", BeI_nav_Producto.No, vbNewLine))
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    If BeI_nav_Producto.No = "00640004" Then
                        Debug.Print("00640004")
                    End If
                    clsLnI_nav_producto.Insertar_Con_Presentacion(BeI_nav_Producto,
                                                                  BeI_nav_Producto.lINavConversion,
                                                                  lConnection,
                                                                  lTransaction)

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               beprodr.No,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet,
                                                               cnnLog)

                    lblprg.AppendText("####### Error #######")
                    lblprg.AppendText(ex.Message)
                    lblprg.AppendText("Ref: " & beprodr.No)
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Application.DoEvents()

                End Try

            Next

            lTransaction.Commit()

            Importar_Productos_DesdeWSNav_A_TablaIntermedia = True

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       MethodBase.GetCurrentMethod.Name(),
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       cnnLog)

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            lblprg.AppendText("####### Error #######")
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Function Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                             ByRef prg As Windows.Forms.ProgressBar,
                                                                             Optional ByVal ForzarEjecucion As Boolean = False,
                                                                             Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Producto") Then

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

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            lblprg.AppendText(String.Format("Conectando a BD: {0} Sever: {1}", BD.Instancia.NombreBD, BD.Instancia.Server))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'lblprg.AppendText("Iniciando transacción a BD: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText("Consultando WebService de producto en: " & My.MySettings.Default.DynamicsNavInterface_wsProductos_Productos_Service)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Productos_DesdeWSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde WS?", "Parámetro productos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Productos_DesdeWSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            Dim lProductosFromNav As New List(Of clsBeI_nav_producto)

            lblprg.AppendText("Consultando productos en tabla intermedia ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lProductosFromNav = clsLnI_nav_producto.GetAll(lConnection,
                                                           lTransaction)

            lblprg.AppendText(String.Format("Productos en tabla intermedia: {0}", lProductosFromNav.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If lProductosFromNav.Count > 0 Then

                Dim BeUnidMed As New clsBeUnidad_medida
                Dim BeProductoExistente As clsBeProducto = Nothing
                Dim BeProductoBodega As clsBeProducto_bodega = Nothing
                Dim BeProductoPresentacion As New clsBeProducto_Presentacion()
                Dim BeProductoPresentacionExistente As New clsBeProducto_Presentacion()
                Dim vIdTipoProductoConformado As String = ""
                Dim BeProducto As clsBeProducto = Nothing
                Dim vSegLote As String = ""
                Dim vCodClasificacion As String = ""

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                              lConnection,
                                                              lTransaction)

                If BeConfigEnc Is Nothing Then
                    If BD.Instancia.IdConfiguracionInterface = 0 Then
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                    Else
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                    End If
                End If

                prg.Maximum = lProductosFromNav.Count

                Dim vContador As Integer = 0

                prg.Value = 0

                lblprg.AppendText("********** TRASLADANDO DOCUMENTO A TOMWMS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                For Each navProducto As clsBeI_nav_producto In lProductosFromNav

                    lblprg.AppendText(String.Format("Transportando a WMS producto: {0} ", navProducto.No))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    BeProducto = New clsBeProducto
                    BeProductoExistente = New clsBeProducto
                    BeProductoExistente = clsLnProducto.Existe(navProducto.No,
                                                               lConnection,
                                                               lTransaction)

                    vIdTipoProductoConformado = navProducto.Item_Category_Code & navProducto.Product_Group_Code
                    vCodClasificacion = navProducto.Item_Category_Code

                    BeProducto.IdClasificacion = clsLnProducto_clasificacion.Get_IdClasificacion_By_Codigo(vCodClasificacion,
                                                                                                           lConnection,
                                                                                                           lTransaction)

                    vSegLote = IIf(IsDBNull(navProducto.Item_Tracking_Code), "", navProducto.Item_Tracking_Code)

                    '#EJC20171107_REF06_0323AM: Validar que IdClasificacion existe en producto_clasificacion, sino dará error por FK
                    If Not clsLnProducto_clasificacion.Exists(BeProducto.IdClasificacion,
                                                              lConnection,
                                                              lTransaction) Then

                        '#EJC20171107_REF09_0354AM: Leer la categoría del producto desde el WS e insertarlo en producto_clasificacion
                        Dim SyncNavCP As New clsSyncNavCategoriasProducto
                        Dim CP As New Categorias_Productos
                        Dim vIdClasificacion As Integer = 0
                        Dim vNombreClas As String = ""
                        Dim vCodigoClasificacion As String = ""

                        If navProducto.Item_Category_Code <> "" Then

                            CP = SyncNavCP.Get_Categoria_Producto_FromWS(navProducto.Item_Category_Code)

                            If Not CP Is Nothing Then
                                vIdClasificacion = clsLnProducto_clasificacion.MaxId(lConnection, lTransaction)
                                vNombreClas = CP.Description
                                vCodigoClasificacion = CP.Code
                            Else
                                vIdClasificacion = 0
                                vNombreClas = "NODEF"
                                vCodClasificacion = navProducto.Item_Category_Code
                            End If

                        Else
                            vIdClasificacion = 0
                            vNombreClas = "NODEF"
                            vCodClasificacion = ""
                        End If

                        Dim BeProductoClas As New clsBeProducto_clasificacion() With {.IdClasificacion = vIdClasificacion,
                            .Nombre = vNombreClas,
                            .Sistema = False,
                            .IsNew = True,
                            .Activo = True,
                            .Fec_agr = Now,
                            .Fec_mod = Now,
                            .User_agr = BeConfigEnc.IdUsuario,
                            .User_mod = BeConfigEnc.IdUsuario,
                            .Codigo = vCodClasificacion}

                        BeProductoClas.Propietario.IdPropietario = BeConfigEnc.IdPropietario

                        Try

                            clsLnProducto_clasificacion.Insertar(BeProductoClas,
                                                                 lConnection,
                                                                 lTransaction)

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProducto.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                            lblprg.AppendText(String.Format("Error: La clasificación no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", navProducto.Item_Category_Code, navProducto.Product_Group_Code, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            Application.DoEvents()

                        End Try

                    End If

                    '#EJC20171107_REF10_0400AM: Leer el grupo del producto desde el WS e insertarlo en producto_tipo
                    Dim SyncNavGP As New clsSyncNavGruposProducto
                    Dim GP As New Grupos_Productos
                    Dim vNombreGP As String = ""
                    Dim vIdTipoProductoExistentePorNombre As Integer = 0

                    Try

                        vIdTipoProductoConformado = navProducto.Item_Category_Code & navProducto.Product_Group_Code

                        If Not clsLnProducto_tipo.Exists_By_IdTipoProducto(vIdTipoProductoConformado,
                                                                           lConnection,
                                                                           lTransaction) Then

                            If navProducto.Item_Category_Code <> "" AndAlso navProducto.Product_Group_Code <> "" Then
                                GP = SyncNavGP.Get_Grupo_Producto_FromWS(navProducto.Item_Category_Code,
                                                                         navProducto.Product_Group_Code)
                            Else
                                GP = Nothing
                            End If

                            If Not GP Is Nothing AndAlso vNombreGP = "" Then
                                vNombreGP = GP.Description
                            End If

                            If Not clsLnProducto_tipo.Exists_By_Nombre(vNombreGP,
                                                                       lConnection,
                                                                       lTransaction,
                                                                       vIdTipoProductoExistentePorNombre) Then

                                Dim BeProductoTipo As New clsBeProducto_tipo()
                                Dim vNombreTipo As String = ""

                                If Not GP Is Nothing Then
                                    vNombreTipo = GP.Description
                                Else
                                    vNombreTipo = "NODEF"
                                End If

                                With BeProductoTipo
                                    .IdTipoProducto = vIdTipoProductoConformado
                                    .IdPropietario = BeConfigEnc.IdPropietario
                                    .NombreTipoProducto = vNombreTipo
                                    .Activo = True
                                    .User_agr = BeConfigEnc.IdUsuario
                                    .Fec_agr = Now
                                    .User_mod = BeConfigEnc.IdUsuario
                                    .Fec_mod = Now
                                End With

                                Try

                                    clsLnProducto_tipo.Insertar(BeProductoTipo,
                                                                lConnection,
                                                                lTransaction)

                                    BeProducto.IdTipoProducto = BeProductoTipo.IdTipoProducto

                                    lblprg.AppendText(String.Format("El tipo de producto no existía y se insertó.: {0}{1}", BeProductoTipo.IdTipoProducto, vbNewLine))
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                                               BeProducto.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                    lblprg.AppendText(String.Format("Error: El tipo de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", navProducto.Item_Category_Code, navProducto.Product_Group_Code, vbNewLine))
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                    Application.DoEvents()

                                End Try

                            Else
                                BeProducto.IdTipoProducto = vIdTipoProductoExistentePorNombre
                            End If

                        Else

                            Dim BeTipoProducto As New clsBeProducto_tipo()
                            BeTipoProducto = clsLnProducto_tipo.Get_Single_By_IdTipoProducto(vIdTipoProductoConformado,
                                                                                             lConnection,
                                                                                             lTransaction)

                            If Not BeTipoProducto Is Nothing Then
                                vNombreGP = BeTipoProducto.NombreTipoProducto
                            End If

                            BeProducto.IdTipoProducto = BeTipoProducto.IdTipoProducto

                        End If

                    Catch ex As Exception

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProducto.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                        lblprg.AppendText(String.Format("Error: El tipo de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", navProducto.Item_Category_Code, navProducto.Product_Group_Code, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        GP = Nothing

                        Application.DoEvents()

                    End Try

                    '#CKFK20220326: Leer el grupo del producto desde el WS e insertarlo en familia
                    Dim SyncNavGPF As New clsSyncNavGruposProducto
                    Dim GPF As New Grupos_Productos
                    Dim vIdFamilia As Integer = 0
                    Dim vCodigoFamila As String = ""
                    Dim BeProductoFamilia As New clsBeProducto_familia()
                    Dim vNombreFamilia As String = ""

                    Try

                        vCodigoFamila = navProducto.Product_Group_Code

                        vIdFamilia = clsLnProducto_familia.Get_IdFamilia_By_Codigo(vCodigoFamila,
                                                                                   lConnection,
                                                                                   lTransaction)

                        If vIdFamilia = 0 Then

                            If navProducto.Item_Category_Code <> "" AndAlso navProducto.Product_Group_Code <> "" Then
                                GPF = SyncNavGP.Get_Grupo_Producto_FromWS(navProducto.Item_Category_Code,
                                                                          navProducto.Product_Group_Code)
                            Else
                                GPF = Nothing
                            End If

                            vIdFamilia = clsLnProducto_familia.MaxId(lConnection, lTransaction)

                            If Not GPF Is Nothing Then
                                vNombreFamilia = GPF.Description
                                vCodigoFamila = GPF.Code
                            Else
                                vNombreFamilia = "NODEF"
                                vCodigoFamila = ""
                            End If

                            With BeProductoFamilia
                                .IdFamilia = vIdFamilia
                                .Nombre = vNombreFamilia
                                .Propietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario,
                                                                           lConnection,
                                                                           lTransaction)
                                .Activo = True
                                .User_agr = BeConfigEnc.IdUsuario
                                .Fec_agr = Now
                                .User_mod = BeConfigEnc.IdUsuario
                                .Fec_mod = Now
                                .Codigo = vCodigoFamila
                            End With

                            Try

                                clsLnProducto_familia.Insertar(BeProductoFamilia,
                                                               lConnection,
                                                               lTransaction)

                                BeProducto.IdFamilia = BeProductoFamilia.IdFamilia

                                lblprg.AppendText(String.Format("La familia de producto no existía y se insertó.: {0}{1}", BeProductoFamilia.IdFamilia, vbNewLine))
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                   BeProducto.Codigo,
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet, CnnLog)

                                lblprg.AppendText(String.Format("Error: El tipo de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", navProducto.Item_Category_Code, navProducto.Product_Group_Code, vbNewLine))
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                                Application.DoEvents()

                            End Try


                        Else

                            BeProducto.IdFamilia = vIdFamilia

                        End If

                    Catch ex As Exception

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProducto.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                        lblprg.AppendText(String.Format("Error: El tipo de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", navProducto.Item_Category_Code, navProducto.Product_Group_Code, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        GP = Nothing

                        Application.DoEvents()

                    End Try

                    BeUnidMed.Nombre = navProducto.Base_Unit_Of_Measure

                    'Valida si existe la UM por el nombre/código que viene de Nav
                    BeUnidMed = clsLnUnidad_medida.Existe_By_Nombre(BeUnidMed.Nombre,
                                                                    lConnection,
                                                                    lTransaction)

                    'Si existe devuelve un objeto del tipo Unidad_Medida que contiene el Id.
                    If Not BeUnidMed Is Nothing Then
                        BeProducto.IdUnidadMedidaBasica = BeUnidMed.IdUnidadMedida
                    Else

                        BeUnidMed = New clsBeUnidad_medida
                        BeUnidMed.IdUnidadMedida = clsLnUnidad_medida.MaxID(lConnection, lTransaction) + 1
                        BeUnidMed.Nombre = navProducto.Base_Unit_Of_Measure
                        BeUnidMed.IdPropietario = BeConfigEnc.IdPropietario
                        BeUnidMed.Activo = True
                        BeUnidMed.User_agr = BeConfigEnc.IdUsuario
                        BeUnidMed.User_mod = BeConfigEnc.IdUsuario
                        BeUnidMed.Fec_agr = Now
                        BeUnidMed.Fec_mod = Now

                        Try

                            clsLnUnidad_medida.InsertarFromInterface(BeUnidMed,
                                                                     lConnection,
                                                                     lTransaction)

                            'Despues que se inserta, se asigna el id de unidad de medida al producto
                            BeProducto.IdUnidadMedidaBasica = BeUnidMed.IdUnidadMedida
                            lblprg.AppendText(String.Format("Nueva unidad de medida insertada: {0}", BeUnidMed.Nombre))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                                       BeProducto.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet,
                                                                       CnnLog)

                            lblprg.AppendText(String.Format("Error: La unidad de medida no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", navProducto.Item_Category_Code, navProducto.Product_Group_Code, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            Application.DoEvents()

                        End Try

                    End If

                    BeProducto.Codigo = navProducto.No
                    BeProducto.IdTipoRotacion = BeConfigEnc.IdTipoRotacion
                    BeProducto.IdTipoEtiqueta = BeConfigEnc.IdTipoEtiqueta
                    BeProducto.Precio = navProducto.Unit_Cost
                    BeProducto.Nombre = Truncate(String.Format("{0} - {1}", navProducto.Description, navProducto.Description_2), 100)
                    BeProducto.Codigo_barra = navProducto.No
                    BeProducto.Existencia_min = 0
                    BeProducto.Existencia_max = 0
                    BeProducto.ExistenciaUMBas = navProducto.Inventory
                    BeProducto.Costo = 0 '#EJC20220502 Ricardo pidió de forma expedita, que esta información no se importe ni se revele (navProducto.Unit_Cost)
                    BeProducto.Activo = True
                    BeProducto.Serializado = False
                    BeProducto.Control_vencimiento = (vSegLote <> "")
                    BeProducto.Control_lote = (vSegLote <> "")
                    BeProducto.Peso_recepcion = False
                    BeProducto.Peso_despacho = False
                    BeProducto.Temperatura_recepcion = False
                    BeProducto.Temperatura_despacho = False
                    BeProducto.Materia_prima = False
                    BeProducto.Kit = False
                    BeProducto.Tolerancia = False
                    BeProducto.Ciclo_vida = False
                    BeProducto.User_agr = BeConfigEnc.IdUsuario
                    BeProducto.Fec_agr = Now
                    BeProducto.User_mod = BeConfigEnc.IdUsuario
                    BeProducto.Fec_mod = Now

                    If Not BeProductoExistente Is Nothing Then

                        Try

                            BeProducto.IdProducto = BeProductoExistente.IdProducto
                            BeProducto.User_mod = BeConfigEnc.IdUsuario
                            BeProducto.Fec_mod = Now
                            BeProducto.IdTipoRotacion = BeProductoExistente.IdTipoRotacion
                            BeProducto.IdIndiceRotacion = BeProductoExistente.IdIndiceRotacion
                            BeProducto.Largo = BeProductoExistente.Largo
                            BeProducto.Alto = BeProductoExistente.Alto
                            BeProducto.Ancho = BeProductoExistente.Ancho
                            BeProducto.Ancho = BeProductoExistente.Ancho
                            BeProducto.Control_peso = BeProductoExistente.Control_peso '#CKFK 20180817 Agregué este campo para que si el producto ya lo tiene definido no lo modifique
                            BeProducto.Precio = navProducto.Unit_Cost
                            BeProducto.Control_vencimiento = (vSegLote <> "")
                            BeProducto.Control_lote = (vSegLote <> "")
                            BeProducto.Nombre = Truncate(String.Format("{0} - {1}", navProducto.Description, navProducto.Description_2), 100)

                            If clsLnProducto.Actualizar(BeProducto,
                                                        lConnection,
                                                        lTransaction) > 0 Then

                                'Presentaciones
                                If Not navProducto.lINavConversion Is Nothing Then

                                    For Each UM In navProducto.lINavConversion

                                        If Not UM.Qty_per_Unit_of_Measure = 1 Then

                                            BeProductoPresentacion = New clsBeProducto_Presentacion()
                                            BeProductoPresentacion.IdProducto = BeProducto.IdProducto
                                            BeProductoPresentacion.Imprime_barra = True
                                            BeProductoPresentacion.Activo = True
                                            BeProductoPresentacion.Codigo_barra = UM.Item_No
                                            BeProductoPresentacion.Factor = UM.Qty_per_Unit_of_Measure
                                            BeProductoPresentacion.Nombre = UM.Code
                                            BeProductoPresentacion.Peso = UM.Weight
                                            BeProductoPresentacion.Largo = UM.Length
                                            BeProductoPresentacion.Ancho = UM.Width
                                            BeProductoPresentacion.Alto = UM.Height
                                            BeProductoPresentacion.Codigo = BeProducto.Codigo

                                            BeProductoPresentacionExistente = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo_And_Nombre(UM.Item_No,
                                                                                                                                                  UM.Code,
                                                                                                                                                  lConnection,
                                                                                                                                                  lTransaction)

                                            If BeProductoPresentacionExistente Is Nothing Then

                                                BeProductoPresentacion.IdPresentacion = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1
                                                clsLnProducto_presentacion.Insertar(BeProductoPresentacion,
                                                                                    lConnection,
                                                                                    lTransaction)

                                            Else
                                                BeProductoPresentacion.IdPresentacion = BeProductoPresentacionExistente.IdPresentacion
                                                clsLnProducto_presentacion.Actualizar_Valores_Relativos(BeProductoPresentacion,
                                                                                                        lConnection,
                                                                                                        lTransaction)
                                            End If

                                        End If

                                    Next

                                End If


                                If Not clsLnProducto_bodega.Exist(BeProducto.Codigo,
                                                                  lConnection,
                                                                  lTransaction) Then

                                    '#EJC20180110: Si el producto existe, pero no existe en producto_bodega, se inserta en productobodega.
                                    BeProductoBodega = New clsBeProducto_bodega
                                    BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction) + 1
                                    BeProductoBodega.IdProducto = BeProducto.IdProducto
                                    BeProductoBodega.IdBodega = BeConfigEnc.Idbodega
                                    BeProductoBodega.Activo = True
                                    BeProductoBodega.User_agr = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                                    BeProductoBodega.User_mod = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                                    BeProductoBodega.Fec_agr = Now
                                    BeProductoBodega.Fec_mod = Now

                                    Try

                                        clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega,
                                                                                   lConnection,
                                                                                   lTransaction)

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                   BeProducto.Codigo,
                                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                   BeConfigDet.Idnavconfigdet,
                                                                                   CnnLog)

                                        lblprg.AppendText(String.Format("Error: No se pudo insertar el producto bodega: {0} Error: {1} {2}", navProducto.No, ex.Message, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        Application.DoEvents()

                                    End Try

                                End If

                                VContadorBitacoraTomims += 1

                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProducto.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet,
                                                                       CnnLog)

                            lblprg.AppendText(String.Format("Error: No se pudo actualizar el producto: {0} {1}", navProducto.No, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            Application.DoEvents()

                        End Try

                        prg.Value = vContador

                        vContador += 1

                    Else

                        BeProducto.IdProducto = clsLnProducto.MaxID(lConnection, lTransaction) + 1

                        If Not BeConfigEnc Is Nothing Then
                            BeProducto.IdPropietario = BeConfigEnc.IdPropietario
                        End If

                        Try

                            clsLnProducto.Insertar(BeProducto, lConnection, lTransaction)

                            BeProductoBodega = New clsBeProducto_bodega
                            BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction) + 1
                            BeProductoBodega.IdProducto = BeProducto.IdProducto
                            BeProductoBodega.IdBodega = BeConfigEnc.Idbodega
                            BeProductoBodega.Activo = True
                            BeProductoBodega.User_agr = BeConfigEnc.IdUsuario
                            BeProductoBodega.User_mod = BeConfigEnc.IdUsuario
                            BeProductoBodega.Fec_agr = Now
                            BeProductoBodega.Fec_mod = Now

                            clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega,
                                                                       lConnection,
                                                                       lTransaction)

                            'Presentaciones
                            If Not navProducto.lINavConversion Is Nothing Then

                                For Each UM In navProducto.lINavConversion.FindAll(Function(x) x.Item_No = BeProducto.Codigo)

                                    If Not UM.Qty_per_Unit_of_Measure = 1 Then 'Si no es la UMBas

                                        BeProductoPresentacion = New clsBeProducto_Presentacion()
                                        BeProductoPresentacion.IdProducto = BeProducto.IdProducto
                                        BeProductoPresentacion.Imprime_barra = True
                                        BeProductoPresentacion.Activo = True
                                        BeProductoPresentacion.Codigo_barra = UM.Item_No
                                        BeProductoPresentacion.Factor = UM.Qty_per_Unit_of_Measure
                                        BeProductoPresentacion.Nombre = UM.Code
                                        BeProductoPresentacion.Peso = UM.Weight
                                        BeProductoPresentacion.Largo = UM.Length
                                        BeProductoPresentacion.Ancho = UM.Width
                                        BeProductoPresentacion.Alto = UM.Height
                                        BeProductoPresentacion.Codigo = BeProductoBodega.Producto.Codigo

                                        BeProductoPresentacionExistente = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo_And_Nombre(UM.Item_No, UM.Code, lConnection, lTransaction)

                                        If BeProductoPresentacionExistente Is Nothing Then

                                            BeProductoPresentacion.IdPresentacion = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1
                                            clsLnProducto_presentacion.Insertar(BeProductoPresentacion, lConnection, lTransaction)

                                        Else
                                            clsLnProducto_presentacion.Actualizar_Valores_Relativos(BeProductoPresentacion,
                                                                                                    lConnection,
                                                                                                    lTransaction)
                                        End If

                                    End If

                                Next

                            End If

                            VContadorBitacoraTomims += 1

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProducto.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                            lblprg.AppendText("Error al insertar producto: " & BeProducto.Codigo & vbNewLine &
                                              ex.Message)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            Application.DoEvents()

                        End Try

                        Application.DoEvents()

                        prg.Value = vContador

                        vContador += 1

                    End If

                Next

            End If

            lTransaction.Commit()

            '#EJC20171107_REF08_0342AM: Desplegar cantidad de registros de productos procesados
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TOMWMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Productos procesados correctamente: {0}", VContadorBitacoraTomims))
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

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            prg.Value = 0
            lblprg.AppendText(String.Format("Error al insertar producto a tabla DE TOMWMS: {0} {1}", ex.Message, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Public Shared Function Truncate(value As String, length As Integer) As String
        If length > value.Length Then
            Return value
        Else
            Return value.Substring(0, length)
        End If
    End Function

    Public Shared Function Importar_Productos_DesdeWSNav_A_TablaIntermedia(ByVal pCodigoProducto As String,
                                                                            ByVal lblprg As RichTextBox,
                                                                            ByRef prg As Windows.Forms.ProgressBar,
                                                                            ByRef cnnLog As SqlConnection) As Boolean

        Importar_Productos_DesdeWSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** INSERTANDO PRODUCTO NO ENCONTRADO DE ORDEN DE COMPRA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim Prod As Productos = Get_Single_Producto_FromWS(pCodigoProducto)

            If Not Prod Is Nothing Then

                Application.DoEvents()

                Dim vContador As Integer = 0

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                Dim BeI_nav_Producto As clsBeI_nav_producto

                Try

                    BeI_nav_Producto = New clsBeI_nav_producto
                    BeI_nav_Producto.No = Prod.No
                    BeI_nav_Producto.Description = Prod.Description
                    BeI_nav_Producto.Description_2 = Prod.Description_2
                    BeI_nav_Producto.Inventory = Prod.Inventory
                    BeI_nav_Producto.Base_Unit_Of_Measure = Prod.Base_Unit_of_Measure
                    BeI_nav_Producto.Unit_Cost = Prod.Unit_Cost
                    BeI_nav_Producto.Inventory_Posting_Group = Prod.Inventory_Posting_Group
                    BeI_nav_Producto.Gen_Prod_Posting_Group = Prod.Gen_Prod_Posting_Group
                    BeI_nav_Producto.Search_Description = Prod.Search_Description
                    BeI_nav_Producto.Item_Category_Code = Prod.Item_Category_Code
                    BeI_nav_Producto.Product_Group_Code = Prod.Product_Group_Code
                    BeI_nav_Producto.Sales_Unit = Prod.Sales_Unit_of_Measure
                    BeI_nav_Producto.Item_Tracking_Code = Prod.Item_Tracking_Code

                    lblprg.AppendText(String.Format("Procesando Producto: {0} {1} ", BeI_nav_Producto.No, vbNewLine))
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

#Region "Insertar Producto en WMS"

                    Dim BeUnidMed As New clsBeUnidad_medida
                    Dim BeProductoExistente As clsBeProducto = Nothing
                    Dim BeProductoBodega As clsBeProducto_bodega = Nothing
                    Dim BeProducto As clsBeProducto = Nothing
                    Dim vSegLote As String = ""
                    Dim VContadorBitacoraTomims As Integer = 0

                    BeProducto = New clsBeProducto
                    BeProductoExistente = New clsBeProducto
                    BeProductoExistente = clsLnProducto.Existe(pCodigoProducto, lConnection, lTransaction)

                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

                    If BeConfigEnc Is Nothing Then
                        If BD.Instancia.IdConfiguracionInterface = 0 Then
                            Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                        Else
                            Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                        End If
                    End If

                    BeProducto.IdClasificacion = IIf(BeI_nav_Producto.Item_Category_Code = "", 0, BeI_nav_Producto.Item_Category_Code)

                    vSegLote = IIf(IsDBNull(BeI_nav_Producto.Item_Tracking_Code), "", BeI_nav_Producto.Item_Tracking_Code)

                    '#EJC20171107_REF06_0323AM: Validar que IdClasificacion existe en producto_clasificacion, sino dará error por FK
                    If Not clsLnProducto_clasificacion.Exists(BeProducto.IdClasificacion, lConnection, lTransaction) Then

                        '#EJC20171107_REF09_0354AM: Leer la categoría del producto desde el WS e insertarlo en producto_clasificacion
                        Dim SyncNavCP As New clsSyncNavCategoriasProducto
                        Dim CP As New Categorias_Productos
                        Dim vIdClasificacion As Integer = 0
                        Dim vNombreClas As String = ""

                        If BeI_nav_Producto.Item_Category_Code <> "" Then

                            CP = SyncNavCP.Get_Categoria_Producto_FromWS(BeI_nav_Producto.Item_Category_Code)

                            If Not CP Is Nothing Then
                                vIdClasificacion = CP.Code
                                vNombreClas = CP.Description
                            Else
                                vIdClasificacion = BeI_nav_Producto.Item_Category_Code
                                vNombreClas = "NODEF"
                            End If

                        Else
                            vIdClasificacion = 0
                            vNombreClas = "NODEF"
                        End If

                        Dim BeProductoClas As New clsBeProducto_clasificacion() With {.IdClasificacion = vIdClasificacion,
                            .Nombre = vNombreClas,
                            .Sistema = False,
                            .IsNew = True,
                            .Activo = True,
                            .Fec_agr = Now,
                            .Fec_mod = Now,
                            .User_agr = BeConfigEnc.IdUsuario,
                            .User_mod = BeConfigEnc.IdUsuario}
                        BeProductoClas.Propietario.IdPropietario = BeConfigEnc.IdPropietario

                        Try

                            clsLnProducto_clasificacion.Insertar(BeProductoClas, lConnection, lTransaction)

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProducto.Codigo,
                                                                       0,
                                                                       0, cnnLog)

                            lblprg.AppendText(String.Format("Error: La clasificación no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeI_nav_Producto.Item_Category_Code, BeI_nav_Producto.Product_Group_Code, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            Application.DoEvents()

                        End Try

                    End If

                    '#EJC20171107_REF10_0400AM: Leer el grupo del producto desde el WS e insertarlo en producto_tipo
                    Dim SyncNavGP As New clsSyncNavGruposProducto
                    Dim GP As New Grupos_Productos

                    Try

                        If BeI_nav_Producto.Item_Category_Code <> "" AndAlso BeI_nav_Producto.Product_Group_Code <> "" Then
                            GP = SyncNavGP.Get_Grupo_Producto_FromWS(BeI_nav_Producto.Item_Category_Code, BeI_nav_Producto.Product_Group_Code)
                        Else
                            GP = Nothing
                        End If

                    Catch ex As Exception

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProducto.Codigo,
                                                                       0,
                                                                       0, cnnLog)

                        lblprg.AppendText(String.Format("Error: El tipo de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeI_nav_Producto.Item_Category_Code, BeI_nav_Producto.Product_Group_Code, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        GP = Nothing

                        Application.DoEvents()

                    End Try

                    '#EJC20171107_REF07_0324AM: Validar que IdTipoProducto existe en producto_tipo, sino dará error por FK
                    BeProducto.IdTipoProducto = IIf(BeI_nav_Producto.Product_Group_Code = "", 0, BeI_nav_Producto.Product_Group_Code)

                    Dim vNombreGP As String = ""

                    If Not GP Is Nothing Then
                        vNombreGP = GP.Description
                    End If

                    If Not clsLnProducto_tipo.Exists_By_Nombre(vNombreGP, lConnection, lTransaction) Then

                        Dim ObjN As New clsBeProducto_tipo()
                        Dim vNombreTipo As String = ""

                        If Not GP Is Nothing Then
                            vNombreTipo = GP.Description
                        Else
                            vNombreTipo = "NODEF"
                        End If

                        With ObjN
                            .IdTipoProducto = BeProducto.IdClasificacion & BeI_nav_Producto.Product_Group_Code 'BeProducto.IdTipoProducto
                            .IdPropietario = BeConfigEnc.IdPropietario
                            .NombreTipoProducto = vNombreTipo
                            .Activo = True
                            .User_agr = BeConfigEnc.IdUsuario
                            .Fec_agr = Now
                            .User_mod = BeConfigEnc.IdUsuario
                            .Fec_mod = Now
                        End With

                        Try

                            clsLnProducto_tipo.Insertar(ObjN, lConnection, lTransaction)

                            BeProducto.IdTipoProducto = ObjN.IdTipoProducto

                            lblprg.AppendText(String.Format("El tipo de producto no existía y se insertó.: {0}{1}", ObjN.IdTipoProducto, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                           BeProducto.Codigo,
                                           0,
                                           0, cnnLog)

                            lblprg.AppendText(String.Format("Error: El tipo de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeI_nav_Producto.Item_Category_Code, BeI_nav_Producto.Product_Group_Code, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            Application.DoEvents()

                        End Try

                    Else

                        BeProducto.IdTipoProducto = BeProducto.IdClasificacion & BeI_nav_Producto.Product_Group_Code 'BeProducto.IdTipoProducto

                        If Not clsLnProducto_tipo.Exists_By_IdTipoProducto(BeProducto.IdTipoProducto, lConnection, lTransaction) Then

                            Dim ObjN As New clsBeProducto_tipo()
                            Dim vNombreTipo As String = ""

                            If Not GP Is Nothing Then
                                vNombreTipo = GP.Description
                            Else
                                vNombreTipo = "NODEF"
                            End If

                            With ObjN
                                .IdTipoProducto = BeProducto.IdClasificacion & BeI_nav_Producto.Product_Group_Code 'BeProducto.IdTipoProducto
                                .IdPropietario = BeConfigEnc.IdPropietario
                                .NombreTipoProducto = vNombreTipo
                                .Activo = True
                                .User_agr = BeConfigEnc.IdUsuario
                                .Fec_agr = Now
                                .User_mod = BeConfigEnc.IdUsuario
                                .Fec_mod = Now
                            End With

                            Try

                                clsLnProducto_tipo.Insertar(ObjN, lConnection, lTransaction)

                                BeProducto.IdTipoProducto = ObjN.IdTipoProducto

                                lblprg.AppendText(String.Format("El tipo de producto no existía y se insertó.: {0}{1}", ObjN.IdTipoProducto, vbNewLine))
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                               BeProducto.Codigo,
                                               0,
                                               0, cnnLog)

                                lblprg.AppendText(String.Format("Error: El tipo de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeI_nav_Producto.Item_Category_Code, BeI_nav_Producto.Product_Group_Code, vbNewLine))
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                                Application.DoEvents()

                            End Try

                        End If

                    End If

                    BeUnidMed.Nombre = BeI_nav_Producto.Base_Unit_Of_Measure

                    'Valida si existe la UM por el nombre que viene de Nav
                    BeUnidMed = clsLnUnidad_medida.Existe_By_Nombre(BeUnidMed.Nombre, lConnection, lTransaction)

                    'Valida si existe la UM por el código que viene de Nav
                    If BeUnidMed Is Nothing Then
                        BeUnidMed = New clsBeUnidad_medida
                        BeUnidMed.Nombre = BeI_nav_Producto.Base_Unit_Of_Measure
                        BeUnidMed = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(BeUnidMed.Nombre,
                                                                                          BeConfigEnc.IdPropietario,
                                                                                          lConnection,
                                                                                          lTransaction)
                    End If

                    lblprg.AppendText(String.Format("Procesando producto: {0}", BeI_nav_Producto.No))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    'Si existe devuelve un objeto del tipo Unidad_Medida que contiene el Id.
                    If Not BeUnidMed Is Nothing Then
                        BeProducto.IdUnidadMedidaBasica = BeUnidMed.IdUnidadMedida
                    Else
                        BeUnidMed = New clsBeUnidad_medida
                        'Si no existe se llena un objeto para su inserción
                        BeUnidMed.IdUnidadMedida = clsLnUnidad_medida.MaxID(lConnection, lTransaction) + 1
                        BeUnidMed.Nombre = BeI_nav_Producto.Base_Unit_Of_Measure
                        BeUnidMed.IdPropietario = BeConfigEnc.IdPropietario 'Esto debería ser parametrizable?
                        BeUnidMed.Activo = True
                        BeUnidMed.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                        BeUnidMed.User_mod = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                        BeUnidMed.Fec_agr = Now
                        BeUnidMed.Fec_mod = Now

                        Try

                            clsLnUnidad_medida.InsertarFromInterface(BeUnidMed, lConnection, lTransaction)

                            'Despues que se inserta, se asigna el id de unidad de medida al producto
                            BeProducto.IdUnidadMedidaBasica = BeUnidMed.IdUnidadMedida
                            lblprg.AppendText(String.Format("Nueva unidad de medida insertada: {0}", BeUnidMed.Nombre))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                                       BeProducto.Codigo,
                                                                       0,
                                                                       0, cnnLog)

                            lblprg.AppendText(String.Format("Error: La unidad de medida no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeI_nav_Producto.Item_Category_Code, BeI_nav_Producto.Product_Group_Code, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            Application.DoEvents()

                        End Try

                    End If

                    BeProducto.Codigo = BeI_nav_Producto.No
                    BeProducto.IdTipoRotacion = BeConfigEnc.IdTipoRotacion
                    BeProducto.Precio = BeI_nav_Producto.Unit_Cost
                    BeProducto.Nombre = Truncate(String.Format("{0} - {1}", BeI_nav_Producto.Description, BeI_nav_Producto.Description_2), 100)
                    BeProducto.Codigo_barra = BeI_nav_Producto.No
                    BeProducto.Existencia_min = 0
                    BeProducto.Existencia_max = 0
                    BeProducto.ExistenciaUMBas = BeI_nav_Producto.Inventory
                    BeProducto.Costo = 0 '#EJC20220502 Ricardo pidió de forma expedita, que esta información no se importe ni se revele (BeI_nav_Producto)
                    BeProducto.Activo = True
                    BeProducto.Serializado = False
                    BeProducto.Control_vencimiento = (vSegLote <> "")
                    BeProducto.Control_lote = (vSegLote <> "")
                    BeProducto.Peso_recepcion = False
                    BeProducto.Peso_despacho = False
                    BeProducto.Temperatura_recepcion = False
                    BeProducto.Temperatura_despacho = False
                    BeProducto.Materia_prima = False
                    BeProducto.Kit = False
                    BeProducto.Tolerancia = False
                    BeProducto.Ciclo_vida = False
                    BeProducto.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeProducto.Fec_agr = Now
                    BeProducto.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeProducto.Fec_mod = Now

                    If Not BeProductoExistente Is Nothing Then

                        Try

                            BeProducto.IdProducto = BeProductoExistente.IdProducto
                            BeProducto.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                            BeProducto.Fec_mod = Now
                            BeProducto.IdTipoRotacion = BeProductoExistente.IdTipoRotacion
                            BeProducto.IdIndiceRotacion = BeProductoExistente.IdIndiceRotacion
                            BeProducto.Largo = BeProductoExistente.Largo
                            BeProducto.Alto = BeProductoExistente.Alto
                            BeProducto.Ancho = BeProductoExistente.Ancho
                            BeProducto.Ancho = BeProductoExistente.Ancho
                            BeProducto.Control_peso = BeProductoExistente.Control_peso '#CKFK 20180817 Agregué este campo para que si el producto ya lo tiene definido no lo modifique
                            BeProducto.Precio = BeI_nav_Producto.Unit_Cost
                            BeProducto.Control_vencimiento = (vSegLote <> "")
                            BeProducto.Control_lote = (vSegLote <> "")
                            BeProducto.Nombre = Truncate(String.Format("{0} - {1}", BeI_nav_Producto.Description, BeI_nav_Producto.Description_2), 100)

                            If clsLnProducto.Actualizar(BeProducto, lConnection, lTransaction) > 0 Then

                                If Not clsLnProducto_bodega.Exist(BeProducto.Codigo, lConnection, lTransaction) Then

                                    '#EJC20180110: Si el producto existe, pero no existe en producto_bodega, se inserta en productobodega.
                                    BeProductoBodega = New clsBeProducto_bodega
                                    BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction) + 1
                                    BeProductoBodega.IdProducto = BeProducto.IdProducto
                                    BeProductoBodega.IdBodega = BeConfigEnc.Idbodega
                                    BeProductoBodega.Activo = True
                                    BeProductoBodega.User_agr = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                                    BeProductoBodega.User_mod = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                                    BeProductoBodega.Fec_agr = Now
                                    BeProductoBodega.Fec_mod = Now

                                    Try

                                        clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, lConnection, lTransaction)

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                    BeProducto.Codigo,
                                                                    0,
                                                                    0, cnnLog)

                                        lblprg.AppendText(String.Format("Error: No se pudo insertar el producto bodega: {0} {1}", BeI_nav_Producto.No, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        Application.DoEvents()

                                    End Try

                                End If

                                VContadorBitacoraTomims += 1

                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProducto.Codigo,
                                                                       0,
                                                                       0, cnnLog)

                            lblprg.AppendText(String.Format("Error: No se pudo actualizar el producto: {0} {1}", BeI_nav_Producto.No, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            Application.DoEvents()

                            Throw ex

                        End Try

                        prg.Value = vContador

                        vContador += 1

                    Else

                        BeProducto.IdProducto = clsLnProducto.MaxID(lConnection, lTransaction) + 1

                        If Not BeConfigEnc Is Nothing Then
                            BeProducto.IdPropietario = BeConfigEnc.IdPropietario
                        End If

                        Try

                            clsLnProducto.Insertar(BeProducto, lConnection, lTransaction)

                            BeProductoBodega = New clsBeProducto_bodega
                            BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction) + 1
                            BeProductoBodega.IdProducto = BeProducto.IdProducto
                            BeProductoBodega.IdBodega = BeConfigEnc.Idbodega
                            BeProductoBodega.Activo = True
                            BeProductoBodega.User_agr = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                            BeProductoBodega.User_mod = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                            BeProductoBodega.Fec_agr = Now
                            BeProductoBodega.Fec_mod = Now

                            clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, lConnection, lTransaction)

                            VContadorBitacoraTomims += 1

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProducto.Codigo,
                                                                       0,
                                                                       0, cnnLog)

                            lblprg.AppendText("Error al insertar producto: " & BeProducto.Codigo & vbNewLine &
                                              ex.Message)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            Application.DoEvents()

                            Throw ex

                        End Try

                        Application.DoEvents()

                        prg.Value = vContador

                        vContador += 1

                    End If

#End Region

                    Importar_Productos_DesdeWSNav_A_TablaIntermedia = True

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                   pCodigoProducto,
                                                                   0,
                                                                   0, cnnLog)

                    lblprg.AppendText("####### Error #######")
                    lblprg.AppendText(ex.Message)
                    lblprg.AppendText("Ref: " & pCodigoProducto)
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Application.DoEvents()

                    Throw ex

                End Try

            Else

                lblprg.AppendText(String.Format("No se pudo obtener el producto: {0} desde el WebService. {1} ", pCodigoProducto, vbNewLine))
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                clsLnI_nav_ejecucion_det_error.Inserta_Log((String.Format("No se pudo obtener el producto: {0} desde el WebService.", pCodigoProducto)),
                                                              MethodBase.GetCurrentMethod.Name(),
                                                              0,
                                                              0, cnnLog)

            End If

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN DE PRODUCTO EN WMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               MethodBase.GetCurrentMethod.Name(),
                                                               0,
                                                               0, cnnLog)
            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            lblprg.AppendText("####### Error #######")
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Private Shared Function Get_Single_Producto_FromWS(ByVal pCodigoProducto As String) As Productos

        Get_Single_Producto_FromWS = Nothing

        Try

            Dim fichaProductos() As Productos
            Dim lfichaProductos As New List(Of Productos)

            Dim vFiltro1 As New Productos_Filter() With {.Field = Productos_Fields.No, .Criteria = pCodigoProducto}
            Dim vFiltros As Productos_Filter() = New Productos_Filter() {vFiltro1}
            Dim CredencialesConexion As New NetworkCredential With
              {.Domain = "byb",
              .UserName = My.Settings.usuariows,
              .Password = My.Settings.clavews}

            Dim wsProdService As New Productos_Service() With
            {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = My.MySettings.Default.DynamicsNavInterface_wsProductos_Productos_Service
            }

            fichaProductos = wsProdService.ReadMultiple(vFiltros, Nothing, 0)

            If fichaProductos.Length > 0 Then
                Return fichaProductos(0)
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class