# La Cumbre: picking 11 hacia rack

#EJC20261024. Diagnostico estatico; sin acceso a BD productiva, confirmado por Carolina. Sin cambios de fuentes, configuracion o inventario.

Foto: Cambio de ubicacion N.D., origen 11 / PICKING - #11; licencia MP000023581; codigo 641194170287; Tona Cerveza Lata 16oz; vence 23/06/2027. Confirmar la transcripcion de la licencia al consultar. El error indica: «No se puede implosionar, no se encontro stock de la licencia origen».

## Evidencia local

Ambos repositorios estan en dev_2026_estable. BOF HEAD 6c5a525a; HH HEAD c7a8d14a. No se verifico equivalencia con los binarios desplegados.

- HH frm_cambio_ubicacion_ciega.java:6145 selecciona el flujo automatico si Ubic_Implosion_Auto esta activo y el destino es rack, en modo cambio de ubicacion.
- DAL clsLnTrans_ubic_hh_det_Partial.vb:2375 activa implosion cuando el rack destino tiene otra licencia. Es una consolidacion automatica de licencia, aunque el usuario solo solicite devolver producto a rack.
- Aplica_Implosion (linea 1411) llama al validador. Validar_Implosion_MismaUbicacionEstado consulta el origen con pRequiereDisponible=True (2128) y lanza el mensaje de la foto si la consulta devuelve Nothing (2143).
- Stock_res/clsLnVW_stock_res.vb:1370 consulta VW_Stock_Res por bodega/licencia, Activo=1, Bloqueada=0 y Disponible_UMBas>0. La ruta automatica agrega producto, ubicacion, estado, presentacion, lote, vencimiento, IdStock si existe y talla/color si corresponde.
- El mismo texto tambien existe en Aplica_LP_Stock:717. Sin traza del servidor no se puede atribuir de forma exclusiva al validador, aunque el contexto de la pantalla corresponde al flujo automatico descrito.

## Hipotesis y limite

El mensaje no demuestra ausencia fisica de producto. Puede significar saldo totalmente reservado, bloqueo/inactividad, ausencia en la vista, o diferencia entre la identidad enviada y el stock vigente, incluso tras un cambio automatico de estado. Falta la consulta productiva para distinguirlos.

La HH tiene una ruta separada para stock reservado (aplicarCambio:5567, execws(22)), que actualiza ubicaciones reservadas. Su seleccion depende de Permitir_Cambio_Ubic_Producto_Picking y de banderas de reserva calculadas en llenaCant:1553-1564; estas se calculan en ese bloque solo cuando item.IdPedido != 0. Es un punto a contrastar con los datos, no un fallo demostrado. Un movimiento por esa ruta tampoco equivale a cancelar el pedido o liberar reservas.

No quitar el filtro de disponible ni liberar reservas como correccion especulativa: Aplica_Implosion tambien descuenta CantidadReservadaUMBas al consumir stock. Hay que confirmar si la cancelacion del despacho ya libero el compromiso, o si el caso requiere el procedimiento de devolucion de picking.

## Siguiente verificacion

Reproduccion local posterior: se respaldo Logcat previo y se limpio emulator-5554 antes del intento. logcat-reproduccion-local.txt registra origen 11, destino resuelto 1094, producto bodega 16792, 96 UMB e IdStock enviado 1009653. La HH selecciono Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack (cb=29), con Ubic_Implosion_Auto=true y destinoRack=true. El servicio devolvio resultado True, pIdStockNuevo=1019926 y pIdMovimientoNuevo=2522794. El error original no se reprodujo en este intento. La seleccion del endpoint no demuestra que se haya ejecutado implosion: ese endpoint tambien resuelve rack vacio y cambio de ubicacion. Pendiente verificar stock/movimientos en la BD y comparar version/ambiente desplegados en la HH que fallo; la respuesta SOAP exitosa no identifica por si sola la secuencia interna ni valida persistencia mediante SELECT independiente.

Nueva evidencia de Carolina: SELECT por IdUbicacion=1094 en VW_Stock_Res devuelve cero filas. La consulta por codigo muestra IdBodega=89, IdProductoBodega=16792 e IdStock=1009653; el valor 691 visible pertenece a IdUbicacion_anterior, NO demuestra la ubicacion actual.

La implementacion local de Get_Info_Ubicacion_Destino (clsLnBodega_ubicacion_Partial.vb:3962 y :4012) obtiene licencia destino exclusivamente de VW_Stock_Res por bodega y ubicacion mediante OUTER APPLY; sin filas devuelve licencia vacia. Ambos orquestadores automaticos requieren licencia destino no vacia y distinta para implosionar. Por tanto, si 1094 estaba vacia al intentar y era el destino efectivo en la misma BD, el codigo local no deberia llegar a la implosion. Priorizar traza del intento (endpoint, BD, IdUbicacionDestino y licenciaDestino calculada) y version desplegada; no atribuir el fallo solo a filtros del origen. La captura es posterior al error y no prueba estado historico ni identidad del servicio usado.

Actualizacion de Carolina (24/10/2026): captura de SELECT por codigo 641194170287 en VW_Stock_Res muestra una fila con Cantidad_UMBas=96, CantidadSF=96, CantidadReservadaUmBas=0 y Disponible_UMBas=96. Se descarta falta de saldo por reserva para la fila mostrada al momento de la consulta. La captura no muestra licencia, bodega, IdStock, ubicacion, estado, lote, presentacion, activo o bloqueo; aun falta confirmar que es la identidad del intento fallido y si hubo un cambio de estado intermedio. No se debe seguir atribuyendo el fallo a reservas sin nueva evidencia.

Ejecutar diagnostico.sql en La Cumbre cuando vuelva el acceso. Es SELECT y variables locales, sin modificaciones. El catalogo consultado para stock y stock_res es un snapshot historico de Killios; cotejar esquema del cliente si alguna columna difiere. Los campos de VW_Stock_Res se toman de la consulta usada por el codigo actual.

Si stock existe pero no aparece en la vista, revisar la definicion vigente de la vista. Si Disponible_UMBas=0, contrastar reservas/documentos y el procedimiento operativo de cancelacion. Si pasa los filtros generales, recuperar la llamada fallida y comparar IdStock, producto, ubicacion, estado, presentacion, lote y vencimiento. Registrar tambien destino real y version desplegada. No se ejecutaron las consultas ni se reprodujo el fallo con datos del cliente.
