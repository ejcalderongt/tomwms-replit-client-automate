---
id: BECOFARMA
tipo: sendero-producto
estado: vigente
titulo: Graph-EQL — BECOFARMA
clientes: [becofarma]
tags: [sendero-producto, cliente/becofarma]
---

# Graph-EQL — BECOFARMA

**Caracter**: farma con cuarentena, 1 bodega, 0 presentaciones,
control_lote+control_vencimiento+genera_lp.

## Sub-grafo

```
=== INGRESO ===
RECEPCION (trans_oc_enc tipo importacion farma)
   │
   --[ 1:RECE | stock_rec | producto_estado_ubic(8, 1) ]-->
   ▼
[Cuarentena @ CUARENTENA]
   │
   --[ 2:UBIC | trans_movimientos | when QC libera ]-->
   ▼
[Buen Estado @ CUARENTENA]
   │
   --[ 2:UBIC | trans_movimientos | mover a zona PRODUCTO LIBERADO ]-->
   ▼
[Buen Estado @ PRODUCTO LIBERADO]
   │
   --[ 2:UBIC | trans_movimientos | put-away final a estanteria ]-->
   ▼
[Buen Estado @ 1A1...]    ← ubicaciones fisicas (1A1, 3C1, 9D3, etc)

=== SALIDA ===
[Buen Estado @ ubicacion_almacen]
   │
   --[ 8:PIK | stock_rec.IdPickingEnc | trans_movimientos ]-->
   ▼
[Buen Estado @ estacion picking]
   │
   --[ 11:VERI | trans_movimientos ]-->
   ▼
[Buen Estado @ verificacion]
   │
   --[ 5:DESP | stock_rec.IdDespachoEnc, activo=0 ]-->
   ▼
DESPACHO

=== AJUSTES ESPORADICOS ===
[Buen Estado @ ubic] --[ 6:INVE | inventario fisico ]--> [Buen Estado @ ubic | corregido]
[Buen Estado @ ubic] --[ 17:AJCANTN | trans_movimientos | merma ]--> [STOCK_BAJA]
```

## Aristas dominantes (datos historicos producto Q4100304)

| Tipo | n | % |
|---|---|---|
| 8 PIK | 592 | 41% |
| 11 VERI | 332 | 23% |
| 5 DESP | 318 | 22% |
| 2 UBIC | 173 | 12% |
| 1 RECE | 31 | 2% |
| 17 AJCANTN | 8 | 0.5% |
| 6 INVE | 3 | 0.2% |

## Caracteristicas del sendero BECOFARMA

- **Cuarentena obligatoria**: TODO producto entrante pasa por cuarentena
  (mapeo `producto_estado_ubic(IdEstado=8, IdBodega=1) → CUARENTENA`).
- **Liberacion en 2 pasos**: cambio de estado + cambio de zona logica
  (CUARENTENA → PRODUCTO LIBERADO) + put-away final a estanteria.
- **0 presentaciones**: maneja UMBas directamente.
- **Sin reabasto explicito**: no usa REEMP_*_PICK ni REABMAN. El stock
  va directo a las ubicaciones fisicas (1A1, 3C1, 9D3) y se pickea de ahi.
- **Sin traslados**: 1 sola bodega.
- **Sin explosion/implosion**: maneja unidades (cajas) sin transformacion.

## Pendientes (relacionados con H29)

- Modelar el cuello de botella entre `8:PIK` y `11:VERI`/`5:DESP`:
  3802 pedidos quedan en estado "Pickeado" sin avanzar. Es UN punto
  del grafo donde se acumula trafico.
- Validar si `genera_lp_old=True` en producto cambia el flujo
  (genera codigos de barra de pallet vs no).
- Ver si hay diferenciacion por tipo de OC (importacion vs nacional vs
  devolucion) que cambia el estado default.
