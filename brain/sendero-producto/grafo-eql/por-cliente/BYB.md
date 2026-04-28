# Graph-EQL — BYB

**Caracter**: distribucion masiva (salsas/condimentos), 7537 ubicaciones, 24 estados,
61% productos con presentacion, control_lote+control_vencimiento, **EXPLOSION**,
**REABMAN**, **CEST**, **PACK**. **Operacion parada en dic-2023** (corte).

## Sub-grafo

```
=== INGRESO MASIVO (un lote → N pallets → N ubicaciones) ===
RECEPCION (trans_oc_enc desde proveedor con NAV pre-asignado)
   │
   │ i_nav_ped_compra_det_lote (lotes pre-asignados)
   ▼
[Buen Estado @ RECEPCION | lote=L75993, presentacion=Pallet]
   │
   --[ 1:RECE | N pallets | cant=6336 c/u ]-->
   │
   ▼ FORK (1 RECE → N stock_rec, 1 por LP)
[Buen Estado @ RECEPCION | LP1]
[Buen Estado @ RECEPCION | LP2]
[Buen Estado @ RECEPCION | LP3]
... (8+ LP del mismo lote)

=== PUT-AWAY DISTRIBUIDO ===
[Buen Estado @ RECEPCION | LPi]
   │
   --[ 2:UBIC | trans_movimientos | distribuir a ubic distinta por cada LP ]-->
   ▼
[Buen Estado @ 25B2/44B4/44A4/45B3/26A4/4A3/8B3/... | LPi]
(8 LP del mismo lote → 8 ubicaciones distintas)

=== TRANSFORMACION (explosion cuando se necesita pickear < pallet) ===
[Buen Estado @ ubic_almacen | presentacion=Pallet | cant=6336]
   │
   --[ 20:EXPLOSION | trans_movimientos | when pedido < pallet ]-->
   ▼
[Buen Estado @ ubic_almacen | presentacion=Caja48 o UMBas | cant=N]

=== REABASTO MANUAL (BYB no tiene REEMP_*_PICK automatico) ===
[Buen Estado @ ubic_almacen]
   │
   --[ 23:REABMAN | trans_movimientos | manual via HH ]-->
   ▼
[Buen Estado @ ubic_picking]

=== SALIDA CON CESTA + PACK ===
[Buen Estado @ ubic_picking]
   │
   --[ 8:PIK | stock_rec.IdPickingEnc ]-->
   ▼
[Buen Estado @ estacion picking | IdPickingEnc=X]
   │
   --[ 3:CEST | trans_movimientos | carga en cesta ]-->
   ▼
[Buen Estado @ cesta]
   │
   --[ 12:PACK | trans_movimientos | empaque ]-->
   ▼
[Buen Estado @ pack]
   │
   --[ 11:VERI | trans_movimientos ]-->
   ▼
[Buen Estado @ verificacion]
   │
   --[ 5:DESP | stock_rec.IdDespachoEnc, activo=0 ]-->
   ▼
DESPACHO

=== AJUSTES Y REEMPLAZOS (esporadicos en BYB) ===
[BE @ ubic] --[ 6:INVE | inventario ]--> [BE @ ubic | corregido]
[BE @ ubic] --[ 13:AJCANTP ]--> [+]
[BE @ ubic] --[ 17:AJCANTN ]--> [-]
[BE @ ubic_pick] --[ 25/26/27 REEMP_*_PICK | pocos casos ]--> [BE @ ubic_pick]
```

## Aristas dominantes (datos historicos producto 00170440 SALSA PICAMAS)

| Tipo | n | % |
|---|---|---|
| 5 DESP | 12873 | 39% |
| 2 UBIC | 9965 | 31% |
| 11 VERI | 4631 | 14% |
| 1 RECE | 4064 | 12% |
| **3 CEST** | **339** | 1% ← caracteristico |
| 6 INVE | 321 | 1% |
| 8 PIK | 251 | 0.8% |
| **23 REABMAN** | **106** | 0.3% ← caracteristico |
| **20 EXPLOSION** | **57** | 0.2% ← caracteristico |
| 26 REEMP_ME_PICK | 16 | 0% |
| 12 PACK | 14 | 0% |
| 25 REEMP_BE_PICK | 6 | 0% |
| 13/17/27 (ajustes) | 6 total | 0% |

## Caracteristicas del sendero BYB

- **Distribucion masiva**: un mismo lote (L75993) se distribuye a 8 LP
  diferentes y a 8 ubicaciones fisicas distintas (mov#1437667-680).
  Volumen: 6336 unidades por LP.
- **Explosion activa**: 57 movimientos. Es el UNICO cliente con uso
  significativo. Se dispara cuando pedido < pallet completo.
- **Reabasto MANUAL** (no automatico): REABMAN dominante sobre REEMP_*_PICK.
  Implica que un operario decide cuando reabastecer, no un job automatico.
- **CESTA + PACK + VERI = 3 etapas de salida**: BYB es el unico cliente
  con cesta (3:CEST) y empaque (12:PACK) explicitos. El picking se carga
  en cesta, luego se empaca, luego se verifica, luego se despacha.
- **24 estados** (mas alta cantidad cross-cliente): catalogo rico de
  estados (incluye Mercancia Especial, Nuevo Estado, etc — base para
  los 3 tipos REEMP_*_PICK).
- **Operacion PARADA en dic-2023**: ultima actividad mov#1437680 fec=2023-12-15.
  El backlog de outbox (256K) y el corte 2024 confirman que despues de
  esa fecha BYB no transacciono mas.

## Implicaciones criticas

- **Q-BYB-CORTE-2024 (pendiente)**: ¿cual fue el evento que paro la
  operacion? ¿migracion de ERP? ¿cierre de operacion? ¿reemplazo de WMS?
- **Q-BYB-VERIF-INCOMPLETA (pendiente)**: ratio VERI/PIK = 4631/251 = 18.4
  (esperable seria ~1). Hipotesis: VERI se usaba como inventario fisico
  en bandeja, no solo verificacion de pedido.

## Pendientes

- Modelar el predicado de EXPLOSION (¿automatico al detectar pedido < pallet
  o manual desde HH?).
- Documentar diferencia REEMP_BE/ME/NE_PICK con base en los 24 estados.
- Investigar por que VERI tiene tanto trafico desproporcionado.
