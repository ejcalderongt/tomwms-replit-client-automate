# Matriz cross-cliente del sendero — el fingerprint DINAMICO

> Esta es la sintesis de las 5 trazas. Captura las divergencias del
> sendero como un nuevo eje de fingerprint, complementario al fingerprint
> estatico (config_enc + flags producto).

## Tabla 1 — Topologia del sendero

| Eje | BECOFARMA | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| Bodegas activas | 1 | 6 (1+5 prorrateo) | 31 (multi-tienda) | varias | 2 |
| Estados en catalogo | 10 | 18 | 19 | **24** | 2 (pobre) |
| Ubicaciones activas | 3120 | 9510 | 5622 | 7537 | 19503 (sintetico) |
| Productos | 1835 | 319 | 31397 | 623 | 1714 |
| % productos con presentacion | 0% | **90%** | 0% | 61% | 0% |
| Tipos de tarea distintos en uso | 7 | 5+ | 9 | 14+ | **1 (solo RECE)** |

## Tabla 2 — Drivers de routing al ingreso

| Driver | BECOFARMA | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| Estado default al ingreso | Cuarentena | Buen Estado | Buen Estado | Buen Estado | NULL |
| Usa producto_estado_ubic | SI (mapeo cuarentena) | NO (default global) | NO | ? (a verificar) | NO |
| Lotes pre-asignados (i_nav_ped_compra_det_lote) | NO | NO | NO | SI (corte 2024) | NO |
| Tarimas pre-recibidas (i_nav_barras_pallet) | ? | ? | ? | ? | ? |

## Tabla 3 — Transiciones internas (%)

| Tipo de tarea | BECOFARMA | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| 1 RECE | 2% | (a calc) | 17% | 12% | 100% |
| 2 UBIC (put-away) | 12% | 5% | 14% | 31% | 0% |
| 3 CEST (cesta) | 0% | 0% | 0% | **1%** | 0% |
| 4 TRAS (traslado bodega) | 0% | 0% | **8%** | 0% | 0% |
| 5 DESP (despacho) | 22% | 24% | 12% | 39% | 0% |
| 6 INVE | 0.2% | ? | 0% | 1% | 0% |
| 8 PIK | **41%** | 26% | 21% | 0.8% | 0% |
| 11 VERI | 23% | 26% | 25% | 14% | 0% |
| 12 PACK | 0% | 0% | 0% | 0.04% | 0% |
| 13 AJCANTP | ? | ? | 0.5% | 0% | 0% |
| 17 AJCANTN | 0.5% | ? | 0.3% | 0% | 0% |
| 20 EXPLOSION | 0% | 0% | 0% | **0.2%** | 0% |
| 21 UBIC PICKING | 0% | 0% | **1%** | 0% | 0% |
| 23 REABMAN | 0% | 0% | 0% | **0.3%** | 0% |
| 25 REEMP_BE_PICK | 0% | **18%** | 0% | 0% | 0% |
| 26 REEMP_ME_PICK | 0% | 0% | 0% | 0.05% | 0% |
| 27 REEMP_NE_PICK | 0% | 0% | 0% | 0.006% | 0% |

> **Aristas distintivas** (≥1% de actividad y exclusivas o casi):
> - K7: REEMP_BE_PICK 18%
> - MAMPA: TRAS 8%, UBIC PICKING 1%
> - BYB: CEST 1%, REABMAN 0.3%, EXPLOSION 0.2%

## Tabla 4 — Indicadores de salud del flujo

| Indicador | BECOFARMA | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| VERI/PIK ratio (esperable ~1.0) | 0.56 | 1.0 (sano) | 1.22 | **18.4 (anomalo)** | n/a |
| DESP/VERI ratio (esperable ~1.0) | 0.96 | 0.93 (sano) | 0.47 | 2.78 | n/a |
| Backlog outbox | **31263 (CRITICO)** | 3 (sano) | (a medir) | 255912 (parado) | 0 (vacio) |
| Pedidos en estado intermedio | 3802 Pickeado | (a medir) | (a medir) | (a medir) | (a medir) |
| Ultima actividad | 2026-04-27 (activo) | 2025-08-19 (activo) | 2026-04-09 (activo) | **2023-12-15 (parado)** | 2026-01-19 (sintetico) |

## Tabla 5 — Patrones del sendero (clasificacion canonica)

| Patron | Cliente representativo | Descripcion |
|---|---|---|
| **P-FARMA-CUARENTENA** | BECOFARMA | Cuarentena obligatoria → liberacion en 2 pasos → put-away → picking. 1 bodega, sin presentaciones. |
| **P-RETAIL-REABASTO** | K7 | Reabasto automatico intensivo (REEMP_BE_PICK ≈ 70% del PIK), multi-source picking, presentaciones activas, prorrateo a 5 bodegas downstream. |
| **P-RETAIL-MULTITIENDA** | MAMPA | TRAS entre 31 bodegas, bodegas-como-estados (CAMBIOS, IMPARES, SEGUNDAS), sin lote, LP por unidad. |
| **P-DISTRIBUCION-MASIVA** | BYB | Distribucion 1-lote-a-N-ubicaciones, EXPLOSION + REABMAN + CEST + PACK, REEMP_*_PICK con 3 estados (BE/ME/NE), 24 estados en catalogo. |
| **P-QAS-TRUNCADO** | CEALSA | Solo RECE, sin estado, sin salida. NO operacion productiva. |

## Tabla 6 — Mapeo cliente → patron (preliminar)

| Cliente | Patron primario | Patron secundario | Notas |
|---|---|---|---|
| BECOFARMA | P-FARMA-CUARENTENA | — | el caso INELAC mencionado por Erik probablemente cae aqui tambien |
| KILLIOS (K7) | P-RETAIL-REABASTO | — | benchmark sano de retail |
| MAMPA | P-RETAIL-MULTITIENDA | — | unico con TRAS dominante |
| BYB | P-DISTRIBUCION-MASIVA | — | parado dic-2023 |
| CEALSA | P-QAS-TRUNCADO | — | excluir de validacion productiva |

## Tabla 7 — Presentacion vs no-presentacion (eje cruzado)

| Eje | Con presentacion | Sin presentacion |
|---|---|---|
| Cliente | K7 (90%), BYB (61%) | BECOFARMA, MAMPA, CEALSA |
| Cantidad en stock_rec | en presentacion (Caja12=12 unidades) | en UMBas directamente |
| Peso | desde producto_presentacion.peso (si configurado) | desde producto.peso o cantidad*peso_unitario |
| Conversion | usa producto_presentaciones_conversiones | n/a |
| Implica EXPLOSION posible | SI (pallet → caja → unidad) | parcialmente (pallet → unidad) |

> **Concepto Erik (textual)**: "el peso no se maneja apropiadamente como
> unidad de medida paralela (todo)". Esto es un TODO transversal: el
> peso deberia ser una UM paralela a la cantidad, no un dato secundario.
> La WebAPI .NET 10 puede corregir esto.

## Tesis del fingerprint dinamico

> El sendero del producto end-to-end es un patron fingerprint en si mismo:
>
> 1. **5 patrones canonicos** capturan la diversidad de los 5 clientes
>    actuales (P-FARMA-CUARENTENA, P-RETAIL-REABASTO, P-RETAIL-MULTITIENDA,
>    P-DISTRIBUCION-MASIVA, P-QAS-TRUNCADO).
> 2. **Cada patron tiene aristas distintivas** (REEMP_BE_PICK, TRAS,
>    EXPLOSION+CEST+PACK, etc) que la WebAPI .NET 10 implementa como
>    capabilities.
> 3. **Un cliente nuevo** (futuro) puede mapearse a un patron existente
>    o requerir uno nuevo. El mapeo es la "firma" de su sendero.
> 4. **Health indicators** (VERI/PIK ratio, backlog outbox, ultima
>    actividad) son universales y derivan del sendero, no del flag estatico.

## Implicaciones para la WebAPI .NET 10

### Capabilities core (los 5 clientes las necesitan)
- `recepcion-fisica` (1:RECE)
- `put-away` (2:UBIC)
- `picking` (8:PIK)
- `verificacion` (11:VERI) — opcional segun cliente
- `despacho` (5:DESP)
- `ajustes` (6:INVE, 13:AJCANTP, 17:AJCANTN)

### Capabilities opcionales (por patron)
- `cuarentena-mapeo-ubicacion` (P-FARMA: producto_estado_ubic)
- `reabasto-automatico` (P-RETAIL-REABASTO: REEMP_BE_PICK)
- `multi-source-picking` (P-RETAIL-REABASTO: K7)
- `traslado-entre-bodegas` (P-RETAIL-MULTITIENDA: 4:TRAS, 21:UBIC PICKING)
- `bodegas-como-estados` (P-RETAIL-MULTITIENDA: MAMPA)
- `explosion-implosion` (P-DISTRIBUCION-MASIVA: 20:EXPLOSION)
- `reabasto-manual` (P-DISTRIBUCION-MASIVA: 23:REABMAN)
- `pipeline-empaque` (P-DISTRIBUCION-MASIVA: 3:CEST + 12:PACK)
- `reemplazo-multiestado` (P-DISTRIBUCION-MASIVA: 25/26/27 REEMP_*_PICK)
- `presentaciones-y-conversiones` (P-RETAIL-REABASTO + P-DISTRIBUCION: K7, BYB)

### Observabilidad universal
- VERI/PIK ratio
- DESP/VERI ratio
- Backlog outbox por cliente
- Pedidos en estado intermedio (cuello H29)
- Ultima actividad por tipo de tarea

## Pendientes

- Validar con Erik los 5 patrones canonicos (P-FARMA-CUARENTENA,
  P-RETAIL-REABASTO, P-RETAIL-MULTITIENDA, P-DISTRIBUCION-MASIVA,
  P-QAS-TRUNCADO).
- Decidir si CEALSA se mantiene en el set o se excluye.
- Revisar si INELAC (mencionado por Erik) cae en P-FARMA-CUARENTENA o
  requiere un patron P-PRODUCCION distinto (recibe de produccion, no
  de proveedor externo).
- Capturar mas trazas (1 producto adicional por cliente para validar
  que los patrones son estables).
