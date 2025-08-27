Public Class clsBeLog_sincronizacion_fallos
	Implements ICloneable

	Public Property IdLogFallo() As Integer = 0
	Public Property IdOrdenCompraEnc() As Integer = 0
	Public Property IdPedidoEnc() As Integer = 0
	Public Property Estado() As String = ""
	Public Property Mensaje_error() As String = ""
	Public Property Fec_agr() As Date = Date.Now
	Public Property IdProducto As Integer = 0


	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
