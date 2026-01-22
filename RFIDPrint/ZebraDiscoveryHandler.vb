Imports Zebra.Sdk.Printer.Discovery

Public Class ZebraDiscoveryHandler
    Implements DiscoveryHandler

    Private ReadOnly _lista As List(Of ImpresoraZebra)
    Public Property Completado As Boolean = False

    Public Sub New(lista As List(Of ImpresoraZebra))
        _lista = lista
    End Sub

    Public Sub DiscoveryError(message As String) Implements DiscoveryHandler.DiscoveryError
        Completado = True
    End Sub

    Public Sub DiscoveryFinished() Implements DiscoveryHandler.DiscoveryFinished
        Completado = True
    End Sub

    Public Sub FoundPrinter(printer As DiscoveredPrinter) Implements DiscoveryHandler.FoundPrinter
        Dim nombre As String = ""
        If printer.DiscoveryDataMap IsNot Nothing AndAlso printer.DiscoveryDataMap.ContainsKey("SYSTEM_NAME") Then
            nombre = printer.DiscoveryDataMap("SYSTEM_NAME")
        End If

        _lista.Add(New ImpresoraZebra With {
            .Nombre = nombre,
            .Direccion = printer.Address,
            .TipoConexion = "Red"
        })
    End Sub

End Class