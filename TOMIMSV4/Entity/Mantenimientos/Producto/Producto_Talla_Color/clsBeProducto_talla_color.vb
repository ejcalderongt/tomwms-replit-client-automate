Public Class clsBeProducto_talla_color
	Implements ICloneable

	Public Property IdProductoTallaColor() As Integer = 0
	Public Property IdProducto() As Integer = 0
	Public Property IdTalla() As Integer = 0
	Public Property IdColor() As Integer = 0
	Public Property CodigoSKU() As String = ""
	Public Property IdCampaña() As Integer = 0
	Public Property Fec_agr() As Date = New Date(1900, 1, 1)
	Public Property User_agr() As String = ""
	Public Property Fec_mod() As Date = New Date(1900, 1, 1)
	Public Property User_mod() As String = ""
	Public Property Activo As Boolean = False

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
