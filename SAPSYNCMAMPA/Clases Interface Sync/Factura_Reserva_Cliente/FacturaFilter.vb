Imports Newtonsoft.Json.Linq

Public Module FacturaFilter

    Public Async Function FiltrarLineasNoServicioPorGrupoAsync(
        factura As JObject,
        cache As OitmCache,
        Optional grupoServicio As String = "19"
    ) As Threading.Tasks.Task

        Dim docLines = TryCast(factura("DocumentLines"), JArray)
        If docLines Is Nothing OrElse docLines.Count = 0 Then Return

        Dim nuevasLineas As New JArray()

        For Each l As JObject In docLines
            Dim itemCode As String = l?("ItemCode")?.ToString()

            ' Si no hay ItemCode, normalmente es línea de servicio/texto → decide tu regla:
            ' Aquí: la EXCLUIMOS (no va al WMS)
            If String.IsNullOrWhiteSpace(itemCode) Then
                Continue For
            End If

            Dim grupo = Await cache.GetUGrupoAsync(itemCode)

            ' Si grupo viene null/vacío, normalmente lo dejamos pasar (ajusta si quieres)
            If String.IsNullOrEmpty(grupo) Then
                nuevasLineas.Add(l)
            ElseIf grupo <> grupoServicio Then
                nuevasLineas.Add(l)
            End If
        Next

        factura("DocumentLines") = nuevasLineas
    End Function

End Module
