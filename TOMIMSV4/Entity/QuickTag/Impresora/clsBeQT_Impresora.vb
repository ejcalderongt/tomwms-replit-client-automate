Public Class clsBeQT_Impresora
    Implements ICloneable

    Public Property IdImpresora() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Predeterminada() As Boolean = False
    Public Property Activo() As Boolean = False
    Public Property IP As String = ""
    Public Property Conexion As String = ""
    Public Property IsNew As Boolean = False
    Public Property user_agr As String = ""
    Public Property fec_agr As DateTime = Now
    Public Property user_mod As String = ""
    Public Property fec_mod As DateTime = Now
    Public Property Formato_Impresion As String = ""
    Public Property Velocidad_Impresion As Integer = 0
    Public Property Reintentos_Impresion As Integer = 0
    Public Property Delay_Impresion As Integer = 0


    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
