Public Class clsBeLicencia_login
    Implements ICloneable

    Private mIdDisp As String = ""
    Private mValor As String = ""

    Public Property IdDisp() As String
        Get
            Return mIdDisp
        End Get
        Set(ByVal Value As String)
            mIdDisp = Value
        End Set
    End Property

    Public Property Valor() As String
        Get
            Return mValor
        End Get
        Set(ByVal Value As String)
            mValor = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef idDisp As String, ByVal valor As String)
        mIdDisp = IdDisp
        mValor = Valor
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
