<Serializable>
Public Class clsBeProducto_Presentacion
    Implements ICloneable
    Public Property IdPresentacion() As Integer = 0
    Public Property IdProducto() As Integer = 0
    Public Property Codigo_barra() As String = ""
    Public Property Nombre() As String = ""
    Public Property Imprime_barra() As Boolean = False
    Public Property Peso() As Double = 0.0
    Public Property Alto() As Double = 0.0
    Public Property Largo() As Double = 0.0
    Public Property Ancho() As Double = 0.0
    Public Property Factor() As Double = 0.0
    Public Property MinimoExistencia() As Double = 0.0
    Public Property MaximoExistencia() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property EsPallet() As Boolean = False
    Public Property Precio() As Double = 0.0
    Public Property MinimoPeso() As Double = 0.0
    Public Property MaximoPeso() As Double = 0.0
    Public Property Costo() As Double = 0.0
    Public Property CamasPorTarima() As Double = 0.0
    Public Property CajasPorCama() As Double = 0.0
    Public Property Genera_lp_auto() As Boolean = False
    Public Property Permitir_paletizar() As Boolean = False
    Public Property Sistema() As Boolean = False
    Public Property IdPresentacionPallet As Integer = 0
    Public Property Codigo As String = ""
    Sub New()
    End Sub

    Sub New(ByRef IdPresentacion As Integer, ByVal IdProducto As Integer, ByVal codigo_barra As String, ByVal nombre As String, ByVal imprime_barra As Boolean, ByVal peso As Double, ByVal alto As Double, ByVal largo As Double, ByVal ancho As Double, ByVal factor As Double, ByVal MinimoExistencia As Double, ByVal MaximoExistencia As Double, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal EsPallet As Boolean, ByVal Precio As Double, ByVal MinimoPeso As Double, ByVal MaximoPeso As Double, ByVal Costo As Double, ByVal CamasPorTarima As Double, ByVal CajasPorCama As Double, ByVal genera_lp_auto As Boolean, ByVal permitir_paletizar As Boolean, ByVal Sistema As Boolean)
        Me.IdPresentacion = IdPresentacion
        Me.IdProducto = IdProducto
        Me.Codigo_barra = codigo_barra
        Me.Nombre = nombre
        Me.Imprime_barra = imprime_barra
        Me.Peso = peso
        Me.Alto = alto
        Me.Largo = largo
        Me.Ancho = ancho
        Me.Factor = factor
        Me.MinimoExistencia = MinimoExistencia
        Me.MaximoExistencia = MaximoExistencia
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
        Me.EsPallet = EsPallet
        Me.Precio = Precio
        Me.MinimoPeso = MinimoPeso
        Me.MaximoPeso = MaximoPeso
        Me.Costo = Costo
        Me.CamasPorTarima = CamasPorTarima
        Me.CajasPorCama = CajasPorCama
        Me.Genera_lp_auto = genera_lp_auto
        Me.Permitir_paletizar = permitir_paletizar
        Me.Sistema = Sistema
    End Sub
    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
