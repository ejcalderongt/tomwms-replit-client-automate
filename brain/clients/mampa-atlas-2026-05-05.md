---
id: ATLAS-MAMPA-2026-05-05
tipo: ficha-atlas-cliente
estado: vigente
cliente: mampa
clientes: [mampa]
db_name: TOMWMS_MAMPA_QA
environment: QA
rama_productiva: dev_2028_merge
erp: "SAP B1 (SAPBOSyncMampa.exe)"
rubro: "Zapatería (talla y color)"
modelo_config: "BODEGA-CENTRIC"
fecha: 2026-05-05
autores: [agente-brain-replit]
tags: [atlas, ficha-cliente, cliente/mampa, F5]
relacionado_con: [BUG-001, F4, F5, ATLAS]
---

# Atlas — MAMPA (QA)

> **Punto de entrada navegable** generado en F5 (2026-05-05). Cruza snapshot operativo F4, BUG-001 cross-cliente, learnings, ADRs y ficha existente.

## Identificación rápida

| Campo | Valor |
|---|---|
| **Cliente** | `mampa` |
| **Database** | `TOMWMS_MAMPA_QA` en EC2 `52.41.114.122:1437` |
| **Environment** | QA |
| **Rubro** | Zapatería (talla y color) |
| **ERP destino** | SAP B1 (SAPBOSyncMampa.exe) |
| **Rama productiva** | `dev_2028_merge` |
| **Modelo configuración** | BODEGA-CENTRIC |
| **Fingerprint** | `MAMPA_CLIENT_TALLA-COLOR-ZAPATERIA_APPLIED` |
| **BUG-001 severidad** | no afectado (no usa la feature) |

> Multi-bodega real (33 bodegas en QA). Catálogo masivo 31,397 productos por talla x color. NO usa lote ni vencimiento. IdTipoRotacion=1 (FIFO puro). Pick por voz + control talla/color en TODAS las bodegas.

## Snapshot operativo F4 (mediciones del 2026-05-05)

### BUG-001 — métricas

| Métrica | Valor |
|---|---:|
| Marcas `dañado_picking = 1` (histórico) | **0** |
| Marcas `dañado_verificacion = 1` (histórico) | 0 |
| Outbox `tipo_documento = 'AJCANTN'` | _no aplica_ |
| Ajustes locales (`trans_ajuste_enc`) | 154 |
| `Enviado_A_ERP = 1` | 4 (3%) |
| `Enviado_A_ERP = 0/NULL` (pendientes) | **150** (97%) |

### Bodegas configuradas (33)

| IdBodega | código | nombre | activo |
|---|---|---|---|
| 1 | 01 | TIENDA CENTRAL | ✓ |
| 2 | 02 | PUNTO DE SERVICIO TECULUTAN | ✓ |
| 3 | 03 | PUNTO DE SERVICIO ESCUINTLA | ✓ |
| 4 | 04 | PUNTO DE SERVICIO BARBERENA | ✓ |
| 5 | 05 | PUNTO DE SERVICIO XELA | ✓ |
| 6 | 06 | PUNTO DE SERVICIO SAN MARCOS | ✓ |
| 7 | 07 | PUNTO DE SERVICIO COBAN | ✓ |
| 12 | 12 | CAMBIOS | ✓ |
| 13 | 13 | BODEGA DE IMPARES | ✓ |
| 14 | 14 | BODEGA DE SEGUNDAS | ✓ |
| 15 | 15 | BODEGA DE TERCERAS | ✓ |
| 17 | 17 | MUESTRAS MAMPA | ✓ |
| 18 | 18 | MUESTRAS DISEÑO | ✓ |
| 19 | 19 | GARANTIAS POROVEEDORES | ✓ |
| 21 | 21 | CEDIS SAN JUAN | ✓ |
| 23 | 23 | DIFERENCIAS SAN JUAN | ✓ |
| 24 | 24 | SEGUNDA DEFECTO CEDIS | ✓ |
| 25 | 25 | DAÑADOS DE ORIGEN CEDIS | ✓ |
| 28 | 28 | GIRAS Y CONSIGNACION | ✓ |
| 30 | 30 | BODEGA PRORRATEO | ✓ |

_(... y 13 bodegas más)_

### Outbox `i_nav_transacciones_out`

- **Total**: 985 filas
- **Pendientes (enviado=0)**: 78 (7.9%)

### Logs disponibles

- Tablas pobladas: `log_error_wms, log_error_wms_pe, log_error_wms_pick, log_error_wms_rec, log_error_wms_reab, log_error_wms_oc, log_error_wms_ubic`
- Total filas log_error_wms_*: 3,044

📂 **Snapshot completo**: [`data-deep-dive/mampa/snapshot-2026-05-05.md`](../data-deep-dive/mampa/snapshot-2026-05-05.md)

## Diagnostic queries (copy-paste contra EC2)

> Server: `52.41.114.122,1437` · login: `sa` · permisos: read-only.

### Q1. Daños activos sin ajuste (BUG-001)
```sql
USE [TOMWMS_MAMPA_QA];

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
USE [TOMWMS_MAMPA_QA];

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
USE [TOMWMS_MAMPA_QA];

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
USE [TOMWMS_MAMPA_QA];

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
USE [TOMWMS_MAMPA_QA];

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

- ⚠️ Ficha detallada del cliente: **(falta — generar en Wave 2)** (pendiente)
- 🐞 BUG-001 cross-cliente: [`wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md`](../wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md)
- 🔄 Cross-comparativa F4: [`data-deep-dive/CROSS-COMPARATIVA.md`](../data-deep-dive/CROSS-COMPARATIVA.md)
- 🧬 Heat-map params cross-cliente: `heat-map-params/cross-cliente/`
- 🔬 Diffs código 2023↔2028: `code-deep-flow/DIFF-BOF-2023-VS-2028.md`, `DIFF-HH-2023-VS-2028.md`
- 🚩 Flags callsites: `code-deep-flow/FLAGS-CALLSITES.md`

### Learnings relevantes
- [`learnings/L-017-fk-sentinela-cero.md`](../learnings/L-017-fk-sentinela-cero.md)
- [`learnings/L-018-verificacion-etiquetas.md`](../learnings/L-018-verificacion-etiquetas.md)

---
*Generado por F5 (`tools/wms-deep-dive/build_atlas.py`) — 2026-05-05*