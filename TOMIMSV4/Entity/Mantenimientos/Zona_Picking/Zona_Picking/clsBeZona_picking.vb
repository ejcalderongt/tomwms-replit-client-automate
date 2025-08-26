Public Class clsBeZona_picking
    Implements ICloneable

    Public Property IdZonaPicking() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Descripcion() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
