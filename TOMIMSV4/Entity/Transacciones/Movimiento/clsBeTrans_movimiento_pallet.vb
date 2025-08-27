Public Class clsBeTrans_movimiento_pallet
    Implements ICloneable

    Public Property Idmovimientopallet() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Lp_origen() As String = ""
    Public Property Lp_destino() As String = ""
    Public Property Orientacion() As String = ""
    Public Property Fecha() As Date = Now
    Public Property Idusuario() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef idmovimientopallet As Integer, ByVal idbodega As Integer, ByVal lp_origen As String, ByVal lp_destino As String, ByVal orientacion As String, ByVal fecha As String, ByVal idusuario As Integer)
        Me.Idmovimientopallet = idmovimientopallet
        Me.Lp_origen = lp_origen
        Me.Orientacion = orientacion
        Me.Idusuario = idusuario
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
