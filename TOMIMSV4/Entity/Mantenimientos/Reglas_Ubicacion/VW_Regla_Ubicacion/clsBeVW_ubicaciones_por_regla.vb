Public Class clsBeVW_ubicaciones_por_regla
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
    Public Property Nombre_Completo() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
