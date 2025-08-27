Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Public Class RawPrinterHelper

    <DllImport("winspool.Drv", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function OpenPrinter(ByVal szPrinter As String, ByRef hPrinter As IntPtr, ByVal pd As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Integer, ByRef di As DOCINFOA) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As Byte(), ByVal dwCount As Integer, ByRef dwWritten As Integer) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Public Shared Function WritePrinterBytes(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Int32, <Out> ByRef dwWritten As Int32) As Boolean
    End Function


    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Public Structure DOCINFOA
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDocName As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDataType As String
    End Structure

    Public Shared Function PrintUSB(ByVal printerName As String, ByVal data As String) As Boolean
        Dim hPrinter As IntPtr = IntPtr.Zero
        Dim di As New DOCINFOA()
        Dim written As Integer = 0
        Dim success As Boolean = False

        di.pDocName = "RAW Print Job"
        di.pDataType = "RAW"

        If OpenPrinter(printerName, hPrinter, IntPtr.Zero) Then
            If StartDocPrinter(hPrinter, 1, di) Then
                If StartPagePrinter(hPrinter) Then
                    Dim bytes As Byte() = Encoding.ASCII.GetBytes(data & vbLf) ' Agregamos salto de línea al final
                    success = WritePrinter(hPrinter, bytes, bytes.Length, written)
                    EndPagePrinter(hPrinter)
                End If
                EndDocPrinter(hPrinter)
            End If
            ClosePrinter(hPrinter)
        End If

        Return success
    End Function

    Public Shared Function SendBytesToPrinter(ByVal szPrinterName As String, ByVal pBytes As IntPtr, ByVal dwCount As Int32) As Boolean
        Dim dwError As Int32 = 0, dwWritten As Int32 = 0
        Dim hPrinter As IntPtr = New IntPtr(0)
        Dim di As DOCINFOA = New DOCINFOA()
        Dim bSuccess As Boolean = False
        di.pDocName = "TOMIMS RAW Document"
        di.pDataType = "RAW"
        If OpenPrinter(szPrinterName.Normalize(), hPrinter, IntPtr.Zero) Then
            If StartDocPrinter(hPrinter, 1, di) Then
                If StartPagePrinter(hPrinter) Then
                    bSuccess = WritePrinterBytes(hPrinter, pBytes, dwCount, dwWritten)
                    EndPagePrinter(hPrinter)
                End If

                EndDocPrinter(hPrinter)
            End If

            ClosePrinter(hPrinter)
        End If

        If bSuccess = False Then
            dwError = Marshal.GetLastWin32Error()
        End If

        Return bSuccess
    End Function

    Public Shared Function SendFileToPrinter(ByVal szPrinterName As String, ByVal szFileName As String) As Boolean
        Dim fs As FileStream = New FileStream(szFileName, FileMode.Open)
        Dim br As BinaryReader = New BinaryReader(fs)
        Dim bytes As Byte() = New Byte(fs.Length - 1) {}
        Dim bSuccess As Boolean = False
        Dim pUnmanagedBytes As IntPtr = New IntPtr(0)
        Dim nLength As Integer
        nLength = Convert.ToInt32(fs.Length)
        bytes = br.ReadBytes(nLength)
        pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength)
        Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength)
        bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength)
        Marshal.FreeCoTaskMem(pUnmanagedBytes)
        Return bSuccess
    End Function

    Public Shared Function SendStringToPrinter(ByVal szPrinterName As String, ByVal szString As String) As Boolean
        Dim pBytes As IntPtr
        Dim dwCount As Int32 = szString.Length
        pBytes = Marshal.StringToCoTaskMemAnsi(szString)
        SendBytesToPrinter(szPrinterName, pBytes, dwCount)
        Marshal.FreeCoTaskMem(pBytes)
        Return True
    End Function

End Class