Public Class clsBeTrans_inv_enc
    Implements ICloneable
    Public Property Idinventarioenc() As Integer = 0
    Public Property Idpropietario() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdTipoInventario() As Integer = 0
    Public Property Tipo_Conteo_Producto() As Integer = 0
    Public Property Doble_verificacion() As Boolean = False
    Public Property Fecha() As Date = Date.Now
    Public Property Fecha_Ultimo_Inventario() As Date = Date.Now
    Public Property Estado() As String = ""
    Public Property Inicial() As Boolean = False
    Public Property Activo() As Boolean = False
    Public Property Regularizado() As Boolean = False
    Public Property Hora_ini() As Date = Date.Now
    Public Property Hora_fin() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property EsSistema() As Boolean = False
    Public Property Cambia_Ubicacion As Boolean = False
    Public Property Mostrar_Cantidad_Teorica_hh As Boolean = False
    Public Property IdProductoFamilia As Integer = 0
    Public Property IdBodegaVirtual As Integer = 0
    Public Property Ajuste_Por_Inventario() As Boolean = False
    Public Property Capturar_no_existente As Boolean = False
    Public Property multi_propietario As Boolean = False
    Public Property Licencia() As String = ""
    Public Property IdCentroCosto As Integer = 0
    Public Property Tipo_Asignacion As Integer = 2
    Public Property Capturar_No_Asignados As Boolean = False
    Public Property IdStock As Integer = 0

    Sub New()
    End Sub
    Sub New(ByRef idinventarioenc As Integer, ByVal idpropietario As Integer, ByVal idbodega As Integer, ByVal idtipoinventario As Integer, ByVal tipo_conteo_producto As Integer, ByVal doble_verificacion As Boolean, ByVal fecha As Date, ByVal estado As String, ByVal inicial As Boolean, ByVal activo As Boolean, ByVal regularizado As Boolean, ByVal hora_ini As Date, ByVal hora_fin As Date, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal EsSistema As Boolean, ByVal cambia_ubicacion As Boolean, ByVal mostrar_cantidad_teorica_hh As Boolean, ByVal IdProductoFamilia As Integer, ByVal IdBodegaVirtual As Integer, ByVal Capturar_no_existente As Boolean, ByVal multi_propietario As Boolean)
        Me.Idinventarioenc = idinventarioenc
        Me.Idpropietario = idpropietario
        Me.IdBodega = idbodega
        Me.IdTipoInventario = idtipoinventario
        Me.Tipo_Conteo_Producto = tipo_conteo_producto
        Me.Doble_verificacion = doble_verificacion
        Me.Fecha = fecha
        Me.Estado = estado
        Me.Inicial = inicial
        Me.Activo = activo
        Me.Regularizado = regularizado
        Me.Hora_ini = hora_ini
        Me.Hora_fin = hora_fin
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.EsSistema = EsSistema
        Me.Cambia_Ubicacion = cambia_ubicacion
        Me.Mostrar_Cantidad_Teorica_hh = mostrar_cantidad_teorica_hh
        Me.IdProductoFamilia = IdProductoFamilia
        Me.IdBodegaVirtual = IdBodegaVirtual
        Me.Capturar_no_existente = Capturar_no_existente
        Me.multi_propietario = multi_propietario
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
