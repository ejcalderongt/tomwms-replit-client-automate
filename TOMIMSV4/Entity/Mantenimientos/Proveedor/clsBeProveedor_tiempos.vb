' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 08-04-2017
' ***********************************************************************
' <copyright file="clsBeProveedor_tiempos.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeProveedor_tiempos.
''' </summary>
''' <seealso cref="System.ICloneable" />
''' <seealso cref="System.IDisposable" />
Public Class clsBeProveedor_tiempos
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier tiempo proveedor.
    ''' </summary>
    ''' <value>The identifier tiempo proveedor.</value>
    Public Property IdTiempoProveedor() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier proveedor.
    ''' </summary>
    ''' <value>The identifier proveedor.</value>
    Public Property IdProveedor() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier familia.
    ''' </summary>
    ''' <value>The identifier familia.</value>
    Public Property IdFamilia() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier clasificacion.
    ''' </summary>
    ''' <value>The identifier clasificacion.</value>
    Public Property IdClasificacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the familia.
    ''' </summary>
    ''' <value>The familia.</value>
    Public Property Familia As clsBeProducto_familia
    ''' <summary>
    ''' Gets or sets the clasificacion.
    ''' </summary>
    ''' <value>The clasificacion.</value>
    Public Property Clasificacion As clsBeProducto_clasificacion
    ''' <summary>
    ''' Gets or sets the dias_ local.
    ''' </summary>
    ''' <value>The dias_ local.</value>
    Public Property Dias_Local() As Integer = 0
    ''' <summary>
    ''' Gets or sets the dias_ exterior.
    ''' </summary>
    ''' <value>The dias_ exterior.</value>
    Public Property Dias_Exterior() As Integer = 0
    ''' <summary>
    ''' Gets or sets the user_agr.
    ''' </summary>
    ''' <value>The user_agr.</value>
    Public Property User_agr() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_agr.
    ''' </summary>
    ''' <value>The fec_agr.</value>
    Public Property Fec_agr() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the user_mod.
    ''' </summary>
    ''' <value>The user_mod.</value>
    Public Property User_mod() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_mod.
    ''' </summary>
    ''' <value>The fec_mod.</value>
    Public Property Fec_mod() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProveedor_tiempos"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProveedor_tiempos"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProveedor_tiempos"/> class.
    ''' </summary>
    ''' <param name="IdTiempoProveedor">The identifier tiempo proveedor.</param>
    ''' <param name="IdProveedor">The identifier proveedor.</param>
    ''' <param name="IdFamilia">The identifier familia.</param>
    ''' <param name="IdClasificacion">The identifier clasificacion.</param>
    ''' <param name="Dias_Local">The dias_ local.</param>
    ''' <param name="Dias_Exterior">The dias_ exterior.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    Sub New(ByRef IdTiempoProveedor As Integer, ByVal IdProveedor As Integer, ByVal IdFamilia As Integer, ByVal IdClasificacion As Integer, ByVal Dias_Local As Integer, ByVal Dias_Exterior As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdTiempoProveedor = IdTiempoProveedor
        Me.IdProveedor = IdProveedor
        Me.IdFamilia = IdFamilia
        Me.IdClasificacion = IdClasificacion
        Me.Dias_Local = Dias_Local
        Me.Dias_Exterior = Dias_Exterior
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

#Region "IDisposable Support"
    ''' <summary>
    ''' The disposed value
    ''' </summary>
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    ''' <summary>
    ''' Releases unmanaged and - optionally - managed resources.
    ''' </summary>
    ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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
    ''' <summary>
    ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
        If Familia IsNot Nothing Then
            Familia.Dispose()
            Familia = Nothing
        End If
    End Sub
#End Region

End Class
