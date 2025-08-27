Public Class clsBeStock_res
    Implements ICloneable
    Implements IDisposable

    Public Property IdStockRes() As Integer = 0
    Public Property IdTransaccion() As Integer = 0
    Public Property Indicador() As String = "UBI"
    Public Property IdPedidoDet() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property IdProductoEstado() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Lic_plate() As String = ""
    Public Property Serial() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Peso() As Double = 0.0
    Public Property Estado() As String = ""
    Public Property Fecha_ingreso() As Date = Date.Now
    Public Property Fecha_vence() As Date = Date.Now
    Public Property Uds_lic_plate() As Double = 0
    Public Property Ubicacion_ant() As String = ""
    Public Property No_bulto() As Integer = 0
    Public Property IdRecepcion() As Integer = 0
    Public Property IdPicking() As Integer = 0
    Public Property IdPedido() As Integer = 0
    Public Property IdDespacho() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Host() As String = ""
    Public Property añada() As Integer = 0
    Public Property Fecha_manufactura() As Date = Date.Now
    Public Property Atributo_Variante_1 As String = ""
    Public Property Pallet_no_estandar() As Boolean = False

    '#EJC20190313_0716PM
    ''' <summary>
    ''' Se utiliza ónicamente en el proceso de reserva de stock como bandera para determinar si se debe excluir el último lote despachado al cliente..
    ''' </summary>
    ''' <returns></returns>
    Public Property Control_Ultimo_Lote As Boolean = False
    Public Property Ultimo_Lote() As String = ""
    ''' <summary>
    ''' Codigo_Producto: Agregado por interface BYB
    ''' #EJC20211210_0041AM_ Carol está bostezando.
    ''' </summary>
    ''' <returns></returns>
    Public Property Codigo_Producto As String = ""

    ''' <summary>
    ''' No_Pedido: Agregado por interface BYB
    ''' #EJC20211210_0041AM_ Carol está bostezando.
    ''' </summary>
    ''' <returns></returns>
    Public Property No_Pedido As String

    ''' <summary>
    ''' #EJC20220329_BYB:En la solicitud de reserva de stock, se puede indicar la Ubicación con la que se quiere
    ''' abastecer el pedido, este IdUbicación proviene del Cliente
    ''' Se debe considerar si el PT, tiene definido que se debe aplicar la regla o no.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdUbicacionAbastecerCon As Integer = 0
    Public Property Talla As String = ""
    Public Property Color As String = ""
    Public Property IdProductoTallaColor As Integer = 0
    Sub New()
    End Sub

    Sub New(ByRef IdStockRes As Integer, ByVal IdTransaccion As Integer, ByVal Indicador As String, ByVal IdPedidoDet As Integer, ByVal IdStock As Integer,
            ByVal IdPropietarioBodega As Integer, ByVal IdProductoBodega As Integer, ByVal IdUbicacion As Integer, ByVal IdEstado As Integer,
            ByVal IdPresentacion As Integer, ByVal IdUnidadMedida As Integer, ByVal lote As String, ByVal lic_plate As Integer, ByVal serial As String,
            ByVal cantidad As Double, ByVal peso As Double, ByVal estado As String, ByVal fecha_ingreso As Date, ByVal fecha_vence As Date,
            ByVal uds_lic_plate As Double, ByVal ubicacion_ant As String, ByVal no_bulto As Integer, ByVal IdRecepcion As Integer, ByVal IdPicking As Integer,
            ByVal IdPedido As Integer, ByVal IdDespacho As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date,
            ByVal host As String, ByVal añada As Integer, ByVal fecha_manufactura As Date, ByVal Pallet_no_estandar As Boolean)
        Me.IdStockRes = IdStockRes
        Me.IdTransaccion = IdTransaccion
        Me.Indicador = Indicador
        Me.IdPedidoDet = IdPedidoDet
        Me.IdStock = IdStock
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdProductoBodega = IdProductoBodega
        Me.IdUbicacion = IdUbicacion
        Me.IdProductoEstado = IdEstado
        Me.IdPresentacion = IdPresentacion
        Me.IdUnidadMedida = IdUnidadMedida
        Me.Lote = lote
        Me.Lic_plate = lic_plate
        Me.Serial = serial
        Me.Cantidad = cantidad
        Me.Peso = peso
        Me.Estado = estado
        Me.Fecha_ingreso = fecha_ingreso
        Me.Fecha_vence = fecha_vence
        Me.Uds_lic_plate = uds_lic_plate
        Me.Ubicacion_ant = ubicacion_ant
        Me.No_bulto = no_bulto
        Me.IdRecepcion = IdRecepcion
        Me.IdPicking = IdPicking
        Me.IdPedido = IdPedido
        Me.IdDespacho = IdDespacho
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Host = host
        Me.añada = añada
        Me.Fecha_manufactura = fecha_manufactura
        Me.Pallet_no_estandar = Pallet_no_estandar
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

End Class
