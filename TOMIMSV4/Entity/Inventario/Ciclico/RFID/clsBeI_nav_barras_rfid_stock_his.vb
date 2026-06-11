Public Class clsBeI_nav_barras_rfid_stock_his
	Implements ICloneable

	Public Property IdRfidStockHis() As Integer = 0
	Public Property IdRfidStock() As Integer = 0
	Public Property Barra_epc() As String = ""
	Public Property Tagid() As String = ""
	Public Property IdBodega() As Integer = 0
	Public Property IdProductoBodega() As Integer = 0
	Public Property Lote() As String = ""
	Public Property Cantidad() As Integer = 0
	Public Property IdUbicacion() As Integer = 0
	Public Property IdRFIDEncOrigen() As Integer = 0
	Public Property IdOrdenCompraEnc() As Integer = 0
	Public Property IdPedidoEnc() As Integer = 0
	Public Property IdRFIDEncSalida() As Integer = 0
	Public Property Fec_Salida() As Date = Date.Now
	Public Property Activo() As Boolean = False
	Public Property User_agr() As Integer = 0
	Public Property Fec_agr() As Date = Date.Now
	Public Property User_mod() As Integer = 0
	Public Property Fec_mod() As Date = Date.Now
	Public Property Motivo() As String = ""

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
