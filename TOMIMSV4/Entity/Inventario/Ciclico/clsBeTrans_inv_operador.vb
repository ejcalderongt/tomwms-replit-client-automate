Public Class clsBeTrans_inv_operador
    Implements ICloneable

    Public Property Idinvoperador() As Integer = 0

    Public Property Idinventarioenc() As Integer = 0

    Public Property Idinvencreconteo() As Integer = 0

    Public Property Idtramo() As Integer = 0

    Public Property Idubic() As Integer = 0

    Public Property Idoperador() As Integer = 0

    Public Property IdBodega As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef idinvoperador As Integer, ByVal idinventarioenc As Integer, ByVal idinvencreconteo As Integer, ByVal idtramo As Integer, ByVal idubic As Integer, ByVal idoperador As Integer)
        Me.Idinvoperador = idinvoperador
        Me.Idinventarioenc = idinventarioenc
        Me.Idinvencreconteo = idinvencreconteo
        Me.Idtramo = idtramo
        Me.Idubic = idubic
        Me.Idoperador = idoperador
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
