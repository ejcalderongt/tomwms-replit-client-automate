Public Class clsBeVW_Pedidos_Revision
    Implements ICloneable

    Public Property Fecha_Pedido() As Date = Date.Now
    Public Property IdPedidoEnc() As Integer = 0
    Public Property Propietario() As String = ""
    Public Property Cliente() As String = ""
    Public Property IdBodega() As Integer = 0
    Public Property No_documento() As Integer = 0
    Public Property Referencia() As String = ""
    Public Property No_documento_externo() As String = ""
    Public Property NIT_Cliente() As String = ""
    Public Property NIT_Propietario() As String = ""
    Public Property Fecha_aceptacion() As Date = Date.Now
    Public Property Numero_orden() As String = ""
    Public Property Dua() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
