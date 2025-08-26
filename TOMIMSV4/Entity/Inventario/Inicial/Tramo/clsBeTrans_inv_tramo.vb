Public Class clsBeTrans_inv_tramo
    Implements ICloneable
    Public Property Idinventario() As Integer = 0
    Public Property Idtramo() As Integer = 0
    Public Property Det_idoperador() As Integer = 0
    Public Property Det_estado() As String = ""
    Public Property Det_inicio() As Date = Date.Now
    Public Property Det_fin() As Date = Date.Now
    Public Property Res_idoperador() As Integer = 0
    Public Property Res_estado() As String = ""
    Public Property Res_inicio() As Date = Date.Now
    Public Property Res_fin() As Date = Date.Now
    Public Property Aplicado() As Boolean = False
    Public Property IdBodega() As Integer = 0

    Sub New()
    End Sub
    Sub New(ByRef idinventario As Integer, ByVal idtramo As Integer, ByVal det_idoperador As Integer, ByVal det_estado As String, ByVal det_inicio As Date, ByVal det_fin As Date, ByVal res_idoperador As Integer, ByVal res_estado As String, ByVal res_inicio As Date, ByVal res_fin As Date, ByVal aplicado As Boolean, ByVal IdBodega As Integer)
        Me.Idinventario = idinventario
        Me.Idtramo = idtramo
        Me.Det_idoperador = det_idoperador
        Me.Det_estado = det_estado
        Me.Det_inicio = det_inicio
        Me.Det_fin = det_fin
        Me.Res_idoperador = res_idoperador
        Me.Res_estado = res_estado
        Me.Res_inicio = res_inicio
        Me.Res_fin = res_fin
        Me.Aplicado = aplicado
        Me.IdBodega = IdBodega
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
