Imports System.Data.SqlClient
Imports System.Net.NetworkInformation
Imports System.Reflection
Imports TOMWMS.clsBeLicencia_item

Partial Public Class clsLnLicencia_item

    Public Shared Function Get_Estatus_Licencia(ByRef pHostName As String) As eEstatusLicencia

        Get_Estatus_Licencia = eEstatusLicencia.No_Valida

        Try

            Dim pBeLicenciaItem As New clsBeLicencia_item
            pBeLicenciaItem = Get_BeLicencia_Item(pHostName)

            If Not pBeLicenciaItem Is Nothing Then

                If Not GetSingle(pBeLicenciaItem) Then

                    '#EJC20171108_REF10_1247AM: Solicitud de licencia.
                    Dim Belicencia_solic As New clsBeLicencia_solic() With {.IdDisp = pBeLicenciaItem.IdDisp,
                        .Identificacion = pBeLicenciaItem.Identificacion,
                        .Tipo = 1} 'Tipo = 1 -> BOF
                    If Not clsLnLicencia_solic.Exist(pBeLicenciaItem.IdDisp) Then
                        clsLnLicencia_solic.Insertar(Belicencia_solic)
                    End If

                    Registra_Ingreso(pHostName)

                    Get_Estatus_Licencia = eEstatusLicencia.Pendiente_Solicitud

                Else
                    Get_Estatus_Licencia = eEstatusLicencia.Activa
                End If

            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Estatus_Licencia(ByRef pHostName As String, ByVal BuscarMac As Boolean) As eEstatusLicencia

        Get_Estatus_Licencia = eEstatusLicencia.No_Valida

        Try

            Dim pBeLicenciaItem As New clsBeLicencia_item
            pBeLicenciaItem = Get_BeLicencia_Item(pHostName, BuscarMac)

            If Not pBeLicenciaItem Is Nothing Then

                If Not GetSingle(pBeLicenciaItem) Then

                    Registra_Ingreso(pHostName)

                    Get_Estatus_Licencia = eEstatusLicencia.Pendiente_Solicitud

                Else
                    Get_Estatus_Licencia = eEstatusLicencia.Activa
                End If

            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Estatus_Licencia(ByVal IdHH As String, ByVal pNomHH As String) As eEstatusLicencia

        Get_Estatus_Licencia = eEstatusLicencia.No_Valida

        Try

            IdHH = clsPublic.EncodeString(IdHH)
            pNomHH = clsPublic.EncodeString(pNomHH)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim pBeLicenciaItem As New clsBeLicencia_item
                    pBeLicenciaItem = Get_BeLicencia_Item(IdHH, pNomHH, lConnection, lTransaction)

                    '#JP20171114
                    'If Not pBeLicenciaItem Is Nothing Then
                    If pBeLicenciaItem Is Nothing Then

                        'Tipo = 0 -> 3HH
                        Dim Belicencia_solic As New clsBeLicencia_solic() With {.IdDisp = IdHH, .Identificacion = pNomHH, .Tipo = 0}

                        Try
                            clsLnLicencia_solic.Insertar(Belicencia_solic, lConnection, lTransaction)
                        Catch ex As Exception
                        End Try

                        Get_Estatus_Licencia = eEstatusLicencia.Pendiente_Solicitud

                    Else
                        Get_Estatus_Licencia = eEstatusLicencia.Activa
                    End If

                    Registra_Ingreso(IdHH, lConnection, lTransaction)

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Estatus_Licencia_HH(ByVal IdHH As String, ByVal pNomHH As String) As eEstatusLicencia

        Get_Estatus_Licencia_HH = eEstatusLicencia.No_Valida

        Try

            IdHH = clsPublic.EncodeString(IdHH)
            pNomHH = clsPublic.EncodeString(pNomHH)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim pBeLicenciaItem As New clsBeLicencia_item
                    pBeLicenciaItem = Get_BeLicencia_Item(IdHH, pNomHH, lConnection, lTransaction)

                    '#JP20171114
                    'If Not pBeLicenciaItem Is Nothing Then
                    If pBeLicenciaItem Is Nothing Then

                        'Tipo = 0 -> 3HH
                        Dim Belicencia_solic As New clsBeLicencia_solic() With {.IdDisp = IdHH, .Identificacion = pNomHH, .Tipo = 0}

                        Try
                            clsLnLicencia_solic.Insertar(Belicencia_solic, lConnection, lTransaction)
                        Catch ex As Exception
                        End Try

                        Get_Estatus_Licencia_HH = eEstatusLicencia.Pendiente_Solicitud

                    Else
                        Get_Estatus_Licencia_HH = eEstatusLicencia.Activa
                    End If

                    Registra_Ingreso_HH(IdHH, lConnection, lTransaction)

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Registra_Ingreso(ByRef pHostName As String)

        Dim vFechaIngreso As String

        Try

            Dim Belicencia_item As New clsBeLicencia_item
            Dim BeListLicLlave As New List(Of clsBeLicencia_llave)

            Belicencia_item = Get_BeLicencia_Item(pHostName)
            BeListLicLlave = clsLnLicencia_llave.GetAll()

            vFechaIngreso = Format(Now, "dd-MM-yyyy")
            vFechaIngreso = clsPublic.EncodeString(vFechaIngreso)

            '#CKFK 20180527 01:18 AM Modifiqu� el valor que se ingresa en la tabla licencia_login, porque la fecha de ingreso no me sirve para saber la fecha de vencimiento, ni la cantidad de licencias de BOF y HH
            '#CKFK 20180527 01:18 AM Modifiqu� el IdDisp que se ingresa en la tabla licencia_login, porque no es el mismo que se ingresa en la tabla licencia_item entonces a la hora de buscar el dato devuelve nulo en la consulta
            'Dim Belicencia_login As New clsBeLicencia_login() With {.IdDisp = Belicencia_item.IdDisp, .Valor = vFechaIngreso}
            Dim Belicencia_login As New clsBeLicencia_login() With {.IdDisp = Belicencia_item.Identificacion, .Valor = vFechaIngreso}

            If Not clsLnLicencia_login.Exist(Belicencia_item.Identificacion) Then
                clsLnLicencia_login.Insertar(Belicencia_login)
            Else
                clsLnLicencia_login.Actualizar(Belicencia_login)
            End If

            Registra_Conexion(pHostName)

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Registra_Ingreso(ByRef pHostName As String,
                                       ByRef lConnection As SqlConnection,
                                       ByRef lTransaction As SqlTransaction)

        Dim vFechaIngreso As String

        Try

            Dim Belicencia_item As New clsBeLicencia_item
            Dim BeListLicLlave As New List(Of clsBeLicencia_llave)

            Belicencia_item = Get_BeLicencia_Item(pHostName)
            BeListLicLlave = clsLnLicencia_llave.GetAll(lConnection, lTransaction)

            '#EJC20220119: Porqu� no incluir la hora?
            vFechaIngreso = Now 'Format(Now, "dd-MM-yyyy")
            vFechaIngreso = clsPublic.EncodeString(vFechaIngreso)

            Dim Belicencia_login As New clsBeLicencia_login() With {.IdDisp = Belicencia_item.Identificacion, .Valor = vFechaIngreso}

            If Not clsLnLicencia_login.Exist(Belicencia_item.Identificacion, lConnection, lTransaction) Then
                clsLnLicencia_login.Insertar(Belicencia_login, lConnection, lTransaction)
            Else
                clsLnLicencia_login.Actualizar(Belicencia_login, lConnection, lTransaction)
            End If

            '#EJC20220119: Porqu� registrar conexi�n si ya se actualiz� arriba?
            'Registra_Conexion(pHostName, True, lConnection, lTransaction)

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Registra_Ingreso_HH(ByRef pHostName As String,
                                          ByRef lConnection As SqlConnection,
                                          ByRef lTransaction As SqlTransaction)

        Dim sFechaIngreso As String = ""

        Try

            Dim Belicencia_item As New clsBeLicencia_item

            '#EJC20220119: Porqu� no incluir la hora?
            sFechaIngreso = FormatoFechas.fFechaHora(Now) 'Format(Now, "dd-MM-yyyy")
            sFechaIngreso = sFechaIngreso.Replace("'", "")
            sFechaIngreso = clsPublic.EncodeString(sFechaIngreso)

            Dim Belicencia_login As New clsBeLicencia_login() With {.IdDisp = pHostName, .Valor = sFechaIngreso}

            If Not clsLnLicencia_login.Exist(Belicencia_login.IdDisp, lConnection, lTransaction) Then
                clsLnLicencia_login.Insertar(Belicencia_login, lConnection, lTransaction)
            Else
                clsLnLicencia_login.Actualizar(Belicencia_login, lConnection, lTransaction)
            End If

            '#EJC20220119: Porqu� registrar conexi�n si ya se actualiz� arriba?
            'Registra_Conexion(pHostName, True, lConnection, lTransaction)

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Registra_Conexion(ByVal pHostName As String)

        Try

            Dim pBeLicenciaItem As New clsBeLicencia_item
            pBeLicenciaItem = Get_BeLicencia_Item(pHostName)

            If GetSingle(pBeLicenciaItem) Then
                '#20171111:0856PM
                'Existe la licencia para este host y se obtuvo el par�metro Tipo para saber si es el servidor de licencias.
            End If

            Dim Belicencia_item As New clsBeLicencia_item
            Belicencia_item = pBeLicenciaItem
            Belicencia_item.Estado = "" 'vEstado

            Actualizar(Belicencia_item)

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Registra_Conexion(ByVal pHostName As String,
                                        ByVal Activa As Boolean,
                                        ByRef lConnection As SqlConnection,
                                        ByRef lTransaction As SqlTransaction)

        Try

            Dim pBeLicenciaItem As New clsBeLicencia_item
            pBeLicenciaItem = Get_BeLicencia_Item(pHostName)

            If GetSingle(pBeLicenciaItem, lConnection, lTransaction) Then
                '#20171111:0856PM
                'Existe la licencia para este host y se obtuvo el par�metro Tipo para saber si es el servidor de licencias.
            End If

            Dim Belicencia_item As New clsBeLicencia_item
            Belicencia_item = pBeLicenciaItem
            Belicencia_item.Estado = "" 'vEstado

            Actualizar(Belicencia_item, lConnection, lTransaction)

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_All_Activos() As List(Of clsBeLicencia_item)

        Try

            Dim lReturnList As New List(Of clsBeLicencia_item)

            Dim vSQL As String = "SELECT licencia_item.idDisp, licencia_item.identificacion, 
                                 licencia_item.tipo, licencia_login.valor, licencia_item.bandera, licencia_item.estado, fecha_sistema 
                                 FROM licencia_item LEFT OUTER JOIN 
                                 licencia_login ON licencia_item.idDisp = licencia_login.idDisp 
                                 ORDER BY fecha_sistema DESC "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim BeLicItem As New clsBeLicencia_item

            For Each dr As DataRow In dt.Rows

                BeLicItem = New clsBeLicencia_item

                Cargar(BeLicItem, dr)

                Try

                    BeLicItem.IdDisp = clsPublic.DecodeString(BeLicItem.IdDisp)
                    BeLicItem.Identificacion = clsPublic.DecodeString(BeLicItem.Identificacion)

                Catch ex As Exception
                    Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                    Throw ex
                End Try


                BeLicItem.Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                Dim vBandera = IIf(IsDBNull(dr.Item("valor")), "", dr.Item("valor"))

                If BeLicItem.Estado <> "" Then

                    Try

                        If BeLicItem.Estado.Length >= 40 Then
                            Try
                                BeLicItem.Estado = clsPublic.DecodeString(BeLicItem.Estado)
                            Catch ex As Exception

                            End Try
                        Else
                            BeLicItem.Estado = BeLicItem.Estado
                        End If

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    If Mid(BeLicItem.Estado, 4, 1) = "1" Then BeLicItem.Estado = "Conectado" Else BeLicItem.Estado = ""

                End If

                If vBandera <> "" Then

                    Try
                        vBandera = clsPublic.DecodeString(vBandera)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                    '#EJC20181207: En Idealsa por alguna reazon, no puedeconvertir la fecha cuando viene con gui�n.
                    vBandera = Replace(vBandera, "-", "/")

                    Try

                        DateTime.TryParse(vBandera, BeLicItem.Vence)

                        If BeLicItem.Vence = "12:00:00 AM" Then

                            If vBandera.ToString.Length = 17 Then

                                '#EJC20220119: Recuperar fecha guardada, en este formato para de la HH.
                                '20220119 13:59:59'
                                Dim vsFecha As String = vBandera.ToString.Substring(0, 8)
                                Dim vsHora As String = vBandera.ToString.Substring(8, 9)
                                Dim vtFecha As Date = FormatoFechas.sFecha_To_Date(vsFecha)
                                Dim vtHora As Date = DateTime.Parse(vsHora)
                                Dim vFechaResult As DateTime = New Date(vtFecha.Year, vtFecha.Month, vtFecha.Day, vtHora.Hour, vtHora.Minute, vtHora.Second)
                                BeLicItem.Vence = vFechaResult

                            End If

                        End If

                    Catch ex As Exception
                        BeLicItem.Vence = "01/01/2019"
                    End Try

                Else
                    BeLicItem.Vence = "01/01/1900"
                End If

                lReturnList.Add(BeLicItem)

            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_Aprobacion() As List(Of clsBeLicencia_solic)

        Try

            Dim lReturnList As New List(Of clsBeLicencia_solic)

            Dim vSQL As String = "SELECT *
                                  FROM licencia_solic 
                                  ORDER BY fecha_solicitud DESC "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim BeLicItem As New clsBeLicencia_solic

            For Each dr As DataRow In dt.Rows

                BeLicItem = New clsBeLicencia_solic

                clsLnLicencia_solic.Cargar(BeLicItem, dr)

                BeLicItem.IdDisp = clsPublic.DecodeString(BeLicItem.IdDisp)
                BeLicItem.Identificacion = clsPublic.DecodeString(BeLicItem.Identificacion)
                BeLicItem.Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))

                If BeLicItem.Estado <> "" Then
                    BeLicItem.Estado = clsPublic.DecodeString(BeLicItem.Estado)
                    If Mid(BeLicItem.Estado, 4, 1) = "1" Then BeLicItem.Estado = "Conectado" Else BeLicItem.Estado = ""
                End If

                lReturnList.Add(BeLicItem)

            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub CargarItem(ByRef pItem As clsBeLicencia_item, ByRef dr As DataRow)

        Dim est As String

        Try

            pItem.IdDisp = clsPublic.DecodeString(dr.Item("idDisp"))
            pItem.Identificacion = clsPublic.DecodeString(dr.Item("identificacion"))
            pItem.Tipo = IIf(dr.Item("tipo") = 1, "BackOffice", "HandHeld")
            pItem.Bandera = IIf(dr.Item("bandera") = 1, "S", "") '1 = Servidor de licencia, 0= cliente

            Try
                est = dr.Item("estado")
                est = clsPublic.DecodeString(est)
                If Mid(est, 4, 1) = "1" Then est = "Conectado" Else est = ""
            Catch ex As Exception
                est = ""
            End Try

            pItem.Estado = est

            Try
                pItem.Bandera = clsPublic.DecodeString(dr.Item("valor"))
            Catch ex As Exception
                pItem.Vence = ""
            End Try

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Licencia_Server_Activa(ByRef pLicencia As clsBeLicencia_item) As Boolean

        Licencia_Server_Activa = False

        Try

            If clsLnLicencia_llave.GetSingle(pLicencia) Then
                Return pLicencia.Vence >= Today
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Estatus_Licencia_Host(ByVal HostName As String) As eEstatusLicencia

        'yuju
        Get_Estatus_Licencia_Host = eEstatusLicencia.No_Valida

        Try

            Dim Belicencia_item As New clsBeLicencia_item
            Belicencia_item = Get_BeLicencia_Item(HostName)

            If Not Belicencia_item Is Nothing Then

                Dim vEstatusByHost As eEstatusLicencia = Get_Estatus_Licencia(HostName)

                Get_Estatus_Licencia_Host = vEstatusByHost

            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'Public Shared Function Get_Estatus_Licencia_Host(ByVal HostName As String, ByVal BuscarMac As Boolean) As eEstatusLicencia

    '    Get_Estatus_Licencia_Host = eEstatusLicencia.No_Valida

    '    Dim pLicencia As New clsBeLicencia_item

    '    Try

    '        Dim Belicencia_item As New clsBeLicencia_item
    '        Belicencia_item = Get_BeLicencia_Item(HostName,BuscarMac)

    '        If Not Belicencia_item Is Nothing Then

    '            Dim vEstatusByHost As eEstatusLicencia = Get_Estatus_Licencia(HostName,BuscarMac)

    '            Get_Estatus_Licencia_Host = vEstatusByHost

    '        End If

    '    Catch ex1 As SqlException
    '        Throw ex1
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Get_Estatus_Licencia_Host(ByVal HostName As String, ByVal BuscarMac As Boolean) As eEstatusLicencia

        Get_Estatus_Licencia_Host = eEstatusLicencia.No_Valida

        Try

            Dim Belicencia_item As New clsBeLicencia_item
            Belicencia_item = Get_BeLicencia_Item(HostName, BuscarMac)

            If Not Belicencia_item Is Nothing Then

                Dim vEstatusByHost As eEstatusLicencia = Get_Estatus_Licencia(HostName, BuscarMac)

                Get_Estatus_Licencia_Host = vEstatusByHost

            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Mac_Host(ByVal pHostName As String) As String

        Get_Mac_Host = ""

        Try
            Dim nics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces
            Get_Mac_Host = nics(0).GetPhysicalAddress.ToString
        Catch ex As Exception
            Get_Mac_Host = Mid(pHostName, 1, 25)
        End Try

    End Function

    Public Shared Function Get_BeLicencia_Item(ByVal pHostName As String) As clsBeLicencia_item

        Get_BeLicencia_Item = Nothing

        Try

            Dim vMacHost As String = Get_Mac_Host(pHostName)

            '#EJC20171112_1218AM: AMAZON MAC.
            'vMacHost = "02405F93A9D9" 

            '#EJC20171108_REF05_0936PM: Refactoring �Qu� es tipo/modo?
            Dim Belicencia_item As New clsBeLicencia_item() With
                {.IdDisp = clsPublic.EncodeString(vMacHost),
                .Identificacion = clsPublic.EncodeString(pHostName),
                .Tipo = 1}

            Get_BeLicencia_Item = Belicencia_item

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BeLicencia_Item(ByVal pHostName As String, ByVal BuscarMac As Boolean) As clsBeLicencia_item

        Get_BeLicencia_Item = Nothing

        Try

            Dim vMacHost As String

            If BuscarMac Then
                vMacHost = Get_Mac_Host(pHostName)
            Else
                vMacHost = pHostName
            End If

            '#EJC20171112_1218AM: AMAZON MAC.
            'vMacHost = "02405F93A9D9" 

            '#EJC20171108_REF05_0936PM: Refactoring �Qu� es tipo/modo?
            Dim Belicencia_item As New clsBeLicencia_item() With
                {.IdDisp = clsPublic.EncodeString(vMacHost),
                .Identificacion = clsPublic.EncodeString(pHostName),
                .Tipo = 1}

            Get_BeLicencia_Item = Belicencia_item

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Get_BeLicencia_Item(ByVal pIdHH As String, ByVal pNomHH As String) As clsBeLicencia_item

        Get_BeLicencia_Item = Nothing

        Try

            Dim Belicencia_item As New clsBeLicencia_item() With
                {.IdDisp = pIdHH,
                .Identificacion = pNomHH,
                .Tipo = 0}

            If GetSingle(Belicencia_item) Then
                Return Belicencia_item
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Get_BeLicencia_Item(ByVal pIdHH As String,
                                                ByVal pNomHH As String,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction) As clsBeLicencia_item

        Get_BeLicencia_Item = Nothing

        Try

            Dim Belicencia_item As New clsBeLicencia_item() With
                {.IdDisp = pIdHH,
                .Identificacion = pNomHH,
                .Tipo = 0}

            If GetSingle(Belicencia_item, lConnection, lTransaction) Then
                Return Belicencia_item
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function loginBackOffice(ByVal pHostName As String, ByRef servlic As Integer) As eEstatusLicencia

        loginBackOffice = eEstatusLicencia.No_Valida

        Try

            Dim Belicencia_item As New clsBeLicencia_item

            If Licencia_Server_Activa(Belicencia_item) Then

                Belicencia_item = Get_BeLicencia_Item(pHostName)

                GetSingle(Belicencia_item)

                If Not Belicencia_item Is Nothing Then

                    Dim vEstatus As eEstatusLicencia = Get_Estatus_Licencia_Host(pHostName, False)

                    '#EJC20180106:Si no se encuentra por nombre de Host, se busca por Mac.
                    If vEstatus = eEstatusLicencia.Pendiente_Solicitud Then
                        vEstatus = Get_Estatus_Licencia_Host(pHostName, True)
                    End If

                    Registra_Ingreso(pHostName)

                    servlic = Belicencia_item.Bandera 'Licencia de tipo server

                    Return vEstatus

                End If

            Else
                Throw New Exception(String.Format("La licencia del servidor expir� el: {0}. Debe activar la licencia del servidor para ingresar en las terminales", Belicencia_item.Vence))
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Valida_HandHeld(ByVal pIdHH As String, ByVal pNomHH As String) As Integer

        Try

            Return Get_Estatus_Licencia_HH(pIdHH, pNomHH)

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#EJC20171108_REF09_1226AM: GetCantBackOffice <> GetCantHandHeld only By tipo?
    Public Shared Function Get_Cantidad_Licencias_By_Tipo(ByVal pTipo As eTipoHost) As Integer

        Get_Cantidad_Licencias_By_Tipo = 0

        Try

            Dim vSQL As String = "SELECT Count(tipo) As Cant FROM licencia_item WHERE tipo=@Tipo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                dad.SelectCommand.Parameters.AddWithValue("@Tipo", pTipo)
                Dim dt As New DataTable

                dad.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Get_Cantidad_Licencias_By_Tipo = IIf(IsDBNull(dt.Rows(0).Item("Cant")), 0, dt.Rows(0).Item("Cant"))
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Todos_Bandera_0(ByRef pConection As SqlConnection, ByRef pTransaction As SqlTransaction) As Integer

        Try

            Upd.Init("licencia_item")
            Upd.Add("estado", "@estado", DataType.Parametro)

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@estado", 0))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Servidor_De_Licencias(ByRef pBeLicencia_item As clsBeLicencia_item) As Boolean

        Existe_Servidor_De_Licencias = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT TOP(1) * 
                    FROM Licencia_item 
                    Where(bandera = 1) "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeLicencia_item, dt.Rows(0))
                pBeLicencia_item.IdDisp = clsPublic.DecodeString(pBeLicencia_item.IdDisp)
                pBeLicencia_item.Identificacion = clsPublic.DecodeString(pBeLicencia_item.Identificacion)
                Existe_Servidor_De_Licencias = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    'Public Shared Function GetSingle(ByRef pBeLicencia_item As clsBeLicencia_item) As Boolean

    '    GetSingle = False

    '    Try

    '        Const sp As String = "SELECT * FROM Licencia_item" & _
    '        " Where(idDisp = @idDisp)"

    '        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '        Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)
    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDISP", pBeLicencia_item.IdDisp))

    '        Dim dt As New DataTable
    '        dad.Fill(dt)

    '        If dt.Rows.Count = 1 Then
    '            Cargar(pBeLicencia_item, dt.Rows(0))
    '            GetSingle = True
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Existe_Servidor_De_Licencias(ByRef pBeLicencia_item As clsBeLicencia_item,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As Boolean

        Existe_Servidor_De_Licencias = False

        Try

            Dim vSQL As String = "SELECT TOP(1) * FROM Licencia_item " &
                                " Where(bandera = 1)"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeLicencia_item, dt.Rows(0))
                Existe_Servidor_De_Licencias = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#JP20180206  Agregado de la base de clase

    Public Shared Sub Asigna_Servidor(ByVal pHostName As String, ByVal pLlaveAplicada As String)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim BeLicItem As New clsBeLicencia_item

            If Not Existe_Servidor_De_Licencias(BeLicItem, lConnection, lTransaction) Then

                BeLicItem = Get_BeLicencia_Item(pHostName)
                BeLicItem.Bandera = 1
                Insertar(BeLicItem, lConnection, lTransaction)

                Dim BeLicLlaveSol As New clsBeLicencia_llave() With {.IdLlave = clsLnLicencia_llave.MaxID(),
                .Llave = pLlaveAplicada}

                '#EJC20171108_REF02_0605PM: Refactoring clsBeLicencia_llave                
                If Not clsLnLicencia_llave.Exist(pLlaveAplicada) Then
                    clsLnLicencia_llave.Insertar(BeLicLlaveSol)
                Else
                    clsLnLicencia_llave.Actualizar(BeLicLlaveSol)
                End If

            Else

                Actualizar_Todos_Bandera_0(lConnection, lTransaction)

                Dim Belicencia_item As New clsBeLicencia_item
                Belicencia_item = Get_BeLicencia_Item(pHostName)
                Belicencia_item.Bandera = 1

                Actualizar(Belicencia_item, lConnection, lTransaction)

            End If

            lTransaction.Commit()

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Aplica_Solicitud(ByVal pHostName As String,
                                            pLic As clsBeLicencia_item,
                                            pServer As Integer,
                                            Optional ByVal BuscarMac As Boolean = False,
                                            Optional ByVal ForceLicenciaSinLicencia As Boolean = False) As Integer
        ' regresa 1 - solicitud aplicada
        '         0 - todas las licencias usadas
        '        -1 - no se pudo procesar la solicitud
        Dim CantLicenciasUtilizadas, CantLicenciasBOF, tipo As Integer

        Aplica_Solicitud = -1

        Dim BeLicItem As New clsBeLicencia_item
        BeLicItem = Get_BeLicencia_Item(pHostName, BuscarMac)

        Dim Sol_item As New clsBeLicencia_solic
        Sol_item = clsLnLicencia_solic.Get_BeSolic_Item(pHostName)

        Try

            'If BeLicItem.Tipo = 1 Then '"BackOffice"
            '    CantLicenciasUtilizadas = Get_Cantidad_Licencias_By_Tipo(eTipoHost.BOF)
            '    CantLicenciasBOF = pLic.CantBackOffice
            '    tipo = 1
            'Else
            '    CantLicenciasUtilizadas = Get_Cantidad_Licencias_By_Tipo(eTipoHost.HH)
            '    CantLicenciasBOF = pLic.CantHandHeld
            '    tipo = 0
            'End If

            'If CantLicenciasUtilizadas >= CantLicenciasBOF Then Return 0

            'Return Aceptar_Solicitud(BeLicItem, tipo, pServer)

            '#jp20171114
            If Sol_item.Tipo = 1 Then '"BackOffice"
                CantLicenciasUtilizadas = Get_Cantidad_Licencias_By_Tipo(eTipoHost.BOF)
                CantLicenciasBOF = pLic.CantBackOffice
                tipo = 1
            Else
                CantLicenciasUtilizadas = Get_Cantidad_Licencias_By_Tipo(eTipoHost.HH)
                CantLicenciasBOF = pLic.CantHandHeld
                tipo = 0
            End If

            If Not ForceLicenciaSinLicencia Then
                If CantLicenciasUtilizadas >= CantLicenciasBOF Then Return 0
            End If

            Sol_item.IdDisp = Sol_item.Identificacion

            Return Convierte_Solicitud(pLic, Sol_item, pServer)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Aplica_SolicitudHH(ByVal pHostName As String,
                                              ByVal pLic As clsBeLicencia_item,
                                              ByVal pServer As Integer,
                                              Optional ByVal BuscarMac As Boolean = False,
                                              Optional ByVal ForceLicenciaSinLicencia As Boolean = False) As Integer
        ' regresa 1 - solicitud aplicada
        '         0 - todas las licencias usadas
        '        -1 - no se pudo procesar la solicitud
        Dim CantLicenciasUtilizadas, CantLicenciasBOF, tipo As Integer

        Aplica_SolicitudHH = -1

        Dim BeLicItem As New clsBeLicencia_item
        BeLicItem = Get_BeLicencia_Item(pHostName, BuscarMac)

        Dim Sol_item As New clsBeLicencia_solic
        Sol_item = clsLnLicencia_solic.Get_BeSolic_Item(pHostName)

        Try

            '#jp20171114
            If Sol_item.Tipo = 1 Then '"BackOffice"
                CantLicenciasUtilizadas = Get_Cantidad_Licencias_By_Tipo(eTipoHost.BOF)
                CantLicenciasBOF = pLic.CantBackOffice
                tipo = 1
            Else
                CantLicenciasUtilizadas = Get_Cantidad_Licencias_By_Tipo(eTipoHost.HH)
                CantLicenciasBOF = pLic.CantHandHeld
                tipo = 0
            End If

            If Not ForceLicenciaSinLicencia Then
                If CantLicenciasUtilizadas >= CantLicenciasBOF Then Return 0
            End If

            'Sol_item.IdDisp = Sol_item.Identificacion

            Return Convierte_Solicitud(pLic, Sol_item, pServer)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Convierte_Solicitud(ByVal pItem As clsBeLicencia_item, sItem As clsBeLicencia_solic, pServer As Integer) As Integer

        Convierte_Solicitud = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If sItem.IdDisp = "" Then
                Throw New Exception("El Id de dispositivo est� vac�o, reporte esto a desarrollo")
            End If

            pItem.IdDisp = sItem.IdDisp
            pItem.Identificacion = sItem.Identificacion
            '#CKFK 20180527 Modifiqu� el dato que se ingresa en el campo estado en la tabla licencia_item para que pueda devolver si la licencia est� activa a o no
            'Dim vEstado As String = String.Format("{0}{2}{1}", Mid(sItem.IdDisp, 1, 3), (sItem.IdDisp), 1)
            'vEstado = Mid(vEstado, 1, 15)
            'vEstado = clsPublic.EncodeString(vEstado)
            pItem.Estado = "" 'vEstado
            Insertar(pItem, lConnection, lTransaction)
            clsLnLicencia_solic.Eliminar(sItem.IdDisp, lConnection, lTransaction)

            lTransaction.Commit()

            Convierte_Solicitud = 1

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Private Shared Function Aceptar_Solicitud(ByVal pItem As clsBeLicencia_item, pServer As Integer) As Integer

        Aceptar_Solicitud = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()

            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Insertar(pItem, lConnection, lTransaction)

            clsLnLicencia_solic.Eliminar(clsPublic.EncodeString(pItem.IdDisp), lConnection, lTransaction)

            lTransaction.Commit()

            Aceptar_Solicitud = 1

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Elimina_Licencia(ByVal pHostName As String) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Elimina_Licencia = 0

        Try

            Dim vResult As Integer = 0

            lConnection.Open()

            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim BeLicItem As New clsBeLicencia_item() With
            {.IdDisp = clsPublic.EncodeString(pHostName),
            .Identificacion = clsPublic.EncodeString(pHostName)}

            'Dim Belicencia_solic As New clsBeLicencia_solic() With {.IdDisp = BeLicItem.Identificacion, .Identificacion = BeLicItem.Identificacion, .Tipo = pTipo}

            'vResult = clsLnLicencia_solic.Insertar(Belicencia_solic, lConnection, lTransaction)
            vResult += Eliminar(BeLicItem, lConnection, lTransaction)

            If vResult > 0 Then Elimina_Licencia = 1

            lTransaction.Commit()

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


    Public Shared Function Es_Server(ByVal vNombreHost As String, ByVal BeLicItem As clsBeLicencia_item, vMac As String) As Boolean

        Dim lLicenciasProgramacion As New List(Of String)
        Es_Server = False
        Try

            '#GT10042023: valido que, el host ejecutando el proceso hist�rico, sea el server, o algun host de desarrollo :)
            Es_Server = (BeLicItem.Identificacion = vNombreHost)

            '#EJC20210702: Verificar si por macadress el la maquina actual es el servidor de licencias.
            If Not Es_Server Then
                Es_Server = (BeLicItem.IdDisp.ToString() = vMac.ToString())
            End If

            If Not Es_Server Then

                lLicenciasProgramacion.Add("PROGRAX")
                lLicenciasProgramacion.Add("DESARROLLO8-PC")
                lLicenciasProgramacion.Add("PROGRA12")
                lLicenciasProgramacion.Add("PROGRA14")
                lLicenciasProgramacion.Add("DESKTOP-DUB9IAH")
                lLicenciasProgramacion.Add("DESKTOP-9U7ICLN")
                lLicenciasProgramacion.Add("ColonialWMS")
                lLicenciasProgramacion.Add("DESKTOP-790O7S2")

                lLicenciasProgramacion.Add("DESKTOP-5BM5P11")
                lLicenciasProgramacion.Add("DESKTOP-790O7S2")

                '#EJC20210517: Cuando lo encuentren, 
                'agreguen sus Identificadores en la lista para que en caso de que no tenga licencia, no les pida.
                'Con cari�o, Erik.
                If lLicenciasProgramacion.Contains(vNombreHost) Then
                    Es_Server = True
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Function

End Class

