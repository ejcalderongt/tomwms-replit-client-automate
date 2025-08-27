Public Class clsBeVersion_wms_hh
    Implements ICloneable

    Private mIdEmpresaVersion As Integer = 0
    Private mIdEmpresa As Integer = 0
    Private mVersion As String = ""
    Private mNotas As String = ""
    Private mFecha As Date = Date.Now

    Public Property IdEmpresaVersion() As Integer
        Get
            Return mIdEmpresaVersion
        End Get
        Set(ByVal Value As Integer)
            mIdEmpresaVersion = Value
        End Set
    End Property

    Public Property IdEmpresa() As Integer
        Get
            Return mIdEmpresa
        End Get
        Set(ByVal Value As Integer)
            mIdEmpresa = Value
        End Set
    End Property

    Public Property Version() As String
        Get
            Return mVersion
        End Get
        Set(ByVal Value As String)
            mVersion = Value
        End Set
    End Property

    Public Property Notas() As String
        Get
            Return mNotas
        End Get
        Set(ByVal Value As String)
            mNotas = Value
        End Set
    End Property

    Public Property Fecha() As Date
        Get
            Return mFecha
        End Get
        Set(ByVal Value As Date)
            mFecha = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef IdEmpresaVersion As Integer, ByVal IdEmpresa As Integer, ByVal version As String, ByVal notas As String, ByVal fecha As Date)
        mIdEmpresaVersion = IdEmpresaVersion
        mIdEmpresa = IdEmpresa
        mVersion = Version
        mNotas = Notas
        mFecha = Fecha
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
