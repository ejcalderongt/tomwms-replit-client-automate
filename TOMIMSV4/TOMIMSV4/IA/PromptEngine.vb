Imports System.Text.RegularExpressions
Imports System.Text.Json

Public Module PromptEngine
    Public Function LoadTemplates(jsonPath As String) As List(Of PromptTemplate)
        Dim text = IO.File.ReadAllText(jsonPath)
        Return JsonSerializer.Deserialize(Of List(Of PromptTemplate))(text, New JsonSerializerOptions With {
            .PropertyNameCaseInsensitive = True
        })
    End Function

    Public Function Render(template As PromptTemplate, vars As Dictionary(Of String, String)) As String
        ' Validación de requeridos
        For Each v In template.variables
            If v.required AndAlso Not vars.ContainsKey(v.name) Then
                If Not String.IsNullOrEmpty(v.defaultValue) Then
                    vars(v.name) = v.defaultValue
                Else
                    Throw New ArgumentException($"Falta variable requerida: {v.name}")
                End If
            End If
        Next

        Dim result = template.content
        Dim rx = New Regex("\{\{([a-zA-Z0-9_\-]+)\}\}", RegexOptions.Compiled)

        result = rx.Replace(result, Function(m)
                                        Dim key = m.Groups(1).Value
                                        If vars.ContainsKey(key) Then
                                            Return vars(key)
                                        Else
                                            ' Si no viene, usa default si existe
                                            Dim def = template.variables?.FirstOrDefault(Function(v) v.name = key)?.defaultValue
                                            Return If(def, "")
                                        End If
                                    End Function)
        Return result
    End Function
End Module
