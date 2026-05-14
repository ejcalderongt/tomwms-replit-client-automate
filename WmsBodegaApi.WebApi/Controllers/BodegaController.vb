Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports TOMWMS
Imports WmsBodegaApi.WebApi.Dtos

Namespace Controllers

    ''' Reglas:
    '''   - El controller NO contiene lógica de negocio. Solo:
    '''       a) deserializa DTO -> entity,
    '''       b) llama el método Shared de la DAL EXISTENTE (sin tocar la DAL),
    '''       c) serializa entity -> DTO,
    '''       d) envuelve en Forma A {data, error}.
    '''   - Auth POC: header X-API-Key contra appSettings("ApiKey"). Reemplazar en PRD.    
    <RoutePrefix("api/bodega")>
    Public Class BodegaController
        Inherits ApiController

        ' =========================================================================
        ' GET /api/bodega/maxid
        ' Reemplaza:   Dim MaxID As String = clsLnBodega.MaxID.ToString  (frm:290)
        '              pBeBodega.IdBodega = clsLnBodega.MaxID()           (frm:1383)
        ' =========================================================================
        <HttpGet, Route("maxid")>
        Public Function GetMaxId() As ApiResponse(Of Integer)
            VerifyApiKey()
            Dim valor As Integer = clsLnBodega.MaxID()
            Return ApiResponse(Of Integer).Ok(valor)
        End Function

        ' =========================================================================
        ' GET /api/bodega/exists/{idEmpresa}
        ' Reemplaza:   If clsLnBodega.Exists_By_IdEmpresa(cmbEmpresa.EditValue) = False Then  (frm:1469)
        ' =========================================================================
        <HttpGet, Route("exists/{idEmpresa:int}")>
        Public Function ExistsByIdEmpresa(idEmpresa As Integer) As ApiResponse(Of Boolean)
            VerifyApiKey()
            Dim existe As Boolean = clsLnBodega.Exists_By_IdEmpresa(idEmpresa)
            Return ApiResponse(Of Boolean).Ok(existe)
        End Function

        ' =========================================================================
        ' GET /api/bodega/{idBodega}
        ' Reemplaza:   clsLnBodega.Obtener(pBeBodega)  (frm:942)
        ' Nota: la firma original es ByRef y devuelve Boolean (true si encontró).
        ' Acá replicamos el patrón: si no encuentra, devolvemos Forma A con error.
        ' =========================================================================
        <HttpGet, Route("{idBodega:int}")>
        Public Function Obtener(idBodega As Integer) As ApiResponse(Of BodegaDto)
            VerifyApiKey()
            Dim be As New clsBeBodega With {.IdBodega = idBodega}
            Dim encontrado As Boolean = clsLnBodega.Obtener(be)
            If Not encontrado Then
                Return ApiResponse(Of BodegaDto).Fail("NOT_FOUND",
                    String.Format("No existe bodega con IdBodega={0}", idBodega))
            End If
            Return ApiResponse(Of BodegaDto).Ok(BodegaMapper.ToDto(be))
        End Function

        ' =========================================================================
        ' POST /api/bodega          body: BodegaDto
        ' Reemplaza:   Guardar = clsLnBodega.Insertar(pBeBodega) > 0  (frm:1568)
        ' Devuelve: { idBodega, rowsAffected }
        ' =========================================================================
        <HttpPost, Route("")>
        Public Function Insertar(<FromBody> dto As BodegaDto) As ApiResponse(Of InsertarResultDto)
            VerifyApiKey()
            If dto Is Nothing Then
                Return ApiResponse(Of InsertarResultDto).Fail("BAD_REQUEST", "Payload BodegaDto requerido en el body")
            End If
            Dim be As clsBeBodega = BodegaMapper.ToEntity(dto)
            Dim rows As Integer = clsLnBodega.Insertar(be)
            Return ApiResponse(Of InsertarResultDto).Ok(New InsertarResultDto With {
                .IdBodega = be.IdBodega,
                .RowsAffected = rows
            })
        End Function

        ' =========================================================================
        ' PUT /api/bodega/{idBodega}    body: BodegaDto
        ' Reemplaza:   Actualizar = clsLnBodega.Actualizar(pBeBodega) > 0  (frm:1815, 2176, 2257)
        ' =========================================================================
        <HttpPut, Route("{idBodega:int}")>
        Public Function Actualizar(idBodega As Integer, <FromBody> dto As BodegaDto) As ApiResponse(Of ActualizarResultDto)
            VerifyApiKey()
            If dto Is Nothing Then
                Return ApiResponse(Of ActualizarResultDto).Fail("BAD_REQUEST", "Payload BodegaDto requerido")
            End If
            ' Forzamos consistencia: el id de la URL manda sobre el del body
            dto.IdBodega = idBodega
            Dim be As clsBeBodega = BodegaMapper.ToEntity(dto)
            Dim rows As Integer = clsLnBodega.Actualizar(be)
            Return ApiResponse(Of ActualizarResultDto).Ok(New ActualizarResultDto With {
                .IdBodega = idBodega,
                .RowsAffected = rows
            })
        End Function

        ' =========================================================================
        ' POST /api/bodega/{idBodega}/ubicaciones-defecto/{idUsuario}
        ' Reemplaza:   If clsLnBodega.Inserta_Ubicaciones_Por_Defecto(pBeBodega.IdBodega, AP.UsuarioAp.IdUsuario) Then  (frm:4621)
        ' =========================================================================
        <HttpPost, Route("{idBodega:int}/ubicaciones-defecto/{idUsuario:int}")>
        Public Function InsertaUbicacionesPorDefecto(idBodega As Integer, idUsuario As Integer) As ApiResponse(Of Boolean)
            VerifyApiKey()
            Dim ok As Boolean = clsLnBodega.Inserta_Ubicaciones_Por_Defecto(idBodega, idUsuario)
            Return ApiResponse(Of Boolean).Ok(ok)
        End Function

        ' =========================================================================
        ' POST /api/bodega/{idBodega}/habilitar-reemplazo    body: { habilitar: bool }
        ' Reemplaza:   clsLnBodega.Habilitar_Reemplazo(AP.IdBodega, True/False)  (frm:5115/5141)
        ' =========================================================================
        <HttpPost, Route("{idBodega:int}/habilitar-reemplazo")>
        Public Function HabilitarReemplazo(idBodega As Integer, <FromBody> body As HabilitarReemplazoRequest) As ApiResponse(Of Boolean)
            VerifyApiKey()
            If body Is Nothing Then
                Return ApiResponse(Of Boolean).Fail("BAD_REQUEST", "Body { habilitar: bool } requerido")
            End If
            Dim ok As Boolean = clsLnBodega.Habilitar_Reemplazo(idBodega, body.Habilitar)
            Return ApiResponse(Of Boolean).Ok(ok)
        End Function

        ' =========================================================================
        ' POST /api/bodega/unificar    body: UnificarBodegasRequest
        ' Reemplaza:   clsLnBodega.Unificar_Bodegas(pBeBodega.IdBodega, ...)  (frm:4891)
        ' Nota: la firma real tiene varios parámetros (ver clsLnBodega_Partial:2520).
        ' Si tu firma necesita más args, agregalos al request DTO y al call.
        ' =========================================================================
        <HttpPost, Route("unificar")>
        Public Function UnificarBodegas(<FromBody> req As UnificarBodegasRequest) As ApiResponse(Of Boolean)
            VerifyApiKey()
            If req Is Nothing Then
                Return ApiResponse(Of Boolean).Fail("BAD_REQUEST", "Body UnificarBodegasRequest requerido")
            End If
            Dim lBodegas = clsLnBodega.GetAll()
            ' AJUSTAR: completar la lista de parámetros segón la firma real (line 2520 del Partial)
            'Dim ok As Boolean = clsLnBodega.Unificar_Bodegas(req.IdBodegaDestino, lBodegas, req.IdBodegaOrigen)
            'Return ApiResponse(Of Boolean).Ok(ok)
        End Function

        ' ----------------------------------------------------------------- helpers
        Private Sub VerifyApiKey()
            Dim expected As String = ConfigurationManager.AppSettings("ApiKey")
            If String.IsNullOrEmpty(expected) Then Return  ' no configurado = sin auth (POC)
            Dim provided As IEnumerable(Of String) = Nothing
            If Request.Headers.TryGetValues("X-API-Key", provided) AndAlso
               provided IsNot Nothing AndAlso
               provided.Any(Function(v) v = expected) Then
                Return
            End If
            Throw New HttpResponseException(
                Request.CreateResponse(HttpStatusCode.Unauthorized,
                    ApiResponse(Of Object).Fail("UNAUTHORIZED", "Falta o no coincide X-API-Key")))
        End Sub

    End Class

    ' ===== DTOs específicos de request/response =====

    Public Class InsertarResultDto
        Public Property IdBodega As Integer
        Public Property RowsAffected As Integer
    End Class

    Public Class ActualizarResultDto
        Public Property IdBodega As Integer
        Public Property RowsAffected As Integer
    End Class

    Public Class HabilitarReemplazoRequest
        Public Property Habilitar As Boolean
    End Class

    Public Class UnificarBodegasRequest
        Public Property IdBodegaDestino As Integer
        Public Property IdBodegaOrigen As Integer
        ' Agregar acá los demás parámetros segón la firma real de Unificar_Bodegas
    End Class

End Namespace
