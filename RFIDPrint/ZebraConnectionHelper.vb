Imports Zebra.Sdk.Comm
Public Class ZebraConnectionHelper
    Public Shared Function ObtenerConexion(impresora As ImpresoraZebra) As Connection
        Select Case impresora.TipoConexion.ToUpper()
            Case "USB"
                Return New UsbConnection(impresora.Direccion)

            Case "DRIVER"
                Return New DriverPrinterConnection(impresora.Nombre) ' Usa el nombre del driver de Windows

            Case "RED"
                Return New TcpConnection(impresora.Direccion, 9100) ' IP + Puerto estándar

            Case Else
                Throw New ApplicationException("Tipo de conexión no soportado: " & impresora.TipoConexion)
        End Select
    End Function

End Class
