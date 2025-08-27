Public Class rptDespacho

    Public Property MostrarEncabezadoSoloEnPrimeraPagina As Boolean = False
    Private Property firstPage As Boolean = True
    Private Sub PageHeader_BeforePrint(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PageHeader.BeforePrint

        If MostrarEncabezadoSoloEnPrimeraPagina Then
            e.Cancel = Not firstPage
            firstPage = False
        End If

    End Sub

End Class