''' <summary>
''' #EJC20260602_SYNC_INGRESO_SAP: Solicitud liviana para que WMS ejecute la interface SAP sin bloquear HH/WS.
''' </summary>
Public Class clsBeI_nav_sync_request
    Implements ICloneable

    Public Enum tEstadoSyncRequest
        Pendiente = 1
        Procesando = 2
        Finalizado = 3
        [Error] = 4
        Ignorado = 5
    End Enum

    Public Const ESTADO_PENDIENTE As String = "PENDIENTE"
    Public Const ESTADO_PROCESANDO As String = "PROCESANDO"
    Public Const ESTADO_FINALIZADO As String = "FINALIZADO"
    Public Const ESTADO_ERROR As String = "ERROR"
    Public Const ESTADO_IGNORADO As String = "IGNORADO"

    Public Const ORIGEN_HH As String = "HH"
    Public Const ORIGEN_BOF As String = "BOF"
    Public Const ORIGEN_WS As String = "WS"
    Public Const ORIGEN_WORKER_WMS As String = "WORKER_WMS"

    ''' <summary>
    ''' Ejemplo: 12345. Identificador identity de i_nav_sync_request.
    ''' </summary>
    Public Property IdSyncRequest As Long = 0

    ''' <summary>
    ''' Ejemplo: 7. Configuracion de interface que contiene bodega, empresa, propietario y ejecutable.
    ''' </summary>
    Public Property IdNavConfigEnc As Integer = 0

    ''' <summary>
    ''' Ejemplo: 1. Empresa WMS asociada a la recepcion.
    ''' </summary>
    Public Property IdEmpresa As Integer = 0

    ''' <summary>
    ''' Ejemplo: 89. Bodega WMS que genero ingresos pendientes para SAP.
    ''' </summary>
    Public Property IdBodega As Integer = 0

    ''' <summary>
    ''' Ejemplo: 101. Usuario que cerro la recepcion o solicito el sync.
    ''' </summary>
    Public Property IdUsuario As Integer = 0

    ''' <summary>
    ''' Ejemplo: 5. Valor de pInterfaceAEjecutar.Enviar_Pedidos_Compra en SAPSYNCCUMBRE.
    ''' </summary>
    Public Property Tipo_Interface As Integer = 0

    ''' <summary>
    ''' Ejemplo: HH, BOF o WS. Punto que solicito la sincronizacion.
    ''' </summary>
    Public Property Origen As String = ORIGEN_WS

    ''' <summary>
    ''' Ejemplo: PENDIENTE, PROCESANDO, FINALIZADO, ERROR o IGNORADO.
    ''' </summary>
    Public Property Estado As String = ESTADO_PENDIENTE

    ''' <summary>
    ''' Ejemplo: {"interface":"SAPSYNCCUMBRE","tipoInterface":5,"idRecepcionEnc":123}. Contexto con el que el worker ejecutara la interface.
    ''' </summary>
    Public Property Parametros As String = ""

    ''' <summary>
    ''' Ejemplo: 2026-06-02 09:25:00. Momento en que WS/BOF encolo la solicitud.
    ''' </summary>
    Public Property Fecha_Solicitud As Date = Date.Now

    ''' <summary>
    ''' Ejemplo: 2026-06-02 09:26:00. Momento en que el worker tomo la solicitud.
    ''' </summary>
    Public Property Fecha_Inicio As Date = New Date(1900, 1, 1)

    ''' <summary>
    ''' Ejemplo: 2026-06-02 09:28:00. Momento en que la interface termino.
    ''' </summary>
    Public Property Fecha_Fin As Date = New Date(1900, 1, 1)

    ''' <summary>
    ''' Ejemplo: 1. Cantidad de intentos de procesamiento del worker WMS.
    ''' </summary>
    Public Property Intento As Integer = 0

    ''' <summary>
    ''' Ejemplo: Sync SAP solicitado, finalizado, sin pendientes o detalle de error controlado.
    ''' </summary>
    Public Property Mensaje As String = ""

    ''' <summary>
    ''' Ejemplo: HH-AND-01 o IIS-TOMHHWS. Host que solicito el sync.
    ''' </summary>
    Public Property Host_Solicita As String = ""

    ''' <summary>
    ''' Ejemplo: WMS-SRV-01. Host WMS que tomo y proceso la solicitud.
    ''' </summary>
    Public Property Host_Procesa As String = ""

    Public Function Clone() As Object Implements ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
