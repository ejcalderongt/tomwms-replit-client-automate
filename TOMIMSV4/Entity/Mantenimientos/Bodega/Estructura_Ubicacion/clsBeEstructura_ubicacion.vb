Public Class clsBeEstructura_ubicacion
    Implements ICloneable

    Public Property IdUbicacion() As Integer = 0

    Public Property IdTramo() As Integer = 0

    Public Property Descripcion() As String = ""

    Public Property Ancho() As Double = 0.0

    Public Property Largo() As Double = 0.0

    Public Property Alto() As Double = 0.0

    Public Property Nivel() As Integer = 0

    Public Property Indice_x() As Integer = 0

    Public Property IdIndiceRotacion() As Integer = 0

    Public Property IdTipoRotacion() As Integer = 0

    Public Property Sistema() As Boolean = False

    Public Property Codigo_barra() As String = ""

    Public Property Codigo_barra2() As String = ""

    Public Property User_agr() As String = ""

    Public Property Fec_agr() As Date = Date.Now

    Public Property User_mod() As String = ""

    Public Property Fec_mod() As Date = Date.Now

    Public Property Dañado() As Boolean = False

    Public Property Activo() As Boolean = False

    Public Property Bloqueada() As Boolean = False

    Public Property Acepta_pallet() As Boolean = False

    Public Property Ubicacion_picking() As Boolean = False

    Public Property Ubicacion_recepcion() As Boolean = False

    Public Property Ubicacion_despacho() As Boolean = False

    Public Property Ubicacion_merma() As Boolean = False

    Public Property Margen_izquierdo() As Double = 0.0

    Public Property Margen_derecho() As Double = 0.0

    Public Property Margen_superior() As Double = 0.0

    Public Property Margen_inferior() As Double = 0

    Public Property Orientacion_pos() As String = ""

    Public Property IdBodega As Integer = 0

    Public Property IdArea As Integer = 0

    Public Property IdSector As Integer = 0

    Public Property Ubicacion_muelle As Boolean = False

    Public Property Posicion_X As Double = 0

    Public Property Posicion_Y As Double = 0

    Sub New()
    End Sub

    Sub New(ByRef IdUbicacion As Integer, ByVal IdTramo As Integer, ByVal descripcion As String, ByVal ancho As Double, ByVal largo As Double, ByVal alto As Double,
            ByVal nivel As Integer, ByVal indice_x As Integer, ByVal IdIndiceRotacion As Integer, ByVal IdTipoRotacion As Integer, ByVal sistema As Boolean,
            ByVal codigo_barra As String, ByVal codigo_barra2 As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date,
            ByVal dañado As Boolean, ByVal activo As Boolean, ByVal bloqueada As Boolean, ByVal acepta_pallet As Boolean, ByVal ubicacion_picking As Boolean,
            ByVal ubicacion_recepcion As Boolean, ByVal ubicacion_despacho As Boolean, ByVal ubicacion_merma As Boolean, ByVal margen_izquierdo As Double,
            ByVal margen_derecho As Double, ByVal margen_superior As Double, ByVal margen_inferior As Double, ByVal orientacion_pos As String, ByVal Ubicacion_muelle As Boolean,
            ByVal Posicion_X As Double, ByVal Posicion_Y As Double)
        Me.IdUbicacion = IdUbicacion
        Me.IdTramo = IdTramo
        Me.Descripcion = descripcion
        Me.Ancho = ancho
        Me.Largo = largo
        Me.Alto = alto
        Me.Nivel = nivel
        Me.Indice_x = indice_x
        Me.IdIndiceRotacion = IdIndiceRotacion
        Me.IdTipoRotacion = IdTipoRotacion
        Me.Sistema = sistema
        Me.Codigo_barra = codigo_barra
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Dañado = dañado
        Me.Activo = activo
        Me.Bloqueada = bloqueada
        Me.Acepta_pallet = acepta_pallet
        Me.Ubicacion_picking = ubicacion_picking
        Me.Ubicacion_recepcion = ubicacion_recepcion
        Me.Ubicacion_despacho = ubicacion_despacho
        Me.Margen_izquierdo = margen_izquierdo
        Me.Margen_derecho = margen_derecho
        Me.Margen_superior = margen_superior
        Me.Orientacion_pos = orientacion_pos
        Me.Ubicacion_muelle = Ubicacion_muelle
        Me.Posicion_Y = Posicion_Y
        Me.Posicion_X = Posicion_X
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
