Public Class clsBeI_nav_productoEquivalencia
    Implements ICloneable
    Sub New()
    End Sub
    Public Property IdProducto As Integer = 0
    Public Property CodigoProductoEmpresa1 As String = ""
    Public Property CodigoProductoEmpresa2 As String = ""
    Public Property CodigoBarra As String = ""
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class