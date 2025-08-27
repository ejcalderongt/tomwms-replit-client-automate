Public Class clsBeResolucion_lp_operador
    Implements ICloneable

    Public Property IdResolucionlp() As Integer = 0
    Public Property IdOperador() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Serie() As String = ""
    Public Property Correlativo_Inicial() As Integer = 0
    Public Property Correlativo_Final() As Integer = 0
    Public Property Correlativo_Actual() As Integer = 0
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
