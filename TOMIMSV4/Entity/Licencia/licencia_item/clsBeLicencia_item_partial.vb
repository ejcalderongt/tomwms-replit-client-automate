Partial Public Class clsBeLicencia_item

    Public Property CantBackOffice() As Integer = 0
    Public Property CantHandHeld() As Integer = 0
    Public Property Vence() As DateTime = Now.AddDays(-1)
    Public Property Tipo() As eTipoHost
    Public Property Bandera() As eTipoLicencia
    Public Property Activa As Boolean = False

    Enum eEstatusLicencia
        Pendiente_Solicitud = -1
        Activa = 1
        No_Valida = 0
    End Enum

    Enum eTipoHost
        BOF = 1
        HH = 0
    End Enum

    Enum eTipoLicencia
        Server = 1
        Cliente = 0
    End Enum

End Class
