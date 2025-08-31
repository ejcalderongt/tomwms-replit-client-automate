Public Class clsBeBodega : Inherits clsBeBodegaBase
    Implements ICloneable
    Implements IDisposable

    Public Property Codigo_barra() As String = ""
    Public Property Nombre_comercial() As String = ""
    Public Property Direccion() As String = ""
    Public Property Telefono() As String = ""
    Public Property Email() As String = ""
    Public Property Encargado() As String = ""
    Public Property Ubic_recepcion() As String = ""
    Public Property Ubic_picking() As String = ""
    Public Property Ubic_despacho() As String = ""
    Public Overloads Property Ubic_merma() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Coordenada_x() As String = ""
    Public Property Coordenada_y() As String = ""
    Public Property Largo() As Double = 0.0
    Public Property Ancho() As Double = 0.0
    Public Property Alto() As Double = 0.0
    Public Property Reservar_stocks_por_linea() As Boolean = False
    Public Property Rechazar_pedido_por_stock() As Boolean = False
    Public Property IdTipoTransaccion() As String = ""
    Public Property Zoom() As Double = 0.0
    Public Property IdMotivoUbicacionDañadoPicking() As Integer = 0
    Public Property cambio_ubicacion_auto() As Boolean = False
    Public Property codigo_bodega_erp As String = ""
    Public Property Cuenta_Ingreso_Mercancias As String = ""
    Public Property Cuenta_Egreso_Mercancias As String = ""
    Public Property Notificacion_Voz() As Boolean = False
    Public Property Control_Tarifa_Servicios() As Boolean = False
    Public Property Id_Motivo_Ubic_Reabasto() As Integer = 0
    Public Property Es_Bodega_Fiscal As Boolean = False
    Public Property habilitar_ingreso_consolidado As Boolean = False
    Public Shadows Property captura_estiba_ingreso As Boolean = False
    Public Shadows Property captura_pallet_no_estandar As Boolean = False
    Public Property valor_porcentaje_iva As Double = 0
    Public Property control_banderas_cliente As Boolean = False

    ''' <summary>
    ''' #EJC20210728:
    ''' Si este parámetro está activo, se permite la verificación por LP o por código de producto desde la lista de
    ''' pedidos en la pantalla de la HH, sin necesidad de ingresar al pedido.
    ''' </summary>
    ''' <returns></returns>
    Public Property Permitir_Verificacion_Consolidada As Boolean = False
    Public Property IdTamañoEtiquetaUbicacionDefecto As Integer = 0
    Public Property Ubicar_Tarimas_Completas_Reabasto As Boolean = False
    Public Property IdTipoTransaccionSalida As Integer = 0
    Public Property Permitir_Eliminar_Documento_Salida As Boolean = False
    Public Property Eliminar_Documento_Salida As Boolean = False

    ''' <summary>
    ''' '#EJC20220330_CEALSA: Si true, se envía en la HH el IdOperadorBodega para filtrar las tareas de verificación
    ''' </summary>
    ''' <returns></returns>
    Public Property Operador_Picking_Realiza_Verificacion As Boolean = False

    ''' <summary>
    ''' #EJC20220330_CEALSA: Si true, se permite realizar el cambio de ubicación de producto 
    ''' que está reservado en picking pero se actualiza el IdUbicacionTemporal.
    ''' </summary>
    ''' <returns></returns>
    Public Property Permitir_Cambio_Ubic_Producto_Picking As Boolean = False

    ''' <summary>
    ''' #EJC20220701: Cuando se listan los estados en la HH, si true, se muestran tambión los estados "buen estado", segón el check de la tabla.
    ''' </summary>
    ''' <returns></returns>
    Public Property Permitir_Buen_Estado_En_Reemplazo As Boolean = False

    ''' <summary>
    ''' #EJC20220707: En el proceso de reemplazo en la HH, si el parámetro está en false, no considerar el lote para filtrar stock para reemplazar.
    ''' </summary>
    ''' <returns></returns>
    Public Property Restringir_Lote_En_Reemplazo As Boolean = False

    ''' <summary>
    ''' #EJC20220707: En el proceso de reemplazo en la HH, si el parámetro está en false, no considerar la fecha de vencimiento para filtrar stock para reemplazar.
    ''' </summary>
    ''' <returns></returns>
    Public Property Restringir_Vencimiento_En_Reemplazo As Boolean = False

    ''' <summary>
    ''' #CKFK20220717: En el proceso de reabastecimiento manual en la HH se listarón la cantidad de registros parametrizados aquí, si es cero se van a listar todos
    ''' </summary>
    ''' <returns></returns>
    Public Property Top_Reabastecimiento_Manual As Integer = 20

    ''' <summary>
    ''' #CKFK20220721: Permitir o no decimales en las transacciones de ingreso, cambios de ubicación, de estados
    ''' </summary>
    ''' <returns></returns>
    Public Property Permitir_Decimales As Boolean = False

    ''' <summary>
    ''' #CKFK20220209: Ordenar el picking por nombre completo
    ''' </summary>
    ''' <returns></returns>
    Public Property Ordenar_Por_Nombre_Completo As Boolean = False

    ''' <summary>
    ''' #CKFK20220209: Ordenar Picking Descendente
    ''' </summary>
    ''' <returns></returns>
    Public Property Ordenar_Picking_Descendente As Boolean = False


    ''' <summary>
    ''' #GT02032023: Indica cuantos dias de antigüedad puede tener un ticket para validar historico
    ''' </summary>
    ''' <returns></returns>
    Public Property Dias_Limite_Retroactivo As Integer = 0

    '#GT13032023: guarda el horario de ejecución del proceso historico
    Public Property Horario_Ejecucion_Historico As TimeSpan

    '#CKFK20230728 Parámetro para filtrar los pedidos en el despacho.
    Public Property Filtrar_Pedidos_Usuario As Boolean = False
    ''' <summary>
    ''' #EJC202310310846: Indica si la fecha de vencimiento debe ser siempre la misma para un lote (aplica en SAP y NAV)
    ''' </summary>
    ''' <returns></returns>
    Public Property Homologar_Lote_Vencimiento As Boolean = False

    ''' <summary>
    ''' #CKCK20240202 Parámetro que va a permitir escanear la licencia en Picking
    ''' </summary>
    ''' <returns></returns>
    Public Property Escanear_Licencia_Picking As Boolean = False

    Public Property IdTipoEtiquetaLicencia As Integer = 0

    Public Property IdSimbologiaLicencia As Integer = 0

    Public Property Interface_SAP As Boolean = False
    Public Property Restringir_Areas_SAP As Boolean = False

    '#AT20240627 Parámetro que va a permitir realizar implosiones o cu con licencias mixtas
    Public Property Control_Pallet_Mixto As Boolean = False
    ''' <summary>
    ''' #EJC20240904:Se agrega el campo despacho_automático para poder saber si en la verificación, al finalizar, 
    ''' se debe ejecutar el proceso de despacho de forma automática y con valores predeterminados.
    ''' </summary>
    ''' <returns></returns>
    Public Property Despacho_Automatico_HH As Boolean = False
    Public Property Limpiar_Campos As Boolean = False
    Public Property Permitir_Cambio_Ubic_Recepcion As Boolean = False
    Public Property Ruta_CDN As String = ""
    Public Property Rango_Dias_Documentos As Integer = 0
    Public Property Agrupar_Sin_Lic_Veri_No_Cons As Boolean = False
    Public Property Advertir_Mpq_Umbas As Boolean = False

    Sub New()
    End Sub
    Sub New(ByRef IdBodega As Integer, ByVal IdPais As Integer, ByVal IdEmpresa As Integer, ByVal codigo As String, ByVal codigo_barra As String, ByVal nombre As String, ByVal nombre_comercial As String, ByVal direccion As String, ByVal telefono As String, ByVal email As String, ByVal encargado As String, ByVal ubic_recepcion As String, ByVal ubic_picking As String, ByVal ubic_despacho As String, ByVal ubic_merma As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal coordenada_x As String, ByVal coordenada_y As String, ByVal largo As Double, ByVal ancho As Double, ByVal alto As Double, ByVal reservar_stocks_por_linea As Boolean, ByVal rechazar_pedido_por_stock As Boolean, ByVal IdTipoTransaccion As String, ByVal zoom As Double, ByVal IdMotivoUbicacionDañadoPicking As Integer, ByVal cambio_ubicacion_auto As Boolean, ByVal bloquear_lp_hh As Boolean)
        Me.IdBodega = IdBodega
        Me.IdPais = IdPais
        Me.IdEmpresa = IdEmpresa
        Me.Codigo = codigo
        Me.Codigo_barra = codigo_barra
        Me.Nombre = nombre
        Me.Nombre_comercial = nombre_comercial
        Me.Direccion = direccion
        Me.Telefono = telefono
        Me.Email = email
        Me.Encargado = encargado
        Me.Ubic_recepcion = ubic_recepcion
        Me.Ubic_picking = ubic_picking
        Me.Ubic_despacho = ubic_despacho
        Me.Ubic_merma = ubic_merma
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
        Me.Coordenada_x = coordenada_x
        Me.Coordenada_y = coordenada_y
        Me.Largo = largo
        Me.Ancho = ancho
        Me.Alto = alto
        Me.Reservar_stocks_por_linea = reservar_stocks_por_linea
        Me.Rechazar_pedido_por_stock = rechazar_pedido_por_stock
        Me.IdTipoTransaccion = IdTipoTransaccion
        Me.Zoom = zoom
        Me.IdMotivoUbicacionDañadoPicking = IdMotivoUbicacionDañadoPicking
        Me.cambio_ubicacion_auto = cambio_ubicacion_auto
        Me.bloquear_lp_hh = bloquear_lp_hh

    End Sub
    Public Function Clone() As Object Implements ICloneable.Clone
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
        If Empresa IsNot Nothing Then
            Empresa.Dispose()
            Empresa = Nothing
        End If
    End Sub
#End Region

End Class

<Serializable>
Public Class clsBeBodegaBase

    Public Property IdBodega() As Integer = 0
    Public Property IdPais() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Nombre() As String = ""

    Public Property bloquear_lp_hh As Boolean = False
    Public Property captura_estiba_ingreso As Boolean = False
    Public Property captura_pallet_no_estandar As Boolean = False
    Public Property priorizar_ubicrec_sobre_ubicest As Boolean = False
    Public Property ubic_merma As String = ""
    Public Property ubic_producto_ne As Integer = 0
    Public Property IdProductoEstadoNE As Integer = 0
    ''' <summary>
    ''' --#EJC20220129: Si true, entonces en el proceso de ubicación se valida que la ubicación destino 
    ''' no tenga el equivalente a un pallet, en base a las tarimas y cajas por cama de la presentación.
    ''' </summary>
    ''' <returns></returns>
    Public Property validar_disponibilidad_ubicaicon_destino As Boolean = False
    ''' <summary>
    ''' #EJC20220224: CEALSA, indica si se muestra la columna zona en picking y consulta de existencias al inicio.
    ''' </summary>
    ''' <returns></returns>
    Public Property Mostrar_Area_En_HH As Boolean = False

    '#AT 20220225 Indica si se debe ESCANEAR/CONFIRMAR el codigo del producto en proceso de picking (CEALSA)
    Public Property confirmar_codigo_en_picking As Boolean = False

    '#EJC20220301: El primer paso al futuro de la gestión automática de operadores.
    Public Property control_operador_ubicacion As Boolean = False

    '#EJC20220314: CEALSA, si true, entonces en el cambio de ubicación, al escanear ónicamente licencia, se coloca automáticamente la ubicación de origen.
    Public Property inferir_origen_en_cambio_ubic As Boolean = False

    ''' <summary>
    ''' #EJC20220510: BYB, si true, no reservar producto vencido.
    ''' </summary>
    ''' <returns></returns>
    Public Property despachar_producto_vencido As Boolean = False

    ''' <summary>
    ''' #EJC20220524: MERCOSAL, nuestra primera parametrización del tipo de pantalla para picking (espero que prevalezca el 3 y no cambie mucho en el tiempo)
    ''' </summary>
    ''' <returns></returns>
    Public Property tipo_pantalla_picking As Integer = 1

    ''' <summary>
    ''' #EJC20220524: MERCOSAL, nuestra primera parametrización del tipo de pantalla para picking (espero que prevalezca el 3 y no cambie mucho en el tiempo)
    ''' </summary>
    ''' <returns></returns>
    Public Property tipo_pantalla_recepcion As Integer = 1

    ''' <summary>
    ''' #EJC20220524: MERCOSAL, nuestra primera parametrización del tipo de pantalla para picking (espero que prevalezca el 3 y no cambie mucho en el tiempo)
    ''' </summary>
    ''' <returns></returns>
    Public Property tipo_pantalla_verificacion As Integer = 1

    ''' <summary>
    ''' #CKFK20220610 Parametrización para mostrar la verificacion consolidada o no
    ''' </summary>
    ''' <returns></returns>
    Public Property Verificacion_Consolidada As Boolean = False

    ''' <summary>
    ''' #GT07072022 Parametrización para indicar giro de empresa como Motriz
    ''' </summary>
    ''' <returns></returns>
    Public Property industria_motriz As Boolean = False

    ''' <summary>
    ''' #EJC20220912_1735: Solicitado por Marcelo MERCOPAN para controlar en los procesos de Reemplazo, 
    ''' con cuantos días máximo puedo reemplazar el producto a partir de su fecha de vencimiento.
    ''' </summary>
    ''' <returns></returns>
    Public Property Dias_Maximo_Vencimiento_Reemplazo As Integer = 0

    ''' <summary>
    ''' #EJC202209121941: Permitir en la pantalla de recepcion, recibir múltiples copias de un producto con los mismos parámetros.
    ''' </summary>
    ''' <returns></returns>
    Public Property Permitir_Repeticiones_En_Ingreso As Boolean = False

    ''' <summary>
    ''' #EJC20221005: En el proceso de carga de inventario inicial, validar si la bodega tiene existencias antes de cargar archivo.
    ''' Carolina...
    ''' </summary>
    ''' <returns></returns>
    Public Property Validar_Existencias_Inv_Ini As Boolean = False

    ''' <summary>
    ''' #EJC202211211022: Determina en la HH si se calcula o no la ubicación sugerida.
    ''' </summary>
    ''' <returns></returns>
    Public Property Calcular_Ubicacion_Sugerida_ML As Boolean = False
    Public Property Permitir_Reemplazo_Picking As Boolean = False
    Public Property Permitir_No_Encontrado_Picking As Boolean = False
    Public Property Permitir_Reemplazo_Verificacion As Boolean = False
    Public Property Permitir_Reemplazo_Picking_Misma_Licencia As Boolean = False
    Public Property Liberar_Stock_Despachos_Parciales As Boolean = False
    Public Property Control_Talla_Color As Boolean = False

End Class