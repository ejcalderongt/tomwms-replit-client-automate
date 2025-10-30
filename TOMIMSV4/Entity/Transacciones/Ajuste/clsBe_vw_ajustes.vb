Public Class clsBe_vw_ajustes
    Implements ICloneable
    Public Property IdAjusteEnc() As Integer = 0
    Public Property IdAjusteDet As Integer
    Public Property Fecha() As String = Nothing
    Public Property Referencia() As String = ""
    Public Property Codigo_Producto() As String = ""
    Public Property Nombre_Producto() As String = ""
    Public Property IdPresentacion() As Integer = 0
    Public Property UMBas() As String = ""
    Public Property IdBodegaERP() As Integer = 0
    Public Property Codigo_Bodega() As String = ""
    Public Property Nombre_Bodega() As String = ""
    Public Property Cantidad_original() As Double = 0.0
    Public Property Cantidad_nueva() As Double = 0.0
    Public Property Peso_nuevo() As Double = 0.0
    Public Property Peso_original() As Double = 0.0
    Public Property Fecha_vence_nueva() As Date = Date.Now
    Public Property Fecha_vence_original() As Date = Date.Now
    Public Property Lote_Original() As String = ""
    Public Property Lote_Nuevo() As String = ""
    Public Property Tipo_Ajuste() As String = ""
    Public Property Modifica_Cantidad() As Boolean = False
    Public Property Enviado() As Boolean = False
    Public Property Motivo_Ajuste() As String = ""
    Public Property Observacion As String = ""
    Public Property Seccion As String = ""
    Public Property IdProductoFamilia As Integer = 0
    Public Property Nombre_Presentacion As String = ""
    Public Property Factor As Double = 0
    Public Property Codigo_Centro_Costo As String = ""
    Public Property Nombre_Centro_Costo As String = ""
    Public Property Talla As String = ""
    Public Property Color As String = ""
    Public Property Usr_Agr As String = ""

    Sub New()
    End Sub
    Sub New(ByRef IdAjusteEnc As Integer, ByVal Fecha As String, ByVal Referencia As String, ByVal Codigo_Producto As String, ByVal Nombre_Producto As String, ByVal IdPresentacion As Integer, ByVal UMBas As String, ByVal IdBodegaERP As Integer, ByVal Codigo_Bodega As String, ByVal Nombre_Bodega As String, ByVal cantidad_original As Double, ByVal cantidad_nueva As Double, ByVal peso_nuevo As Double, ByVal peso_original As Double, ByVal fecha_vence_nueva As Date, ByVal fecha_vence_original As Date, ByVal Lote_Original As String, ByVal Lote_Nuevo As String, ByVal Tipo_Ajuste As String, ByVal Modifica_Cantidad As Boolean, ByVal Enviado As Boolean, ByVal Motivo_Ajuste As String)
        Me.IdAjusteEnc = IdAjusteEnc
        Me.Fecha = Fecha
        Me.Referencia = Referencia
        Me.Codigo_Producto = Codigo_Producto
        Me.Nombre_Producto = Nombre_Producto
        Me.IdPresentacion = IdPresentacion
        Me.UMBas = UMBas
        Me.IdBodegaERP = IdBodegaERP
        Me.Codigo_Bodega = Codigo_Bodega
        Me.Nombre_Bodega = Nombre_Bodega
        Me.Cantidad_original = Cantidad_original
        Me.Cantidad_nueva = Cantidad_nueva
        Me.Peso_nuevo = Peso_nuevo
        Me.Peso_original = Peso_original
        Me.Fecha_vence_nueva = Fecha_vence_nueva
        Me.Fecha_vence_original = Fecha_vence_original
        Me.Lote_Original = Lote_Original
        Me.Lote_Nuevo = Lote_Nuevo
        Me.Tipo_Ajuste = Tipo_Ajuste
        Me.Modifica_Cantidad = Modifica_Cantidad
        Me.Enviado = Enviado
        Me.Motivo_Ajuste = Motivo_Ajuste
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
