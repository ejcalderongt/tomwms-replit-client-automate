# Matriz de compatibilidad cliente × escenario

> **Auto-generada** por `compute-matrix.cjs`. NO editar a mano.
> Última generación manual: 2026-04-27. Volver a correr el script tras cualquier cambio en escenarios o clientes.

## Leyenda

- `OK` — el cliente cumple los `requires_config` del escenario, se debe correr.
- `N/A` — el cliente NO cumple los requisitos, no aplica (no es un fallo).
- `unknown` — falta aprender la config del cliente (`learned: false`) o falta declarar el flag.
- `INVALID` — el escenario tiene `requires_config` contradictorio.

## Estado actual

Hoy NINGÚN cliente está aprendido (`learned: false` en los 4 YAML). Por lo tanto la matriz inicial es enteramente `unknown`. Llenar en orden:

1. Correr `wmsa learn-config killios` (BD ya disponible read-only).
2. Pedir snapshot de configuración a IDEALSA y BYB cuando estén accesibles.
3. Confirmar con LA CUMBRE el caso de `Rechazar_Pedido_Si_Esta_Incompleto`.

## Tabla (placeholder)

| Escenario | Origen legacy | IDEALSA | KILLIOS | LA CUMBRE | BYB |
|---|---|---|---|---|---|
| `RES-001` (FEFO simple) | CASO 1 | unknown | unknown | unknown | unknown |
| `RES-002` (paquete completo zona picking) | — | unknown | unknown | unknown | unknown |
| `RES-003` (explosión automática) | — | unknown | unknown | unknown | unknown |
| `RES-004` (stock insuficiente parcial) | — | unknown | unknown | unknown | unknown |
| `RES-005` (cancelar y liberar) | — | unknown | unknown | unknown | unknown |
| `RES-006` (FEFO ALM corto + completar picking) | CASO 2 | unknown | unknown | unknown | unknown |
| `RES-007` (FEFO ALM corto + completar picking var) | CASO 3 | unknown | unknown | unknown | unknown |
| `RES-008` (zigzag ALM-PICK-ALM) | CASO 4 | unknown | unknown | unknown | unknown |
| `RES-009` (zigzag PICK-ALM-PICK) | CASO 5 | unknown | unknown | unknown | unknown |
| `RES-010` (zigzag variante 6) | CASO 6 | unknown | unknown | unknown | unknown |
| `RES-011` (sin picking) | CASO 7 | unknown | unknown | unknown | unknown |
| `RES-012` (rechazo por incompleto) | CASO 8 | unknown | unknown | **N/A esperado** | unknown |
| `RES-013` (explosión desde caja en picking) | CASO 9 | unknown | unknown | unknown | unknown |
| `RES-014` (explosión + faltante → rechazo) | CASO 10 | unknown | unknown | **N/A esperado** | unknown |
| `RES-015` (múltiples ALM más cortas) | CASO 11 | unknown | unknown | unknown | unknown |
| `RES-016` (múltiples ALM variante 12) | CASO 12 | unknown | unknown | unknown | unknown |
| `RES-017` (sin picking explosionable → rechazo) | CASO 13 | unknown | unknown | **N/A esperado** | unknown |
| `RES-018` (picking corto sin re-explosionar) | CASO 14 | unknown | unknown | unknown | unknown |
| `RES-019` (mezcla UDS+CJS FEFO estricto) | CASO 15 | unknown | unknown | unknown | unknown |
| `RES-020` (mezcla UDS+CJS + explosión final) | CASO 16 | unknown | unknown | unknown | unknown |
| `RES-021` (empate de fecha → priorizar UDS) | CASO 17 | unknown | unknown | unknown | unknown |
| `RES-DIN` (dinámico parametrizado) | CASO Dinamico | unknown | unknown | unknown | unknown |
| `RES-022` (BYB variante 18) | CASO 18 | **N/A esperado** | **N/A esperado** | **N/A esperado** | unknown |
| `RES-023` (BYB variante 19) | CASO 19 | **N/A esperado** | **N/A esperado** | **N/A esperado** | unknown |
| `RES-024` (BYB variante 20, fecha 1900) | CASO 20 | **N/A esperado** | **N/A esperado** | **N/A esperado** | unknown |
| `PIC-001` (pedido reservado picking) | — | unknown | unknown | unknown | unknown |
| `DES-001` (verificación obligatoria) | — | unknown | unknown | unknown | unknown |
| `INV-001` (ajuste positivo, modifica venc.) | — | unknown | unknown | unknown | unknown |

> Las celdas marcadas **"N/A esperado"** son la HIPÓTESIS antes de aprender. Por ejemplo `RES-022..024` son específicos del cliente BYB (lote `CASO_USOnn_MI3_AUTOMATE` con fechas BYB), por eso se espera que para los demás clientes dé `N/A`. Pero hasta no aprender la config real **ninguna celda es definitiva**.

## Cómo regenerar

```bash
node brain/test-scenarios/_matrix/compute-matrix.cjs > brain/test-scenarios/_matrix/compatibility.md
```

El script lee:
- `brain/test-scenarios/**/RES-*.yaml`, `PIC-*.yaml`, `DES-*.yaml`, `INV-*.yaml`
- `brain/test-scenarios/_matrix/clients/*.yaml`

Y emite esta tabla.
