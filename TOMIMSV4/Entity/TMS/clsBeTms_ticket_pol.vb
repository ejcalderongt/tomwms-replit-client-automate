Public Class clsBeTms_ticket_pol
    Implements ICloneable

    Public Property IdTicket() As Integer = 0
    Public Property IdOrdenTmsEnc() As Integer = 0
    Public Property NoPoliza() As String = ""
    Public Property Dua() As String = ""
    Public Property Fecha_poliza() As Date = Date.Now
    Public Property Pais_procede() As String = ""
    Public Property Tipo_cambio() As Double = 0.0
    Public Property Total_valoraduana() As Double = 0.0
    Public Property Total_bultos_peso() As Double = 0.0
    Public Property Total_usd() As Double = 0.0
    Public Property Total_flete() As Double = 0.0
    Public Property Total_seguro() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property IdRegimen() As Integer = 0
    Public Property Clave_aduana() As String = ""
    Public Property Nit_imp_exp() As String = ""
    Public Property Clase() As String = ""
    Public Property Mod_transporte() As String = ""
    Public Property Total_liquidar() As Double = 0.0
    Public Property Total_general() As Double = 0.0
    Public Property Codigo_Barra As String = ""



    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
