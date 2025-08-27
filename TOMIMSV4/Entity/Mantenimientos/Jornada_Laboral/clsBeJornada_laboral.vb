Public Class clsBeJornada_laboral
    Implements ICloneable

    Private mIdJornada As Integer = 0
    Private mIdBodega As Integer = 0
    Private mNombre_jornada As String = ""
    Private mFecha_inicio As Date = Date.Now
    Private mFecha_fin As Date = Date.Now
    Private mHoras_trabajadas As Integer = 0
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mUser_mod As String = ""
    Private mFec_mod As Date = Date.Now
    Private mFecha_baja As Date = Date.Now
    Private mActivo As Boolean = False

    Public Property IdJornada() As Integer
        Get
            Return mIdJornada
        End Get
        Set(ByVal Value As Integer)
            mIdJornada = Value
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

    Public Property Nombre_jornada() As String
        Get
            Return mNombre_jornada
        End Get
        Set(ByVal Value As String)
            mNombre_jornada = Value
        End Set
    End Property

    Public Property Fecha_inicio() As Date
        Get
            Return mFecha_inicio
        End Get
        Set(ByVal Value As Date)
            mFecha_inicio = Value
        End Set
    End Property

    Public Property Fecha_fin() As Date
        Get
            Return mFecha_fin
        End Get
        Set(ByVal Value As Date)
            mFecha_fin = Value
        End Set
    End Property

    Public Property Horas_trabajadas() As Integer
        Get
            Return mHoras_trabajadas
        End Get
        Set(ByVal Value As Integer)
            mHoras_trabajadas = Value
        End Set
    End Property

    Public Property User_agr() As String
        Get
            Return mUser_agr
        End Get
        Set(ByVal Value As String)
            mUser_agr = Value
        End Set
    End Property

    Public Property Fec_agr() As Date
        Get
            Return mFec_agr
        End Get
        Set(ByVal Value As Date)
            mFec_agr = Value
        End Set
    End Property

    Public Property User_mod() As String
        Get
            Return mUser_mod
        End Get
        Set(ByVal Value As String)
            mUser_mod = Value
        End Set
    End Property

    Public Property Fec_mod() As Date
        Get
            Return mFec_mod
        End Get
        Set(ByVal Value As Date)
            mFec_mod = Value
        End Set
    End Property

    Public Property Fecha_baja() As Date
        Get
            Return mFecha_baja
        End Get
        Set(ByVal Value As Date)
            mFecha_baja = Value
        End Set
    End Property

    Public Property Activo() As Boolean
        Get
            Return mActivo
        End Get
        Set(ByVal Value As Boolean)
            mActivo = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef IdJornada As Integer, ByVal IdBodega As Integer, ByVal nombre_jornada As String, ByVal fecha_inicio As Date, ByVal fecha_fin As Date, ByVal horas_trabajadas As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal fecha_baja As Date, ByVal activo As Boolean)
        mIdJornada = IdJornada
        mIdBodega = IdBodega
        mNombre_jornada = nombre_jornada
        mFecha_inicio = fecha_inicio
        mFecha_fin = fecha_fin
        mHoras_trabajadas = horas_trabajadas
        mUser_agr = user_agr
        mFec_agr = fec_agr
        mUser_mod = user_mod
        mFec_mod = fec_mod
        mFecha_baja = fecha_baja
        mActivo = activo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
