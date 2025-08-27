Public Class FacturaReservaDto
    Public Property DocEntry As Integer
    Public Property DocNum As Integer
    Public Property CardCode As String
    Public Property CardName As String
    Public Property DocDate As Date
    Public Property DocumentLines As List(Of FacturaReservaLine)
    Public Property Comments As String = ""
End Class

Public Class FacturaReservaLine
    Public Property LineNum As Integer
    Public Property ItemCode As String
    Public Property Quantity As Double
    Public Property OpenQty As Double
    Public Property WarehouseCode As String
    Public Property U_Color As String
    Public Property U_Talla As String
    Public Property BaseType As Integer
    Public Property BaseEntry As Integer
    Public Property BaseLine As Integer
End Class


