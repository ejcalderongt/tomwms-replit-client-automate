Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM

Public Class clsSyncSAPProducto : Inherits clsInterfaceBase

    Shared VContadorBitacoraTOMWMS As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0
    Shared lReturnListProductoMatch As New List(Of clsBeI_nav_producto)
    Public ContadorRecursivo As Integer = 1

    Private Shared Function ConstruirQueryProductos(bodegas As List(Of clsBeI_nav_bodega)) As String
        Return ";WITH BASE AS (
                    SELECT t1.U_CodWMS, t1.ItemCode AS CodGaresa, t1.ItemName, CodeBars AS CodigoBarra,
                           '1' AS Codigo_Tipo, T2.ItmsGrpCod AS CodClasificacion, T2.ItmsGrpNam AS Clasificacion,
                           T1.U_Marca AS CodigoMarca, m.Name AS NombreMarca,
                           ISNULL(U.UomCode,'UN') AS Umbas,
                           (SELECT TOP(1) O.ItemCode FROM " & BD.Instancia.SAP_COMPANY_DB & ".dbo.OITM O WHERE O.U_CodWMS = T1.U_CodWMS) AS CodKilio,
                           'Garesa' AS Sociedad, T1.U_reqLote, T1.U_reqFVencimiento
                    FROM " & BD.Instancia.SAP_COMPANY_DB2 & ".dbo.OITM T1
                    RIGHT JOIN " & BD.Instancia.SAP_COMPANY_DB2 & ".dbo.OITB T2 ON T2.ItmsGrpCod = T1.ItmsGrpCod
                    INNER JOIN " & BD.Instancia.SAP_COMPANY_DB2 & ".dbo.[@MARCA] M ON T1.U_Marca = M.DocEntry
                    LEFT JOIN " & BD.Instancia.SAP_COMPANY_DB2 & ".dbo.OUOM U ON U.UomCode = T1.InvntryUom
                    WHERE T1.frozenFor = 'N' AND T1.validFor = 'Y' AND T1.U_ENVIADO_WMS = '2'
                          AND T1.ItemName NOT LIKE '%export%' AND T1.U_CodWMS IS NOT NULL
                )
                SELECT T1.U_CodWMS,
                       (SELECT TOP(1) ItemCode FROM " & BD.Instancia.SAP_COMPANY_DB2 & ".dbo.OITM O WHERE O.U_CodWMS = T1.U_CodWMS) AS CodGaresa,
                       T1.ItemName, CodeBars AS CodigoBarra,
                       '2' AS Codigo_Tipo, T2.ItmsGrpCod AS CodClasificacion, T2.ItmsGrpNam AS Clasificacion,
                       T1.U_Marca AS CodigoMarca, M.Name AS NombreMarca,
                       ISNULL(U.UomCode,'UN') AS Umbas, T1.ItemCode AS CodKilio,
                       'Kilio' AS Sociedad, T1.U_reqLote, T1.U_reqFVencimiento
                FROM " & BD.Instancia.SAP_COMPANY_DB & ".dbo.OITM T1
                RIGHT JOIN " & BD.Instancia.SAP_COMPANY_DB & ".dbo.OITB T2 ON T2.ItmsGrpCod = T1.ItmsGrpCod
                INNER JOIN " & BD.Instancia.SAP_COMPANY_DB & ".dbo.[@MARCA] M ON T1.U_Marca = M.DocEntry
                LEFT JOIN " & BD.Instancia.SAP_COMPANY_DB & ".dbo.OUOM U ON U.UomCode = T1.InvntryUom
                WHERE T1.frozenFor = 'N' AND T1.validFor = 'Y' AND T1.U_ENVIADO_WMS = '2'
                      AND T1.ItemName NOT LIKE '%export%' AND T1.U_CodWMS IS NOT NULL
                      AND NOT EXISTS (
                          SELECT 1 FROM BASE tsub0 WHERE T1.U_CodWMS = tsub0.U_CodWMS
                      )
                UNION
                SELECT * FROM BASE
                ORDER BY Sociedad, U_CodWMS;"
    End Function


    ''' <summary>
    ''' '#EJC20241001: Tome la decisión de manejar en UMBas UN la medida base del producto 
    ''' porque SAP maneja en presentación los pedidos pero la unidad base no está relacionada 
    ''' de forma precisa o correcta con la del maestro del producto.
    ''' </summary>
    ''' <param name="pTEmpresa"></param>
    ''' <returns></returns>
    Private Shared Function Get_Productos_From_SAP(Optional pTEmpresa As pEmpresa = pEmpresa.Killios) As List(Of clsBeI_nav_producto)
        Dim productos As New List(Of clsBeI_nav_producto)
        Dim sapConn As SapConnectionWrapper = Nothing
        Dim rs As Recordset = Nothing

        Try

            Dim bodegas = clsLnI_nav_bodega.GetAll()
            Dim querySAP = ConstruirQueryProductos(bodegas)

            sapConn = sapPool.GetConnection(pTEmpresa)
            Dim oCompany As Company = sapConn.Company

            rs = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(querySAP)

            While Not rs.EoF
                If rs.Fields.Item(0).Value.ToString() <> "" Then
                    Dim producto = MapearProductoDesdeRecordset(rs, pTEmpresa)

                    Dim idx = productos.FindIndex(Function(p) p.No = producto.No)
                    If idx = -1 Then
                        productos.Add(producto)
                    ElseIf productos(idx).Item_Tracking_Code <> producto.Item_Tracking_Code Then
                        producto.Description_2 = productos(idx).Item_Tracking_Code
                        lReturnListProductoMatch.Add(producto)
                    End If
                End If
                rs.MoveNext()
            End While

            Return productos

        Catch ex As Exception
            Throw New Exception("Error al obtener productos desde SAP: " & ex.Message)
        Finally
            If rs IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rs)
            If sapConn IsNot Nothing Then sapPool.ReleaseConnection(sapConn)
        End Try
    End Function
    Private Shared Function MapearProductoDesdeRecordset(rs As Recordset, pTEmpresa As pEmpresa) As clsBeI_nav_producto
        Return New clsBeI_nav_producto With {
        .No = rs.Fields.Item("U_CodWMS").Value.ToString(),
        .Description = rs.Fields.Item("ItemName").Value.ToString(),
        .Description_2 = rs.Fields.Item("CodKilio").Value.ToString(),
        .Inventory = 0,
        .Base_Unit_Of_Measure = "UN",
        .Unit_Cost = 0,
        .Item_Category_Code = rs.Fields.Item("CodClasificacion").Value.ToString(),
        .Item_Category_Name = rs.Fields.Item("Clasificacion").Value.ToString(),
        .Gen_Prod_Posting_Group = rs.Fields.Item("CodigoMarca").Value.ToString(),
        .Gen_Prod_Posting_Name = rs.Fields.Item("NombreMarca").Value.ToString(),
        .Product_Group_Code = pTEmpresa,
        .Producto_Group_Name = pTEmpresa.ToString(),
        .Search_Description = rs.Fields.Item("CodGaresa").Value.ToString(),
        .Sales_Unit = rs.Fields.Item("Umbas").Value.ToString(),
        .Item_Tracking_Code = rs.Fields.Item("CodigoBarra").Value.ToString(),
        .Product_Class_Code = rs.Fields.Item("Codigo_Tipo").Value.ToString(),
        .Product_Class_Name = rs.Fields.Item("Sociedad").Value.ToString(),
        .BatchControl = IIf(rs.Fields.Item("U_reqLote").Value.ToString() = "Y" OrElse rs.Fields.Item("U_reqLote").Value.ToString() = "S", True, False),
        .ExpirationControl = IIf(rs.Fields.Item("U_reqFVencimiento").Value.ToString() = "Y" OrElse rs.Fields.Item("U_reqFVencimiento").Value.ToString() = "S", True, False)
    }
    End Function

    Private Shared Function Importar_Productos_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
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

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0}", Prod.No))

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

                If lReturnListProductoMatch.Count > 0 Then

                    For Each ProductoDuplicadoEnEmperesas In lReturnListProductoMatch

                        clsPublic.Actualizar_Progreso(lblprg, "El producto: " & ProductoDuplicadoEnEmperesas.No & " existe en ambas empresas y tiene código de barra: " & ProductoDuplicadoEnEmperesas.Item_Tracking_Code & " en destino y " & ProductoDuplicadoEnEmperesas.Description_2 & " en origen.")

                    Next

                End If


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
            Throw

        Finally
            If Cnn.State = ConnectionState.Open Then Cnn.Close()
        End Try

    End Function

    'Public Function Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
    '                                                                         ByRef prg As ProgressBar,
    '                                                                         Optional ByVal ForzarEjecucion As Boolean = False,
    '                                                                         Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

    '    Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

    '    Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
    '    Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
    '    Dim lTrans As SqlTransaction = Nothing
    '    Dim vCodigoClas As String = ""
    '    Dim vNombreClas As String = ""
    '    Dim vCodigoGrupo As String = ""
    '    Dim vNombreGrupo As String = ""
    '    Dim vCodigoMarca As String = ""
    '    Dim vNombreMarca As String = ""
    '    Dim BeClasificacion As New clsBeProducto_clasificacion
    '    Dim BeProductoTipo As New clsBeProducto_tipo
    '    Dim BeProductoMarca As New clsBeProducto_marca
    '    Dim BeUnidMed As New clsBeUnidad_medida
    '    Dim BeProductoExistente As clsBeProducto = Nothing
    '    Dim BeProductoBodega As clsBeProducto_bodega = Nothing
    '    Dim BeProducto As clsBeProducto = Nothing
    '    Dim vSegLote As String = ""

    '    Try

    '        If Not ForzarEjecucion Then

    '            If Not Ejecutar_Interfaz("Producto") Then
    '                clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
    '                Exit Function
    '            End If

    '        End If

    '        CnnLog.Open()

    '        BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
    '        BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
    '        BeNavEjecucionEnc.Fecha = Now

    '        clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

    '        CnnInterface.Open() : lTrans = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

    '        clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

    '        BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
    '        BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
    '        BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
    '        BeNavEjecucionRes.Registros_ws = 0
    '        BeNavEjecucionRes.Registros_ti = 0
    '        BeNavEjecucionRes.Registros_WMS = 0
    '        BeNavEjecucionRes.Exitosa = False

    '        clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

    '        BeNavEjecRes = BeNavEjecucionRes

    '        If Not Pregunta_Si_LLena_Intermedia Then

    '            If Not Importar_Productos_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
    '                Exit Function
    '            End If

    '        Else

    '            If MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

    '                If Not Importar_Productos_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
    '                    Exit Function
    '                End If

    '            End If

    '        End If

    '        Dim lProductosFromSAP As New List(Of clsBeI_nav_producto)

    '        clsPublic.Actualizar_Progreso(lblprg, "Consultando productos en tabla intermedia ")

    '        lProductosFromSAP = clsLnI_nav_producto.GetAll(CnnInterface, lTrans)

    '        clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos en tabla intermedia: {0}", lProductosFromSAP.Count))

    '        If lProductosFromSAP.Count > 0 Then

    '            If Not Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then
    '                oCompany.GetLastError(lErrCode, sErrMsg)
    '                Throw New Exception(sErrMsg)
    '            End If

    '            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnInterface, lTrans)

    '            prg.Maximum = lProductosFromSAP.Count

    '            Dim vContador As Integer = 0

    '            prg.Value = 0

    '            clsPublic.Actualizar_Progreso(lblprg, "Trasladando producto desde SAP a TOMWMS...")

    '            For Each BeSAPProducto As clsBeI_nav_producto In lProductosFromSAP

    '                BeProducto = New clsBeProducto
    '                BeProductoExistente = New clsBeProducto
    '                BeProductoExistente = clsLnProducto.Existe_By_Codigo_Or_NoParte(BeSAPProducto.No, CnnInterface, lTrans)

    '                vCodigoClas = IIf(BeSAPProducto.Item_Category_Code = "", "0", BeSAPProducto.Item_Category_Code)
    '                vNombreClas = BeSAPProducto.Item_Category_Name

    '                If IsNumeric(vCodigoClas) OrElse vCodigoClas <> "" Then

    '                    BeProducto.IdClasificacion = IIf(BeSAPProducto.Item_Category_Code = "", 0, BeSAPProducto.Item_Category_Code)

    '                    BeClasificacion = clsLnProducto_clasificacion.Get_Single_By_Codigo(vCodigoClas, CnnInterface, lTrans)

    '                    If Not BeClasificacion Is Nothing Then
    '                        BeProducto.IdClasificacion = BeClasificacion.IdClasificacion
    '                    Else

    '                        Dim vIdClasificacion As Integer = clsLnProducto_clasificacion.MaxId(CnnInterface, lTrans)

    '                        Dim BeProductoClas As New clsBeProducto_clasificacion() With {.IdClasificacion = vIdClasificacion,
    '                                                                                    .Nombre = vNombreClas,
    '                                                                                    .Codigo = vCodigoClas,
    '                                                                                    .Sistema = False,
    '                                                                                    .IsNew = True,
    '                                                                                    .Activo = True,
    '                                                                                    .Fec_agr = Now,
    '                                                                                    .Fec_mod = Now,
    '                                                                                    .User_agr = BeConfigEnc.IdUsuario,
    '                                                                                    .User_mod = BeConfigEnc.IdUsuario}

    '                        BeProductoClas.Propietario.IdPropietario = BeConfigEnc.IdPropietario

    '                        Try

    '                            clsLnProducto_clasificacion.Insertar(BeProductoClas, CnnInterface, lTrans)

    '                            BeProducto.IdClasificacion = BeProductoClas.IdClasificacion

    '                            clsPublic.Actualizar_Progreso(lblprg, String.Format("La clasificación no existía y se insertó.: {0}{1}", BeProductoClas.IdClasificacion, vbNewLine))

    '                        Catch ex As Exception

    '                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
    '                                                                       BeProducto.Codigo,
    '                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
    '                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

    '                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: La clasificación no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeSAPProducto.Item_Category_Code, BeSAPProducto.Product_Group_Code, vbNewLine))

    '                            Application.DoEvents()

    '                        End Try

    '                    End If


    '                Else
    '                    BeProducto.IdClasificacion = 0
    '                End If

    '                vSegLote = IIf(IsDBNull(BeSAPProducto.Item_Tracking_Code), "", BeSAPProducto.Item_Tracking_Code)

    '                vCodigoGrupo = BeSAPProducto.Product_Class_Code
    '                vNombreGrupo = BeSAPProducto.Product_Class_Name

    '                If IsNumeric(vCodigoGrupo) OrElse vCodigoGrupo <> "" Then

    '                    BeProducto.IdTipoProducto = vCodigoGrupo

    '                    BeProductoTipo = clsLnProducto_tipo.Get_Single_By_Codigo(vCodigoGrupo, CnnInterface, lTrans)

    '                    If Not BeProductoTipo Is Nothing Then
    '                        BeProducto.IdTipoProducto = BeProductoTipo.IdTipoProducto
    '                    Else

    '                        Dim BeTipoProducto As New clsBeProducto_tipo()

    '                        With BeTipoProducto
    '                            .IdTipoProducto = clsLnProducto_tipo.MaxID(CnnInterface, lTrans)
    '                            .IdPropietario = BeConfigEnc.IdPropietario
    '                            .NombreTipoProducto = vNombreGrupo
    '                            .Codigo = vCodigoGrupo
    '                            .Activo = True
    '                            .User_agr = BeConfigEnc.IdUsuario
    '                            .Fec_agr = Now
    '                            .User_mod = BeConfigEnc.IdUsuario
    '                            .Fec_mod = Now
    '                        End With

    '                        Try

    '                            clsLnProducto_tipo.Insertar(BeTipoProducto, CnnInterface, lTrans)

    '                            BeProducto.IdTipoProducto = BeTipoProducto.IdTipoProducto

    '                            clsPublic.Actualizar_Progreso(lblprg, String.Format("El tipo de producto no existía y se insertó.: {0}{1}", BeTipoProducto.IdTipoProducto, vbNewLine))

    '                        Catch ex As Exception

    '                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '                                                                       BeProducto.Codigo,
    '                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
    '                                                                       BeConfigDet.Idnavconfigdet,
    '                                                                       CnnLog)

    '                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: El tipo de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeSAPProducto.Item_Category_Code, BeSAPProducto.Product_Group_Code, vbNewLine))

    '                            Application.DoEvents()

    '                        End Try

    '                    End If

    '                Else
    '                    BeProducto.IdTipoProducto = 0
    '                End If

    '                vCodigoMarca = IIf(BeSAPProducto.Gen_Prod_Posting_Group = "", 0, BeSAPProducto.Gen_Prod_Posting_Group)
    '                vNombreMarca = BeSAPProducto.Gen_Prod_Posting_Name

    '                If IsNumeric(vCodigoMarca) OrElse vCodigoMarca <> "" Then

    '                    BeProductoMarca = clsLnProducto_marca.Get_Single_By_Codigo(vCodigoMarca, CnnInterface, lTrans)

    '                    If Not BeProductoMarca Is Nothing Then
    '                        BeProducto.IdMarca = BeProductoMarca.IdMarca
    '                    Else

    '                        Dim BeMarca As New clsBeProducto_marca()

    '                        With BeMarca
    '                            .IdMarca = clsLnProducto_marca.MaxID(CnnInterface, lTrans)
    '                            .IdPropietario = BeConfigEnc.IdPropietario
    '                            .Nombre = vNombreMarca
    '                            .Codigo = vCodigoMarca
    '                            .Activo = True
    '                            .User_agr = BeConfigEnc.IdUsuario
    '                            .Fec_agr = Now
    '                            .User_mod = BeConfigEnc.IdUsuario
    '                            .Fec_mod = Now
    '                        End With

    '                        Try

    '                            clsLnProducto_marca.Insertar(BeMarca, CnnInterface, lTrans)

    '                            BeProducto.IdMarca = BeMarca.IdMarca

    '                            clsPublic.Actualizar_Progreso(lblprg, String.Format("La marca de producto no existía y se insertó.: {0}{1}", BeMarca.IdMarca, vbNewLine))

    '                        Catch ex As Exception

    '                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '                                                                       BeProducto.Codigo,
    '                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
    '                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

    '                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: La marca de producto no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeSAPProducto.Item_Category_Code, BeSAPProducto.Product_Group_Code, vbNewLine))

    '                            Application.DoEvents()

    '                        End Try

    '                    End If

    '                Else
    '                    BeProducto.IdMarca = 0
    '                End If

    '                BeUnidMed.Nombre = BeSAPProducto.Base_Unit_Of_Measure

    '                If BeUnidMed.Nombre = "" Then
    '                    Throw New Exception("No definió la UM de venta SALUNITMSR")
    '                End If

    '                'Valida si existe la UM por el nombre/código que viene de Nav
    '                BeUnidMed = clsLnUnidad_medida.Existe_By_Nombre(BeUnidMed.Nombre, CnnInterface, lTrans)

    '                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando producto: {0}", BeSAPProducto.No))

    '                'Si existe devuelve un objeto del tipo Unidad_Medida que contiene el Id.
    '                If Not BeUnidMed Is Nothing Then
    '                    BeProducto.IdUnidadMedidaBasica = BeUnidMed.IdUnidadMedida
    '                    BeProducto.Codigo = BeSAPProducto.Base_Unit_Of_Measure
    '                Else

    '                    BeUnidMed = New clsBeUnidad_medida
    '                    BeUnidMed.IdUnidadMedida = clsLnUnidad_medida.MaxID(CnnInterface, lTrans) + 1
    '                    BeUnidMed.Nombre = BeSAPProducto.Base_Unit_Of_Measure
    '                    BeUnidMed.Codigo = BeSAPProducto.Base_Unit_Of_Measure
    '                    BeUnidMed.IdPropietario = BeConfigEnc.IdPropietario
    '                    BeUnidMed.Activo = True
    '                    BeUnidMed.User_agr = BeConfigEnc.IdUsuario
    '                    BeUnidMed.User_mod = BeConfigEnc.IdUsuario
    '                    BeUnidMed.Fec_agr = Now
    '                    BeUnidMed.Fec_mod = Now

    '                    Try

    '                        clsLnUnidad_medida.InsertarFromInterface(BeUnidMed, CnnInterface, lTrans)

    '                        'Despues que se inserta, se asigna el id de unidad de medida al producto
    '                        BeProducto.IdUnidadMedidaBasica = BeUnidMed.IdUnidadMedida

    '                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Nueva unidad de medida insertada: {0}", BeUnidMed.Nombre))

    '                    Catch ex As Exception

    '                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '                                                                   BeProducto.Codigo,
    '                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
    '                                                                   BeConfigDet.Idnavconfigdet, CnnLog)

    '                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: La unidad de medida no existía y no se pudo insertar Item_Category_Code: {0} Product_Group_Code:{1} {2}", BeSAPProducto.Item_Category_Code, BeSAPProducto.Product_Group_Code, vbNewLine))

    '                        Application.DoEvents()

    '                    End Try

    '                End If

    '                BeProducto.Codigo = BeSAPProducto.No

    '                If (vSegLote = "Y" OrElse vSegLote = "S") Then
    '                    BeProducto.IdTipoRotacion = 3
    '                Else
    '                    BeProducto.IdTipoRotacion = 1
    '                End If

    '                BeProducto.Precio = BeSAPProducto.Unit_Cost
    '                BeProducto.Nombre = Truncate(String.Format("{0}", BeSAPProducto.Description), 100)
    '                BeProducto.Noparte = BeSAPProducto.Description_2 'Kilio
    '                BeProducto.Noserie = BeSAPProducto.Search_Description 'Garesa
    '                BeProducto.Codigo_barra = BeSAPProducto.Item_Tracking_Code
    '                BeProducto.Existencia_min = 0
    '                BeProducto.Existencia_max = 0
    '                BeProducto.ExistenciaUMBas = BeSAPProducto.Inventory
    '                BeProducto.Costo = BeSAPProducto.Unit_Cost
    '                BeProducto.Activo = True
    '                BeProducto.Serializado = False
    '                BeProducto.Control_vencimiento = BeConfigEnc.Control_vencimiento ' (vSegLote <> "")
    '                BeProducto.Control_lote = BeConfigEnc.Control_lote '(vSegLote <> "")
    '                BeProducto.Genera_lp = BeConfigEnc.Genera_lp
    '                BeProducto.IdTipoEtiqueta = BeConfigEnc.IdTipoEtiqueta
    '                BeProducto.Peso_recepcion = False
    '                BeProducto.Peso_despacho = False
    '                BeProducto.Temperatura_recepcion = False
    '                BeProducto.Temperatura_despacho = False
    '                BeProducto.Materia_prima = False
    '                BeProducto.Kit = False
    '                BeProducto.Tolerancia = False
    '                BeProducto.Ciclo_vida = False
    '                BeProducto.User_agr = BeConfigEnc.IdUsuario
    '                BeProducto.Fec_agr = Now
    '                BeProducto.User_mod = BeConfigEnc.IdUsuario
    '                BeProducto.Fec_mod = Now

    '                If Not BeProductoExistente Is Nothing Then

    '                    Try

    '                        BeProducto.IdProducto = BeProductoExistente.IdProducto
    '                        BeProducto.User_mod = BeConfigEnc.IdUsuario
    '                        BeProducto.Fec_mod = Now
    '                        BeProducto.IdTipoRotacion = BeProductoExistente.IdTipoRotacion
    '                        BeProducto.IdIndiceRotacion = BeProductoExistente.IdIndiceRotacion
    '                        BeProducto.Largo = BeProductoExistente.Largo
    '                        BeProducto.Alto = BeProductoExistente.Alto
    '                        BeProducto.Ancho = BeProductoExistente.Ancho
    '                        BeProducto.Ancho = BeProductoExistente.Ancho
    '                        BeProducto.Control_peso = BeProductoExistente.Control_peso
    '                        BeProducto.Precio = BeSAPProducto.Unit_Cost
    '                        BeProducto.Control_vencimiento = BeConfigEnc.Control_vencimiento ' (vSegLote <> "")
    '                        BeProducto.Control_lote = BeConfigEnc.Control_lote '(vSegLote <> "")
    '                        BeProducto.Genera_lp = BeConfigEnc.Genera_lp
    '                        BeProducto.IdTipoEtiqueta = BeConfigEnc.IdTipoEtiqueta
    '                        BeProducto.Nombre = Truncate(String.Format("{0}", BeSAPProducto.Description), 100)
    '                        BeProducto.Noparte = IIf(BeSAPProducto.Description_2 = "", BeProducto.Noparte, BeSAPProducto.Description_2) 'Código Kilio
    '                        BeProducto.Noserie = IIf(BeSAPProducto.Search_Description = "", BeProducto.Noserie, BeSAPProducto.Search_Description) 'Código Garesa                            

    '                        If clsLnProducto.Actualizar(BeProducto, CnnInterface, lTrans) > 0 Then

    '                            If Not clsLnProducto_bodega.Exist(BeProducto.Codigo, CnnInterface, lTrans) Then

    '                                BeProductoBodega = New clsBeProducto_bodega
    '                                BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(CnnInterface, lTrans) + 1
    '                                BeProductoBodega.IdProducto = BeProducto.IdProducto
    '                                BeProductoBodega.IdBodega = BeConfigEnc.Idbodega
    '                                BeProductoBodega.Activo = True
    '                                BeProductoBodega.User_agr = BeConfigEnc.IdUsuario
    '                                BeProductoBodega.User_mod = BeConfigEnc.IdUsuario
    '                                BeProductoBodega.Fec_agr = Now
    '                                BeProductoBodega.Fec_mod = Now

    '                                Try

    '                                    clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, CnnInterface, lTrans)

    '                                    Marcar_Producto_Sincronizado_SAP(BeProducto.Codigo)

    '                                Catch ex As Exception

    '                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
    '                                                                               BeProducto.Codigo,
    '                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
    '                                                                               BeConfigDet.Idnavconfigdet, CnnLog)


    '                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo insertar el producto bodega: {0} {1}", BeSAPProducto.No, vbNewLine))

    '                                    Application.DoEvents()

    '                                End Try

    '                            Else
    '                                Marcar_Producto_Sincronizado_SAP(BeProducto.Codigo)
    '                            End If

    '                            VContadorBitacoraTOMWMS += 1

    '                        End If

    '                    Catch ex As Exception

    '                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
    '                                                                   BeProducto.Codigo,
    '                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
    '                                                                   BeConfigDet.Idnavconfigdet, CnnLog)

    '                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo actualizar el producto: {0} {1}", BeSAPProducto.No, vbNewLine))

    '                        Application.DoEvents()

    '                    End Try

    '                    prg.Value = vContador

    '                    vContador += 1

    '                Else

    '                    BeProducto.IdProducto = clsLnProducto.MaxID(CnnInterface, lTrans) + 1

    '                    If Not BeConfigEnc Is Nothing Then
    '                        BeProducto.IdPropietario = BeConfigEnc.IdPropietario
    '                    End If

    '                    Try

    '                        clsLnProducto.Insertar(BeProducto, CnnInterface, lTrans)

    '                        BeProductoBodega = New clsBeProducto_bodega
    '                        BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(CnnInterface, lTrans) + 1
    '                        BeProductoBodega.IdProducto = BeProducto.IdProducto
    '                        BeProductoBodega.IdBodega = BeConfigEnc.Idbodega
    '                        BeProductoBodega.Activo = True
    '                        BeProductoBodega.User_agr = BeConfigEnc.IdUsuario
    '                        BeProductoBodega.User_mod = BeConfigEnc.IdUsuario
    '                        BeProductoBodega.Fec_agr = Now
    '                        BeProductoBodega.Fec_mod = Now

    '                        clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, CnnInterface, lTrans)

    '                        Dim lBodegas As New List(Of clsBeBodega)
    '                        lBodegas = clsLnBodega.GetAll(CnnInterface, lTrans)

    '                        For Each Bodega In lBodegas

    '                            BeProductoBodega = New clsBeProducto_bodega
    '                            BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(CnnInterface, lTrans) + 1
    '                            BeProductoBodega.IdProducto = BeProducto.IdProducto
    '                            BeProductoBodega.IdBodega = Bodega.IdBodega
    '                            BeProductoBodega.Activo = True
    '                            BeProductoBodega.User_agr = BeConfigEnc.IdUsuario
    '                            BeProductoBodega.User_mod = BeConfigEnc.IdUsuario
    '                            BeProductoBodega.Fec_agr = Now
    '                            BeProductoBodega.Fec_mod = Now

    '                            clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, CnnInterface, lTrans)

    '                            clsPublic.Actualizar_Progreso(lblprg, "Se asoció el producto: " & BeProducto.Codigo & " a la bodega: " & Bodega.Codigo)

    '                        Next

    '                        VContadorBitacoraTOMWMS += 1

    '                        Marcar_Producto_Sincronizado_SAP(BeProducto.Codigo)

    '                    Catch ex As Exception

    '                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
    '                                                                   BeProducto.Codigo,
    '                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
    '                                                                   BeConfigDet.Idnavconfigdet, CnnLog)

    '                        clsPublic.Actualizar_Progreso(lblprg, "Error al insertar producto: " & BeProducto.Codigo & vbNewLine & ex.Message)

    '                        Application.DoEvents()

    '                    End Try

    '                    Application.DoEvents()

    '                    prg.Value = vContador

    '                    vContador += 1

    '                End If

    '            Next

    '        End If

    '        lTrans.Commit()

    '        clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso -> " & Now)
    '        clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos procesados correctamente: {0}", VContadorBitacoraTOMWMS))
    '        Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
    '        clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

    '        BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
    '        BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS

    '        If VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS Then
    '            BeNavEjecucionRes.Exitosa = True
    '        Else
    '            BeNavEjecucionRes.Exitosa = False
    '        End If

    '        clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

    '    Catch ex As Exception
    '        If Not lTrans Is Nothing Then lTrans.Rollback()
    '        prg.Value = 0
    '        lblprg.AppendText(String.Format("Error al insertar producto a tabla de TOMWMS: {0} {1}", ex.Message, vbNewLine))
    '        lblprg.SelectionStart = lblprg.TextLength
    '        lblprg.ScrollToCaret()
    '        Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
    '    Finally
    '        If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
    '        If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
    '        Desconectar_SAP(oCompany)
    '    End Try

    'End Function
    Public Shared Function Truncate(value As String, length As Integer) As String
        If length > value.Length Then
            Return value
        Else
            Return value.Substring(0, length)
        End If
    End Function
    Public Shared Function Marcar_Producto_Sincronizado_SAP(ByVal pCodigoProducto As String, ByVal oCompany As Company) As Boolean

        Dim resultado As Boolean = False

        Try

            Dim oRecordset As Recordset
            oRecordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

            Dim query As String = String.Format("UPDATE " & BD.Instancia.SAP_COMPANY_DB & ".dbo.OITM SET U_Enviado_WMS = '1' WHERE U_CodWMS = '{0}'", pCodigoProducto)
            oRecordset.DoQuery(query)

            Dim query2 As String = String.Format("UPDATE " & BD.Instancia.SAP_COMPANY_DB2 & ".dbo.OITM SET U_Enviado_WMS = '1' WHERE U_CodWMS = '{0}'", pCodigoProducto)
            oRecordset.DoQuery(query2)

            resultado = True

        Catch ex As Exception
            Throw New Exception(String.Format("Error al marcar el producto sincronizado para clave: {0}", ex.Message))
        End Try

        Return resultado

    End Function
    Public Shared Function Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                                     ByRef prg As ProgressBar,
                                                                                     Optional ForzarEjecucion As Boolean = False,
                                                                                     Optional Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean
        Dim cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim cnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim trx As SqlTransaction = Nothing
        Dim oCompany As Company = Nothing
        Dim lErrCode As Integer = 0
        Dim sErrMsg As String = ""

        Try

            cnnLog.Open()

            InicializarEjecucionProducto(cnnLog)

            cnn.Open()
            trx = cnn.BeginTransaction(IsolationLevel.ReadUncommitted)

            If Not ValidarYImportarProductosIntermedios(lblprg, prg, cnnLog, Pregunta_Si_LLena_Intermedia) Then Return False

            Dim productos As List(Of clsBeI_nav_producto) = clsLnI_nav_producto.GetAll(cnn, trx)
            clsPublic.Actualizar_Progreso(lblprg, $"Productos en tabla intermedia: {productos.Count}")
            If productos.Count = 0 Then Return False

            If Not Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then Throw New Exception(sErrMsg)
            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, cnn, trx)

            prg.Maximum = productos.Count : prg.Value = 0

            For i = 0 To productos.Count - 1
                clsPublic.Actualizar_Progreso(lblprg, $"Procesando producto: {productos(i).No}")
                ProcesarProductoSAP(productos(i), cnn, trx, cnnLog, lblprg, oCompany)
                prg.Value = i + 1
            Next

            trx.Commit()

            FinalizarEjecucionProducto(lblprg, cnnLog)

            Return True

        Catch ex As Exception
            If trx IsNot Nothing Then trx.Rollback()
            clsPublic.Actualizar_Progreso(lblprg, $"Error en proceso: {ex.Message}")
            Throw
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            If cnnLog.State = ConnectionState.Open Then cnnLog.Close()
            Desconectar_SAP(oCompany)
            prg.Value = 0
        End Try

    End Function

    ' Procesa un producto individual desde SAP hacia TOMWMS
    Private Shared Sub ProcesarProductoSAP(productoSAP As clsBeI_nav_producto,
                                            cnn As SqlConnection,
                                            trx As SqlTransaction,
                                            cnnLog As SqlConnection,
                                            lblprg As RichTextBox,
                                            oCompany As Company)

        Try

            Dim productoWMS As clsBeProducto = clsLnProducto.Existe_By_Codigo_Or_NoParte(productoSAP.No, cnn, trx)
            Dim productoNuevo As Boolean = (productoWMS Is Nothing)
            If productoNuevo Then productoWMS = New clsBeProducto

            productoWMS.IdClasificacion = ResolverClasificacion(productoSAP, cnn, trx, cnnLog, lblprg)
            productoWMS.IdTipoProducto = ResolverTipoProducto(productoSAP, cnn, trx, cnnLog, lblprg)
            productoWMS.IdMarca = ResolverMarca(productoSAP, cnn, trx, cnnLog, lblprg)
            productoWMS.IdUnidadMedidaBasica = ResolverUnidadMedida(productoSAP, cnn, trx, cnnLog, lblprg)
            productoWMS.IdPropietario = BeConfigEnc.IdPropietario

            MapearDatosProducto(productoWMS, productoSAP)

            If productoNuevo Then
                productoWMS.IdProducto = clsLnProducto.MaxID(cnn, trx) + 1
                clsLnProducto.Insertar(productoWMS, cnn, trx)
                AsociarBodegas(productoWMS, cnn, trx, lblprg)
            Else
                productoWMS.IdProducto = productoWMS.IdProducto ' se conserva el ID
                clsLnProducto.Actualizar(productoWMS, cnn, trx)
            End If

            Marcar_Producto_Sincronizado_SAP(productoWMS.Codigo, oCompany)

            VContadorBitacoraTOMWMS += 1

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                   productoSAP.No,
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet,
                                                   cnnLog)
            clsPublic.Actualizar_Progreso(lblprg, $"Error con producto {productoSAP.No}: {ex.Message}")
        End Try

    End Sub
    Private Shared Function ResolverClasificacion(productoSAP As clsBeI_nav_producto, cnn As SqlConnection, trx As SqlTransaction, cnnLog As SqlConnection, lblprg As RichTextBox) As Integer

        Try

            Dim codigo As String = If(productoSAP.Item_Category_Code = "", "0", productoSAP.Item_Category_Code)
            Dim clasificacion = clsLnProducto_clasificacion.Get_Single_By_Codigo(codigo, cnn, trx)

            If clasificacion IsNot Nothing Then Return clasificacion.IdClasificacion

            Dim nuevaClas As New clsBeProducto_clasificacion With {
                .IdClasificacion = clsLnProducto_clasificacion.MaxId(cnn, trx),
                .Nombre = productoSAP.Item_Category_Name,
                .codigo = codigo,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now,
                .Sistema = False,
                .IsNew = True
            }
            nuevaClas.Propietario.IdPropietario = BeConfigEnc.IdPropietario
            clsLnProducto_clasificacion.Insertar(nuevaClas, cnn, trx)

            Return nuevaClas.IdClasificacion

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Function ResolverTipoProducto(productoSAP As clsBeI_nav_producto, cnn As SqlConnection, trx As SqlTransaction, cnnLog As SqlConnection, lblprg As RichTextBox) As Integer

        Try

            Dim codigo As String = productoSAP.Product_Class_Code
            Dim tipo = clsLnProducto_tipo.Get_Single_By_Codigo(codigo, cnn, trx)
            If tipo IsNot Nothing Then Return tipo.IdTipoProducto

            Dim nuevoTipo As New clsBeProducto_tipo With {
                .IdTipoProducto = clsLnProducto_tipo.MaxID(cnn, trx),
                .IdPropietario = BeConfigEnc.IdPropietario,
                .NombreTipoProducto = productoSAP.Product_Class_Name,
                .codigo = codigo,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now
            }
            clsLnProducto_tipo.Insertar(nuevoTipo, cnn, trx)

            Return nuevoTipo.IdTipoProducto

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Function ResolverMarca(productoSAP As clsBeI_nav_producto, cnn As SqlConnection, trx As SqlTransaction, cnnLog As SqlConnection, lblprg As RichTextBox) As Integer
        Dim codigo As String = productoSAP.Gen_Prod_Posting_Group
        Dim marca = clsLnProducto_marca.Get_Single_By_Codigo(codigo, cnn, trx)
        If marca IsNot Nothing Then Return marca.IdMarca

        Dim nuevaMarca As New clsBeProducto_marca With {
            .IdMarca = clsLnProducto_marca.MaxID(cnn, trx),
            .IdPropietario = BeConfigEnc.IdPropietario,
            .Nombre = productoSAP.Gen_Prod_Posting_Name,
            .codigo = codigo,
            .Activo = True,
            .User_agr = BeConfigEnc.IdUsuario,
            .User_mod = BeConfigEnc.IdUsuario,
            .Fec_agr = Now,
            .Fec_mod = Now
        }
        clsLnProducto_marca.Insertar(nuevaMarca, cnn, trx)
        Return nuevaMarca.IdMarca
    End Function
    Private Shared Function ResolverUnidadMedida(productoSAP As clsBeI_nav_producto, cnn As SqlConnection, trx As SqlTransaction, cnnLog As SqlConnection, lblprg As RichTextBox) As Integer
        Dim nombreUM As String = productoSAP.Base_Unit_Of_Measure
        Dim unidad = clsLnUnidad_medida.Existe_By_Nombre(nombreUM, cnn, trx)
        If unidad IsNot Nothing Then Return unidad.IdUnidadMedida

        Dim nuevaUM As New clsBeUnidad_medida With {
            .IdUnidadMedida = clsLnUnidad_medida.MaxID(cnn, trx) + 1,
            .Nombre = nombreUM,
            .Codigo = nombreUM,
            .IdPropietario = BeConfigEnc.IdPropietario,
            .Activo = True,
            .User_agr = BeConfigEnc.IdUsuario,
            .User_mod = BeConfigEnc.IdUsuario,
            .Fec_agr = Now,
            .Fec_mod = Now
        }
        clsLnUnidad_medida.InsertarFromInterface(nuevaUM, cnn, trx)
        Return nuevaUM.IdUnidadMedida
    End Function
    Private Shared Sub MapearDatosProducto(prod As clsBeProducto, sap As clsBeI_nav_producto)
        prod.Codigo = sap.No
        prod.Nombre = Truncate(sap.Description, 100)
        prod.Noparte = sap.Description_2
        prod.Noserie = sap.Search_Description
        prod.Codigo_barra = sap.Item_Tracking_Code
        prod.Precio = sap.Unit_Cost
        prod.Costo = sap.Unit_Cost
        prod.ExistenciaUMBas = sap.Inventory
        prod.Activo = True
        prod.Fec_agr = Now
        prod.Fec_mod = Now
        prod.User_agr = BeConfigEnc.IdUsuario
        prod.User_mod = BeConfigEnc.IdUsuario
        '#CKFK20250826 Se agregaron estos campos porque la info ahora viene de SAP
        prod.Control_vencimiento = sap.ExpirationControl
        prod.Control_lote = sap.BatchControl
        prod.Genera_lp = BeConfigEnc.Genera_lp
        prod.IdTipoEtiqueta = BeConfigEnc.IdTipoEtiqueta
        prod.IdTipoRotacion = If(sap.Item_Tracking_Code = "Y" OrElse sap.Item_Tracking_Code = "S", 3, 1)
    End Sub
    Private Shared Sub AsociarBodegas(producto As clsBeProducto, cnn As SqlConnection, trx As SqlTransaction, lblprg As RichTextBox)

        Try

            Dim bodegas = clsLnBodega.GetAll(cnn, trx)
            For Each bodega In bodegas
                Dim prodBodega As New clsBeProducto_bodega With {
                .IdProductoBodega = clsLnProducto_bodega.MaxID(cnn, trx) + 1,
                .IdProducto = producto.IdProducto,
                .IdBodega = bodega.IdBodega,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now
            }
                clsLnProducto_bodega.InsertarFromInterface(prodBodega, cnn, trx)
                clsPublic.Actualizar_Progreso(lblprg, $"Producto {producto.Codigo} asociado a bodega {bodega.Codigo}")
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Shared Sub FinalizarEjecucionProducto(lblprg As RichTextBox, cnnLog As SqlConnection)
        Dim tiempo = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
        clsPublic.Actualizar_Progreso(lblprg, $"Productos procesados correctamente: {VContadorBitacoraTOMWMS}")
        clsPublic.Actualizar_Progreso(lblprg, $"Tiempo transcurrido: {tiempo} segundo(s)")

        BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
        BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS
        BeNavEjecucionRes.Exitosa = (VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS)
        clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)
    End Sub
    Private Shared Function ValidarYImportarProductosIntermedios(lblprg As RichTextBox, prg As ProgressBar, cnnLog As SqlConnection, preguntar As Boolean) As Boolean
        If Not preguntar Then
            Return Importar_Productos_Desde_SAP_A_TablaIntermedia(lblprg, prg, cnnLog)
        End If

        Dim resp = MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Parámetro", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp = DialogResult.Yes Then
            Return Importar_Productos_Desde_SAP_A_TablaIntermedia(lblprg, prg, cnnLog)
        End If
        Return True ' Si el usuario dice que no, simplemente continúa con los datos existentes
    End Function
    Private Shared Sub InicializarEjecucionProducto(cnnLog As SqlConnection)
        BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(cnnLog)
        BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
        BeNavEjecucionEnc.Fecha = Now
        clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, cnnLog)

        BeNavEjecucionRes = New clsBeI_nav_ejecucion_res With {
            .IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(cnnLog) + 1,
            .IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc,
            .IdNavConfigDet = BeConfigDet.Idnavconfigdet,
            .Registros_ws = 0,
            .Registros_ti = 0,
            .Registros_WMS = 0,
            .Exitosa = False
        }
        clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, cnnLog)
    End Sub

End Class