Public Class clsBeTrans_pe_tipo
    Implements ICloneable
    Implements IDisposable

    Public Property IdTipoPedido() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Preparar() As Boolean = False
    Public Property Verificar() As Boolean = False
    Public Property ReservaStock() As Boolean = False
    Public Property ImprimeBarrasPicking() As Boolean = False
    Public Property ImprimeBarrasPacking() As Boolean = False
    Public Property Control_Poliza() As Boolean = False
    Public Property Requerir_Documento_Ref As Boolean = False
    Public Property Generar_pedido_ingreso_bodega_destino As Boolean = False
    Public Property IdTipoIngresoOC As Integer = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
    Public Property Trasladar_Lotes_Doc_Ingreso() As Boolean = False
    Public Property Requerir_Cliente_Es_Bodega_WMS() As Boolean = False
    Public Property Activo As Boolean = False
    Public Property Marcar_Registros_Enviados_MI3 As Boolean = False

    ''' <summary>
    ''' #EJC20220428: Determina si se genera la tarea de recepci�n en la bodega destino.
    ''' </summary>
    ''' <returns></returns>
    Public Property Generar_Recepcion_Auto_Bodega_Destino As Boolean = False

    ''' <summary>
    ''' #EJC20220428: Determina si se debe recibir (push to stock) autom�ticamente el producto en bodega de destino (si es de WMS).
    ''' </summary>
    ''' <returns></returns>
    Public Property Recibir_Producto_Auto_Bodega_Destino As Boolean = False

    ''' <summary>
    ''' #EJC20220512: BYB: Indica si el detalle del documento de salida tiene asociaci�n con cliente.
    ''' </summary>
    ''' <returns></returns>
    Public Property Control_Cliente_En_Detalle As Boolean = False

    ''' <summary>
    ''' #EJC20220525: Define si permite el despacho parcial de un pedido.
    ''' MARCELO, Mercosal.
    ''' </summary>
    ''' <returns></returns>
    Public Property Permitir_Despacho_Parcial As Boolean = False

    ''' <summary>
    ''' #EJC20220627: 
    ''' Se busca a trav�s de este par�metro que ciertos pedidos de cliente, 
    ''' no puedan tener despachos m�ltiples o parciales, 
    ''' porque en el ERP ya fueron cerrados y no pueden ser procesados nuevamente
    ''' </summary>
    ''' <returns></returns>
    Public Property Permitir_Despacho_Multiple As Boolean = True

    ''' <summary>
    ''' #CKFK20231027
    ''' Indica si en el proceso de verificaci�n en la HH se debe permitir tomar fotografias.
    ''' </summary>
    ''' <returns></returns>
    Public Property Fotografia_Verificacion() As Boolean = False

    ''' <summary>
    ''' #CKFK20240620
    ''' Indica si el documento es de tipo devolución.
    ''' </summary>
    ''' <returns></returns>
    Public Property Es_Devolucion() As Boolean = False

    Public Property Empaque_Tarima As Boolean = False

    Public Property IdProductoEstado() As Integer = 0

    Public Property IdPropietario() As Integer = 0

    '#GT15042025: campos para pickear en muelle
    Public Property Mover_Producto_Zona_Muelle As Boolean = False
    Public Property Escanear_Muelle_Picking As Boolean = False

    Public Property Transferir_Ubicacion As Boolean = False

    Public Property Verificar_con_imagen As Boolean = False
    Public Property Genera_Guia_Remision As Boolean = False
    Public Property Asignar_Todos_Operadores As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdTipoPedido As Integer, ByVal Nombre As String, ByVal Descripcion As String, ByVal Preparar As Boolean, ByVal Verificar As Boolean, ByVal ReservaStock As Boolean, ByVal ImprimeBarrasPicking As Boolean, ByVal ImprimeBarrasPacking As Boolean)
        Me.IdTipoPedido = IdTipoPedido
        Me.Nombre = Nombre
        Me.Descripcion = Descripcion
        Me.Preparar = Preparar
        Me.Verificar = Verificar
        Me.ReservaStock = ReservaStock
        Me.ImprimeBarrasPicking = ImprimeBarrasPicking
        Me.ImprimeBarrasPacking = ImprimeBarrasPacking
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
