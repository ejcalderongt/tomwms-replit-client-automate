Public Class clsBeI_nav_ped_compra_det_lote
    Implements ICloneable
    Public Property NoEnc() As String = ""
    Public Property source_ID() As String = ""
    Public Property Source_Prod_Order_Line() As Integer = 0
    Public Property Item_No() As String = ""
    Public Property Lot_No() As String = ""
    Public Property Expiration_Date() As Date = Date.Now
    Public Property Entry_No() As String = ""
    Public Property Source_Type() As Integer = 0
    Public Property Quantity_Base() As Double = 0.0
    Public Property Variant_Code() As String = ""

    Sub New()
    End Sub

    Sub New(ByRef NoEnc As String, ByVal source_IDField As String, ByVal Source_Prod_Order_Line As Integer, ByVal Item_No As Integer, ByVal Lot_No As String, ByVal Expiration_Date As Date, ByVal Entry_No As String, ByVal Source_Type As Integer, ByVal Quantity_Base As Double, ByVal Variant_Code As String)
        Me.NoEnc = NoEnc
        Me.source_ID = Source_IDField
        Me.Source_Prod_Order_Line = Source_Prod_Order_Line
        Me.Item_No = Item_No
        Me.Lot_No = Lot_No
        Me.Expiration_Date = Expiration_Date
        Me.Entry_No = Entry_No
        Me.Source_Type = Source_Type
        Me.Quantity_Base = Quantity_Base
        Me.Variant_Code = Variant_Code
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class