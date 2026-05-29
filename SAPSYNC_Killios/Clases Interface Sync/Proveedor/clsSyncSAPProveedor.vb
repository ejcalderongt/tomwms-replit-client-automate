Imports System.Data.SqlClient
Imports SAPbobsCOM

Public Class clsSyncSAPProveedor : Inherits clsInterfaceBase

    Shared VContadorBitacoraTOMWMS As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0
    Shared lProveedores As New List(Of clsBeI_nav_proveedor)

    Private Shared Function ConstruirCriterioFiltro(nombreEntidad As String) As String
        Dim entidad = clsLnI_nav_ent.Get_Single_By_Nombre(nombreEntidad)

        If entidad Is Nothing OrElse entidad.lDetalleFiltros Is Nothing OrElse entidad.lDetalleFiltros.Count = 0 Then
            Return ""
        End If

        Dim filtros = entidad.lDetalleFiltros
        Dim criteriosAgrupados = filtros.
        GroupBy(Function(f) f.Tipo_Filtro).
        Select(Function(g)
                   Return g.Key & " IN (" & String.Join(",", g.Select(Function(f) f.Valor)) & ")"
               End Function)

        Return String.Join(" AND ", criteriosAgrupados)
    End Function

    Public Shared Function Get_Proveedores_SAP(pCompany As pEmpresa) As List(Of clsBeI_nav_proveedor)

        Dim proveedores As New List(Of clsBeI_nav_proveedor)
        Dim sapConn As SapConnectionWrapper = Nothing
        Dim rs As Recordset = Nothing

        Try
            ' Obtener filtros si existen
            Dim criterioFiltro As String = ConstruirCriterioFiltro("proveedor")

            ' Construir query SAP
            Dim query As String = "
            SELECT DISTINCT 
                T0.CARDCODE AS CODIGO,
                T0.CARDNAME AS NOMBRE_COMERCIAL,
                T0.Phone1,
                T0.CntctPrsn AS CONTACTO,
                T0.AddId AS NIT,
                ISNULL(T1.Street, '') + ' ' + ISNULL(T1.City, '') AS DIRECCION,
                T0.E_Mail
            FROM OCRD T0
            LEFT JOIN (
                SELECT * FROM CRD1 T3 
                WHERE T3.Address = 'ENTREGA' 
                  AND LINENUM = (
                      SELECT MAX(LINENUM) 
                      FROM CRD1 T4 
                      WHERE T4.Address = 'ENTREGA' 
                        AND T4.CardCode = T3.CARDCODE
                  )
            ) T1 ON T1.CardCode = T0.CardCode
            WHERE T0.CardType = 'S' AND T0.validfor = 'Y'"

            If Not String.IsNullOrWhiteSpace(criterioFiltro) Then
                query &= " AND " & criterioFiltro
            End If

            ' Obtener conexión desde pool
            sapConn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = sapConn.Company

            ' Ejecutar consulta
            rs = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(query)

            ' Leer resultados
            While Not rs.EoF
                Dim proveedor As New clsBeI_nav_proveedor With {
                .No = pCompany.ToString().Substring(0, 1) & rs.Fields.Item("CODIGO").Value.ToString(),
                .Name = rs.Fields.Item("NOMBRE_COMERCIAL").Value.ToString(),
                .Phone_No = rs.Fields.Item("Phone1").Value.ToString(),
                .Contact = rs.Fields.Item("CONTACTO").Value.ToString(),
                .VAT_Registratrion_No = rs.Fields.Item("NIT").Value.ToString(),
                .Adress = rs.Fields.Item("DIRECCION").Value.ToString(),
                .Search_Name = pCompany.ToString()
            }

                proveedores.Add(proveedor)
                rs.MoveNext()
            End While

            Return proveedores

        Catch ex As Exception
            Throw New Exception("Error al obtener proveedores desde SAP: " & ex.Message)
        Finally
            If rs IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(rs)
            If sapConn IsNot Nothing Then sapPool.ReleaseConnection(sapConn)
        End Try

    End Function

    Public Shared Function Get_Proveedor_SAP(ByVal pCodigo As String,
                                             ByVal pCompany As Integer,
                                             ByVal oCompany As Company) As clsBeI_nav_proveedor

        Get_Proveedor_SAP = Nothing

        Try

            Dim query_sap As String = $"SELECT
                                        T0.CARDCODE AS CODIGO,
                                        T0.CARDNAME AS NOMBRE_COMERCIAL,
                                        T0.Phone1,
                                        T0.CntctPrsn AS CONTACTO,
                                        T0.AddId AS NIT,
                                        ISNULL(T1.Street, '') + ' ' + ISNULL(T1.City, '') AS DIRECCION,
                                        T1.Address AS DIR1,
                                        T0.E_Mail
                                    FROM OCRD T0
                                    LEFT JOIN CRD1 T1 ON T1.CardCode = T0.CardCode AND T1.Address = 'ENTREGA'
                                    WHERE T0.CARDTYPE = 'S'
                                      AND T0.CARDCODE = '{pCodigo.Replace("'", "''")}'
                                      AND T0.validfor = 'Y'"

            Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(query_sap)

            If Not rs.EoF Then
                Dim proveedor As New clsBeI_nav_proveedor With {
                .No = rs.Fields.Item("CODIGO").Value.ToString(),
                .Name = rs.Fields.Item("NOMBRE_COMERCIAL").Value.ToString(),
                .Phone_No = rs.Fields.Item("Phone1").Value.ToString(),
                .Contact = rs.Fields.Item("CONTACTO").Value.ToString(),
                .VAT_Registratrion_No = rs.Fields.Item("NIT").Value.ToString(),
                .Adress = rs.Fields.Item("DIRECCION").Value.ToString(),
                .Search_Name = pCompany.ToString()
            }

                Get_Proveedor_SAP = proveedor
            End If

        Catch ex As Exception
            Throw New Exception("Error al consultar proveedor SAP: " & ex.Message, ex)
        End Try

    End Function

    Private Shared Function Importar_Proveedores_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                             ByRef prg As ProgressBar,
                                                                             ByRef cnnLog As SqlConnection) As Boolean
        Importar_Proveedores_Desde_SAP_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Consultando proveedores nuevos en SAP.")

            lProveedores.AddRange(Get_Proveedores_SAP(pEmpresa.Killios))
            lProveedores.AddRange(Get_Proveedores_SAP(pEmpresa.Garesa))

            BeNavEjecucionRes.Registros_ws = lProveedores.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            clsPublic.Actualizar_Progreso(lblprg, String.Format("prvoeedores encontrados en SAP: {0} ", lProveedores.Count & vbNewLine))

            prg.Maximum = lProveedores.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_proveedor.EliminarTodos(lConnection, lTransaction)

            If cnnLog.State = ConnectionState.Closed Then cnnLog.Open()

            For Each Prov In lProveedores.OrderBy(Function(x) x.Search_Name)

                Try

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Proveedor: {0} ", Prov.No))

                    Try
                        clsLnI_nav_proveedor.Insertar(Prov, lConnection, lTransaction)
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblprg, "El proveedor: " & Prov.Name & " ya existe un poco.")
                    End Try

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

            Throw ex

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Insertar_Proveedores_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                                       ByRef prg As ProgressBar,
                                                                                       Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                       Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean
        Dim cnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim cnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim transInterface As SqlTransaction = Nothing
        VContadorBitacoraTOMWMS = 0
        Dim proveedoresNav As List(Of clsBeI_nav_proveedor)

        Try

            clsPublic.Actualizar_Progreso(lblprg, $"Force_Ejecución: {ForzarEjecucion}")

            InicializarBitacoraEjecucion(cnnLog)

            cnnInterface.Open()

            transInterface = cnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            If Not CargarDatosIntermedia(lblprg, prg, cnnLog, Pregunta_Si_LLena_Intermedia) Then Return False

            proveedoresNav = clsLnI_nav_proveedor.GetAll(cnnInterface, transInterface)
            clsPublic.Actualizar_Progreso(lblprg, $"Proveedores en tabla intermedia: {proveedoresNav.Count}")

            If proveedoresNav.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No se encontraron proveedores para importar.")
                Return False
            End If

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, cnnInterface, transInterface)
            If BeConfigEnc Is Nothing Then Throw New Exception("No se encontró configuración de interface.")

            prg.Maximum = proveedoresNav.Count
            prg.Value = 0

            ProcesarProveedores(lblprg, prg, proveedoresNav, cnnInterface, transInterface, cnnLog)

            transInterface.Commit()

            FinalizarBitacora(lblprg, cnnLog)

            Return True

        Catch ex As Exception
            If transInterface IsNot Nothing Then transInterface.Rollback()
            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar Proveedor: {ex.Message}")
            Throw
        Finally
            If cnnInterface.State = ConnectionState.Open Then cnnInterface.Close()
            If cnnLog.State = ConnectionState.Open Then cnnLog.Close()
            prg.Value = 0 : prg.Visible = False
        End Try
    End Function
    Private Shared Sub InicializarBitacoraEjecucion(cnnLog As SqlConnection)
        cnnLog.Open()
        BeNavEjecucionEnc.IdEjecucionEnc = 0'0' 0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
        BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
        BeNavEjecucionEnc.Fecha = Now
        clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, cnnLog)

        BeNavEjecucionRes = New clsBeI_nav_ejecucion_res With {
        .IdEjecucionRes = 0,' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1,
        .IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc,
        .IdNavConfigDet = BeConfigDet.Idnavconfigdet,
        .Registros_ws = 0,
        .Registros_ti = 0,
        .Registros_WMS = 0,
        .Exitosa = False
    }
        clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, cnnLog)
    End Sub
    Private Shared Function CargarDatosIntermedia(lblprg As RichTextBox, prg As ProgressBar, cnnLog As SqlConnection, preguntar As Boolean) As Boolean
        If Not preguntar Then
            Return Importar_Proveedores_Desde_SAP_A_TablaIntermedia(lblprg, prg, cnnLog)
        End If

        Dim resp = MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp = DialogResult.Yes Then
            Return Importar_Proveedores_Desde_SAP_A_TablaIntermedia(lblprg, prg, cnnLog)
        End If

        Return True
    End Function
    Private Shared Sub ProcesarProveedores(lblprg As RichTextBox, prg As ProgressBar,
                                            proveedores As List(Of clsBeI_nav_proveedor),
                                            cnn As SqlConnection,
                                            trx As SqlTransaction,
                                            cnnLog As SqlConnection)

        For i = 0 To proveedores.Count - 1

            prg.Value = i

            Dim navProv = proveedores(i)

            clsPublic.Actualizar_Progreso(lblprg, $"Procesando: {navProv.No}")

            Try

                Dim existente = clsLnProveedor.Existe(GetCodigoLimpio(navProv.No), cnn, trx)

                If existente IsNot Nothing Then
                    ActualizarProveedor(navProv, existente, cnn, trx)
                Else
                    InsertarProveedorNuevo(navProv, cnn, trx)
                End If

                Marcar_Proveedor_Sincronizado_SAP(navProv.No)

                VContadorBitacoraTOMWMS += 1

            Catch ex As Exception
                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, navProv.No,
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)
                clsPublic.Actualizar_Progreso(lblprg, $"Error con proveedor {navProv.No}: {ex.Message}")
            End Try

        Next

    End Sub
    Private Shared Function GetCodigoLimpio(codigo As String) As String
        Return If(codigo.StartsWith("K") OrElse codigo.StartsWith("G"), codigo.Substring(1), codigo)
    End Function

    Private Shared Sub ActualizarProveedor(nav As clsBeI_nav_proveedor, base As clsBeProveedor, cnn As SqlConnection, trx As SqlTransaction)
        base.Nombre = nav.Name
        base.Telefono = nav.Phone_No
        base.Direccion = nav.Adress
        base.Contacto = nav.Contact
        base.Nit = nav.VAT_Registratrion_No
        base.Codigo_Empresa_ERP = nav.Search_Name
        base.Activo = True
        clsLnProveedor.Actualizar(base, cnn, trx)
    End Sub

    Private Shared Sub InsertarProveedorNuevo(nav As clsBeI_nav_proveedor, cnn As SqlConnection, trx As SqlTransaction)

        Try

            Dim nuevo As New clsBeProveedor With {
        .IdProveedor = clsLnProveedor.MaxID(cnn, trx) + 1,
        .IdEmpresa = BeConfigEnc.Idempresa,
        .IdPropietario = BeConfigEnc.IdPropietario,
        .Codigo = nav.No,
        .Nombre = nav.Name,
        .Telefono = nav.Phone_No,
        .Direccion = nav.Adress,
        .Contacto = nav.Contact,
        .Nit = nav.VAT_Registratrion_No,
        .Codigo_Empresa_ERP = nav.Search_Name,
        .Activo = True,
        .User_agr = BeConfigEnc.IdUsuario,
        .Fec_agr = Date.UtcNow,
        .User_mod = BeConfigEnc.IdUsuario,
        .Fec_mod = Date.UtcNow
    }

            clsLnProveedor.Insertar(nuevo, cnn, trx)

            Dim lBodegas As New List(Of clsBeBodega)
            lBodegas = clsLnBodega.GetAll(cnn, trx)

            For Each Bod In lBodegas

                Dim pb As New clsBeProveedor_bodega With {
                    .IdAsignacion = clsLnProveedor_bodega.MaxID(cnn, trx) + 1,
                    .IdProveedor = nuevo.IdProveedor,
                    .IdBodega = Bod.IdBodega,
                    .Activo = True,
                    .User_agr = BeConfigEnc.IdUsuario,
                    .User_mod = BeConfigEnc.IdUsuario,
                    .Fec_agr = Now,
                    .Fec_mod = Now}

                clsLnProveedor_bodega.InsertarFromInterface(pb, cnn, trx)

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Shared Sub FinalizarBitacora(lblprg As RichTextBox, cnnLog As SqlConnection)

        Dim duracion = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
        clsPublic.Actualizar_Progreso(lblprg, $"Tiempo transcurrido: {duracion} segundos")
        clsPublic.Actualizar_Progreso(lblprg, $"Proveedores procesados: {VContadorBitacoraTOMWMS}")

        BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
        BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS
        BeNavEjecucionRes.Exitosa = (VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS)

        clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

    End Sub


    Public Shared Function Marcar_Proveedor_Sincronizado_SAP(ByVal pCodigoProveedor As String,
                                                            Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As Boolean

        Dim conn As SapConnectionWrapper = Nothing
        Marcar_Proveedor_Sincronizado_SAP = False

        Try
            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company
            Dim safeCodigo As String = pCodigoProveedor.Replace("'", "''")
            Dim sQuery As String = $"UPDATE OCRD SET U_Enviado_WMS = '1' WHERE CardCode = '{safeCodigo}'"
            Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(sQuery)
            Marcar_Proveedor_Sincronizado_SAP = True
        Catch ex As Exception
            If ex.Message.Contains("Invalid field name") Then
                Throw New Exception("El campo U_Enviado_WMS no existe en la tabla OCRD.")
            Else
                Throw New Exception("Error al marcar proveedor como sincronizado en SAP: " & ex.Message, ex)
            End If
        Finally
            If conn IsNot Nothing Then sapPool.ReleaseConnection(conn)
        End Try

    End Function

    Public Shared Function Get_Proveedor_Devolucion_SAP(pCodigo As String, pEmpresaOrigen As Integer) As clsBeI_nav_proveedor
        Dim sapConn As SapConnectionWrapper = Nothing
        Dim rs As Recordset = Nothing

        Try
            ' Obtener conexión desde pool
            sapConn = sapPool.GetConnection(pEmpresaOrigen)
            Dim oCompany As Company = sapConn.Company

            ' Ejecutar consulta
            Dim query As String = $"SELECT 
                                    T0.CARDCODE AS CODIGO,
                                    T0.CARDNAME AS NOMBRE_COMERCIAL,
                                    T0.Phone1, 
                                    ISNULL(T1.FirstName,'ND') AS CONTACTO,  
                                    T0.AddId AS NIT, 
                                    T0.Address AS DIRECCION, 
                                    T0.E_Mail 
                               FROM OCRD T0 
                               LEFT JOIN OCPR T1 ON T0.CardCode = T1.CardCode
                               WHERE T0.CARDTYPE = 'C' AND T0.CARDCODE = '{pCodigo}' AND T0.ValidFor = 'Y'"

            rs = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(query)

            If rs.EoF Then Return Nothing

            Return MapearProveedorDesdeRecordset(rs, pEmpresaOrigen)

        Catch ex As Exception
            Throw New Exception("Error al consultar proveedor desde SAP: " & ex.Message)
        Finally
            If rs IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rs)
            If sapConn IsNot Nothing Then sapPool.ReleaseConnection(sapConn)
        End Try
    End Function
    Private Shared Function MapearProveedorDesdeRecordset(rs As Recordset, empresa As Integer) As clsBeI_nav_proveedor
        Return New clsBeI_nav_proveedor With {
        .No = rs.Fields.Item("CODIGO").Value.ToString(),
        .Name = rs.Fields.Item("NOMBRE_COMERCIAL").Value.ToString(),
        .Phone_No = rs.Fields.Item("Phone1").Value.ToString(),
        .Contact = rs.Fields.Item("CONTACTO").Value.ToString(),
        .VAT_Registratrion_No = rs.Fields.Item("NIT").Value.ToString(),
        .Adress = rs.Fields.Item("DIRECCION").Value.ToString(),
        .Search_Name = empresa.ToString()
    }
    End Function


End Class