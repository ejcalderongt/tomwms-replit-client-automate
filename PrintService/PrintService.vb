Imports System.Drawing.Printing
Imports System.Runtime.InteropServices
Imports System.Timers

Public Class PrintService

    Private aTimer As Timer
    Declare Auto Function SetServiceStatus Lib "advapi32.dll" (ByVal handle As IntPtr, ByRef serviceStatus As ServiceStatus) As Boolean
    Public ListaInstancias As New List(Of clsCadenaConexion)
    Private BeListImpresion As List(Of clsBeImpresion_productos_barras)

    Public Enum ServiceState
        SERVICE_STOPPED = 1
        SERVICE_START_PENDING = 2
        SERVICE_STOP_PENDING = 3
        SERVICE_RUNNING = 4
        SERVICE_CONTINUE_PENDING = 5
        SERVICE_PAUSE_PENDING = 6
        SERVICE_PAUSED = 7
    End Enum

    <StructLayout(LayoutKind.Sequential)>
    Public Structure ServiceStatus
        Public dwServiceType As Long
        Public dwCurrentState As ServiceState
        Public dwControlsAccepted As Long
        Public dwWin32ExitCode As Long
        Public dwServiceSpecificExitCode As Long
        Public dwCheckPoint As Long
        Public dwWaitHint As Long
    End Structure

    Protected Overrides Sub OnStart(ByVal args() As String)
        Dim serviceStatus As ServiceStatus = New ServiceStatus()
        serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING
        serviceStatus.dwWaitHint = 10000
        SetServiceStatus(Me.ServiceHandle, serviceStatus)

        Dim elapsedTime As Double = 2
        Dim Path As String = ""
        Dim UbicFactura As String = ""

        If args.Length > 0 Then

            elapsedTime = Double.Parse(args(0))
            Path = args(1)

        End If

        Carga_ini(Path)

        If Conexion IsNot Nothing Then
            EventLog.WriteEntry("Objeto conexión correcto" & "Instancia: " & Conexion.NombreInstancia & "BD: " & Conexion.NombreBD)
        Else
            EventLog.WriteEntry("Objeto conexión vacío ")
            OnStop()
        End If

        aTimer = New Timers.Timer
        AddHandler aTimer.Elapsed, New ElapsedEventHandler(AddressOf Timer1_Tick)

        aTimer.Interval = elapsedTime * 10
        aTimer.Enabled = True

        ' Update the service state to Running.
        serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING
        SetServiceStatus(Me.ServiceHandle, serviceStatus)

        EventLog.WriteEntry("TOMIMS Print Service Conexion", "Se: " & elapsedTime & ", App Path: " & Conexion.NombreInstancia & "Ubicación de XML facturas: " & UbicFactura, EventLogEntryType.Warning, 234)

    End Sub

    Protected Overrides Sub OnStop()
        Dim serviceStatus As ServiceStatus = New ServiceStatus()
        serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED
        serviceStatus.dwWaitHint = 100000
        SetServiceStatus(Me.ServiceHandle, serviceStatus)
        aTimer.Stop()
        aTimer.Dispose()
    End Sub

    Private Sub Carga_ini(ByVal AppPath As String)

        Try

            If Not Existe_Ini(AppPath) Then
                EventLog.WriteEntry("No se encontró ninguna cadena de conexión válida en el archivo ini. se cerrará la aplicación")
                OnStop()
            Else

                Dim IndiceInstanciaDefecto As Integer = -1

                ListaInstancias = clsPublic.Leer_Archivo_Configuracion_Ini(IndiceInstanciaDefecto)

                EventLog.WriteEntry("Encontro instancias" & ListaInstancias(0).CadenaConexionSQLClient)

                If ListaInstancias Is Nothing Then

                    EventLog.WriteEntry("No se encontró ninguna cadena de conexión válida en el archivo ini. se parará el servicio", EventLogEntryType.Error, 234)
                    OnStop()

                Else

                    If Abrio_Conexion(IndiceInstanciaDefecto, ListaInstancias) Then

                        CopyObject(ListaInstancias(0), Conexion)

                        EventLog.WriteEntry("Se estableción la conexión con la bd de datos: " & ListaInstancias.Single.NombreBD, EventLogEntryType.Information, 234)

                    Else

                        EventLog.WriteEntry("No fue posible establecer conexión con la base de datos: " & Conexion.NombreBD & " se parará el servicio", EventLogEntryType.Error, 234)
                        OnStop()

                    End If

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Function Abrio_Conexion(ByVal IndiceInstanciaDefecto As Integer,
                                   ByVal ListaInstancias As List(Of clsCadenaConexion)) As Boolean

        Abrio_Conexion = False

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

    Public Sub Timer1_Tick(ByVal sender As Object, ByVal e As ElapsedEventArgs)

        Try

            EventLog.WriteEntry("TOMIMS Print Service", "Ingresa a Timer")

            aTimer.Enabled = False
            Imprime_productos_barras()
            aTimer.Enabled = True

        Catch ex As Exception
            EventLog.WriteEntry("TOMIMS Print Service", "Ocurrió un error " & ex.Message, EventLogEntryType.Warning, 234)
            aTimer.Enabled = True
        End Try

    End Sub

    Private Sub Imprime_productos_barras()

        'Dim ConexionString As String = Conexion.CadenaConexionSQLClient

        'Try

        '    BeListImpresion = clsLnImpresion_productos_barras.GetAll(ConexionString)

        '    If BeListImpresion.Count > 0 AndAlso Not BeListImpresion Is Nothing Then

        '        OnStop()

        '        For Each Obj In BeListImpresion

        '            Imprimir_Etiqueta(Obj.Codigo, Obj.Nombre, Obj.Codigo_barra)

        '            Obj.Activo = 0
        '            Obj.Impreso = 1
        '            clsLnImpresion_productos_barras.Actualizar(ConexionString, Obj)

        '        Next

        '        OnStart({})

        '    End If

        'Catch ex As Exception
        '    EventLog.WriteEntry("TOMIMS Print Service", "Conexion" & ex.Message, EventLogEntryType.Warning, 234)
        '    EventLog.WriteEntry("TOMIMS Print Service", "Ocurrió un error " & ex.Message, EventLogEntryType.Warning, 234)
        'End Try

    End Sub

    Private Sub Imprimir_Etiqueta(ByVal pCodigo As String, ByVal pProducto As String, ByVal pBarra As String)

        Try
            Dim pd As New PrintDocument

            ' Default printer      
            Dim s_Default_Printer As String = pd.PrinterSettings.PrinterName

            Dim wt As Integer = 2
            If pCodigo.Length > 10 Then
                wt = 1
            End If

            Dim ZPLString As String = String.Format(
               " ^XA
                ^FT385,160^A0I,0,8^FH\^FD{0}^FS
                ^FT340,10^A0I,0,8^FH\^FD{1}^FS
                ^FT385,10^A0I,0,12^FH\^FDCODIGO:^FS
                ^FT385,189^A0I,0,16^FH\^FDTOMIMS, WMS. - Product Barcode^FS
                ^FO2,183^GB606,0,3^FS
                ^BY{3},3,110^FT385,45^BCI,,Y,N
                ^FD{2}
                ^PQ1,0,1,Y
                ^XZ", pProducto,
                      pCodigo,
                      pBarra,
                      wt)

            RawPrinterHelper.SendStringToPrinter(s_Default_Printer, ZPLString)

        Catch ex As Exception
            EventLog.WriteEntry("TOMIMS Print Service", "Ocurrió un error al imprimir " & ex.Message, EventLogEntryType.Warning, 234)
        End Try

    End Sub

End Class
