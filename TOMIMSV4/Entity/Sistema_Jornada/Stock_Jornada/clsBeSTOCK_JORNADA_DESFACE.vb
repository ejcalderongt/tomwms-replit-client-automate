Public Class clsBeStock_Jornada_Desface
    Implements ICloneable

    Public Property CONSECUTIVO() As Integer = 0
    Public Property LIC_PLATE() As String = ""
    Public Property FECHA() As Date = New Date(1900, 1, 1)
    Public Property IDSTOCK() As Integer = 0
    Public Property IDJORNADASISTEMA() As Integer = 0
    Public Property IDBODEGA() As Integer = 0
    Public Property IDTICKETTMS() As Integer = 0
    Public Property FECHA_CONSECUTIVA() As Date = New Date(1900, 1, 1)
    Public Property MIN_FECHA() As Date = New Date(1900, 1, 1)
    Public Property MAX_FECHA() As Date = New Date(1900, 1, 1)

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
