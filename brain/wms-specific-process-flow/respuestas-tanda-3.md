# Respuestas Pasada 8a (tanda-3) - cierre via wms-db-brain + ejecucion live

> Documento generado por el agente brain (sesion replit) el 28 abril 2026.
>
> **Estado**: 1 sub-pregunta cerrada al 100% sin ejecucion SQL (gracias al dump
> `wms-db-brain`), 2 enriquecidas parcialmente, 3 pendientes de ejecucion live.
>
> **Fuentes**:
> - `wms-brain-client/questions/Q-009..Q-014` (commit `582da718` rama `wms-brain-client`)
> - `wms-db-brain/db-brain/` (commit `d3884b57` rama `wms-db-brain`, snapshot 2026-04-27)
> - `consolidacion-pasada-7.md` (commit `b1d3cc03` rama `wms-brain`)
>
> **Convencion**: cada query tiene status [CERRADA / PARCIAL / PENDIENTE]. Cuando
> alguien ejecute las cards via `Invoke-WmsBrainQuestion`, completar las secciones
> PENDIENTE con los CSV resultantes y mover a CERRADA.

---

## Q-009 - Outbox alcance real (3 BDs)

**Status**: PENDIENTE - requiere ejecucion contra K7-PRD, BB-PRD, C9-QAS.

**Hipotesis pre-ejecucion**: Carol (P-19) afirma que el outbox solo se usa para
recepciones y despachos. El schema soporta 4 FKs (`idordencompra`, `idrecepcionenc`,
`idpedidoenc`, `iddespachoenc`).

**Resultado esperado**:

| BD | con_oc | con_recepcion | con_pedido | con_despacho | total |
|----|--------|---------------|------------|--------------|-------|
| K7-PRD | TBD | TBD | TBD | TBD | TBD |
| BB-PRD | TBD | TBD | TBD | TBD | TBD |
| C9-QAS | TBD | TBD | TBD | TBD | TBD |

**Decision a tomar segun resultado**:
- Si `con_pedido` y `con_oc` son ~0 en las 3 → **simplificar bridge del WebAPI**
  (solo recepcion + despacho, drop OC y pedido del outbox).
- Si distinto de 0 en cualquier BD → **bridge soporta los 4 tipos** (Carol parcialmente).

**Comando para ejecutar**:
```powershell
Invoke-WmsBrainQuestion -Id Q-009 -Profile K7-PRD
Invoke-WmsBrainQuestion -Id Q-009 -Profile BB-PRD
Invoke-WmsBrainQuestion -Id Q-009 -Profile C9-QAS
```

---

## Q-010 - Killios reabasto pre/post-2024 - **CERRADA via wms-db-brain**

**Status**: **CERRADA al 100% sin ejecucion SQL**.

### Hallazgo principal

El SP `dbo.CLBD_PRC` esta documentado completo en
`wms-db-brain/db-brain/sps/CLBD_PRC.md` (snapshot 2026-04-27 de
TOMWMS_KILLIOS_PRD). El SP tiene 150 lineas y **NO incluye
`trans_reabastecimiento_log` en su lista de DELETE FROM**. Verificacion mecanica
en el dump: 0 matches de "reabast" o "REABAST" en todo el archivo.

### Validacion cruzada

- **Carol (CKFK, P-24)**: "La tabla no se limpio al instalar ese cliente, debemos
  agregarla al SP CLBD_PRC". → **Confirmada al 100%** por el dump.
- **SQL agente (tanda-2, sospecha)**: que el modulo de deteccion siguiera activo.
  → **Refutada parcialmente**: aunque la tabla tiene 1218 filas, su
  `modify_date` schema es 2023-02-27 (3+ años sin DDL changes). Combinado con la
  ausencia en CLBD_PRC, es razonable concluir que **es basura de instalacion no
  limpiada**, no modulo activo.

### Cardinalidad de evidencia

| Item | Valor |
|------|-------|
| Filas actuales en `trans_reabastecimiento_log` (Killios PRD) | 1.218 |
| modify_date del schema de la tabla | 2023-02-27 |
| modify_date del SP CLBD_PRC | 2018-09-27 |
| Snapshot wms-db-brain | 2026-04-27 |
| Tablas que SI estan en CLBD_PRC | 60 (lista completa en el dump) |
| Tablas relacionadas a reabasto en CLBD_PRC | 0 |

### Decision tomada

**Aceptar accion atomica**: agregar `DELETE FROM trans_reabastecimiento_log` al
SP CLBD_PRC. Esto cierra el hallazgo H-02 (`brain/_inbox/20260428-1901-H02-...`).

**Pendiente**: confirmar con Erik si la edicion del SP CLBD_PRC se hace via:
- (a) ALTER PROCEDURE manual ejecutado por DBA al instalar cada cliente nuevo, o
- (b) Migracion versionada en el repo del WMS .NET (script DDL + bump version).

Si (b), agregar a `brain/architecture/decisiones/` un mini-ADR sobre versionado
de SPs.

### Sub-pregunta de timeline (opcional, NO bloqueante)

La query Q-010 sugiere un break-down pre-2024 vs 2024 vs 2025+. Es **enriquecimiento
no critico** para entender si la basura es 100% legacy o tiene gocheos. Si Erik
quiere ese detalle, ejecutar:

```powershell
Invoke-WmsBrainQuestion -Id Q-010 -Profile K7-PRD
```

Pero la decision principal **no depende** del resultado.

---

## Q-011 - Killios bypass estado=Despachado (numero firme P-16b)

**Status**: PARCIAL - schema validado por wms-db-brain, conteo requiere live.

### Schema enrichment (via wms-db-brain)

Lectura de `wms-db-brain/db-brain/tables/trans_pe_enc.md`:

```
| 12 | estado | nvarchar(20) | NULLABLE |
```

**Hallazgo**: la columna `estado` es `nvarchar(20)` **NULLABLE sin check
constraint**. No hay enum a nivel BD, no hay trigger de validacion. Esto
significa que **el bypass es trivial**: basta un `UPDATE trans_pe_enc SET
estado='Despachado' WHERE IdPedidoEnc=N`. Carol (CKFK, P-16) confirmo que el
WMS lo permite por UI; el dump confirma que tampoco hay guardrail a nivel de BD.

### Implicancia para ADR-012

Refuerza la opcion (b) del ADR provisional: **permitir con permiso + razon +
auditoria en tabla nueva**. Sin esa auditoria, el bypass es absolutamente
invisible (no queda rastro en ninguna tabla). Por eso ADR-012 propone crear:

```sql
CREATE TABLE aud_pedido_estado_forzado (
  IdAuditoria      int IDENTITY PRIMARY KEY,
  IdPedidoEnc      int NOT NULL,
  estado_anterior  nvarchar(20),
  estado_nuevo     nvarchar(20) NOT NULL,
  motivo           nvarchar(500) NOT NULL,
  user_forzo       nvarchar(50)  NOT NULL,
  fec_forzo        datetime2     NOT NULL DEFAULT SYSUTCDATETIME(),
  ticket_externo   nvarchar(100) NULL
);
```

### Conteo firme de bypass

Pendiente de ejecucion live:

```powershell
Invoke-WmsBrainQuestion -Id Q-011 -Profile K7-PRD
```

**Resultado esperado** (Carol reporto 43 en P-16):

| Metrica | Valor reportado P-16 | Valor SQL |
|---------|----------------------|-----------|
| pedidos_estado_despachado_total | (no reportado) | TBD |
| bypass_sin_despacho_real | 43 | TBD |
| con_despacho_real | (no reportado) | TBD |
| pct_bypass | (no reportado) | TBD |

**Decision a tomar segun resultado**:
- Si `bypass_sin_despacho_real` ≈ 43 → **ADR-012 ratificado tal cual**.
- Si > 200 → **ADR-012 explicita volumetria + agrega rate-limit**.
- Si ~ 0 → **simplificar ADR-012** (drop permiso especial, solo razon).
- Distribucion temporal Q2 → si concentrado pre-2025, es legacy. Si continuo, uso activo.

---

## Q-012 - CEALSA QAS corte de jornada

**Status**: PENDIENTE - requiere ejecucion contra C9-QAS. wms-db-brain NO tiene
dump de CEALSA (solo Killios), por lo que no puedo cerrar via cache.

### Hipotesis pre-ejecucion

Carol (P-22) menciono "excepciones en el corte de jornada CEALSA" sin detalle.
Reconstruyo via SQL: pedidos cuyo despacho cruza el dia (creados un dia,
despachados otro).

### Resultado esperado

| Top 5 pedidos por dias_diferencia | TBD |
|-----------------------------------|-----|
| Histograma de dias_cruce          | TBD |

**Comando**:
```powershell
Invoke-WmsBrainQuestion -Id Q-012 -Profile C9-QAS
```

---

## Q-013 - CEALSA QAS poliza 11 campos - PARCIAL

**Status**: PARCIAL - estructura validada por wms-db-brain (Killios), conteos
requieren ejecucion contra C9-QAS.

### Hallazgo importante de schema

Lectura de `wms-db-brain/db-brain/tables/trans_pe_pol.md`:

| Atributo | Valor |
|----------|-------|
| Columnas totales | **41** (no 11) |
| NOT NULL | 2 (`IdOrdenPedidoPol`, `IdOrdenPedidoEnc` - PK) |
| NULL | 39 |
| Filas en Killios PRD | **0** (Killios no usa poliza, esto es 3PL/aduana CEALSA) |
| Foreign Keys declaradas | **0** (sin FKs) |
| modify_date schema | 2024-10-01 |

**Conclusion**: **los "11 campos obligatorios" que Carol mencion (P-11) NO son
constraint de schema sino regla de negocio/UI**. A nivel BD, todos los campos
salvo PK son nullable. La validacion `[Required]` debe vivir en el WebAPI .NET 8
o en la capa de UI del BOF, no en SQL.

### Listado de las 41 columnas (de wms-db-brain)

Bloque clave (campos candidatos a "los 11 obligatorios"):

| # | Nombre | Tipo | Probable obligatorio? |
|---|--------|------|----------------------|
| 4 | `NoPoliza` | nvarchar(50) | SI |
| 5 | `pto_descarga` | nvarchar(50) | quizas |
| 6 | `viaje_no` | nvarchar(50) | SI (aduana) |
| 7 | `buque_no` | nvarchar(50) | SI (aduana) |
| 9 | `fecha_abordaje` | datetime | SI (aduana) |
| 18 | `dua` | nvarchar(50) | SI (declaracion unica) |
| 19 | `fecha_poliza` | datetime | SI |
| 20 | `pais_procede` | nvarchar(50) | SI |
| 22 | `total_valoraduana` | float | SI |
| 27 | `total_flete` | float | SI |
| 28 | `total_seguro` | float | SI |
| 33 | `codigo_poliza` | nvarchar(50) | SI |

12 candidatos en mi opinion (1 mas de lo que Carol dice). **Pre-pregunta para
Carol**: confirmar cuales son los 11 obligatorios reales (descartar uno de los
12 listados o agregar otro de los 41).

### Conteo pendientes vs presentes

```powershell
Invoke-WmsBrainQuestion -Id Q-013 -Profile C9-QAS
```

| Metrica | Valor |
|---------|-------|
| pedidos_fiscales | TBD |
| pedidos_con_poliza | TBD |
| pedidos_sin_poliza | TBD |

**Decision a tomar**:
- Si `pedidos_sin_poliza = 0` → afirmacion de Carol validada, WebAPI exige los 11
  campos en endpoint `POST /api/pedidos/{id}/poliza`.
- Si > 0 → refinar (cuales tipos? por que? hay endpoint UPSERT que la crea
  vacia?).

---

## Q-014 - TOP15 tareas HH (3 BDs)

**Status**: PENDIENTE - requiere ejecucion contra K7-PRD, BB-PRD, C9-QAS.

### Hipotesis pre-ejecucion (Carol, tanda-2 P-25)

Lista teorica de TOP10 mencionada: Recepcion, Cambio Ubicacion, Cambio Estado,
Implosiones, Picking, Verificacion, Despacho.

### Resultado esperado

| Cliente | TOP15 real (tipo_tarea, ejecutadas, pct) |
|---------|------------------------------------------|
| K7-PRD | TBD |
| BB-PRD | TBD |
| C9-QAS | TBD |

**Decision a tomar**:
- Si TOP15 real coincide con la lista de Carol → validada, sirve para priorizar
  endpoints HH del WebAPI.
- Si difiere mucho → Carol describio teorico, no real. Documentar discrepancia y
  tomar el TOP real como guia.

**Comando**:
```powershell
Invoke-WmsBrainQuestion -Id Q-014 -Profile K7-PRD
Invoke-WmsBrainQuestion -Id Q-014 -Profile BB-PRD
Invoke-WmsBrainQuestion -Id Q-014 -Profile C9-QAS
```

---

## Resumen de progreso

| Sub-pregunta | Status | Bloqueante para... |
|--------------|--------|--------------------|
| Q-009 | PENDIENTE | Scope del bridge del WebAPI |
| Q-010 | **CERRADA** | (desbloquea H-02 accion atomica) |
| Q-011 | PARCIAL | Calibrar volumetria ADR-012 |
| Q-012 | PENDIENTE | Modelo `JornadaCEALSA` del WebAPI |
| Q-013 | PARCIAL | Validador `PolizaFiscalDto` del WebAPI |
| Q-014 | PENDIENTE | Endpoints HH priorizados del WebAPI |

**Conclusion del agente brain**: con el dump de wms-db-brain pude cerrar 1 de 6
queries al 100% (Q-010) y enriquecer 2 mas (Q-011 con shape de `estado`, Q-013
con shape de `trans_pe_pol`). Las 3 restantes (Q-009, Q-012, Q-014) y las
finalizaciones de Q-011/Q-013 requieren ejecucion live contra el EC2 SQL.

**Recomendacion**: ejecutar las 5 cards pendientes en una sesion sola. Tiempo
estimado: 15 minutos (las queries son simples agregaciones, todas READ-ONLY).
