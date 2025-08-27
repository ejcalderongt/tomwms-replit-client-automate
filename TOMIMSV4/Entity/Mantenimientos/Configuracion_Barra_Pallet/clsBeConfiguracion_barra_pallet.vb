Public Class clsBeConfiguracion_barra_pallet
    Implements ICloneable

    Private mIdConfiguracionPallet As Integer = 0
    Private mLongCodBodegaOrigen As Integer = 0
    Private mLongCodProducto As Integer = 0
    Private mLongLP As Integer = 0
    Private mCodigoNumerico As Boolean = False
    Private mIdentificadorInicio As String = ""

    Public Property IdConfiguracionPallet() As Integer
        Get
            Return mIdConfiguracionPallet
        End Get
        Set(ByVal Value As Integer)
            mIdConfiguracionPallet = Value
        End Set
    End Property

    Public Property LongCodBodegaOrigen() As Integer
        Get
            Return mLongCodBodegaOrigen
        End Get
        Set(ByVal Value As Integer)
            mLongCodBodegaOrigen = Value
        End Set
    End Property

    Public Property LongCodProducto() As Integer
        Get
            Return mLongCodProducto
        End Get
        Set(ByVal Value As Integer)
            mLongCodProducto = Value
        End Set
    End Property

    Public Property LongLP() As Integer
        Get
            Return mLongLP
        End Get
        Set(ByVal Value As Integer)
            mLongLP = Value
        End Set
    End Property

    Public Property CodigoNumerico() As Boolean
        Get
            Return mCodigoNumerico
        End Get
        Set(ByVal Value As Boolean)
            mCodigoNumerico = Value
        End Set
    End Property

    Public Property IdentificadorInicio() As String
        Get
            Return mIdentificadorInicio
        End Get
        Set(ByVal Value As String)
            mIdentificadorInicio = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef IdConfiguracionPallet As Integer, ByVal LongCodBodegaOrigen As Integer, ByVal LongCodProducto As Integer, ByVal LongLP As Integer, ByVal CodigoNumerico As Boolean, ByVal IdentificadorInicio As String)
        mIdConfiguracionPallet = IdConfiguracionPallet
        mLongCodBodegaOrigen = LongCodBodegaOrigen
        mLongCodProducto = LongCodProducto
        mLongLP = LongLP
        mCodigoNumerico = CodigoNumerico
        mIdentificadorInicio = IdentificadorInicio
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
