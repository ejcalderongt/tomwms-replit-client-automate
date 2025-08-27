Public Class clsBePedidoDetalleGrid

    public property IdProducto As Integer
    public property IsNew As boolean = true
    public property Codigo As string = ""
    public property Descripcion As string = ""
    public property UMBasica As New clsBeUnidad_medida
    public property Presentacion As New clsBeProducto_Presentacion
    public property Estado As New clsbeproducto_estado
    public property CantDisp As double = 0
    public property PesoDisp As double = 0
    public property Cantidad As double = 0
    public property Peso As double = 0
    public property Precio As double = 0
    public property Total As double = 0
    public property IdPedidoDet As integer = 0
    public property IdStockRes As integer = 0
    public property NoDias As integer = 0
    public property FechaEspecifica As boolean = false
    public property Serie As string = ""

End Class
