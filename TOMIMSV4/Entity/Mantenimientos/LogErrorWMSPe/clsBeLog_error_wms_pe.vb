Public Class clsBeLog_error_wms_pe
    Implements ICloneable

    Public Property IdError() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property MensajeError() As String = String.Empty
    Public Property RutaError() As String = String.Empty
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPedidoDet() As Integer = 0
    Public Property CodigoProducto() As String = String.Empty
    Public Property Cantidad() As Double = 0
    Public Property IdUMBas() As Integer = 0
    Public Property IdEstado() As Integer = 0
    Public Property NoLinea() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property Talla() As String = String.Empty
    Public Property Color() As String = String.Empty
    Public Property UsrAgr() As Integer = 0
    Public Property FecAgr() As Date = New Date(1900, 1, 1)

    Sub New()
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function

End Class
