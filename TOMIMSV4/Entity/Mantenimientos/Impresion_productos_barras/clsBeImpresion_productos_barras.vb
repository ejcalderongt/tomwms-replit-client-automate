Public Class clsBeImpresion_productos_barras
    Implements ICloneable

    Private mIdProductoBarra As Integer = 0
    Private mCodigo As String = ""
    Private mNombre As String = ""
    Private mCodigo_barra As String = ""
    Private mCantidad_impresiones As Integer = 0
    Private mIdPresentacion As Integer = 0
    Private mIdUnidadMedidaBasica As Integer = 0
    Private mUnidadMedida As String = ""
    Private mPresentacion As String = ""
    Private mCamas_Por_Tarima As Integer = 0
    Private mCajas_Por_Cama As Integer = 0
    Private mCantidad_Presentacion As Double = 0.0
    Private mFactor As Double = 0.0
    Private mLote As String = ""
    Private mFecha_Ingreso As Date = Date.Now
    Private mFecha_Vence As Date = Date.Now
    Private mFecha_agr As Date = Date.Now
    Private mUser_agr As String = ""
    Private mImpreso As Boolean = False
    Private mActivo As Boolean = False

    Public Property IdProductoBarra() As Integer
        Get
            Return mIdProductoBarra
        End Get
        Set(ByVal Value As Integer)
            mIdProductoBarra = Value
        End Set
    End Property

    Public Property Codigo() As String
        Get
            Return mCodigo
        End Get
        Set(ByVal Value As String)
            mCodigo = Value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return mNombre
        End Get
        Set(ByVal Value As String)
            mNombre = Value
        End Set
    End Property

    Public Property Codigo_barra() As String
        Get
            Return mCodigo_barra
        End Get
        Set(ByVal Value As String)
            mCodigo_barra = Value
        End Set
    End Property

    Public Property Cantidad_impresiones() As Integer
        Get
            Return mCantidad_impresiones
        End Get
        Set(ByVal Value As Integer)
            mCantidad_impresiones = Value
        End Set
    End Property

    Public Property IdPresentacion() As Integer
        Get
            Return mIdPresentacion
        End Get
        Set(ByVal Value As Integer)
            mIdPresentacion = Value
        End Set
    End Property

    Public Property IdUnidadMedidaBasica() As Integer
        Get
            Return mIdUnidadMedidaBasica
        End Get
        Set(ByVal Value As Integer)
            mIdUnidadMedidaBasica = Value
        End Set
    End Property

    Public Property UnidadMedida() As String
        Get
            Return mUnidadMedida
        End Get
        Set(ByVal Value As String)
            mUnidadMedida = Value
        End Set
    End Property

    Public Property Presentacion() As String
        Get
            Return mPresentacion
        End Get
        Set(ByVal Value As String)
            mPresentacion = Value
        End Set
    End Property

    Public Property Camas_Por_Tarima() As Integer
        Get
            Return mCamas_Por_Tarima
        End Get
        Set(ByVal Value As Integer)
            mCamas_Por_Tarima = Value
        End Set
    End Property

    Public Property Cajas_Por_Cama() As Integer
        Get
            Return mCajas_Por_Cama
        End Get
        Set(ByVal Value As Integer)
            mCajas_Por_Cama = Value
        End Set
    End Property

    Public Property Cantidad_Presentacion() As Double
        Get
            Return mCantidad_Presentacion
        End Get
        Set(ByVal Value As Double)
            mCantidad_Presentacion = Value
        End Set
    End Property

    Public Property Factor() As Double
        Get
            Return mFactor
        End Get
        Set(ByVal Value As Double)
            mFactor = Value
        End Set
    End Property

    Public Property Lote() As String
        Get
            Return mLote
        End Get
        Set(ByVal Value As String)
            mLote = Value
        End Set
    End Property

    Public Property Fecha_Ingreso() As Date
        Get
            Return mFecha_Ingreso
        End Get
        Set(ByVal Value As Date)
            mFecha_Ingreso = Value
        End Set
    End Property

    Public Property Fecha_Vence() As Date
        Get
            Return mFecha_Vence
        End Get
        Set(ByVal Value As Date)
            mFecha_Vence = Value
        End Set
    End Property

    Public Property Fecha_agr() As Date
        Get
            Return mFecha_agr
        End Get
        Set(ByVal Value As Date)
            mFecha_agr = Value
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

    Public Property Impreso() As Boolean
        Get
            Return mImpreso
        End Get
        Set(ByVal Value As Boolean)
            mImpreso = Value
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

    Public Property IdImpresora As Integer

    Sub New(ByRef IdProductoBarra As Integer, ByVal codigo As String, ByVal nombre As String, ByVal codigo_barra As String, ByVal cantidad_impresiones As Integer, ByVal IdPresentacion As Integer, ByVal IdUnidadMedidaBasica As Integer, ByVal UnidadMedida As String, ByVal Presentacion As String, ByVal Camas_Por_Tarima As Integer, ByVal Cajas_Por_Cama As Integer, ByVal Cantidad_Presentacion As Double, ByVal Factor As Double, ByVal Lote As String, ByVal Fecha_Ingreso As String, ByVal Fecha_Vence As String, ByVal fecha_agr As Date, ByVal user_agr As String, ByVal impreso As Boolean, ByVal activo As Boolean, ByVal IdImpresora As Integer)
        mIdProductoBarra = IdProductoBarra
        mCodigo = codigo
        mNombre = nombre
        mCodigo_barra = codigo_barra
        mCantidad_impresiones = cantidad_impresiones
        mIdPresentacion = IdPresentacion
        mIdUnidadMedidaBasica = IdUnidadMedidaBasica
        mUnidadMedida = UnidadMedida
        mPresentacion = Presentacion
        mCamas_Por_Tarima = Camas_Por_Tarima
        mCajas_Por_Cama = Cajas_Por_Cama
        mCantidad_Presentacion = Cantidad_Presentacion
        mFactor = Factor
        mLote = Lote
        mFecha_Ingreso = Fecha_Ingreso
        mFecha_Vence = Fecha_Vence
        mFecha_agr = fecha_agr
        mUser_agr = user_agr
        mImpreso = impreso
        mActivo = activo
        IdImpresora = IdImpresora
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
