Public Class clsBeConfiguracion_qa
    Implements ICloneable

    Public Property IdConfiguracionQA() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property FechaEjecucion() As Date = Date.Now
    Public Property IdEmpresaOrigen() As Integer = 0
    Public Property IdBodegaOrigen() As Integer = 0
    Public Property IdPropietarioOrigen() As Integer = 0
    Public Property IdProducto() As Integer = 0
    Public Property IdCliente() As Integer = 0
    Public Property Cantidad_Pedido_Presentacion() As Double = 0.0
    Public Property Cantidad_Pedido_UMBas() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Resultado() As String = ""
    Public Property Observaciones() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
