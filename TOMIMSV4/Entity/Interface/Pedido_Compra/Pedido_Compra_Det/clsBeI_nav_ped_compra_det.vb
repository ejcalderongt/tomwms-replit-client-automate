Public Class clsBeI_nav_ped_compra_det
    Implements ICloneable
    Public Property NoEnc() As String = ""
    Public Property Line_No() As Integer = 0
    Public Property Variant_Code() As String = ""
    Public Property No() As String = ""
    Public Property Type() As New Object
    Public Property Description() As String = ""
    Public Property Description2() As String = ""
    Public Property Location_Code() As String = ""
    Public Property Quantity() As Decimal = 0.0
    Public Property Unit_of_Measure_Code() As String = ""
    Public Property Direct_Unit_Cost() As Decimal = 0.0
    Public Property Line_Amount() As Decimal = 0.0
    Public Property Quantity_Received() As Decimal = 0.0
    Public Property Planed_Receipt_Date() As Date = Now.Date
    Public Property Barcode() As String = ""
    Public Property Size() As String = ""
    Public Property Color() As String = ""
    Sub New()
    End Sub

    Sub New(ByRef NoEnc As String, ByVal No As String, ByVal Type As String, ByVal Description As String, ByVal Description2 As String, ByVal Location_Code As String, ByVal Quantity As Decimal, ByVal Unit_Of_Measure_Code As String, ByVal Direct_Unit_Cost As Decimal, ByVal Line_Amount As Decimal, ByVal Quantity_Received As Decimal, ByVal Planed_Receipt_Date As String)
        Me.NoEnc = NoEnc
        Me.No = No
        Me.Type = Type
        Me.Description = Description
        Me.Description2 = Description2
        Me.Location_Code = Location_Code
        Me.Quantity = Quantity
        Me.Unit_of_Measure_Code = Unit_Of_Measure_Code
        Me.Direct_Unit_Cost = Direct_Unit_Cost
        Me.Line_Amount = Line_Amount
        Me.Quantity_Received = Quantity_Received
        Me.Planed_Receipt_Date = Planed_Receipt_Date
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
