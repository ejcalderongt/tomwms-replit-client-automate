Imports System.Configuration
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Namespace TOMWMS

    ''' <summary>
    ''' PROXY DE TRANSICIÓN — POC Remote DAL.
    '''
    ''' Reemplazo drop-in de TOMWMS.clsLnBodega para los 8 métodos que frmBodega.vb usa.
    ''' Toggle por App.config:
    '''
    '''   <appSettings>
    '''     <add key="BodegaDataMode"  value="Local"  />   <!-- "Local" o "Remote" -->
    '''     <add key="WebApiBaseUrl"   value="http://localhost:59800/" />
    '''     <add key="WebApiKey"       value="poc-bodega-key-cambiar" />
    '''   </appSettings>
    '''
    ''' En frmBodega.vb hacé Find &amp; Replace:
    '''   "clsLnBodega."   →   "clsLnBodegaProxy."
    ''' (8 ocurrencias en líneas 290, 942, 1383, 1469, 1568, 1815, 2176, 2257, 4621, 4891, 5115, 5141)
    '''
    ''' Si BodegaDataMode = "Local" (default), el proxy delega 1:1 al clsLnBodega original
    ''' → comportamiento idéntico a hoy, riesgo cero.
    '''
    ''' Si BodegaDataMode = "Remote", el proxy llama por HTTP a la WebAPI.
    ''' </summary>
    Public Module clsLnBodegaProxy

        ' =========================================================================
        ' Cliente HTTP único (reusable, thread-safe)
        ' =========================================================================
        Private ReadOnly _http As HttpClient = BuildHttpClient()
        Private ReadOnly _jsonSettings As New JsonSerializerSettings With {
            .ContractResolver = New CamelCasePropertyNamesContractResolver(),
            .NullValueHandling = NullValueHandling.Include,
            .DateFormatHandling = DateFormatHandling.IsoDateFormat,
            .ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }

        Private Function BuildHttpClient() As HttpClient
            Dim baseUrl As String = ConfigurationManager.AppSettings("WebApiBaseUrl")
            Dim apiKey As String = ConfigurationManager.AppSettings("WebApiKey")
            Dim cli As New HttpClient()
            If Not String.IsNullOrEmpty(baseUrl) Then cli.BaseAddress = New Uri(baseUrl)
            cli.Timeout = TimeSpan.FromSeconds(30)
            If Not String.IsNullOrEmpty(apiKey) Then
                cli.DefaultRequestHeaders.Add("X-API-Key", apiKey)
            End If
            cli.DefaultRequestHeaders.Accept.Add(
                New Headers.MediaTypeWithQualityHeaderValue("application/json"))
            Return cli
        End Function

        Private ReadOnly Property IsRemote As Boolean
            Get
                Dim mode As String = ConfigurationManager.AppSettings("BodegaDataMode")
                Return String.Equals(mode, "Remote", StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        ' Wrapper Forma A — espejo del que usa la WebAPI
        Private Class ApiResponseDto(Of T)
            Public Property Data As T
            Public Property [Error] As ApiErrorDto
        End Class
        Private Class ApiErrorDto
            Public Property Code As String
            Public Property Message As String
            Public Property Details As String
        End Class

        Private Function GetJson(Of T)(relativeUrl As String) As T
            Dim resp = _http.GetAsync(relativeUrl).Result
            Dim body = resp.Content.ReadAsStringAsync().Result
            Dim wrap = JsonConvert.DeserializeObject(Of ApiResponseDto(Of T))(body, _jsonSettings)
            If wrap.Error IsNot Nothing Then
                Throw New ApplicationException(
                    String.Format("WebAPI {0}: {1} — {2}",
                                  wrap.Error.Code, wrap.Error.Message, wrap.Error.Details))
            End If
            Return wrap.Data
        End Function

        Private Function PostJson(Of TResp)(relativeUrl As String, payload As Object) As TResp
            Dim json As String = If(payload Is Nothing, "{}",
                                     JsonConvert.SerializeObject(payload, _jsonSettings))
            Dim content As New StringContent(json, Encoding.UTF8, "application/json")
            Dim resp = _http.PostAsync(relativeUrl, content).Result
            Dim body = resp.Content.ReadAsStringAsync().Result
            Dim wrap = JsonConvert.DeserializeObject(Of ApiResponseDto(Of TResp))(body, _jsonSettings)
            If wrap.Error IsNot Nothing Then
                Throw New ApplicationException(
                    String.Format("WebAPI {0}: {1} — {2}",
                                  wrap.Error.Code, wrap.Error.Message, wrap.Error.Details))
            End If
            Return wrap.Data
        End Function

        Private Function PutJson(Of TResp)(relativeUrl As String, payload As Object) As TResp
            Dim json As String = JsonConvert.SerializeObject(payload, _jsonSettings)
            Dim content As New StringContent(json, Encoding.UTF8, "application/json")
            Dim resp = _http.PutAsync(relativeUrl, content).Result
            Dim body = resp.Content.ReadAsStringAsync().Result
            Dim wrap = JsonConvert.DeserializeObject(Of ApiResponseDto(Of TResp))(body, _jsonSettings)
            If wrap.Error IsNot Nothing Then
                Throw New ApplicationException(
                    String.Format("WebAPI {0}: {1} — {2}",
                                  wrap.Error.Code, wrap.Error.Message, wrap.Error.Details))
            End If
            Return wrap.Data
        End Function

        ' =========================================================================
        ' 8 métodos espejo de clsLnBodega — Toggle Local vs Remote
        ' =========================================================================

        ' ---- 1) MaxID ---- (frm:290, 1383)  ★ ARRANCAR EL TEST POR ACÁ (read-only, simple)
        Public Function MaxID() As Integer
            If IsRemote Then
                Return GetJson(Of Integer)("api/bodega/maxid")
            Else
                Return clsLnBodega.MaxID()
            End If
        End Function

        ' ---- 2) Exists_By_IdEmpresa ---- (frm:1469)  ★ Segundo test (read-only)
        Public Function Exists_By_IdEmpresa(pIdEmpresa As Integer) As Boolean
            If IsRemote Then
                Return GetJson(Of Boolean)(String.Format("api/bodega/exists/{0}", pIdEmpresa))
            Else
                Return clsLnBodega.Exists_By_IdEmpresa(pIdEmpresa)
            End If
        End Function

        ' ---- 3) Obtener ---- (frm:942)  ★ Tercer test (read-only, parsea entidad completa)
        Public Function Obtener(ByRef oBeBodega As clsBeBodega) As Boolean
            If IsRemote Then
                Try
                    ' La WebAPI devuelve BodegaDto. Lo deserializamos sobre la misma instancia
                    ' usando PopulateObject — así no necesitamos el tipo BodegaDto en el cliente.
                    Dim url As String = String.Format("api/bodega/{0}", oBeBodega.IdBodega)
                    Dim resp = _http.GetAsync(url).Result
                    Dim body = resp.Content.ReadAsStringAsync().Result
                    Dim doc = JObject_FromJson(body)
                    If doc("error") IsNot Nothing AndAlso doc("error").Type <> Linq.JTokenType.Null Then
                        Return False  ' NOT_FOUND u otro error → no encontrado
                    End If
                    Dim data = doc("data")
                    If data Is Nothing OrElse data.Type = Linq.JTokenType.Null Then Return False
                    JsonConvert.PopulateObject(data.ToString(), oBeBodega, _jsonSettings)
                    Return True
                Catch ex As Exception
                    Throw New ApplicationException(
                        "Error invocando WebAPI Obtener Bodega: " & ex.Message, ex)
                End Try
            Else
                Return clsLnBodega.Obtener(oBeBodega)
            End If
        End Function

        Private Function JObject_FromJson(s As String) As Linq.JObject
            Return Linq.JObject.Parse(s)
        End Function

        ' ---- 4) Insertar ---- (frm:1568)  ☐ Siguiente fase (write — validar campos primero)
        Public Function Insertar(ByRef oBeBodega As clsBeBodega) As Integer
            If IsRemote Then
                ' La WebAPI devuelve { idBodega, rowsAffected }. Tomamos rowsAffected
                ' (que es lo que el form compara con > 0) y propagamos idBodega de vuelta.
                Dim res = PostJson(Of InsertResultClient)("api/bodega", oBeBodega)
                If res IsNot Nothing AndAlso res.IdBodega > 0 Then
                    oBeBodega.IdBodega = res.IdBodega
                End If
                Return If(res IsNot Nothing, res.RowsAffected, 0)
            Else
                Return clsLnBodega.Insertar(oBeBodega)
            End If
        End Function

        Private Class InsertResultClient
            Public Property IdBodega As Integer
            Public Property RowsAffected As Integer
        End Class

        ' ---- 5) Actualizar ---- (frm:1815, 2176, 2257)  ☐ Siguiente fase
        Public Function Actualizar(ByRef oBeBodega As clsBeBodega) As Integer
            If IsRemote Then
                Dim url As String = String.Format("api/bodega/{0}", oBeBodega.IdBodega)
                Dim res = PutJson(Of UpdateResultClient)(url, oBeBodega)
                Return If(res IsNot Nothing, res.RowsAffected, 0)
            Else
                Return clsLnBodega.Actualizar(oBeBodega)
            End If
        End Function

        Private Class UpdateResultClient
            Public Property IdBodega As Integer
            Public Property RowsAffected As Integer
        End Class

        ' ---- 6) Inserta_Ubicaciones_Por_Defecto ---- (frm:4621)
        Public Function Inserta_Ubicaciones_Por_Defecto(IdBodega As Integer, IdUsuario As Integer) As Boolean
            If IsRemote Then
                Return PostJson(Of Boolean)(
                    String.Format("api/bodega/{0}/ubicaciones-defecto/{1}", IdBodega, IdUsuario),
                    Nothing)
            Else
                Return clsLnBodega.Inserta_Ubicaciones_Por_Defecto(IdBodega, IdUsuario)
            End If
        End Function

        ' ---- 7) Habilitar_Reemplazo ---- (frm:5115, 5141)
        Public Function Habilitar_Reemplazo(pIdBodega As Integer, pHabilitar As Boolean) As Boolean
            If IsRemote Then
                Return PostJson(Of Boolean)(
                    String.Format("api/bodega/{0}/habilitar-reemplazo", pIdBodega),
                    New With {.habilitar = pHabilitar})
            Else
                Return clsLnBodega.Habilitar_Reemplazo(pIdBodega, pHabilitar)
            End If
        End Function

        ' ---- 8) Unificar_Bodegas ---- (frm:4891)
        Public Function Unificar_Bodegas(IdBodegaDestino As Integer, IdBodegaOrigen As Integer) As Boolean
            If IsRemote Then
                Return PostJson(Of Boolean)("api/bodega/unificar",
                    New With {
                        .idBodegaDestino = IdBodegaDestino,
                        .idBodegaOrigen = IdBodegaOrigen
                    })
            Else
                ' AJUSTAR: completar segón firma real del LN (varios parámetros más)
                Return clsLnBodega.Unificar_Bodegas(IdBodegaDestino, IdBodegaOrigen)
            End If
        End Function

    End Module

End Namespace
