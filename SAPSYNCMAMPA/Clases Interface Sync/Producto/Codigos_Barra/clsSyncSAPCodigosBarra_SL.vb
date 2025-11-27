Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text.Json
Imports System.Text.Json.Serialization
Imports System.Threading.Tasks

' ===== DTOs para deserializar el Service Layer =====

Public Class CodigoBarrasSlRow
    Public Property Name As String
    Public Property U_Color As String
    Public Property U_Talla As String
    Public Property U_CodigoAnterior As String
    Public Property U_CodigoProv As String
    Public Property U_CodigoAnterior2 As String
    Public Property U_CodigoAnterior3 As String
    Public Property U_ENVIADO_WMS As Integer?
End Class

Public Class SlResponse(Of T)
    Public Property value As List(Of T)

    <JsonPropertyName("@odata.nextLink")>
    Public Property NextLink As String
End Class

Public Class ItemBarcodeDto
    Public Property ItemCode As String
    Public Property CodigoBarra As String
End Class

' ===== Servicio principal =====

Public Class clsSyncSAPCodigosBarra_SL

    Public Shared Async Function GetCodigosBarrasWmsAsync(ByVal baseUrl As String,
                                                          ByVal sessionCookie As String) As Task(Of List(Of ItemBarcodeDto))

        Dim resultado As New List(Of ItemBarcodeDto)()
        Dim vistos As New HashSet(Of String)() ' para Distinct por ItemCode+CodigoBarra

        Using httpClient As New HttpClient()

            httpClient.BaseAddress = New Uri(baseUrl.TrimEnd("/"c) & "/b1s/v1/")
            httpClient.DefaultRequestHeaders.Accept.Clear()
            httpClient.DefaultRequestHeaders.Accept.Add(
                New MediaTypeWithQualityHeaderValue("application/json"))

            ' Cookie de sesión (B1SESSION=...;ROUTEID=...)
            httpClient.DefaultRequestHeaders.Add("Cookie", sessionCookie)

            ' Primer URL: filtramos por U_ENVIADO_WMS eq 2
            Dim requestUrl As String =
                "U_CODIGO_BARRAS?$select=Name,U_Color,U_Talla," &
                "U_CodigoAnterior,U_CodigoProv,U_CodigoAnterior2,U_CodigoAnterior3,U_ENVIADO_WMS" &
                "&$filter=U_ENVIADO_WMS eq 2"

            While Not String.IsNullOrEmpty(requestUrl)

                Dim response As HttpResponseMessage = Await httpClient.GetAsync(requestUrl)
                response.EnsureSuccessStatusCode()

                Dim json As String = Await response.Content.ReadAsStringAsync()

                Dim opcionesJson As New JsonSerializerOptions() With {
                    .PropertyNameCaseInsensitive = True
                }

                Dim slResponse As SlResponse(Of CodigoBarrasSlRow) =
                    JsonSerializer.Deserialize(Of SlResponse(Of CodigoBarrasSlRow))(json, opcionesJson)

                If slResponse IsNot Nothing AndAlso slResponse.value IsNot Nothing Then

                    For Each row In slResponse.value

                        Dim itemCode As String = If(row.Name, String.Empty).Trim()
                        If String.IsNullOrEmpty(itemCode) Then
                            Continue For
                        End If

                        ' 1) Name + U_Color + U_Talla
                        Dim concat As String = (If(row.Name, String.Empty) &
                                                If(row.U_Color, String.Empty) &
                                                If(row.U_Talla, String.Empty)).Trim()
                        AgregarSiValido(resultado, vistos, itemCode, concat)

                        ' 2) U_CodigoAnterior
                        AgregarSiValido(resultado, vistos, itemCode, row.U_CodigoAnterior)

                        ' 3) U_CodigoProv
                        AgregarSiValido(resultado, vistos, itemCode, row.U_CodigoProv)

                        ' 4) U_CodigoAnterior2
                        AgregarSiValido(resultado, vistos, itemCode, row.U_CodigoAnterior2)

                        ' 5) U_CodigoAnterior3
                        AgregarSiValido(resultado, vistos, itemCode, row.U_CodigoAnterior3)

                    Next

                End If

                ' Paginación: si viene @odata.nextLink, la usamos
                If slResponse IsNot Nothing AndAlso
                   Not String.IsNullOrEmpty(slResponse.NextLink) Then

                    ' Normalmente viene como "/b1s/v1/U_CODIGO_BARRAS?...&$skip=20"
                    requestUrl = slResponse.NextLink

                    ' Si viniera con host completo, podrías hacer:
                    ' Dim uriNext = New Uri(slResponse.NextLink)
                    ' requestUrl = uriNext.PathAndQuery
                Else
                    requestUrl = Nothing
                End If

            End While

        End Using

        ' Ordenar por ItemCode antes de devolver
        Return resultado.OrderBy(Function(r) r.ItemCode).ToList()

    End Function

    Private Shared Sub AgregarSiValido(
        lista As List(Of ItemBarcodeDto),
        vistos As HashSet(Of String),
        itemCode As String,
        valor As String
    )
        If String.IsNullOrWhiteSpace(valor) Then
            Return
        End If

        Dim codigo As String = valor.Trim()
        If String.IsNullOrEmpty(codigo) Then
            Return
        End If

        ' Distinct por ItemCode + CodigoBarra
        Dim key As String = itemCode & "|" & codigo
        If Not vistos.Contains(key) Then
            vistos.Add(key)
            lista.Add(New ItemBarcodeDto() With {
                .ItemCode = itemCode,
                .CodigoBarra = codigo
            })
        End If
    End Sub

End Class

