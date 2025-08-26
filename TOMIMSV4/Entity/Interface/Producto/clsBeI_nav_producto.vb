Public Class clsBeI_nav_producto
    Implements ICloneable
    Public Property No() As String = ""
    Public Property Description() As String = ""
    Public Property Description_2() As String = ""
    Public Property Inventory() As Double? = 0.0
    Public Property Base_Unit_Of_Measure() As String = ""
    Public Property Unit_Cost() As Double? = 0.0
    Public Property Inventory_Posting_Group() As String = ""
    Public Property Inventory_Posting_Name As String = ""
    Public Property Gen_Prod_Posting_Group() As String = ""
    Public Property Gen_Prod_Posting_Name As String = ""
    Public Property Search_Description() As String = ""
    Public Property Item_Category_Code() As String = ""
    Public Property Item_Category_Name As String = ""
    Public Property Product_Group_Code() As String = ""
    Public Property Producto_Group_Name As String = ""
    Public Property Sales_Unit() As String = ""
    Public Property Item_Tracking_Code() As String = ""
    Public Property lINavConversion As List(Of clsBeI_nav_conversion)
    Public Property Manufacturing_Process As String = ""
    Public Property BatchControl As Boolean = False
    Public Property Product_Class_Code() As String = ""
    Public Property Product_Class_Name() As String = ""
    Sub New()
    End Sub
    Sub New(ByRef No As String, ByVal Description As String, ByVal Description_2 As String, ByVal Inventory As Double, ByVal Base_Unit_Of_Measure As String, ByVal Unit_Cost As Double, ByVal Inventory_Posting_Group As String, ByVal Gen_Prod_Posting_Group As String, ByVal Search_Description As String, ByVal Item_Category_Code As String, ByVal Product_Group_Code As String, ByVal Sales_Unit As String, ByVal Item_Tracking_Code As String)
        Me.No = No
        Me.Description = Description
        Me.Description_2 = Description_2
        Me.Inventory = Inventory
        Me.Base_Unit_Of_Measure = Base_Unit_Of_Measure
        Me.Unit_Cost = Unit_Cost
        Me.Inventory_Posting_Group = Inventory_Posting_Group
        Me.Gen_Prod_Posting_Group = Gen_Prod_Posting_Group
        Me.Search_Description = Search_Description
        Me.Item_Category_Code = Item_Category_Code
        Me.Product_Group_Code = Product_Group_Code
        Me.Sales_Unit = Sales_Unit
        Me.Item_Tracking_Code = Item_Tracking_Code
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class