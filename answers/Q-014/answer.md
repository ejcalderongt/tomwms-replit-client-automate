---
protocolVersion: 2
answerForId: Q-014
answeredBy: agent-replit
answeredAt: 2026-04-28T22:30:00-03:00
executedVia: live-sql-pymssql-2.3.13
executedBy: sa@52.41.114.122,1437
status: answered
---

# Q-014 - TOP15 tareas HH ejecutadas - 3 BDs

> Respondida por agente brain (sesion replit) el 28 abril 2026 via ejecucion live SQL Server (sa) contra EC2 52.41.114.122,1437.
>
> **BDs reales descubiertas**: K7-PRD = `TOMWMS_KILLIOS_PRD`, BB-PRD = `IMS4MB_BYB_PRD`, C9-QAS = `IMS4MB_CEALSA_QAS`. Las 3 comparten schema (TOMWMS y IMS4MB son aparentemente forks del mismo modelo).

## Resultado

**CERRADA con hallazgo bomba sobre BB**. Ejecutada contra las 3 BDs.

### K7-PRD (Killios)
| Tipo | Ejecutadas | pct |
|------|-----------:|----:|
| **PIK** | 1.292 | **71,11%** |
| RECE | 503 | 27,68% |
| UBIC | 17 | 0,94% |
| INVE | 3 | 0,17% |
| CEST | 2 | 0,11% |
| Total tipos usados | **5 de 35** | |

### BB-PRD (Becofarma) - **PERFIL RADICALMENTE DISTINTO**
| Tipo | Ejecutadas | pct |
|------|-----------:|----:|
| **UBIC** | 25.784 | **50,31%** |
| RECE | 15.801 | 30,83% |
| PIK | 9.622 | 18,77% |
| INVE | 44 | 0,09% |
| CEST | 1 | 0,00% |
| Total tipos usados | **5 de 35** | |

### C9-QAS (CEALSA QAS)
| Tipo | Ejecutadas | pct |
|------|-----------:|----:|
| **PIK** | 13.836 | **73,59%** |
| RECE | 4.964 | 26,40% |
| INVE | 1 | 0,01% |
| Total tipos usados | **3 de 33** | |

## Datos crudos

- `q1-top15-K7-PRD.csv`
- `q1-top15-BB-PRD.csv`
- `q1-top15-C9-QAS.csv`

## Hallazgos derivados

1. **BB es putaway-intensivo**: 50% UBIC vs 71% PIK en K7. Razonable para farmaceutica con miles de SKUs y rotacion alta.
2. **Carol tenia razon sobre TRASL/REUB/CEST**: sumadas son <1% en las 3 BDs. NO hace falta optimizar esos modulos.
3. **35 tipos definidos pero solo 3-5 usados**: hay tech-debt de tipos huerfanos. La nueva WebAPI puede ofrecer un endpoint "tipos activos" filtrado para evitar mostrar opciones muertas en HH.
4. **Implicaciones para KPIs**: la productividad NO puede medirse uniformemente. UBIC en BB y PIK en K7 son los drivers principales - los dashboards deben pesarlos distinto por cliente.
5. **C9-QAS solo usa 3 tipos**: confirma que es ambiente de smoke-test (PIK + RECE + 1 INVE de prueba).

## Eventos generados en _inbox

- H07 (BB putaway-intensivo)
