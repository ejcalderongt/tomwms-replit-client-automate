Public Class clsBeTrans_manufactura_enc
    Implements ICloneable

    Public Property IdManufacturaEnc() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdTipoManufactura() As clsDataContractDI.Manufacturing_Process = clsDataContractDI.Manufacturing_Process.Sin_Proceso_Nativo
    Public Property IdPedidoEnc() As Integer = 0
    Public Property Fecha_manufactura() As Date = Date.Now
    Public Property Hora_ini() As Date = Date.Now
    Public Property Hora_fin() As Date = Date.Now
    Public Property Estado() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Escaneo() As Boolean = False
    Public Property Activo() As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
