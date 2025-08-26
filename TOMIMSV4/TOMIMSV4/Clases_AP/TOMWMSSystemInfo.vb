Imports System.Data.SqlClient
Imports System.Management
Imports System.Net.NetworkInformation
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic.Devices
Imports Microsoft.Win32

Public Class TOMWMSSystemInfo
    Public Structure system_info_bof_wms
        Public Property OSVersion As String
        Public Property DiskSpaceAvailable As Long
        Public Property SerialNumber As String
        Public Property MacAddress As String
        Public Property ProcessorName As String
        Public Property ProcessorSpeed As String
        Public Property UsedMemoryPercentage As Double
        Public Property AvailableMemoryPercentage As Double
        Public Property InternetConnectionAvailable As Boolean

    End Structure

    Public Shared Function Get_System_Info() As system_info_bof_wms

        Dim info As New system_info_bof_wms

        Try

            info.OSVersion = Environment.OSVersion.ToString()
            Dim driveInfo As New IO.DriveInfo("C")
            info.DiskSpaceAvailable = driveInfo.AvailableFreeSpace
            info.SerialNumber = Get_System_Serial_Number()
            info.MacAddress = Get_Mac_Address()
            info.ProcessorName = Get_Processor_Name()
            info.ProcessorSpeed = Get_Processor_Speed()
            info.UsedMemoryPercentage = Get_Used_Memory_Percentage()
            info.AvailableMemoryPercentage = Get_Available_Memory_Percentage()
            info.InternetConnectionAvailable = Is_Online()

        Catch ex As Exception
            Throw
        End Try

        Return info

    End Function
    Private Shared Function Get_System_Serial_Number() As String
        Dim serialNumber As String = ""
        Dim searcher As New ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS")
        For Each mo In searcher.Get()
            serialNumber = mo("SerialNumber").ToString()
            Exit For
        Next
        Return serialNumber
    End Function
    Private Shared Function Get_Mac_Address() As String

        Get_Mac_Address = ""

        Try

            Dim macAddr = (From netInterface In NetworkInterface.GetAllNetworkInterfaces()
                           Where netInterface.OperationalStatus = OperationalStatus.Up
                           Select netInterface.GetPhysicalAddress()).FirstOrDefault()

            Get_Mac_Address = macAddr.ToString()

        Catch ex As Exception
            Console.WriteLine("Error al leer macadres: " & ex.Message)
        End Try

    End Function
    Private Shared Function Get_Processor_Name() As String

        Get_Processor_Name = ""

        Try

            Dim processorName As String = ""
            Dim searcher As New ManagementObjectSearcher("SELECT Name FROM Win32_Processor")

            For Each mo In searcher.Get()
                processorName = mo("Name").ToString()
                Exit For
            Next

            Get_Processor_Name = processorName

        Catch ex As Exception

            Dim vMsgError As String = ex.Message
            Console.WriteLine("Error_20231204: " & vMsgError)

        End Try

    End Function
    Private Shared Function Get_Processor_Speed() As String

        Get_Processor_Speed = ""

        Try

            Dim processorSpeed As String = ""
            Dim searcher As New ManagementObjectSearcher("SELECT MaxClockSpeed FROM Win32_Processor")

            For Each mo In searcher.Get()
                processorSpeed = mo("MaxClockSpeed").ToString() & " MHz"
                Exit For
            Next

            Get_Processor_Speed = processorSpeed

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            Console.WriteLine("Error_20231204: " & vMsgError)
        End Try

    End Function
    Private Shared Function Get_Used_Memory_Percentage() As Double
        Dim totalMemory As Double = Get_Total_Memory_In_Mb()
        Dim availableMemory As Double = Get_Available_Memory_In_Mb()
        Dim usedMemory As Double = totalMemory - availableMemory
        Return (usedMemory / totalMemory) * 100
    End Function
    Private Shared Function Get_Available_Memory_Percentage() As Double
        Return 100 - Get_Used_Memory_Percentage()
    End Function
    Private Shared Function Get_Available_Memory_In_Mb() As Double
        Dim pcRamTotal As New PerformanceCounter("Memory", "Available MBytes")
        Return pcRamTotal.NextValue()
    End Function
    Public Shared Function Get_Application_Memory_Usage() As Long

        Get_Application_Memory_Usage = 0

        Try

            Dim currentProcessName As String = Process.GetCurrentProcess().ProcessName
            Dim processes As Process() = Process.GetProcessesByName(currentProcessName)

            If processes.Length > 0 Then
                Get_Application_Memory_Usage = processes(0).WorkingSet64
            Else
                Get_Application_Memory_Usage = 0
            End If

        Catch ex As Exception
            Console.WriteLine("Error_20231204: " & ex.Message)
        End Try

    End Function
    Public Shared Function Get_Total_Memory_In_Mb() As Double
        Dim computerInfo As New ComputerInfo()
        Return computerInfo.TotalPhysicalMemory / 1024 / 1024
    End Function
    Public Shared Function Get_Salud_Tablas_BD() As DataTable

        Get_Salud_Tablas_BD = Nothing

        Try

            Dim vSQL As String = "SELECT TableName as Tabla, ROWS AS Registros, TotalSpaceMB EspacioTotalMB, UsedSpacePercentage AS EspacioUsadoPorcentaje
                                  FROM VW_Tamaño_Tablas WHERE ROWS > 0 ORDER BY ROWS DESC "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Salud_Tablas_BD = lDataTable
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Sql_Server_Version(connectionString As String) As String

        Dim version As String = String.Empty

        Try

            Using lConnecion As New SqlConnection(connectionString)
                Dim cmd As New SqlCommand("SELECT @@VERSION", lConnecion)
                lConnecion.Open()
                version = cmd.ExecuteScalar().ToString()
            End Using

        Catch ex As Exception
            Console.WriteLine("Error al obtener la versión de SQL Server: " & ex.Message)
        End Try

        Return version

    End Function
    Public Shared Function get_dot_net_framework_version() As String
        Dim version As String = "No se pudo detectar la versión de .NET Framework"
        Try
            Using ndpKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\")
                If ndpKey IsNot Nothing AndAlso ndpKey.GetValue("Release") IsNot Nothing Then
                    version = check_for_45_plus_version(ndpKey.GetValue("Release"))
                End If
            End Using
        Catch ex As Exception
            ' Manejar la excepción
            Console.WriteLine("Error al obtener la versión de .NET Framework: " & ex.Message)
        End Try
        Return version
    End Function

    ' Función auxiliar para determinar la versión de .NET Framework 4.5 o posterior
    Private Shared Function check_for_45_plus_version(releaseKey As Integer) As String
        If releaseKey >= 528040 Then Return "4.8 o posterior"
        If releaseKey >= 461808 Then Return "4.7.2"
        ' Agrega más casos según sea necesario
        ' Puedes encontrar los valores de 'releaseKey' para diferentes versiones en la documentación oficial de Microsoft
        Return "Versión desconocida"
    End Function
    Public Class NetworkUtilities
        <DllImport("wininet.dll")>
        Private Shared Function InternetGetConnectedState(ByRef lpdwFlags As Integer, ByVal dwReserved As Integer) As Boolean
        End Function
        Public Shared Function IsInternetAvailable() As Boolean
            Dim flags As Integer
            Return InternetGetConnectedState(flags, 0)
        End Function
    End Class

    Public Shared Function Is_Online() As Boolean

        Is_Online = False

        Try

            Return My.Computer.Network.Ping("www.google.com")

        Catch ex As Exception
            Return False
        End Try

    End Function

End Class