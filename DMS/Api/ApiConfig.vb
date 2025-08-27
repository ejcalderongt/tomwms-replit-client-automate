Imports System.Configuration
Imports System.IO
Imports System.Xml

Public Class ApiConfig
    Public Shared Function ObtenerApiBaseUrl() As String
        ' Ruta del archivo ApiConfig.config
        Dim rutaConfig As String = Path.Combine(Application.StartupPath, "ApiConfig.config")

        ' Verificar si el archivo existe
        If Not IO.File.Exists(rutaConfig) Then
            Throw New Exception("No se encuentra el archivo de configuración: " & rutaConfig)
        End If

        ' Cargar el archivo XML
        Dim xmlDoc As New XmlDocument()
        xmlDoc.Load(rutaConfig)

        ' Buscar el valor de ApiBaseUrl
        Dim apiBaseUrl As String = xmlDoc.SelectSingleNode("//appSettings/add[@key='ApiBaseUrl']").Attributes("value").Value

        ' Validar si el valor es válido
        If String.IsNullOrEmpty(apiBaseUrl) Then
            Throw New Exception("La URL de la API no está definida en el archivo ApiConfig.config.")
        End If

        Return apiBaseUrl
    End Function
End Class
