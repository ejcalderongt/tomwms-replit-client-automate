Public Class clsBeLog_error_wms_pick
    Implements ICloneable

    Public Property IdError() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property MensajeError() As String = String.Empty
    Public Property RutaError() As String = String.Empty
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdPickingDet() As Integer = 0
    Public Property IdPickingUbic() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPedidoDet() As Integer = 0
    Public Property CodigoProducto() As String = String.Empty
    Public Property NombreProducto() As String = String.Empty
    Public Property Cantidad_Recibida() As Double = 0
    Public Property User_Agr() As Integer = 0
    Public Property Fec_agr() As Date = New Date(1900, 1, 1)

    Sub New()
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function
End Class
