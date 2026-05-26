Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class clsSyncSAPProducto : Inherits clsInterfaceBase
    Implements IDisposable

    Private Shared VContadorBitacoraTOMWMS As Integer = 0
    Private Shared VContadorBitacoraIntermedia As Integer = 0
    Shared vHanaService As SapServiceLayerClient

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Private Shared Async Function Importar_Productos_Desde_SAP_A_TablaIntermediaAsync(ByVal lblprg As RichTextBox,
                                                                                     ByVal prg As ProgressBar,
                                                                                     ByVal cnnLog As SqlConnection,
                                                                                      Optional codigo As String = "") As Task(Of Boolean)

        Dim Cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing
        Dim RegistrosNoEncontrados As Boolean = False

        Try

            Dim vHanaService As New SapServiceLayerClient
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return False
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Sesión iniciada correctamente.")

            Dim lfichaProductos As New List(Of clsBeI_nav_producto)
            lfichaProductos = Await Get_Productos_SAP_SL(vHanaService.SessionCookie, SapServiceLayerClient.baseUrl, lblprg, codigo)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando bodegas en SAP (OWHS).")

            Application.DoEvents()

            If Not lfichaProductos Is Nothing Then

                prg.Maximum = lfichaProductos.Count

                Dim vContador As Integer = 0

                Cnn.Open() : lTrans = Cnn.BeginTransaction(IsolationLevel.ReadUncommitted)

                clsLnI_nav_producto.EliminarTodos(Cnn, lTrans)
                clsLnI_nav_producto_presentacion.EliminarTodos(Cnn, lTrans)

                BeNavEjecucionRes.Registros_ws = lfichaProductos.Count()

                clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

                If lfichaProductos.Count > 0 Then

                    RegistrosNoEncontrados = True

                    For Each singleProduct In lfichaProductos

                        Try

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} {1} ", singleProduct.No, vbNewLine))

                            clsLnI_nav_producto.Insertar(singleProduct, Cnn, lTrans)

                            VContadorBitacoraIntermedia += 1

                            prg.Value = vContador

                            vContador += 1

                            Application.DoEvents()

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                   singleProduct.No,
                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                   BeConfigDet.Idnavconfigdet, cnnLog)

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al procesar producto: {0} {1} ", singleProduct.No, ex.Message))

                            Application.DoEvents()

                        End Try

                    Next

                Else
                    clsPublic.Actualizar_Progreso(lblprg, "No se encontraron productos pendientes de procesar (Enviado_SAP =No) en SAP.")
                End If

                lTrans.Commit()

                If RegistrosNoEncontrados Then
                    Return True
                End If

                clsPublic.Actualizar_Progreso(lblprg, "Fin de procesamiento de productos -> " & Now)

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No se encontraron productos pendientes de procesar (Enviado_SAP =No) en SAP.")
            End If

            Return True

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       MethodBase.GetCurrentMethod.Name(),
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            If Not lTrans Is Nothing Then lTrans.Rollback()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de procesamiento de productos -> " & Now)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: {0} ", ex.Message))

            Throw ex

        Finally
            If Cnn.State = ConnectionState.Open Then Cnn.Close()
        End Try

    End Function

    Public Shared Function Truncate(value As String, length As Integer) As String
        If length > value.Length Then
            Return value
        Else
            Return value.Substring(0, length)
        End If
    End Function

    Private Shared Function MapRowToProducto(row As DataRow) As clsBeI_nav_producto
        Return New clsBeI_nav_producto With {
            .No = row("ItemCode").ToString(),
            .Item_Tracking_Code = row("CodeBars").ToString(),
            .Description = row("ItemName").ToString(),
            .Description_2 = row("CodigoBarra").ToString(),
            .Item_Category_Code = row("CodigoDivision").ToString(),
            .Item_Category_Name = row("NombreDivision").ToString(),
            .Inventory = 0,
            .Base_Unit_Of_Measure = row("UmBas").ToString(),
            .Unit_Cost = 0,
            .Product_Group_Code = row("CodigoFabricante").ToString(),
            .Producto_Group_Name = row("NombreFabricante").ToString(),
            .Gen_Prod_Posting_Group = row("CodFamilia").ToString(),
            .Gen_Prod_Posting_Name = row("NomFamilia").ToString(),
            .Product_Class_Code = row("CodGrupo").ToString(),
            .Product_Class_Name = row("Grupo").ToString(),
            .BatchControl = False
        }
    End Function

    Public Shared Async Function Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByVal lblprg As RichTextBox,
                                                                                          ByVal prg As ProgressBar,
                                                                                          Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                          Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                                                          Optional codigo As String = "") As Task(Of Boolean)

        Dim CnnInterface As SqlConnection = Nothing
        Dim CnnLog As SqlConnection = Nothing
        Dim lTrans As SqlTransaction = Nothing

        Try

            ' Inicializar conexiones
            CnnInterface = New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
            CnnLog = New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

            CnnLog.Open()
            Iniciar_Ejecucion(CnnLog)

            CnnInterface.Open()
            lTrans = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            ' Confirmar y llenar tabla intermedia
            If Not Await Confirmar_Y_Llenar_Intermedia(Pregunta_Si_LLena_Intermedia, lblprg, prg, CnnLog, codigo) Then
                Return False
            End If

            ' Obtener y procesar productos
            Dim productos As List(Of clsBeI_nav_producto) = clsLnI_nav_producto.GetAll()
            clsPublic.Actualizar_Progreso(lblprg, $"Productos en tabla intermedia: {productos.Count}")

            If productos.Count > 0 Then
                ProcesarProductosDesdeSAP(productos, lblprg, prg, CnnInterface, lTrans)
            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay productos para procesar en la tabla intermedia.")
            End If

            ' Confirmar transacción
            lTrans.Commit()

            Finalizar_Ejecucion(lblprg, CnnLog, "Productos procesados correctamente: ")

            Return True

        Catch ex As Exception
            ' Manejo de errores
            If lTrans IsNot Nothing Then
                lTrans.Rollback()
            End If

            prg.Value = 0
            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar producto a tabla de TOMWMS: {ex.Message}")

            Throw New Exception($"Error en inserción de productos a TOMWMS: {ex.Message}", ex)
        Finally
            ' Limpieza de recursos
            If lTrans IsNot Nothing Then
                lTrans.Dispose()
            End If

            If CnnInterface IsNot Nothing Then
                If CnnInterface.State = ConnectionState.Open Then
                    CnnInterface.Close()
                End If
                CnnInterface.Dispose()
            End If

            If CnnLog IsNot Nothing Then
                If CnnLog.State = ConnectionState.Open Then
                    CnnLog.Close()
                End If
                CnnLog.Dispose()
            End If
        End Try
    End Function

    Private Shared Function ProcesarProductosDesdeSAP(productos As List(Of clsBeI_nav_producto),
                                                      lblprg As RichTextBox,
                                                      prg As ProgressBar,
                                                      lConnection As SqlConnection,
                                                      lTransaction As SqlTransaction) As Boolean

        ' Obtener configuración
        BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

        Dim vMostrarProgreso As Boolean = (lblprg IsNot Nothing AndAlso prg IsNot Nothing)
        Dim productosProcesados As Integer = 0
        Dim productosConError As Integer = 0

        If vMostrarProgreso Then
            prg.Maximum = productos.Count
            prg.Value = 0
        End If

        ' Procesar cada producto
        For Each productoSAP In productos
            Try
                If vMostrarProgreso Then
                    clsPublic.Actualizar_Progreso(lblprg, $"Procesando producto: {productoSAP.No}")
                End If

                ' Verificar si el producto existe
                Dim productoExistente = clsLnProducto.Existe(productoSAP.No, lConnection, lTransaction)
                Dim producto = InicializarProductoDesdeSAP(productoSAP, productoExistente)

                ' Enlazar entidades relacionadas
                EnlazarEntidadRelacionada(productoSAP, producto, lblprg)

                ' Insertar o actualizar producto
                If productoExistente IsNot Nothing Then
                    ActualizarProductoExistente(producto, productoExistente, lblprg)
                Else
                    InsertarProductoNuevo(producto, lblprg)
                End If

                Dim marcadoOk As Boolean = Marcar_Producto_Sincronizado_SL(productoSAP.No, lblprg)

                If Not marcadoOk Then
                    ' Aquí puedes decidir si lo cuentas como error
                    productosConError += 1
                Else
                    productosProcesados += 1
                End If

                If vMostrarProgreso Then
                    prg.Value += 1
                End If

            Catch ex As Exception
                productosConError += 1
                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, productoSAP.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)

                If vMostrarProgreso Then
                    clsPublic.Actualizar_Progreso(lblprg, $"Error en producto {productoSAP.No}: {ex.Message}")
                Else
                    ' En modo sin progreso, lanzar la excepción para manejo superior
                    Throw New Exception($"Error procesando producto {productoSAP.No}: {ex.Message}", ex)
                End If
            End Try
        Next

        ' Resumen del procesamiento
        If vMostrarProgreso Then
            clsPublic.Actualizar_Progreso(lblprg, $"Procesamiento completado: {productosProcesados} exitosos, {productosConError} con errores")
        End If

        Return productosConError = 0
    End Function

    Private Shared Sub Iniciar_Ejecucion(cnnLog As SqlConnection)
        BeNavEjecucionEnc = New clsBeI_nav_ejecucion_enc With {
            .IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface,
            .Fecha = Now
        }
        '#EJCCKFK20260520: Cambio por Identity en tabla.
        clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, cnnLog)

        BeNavEjecucionRes = New clsBeI_nav_ejecucion_res With {
            .IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc,
            .IdNavConfigDet = BeConfigDet.Idnavconfigdet,
            .Registros_ws = 0,
            .Registros_ti = 0,
            .Registros_WMS = 0,
            .Exitosa = False
        }
        '#EJCCKFK20260520: Cambio por Identity en tabla.
        clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, cnnLog)
    End Sub

    Private Shared Async Function Confirmar_Y_Llenar_Intermedia(preguntar As Boolean,
                                                                lblprg As RichTextBox,
                                                                prg As ProgressBar,
                                                                cnnLog As SqlConnection,
                                                                Optional codigo As String = "") As Task(Of Boolean)
        If Not preguntar Then
            Return Await Importar_Productos_Desde_SAP_A_TablaIntermediaAsync(lblprg, prg, cnnLog, codigo)
        End If

        Dim respuesta = MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If respuesta = DialogResult.Yes Then
            Return Await Importar_Productos_Desde_SAP_A_TablaIntermediaAsync(lblprg, prg, cnnLog, codigo)
        End If

        Return True
    End Function

    Private Shared Function InicializarProductoDesdeSAP(productoSAP As clsBeI_nav_producto, productoExistente As clsBeProducto) As clsBeProducto
        Dim producto As New clsBeProducto With {
            .Codigo = productoSAP.No,
            .Nombre = Truncate(productoSAP.Description, 150),
            .Codigo_barra = productoSAP.Description_2,
            .ExistenciaUMBas = productoSAP.Inventory,
            .Precio = productoSAP.Unit_Cost,
            .Costo = productoSAP.Unit_Cost,
            .Activo = True,
            .Serializado = False,
            .Control_vencimiento = productoSAP.BatchControl,
            .Control_lote = BeConfigEnc.Control_lote,
            .User_agr = BeConfigEnc.IdUsuario,
            .Fec_agr = Now,
            .User_mod = BeConfigEnc.IdUsuario,
            .Fec_mod = Now,
            .IdTipoEtiqueta = BeConfigEnc.IdTipoEtiqueta,
            .Genera_lp = BeConfigEnc.Genera_lp,
            .IdIndiceRotacion = BeConfigEnc.IdIndiceRotacion,
            .IdTipoManufactura = Val(productoSAP.Manufacturing_Process),
            .IdTipoRotacion = If(productoSAP.BatchControl, 3, 1)
        }
        Return producto
    End Function

    Private Shared Sub EnlazarEntidadRelacionada(productoSAP As clsBeI_nav_producto,
                                                 ByRef producto As clsBeProducto,
                                                 lbl As RichTextBox)
        EnlazarClasificacion(productoSAP, producto, lbl)
        EnlazarTipoProducto(productoSAP, producto, lbl)
        EnlazarMarca(productoSAP, producto, lbl)
        EnlazarFamilia(productoSAP, producto, lbl)
        EnlazarUnidadMedida(productoSAP, producto, lbl)
    End Sub

    Private Shared Sub EnlazarClasificacion(productoSAP As clsBeI_nav_producto,
                                            ByRef producto As clsBeProducto,
                                            cnn As SqlConnection,
                                            tran As SqlTransaction,
                                            lbl As RichTextBox)

        Dim codigoClas As String = If(String.IsNullOrWhiteSpace(productoSAP.Item_Category_Code), "0", productoSAP.Item_Category_Code)
        If Not IsNumeric(codigoClas) OrElse codigoClas = "0" Then
            producto.IdClasificacion = 0
            Return
        End If

        Try
            Dim clasif = clsLnProducto_clasificacion.Get_Single_By_Codigo(codigoClas, cnn, tran)
            If clasif IsNot Nothing Then
                producto.IdClasificacion = clasif.IdClasificacion
            Else
                Dim nuevaClasif As New clsBeProducto_clasificacion With {
                    .IdClasificacion = clsLnProducto_clasificacion.MaxId(cnn, tran),
                    .Codigo = codigoClas,
                    .Nombre = productoSAP.Item_Category_Name,
                    .Activo = True,
                    .Sistema = False,
                    .IsNew = True,
                    .Fec_agr = Now,
                    .Fec_mod = Now,
                    .User_agr = BeConfigEnc.IdUsuario,
                    .User_mod = BeConfigEnc.IdUsuario
                }
                nuevaClasif.Propietario.IdPropietario = BeConfigEnc.IdPropietario

                clsLnProducto_clasificacion.Insertar(nuevaClasif, cnn, tran)
                producto.IdClasificacion = nuevaClasif.IdClasificacion
                clsPublic.Actualizar_Progreso(lbl, $"Insertada nueva clasificación: {nuevaClasif.IdClasificacion}")
            End If
        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, producto.Codigo, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lbl, $"Error al insertar clasificación {codigoClas}: {ex.Message}")
        End Try
    End Sub

    Private Shared Sub EnlazarTipoProducto(productoSAP As clsBeI_nav_producto,
                                            ByRef producto As clsBeProducto,
                                            lbl As RichTextBox)

        Dim codigoTipo As String = If(String.IsNullOrWhiteSpace(productoSAP.Product_Group_Code), "", productoSAP.Product_Group_Code)
        If Not IsNumeric(codigoTipo) AndAlso codigoTipo = "" Then
            producto.IdTipoProducto = 0
            Return
        End If

        Try
            Dim tipo = clsLnProducto_tipo.Get_Single_By_Codigo(codigoTipo)
            If tipo IsNot Nothing Then
                producto.IdTipoProducto = tipo.IdTipoProducto
            Else
                Dim nuevoTipo As New clsBeProducto_tipo With {
                    .IdTipoProducto = clsLnProducto_tipo.MaxID(),
                    .IdPropietario = BeConfigEnc.IdPropietario,
                    .Codigo = codigoTipo,
                    .NombreTipoProducto = productoSAP.Producto_Group_Name,
                    .Activo = True,
                    .User_agr = BeConfigEnc.IdUsuario,
                    .Fec_agr = Now,
                    .User_mod = BeConfigEnc.IdUsuario,
                    .Fec_mod = Now
                }

                clsLnProducto_tipo.Insertar(nuevoTipo)
                producto.IdTipoProducto = nuevoTipo.IdTipoProducto
                clsPublic.Actualizar_Progreso(lbl, $"Insertado nuevo tipo producto: {nuevoTipo.IdTipoProducto}")
            End If
        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, producto.Codigo, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lbl, $"Error al insertar tipo producto {codigoTipo}: {ex.Message}")
        End Try
    End Sub

    Private Shared Sub EnlazarMarca(productoSAP As clsBeI_nav_producto,
                                     ByRef producto As clsBeProducto,
                                     lbl As RichTextBox)
        Dim codigoMarca As String = If(String.IsNullOrWhiteSpace(productoSAP.Gen_Prod_Posting_Group), "0", productoSAP.Gen_Prod_Posting_Group)
        If Not IsNumeric(codigoMarca) OrElse codigoMarca = "0" Then
            producto.IdMarca = 0
            Return
        End If

        Try
            Dim marca = clsLnProducto_marca.Get_Single_By_Codigo(codigoMarca)
            If marca IsNot Nothing Then
                producto.IdMarca = marca.IdMarca
            Else
                Dim nuevaMarca As New clsBeProducto_marca With {
                    .IdMarca = clsLnProducto_marca.MaxID(),
                    .IdPropietario = BeConfigEnc.IdPropietario,
                    .Codigo = codigoMarca,
                    .Nombre = productoSAP.Gen_Prod_Posting_Name,
                    .Activo = True,
                    .User_agr = BeConfigEnc.IdUsuario,
                    .Fec_agr = Now,
                    .User_mod = BeConfigEnc.IdUsuario,
                    .Fec_mod = Now
                }

                clsLnProducto_marca.Insertar(nuevaMarca)
                producto.IdMarca = nuevaMarca.IdMarca
                clsPublic.Actualizar_Progreso(lbl, $"Insertada nueva marca: {nuevaMarca.IdMarca}")
            End If
        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, producto.Codigo, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lbl, $"Error al insertar marca {codigoMarca}: {ex.Message}")
        End Try
    End Sub

    Private Shared Sub EnlazarFamilia(productoSAP As clsBeI_nav_producto,
                                      ByRef producto As clsBeProducto,
                                      lbl As RichTextBox)
        Dim codigoFamilia As String = If(String.IsNullOrWhiteSpace(productoSAP.Product_Class_Code), "0", productoSAP.Product_Class_Code)
        If Not IsNumeric(codigoFamilia) OrElse codigoFamilia = "0" Then
            producto.IdFamilia = 0
            Return
        End If

        Try
            Dim familia = clsLnProducto_familia.Get_Single_By_Codigo(codigoFamilia)
            If familia IsNot Nothing Then
                producto.IdFamilia = familia.IdFamilia
            Else
                Dim nuevaFamilia As New clsBeProducto_familia With {
                    .IdFamilia = clsLnProducto_familia.MaxId(),
                    .Codigo = codigoFamilia,
                    .Nombre = productoSAP.Product_Class_Name,
                    .Activo = True,
                    .IsNew = True,
                    .Fec_agr = Now,
                    .Fec_mod = Now,
                    .User_agr = BeConfigEnc.IdUsuario,
                    .User_mod = BeConfigEnc.IdUsuario
                }
                nuevaFamilia.Propietario.IdPropietario = BeConfigEnc.IdPropietario

                clsLnProducto_familia.Insertar(nuevaFamilia)
                producto.IdFamilia = nuevaFamilia.IdFamilia
                clsPublic.Actualizar_Progreso(lbl, $"Insertada nueva familia: {nuevaFamilia.IdFamilia}")
            End If
        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, producto.Codigo, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lbl, $"Error al insertar familia {codigoFamilia}: {ex.Message}")
        End Try
    End Sub

    Private Shared Sub EnlazarUnidadMedida(productoSAP As clsBeI_nav_producto,
                                           ByRef producto As clsBeProducto,
                                           lbl As RichTextBox)
        Dim nombreUM As String = productoSAP.Base_Unit_Of_Measure

        If String.IsNullOrWhiteSpace(nombreUM) Then
            clsPublic.Actualizar_Progreso(lbl, $"ERROR: No definió la UMBas para el producto: {productoSAP.No}. No se importará.")
            producto.IdUnidadMedidaBasica = 0
            Return
        End If

        Try
            Dim unidad = clsLnUnidad_medida.Existe_By_Nombre(nombreUM)
            If unidad IsNot Nothing Then
                producto.IdUnidadMedidaBasica = unidad.IdUnidadMedida
            Else
                Dim nuevaUM As New clsBeUnidad_medida With {
                    .IdUnidadMedida = clsLnUnidad_medida.MaxID() + 1,
                    .Nombre = nombreUM,
                    .Codigo = nombreUM,
                    .IdPropietario = BeConfigEnc.IdPropietario,
                    .Activo = True,
                    .User_agr = BeConfigEnc.IdUsuario,
                    .User_mod = BeConfigEnc.IdUsuario,
                    .Fec_agr = Now,
                    .Fec_mod = Now
                }

                clsLnUnidad_medida.InsertarFromInterface(nuevaUM)
                producto.IdUnidadMedidaBasica = nuevaUM.IdUnidadMedida
                clsPublic.Actualizar_Progreso(lbl, $"Insertada nueva unidad de medida: {nombreUM}")
            End If
        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, producto.Codigo, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lbl, $"Error al insertar unidad de medida {nombreUM}: {ex.Message}")
        End Try
    End Sub

    Public Shared Sub Finalizar_Ejecucion(lbl As RichTextBox, cnnLog As SqlConnection, resumen As String)
        Try
            clsPublic.Actualizar_Progreso(lbl, "Fin de inserción en TOMWMS.")
            clsPublic.Actualizar_Progreso(lbl, resumen & $": {VContadorBitacoraTOMWMS}")
            clsPublic.Actualizar_Progreso(lbl, $"Tiempo transcurrido: {DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)} segundo(s)")

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS
            BeNavEjecucionRes.Exitosa = (VContadorBitacoraTOMWMS = VContadorBitacoraIntermedia)

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lbl, $"Error al finalizar ejecución: {ex.Message}")
        End Try
    End Sub

    Private Shared Function ActualizarProductoExistente(producto As clsBeProducto,
                                                        productoExistente As clsBeProducto,
                                                        lbl As RichTextBox) As Boolean
        Try
            ' Preparar producto para actualización
            PrepararProductoParaActualizacion(producto, productoExistente)

            ' Actualizar producto principal
            Dim filasAfectadas As Integer = clsLnProducto.Actualizar(producto)

            If filasAfectadas > 0 Then
                ' Gestionar relación con bodega
                GestionarProductoBodegaAsync(producto, lbl)

                '' Actualizar contadores y sincronización
                'Dim usado = Marcar_Producto_Sincronizad0_SLAsync(producto.Codigo, lbl)

                clsPublic.Actualizar_Progreso(lbl, $"Producto {producto.Codigo} actualizado correctamente")
                Return True
            Else
                clsPublic.Actualizar_Progreso(lbl, $"No se pudo actualizar el producto {producto.Codigo}")
                Return False
            End If

        Catch ex As Exception
            ManejarErrorActualizacionAsync(ex, producto.Codigo, lbl)
            Return False
        End Try
    End Function

    ' Métodos auxiliares para modularizar la funcionalidad
    Private Shared Sub PrepararProductoParaActualizacion(producto As clsBeProducto, productoExistente As clsBeProducto)
        producto.IdProducto = productoExistente.IdProducto
        producto.Largo = productoExistente.Largo
        producto.Ancho = productoExistente.Ancho
        producto.Alto = productoExistente.Alto
        producto.Control_peso = productoExistente.Control_peso
        producto.IdTipoRotacion = productoExistente.IdTipoRotacion
        producto.IdIndiceRotacion = productoExistente.IdIndiceRotacion
        producto.User_mod = BeConfigEnc.IdUsuario
        producto.Fec_mod = DateTime.Now
    End Sub

    Private Shared Sub GestionarProductoBodegaAsync(producto As clsBeProducto,
                                                    lbl As RichTextBox)
        Dim existeEnBodega As Boolean = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(producto.Codigo, BeConfigEnc.Idbodega)

        If Not existeEnBodega Then
            Dim productoBodega As clsBeProducto_bodega = CrearProductoBodega(producto)
            clsLnProducto_bodega.InsertarFromInterface(productoBodega)
            clsPublic.Actualizar_Progreso(lbl, $"Producto {producto.Codigo} asociado a bodega {BeConfigEnc.Idbodega}")
        End If
    End Sub

    Private Shared Function CrearProductoBodega(producto As clsBeProducto) As clsBeProducto_bodega
        Return New clsBeProducto_bodega With {
        .IdProductoBodega = clsLnProducto_bodega.MaxID() + 1,
        .IdProducto = producto.IdProducto,
        .IdBodega = BeConfigEnc.Idbodega,
        .Activo = True,
        .User_agr = BeConfigEnc.IdUsuario,
        .User_mod = BeConfigEnc.IdUsuario,
        .Fec_agr = DateTime.Now,
        .Fec_mod = DateTime.Now
    }
    End Function

    Private Shared Sub ManejarErrorActualizacionAsync(ex As Exception, codigoProducto As String, lbl As RichTextBox)
        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, codigoProducto, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
        clsPublic.Actualizar_Progreso(lbl, $"Error al actualizar producto {codigoProducto}: {ex.Message}")
    End Sub

    Private Shared Function InsertarProductoNuevo(producto As clsBeProducto,
                                                  lbl As RichTextBox) As Integer
        InsertarProductoNuevo = 0
        Try

            producto.IdProducto = clsLnProducto.MaxID() + 1
            producto.IdPropietario = BeConfigEnc.IdPropietario
            clsLnProducto.Insertar(producto)

            Dim bodegas = clsLnBodega.GetAll()
            For Each bodega In bodegas
                Dim productoBodega As New clsBeProducto_bodega With {
                    .IdProductoBodega = clsLnProducto_bodega.MaxID() + 1,
                    .IdProducto = producto.IdProducto,
                    .IdBodega = bodega.IdBodega,
                    .Activo = True,
                    .User_agr = BeConfigEnc.IdUsuario,
                    .User_mod = BeConfigEnc.IdUsuario,
                    .Fec_agr = Now,
                    .Fec_mod = Now
                }
                clsLnProducto_bodega.InsertarFromInterface(productoBodega)
                If Not lbl Is Nothing Then clsPublic.Actualizar_Progreso(lbl, $"Asociado producto {producto.Codigo} a bodega {bodega.Codigo}")
            Next

            VContadorBitacoraTOMWMS += 1

            'Marcar_Producto_Sincronizad0_SLAsync(producto.Codigo, lbl)

            InsertarProductoNuevo = producto.IdProducto

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, producto.Codigo, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            If Not lbl Is Nothing Then clsPublic.Actualizar_Progreso(lbl, $"Error al insertar producto nuevo {producto.Codigo}: {ex.Message}")
        End Try

    End Function

    Private Shared Function ProcesarProductosDesdeSAPSingle(productoSAP As clsBeI_nav_producto) As Boolean

        ProcesarProductosDesdeSAPSingle = 0

        BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

        Try

            Dim productoExistente = clsLnProducto.Existe(productoSAP.No)
            Dim producto = InicializarProductoDesdeSAP(productoSAP, productoExistente)

            EnlazarEntidadRelacionada(productoSAP, producto, Nothing)

            If productoExistente IsNot Nothing Then
                ActualizarProductoExistente(producto, productoExistente, Nothing)
                ProcesarProductosDesdeSAPSingle = producto.IdProducto
            Else
                ProcesarProductosDesdeSAPSingle = InsertarProductoNuevo(producto, Nothing)
            End If

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, productoSAP.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            Throw ex
        End Try

    End Function

    Public Shared Function Insertar_Producto_Desde_Tabla_Intermedia_A_Tabla_TOMWMS() As Integer

        Insertar_Producto_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = 0

        Try

            Dim productos As List(Of clsBeI_nav_producto) = clsLnI_nav_producto.GetAll()

            If productos.Count > 0 Then
                Insertar_Producto_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = ProcesarProductosDesdeSAPSingle(productos.FirstOrDefault())
            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Async Function Get_Productos_SAP_SL(sessionCookie As String,
                                                      baseUrl As String,
                                                      lbl As RichTextBox,
                                                      Optional codigo As String = "") As Task(Of List(Of clsBeI_nav_producto))

        Dim productos As New List(Of clsBeI_nav_producto)

        Try
            Dim fechaHasta As Date = Date.Today
            Dim fechaDesde As Date = fechaHasta.AddDays(-BeConfigEnc.Rango_Dias_Importacion)

            Dim fechaDesdeStr As String = fechaDesde.ToString("yyyy-MM-dd")
            Dim fechaHastaStr As String = fechaHasta.ToString("yyyy-MM-dd")

            Dim filtro As New Text.StringBuilder()

            filtro.Append("U_Grupo ne '19' and " &
                     If(codigo <> "", "", " (U_ENVIADO_WMS eq null or U_ENVIADO_WMS eq 2) and ") &
                      " Valid eq 'tYES' and " &
                      " InventoryUOM ne '' ")

            If Not String.IsNullOrWhiteSpace(codigo) Then
                Dim codEscapado = codigo.Replace("'", "''")
                filtro.AppendFormat(" and ItemCode eq '{0}'", codEscapado)
            End If

            Dim filterEncoded = Uri.EscapeDataString(filtro.ToString())
            Dim pageSize As Integer = 100
            Dim skip As Integer = 0

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True
                    client.DefaultRequestHeaders.Add("Cookie", sessionCookie)
                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                    Dim hayMas As Boolean = True

                    While hayMas
                        Dim requestUrl As String =
                        $"{baseUrl}Items?$filter={filterEncoded}&$top={pageSize}&$skip={skip}"

                        Using request As New HttpRequestMessage(HttpMethod.Get, requestUrl)
                            request.Headers.ConnectionClose = True

                            Dim response As HttpResponseMessage = Await client.SendAsync(request).ConfigureAwait(False)

                            If Not response.IsSuccessStatusCode Then
                                Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                                Throw New Exception($"Error al obtener productos desde Service Layer. Código: {response.StatusCode}, Detalle: {errContent}")
                            End If

                            Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Dim obj = JObject.Parse(jsonResponse)
                            Dim rows = obj("value")

                            If rows Is Nothing OrElse Not rows.HasValues Then
                                ' Ya no hay más páginas
                                hayMas = False
                                Exit While
                            End If

                            Dim filasPagina As Integer = rows.Count()

                            For Each row In rows
                                Dim prod As New clsBeI_nav_producto()

                                prod.No = SafeGetString(row, "ItemCode")

                                If lbl IsNot Nothing Then
                                    clsPublic.Actualizar_Progreso(lbl, $"Producto encontrado: {prod.No}")
                                End If

                                prod.Item_Tracking_Code = SafeGetString(row, "BarCode")
                                prod.Description = SafeGetString(row, "ItemName")
                                prod.Description_2 = SafeGetString(row, "BarCode")

                                prod.Item_Category_Code = SafeGetString(row, "U_Division")
                                prod.Product_Group_Code = SafeGetString(row, "U_Fabricante")
                                prod.Gen_Prod_Posting_Group = SafeGetString(row, "U_Marca")
                                prod.Product_Class_Code = SafeGetString(row, "U_Grupo")

                                prod.Base_Unit_Of_Measure = SafeGetString(row, "InventoryUOM")

                                prod.Inventory = 0D
                                prod.Unit_Cost = 0D
                                prod.BatchControl = False

                                prod.Item_Category_Name = String.Empty
                                prod.Producto_Group_Name = String.Empty
                                prod.Gen_Prod_Posting_Name = String.Empty
                                prod.Product_Class_Name = String.Empty

                                Dim inventoryUom As String = SafeGetString(row, "InventoryUOM")
                                Dim salesUnit As String = SafeGetString(row, "SalesUnit")
                                Dim purchaseUnit As String = SafeGetString(row, "PurchaseUnit")

                                Dim uomGroupEntry As Integer = SafeGetInteger(row, "UoMGroupEntry")
                                Dim inventoryUomEntry As Integer = SafeGetInteger(row, "InventoryUoMEntry")
                                Dim defaultSalesUomEntry As Integer = SafeGetInteger(row, "DefaultSalesUoMEntry")
                                Dim defaultPurchasingUomEntry As Integer = SafeGetInteger(row, "DefaultPurchasingUoMEntry")

                                If String.IsNullOrWhiteSpace(inventoryUom) _
                                   OrElse String.IsNullOrWhiteSpace(salesUnit) _
                                   OrElse String.IsNullOrWhiteSpace(purchaseUnit) _
                                   OrElse inventoryUomEntry <= 0 _
                                   OrElse defaultSalesUomEntry <= 0 _
                                   OrElse defaultPurchasingUomEntry <= 0 Then

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(
                                    "Producto rechazado por falta de Unidades de Medida (TN04020)",
                                    prod.No,
                                    BeNavEjecucionEnc.IdEjecucionEnc,
                                    BeConfigDet.Idnavconfigdet)

                                    'Continue For
                                End If

                                If Not clsLnProducto.Existe_Codigo(prod.No) Then
                                    productos.Add(prod)

                                    If lbl IsNot Nothing Then
                                        clsPublic.Actualizar_Progreso(lbl, $"Producto agregado: {prod.No}")
                                    End If

                                End If

                            Next

                            ' Avanzar al siguiente bloque
                            skip += filasPagina
                        End Using
                    End While

                End Using

            End Using

            Return productos

        Catch ex As Exception
            Throw New Exception("Error en Get_Productos_SAP_SL: " & ex.Message, ex)
        End Try
    End Function

    ' Métodos auxiliares para manejo seguro de valores
    Private Shared Function SafeGetString(token As JToken, propertyName As String) As String
        Return If(token?.Value(Of String)(propertyName)?.Trim(), String.Empty)
    End Function

    Private Shared Function SafeGetInteger(token As JToken, propertyName As String) As Integer
        Dim value = token?.Value(Of Integer?)(propertyName)
        Return If(value.HasValue, value.Value, 0)
    End Function

    Private Shared Function SafeGetDecimal(token As JToken, propertyName As String) As Decimal
        Dim value = token?.Value(Of Decimal?)(propertyName)
        Return If(value.HasValue, value.Value, 0D)
    End Function

    Private Shared Function SafeGetDateTime(token As JToken, propertyName As String) As DateTime
        Dim value = token?.Value(Of DateTime?)(propertyName)
        Return If(value.HasValue, value.Value, DateTime.MinValue)
    End Function

    Private Shared Function Marcar_Producto_Sincronizado_SL(ItemCode As String,
                                                            lbl As RichTextBox) As Boolean

        Try
            If String.IsNullOrWhiteSpace(ItemCode) Then Return False

            Dim vHanaService As SapServiceLayerClient

            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lbl, "No se pudo obtener sesión.")
            Else
                clsPublic.Actualizar_Progreso(lbl, "Conexión correcta. Token: " & vHanaService.SessionCookie)
                Debug.WriteLine(vHanaService.SessionCookie)
            End If

            Dim baseUrl As String = SapServiceLayerClient.baseUrl
            Dim sessionCookie As String = vHanaService.SessionCookie

            Dim requestUrl As String = $"Items('{ItemCode}')"
            Dim payload As String = "{""U_ENVIADO_WMS"": 1}"

            Dim httpPatch As New HttpMethod("PATCH")

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(httpPatch, baseUrl & requestUrl)
                        request.Headers.ConnectionClose = True
                        request.Headers.Add("Cookie", sessionCookie)
                        request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                        request.Content = New StringContent(payload, Encoding.UTF8, "application/json")

                        Dim response = client.SendAsync(request).Result

                        If response.IsSuccessStatusCode Then
                            Return True
                        Else
                            Dim errContent = response.Content.ReadAsStringAsync().Result
                            Throw New Exception($"Error al actualizar productos. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try

    End Function

    Public Shared Function Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS() As Integer

        Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = 0

        Try

            Dim productos As List(Of clsBeI_nav_producto) = clsLnI_nav_producto.GetAll()

            If productos.Count > 0 Then
                If ProcesarProductosDesdeSAP(productos, Nothing, Nothing) Then
                    Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = 1
                End If
            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Private Shared Function ProcesarProductosDesdeSAP(productos As List(Of clsBeI_nav_producto),
                                                      lblprg As RichTextBox,
                                                      prg As ProgressBar) As Boolean

        ' Obtener configuración
        BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

        Dim vMostrarProgreso As Boolean = (lblprg IsNot Nothing AndAlso prg IsNot Nothing)
        Dim productosProcesados As Integer = 0
        Dim productosConError As Integer = 0

        If vMostrarProgreso Then
            prg.Maximum = productos.Count
            prg.Value = 0
        End If

        ' Procesar cada producto
        For Each productoSAP In productos
            Try
                If vMostrarProgreso Then
                    clsPublic.Actualizar_Progreso(lblprg, $"Procesando producto: {productoSAP.No}")
                End If

                ' Verificar si el producto existe
                Dim productoExistente = clsLnProducto.Existe(productoSAP.No)
                Dim producto = InicializarProductoDesdeSAP(productoSAP, productoExistente)

                ' Enlazar entidades relacionadas
                EnlazarEntidadRelacionada(productoSAP, producto, lblprg)

                ' Insertar o actualizar producto
                If productoExistente IsNot Nothing Then
                    ActualizarProductoExistente(producto, productoExistente, lblprg)
                Else
                    InsertarProductoNuevo(producto, lblprg)
                End If

                Dim marcadoOk As Boolean = Marcar_Producto_Sincronizado_SL(productoSAP.No, lblprg)

                If Not marcadoOk Then
                    ' Aquí puedes decidir si lo cuentas como error
                    productosConError += 1
                Else
                    productosProcesados += 1
                End If

                If vMostrarProgreso Then
                    prg.Value += 1
                End If

            Catch ex As Exception
                productosConError += 1
                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, productoSAP.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)

                If vMostrarProgreso Then
                    clsPublic.Actualizar_Progreso(lblprg, $"Error en producto {productoSAP.No}: {ex.Message}")
                Else
                    ' En modo sin progreso, lanzar la excepción para manejo superior
                    Throw New Exception($"Error procesando producto {productoSAP.No}: {ex.Message}", ex)
                End If
            End Try
        Next

        ' Resumen del procesamiento
        If vMostrarProgreso Then
            clsPublic.Actualizar_Progreso(lblprg, $"Procesamiento completado: {productosProcesados} exitosos, {productosConError} con errores")
        End If

        Return productosConError = 0
    End Function

    Private Shared Sub EnlazarClasificacion(productoSAP As clsBeI_nav_producto,
                                            ByRef producto As clsBeProducto,
                                            lbl As RichTextBox)

        Dim codigoClas As String = If(String.IsNullOrWhiteSpace(productoSAP.Item_Category_Code), "0", productoSAP.Item_Category_Code)
        If Not IsNumeric(codigoClas) OrElse codigoClas = "0" Then
            producto.IdClasificacion = 0
            Return
        End If

        Try
            Dim clasif = clsLnProducto_clasificacion.Get_Single_By_Codigo(codigoClas)
            If clasif IsNot Nothing Then
                producto.IdClasificacion = clasif.IdClasificacion
            Else
                Dim nuevaClasif As New clsBeProducto_clasificacion With {
                    .IdClasificacion = clsLnProducto_clasificacion.MaxId(),
                    .Codigo = codigoClas,
                    .Nombre = productoSAP.Item_Category_Name,
                    .Activo = True,
                    .Sistema = False,
                    .IsNew = True,
                    .Fec_agr = Now,
                    .Fec_mod = Now,
                    .User_agr = BeConfigEnc.IdUsuario,
                    .User_mod = BeConfigEnc.IdUsuario
                }
                nuevaClasif.Propietario.IdPropietario = BeConfigEnc.IdPropietario

                clsLnProducto_clasificacion.Insertar(nuevaClasif)
                producto.IdClasificacion = nuevaClasif.IdClasificacion
                clsPublic.Actualizar_Progreso(lbl, $"Insertada nueva clasificación: {nuevaClasif.IdClasificacion}")
            End If
        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, producto.Codigo, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lbl, $"Error al insertar clasificación {codigoClas}: {ex.Message}")
        End Try
    End Sub

    Public Shared Async Function Insertar_Producto_From_Sap_HanaAsync(
    ByVal codigo As String,
    lConnection As SqlConnection,
    lTransaction As SqlTransaction,
    SessionCookie As String,
    baseUrl As String,
    lbl As RichTextBox
) As Task(Of Integer)

        Dim registrosNoEncontrados As Boolean = False
        Dim resultado As Integer = 0

        Try
            clsLnI_nav_producto.EliminarTodos(lConnection, lTransaction)
            clsLnI_nav_producto_presentacion.EliminarTodos(lConnection, lTransaction)

            Dim lFichaProducto As List(Of clsBeI_nav_producto) =
            Await Get_Productos_SAP_SL(SessionCookie, baseUrl, lbl, codigo)

            BeNavEjecucionRes.Registros_ws = 1

            If lFichaProducto IsNot Nothing Then
                For Each fichaProducto In lFichaProducto
                    Try
                        registrosNoEncontrados = True
                        clsLnI_nav_producto.Insertar(fichaProducto, lConnection, lTransaction)
                    Catch ex As Exception
                        clsLnI_nav_ejecucion_det_error.Inserta_Log(
                        ex.Message,
                        fichaProducto.No,
                        BeNavEjecucionEnc.IdEjecucionEnc,
                        BeConfigDet.Idnavconfigdet
                    )
                        Application.DoEvents()
                    End Try
                Next

                If registrosNoEncontrados Then
                    resultado = Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS()
                End If
            End If

            Return resultado

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(
            ex.Message,
            MethodBase.GetCurrentMethod.Name(),
            BeNavEjecucionEnc.IdEjecucionEnc,
            BeConfigDet.Idnavconfigdet
        )
            Throw
        End Try
    End Function

    Public Shared Async Function Insertar_Producto_From_Sap_HanaAsync(ByVal codigo As String,
                                                                      SessionCookie As String,
                                                                      baseUrl As String,
                                                                      Optional lbl As RichTextBox = Nothing) As Task(Of Integer)

        Dim RegistrosNoEncontrados As Boolean = False
        Dim resultado As Integer = 0
        Dim vContador As Integer = 0

        Try

            Using lConnection As New SqlConnection((BD.Instancia.CadenaConexionSQLClient))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    clsLnI_nav_producto.EliminarTodos(lConnection, lTransaction)
                    clsLnI_nav_producto_presentacion.EliminarTodos(lConnection, lTransaction)

                    Dim lFichaProducto As List(Of clsBeI_nav_producto) = Await Get_Productos_SAP_SL(SessionCookie, baseUrl, lbl, codigo)

                    BeNavEjecucionRes.Registros_ws = 1

                    If Not lFichaProducto Is Nothing Then

                        For Each fichaProducto In lFichaProducto

                            Try

                                RegistrosNoEncontrados = True

                                clsLnI_nav_producto.Insertar(fichaProducto, lConnection, lTransaction)

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                   fichaProducto.No,
                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                   BeConfigDet.Idnavconfigdet)

                                Application.DoEvents()

                            End Try


                        Next

                        If RegistrosNoEncontrados Then
                            resultado = Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS()
                        End If

                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return resultado > 1

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       MethodBase.GetCurrentMethod.Name(),
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet)

            Throw ex

        End Try

    End Function

End Class
