# Modelo de identidad del IdStock

> **Tesis**: el IdStock no es una identidad estable. Es un identificador efímero que se destruye y se recrea en cada operación significativa. Reconstruir el linaje (idstock N → M → P) **no es posible con el modelo actual** — requiere conocimiento tribal de qué operación pudo haber generado cada split.

## El patrón maestro: DELETE + INSERT

Toda operación que modifica un IdStock sigue (o debería seguir) este patrón en el código:

```text
Operacion(idstock N, cambios):
    1. Leer fila stock(idstock = N)
    2. Construir fila nueva stock(idstock = M) = N + cambios
    3. INSERT stock(idstock = M)
    4. DELETE stock(idstock = N)
    5. Insertar trans_movimientos con TipoTarea correspondiente
       (referenciando IdStock de origen y/o destino segun el caso)
    6. Si hay reservas previas a N: re-puntearlas a M (stock_res)
```

**Confirmación textual de Erik** (chat 2026-04-29):

> "cuando N se mueve o cambia de ubicación, en realidad la HH lo que hace vía webservice es eliminar el idstock N e insertar un idstock que sea una copia del origen mas los cambios aplicados (ubicación diferente, pej, estado,) este proceso ocure tanto para el cambio de ubicación como para el cambio de estado, ergo el idstock deja de ser el mismo"

## La consecuencia: una licencia → N idstocks

Una licencia física (`lic_plate`, ej `XY`) puede terminar asociada a **múltiples idstocks** porque cada operación que mute parcialmente la licencia genera un nuevo idstock:

```text
Estado inicial:
    licencia XY = idstock N (100 cajas, ubicación PENDIENTE_UBIC)

Operador hace UBIC parcial de 50 cajas → posición A:
    DELETE stock(idstock = N)
    INSERT stock(idstock = M, lic_plate = XY, cantidad = 50, ubicacion = A)
    INSERT stock(idstock = O, lic_plate = XY, cantidad = 50, ubicacion = PENDIENTE_UBIC)
    
    Resultado: licencia XY = {idstock M, idstock O}  ← 2 idstocks ahora

Operador hace UBIC del resto (50 cajas) → posición B:
    DELETE stock(idstock = O)
    INSERT stock(idstock = P, lic_plate = XY, cantidad = 50, ubicacion = B)
    
    Resultado: licencia XY = {idstock M (50, A), idstock P (50, B)}  ← 2 idstocks definitivos
```

El operador físicamente **vio una sola licencia siempre**. Pero en BD ahora hay 2 idstocks (M y P), ninguno con relación explícita al N original.

## Por qué este modelo

(Hipótesis razonadas — pendiente confirmar con Erik la decisión histórica)

1. **Simplicidad de query**: la tabla `stock` representa **el estado actual** del inventario. Si una caja se movió, ya no está en el origen. Tener un solo identificador por (producto + ubicación + estado + presentación + lote + fecha_vence) hace que un `SELECT` por ubicación devuelva exactamente lo que hay físicamente ahí.

2. **Append-only en `trans_movimientos`**: la historia vive en `trans_movimientos` (tabla append-only). Cada DELETE+INSERT genera 1 o 2 movimientos (según operación) que registran el evento. Para reconstruir historia, se consulta `trans_movimientos`, no `stock`.

3. **Evita actualizaciones complejas**: con DELETE+INSERT no hay riesgo de tener "filas zombie" con cantidad 0 o estado inconsistente — el DELETE garantiza que la fila vieja desaparece.

4. **Performance de índices**: el IdStock es PK. Si fuera mutable (UPDATE de columnas indexadas), reorganizaría índices. DELETE+INSERT es más predecible para el optimizador.

## La consecuencia patológica: pérdida de linaje

El modelo **no preserva** la cadena `N → M → P`. La información existe **implícitamente** en `trans_movimientos`:

- Movimiento UBIC con `IdStockOrigen = N` y `IdStockDestino = M` (si los campos existen — pendiente confirmar en `04-ecuacion-de-balance/anti-patron-modo-depuracion.md`).
- Pero **no hay query directa** que dé "todos los idstocks que descienden de N".

Para reconstruir el linaje hoy, hay que:

1. Consultar `trans_movimientos` filtrando por `IdStock = N` para encontrar el movimiento que lo destruyó.
2. Inferir cuál fue el idstock destino del movimiento (campo `IdStock_Destino` o equivalente).
3. Recursar: para el idstock destino, repetir la búsqueda.
4. Continuar hasta llegar a un idstock que aún exista en `stock`.

Este proceso es **propenso a error humano** y **lento** (no hay índices optimizados para esta búsqueda).

## El blockchain deseado (referencia futura)

Lo que Erik plantea (chat 2026-04-29):

> "actualmente no tengo (aunque quisiera) un mecanismo tipo blockchain que me diga el historial de un idstock, ej. N, se convirtio en M y luego en P"

Propuestas conceptuales (a desarrollar en `06-blockchain-deseado/`):

1. **Columna `parent_idstock`** en `stock`: cada nuevo idstock referencia al idstock del que descendió. Reconstrucción del linaje = `WITH RECURSIVE ancestor AS (...)`.
2. **Tabla `stock_lineage`**: tabla separada con `(idstock_hijo, idstock_padre, operacion, fecha)`. Permite linaje muchos-a-uno (ej: implosión 1→N) y muchos-a-muchos (ej: fusión N→1).
3. **Event sourcing**: `stock` se calcula desde `stock_events` (no desde `stock` mutable). Cada operación genera un event. El estado actual es la proyección.

Cada opción tiene trade-offs distintos (impacto en performance, reescritura de queries, migración histórica). Ver `06-blockchain-deseado/opciones-implementacion.md` (sub-wave siguiente).

## Operaciones que mutan el IdStock (overview)

Las siguientes operaciones generan DELETE+INSERT (o INSERT múltiple) sobre `stock`. Cada una se documenta en detalle en `01-operaciones-que-mutan-idstock/` (sub-wave siguiente):

| # | Operación | Trigger | Patrón | Particiona licencia? |
|---|---|---|---|---|
| 01 | Recepción inicial | Operador HH escanea OC y confirma | INSERT solo (no DELETE) | No (1 idstock por licencia recibida) |
| 02 | Cambio de ubicación HH | Operador HH escanea licencia + ubicación destino | DELETE + INSERT vía webservice | No (mismo idstock se mueve completo) |
| 03 | Cambio de estado HH | Operador HH cambia estado (ej: BUENO → AVERIADO) | DELETE + INSERT vía webservice | No |
| 04 | UBIC parcial HH | Operador HH confirma posicionamiento de parte de la licencia | DELETE + INSERT × 2 (split) | **Sí** |
| 05 | Implosión | Descomposición de presentación | DELETE + INSERT × N | Posible (depende de estructura) |
| 06 | Ajuste positivo | Operador BOF / HH agrega cantidad | INSERT (o UPDATE — pendiente confirmar) | No |
| 07 | Ajuste negativo | Operador BOF / HH resta cantidad | DELETE (si llega a 0) o UPDATE | No |
| 08 | Reubicación parcial BOF | Mover parte del idstock a otra ubicación | DELETE + INSERT × 2 (split) | **Sí** |
| 09 | Split físico durante reserva (Reabasto) | Algoritmo de reserva detecta que necesita reservar parte | INSERT con `No_bulto = 1989` | **Sí** (pero virtual: marker temporal) |
| 10 | Split físico durante reserva (MI3) | Idem para origen MI3 | (pendiente Q-MI3-11) | (pendiente confirmar) |
| 11 | Picking | Operador picka contra una reserva | DELETE + INSERT (cantidad reducida) | No (pero genera movimiento DESP) |
| 12 | Despacho | Operador BOF cierra el despacho | DELETE (idstock vacío) o UPDATE | No |

**Atención**: el cuadro anterior es **hipótesis basada en chat con Erik**. La confirmación detallada de cada operación (qué tabla muta, en qué línea de qué archivo) requiere lectura del código de cada caso, planificada en sub-waves siguientes.

## Cross-refs

- **Modelo conceptual del puntero**: ver `dataway-analysis/04-ecuacion-de-balance/modelo-conceptual.md` y `glossary.md` entrada `puntero`.
- **Split físico de Reabasto** (ya documentado parcialmente): `brain/entities/modules/reservation/legacy-process-flow/06-reserva-stock-from-reabasto.md` — `V-FROMR-005` (Wave 12B-2).
- **Split físico de MI3** (pendiente): `Q-MI3-11` en cuestionario CAROLINA.
- **Webservice HH**: pendiente localizar el endpoint exacto que recibe las llamadas de la HH para mover/cambiar estado. Probable ubicación: `TOMWMS_BOF/MI3/Servicios/` o `TOMWMS_BOF/WMSWebAPI/Controllers/`.

## Notas

- El comment `'Puntero =>` en `frmStockEnUnaFecha.vb:172` muestra que el autor del reporte de inventario teórico tenía la analogía conceptual de memoria ya en la cabeza. Esto valida que el modelo no es invención retroactiva.
- La tabla `stock` tiene granularidad muy fina: producto + bodega + ubicación + estado + presentación + lote + fecha_vence + propietario. Cualquiera de estos campos que cambie genera un idstock distinto. Esto multiplica las "razones de mutación" de un idstock dado.
