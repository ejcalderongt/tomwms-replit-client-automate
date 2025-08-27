Public Class clsBeEstructura_tramo
    Implements ICloneable

    Public Property IdTramo() As Integer = 0

    Public Property IdSector() As Integer = 0
    Public Property IdArea As Integer = 0
    Public Property Sistema() As Boolean = False
    Public Property Descripcion() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Now
    Public Property Activo() As Boolean = False
    Public Property Alto() As Double = 0D
    Public Property Largo() As Double = 0D
    Public Property Ancho() As Double = 0D
    Public Property Margen_izquierdo() As Double = 0D
    Public Property Margen_derecho() As Double = 0D
    Public Property Margen_superior() As Double = 0D
    Public Property Margen_inferior() As Double = 0D
    Public Property Codigo() As String = ""
    Public Property Indice_x() As Integer = 0
    Public Property Orientacion() As Integer = 0
    Public Property IdTipoProductoDefault() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Horizontal As Boolean = False
    Public Property Orden_Descendente As Boolean = False

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
