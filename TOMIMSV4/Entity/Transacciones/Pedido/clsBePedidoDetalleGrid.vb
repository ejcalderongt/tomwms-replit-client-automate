Public Class clsBePedidoDetalleGrid

    public property IdProducto as Integer
    public property IsNew as boolean = true
    public property Codigo as string = ""
    public property Descripcion as string = ""
    public property UMBasica as string = ""
    public property Presentacion as clsBeProducto_Presentacion
    public property Estado as clsbeproducto_estado
    public property CantDisp as double = 0
    public property PesoDisp as double = 0
    public property Cantidad as double = 0
    public property Peso as double = 0
    public property Precio as double = 0
    public property Total as double = 0
    public property IdPedidoDet as integer = 0
    public property IdStockRes as integer = 0
    public property NoDias as integer = 0
    public property FechaEspecifica as boolean = false
    public property Serie as string = ""

End Class
