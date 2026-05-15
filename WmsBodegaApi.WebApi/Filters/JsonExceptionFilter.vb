Imports System.Net
Imports System.Net.Http
Imports System.Web.Http.Filters
Imports WmsBodegaApi.WebApi.Dtos

Namespace Filters

    ''' <summary>
    ''' Filtro global de excepciones. Convierte cualquier excepción no controlada
    ''' en una respuesta Forma A consistente:
    '''   { "data": null, "error": { "code": "...", "message": "...", "details": "..." } }
    ''' Status HTTP: 500 por defecto, 401 si es HttpResponseException Unauthorized.
    ''' </summary>
    Public Class JsonExceptionFilter
        Inherits ExceptionFilterAttribute

        Public Overrides Sub OnException(ctx As HttpActionExecutedContext)

            ' Si ya se lanzó un HttpResponseException con respuesta lista, dejarlo pasar.
            Dim httpEx As HttpResponseException = TryCast(ctx.Exception, HttpResponseException)
            If httpEx IsNot Nothing Then
                ctx.Response = httpEx.Response
                Return
            End If

            Dim ex As Exception = ctx.Exception
            Dim payload = ApiResponse(Of Object).Fail(
                code:=ex.GetType().Name,
                message:=ex.Message,
                details:=If(ex.InnerException IsNot Nothing, ex.InnerException.ToString(), Nothing))

            ctx.Response = ctx.Request.CreateResponse(
                HttpStatusCode.InternalServerError, payload)

        End Sub

    End Class

End Namespace
