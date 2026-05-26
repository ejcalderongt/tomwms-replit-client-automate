Public Class clsBeI_nav_barras_rfid_mov
	Implements ICloneable

	Public Property IdRfidMovimiento() As Integer = 0
	Public Property IdRFIDEnc() As Integer = 0
	Public Property IdRfidTipoMov() As Integer = 0
	Public Property IdRfidStock() As Integer = 0
	Public Property Barra_epc() As String = ""
	Public Property Tagid() As String = ""
	Public Property IdBodega() As Integer = 0
	Public Property IdProductoBodega() As Integer = 0
	Public Property Lote() As String = ""
	Public Property IdOrdenCompraEnc() As Integer = 0
	Public Property IdPedidoEnc() As Integer = 0
	Public Property IdUbicacion() As Integer = 0
	Public Property Cantidad() As Integer = 0
	Public Property Fecha() As Date = New Date (1900,1,1)
	Public Property User_agr() As Integer = 0
	Public Property Fec_agr() As Date = Date.Now

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
