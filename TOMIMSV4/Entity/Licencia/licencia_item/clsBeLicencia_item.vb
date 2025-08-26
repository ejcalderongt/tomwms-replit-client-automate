Public Class clsBeLicencia_item
    Implements ICloneable

    Public Property IdDisp() As String = ""
    Public Property Identificacion() As String = ""
    Public Property Estado() As String = ""
    Public Property Fecha_Sistema As DateTime = Now

    Sub New()
    End Sub

    Sub New(ByRef idDisp As String, ByVal identificacion As String, ByVal tipo As Integer, ByVal bandera As Integer, ByVal estado As String)
        Me.IdDisp = IdDisp
        Me.Identificacion = Identificacion
        Me.Tipo = Tipo
        Me.Bandera = Bandera
        Me.Estado = Estado
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
