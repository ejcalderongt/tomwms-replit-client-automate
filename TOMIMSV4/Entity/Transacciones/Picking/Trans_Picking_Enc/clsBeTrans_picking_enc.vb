Public Class clsBeTrans_picking_enc
    Implements ICloneable
    Implements IDisposable
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdUbicacionPicking() As Integer = 0
    Public Property Fecha_picking() As Date = Date.Now
    Public Property Hora_ini() As Date = Date.Now
    Public Property Hora_fin() As Date = Date.Now
    Public Property Estado() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Detalle_operador() As Boolean = False
    Public Property Activo() As Boolean = False
    Public Property Estado_Preparacion As String = ""
    Public Property Fecha_Inicio_Preparacion As Date = New Date(1900, 1, 1)
    Public Property Fecha_Fin_Preparacion As Date = New Date(1900, 1, 1)

    ''' <summary>
    ''' #EJC20200123
    ''' Indica si en el proceso de picking en la HH se debe marcar autom�ticamente como verificada la cantidad.
    ''' </summary>
    ''' <returns></returns>
    Public Property verifica_auto() As Boolean = False

    ''' <summary>
    ''' #EJC20200123
    ''' True: Indica que la tarea de picking fue procesada autom�ticamente desde el BOF.
    ''' False: Si la tarea de picking fue procesada por la HH
    ''' </summary>
    ''' <returns></returns>
    Public Property procesado_bof As Boolean = False

    ''' <summary>
    ''' #EJC20210805A: Indica si un picking debe ser preparado, antes de despachar.
    ''' </summary>
    ''' <returns></returns>
    Public Property Requiere_Preparacion As Boolean = False

    ''' <summary>
    ''' #EJC20210805B: Indica si la preparación es Granel o Tarima.
    ''' </summary>
    ''' <returns></returns>
    Public Property Tipo_Preparacion As String = ""
    '#AT20220511 Agrego este nuevo campo para mostrarse en la HH
    Public Property Referencia As String = ""

    ''' <summary>
    ''' #CKFK20231027
    ''' Indica si en el proceso de verificación en la HH se debe permitir tomar fotografias.
    ''' </summary>
    ''' <returns></returns>
    Public Property Fotografia_Verificacion() As Boolean = False
    ''' <summary>
    ''' #EJC20240609: Muelle.
    ''' </summary>
    ''' <returns></returns>   
    Public Property IdBodegaMuelle() As Integer = 0

    Public Property IdPrioridadPicking() As Integer = 0
    Public Property Observacion As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdPickingEnc As Integer, ByVal IdBodega As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdUbicacionPicking As Integer, ByVal fecha_picking As Date, ByVal hora_ini As Date, ByVal hora_fin As Date, ByVal estado As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal detalle_operador As Boolean, ByVal activo As Boolean, ByVal verifica_auto As Boolean)
        Me.IdPickingEnc = IdPickingEnc
        Me.IdBodega = IdBodega
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdUbicacionPicking = IdUbicacionPicking
        Me.Fecha_picking = fecha_picking
        Me.Hora_ini = hora_ini
        Me.Hora_fin = hora_fin
        Me.Estado = estado
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Detalle_operador = detalle_operador
        Me.Activo = activo
        Me.verifica_auto = verifica_auto
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
