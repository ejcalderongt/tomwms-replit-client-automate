Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM

Public Class clsSyncSAPProveedor : Inherits clsInterfaceBase
    Implements IDisposable

    Shared VContadorBitacoraTOMWMS As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0
    Dim lProveedores As New List(Of clsBeI_nav_proveedor)
    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Private Shared oCompany As Company
    Private Shared lRetCode, lErrCode As Long
    Private Shared sErrMsg As String = ""

    Public Shared Function Get_Proveedores_SAP() As List(Of clsBeI_nav_proveedor)

        Get_Proveedores_SAP = Nothing

        Dim lReturnList As New List(Of clsBeI_nav_proveedor)
        Dim query_sap As String = ""

        Try

            Dim BeNavEnt As New clsBeI_nav_ent
            BeNavEnt = clsLnI_nav_ent.Get_Single_By_Nombre("proveedor")
            Dim vCriteria As String = ""
            Dim TieneFiltros As Boolean = False
            Dim vContador As Integer = 0

            If Not BeNavEnt Is Nothing Then

                If Not BeNavEnt.lDetalleFiltros Is Nothing Then

                    If BeNavEnt.lDetalleFiltros.Count > 0 Then

                        TieneFiltros = True

                        Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
                        lFiltros = BeNavEnt.lDetalleFiltros

                        For Each FiltroCategoria In lFiltros
                            If vContador = 0 Then
                                vCriteria = FiltroCategoria.Tipo_Filtro & " in ("
                                vCriteria += FiltroCategoria.Valor
                            Else
                                vCriteria += "," & FiltroCategoria.Valor
                            End If
                        Next

                        vCriteria += ")"

                    End If

                End If

            End If


            'If pConfigInterface = NombreInterface.Becofarma Then

            query_sap = "SELECT T0.CARDCODE AS CODIGO,
                             T0.CARDNAME AS NOMBRE_COMERCIAL ,
                             T0.Phone1, 
                             ISNULL(T1.FirstName,'ND') AS CONTACTO,  
                             T0.u_nit As NIT, 
                             T0.Address As DIRECCION, 
                             T0.E_Mail 
                             FROM OCRD T0 LEFT JOIN OCPR T1 ON T0.CardCode = T1.CardCode
                             WHERE (T0.CARDTYPE = 'S' AND T0.validfor = 'Y')"
            If TieneFiltros Then
                query_sap += " AND " & vCriteria
            End If

            'Else
            '    query_sap = "SELECT T0.CARDCODE AS CODIGO,
            '                 T0.CARDNAME AS NOMBRE_COMERCIAL ,
            '                 T0.Phone1, 
            '                 ISNULL(T1.FirstName,'ND') AS CONTACTO,  
            '                 T0.u_nit As NIT, 
            '                 T0.Address As DIRECCION, 
            '                 T0.E_Mail 
            '                 FROM OCRD T0 LEFT JOIN OCPR T1 ON T0.CardCode = T1.CardCode
            '                 WHERE (T0.CARDTYPE = 'S'  
            '                 AND t0.U_Enviado_WMS= '2') "
            'End If


            Conectar_A_SAP(oCompany, False, lRetCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeProveedor As clsBeI_nav_proveedor

                While rs.EoF = False

                    BeProveedor = New clsBeI_nav_proveedor()
                    BeProveedor.No = (rs.Fields.Item(0).Value.ToString())
                    BeProveedor.Name = (rs.Fields.Item(1).Value.ToString())
                    BeProveedor.Phone_No = (rs.Fields.Item(2).Value.ToString())
                    BeProveedor.Contact = (rs.Fields.Item(3).Value.ToString())
                    BeProveedor.VAT_Registratrion_No = (rs.Fields.Item(4).Value.ToString())
                    BeProveedor.Adress = (rs.Fields.Item(5).Value.ToString())
                    BeProveedor.Search_Name = (rs.Fields.Item(6).Value.ToString())
                    lReturnList.Add(BeProveedor)

                    rs.MoveNext()

                End While

            End If

            oCompany.Disconnect()

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Proveedor_SAP(ByVal pCodigo As String) As clsBeI_nav_proveedor

        Get_Proveedor_SAP = Nothing

        Dim lReturnList As New List(Of clsBeI_nav_proveedor)

        Try

            Dim query_sap As String = "SELECT T0.CARDCODE AS CODIGO,
                                              T0.CARDNAME AS NOMBRE_COMERCIAL ,
                                              T0.Phone1, 
                                              ISNULL(T1.FirstName,'ND') AS CONTACTO,  
                                              T0.u_nit As NIT, 
                                              T0.Address As DIRECCION, 
                                              T0.E_Mail 
                                              FROM OCRD T0 LEFT JOIN OCPR T1 ON T0.CardCode = T1.CardCode
                                              WHERE (T0.CARDTYPE = 'S'  
                                              AND t0.CARDCODE= '" & pCodigo & "' AND T0.validfor = 'Y' AND GroupCode = 101) "

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeProveedor As clsBeI_nav_proveedor

                While rs.EoF = False

                    BeProveedor = New clsBeI_nav_proveedor()
                    BeProveedor.No = (rs.Fields.Item(0).Value.ToString())
                    BeProveedor.Name = (rs.Fields.Item(1).Value.ToString())
                    BeProveedor.Phone_No = (rs.Fields.Item(2).Value.ToString())
                    BeProveedor.Contact = (rs.Fields.Item(3).Value.ToString())
                    BeProveedor.VAT_Registratrion_No = (rs.Fields.Item(4).Value.ToString())
                    BeProveedor.Adress = (rs.Fields.Item(5).Value.ToString())
                    BeProveedor.Search_Name = (rs.Fields.Item(6).Value.ToString())
                    Return BeProveedor

                    rs.MoveNext()

                End While

            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Shared Function Get_Proveedor_Devolucion_SAP(ByVal pCodigo As String) As clsBeI_nav_proveedor

        Get_Proveedor_Devolucion_SAP = Nothing

        Dim lReturnList As New List(Of clsBeI_nav_proveedor)

        Try

            Dim query_sap As String = "SELECT T0.CARDCODE AS CODIGO,
                                              T0.CARDNAME AS NOMBRE_COMERCIAL ,
                                              T0.Phone1, 
                                              ISNULL(T1.FirstName,'ND') AS CONTACTO,  
                                              T0.u_nit As NIT, 
                                              T0.Address As DIRECCION, 
                                              T0.E_Mail 
                                              FROM OCRD T0 LEFT JOIN OCPR T1 ON T0.CardCode = T1.CardCode
                                              WHERE (T0.CARDTYPE = 'C'  
                                              AND t0.CARDCODE= '" & pCodigo & "' AND T0.validfor = 'Y') "
            '#CKFK20240503 Quité esto del where
            ' AND GroupCode = 101

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeProveedor As clsBeI_nav_proveedor

                While rs.EoF = False

                    BeProveedor = New clsBeI_nav_proveedor()
                    BeProveedor.No = (rs.Fields.Item(0).Value.ToString())
                    BeProveedor.Name = (rs.Fields.Item(1).Value.ToString())
                    BeProveedor.Phone_No = (rs.Fields.Item(2).Value.ToString())
                    BeProveedor.Contact = (rs.Fields.Item(3).Value.ToString())
                    BeProveedor.VAT_Registratrion_No = (rs.Fields.Item(4).Value.ToString())
                    BeProveedor.Adress = (rs.Fields.Item(5).Value.ToString())
                    BeProveedor.Search_Name = (rs.Fields.Item(6).Value.ToString())
                    Return BeProveedor

                    rs.MoveNext()

                End While

            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing
    Private Function Importar_Proveedores_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                       ByRef prg As ProgressBar,
                                                                       ByRef cnnLog As SqlConnection) As Boolean
        Importar_Proveedores_Desde_SAP_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Consultando proveedores nuevos en SAP.")

            lProveedores = Get_Proveedores_SAP()

            BeNavEjecucionRes.Registros_ws = lProveedores.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            clsPublic.Actualizar_Progreso(lblprg, String.Format("prvoeedores encontrados en WS: {0} ", lProveedores.Count & vbNewLine))

            prg.Maximum = lProveedores.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_proveedor.EliminarTodos(lConnection, lTransaction)

            If cnnLog.State = ConnectionState.Closed Then cnnLog.Open()

            For Each Prov In lProveedores

                Try

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Proveedor: {0} ", Prov.No, vbNewLine))

                    clsLnI_nav_proveedor.Insertar(Prov, lConnection, lTransaction)

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                              Prov.No,
                                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                                              BeConfigDet.Idnavconfigdet,
                                                              cnnLog)

                    clsPublic.Actualizar_Progreso(lblprg, "Error al insertar desde SAP a intermedia: " & Prov.No & vbNewLine &
                                                   ex.Message)

                End Try

            Next

            lTransaction.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de inserción en tabla intermedia.")

            Importar_Proveedores_Desde_SAP_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar proveedores desde SAP a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Insertar_Proveedor_Single(ByVal NoProveedor As String,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction,
                                                     ByRef lConnectionLog As SqlConnection,
                                                     ByRef lblprg As RichTextBox,
                                                     ByRef prg As ProgressBar) As clsBeProveedor_bodega

        Insertar_Proveedor_Single = Nothing

        Try

            Dim navProveedor As New clsBeI_nav_proveedor

            navProveedor = Get_Proveedor_SAP(NoProveedor)

            Dim BeProveedor As New clsBeProveedor
            Dim BeProveedorBodega As New clsBeProveedor_bodega

            If Not navProveedor Is Nothing Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Proveedor: {0} ", navProveedor.No, vbNewLine))

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1

                BeProveedor.Codigo = navProveedor.No
                BeProveedor.Nombre = navProveedor.Name
                BeProveedor.Telefono = IIf(navProveedor.Phone_No Is Nothing, "", navProveedor.Phone_No)
                BeProveedor.Nit = IIf(navProveedor.VAT_Registratrion_No Is Nothing, "", navProveedor.VAT_Registratrion_No)
                BeProveedor.Direccion = IIf(navProveedor.Adress Is Nothing, "", navProveedor.Adress)
                BeProveedor.Contacto = IIf(navProveedor.Contact Is Nothing, "", navProveedor.Contact)
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    VContadorBitacoraTOMWMS += 1

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

                    Return BeProveedorBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, ex.Message))

                End Try

            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Proveedor a tabla de TOMWMS: {0}", ex.Message))
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Insertar_Proveedores_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                               ByRef prg As ProgressBar,
                                                                               Optional ByVal ForzarEjecucion As Boolean = False,
                                                                               Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Proveedores_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing

        VContadorBitacoraTOMWMS = 0

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Force_Ejecución: " & ForzarEjecucion)

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Proveedor") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento.")
                    Exit Function
                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc =0' 0'0' 0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = 0'0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Proveedores_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Proveedores_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If

                End If

            End If

            Dim lProveedoresFromNav As New List(Of clsBeI_nav_proveedor)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando proveedores en tabla intermedia ")

            lProveedoresFromNav = clsLnI_nav_proveedor.GetAll(CnnInterface, lTransInterface)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Proveedores en tabla intermedia: {0}", lProveedoresFromNav.Count))

            If lProveedoresFromNav.Count > 0 Then

                Dim BeProveedor As clsBeProveedor = Nothing
                Dim BeProveedorBodega As clsBeProveedor_bodega = Nothing
                Dim BeProveedorExistente As clsBeProveedor = Nothing

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnInterface, lTransInterface)

                If BeConfigEnc Is Nothing Then
                    Throw New Exception("No se obtuvo la configuración de interface para el identificador: BD.Instancia.IdConfiguracionInterface = " & BD.Instancia.IdConfiguracionInterface)
                End If

                prg.Maximum = lProveedoresFromNav.Count

                Dim vContador As Integer = 0

                prg.Value = 0

                clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAPBO...")

                If Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then
                    clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
                Else
                    clsPublic.Actualizar_Progreso(lblprg, "No se pudo conectar a SAPBO: " & sErrMsg)
                    Exit Function
                End If

                If Not lProveedoresFromNav Is Nothing Then

                    If lProveedoresFromNav.Count > 0 Then

                        clsPublic.Actualizar_Progreso(lblprg, "Trasladando documento a TOMWMS.")

                        For Each navProveedor As clsBeI_nav_proveedor In lProveedoresFromNav

                            BeProveedor = New clsBeProveedor
                            BeProveedorExistente = New clsBeProveedor

                            BeProveedorExistente = clsLnProveedor.Existe(navProveedor.No, CnnInterface, lTransInterface)

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Proveedor en WMS: {0}", navProveedor.No))

                            prg.Value = vContador

                            vContador += 1

                            If Not BeProveedorExistente Is Nothing Then

                                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                                BeProveedor.IdProveedor = BeProveedorExistente.IdProveedor
                                BeProveedor.Codigo = navProveedor.No
                                BeProveedor.Nombre = navProveedor.Name
                                BeProveedor.Telefono = navProveedor.Phone_No
                                BeProveedor.Nit = navProveedor.VAT_Registratrion_No
                                BeProveedor.Direccion = navProveedor.Adress
                                BeProveedor.Contacto = navProveedor.Contact
                                BeProveedor.Activo = True

                                Try

                                    clsLnProveedor.Actualizar(BeProveedor, CnnInterface, lTransInterface)

                                Catch ex As Exception

                                    Dim vMensaje As String = "Error al actualizar en WMS: " & ex.Message

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensaje,
                                                                               BeProveedor.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al actualizar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, vMensaje))

                                End Try

                                Try

                                    'Marcar_Enviado_SAP
                                    Marcar_Proveedor_Sincronizado_SAP(BeProveedor.Codigo)

                                Catch ex As Exception

                                    Dim vMensaje As String = "Error al actualizar en SAP: " & ex.Message

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensaje,
                                                                               BeProveedor.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al actualizar proveedor en SAP: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, vMensaje))

                                End Try

                                VContadorBitacoraTOMWMS += 1

                            Else

                                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                                BeProveedor.IdProveedor = clsLnProveedor.MaxID(CnnInterface, lTransInterface) + 1
                                BeProveedor.Codigo = navProveedor.No
                                BeProveedor.Nombre = navProveedor.Name
                                BeProveedor.Telefono = navProveedor.Phone_No
                                BeProveedor.Nit = navProveedor.VAT_Registratrion_No
                                BeProveedor.Direccion = navProveedor.Adress
                                BeProveedor.Contacto = navProveedor.Contact
                                BeProveedor.Activo = True
                                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                                BeProveedor.Fec_agr = Date.UtcNow
                                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                                BeProveedor.Fec_mod = Date.UtcNow

                                Try

                                    clsLnProveedor.Insertar(BeProveedor, CnnInterface, lTransInterface)

                                    VContadorBitacoraTOMWMS += 1

                                    BeProveedorBodega = New clsBeProveedor_bodega
                                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(CnnInterface, lTransInterface) + 1
                                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                                    BeProveedorBodega.Activo = True
                                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                                    BeProveedorBodega.Fec_agr = Now
                                    BeProveedorBodega.Fec_mod = Now

                                    clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, CnnInterface, lTransInterface)


                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               BeProveedor.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, ex.Message))

                                End Try

                                Try

                                    'Marcar_Enviado_SAP
                                    Marcar_Proveedor_Sincronizado_SAP(BeProveedor.Codigo)

                                Catch ex As Exception

                                    Dim vMensaje As String = "Error al actualizar en SAP: " & ex.Message

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensaje,
                                                                               BeProveedor.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al actualizar proveedor en SAP: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, vMensaje))

                                End Try

                                Application.DoEvents()

                            End If

                        Next

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "No se encontraron registros para importar.")
                    End If

                End If

            End If

            lTransInterface.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de inserción en TOMWMS.")
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Proveedores procesados correctamente: {0}", VContadorBitacoraTOMWMS))

            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS

            If VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS Then
                BeNavEjecucionRes.Exitosa = True
            Else
                BeNavEjecucionRes.Exitosa = False
            End If

            '#EJC20171006_0106AM: Se quitó la tnrasacción porque se mandaba la de la interface.
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

            Insertar_Proveedores_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = True

        Catch ex As Exception
            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            lblprg.AppendText(String.Format("Error al insertar Proveedor a tabla de TOMWMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
            prg.Visible = False
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Shared Function Marcar_Proveedor_Sincronizado_SAP(ByVal pCodigoProveedor As String) As Boolean

        Marcar_Proveedor_Sincronizado_SAP = False

        Try

            If oCompany.Connected Then

                If lRetCode <> 0 Then
                    oCompany.GetLastError(lErrCode, sErrMsg)
                    Throw New Exception(sErrMsg)
                Else
                    Dim oBusinessPartnerSBO As BusinessPartners = CType(oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners), BusinessPartners)

                    If oBusinessPartnerSBO.GetByKey(pCodigoProveedor) Then
                        oBusinessPartnerSBO.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                        oBusinessPartnerSBO.Update()
                        Marcar_Proveedor_Sincronizado_SAP = True
                    End If

                End If

            End If

        Catch ex As Exception
            If ex.Message.Contains("Invalid field name") Then
                Throw New Exception("El campo U_Enviado_WMS no existe en la tabla de socios de negocio (oBusinessPartners) ")
            Else
                Throw ex
            End If
        End Try

    End Function

End Class