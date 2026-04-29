# Encargo SQL Agente - Pasada 8a (5 cards)

> **De**: Erik Calderon (PrograX24) via agente brain (sesion replit, 28 abril 2026)
> **Para**: SQL Agente (mismo perfil que ejecuto tanda-2)
> **Plazo sugerido**: 1 sesion de trabajo (15-20 minutos efectivos)
> **Bloqueo**: este encargo destraba **6 decisiones** del WebAPI .NET 8 nuevo
> (P-16b) y la ratificacion firme de **ADR-012**.

## Que hay que hacer

Ejecutar **10 invocaciones** de un comando PowerShell ya construido y entregar
los CSVs resultantes via commit a la rama `wms-brain-client`.

## Pre-requisitos del entorno

Tu maquina ya los tiene si hiciste tanda-2. Re-confirmacion:

1. **Modulo PowerShell `WmsBrainClient`** instalado y cargable
   (`Import-Module WmsBrainClient` debe funcionar).
2. **3 perfiles SQL configurados** apuntando al EC2 `52.41.114.122,1437`:
   - `K7-PRD` -> TOMWMS_KILLIOS_PRD (READ-ONLY)
   - `BB-PRD` -> TOMWMS_BECOFARMA_PRD (READ-ONLY)
   - `C9-QAS` -> TOMWMS_CEALSA_QAS  (READ-ONLY)
3. **Clone local del repo** `ejcalderongt/tomwms-replit-client-automate`,
   rama `wms-brain-client`, en su ultimo commit (HEAD = `582da718`).

Si algo falta, ver `wms-brain-client/README.md` o pedir setup a Erik antes de
arrancar.

## Como ejecutar

Las 6 cards Q-009..Q-014 viven en `wms-brain-client/questions/`. Q-010 ya esta
cerrada por dump (no hace falta ejecutarla). Quedan **5 cards**, algunas con
multiples targets, que totalizan **10 invocaciones**:

```powershell
# Bloque 1 - Outbox alcance real (3 BDs)
Invoke-WmsBrainQuestion -Id Q-009 -Profile K7-PRD
Invoke-WmsBrainQuestion -Id Q-009 -Profile BB-PRD
Invoke-WmsBrainQuestion -Id Q-009 -Profile C9-QAS

# Bloque 2 - Bypass despachado (P-16b numero firme)
Invoke-WmsBrainQuestion -Id Q-011 -Profile K7-PRD

# Bloque 3 - CEALSA QAS (corte jornada + poliza)
Invoke-WmsBrainQuestion -Id Q-012 -Profile C9-QAS
Invoke-WmsBrainQuestion -Id Q-013 -Profile C9-QAS

# Bloque 4 - TOP15 tareas HH (3 BDs)
Invoke-WmsBrainQuestion -Id Q-014 -Profile K7-PRD
Invoke-WmsBrainQuestion -Id Q-014 -Profile BB-PRD
Invoke-WmsBrainQuestion -Id Q-014 -Profile C9-QAS
```

> **Nota Q-010**: ya cerrada al 100% via `wms-db-brain` dump del SP CLBD_PRC.
> Ver `brain/wms-specific-process-flow/respuestas-tanda-3.md` seccion Q-010.
> NO ejecutar contra K7-PRD, no aporta valor adicional.

Cada invocacion genera, dentro del repo local:

```
wms-brain-client/answers/Q-NNN/
  q1-<descriptor>.csv      <- output de la primera query de la card
  q2-<descriptor>.csv      <- (si la card tiene mas de una query)
  ...
  summary.md               <- generado por el cliente automaticamente
  answer-draft.md          <- draft a completar (ver siguiente seccion)
```

## Que entregar (workflow)

### Paso 1 - Completar `answer-draft.md` de cada card

El cliente PowerShell deja un draft. Vos sumas:

- **Interpretacion 1-2 parrafos**: que dice el dato, sin opinion ni decision
  de producto. Solo lectura del numero.
- **Sanity checks**: si los conteos son anomalos (ej. 0 filas cuando esperabas
  miles, o un crecimiento abrupto en un mes), anotalo.
- **Discrepancia con hipotesis pre-ejecucion** (si la hay): cada card en
  `respuestas-tanda-3.md` tiene una "Decision a tomar segun resultado" -
  marcar cual disparo el resultado real.

### Paso 2 - Push a `wms-brain-client`

Una vez completados los 5 drafts, agregar las carpetas
`wms-brain-client/answers/Q-009/`..`Q-014/` y pushear a la rama
`wms-brain-client` con mensaje tipo:

```
sql-agente@<tu-handle>: pasada 8a - resultados Q-009/Q-011/Q-012/Q-013/Q-014

Ejecucion contra EC2 SQL 52.41.114.122,1437 perfiles K7-PRD/BB-PRD/C9-QAS.
10 invocaciones, todas READ-ONLY. CSVs + drafts completados.

Encargo: brain/wms-specific-process-flow/encargo-sql-agente-pasada-8a.md (rama wms-brain)
```

### Paso 3 - Avisar al brain

Una vez pusheado, dejar un evento en el inbox del brain via PowerShell:

```powershell
New-WmsBrainInboxEvent `
  -Type 'question_answer' `
  -Source 'replit' `
  -Ref @{
    questions_executed = @('Q-009','Q-011','Q-012','Q-013','Q-014')
    invocations = 10
    answers_commit = '<sha-del-commit-anterior>'
    branch = 'wms-brain-client'
  } `
  -Context @{
    summary = 'Pasada 8a - 5 cards ejecutadas, drafts completados'
    notes = 'Ver wms-brain-client/answers/Q-NNN/answer-draft.md para interpretacion por card'
  }
```

Eso emite un event con id auto-generado al regex correcto, status='answered',
schema_version='2'. El agente brain lo procesara en la proxima sesion para
actualizar `respuestas-tanda-3.md` con los datos reales y avanzar Pasada 8b
(roadmap WebAPI .NET 8).

## Validacion / sanity checks por card

Antes de dar por buena cada card, mirale:

| Card | Sanity check |
|------|--------------|
| Q-009 | Total de filas en `i_nav_transacciones_out` debe ser >> 0 en las 3 BDs (en Killios snapshot anterior eran 24.193). Si te da 0, perfil mal apuntado. |
| Q-011 | `pedidos_estado_despachado_total` debe ser >> 43. Si es ~43, query mal escrita. El campo bypass es la **diferencia**, no el total. |
| Q-012 | Tomar TOP 50 en orden DESC por dias_diferencia. Si todos son 0 dias, no hay excepciones (poco probable - Carol mencion lo contrario). |
| Q-013 | En CEALSA QAS deberias ver MUCHOS pedidos fiscales (mayoria de su carga es 3PL/aduana). Si te da 0, perfil mal o BD vacia. |
| Q-014 | Si "Recepcion" no aparece en el TOP3 de Killios o Becofarma, algo raro. |

## Si algo sale mal

- **Connection timeout**: confirmar que tu IP esta en el whitelist del EC2.
  Pedir a Erik el ssh tunnel si hace falta.
- **Login failed**: las credenciales READ-ONLY son las mismas de tanda-2.
- **Card no existe**: verificar `git pull` reciente en `wms-brain-client`.
- **El cliente PowerShell tira error de schema_version**: el bridge en main del
  repo todavia es v1 pero las cards son v2. Por ahora ejecutarlas con flag
  `-AllowSchemaV2Cards` si tu version del modulo lo soporta. Si no, pedirle a
  Erik el bump del bridge (PROTOCOL.md tiene los pasos).

## Contexto rapido (para entender por que importa cada card)

| Card | Decision que se destraba |
|------|--------------------------|
| Q-009 | Si el bridge del WebAPI nuevo soporta 4 tipos de movimiento o solo 2 |
| Q-011 | Calibrar guardrails de `ADR-012` (bypass de estado): permiso, rate-limit, alertas |
| Q-012 | Modelar `JornadaCEALSA` con/sin excepciones de cruce de dia |
| Q-013 | Donde poner los `[Required]` de `PolizaFiscalDto` (validacion de los 11 campos) |
| Q-014 | Que endpoints HH del WebAPI armar primero (priorizacion por uso real) |

Ver `brain/wms-specific-process-flow/respuestas-tanda-3.md` para detalle por card.

## Cierre

Cuando termines y emitas el event `question_answer`, el bloqueo de Pasada 8a
queda 100% cerrado y el agente brain puede arrancar Pasada 8b
(scaffold WebAPI .NET 8 con datos firmes, sin asumir).

Gracias.
