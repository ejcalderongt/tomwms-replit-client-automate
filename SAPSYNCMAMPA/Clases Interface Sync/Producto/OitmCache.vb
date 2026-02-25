Imports System.Net.Http
Imports Newtonsoft.Json.Linq

Public Class OitmCache
    Private ReadOnly _http As HttpClient
    Private ReadOnly _slBase As String

    ' Cache: ItemCode -> U_Grupo
    Private ReadOnly _grupoCache As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    ' (Opcional) Cache: ItemCode -> ItemType (I/S/L)
    Private ReadOnly _itemTypeCache As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    Public Sub New(http As HttpClient, slBase As String)
        _http = http
        _slBase = If(slBase.EndsWith("/"), slBase, slBase & "/")
    End Sub

    Private Shared Function EscapeODataKey(value As String) As String
        ' OData key string: se escapa comilla simple duplicándola
        Return value.Replace("'", "''")
    End Function

    Public Async Function GetUGrupoAsync(itemCode As String) As Threading.Tasks.Task(Of String)
        If String.IsNullOrWhiteSpace(itemCode) Then Return Nothing

        Dim key = itemCode.Trim()
        If _grupoCache.TryGetValue(key, Nothing) Then
            Return _grupoCache(key)
        End If

        ' Importante: Items('ITEMCODE') necesita comillas y escape de '
        Dim url = $"{_slBase}Items('{EscapeODataKey(key)}')?$select=ItemCode,U_Grupo"

        Dim resp = Await _http.GetAsync(url)
        If Not resp.IsSuccessStatusCode Then
            ' No rompas el proceso: cachea Nothing para evitar reintentos infinitos
            _grupoCache(key) = Nothing
            Return Nothing
        End If

        Dim jsonText = Await resp.Content.ReadAsStringAsync()
        Dim jo = JObject.Parse(jsonText)
        Dim grupo = jo?("U_Grupo")?.ToString()

        _grupoCache(key) = grupo
        Return grupo
    End Function

    Public Async Function GetItemTypeAsync(itemCode As String) As Threading.Tasks.Task(Of String)
        If String.IsNullOrWhiteSpace(itemCode) Then Return Nothing

        Dim key = itemCode.Trim()
        If _itemTypeCache.TryGetValue(key, Nothing) Then
            Return _itemTypeCache(key)
        End If

        Dim url = $"{_slBase}Items('{EscapeODataKey(key)}')?$select=ItemCode,ItemType"

        Dim resp = Await _http.GetAsync(url)
        If Not resp.IsSuccessStatusCode Then
            _itemTypeCache(key) = Nothing
            Return Nothing
        End If

        Dim jsonText = Await resp.Content.ReadAsStringAsync()
        Dim jo = JObject.Parse(jsonText)
        Dim itemType = jo?("ItemType")?.ToString() ' "I" / "S" / "L"

        _itemTypeCache(key) = itemType
        Return itemType
    End Function
End Class
