---
id: mod-conexion-sqlserver
type: module
title: Conexión a SQL Server productivo (referencia técnica)
status: estable
sources:
  - skill: wms-tomwms §8
  - validated_at: 2026-04-27 desde Replit con driver mssql Node
---

# Conexión a SQL Server productivo

> Esta entity es la **referencia técnica** (cómo conectarse, snippets, parámetros). El **rationale** del acceso desde Replit vive en `decisions/dec-2026-04-killios-acceso-replit`.

## Endpoint Killios (cliente activo)

| Dato | Valor |
|---|---|
| Servidor | `52.41.114.122,1437` (EC2 AWS, Windows Server 2022) |
| Puerto | `1437` (no el 1433 default) |
| Versión | SQL Server 2022 CU18 (16.0.4185.3) |
| Edición | Standard |
| Usuario default | `sa` |
| Password | secret `WMS_KILLIOS_DB_PASSWORD` |
| Encrypt / TrustCert | `false` / `true` |

## Bases WMS accesibles

| BD | Cliente | Entorno |
|---|---|---|
| `TOMWMS_KILLIOS_PRD` | Killios | Productivo (target principal) |
| `IMS4MB_BYB_PRD` | BYB | Productivo |
| `IMS4MB_CEALSA_QAS` | Cealsa | QA |

## Driver y snippet Node

```javascript
const sql = require("mssql");
const cfg = {
  server: "52.41.114.122", port: 1437, user: "sa",
  password: process.env.WMS_KILLIOS_DB_PASSWORD,
  database: "TOMWMS_KILLIOS_PRD",
  options: { encrypt: false, trustServerCertificate: true },
};
const pool = await sql.connect(cfg);
const r = await pool.request().query("SELECT TOP 10 * FROM stock");
```

## Driver Python (vía wmsa CLI)

```cmd
wmsa db query "SELECT TOP 10 idstock, codigo, cantidad FROM stock"
wmsa db tables --like %trans_ajuste%
wmsa db sp-text Get_Stock_Actual
```

`wmsa db` valida toda query con whitelist `SELECT|WITH|EXEC|EXECUTE|SET` y rechaza `INSERT|UPDATE|DELETE|MERGE|DROP|ALTER|CREATE|TRUNCATE`.

## Naming de tablas (validado 2026-04-27)

La BD productiva mezcla 2 convenciones:

- **Sin prefijo** (mayoría): `stock`, `cliente`, `producto`, `bodega`, `trans_*`, `cliente_lotes`, `log_*`.
- **Con prefijo `t_`** (minoría heredada): ej. `t_producto_bodega` (42.357 filas, modify_date 2019).
- **Sufijos**: `_bk` (backup, ej. `producto_presentacion_bk`), `_YYYYMMDD` (snapshots, ej. `stock_20250624`), `_hist` (histórico, ej. `stock_hist`).

> El SKILL viejo afirmaba "NO usan prefijo `t_*`" — es **incorrecto**. Convive ambas convenciones. Corrección a propagar al stub del SKILL.

## Catálogo del schema

Detalle completo de tablas, vistas, SPs y funciones del schema productivo: `db-brain://README` (branch `wms-db-brain`).

## Cross-refs
- `decisions/dec-2026-04-killios-acceso-replit` — rationale acceso
- `rules/rule-08-killios-prod-solo-lectura` (M2) — restricción
- `db-brain://_meta/stats` — stats globales del schema
