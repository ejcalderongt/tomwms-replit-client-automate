Public Class clsBeCEALSA_acuerdoscomerciales
    Implements ICloneable

    Public Property Emp() As Integer = 0
    Public Property Nombre_emp() As String = ""
    Public Property Codcliente() As Integer = 0
    Public Property Nomcliente() As String = ""
    Public Property Codacuerdo() As Integer = 0
    Public Property Descrip() As String = ""
    Public Property Tipocobro() As String = ""
    Public Property Codmoneda() As Integer = 0
    Public Property Moneda() As String = ""
    Public Property Procesado_wms() As String = ""
    Public Property Estado() As String = ""


    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
