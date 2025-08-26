Partial Public Class clsBeUbicaciones_por_regla

    Public Property UBS_Cantidad_Maxima_Por_Ubicacion As Double = 0

    Public Property UBS_Cantidad_A_Ubicar As Double = 0

    Public Property UBS_Cantidad_Balance As Double = 0

    Public Property UBS_IdReglaFiltro As Integer = 0


    Public ReadOnly Property VolumenUbicacion As Double
        Get
            Return Alto * Largo * Ancho
        End Get
    End Property

    Public Property VolumenDisponible As Double = 0


End Class
