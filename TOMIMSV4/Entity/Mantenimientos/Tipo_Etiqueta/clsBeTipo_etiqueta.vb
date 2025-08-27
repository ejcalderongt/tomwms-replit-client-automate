Public Class clsBeTipo_etiqueta
    Implements ICloneable

    Public Property IdTipoEtiqueta As Integer = 0
    Public Property Nombre As String = ""
    Public Property Alto As Double = 0.0
    Public Property Ancho As Double = 0.0
    Public Property MargenIzq As Double = 0.0
    Public Property MagenDer As Double = 0.0
    Public Property MargenSup As Double = 0.0
    Public Property MargenInf As Double = 0.0
    Public Property User_agr As String = ""
    Public Property Fec_agr As Date = Now
    Public Property User_mod As String = ""
    Public Property Fec_mod As Date = Now
    Public Property Activo As Boolean = False
    Public Property dpi As Integer = 0
    Public Property codigo_zpl As String = ""
    Public Property Idclasificacion_etiqueta As Integer = 0
    Public Property Es_Inkjet As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdTipoEtiqueta As Integer, ByVal Nombre As String, ByVal Alto As Double, ByVal Ancho As Double, ByVal MargenIzq As Double, ByVal MagenDer As Double, ByVal MargenSup As Double, ByVal MargenInf As Double, ByVal user_agr As String, ByVal fec_agr As String, ByVal user_mod As String, ByVal fec_mod As String, ByVal activo As Boolean)
        IdTipoEtiqueta = IdTipoEtiqueta
        Nombre = Nombre
        Alto = Alto
        Ancho = Ancho
        MargenIzq = MargenIzq
        MagenDer = MagenDer
        MargenSup = MargenSup
        MargenInf = MargenInf
        user_agr = user_agr
        fec_agr = fec_agr
        user_mod = user_mod
        fec_mod = fec_mod
        activo = activo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
