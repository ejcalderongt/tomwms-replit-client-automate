Partial Public Class clsBeHorario_laboral_det
    Private mNombreDia As String = ""
    Private mNHoraInicio As String = ""
    Private mNHoraFin As String = ""


    Public Property NombreDia() As String
        Get
            Return mNombreDia
        End Get
        Set(ByVal Value As String)
            mNombreDia = Value
        End Set
    End Property

    Public Property NHoraInicio() As String
        Get
            Return mNHoraInicio
        End Get
        Set(ByVal Value As String)
            mNHoraInicio = Value
        End Set
    End Property

    Public Property NHoraFin() As String
        Get
            Return mNHoraFin
        End Get
        Set(ByVal Value As String)
            mNHoraFin = Value
        End Set
    End Property
End Class
