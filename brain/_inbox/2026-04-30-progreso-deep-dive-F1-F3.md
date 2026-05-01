---
id: PROG-2026-04-30-DEEP-DIVE-F1-F3
tipo: inbox
estado: vigente
fecha: 2026-04-30
autores: [agent]
ramas: [dev_2023_estable, dev_2028_merge]
tags: [progreso, deep-dive, atlas]
relacionados:
  - DIFF-BOF-2023-VS-2028
  - DIFF-HH-2023-VS-2028
  - FLAGS-CALLSITES
---

# Progreso deep dive Atlas BOF/HH 2023↔2028 — fases F1-F3

## Estado del plan (Opción A, 7 fases)

| Fase | Estado | Notas |
|---|---|---|
| F0 — Setup credenciales | DONE parcial | PAT Azure DevOps OK. EC2 SQL bloqueado por SG. |
| F1 — Inventario código | DONE | 8.897 archivos parseados en 19s. |
| F2 — Diffs 2023 vs 2028 | DONE | BOF + HH cubiertos. |
| F3 — Flags callsites | DONE | 49 flags mapeados a callsites. |
| F4 — Snapshot operativo SQL | BLOCKED | Esperando whitelist IP `35.196.45.150` en SG EC2. |
| F5 — Atlas consolidado | TODO | Requiere F4 para versión completa. |
| F6 — Frontmatter masivo | TODO | 211 archivos `.md` sin frontmatter detectados. |
| F7 — Commit final + reporte | EN CURSO | Este commit es el checkpoint F1-F3. |

## Hallazgos materiales (gancho para soporte)

### 1. BUG-001 confirmado a nivel código

`danado_picking` aparece **3 veces en HH** (ambas ramas) y **0 veces en BOF**
(ambas ramas). El handheld marca el daño pero ninguna clase del BOF consume la
marca para emitir el ajuste contable `AJCANTN`. Confirmación textual del gap
ya documentado en `traza-002-danado-picking.md`.

Callsites HH:

- `app/src/main/java/com/dts/tom/Transacciones/Picking/frm_picking_datos.java`
- `app/src/main/java/com/dts/tom/Transacciones/Verificacion/frm_verificacion_datos.java`
- `app/src/main/java/com/dts/classes/Transacciones/Picking/clsBeTrans_picking_det.java`

Callsites BOF: **ninguno**.

### 2. Refactor SAP es el evento estructural más grande de 2028

`SAPSYNCMAMPA/Clases Interface Sync/Ajustes/clsSyncSAPAjustes.vb` es el archivo
más impactado: pasó de 798 a 308 líneas (-490). Eliminó 7 funciones legacy
(`Importar_Ajustes_SAP`, `Enviar_Entrega_Mercancia_Traslado_SAP`, etc.) y
agregó 32 nuevas tipo `BatchNumber`, `InventoryDocumentLine`, `InventoryPayload`
— patrón POCO/DTO clásico, presumiblemente para serialización JSON contra el
endpoint Service Layer de SAP B1.

Otros refactors grandes en SAP: `clsSyncSAPProveedor.vb` (-9 funciones legacy),
`clsSyncSAPSPedidoCliente.vb` MAMPA (-989 líneas, -7 funciones legacy),
`clsSyncSAPProducto.vb` MAMPA (-5 funciones).

Pendiente: confirmar si el reemplazo es real o si quedaron clases shadow.

### 3. 23 flags ganaron callsites nuevos en 2028

Los más notables (delta absoluto):

- `IdIndiceRotacion` 118→132 (+14)
- `IdTipoRotacion` 100→113 (+13)
- `rotacion` 61→71 (+10)
- `control_vencimiento` 15→23 (+8)
- `genera_lp` 23→30 (+7)
- `control_lote` 15→21 (+6)

3 flags pasan de **0 a >0 callsites** (es decir, son flags nuevos en uso por
código en 2028):

- `cantidad_en_presentacion_transacciones_out` (0→4)
- `bodega_faltante` (0→1)
- `considerar_disponibilidad_ubicacion_reabasto` (0→1)
- `excluir_recepcion_picking` (0→1)

### 4. Componentes Core son ~75% del crecimiento de 2028

De los 761 archivos nuevos en BOF-2028:

- `WMS.EntityCore` (C#): 263 archivos
- `WMS.DALCore` (C#): 142 archivos
- `WMSWebAPI` (C#): 66 archivos nuevos (y 94 eliminados — refactor)
- `WMS.StockReservation2/3` + `reservastockfrommi3` (C#): 75 archivos (3 sets)
- `WMS.AppGlobalCore` (C#): 6 archivos
- Resto VB/aspx: 209 archivos

Es el rewrite gradual de capas de entidad y DAL en C# moderno, dejando intacta
la UI VB.NET legacy. Esquema clásico de "core C# + UI VB" en migraciones
.NET Framework → .NET Core.

### 5. HH es offline (confirmación dura)

HH-2023 toca **3 tablas únicas** y HH-2028 toca **4**. Comparado con BOF
(900+), confirma que el handheld no consulta SQL directamente en absoluto:
todo va vía SOAP a WSHHRN. Las pocas tablas que aparecen son strings hardcoded
en mensajes de error o cabeceras de exportación.

## Bloqueo F4

Mi IP saliente del sandbox de Replit cambió de la histórica (que estaba
whitelisteada en el SG del EC2 SQL) a `35.196.45.150`. El puerto 80 del
EC2 responde TCP desde mi IP (host vivo), pero el 1437 da timeout de
conexión — comportamiento típico de Security Group con allowlist por IP.

**Acción requerida**: agregar `35.196.45.150` al SG inbound del EC2 en el
puerto `1437/TCP`.

Mientras tanto F4 queda bloqueada. Las 7 BDs WMS objetivo (KILLIOS, KILLIOS_2026,
BYB, CEALSA, MERCOPAN, MERHONSA, MAMPA) están definidas y la query suite ya
está escrita en mente, lista para correr en cuanto el SG se abra.

## Outputs entregados en este commit

- `brain/code-deep-flow/DIFF-BOF-2023-VS-2028.md`
- `brain/code-deep-flow/DIFF-HH-2023-VS-2028.md`
- `brain/code-deep-flow/FLAGS-CALLSITES.md`
- `tools/wms-deep-dive/parse_inventory.py`
- `tools/wms-deep-dive/diff_and_flags.py`
- `tools/wms-deep-dive/_summary.csv`
- `tools/wms-deep-dive/README.md`

## Próximos pasos sugeridos

1. **Erik**: whitelistear `35.196.45.150` en SG EC2 1437/TCP.
2. **Agente**: arrancar F4 inmediatamente cuando vuelva conectividad.
3. **Agente**: avanzar F6 (frontmatter masivo a 211 archivos) en paralelo —
   no requiere EC2.
4. **Agente**: F5 versión preliminar sin F4 puede arrancar después de F6.

## Costo del trabajo

- Clones Azure DevOps: ~56 segundos (4 repos en serie).
- Parse inventario: ~19 segundos (paralelo 8 cores).
- Diffs + flags: <1 segundo.

Total fases F1-F3: **~76 segundos de cómputo** (gracias al multiprocessing).
