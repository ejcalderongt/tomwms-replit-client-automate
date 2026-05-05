---
id: ATLAS-MERHONSA-2026-05-05
tipo: ficha-atlas-cliente
estado: vigente
cliente: merhonsa
clientes: [merhonsa]
db_name: IMS4MB_MERHONSA_PRD
environment: PRD
rama_productiva: dev_2023_estable
erp: "(a confirmar)"
rubro: "(rubro a confirmar)"
modelo_config: "(pendiente análisis)"
fecha: 2026-05-05
autores: [agente-brain-replit]
tags: [atlas, ficha-cliente, cliente/merhonsa, F5]
relacionado_con: [BUG-001, F4, F5, ATLAS]
---

# Atlas — MERHONSA (PRD)

> **Punto de entrada navegable** generado en F5 (2026-05-05). Cruza snapshot operativo F4, BUG-001 cross-cliente, learnings, ADRs y ficha existente.

## Identificación rápida

| Campo | Valor |
|---|---|
| **Cliente** | `merhonsa` |
| **Database** | `IMS4MB_MERHONSA_PRD` en EC2 `52.41.114.122:1437` |
| **Environment** | PRD |
| **Rubro** | (rubro a confirmar) |
| **ERP destino** | (a confirmar) |
| **Rama productiva** | `dev_2023_estable` |
| **Modelo configuración** | (pendiente análisis) |
| **Fingerprint** | `(pendiente)` |
| **BUG-001 severidad** | n/a (backup vacío en CLIENTES-AFECTADOS, refresh 2026-04 muestra 0 daños / 1.014M outbox) |

> ATÍPICO: 0 trans_picking_ubic / 0 trans_picking_det / 1.014M filas en outbox. Ratio inusual sugiere uso solo de cabeceras o flujo distinto al estándar TOM.

## Snapshot operativo F4 (mediciones del 2026-05-05)

### BUG-001 — métricas

| Métrica | Valor |
|---|---:|
| Marcas `dañado_picking = 1` (histórico) | **0** |
| Marcas `dañado_verificacion = 1` (histórico) | 0 |
| Outbox `tipo_documento = 'AJCANTN'` | **0** |
| Ajustes locales (`trans_ajuste_enc`) | 874 |
| `Enviado_A_ERP = 1` | 108 (12%) |
| `Enviado_A_ERP = 0/NULL` (pendientes) | **766** (88%) |

### Bodegas configuradas (2)

| IdBodega | código | nombre | activo |
|---|---|---|---|
| 1 | BV01 | VILLANUEVA | ✓ |
| 2 | BT1 | TEGUCIGALPA | ✓ |

### Outbox `i_nav_transacciones_out`

- **Total**: 1,014,936 filas
- **Pendientes (enviado=0)**: 11,317 (1.1%)

### Logs disponibles

- Tablas pobladas: `log_error_wms`
- Total filas log_error_wms_*: 3

📂 **Snapshot completo**: [`data-deep-dive/merhonsa/snapshot-2026-05-05.md`](../data-deep-dive/merhonsa/snapshot-2026-05-05.md)

## Diagnostic queries (copy-paste contra EC2)

> Server: `52.41.114.122,1437` · login: `sa` · permisos: read-only.

### Q1. Daños activos sin ajuste (BUG-001)
```sql
USE [IMS4MB_MERHONSA_PRD];

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
USE [IMS4MB_MERHONSA_PRD];

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
USE [IMS4MB_MERHONSA_PRD];

SELECT tipo_documento, IdTipoDocumento,
       COUNT(*) as total,
       SUM(CASE WHEN enviado=0 THEN 1 ELSE 0 END) as pendientes,
       MIN(fec_agr) as primer_pendiente, MAX(fec_agr) as ultimo
FROM i_nav_transacciones_out
WHERE enviado = 0
GROUP BY tipo_documento, IdTipoDocumento
ORDER BY pendientes DESC;
```

### Q4. Heat-map operativo último mes por bodega
```sql
USE [IMS4MB_MERHONSA_PRD];

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
USE [IMS4MB_MERHONSA_PRD];

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

- 📄 Ficha detallada: [`clients/merhonsa.md`](../clients/merhonsa.md)
- 🐞 BUG-001 cross-cliente: [`wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md`](../wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md)
- 🔄 Cross-comparativa F4: [`data-deep-dive/CROSS-COMPARATIVA.md`](../data-deep-dive/CROSS-COMPARATIVA.md)
- 🧬 Heat-map params cross-cliente: `heat-map-params/cross-cliente/`
- 🔬 Diffs código 2023↔2028: `code-deep-flow/DIFF-BOF-2023-VS-2028.md`, `DIFF-HH-2023-VS-2028.md`
- 🚩 Flags callsites: `code-deep-flow/FLAGS-CALLSITES.md`

---
*Generado por F5 (`tools/wms-deep-dive/build_atlas.py`) — 2026-05-05*