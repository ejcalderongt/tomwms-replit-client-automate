Public Class clsBeTrans_oc_imagen
    Implements ICloneable
    Implements IDisposable

    Public Property IdOrdenCompraImg() As Integer = 0
    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property Orden() As Integer = 0
    Public Property Imagen() As Byte()
    Public Property Descripcion() As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdOrdenCompraImg As Integer, ByVal IdOrdenCompraEnc As Integer, ByVal Orden As Integer, ByVal Imagen As Byte(), ByVal descripcion As String)
        Me.IdOrdenCompraImg = IdOrdenCompraImg
        Me.IdOrdenCompraEnc = IdOrdenCompraEnc
        Me.Orden = Orden
        Me.Imagen = Imagen
        Me.Descripcion = descripcion
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
