Partial Public Class clsBeBodega_monitor_parametro


    Private _idbodega As Integer

    Public Property IdBodega() As Integer
        Get
            Return _idbodega
        End Get
        Set(value As Integer)
            _idbodega = value
        End Set
    End Property


    Private _nombreempresa As String
    Public Property NombreEmpresa As String
        Get
            Return _nombreempresa
        End Get
        Set(value As String)
            _nombreempresa = value
        End Set
    End Property


    Private _imagenempresa As Byte()
    Public Property ImagenEmpresa As Byte()
        Get
            Return _imagenempresa
        End Get
        Set(value As Byte())
            _imagenempresa = value
        End Set
    End Property

End Class
