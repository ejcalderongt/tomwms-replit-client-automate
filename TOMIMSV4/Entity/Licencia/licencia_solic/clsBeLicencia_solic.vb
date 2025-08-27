Public Class clsBeLicencia_solic
    Implements ICloneable

    Public Property IdDisp() As String = ""
    Public Property Identificacion() As String = ""
    Public Property Tipo() As Integer = 0
    Public Property Estado() As String = ""
    Public Property Fecha_Solicitud As DateTime = Now

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
