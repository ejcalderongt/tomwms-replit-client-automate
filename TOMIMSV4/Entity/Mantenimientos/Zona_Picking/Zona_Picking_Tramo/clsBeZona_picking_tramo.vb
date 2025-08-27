Public Class clsBeZona_picking_tramo
    Implements ICloneable

    Public Property IdZonaPickingTramo() As Integer = 0
    Public Property IdZonaPicking() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdArea() As Integer = 0
    Public Property IdSector() As Integer = 0
    Public Property IdTramo() As Integer = 0
    Public Property Min_x() As Integer = 0
    Public Property Max_x() As Integer = 0
    Public Property Min_y() As Integer = 0
    Public Property Max_y() As Integer = 0
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
