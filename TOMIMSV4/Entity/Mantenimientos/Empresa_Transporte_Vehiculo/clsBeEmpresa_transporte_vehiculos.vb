Public Class clsBeEmpresa_transporte_vehiculos
    Implements ICloneable

    Public Property IdVehiculo() As Integer = 0
    Public Property IdEmpresaTransporte() As Integer = 0
    Public Property IdTipoContenedor() As Integer = 0
    Public Property Placa() As String = ""
    Public Property Marca() As String = ""
    Public Property Modelo() As String = ""
    Public Property Peso() As Double = 0.0
    Public Property Volumen() As Double = 0.0
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Tipo() As String = ""
    Public Property Alto() As Double = 0.0
    Public Property Largo() As Double = 0.0
    Public Property Ancho() As Double = 0.0
    Public Property Placa_comercial() As String = ""
    Public Property Es_contedor() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdVehiculo As Integer, ByVal IdEmpresaTransporte As Integer, ByVal IdTipoContenedor As Integer, ByVal placa As String, ByVal marca As String, ByVal modelo As String, ByVal peso As Double, ByVal volumen As Double, ByVal activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal tipo As String, ByVal alto As Double, ByVal largo As Double, ByVal ancho As Double, ByVal placa_comercial As String, ByVal es_contedor As Integer)
        Me.IdVehiculo = IdVehiculo
        Me.IdEmpresaTransporte = IdEmpresaTransporte
        Me.IdTipoContenedor = IdTipoContenedor
        Me.Placa = placa
        Me.Marca = marca
        Me.Modelo = modelo
        Me.Peso = peso
        Me.Volumen = volumen
        Me.Activo = activo
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Tipo = tipo
        Me.Alto = alto
        Me.Largo = largo
        Me.Ancho = ancho
        Me.Placa_comercial = placa_comercial
        Me.Es_contedor = es_contedor
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
