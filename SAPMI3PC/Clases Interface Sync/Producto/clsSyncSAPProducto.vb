Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM

Public Class clsSyncSAPProducto : Inherits clsInterfaceBase
    Implements IDisposable

    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    '·EJC20240318
    Private oCompany As Company
    Dim sErrMsg As String = ""
    Dim lRetCode, lErrCode As Long
    Private Function Get_Productos_From_SAP() As List(Of clsBeI_nav_producto)

        Get_Productos_From_SAP = Nothing

        Dim lReturnList As New List(Of clsBeI_nav_producto)
        Dim lBodegasWMS As New List(Of clsBeI_nav_bodega)
        Dim query_sap As String = ""
        Dim sCookie As String = Nothing

        Try

            lBodegasWMS = clsLnI_nav_bodega.GetAll()

            Dim StrBodegasInQuery = ""

            If lBodegasWMS.Count = 1 Then
                StrBodegasInQuery = String.Join(",", lBodegasWMS.Select(Function(x) x.Bodega_code))
            Else
                StrBodegasInQuery = String.Join("','", lBodegasWMS.Select(Function(x) x.Bodega_code))
            End If

            query_sap = "SELECT OITM.ItemCode, --0
                                OITM.CodeBars AS CodigoBarra, --1
                                OITM.u_ubicacion AS ItemName, --2
                                OITM.U_molecula AS Clasificacion,--3 
                                OITB.ItmsGrpCod AS CodFamilia, --4 
                                OITB.ItmsGrpNam AS Familia, --5
                                OITM.U_art_marca AS Marca, --6
                                OITM.U_categoria AS TipoProducto, -- 7
                                OITM.U_Um_Prod AS Umbas, --8
                                OITM.U_ENVIADO_WMS, --9
                                OITM.ManBtchNum, --10 
                                OITM.ValidComm --11 
                                FROM OITB LEFT OUTER JOIN
                                OITM ON OITB.ItmsGrpCod = OITM.ItmsGrpCod 
                                WHERE OITM.INVNTITEM = 'Y' 
                                AND OITM.ItemType = 'I' 
                                AND OITM.U_ENVIADO_WMS = 2    
                                AND OITM.VALIDFOR = 'Y' "

            If Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeProducto As clsBeI_nav_producto

                While rs.EoF = False

                    BeProducto = New clsBeI_nav_producto()
                    BeProducto.No = rs.Fields.Item(0).Value.ToString() 'ItemCode
                    BeProducto.Description = rs.Fields.Item(2).Value.ToString() 'ItemName
                    BeProducto.Description_2 = rs.Fields.Item(2).Value.ToString() 'CodeBars
                    BeProducto.Inventory = 0
                    BeProducto.Base_Unit_Of_Measure = rs.Fields.Item(8).Value.ToString()
                    BeProducto.Unit_Cost = 0
                    BeProducto.Item_Category_Code = rs.Fields.Item(4).Value.ToString() 'CODIGO LINEA/FAMILIA
                    BeProducto.Item_Category_Name = rs.Fields.Item(5).Value.ToString() 'NOMBRE LINEA/FAMILIA
                    BeProducto.Gen_Prod_Posting_Group = rs.Fields.Item(6).Value.ToString() 'MARCA MAR0001	MOVESA
                    BeProducto.Gen_Prod_Posting_Name = rs.Fields.Item(6).Value.ToString() 'MARCA MAR0001	MOVESA
                    BeProducto.Product_Group_Code = rs.Fields.Item(7).Value.ToString() 'GRUPO/TIPO - M000000 - MOTO
                    BeProducto.Producto_Group_Name = rs.Fields.Item(7).Value.ToString() 'GRUPO/TIPO - M000000 - MOTO
                    BeProducto.Search_Description = rs.Fields.Item(2).Value.ToString()
                    BeProducto.Sales_Unit = rs.Fields.Item(8).Value.ToString()
                    BeProducto.BatchControl = IIf(rs.Fields.Item(10).Value.ToString() = "Y" OrElse rs.Fields.Item(10).Value.ToString() = "S", True, False)

                    If pConfigInterface = NombreInterface.Becofarma Then
                        BeProducto.Item_Tracking_Code = rs.Fields.Item(1).Value.ToString()
                        BeProducto.Manufacturing_Process = IIf(rs.Fields.Item(11).Value.ToString() = "C/BONO", "1", "0")
                    Else
                        BeProducto.Item_Tracking_Code = rs.Fields.Item(13).Value.ToString()
                    End If

                    lReturnList.Add(BeProducto)

                    rs.MoveNext()

                End While

                Get_Productos_From_SAP = lReturnList

            Else
                Throw New Exception(sErrMsg)
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing
    Private Function Importar_Productos_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                     ByRef prg As ProgressBar,
                                                                     ByRef cnnLog As SqlConnection) As Boolean

        Importar_Productos_Desde_SAP_A_TablaIntermedia = False

        Dim Cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing
        Dim RegistrosNoEncontrados As Boolean = False

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Iniciando procesamiento de productos a tabla intermedia -> " & Now)

            Dim lfichaProductos As New List(Of clsBeI_nav_producto)

            lfichaProductos = Get_Productos_From_SAP()

            Application.DoEvents()

            prg.Maximum = lfichaProductos.Count

            Dim vContador As Integer = 0

            Cnn.Open() : lTrans = Cnn.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_producto.EliminarTodos(Cnn, lTrans)

            BeNavEjecucionRes.Registros_ws = lfichaProductos.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            If lfichaProductos.Count > 0 Then

                RegistrosNoEncontrados = True

                For Each Prod In lfichaProductos

                    Try

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} {1} ", Prod.No, vbNewLine))

                        clsLnI_nav_producto.Insertar(Prod, Cnn, lTrans)

                        VContadorBitacoraIntermedia += 1

                        prg.Value = vContador

                        vContador += 1

                        Application.DoEvents()

                    Catch ex As Exception

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                   Prod.No,
                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                   BeConfigDet.Idnavconfigdet, cnnLog)

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al procesar producto: {0} {1} ", Prod.No, ex.Message))

                        Application.DoEvents()

                    End Try

                Next

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No se encontraron productos pendientes de procesar (Enviado_SAP =No) en SAP.")
            End If

            lTrans.Commit()

            If RegistrosNoEncontrados Then
                Importar_Productos_Desde_SAP_A_TablaIntermedia = True
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Fin de procesamiento de productos -> " & Now)

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

    Public Function Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                             ByRef prg As ProgressBar,
                                                                             Optional ByVal ForzarEjecucion As Boolean = False,
                                                                             Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Producto") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function
                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc =0' 0'0' 0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            CnnInterface.Open() : lTrans = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            BeNavEjecucionRes.IdEjecucionRes = 0'0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Productos_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Productos_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If

                End If

            End If

            Dim lProductosFromSAP As New List(Of clsBeI_nav_producto)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando productos en tabla intermedia ")

            lProductosFromSAP = clsLnI_nav_producto.GetAll(CnnInterface, lTrans)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos en tabla intermedia: {0}", lProductosFromSAP.Count))

            If lProductosFromSAP.Count > 0 Then

                If Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then

                    Dim BeUnidMed As New clsBeUnidad_medida
                    Dim BeProductoExistente As clsBeProducto = Nothing
                    Dim BeProductoBodega As clsBeProducto_bodega = Nothing
                    Dim BeProducto As clsBeProducto = Nothing
                    Dim vSegLote As Boolean = False

                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnInterface, lTrans)

                    prg.Maximum = lProductosFromSAP.Count

                    Dim vContador As Integer = 0

                    prg.Value = 0

                    clsPublic.Actualizar_Progreso(lblprg, "Trasladando producto desde SAP a TOMWMS...")

                    Dim vCodigoClas As String = ""
                    Dim vCodigoGrupo As String = ""
                    Dim vCodigoMarca As String = ""

                    Dim BeClasificacion As New clsBeProducto_clasificacion
                    Dim BeProductoTipo As New clsBeProducto_tipo
                    Dim BeProductoMarca As New clsBeProducto_marca

                    For Each BeSAPProducto As clsBeI_nav_producto In lProductosFromSAP

                        BeProducto = New clsBeProducto
                        BeProductoExistente = New clsBeProducto
                        BeProductoExistente = clsLnProducto.Existe(BeSAPProducto.No, CnnInterface, lTrans)

                        vCodigoClas = IIf(BeSAPProducto.Item_Category_Code = "", 0, BeSAPProducto.Item_Category_Code)

                        If IsNumeric(vCodigoClas) OrElse vCodigoClas <> "" OrElse vCodigoClas <> "0" Then

                            BeClasificacion = clsLnProducto_clasificacion.Get_Single_By_Codigo(vCodigoClas, CnnInterface, lTrans)

                            If Not BeClasificacion Is Nothing Then
                                BeProducto.IdClasificacion = BeClasificacion.IdClasificacion
                            Else

                                Dim vIdClasificacion As Integer = clsLnProducto_clasificacion.MaxId(CnnInterface, lTrans)
                                Dim vNombreClas As String = BeSAPProducto.Item_Category_Name

                                Dim BeProductoClas As New clsBeProducto_clasificacion() With {.IdClasificacion = vIdClasificacion,
                                                                                            .Nombre = vNombreClas,
                                                                                            .Codigo = vCodigoClas,
                                                                                            .Sistema = False,
                                                                                            .IsNew = True,
                                                                                            .Activo = True,
                                                                                            .Fec_agr = Now,
                                                                                            .Fec_mod = Now,
                                                                                            .User_agr = BeConfigEnc.IdUsuario,
                                                                                            .User_mod = BeConfigEnc.IdUsuario}

                                BeProductoClas.Propietario.IdPropietario = BeConfigEnc.IdPropietario

                                Try

                                    clsLnProducto_clasificacion.Insertar(BeProductoClas, CnnInterface, lTrans)

                                    BeProducto.IdClasificacion = BeProductoClas.IdClasificacion

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("La clasificación no existía y se insertó.: {0}{1}", BeProductoClas.IdClasificacion, vbNewLine))

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               BeProducto.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: La clasificación no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeSAPProducto.Item_Category_Code, BeSAPProducto.Product_Group_Code, vbNewLine))

                                    Application.DoEvents()

                                End Try

                            End If


                        Else
                            BeProducto.IdClasificacion = 0
                        End If

                        vSegLote = IIf(IsDBNull(BeSAPProducto.BatchControl), False, BeSAPProducto.BatchControl)

                        vCodigoGrupo = IIf(BeSAPProducto.Product_Group_Code = "", "", BeSAPProducto.Product_Group_Code)

                        If IsNumeric(vCodigoGrupo) OrElse vCodigoGrupo <> "" Then

                            BeProductoTipo = clsLnProducto_tipo.Get_Single_By_Nombre(vCodigoGrupo, CnnInterface, lTrans)

                            If Not BeProductoTipo Is Nothing Then
                                BeProducto.IdTipoProducto = BeProductoTipo.IdTipoProducto
                            Else

                                Dim BeTipoProducto As New clsBeProducto_tipo()
                                Dim vNombreTipo As String = BeSAPProducto.Producto_Group_Name

                                With BeTipoProducto
                                    .IdTipoProducto = clsLnProducto_tipo.MaxID(CnnInterface, lTrans)
                                    .IdPropietario = BeConfigEnc.IdPropietario
                                    .NombreTipoProducto = vNombreTipo
                                    .Codigo = vCodigoGrupo
                                    .Activo = True
                                    .User_agr = BeConfigEnc.IdUsuario
                                    .Fec_agr = Now
                                    .User_mod = BeConfigEnc.IdUsuario
                                    .Fec_mod = Now
                                End With

                                Try

                                    clsLnProducto_tipo.Insertar(BeTipoProducto, CnnInterface, lTrans)

                                    BeProducto.IdTipoProducto = BeTipoProducto.IdTipoProducto

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("El tipo de producto no existía y se insertó.: {0}{1}", BeTipoProducto.IdTipoProducto, vbNewLine))

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                                               BeProducto.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: El tipo de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeSAPProducto.Item_Category_Code, BeSAPProducto.Product_Group_Code, vbNewLine))

                                    Application.DoEvents()

                                End Try

                            End If

                        Else
                            BeProducto.IdTipoProducto = 0
                        End If

                        vCodigoMarca = IIf(BeSAPProducto.Gen_Prod_Posting_Group = "", 0, BeSAPProducto.Gen_Prod_Posting_Group)

                        If IsNumeric(vCodigoMarca) OrElse vCodigoMarca <> "" OrElse vCodigoMarca <> "0" Then

                            Dim BeMarca As New clsBeProducto_marca()
                            Dim vNombreMarca As String = BeSAPProducto.Gen_Prod_Posting_Name
                            vCodigoMarca = vCodigoMarca

                            BeProductoMarca = clsLnProducto_marca.Get_Single_By_Nombre(vCodigoMarca, CnnInterface, lTrans)

                            If Not BeProductoMarca Is Nothing Then
                                BeProducto.IdMarca = BeProductoMarca.IdMarca
                            Else

                                With BeMarca
                                    .IdMarca = clsLnProducto_marca.MaxID(CnnInterface, lTrans)
                                    .IdPropietario = BeConfigEnc.IdPropietario
                                    .Nombre = vNombreMarca
                                    .Activo = True
                                    .User_agr = BeConfigEnc.IdUsuario
                                    .Fec_agr = Now
                                    .User_mod = BeConfigEnc.IdUsuario
                                    .Fec_mod = Now
                                End With

                                Try

                                    clsLnProducto_marca.Insertar(BeMarca, CnnInterface, lTrans)

                                    BeProducto.IdMarca = BeMarca.IdMarca

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("La marca de producto no existía y se insertó.: {0}{1}", BeMarca.IdMarca, vbNewLine))

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                                               BeProducto.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: La marca de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeSAPProducto.Item_Category_Code, BeSAPProducto.Product_Group_Code, vbNewLine))

                                    Application.DoEvents()

                                End Try

                            End If

                        Else
                            BeProducto.IdMarca = 0
                        End If

                        BeUnidMed.Nombre = BeSAPProducto.Base_Unit_Of_Measure

                        If BeUnidMed.Nombre = "" Then
                            clsPublic.Actualizar_Progreso(lblprg, "ERROR_2401310758: No definió la UMBas para el producto: " & BeSAPProducto.No & " No se importará.")
                            Continue For
                        End If

                        'Valida si existe la UM por el nombre/código que viene de Nav
                        BeUnidMed = clsLnUnidad_medida.Existe_By_Nombre(BeUnidMed.Nombre, CnnInterface, lTrans)

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando producto: {0}", BeSAPProducto.No))

                        'Si existe devuelve un objeto del tipo Unidad_Medida que contiene el Id.
                        If Not BeUnidMed Is Nothing Then
                            BeProducto.IdUnidadMedidaBasica = BeUnidMed.IdUnidadMedida
                        Else

                            BeUnidMed = New clsBeUnidad_medida
                            BeUnidMed.IdUnidadMedida = clsLnUnidad_medida.MaxID(CnnInterface, lTrans) + 1
                            BeUnidMed.Nombre = BeSAPProducto.Base_Unit_Of_Measure
                            BeUnidMed.Codigo = BeSAPProducto.Base_Unit_Of_Measure
                            BeUnidMed.IdPropietario = BeConfigEnc.IdPropietario
                            BeUnidMed.Activo = True
                            BeUnidMed.User_agr = BeConfigEnc.IdUsuario
                            BeUnidMed.User_mod = BeConfigEnc.IdUsuario
                            BeUnidMed.Fec_agr = Now
                            BeUnidMed.Fec_mod = Now

                            Try

                                clsLnUnidad_medida.InsertarFromInterface(BeUnidMed, CnnInterface, lTrans)

                                'Despues que se inserta, se asigna el id de unidad de medida al producto
                                BeProducto.IdUnidadMedidaBasica = BeUnidMed.IdUnidadMedida

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Nueva unidad de medida insertada: {0}", BeUnidMed.Nombre))

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                                           BeProducto.Codigo,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: La unidad de medida no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeSAPProducto.Item_Category_Code, BeSAPProducto.Product_Group_Code, vbNewLine))

                                Application.DoEvents()

                            End Try

                        End If

                        BeProducto.Codigo = BeSAPProducto.No

                        If (vSegLote) Then
                            BeProducto.IdTipoRotacion = 3
                        Else
                            BeProducto.IdTipoRotacion = 1
                        End If

                        BeProducto.Precio = BeSAPProducto.Unit_Cost
                        BeProducto.Nombre = Truncate(BeSAPProducto.Description, 150)
                        BeProducto.Codigo_barra = BeSAPProducto.Item_Tracking_Code
                        BeProducto.Existencia_min = 0
                        BeProducto.Existencia_max = 0
                        BeProducto.ExistenciaUMBas = BeSAPProducto.Inventory
                        BeProducto.Costo = BeSAPProducto.Unit_Cost
                        BeProducto.Activo = True
                        BeProducto.Serializado = False
                        BeProducto.Control_vencimiento = vSegLote
                        BeProducto.Control_lote = vSegLote
                        BeProducto.Peso_recepcion = False
                        BeProducto.Peso_despacho = False
                        BeProducto.Temperatura_recepcion = False
                        BeProducto.Temperatura_despacho = False
                        BeProducto.Materia_prima = False
                        BeProducto.Kit = False
                        BeProducto.Tolerancia = False
                        BeProducto.Ciclo_vida = False

                        '#CKFK20240125 Campos que faltaban
                        BeProducto.IdTipoEtiqueta = BeConfigEnc.IdTipoEtiqueta
                        BeProducto.Genera_lp = BeConfigEnc.Genera_lp

                        'Bitácora
                        BeProducto.User_agr = BeConfigEnc.IdUsuario
                        BeProducto.Fec_agr = Now
                        BeProducto.User_mod = BeConfigEnc.IdUsuario
                        BeProducto.Fec_mod = Now
                        '#EJC202403280838: Agruegue IdTipoManufactura en interface SAP productos.
                        BeProducto.IdTipoManufactura = Val(BeSAPProducto.Manufacturing_Process)

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
                                BeProducto.Control_peso = BeProductoExistente.Control_peso
                                BeProducto.Precio = BeSAPProducto.Unit_Cost
                                BeProducto.Control_vencimiento = vSegLote
                                BeProducto.Control_lote = vSegLote
                                BeProducto.Nombre = Truncate(BeSAPProducto.Description, 150)

                                If clsLnProducto.Actualizar(BeProducto, CnnInterface, lTrans) > 0 Then

                                    If Not clsLnProducto_bodega.Exist(BeProducto.Codigo, CnnInterface, lTrans) Then

                                        BeProductoBodega = New clsBeProducto_bodega
                                        BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(CnnInterface, lTrans) + 1
                                        BeProductoBodega.IdProducto = BeProducto.IdProducto
                                        BeProductoBodega.IdBodega = BeConfigEnc.Idbodega
                                        BeProductoBodega.Activo = True
                                        BeProductoBodega.User_agr = BeConfigEnc.IdUsuario
                                        BeProductoBodega.User_mod = BeConfigEnc.IdUsuario
                                        BeProductoBodega.Fec_agr = Now
                                        BeProductoBodega.Fec_mod = Now

                                        Try

                                            clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, CnnInterface, lTrans)

                                            Marcar_Producto_Sincronizado_SAP(BeProducto.Codigo)

                                        Catch ex As Exception

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                       BeProducto.Codigo,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       BeConfigDet.Idnavconfigdet, CnnLog)


                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo insertar el producto bodega: {0} {1}", BeSAPProducto.No, vbNewLine))

                                            Application.DoEvents()

                                        End Try

                                    Else
                                        Marcar_Producto_Sincronizado_SAP(BeProducto.Codigo)
                                    End If

                                    VContadorBitacoraTOMWMS += 1

                                End If

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                           BeProducto.Codigo,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo actualizar el producto: {0} {1}", BeSAPProducto.No, vbNewLine))

                                Application.DoEvents()

                            End Try

                            prg.Value = vContador

                            vContador += 1

                        Else

                            BeProducto.IdProducto = clsLnProducto.MaxID(CnnInterface, lTrans) + 1

                            If Not BeConfigEnc Is Nothing Then
                                BeProducto.IdPropietario = BeConfigEnc.IdPropietario
                            End If

                            Try

                                clsLnProducto.Insertar(BeProducto, CnnInterface, lTrans)

                                BeProductoBodega = New clsBeProducto_bodega
                                BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(CnnInterface, lTrans) + 1
                                BeProductoBodega.IdProducto = BeProducto.IdProducto
                                BeProductoBodega.IdBodega = BeConfigEnc.Idbodega
                                BeProductoBodega.Activo = True
                                BeProductoBodega.User_agr = BeConfigEnc.IdUsuario
                                BeProductoBodega.User_mod = BeConfigEnc.IdUsuario
                                BeProductoBodega.Fec_agr = Now
                                BeProductoBodega.Fec_mod = Now

                                clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, CnnInterface, lTrans)

                                VContadorBitacoraTOMWMS += 1

                                Marcar_Producto_Sincronizado_SAP(BeProducto.Codigo)

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                           BeProducto.Codigo,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                                clsPublic.Actualizar_Progreso(lblprg, "Error al insertar producto: " & BeProducto.Codigo & vbNewLine & ex.Message)

                                Application.DoEvents()

                            End Try

                            Application.DoEvents()

                            prg.Value = vContador

                            vContador += 1

                        End If

                    Next

                Else
                    oCompany.GetLastError(lErrCode, sErrMsg)
                    Throw New Exception(sErrMsg)
                End If

            End If

            lTrans.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso -> " & Now)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos procesados correctamente: {0}", VContadorBitacoraTOMWMS))
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS

            If VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS Then
                BeNavEjecucionRes.Exitosa = True
            Else
                BeNavEjecucionRes.Exitosa = False
            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

            Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = True

        Catch ex As Exception
            If Not lTrans Is Nothing Then lTrans.Rollback()
            prg.Value = 0
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar producto a tabla de TOMWMS: {0}", ex.Message))
            Throw ex
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Shared Function Truncate(value As String, length As Integer) As String
        If length > value.Length Then
            Return value
        Else
            Return value.Substring(0, length)
        End If
    End Function

    Public Function Marcar_Producto_Sincronizado_SAP(ByVal pCodigoProducto As String) As Boolean

        Marcar_Producto_Sincronizado_SAP = False

        Try

            Dim oItemsSBO As Items = CType(oCompany.GetBusinessObject(BoObjectTypes.oItems), Items)

            If oItemsSBO.GetByKey(pCodigoProducto) Then
                Try
                    oItemsSBO.UserFields.Fields.Item("U_ENVIADO_WMS").Value = "1"
                    oItemsSBO.Update()
                Catch e As Exception
                    Throw e
                End Try
            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class