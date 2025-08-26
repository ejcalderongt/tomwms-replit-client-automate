<Serializable>
Public Class clsBeTrans_tras_enc
    Implements ICloneable
    Implements IDisposable

    Public Property IdTrasladoEnc() As Integer = 0
    Public Property IdBodegaOrigen() As Integer = 0
    Public Property IdBodegaDestino() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdMuelleOrigen() As Integer = 0
    Public Property IdPiloto() As Integer = 0
    Public Property IdVehiculo() As Integer = 0
    Public Property IdRuta() As Integer = 0
    Public Property FechaTraslado() As Date = Date.Now
    Public Property Hora_ini() As Date = Date.Now
    Public Property Hora_fin() As Date = Date.Now
    Public Property Ubicacion() As String = ""
    Public Property Estado() As String = ""
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property No_documento() As Integer = 0
    Public Property Local() As Boolean = False
    Public Property Pallet_primero() As Boolean = False
    Public Property Anulado() As Boolean = False
    Public Property FechaEntrega() As Date = Date.Now
    Public Property Observacion() As String = ""
    Public Property HoraEntregaDesde() As Date = Date.Now
    Public Property HoraEntregaHasta() As Date = Date.Now
    Public Property NoGuia() As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdTrasladoEnc As Integer, ByVal IdBodegaOrigen As Integer, ByVal IdBodegaDestino As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdMuelleOrigen As Integer, ByVal IdPiloto As Integer, ByVal IdVehiculo As Integer, ByVal IdRuta As Integer, ByVal FechaTraslado As Date, ByVal hora_ini As Date, ByVal hora_fin As Date, ByVal ubicacion As String, ByVal estado As String, ByVal activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal no_documento As Integer, ByVal local As Boolean, ByVal pallet_primero As Boolean, ByVal anulado As Boolean, ByVal FechaEntrega As Date, ByVal Observacion As String, ByVal HoraEntregaDesde As Date, ByVal HoraEntregaHasta As Date, ByVal NoGuia As String)
        Me.IdTrasladoEnc = IdTrasladoEnc
        Me.IdBodegaOrigen = IdBodegaOrigen
        Me.IdBodegaDestino = IdBodegaDestino
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdMuelleOrigen = IdMuelleOrigen
        Me.IdPiloto = IdPiloto
        Me.IdVehiculo = IdVehiculo
        Me.IdRuta = IdRuta
        Me.FechaTraslado = FechaTraslado
        Me.Hora_ini = hora_ini
        Me.Hora_fin = hora_fin
        Me.Ubicacion = ubicacion
        Me.Estado = estado
        Me.Activo = activo
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.No_documento = no_documento
        Me.Local = local
        Me.Pallet_primero = pallet_primero
        Me.Anulado = anulado
        Me.FechaEntrega = FechaEntrega
        Me.Observacion = Observacion
        Me.HoraEntregaDesde = HoraEntregaDesde
        Me.HoraEntregaHasta = HoraEntregaHasta
        Me.NoGuia = NoGuia
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
#End Region
End Class
