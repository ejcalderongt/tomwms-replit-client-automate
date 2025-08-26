Public Class clsBeUbicacionSugerida

    Implements ICloneable

    Public Property IdUbicacion() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property IdTramo() As Integer = 0
    Public Property Indice_x() As Integer = 0
    Public Property Nivel() As Integer = 0
    Public Property Acepta_pallet() As Boolean = False
    Public Property Cant_Max As Double = 0
    Public Property Cant_Ubicar As Double = 0
    Public Property Cant_Balance As Double = 0
    Public Property IdPrior As Integer = 0
    Public Property Tramo() As String = ""

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
