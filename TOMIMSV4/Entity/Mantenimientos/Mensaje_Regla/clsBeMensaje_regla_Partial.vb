Partial Public Class clsBeMensaje_regla

    Private _isnew As Boolean
    Public Property IsNew() As Boolean
        Get
            Return _isnew
        End Get
        Set(value As Boolean)
            _isnew = value
        End Set
    End Property


End Class
