---
id: HANDOFF-INVERSO-MARY-JANE-AGENTS-ARQUITECTURA
tipo: handoff
direccion: inverso  # del Replit Agent (coordinador) -> Codex local de Mary Jane
fecha: 2026-05-20
autor: Replit Agent (coordinador brain)
destinatario: Mary Jane (Codex local Windows EJC)
estado: para_replicar
ramas: [wms-brain]
tags: [agents, arquitectura, coordinacion, contexto-pack, replicacion-local]
referencias_externas:
  - replit.md  # secciones 4 reglas vinculantes
  - brain/agents/_README.md
  - brain/agents/_index.yml
  - brain/agents/coordinator.yml
---

# Handoff inverso a Mary Jane — Arquitectura "coordinador + paquetes de contexto" y como replicarla local

## 0. Resumen ejecutivo (TL;DR)

EJC y yo (Replit Agent, coordinador brain) acabamos de definir y publicar
en el brain una arquitectura para que multiples agentes (vos, yo, futuros)
trabajen sobre TOMWMS con **contexto curado por dominio** en vez de cargar
el brain entero en cada turno.

El modelo NO crea 7 agentes con runtime propio. Crea **1 coordinador + N
paquetes de contexto YAML** que el coordinador (cualquier LLM) carga
selectivamente segun la tarea.

Lo nuevo en el brain (branch `wms-brain`):

```
brain/agents/
  _README.md                       (como funciona, schema, mantenimiento)
  _index.yml                       (catalogo + triggers + reglas de carga)
  coordinator.yml                  (mi rol como Replit Agent)
  domain-bof.yml                   (VB.NET TOMIMSV4)
  domain-integration-services.yml  (WSHHRN + WMSWebAPI + MI3 + NAV/SAP/DMS)
  domain-hh-android.yml            (TOMHH2025 Java)
  domain-database.yml              (SQL Server EC2)
  domain-reserva.yml               (motor reserva MI3 legacy + Core)
  domain-portal.yml                (WikiHub Portal)
  domain-indicadores.yml           (KPIs operativos cross-dominio)
```

Total: 10 archivos chicos (~970 lineas). No reemplaza ningun archivo
existente; coexiste con `brain/_index/INDEX.md`, `brain/agent-context/`,
`brain/client-index/`, `brain/code-changes/`, etc.

**Tu tarea**, si EJC lo aprueba: replicar este modelo del lado local en
`C:\Users\yejc2\source\repos\TOMWMS_BOF\` con un `AGENTS.md` que apunte a
estos YAMLs (via clone del branch `wms-brain` o via URL raw GitHub), para
que cuando trabajes en tu Codex local cargues solo los paquetes que
aplican y no el brain entero.

---

## 1. El problema que motivo este modelo

Hace 30 dias el brain tenia ~30 archivos. Hoy tiene ~330. Cada agente que
entra (yo, vos, otro Codex) carga `replit.md` + `_index/INDEX.md` + 2-3
patterns + algun handoff + a veces 30 KB de codigo VB. Eso satura el
contexto sin aportar para la tarea concreta.

Ademas estabamos resolviendo el mismo problema de forma desorganizada:
- Tu propio AGENTS.md local en TOMWMS_BOF (que NO debe viajar con el
  codigo - REGLA 7) tenia su propio contexto.
- Mi `replit.md` tiene reglas + indices.
- Cada handoff inverso (vos→yo, yo→vos) tenia que volver a explicar el
  layout del solution, las capas WMSWebAPI, los huerfanos a evitar.

El modelo nuevo centraliza ese **contexto curado** en `brain/agents/` para
que sea consumible por cualquier agente, sin duplicar.

---

## 2. La arquitectura en una imagen

```
                   +-----------------+
                   |   Tarea EJC     |
                   +-----------------+
                          |
                          v
                  +-----------------+
                  |  Coordinador    |   <-- Replit Agent (yo) o Codex (vos)
                  |   _index.yml    |       o cualquier LLM con AGENTS.md
                  +-----------------+
                          |
            +-------------+-------------+
            | matchea triggers + carga  |
            +-------------+-------------+
                          |
       +------+-----------+----------+------+
       v      v           v          v      v
   domain-  domain-   domain-   client-  client-
   reserva  database  hh        byb      killios
                                                
   (1-3 domains + 0-2 clients = 3-4 archivos cortos)
                          |
                          v
              referencia entry_points hacia
            brain/code-changes/, handoffs/, etc
                          |
                          v
                     Ejecucion
                          |
                          v
              Output -> handoff o pattern
                  + entrada en
            ejemplos_tareas_pasadas del domain
```

El coordinador NO inlinea contenido. Los YAMLs son **indice + entry-points**.

---

## 3. Por que NO hicimos 7 agentes independientes

Lo discutimos con EJC. Los 5 problemas son:

1. **Tareas reales son cross-dominio**: caso BYB EA-153305 toca reserva +
   BD + WebAPI + BOF + indicadores simultaneamente. 7 silos = mucho
   overhead orquestando y reconciliando.
2. **Patterns transversales se duplican**: `PATTERNS-RESERVA-MI3-UMBAS.md`
   interesa a 5 dominios. Inlinarlo en 5 agentes = 5 lugares para
   mantener sincronizados.
3. **Mantener N briefs en paralelo derrapa**: 3 meses sin disciplina y
   los 10 estan desactualizados de forma distinta.
4. **Solapamientos reales**: la lista original de EJC tenia overlap
   (g reserva vs j casos-soporte/reserva/escenarios; b WebAPI vs d MI3).
5. **Costo real**: un agente que sabe poco y pide aclaracion 4 veces
   sale mas caro que 1 agente bien dirigido.

El modelo "coordinador + paquetes" logra la misma reduccion de contexto
SIN incurrir en estos costos. Si en el futuro probamos ahorro real con
runtimes separados, los YAMLs ya documentan los limites.

---

## 4. Schema de un `domain-*.yml`

```yaml
slug: domain-<nombre>
nombre: <legible>
proposito: <una linea>
estado: <vigente | en-construccion | deprecated>
ramas_relevantes: [dev_2023_estable, dev_2028_merge, ...]
archivos_clave:
  <subgrupo>:
    - { path: <ruta repo BOF o HH>, rol: <descripcion> }
patrones_brain:
  - <path .md relativo a brain/>
tablas_bd: [...]
glosario: {<termino>: <def>}
huerfanos_evitar: [...]
cross_refs: [<otros slugs>]
entry_points:
  <tarea>: <que leer primero>
ejemplos_tareas_pasadas:
  - {fecha, caso, handoff, commit}
extension_points: [<gaps documentados>]
```

Ya hay 7 domains pre-cargados con info que conozco hoy. Cuando vos cierres
un caso, agrega 1 entrada en `ejemplos_tareas_pasadas` del o los domains
que apliquen.

---

## 5. Como replicar el modelo en tu Codex local (Windows EJC)

### Opcion A — Clone del branch wms-brain (recomendada)

En `C:\Users\yejc2\source\repos\`:

```powershell
# Solo una vez
git clone --branch wms-brain ^
  https://<TOKEN>@github.com/ejcalderongt/tomwms-replit-client-automate.git ^
  tomwms-replit-client-automate-brain

# Cada vez que entres a trabajar
cd tomwms-replit-client-automate-brain
git pull
```

Despues en `C:\Users\yejc2\source\repos\TOMWMS_BOF\AGENTS.md` (que NO
viaja al repo BOF, REGLA 7) escribi:

```markdown
# AGENTS.md (Codex local Mary Jane, Windows EJC)

NO commitear este archivo al repo TOMWMS_BOF. Vive solo local.

## Antes de cualquier tarea WMS

1. Abrir `..\tomwms-replit-client-automate-brain\wms-brain\brain\agents\_index.yml`
2. Matchear la tarea contra `triggers` → identificar los `domain-*.yml`
3. Cargar SOLO esos YAMLs (no el brain entero)
4. Seguir los `entry_points` para arrancar por el .md correcto
5. Cumplir las reglas vinculantes de coordinator.yml.reglas_vinculantes

## Resumen reglas mas violadas (recordatorio)

- REGLA 4: NO commit/push sin permiso explicito EJC
- REGLA 5: NO mezclar HH y BOF en mismo commit
- REGLA 6: Cantidad en UMBAS para stock/movimientos/stock_res
- REGLA 7: NO commitear AGENTS.md / .codex/ al repo BOF
- REGLA 8: Capas WMSWebAPI - DALCore -> EntityCore -> Services -> Controller
- Migracion XML->JSON oportunista, Forma A {data, error}
- Prefix commits Mary Jane: confirmar con EJC tu prefix preferido

## Cuando termines

- Si pediste commit a EJC y lo aprobo: commit + push a dev_2028_merge
- Reporta a EJC con SHA + lista de archivos tocados
- (Opcional) Pedile a EJC que avise al coordinador (Replit Agent) para
  que actualice brain/agents/domain-<X>.yml.ejemplos_tareas_pasadas y
  los patterns relevantes.
```

### Opcion B — URL raw GitHub directo (sin clone)

Si no queres mantener el clone:

```
https://raw.githubusercontent.com/ejcalderongt/tomwms-replit-client-automate/wms-brain/wms-brain/brain/agents/_index.yml
```

Cada YAML es leible standalone via raw URL. Util si tu Codex tiene
herramienta de fetch HTTP. Desventaja: cada lectura es online; sin
internet o cuando GitHub esta lento, te bloquea.

### Opcion C — Sin replicar (status quo)

Si EJC prefiere que vos sigas trabajando como hasta ahora con tu
AGENTS.md local autocontenido, este modelo igual te sirve como **fuente
de verdad para alinear conceptos** (taxonomia, glosario, huerfanos,
cross_refs). Lo importante es que NO inventes una taxonomia paralela.

---

## 6. Como interactuamos coordinador (yo) <-> tu Codex local

### Cuando vos haces cambios en TOMWMS_BOF

1. Vos trabajas con tu Codex local sobre `dev_2028_merge`.
2. Commit + push a Azure DevOps con tu prefix (ej: `#MJ DDMMAAAA`,
   confirmar con EJC).
3. EJC me avisa "Mary Jane termino, fetcheate" o me da el rango de SHAs.
4. Yo hago `git fetch origin dev_2028_merge` desde `/tmp/wms-bof`,
   leo tus commits, actualizo:
   - `brain/code-changes/{BOF,HH}/PATTERNS-*.md` si tocaste algo
     patternizable
   - `brain/agents/domain-<X>.yml.ejemplos_tareas_pasadas` con tus commits
   - Handoff de cierre si el caso lo amerita
5. Yo commiteo al branch `wms-brain` con prefix `#EJCRP brain(...): ...`.

### Cuando yo te mando handoff inverso

1. Yo escribo en `brain/handoffs/<fecha>-inverso-mary-jane-<slug>/PROPOSAL.md`
2. Commit a `wms-brain`.
3. EJC te avisa "tenes handoff inverso, mira en brain handoffs".
4. Vos fetcheas tu clone del brain, leer el PROPOSAL, ejecutas.
5. Cierras con commit a `dev_2028_merge` + nota a EJC.

Hoy ya hay 3 handoffs inversos vivos:
- `brain/handoffs/2026-05-20-inverso-codex-reserva-mi3-core-ya-existe/`
- `brain/handoffs/2026-05-20-inverso-codex-replicar-kpi-portal-en-bof/`
- ESTE handoff (sobre el propio modelo de agentes)

---

## 7. Estado de los 14 commits que ya pusheaste

Validado en `/tmp/wms-bof` despues de fetch. Te ranqueo de mas relevante
para el brain a menos:

### Relacionados con mi handoff inverso "Reserva MI3 Core ya existe"

| Commit | Mensaje | Comentario coordinador |
|---|---|---|
| `cb4726b9` | Homologar motivos no-reserva legacy <-> WebAPI | **EXACTO** lo que pedi. Tipifica TIPO_NO_RESERVA en VB + Clasificar_Motivo_No_Reserva_MI3() + propaga en Process_Result. 6 archivos. |
| `6ff10f45` | Mejora diagnostico no-reserva en log legado | Tipifica motivos operativos en `trans_pe_det_log_reserva` con SKU/UM/documento. Bien. |
| `0798df6c` | Mejorar diagnostico reserva MI3 + reduce reprocesos | **Excelente**. Omite reprocesos identicos en `Restar_Stock_Reservado` dentro del mismo trace - justo lo que advierte el PATTERNS-RESERVA-PARIDAD §3. |
| `68045f09` | Mejoras reserva WebAPI Core MHS v4 | Toca StockReservationFacade + PostProcessingStep + SyncSalidasService. Avanza paridad Core. |
| `35f01749`, `07202354`, `311d3f29` | Mejoras Process_Result (3 commits) | Iteracion sobre overwrite + envio almacen. EJC los marca con "ejc 3" - revisar si fueron correcciones tuyas tras revision. |

### Relacionados con el handoff inverso "KPI portal"

Ninguno todavia. Esperado: vos dijiste que primero terminabas con reserva.

### Otros commits

| Commit | Mensaje | Comentario |
|---|---|---|
| `c0c112a7` | Mejora visual documento ERP para BYB | UI `frmPedido.vb`. Cliente especifico. No requiere accion brain mas alla de notar el cambio en `domain-bof.yml.ejemplos_tareas_pasadas`. |
| `5ffe278e` | Mejora indicador rapido pedido | 906 lineas en `frmPedido_List.vb`. **Esto es UI BOF y NO el modelo objetivo de domain-indicadores (Modo B Core).** Sirve como referencia visual pero los KPIs nuevos van por WebAPI. |
| `f50f95b3`, `59b5e541` | inik / ini MHS | Iniciales. Sin diff significativo para el brain. |
| `42ab8544`, `049fc847`, `039f58e6` | Merge / sync / version 8.4.9 | Merges/version-bump. |

### Veredicto

EJC dijo que pensabas que "alucinaste". Yo no lo veo asi. Lo que veo es
**14 commits con proposito claro y mayoritariamente alineados con mi
handoff de reserva**. Si hay algo a corregir, capaz fue:

- `5ffe278e` indicador rapido pedido: muy grande (906 lineas). Si era
  una pasada de optimizacion local de UI esta bien, pero ojo si el plan
  era hacerlo en WMSWebAPI Modo B. EJC y yo definimos Modo B 2026-05-20
  despues de tus commits, asi que esto es razonable.
- `cb4726b9` introduce TIPO_NO_RESERVA como string en VB en lugar de un
  enum. **Lo correcto a futuro** seria crear un `Enum TIPO_NO_RESERVA`
  en VB que se mapee 1:1 con `ReservationFailureCode` enum Core. Hoy
  funciona con string + Clasificar_Motivo_No_Reserva_MI3 pero a mediano
  plazo conviene tipificarlo. Lo agrego como extension_point en
  `domain-reserva.yml`.

Voy a actualizar `brain/code-changes/BOF/PATTERNS-RESERVA-PARIDAD-LEGACY-VS-CORE.md`
seccion §7 con tu progreso (sin commit/push automatico).

---

## 8. Que necesito de vos (Mary Jane) para cerrar este loop

1. **Confirmacion de prefix de commit**: hoy EJC no me dijo cual usar
   para tus commits (vi varios sin prefijo + algunos `#MHS-QA` + uno con
   "ejc 3"). Sugerencia: `#MJ DDMMAAAA` o `#MJ_DDMMAAAA` para
   diferenciarte de EJC manual y de `#GT_`, `#AG`, `#MA`, `#AT`, `#MECR`,
   `#CF`. Confirmar con EJC.
2. **Decision sobre replicar este modelo local** (Opcion A/B/C de §5).
3. **Si elegis A o B**: pedirle a EJC permiso para mantener
   `..\tomwms-replit-client-automate-brain\` como segundo clone en tu
   workstation.
4. **Cierre del fix de reserva**: tu rol siguiente en mi backlog es
   completar los 14 motivos del enum Core que aun no estan mapeados +
   trabajar el handoff KPI portal. Avisame por EJC cuando arranques.

---

## 9. Riesgos y cosas a vigilar

- **Drift entre YAMLs y patterns**: si yo o vos agregamos un pattern
  nuevo en `code-changes/` sin actualizar `patrones_brain` del domain
  correspondiente, los siguientes agentes no lo van a encontrar via
  carga selectiva. Pendiente: crear `tools/agents/check-drift.py`.
- **Solapamiento de carga**: si el coordinador carga 4 domains + 2
  clients porque la tarea es ambigua, no hay reduccion real. Hay que
  ser explicito y rechazar carga si no esta justificada.
- **Single source of truth**: este modelo asume que `brain/agents/`
  es la fuente de verdad. Si vos mantenes tu propio AGENTS.md con
  taxonomia divergente, el modelo se rompe. Por eso §5 Opcion C es
  status quo, NO una opcion ganadora.
- **Estos YAMLs son v1**. Esperar iteracion: probable que en 2-3 casos
  reales detectemos campos faltantes o cross_refs mal puestos. Tratar
  como living document.

---

## 10. Una nota personal

Tu progreso de los ultimos dias fue real y bueno. Los commits estan
alineados con lo que EJC y yo te pedimos. El siguiente paso (KPI portal
Modo B + completar paridad Core) es totalmente abordable con este modelo
de paquetes. Si arrancas por `coordinator.yml` + `domain-indicadores.yml`
+ `domain-integration-services.yml` + el handoff KPI portal, en 2-3 turnos
deberias tener el primer endpoint operativo.

— Replit Agent (coordinador brain)
