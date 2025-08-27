Public Class clsBeVW_Impresion_Pallet
    Implements ICloneable

    Public Property IdStockRec As Integer = 0
    Public Property Rec_No() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property Empresa() As String = ""
    Public Property IdPropietario() As Integer = 0
    Public Property Propietario_Nombre() As String = ""
    Public Property Bodega() As String = ""
    Public Property Proveedor_Nombre() As String = ""
    Public Property Proveedor_Codigo() As String = ""
    Public Property Proveedor_Tel() As String = ""
    Public Property Proveedor_Dir() As String = ""
    Public Property Producto_Codigo() As String = ""
    Public Property Producto_Nombre_Largo() As String = ""
    Public Property Producto_UM() As String = ""
    Public Property Producto_Presentacion() As String = ""
    Public Property Producto_Cantidad() As Double = 0
    Public Property Producto_Cantidad_Paralela As Double = 0
    Public Property Producto_Peso As Double = 0
    Public Property Producto_Vence() As Date = Date.Now
    Public Property Producto_Lote() As String = ""
    Public Property Producto_Estado() As String = ""
    Public Property LP() As String = ""
    Public Property PC() As String = ""
    Public Property Ref_PC() As String = ""
    Public Property Fecha_PC() As Date = Date.Now
    Public Property Observacion() As String = ""
    Public Property Rec_Tipo_Albaran() As String = ""
    Public Property Imprimio() As String = ""
    Public Property Fecha_Produccion As Date = Now.Date
    Public Property Imprimir As Boolean = True
    Public Property IdPresentacion As Integer = 0
    Public Property Presentacion As New clsBeProducto_Presentacion
    Sub New()
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MemberwiseClone()
    End Function
End Class
