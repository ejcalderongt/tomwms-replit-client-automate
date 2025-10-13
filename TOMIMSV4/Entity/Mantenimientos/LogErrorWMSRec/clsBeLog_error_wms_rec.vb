Public Class clsBeLog_error_wms_rec
    Implements ICloneable

    Public Property IdError() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Fecha() As Date = New Date(1900, 1, 1)
    Public Property MensajeError() As String = String.Empty
    Public Property RutaError() As String = String.Empty
    Public Property IdPedidoEnc As Integer = 0
    Public Property IdPickingEnc As Integer = 0
    Public Property IdRecepcionEnc As Integer = 0
    Public Property IdUsuarioAgr As Integer = 0
    Public Property Line_No As Integer = 0
    Public Property Item_No As String = String.Empty
    Public Property UmBas As String = String.Empty
    Public Property Variant_Code As String = String.Empty
    Public Property Cantidad As Double = 0
    Public Property Referencia_Documento As String = String.Empty

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
