Public Class clsBeRegla_vencimiento
    Implements ICloneable

    Public Property IsNew As Boolean = False
    Public Property IdReglaVencimiento() As Integer = 0
    Public Property NombreRegla() As String = ""
    Public Property IdBodega() As Integer = 0
    Public Property IdProductoFamilia() As Integer = 0
    Public Property IdProductoClasificacion() As Integer = 0
    Public Property TiempoVencimientoDias() As Integer = 0
    Public Property TipoNotificacion() As String = ""
    Public Property IdPropietarioMercancia() As Integer = 0
    Public Property IdProveedor() As Integer = 0
    Public Property IdCliente() As Integer = 0
    Public Property Activa() As Boolean = False
    Public Property FechaCreacion() As Date = Date.Now
    Public Property UsuarioCreacion() As String = ""
    Public Property FechaModificacion() As Date = Date.Now
    Public Property UsuarioModificacion() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
