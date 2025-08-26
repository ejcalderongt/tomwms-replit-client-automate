Imports System.Runtime.InteropServices
Imports System.IO
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

End Class