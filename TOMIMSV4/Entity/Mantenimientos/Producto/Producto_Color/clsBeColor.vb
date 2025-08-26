Public Class clsBeColor
    Implements ICloneable

    Public Property IdColor() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property CodigoHex() As String = ""
    Public Property IdPropietario() As Integer = 0
    Public Property Fec_agr() As Date = Now
    Public Property User_agr() As String = ""
    Public Property Fec_mod() As Date = Now
    Public Property User_mod() As String = ""

    Public Property Activo() As Boolean = False
    Public Property IsNew() As Boolean = False
    Public Property Codigo() As String = ""
    Public Property Propietario As New clsBePropietarios


    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
