Public Class clsBeVW_Pe_Con_Diferencias
    Implements ICloneable

    Public Property ORDENPEDIDO() As Integer = 0
    Public Property Codigo_producto() As String = ""
    Public Property Nombre_producto() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Cant_despachada() As Double = 0.0
    Public Property PRESENTACION() As String = ""
    Public Property DIFERENCIA() As Double = 0.0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property BODEGA() As String = ""
    Public Property PROPIETARIO() As String = ""
    Public Property IdTipoPedido() As Integer = 0
    Public Property NOMBRE_PEDIDO() As String = ""
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedidaBasica() As Integer = 0
    Public Property UMBas() As String = ""
    Public Property Estado() As String = ""
    Public Property Activo() As Boolean = False
    Public Property Fecha_Pedido() As Date = Date.Now
    Public Property IdPropietario() As Integer = 0
    Public Property IdBodega() As Integer = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
