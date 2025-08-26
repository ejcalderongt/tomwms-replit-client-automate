Public Class clsBeI_nav_cliente
    Implements ICloneable

    Public Property IdCliente() As Integer = 0
    Public Property Codigo_cliente() As String = ""
    Public Property Nombre_cliente() As String = ""
    Public Property Nit() As String = ""
    Public Property Razon_social() As String = ""
    Public Property Procesado_wms() As Boolean = False
    Public Property No() As String = ""
    Public Property Name() As String = ""
    Public Property Adress() As String = ""
    Public Property City() As String = ""
    Public Property Country() As String = ""
    Public Property Phone_No() As String = ""
    Public Property ContactName() As String = ""
    Public Property Search_Name() As String = ""
    Public Property VAT_Registratrion_No() As String = ""
    Public Property Location_Code() As String = ""
    Public Property lAcuerdos As New List(Of clsBeI_nav_acuerdo_det)
    Public Property lAcuerdosDetERP As New List(Of clsBeI_nav_acuerdo_det)
    Public Property lAcuerdosEncERP As New List(Of clsBeI_nav_acuerdo_enc)


    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
