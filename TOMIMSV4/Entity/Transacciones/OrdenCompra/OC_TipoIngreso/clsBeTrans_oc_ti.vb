Public Class clsBeTrans_oc_ti
    Implements ICloneable
    Implements IDisposable

    Public Property IdTipoIngresoOC() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Es_devolucion() As Boolean = False
    Public Property Control_Poliza As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Requerir_Documento_Ref As Boolean = False
    Public Property Es_Poliza_Consolidada As Boolean = False
    Public Property Genera_Tarea_Ingreso As Boolean = False
    Public Property Requerir_Proveedor_Es_Bodega_WMS As Boolean = False
    Public Property Requerir_Documento_Ref_WMS As Boolean = False

    ''' <summary>
    ''' #EJC20220330_CEALSA: Si true, se traslada el parámetro a la tarea de recepción en el BOF
    ''' Para marcar el check, Escanear ubicación en recepción.
    ''' Si Escanear ubicación en recepción True en Rec.
    ''' HH exige una ubicación de recepción válida antes de continuar con el proceso.
    ''' </summary>
    ''' <returns></returns>
    Public Property Requerir_Ubic_Rec_Ingreso As Boolean = False

    ''' <summary>
    ''' #EJC20220504_CEALSA: Si true, en la pantalla de documento de ingreso, se exige se ingrese el campo referencia.
    ''' </summary>
    ''' <returns></returns>
    Public Property Exigir_Campo_Referencia As Boolean = False

    ''' <summary>
    ''' #EJC20220504: A petición de Carolina, si true, en I_NAV_TRANSACICONES_OUT se marcan los registros como enviados
    ''' utilizado en BYB para marcar los registros de bodega 2 (PT) como enviados.
    ''' </summary>
    ''' <returns></returns>
    Public Property Marcar_Registros_Enviados_MI3 As Boolean = False

    ''' <summary>
    ''' #EJC20220601: En tarea de recepción, si no se ha completado el documento, preguntar si se quiere dejar en backorder. Marcelo, Mercosal.
    ''' </summary>
    ''' <returns></returns>
    Public Property Preguntar_En_BackOrder As Boolean = False

    ''' <summary>
    ''' #EJC202303161024: Bloquear el lote para que no se modifique en la HH si están previamente definidos.
    ''' </summary>
    ''' <returns></returns>
    Public Property Bloquear_Lotes As Boolean = False

    ''' <summary>
    ''' #EJC20230508: Parámetro agregado para validar si se va a permitir o no agregar de un 
    ''' lote una cantidad mayor de lo ingresado en la tabla trans_oc_det_lote
    ''' </summary>
    ''' <returns></returns>
    Public Property Permitir_Excedente_Lotes As Boolean = False

    ''' <summary>
    ''' #EJC202310171614: Determina si un documento de ingreso es una importación
    ''' Se utiliza para la interface de SAP en BECO para enviar a bodega PNC el producto faltante.
    ''' </summary>
    ''' <returns></returns>
    Public Property Es_Importacion As Boolean = False

    ''' <summary>
    ''' #GT23102023: Determina si un documento recibe producto vencido o no
    ''' Se utiliza para la interface de SAP en BECO para recibir vencidos
    ''' </summary>
    ''' <returns></returns>
    Public Property Permitir_Vencido_Ingreso As Boolean = True

    ''' <summary>
    ''' #GT23102023: Determina si una recepción maneja un estado general para el documento
    ''' Se utiliza para la interface
    ''' </summary>
    ''' <returns></returns>
    Public Property IdProductoEstado As Integer = 0

    ''' <summary>
    ''' #GT23102023: Determina si una recepción tiene un propietario asociado a un estado
    ''' Se utiliza para la interface
    ''' </summary>
    ''' <returns></returns>
    Public Property IdPropietario As Integer = 0

    Sub New()
    End Sub


    Sub New(ByRef IdTipoIngresoOC As Integer,
            ByVal Nombre As String,
            ByVal es_devolucion As Boolean,
            ByVal user_agr As String,
            ByVal fec_agr As Date,
            ByVal user_mod As String,
            ByVal fec_mod As Date,
            ByVal activo As Boolean,
            ByVal Control_Poliza As Boolean,
            ByVal Requerir_Documento_Ref As Boolean)
        Me.IdTipoIngresoOC = IdTipoIngresoOC
        Me.Nombre = Nombre
        Me.Es_devolucion = es_devolucion
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
        Me.Control_Poliza = Control_Poliza
        Me.Requerir_Documento_Ref = Requerir_Documento_Ref
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
