Public Class clsBeVWInventarioTeoricoSAP
    Public Property Codigo As String = String.Empty
    Public Property Nombre As String = String.Empty
    Public Property Codigo_Bodega As String = String.Empty
    Public Property Nombre_Bodega As String = String.Empty
    Public Property Total_Almacen As Decimal = 0
    Public Property Lote As String = String.Empty
    Public Property Cantidad_Lote As Decimal = 0
    Public Property Fecha_Vence As Date = New DateTime(1900, 1, 1)

    Public Sub New()
        ' Constructor vacío
    End Sub

End Class
