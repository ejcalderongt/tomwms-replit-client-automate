Imports System.Reflection
Imports SAPbobsCOM

Public Module m_Global

    Public BD As New BaseDatos

    Public IndiceInstanciaDefecto As Integer = -1

    Public IdUsuario As Integer = -1
    Public Property BeConfigEnc As New clsBeI_nav_config_enc
    Public Property RemoteCallBack As Boolean = False
    Public Property pConfigInterface As NombreInterface = NombreInterface.Kilios
    Public Property HostName As String = ""
    Public Property NoDocEntrySAP As Integer = 0
    Public Property EstadoEnviadoSAP As clsDataContractDI.Estado_Enviado_SAP? = 0
    Public Property gVersionApp As String = "7.7.8"
    Public Property gFechaVersion As Date = New Date(2025, 9, 8)
    Public Property gNombreInstancia As String = ""
    Public Property UsuarioSapFromUsuarioWMS As String = ""
    Public Property ClaveSapFromUsuarioWMS As String = ""

    Public Enum NombreInterface
        Becofarma = 0
        Kilios = 1
    End Enum

    Public Enum pInterfaceAEjecutar
        Ninguna = -1

        'Ingresos
        Importar_Bodegas = 0
        Importar_Productos = 1
        Importar_Proveedores = 2
        Importar_Pedidos_De_Compra = 3
        Importar_Pedidos_De_Transferencia = 4

        'Salidas
        Enviar_Pedidos_Compra = 5
        Enviar_Pedidos_Transferencia = 6
        Enviar_Pedidos_Cliente_SAP = 9
        Enviar_Devolucion_Proveedor_SAP = 10
        Enviar_Traslados_SAP = 11

        'Actualizacion de campo U_ENVIADO_WMS
        Actualizar_Traslado_No_Enviado = 7
        Actualizar_Pedido_Cliente_No_Enviado = 8
        Actualizar_Devolucion_Proveedor_No_Enviado_SAP = 12
        Actualizar_Traslados_No_Enviado_SAP = 13

        'Ajustes
        Enviar_Ajustes_Inventario = 20

        'Cerrar documento de salida SAP
        Cerrar_Documento_Salida_SAP = 21

    End Enum

    Public Sub CopyObject(Of tom)(ByVal ObjOrigen As Object, ByRef ObjDestino As tom)

        Dim pName As String = ""

        Try

            If ObjOrigen Is Nothing OrElse ObjDestino Is Nothing Then Return
            Dim TipoFuente As Type = ObjOrigen.[GetType]()
            Dim TipoDestino As Type = ObjDestino.[GetType]()

            If TipoFuente IsNot Nothing AndAlso TipoDestino IsNot Nothing Then

                For Each p As PropertyInfo In TipoFuente.GetProperties()

                    pName = p.Name

                    Dim ObjPI As PropertyInfo = TipoDestino.GetProperty(p.Name)

                    If ObjPI IsNot Nothing Then
                        Dim l As Object = p.GetValue(ObjOrigen, Nothing)
                        ObjPI.SetValue(ObjDestino, l)
                        ObjPI.SetValue(ObjDestino, p.GetValue(ObjOrigen, Nothing), Nothing)
                    End If

                Next

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} Campo: {2} ", MethodBase.GetCurrentMethod.Name(), ex.Message, pName))
        End Try

    End Sub

    Public Function ToDataTable(Of T)(items As List(Of T)) As DataTable
        Dim dataTable As New DataTable(GetType(T).Name)

        'Get all the properties
        Dim Props As PropertyInfo() = GetType(T).GetProperties(BindingFlags.[Public] Or BindingFlags.Instance)
        For Each prop As PropertyInfo In Props
            'Setting column names as Property names
            dataTable.Columns.Add(prop.Name)
        Next
        For Each item As T In items
            Dim values = New Object(Props.Length - 1) {}
            For i As Integer = 0 To Props.Length - 1
                'inserting property values to datatable rows
                values(i) = Props(i).GetValue(item, Nothing)
            Next
            dataTable.Rows.Add(values)
        Next
        'put a breakpoint here and check datatable
        Return dataTable
    End Function

    Public Function Existe_Ini() As Boolean

        If IO.File.Exists(CurDir() & "\Conn.ini") Then
            Existe_Ini = True
        Else
            Existe_Ini = False
        End If

    End Function

    Public Function Conectar_A_SAP_2017(ByRef oCompany As Company,
                                       Optional ByVal pThrowException As Boolean = False,
                                       Optional ByRef pCodigoError As Integer = 0,
                                       Optional ByRef pMensajeError As String = "") As Boolean

        Conectar_A_SAP_2017 = False

        pCodigoError = 0

        Try

            If oCompany Is Nothing Then
                oCompany = New Company
            End If

            If (Not oCompany.Connected) Then

                oCompany = New Company
                oCompany.SLDServer = BD.Instancia.LICENSESERVER_SAP_BO
                oCompany.Server = BD.Instancia.SERVER_BD_SAP
                oCompany.CompanyDB = BD.Instancia.SAP_COMPANY_DB
                oCompany.UserName = BD.Instancia.SAP_USR.Trim()
                oCompany.Password = BD.Instancia.SAP_USR_PW.Trim()
                oCompany.DbUserName = BD.Instancia.SAP_DB_USR.Trim()
                oCompany.DbPassword = BD.Instancia.SAP_DB_PW.Trim()
                oCompany.UseTrusted = False
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2017
                oCompany.language = BoSuppLangs.ln_Spanish_La

                Dim lRetCode As Integer = oCompany.Connect()

                If lRetCode <> 0 Then
                    oCompany.GetLastError(pCodigoError, pMensajeError)
                    If pThrowException Then
                        Throw New Exception(pMensajeError)
                    End If
                Else
                    Conectar_A_SAP_2017 = True
                End If

            End If

        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Function Desconectar_SAP(ByRef oCompany As Company) As Boolean

        Desconectar_SAP = False

        Try

            If Not IsNothing(oCompany) Then
                If oCompany.Connected Then
                    oCompany.Disconnect()
                End If
            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Conectar_A_SAP_2019(ByRef oCompany As Company,
                                       Optional ByVal pThrowException As Boolean = False,
                                       Optional ByRef pCodigoError As Integer = 0,
                                       Optional ByRef pMensajeError As String = "") As Boolean

        Conectar_A_SAP_2019 = False

        pCodigoError = 0

        Try

            If oCompany Is Nothing Then
                oCompany = New Company
            End If

            If (Not oCompany.Connected) Then

                oCompany = New Company
                oCompany.SLDServer = BD.Instancia.LICENSESERVER_SAP_BO
                oCompany.LicenseServer = BD.Instancia.SERVER_BD_SAP
                oCompany.Server = BD.Instancia.SERVER_BD_SAP
                oCompany.CompanyDB = BD.Instancia.SAP_COMPANY_DB
                oCompany.UserName = BD.Instancia.SAP_USR.Trim()
                oCompany.Password = BD.Instancia.SAP_USR_PW.Trim()
                oCompany.DbUserName = BD.Instancia.SAP_DB_USR.Trim()
                oCompany.DbPassword = BD.Instancia.SAP_DB_PW.Trim()
                oCompany.language = BoSuppLangs.ln_Spanish_La
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2019
                oCompany.UseTrusted = False

                Dim lRetCode As Integer = oCompany.Connect()
                Dim errMsg As String = oCompany.GetLastErrorDescription()
                Dim ErrNo As Integer = oCompany.GetLastErrorCode()
                Dim ErrContext As String = oCompany.GetLastErrorContext()

                If lRetCode <> 0 Then
                    oCompany.GetLastError(pCodigoError, pMensajeError)
                    If pThrowException Then
                        Throw New Exception(pMensajeError)
                    End If
                Else
                    Conectar_A_SAP_2019 = True
                End If

            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Enum pEmpresa
        Killios = 1
        Garesa = 2
    End Enum
    Public Function Conectar_A_SAP(ByRef oCompany As Company,
                                   Optional ByVal pThrowException As Boolean = False,
                                   Optional ByRef pCodigoError As Integer = 0,
                                   Optional ByRef pMensajeError As String = "",
                                   Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As Boolean

        Conectar_A_SAP = False

        pCodigoError = 0

        Try

            If oCompany Is Nothing OrElse pCompany <> pEmpresa.Killios Then
                oCompany = New Company
            End If

            If (Not oCompany.Connected) Then

                oCompany = New Company
                oCompany.SLDServer = BD.Instancia.LICENSESERVER_SAP_BO
                oCompany.Server = BD.Instancia.SERVER_BD_SAP

                If pCompany = pEmpresa.Killios Then
                    oCompany.CompanyDB = BD.Instancia.SAP_COMPANY_DB
                Else
                    oCompany.CompanyDB = BD.Instancia.SAP_COMPANY_DB2
                End If

                oCompany.UserName = BD.Instancia.SAP_USR.Trim()
                oCompany.Password = BD.Instancia.SAP_USR_PW.Trim()
                oCompany.DbUserName = BD.Instancia.SAP_DB_USR.Trim()
                oCompany.DbPassword = BD.Instancia.SAP_DB_PW.Trim()
                oCompany.language = BoSuppLangs.ln_Spanish_La
                oCompany.UseTrusted = False

                If BD.Instancia.SAP_DB_VERSION = 2017 Then
                    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2017
                ElseIf BD.Instancia.SAP_DB_VERSION = 2019 Then
                    oCompany.LicenseServer = BD.Instancia.SERVER_BD_SAP
                    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2019
                End If

                oCompany.UseTrusted = False

                Dim lRetCode As Integer = oCompany.Connect()

                If lRetCode <> 0 Then
                    oCompany.GetLastError(pCodigoError, pMensajeError)
                    If pThrowException Then
                        Throw New Exception(pMensajeError)
                    End If
                Else
                    Conectar_A_SAP = True
                End If

            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public sapPool As New SapConnectionPool(10)

End Module