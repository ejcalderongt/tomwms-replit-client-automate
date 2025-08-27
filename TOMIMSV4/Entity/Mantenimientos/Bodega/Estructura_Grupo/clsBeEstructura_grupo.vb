Public Class clsBeEstructura_grupo
    Implements ICloneable
    Public Property IdBodega As Integer = 0
    Public Property IdGrupo() As Integer = 0
    Public Property IdTramo() As Integer = 0
    Public Property Pos() As Integer = 0
    Public Property Cant() As Integer = 0
    Public Property Tamano() As Integer = 0
    Public Property Offset() As Integer = 0
    Public Property Ancho() As Double = 0.0
    Public Property Alto() As Double = 0.0
    Public Property Largo() As Double = 0.0
    Public Property Palet() As Integer = 0
    Public Property Orient() As Integer = 0
    Public Property Agrupacion() As Integer = 0
    Public Property Orden_Descendente As Boolean = False
    Sub New()
    End Sub
    Sub New(ByRef IdGrupo As Integer, ByVal IdTramo As Integer, ByVal pos As Integer, ByVal cant As Integer, ByVal tamano As Integer,
            ByVal offset As Integer, ByVal ancho As Double, ByVal alto As Double, ByVal largo As Double, ByVal palet As Integer,
            ByVal orient As Integer, ByVal agrupacion As Integer, ByVal orden_descendente As Boolean)
        Me.IdGrupo = IdGrupo
        Me.IdTramo = IdTramo
        Me.Pos = pos
        Me.Cant = cant
        Me.Tamano = tamano
        Me.Offset = offset
        Me.Ancho = ancho
        Me.Alto = alto
        Me.Largo = largo
        Me.Palet = palet
        Me.Orient = orient
        Me.Agrupacion = agrupacion
        Me.Orden_Descendente = orden_descendente
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
