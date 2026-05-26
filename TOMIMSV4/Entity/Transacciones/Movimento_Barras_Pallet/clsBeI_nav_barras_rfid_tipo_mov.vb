Public Class clsBeI_nav_barras_rfid_tipo_mov
	Implements ICloneable

	Public Property IdRfidTipoMov() As Integer = 0
	Public Property Codigo() As String = ""
	Public Property Nombre() As String = ""
	Public Property Signo() As Integer = 0
	Public Property Afecta_stock() As Boolean = False
	Public Property Activo() As Boolean = False
	Public Property User_agr() As Integer = 0
	Public Property Fec_agr() As Date = Date.Now
	Public Property User_mod() As Integer = 0
	Public Property Fec_mod() As Date = Date.Now

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
