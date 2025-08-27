Public Class clsBeOperador_zona_picking_tramo
    Implements ICloneable

    Public Property IdZonaPickingTramoOperador() As Integer = 0
    Public Property IdZonaPickingTramo() As Integer = 0
    Public Property IdZonaPicking() As Integer = 0
    Public Property IdOperador() As Integer = 0
    Public Property Dia_semana() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Lista_Tramos_Zona_Picking_Tramo As New List(Of clsBeZona_picking_tramo)

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
