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

            query_sap = "SELECT 
                            oitm.ItemCode, -- Código del artículo 0
                            oitm.CodeBars as CodigoBarra, -- Código de barras 1
                            oitm.ItemName, -- Nombre del artículo 2
                            oitb.ItmsGrpCod as CodFamilia, -- Código de familia 3
                            oitb.ItmsGrpNam as Familia, -- Nombre de la familia 4
                            oitm.InvntryUom as Umbas, -- Unidad de medida del inventario 5
                            oitm.U_Categoria as CodigoCategoria, -- Categoría (del OITM)  6  
                            catd.U_name as NombreCategoria, -- Otros campos relevantes de [@CAT_D] 7
                            omrc.FirmCode as CodigoFabricante, -- 8
                            omrc.FirmName as NombreFabricante, -- Nombre del fabricante 9
                            oitm.U_ENVIADO_WMS, -- Campo enviado a WMS 10
                            oitm.U_extran, -- Importación si o no. 11
                            oitm.OrdrMulti, -- Presentación del producto, 12
                            oitm.U_Contlote, -- Control lote 13
                            oitm.U_TipoProduct as CodClass, -- Código de familia 14
                            'NombreClass' as NombreClass -- NombreClass de la familia 15
                        FROM 
                            dbo.OITM oitm
                        LEFT OUTER JOIN 
                            dbo.OITB oitb ON oitm.ItmsGrpCod = oitb.ItmsGrpCod
                        LEFT OUTER JOIN 
                            dbo.OMRC omrc ON oitm.FirmCode = omrc.FirmCode
                        LEFT OUTER JOIN 
                            dbo.[@CAT_D] catd ON oitm.U_Categoria = catd.U_Code
                        WHERE 
                            oitm.validFor = 'Y' 
                            AND oitm.InvntItem = 'Y' 
                            AND oitm.ItmsGrpCod NOT IN ('141','100') 
                            AND (oitm.U_ENVIADO_WMS =2 OR oitm.UpdateDate BETWEEN DATEADD(DAY,-" & BeConfigEnc.Rango_Dias_Importacion & ",convert(date,GETDATE())) AND convert(date,GETDATE())) "

            If Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeProducto As clsBeI_nav_producto

                While rs.EoF = False

                    BeProducto = New clsBeI_nav_producto()
                    BeProducto.No = rs.Fields.Item(0).Value.ToString() 'ItemCode
                    BeProducto.Item_Tracking_Code = rs.Fields.Item(1).Value.ToString()
                    BeProducto.Description = rs.Fields.Item(2).Value.ToString() 'ItemName
                    BeProducto.Description_2 = rs.Fields.Item(2).Value.ToString() 'CodeBars
                    BeProducto.Item_Category_Code = rs.Fields.Item(3).Value.ToString() 'CODIGO LINEA/CLASIFICACION
                    BeProducto.Item_Category_Name = rs.Fields.Item(4).Value.ToString() 'NOMBRE LINEA/CLASIFICACION
                    BeProducto.Inventory = 0
                    BeProducto.Base_Unit_Of_Measure = rs.Fields.Item(5).Value.ToString()
                    BeProducto.Unit_Cost = 0
                    BeProducto.Product_Group_Code = rs.Fields.Item(6).Value.ToString() 'TIPOWMS
                    BeProducto.Producto_Group_Name = rs.Fields.Item(7).Value.ToString() 'TIPOWMS
                    BeProducto.Gen_Prod_Posting_Group = rs.Fields.Item(8).Value.ToString() 'MARCA
                    BeProducto.Gen_Prod_Posting_Name = rs.Fields.Item(9).Value.ToString() 'MARCA
                    BeProducto.Product_Class_Code = rs.Fields.Item(14).Value.ToString() 'Clasificación/Tipo almacenaje
                    BeProducto.BatchControl = IIf(rs.Fields.Item(13).Value.ToString = "1", True, False) 'Control Lote

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
        Dim SyncSAP As New clsSyncSAPPresentaciones

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Iniciando procesamiento de productos a tabla intermedia -> " & Now)

            Dim lfichaProductos As New List(Of clsBeI_nav_producto)

            lfichaProductos = Get_Productos_From_SAP()

            Application.DoEvents()

            prg.Maximum = lfichaProductos.Count

            Dim vContador As Integer = 0

            Cnn.Open() : lTrans = Cnn.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_producto.EliminarTodos(Cnn, lTrans)
            clsLnI_nav_producto_presentacion.EliminarTodos(Cnn, lTrans)

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

                If Not Importar_Productos_y_Pres_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If
            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Productos_y_Pres_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
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
                    Dim vCodigoFamilia As String = ""

                    Dim BeClasificacion As New clsBeProducto_clasificacion
                    Dim BeFamilia As New clsBeProducto_familia
                    Dim BeProductoTipo As New clsBeProducto_tipo
                    Dim BeProductoMarca As New clsBeProducto_marca
                    Dim BeSyncSapPresentacion As New clsSyncSAPPresentaciones

                    For Each BeSAPProducto As clsBeI_nav_producto In lProductosFromSAP

                        BeProducto = New clsBeProducto
                        BeProductoExistente = New clsBeProducto
                        BeProductoExistente = clsLnProducto.Existe(BeSAPProducto.No, CnnInterface, lTrans)

                        vSegLote = IIf(IsDBNull(BeSAPProducto.BatchControl), False, BeSAPProducto.BatchControl)

#Region "Clasificacion"

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

#End Region

#Region "Tipo"

                        vCodigoGrupo = IIf(BeSAPProducto.Product_Group_Code = "", "", BeSAPProducto.Product_Group_Code)

                        If IsNumeric(vCodigoGrupo) OrElse vCodigoGrupo <> "" Then

                            BeProductoTipo = clsLnProducto_tipo.Get_Single_By_Codigo(vCodigoGrupo, CnnInterface, lTrans)

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

#End Region

#Region "Marca"

                        vCodigoMarca = IIf(BeSAPProducto.Gen_Prod_Posting_Group = "", 0, BeSAPProducto.Gen_Prod_Posting_Group)

                        If IsNumeric(vCodigoMarca) OrElse vCodigoMarca <> "" OrElse Val(vCodigoMarca) > 0 Then

                            Dim BeMarca As New clsBeProducto_marca()
                            Dim vNombreMarca As String = BeSAPProducto.Gen_Prod_Posting_Name
                            vCodigoMarca = vCodigoMarca

                            BeProductoMarca = clsLnProducto_marca.Get_Single_By_Codigo(vCodigoMarca, CnnInterface, lTrans)

                            If Not BeProductoMarca Is Nothing Then
                                BeProducto.IdMarca = BeProductoMarca.IdMarca
                            Else

                                With BeMarca
                                    .IdMarca = clsLnProducto_marca.MaxID(CnnInterface, lTrans)
                                    .IdPropietario = BeConfigEnc.IdPropietario
                                    .Codigo = vCodigoMarca
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

#End Region

#Region "Familia"

                        vCodigoFamilia = IIf(BeSAPProducto.Product_Class_Code = "", 0, BeSAPProducto.Product_Class_Code)

                        If IsNumeric(vCodigoFamilia) OrElse vCodigoFamilia <> "" OrElse vCodigoFamilia <> "0" Then

                            BeFamilia = clsLnProducto_familia.Get_Single_By_Codigo(vCodigoFamilia, CnnInterface, lTrans)

                            If Not BeFamilia Is Nothing Then
                                BeProducto.IdFamilia = BeFamilia.IdFamilia
                            Else

                                If Not vCodigoFamilia = 0 Then

                                    Dim vIdFamilia As Integer = clsLnProducto_clasificacion.MaxId(CnnInterface, lTrans)
                                    Dim vNombreFamilia As String = BeSAPProducto.Item_Category_Name

                                    Select Case vCodigoFamilia
                                        Case 1
                                            vNombreFamilia = "Almacenamiento"
                                        Case 2
                                            vNombreFamilia = "Cross Docking"
                                        Case 3
                                            vNombreFamilia = "Entrega Directa"

                                    End Select

                                    Dim BeProductoFamilia As New clsBeProducto_familia() With {.IdFamilia = vIdFamilia,
                                                                                            .Nombre = vNombreFamilia,
                                                                                            .Codigo = vCodigoClas,
                                                                                            .IsNew = True,
                                                                                            .Activo = True,
                                                                                            .Fec_agr = Now,
                                                                                            .Fec_mod = Now,
                                                                                            .User_agr = BeConfigEnc.IdUsuario,
                                                                                            .User_mod = BeConfigEnc.IdUsuario}

                                    BeProductoFamilia.Propietario.IdPropietario = BeConfigEnc.IdPropietario

                                    Try

                                        clsLnProducto_familia.Insertar(BeProductoFamilia, CnnInterface, lTrans)

                                        BeProducto.IdClasificacion = BeProductoFamilia.IdFamilia

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("La familia no existía y se insertó.: {0}{1}", BeProductoFamilia.IdFamilia, vbNewLine))

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               BeProducto.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: La clasificación no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeSAPProducto.Item_Category_Code, BeSAPProducto.Product_Group_Code, vbNewLine))

                                        Application.DoEvents()

                                    End Try


                                Else
                                    BeProducto.IdFamilia = 0
                                End If

                            End If

                        Else
                            BeProducto.IdFamilia = 0
                        End If

#End Region

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
                        BeProducto.Control_lote = BeConfigEnc.Control_lote
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
                        BeProducto.IdIndiceRotacion = BeConfigEnc.IdIndiceRotacion

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
                                BeProducto.Control_lote = BeConfigEnc.Control_lote
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

                                            clsPublic.Actualizar_Progreso(lblprg, "Se asoció el producto: " & BeProducto.Codigo & " a la bodega: " & BeConfigEnc.Idbodega)

                                        Catch ex As Exception

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                       BeProducto.Codigo,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       BeConfigDet.Idnavconfigdet, CnnLog)


                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo insertar el producto bodega: {0} {1}", BeSAPProducto.No, vbNewLine))

                                            Application.DoEvents()

                                        End Try

                                    End If

                                    VContadorBitacoraTOMWMS += 1

                                    Marcar_Producto_Sincronizado_SAP(BeProducto.Codigo)

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

                                Dim lBodegas As New List(Of clsBeBodega)
                                lBodegas = clsLnBodega.GetAll(CnnInterface, lTrans)

                                For Each Bodega In lBodegas

                                    BeProductoBodega = New clsBeProducto_bodega
                                    BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(CnnInterface, lTrans) + 1
                                    BeProductoBodega.IdProducto = BeProducto.IdProducto
                                    BeProductoBodega.IdBodega = Bodega.IdBodega
                                    BeProductoBodega.Activo = True
                                    BeProductoBodega.User_agr = BeConfigEnc.IdUsuario
                                    BeProductoBodega.User_mod = BeConfigEnc.IdUsuario
                                    BeProductoBodega.Fec_agr = Now
                                    BeProductoBodega.Fec_mod = Now

                                    clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, CnnInterface, lTrans)

                                    clsPublic.Actualizar_Progreso(lblprg, "Se asoció el producto: " & BeProducto.Codigo & " a la bodega: " & Bodega.Codigo)

                                Next

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

    '#CKFK20240423 Agregué esta función para importar los productos y sus presentaciones
    Private Function Importar_Productos_y_Pres_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                           ByRef prg As ProgressBar,
                                                                           ByRef cnnLog As SqlConnection) As Boolean

        Importar_Productos_y_Pres_Desde_SAP_A_TablaIntermedia = False

        Dim Cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing
        Dim RegistrosNoEncontrados As Boolean = False
        Dim SyncSAP As New clsSyncSAPPresentaciones

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Iniciando procesamiento de productos a tabla intermedia -> " & Now)

            Dim lfichaProductos As New List(Of clsBeI_nav_producto)
            Dim lPresentaciones As New List(Of clsBeI_nav_producto_presentacion)

            lfichaProductos = Get_Productos_From_SAP()
            lPresentaciones = SyncSAP.Get_Producto_Presentacion_From_SAP()

            Application.DoEvents()

            prg.Maximum = lfichaProductos.Count

            Dim vContador As Integer = 0

            Cnn.Open() : lTrans = Cnn.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_producto.EliminarTodos(Cnn, lTrans)
            clsLnI_nav_producto_presentacion.EliminarTodos(Cnn, lTrans)

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

            If lPresentaciones.Count > 0 Then

                prg.Maximum = lPresentaciones.Count
                prg.Value = 0

                vContador = 0

                clsLnI_nav_producto_presentacion.EliminarTodos(Cnn, lTrans)

                For Each Pres In lPresentaciones

                    If lfichaProductos.Any(Function(X) X.No = Pres.No) Then

                        Try

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando presentación: {0} de producto {1} {2} ", Pres.Codigo_Pres, Pres.No, vbNewLine))

                            clsLnI_nav_producto_presentacion.Insertar(Pres, Cnn, lTrans)

                            VContadorBitacoraIntermedia += 1

                            prg.Value = vContador

                            vContador += 1

                            Application.DoEvents()

                        Catch ex As Exception

                            clsLnLog_error_wms.Agregar_Error(ex.Message)

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo actualizar la presentación del producto: {0} {1}", Pres.No, vbNewLine))

                            Application.DoEvents()

                        End Try

                    Else

                    End If

                Next

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No se encontraron presentaciones pendientes de procesar (Enviado_SAP =No) en SAP.")
            End If

            lTrans.Commit()

            If RegistrosNoEncontrados Then
                Importar_Productos_y_Pres_Desde_SAP_A_TablaIntermedia = True
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Fin de procesamiento de productos y presentaciones -> " & Now)

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


    '#CKFK20240423 Agregué esta función para importar los productos y sus presentaciones
    Public Function Insertar_Productos_y_Pres_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                                    ByRef prg As ProgressBar,
                                                                                    Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                    Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Productos_y_Pres_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

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

                If Not Importar_Productos_y_Pres_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Productos_y_Pres_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If

                End If

            End If

            Dim lProductosFromSAP As New List(Of clsBeI_nav_producto)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando productos en tabla intermedia ")

            lProductosFromSAP = clsLnI_nav_producto.GetAll(CnnInterface, lTrans)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos en tabla intermedia: {0}", lProductosFromSAP.Count))

            If lProductosFromSAP.Count > 0 Then

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
                Dim BeSyncSapPresentacion As New clsSyncSAPPresentaciones

                For Each BeSAPProducto As clsBeI_nav_producto In lProductosFromSAP

                    BeProducto = New clsBeProducto
                    BeProductoExistente = New clsBeProducto
                    BeProductoExistente = clsLnProducto.Existe(BeSAPProducto.No, CnnInterface, lTrans)

                    vSegLote = IIf(IsDBNull(BeSAPProducto.BatchControl), False, BeSAPProducto.BatchControl)

#Region "Clasificacion"

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

#End Region

#Region "Tipo"

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

#End Region

#Region "Marca"

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

#End Region

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
                    BeProducto.Control_lote = BeConfigEnc.Control_lote
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
                            BeProducto.Control_lote = BeConfigEnc.Control_lote
                            BeProducto.Nombre = Truncate(BeSAPProducto.Description, 150)

                            If clsLnProducto.Actualizar(BeProducto, CnnInterface, lTrans) > 0 Then

                                If Not clsLnProducto_bodega.Exist(BeProducto.Codigo, CnnInterface, lTrans) Then

                                    Dim lBodegasWMS As New List(Of clsBeBodega)
                                    lBodegasWMS = clsLnBodega.GetAll()

                                    For Each Bodega In lBodegasWMS

                                        BeProductoBodega = New clsBeProducto_bodega
                                        BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(CnnInterface, lTrans) + 1
                                        BeProductoBodega.IdProducto = BeProducto.IdProducto
                                        BeProductoBodega.IdBodega = Bodega.IdBodega
                                        BeProductoBodega.Activo = True
                                        BeProductoBodega.User_agr = BeConfigEnc.IdUsuario
                                        BeProductoBodega.User_mod = BeConfigEnc.IdUsuario
                                        BeProductoBodega.Fec_agr = Now
                                        BeProductoBodega.Fec_mod = Now

                                        Try

                                            clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, CnnInterface, lTrans)

                                            clsPublic.Actualizar_Progreso(lblprg, "Asociado producto a bodega: " & Bodega.Codigo)

                                        Catch ex As Exception

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                           BeProducto.Codigo,
                                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                           BeConfigDet.Idnavconfigdet, CnnLog)


                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo insertar el producto bodega: {0} {1}", BeSAPProducto.No, vbNewLine))

                                            Application.DoEvents()

                                        End Try

                                    Next

                                    Marcar_Producto_Sincronizado_SAP(BeProducto.Codigo)

                                Else
                                    Marcar_Producto_Sincronizado_SAP(BeProducto.Codigo)
                                End If

                                VContadorBitacoraTOMWMS += 1

                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProducto.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet,
                                                                       CnnLog)

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

                            Dim lBodegasWMS As New List(Of clsBeBodega)
                            lBodegasWMS = clsLnBodega.GetAll()

                            For Each Bodega In lBodegasWMS

                                BeProductoBodega = New clsBeProducto_bodega
                                BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(CnnInterface, lTrans) + 1
                                BeProductoBodega.IdProducto = BeProducto.IdProducto
                                BeProductoBodega.IdBodega = Bodega.IdBodega
                                BeProductoBodega.Activo = True
                                BeProductoBodega.User_agr = BeConfigEnc.IdUsuario
                                BeProductoBodega.User_mod = BeConfigEnc.IdUsuario
                                BeProductoBodega.Fec_agr = Now
                                BeProductoBodega.Fec_mod = Now

                                Try

                                    clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, CnnInterface, lTrans)

                                    clsPublic.Actualizar_Progreso(lblprg, "Asociado producto a bodega: " & Bodega.Codigo)

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                           BeProducto.Codigo,
                                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                           BeConfigDet.Idnavconfigdet, CnnLog)


                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo insertar el producto bodega: {0} {1}", BeSAPProducto.No, vbNewLine))

                                    Application.DoEvents()

                                End Try

                            Next

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

                    BeSyncSapPresentacion.Importar_Presentaciones_By_Producto(BeProducto.Codigo, lblprg, CnnInterface, lTrans)

                Next

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

    'Public Function Marcar_Producto_Sincronizado_SAP(ByVal pCodigoProducto As String) As Boolean

    '    Marcar_Producto_Sincronizado_SAP = False

    '    Try

    '        Dim oItemsSBO As Items = CType(oCompany.GetBusinessObject(BoObjectTypes.oItems), Items)

    '        If oItemsSBO.GetByKey(pCodigoProducto) Then
    '            Try
    '                oItemsSBO.UserFields.Fields.Item("U_ENVIADO_WMS").Value = "1"
    '                Dim registros As Integer
    '                registros = oItemsSBO.Update()
    '                Debug.Write(registros)
    '            Catch e As Exception
    '                Throw e
    '            End Try
    '        End If

    '    Catch ex As Exception
    '        Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
    '    End Try

    'End Function

    Public Function Marcar_Producto_Sincronizado_SAP(ByVal code As String) As Boolean

        Dim resultado As Boolean = False

        Try


            ' Crear y preparar el objeto Recordset para la consulta
            Dim oRecordset As Recordset
            oRecordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

            ' Construir la consulta SQL para actualizar el registro basado en la clave compuesta
            Dim query As String = String.Format("UPDATE OITM SET U_ENVIADO_WMS = '1' WHERE ItemCode = '{0}'", code)

            ' Ejecutar la consulta de actualización
            oRecordset.DoQuery(query)

            resultado = True

        Catch ex As Exception
            Throw New Exception(String.Format("Error al marcar el centro de costo sincronizado para clave compuesta: {0}", ex.Message))
        End Try

        Return resultado

    End Function

End Class