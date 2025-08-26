Public Class clsBeRol_usuario_estado
    Implements ICloneable

    Public Property IdRolUsuarioEstado() As Integer = 0
    Public Property IdRolUsuario() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property IdEstadoOrigen() As Integer = 0
    Public Property IdEstadoDestino() As Integer = 0
    Public Property Permitir() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
