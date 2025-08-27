Public Class clsBeTrans_inv_detalle_grid
    Implements ICloneable
    Public Property Idinventariodet() As Integer = 0
    Public Property Idinventarioenc() As Integer = 0
    Public Property Idtramo() As Integer = 0
    Public Property IdUbic() As Integer = 0
    Public Property Ubic() As String = ""
    Public Property Idproducto() As Integer = 0
    Public Property productoestado() As String = ""
    Public Property presentacion() As String = ""
    Public Property UnidadMedida() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Peso() As Double = 0.0
    Sub New()
    End Sub
    Sub New(ByRef idinventariores As Integer, ByVal idinventarioenc As Integer, ByVal idtramo As Integer, ByVal idubic As Integer, ByVal ubic As String, ByVal idproducto As Integer, ByVal idproductoestado As String, ByVal idpresentacion As String, ByVal cantidad As Double, ByVal UnidadMedida As String, peso As Double)
        Idinventariodet = idinventariores
        Me.Idinventarioenc = idinventarioenc
        Me.Idtramo = idtramo
        Me.IdUbic = idubic
        Me.Ubic = ubic
        Me.Idproducto = idproducto
        productoestado = idproductoestado
        presentacion = idpresentacion
        Me.Cantidad = cantidad
        Me.UnidadMedida = UnidadMedida
        Me.Peso = peso
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
