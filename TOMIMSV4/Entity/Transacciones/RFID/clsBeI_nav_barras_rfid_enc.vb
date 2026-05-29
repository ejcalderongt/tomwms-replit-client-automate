Public Class clsBeI_nav_barras_rfid_enc
	Implements ICloneable

	Public Property IdRFIDEnc() As Integer = 0
	Public Property IdOrdenCompraEnc() As Integer = 0
	Public Property IdRecepcionEnc() As Integer = 0
	Public Property IdBodega() As Integer = 0
	Public Property Fec_agr() As Date = Date.Now
	Public Property Fec_mod() As Date = Date.Now
	Public Property Estado() As String = ""
	Public Property Tipo() As Integer = 0
	Public Property IdProveedor() As Integer = 0
	Public Property IdCliente() As Integer = 0
	Public Property IdPedidoEnc() As Integer = 0

	Public Property Detalle() As New List(Of clsBeI_nav_barras_rfid_det)

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
