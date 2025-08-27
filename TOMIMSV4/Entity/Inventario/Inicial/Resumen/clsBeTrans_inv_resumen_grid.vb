Public Class clsBeTrans_inv_resumen_grid
    Implements ICloneable
    Public Property Idinventariores() As Integer = 0
    Public Property Idinventarioenct() As Integer = 0
    Public Property Idtramo() As Integer = 0
    Public Property Idproducto() As Integer = 0
    Public Property productoestado() As String = ""
    Public Property presentacion() As String = ""
    Public Property UnidadMedida() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property IdUbicacion() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Nom_Ubicacion() As String = ""
    Sub New()
    End Sub
    Sub New(ByRef idinventariores As Integer, ByVal idinventarioenct As Integer, ByVal idtramo As Integer, ByVal idproducto As Integer, ByVal idproductoestado As String, ByVal idpresentacion As String, ByVal cantidad As Double, ByVal UnidadMedida As String)
        Me.Idinventariores = idinventariores
        Me.Idinventarioenct = idinventarioenct
        Me.Idtramo = idtramo
        Me.Idproducto = idproducto
        productoestado = idproductoestado
        presentacion = idpresentacion
        Me.Cantidad = cantidad
        Me.UnidadMedida = UnidadMedida
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
