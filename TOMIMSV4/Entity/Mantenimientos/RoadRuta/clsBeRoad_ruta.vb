Public Class clsBeRoad_ruta
    Implements ICloneable
    Implements IDisposable

    Public Property IdRuta() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdUbicacionTransito() As Integer = 0
    Public Property CODIGO() As String = ""
    Public Property NOMBRE() As String = ""
    Public Property ACTIVO() As String = ""
    Public Property VENDEDOR() As String = ""
    Public Property VENTA() As String = ""
    Public Property FORANIA() As String = ""
    Public Property SUCURSAL() As String = ""
    Public Property TIPO() As String = ""
    Public Property SUBTIPO() As String = ""
    Public Property BODEGA() As String = ""
    Public Property SUBBODEGA() As String = ""
    Public Property DESCUENTO() As String = ""
    Public Property BONIF() As String = ""
    Public Property KILOMETRAJE() As String = ""
    Public Property IMPRESION() As String = ""
    Public Property RECIBOPROPIO() As String = ""
    Public Property CELULAR() As String = ""
    Public Property RENTABIL() As String = ""
    Public Property OFERTA() As String = ""
    Public Property PERCRENT() As Double = 0.0
    Public Property PASARCREDITO() As String = ""
    Public Property TECLADO() As String = ""
    Public Property EDITDEVPREC() As String = ""
    Public Property EDITDESC() As String = ""
    Public Property PARAMS() As String = ""
    Public Property SEMANA() As Integer = 0
    Public Property OBJANO() As Integer = 0
    Public Property OBJMES() As Integer = 0
    Public Property SYNCFOLD() As String = ""
    Public Property WLFOLD() As String = ""
    Public Property FTPFOLD() As String = ""
    Public Property EMAIL() As String = ""
    Public Property LASTIMP() As Integer = 0
    Public Property LASTCOM() As Integer = 0
    Public Property LASTEXP() As Integer = 0
    Public Property IMPSTAT() As String = ""
    Public Property EXPSTAT() As String = ""
    Public Property COMSTAT() As String = ""
    Public Property PARAM1() As String = ""
    Public Property PARAM2() As String = ""
    Public Property PESOLIM() As Double = 0.0
    Public Property INTERVALO_MAX() As Integer = 0
    Public Property LECTURAS_VALID() As Integer = 0
    Public Property INTENTOS_LECT() As Integer = 0
    Public Property HORA_INI() As Integer = 0
    Public Property HORA_FIN() As Integer = 0
    Public Property APLICACION_USA() As Integer = 0
    Public Property PUERTO_GPS() As Integer = 0
    Public Property ES_RUTA_OFICINA() As Boolean = False
    Public Property DILUIR_BON() As Boolean = False
    Public Property PREIMPRESION_FACTURA() As Boolean = False
    Sub New()
    End Sub
    Sub New(ByRef IdRuta As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdUbicacionTransito As Integer, ByVal CODIGO As String, ByVal NOMBRE As String, ByVal ACTIVO As String, ByVal VENDEDOR As String, ByVal VENTA As String, ByVal FORANIA As String, ByVal SUCURSAL As String, ByVal TIPO As String, ByVal SUBTIPO As String, ByVal BODEGA As String, ByVal SUBBODEGA As String, ByVal DESCUENTO As String, ByVal BONIF As String, ByVal KILOMETRAJE As String, ByVal IMPRESION As String, ByVal RECIBOPROPIO As String, ByVal CELULAR As String, ByVal RENTABIL As String, ByVal OFERTA As String, ByVal PERCRENT As Double, ByVal PASARCREDITO As String, ByVal TECLADO As String, ByVal EDITDEVPREC As String, ByVal EDITDESC As String, ByVal PARAMS As String, ByVal SEMANA As Integer, ByVal OBJANO As Integer, ByVal OBJMES As Integer, ByVal SYNCFOLD As String, ByVal WLFOLD As String, ByVal FTPFOLD As String, ByVal EMAIL As String, ByVal LASTIMP As Integer, ByVal LASTCOM As Integer, ByVal LASTEXP As Integer, ByVal IMPSTAT As String, ByVal EXPSTAT As String, ByVal COMSTAT As String, ByVal PARAM1 As String, ByVal PARAM2 As String, ByVal PESOLIM As Double, ByVal INTERVALO_MAX As Integer, ByVal LECTURAS_VALID As Integer, ByVal INTENTOS_LECT As Integer, ByVal HORA_INI As Integer, ByVal HORA_FIN As Integer, ByVal APLICACION_USA As Integer, ByVal PUERTO_GPS As Integer, ByVal ES_RUTA_OFICINA As Boolean, ByVal DILUIR_BON As Boolean, ByVal PREIMPRESION_FACTURA As Boolean)
        Me.IdRuta = IdRuta
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdUbicacionTransito = IdUbicacionTransito
        Me.CODIGO = CODIGO
        Me.NOMBRE = NOMBRE
        Me.ACTIVO = ACTIVO
        Me.VENDEDOR = VENDEDOR
        Me.VENTA = VENTA
        Me.FORANIA = FORANIA
        Me.SUCURSAL = SUCURSAL
        Me.TIPO = TIPO
        Me.SUBTIPO = SUBTIPO
        Me.BODEGA = BODEGA
        Me.SUBBODEGA = SUBBODEGA
        Me.DESCUENTO = DESCUENTO
        Me.BONIF = BONIF
        Me.KILOMETRAJE = KILOMETRAJE
        Me.IMPRESION = IMPRESION
        Me.RECIBOPROPIO = RECIBOPROPIO
        Me.CELULAR = CELULAR
        Me.RENTABIL = RENTABIL
        Me.OFERTA = OFERTA
        Me.PERCRENT = PERCRENT
        Me.PASARCREDITO = PASARCREDITO
        Me.TECLADO = TECLADO
        Me.EDITDEVPREC = EDITDEVPREC
        Me.EDITDESC = EDITDESC
        Me.PARAMS = PARAMS
        Me.SEMANA = SEMANA
        Me.OBJANO = OBJANO
        Me.OBJMES = OBJMES
        Me.SYNCFOLD = SYNCFOLD
        Me.WLFOLD = WLFOLD
        Me.FTPFOLD = FTPFOLD
        Me.EMAIL = EMAIL
        Me.LASTIMP = LASTIMP
        Me.LASTCOM = LASTCOM
        Me.LASTEXP = LASTEXP
        Me.IMPSTAT = IMPSTAT
        Me.EXPSTAT = EXPSTAT
        Me.COMSTAT = COMSTAT
        Me.PARAM1 = PARAM1
        Me.PARAM2 = PARAM2
        Me.PESOLIM = PESOLIM
        Me.INTERVALO_MAX = INTERVALO_MAX
        Me.LECTURAS_VALID = LECTURAS_VALID
        Me.INTENTOS_LECT = INTENTOS_LECT
        Me.HORA_INI = HORA_INI
        Me.HORA_FIN = HORA_FIN
        Me.APLICACION_USA = APLICACION_USA
        Me.PUERTO_GPS = PUERTO_GPS
        Me.ES_RUTA_OFICINA = ES_RUTA_OFICINA
        Me.DILUIR_BON = DILUIR_BON
        Me.PREIMPRESION_FACTURA = PREIMPRESION_FACTURA
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
