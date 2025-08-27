Public Class clsBeUbicaciones_por_regla
    Implements ICloneable

    Public Property IdReglaUbicacionEnc() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Ancho() As Double = 0.0
    Public Property Largo() As Double = 0.0
    Public Property Alto() As Double = 0.0
    Public Property IdTramo() As Integer = 0
    Public Property Indice_x() As Integer = 0
    Public Property Nivel() As Integer = 0
    Public Property IdIndiceRotacion() As Integer = 0
    Public Property IdTipoRotacion() As Integer = 0
    Public Property Dañado() As Boolean = False
    Public Property Activo() As Boolean = False
    Public Property Bloqueada() As Boolean = False
    Public Property Acepta_pallet() As Boolean = False
    Public Property IdBodega() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property Regla_ubic_det_prop_Activo() As Boolean = False
    Public Property IdPropietario() As Integer = 0
    Public Property IdIndiceRotacionRegla() As Integer = 0
    Public Property IdTipoRotacionRegla() As Integer = 0
    Public Property IdTipoProducto() As Integer = 0
    Public Property Regla_ubic_det_tp_Activo() As Boolean = False
    Public Property IdEstado() As Integer = 0
    Public Property Regla_ubic_det_pe_Activo() As Boolean = False
    Public Property IdPresentacion() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdReglaUbicacionEnc As Integer, ByVal IdUbicacion As Integer, ByVal descripcion As String, ByVal ancho As Double, ByVal largo As Double, ByVal alto As Double, ByVal IdTramo As Integer, ByVal indice_x As Integer, ByVal nivel As Integer, ByVal IdIndiceRotacion As Integer, ByVal IdTipoRotacion As Integer, ByVal dañado As Boolean, ByVal activo As Boolean, ByVal bloqueada As Boolean, ByVal acepta_pallet As Boolean, ByVal IdBodega As Integer, ByVal IdPropietarioBodega As Integer, ByVal regla_ubic_det_prop_Activo As Boolean, ByVal IdPropietario As Integer, ByVal IdIndiceRotacionRegla As Integer, ByVal IdTipoRotacionRegla As Integer, ByVal IdTipoProducto As Integer, ByVal regla_ubic_det_tp_Activo As Boolean, ByVal IdEstado As Object, ByVal regla_ubic_det_pe_Activo As Boolean, ByVal IdPresentacion As Integer)
        Me.IdReglaUbicacionEnc = IdReglaUbicacionEnc
        Me.IdUbicacion = IdUbicacion
        Me.Descripcion = descripcion
        Me.Ancho = ancho
        Me.Largo = largo
        Me.Alto = alto
        Me.IdTramo = IdTramo
        Me.Indice_x = indice_x
        Me.Nivel = nivel
        Me.IdIndiceRotacion = IdIndiceRotacion
        Me.IdTipoRotacion = IdTipoRotacion
        Me.Dañado = dañado
        Me.Activo = activo
        Me.Bloqueada = bloqueada
        Me.Acepta_pallet = acepta_pallet
        Me.IdBodega = IdBodega
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.Regla_ubic_det_prop_Activo = regla_ubic_det_prop_Activo
        Me.IdPropietario = IdPropietario
        Me.IdIndiceRotacionRegla = IdIndiceRotacionRegla
        Me.IdTipoRotacionRegla = IdTipoRotacionRegla
        Me.IdTipoProducto = IdTipoProducto
        Me.Regla_ubic_det_tp_Activo = regla_ubic_det_tp_Activo
        Me.IdEstado = IdEstado
        Me.Regla_ubic_det_pe_Activo = regla_ubic_det_pe_Activo
        Me.IdPresentacion = IdPresentacion
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
