---
id: 00-INDEX
tipo: wms-test-natural-case
estado: vigente
titulo: wms-test-natural-cases — Índice
ramas: [dev_2028_merge]
tags: [wms-test-natural-case]
---

# wms-test-natural-cases — Índice

> **Wave 9 (2026-04-29)** — Casos de uso reales del algoritmo de reserva del WMS, mapeados desde código + revelaciones operativas de Erik.
> Objetivo: que cada caso quede documentado como **pseudo-código ejecutable**, con su entrada, su salida esperada, los parámetros que lo gatillan y los archivos / funciones / tablas involucrados. Es la base para tests naturales (no synthetic).

## Convenciones

Cada caso sigue esta estructura:
```
# CASO N — <nombre>

## Trigger (cuándo se dispara)
## Parámetros activos (flags y configs que importan)
## Tablas involucradas
## Función(es) llamada(s)
## Pseudo-código del flujo
## Caso operativo real (cliente conocido / anécdota)
## Variantes y combinaciones
## Q-* asociadas
```

## Casos catalogados

### Generales (aplican a cualquier cliente con interface NAV/SAP/MI3)
- **[01](./01-matriz-funcion-cliente-canal.md)** — Matriz completa "función × canal × cliente" (mapa maestro)
- **[02](./02-caso-base-pedido-normal.md)** — Reserva normal de pedido NAV (camino feliz)
- **[10](./10-caso-reemplazo-en-hh.md)** — Reemplazo de stock por operador HH (form `frmCantidadreemplazo`)

### Específicos / parametrizados
- **[03](./03-caso-clavaud-conservar-picking.md)** — Estrategia Clavaud (conservar zona picking si pedido equivale a 1+ pallet)
- **[04](./04-caso-explosion-cajas-a-unidades.md)** — Explosión automática (10.3 cajas → 10 cajas + remanente unidades), parámetro `explosion_automatica_nivel_max`
- **[05](./05-caso-restriccion-ubicacion-por-cliente.md)** — Cliente con `IdUbicacionAbastecerCon` (restricción "solo desde esta ubicación")
- **[06](./06-caso-lote-numerico-correlativo.md)** — `control_ultimo_lote`: si despachó N no puede despachar N-1 (acuerdo proveedor-cliente)
- **[07](./07-caso-tolerancia-vencimiento.md)** — `producto.tolerancia` + `producto_estado.tolerancia_dias_vencimiento` (cliente acepta solo >= N días de vida útil)

### Por canal/origen
- **[08](./08-caso-reserva-from-sap.md)** — `Reserva_Stock_From_SAP` (línea 26680 de `clsLnStock_res_Partial.vb`)
- **[09](./09-caso-reserva-from-reabasto.md)** — `Reserva_Stock_From_Reabasto` (cuando se reabastece picking)

## Datos brutales del archivo monstruo

`TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` en branch `dev_2028_merge`:
- **>26.680 líneas** (no 4.374 como estimaba — mi grep solo capturó el primer tercio)
- 3 funciones específicas por canal `_From_*`:
  - `Reserva_Stock_From_Reabasto` línea 9856
  - `Reserva_Stock_From_MI3` línea 18192
  - `Reserva_Stock_From_SAP` línea 26680

→ Cada `_From_*` es un **adapter** que ajusta el flujo según el canal de origen. El core sigue siendo `Reserva_Stock` (línea 138).

## Estado actual de la documentación

| Caso | Status | Fuentes |
|------|--------|---------|
| 01 Matriz | ✅ documentado | grep callers + lectura partial |
| 02 Pedido normal | 🟡 parcial | covered en `code-deep-flow/04-mi3-y-reserva-clavaud.md` |
| 03 Clavaud | ✅ documentado | Wave 8 + revelación Erik |
| 04 Explosión | ✅ documentado | revelación Erik 2026-04-29 + `i_nav_config_enc` cols |
| 05 Restricción ubicación | ✅ documentado | revelación Erik + `cliente.IdUbicacionAbastecerCon` |
| 06 Lote numérico | ✅ documentado | revelación Erik + `cliente.control_ultimo_lote` + `trans_re_det_lote_num` |
| 07 Tolerancia vencimiento | ✅ documentado | revelación Erik + `producto_estado` |
| 08 SAP | 🟡 firma confirmada | falta cuerpo de la función |
| 09 Reabasto | 🟡 firma confirmada | falta cuerpo |
| 10 Reemplazo HH | 🟡 callers mapeados | falta flujo end-to-end |
