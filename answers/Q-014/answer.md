---
protocolVersion: 2
answerForId: Q-014
answeredBy: agent-replit
answeredAt: 2026-04-28T22:30:00-03:00
executedVia: live-sql-pymssql-2.3.13
executedBy: sa@52.41.114.122,1437
status: answered
dimension: afinidad-de-procesos
---

# Q-014 - TOP15 tareas HH ejecutadas - 3 BDs

> Respondida por agente brain (sesion replit) el 28 abril 2026 via ejecucion live SQL Server contra EC2 52.41.114.122,1437.
>
> **Dimension**: esta respuesta es de **afinidad de procesos**. Los numeros observados en nuestro snapshot NO se comparan con los reportados por las personas del cliente (que trabajan con backups recientes propios). La comparacion cuantitativa queda diferida a un segmento futuro de afinidad-de-datos.
>
> **BDs reales descubiertas**: K7-PRD = `TOMWMS_KILLIOS_PRD`, BB-PRD = `IMS4MB_BYB_PRD`, C9-QAS = `IMS4MB_CEALSA_QAS`. Las 3 comparten schema (TOMWMS y IMS4MB son aparentemente forks del mismo modelo).

## Hallazgo de PROCESO

Las 3 BDs comparten el catalogo `sis_tipo_tarea` (mismos IdTipoTarea: PIK, RECE, UBIC, INVE, CEST, etc.) y la tabla `tarea_hh` registra ejecuciones del mismo modo en las 3. El catalogo tiene 30+ tipos definidos.

**Hallazgo de proceso**: cada deployment **usa un perfil operativo distinto** sobre el mismo catalogo. Un deployment puede tener 71% PIK (outbound-heavy), otro puede tener 50% UBIC (putaway-heavy), etc. Es coherente con que cada cliente tiene un proceso operativo propio dentro del mismo modelo.

## Datos observados en nuestro snapshot (no comparables)

**K7-PRD (Killios)**:
| Tipo | Ejecutadas | pct |
|------|-----------:|----:|
| PIK | 1.292 | 71,11% |
| RECE | 503 | 27,68% |
| UBIC | 17 | 0,94% |
| INVE | 3 | 0,17% |
| CEST | 2 | 0,11% |

**BB-PRD (Becofarma)**:
| Tipo | Ejecutadas | pct |
|------|-----------:|----:|
| UBIC | 25.784 | 50,31% |
| RECE | 15.801 | 30,83% |
| PIK | 9.622 | 18,77% |
| INVE | 44 | 0,09% |
| CEST | 1 | 0,00% |

**C9-QAS (CEALSA QAS)**:
| Tipo | Ejecutadas | pct |
|------|-----------:|----:|
| PIK | 13.836 | 73,59% |
| RECE | 4.964 | 26,40% |
| INVE | 1 | 0,01% |

(Tipos como TRASL/REUB/CEST observados en <1% en las 3 - no se comparan con frecuencias reportadas, solo se observa que el catalogo los soporta pero el uso real es marginal.)

## Datos crudos

- `q1-top15-K7-PRD.csv`
- `q1-top15-BB-PRD.csv`
- `q1-top15-C9-QAS.csv`

## Decision recomendada

(a) la nueva WebAPI debe parametrizar peso de tareas por cliente para KPIs (no asumir un tipo dominante uniforme), (b) ofrecer endpoint "tipos activos" filtrado para que la HH no muestre opciones que el deployment no usa, (c) los dashboards deben pesar tareas distinto por cliente.

## Eventos generados en _inbox

- H07 (`20260428-1906-H07-bb-putaway-intensivo-50pct-ubic.json`)
