Public Class clsBeLog_verificacion_bof_error
    Implements ICloneable
    Public Property MensajeError() As String = String.Empty
    Public Property RutaError() As String = String.Empty
    Public Property IdBodega() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPedidoDet() As Integer = 0
    Public Property IdPickingUbic() As Integer = 0
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdPickingDet() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property Sku() As String = String.Empty
    Public Property Cantidad() As Double = 0
    Public Property IdMotivo() As Integer = 0
    Public Property IdEstado() As Integer = 0
    Public Property User_agr() As String = String.Empty
    Public Property Fec_agr() As Date = New Date(1900, 1, 1)

    Sub New()
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function
End Class