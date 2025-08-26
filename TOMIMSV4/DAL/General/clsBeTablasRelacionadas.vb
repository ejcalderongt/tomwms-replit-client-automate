Public Class clsBeTablasRelacionadas
    Private mNombreTabla As String = ""
    Private mNoRelaciones As Integer = 0
    Private mColumnaForanea As String = ""

    Public Property NombreTabla() As String
        Get
            Return mNombreTabla
        End Get
        Set(ByVal Value As String)
            mNombreTabla = Value
        End Set
    End Property

    Public Property NoRelaciones() As Integer
        Get
            Return mNoRelaciones
        End Get
        Set(ByVal Value As Integer)
            mNoRelaciones = Value
        End Set
    End Property

    Public Property ColumnaForanea() As String
        Get
            Return mColumnaForanea
        End Get
        Set(ByVal Value As String)
            mColumnaForanea = Value
        End Set
    End Property

End Class
