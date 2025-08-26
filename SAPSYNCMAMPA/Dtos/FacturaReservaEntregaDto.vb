Public Class FacturaReservaEntregaDto
    Public Property CardCode As String
    Public Property DocDate As String
    Public Property DocDueDate As String
    Public Property Comments As String
    Public Property U_LOGISTIKA_ID As String = ""
    Public Property DocumentLines As List(Of FacturaReservaEntregaLineDto)
End Class

Public Class FacturaReservaEntregaLineDto
    Public Property BaseType As Integer
    Public Property BaseEntry As Integer
    Public Property BaseLine As Integer
    Public Property ItemCode As String
    Public Property Quantity As Double
    Public Property WarehouseCode As String
    Public Property U_Color As String
    Public Property U_Talla As String
    Public Property BatchNumbers As List(Of BatchNumberDto)
End Class
Public Class BatchNumberDto
    Public Property BatchNumber As String
    Public Property Quantity As Decimal
End Class
