<Serializable>
Public Class clsBeProducto_kit_composicion
    Implements ICloneable

    Public Property IdProductoKitComposicion() As Integer = 0
    Public Property IdProductoPadre() As Integer = 0
    Public Property IdProductoHijo() As Integer = 0
    Public Property IdUnidadMedidaBasicaPadre() As Integer = 0
    Public Property IdUnidadMedidaBasicaHijo() As Integer = 0
    Public Property Cantidad() As Double = 0.0
    Public Property Fecha_agr() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fecha_mod() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property IdBodega() As Integer = 0
    Public Property Producto As clsBeProducto = New clsBeProducto()
    Public Property IsNew() As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdProductoKitComposicion As Integer, ByVal IdProductoPadre As Integer, ByVal IdProductoHijo As Integer, ByVal IdUnidadMedidaBasicaPadre As Integer, ByVal IdUnidadMedidaBasicaHijo As Integer, ByVal Cantidad As Double, ByVal fecha_agr As Date, ByVal user_agr As String, ByVal fecha_mod As Date, ByVal user_mod As String, ByVal IdBodega As Integer)
        Me.IdProductoKitComposicion = IdProductoKitComposicion
        Me.IdProductoPadre = IdProductoPadre
        Me.IdProductoHijo = IdProductoHijo
        Me.IdUnidadMedidaBasicaPadre = IdUnidadMedidaBasicaPadre
        Me.IdUnidadMedidaBasicaHijo = IdUnidadMedidaBasicaHijo
        Me.Cantidad = Cantidad
        Me.Fecha_agr = fecha_agr
        Me.User_agr = user_agr
        Me.Fecha_mod = fecha_mod
        Me.User_mod = user_mod
        Me.IdBodega = IdBodega
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
