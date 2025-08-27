Public Class clsBeConfiguracion_usuario_enc
    Implements ICloneable

    Public Property IdConfiguracionUsuarioEnc() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdUsuario() As Integer = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
