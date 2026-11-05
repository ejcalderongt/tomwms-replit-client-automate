# #EJC20261026_PICKING_VISOR_EMPAQUE

## Traza previa y alcance
Usuario agregó tabEmpaque/dgridEmpaque/GridView2 al diseñador de frmPicking.
Se respetan los controles existentes. Patrón de carga: Cargar_Datos ->
Cargar_Pedidos_Impresion(conn,tx), DataTable -> DataSource -> Set_Formato/BestFit.
Referencia de generación: frmLicenciaEmpaque y su catálogo trans_licencia_empaque.
Referencia de preimpresión: frmImpresionRecepcion_OC / consultas de preimpresión.

Vista solicitada: licencia (maestro) -> contenido real trans_packing_enc (detalle).
Mostrar el tab solo con configuración de empaque, sin campos persistidos nuevos.
No generar, asociar, imprimir, finalizar ni cambiar stock desde la consulta.

## Métricas: fuentes
FechaGeneracion, impresión y FechaAsociacion de cabecera: eventos de servidor SQL.
Fecha_packing de detalle: enviada por HH du.Fecha_CompletaT(); Fec_mod también.
No equiparar diferencia entre registros a tiempo efectivo de trabajo. Solo hay
usuario de última impresión; sin historial para reconstruir todos los impresores.
Estado Finalizado del detalle no tiene timestamp dedicado e inmutable de cierre.
No inferirlo de fec_mod ni del último registro.

## Implementación

Tag: #EJC20261026_PICKING_VISOR_EMPAQUE. Fecha local 2026-10-26.

- frmPicking respeta tabEmpaque/dgridEmpaque añadidos por Erik; GridView2 se
  denomina grdvEmpaque. No cambios ajenos al nuevo tab en el diseñador.
- Cargar_Datos llama Cargar_Empaque(conn,tx) luego del encabezado, igual que
  Cargar_Pedidos_Impresion: fuente DAL, asignación DataSource, Set_Formato y BestFit.
- Al cerrar frmLicenciaEmpaque se refresca únicamente empaque para no descartar
  ediciones pendientes del resto del picking.
- Visibilidad: configuración persistida o casilla de empaque seleccionada; sin
  configuración se oculta y se retorna a la pestaña de datos si estaba seleccionada.
  No se consulta empaque de picking nuevo ni del que no requiere preparación.
- Maestro: una fila por pedido/licencia, incluida cabecera vacía; detalle por
  IdPackingEnc enlazado por No_linea (licencia de empaque), no por Lic_plate
  (licencia de origen del producto). Esta distinción evita atribuir otro contenido.
- Packing histórico sin cabecera permanece visible y se marca SIN CABECERA;
  registros sin No_linea tienen clave propia por IdPackingEnc y alerta visible.
- Origen etiqueta distingue NUEVA/REUTILIZADA/HISTORICA. FechaGeneracion de una
  reutilizada es el registro de su cabecera como empaque, no su impresión original
  de recepción. No se reconstruyen datos de impresión que no fueron guardados.
- Usuarios de generación/última impresión se resuelven por IdUsuario e IdEmpresa
  con OUTER APPLY TOP(1) para no multiplicar filas. Se conservan códigos originales
  en columnas de datos para reportes; si no hay usuario catalogado se muestra el
  valor de auditoría original.
- Colores solo en contenido/alertas: amarillo VACIA, azul CON CONTENIDO, verde
  LINEAS FINALIZADAS; rojo para anulada/inactiva, fechas anómalas o falta/conflicto
  de trazabilidad. No cambia colores de los demás grids ni estados operativos.
- Grids de solo lectura, expansión vacía permitida, formatos de fecha con hora y
  segundos, cantidades a seis decimales, minutos a dos. Formato del detalle se
  aplica también a la vista clonada al expandir. No suma cantidades heterogéneas.
- Resumen superior usa fechas existentes del picking y su estado de preparación.
  No se modifica ningún timestamp. La duración no descuenta pausas.
- DAL Get_Visor_Empaque_Picking retorna DataSet con tablas Licencias/Contenido/
  Proceso y relación ContenidoEmpacado. Un batch parametrizado, sin N+1,
  sin NOLOCK añadido, sin DDL, sin escrituras del visor. Respeta la transacción
  de lectura recibida; no convierte la consulta operativa en historial inmutable.

## Contrato reutilizable para reportes

| Tabla | Grano | Llaves |
|---|---|---|
| Licencias | Pedido + licencia, dentro del picking/bodega | IdBodega, IdPickingEnc, IdPedidoEnc, LicenciaEmpaque; IdLicenciaEmpaque cuando existe |
| Contenido | Una fila operativa de packing | IdPackingEnc; IdProductoBodega, IdPresentacion, IdUnidadMedida, IdOperadorBodega, LicenciaOrigen |
| Proceso | Una fila de picking | IdBodega, IdPickingEnc |

ClaveLicencia conecta maestro y detalle dentro del dataset de un picking/bodega.
Para consolidar datasets de varias consultas, incluir IdBodega e IdPickingEnc en
la clave de cruce. No unir contenidos solo por producto ni por LicenciaOrigen.
No sumar cantidades de distintas presentaciones/UM. CantidadRegistrada conserva
cantidad_bultos_packing, sin asumir conversión adicional; se proveen factor e IDs.
Operadores cuenta IDs distintos válidos, excluyendo 0/NULL.

| Métrica | Fuente | Condición / interpretación |
|---|---|---|
| MinGeneracionImpresion | FechaPrimeraImpresion - FechaGeneracion | Hitos SQL no negativos; no es duración de impresión física |
| MinGeneracionAsociacion | FechaAsociacion - FechaGeneracion | Espera hasta primera asociación, no mano de obra efectiva |
| MinEntreRegistrosPacking | MAX(fecha_packing) - MIN(fecha_packing) | Dos o más filas; ventana entre registros HH, sin descontar pausas |
| MinPreparacionPicking | fecha_fin_preparacion - fecha_inicio_preparacion | Solo Finalizado con ambas fechas válidas; una sola fila Proceso |
| LineasEmpacadas / LineasFinalizadas | COUNT packing / SUM Finalizado | Registros operativos; no unidades ni pedidos completos |
| CantidadImpresiones | Cabecera | Envíos registrados a impresión; no número físico de copias |

NULL significa sin medición suficiente, no cero minutos. Fechas invertidas se
marcan y no generan duración válida. No se mezclan relojes SQL/HH en una métrica
entre generación y registros del dispositivo. Fecha_packing/Fec_mod proceden de
HH (frm_preparacion_packing.java, pfecha=du.Fecha_CompletaT()); las fechas globales
se actualizan con Now en DAL de preparación. Conservar valores originales en
reportes: no se conoce offset por evento para convertir históricamente a UTC.
Solo existe último impresor y primera/última fecha; no historial de todos los
impresores ni fecha inmutable de finalización por licencia. Fec_mod NO es cierre.
No se implementó un reporte nuevo: el read-model queda reutilizable para reportes.

## Validación

- BOF y WSHHRN Debug/AnyCPU: PASS, bof-build.log / ws-build.log.
- DLL real contra MHS-DEV: PASS, validacion-dev.log; fixture dentro de transacción
  con rollback. Caso 126/88/MH07 real: cuatro licencias (dos reutilizadas, dos nuevas),
  cero líneas al iniciar esta validación. No cambia ese contenido operativo.
- Pruebas: vacías; 2 detalles por licencia sin duplicación; históricos sin cabecera;
  falta de licencia; parcial/finalizado; métricas 5 y 10 min; fechas invertidas;
  aislamiento por bodega; proceso 90 min con una sola fila; rollback final.
- UI real DevExpress: PASS, validacion-ui.log. Se instancia frmPicking sin mostrar
  su ventana operativa ni disparar Shown; el grid se aloja en una forma de prueba
  fuera de pantalla. Expansión con dos filas, expansión vacía, solo lectura,
  formato fecha/hora, claves ocultas, tab condicional y docking sin solapamiento.
- visor-empaque-preview.png y fixture-ui.xml son DATOS DE PRUEBA con rollback,
  no un snapshot operativo del picking. No se enviaron etiquetas a impresión.
- UTF-8 BOM conservado; diff --check y lint-brain correctos.
- No estrés concurrente/carga. Lectura acotada al picking, sin cambios de reservas,
  HH, proxies, firmas públicas existentes, generación ni asociación de licencias.
- Brain remoto no disponible (404), sin token de importación; contexto local actualizado.
- Sin commits automáticos.

## Uso local

Salida validada: ChatGPT/TOMWMS/deployments/picking-empaque-20261026/WMS_MHS.exe.
No se reemplazó el DAL de la sesión que permanece abierta. Detener depuración y
recompilar la salida habitual para usar los cambios. No requiere migración nueva;
reutiliza el esquema de empaque ya instalado por el cambio anterior.


## Ajuste visual solicitado #EJC20261026_PICKING_EMPAQUE_SIMPLE

Sustituye la presentación descrita anteriormente: tab con texto Empaque - X Lic,
sin mensajes de estado en el caption. Se retira el resumen temporal superior.
El maestro muestra solo 12 columnas, desde Licencia hasta Operadores, conservando
fecha/hora de generación y última impresión. Las métricas temporales se retiran
de la UI principal y del selector de columnas; DAL/contrato de reportes y detalle
operativo permanecen disponibles. Sin cambios a consultas, esquema o persistencia.
