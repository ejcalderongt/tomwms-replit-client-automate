Public Class ResultadoOC
    Public Property ListaOC As New List(Of OrdenCompra)
    Public Property BeRecepcion As clsBeTrans_re_enc = Nothing
End Class

Public Class OrdenCompra
    Public Property IdOrdenCompraEnc As Integer
    Public Property IdRecepcionEnc As Integer
End Class
