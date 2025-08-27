Public Class clsBeI_nav_acuerdo_enc
    Implements ICloneable


    Public Property IdAcuerdo As Integer = 0
    Public Property Idcliente As Integer = 0
    Public Property Codigo_acuerdo() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Tipo_cobro() As String = ""
    Public Property Cod_moneda() As Integer = 0
    Public Property Nom_moneda() As String = ""
    Public Property Procesado_wms() As Boolean = False
    Public Property Estado() As Boolean = True


    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

    Public Function IdContrato() As Integer
        Throw New NotImplementedException()
    End Function
End Class
