Public Class clsBeLog_verificacion_bof
	Implements ICloneable

	Public Property IdLogVerificacion() As Integer = 0
	Public Property IdBodega() As Integer = 0
	Public Property IdPedidoEnc() As Integer = 0
	Public Property IdPedidoDet() As Integer = 0
	Public Property IdPickingUbic() As Integer = 0
	Public Property IdPickingEnc() As Integer = 0
	Public Property IdPickingDet() As Integer = 0
	Public Property IdProductoBodega() As Integer = 0
	Public Property Sku() As String = ""
	Public Property Cantidad() As Double = 0.0
	Public Property IdMotivo() As Integer = 0
	Public Property IdEstado() As Integer = 0
	Public Property User_agr() As String = ""
	Public Property Fec_agr() As Date = Date.Now
	Public Property IdProductoTallaColor() As Integer = 0

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
