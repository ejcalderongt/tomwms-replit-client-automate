Public Class clsBeI_nav_ped_compra_enc
    Implements ICloneable
    Public Property No() As String = ""
    Public Property Buy_From_Vendor_No() As String = ""
    Public Property Buy_From_Vendor_Name() As String = ""
    Public Property Posting_Description() As String = ""
    Public Property Posting_Date() As Date? = Date.Now
    Public Property Order_Date() As Date? = Date.Now
    Public Property Document_Date() As Date? = Date.Now
    Public Property Vendor_Invoice_No() As String = ""
    Public Property Status() As Object
    Public Property Payment_Terms_Code() As String = ""
    Public Property Ship_To_Name() As String = ""
    Public Property Location_Code() As String = ""
    Public Property Ship_To_Contact() As String = ""
    Public Property Expected_Receipt_Date() As Date? = Now.Date
    Public Property Is_Internal_Transfer As Boolean = False
    Public Property Product_Owner_Code As String = ""
    Public Property Internal_Transfer_Document_No As String = ""
    Public Property Document_Type As clsDataContractDI.tTipoDocumentoIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso
    Public Property IsImport As Boolean = False
    Public Property Company_Code As String = ""
    Public Property Campaign_No As String = ""
    Public Property Comments As String = ""
    Public Property Series As String = ""
    Public Property User_Document As String = ""
    Sub New()
    End Sub
    Sub New(ByRef No As String, ByVal Buy_From_Vendor_No As String, ByVal Buy_From_Vendor_Name As String, ByVal Posting_Description As String, ByVal Posting_Date As Date, ByVal Order_Date As Date, ByVal Document_Date As Date, ByVal Vendor_Invoice_No As String, ByVal Status As String, ByVal Payment_Terms_Code As String, ByVal Ship_To_Name As String, ByVal Location_Code As String, ByVal Ship_To_Contact As String, ByVal Expected_Receipt_Date As String)
        Me.No = No
        Me.Buy_From_Vendor_No = Buy_From_Vendor_No
        Me.Buy_From_Vendor_Name = Buy_From_Vendor_Name
        Me.Posting_Description = Posting_Description
        Me.Posting_Date = Posting_Date
        Me.Order_Date = Order_Date
        Me.Document_Date = Document_Date
        Me.Vendor_Invoice_No = Vendor_Invoice_No
        Me.Status = Status
        Me.Payment_Terms_Code = Payment_Terms_Code
        Me.Ship_To_Name = Ship_To_Name
        Me.Location_Code = Location_Code
        Me.Ship_To_Contact = Ship_To_Contact
        Me.Expected_Receipt_Date = Expected_Receipt_Date
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class