Public Class clsBeTrans_picking_img
    Implements ICloneable

    Public Property IdImagen() As Integer = 0
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdPickingDet() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPedidoDet() As Integer = 0
    Public Property Imagen() As Byte() = Nothing
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property Observacion() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
