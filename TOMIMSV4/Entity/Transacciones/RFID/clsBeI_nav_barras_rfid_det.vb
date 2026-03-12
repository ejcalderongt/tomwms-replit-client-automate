Public Class clsBeI_nav_barras_rfid_det
	Implements ICloneable

	Public Property IdRFIDEnc() As Integer = 0
	Public Property Barra_epc() As String = ""
	Public Property Tagid() As String = ""
	Public Property IdDispositivo() As Date = Date.Now
	Public Property IdOperador() As Integer = 0

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
