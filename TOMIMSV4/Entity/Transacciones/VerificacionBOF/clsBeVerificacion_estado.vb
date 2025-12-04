Public Class clsBeVerificacion_estado
	Implements ICloneable

	Public Property IdEstado() As Integer = 0
	Public Property Descripcion() As String = ""
	Public Property User_agr() As String = ""
	Public Property Fec_agr() As Date = Date.Now
	Public Property Activo() As Boolean = False

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
