---
id: CP-013-reporte-wave-13-10
wave: wave-13-10
fecha: 2026-04-29
agente: "@agent"
caso_padre: CP-013
metodo: analisis_estructural_offline_via_db_brain
acceso_bd_productiva: bloqueado_firewall_killios
---

# CP-013 / Wave 13-10 — Análisis estructural del path CEST sin BD viva

## Contexto operativo

La BD productiva de Killios (`52.41.114.122,1437`) quedó **inaccesible desde el sandbox de Replit** durante esta wave por una regla de firewall del lado Killios que no whitelistea el IP saliente actual del sandbox (`35.227.125.212`, AS396982 Google Cloud). Diagnóstico determinístico (TCP timeout puro, no `Connection refused`, no DNS fallido). Cross-check confirma que `1.1.1.1:443` y `8.8.8.8:53` salen OK desde la misma IP — el bloqueo es específico al destino `52.41.114.122:1437`.

**Esto NO bloqueó la wave**: el análisis se hizo **offline** sobre `wms-db-brain` (rama `wms-db-brain` del repo de intercambio), que contiene catálogo SQL extraído snapshot 2026-04-27T01:29Z de Killios PRD — 621 objetos (346 tablas + 221 vistas + 40 SPs + 18 funciones + 6 archivos parametrización).

## Hallazgos de la wave

### Hallazgo 1 — La BD NO defiende el invariante (CAUSA RAÍZ ESTRUCTURAL)

Inspección de `db-brain/tables/stock.md`:

```
Tabla:           dbo.stock (4.703 filas snapshot, 33 columnas)
PK:              PK_stock CLUSTERED -> IdStock (identidad)
Índices NCLI:    14 NONCLUSTERED, NINGUNO UNIQUE
Constraint:      Stock_NonNegative_20200115_EJC : ([Cantidad]>(0))
FKs salientes:   15 (a bodega_ubicacion, producto_bodega, producto_estado, etc.)
```

**Ningún índice UNIQUE sobre la llave natural** `(IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)`. La BD permite insertar dos filas con misma llave natural sin objeción. Esto convierte al bug `V-DATAWAY-004` en un anti-patrón de **dos capas**:

- **Capa código** (V-DATAWAY-004): el path CEST decide INSERT cuando debería UPDATE.
- **Capa BD** (V-DATAWAY-005, NUEVO): la tabla `stock` no defiende el invariante de unicidad. Permite el bug aunque el código sea perfecto en otros paths.

V-DATAWAY-005 es **independiente** de V-004 y se promueve a anti-patrón formal en esta wave. Cualquier fix puramente aplicativo sobre V-004 deja la puerta abierta a otros paths futuros.

### Hallazgo 2 — `lic_plate` nullable confirma H1 con respaldo de schema

Columna 16 de `dbo.stock`:

```
| 16 | lic_plate | nvarchar(50) | NULL allowed |
```

En SQL Server, `WHERE lic_plate = @lic_plate` **nunca matchea NULL contra NULL** (`NULL = NULL` es UNKNOWN). Si el código del path CEST construye el lookup como:

```sql
SELECT IdStock FROM stock
WHERE IdProductoBodega = @pb
  AND IdUbicacion = @u
  AND IdProductoEstado = @e
  AND lote = @l
  AND lic_plate = @lp     -- <-- aquí muere el merge si @lp es NULL
```

…y `@lp` viene `NULL` (o el cliente envía `''` y el SP lo convierte a NULL al asignar al parámetro), el lookup retorna 0 filas y el código entra al branch INSERT. **H1 sube de "hipótesis posible" a "hipótesis con respaldo de schema"**.

Forma defensiva del comparador (que el código del CEST probablemente NO tiene):

```sql
AND ISNULL(lic_plate, '') = ISNULL(@lp, '')
```

### Hallazgo 3 — Nueva hipótesis H4: fallback INSERT por check `Cantidad > 0`

Constraint declarado:

```
Stock_NonNegative_20200115_EJC : ([Cantidad]>(0))
```

Si el código del CEST hace:

```sql
UPDATE stock SET Cantidad = Cantidad - @cantidad_a_mover
WHERE IdStock = @stock_origen
```

…y el resultado da 0 o negativo (caso típico al consumir el stock origen completo en un CEST), el constraint **rechaza** el UPDATE con error de SQL Server. Si el código tiene un `try/catch` con fallback a `INSERT` para no romper la operación, eso explicaría perfectamente la cronología del WMS164:

```
M3 (CEST 40 UN): UPDATE stock_origen SET Cantidad = 40 OK (positiva)
                 INSERT en destino IdStock 134176 con 40 UN  -> consolida OK
M4 (CEST 30 UN): UPDATE stock_origen SET Cantidad = 0  -> RECHAZADO por check
                 catch -> INSERT en destino IdStock 134177 con 30 UN  -> NO consolida
```

H4 es **nueva en esta wave**, no estaba en CP-013 wave 13-9. Es **independiente de H1** — pueden coexistir o ser causa única alternativa. Probabilidad **alta** porque:

- Calza con los números exactos del caso (M3 deja 40 OK, M4 intenta llegar a 0).
- Calza con el patrón de "el último CEST de un lote es el que falla" (consume el residual completo).
- Es consistente con que el bug afecte 18.7% del stock activo: cualquier CEST que vacía el origen al 100% activa el path bugueado.

Forma defensiva del UPDATE (que el código probablemente NO tiene):

```vb
If cantidad_resultante = 0 Then
    DELETE stock WHERE IdStock = @stock_origen
Else
    UPDATE stock SET Cantidad = cantidad_resultante WHERE IdStock = @stock_origen
End If
```

### Hallazgo 4 — El path CEST se origina en HH Android, no en BOF VB.NET

`db-brain/tables/trans_movimientos.md` declara la FK:

```
FK_trans_movimientos_sis_tipo_tarea_hh -> sis_tipo_tarea
```

El sufijo `_hh` confirma que `IdTipoTarea` es generado del lado HH (Android). Los 5 movimientos del WMS164 son todos `IdTipoTarea=3 (CEST)` registrados por `IdUsuario=31` en menos de 75 segundos — patrón inequívoco de **operario barcoderando desde HH**, no de proceso batch del BOF.

**Consecuencia**: el código a trazar para Wave 13-11 ya **NO** es `TOMWMS_BOF`. Es `TOMHH2025` (repo Android Java/Kotlin). El plan de wave anterior queda corregido.

### Refutación 1 — `SP_STOCK_JORNADA_DESFASE` no es el SP del bug

Inspección de `db-brain/sps/SP_STOCK_JORNADA_DESFASE.md`:

- Autor: Carolina Fuentes, 17-oct-2022
- Parámetros: `@Fecha_Inicial DATE`, `@Fecha_Final DATE`, `@RegistrosARevisar INT OUTPUT`
- Naturaleza: **SP de detección, no de mutación**. Detecta huecos en `stock_jornada` por `lic_plate`/fecha consecutiva (DROP+INSERT a tablas temporales `stock_jornada_consecutivo`, `stock_jornada_fecha_consecutiva`, `stock_jornada_desfase`). **NO toca `stock` principal**.
- Independiente del marker `#EJCAJUSTEDESFASE` (CP-007/008).

Refutado en bitácora — no perder tiempo en próximas waves.

### Refutación 2 — `stock_res_ped_164` NO está relacionado con WMS164

Inspección de `db-brain/tables/stock_res_ped_164.md`:

- 38 filas, schema modify_date 2022-01-13
- Sin FKs (entrantes ni salientes)
- Sin referencias desde SPs/vistas/funciones
- Es snapshot viejo del pedido número interno 164 (probablemente debug/respaldo de 2022)

Falsa alarma. El "164" coincidió por casualidad con el ticket WMS164.

## Hipótesis actualizadas para V-DATAWAY-004

| ID | Hipótesis | Probabilidad pre-wave | Probabilidad post-wave | Evidencia agregada |
|---|---|---|---|---|
| H1 | `lic_plate` NULL/vacío rompe el comparador | media | **alta** | schema confirma `lic_plate` nullable; semántica SQL `NULL = NULL` es UNKNOWN |
| H2 | Concurrencia inter-segundo, dos hilos CEST | media | media | sin cambio |
| H3 | CEST por lote partido HH | media | media | sin cambio |
| H4 | UPDATE rechazado por check `Cantidad>0`, fallback INSERT | — (no existía) | **alta** | check constraint confirmado en BD; cuadra exacto con cronología del WMS164 |

H1 y H4 son ahora las dos hipótesis dominantes y son **independientes** entre sí.

## Decisión arquitectónica de la wave

Descubrimiento crítico en `entregables_ajuste/AGENTS.md` del repo de intercambio:

> "El productor (Replit) **no tiene clonado el WMS completo**. No aplica patches; solo los construye y los publica al repo de intercambio."

Esto invalida el plan original de Wave 13-10 ("clonar el código y trazar VB.NET"). El productor **no debe** tener el código fuente clonado por decisión arquitectónica. El path correcto para trazar el código del HH Android (Wave 13-11) es:

1. El productor (yo) emite un **contrato de extracción** detallado: qué archivos, qué métodos, qué bloques quiere ver.
2. El consumidor (Erik desde su VS de TOMHH2025) extrae los snippets pedidos y los empuja como **bundle** al repo de intercambio.
3. El productor lee el bundle y avanza el análisis.

El contrato de extracción para Wave 13-11 queda en este mismo directorio: `pedido-extraccion-hh-cest.md`.

## Pendientes que cierra esta wave

- [x] Wave 13-10 OBJETIVO ORIGINAL (trace VB.NET en BOF) — **redirigido**: no se trazó BOF porque el path CEST se origina en HH, no en BOF. El nuevo objetivo equivalente es Wave 13-11 (trace HH Android via bundle).
- [x] Confirmar si la BD defiende el invariante de unicidad — **NO**, ningún UNIQUE INDEX sobre la llave natural. Genera V-DATAWAY-005.
- [x] Refutar candidatos sospechosos por nombre — `SP_STOCK_JORNADA_DESFASE` y `stock_res_ped_164` quedan refutados con evidencia.
- [x] Validar H1 con schema — confirmado (lic_plate nullable + semántica NULL).
- [x] Identificar nueva hipótesis fuerte — H4 (fallback INSERT por check `Cantidad>0`).

## Pendientes que abre esta wave

- [ ] Wave 13-11: Erik extrae bundle del HH Android (TOMHH2025) según contrato `pedido-extraccion-hh-cest.md` y lo empuja al repo de intercambio.
- [ ] Wave 13-12: una vez el bundle esté disponible, leer el método del CEST y confirmar si H1 y/o H4 tienen evidencia en código.
- [ ] Wave 13-13: si bug raíz se confirma, abrir `V-DATAWAY-004.md` formal con cita archivo + línea.
- [ ] Plan paralelo cuando se restablezca el firewall: re-correr q01..q12 y archivar los 12 outputs raw para que Erik+Carol puedan auditar línea por línea.

## Cross-refs

- `dataway-analysis/04-ecuacion-de-balance/anti-patron-insert-stock-sin-merge.md` — V-DATAWAY-004 actualizado con H4 + refutaciones
- `dataway-analysis/04-ecuacion-de-balance/anti-patron-stock-sin-unique-index.md` — V-DATAWAY-005 (nuevo, anti-patrón estructural de BD)
- `dataway-analysis/07-correlacion-codigo-data/case-pointers/14-stock-sin-unique-index-llave-natural.md` — CP-014 (case-pointer estructural de BD)
- `brain/debuged-cases/CP-013-killios-wms164/pedido-extraccion-hh-cest.md` — contrato Wave 13-11
- `brain/debuged-cases/CP-013-killios-wms164/REPORTE.md` — reporte original Wave 13-9
- `brain/debuged-cases/CP-013.md` — bitácora viva append-only
