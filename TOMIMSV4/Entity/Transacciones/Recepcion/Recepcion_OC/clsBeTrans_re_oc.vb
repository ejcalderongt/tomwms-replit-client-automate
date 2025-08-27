Public Class clsBeTrans_re_oc
    Implements ICloneable
    Implements IDisposable

    Public Property IdRecepcionOc() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property Recepcion_ciega() As Boolean = False
    Public Property Recepcion_manual() As Boolean = False
    Public Property No_docto() As String = ""
    Public Property Hora_ini_hh() As Date = Date.Now
    Public Property Hora_fin_hh() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property Firma_operador() As Byte() = Nothing

    Sub New()
    End Sub

    Sub New(ByRef IdRecepcionOc As Integer, ByVal IdRecepcionEnc As Integer, ByVal IdOrdenCompraEnc As Integer, ByVal recepcion_ciega As Boolean, ByVal recepcion_manual As Boolean, ByVal no_docto As String, ByVal hora_ini_hh As Date, ByVal hora_fin_hh As Date, ByVal user_agr As String, ByVal fec_agr As Date, ByVal firma_operador As Byte())
        Me.IdRecepcionOc = IdRecepcionOc
        Me.IdRecepcionEnc = IdRecepcionEnc
        Me.IdOrdenCompraEnc = IdOrdenCompraEnc
        Me.Recepcion_ciega = recepcion_ciega
        Me.Recepcion_manual = recepcion_manual
        Me.No_docto = no_docto
        Me.Hora_ini_hh = hora_ini_hh
        Me.Hora_fin_hh = hora_fin_hh
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.Firma_operador = firma_operador
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
