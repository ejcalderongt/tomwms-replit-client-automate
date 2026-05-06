Public Class clsBeTrans_inv_ciclico_rfid
	Implements ICloneable

	Public Property Idinvciclico() As Integer = 0
	Public Property Idinventarioenc() As Integer = 0
	Public Property IdPallet() As Integer = 0
	Public Property Codigo() As String = ""
	Public Property Nombre() As String = ""
	Public Property Lote() As String = ""
	Public Property Codigo_Barra() As String = ""
	Public Property SSCC() As String = ""
	Public Property GTIN() As String = ""
	Public Property Fecha_Produccion() As Date = Date.Now
	Public Property IdProductoBodega() As Integer = 0
	Public Property User_agr() As String = ""
	Public Property Fec_agr() As Date = Date.Now
	Public Property User_mod() As String = ""
	Public Property Fec_mod() As Date = Date.Now
	Public Property IdOperador() As Integer = 0
	Public Property Cantidad() As Integer = 0
	Public Property EsPallet() As Boolean = False

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
