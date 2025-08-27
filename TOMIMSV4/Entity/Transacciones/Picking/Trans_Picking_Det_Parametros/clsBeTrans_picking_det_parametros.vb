Public Class clsBeTrans_picking_det_parametros
    Implements ICloneable
    Implements IDisposable

    Public Property IdParametroPicking() As Integer = 0
    Public Property IdPickingDet() As Integer = 0
    Public Property IdProductoParametro() As Integer = 0
    Public Property Valor_texto() As String = ""
    Public Property Valor_numerico() As Nullable(Of Double)
    Public Property Valor_fecha() As Nullable(Of Date)
    Public Property Valor_logico() As Nullable(Of Boolean)
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now

    Sub New()
    End Sub

    Sub New(ByRef IdParametroPicking As Integer, ByVal IdPickingDet As Integer, ByVal IdProductoParametro As Integer, ByVal valor_texto As String, ByVal valor_numerico As Double, ByVal valor_fecha As Date, ByVal valor_logico As Boolean, ByVal user_agr As String, ByVal fec_agr As Date)
        Me.IdParametroPicking = IdParametroPicking
        Me.IdPickingDet = IdPickingDet
        Me.IdProductoParametro = IdProductoParametro
        Me.Valor_texto = Valor_texto
        Me.Valor_numerico = Valor_numerico
        Me.Valor_fecha = Valor_fecha
        Me.Valor_logico = Valor_logico
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
