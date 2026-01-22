Public Class clsBeTrans_inv_resumen
    Implements ICloneable
    Public Property Idinventariores() As Integer = 0
    Public Property Idinventarioenct() As Integer = 0
    Public Property Idtramo() As Integer = 0
    Public Property Idproducto() As Integer = 0
    Public Property Idoperador() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property Idpresentacion() As Integer = 0
    Public Property Idproductoestado() As Integer = 0
    Public Property Cantidad() As Double = 0.0
    Public Property Fecha_captura() As Date = Date.Now
    Public Property Host() As String = ""
    Public Property Nom_producto() As String = ""
    Public Property Nom_operador() As String = ""
    Public Property IdUbicacion() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Lic_plate() As String = ""
    Public Property IdProductoTallaColor() As Integer = 0
    Sub New()
    End Sub
    Sub New(ByRef idinventariores As Integer, ByVal idinventarioenct As Integer, ByVal idtramo As Integer, ByVal idproducto As Integer, ByVal idoperador As Integer, ByVal IdUnidadMedida As Integer, ByVal idpresentacion As Integer, ByVal idproductoestado As Integer, ByVal cantidad As Double, ByVal fecha_captura As Date, ByVal host As String, ByVal nom_producto As String, ByVal nom_operador As String)
        Me.Idinventariores = idinventariores
        Me.Idinventarioenct = idinventarioenct
        Me.Idtramo = idtramo
        Me.Idproducto = idproducto
        Me.Idoperador = idoperador
        Me.IdUnidadMedida = IdUnidadMedida
        Me.Idpresentacion = idpresentacion
        Me.Idproductoestado = idproductoestado
        Me.Cantidad = cantidad
        Me.Fecha_captura = fecha_captura
        Me.Host = host
        Me.Nom_producto = nom_producto
        Me.Nom_operador = nom_operador
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
