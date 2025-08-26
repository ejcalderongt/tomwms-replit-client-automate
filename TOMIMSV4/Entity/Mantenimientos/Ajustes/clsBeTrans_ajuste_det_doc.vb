Public Class clsBeTrans_ajuste_det_doc
    Implements ICloneable

    Private mIdajustedoc As Integer = 0
    Private mIdajusteenc As Integer = 0
    Private mDocumento As String = ""

    Public Property Idajustedoc() As Integer
        Get
            Return mIdajustedoc
        End Get
        Set(ByVal Value As Integer)
            mIdajustedoc = Value
        End Set
    End Property

    Public Property Idajusteenc() As Integer
        Get
            Return mIdajusteenc
        End Get
        Set(ByVal Value As Integer)
            mIdajusteenc = Value
        End Set
    End Property

    Public Property Documento() As String
        Get
            Return mDocumento
        End Get
        Set(ByVal Value As String)
            mDocumento = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef idajustedoc As Integer, ByVal idajusteenc As Integer, ByVal documento As String)
        mIdajustedoc = Idajustedoc
        mIdajusteenc = Idajusteenc
        mDocumento = Documento
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
