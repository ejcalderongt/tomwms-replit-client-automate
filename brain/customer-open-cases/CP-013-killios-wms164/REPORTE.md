---
id: REPORTE
tipo: cp-open
estado: vigente
titulo: CP-013 — Reporte de trazabilidad caso WMS164 (Killios PRD)
tags: [cp-open]
---

# CP-013 — Reporte de trazabilidad caso WMS164 (Killios PRD)

> Caso de campo. Recepción ingresada el 9-feb-2026 que el 23-abr-2026 quedó con stock partido en dos filas con la misma llave natural (ProductoBodega + Ubicación + Estado + Lote + Licencia). El bug no es de cálculo: es de **persistencia** — un INSERT a `stock` que debió consolidar contra una fila preexistente y, en su lugar, creó una fila nueva.

## Resumen ejecutivo

| Campo | Valor |
|---|---|
| Caso reportado | WMS164 |
| Cliente | Killios (BD `TOMWMS_KILLIOS_PRD_2026`) |
| Servidor | EC2 `52.41.114.122,1437` |
| Producto | `IdProducto = 77`, `IdProductoBodega = 381`, `IdBodega = 1` (BOD1) |
| Propietario | `IdPropietario = 1` (KILIO-GARESA, código `'01'`) |
| Recepción | `IdRecepcion = 2179` |
| Síntoma | Misma llave natural de stock, dos filas de **40** y **30** UN, donde debió haber una sola de **70** UN |
| Cantidad total involucrada en el caso | 270 UN distribuidas en 5 movimientos, balance cuadra |
| Fecha apertura caso | 23-abr-2026 |
| Fecha trace | 29-abr-2026 |
| Status diagnóstico | **confirmed** — bug de path INSERT-sin-merge |
| Bug raíz inferido | `V-DATAWAY-004` (anti-patrón insert-stock-sin-merge) |
| Pattern sistémico | 469 combos (ProductoBodega + Ubicación + Estado + Lote + Licencia) con > 1 fila → 919 filas redundantes (18.7% del stock activo de 4.914 filas) → **183.375 UN afectadas** |

## Aclaración importante: NO es ModoDepuracion (V-DATAWAY-001)

La hipótesis inicial razonable era que este caso correspondiera al CP-007 / CP-008 (mutador `#EJCAJUSTEDESFASE` en los tres reportes con `ModoDepuracion`). **Refutada por evidencia directa**:

```
Marker '#EJCAJUSTEDESFASE' en TOMWMS_KILLIOS_PRD_2026 → 0 (cero) ocurrencias
                                                         en trans_movimientos.
```

Killios **nunca corrió** ninguno de los tres reportes mutadores en `ModoDepuracion`. El caso WMS164 es un anti-patrón **independiente** de la familia ModoDepuracion. Por eso amerita su propio bug ID y su propio anti-patrón documentado.

## Cronología reconstruida (23-abr-2026 entre 10:32 y 10:33)

5 movimientos del usuario `IdUsuario = 31` sobre `IdRecepcion = 2179`. Todos los `lic_plate` aparecen vacíos (`''`) en `trans_movimientos` para los CEST y UBIC — eso ya es una grieta de trazabilidad independiente, pero no es la causa de este caso.

```
10:32:18  M1  UBIC   307 -> 308    40 UN  estado=1   IdStock 134175
10:32:42  M2  UBIC   307 -> 308    95 UN  estado=1   IdStock 134175
10:33:01  M3  CEST   307 -> 22     40 UN  estado=1->16  IdStock 134176  *** split 1 ***
10:33:14  M4  CEST   307 -> 22     30 UN  estado=1->16  IdStock 134177  *** split 2, NO consolida ***
10:33:33  M5  UBIC   307 -> 308    65 UN  estado=1   IdStock 134178
```

**Suma física**: 40 + 95 + 40 + 30 + 65 = **270 UN** distribuidas en 5 movimientos. La suma cuadra con la posición original.

**Estado final del stock** (3 filas activas, no-cero):

```
IdStock 134176   IdProductoBodega=381  IdUbicacion=22   Estado=16  Lote=L  lic_plate=''  Cantidad=40
IdStock 134177   IdProductoBodega=381  IdUbicacion=22   Estado=16  Lote=L  lic_plate=''  Cantidad=30   <-- debio ser merge a 134176
IdStock 134178   IdProductoBodega=381  IdUbicacion=308  Estado=1   Lote=L  lic_plate=''  Cantidad=65
```

Las filas 134176 y 134177 tienen **idéntica llave natural** — `ProductoBodega + Ubicacion + Estado + Lote + Lic_Plate (vacío)` — y deberían haberse consolidado a una sola fila de 70 UN. No lo hicieron.

## Las 12 queries del trace

Todas en `queries/q01..q12.py` (más helper `_db.py`). Ejecutadas vía `pymssql` con credenciales `WMS_DB_USER` + `WMS_KILLIOS_DB_PASSWORD` en modo **READ-ONLY** (sólo `SELECT`, ningún `INSERT/UPDATE/DELETE/DDL`).

| # | Archivo | Propósito | Hallazgo |
|---|---|---|---|
| 01 | `q01_producto_y_posicion.py` | Localizar producto por código y bodega | `IdProducto=77`, `IdProductoBodega=381` |
| 02 | `q02_schema.py` | Inventario de tablas Killios | confirma esquema lowercase, ausencia de `posicion`/`licencia` (es `bodega_ubicacion` + `lic_plate` string en `stock`) |
| 03 | `q03_schema_v2.py` | Schema detallado de `stock`, `bodega_ubicacion`, `trans_movimientos`, `sis_tipo_tarea` | confirma `lic_plate` como string libre en `stock`, `IdUbicacion` no único entre bodegas |
| 04 | `q04_localizar.py` | Localizar el caso con criterio amplio | confirma 5 movimientos del 23-abr y 3 stocks vivos |
| 05 | `q05_locate.py` | Refinar localización con `IdRecepcion` | confirma `IdRecepcion=2179`, `IdUsuario=31` |
| 06 | `q06_stock_actual.py` | Foto actual de `stock` para el `IdProductoBodega=381` | 3 filas, dos con misma llave natural |
| 07 | `q07_stock.py` | Foto cruzada `stock` × `bodega_ubicacion` | revela FK roto: `stock.IdUbicacion=22` con `IdBodega=1`, pero `bodega_ubicacion(22)` sólo existe en `IdBodega=6` |
| 08 | `q08_movimientos.py` | Cardex completo de los 5 movimientos | reconstruye cronología 10:32-10:33 |
| 09 | `q09_origen.py` | Buscar `RECE` original del 9-feb-2026 | confirma fecha de recepción anterior, `fec_agr=23-abr` > `fec_mod=09-feb` (incoherencia menor) |
| 10 | `q10_balance.py` | Aplicar fórmula de balance al caso | balance cuadra (270 UN entran, 270 UN siguen vivas) — el bug no es de cantidad |
| 11 | `q11_final.py` | Confirmar marker `#EJCAJUSTEDESFASE` en Killios | **0 ocurrencias** → refuta hipótesis ModoDepuracion |
| 12 | `q12_alcance.py` | Medir alcance sistémico del bug | **469 combos / 919 filas / 183.375 UN** afectadas; top WMS167 (14 filas), WMS384/56/89 (12 filas), WMS221 (11 filas) |

Outputs raw conservados:

- `outputs/q11_final.out` — refutación ModoDepuracion
- `outputs/q12_alcance.out` — medición sistémica del anti-patrón

## Diagnóstico

### Lo que pasa

Cuando la HH (o algún flujo del BOF) ejecuta un `CEST` (Cambio de Estado) y el stock destino lógicamente debería caer en una fila preexistente con misma llave natural, el flujo:

- **Caso correcto esperado**: `UPDATE stock SET cantidad = cantidad + N WHERE ProductoBodega=... AND Ubicacion=... AND Estado=... AND Lote=... AND lic_plate=...`
- **Caso real observado**: `INSERT INTO stock (...) VALUES (...)` — fila nueva

El comparador del CEST no detecta la fila preexistente y crea una nueva. Por eso aparece M3 → IdStock 134176 (40) y M4 → IdStock 134177 (30) en lugar de 134176 (70).

### Por qué pasa probablemente

Hipótesis ordenadas por probabilidad:

1. **lic_plate vacío rompe la llave natural**. Si el comparador que decide consolidar usa `lic_plate` como parte de la llave y dos filas con `lic_plate=''` no se consideran iguales (por ej. NULL semantics o trim-equivalence), el merge nunca ocurre.

2. **Concurrencia inter-segundo**. Los 5 movimientos están a 13-19 segundos uno de otro. Si M3 y M4 son llamadas concurrentes desde dos hilos del CEST (por ej. la HH dispara un job y mientras tanto el operador escanea otra etiqueta), el read del stock destino lee 0 filas en ambos casos antes de que el primer INSERT haya commiteado.

3. **CEST por lote partido**. La HH puede haber permitido al operador confirmar el CEST en dos eventos separados (40 y luego 30) y el flujo interpreta "dos eventos = dos filas" en lugar de "un destino = un merge".

### Por qué no es ModoDepuracion

Confirmado por query 11. La presencia simultánea de:
- 5 movimientos en `trans_movimientos` con cantidades coherentes
- ningún marker `#EJCAJUSTEDESFASE`
- el balance cuadra perfectamente

descarta categóricamente que el caso provenga del path mutador del CP-007 / CP-008.

## Severidad y alcance

**Severidad: alta**.

- **Caso individual**: confunde al inventario (parece haber dos lotes distintos cuando es uno).
- **Operativo**: la HH al pickear puede agarrar el de 30 primero y dejar el de 40 fragmentando aún más.
- **Reportes**: cualquier reporte que agrupa por llave natural muestra la suma correcta, pero cualquier reporte que itera por `IdStock` muestra dos líneas, generando ruido y dudas en operación.
- **Sistémico**: 18.7% del stock activo de Killios sufre este patrón. Top 5 productos: WMS167 (14 filas), WMS384/056/089 (12 filas cada uno), WMS221 (11 filas).

## Próximos pasos

| # | Acción | Tipo | Bloquea |
|---|---|---|---|
| 1 | Confirmar con Erik si autoriza ampliar `q12_alcance` a `IMS4MB_BYB_PRD` y `IMS4MB_CEALSA_QAS` para medir si el patrón es Killios-only o sistémico cross-cliente | medición | autorización |
| 2 | Ubicar en VB.NET el flujo de CEST y leer su path INSERT vs UPDATE — buscar el comparador de llave natural | trace código | Wave 13-10 |
| 3 | Si se localiza el bug raíz, abrir `V-DATAWAY-004` formal con cita al archivo y línea | bug formal | step 2 |
| 4 | Plantear remediación: a) script de consolidación batch (UPDATE merge) sobre los 919 stocks afectados, b) fix en código del CEST | acción | Erik decide |

## Cross-refs

- `dataway-analysis/04-ecuacion-de-balance/anti-patron-insert-stock-sin-merge.md` — anti-patrón formalizado
- `dataway-analysis/07-correlacion-codigo-data/case-pointers/13-stock-insert-no-consolida-killios-wms164.md` — case-pointer formal
- `brain/debuged-cases/CP-013.md` — bitácora viva append-only
- `brain/agent-context/glossary.md` — términos: llave natural de stock, consolidación, stock fantasma, CEST/UBIC/RECE
- `brain/dataway-analysis/04-ecuacion-de-balance/anti-patron-modo-depuracion.md` — V-DATAWAY-001 (NO es este caso, sirve de contraste)
