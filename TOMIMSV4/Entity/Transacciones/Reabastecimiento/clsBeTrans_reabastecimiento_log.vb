Public Class clsBeTrans_reabastecimiento_log
    Implements ICloneable

    Public Property IdReabastecimientoLog() As Integer = 0
    Public Property IdRellenado() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdProducto() As Integer = 0
    Public Property Codigo_Producto() As String = ""
    Public Property Nombre_Producto() As String = ""
    Public Property IdUnidadMedidaBasica() As Integer = 0
    Public Property NombreUmBas() As String = ""
    Public Property IdPresentacion() As Integer = 0
    Public Property Presentacion() As String = ""
    Public Property Minimo() As Double = 0.0
    Public Property Maximo() As Double = 0.0
    Public Property IdProductoEstado() As Integer = 0
    Public Property Estado() As String = ""
    Public Property StockUMBas() As Double = 0.0
    Public Property ReservadoUmBas() As Double = 0.0
    Public Property DisponibleUMBas() As Double = 0.0
    Public Property Factor() As Double = 0.0
    Public Property FactorAbastecerCon() As Double = 0.0
    Public Property StockPres() As Double = 0.0
    Public Property ReservadoPres() As Double = 0.0
    Public Property DisponiblePres() As Double = 0.0
    Public Property Ubicacion() As String = ""
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property IdTipoAccion() As Integer = 0
    Public Property Activo() As Boolean = False
    Public Property IdPropietario() As Integer = 0
    Public Property Nombre_Propietario() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property IdUmBasAbastercerCon() As Integer = 0
    Public Property IdPresentacionAbastercerCon() As Integer = 0
    Public Property NombrePresentacionAbastecerCon() As String = ""
    Public Property Enviado() As Boolean = False
    Public Property Cancelado() As Boolean = False
    Public Property Fecha_Procesamiento_BOF() As Date = New Date(1900, 1, 1)
    Public Property Hora_Procesamiento_BOF() As Date = New Date(1900, 1, 1)
    Public Property IdOperadorBodega() As Integer = 0
    Public Property Procesado_HH() As Boolean = False
    Public Property Fecha_Procesamiento_HH() As Date = New Date(1900, 1, 1)
    Public Property Fecha_Generacion_Inexistencia As Date = New Date(1900, 1, 1)
    Public Property Hora_Generacion_Inexistencia As Date = New Date(1900, 1, 1)

    '#EJC20210303: Virtuales
    Public Property IsNew As Boolean = False
    Public Property Stock_Ubicacion As Double = 0
    Public Property Cantidad_A_Ubicar As Double = 0
    Public Property CantidadSFUbicDestino As Double = 0
    Public Property CantidadPresUbicDestino As Double = 0
    Public Property CantidadReservadaUbicDestino As Double = 0
    Public Property Stock_Inferior As Boolean = False
    Public Property Stock_Disponible As Double = 0
    Public Property Seleccionado As Boolean = False
    Public Property IdTareaUbicacionEnc As Integer = 0
    Public Property CantidadSFDispo As Double = 0
    Public Property CantidadPresDispo As Double = 0
    Public Property CantidadReservadaDispo As Double = 0
    Public Property IdOperadorDefecto As Integer = 0
    Public Property Operador As String = ""
    Public Property Pickeado() As Double = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
