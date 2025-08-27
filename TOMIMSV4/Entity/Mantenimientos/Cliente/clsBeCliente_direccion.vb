' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 08-04-2017
' ***********************************************************************
' <copyright file="clsBeCliente_direccion.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeCliente_direccion.
''' </summary>
''' <seealso cref="System.IDisposable" />
''' <seealso cref="System.ICloneable" />
Public Class clsBeCliente_direccion
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier direccion.
    ''' </summary>
    ''' <value>The identifier direccion.</value>
    Public Property IdDireccion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier cliente.
    ''' </summary>
    ''' <value>The identifier cliente.</value>
    Public Property IdCliente() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier municipio.
    ''' </summary>
    ''' <value>The identifier municipio.</value>
    Public Property IdMunicipio() As Integer = 0
    ''' <summary>
    ''' Gets or sets the avenida.
    ''' </summary>
    ''' <value>The avenida.</value>
    Public Property Avenida() As String = ""
    ''' <summary>
    ''' Gets or sets the calle.
    ''' </summary>
    ''' <value>The calle.</value>
    Public Property Calle() As String = ""
    ''' <summary>
    ''' Gets or sets the no_ casa.
    ''' </summary>
    ''' <value>The no_ casa.</value>
    Public Property No_Casa() As String = ""
    ''' <summary>
    ''' Gets or sets the zona.
    ''' </summary>
    ''' <value>The zona.</value>
    Public Property Zona() As String = ""
    ''' <summary>
    ''' Gets or sets the direccion.
    ''' </summary>
    ''' <value>The direccion.</value>
    Public Property Direccion() As String = ""
    ''' <summary>
    ''' Gets or sets the identifier region.
    ''' </summary>
    ''' <value>The identifier region.</value>
    Public Property IdRegion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the referencia.
    ''' </summary>
    ''' <value>The referencia.</value>
    Public Property Referencia() As String = ""
    ''' <summary>
    ''' Gets or sets the coordenada_x.
    ''' </summary>
    ''' <value>The coordenada_x.</value>
    Public Property Coordenada_x() As String = ""
    ''' <summary>
    ''' Gets or sets the coordenada_y.
    ''' </summary>
    ''' <value>The coordenada_y.</value>
    Public Property Coordenada_y() As String = ""
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeCliente_direccion"/> is local.
    ''' </summary>
    ''' <value><c>true</c> if local; otherwise, <c>false</c>.</value>
    Public Property Local() As Boolean = False
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
    ''' Gets or sets a value indicating whether this <see cref="clsBeCliente_direccion"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeCliente_direccion"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeCliente_direccion"/> class.
    ''' </summary>
    ''' <param name="IdDireccion">The identifier direccion.</param>
    ''' <param name="IdCliente">The identifier cliente.</param>
    ''' <param name="IdMunicipio">The identifier municipio.</param>
    ''' <param name="Avenida">The avenida.</param>
    ''' <param name="Calle">The calle.</param>
    ''' <param name="No_Casa">The no_ casa.</param>
    ''' <param name="Zona">The zona.</param>
    ''' <param name="Direccion">The direccion.</param>
    ''' <param name="IdRegion">The identifier region.</param>
    ''' <param name="Referencia">The referencia.</param>
    ''' <param name="coordenada_x">The coordenada_x.</param>
    ''' <param name="coordenada_y">The coordenada_y.</param>
    ''' <param name="Local">if set to <c>true</c> [local].</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    Sub New(ByRef IdDireccion As Integer, ByVal IdCliente As Integer, ByVal IdMunicipio As Integer, ByVal Avenida As String, ByVal Calle As String, ByVal No_Casa As String, ByVal Zona As String, ByVal Direccion As String, ByVal IdRegion As Integer, ByVal Referencia As String, ByVal coordenada_x As String, ByVal coordenada_y As String, ByVal Local As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdDireccion = IdDireccion
        Me.IdCliente = IdCliente
        Me.IdMunicipio = IdMunicipio
        Me.Avenida = Avenida
        Me.Calle = Calle
        Me.No_Casa = No_Casa
        Me.Zona = Zona
        Me.Direccion = Direccion
        Me.IdRegion = IdRegion
        Me.Referencia = Referencia
        Me.Coordenada_x = Coordenada_x
        Me.Coordenada_y = Coordenada_y
        Me.Local = Local
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
        Me.Activo = Activo
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
    End Sub
#End Region

End Class
