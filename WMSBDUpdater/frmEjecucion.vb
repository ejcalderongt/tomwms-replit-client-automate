Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS

Public Class frmEjecucion

    Private ListaInstancias As New List(Of clsCadenaConexion)
    Private IsLoading As Boolean = False
    Public WS As New AWSWS.syncSoapClient
    Private Sub frmEjecucion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CheckForIllegalCrossThreadCalls = False

        Dim lBeEmpresa As New List(Of clsBeEmpresa)
        Dim vVersionActualBDEmpresa As Integer = 0
        Dim vVersionRemotaBDEmpresa As Integer = 0

        Try

            GetIPAddress()

            If clsPublic.Existe_Ini() Then

                Dim IndiceInstanciaDefecto As Integer = -1

                ListaInstancias = clsPublic.Leer_Archivo_Configuracion_Ini(IndiceInstanciaDefecto)


                If ListaInstancias Is Nothing Then

                    '#CKFK 20180509 07:38 Corregí mensaje, le puse el No
                    XtraMessageBox.Show("No se encontró ninguna cadena de conexión válida en el archivo ini. se cerrará la aplicación",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                    Close()

                Else

                    Dim ArchHeader As New AWSWS.clsArchHeader
                    ArchHeader.Tipo = "WM"

                    If clsPublic.isOnline() Then

                        For Each CadenaConexion In ListaInstancias

                            Dim vResult As String = Abrio_Conexion(CadenaConexion, False)

                            '#EJC20220804: Conexión correcta a instancia
                            If vResult = "" Then

                                '#EJC202208041133: Actualizar cadena de conexión para la clase DAL
                                If Actualiza_APP_Config(CadenaConexion) Then

                                    lBeEmpresa = clsLnEmpresa.GetAll(True)

                                    For Each Empresa In lBeEmpresa

                                        Try

                                            If Empresa.AWS_Token.Trim <> "" Then

                                                If WS.Existe_Empresa(ArchHeader, Empresa.AWS_Token) Then

                                                    Dim vBDAWS As String = 0 'WS.Get_Version_BD_By_AWS_Token(ArchHeader, Empresa.AWS_Token)
                                                    vVersionRemotaBDEmpresa = Val(vBDAWS)

                                                    If Empresa.Version_BD < vVersionRemotaBDEmpresa Then

                                                        'Leer scripts de la nueva versión de la base de datos de amazon.



                                                    End If

                                                End If

                                            Else

                                                Dim myuuid As Guid = Guid.NewGuid()
                                                Dim myuuidAsString As String = myuuid.ToString()

                                                If WS.Registrar_Empresa(ArchHeader,
                                                                        Empresa.Nombre,
                                                                        myuuidAsString,
                                                                        "1",
                                                                        CadenaConexion.URL_WSHH_QAS,
                                                                        CadenaConexion.URL_WSHH_PRD) Then

                                                    Empresa.AWS_Token = myuuidAsString
                                                    Empresa.Version_BD = "1"
                                                    clsLnEmpresa.Actualizar(Empresa)

                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.AppendText("Token registrado para la empresa: " & Empresa.Nombre & " " & " Token: " & myuuidAsString & " Versión de BD: " & "1.0")
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()
                                                    lblprg.Refresh()

                                                    Close()

                                                End If

                                            End If


                                            Dim vBDEmpresaStr As String = Empresa.Version_BD.Replace(".", "")

                                            vVersionActualBDEmpresa = Val(vBDEmpresaStr)

                                        Catch ex As Exception
                                            vVersionActualBDEmpresa = 0
                                        End Try


                                        If vVersionActualBDEmpresa Then

                                        End If

                                    Next


                                End If

                            Else

                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText(vResult)
                                lblprg.AppendText(vbNewLine)
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                                lblprg.Refresh()

                            End If

                        Next

                    Else

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("No hay conexión hacia el repositorio remoto para actualizar la base de datos. (verifique navegación a internet)")
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        lblprg.Refresh()

                    End If

                End If

            End If

        Catch ex1 As SqlClient.SqlException

            If ex1.HResult = -2146233088 OrElse ex1.HResult = -2146232060 Then

                If Not ListaInstancias Is Nothing Then
                    XtraMessageBox.Show("No se pudo abrir la conexión a la base de datos con los parámetros de conexión para la instancia: " & ListaInstancias(0).NombreInstancia,
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
                Else
                    XtraMessageBox.Show("No se pudo abrir la conexión a la base de datos los parámetros de conexión están vacíos",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
                End If

            Else
                XtraMessageBox.Show("Error: " & ex1.Message,
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If

        Catch ex As Exception

            XtraMessageBox.Show("Error: " & ex.Message,
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            Close()

        Finally
            IsLoading = False
        End Try

    End Sub



    Private Sub GetIPAddress()

        Try

            'AP.HostName = Net.Dns.GetHostName()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Function Abrio_Conexion(ByVal IndiceInstanciaDefecto As Integer,
                                   ByVal ListaInstancias As List(Of clsCadenaConexion)) As Boolean

        Abrio_Conexion = False

        '#EJC20171031_1259AM_REF: Cambiar timeOut de archivo de configuración para verificación de conexión a BD de forma temporal.
        Dim vTimeOutConfig As Double
        Dim CadenaConexion As String = ""

        If IndiceInstanciaDefecto = -1 Then
            vTimeOutConfig = ListaInstancias(0).TimeOutConBD
            ListaInstancias(0).TimeOutConBD = 10
            CadenaConexion = ListaInstancias(0).CadenaConexionSQLClient
        Else
            vTimeOutConfig = ListaInstancias(IndiceInstanciaDefecto).TimeOutConBD
            ListaInstancias(IndiceInstanciaDefecto).TimeOutConBD = 10
            CadenaConexion = ListaInstancias(IndiceInstanciaDefecto).CadenaConexionSQLClient
        End If

        Dim lConnection As New SqlClient.SqlConnection(CadenaConexion)

        Try

            lConnection.Open()
            lConnection.Close()

            Abrio_Conexion = True

        Catch ex1 As SqlClient.SqlException
            Throw ex1
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", "Error al intenar conectarse a la base de datos: ", ex.Message))
        Finally
            lConnection.Dispose()
            ListaInstancias(0).TimeOutConBD = vTimeOutConfig
        End Try

    End Function

    Public Function Abrio_Conexion(ByVal BeInstancia As clsCadenaConexion,
                                   ByVal ThrowEx As Boolean) As String

        Abrio_Conexion = "-"

        '#EJC20171031_1259AM_REF: Cambiar timeOut de archivo de configuración para verificación de conexión a BD de forma temporal.
        Dim vTimeOutConfig As Double
        Dim CadenaConexion As String = ""

        CadenaConexion = BeInstancia.CadenaConexionSQLClient

        Dim lConnection As New SqlClient.SqlConnection(CadenaConexion)

        Try

            lConnection.Open()
            lConnection.Close()

            Abrio_Conexion = ""

        Catch ex1 As SqlClient.SqlException
            If ThrowEx Then
                Throw ex1
            Else
                Abrio_Conexion = String.Format("{0} {1}", "Error al intenar conectarse a la base de datos: ", ex1.Message)
            End If
        Catch ex As Exception
            If ThrowEx Then
                Throw New Exception(String.Format("{0} {1}", "Error al intenar conectarse a la base de datos: ", ex.Message))
            Else
                Abrio_Conexion = String.Format("{0} {1}", "Error al intenar conectarse a la base de datos: ", ex.Message)
            End If
        Finally
            lConnection.Dispose()
            ListaInstancias(0).TimeOutConBD = vTimeOutConfig
        End Try

    End Function

    Private Sub RibbonControl_Click(sender As Object, e As EventArgs) Handles RibbonControl.Click

    End Sub

    Private Function Actualiza_APP_Config(ByVal Instancia As clsCadenaConexion) As Boolean

        Actualiza_APP_Config = False

        Try

            Configuration.ConfigurationManager.AppSettings("CST") = Instancia.CadenaConexionSQLClient
            Configuration.ConfigurationManager.AppSettings("EFREN_IMS4MB_CEALSA_PRD_Connection") = Instancia.CadenaConexionSQLClient
            Configuration.ConfigurationManager.AppSettings("IMS4MB_QAConnectionStringPrograN") = Instancia.CadenaConexionSQLClient
            Configuration.ConfigurationManager.AppSettings("IMS4MB_PRDConnectionString") = Instancia.CadenaConexionSQLClient

            Actualiza_APP_Config = True

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

End Class