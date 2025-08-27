Public Class clsBeI_nav_ped_traslado_enc
    Implements ICloneable
    Public Property No() As String = ""
    Public Property Posting_Date() As Date? = Date.Now
    Public Property Receipt_Date() As Date? = Date.Now
    Public Property Shipment_Date() As Date? = Date.Now
    Public Property Status() As Integer
    Public Property Transfer_from_Code() As String = ""
    Public Property Transfer_from_Contact() As String = ""
    Public Property Transfer_from_Name() As String = ""
    Public Property Transfer_to_Code() As String = ""
    Public Property Transfer_to_Contact() As String = ""
    Public Property Transfer_to_Name() As String = ""
    Public Property Transfer_to_CodeField() As String = ""
    Public Property Product_Owner_Code As String = ""
    Public Property Is_Internal_Transfer As Boolean = False
    Public Property Receipt_Document_Reference As String = ""
    Public Property Document_Type As clsDataContractDI.tTipoDocumentoSalida = 0
    Public Property External_Document_No As String = ""
    Public Property RoadCodigoRuta As String = ""
    Public Property RoadCodigoVendedor As String = ""
    Public Property Manufacturing_Process As clsDataContractDI.Manufacturing_Process = 0
    Public Property Address As String = ""
    Public Property Comments As String = ""
    Public Property Company_Code As String = ""
    Public Property IsExport As Boolean = False
    Sub New()
    End Sub
    Sub New(ByRef No As String, ByVal Posting_Date As Date, ByVal Receipt_Date As Date, ByVal Shipment_Date As Date, ByVal Status As Boolean, ByVal Transfer_from_Code As String, ByVal Transfer_from_Contact As String, ByVal Transfer_from_Name As String, ByVal Transfer_to_Code As String, ByVal Transfer_to_Contact As String, ByVal Transfer_to_Name As String, ByVal transfer_to_CodeField As String)
        Me.No = No
        Me.Posting_Date = Posting_Date
        Me.Receipt_Date = Receipt_Date
        Me.Shipment_Date = Shipment_Date
        Me.Status = Status
        Me.Transfer_from_Code = Transfer_from_Code
        Me.Transfer_from_Contact = Transfer_from_Contact
        Me.Transfer_from_Name = Transfer_from_Name
        Me.Transfer_to_Code = Transfer_to_Code
        Me.Transfer_to_Contact = Transfer_to_Contact
        Me.Transfer_to_Name = Transfer_to_Name
        Me.Transfer_to_CodeField = transfer_to_CodeField
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class