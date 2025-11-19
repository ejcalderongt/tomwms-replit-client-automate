Imports System.IO
Imports Newtonsoft.Json

Public Class ConfigManager

    Private Shared ReadOnly ConfigPath As String =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                     "MiEmpresa", "MiApp", "smtpconfig.json")

    Public Shared Function Cargar() As Smtp_Configuracion
        If Not File.Exists(ConfigPath) Then
            Return New Smtp_Configuracion With {
                .Servidor = "smtp.servidor.com",
                .Puerto = 587,
                .Usuario = "",
                .Password = "",
                .UsarSsl = True,
                .RemitentePorDefecto = "no-reply@miempresa.com"
            }
        End If

        Dim json As String = File.ReadAllText(ConfigPath)
        Return JsonConvert.DeserializeObject(Of Smtp_Configuracion)(json)
    End Function

    Public Shared Sub Guardar(config As Smtp_Configuracion)
        Dim dir = Path.GetDirectoryName(ConfigPath)
        If Not Directory.Exists(dir) Then
            Directory.CreateDirectory(dir)
        End If

        Dim json As String = JsonConvert.SerializeObject(config, Formatting.Indented)
        File.WriteAllText(ConfigPath, json)
    End Sub

    Public Shared Sub RestablecerAValoresDefault()
        Dim cfgDefault = CrearConfigDefault()
        Guardar(cfgDefault)
    End Sub

    ' <<< NUEVO: fábrica de configuración por defecto >>>
    Public Shared Function CrearConfigDefault() As Smtp_Configuracion
        Return New Smtp_Configuracion With {
            .Servidor = "smtp.servidor.com",
            .Puerto = 587,
            .Usuario = "",
            .Password = "",
            .UsarSsl = True,
            .RemitentePorDefecto = "no-reply@miempresa.com"
        }
    End Function

End Class
