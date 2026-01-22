Public Class clsBeStock_rec
    Implements ICloneable
    Implements IDisposable

    Public Property IdBodega As Integer = 0
    Public Property IdStockRec() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdProductoEstado() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property IdUbicacion_anterior() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property IdRecepcionDet() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdDespachoEnc() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Lic_plate() As String = ""
    Public Property Serial() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Fecha_Ingreso() As Date = Date.Now
    Public Property Fecha_vence() As Date = New Date(1900, 1, 1)
    Public Property Uds_lic_plate() As Double = 0
    Public Property No_bulto() As Integer = 0
    Public Property Fecha_Manufactura() As Date = Now.Date
    Public Property Añada() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Peso() As Double = 0.0
    Public Property Temperatura() As Double = 0.0
    Public Property Regularizado() As Boolean = False
    Public Property Fecha_regularizacion() As Date = Date.Now
    Public Property No_linea As Integer = 0 '#CKFK 20180113 11:27PM Agregue el No_linea -> #EJC20180114 Cambie =0
    Public Property Atributo_Variante_1 As String = ""
    Public Property Pallet_No_Estandar As Boolean = False
    Public Property Impreso As Boolean = False '#GT13052025 campo definido en la tabla pero no en la clase
    Public Property IdProductoTallaColor As Integer = 0
    Public Property Talla As String = ""
    Public Property Color As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdStockRec As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdProductoBodega As Integer, ByVal IdUnidadMedida As Integer, ByVal IdUbicacion As Integer,
            ByVal IdUbicacion_anterior As Integer, ByVal IdRecepcionEnc As Integer, ByVal IdRecepcionDet As Integer, ByVal IdPedidoEnc As Integer, ByVal IdPickingEnc As Integer,
            ByVal IdDespachoEnc As Integer, ByVal lote As String, ByVal lic_plate As Integer, ByVal serial As String, ByVal cantidad As Double, ByVal fecha_ingreso As Date,
            ByVal fecha_vence As Date, ByVal uds_lic_plate As Integer, ByVal no_bulto As String, ByVal fecha_manufactura As Date, ByVal añada As Integer, ByVal user_agr As String,
            ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal peso As Double, ByVal temperatura As Double, ByVal regularizado As Boolean,
            ByVal fecha_regularizacion As Date, No_Linea As Integer, Atributo_Variante_1 As String, ByVal IdProductoEstado As Integer, ByVal IdPresentacion As Integer,
            ByVal pallet_no_estandar As Boolean)
        Me.IdStockRec = IdStockRec
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdProductoBodega = IdProductoBodega
        Me.IdProductoEstado = IdProductoEstado
        Me.IdPresentacion = IdPresentacion
        Me.IdUnidadMedida = IdUnidadMedida
        Me.IdUbicacion = IdUbicacion
        Me.IdUbicacion_anterior = IdUbicacion_anterior
        Me.IdRecepcionEnc = IdRecepcionEnc
        Me.IdRecepcionDet = IdRecepcionDet
        Me.IdPedidoEnc = IdPedidoEnc
        Me.IdPickingEnc = IdPickingEnc
        Me.IdDespachoEnc = IdDespachoEnc
        Me.Lote = lote
        Me.Lic_plate = lic_plate
        Me.Serial = serial
        Me.Cantidad = cantidad
        Me.Fecha_Ingreso = fecha_ingreso
        Me.Fecha_vence = fecha_vence
        Me.Uds_lic_plate = uds_lic_plate
        Me.No_bulto = no_bulto
        Me.Fecha_Manufactura = fecha_manufactura
        Me.Añada = añada
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
        Me.Peso = peso
        Me.Temperatura = temperatura
        Me.Regularizado = regularizado
        Me.Fecha_regularizacion = fecha_regularizacion
        Me.Atributo_Variante_1 = Atributo_Variante_1
        Me.No_linea = No_Linea '#CKFK 20180113 11:27PM Agregue el No_linea
        Me.Pallet_No_Estandar = pallet_no_estandar
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
        If Presentacion IsNot Nothing Then
            Presentacion.Dispose()
            Presentacion = Nothing
        End If
        If ProductoEstado IsNot Nothing Then
            ProductoEstado.Dispose()
            ProductoEstado = Nothing
        End If
    End Sub
#End Region

End Class
