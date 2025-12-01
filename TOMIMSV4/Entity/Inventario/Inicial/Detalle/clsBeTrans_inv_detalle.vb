Public Class clsBeTrans_inv_detalle
    Implements ICloneable
    Public Property Idinventariodet() As Integer = 0
    Public Property Idinventarioenc() As Integer = 0
    Public Property Idtramo() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property Idoperador() As Integer = 0
    Public Property Idproducto() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property Idunidadmedida() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Fecha_vence() As Date = Date.Now
    Public Property Serie() As String = ""
    Public Property Idproductoestado() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Fecha_captura() As DateTime = Now
    Public Property Host() As String = ""
    Public Property Nom_producto() As String = ""
    Public Property Nom_operador() As String = ""
    Public Property Carga() As Integer = 0
    Public Property Peso() As Double = 0

    'GT 10052021 propiedades para identificar un inv. multiempresa
    Public Property IdPropietarioBodega As Integer = 0
    Public Property nombre_propietario As String = ""

    Public Property License_plate As String = ""
    Public Property Codigo_variante As String = ""

    'AT20220504 Se agrega este campo para poder ser guardado
    Public Property IdBodega As Integer = 0


    '#GT25112022_1130: campos DyD
    Public Property costo As Double = 0.0
    Public Property precio As Double = 0.0
    Public Property IdProductoParametroA As Integer = 0
    Public Property IdProductoParametroB As Integer = 0
    Public Property IdProductoTallaColor As Integer = 0

    Sub New()
    End Sub
    Sub New(ByRef idinventariodet As Integer, ByVal idinventarioenc As Integer, ByVal idtramo As Integer, ByVal IdUbicacion As Integer,
            ByVal idoperador As Integer, ByVal idproducto As Integer, ByVal IdPresentacion As Integer, ByVal idunidadmedida As Integer,
            ByVal lote As String, ByVal fecha_vence As Date, ByVal serie As String, ByVal idproductoestado As String, ByVal cantidad As Double,
            ByVal fecha_captura As Date, ByVal host As String, ByVal nom_producto As String, ByVal nom_operador As String,
            ByVal carga As Integer, peso As Double, ByVal IdPropietarioBodega As Integer, ByVal nombre_propietario As String)
        Me.Idinventariodet = idinventariodet
        Me.Idinventarioenc = idinventarioenc
        Me.Idtramo = idtramo
        Me.IdUbicacion = IdUbicacion
        Me.Idoperador = idoperador
        Me.Idproducto = idproducto
        Me.IdPresentacion = IdPresentacion
        Me.Idunidadmedida = idunidadmedida
        Me.Lote = lote
        Me.Fecha_vence = fecha_vence
        Me.Serie = serie
        Me.Idproductoestado = idproductoestado
        Me.Cantidad = cantidad
        Me.Fecha_captura = fecha_captura
        Me.Host = host
        Me.Nom_producto = nom_producto
        Me.Nom_operador = nom_operador
        Me.Carga = carga
        Me.Peso = peso
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.nombre_propietario = nombre_propietario

    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
