Public Class clsBeTrans_verificacion_etiqueta
	Implements ICloneable

	Public Property IdVerificacionEtiqueta() As Integer = 0
	Public Property IdPickingUbic() As Integer = 0
	Public Property IdPickingEnc() As Integer = 0
	Public Property IdPickingDet() As Integer = 0
	Public Property IdStock() As Integer = 0
	Public Property IdPropietarioBodega() As Integer = 0
	Public Property IdProductoBodega() As Integer = 0
	Public Property IdProductoEstado() As Integer = 0
	Public Property IdPresentacion() As Integer = 0
	Public Property IdUnidadMedida() As Integer = 0
	Public Property IdPedidoEnc() As Integer = 0
	Public Property IdPedidoDet() As Integer = 0
	Public Property IdStockRes() As Integer = 0
	Public Property IdBodega() As Integer = 0
	Public Property IdOperadorBodega_Pickeo() As Integer = 0
	Public Property IdOperadorBodega_Verifico() As Integer = 0
	Public Property IdUbicacionTemporal() As Integer = 0
	Public Property IdOperadorBodega_Asignado() As Integer = 0
	Public Property IdProductoTallaColor() As Integer = 0
	Public Property Codigo_producto() As String = ""
	Public Property Nombre_producto() As String = ""
	Public Property Nombre_operador_pickeo() As String = ""
	Public Property Nombre_operador_verifico() As String = ""
	Public Property Nombre_cliente() As String = ""
	Public Property Codigo_talla() As String = ""
	Public Property Codigo_color() As String = ""
	Public Property Codigo_barra_etiqueta() As String = ""
	Public Property Lote() As String = ""
	Public Property Fecha_vence() As Date = Date.Now
	Public Property Lic_plate() As String = ""
	Public Property Peso_verificado() As Double = 0.0
	Public Property Cantidad_verificada() As Double = 0.0
	Public Property Fecha_verificado() As Date = Date.Now
	Public Property Referencia_pedido() As String = ""
	Public Property User_agr() As String = ""
	Public Property Fec_agr() As Date = Date.Now
	Public Property User_mod() As String = ""
	Public Property Fec_mod() As Date = Date.Now
	Public Property Activo() As Boolean = False
	Public Property ZPL_Etiqueta() As String = ""

	Sub New()
		'#EJC: Add your constructor here... 
	End Sub
	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return MyBase.MemberwiseClone()
	End Function

End Class
