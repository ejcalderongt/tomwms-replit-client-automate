# Tablas por funcionalidad — vista invertida del brain-map

> Para cada funcionalidad, lista de tablas que la respaldan, con
> cardinalidades esperadas y reglas de validacion. Util para escribir
> sets de prueba, validar deploys y diagnosticar problemas.

## Verificacion de etiquetas (modulo HH parametrizable)

### Tablas core (todas presentes si modulo ON):
- `trans_verificacion_etiqueta` — header de cada verificacion. Columnas
  clave: `codigo_barra_etiqueta`, `lote`, `fecha_vence`, `lic_plate`,
  `peso_verificado`, `cantidad_verificada`, `fecha_verificado`,
  `referencia_pedido`, `zpl_etiqueta`, `user_agr`, `fec_agr`.
- `verificacion_estado` — catalogo de estados posibles del proceso de
  verificacion.
- `log_verificacion_bof` — log especifico del BOF VB.NET para este
  modulo (no del HH; el HH probablemente loguea en otra tabla o en el
  log unificado).

### Validaciones / criterios de aceptacion:
- Si modulo ON: para cada pedido en estado `Pickeado` que pase a
  `Verificado`, debe existir N+1 filas en `trans_verificacion_etiqueta`
  (1 por etiqueta verificada).
- `lic_plate` debe ser unico dentro del pedido pero puede repetirse
  cross-pedido (1 license plate puede alimentar multiples picks).
- `cantidad_verificada` debe coincidir con la cantidad pickeada
  correspondiente, salvo split por etiqueta.

### Set de prueba minimo (para K7, segun confirmacion Erik):
1. Crear pedido en `trans_pe_enc` con `estado='Pickeado'`.
2. Desde HH, escanear `codigo_barra_etiqueta` valido del pedido.
3. Confirmar verificacion en HH.
4. Validar:
   - Fila nueva en `trans_verificacion_etiqueta` con `lic_plate` no
     nulo, `fecha_verificado` reciente, `user_agr` igual al operador
     HH.
   - Estado del pedido cambia a `Verificado` (o queda en `Pickeado`
     si verificacion es parcial — pendiente confirmar regla).
   - Si `verificacion_estado` tiene flag de "completo", el siguiente
     paso `Pickeado/Verificado -> Despachado` se habilita.

## Licenciamiento de stock (license plate)

### Tablas core (universales, presentes en todos los clientes):
- `licencia_item` — relacion (lic_plate, producto, lote, cantidad).
  Es la "lista de items contenidos en una licencia fisica".
- `licencia_llave` — identificacion unica de licencia. PROBABLE: el
  generador / secuencia que asigna nuevos `lic_plate`.
- `licencia_login` — asignacion de licencia a operador (¿quien la
  abrio? ¿quien la cerro?).
- `licencia_solic` — solicitudes de creacion de licencia (cola).
- `licencias_pendientes_retroactivo` — cola de licencias retroactivas
  (vacia en todos los clientes 28-abr).
- `tipo_etiqueta` — catalogo de tipos.
- `temp_licencia_llave` — staging.

### Tablas extendidas (modelo nuevo? solo BECOFARMA + CEALSA):
- `Licencias` (PascalCase) — modelo paralelo de licencias. ¿migracion
  en curso?
- `LIcencias2` — segundo modelo (legacy?). Solo BECOFARMA, 2 filas.
- `licencia_pendientes` — cola pendientes (no retroactivo). 45 filas
  en BECOFARMA y CEALSA, 0 en K7/BB.
- `producto_clasificacion_etiqueta` — reglas de etiquetado por producto.
- `tipo_etiqueta_detalle` — atributos detallados por tipo.

### Vista universal (cross-cliente) para reporting:
> Use SIEMPRE `i_nav_transacciones_out.lic_plate` como pivote para
> trazabilidad cross-cliente. NUNCA unirse contra `licencia_item`
> u otras tablas core, porque la cardinalidad varia por cliente.

```sql
-- Ejemplo: trazabilidad de una licencia en cualquier cliente
SELECT idtransaccion, tipo_transaccion, idpedidoenc, iddespachoenc,
       idrecepcionenc, idordencompra, codigo_producto, cantidad,
       lic_plate, fec_agr, enviado
FROM i_nav_transacciones_out
WHERE lic_plate = @lic_plate
ORDER BY fec_agr;
```

## Logging y errores

### Modelo unico (legacy, todos los clientes):
- `log_error_wms` — bitacora unificada (L-011, L-016).

### Modelo segmentado (mejora reciente, solo BECOFARMA hoy):
- `log_error_wms_pe` (pedido)
- `log_error_wms_rec` (recepcion)
- `log_error_wms_reab` (reabasto)
- `log_error_wms_ubic` (ubicacion)
- `log_error_wms_pick` (picking)

### Modelo propuesto WebAPI .NET 10 (L-016 §Recomendacion):
- Tabla unica `log_error_wms` con discriminador `proceso VARCHAR(40)`,
  `correlation_id`, `contexto_json`. Backfill desde tablas segmentadas
  en migracion posterior.

## Interface al ERP (outbox + binario sincronizador)

### Tabla central:
- `i_nav_transacciones_out` (outbox). Ver L-017 para semantica de FKs
  sentinela 0.

### Binarios sincronizadores por cliente:
- K7: `SAPSync.exe` (?, confirmar) → SAP B1, solo enteros (L-009).
- BB: `NavSync.exe` → NAV, NO procesa ingresos (L-010, L-012).
- BECOFARMA: `SAPBOSync.exe` → SAP B1.
- C9: ? — desconocido.

### Configuracion por cliente:
- Tabla `i_nav_config_enc` con campo `nombre_ejecutable` que dispara
  el binario correcto (L-015 §dispatch dinamico ClickOnce).
