Public Class clsBeTrans_inv_ciclico
    Implements ICloneable

    Public Property IdInvCiclico() As Integer = 0

    Public Property Idinventarioenc() As Integer = 0

    Public Property IdStock() As Integer = 0

    Public Property IdProductoBodega() As Integer = 0

    Public Property IdProductoEstado() As Integer = 0

    Public Property IdPresentacion() As Integer = 0

    Public Property IdUbicacion() As Integer = 0

    Public Property EsNuevo() As Boolean = False

    Public Property Lote_stock() As String = ""

    Public Property Lote() As String = ""

    Public Property Fecha_vence_stock() As Date = Nothing

    Public Property Fecha_vence() As Date = Nothing

    Public Property Cant_stock() As Double = 0.0

    Public Property Cantidad() As Double = 0.0

    Public Property Cant_reconteo() As Double = 0.0

    Public Property Peso_stock() As Double = 0.0

    Public Property Peso() As Double = 0.0

    Public Property Peso_reconteo() As Double

    Public Property Idoperador() As Integer = 0

    Public Property User_agr() As String = ""

    Public Property Fec_agr() As Date = Date.Now

    Public Property IdProductoEst_nuevo() As Integer = 0

    Public Property IdPresentacion_nuevo() As Integer = 0

    Public Property IdUbicacion_nuevo() As Integer = 0

    Public Property EsPallet() As Boolean = False

    Public Property lic_plate() As String
    Public Property Fec_Mod As DateTime = Now
    Public Property IdBodega As Integer = 0
    Public Property EstadoNuevo As String = ""
    Public Property Regularizar As Boolean = True


    Sub New()
    End Sub

    Sub New(ByRef idinvciclico As Integer,
            ByVal idinventarioenc As Integer,
            ByVal IdStock As Integer,
            ByVal IdProductoBodega As Integer,
            ByVal IdProductoEstado As Integer,
            ByVal IdPresentacion As Integer,
            ByVal IdUbicacion As Integer,
            ByVal EsNuevo As Boolean,
            ByVal lote_stock As String,
            ByVal lote As String,
            ByVal fecha_vence_stock As Date,
            ByVal fecha_vence As Date,
            ByVal cant_stock As Double,
            ByVal cantidad As Double,
            ByVal cant_reconteo As Double,
            ByVal peso_stock As Double,
            ByVal peso As Double,
            ByVal peso_reconteo As Double,
            ByVal idoperador As Integer,
            ByVal user_agr As String,
            ByVal fec_agr As Date,
            ByVal IdProductoEst_nuevo As Integer,
            ByVal IdPresentacion_nuevo As Integer,
            ByVal IdUbicacion_nuevo As Integer,
            ByVal EsPallet As Boolean,
            ByVal lic_plate As String,
            ByVal IdBodega As Integer)

        Me.IdInvCiclico = idinvciclico
        Me.Idinventarioenc = idinventarioenc
        Me.IdStock = IdStock
        Me.IdProductoBodega = IdProductoBodega
        Me.IdProductoEstado = IdProductoEstado
        Me.IdPresentacion = IdPresentacion
        Me.IdUbicacion = IdUbicacion
        Me.EsNuevo = EsNuevo
        Me.Lote_stock = lote_stock
        Me.Lote = lote
        Me.Fecha_vence_stock = fecha_vence_stock
        Me.Fecha_vence = fecha_vence
        Me.Cant_stock = cant_stock
        Me.Cantidad = cantidad
        Me.Cant_reconteo = cant_reconteo
        Me.Peso_stock = peso_stock
        Me.Peso = peso
        Me.Peso_reconteo = peso_reconteo
        Me.Idoperador = idoperador
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.IdProductoEst_nuevo = IdProductoEst_nuevo
        Me.IdPresentacion_nuevo = IdPresentacion_nuevo
        Me.IdUbicacion_nuevo = IdUbicacion_nuevo
        Me.EsPallet = EsPallet
        Me.lic_plate = lic_plate
        Me.IdBodega = IdBodega
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class

