Public Class clsBeI_nav_conversion
    Implements ICloneable

    Public Property IdConversion() As Integer = 0
    Public Property Item_No() As String = ""
    Public Property Code() As String = ""
    Public Property Qty_per_Unit_of_Measure() As Decimal = 0.0
    Public Property Height() As Decimal = 0.0
    Public Property Width() As Decimal = 0.0
    Public Property Length() As Decimal = 0.0
    Public Property Cubage() As Decimal = 0.0
    Public Property Weight() As Decimal = 0.0
    Public Property Package_Type() As String = ""
    Public Property ItemUnitOfMeasure() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
