# Modelo del sendero — Catalogo de tipos de tarea

> `sis_tipo_tarea` define las **aristas** del grafo de movimientos. Cada
> fila en `trans_movimientos` lleva un `IdTipoTarea` que clasifica la
> transicion. Este catalogo NO es identico entre clientes — algunos
> tipos solo existen en algunos clientes.

## Catalogo cross-cliente (descubierto via trazas reales 29-abr-2026)

| Id | Nombre | Significado | BECOFARMA | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|---|---|
| 1 | RECE | Recepcion (entrada al WMS desde un OC) | ✓ | ✓ | ✓ | ✓ | ✓ |
| 2 | UBIC | Ubicacion / put-away (mover desde recepcion a almacen) | ✓ | ✓ | ✓ | ✓ | ✗ |
| 3 | CEST | Cestas / cesto (carga manual?) | ✗ | ✗ | ✗ | ✓ (339 movs) | ✗ |
| 4 | TRAS | Traslado (mover entre bodegas) | ✗ | ✗ | ✓ (90 movs) | ✗ | ✗ |
| 5 | DESP | Despacho (salida final del WMS) | ✓ | ✓ | ✓ | ✓ | ✗ |
| 6 | INVE | Inventario / ajuste de inventario | ✓ | ? | ? | ✓ | ✗ |
| 8 | PIK | Picking (recolectar para un pedido) | ✓ | ✓ | ✓ | ✓ | ✗ |
| 11 | VERI | Verificacion (etapa de validacion despues del picking) | ✓ | ✓ | ✓ | ✓ | ✗ |
| 12 | PACK | Empaque (preparacion final del bulto) | ✗ | ? | ✗ | ✓ (14 movs) | ✗ |
| 13 | AJCANTP | Ajuste cantidad positivo (entra cantidad sin doc) | ✓ | ? | ✓ (6 movs) | ✓ (2) | ✗ |
| 14 | (a documentar) | aparece en filtro VW_Movimientos_N1 | ? | ? | ? | ? | ? |
| 15 | (a documentar) | aparece en filtro VW_Movimientos_N1 | ? | ? | ? | ? | ? |
| 16 | (a documentar) | aparece en filtro VW_Movimientos_N1 | ? | ? | ? | ? | ? |
| 17 | AJCANTN | Ajuste cantidad negativo (sale cantidad sin doc) | ✓ (8 movs) | ? | ✓ (4 movs) | ✓ (2) | ✗ |
| 20 | EXPLOSION | Explosion (descomponer pallet/presentacion) | ✗ | ? | ✗ | ✓ (57 movs) | ✗ |
| 21 | UBIC PICKING | Cambio de ubicacion de picking | ✗ | ? | ✓ (12 movs) | ? | ✗ |
| 23 | REABMAN | Reabasto manual (mover a ubicacion picking manualmente) | ✗ | ? | ✗ | ✓ (106 movs) | ✗ |
| 25 | REEMP_BE_PICK | Reemplazo Buen Estado en ubicacion de Picking | ✗ | ✓ (606 movs!) | ? | ✓ (6) | ✗ |
| 26 | REEMP_ME_PICK | Reemplazo Mercancia Especial en Picking | ✗ | ? | ? | ✓ (16) | ✗ |
| 27 | REEMP_NE_PICK | Reemplazo Nuevo Estado en Picking | ✗ | ? | ? | ✓ (2) | ✗ |

> Pendiente: capturar el catalogo COMPLETO de cada cliente con
> `SELECT * FROM sis_tipo_tarea` y ver tipos 14, 15, 16 que aparecen
> en el WHERE de `VW_Movimientos_N1`.

## Aristas del grafo (clasificacion semantica)

Los tipos de tarea agrupan por familia:

### Familia INGRESO (entrada al WMS)
- `1 RECE` — recepcion fisica desde una OC
- `13 AJCANTP` — entrada de cantidad sin OC (ajuste manual)
- `14, 15, 16` — (a clasificar)

### Familia POSICIONAMIENTO (mover dentro de la bodega)
- `2 UBIC` — put-away principal: de RECEPCION → ubicacion de almacen
- `4 TRAS` — traslado entre bodegas (MAMPA)
- `21 UBIC PICKING` — cambio de ubicacion de picking (MAMPA)
- `23 REABMAN` — reabasto manual: almacen → ubicacion picking (BYB)
- `25/26/27 REEMP_*_PICK` — reemplazo automatico de stock en picking (K7, BYB)

### Familia TRANSFORMACION (cambia presentacion/cantidad)
- `20 EXPLOSION` — descomponer (1 pallet → N cajas; o 1 caja → N unidades) (BYB)
- (implicit) IMPLOSION — agrupar (N unidades → 1 caja) — no se vio en trazas pero existe en config
- `12 PACK` — empaque para despacho (BYB)
- `3 CEST` — carga en cestas (BYB)

### Familia SALIDA (preparacion para entregar)
- `8 PIK` — picking: tomar de almacen para un pedido
- `11 VERI` — verificacion: validar el picking antes de despachar
- `5 DESP` — despacho: salida final del WMS

### Familia AJUSTE (correccion sin doc)
- `13 AJCANTP` — entrada por ajuste
- `17 AJCANTN` — salida por ajuste
- `6 INVE` — ajuste por inventario fisico

## Patrones de uso por cliente

| Cliente | Tipos dominantes | Caracteristica |
|---|---|---|
| BECOFARMA | PIK (592), VERI (332), DESP (318), UBIC (173), RECE (31) | flujo farma estandar: ingresar, ubicar, picking, verificar, despachar. AJCANTN bajo (8) |
| K7 | VERI (869), PIK (863), DESP (812), **REEMP_BE_PICK (606)**, UBIC (173) | retail con REABASTO INTENSIVO de picking |
| MAMPA | VERI (289), PIK (236), RECE (192), UBIC (155), DESP (136), **TRAS (90)**, UBIC PICKING (12) | retail multi-tienda con TRASLADOS frecuentes |
| BYB | DESP (12873), UBIC (9965), VERI (4631), RECE (4064), CEST (339), INVE (321), PIK (251), **REABMAN (106)**, **EXPLOSION (57)**, PACK (14), REEMP_ME_PICK (16) | distribucion masiva con explosiones, packing, reabasto manual y cestas |
| CEALSA | RECE (1798) — UNICO TIPO USADO | solo recepcion, NO sale del WMS |

## Implicaciones para WebAPI

1. **Catalogo configurable**: `sis_tipo_tarea` es por cliente. La WebAPI
   debe leerlo dinamicamente, NO asumirlo.
2. **Capabilities por cliente**: cada cliente tiene un subset de tipos.
   La WebAPI define modulos opcionales (`reabasto-manual`, `explosion`,
   `traslado-entre-bodegas`, etc) y se activan segun el catalogo.
3. **El sendero como metrica de salud**: si un cliente solo tiene RECE
   (CEALSA) → ambiente de prueba o operacion incompleta. Si tiene RECE
   sin DESP → producto se acumula. Si tiene EXPLOSION sin REABMAN →
   probable inconsistencia.

## Pendientes

- Capturar `sis_tipo_tarea` completo de cada cliente (no solo los
  visibles en trazas).
- Identificar tipos 14, 15, 16.
- Confirmar diferencia REEMP_BE_PICK vs REEMP_ME_PICK vs REEMP_NE_PICK
  (BE=Buen Estado, ME=Mercancia Especial?, NE=Nuevo Estado?). Son
  reabastos automaticos triggereados por flag distinto del estado.
