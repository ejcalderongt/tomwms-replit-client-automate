Public Class clsBeLog_error_wms_oc
    Implements ICloneable
    Public Property IdError() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Fecha() As Date = New Date(1900, 1, 1)
    Public Property MensajeError() As String = String.Empty
    Public Property RutaError() As String = String.Empty
    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property IdOrdenCompraDet() As Integer = 0
    Public Property IdUsuarioAgr() As Integer = 0
    Public Property Codigo_producto() As String = String.Empty
    Public Property UmBas() As String = String.Empty
    Public Property Variant_Code As String = String.Empty
    Public Property Cantidad As Double = 0
    Public Property Referencia_Documento As String = String.Empty
    Public Property IdUsuario() As Integer = 0
    Public Property IdOperador() As Integer = 0


    Sub New()
    End Sub
    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function
End Class
