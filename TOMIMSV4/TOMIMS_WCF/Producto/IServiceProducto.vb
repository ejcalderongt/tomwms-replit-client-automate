Imports TOMIMS

<ServiceContract()>
Public Interface IServiceProducto

    <OperationContract()>
    Sub Insert(ByVal BeProducto As clsBeProducto,
                       ByVal ListBeProductoParametro As List(Of clsBeProducto_parametros),
                       ByVal ListBeProductoCodigosBarra As List(Of clsBeProducto_codigos_barra),
                       ByVal ListBeProductoPresentacion As List(Of clsBeProducto_Presentacion),
                       ByVal ListBeProductosSustitutos As List(Of clsBeProducto_sustituto),
                       ByVal ListBeProductoRellenado As List(Of clsBeProducto_rellenado),
                       ByVal ListBePresentacionTarima As List(Of clsBeProducto_presentacion_tarima),
                       ByVal ListBeProductoBodega As List(Of clsBeProducto_bodega),
                       ByVal ListBeConversionesPresentacion As List(Of clsBeProducto_presentaciones_conversiones))


    <OperationContract()>
    Sub Disable(ByVal BeProducto As clsBeProducto)

    <OperationContract()>
    Function Update(ByVal BeProducto As clsBeProducto) As Boolean

    <OperationContract()>
    Sub Delete(ByVal IdProducto As Integer)

    <OperationContract()>
    Function Get_All_Producto(ByVal Activo As Boolean) As List(Of clsBeProducto)

    <OperationContract()>
    Function Get_All_By_IdPropietario(ByVal ActivoDefault As Boolean, ByVal Activo As Boolean, ByVal IdBodega As Integer, ByVal IdPropietario As Integer) As List(Of clsBeProducto)

    <OperationContract()>
    Function Get_All_By_IdPropietarioBodega(ByVal ActivoDefault As Boolean, ByVal Activo As Boolean, ByVal IdBodega As Integer, ByVal IdPropietarioBodega As Integer) As List(Of clsBeProducto)

    <OperationContract()>
    Function Get_Single_By_IdProducto(ByVal IdProducto As Integer) As clsBeProducto

    <OperationContract()>
    Function Get_Single_By_IdProductoBodega(ByVal IdProductoBodega As Integer) As clsBeProducto

    <OperationContract()>
    Function Exist_By_Codigo_Barra(ByVal CodigoBarra As String) As Boolean

    <OperationContract()>
    Function Exist_Producto_By_Bodega(ByVal IdBodega As Integer, ByVal IdPropietarioBodega As Integer, ByVal CodigoProducto As String) As Boolean

    <OperationContract()>
    Function Exist_By_IdProducto(ByVal pIdProducto As Integer) As Boolean

    <OperationContract()>
    Function Exist_Stock_By_IdProducto(ByVal pIdProducto As Integer) As Boolean

    <OperationContract()>
    Function Get_IdProducto_By_Codigo(ByVal pCodigo As String) As Integer

    <OperationContract()>
    Function Get_Single_By_Codigo(ByVal pCodigo As String) As clsBeProducto

    <OperationContract()>
    Function GetByCodigoAndBodega(ByVal pCodigo As String, ByVal IdBodega As Integer) As clsBeProducto

    <OperationContract()>
    Function Get_IdProductoBodega_By_Codigo(ByVal pCodigo As String) As Integer

    <OperationContract()>
    Function MaxID() As Integer

    <OperationContract()>
    Function Get_Rep_Minimos_Y_Maximos_By_IdProducto(ByVal pIdProducto As Integer) As DataTable

    <OperationContract()>
    Function Get_Rep_Stock_Minimo() As List(Of clsBeStock.Revision)

    <OperationContract()>
    Function Get_All_Parametros_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_parametros)

    <OperationContract()>
    Function Get_All_Codigos_Barra_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_codigos_barra)

    <OperationContract()>
    Function Get_All_Presentaciones_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_Presentacion)

    <OperationContract()>
    Function Get_All_Productos_Sustitutos_By_IdProducto(ByVal pIdProductoOriginal As Integer) As List(Of clsBeProducto_sustituto)

    <OperationContract()>
    Function Get_All_Bodegas_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_bodega)

    <OperationContract()>
    Function Get_All_Presentaciones_Tarima_By_IdProducto(ByVal IdProducto As Integer) As List(Of clsBeProducto_presentacion_tarima)

    <OperationContract()>
    Function Get_All_Presentaciones_By_IdProducto(IdProducto As Integer, pIdBodega As Integer) As List(Of clsBeProducto_Presentacion)

    <OperationContract()>
    Function Get_All_Rellenado_Producto_By_IdProducto(IdProducto As Integer) As List(Of clsBeProducto_rellenado)

End Interface
