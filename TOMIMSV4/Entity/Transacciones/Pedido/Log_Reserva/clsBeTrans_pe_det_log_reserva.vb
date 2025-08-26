Public Class clsBeTrans_pe_det_log_reserva
    Implements ICloneable

    Public Property IdLogReserva() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Fecha() As Date = Date.Now
    Public Property IdPedidoEnc() As Integer = 0
    Public Property Line_No() As Integer = 0
    Public Property Item_No() As String = ""
    Public Property UmBas() As String = ""
    Public Property Variant_Code() As String = ""
    Public Property MensajeLog() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Caso_Reserva() As String = ""
    Public Property EsError() As Boolean = False
    Public Property Referencia_Documento() As String = ""
    Public Property Fecha_Vence() As Date = New Date(1900, 1, 1)
    Public Property IdPedidoDet() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdStockRes() As Integer = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
