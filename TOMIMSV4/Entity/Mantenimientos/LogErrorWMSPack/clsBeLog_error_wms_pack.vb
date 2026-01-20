Public Class clsBeLog_error_wms_pack
    Implements ICloneable

    Public Property IdError() As Integer = 0
    Public Property MensajeError() As String = String.Empty
    Public Property RutaError() As String = String.Empty
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdPickingUbic() As Integer = 0
    Public Property IdDespachoEnc() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdProductoEstado() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property Lic_Plate() As String = String.Empty
    Public Property Cantidad_Bultos_Packing() As Integer = 0
    Public Property IdOperador() As Integer = 0
    Public Property Fec_agr() As Date = New Date(1900, 1, 1)
    Public Property User_agr() As String = String.Empty
    Public Property EsImplosion() As Boolean = False

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
