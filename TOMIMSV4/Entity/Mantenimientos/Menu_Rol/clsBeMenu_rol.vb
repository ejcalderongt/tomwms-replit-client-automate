Public Class clsBeMenu_rol
    Implements ICloneable

    Public Property IdMenuRol As Integer = 0
    Public Property IdMenu As String = ""
    Public Property IdRol As Integer = 0
    Public Property User_agr As String = ""
    Public Property Fec_agr As Date = Date.Now
    Public Property User_mod As String = ""
    Public Property Fec_mod As Date = Date.Now
    Public Property Activo As Boolean = False
    Public Property Visible As Boolean = False
    Public Property Leer As Boolean = False
    Public Property Modificar As Boolean = False
    Public Property Eliminar As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdMenuRol As Integer, ByVal IdMenu As String, ByVal IdRol As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal visible As Boolean)
        IdMenuRol = IdMenuRol
        IdMenu = IdMenu
        IdRol = IdRol
        user_agr = user_agr
        fec_agr = fec_agr
        user_mod = user_mod
        fec_mod = fec_mod
        activo = activo
        visible = visible
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
