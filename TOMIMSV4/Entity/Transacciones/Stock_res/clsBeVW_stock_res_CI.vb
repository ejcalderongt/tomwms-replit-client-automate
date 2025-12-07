Public Class clsBeVW_stock_res_CI
    Implements ICloneable

    Public Property Codigo() As String = ""
    Public Property Nombre() As String = ""
    Public Property UM() As String = ""
    Public Property ExistUMBAs() As String = ""
    Public Property Pres() As String = ""
    Public Property ExistPres() As String = ""
    Public Property ReservadoUMBAs() As String = ""
    Public Property DisponibleUMBas() As String = ""
    Public Property Lote() As String = ""
    Public Property Fecha_Vence() As Date = New Date(1900, 1, 1)
    Public Property Estado() As String = ""
    Public Property Ubic() As String = ""
    Public Property idUbic() As String = ""
    Public Property Pedido() As String = ""
    Public Property Pick() As String = ""
    Public Property LicPlate() As String = ""
    Public Property IdProductoEstado() As String = ""
    Public Property IdProductoBodega() As String = ""
    Public Property factor As Integer = 0
    Public Property ingreso() As String = ""
    Public Property IdTipoEtiqueta As Integer = 0
    Public Property DispPres() As String = ""
    Public Property ResPres() As String = ""
    Public Property NombreArea As String = ""
    Public Property Clasificacion As String = ""
    Public Property IdPresentacion As Integer = 0
    Public Property IdArea As Integer = 0
    '#AT20240805 Agregue el campo IdStock para la consulta detallada de existencia
    Public Property IdStock As Integer = 0
    '#GT25072025: campo para la HH en existencias
    Public Property IdUbicacionAnterior As Integer = 0
    Public Property Codigo_Barra As String = ""

    Sub New()
    End Sub

    Sub New(ByRef Codigo As String,
            ByVal Nombre As String,
            ByVal UM As String,
            ByVal ExistUMBAs As String,
            ByVal Pres As String,
            ByVal ExistPres As String,
            ByVal ReservadoUMBAs As Integer,
            ByVal DisponibleUMBas As String,
            ByVal Lote As String,
            ByVal Fecha_Vence As Date,
            ByVal Estado As String,
            ByVal Ubic As String,
            ByVal idUbic As String,
            ByVal Pedido As String,
            ByVal Pick As String,
            ByVal LicPlate As String,
            ByVal IdProductoEstado As String,
            ByVal IdProductoBodega As String,
            ByVal factor As Integer,
            ByVal ingreso As String, ByVal IdTipoEtiqueta As Integer)

        Me.Codigo = Codigo
        Me.Nombre = Nombre
        Me.UM = UM
        Me.ExistUMBAs = ExistUMBAs
        Me.Pres = Pres
        Me.ExistPres = ExistPres
        Me.ReservadoUMBAs = ReservadoUMBAs
        Me.DisponibleUMBas = DisponibleUMBas
        Me.Lote = Lote
        Me.Fecha_Vence = Fecha_Vence
        Me.Estado = Estado
        Me.Ubic = Ubic
        Me.idUbic = idUbic
        Me.Pedido = Pedido
        Me.Pick = Pick
        Me.LicPlate = LicPlate
        Me.IdProductoEstado = IdProductoEstado
        Me.IdProductoBodega = IdProductoBodega
        Me.factor = factor
        Me.ingreso = ingreso
        Me.IdTipoEtiqueta = IdTipoEtiqueta
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
