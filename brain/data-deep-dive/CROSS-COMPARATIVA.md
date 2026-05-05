---
id: CROSS-COMPARATIVA-2026-05-05
tipo: cross-comparativa
estado: vigente
fecha: 2026-05-05
autores: [agente-brain-replit]
clientes: [becofarma, byb, cealsa, mercopan, merhonsa, killios, mampa]
ramas: [dev_2028_merge]
tags: [snapshot, cross-cliente, bug-001, heat-map]
relacionado_con: [BUG-001, F4]
---

# CROSS-COMPARATIVA WMS — snapshot 2026-05-05

> 8 BDs analizadas en EC2 `52.41.114.122:1437` (excluidos LIVE/mpos/POD_BETA).
> Generado por `tools/wms-deep-dive/snapshot_runner.py`.

## 1. Inventario general

| Cliente | Env | DB | Bodegas | Outbox total | Daños hist. | Ajustes locales | tipo_documento varchar | Logs especializados |
|---|---|---|---:|---:|---:|---:|:---:|---|
| becofarma | PRD | `IMS4MB_BECOFARMA_PRD` | 1 | 36,576 | 0 | 10 | · | oc, pe, pick, reab, rec, ubic |
| byb | PRD | `IMS4MB_BYB_PRD` | 2 | 533,329 | 510 | 38 | · | oc, LOG_ERROR_WMS_PE, pick, rec, ubic |
| cealsa | QAS | `IMS4MB_CEALSA_QAS` | 3 | 0 | 0 | 460 | · | _solo general_ |
| mercopan | PRD | `IMS4MB_MERCOPAN_PRD` | 1 | 147,137 | 19,628 | 1,877 | ✓ | _solo general_ |
| merhonsa | PRD | `IMS4MB_MERHONSA_PRD` | 2 | 1,014,936 | 0 | 874 | ✓ | _solo general_ |
| killios | PRD | `TOMWMS_KILLIOS_PRD` | 6 | 24,193 | 6,509 | 825 | ✓ | _solo general_ |
| killios_2026 | PRD | `TOMWMS_KILLIOS_PRD_2026` | 6 | 58,491 | 10,565 | 986 | ✓ | oc, pe, pick, reab, rec, ubic |
| mampa | QA | `TOMWMS_MAMPA_QA` | 33 | 985 | 0 | 154 | · | oc, pe, pick, reab, rec, ubic |

## 2. BUG-001 cross-cliente

> Métrica universal: **dañado_picking en HH** vs **ajustes locales con Enviado_A_ERP=0**.
> Métrica solo donde aplica: **AJCANTN en outbox** (4 BDs con tipo_documento varchar).

| Cliente | Daños picking | Daños verif | Ajustes locales | Enviados ERP | Pendientes ERP | AJCANTN outbox | AJ% outbox | Verdict |
|---|---:|---:|---:|---:|---:|---:|---:|---|
| becofarma | 0 | 0 | 10 | 0 | 10 | n/a | n/a | BACKLOG ERP (100% pend) |
| byb | 495 | 15 | 38 | 27 | 11 | n/a | n/a | OK / parcial |
| cealsa | 0 | 0 | 460 | 92 | 368 | n/a | n/a | BACKLOG ERP (80% pend) |
| mercopan | 19,607 | 21 | 1,877 | 171 | 1,706 | 0 | 0 | **BUG GAP NAV (sin AJCANTN)** |
| merhonsa | 0 | 0 | 874 | 108 | 766 | 0 | 0 | BACKLOG ERP (88% pend) |
| killios | 6,500 | 9 | 825 | 103 | 722 | 0 | 0 | **BUG GAP NAV (sin AJCANTN)** |
| killios_2026 | 10,565 | 0 | 986 | 277 | 709 | 0 | 0 | **BUG GAP NAV (sin AJCANTN)** |
| mampa | 0 | 0 | 154 | 4 | 150 | n/a | n/a | BACKLOG ERP (97% pend) |

## 3. Volumen log_error_wms_* por cliente

| Cliente | general | pedido | picking | recepción | reabasto | OC | ubicación |
|---|---:|---:|---:|---:|---:|---:|---:|
| becofarma | 52,418 | 7,098 | 5,762 | 6,941 | 6,462 | 1,732 | 6,426 |
| byb | 203,782 | 14 | 3 | 18 | — | 9 | 1 |
| cealsa | 3 | — | — | — | — | — | — |
| mercopan | 70,656 | — | — | — | — | — | — |
| merhonsa | 3 | — | — | — | — | — | — |
| killios | 66,339 | — | — | — | — | — | — |
| killios_2026 | 873 | 16,914 | 20,852 | 42,607 | 13,256 | 1,691 | 16,808 |
| mampa | 2,334 | 87 | 69 | 179 | 24 | 56 | 295 |

## 4. Notas distintivas por cliente

- **becofarma**: schema legacy IMS4MB (sin tipo_documento varchar)
- **byb**: schema legacy IMS4MB (sin tipo_documento varchar)
- **cealsa**: entorno QAS; outbox vacío; schema legacy IMS4MB (sin tipo_documento varchar)
- **mercopan**: BUG-001 gap NAV: 19,607 daños / 0 AJCANTN
- **merhonsa**: perfil estándar
- **killios**: BUG-001 gap NAV: 6,500 daños / 0 AJCANTN
- **killios_2026**: BUG-001 gap NAV: 10,565 daños / 0 AJCANTN
- **mampa**: multi-bodega (33 bodegas); entorno QA; schema legacy IMS4MB (sin tipo_documento varchar)

## 5. Snapshots individuales

- [`becofarma/snapshot-2026-05-05.md`](./becofarma/snapshot-2026-05-05.md)
- [`byb/snapshot-2026-05-05.md`](./byb/snapshot-2026-05-05.md)
- [`cealsa/snapshot-2026-05-05.md`](./cealsa/snapshot-2026-05-05.md)
- [`mercopan/snapshot-2026-05-05.md`](./mercopan/snapshot-2026-05-05.md)
- [`merhonsa/snapshot-2026-05-05.md`](./merhonsa/snapshot-2026-05-05.md)
- [`killios/snapshot-2026-05-05.md`](./killios/snapshot-2026-05-05.md)
- [`killios_2026/snapshot-2026-05-05.md`](./killios_2026/snapshot-2026-05-05.md)
- [`mampa/snapshot-2026-05-05.md`](./mampa/snapshot-2026-05-05.md)