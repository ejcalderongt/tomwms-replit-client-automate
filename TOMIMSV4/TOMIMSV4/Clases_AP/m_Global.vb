Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports TOMWMS.wsTOMHH

Module m_Global

    Public vSQL As String = ""
    Public BD As New clsBD
    Public AP As New IMS
    Public Emp As New clsBeEmpresa
    Public Ins As New clsInsert
    Public Upd As New clsUpdate
    Public vRutaInterfaceNAV As String = CurDir() & "/NavSync.exe"
    Public vRutaInterfaceSAP As String = CurDir() & "/SAPBOSync.exe"
    Public vRutaServicio As String = CurDir() & "/WMS_PrintService.exe"
    Public vRutaInterfaceCEALSA As String = CurDir() & "/CEALSASync.exe"

    Public Property gVersionApp As String = "7.7.6"

    Public gVersionBD As String = "1"
    Public Property gFechaVersion As Date = New Date(2025, 8, 26)
    Public Property wsTOMHHInstance As TOMHHWSSoapClient

    Public gIndiceInstancia As Integer = -1

    Public Property lConfiguracionAliasCampos As New List(Of clsBeConfiguracion_alias_campos)

    Public Sub CopyObject(Of tom)(ByVal ObjOrigen As Object, ByRef ObjDestino As tom)

        Try

            If ObjOrigen Is Nothing OrElse ObjDestino Is Nothing Then Return
            Dim TipoFuente As Type = ObjOrigen.[GetType]()
            Dim TipoDestino As Type = ObjDestino.[GetType]()

            If TipoFuente IsNot Nothing AndAlso TipoDestino IsNot Nothing Then

                For Each p As PropertyInfo In TipoFuente.GetProperties()

                    Dim ObjPI As PropertyInfo = TipoDestino.GetProperty(p.Name)

                    If ObjPI IsNot Nothing Then
                        Dim l As Object = p.GetValue(ObjOrigen, Nothing)
                        ObjPI.SetValue(ObjDestino, l)
                        ObjPI.SetValue(ObjDestino, p.GetValue(ObjOrigen, Nothing), Nothing)
                    End If

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
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
    Public Function GetDBValue(ByVal pCampo As String, ByVal pTabla As String, ByVal pFiltro As String) As String

        Try

            Dim lResult As String
            Using lCnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)

                Dim lCmd As New SqlCommand(String.Format("SELECT {0} FROM {1} WHERE {2}", pCampo, pTabla, pFiltro), lCnn) With {.CommandType = CommandType.Text}

                Dim lDT As New DataTable("Result")

                Using lDA As New SqlDataAdapter()
                    lDA.SelectCommand = lCmd
                    lDA.Fill(lDT)
                End Using

                If lDT.Rows.Count > 0 Then
                    If IsDBNull(lDT(0)(0)) Then
                        lResult = String.Empty
                    Else
                        lResult = lDT.Rows(0)(0)
                    End If
                Else
                    lResult = String.Empty
                End If

                Return lResult

            End Using

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

    End Function
    Public Function ObtenerImagen(ByVal pCampo As String, ByVal pTabla As String, ByVal pFiltro As String) As DataTable

        Try

            Using lCnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)

                Dim lCmd As New SqlCommand(String.Format("SELECT {0} FROM {1} WHERE {2}", pCampo, pTabla, pFiltro), lCnn) With {.CommandType = CommandType.Text}

                Dim lDT As New DataTable("Result")
                Dim lDA As New SqlDataAdapter()
                lDA.SelectCommand = lCmd
                lDA.Fill(lDT)

                Return lDT

            End Using

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Function NITvalido(ByVal pNIT As String) As Boolean

        Dim POS As Integer
        Dim Correlativo As String
        Dim DigitoVerificador As String
        Dim Factor As Integer
        Dim Suma As Integer = 0
        Dim Valor As Integer = 0
        Dim X As Integer
        Dim xMOD11 As Double = 0
        Dim S As String = Nothing

        NITvalido = False

        Try

            POS = pNIT.IndexOf("-")
            If POS = -1 Then Exit Function
            Correlativo = pNIT.Substring(0, POS)

            DigitoVerificador = pNIT.Substring(POS + 1)
            Factor = Correlativo.Length + 1

            For X = 0 To (pNIT.IndexOf("-") - 1)
                Valor = Convert.ToInt32(pNIT.Substring(X, 1))
                Suma += (Valor * Factor)
                Factor -= 1
            Next

            xMOD11 = (11 - (Suma Mod 11)) Mod 11
            S = Convert.ToString(xMOD11)

            If (xMOD11 = 10 And DigitoVerificador = "K") Or (S = DigitoVerificador) Then
                NITvalido = True
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

    End Function
    Public Sub TablasRelacionadas(pTabla As String, ByVal IdToDelete As Integer)

        Dim lTabRel As New List(Of clsBeTablasRelacionadas)

        lTabRel = clsLnEmpresa.Get_Tablas_Relacionadas(pTabla, IdToDelete)

        If lTabRel.Count > 0 Then
            Dim FrmRel As New frmTablasRelacionadas() With {.ListObjTablasConRelacion = lTabRel, .IdEmpresa = AP.IdEmpresa}
            FrmRel.ShowDialog()
            FrmRel.Dispose()
        End If

    End Sub

    Public Function Ejecutar_Servicio(ByVal ParametroEjecucion As String, ByVal frm As RibbonForm) As Boolean

        Ejecutar_Servicio = False

        Try

            If IO.File.Exists(vRutaServicio) Then

                Dim aplicacioncorriendo As Process() = Process.GetProcessesByName("WMS_PrintService.exe")

                If aplicacioncorriendo.Length > 0 Then

                    MessageBox.Show("El servicio de impresión de etiquetas de WMS ya se encuentra en ejecución",
                    "WMS_PrintService.exe",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                    Return False

                Else

                    If ParametroEjecucion <> "" Then
                        ShellandWait(vRutaServicio, ParametroEjecucion, frm)
                    Else
                        Dim startInfo As New ProcessStartInfo() With {
                        .FileName = vRutaServicio,
                        .Arguments = clsBD.Instancia.Indice & " " & AP.IdEmpresa & " " & AP.IdBodega}
                        Process.Start(startInfo)
                    End If

                End If

                Ejecutar_Servicio = True

            Else
                MessageBox.Show("No existe el ejecutable para servicio de impresión de WMS.",
                "WMS_PrintService.exe",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            MessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Exec_PrintsService",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function
    Public Sub ShellandWait(ByVal ProcessPath As String,
                             ByVal Args As String,
                             ByVal frm As RibbonForm)

        Dim objProcess As Process

        Try

            objProcess = New Process()
            objProcess.StartInfo.FileName = ProcessPath
            objProcess.StartInfo.Arguments = Args
            objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            objProcess.Start()

            SplashScreenManager.ShowForm(frm, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Esperando a que la interface se cierre...")

        Catch
            MessageBox.Show("Could not start process " & ProcessPath, "Error")
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Public Function permiteMenu(menu As String) As Boolean

        Dim us As New clsBeUsuario
        Dim ms As New clsBeMenu_sistema
        Dim clave As String

        Try

            ms.IdMenu = menu
            'MsgBox(link.KeyTip)
            clsLnMenu_sistema.GetSingle(ms)

            If (ms.Solicitar_clave_autorizacion) Then

                us.IdUsuario = AP.UsuarioAp.IdUsuario
                clsLnUsuario.GetSingle(us)

                Try

                    clave = clsPublic.Desencriptar(us.Clave_autorizacion)

                    If (clave = "") Then Throw New Exception("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.")

                Catch ex As Exception
                    XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End Try

                Dim frmlog As New frmAjusteLogin() With {.clave = clave}

                If frmlog.ShowDialog() <> DialogResult.Yes Then
                    frmlog.Dispose() : Return False
                End If

                frmlog.Dispose()

                Return True

            Else
                Return True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try

    End Function

    Public Function solicitarClave() As Boolean

        Dim us As New clsBeUsuario
        Dim ms As New clsBeMenu_sistema
        Dim clave As String

        Try

            us.IdUsuario = AP.UsuarioAp.IdUsuario
            clsLnUsuario.GetSingle(us)

            Try

                clave = clsPublic.Desencriptar(us.Clave_autorizacion)

                If (clave = "") Then Throw New Exception("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.")

            Catch ex As Exception
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End Try

            Dim frmlog As New frmAjusteLogin() With {.clave = clave}

            If frmlog.ShowDialog() <> DialogResult.Yes Then
                frmlog.Dispose() : Return False
            End If

            frmlog.Dispose()

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try

    End Function

    Public Function Ejecutar_Interface(ByVal ParametroEjecucion As String, ByVal frm As RibbonForm, Optional pEjecutable As String = "") As Boolean

        Dim vRutaInterface As String = ""
        Dim vNombre_Ejecutable As String = ""

        Ejecutar_Interface = False

        Try

            If Not String.IsNullOrEmpty(pEjecutable) Then

                If clsLnI_nav_config_enc.Get_Existe_by_Ejecutable(pEjecutable) Then
                    vNombre_Ejecutable = pEjecutable
                End If

            Else
                vNombre_Ejecutable = clsLnI_nav_config_enc.Get_Nombre_Ejecutable(AP.IdConfiguracionInterface)
            End If


            If Not vNombre_Ejecutable = "" Then

                vRutaInterface = CurDir() & "\" & vNombre_Ejecutable

                If IO.File.Exists(vRutaInterface) Then

                    If ParametroEjecucion <> "" Then
                        ShellandWait(vRutaInterface, ParametroEjecucion, frm)
                    Else
                        Dim startInfo As New ProcessStartInfo() With {.FileName = vRutaInterface,
                                                                      .Arguments = ParametroEjecucion}
                        Process.Start(startInfo)
                    End If

                    Ejecutar_Interface = True

                Else
                    MessageBox.Show("No existe archivo de interface " & vRutaInterface,
                                    "Exec_MI3_Sync",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Exec_MI3_Sync",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function


    '#GT28072025: interface para ejecutar otros procesos ajenos a MI3
    Public Function Ejecutar_Interface_Exe(ByVal ParametroEjecucion As String, ByVal frm As RibbonForm, Optional pEjecutable As String = "") As Boolean

        Dim vRutaInterface As String = ""
        Dim vNombre_Ejecutable As String = ""

        Ejecutar_Interface_Exe = False

        Try

            If Not String.IsNullOrEmpty(pEjecutable) Then

                If clsLnI_nav_config_enc.Get_Existe_by_Ejecutable(pEjecutable) Then
                    vNombre_Ejecutable = pEjecutable
                End If

            Else
                vNombre_Ejecutable = clsLnI_nav_config_enc.Get_Nombre_Ejecutable(AP.IdConfiguracionInterface)
            End If


            If Not vNombre_Ejecutable = "" Then

                vRutaInterface = CurDir() & "\" & vNombre_Ejecutable

                If IO.File.Exists(vRutaInterface) Then


                    If ParametroEjecucion <> "" Then
                        ShellandWait(vRutaInterface, ParametroEjecucion, frm)
                    Else
                        Dim startInfo As New ProcessStartInfo() With {.FileName = vRutaInterface,
                                                                      .Arguments = ParametroEjecucion}
                        Process.Start(startInfo)
                    End If

                    Ejecutar_Interface_Exe = True

                Else
                    MessageBox.Show("No existe archivo de interface " & vRutaInterface,
                                    "Exec_Sync",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Exec_MI3_Sync",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function


End Module

Public Module SqlDashboardHelper
    Sub SetupSqlParameters(ByVal connectionParameters As MsSqlConnectionParameters)

        connectionParameters.AuthorizationType = MsSqlAuthorizationType.SqlServer
        connectionParameters.ServerName = clsBD.Instancia.Server
        connectionParameters.DatabaseName = clsBD.Instancia.NombreBD
        connectionParameters.UserName = clsBD.Instancia.Usuario
        connectionParameters.Password = clsBD.Instancia.Clave

    End Sub

    Public Function Genera_Licencia_BOF(ByVal IdBodega As Integer, ByVal IdUsuario As Integer) As String

        '#GT26012024: Obtener al operador para resolucion lp en BOF
        Dim BeUsuario As New clsBeUsuario()
        Dim lResolucionesLPBOF As New clsBeResolucion_lp_usuario()
        Genera_Licencia_BOF = ""

        Try

            BeUsuario = clsLnUsuario.GetSingle(IdUsuario)

            If BeUsuario IsNot Nothing Then

                lResolucionesLPBOF = clsLnResolucion_lp_usuario.Get_Resolucion_By_IdOperador_And_IdBodega(IdBodega, IdUsuario)

                If lResolucionesLPBOF IsNot Nothing Then
                    Genera_Licencia_BOF = clsLnResolucion_lp_usuario.Get_Nuevo_Correlativo_LP_BOF(lResolucionesLPBOF)
                Else
                    Throw New Exception("No esta definida la resolución de licencia para recepción en BOF")
                End If

            Else
                Throw New Exception("No esta definido un operador con resolución de licencia.")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Incrementar_Licencia_BOF(ByVal IdBodega As Integer,
                                             ByVal IdUsuario As Integer,
                                             ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction) As String

        Dim BeUsuario As New clsBeUsuario()
        Dim lResolucionesLP As New clsBeResolucion_lp_usuario()

        Incrementar_Licencia_BOF = ""

        Dim vLicenciaStr As String = ""

        Try

            clsLnUsuario.GetSingle(BeUsuario, lConnection, lTransaction)

            If BeUsuario IsNot Nothing Then

                lResolucionesLP = clsLnResolucion_lp_usuario.Get_Resolucion_By_IdOperador_And_IdBodega(IdBodega, IdUsuario)

                If lResolucionesLP IsNot Nothing Then
                    vLicenciaStr = clsLnResolucion_lp_usuario.Get_Nuevo_Correlativo_LP_BOF(lResolucionesLP)
                    lResolucionesLP.Correlativo_actual += 1
                    clsLnResolucion_lp_usuario.Actualizar_Correlativo_Actual(lResolucionesLP, lConnection, lTransaction)
                Else
                    Throw New Exception("No esta definida la resolución de licencia para recepción en BOF")
                End If

            Else
                Throw New Exception("No esta definido un operador con resolución de licencia.")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Incrementar_Licencia_BOF(ByVal IdBodega As Integer,
                                             ByVal IdUsuario As Integer)

        Dim BeUsuario As New clsBeUsuario()
        Dim lResolucionesLP As New clsBeResolucion_lp_usuario()

        Dim vLicenciaStr As String = ""

        Try

            Using lConnection As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    clsLnUsuario.GetSingle(BeUsuario, lConnection, lTransaction)

                    If BeUsuario IsNot Nothing Then

                        lResolucionesLP = clsLnResolucion_lp_usuario.Get_Resolucion_By_IdOperador_And_IdBodega(IdBodega, IdUsuario)

                        If lResolucionesLP IsNot Nothing Then
                            vLicenciaStr = clsLnResolucion_lp_usuario.Get_Nuevo_Correlativo_LP_BOF(lResolucionesLP)
                            lResolucionesLP.Correlativo_actual += 1
                            clsLnResolucion_lp_usuario.Actualizar_Correlativo_Actual(lResolucionesLP, lConnection, lTransaction)
                        Else
                            Throw New Exception("No esta definida la resolución de licencia para recepción en BOF")
                        End If

                    Else
                        Throw New Exception("No esta definido un operador con resolución de licencia.")
                    End If

                    lTransaction.Commit()

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    '#GT02122024: actualizar resolucion de usuario bof para ajuste positivo de producto sin stock
    Public Function Incrementar_Licencia_BOF_By_Ajuste_Positivo(ByVal IdBodega As Integer,
                                                                ByVal IdUsuario As Integer,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As Boolean

        Dim BeUsuario As New clsBeUsuario()
        Dim lResolucionesLP As New clsBeResolucion_lp_usuario()
        Incrementar_Licencia_BOF_By_Ajuste_Positivo = False

        Try

            clsLnUsuario.GetSingle(BeUsuario, lConnection, lTransaction)

            If BeUsuario IsNot Nothing Then

                lResolucionesLP = clsLnResolucion_lp_usuario.Get_Resolucion_By_IdOperador_And_IdBodega(IdBodega, IdUsuario)

                If lResolucionesLP IsNot Nothing Then
                    'vLicenciaStr = clsLnResolucion_lp_usuario.Get_Nuevo_Correlativo_LP_BOF(lResolucionesLP)
                    lResolucionesLP.Correlativo_actual += 1
                    clsLnResolucion_lp_usuario.Actualizar_Correlativo_Actual(lResolucionesLP, lConnection, lTransaction)
                    Incrementar_Licencia_BOF_By_Ajuste_Positivo = True
                Else
                    Throw New Exception("No esta definida la resolución de licencia para recepción en BOF")
                End If

            Else
                Throw New Exception("No esta definido un operador con resolución de licencia.")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Module