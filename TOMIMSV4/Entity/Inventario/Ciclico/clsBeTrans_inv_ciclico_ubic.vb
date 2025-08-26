Public Class clsBeTrans_inv_ciclico_ubic
    Implements ICloneable

    Private mIdinventarioenc As Integer = 0
    Private mIdubicacion As Integer = 0

    Public Property Idinventarioenc() As Integer
        Get
            Return mIdinventarioenc
        End Get
        Set(ByVal Value As Integer)
            mIdinventarioenc = Value
        End Set
    End Property

    Public Property Idubicacion() As Integer
        Get
            Return mIdubicacion
        End Get
        Set(ByVal Value As Integer)
            mIdubicacion = Value
        End Set
    End Property

    Public Property IdBodega As Integer = 0


    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
