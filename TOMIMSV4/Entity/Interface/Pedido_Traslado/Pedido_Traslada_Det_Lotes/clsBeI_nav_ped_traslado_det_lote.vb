Public Class clsBeI_nav_ped_traslado_det_lote
    Implements ICloneable

    Public Property NoEnc() As String = ""
    Public Property Line_No() As Integer = 0
    Public Property No() As String = ""
    Public Property Batch_No() As String = ""
    Public Property Serial_No() As String = ""
    Public Property Expiration_Date() As Date = Date.Now
    Public Property Quantity_Base() As Double = 0.0
    Public Property Variant_Code() As String = ""
    Public Property WhsFrom() As String = ""
    Public Property WhsTo() As String = ""
    Public Property Fec_Agr() As Date = Date.Now

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class