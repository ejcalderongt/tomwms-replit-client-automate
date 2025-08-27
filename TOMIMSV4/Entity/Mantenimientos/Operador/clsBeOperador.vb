
Public Class clsBeOperador
    Implements ICloneable
    Implements IDisposable

    Public Property IdOperador() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdRolOperador() As Integer = 0
    Public Property IdJornada() As Integer = 0
    Public Property Nombres() As String = ""
    Public Property Apellidos() As String = ""
    Public Property Direccion() As String = ""
    Public Property Telefono() As String = ""
    Public Property Codigo() As String = ""
    Public Property Clave() As String = ""
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Costo_hora() As Double = 0.0
    Public Property Usa_hh() As Boolean = False
    Public Property Foto() As Byte() = Nothing

    '#20200407_WMS_Features on Git.
    Public Property Recibe As Boolean = False
    Public Property Ubica As Boolean = False
    Public Property Transporta As Boolean = False
    Public Property Pickea As Boolean = False
    Public Property Verifica As Boolean = False

    ''' <summary>
    ''' #EJC20220613:Determinar si maneja o no  montacarga, importante para la asignación automática de líneas de picking.
    ''' </summary>
    ''' <returns></returns>
    Public Property Montacarga As Boolean = False

    ''' <summary>
    ''' #GT26012024: campo para determinar que el operador trabaja para WMS y no es de uso operativo
    ''' </summary>
    ''' <returns></returns>
    Public Property Sistema As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdOperador As Integer, ByVal IdEmpresa As Integer, ByVal IdRolOperador As Integer, ByVal IdJornada As Integer, ByVal nombres As String, ByVal apellidos As String, ByVal direccion As String, ByVal telefono As String, ByVal codigo As String, ByVal clave As String, ByVal activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal costo_hora As Double, ByVal usa_hh As Boolean, ByVal foto As Byte())
        Me.IdOperador = IdOperador
        Me.IdEmpresa = IdEmpresa
        Me.IdRolOperador = IdRolOperador
        Me.IdJornada = IdJornada
        Me.Nombres = nombres
        Me.Apellidos = apellidos
        Me.Direccion = direccion
        Me.Telefono = telefono
        Me.Codigo = codigo
        Me.Clave = clave
        Me.Activo = activo
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Costo_hora = costo_hora
        Me.Usa_hh = usa_hh
        Me.Foto = foto
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
