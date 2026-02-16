Public Class clsSyncSAPDevolucionMercancia : Inherits clsInterfaceBase

    Implements IDisposable
    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Public Class DetalleDevolucionCliente
        Public Property LineNum As Integer
        Public Property ItemCode As String
        Public Property Dscription As String
        Public Property Quantity As Double
        Public Property Price As Double
        Public Property UomCode As String

    End Class

End Class