Imports System.Web.Http
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization
Imports WmsBodegaApi.WebApi.Filters

Public Module WebApiConfig

    Public Sub Register(config As HttpConfiguration)

        ' --- Routing por atributos (preferido) ---
        config.MapHttpAttributeRoutes()

        ' --- Fallback de routing convencional ---
        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        ' --- JSON formatter: camelCase + ignore nulls + dates ISO ---
        Dim jsonFormatter = config.Formatters.JsonFormatter
        jsonFormatter.SerializerSettings.ContractResolver = New CamelCasePropertyNamesContractResolver()
        jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Include
        jsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat
        jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore

        ' --- Quitar XML formatter (solo JSON) ---
        config.Formatters.Remove(config.Formatters.XmlFormatter)

        ' --- Filtro global de excepciones (Forma A: {data, error}) ---
        config.Filters.Add(New JsonExceptionFilter())

    End Sub

End Module
