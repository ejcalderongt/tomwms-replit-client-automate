Public Class clsBeTrans_pe_ref_mi3
	Implements ICloneable

	Public Property Idpedidoencrefmi3() As Integer = 0
	Public Property Idpedidoenc() As Integer = 0
	Public Property Iddespachoenc() As Integer = 0
	Public Property Docnumtraslado() As Integer = 0
	Public Property Docentrytraslado() As Integer = 0
	Public Property Docnumentrega() As Integer = 0
	Public Property Docentryentrega() As Integer = 0
	Public Property Referencia_documento_origen() As String = ""
	Public Property Referencia_documento_destino() As String = ""
	Public Property Observacion() As String = ""
	Public Property Codigo_bodega_origen() As String = ""
	Public Property Codigo_bodega_destino() As String = ""
	Public Property Codigo_bodega_virtual() As String = ""
	Public Property Fec_agr() As Date = Date.Now
	Public Property Usr_agr() As String = ""

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
