Public Class clsBeD_PEDIDOD
    Implements ICloneable

    Public Property COREL() As String = ""
    Public Property PRODUCTO() As String = ""
    Public Property EMPRESA() As String = ""
    Public Property ANULADO() As String = ""
    Public Property CANT() As Double = 0.0
    Public Property PRECIO() As Double = 0.0
    Public Property IMP() As Double = 0.0
    Public Property DES() As Double = 0.0
    Public Property DESMON() As Double = 0.0
    Public Property TOTAL() As Double = 0.0
    Public Property PRECIODOC() As Double = 0.0
    Public Property PESO() As Double = 0.0
    Public Property VAL1() As Double = 0.0
    Public Property VAL2() As String = ""
    Public Property CANTPROC() As Double = 0.0
    Public Property UMVENTA() As String = ""
    Public Property FACTOR() As Double = 0.0
    Public Property UMSTOCK() As String = ""
    Public Property UMPESO() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
