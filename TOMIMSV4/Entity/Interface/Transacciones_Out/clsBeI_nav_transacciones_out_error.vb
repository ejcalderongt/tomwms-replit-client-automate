Public Class clsBeI_nav_transacciones_out_error
    Implements ICloneable

    ''' <summary>
    ''' Este es un Identity controlado internamente por el WMS, puede llenarse en 0.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdMensaje() As Integer = 0
    ''' <summary>
    ''' Identificador del Idtransacción enviada por el WMS hacia el ERP
    ''' </summary>
    ''' <returns></returns>
    Public Property IdTransaccionWMS() As Integer = 0
    ''' <summary>
    ''' Indica el número asociado con el tipo de transacción
    ''' 1. recepción|Ingreso.
    ''' 2. Despacho|Pedido de Cliente| Orden de Venta.
    ''' 3. Ajuste Positivo.
    ''' 4. Ajuste Negativo.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdTipoTransaccionWMS() As String = ""
    ''' <summary>
    ''' código de transacción asociada a ERP si aplica.
    ''' </summary>
    ''' <returns></returns>
    Public Property ReferenciaERP() As String = ""
    ''' <summary>
    ''' transacción que se está generado del lado del ERP.
    ''' Ej: Tralsado|recepción|Ingreso|Despacho
    ''' </summary>
    ''' <returns></returns>
    Public Property TransaccionERP() As String = ""
    ''' <summary>
    ''' Mensaje a adicionar, referente a la transacción.
    ''' </summary>
    ''' <returns></returns>
    Public Property Mensaje() As String = ""
    ''' <summary>
    ''' Marcar true si el mensaje representa une error en la transacción.
    ''' </summary>
    ''' <returns></returns>
    Public Property EsError() As Boolean = False
    ''' <summary>
    ''' número asociado al error, capturado o devuelto por la excepcion.
    ''' </summary>
    ''' <returns></returns>
    Public Property NumeroError() As String = 0
    ''' <summary>
    ''' Observación que el programador desee agregar.
    ''' </summary>
    ''' <returns></returns>
    Public Property Observacion() As String = ""

    Public Property Fecha As Date = Now

    Public Property UsuarioERP As String = ""

    Public Property UsuarioWMS As String = "MI3"

    ''' <summary>
    ''' Asocia el directorio de la interfaz web al mensaje de error.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDirectorio As Integer = 0


    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
