# wms-db-brain

Brain especializado en la BD productiva del WMS. Catálogo completo (tablas, vistas, SPs, funciones) y modelo de cómo cambia el comportamiento según la **parametrización por cliente**.

> **Cliente productor de este snapshot**: Killios (`TOMWMS_KILLIOS_PRD`).
> **Última extracción**: ver `_meta/extracted_at.txt`.

## Estructura

```
db-brain/
├── README.md                         # este archivo
├── _meta/                            # metadatos del snapshot
│   ├── extracted_at.txt
│   ├── stats.md                      # stats globales del catálogo
│   └── extractor.md                  # cómo se regenera
├── tables/                           # 1 entity por tabla (target: 345)
│   ├── _index.md
│   └── *.md
├── views/                            # 1 entity por vista (target: 220)
│   └── _index.md
├── sps/                              # 1 entity por SP (target: 39)
│   └── _index.md
├── functions/                        # 1 entity por función (target: 17)
│   └── _index.md
├── parametrizacion/                  # flags y matrices por cliente
│   ├── README.md
│   ├── flags-cliente.md
│   ├── flags-producto.md
│   ├── flags-bodega.md
│   ├── flags-ajuste-tipo.md
│   └── matriz-killios.md
└── dependencias/                     # grafo cross-objeto (TBD en commit posterior)
```

## Cross-link con `wms-brain`

- `wms-brain` referencia objetos SQL como `db-brain://tables/<nombre>`. Ej:
  `wms-brain://entities/modules/mod-cliente-lotes` linkea `db-brain://tables/cliente_lotes`.
- Cada entity SQL aquí declara `referenced_by:` apuntando a entities de `wms-brain` que la mencionan.

## Estado de extracción

**Esta versión inicial (2026-04-27)** contiene:
- `_meta/` con stats globales (621 objetos en Killios)
- **5 sample tables** (`cliente`, `producto`, `stock`, `cliente_lotes`, `log_importacion_excel`)
- **Parametrización inicial**: flags reales de Killios para los 4 maestros más configurables
- **Matriz Killios**: estado real HOY de los flags clave

La extracción **FULL** (345 tablas + 220 vistas + 39 SPs + 17 funciones = 621 entities) se publica en commit posterior tras validación de formato por EJC.

## Cómo se regenera

`brain/sql-catalog/extract_sql_catalog.py` (en branch `wms-brain`) extrae el catálogo. Output dual planeado:
1. JSON al Brain API REST (`/api/brain/import/sql-catalog`) — query rápida.
2. Markdown a este branch (`db-brain/`) — análisis humano + parametrización.

Detalle: `_meta/extractor.md`.

## Multi-cliente

`parametrizacion/` modela cómo cambia el comportamiento del WMS según los flags configurados en cada BD cliente. Incluye:
- `flags-*.md` — qué hace cada flag (descripción funcional).
- `matriz-killios.md` — estado real HOY en Killios.
- `matriz-becofarma.md`, `matriz-bybcealsa.md` — placeholders TBD (no hay acceso directo aún).
- `diff-cross-cliente.md` — qué cambia entre clientes (cuando se completen las matrices).

## Reglas críticas

- **Definitions de SPs/vistas son sensibles**: no exponer a logs ni respuestas que vayan a clientes finales o terceros.
- Branch read-only para el agente Replit en cuanto a la BD: solo `SELECT`. Cualquier write lo hace EJC.
