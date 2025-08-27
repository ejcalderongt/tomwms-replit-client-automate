Public Class clsBeI_nav_proveedor
    Implements ICloneable
    Public Property No() As String = ""
    Public Property Name() As String = ""
    Public Property Adress() As String = ""
    Public Property City() As String = ""
    Public Property Country() As String = ""
    Public Property Phone_No() As String = ""
    Public Property Contact() As String = ""
    Public Property Search_Name() As String = ""
    Public Property VAT_Registratrion_No() As String = ""
    Public Property Location_Code() As String = ""
    Sub New()
    End Sub
    Sub New(ByRef No As String, ByVal Name As String, ByVal Adress As String, ByVal City As String, ByVal Country As String, ByVal Phone_No As String, ByVal Contact As String, ByVal Search_Name As String, ByVal VAT_Registratrion_No As String, ByVal Location_Code As String)
        Me.No = No
        Me.Name = Name
        Me.Adress = Adress
        Me.City = City
        Me.Country = Country
        Me.Phone_No = Phone_No
        Me.Contact = Contact
        Me.Search_Name = Search_Name
        Me.VAT_Registratrion_No = VAT_Registratrion_No
        Me.Location_Code = Location_Code
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
