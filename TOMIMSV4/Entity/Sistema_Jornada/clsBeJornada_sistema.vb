Public Class clsBeJornada_sistema
    Implements ICloneable

    Public Property IdJornada() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Fecha() As Date = New Date(1900, 1, 1)
    Public Property IdUsuario() As Integer = 0
    Public Property FechaAgregado() As Date = Date.Now

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
