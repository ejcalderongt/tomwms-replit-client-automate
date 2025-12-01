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
        Me.Idinventariores = Idinventariores
        Me.Idinventarioenct = Idinventarioenct
        Me.Idtramo = Idtramo
        Me.Idproducto = Idproducto
        Me.Idoperador = Idoperador
        Me.IdUnidadMedida = IdUnidadMedida
        Me.Idpresentacion = Idpresentacion
        Me.Idproductoestado = Idproductoestado
        Me.Cantidad = Cantidad
        Me.Fecha_captura = Fecha_captura
        Me.Host = Host
        Me.Nom_producto = Nom_producto
        Me.Nom_operador = Nom_operador
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
