Public Class clsBeCEALSA_clientes
    Implements ICloneable

    Public Property IdCliente() As Integer = 0
    Public Property Codigo_cliente() As String = ""
    Public Property Nombre_cliente() As String = ""
    Public Property Nit() As String = ""
    Public Property Razon_social() As String = ""
    Public Property Procesado_wms() As Boolean = False
    Public Property lAcuerdos As New List(Of clsBeI_nav_acuerdo_enc)

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
