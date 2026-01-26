Public Class clsBeLog_error_wms_reab
    Implements ICloneable

    Public Property IdError() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property MensajeError() As String = String.Empty
    Public Property RutaError() As String = String.Empty
    Public Property IdStock() As Integer = 0
    Public Property IdMovimiento() As Integer = 0
    Public Property Lic_Plate_Anterior() As String = String.Empty
    Public Property Lic_Plate() As String = String.Empty
    Public Property IdResolucion() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property Cantidad() As Double = 0
    Public Property User_agr() As Integer = 0
    Public Property Fec_agr() As Date = New Date(1900, 1, 1)

    Sub New()
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function
End Class
