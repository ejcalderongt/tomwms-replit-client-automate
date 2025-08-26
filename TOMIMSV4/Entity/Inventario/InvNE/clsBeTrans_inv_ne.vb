Public Class clsBeTrans_inv_ne
    Implements ICloneable

    Private mIdinventarione As Integer = 0
    Private mIdinventarioenc As Integer = 0
    Private mIdproducto As Integer = 0
    Private mCodigo As String = ""
    Private mNombre As String = ""
    Private mCantidad As Double = 0.0
    Private mFec_agr As Date = Date.Now
    Private mUsr_agr As String = ""

    Public Property Idinventarione() As Integer
        Get
            Return mIdinventarione
        End Get
        Set(ByVal Value As Integer)
            mIdinventarione = Value
        End Set
    End Property

    Public Property Idinventarioenc() As Integer
        Get
            Return mIdinventarioenc
        End Get
        Set(ByVal Value As Integer)
            mIdinventarioenc = Value
        End Set
    End Property

    Public Property Idproducto() As Integer
        Get
            Return mIdproducto
        End Get
        Set(ByVal Value As Integer)
            mIdproducto = Value
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

    Public Property Cantidad() As Double
        Get
            Return mCantidad
        End Get
        Set(ByVal Value As Double)
            mCantidad = Value
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

    Public Property Usr_agr() As String
        Get
            Return mUsr_agr
        End Get
        Set(ByVal Value As String)
            mUsr_agr = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef idinventarione As Integer, ByVal idinventarioenc As Integer, ByVal idproducto As Integer, ByVal codigo As String, ByVal nombre As String, ByVal cantidad As Double, ByVal fec_agr As Date, ByVal usr_agr As String)
        mIdinventarione = Idinventarione
        mIdinventarioenc = Idinventarioenc
        mIdproducto = Idproducto
        mCodigo = Codigo
        mNombre = Nombre
        mCantidad = Cantidad
        mFec_agr = Fec_agr
        mUsr_agr = Usr_agr
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
