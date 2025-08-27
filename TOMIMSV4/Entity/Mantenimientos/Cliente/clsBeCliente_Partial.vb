' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 09-05-2017
' ***********************************************************************
' <copyright file="clsBeCliente_Partial.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
partial Public Class clsBeCliente
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the tipo.
    ''' </summary>
    ''' <value>The tipo.</value>
    Public Property Tipo As New clsBeCliente_tipo
    ''' <summary>
    ''' Gets or sets the drecciones.
    ''' </summary>
    ''' <value>The drecciones.</value>
    Public Property Drecciones As List(Of clsBeCliente_direccion) = Nothing
    ''' <summary>
    ''' Gets or sets the tiempos.
    ''' </summary>
    ''' <value>The tiempos.</value>
    Public Property Tiempos As List(Of clsBeCliente_tiempos) = Nothing
    ''' <summary>
    ''' Gets or sets the empresa.
    ''' </summary>
    ''' <value>The empresa.</value>
    Public Property Empresa As clsBeEmpresa = New clsBeEmpresa()
    ''' <summary>
    ''' Gets or sets the propietario.
    ''' </summary>
    ''' <value>The propietario.</value>
    Public Property Propietario As clsBePropietarios = New clsBePropietarios()
    ''' <summary>
    ''' Gets or sets the cliente tipo.
    ''' </summary>
    ''' <value>The cliente tipo.</value>
    Public Property ClienteTipo As clsBeCliente_tipo = New clsBeCliente_tipo()

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
        If Tipo IsNot Nothing Then
            Tipo.Dispose()
            Tipo = Nothing
        End If
        If Empresa IsNot Nothing Then
            Empresa.Dispose()
            Empresa = Nothing
        End If
        If Propietario IsNot Nothing Then
            Propietario.Dispose()
            Propietario = Nothing
        End If
        If ClienteTipo IsNot Nothing Then
            ClienteTipo.Dispose()
            ClienteTipo = Nothing
        End If
    End Sub
#End Region

End Class
