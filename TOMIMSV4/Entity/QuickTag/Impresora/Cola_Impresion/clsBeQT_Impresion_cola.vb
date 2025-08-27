Public Class clsBeQT_Impresion_cola
    Implements ICloneable

    Public Property IdColaImpresion() As Integer = 0
    Public Property Impresora() As String = ""
    Public Property Tipoconexion() As String = ""
    Public Property Detalleconexion() As String = ""
    Public Property DataImpresion() As String = ""
    Public Property Estado() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property TipoImpresion As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
