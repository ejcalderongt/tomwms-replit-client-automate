Public Class clsBeTrans_re_enc
    Implements ICloneable
    Implements IDisposable

    Public Property IdRecepcionEnc() As Integer = 0
    Public Property IdMuelle() As Integer = 0
    Public Property IdUbicacionRecepcion() As Integer = 0
    Public Property IdTipoTransaccion() As String = ""
    Public Property Fecha_recepcion() As Date = Date.Now
    Public Property Hora_ini_pc() As Date = Date.Now
    Public Property Hora_fin_pc() As Date = Date.Now
    Public Property Muestra_precio() As Boolean = False
    Public Property Estado() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Fecha_tarea() As Date = Date.Now
    Public Property Tomar_fotos() As Boolean = False
    Public Property Escanear_rec_ubic() As Boolean = False
    Public Property Para_por_codigo() As Boolean = False
    Public Property Observacion() As String = ""
    Public Property Firma_piloto() As Byte()
    Public Property Activo() As Boolean = False
    Public Property NoGuia() As String = ""
    Public Property CorreoEnviado() As Boolean = False
    Public Property Revision_Inconsistencia() As Boolean = False
    '#GT07052025: campo faltante que existe en la tabla
    Public Property bloqueada() As Boolean = False
    Public Property bloqueada_por() As String = ""
    Public Property IdUsuarioBloqueo() As Integer = 0
    Public Property IdMotivoAnulacionBodega As Integer = 0
    Public Property Habilitar_Stock As Boolean = False
    Public Property IdVehiculo As Integer = 0
    Public Property IdPiloto As Integer = 0
    Public Property No_Marchamo As String = ""
    Public Property Mostrar_Cantidad_Esperada As Boolean = True
    Public Property IdBodega As Integer = 0
    Public Property Carta_Cupo As String = ""
    Public Property No_Contenedor As String = ""
    Public Property IdEstado_Defecto_Recepcion As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdRecepcionEnc As Integer, ByVal IdMuelle As Integer, ByVal IdTipoTransaccion As String, ByVal fecha_recepcion As Date, ByVal hora_ini_pc As Date, ByVal hora_fin_pc As Date, ByVal muestra_precio As Boolean, ByVal estado As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal fecha_tarea As Date, ByVal tomar_fotos As Boolean, ByVal escanear_rec_ubic As Boolean, ByVal para_por_codigo As Boolean, ByVal observacion As String, ByVal firma_piloto As Byte(), ByVal activo As Boolean, noguia As String, correoenviado As Boolean, revision_inconsistencia As Boolean, bloqueada_por As String, IdUsuarioBloqueo As Integer, habilitar_stock As Boolean, ByVal IdVehiculo As Integer, ByVal IdPiloto As Integer, ByVal No_Marchamo As String)
        Me.IdRecepcionEnc = IdRecepcionEnc
        Me.IdMuelle = IdMuelle
        Me.IdTipoTransaccion = IdTipoTransaccion
        Me.Fecha_recepcion = fecha_recepcion
        Me.Hora_ini_pc = hora_ini_pc
        Me.Hora_fin_pc = hora_fin_pc
        Me.Muestra_precio = muestra_precio
        Me.Estado = estado
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Fecha_tarea = fecha_tarea
        Me.Tomar_fotos = tomar_fotos
        Me.Escanear_rec_ubic = escanear_rec_ubic
        Me.Para_por_codigo = para_por_codigo
        Me.Observacion = observacion
        Me.Firma_piloto = firma_piloto
        Me.Activo = activo
        Me.NoGuia = noguia
        Me.CorreoEnviado = correoenviado
        Me.Revision_Inconsistencia = revision_inconsistencia
        Me.bloqueada_por = bloqueada_por
        Me.IdUsuarioBloqueo = IdUsuarioBloqueo
        Me.Habilitar_Stock = habilitar_stock
        Me.IdVehiculo = IdVehiculo
        Me.IdPiloto = IdPiloto
        Me.No_Marchamo = No_Marchamo
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
        If OrdenCompraRec IsNot Nothing Then
            OrdenCompraRec.Dispose()
            OrdenCompraRec = Nothing
        End If
        If OrdenCompraRec IsNot Nothing Then
            OrdenCompraRec.Dispose()
            OrdenCompraRec = Nothing
        End If
        If OrdenCompraRec IsNot Nothing Then
            OrdenCompraRec.Dispose()
            OrdenCompraRec = Nothing
        End If
        If OrdenCompraRec IsNot Nothing Then
            OrdenCompraRec.Dispose()
            OrdenCompraRec = Nothing
        End If
        If OrdenCompraRec IsNot Nothing Then
            OrdenCompraRec.Dispose()
            OrdenCompraRec = Nothing
        End If
    End Sub
#End Region

End Class
