Public Class clsBeTrans_picking_prioridad
	Implements ICloneable

	Public Property IdPrioridadPicking() As Integer = 0
	Public Property Codigo() As Integer = 0
	Public Property Nombre() As String = ""
	Public Property User_agr() As String = ""
	Public Property Fec_agr() As Date = Date.Now
	Public Property User_mod() As String = ""
	Public Property Fec_mod() As Date = Date.Now
	Public Property Activo() As Boolean = False

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
