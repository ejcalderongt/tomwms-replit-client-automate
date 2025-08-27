Public Class clsSBO_Stock_Item

    Public Property ITEMCODE As String
    Public Property ITEMNAME As String
    Public Property WHSCODE As String
    Public Property ONHAND As Decimal
    Public Property ISCOMMITED As Decimal
    Public Property ONORDER As Decimal
    Public Property MINSTOCK As Decimal
    Public Property MAXSTOCK As Decimal

    Public Sub New()
        ITEMCODE = ""
        ITEMNAME = ""
        WHSCODE = ""
        ONHAND = 0
        ISCOMMITED = 0
        ONORDER = 0
        MINSTOCK = 0
        MAXSTOCK = 0
    End Sub

    Public Sub New(
            ByVal ITEMCODE As String,
            ByVal ITEMNAME As String,
            ByVal WHSCODE As String,
            ByVal ONHAND As Decimal,
            ByVal ISCOMMITED As Decimal,
            ByVal ONORDER As Decimal,
            ByVal MINSTOCK As Decimal,
            ByVal MAXSTOCK As Decimal)

        Me.ITEMCODE = ITEMCODE
        Me.ITEMNAME = ITEMNAME
        Me.WHSCODE = WHSCODE
        Me.ONHAND = ONHAND
        Me.ISCOMMITED = ISCOMMITED
        Me.ONORDER = ONORDER
        Me.MINSTOCK = MINSTOCK
        Me.MAXSTOCK = MAXSTOCK


    End Sub

End Class

