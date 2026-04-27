---
protocolVersion: 1
id: L-011
title: log_error_wms es bitacora mixta (avisos + trazas + errores), no log de errores puros
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-27T15:00:00Z
target:
  codename: TODOS
  environment: PRD
relatedQuestions: [Q-004]
relatedDocs:
  - brain/wms-brain-client/answers/A-004-log-error-wms.md
status: open
priority: medium
tags: [errores, log, K7, BB, PEND-11]
---

## Que aprendimos

`log_error_wms` no es un log de excepciones puras: en K7 acumula
~16k filas/mes y los top mensajes son **trazas operativas**
(`Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario X`,
`AVISO_2024..._HH_CambioEstadoUbic: ubicacion N`). Solo una
fraccion son errores reales (ej. ~2,346 ocurrencias de
NullReferenceException en `Agregar_Marcaje`). El esquema (15
columnas, sin severidad explicita) refleja esto: los SPs lo usan
como bitacora general.

## Evidencia

- Answer card: A-004
- Queries: `Q-004/q1` (esquema), `q3` (top mensajes), `q4`
  (modulos via FK), `q5` (volumen mensual)
- Estructura real:

```
IdError, IdEmpresa, IdBodega, Fecha, MensajeError (K7: nvarchar(2500); BB: nvarchar(max)),
IdPedidoEnc, IdPickingEnc, IdRecepcionEnc, IdUsuarioAgr,
Line_No, Item_No, UmBas, Variant_Code, Cantidad, Referencia_Documento
(NO hay columna 'severidad' ni 'modulo')
```

Volumen K7: 66,339 filas en 4 meses (May-Sep 2025), pico junio-julio
~25k/mes; BB: 203,782 historicas.

## Implicancias

### Para el codigo

- El bridge puede insertar al log usando estos campos pero **debe
  adoptar prefijo claro** (`BRIDGE_ERR:` / `BRIDGE_INFO:`) para
  no mezclarse con avisos del WMS-base.
- Cualquier consumer del log **debe filtrar por exclusion** de
  prefijos `AVISO_*` y `Aplica_*_HH:` antes de alertar.

### Para la operacion

- Monitoreo automatizado **no puede usar `COUNT(*) > N`** como
  alerta — siempre dispararia. Debe filtrar por mensaje.
- Hay un loop ruidoso de `Aplica_Cambio_Estado_Ubic_HH` que
  genera ~50% del log diario; vale la pena revisar si necesita
  loggearse a ese nivel.

### Para el equipo

- Documentar la convencion de prefijos en `interfaces-erp-por-cliente.md`
  o en un nuevo `log_error_wms_convention.md`.
- Considerar agregar columna `Severidad nvarchar(8)` en una
  pasada futura (bumping schema_version).

## Acciones propuestas

- [ ] Definir y documentar prefijos de mensaje (DEBUG/INFO/WARN/ERR/FATAL)
- [ ] Crear vista `VW_LogErrorReal` que excluya AVISO/Aplica_* y
      sea la fuente de monitoreo
- [ ] Investigar el bug `Agregar_Marcaje` NullReferenceException

## Como se cierra esta learning

Cerrar cuando exista la vista `VW_LogErrorReal` (o equivalente)
y el monitoreo apunte a ella, y cuando la convencion de prefijos
este declarada en doc estable.
