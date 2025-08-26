Public Class clsBeTipo_tarea_tiempos
    Implements ICloneable

    Private mIdEmpresa As Integer = 0
    Private mIdBodega As Integer = 0
    Private mIdTipoTarea As Integer = 0
    Private mTiempoMedioMinutos As Double = 0.0

    Public Property IdEmpresa() As Integer
        Get
            Return mIdEmpresa
        End Get
        Set(ByVal Value As Integer)
            mIdEmpresa = Value
        End Set
    End Property

    Public Property IdBodega() As Integer
        Get
            Return mIdBodega
        End Get
        Set(ByVal Value As Integer)
            mIdBodega = Value
        End Set
    End Property

    Public Property IdTipoTarea() As Integer
        Get
            Return mIdTipoTarea
        End Get
        Set(ByVal Value As Integer)
            mIdTipoTarea = Value
        End Set
    End Property

    Public Property TiempoMedioMinutos() As Double
        Get
            Return mTiempoMedioMinutos
        End Get
        Set(ByVal Value As Double)
            mTiempoMedioMinutos = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef IdEmpresa As Integer, ByVal IdBodega As Integer, ByVal IdTipoTarea As Integer, ByVal TiempoMedioMinutos As Double)
        mIdEmpresa = IdEmpresa
        mIdBodega = IdBodega
        mIdTipoTarea = IdTipoTarea
        mTiempoMedioMinutos = TiempoMedioMinutos
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
