Public Class clsBeLog_error_wms_ubic
    Implements ICloneable

    Public Property IdError() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property MensajeError() As String = String.Empty
    Public Property RutaError() As String = String.Empty
    Public Property IdTareaUbicacionEnc() As Integer = 0
    Public Property IdMotivoUbicacion() As Integer = 0
    Public Property IdTareaUbicacionDet() As Integer = 0
    Public Property IdUbicacionOrigen() As Integer = 0
    Public Property IdUbicacionDestino() As Integer = 0
    Public Property IdEstadoOrigen() As Integer = 0
    Public Property IdEstadoDestino() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdUMBAs() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property Cantidad() As Double = 0
    Public Property Licencia() As String = String.Empty
    Public Property IdOperador() As Integer = 0
    Public Property usuario_agr() As Integer = 0
    Public Property fec_agr() As Date = New Date(1900, 1, 1)

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
