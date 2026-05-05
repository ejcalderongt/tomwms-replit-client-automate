---
id: ATLAS-BECOFARMA-2026-05-05
tipo: ficha-atlas-cliente
estado: vigente
cliente: becofarma
clientes: [becofarma]
db_name: IMS4MB_BECOFARMA_PRD
environment: PRD
rama_productiva: dev_2023_estable
erp: "SAP B1 (DI-API)"
rubro: "Farmacia"
modelo_config: "PRODUCT-CENTRIC"
fecha: 2026-05-05
autores: [agente-brain-replit]
tags: [atlas, ficha-cliente, cliente/becofarma, F5]
relacionado_con: [BUG-001, F4, F5, ATLAS]
---

# Atlas — BECOFARMA (PRD)

> **Punto de entrada navegable** generado en F5 (2026-05-05). Cruza snapshot operativo F4, BUG-001 cross-cliente, learnings, ADRs y ficha existente.

## Identificación rápida

| Campo | Valor |
|---|---|
| **Cliente** | `becofarma` |
| **Database** | `IMS4MB_BECOFARMA_PRD` en EC2 `52.41.114.122:1437` |
| **Environment** | PRD |
| **Rubro** | Farmacia |
| **ERP destino** | SAP B1 (DI-API) |
| **Rama productiva** | `dev_2023_estable` |
| **Modelo configuración** | PRODUCT-CENTRIC |
| **Fingerprint** | `BECOFARMA_CLIENT_FARMA-VERIF-COMPLETO_APPLIED` |
| **BUG-001 severidad** | no afectado (compensa con AJCANTN intensivo) |

> BD diagnóstica en EC2, no productiva real; AJCANTN intensivo es diseño operativo, no compensación de bug.

## Snapshot operativo F4 (mediciones del 2026-05-05)

### BUG-001 — métricas

| Métrica | Valor |
|---|---:|
| Marcas `dañado_picking = 1` (histórico) | **0** |
| Marcas `dañado_verificacion = 1` (histórico) | 0 |
| Outbox `tipo_documento = 'AJCANTN'` | _no aplica_ |
| Ajustes locales (`trans_ajuste_enc`) | 10 |
| `Enviado_A_ERP = 1` | 0 (0%) |
| `Enviado_A_ERP = 0/NULL` (pendientes) | **10** (100%) |

### Bodegas configuradas (1)

| IdBodega | código | nombre | activo |
|---|---|---|---|
| 1 | 01 | GENERAL | ✓ |

### Outbox `i_nav_transacciones_out`

- **Total**: 36,576 filas
- **Pendientes (enviado=0)**: 31,263 (85.5%)

### Logs disponibles

- Tablas pobladas: `log_error_wms, log_error_wms_pe, log_error_wms_pick, log_error_wms_rec, log_error_wms_reab, log_error_wms_oc, log_error_wms_ubic`
- Total filas log_error_wms_*: 86,839

📂 **Snapshot completo**: [`data-deep-dive/becofarma/snapshot-2026-05-05.md`](../data-deep-dive/becofarma/snapshot-2026-05-05.md)

## Diagnostic queries (copy-paste contra EC2)

> Server: `52.41.114.122,1437` · login: `sa` · permisos: read-only.

### Q1. Daños activos sin ajuste (BUG-001)
```sql
USE [IMS4MB_BECOFARMA_PRD];

-- Lineas con daño marcado pero stock sin descontar
SELECT TOP 100
    pu.IdPickingUbic, pu.IdPickingEnc, pu.IdBodega,
    pu.IdProductoBodega, pu.cantidad_solicitada, pu.cantidad_despachada,
    pu.dañado_picking, pu.dañado_verificacion,
    pu.fec_agr, pu.user_agr,
    pe.estado as estado_picking_enc
FROM trans_picking_ubic pu
LEFT JOIN trans_picking_enc pe ON pe.IdPickingEnc = pu.IdPickingEnc
WHERE pu.dañado_picking = 1
  AND pu.cantidad_despachada = 0
  AND pu.fec_agr >= DATEADD(MONTH, -3, GETDATE())
ORDER BY pu.fec_agr DESC;
```

### Q2. Backlog Enviado_A_ERP=0 (ajustes locales sin sincronizar)
```sql
USE [IMS4MB_BECOFARMA_PRD];

SELECT
    ae.idajusteenc, ae.idbodega, ae.fecha,
    ae.referencia, ae.idusuario,
    ae.fec_agr, ae.user_agr,
    ae.IdPropietarioBodega, ae.IdCentroCosto,
    (SELECT COUNT(*) FROM trans_ajuste_det ad WHERE ad.idajusteenc = ae.idajusteenc) as lineas
FROM trans_ajuste_enc ae
WHERE (ae.Enviado_A_ERP = 0 OR ae.Enviado_A_ERP IS NULL)
  AND ae.fec_agr >= DATEADD(MONTH, -1, GETDATE())
ORDER BY ae.fec_agr DESC;
```

### Q3. Outbox pendiente por tipo de documento
```sql
USE [IMS4MB_BECOFARMA_PRD];

-- Esta BD usa solo IdTipoDocumento (int 1/2/3), sin string varchar
SELECT IdTipoDocumento,
       COUNT(*) as total,
       SUM(CASE WHEN enviado=0 THEN 1 ELSE 0 END) as pendientes,
       MIN(fec_agr) as primer_pendiente, MAX(fec_agr) as ultimo
FROM i_nav_transacciones_out
GROUP BY IdTipoDocumento
ORDER BY pendientes DESC;
```

### Q4. Heat-map operativo último mes por bodega
```sql
USE [IMS4MB_BECOFARMA_PRD];

SELECT b.IdBodega, b.codigo, b.nombre,
    (SELECT COUNT(*) FROM trans_picking_enc
        WHERE IdBodega=b.IdBodega
        AND fec_agr >= DATEADD(MONTH,-1,GETDATE())) as picks_30d,
    (SELECT COUNT(*) FROM trans_ajuste_enc
        WHERE idbodega=b.IdBodega
        AND fec_agr >= DATEADD(MONTH,-1,GETDATE())) as ajustes_30d,
    (SELECT COUNT(*) FROM trans_picking_ubic
        WHERE IdBodega=b.IdBodega AND dañado_picking=1
        AND fec_agr >= DATEADD(MONTH,-1,GETDATE())) as danados_30d
FROM bodega b
WHERE b.activo = 1
ORDER BY b.IdBodega;
```

### Q5. Top SPs ejecutados (resetea con reinicio SQL)
```sql
USE [IMS4MB_BECOFARMA_PRD];

SELECT TOP 30
    OBJECT_NAME(object_id, database_id) as sp,
    execution_count,
    total_elapsed_time/1000 as ms_total,
    total_elapsed_time/NULLIF(execution_count,0)/1000 as ms_avg,
    last_execution_time
FROM sys.dm_exec_procedure_stats
WHERE database_id = DB_ID()
ORDER BY execution_count DESC;
```

## Cross-refs al brain

### Documentación detallada

- 📄 Ficha detallada: [`clients/becofarma.md`](../clients/becofarma.md)
- 🐞 BUG-001 cross-cliente: [`wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md`](../wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md)
- 🔄 Cross-comparativa F4: [`data-deep-dive/CROSS-COMPARATIVA.md`](../data-deep-dive/CROSS-COMPARATIVA.md)
- 🧬 Heat-map params cross-cliente: `heat-map-params/cross-cliente/`
- 🔬 Diffs código 2023↔2028: `code-deep-flow/DIFF-BOF-2023-VS-2028.md`, `DIFF-HH-2023-VS-2028.md`
- 🚩 Flags callsites: `code-deep-flow/FLAGS-CALLSITES.md`

### Learnings relevantes
- [`learnings/L-013-sapbosync-exe.md`](../learnings/L-013-sapbosync-exe.md)
- [`learnings/L-014-becofarma-bd-diagnostica.md`](../learnings/L-014-becofarma-bd-diagnostica.md)
- [`learnings/L-016-log-segmentado.md`](../learnings/L-016-log-segmentado.md)

---
*Generado por F5 (`tools/wms-deep-dive/build_atlas.py`) — 2026-05-05*