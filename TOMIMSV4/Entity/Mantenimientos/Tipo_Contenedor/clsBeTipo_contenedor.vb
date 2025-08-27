Public Class clsBeTipo_contenedor
    Implements ICloneable
    Public Property IdTipoContenedor() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Largo() As Double = 0.0
    Public Property Ancho() As Double = 0.0
    Public Property Alto() As Double = 0.0
    Public Property Pies() As Double = 0.0
    Public Property Tonealadas() As Double = 0.0
    Public Property VolumenUtil() As Double = 0.0
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Activo() As Boolean = False
    Public Property Tara() As Double = 0.0
    Sub New()
    End Sub
    Sub New(ByRef IdTipoContenedor As Integer, ByVal Nombre As String, ByVal Largo As Double, ByVal Ancho As Double, ByVal Alto As Double, ByVal Pies As Double, ByVal Tonealadas As Double, ByVal VolumenUtil As Double, ByVal fec_agr As Date, ByVal user_agr As String, ByVal fec_mod As Date, ByVal user_mod As String, ByVal activo As Boolean, ByVal Tara As Double)
        Me.IdTipoContenedor = IdTipoContenedor
        Me.Nombre = Nombre
        Me.Largo = Largo
        Me.Ancho = Ancho
        Me.Alto = Alto
        Me.Pies = Pies
        Me.Tonealadas = Tonealadas
        Me.VolumenUtil = VolumenUtil
        Me.Fec_agr = Fec_agr
        Me.User_agr = User_agr
        Me.Fec_mod = Fec_mod
        Me.User_mod = User_mod
        Me.Activo = Activo
        Me.Tara = Tara
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
