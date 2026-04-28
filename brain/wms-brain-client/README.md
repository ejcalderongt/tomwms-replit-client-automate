```
                          .---------------------.
                         /                       \
                        /        A C M E         \
                       /                          \
                       \      W M S - B R A I N    /
                        \      C L I E N T        /
                         \                       /
                          '---------------------'
                                  |||
                              ____|||____
                             /  C-4 -ish \
                            (   pero MD   )
                             \___________/
                                  |||
                            *--- BOOM ---*
              (broma — no explota nada sin -Confirm)
```

> _En el principio fue el outbox, y el outbox era `enviado=0`, y el outbox_
> _era SALIDA. Y dijo el ERP: "hagase la luz", y se procesaron 277,310._
> _Pero los INGRESOS quedaron en tinieblas, y nadie noto durante meses._
> _**Esta es la herramienta que enciende la lampara.**_

---

# wms-brain-client

Un modulo PowerShell que tu BackOffice no necesita pero tu cordura sí.

## ¿Que es esto?

`wms-brain-client` es **la navaja suiza local** que cada developer del
equipo PrograX24 corre en su Windows para hablar con el WMS, aprender
cosas del WMS, y dejar el aprendizaje guardado en el repo `wms-brain`
para que el proximo que llegue (o vuelva) no tenga que reaprenderlo
desde cero.

Es como tener un **stetoscopio para SQL Server**. Pones la campanita
en una BD, le preguntas "¿que tenes?" y la BD te canta sus secretos.
Despues los anotas en un cuaderno compartido — pero el cuaderno es
un repo de Git, y los secretos son cards Markdown con front-matter
YAML, porque somos developers y no podemos evitar la sintaxis.

## ¿Por que existe?

Porque hasta ayer:

- Para investigar la BD productiva de cualquiera de los 11 clientes
  habia que pedirle al developer que haga el SELECT, lo pegue al chat,
  y el agente lo interprete. **Cadencia: 1 query cada 30 minutos
  laborales.** Asi no se mueven los proyectos.
- Cada developer descubria una rareza del esquema, la guardaba en su
  cabeza, y a las 3 semanas la olvidaba. La memoria del equipo era
  efimera y duplicada.
- Cuando aparecia un bug raro (ver `wms-specific-process-flow/bug-report-p16b.md`,
  el clasico "Despachado vuelve a Pendiente"), nadie podia
  reproducirlo en su local sin pasar 4 horas armando el escenario.

`wms-brain-client` resuelve los tres problemas a la vez:

1. **Conecta a tu SQL local o productivo** y ejecuta queries dirigidas
   por el brain (read-only por default, escritura con confirmacion +
   snapshot + rollback).
2. **Versiona cada aprendizaje** como un archivo en el repo, atado a
   un developer (slug `ejc`, `juanp`, `mariag`) y a un cliente
   (codename `K7`, `BB`, `C9`, etc — los nombres reales nunca tocan
   el repo).
3. **Reproduce escenarios** via seed scripts, asi el bug del coyote
   se replica en local con un solo cmdlet.

## Diagrama del flow (el coyote y el correcaminos)

```
                       ┌─────────────────────────┐
        publica Q-XXX  │                         │  pull preguntas
        ┌─────────────►│   wms-brain (repo)      │◄─────────┐
        │              │                         │          │
        │              │   pending/   answered/  │          │
        │              │   closed/    suites/    │          │
        │              │   scenarios/ snapshots/ │          │
        │              └─────────────────────────┘          │
        │                                                   │
        │                                            push answer
        │                                                   │
  ┌─────┴──────┐                                    ┌──────┴───────┐
  │            │                                    │              │
  │   brain    │                                    │  wms-brain   │
  │  (humano   │                                    │    client    │
  │  o agente) │                                    │  (Windows)   │
  │            │                                    │              │
  └────────────┘                                    └──────┬───────┘
                                                           │
                                              Invoke-Sqlcmd│
                                                           ▼
                                                ┌──────────────────┐
                                                │   SQL Server     │
                                                │  local o cliente │
                                                │   (read mostly)  │
                                                └──────────────────┘
```

## Documentacion

| Archivo | Para que | Hojas equivalentes |
|---|---|---:|
| [SPEC.md](./SPEC.md) | Arquitectura, principios, bootstrap, identidad, roadmap | 464 |
| [PROTOCOL.md](./PROTOCOL.md) | Formato exacto de question/answer/learning/closed cards | 353 |
| [CMDLETS.md](./CMDLETS.md) | Catalogo de los 22 cmdlets PowerShell con sintaxis y ejemplos | 311 |
| [ALIASES.md](./ALIASES.md) | Codenames de clientes (politica de seguridad anti-leak) | 72 |
| [PROMPT-OPENCLAW.md](./PROMPT-OPENCLAW.md) | Prompt listo para Claude Desktop/Code para implementar el modulo | 233 |
| [examples/Q-001-cadencia-navsync.md](./examples/Q-001-cadencia-navsync.md) | Ejemplo de question card real (cadencia NavSync para BB) | 140 |

Total: **~1,573 lineas** de spec. Si pensas "es mucho", esperate a
ver el WMS.

## Catalogo express de cmdlets

| Cmdlet | Que hace | Te puede romper algo? |
|---|---|:---:|
| `Initialize-WmsBrain` | Te instala todo lo que falte (PS7, .NET, gh, modulos) | No (instala) |
| `Set-WmsBrainConfig` | Configura operador, perfiles SQL, alias, token | No |
| `Test-WmsBrainConnection` | Verifica que SQL y GitHub respondan | No |
| `Sync-WmsBrain` | git pull + push del repo brain | No (ramas locales protegidas) |
| `Get-WmsBrainQuestion` | Lista preguntas pendientes del brain | No |
| `Show-WmsBrainQuestion` | Muestra una pregunta en detalle | No |
| `Invoke-WmsBrainQuestion` | Corre las queries SQL de la pregunta | No (read-only) |
| `Submit-WmsBrainAnswer` | Confirma y commitea la respuesta al brain | Pide -Confirm |
| `Get-WmsBrainScenario` | Lista escenarios de seed disponibles | No |
| `Send-WmsBrainSeed` | Inyecta seed data para reproducir un caso | Si (con safety) |
| `Test-WmsBrainScenario` | Valida que el seed se aplico bien | No |
| `Invoke-WmsBrainAnalysis` | Corre suite de queries de diagnostico | No |
| `Compare-WmsBrainSchema` | Diff de schema vs baseline en repo | No |
| `Save-WmsBrainSchemaSnapshot` | Guarda snapshot SOLO si hay diff | Solo si vos queres |
| `Submit-WmsBrainLearning` | Subis un hallazgo libre al brain | Pide -Confirm |
| `Show-WmsBrainHistory` | Tu historial de operaciones locales | No |
| `Undo-WmsBrainOperation -Id <opid>` | Rollback de una operacion previa | Restaura snapshot |
| `Start-WmsBrainInteractive` (alias `wmsbc`) | Lanza el menu REPL interactivo | No |

Para la lista completa con sintaxis, ver [CMDLETS.md](./CMDLETS.md).

## Reglas de la casa (alias "como no autoflagelarse")

```
   ___________________________________________________
  |                                                   |
  |   1. Read-only por default. Escritura grita.     |
  |   2. Toda escritura → snapshot → log → confirma. |
  |   3. Nombres reales de clientes NUNCA al repo.   |
  |   4. PAT GitHub vive en Credential Manager.      |
  |   5. Si dudas, -WhatIf. Si seguis dudando, no.   |
  |___________________________________________________|
            \\
             \\
              \\        _______
               \\      / ACME  \
                \\    | DYNAMITE|
                 \\    \_______/
                  \\      |||
                   \\     |||
                          (no es para vos, es para el outbox)
```

## Como arrancar (TL;DR para el desesperado)

```powershell
# 1. Instalar el modulo (una sola vez)
Install-Module -Name WmsBrainClient -Scope CurrentUser    # cuando este publicado
# o
git clone https://github.com/ejcalderongt/WmsBrainClient.git
Import-Module .\WmsBrainClient\WmsBrainClient.psd1

# 2. Bootstrap (una sola vez por maquina)
Initialize-WmsBrain
# Te va a pedir: slug, email, PAT GitHub, perfil SQL, codename del cliente

# 3. Modo interactivo (uso diario)
wmsbc
# Menu: [1] Sync  [2] Pendientes  [3] Suites  [4] Escenarios  [5] Schema  ...

# 4. O modo cmdlet (para los que prefieren teclear)
Sync-WmsBrain
Get-WmsBrainQuestion -Status Pending
Invoke-WmsBrainQuestion -Id Q-001 -Profile K7-PRD
Submit-WmsBrainAnswer -Id Q-001 -Verdict confirmed -Confidence high
```

## Roadmap

- **v1.0 — El Aprendiz**: bootstrap, sync, question/answer, modo
  interactivo. Lo basico para que el equipo deje de copiar SQL al chat.
- **v1.1 — El Cirujano**: scenarios + Send-Seed, suites de analysis,
  rollback. Para reproducir bugs en local sin sufrir.
- **v1.2 — El Archivero**: schema snapshots con drift detection, modo
  libre de learnings. Para que la memoria del equipo sea eterna.
- **v2.0 — El Embajador**: instalable en server cliente para diagnostico
  remoto programado. Para que un cliente diga "esta lento" y la
  herramienta sepa por que antes de que abramos el ticket.
- **v3.0 — El Oraculo**: integracion con LLM local que lea suites y
  proponga nuevas questions automaticamente. (Si llegamos aca, brindamos.)

## Quien lo hizo

Erik Calderon (`ejc`) y un agente que hizo demasiadas preguntas durante
un ciclo de 9 horas que termino con 12 de 25 preguntas respondidas, 1
bug confirmado en datos reales, 3 patrones de integracion ERP mapeados, y
una bandera roja sobre 110,795 INGRESOS olvidados en BB. La herramienta
nacio para que el ciclo 10 sea mas corta y la 20 no haga falta.

## Licencia

Interna PrograX24. No redistribuir sin acuerdo del equipo.

---

```
                  *
                 ***
                *****
               *******
              *********
             ***********
            *************
           ***************
          *****************
         *******************
                   |
                   |
                   |
            ───────┴───────
              brain awake
```

> _El WMS es el oceano y los pedidos son las olas. La BD es la marea._
> _El developer es el cartografo, el cliente es el navegante,_
> _y el cliente local es el sextante. **Que ninguno navegue ciego**._
