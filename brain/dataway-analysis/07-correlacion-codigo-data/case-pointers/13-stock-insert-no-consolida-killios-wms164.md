# CP-013 — Killios WMS164: stock partido en dos filas con misma llave natural (CEST sin merge)

> **Primer case-pointer "de campo" del catálogo.** No nace de un hardcode en código fuente, nace de un caso reportado por operación (Killios, ticket WMS164) y reproducido con queries READ-ONLY directas sobre la BD productiva. Diferente a los CP-001..CP-012 (que son todos breakpoint arqueológico o marker persistente), CP-013 es **evidencia de un bug vivo en producción**, no de un fósil.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-013-stock-insert-no-consolida-killios-wms164 |
| Tipo | caso de campo (data discrepancy) — primer ejemplar de la categoría |
| Estado | confirmado con datos reales (auto-confirmable, refutado vs CP-007/008) |
| Severidad | alta |
| Persistencia (afecta BD) | sí — anti-patrón ya impactó 919 filas de `stock` activo en Killios |
| Origen del caso | ticket WMS164 reportado por operación de Killios el 23-abr-2026 |
| Servidor BD | EC2 `52.41.114.122,1437` (`TOMWMS_KILLIOS_PRD_2026`) |
| Producto | `IdProducto=77`, `IdProductoBodega=381`, `IdBodega=1` (BOD1) |
| Propietario | `IdPropietario=1` (KILIO-GARESA, código `'01'`) |
| Recepción | `IdRecepcion=2179` |
| Detectado en | wave 13-9 vía trace SQL READ-ONLY (12 queries) |
| Bug raíz inferido | `V-DATAWAY-004` (anti-patrón insert-stock-sin-merge en path CEST) |
| Pattern asociado | aún no formalizado — candidato a `P-002` "INSERT sin merge contra llave natural" |

## El caso en una imagen

```
Estado esperado de stock para IdProductoBodega=381 al 23-abr-2026 10:33:14:

  IdStock 134176   IdUbicacion=22   Estado=16   Lote=L   lic_plate=''   Cantidad=70
  IdStock 134178   IdUbicacion=308  Estado=1    Lote=L   lic_plate=''   Cantidad=65

Estado real de stock (lo que dejó el path bugueado del CEST):

  IdStock 134176   IdUbicacion=22   Estado=16   Lote=L   lic_plate=''   Cantidad=40   <-- M3
  IdStock 134177   IdUbicacion=22   Estado=16   Lote=L   lic_plate=''   Cantidad=30   <-- M4 (debio ser merge a 134176)
  IdStock 134178   IdUbicacion=308  Estado=1    Lote=L   lic_plate=''   Cantidad=65
                                                                                     ^
                                                                          fila fantasma
                                                                          con llave natural
                                                                          identica a 134176
```

134176 y 134177 son **gemelas en llave natural**. Cualquier query que agrupe por `(IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)` retorna 70 UN; cualquier query que itera por `IdStock` ve dos filas distintas.

## Cronología (5 movimientos, 75 segundos)

```
10:32:18  M1  TipoTarea=UBIC   IdUbicOrigen=307  IdUbicDestino=308  Cantidad=40   Estado=1
10:32:42  M2  TipoTarea=UBIC   IdUbicOrigen=307  IdUbicDestino=308  Cantidad=95   Estado=1
10:33:01  M3  TipoTarea=CEST   IdUbicOrigen=307  IdUbicDestino=22   Cantidad=40   Estado=1->16   *** split 1 ***
10:33:14  M4  TipoTarea=CEST   IdUbicOrigen=307  IdUbicDestino=22   Cantidad=30   Estado=1->16   *** split 2, NO consolida ***
10:33:33  M5  TipoTarea=UBIC   IdUbicOrigen=307  IdUbicDestino=308  Cantidad=65   Estado=1
```

Todos del usuario `IdUsuario=31`, todos sobre `IdRecepcion=2179`, todos con `lic_plate=''` (vacío) en `trans_movimientos`.

## Lectura

El bug está en el path INSERT-vs-UPDATE del CEST (Cambio de Estado). El comparador que decide si consolidar contra una fila preexistente o crear una fila nueva **falló entre M3 y M4** — apenas 13 segundos.

Hipótesis ordenadas por probabilidad:

1. **`lic_plate` vacío rompe la llave natural** — si el comparador trata `''` como NULL o no normaliza vacío vs NULL, dos filas con `lic_plate=''` no se consideran iguales y el merge nunca ocurre.

2. **Concurrencia inter-segundo** — si M3 y M4 son disparados por dos hilos concurrentes del CEST (HH dispara job + operador escanea de nuevo), ambos hacen `SELECT count(*)` antes del primer commit y ambos deciden hacer `INSERT`.

3. **CEST por lote partido** — la HH puede haber permitido confirmar el CEST en dos eventos separados, y el flujo interpreta "dos eventos = dos filas" en lugar de "un destino = un merge".

## Refutación vs hipótesis ModoDepuracion (CP-007 / CP-008)

Confirmada por query 11:

```
TOMWMS_KILLIOS_PRD_2026:
SELECT COUNT(*) FROM trans_movimientos WHERE Serie = '#EJCAJUSTEDESFASE'
-> 0 (cero)
```

Killios **nunca corrió** ninguno de los tres reportes mutadores en `ModoDepuracion`. CP-013 es un anti-patrón **independiente** de la familia ModoDepuracion (V-DATAWAY-001). Por eso amerita su propio bug raíz `V-DATAWAY-004` y su propio anti-patrón documentado.

## Alcance sistémico (query 12)

```
TOMWMS_KILLIOS_PRD_2026 — SELECT desde stock WHERE Cantidad > 0:

Total filas activas:                                4.914
Filas con (PB+Ubic+Estado+Lote+Lic) duplicada:        919  (18.7%)
Combos distintos con > 1 fila:                        469
UN totales involucradas en el patrón:             183.375

Top 5 productos afectados:
  WMS167  -> 14 filas con misma llave natural
  WMS384  -> 12
  WMS056  -> 12
  WMS089  -> 12
  WMS221  -> 11
```

**El caso WMS164 no es un evento aislado** — es la punta visible de un patrón sistémico que ya partió 919 filas de stock activo en Killios.

## Acción propuesta esta wave

Solo medición — promover el caso a brain con queries reproducibles y bitácora viva. **Ninguna mutación de BD**. Erik confirma autorización antes de avanzar a Wave 13-10 (trace de código del CEST).

## Acciones propuestas (próximas waves)

1. **Wave 13-10**: localizar en `TOMWMS_BOF` el flujo del CEST (probablemente `clsLnTrans_movimientos` + `clsLnStock`) y leer el path INSERT vs UPDATE.
2. **Wave 13-11**: confirmar hipótesis 1, 2 o 3 con caso de prueba reproducible.
3. **Wave 13-12**: si el bug raíz se confirma, abrir `V-DATAWAY-004.md` formal con cita.
4. **Próxima**: si Erik autoriza, plantear:
   - Script de consolidación batch (UPDATE merge) sobre los 919 stocks afectados
   - Fix en código del CEST (path INSERT-sin-merge)
5. **Investigación cross-cliente**: si Erik autoriza acceso a `IMS4MB_BYB_PRD` y `IMS4MB_CEALSA_QAS`, replicar query 12 para ver si el patrón es Killios-only o sistémico.

## Por qué este case-pointer es distinto de CP-001..CP-012

- CP-001..CP-006, CP-009..CP-012: **breakpoint arqueológico** (pattern P-001) — hardcode en código sin efecto persistente en BD.
- CP-007 / CP-008: **marker persistente** (#EJCAJUSTEDESFASE) — hardcode en código que **sí** deja huella en BD; auto-confirmable por query.
- **CP-013**: **caso de campo confirmado con datos** — no parte de un hardcode encontrado en código, parte de un ticket de operación reproducido contra BD productiva. Confirma un anti-patrón sistémico **antes** de que se localice el código bugueado.

Esta categoría es nueva y conviene formalizarla en `00-INDEX.md` como **"Casos de campo (auto-confirmables sin entrevista)"**.

## Cross-refs

- `dataway-analysis/04-ecuacion-de-balance/anti-patron-insert-stock-sin-merge.md` — anti-patrón formalizado (V-DATAWAY-004)
- `brain/debuged-cases/CP-013.md` — bitácora viva append-only
- `brain/debuged-cases/CP-013-killios-wms164/` — subcarpeta con queries reproducibles + outputs raw + reporte estructurado
- `dataway-analysis/04-ecuacion-de-balance/anti-patron-modo-depuracion.md` — V-DATAWAY-001 (contraste: este caso NO es ModoDepuracion)
- `brain/agent-context/glossary.md` — términos nuevos: llave natural de stock, consolidación de stock, stock fantasma, CEST, UBIC
