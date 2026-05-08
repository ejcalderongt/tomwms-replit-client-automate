Namespace Dtos

    ''' <summary>
    ''' Wrapper Forma A — convención del WMS para respuestas JSON.
    '''   Éxito : { "data": <payload>, "error": null }
    '''   Error : { "data": null,      "error": { "code": "...", "message": "...", "details": "..." } }
    ''' Compatibilidad con la regla establecida del proyecto (replit.md §4.2).
    ''' </summary>
    Public Class ApiResponse(Of T)
        Public Property Data As T
        Public Property [Error] As ApiError

        Public Shared Function Ok(payload As T) As ApiResponse(Of T)
            Return New ApiResponse(Of T) With {.Data = payload, .[Error] = Nothing}
        End Function

        Public Shared Function Fail(code As String, message As String, Optional details As String = Nothing) As ApiResponse(Of T)
            Return New ApiResponse(Of T) With {
                .Data = Nothing,
                .[Error] = New ApiError With {.Code = code, .Message = message, .Details = details}
            }
        End Function
    End Class

    Public Class ApiError
        Public Property Code As String
        Public Property Message As String
        Public Property Details As String
    End Class

End Namespace
