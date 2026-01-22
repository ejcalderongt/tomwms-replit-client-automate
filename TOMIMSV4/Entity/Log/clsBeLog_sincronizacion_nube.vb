Public Class clsBeLog_sincronizacion_nube
	Implements ICloneable

	Public Property IdLog() As Integer = 0
	Public Property Fecha_sincronizacion() As Date = Date.Now
	Public Property Registros_enviados() As Integer = 0
	Public Property Estado() As String = ""
	Public Property Mensaje_error() As String = ""
	Public Property Tiempo_de_envio() As Integer = 0
	Public Property User_agr() As String = ""
	Public Property Fec_agr() As Date = Date.Now
	Public Property Entidad As String = ""
	Sub New()
		'#EJC: Add your constructor here... 
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
