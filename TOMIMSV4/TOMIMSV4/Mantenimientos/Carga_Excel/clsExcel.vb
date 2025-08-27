Public Class clsExcel

    Private _index As Integer
    Public Property Index() As Integer
        Get
            Return _index
        End Get
        Set(ByVal value As Integer)
            _index = value
        End Set
    End Property

    Private _checked As Integer
    Public Property Checked() As Integer
        Get
            Return _checked
        End Get
        Set(ByVal value As Integer)
            _checked = value
        End Set
    End Property

    Private _nombrehoja As String
    Public Property NombreHoja() As String
        Get
            Return _nombrehoja
        End Get
        Set(ByVal value As String)
            _nombrehoja = value
        End Set
    End Property

    Public Sub New()

        _index = Nothing
        _checked = False
        _nombrehoja = String.Empty

    End Sub

End Class
