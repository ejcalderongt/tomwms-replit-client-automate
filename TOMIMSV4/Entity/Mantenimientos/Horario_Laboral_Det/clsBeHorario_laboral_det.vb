Public Class clsBeHorario_laboral_det
    Implements ICloneable

    Private mIdHorarioLaboralDet As Integer = 0
    Private mIdHorarioLaboralEnc As Integer = 0
    Private mDia As Integer = 0
    Private mHora_inicio As Date = Date.Now
    Private mHora_fin As Date = Date.Now
    Private mMinimo_min_hora_ingreso As Integer = 0
    Private mMaximo_min_hora_ingreso As Integer = 0
    Private mMinimo_min_hora_salida As Integer = 0
    Private mMaximo_min_hora_salida As Integer = 0
    Private mTiempo_retraso_permitido As Integer = 0
    Private mHoras_extras As Boolean = False
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mUser_mod As String = ""
    Private mFec_mod As Date = Date.Now
    Private mFecha_baja As Date = Date.Now
    Private mActivo As Boolean = False

    Public Property IdHorarioLaboralDet() As Integer
        Get
            Return mIdHorarioLaboralDet
        End Get
        Set(ByVal Value As Integer)
            mIdHorarioLaboralDet = Value
        End Set
    End Property

    Public Property IdHorarioLaboralEnc() As Integer
        Get
            Return mIdHorarioLaboralEnc
        End Get
        Set(ByVal Value As Integer)
            mIdHorarioLaboralEnc = Value
        End Set
    End Property

    Public Property Dia() As Integer
        Get
            Return mDia
        End Get
        Set(ByVal Value As Integer)
            mDia = Value
        End Set
    End Property

    Public Property Hora_inicio() As Date
        Get
            Return mHora_inicio
        End Get
        Set(ByVal Value As Date)
            mHora_inicio = Value
        End Set
    End Property

    Public Property Hora_fin() As Date
        Get
            Return mHora_fin
        End Get
        Set(ByVal Value As Date)
            mHora_fin = Value
        End Set
    End Property

    Public Property Minimo_min_hora_ingreso() As Integer
        Get
            Return mMinimo_min_hora_ingreso
        End Get
        Set(ByVal Value As Integer)
            mMinimo_min_hora_ingreso = Value
        End Set
    End Property

    Public Property Maximo_min_hora_ingreso() As Integer
        Get
            Return mMaximo_min_hora_ingreso
        End Get
        Set(ByVal Value As Integer)
            mMaximo_min_hora_ingreso = Value
        End Set
    End Property

    Public Property Minimo_min_hora_salida() As Integer
        Get
            Return mMinimo_min_hora_salida
        End Get
        Set(ByVal Value As Integer)
            mMinimo_min_hora_salida = Value
        End Set
    End Property

    Public Property Maximo_min_hora_salida() As Integer
        Get
            Return mMaximo_min_hora_salida
        End Get
        Set(ByVal Value As Integer)
            mMaximo_min_hora_salida = Value
        End Set
    End Property

    Public Property Tiempo_retraso_permitido() As Integer
        Get
            Return mTiempo_retraso_permitido
        End Get
        Set(ByVal Value As Integer)
            mTiempo_retraso_permitido = Value
        End Set
    End Property

    Public Property Horas_extras() As Boolean
        Get
            Return mHoras_extras
        End Get
        Set(ByVal Value As Boolean)
            mHoras_extras = Value
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

    Sub New(ByRef IdHorarioLaboralDet As Integer, ByVal IdHorarioLaboralEnc As Integer, ByVal dia As Integer, ByVal hora_inicio As Date, ByVal hora_fin As Date, ByVal minimo_min_hora_ingreso As Integer, ByVal maximo_min_hora_ingreso As Integer, ByVal minimo_min_hora_salida As Integer, ByVal maximo_min_hora_salida As Integer, ByVal tiempo_retraso_permitido As Integer, ByVal horas_extras As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal fecha_baja As Date, ByVal activo As Boolean)
        mIdHorarioLaboralDet = IdHorarioLaboralDet
        mIdHorarioLaboralEnc = IdHorarioLaboralEnc
        mDia = Dia
        mHora_inicio = Hora_inicio
        mHora_fin = Hora_fin
        mMinimo_min_hora_ingreso = Minimo_min_hora_ingreso
        mMaximo_min_hora_ingreso = Maximo_min_hora_ingreso
        mMinimo_min_hora_salida = Minimo_min_hora_salida
        mMaximo_min_hora_salida = Maximo_min_hora_salida
        mTiempo_retraso_permitido = Tiempo_retraso_permitido
        mHoras_extras = Horas_extras
        mUser_agr = User_agr
        mFec_agr = Fec_agr
        mUser_mod = User_mod
        mFec_mod = Fec_mod
        mFecha_baja = Fecha_baja
        mActivo = Activo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
