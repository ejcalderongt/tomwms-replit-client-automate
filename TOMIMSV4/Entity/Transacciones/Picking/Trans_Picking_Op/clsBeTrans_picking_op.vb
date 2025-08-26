Public Class clsBeTrans_picking_op
    Implements ICloneable

    Public Property IdOperadorPicking() As Integer = 0
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdOperadorBodega() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now

    Sub New()
    End Sub

    Sub New(ByRef IdOperadorPicking As Integer, ByVal IdPickingEnc As Integer, ByVal IdOperadorBodega As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        Me.IdOperadorPicking = IdOperadorPicking
        Me.IdPickingEnc = IdPickingEnc
        Me.IdOperadorBodega = IdOperadorBodega
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
