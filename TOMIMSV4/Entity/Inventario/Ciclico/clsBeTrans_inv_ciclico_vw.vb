Public Class clsBeTrans_inv_ciclico_vw
    Implements ICloneable

    Private mIdinvciclico As Integer = 0
    Private mIdinventarioenc As Integer = 0
    Private mIdStock As Integer = 0
    Private mIdProductoBodega As Integer = 0
    Private mIdProductoEstado As Integer = 0
    Private mIdPresentacion As Integer = 0
    Private mIdUbicacion As Integer = 0
    Private mEsNuevo As Boolean = False
    Private mLote_stock As String = ""
    Private mLote As String = ""
    Private mFecha_vence_stock As String = ""
    Private mFecha_vence As Date = Nothing
    Private mCant_stock As Double = 0.0
    Private mCantidad As Double = 0.0
    Private mCant_reconteo As Double = 0.0
    Private mPeso_stock As Double = 0.0
    Private mPeso As Double = 0.0
    Private mPeso_reconteo As Double = 0.0
    Private mIdoperador As Integer = 0
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mIdTramo As Integer = 0
    Private mEstado_nombre As String = ""
    Private mProducto_nombre As String = ""
    Private mUbic_nombre As String = ""
    Private mPres_nombre As String = ""
    Private mUnid_nombre As String = ""
    Private mControl_peso As Boolean = False
    Private mGenera_lote As Boolean = False
    Private mControl_vencimiento As Boolean = False
    Private mIdProductoEst_nuevo As Integer = 0
    Private midPresentacion_nuevo As Integer = 0
    Private midreconteo As Integer = 0
    Private mCodigo_Producto As String = ""
    Private mColumna As Integer = 0
    Private mNivel As Integer = 0
    Private mPosicion As String = ""
    Private mTotal As Integer = 0
    Private mFactor As Double = 0
    Private mIdBodega As Integer = 0

    Public Property Idinvciclico() As Integer
        Get
            Return mIdinvciclico
        End Get
        Set(ByVal Value As Integer)
            mIdinvciclico = Value
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

    Public Property IdStock() As Integer
        Get
            Return mIdStock
        End Get
        Set(ByVal Value As Integer)
            mIdStock = Value
        End Set
    End Property

    Public Property IdProductoBodega() As Integer
        Get
            Return mIdProductoBodega
        End Get
        Set(ByVal Value As Integer)
            mIdProductoBodega = Value
        End Set
    End Property

    Public Property IdProductoEstado() As Integer
        Get
            Return mIdProductoEstado
        End Get
        Set(ByVal Value As Integer)
            mIdProductoEstado = Value
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

    Public Property IdUbicacion() As Integer
        Get
            Return mIdUbicacion
        End Get
        Set(ByVal Value As Integer)
            mIdUbicacion = Value
        End Set
    End Property

    Public Property EsNuevo() As Boolean
        Get
            Return mEsNuevo
        End Get
        Set(ByVal Value As Boolean)
            mEsNuevo = Value
        End Set
    End Property

    Public Property Lote_stock() As String
        Get
            Return mLote_stock
        End Get
        Set(ByVal Value As String)
            mLote_stock = Value
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

    Public Property Fecha_vence_stock() As Date
        Get
            Return mFecha_vence_stock
        End Get
        Set(ByVal Value As Date)
            mFecha_vence_stock = Value
        End Set
    End Property

    Public Property Fecha_vence() As Date
        Get
            Return mFecha_vence
        End Get
        Set(ByVal Value As Date)
            mFecha_vence = Value
        End Set
    End Property

    Public Property Cant_stock() As Double
        Get
            Return mCant_stock
        End Get
        Set(ByVal Value As Double)
            mCant_stock = Value
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

    Public Property Cant_reconteo() As Double
        Get
            Return mCant_reconteo
        End Get
        Set(ByVal Value As Double)
            mCant_reconteo = Value
        End Set
    End Property

    Public Property Peso_stock() As Double
        Get
            Return mPeso_stock
        End Get
        Set(ByVal Value As Double)
            mPeso_stock = Value
        End Set
    End Property

    Public Property Peso() As Double
        Get
            Return mPeso
        End Get
        Set(ByVal Value As Double)
            mPeso = Value
        End Set
    End Property

    Public Property Peso_reconteo() As Double
        Get
            Return mPeso_reconteo
        End Get
        Set(ByVal Value As Double)
            mPeso_reconteo = Value
        End Set
    End Property

    Public Property Idoperador() As Integer
        Get
            Return mIdoperador
        End Get
        Set(ByVal Value As Integer)
            mIdoperador = Value
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

    Public Property IdTramo() As Integer
        Get
            Return mIdTramo
        End Get
        Set(ByVal Value As Integer)
            mIdTramo = Value
        End Set
    End Property

    Public Property Estado_nombre() As String
        Get
            Return mEstado_nombre
        End Get
        Set(ByVal Value As String)
            mEstado_nombre = Value
        End Set
    End Property

    Public Property Producto_nombre() As String
        Get
            Return mProducto_nombre
        End Get
        Set(ByVal Value As String)
            mProducto_nombre = Value
        End Set
    End Property

    Public Property Ubic_nombre() As String
        Get
            Return mUbic_nombre
        End Get
        Set(ByVal Value As String)
            mUbic_nombre = Value
        End Set
    End Property

    Public Property Pres_nombre() As String
        Get
            Return mPres_nombre
        End Get
        Set(ByVal Value As String)
            mPres_nombre = Value
        End Set
    End Property

    Public Property Unid_nombre() As String
        Get
            Return mUnid_nombre
        End Get
        Set(ByVal Value As String)
            mUnid_nombre = Value
        End Set
    End Property

    Public Property Control_peso() As Boolean
        Get
            Return mControl_peso
        End Get
        Set(ByVal Value As Boolean)
            mControl_peso = Value
        End Set
    End Property

    Public Property Genera_lote() As Boolean
        Get
            Return mGenera_lote
        End Get
        Set(ByVal Value As Boolean)
            mGenera_lote = Value
        End Set
    End Property

    Public Property Control_vencimiento As Boolean
        Get
            Return Control_vencimiento
        End Get
        Set(ByVal Value As Boolean)
            mControl_vencimiento = Value
        End Set
    End Property

    Public Property IdProductoEst_nuevo() As Integer
        Get
            Return mIdProductoEst_nuevo
        End Get
        Set(ByVal Value As Integer)
            mIdProductoEst_nuevo = Value
        End Set
    End Property

    Public Property idPresentacion_nuevo() As Integer
        Get
            Return midPresentacion_nuevo
        End Get
        Set(ByVal Value As Integer)
            midPresentacion_nuevo = Value
        End Set
    End Property

    Public Property IdReconteo() As Integer
        Get
            Return midreconteo
        End Get
        Set(ByVal Value As Integer)
            midreconteo = Value
        End Set
    End Property

    Public Property Codigo_Producto() As String
        Get
            Return mCodigo_Producto
        End Get
        Set(ByVal Value As String)
            mCodigo_Producto = Value
        End Set
    End Property

    Public Property Columna() As Integer
        Get
            Return mColumna
        End Get
        Set(ByVal Value As Integer)
            mColumna = Value
        End Set
    End Property

    Public Property Nivel() As Integer
        Get
            Return mNivel
        End Get
        Set(ByVal Value As Integer)
            mNivel = Value
        End Set
    End Property

    Public Property Posicion() As String
        Get
            Return mPosicion
        End Get
        Set(ByVal Value As String)
            mPosicion = Value
        End Set
    End Property
    Public Property Ubicacion As New clsBeBodega_ubicacion

    Public Property Total() As Integer
        Get
            Return mTotal
        End Get
        Set(ByVal Value As Integer)
            mTotal = Value
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

    Public Property IdBodega() As Integer
        Get
            Return mIdBodega
        End Get
        Set(ByVal Value As Integer)
            mIdBodega = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef idinvciclico As Integer, ByVal idinventarioenc As Integer, ByVal IdStock As Integer, ByVal IdProductoBodega As Integer, ByVal IdProductoEstado As Integer, ByVal IdPresentacion As Integer, ByVal IdUbicacion As Integer, ByVal EsNuevo As Boolean, ByVal lote_stock As String, ByVal lote As String, ByVal fecha_vence_stock As Date, ByVal fecha_vence As Date, ByVal cant_stock As Double, ByVal cantidad As Double, ByVal cant_reconteo As Double, ByVal peso_stock As Double, ByVal peso As Double, ByVal peso_reconteo As Double, ByVal idoperador As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal IdTramo As Integer, ByVal estado_nombre As String, ByVal producto_nombre As String, ByVal ubic_nombre As String, ByVal pres_nombre As String, ByVal unid_nombre As String, control_peso As Boolean, Genera_lote As Boolean, Control_vencimiento As Boolean, idReconteo As Integer, Codigo_Producto As String, Columna As Integer, Nivel As Integer, Posicion As String, ByVal Total As Integer, ByVal Factor As Double)
        mIdinvciclico = idinvciclico
        mIdinventarioenc = idinventarioenc
        mIdStock = IdStock
        mIdProductoBodega = IdProductoBodega
        mIdProductoEstado = IdProductoEstado
        mIdPresentacion = IdPresentacion
        mIdUbicacion = IdUbicacion
        mEsNuevo = EsNuevo
        mLote_stock = lote_stock
        mLote = lote
        mFecha_vence_stock = fecha_vence_stock
        mFecha_vence = fecha_vence
        mCant_stock = cant_stock
        mCantidad = cantidad
        mCant_reconteo = cant_reconteo
        mPeso_stock = peso_stock
        mPeso = peso
        mPeso_reconteo = peso_reconteo
        mIdoperador = idoperador
        mUser_agr = user_agr
        mFec_agr = fec_agr
        mIdTramo = IdTramo
        mEstado_nombre = estado_nombre
        mProducto_nombre = producto_nombre
        mUbic_nombre = ubic_nombre
        mPres_nombre = pres_nombre
        mUnid_nombre = unid_nombre
        mControl_peso = control_peso
        mGenera_lote = Genera_lote
        mControl_vencimiento = Control_vencimiento
        midreconteo = idReconteo
        mCodigo_Producto = Codigo_Producto
        mColumna = Columna
        mNivel = Nivel
        mPosicion = Posicion
        mTotal = Total
        mFactor = Factor
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
