# L-023 — BYB outbox parado entre 2024-2025 (caso de estudio)

> Etiqueta: `L-023_DIAG_BYB-CORTE-OPERATIVO-2024_APPLIED`
> Fecha: 29-abr-2026
> Origen: analisis temporal outbox BYB durante fingerprint

## Hallazgo

BYB (`IMS4MB_BYB_PRD`) muestra un patron temporal devastador en
`i_nav_transacciones_out`:

| Periodo | Volumen mensual | Estado |
|---|---|---|
| 2022 | 5K-10K/mes | crecimiento |
| 2023 (todo el año) | 32K-44K/mes | operacion intensa |
| 2024-01 a 2024-05 | **0 filas** (gap) | corte |
| 2024-06 | 1 fila | residual |
| 2024-07 a 2025-08 | **0 filas** | sin operacion |
| 2025-09 | 6 filas | residual |
| 2025-10 | 13 filas | ultimo registro |

→ Despues de oct-2025, **silencio total**.

## Diagnostico

El sync NAV (`NavSync.exe`) se rompio entre dic-2023 y jun-2024. Posibles
causas:

1. **NavSync.exe rotos**: el cliente actualizo NAV o se rompio el
   ClickOnce y nunca se recompuso.
2. **Cliente cambio de WMS**: BYB migro a otro sistema y dejo TOMWMS.
3. **Suspension contractual**: cliente pauso el servicio.
4. **Operacion residual**: el negocio sigue pero a volumen muy bajo
   (los 19 registros entre jun-2024 y oct-2025 sugieren actividad
   esporadica de prueba o reactivacion fallida).

## Por que importa para el brain

1. **Caso de estudio para detectar problemas similares en otros clientes**:
   miras el patron temporal del outbox y si hay un quiebre marcado, hay
   problema.
2. **Refuerza L-010 (NAV no procesa ingresos)**: el modelo de
   integracion NAV no se mantuvo. Sugiere que la decision arquitectonica
   de SAP B1 sobre NAV es la correcta para clientes nuevos.
3. **Refuerza L-014 (EC2 copia parcial)**: EC2 muestra el ultimo
   estado conocido, pero la productiva real puede estar mas o menos
   actualizada que esto. CONFIRMAR con Erik si BYB sigue activo.

## Pendientes

- **Q-BYB-CORTE-2024 (en _inbox)**: confirmar con Erik el estado real
  de la cuenta BYB. Si ya no son cliente activo, archivar el
  fingerprint con tag `INACTIVE`. Si siguen activos, urgente investigar
  por que el outbox EC2 no refleja la operacion.
- Si BYB sigue activo: ver si la productiva real (laptop Erik) tiene
  outbox al dia. Si si: el problema es solo la copia EC2. Si no: el
  problema es real y BYB esta operando sin sincronizar al ERP.
