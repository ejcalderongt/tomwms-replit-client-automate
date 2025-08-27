Public Class clsBeCliente_lotes
	Implements ICloneable

	Public Property IdClienteLote() As Integer = 0
	Public Property IdCliente() As Integer = 0
	Public Property Lote() As String = ""
	Public Property IdProductoEstado() As Integer = 0
	Public Property User_agr() As String = ""
	Public Property Fec_agr() As Date = Date.Now
	Public Property User_mod() As String = ""
	Public Property Fec_mod() As Date = Date.Now
	Public Property Activo() As Boolean = False
	Public Property Bloquear() As Boolean = False
	Public Property IdProducto() As Integer = 0

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
