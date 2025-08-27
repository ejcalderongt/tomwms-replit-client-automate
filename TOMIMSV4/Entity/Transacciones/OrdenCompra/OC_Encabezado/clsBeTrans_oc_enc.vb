Public Class clsBeTrans_oc_enc
    Implements ICloneable
    Implements IDisposable

    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdProveedorBodega() As Integer = 0
    Public Property IdTipoIngresoOC() As Integer = 0
    Public Property IdEstadoOC() As Integer = 0
    Public Property IdMotivoDevolucion() As Integer = 0
    Public Property Fecha_Creacion() As Date = New Date(1900, 1, 1)
    Public Property Hora_Creacion() As Date = New Date(1900, 1, 1)
    ''' <summary>
    ''' #EJC20240606: DocNum SAP.
    ''' </summary>
    ''' <returns></returns>
    Public Property No_Documento() As String = ""
    Public Property User_Agr() As String = ""
    Public Property Fec_Agr() As Date = Now
    Public Property User_Mod() As String = ""
    Public Property Fec_Mod() As Date = New Date(1900, 1, 1)
    Public Property Procedencia() As String = ""
    Public Property No_Marchamo() As String = ""
    Public Property Referencia() As String = ""
    Public Property Observacion() As String = ""
    Public Property Control_Poliza() As Boolean = False
    Public Property Activo() As Boolean = False
    Public Property Fecha_Recepcion() As Date = New Date(1900, 1, 1)
    Public Property Hora_Inicio_Recepcion() As Date = New Date(1900, 1, 1)
    Public Property Hora_Fin_Recepcion() As Date = New Date(1900, 1, 1)
    Public Property IdMuelleRecepcion() As Integer = 0
    Public Property Programar_Recepcion() As Boolean = False
    Public Property IdMotivoAnulacionBodega() As Integer = 0
    Public Property Enviado_A_ERP As Boolean = False
    Public Property No_Ticket_TMS As String = ""
    Public Property IdNoDocumentoRef As Integer = 0

    '#EJC20190314_0701: Control de series y correlativos por documento en O.C.
    Public Property Serie As String = ""
    Public Property Correlativo As String = ""

    Public Property IdOperadorBodegaDefecto As Integer = 0

    ''' <summary>
    ''' Se utiliza para la gestión del acuerdo comercial relacionado a un proveedor en CEALSA.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdAcuerdoComercial As Integer = 0

    ''' <summary>
    ''' Si es una transferencia de otra bodega de WMS aquí estará contenido el IdDespachoEnc de la bodega origen.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDespachoEnc As Integer = 0

    ''' <summary>
    ''' #EJC20210427: Se utiliza para almacenar el número de documento de recepción del mdulo de bodega de NAV para la bodega de PT.
    ''' </summary>
    ''' <returns></returns>
    Public Property No_Documento_Recepcion_ERP As String = ""
    Public Property No_Documento_Devolucion() As String = ""
    Public Property IdPedidoEncDevolucion() As Integer = 0
    Public Property Push_To_NAV As Boolean = False
    Public Property No_Documento_Ubicacion_ERP As String = ""
    Public Property PutAway_Registrado As Boolean = False
    Public Property Codigo_Empresa_ERP As String = ""
    Public Property IdCampaña As Integer = 0
    Sub New()
    End Sub

    Sub New(ByRef IdOrdenCompraEnc As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdProveedorBodega As Integer, ByVal IdTipoIngresoOC As Integer, ByVal IdEstadoOC As Integer, ByVal IdMotivoDevolucion As Integer, ByVal Fecha_Creacion As Date, ByVal Hora_Creacion As Date, ByVal No_Documento As String, ByVal User_Agr As String, ByVal Fec_Agr As Date, ByVal User_Mod As String, ByVal Fec_Mod As Date, ByVal Procedencia As String, ByVal No_Marchamo As String, ByVal Referencia As String, ByVal Observacion As String, ByVal Control_Poliza As Boolean, ByVal Activo As Boolean, ByVal Fecha_Recepcion As Date, ByVal Hora_Inicio_Recepcion As Date, ByVal Hora_Fin_Recepcion As Date, ByVal IdMuelleRecepcion As Integer, ByVal Programar_Recepcion As Boolean, ByVal IdMotivoAnulacionBodega As Integer)
        Me.IdOrdenCompraEnc = IdOrdenCompraEnc
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdProveedorBodega = IdProveedorBodega
        Me.IdTipoIngresoOC = IdTipoIngresoOC
        Me.IdEstadoOC = IdEstadoOC
        Me.IdMotivoDevolucion = IdMotivoDevolucion
        Me.Fecha_Creacion = Fecha_Creacion
        Me.Hora_Creacion = Hora_Creacion
        Me.No_Documento = No_Documento
        Me.User_Agr = User_Agr
        Me.Fec_Agr = Fec_Agr
        Me.User_Mod = User_Mod
        Me.Fec_Mod = Fec_Mod
        Me.Procedencia = Procedencia
        Me.No_Marchamo = No_Marchamo
        Me.Referencia = Referencia
        Me.Observacion = Observacion
        Me.Control_Poliza = Control_Poliza
        Me.Activo = Activo
        Me.Fecha_Recepcion = Fecha_Recepcion
        Me.Hora_Inicio_Recepcion = Hora_Inicio_Recepcion
        Me.Hora_Fin_Recepcion = Hora_Fin_Recepcion
        Me.IdMuelleRecepcion = IdMuelleRecepcion
        Me.Programar_Recepcion = Programar_Recepcion
        Me.IdMotivoAnulacionBodega = IdMotivoAnulacionBodega
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
