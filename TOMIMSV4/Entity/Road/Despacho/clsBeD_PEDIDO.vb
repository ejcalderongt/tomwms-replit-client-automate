Public Class clsBeD_PEDIDO
    Implements ICloneable

    Public Property COREL() As String = ""
    Public Property ANULADO() As String = ""
    Public Property FECHA() As Date = Date.Now
    Public Property EMPRESA() As String = ""
    Public Property RUTA() As String = ""
    Public Property VENDEDOR() As String = ""
    Public Property CLIENTE() As String = ""
    Public Property KILOMETRAJE() As Double = 0.0
    Public Property FECHAENTR() As Date = Date.Now
    Public Property DIRENTREGA() As String = ""
    Public Property TOTAL() As Double = 0.0
    Public Property DESMONTO() As Double = 0.0
    Public Property IMPMONTO() As Double = 0.0
    Public Property PESO() As Double = 0.0
    Public Property BANDERA() As String = ""
    Public Property STATCOM() As String = ""
    Public Property CALCOBJ() As String = ""
    Public Property IMPRES() As Integer = 0
    Public Property ADD1() As String = ""
    Public Property ADD2() As String = ""
    Public Property ADD3() As String = ""
    Public Property STATPROC() As String = ""
    Public Property RECHAZADO() As Boolean = False
    Public Property RAZON_RECHAZADO() As String = ""
    Public Property INFORMADO() As Boolean = False
    Public Property SUCURSAL() As String = ""
    Public Property ID_DESPACHO() As Integer = 0
    Public Property ID_FACTURACION() As Integer = 0
    Public Property RUTASUPER() As String = ""
    Public Property NO_PEDIDO_ERP() As String = ""

    Public Detalle As New List(Of clsBeD_PEDIDOD)

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
