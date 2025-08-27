Public Class clsBeTrans_ajuste_enc

    Implements ICloneable
    Public Property Idajusteenc() As Integer = 0
    Public Property Fecha() As DateTime = Date.Now
    Public Property Idusuario() As Integer = 0
    Public Property Referencia() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property IdBodega As Integer = 0
    Public Property IdProductoFamilia As Integer = 0
    Public Property Enviado_A_ERP As Boolean = False
    Public Property IdPropietarioBodega As Integer = 0
    Public Property Ajuste_Por_Inventario As Integer = 0

    '#EJC Agregó el centro de costo
    Public Property IdCentroCosto As Integer = 0
    '''#EJC20240719: Auditado, determina si el ajuste ya se validó previo a ser enviado a ERP.<summary>
    ''' </summary>
    ''' <returns></returns>
    Public Property Auditado As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef idajusteenc As Integer, ByVal fecha As Date, ByVal idusuario As Integer, ByVal referencia As String, ByVal fec_agr As Date, ByVal user_agr As String, ByVal fec_mod As Date, ByVal user_mod As String, ByVal IdBodega As Integer)
        Me.Idajusteenc = idajusteenc
        Me.Fecha = fecha
        Me.Idusuario = idusuario
        Me.Referencia = referencia
        Me.Fec_agr = fec_agr
        Me.User_agr = user_agr
        Me.Fec_mod = fec_mod
        Me.User_mod = user_mod
        Me.IdBodega = IdBodega
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
