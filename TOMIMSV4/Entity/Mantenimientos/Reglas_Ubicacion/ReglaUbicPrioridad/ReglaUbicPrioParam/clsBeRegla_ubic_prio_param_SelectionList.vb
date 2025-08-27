Public Class clsBeRegla_ubic_prio_param_SelectionList : Inherits clsBeRegla_ubic_prio_param

    Public Sub New()

    End Sub

    Public Sub New(ByRef IdReglaUbicPrioParam As Integer, ByVal Nombre As String, ByVal Activo As Integer, ByVal Orden As Integer, ByVal Tipo As Integer)
        MyBase.New(IdReglaUbicPrioParam, Nombre, Activo, Orden, Tipo)
    End Sub

    Public Property Seleccionar As Boolean = False

End Class
