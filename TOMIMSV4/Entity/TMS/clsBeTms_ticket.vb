Public Class clsBeTms_ticket
    Implements ICloneable

    Public Property IdTicket() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property IdUbicacionDestino() As Integer = 0
    Public Property IdPiloto() As Integer = 0
    Public Property IdVehiculo() As Integer = 0
    Public Property IdEmpresaTransporte() As Integer = 0
    Public Property Tipo_Operacion() As String = ""
    Public Property Fecha_Ingreso() As Date = New Date(1900, 1, 1)
    Public Property Fecha_Salida() As Date = New Date(1900, 1, 1)
    Public Property Fecha_Finalizado() As Date = New Date(1900, 1, 1)
    Public Property Estado() As String = ""
    Public Property No_Poliza() As String = ""
    Public Property No_Placa As String = ""
    Public Property No_Documento_Piloto As String = ""
    Public Property Tipo_Documento_Piloto As String = ""
    Public Property Nombres_Piloto As String = ""
    Public Property Apellidos_Piloto As String = ""
    Public Property No_TC As String = ""
    '#EJC20210222
    Public Property ObjPoliza As New clsBeTms_ticket_pol

    '#EJC20210520: 2:35 AM. bien jodido, estoy empezando a alucinar jaja.
    ''' <summary>
    ''' Determina si stock jornada, ya analizó este ticket.
    ''' </summary>
    ''' <returns></returns>
    Public Property Procesado_Stock_Jornada As Boolean = False
    Public Property Fecha_Procesado_Stock_Jornada As Date = New Date(1900, 1, 1)

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
