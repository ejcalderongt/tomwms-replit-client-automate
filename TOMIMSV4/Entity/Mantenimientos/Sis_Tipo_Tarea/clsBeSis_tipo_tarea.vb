Public Class clsBeSis_tipo_tarea
    Implements ICloneable
    Public Property IdTipoTarea() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Contabilizar() As Boolean = False
    Sub New()
    End Sub
    Sub New(ByRef IdTipoTarea As Integer, ByVal Nombre As String, ByVal Contabilizar As Boolean)
        Me.IdTipoTarea = IdTipoTarea
        Me.Nombre = Nombre
        Me.Contabilizar = Contabilizar
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
