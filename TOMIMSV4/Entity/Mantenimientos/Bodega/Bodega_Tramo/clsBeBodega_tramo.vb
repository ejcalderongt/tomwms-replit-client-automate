<Serializable>
Public Class clsBeBodega_tramo
    Implements ICloneable
    Public Property IdTramo() As Integer = 0
    Public Property IdSector() As Integer = 0
    Public Property IdArea() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Sistema() As Boolean = False
    Public Property Descripcion() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Alto() As Double = 0.0
    Public Property Largo() As Double = 0.0
    Public Property Ancho() As Double = 0.0
    Public Property Margen_izquierdo() As Double = 0.0
    Public Property Margen_derecho() As Double = 0.0
    Public Property Margen_superior() As Double = 0.0
    Public Property Margen_inferior() As Double = 0.0
    Public Property Codigo() As String = ""
    Public Property Indice_x() As Integer = 0
    Public Property Orientacion() As Integer = 0
    Public Property IdTipoProductoDefault() As Integer = 0
    Public Property IdFontEnc As Integer = 0
    Public Property IdTipoRack As Integer = 0
    Public Property Es_Rack As Boolean = True
    Public Property Horizontal As Boolean = False
    Public Property Orden_Descendente As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdTramo As Integer,
            ByVal IdSector As Integer,
            ByVal sistema As Boolean,
            ByVal descripcion As String,
            ByVal user_agr As String,
            ByVal fec_agr As Date,
            ByVal user_mod As String,
            ByVal fec_mod As Date,
            ByVal activo As Boolean,
            ByVal alto As Double,
            ByVal largo As Double,
            ByVal ancho As Double,
            ByVal margen_izquierdo As Double,
            ByVal margen_derecho As Double,
            ByVal margen_superior As Double,
            ByVal margen_inferior As Double,
            ByVal Codigo As String,
            ByVal Indice_x As Integer,
            ByVal Orientacion As Integer,
            ByVal IdTipoProductoDefault As Integer,
            ByVal IdFontEnc As Integer,
            ByVal Horizontal As Boolean,
            ByVal Orden_Descendente As Boolean)

        Me.IdTramo = IdTramo
        Me.IdSector = IdSector
        Me.Sistema = sistema
        Me.Descripcion = descripcion
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
        Me.Alto = alto
        Me.Largo = largo
        Me.Ancho = ancho
        Me.Margen_izquierdo = margen_izquierdo
        Me.Margen_derecho = margen_derecho
        Me.Margen_superior = margen_superior
        Me.Margen_inferior = margen_inferior
        Me.Codigo = Codigo
        Me.Indice_x = Indice_x
        Me.Orientacion = Orientacion
        Me.IdTipoProductoDefault = IdTipoProductoDefault
        Me.IdFontEnc = IdFontEnc
        Me.Horizontal = Horizontal
        Me.Orden_Descendente = Orden_Descendente
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
