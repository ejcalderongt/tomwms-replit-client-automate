---
id: README
tipo: sendero-producto
estado: vigente
titulo: Sendero del producto — TomWMS
tags: [sendero-producto]
---

# Sendero del producto — TomWMS

> **Concepto Erik (29-abr-2026)**: el sendero del producto end-to-end
> es un **patron fingerprint dinamico** del WMS, complementario al
> fingerprint estatico (config_enc, flags de producto). Mientras el
> fingerprint estatico captura *que* esta configurado, el sendero
> captura *como se mueve realmente* el producto en cada cliente.
>
> Esta carpeta documenta:
>
> 1. **Modelo conceptual** del sendero (entrada, transiciones, salida).
> 2. **Mapeo a las tablas reales** de TomWMS (con nombres correctos
>    confirmados en EC2).
> 3. **Catalogo completo de tipos de tarea** (`sis_tipo_tarea`) que
>    son las aristas del grafo de movimientos.
> 4. **Graph-EQL** (notacion propuesta por Erik) — un grafo abstracto
>    que representa el flujo del producto bajo cada configuracion.
> 5. **Trazas reales** de un producto representativo por cliente.
> 6. **Matriz cross-cliente** que sintetiza variaciones del sendero
>    como nuevo eje de fingerprint.

## Estructura

```
sendero-producto/
├── README.md                    (este archivo)
├── modelo/
│   ├── 01-puntos-de-entrada.md  (ingreso: OC, recepcion, lotes pre-asignados)
│   ├── 02-mapeo-tablas.md       (tablas reales del sendero, schema)
│   ├── 03-tipos-tarea.md        (catalogo completo IdTipoTarea cross-cliente)
│   ├── 04-transiciones-internas.md (movimientos dentro de bodega)
│   └── 05-puntos-de-salida.md   (pedido, picking, despacho)
├── grafo-eql/
│   ├── notacion.md              (sintaxis del Graph-EQL)
│   ├── grafo-base.md            (grafo abstracto generico)
│   └── por-cliente/
│       ├── BECOFARMA.md         (farma con cuarentena, 1 bodega)
│       ├── K7.md                (retail con prorrateo, 6 bodegas)
│       ├── MAMPA.md             (retail multi-tienda, 31 bodegas, talla y color)
│       ├── BYB.md               (distribucion masiva con explosion, parado 2024)
│       └── CEALSA.md            (solo recepcion en QAS, no llega a salir)
├── trazas/
│   ├── README.md                (protocolo y como leer las trazas)
│   ├── traza-001-becofarma-Q4100304.md  (AMBIARE 2MG, 1457 movs)
│   ├── traza-002-k7-WMS167.md           (MELOCOTON MIGUELS, 3657 movs)
│   ├── traza-003-mampa-AG00030021.md    (Dama bajo tenis, 1120 movs)
│   ├── traza-004-byb-00170440.md        (SALSA PICAMAS, 32649 movs)
│   └── traza-005-cealsa-NEN025.md       (AMOXICILINA, 1798 movs)
└── matriz-flujos.md             (matriz cross-cliente — el verdadero fingerprint dinamico)
```

## Tesis principal

> **El sendero es un patron fingerprint en si mismo.**
>
> Dos clientes pueden tener flags identicos (control_lote=True, genera_lp=True,
> etc) pero comportarse radicalmente distinto en el flujo real porque:
>
> - Tienen catalogos distintos de `producto_estado` (10 vs 24).
> - Usan distintos `IdTipoTarea` (BYB usa EXPLOSION + REABMAN, MAMPA usa
>   TRAS + UBIC PICKING, K7 usa REEMP_BE_PICK masivamente).
> - Tienen mapeos `producto_estado_ubic` distintos (1 ubicacion vs N).
> - Tienen distinta cantidad de bodegas (1 BECOFARMA vs 31 MAMPA).
> - Tienen presentaciones o no (BECOFARMA/MAMPA/CEALSA=0, K7=90%, BYB=61%).
>
> El **objetivo final** del sendero-producto es:
>
> 1. Definir un **conjunto cerrado de patrones de sendero** (graph-EQL
>    canonicos) que capturen las N variantes posibles del flujo.
> 2. Mapear cada cliente (y cada bodega dentro del cliente) a uno o mas
>    patrones canonicos.
> 3. La WebAPI .NET 10 implementa un **conjunto de capabilities** y cada
>    cliente activa solo las que su sendero necesita.

## Como contribuir

- Agregar trazas: usar `trazas/README.md` como guia de protocolo.
- Validar grafos por cliente: Erik o Francisco confirman si el grafo
  refleja la operacion real.
- Identificar nuevas variantes: cuando un cliente usa un IdTipoTarea
  o transicion que no esta en el grafo base, ampliar el modelo.

## Trazabilidad

| Item | Origen |
|---|---|
| Modelo conceptual | Diseñado por Erik (sesion 29-abr-2026) |
| Schemas y datos | Capturados de EC2 `52.41.114.122,1437` (READ-ONLY) |
| Trazas | 1 producto representativo por cliente, query directa con SQL |
| Catalogo tipos tarea | `sis_tipo_tarea` por cliente, agregado cross-cliente |
| Graph-EQL notacion | Propuesta inicial inspirada en Cypher/EQL/Mermaid |
