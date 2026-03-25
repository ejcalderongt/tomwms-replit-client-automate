Public Class clsBENotificacionCola

#Region "Propiedades Auto-Implementadas"

    Public Property IdColaNotificacion As Long = 0
    Public Property IdEvento As Integer = 0
    Public Property IdPlantilla As Integer = 0
    Public Property OrigenSistema As String = String.Empty
    Public Property LlaveNegocio As String = String.Empty
    Public Property EmpresaCodigo As String = String.Empty
    Public Property SucursalCodigo As String = String.Empty
    Public Property BodegaCodigo As String = String.Empty
    Public Property TipoDocumento As String = String.Empty
    Public Property Referencia1 As String = String.Empty
    Public Property Referencia2 As String = String.Empty
    Public Property DataJson As String = String.Empty
    Public Property Estado As String = "PENDIENTE"
    Public Property Intentos As Integer = 0
    Public Property MaxIntentos As Integer = 3
    Public Property FechaProgramada As DateTime = DateTime.Now
    Public Property FechaUltimoIntento As Nullable(Of DateTime) = Nothing
    Public Property FechaProcesado As Nullable(Of DateTime) = Nothing
    Public Property ErrorUltimo As String = String.Empty
    Public Property UsuarioCreacion As String = String.Empty
    Public Property FechaCreacion As DateTime = DateTime.Now

#End Region

#Region "Constructores"

    Public Sub New()
        ' Valores por defecto ya asignados en las propiedades
    End Sub

    Public Sub New(ByVal pIdEvento As Integer,
                   ByVal pOrigenSistema As String,
                   ByVal pDataJson As String,
                   Optional ByVal pEstado As String = "PENDIENTE")
        Me.IdEvento = pIdEvento
        Me.OrigenSistema = pOrigenSistema
        Me.DataJson = pDataJson
        Me.Estado = pEstado
        Me.FechaProgramada = DateTime.Now
        Me.FechaCreacion = DateTime.Now
    End Sub

    Public Sub New(ByVal pIdEvento As Integer,
                   ByVal pIdPlantilla As Integer,
                   ByVal pOrigenSistema As String,
                   ByVal pDataJson As String,
                   Optional ByVal pEstado As String = "PENDIENTE")
        Me.IdEvento = pIdEvento
        Me.IdPlantilla = pIdPlantilla
        Me.OrigenSistema = pOrigenSistema
        Me.DataJson = pDataJson
        Me.Estado = pEstado
        Me.FechaProgramada = DateTime.Now
        Me.FechaCreacion = DateTime.Now
    End Sub

    Public Sub New(ByVal pIdEvento As Integer,
                   ByVal pOrigenSistema As String,
                   ByVal pLlaveNegocio As String,
                   ByVal pDataJson As String,
                   Optional ByVal pEstado As String = "PENDIENTE")
        Me.IdEvento = pIdEvento
        Me.OrigenSistema = pOrigenSistema
        Me.LlaveNegocio = pLlaveNegocio
        Me.DataJson = pDataJson
        Me.Estado = pEstado
        Me.FechaProgramada = DateTime.Now
        Me.FechaCreacion = DateTime.Now
    End Sub

#End Region

#Region "Métodos Públicos"

    ''' <summary>
    ''' Verifica si el registro puede reintentarse
    ''' </summary>
    Public Function PuedeReintentar() As Boolean
        Return Estado = "ERROR" AndAlso Intentos < MaxIntentos
    End Function

    ''' <summary>
    ''' Verifica si el reintento es válido
    ''' </summary>
    Public Function EsReintentoValido() As Boolean
        Return Intentos < MaxIntentos
    End Function

    ''' <summary>
    ''' Registra un nuevo intento de procesamiento
    ''' </summary>
    Public Sub RegistrarIntento()
        Intentos += 1
        FechaUltimoIntento = DateTime.Now
    End Sub

    ''' <summary>
    ''' Marca el registro como procesado exitosamente
    ''' </summary>
    Public Sub MarcarComoProcesado()
        Estado = "PROCESADO"
        FechaProcesado = DateTime.Now
        ErrorUltimo = String.Empty
    End Sub

    ''' <summary>
    ''' Marca el registro como error
    ''' </summary>
    Public Sub MarcarComoError(ByVal pError As String)
        Estado = "ERROR"
        ErrorUltimo = pError
        FechaUltimoIntento = DateTime.Now
    End Sub

    ''' <summary>
    ''' Marca el registro como finalizado (máximo de intentos alcanzado)
    ''' </summary>
    Public Sub MarcarComoFinalizado()
        Estado = "FINALIZADO"
        FechaProcesado = DateTime.Now
    End Sub

    ''' <summary>
    ''' Marca el registro como pendiente para reintentar
    ''' </summary>
    Public Sub MarcarComoPendiente()
        Estado = "PENDIENTE"
        FechaProcesado = Nothing
        ErrorUltimo = String.Empty
    End Sub

    ''' <summary>
    ''' Obtiene una clave única para identificar el registro
    ''' </summary>
    Public Function ObtenerClaveUnica() As String
        Return String.Format("{0}|{1}|{2}|{3}|{4}",
                            OrigenSistema,
                            EmpresaCodigo,
                            SucursalCodigo,
                            TipoDocumento,
                            LlaveNegocio)
    End Function

    ''' <summary>
    ''' Verifica si el registro está pendiente de procesamiento
    ''' </summary>
    Public Function EstaPendiente() As Boolean
        Return Estado = "PENDIENTE" AndAlso FechaProgramada <= DateTime.Now
    End Function

    ''' <summary>
    ''' Verifica si el registro ya fue procesado exitosamente
    ''' </summary>
    Public Function EstaProcesado() As Boolean
        Return Estado = "PROCESADO"
    End Function

    ''' <summary>
    ''' Verifica si el registro está en error
    ''' </summary>
    Public Function EstaEnError() As Boolean
        Return Estado = "ERROR"
    End Function

    ''' <summary>
    ''' Verifica si el registro está finalizado (máximo intentos)
    ''' </summary>
    Public Function EstaFinalizado() As Boolean
        Return Estado = "FINALIZADO"
    End Function

    ''' <summary>
    ''' Reinicia el contador de intentos
    ''' </summary>
    Public Sub ReiniciarIntentos()
        Intentos = 0
        ErrorUltimo = String.Empty
        FechaUltimoIntento = Nothing
    End Sub

#End Region

End Class