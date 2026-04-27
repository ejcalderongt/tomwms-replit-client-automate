# Casos de reserva observados en reserva-WMS legacy (Killios PRD)

> Snapshot 2026-04-27T15:28:58.419Z. Aprendido de `SELECT Caso_Reserva, COUNT(*) FROM trans_pe_det_log_reserva GROUP BY Caso_Reserva` en TOMWMS_KILLIOS_PRD.

Sufijo `_EJC202310090957` = build del autor del 2023-10-09 09:57.
Sufijo `_LLR_CASO_#X_` = "Llamado Luego de Reserva" (caso secundario derivado del principal).

## Killios PRD

| Caso_Reserva | Conteo | Última | MensajeLog (sample) |
|---|---:|---|---|
| `CASO_#24_EJC202310090957` | 18587 | 2025-08-19 | CASO_#24_EJC202310090957 Fecha Mínima: 12/11/2027 DiasVencimiento: 0 FechaMinimaVenceZonaP |
| `CASO_#8_EJC202310090957` | 1675 | 2025-08-19 | CASO_#8_EJC202310090957 Fecha Mínima: 5/02/2028 DiasVencimiento: FechaMinimaVenceZonaPick |
| `CASO_#21_EJC202310090957` | 1065 | 2025-08-19 | CASO_#21_EJC202310090957 Fecha Mínima: 1/07/2026 DiasVencimiento: FechaMinimaVenceZonaPic |
| `CASO_#12_EJC202310090957` | 276 | 2025-08-19 | CASO_#12_EJC202310090957 Fecha Mínima: 26/07/2027 DiasVencimiento: FechaMinimaVenceZonaPi |
| `CASO_#20_EJC202310090957` | 267 | 2025-08-19 | CASO_#20_EJC202310090957 Fecha Mínima: 18/03/2028 DiasVencimiento: FechaMinimaVenceZonaPi |
| `CASO_#24_EJC202310090957_LLR_CASO_#29_` | 251 | 2025-08-18 | CASO_#24_EJC202310090957_LLR_CASO_#29_ Fecha Mínima: 1/05/2028 DiasVencimiento: 0 FechaMin |
| `CASO_#23_EJC202310090957` | 125 | 2025-08-19 | CASO_#23_EJC202310090957 Fecha Mínima: 1/03/2029 DiasVencimiento: FechaMinimaVenceZonaPic |
| `CASO_#22_EJC202310090957` | 44 | 2025-06-25 | CASO_#22_EJC202310090957 Fecha Mínima: 28/08/2027 DiasVencimiento: FechaMinimaVenceZonaPi |
| `CASO_#8_EJC202310090957_LLR_CASO_#29_` | 24 | 2025-06-10 | CASO_#8_EJC202310090957_LLR_CASO_#29_ Fecha Mínima: 18/07/2027 DiasVencimiento: 0 FechaMin |
| `#SR240315` | 17 | 2025-06-16 | #MI3_2312201922: No se pudo explosionar en zonas de almacenamiento (Rack), la bandera rech |
| `CASO_#23_EJC202310090957_LLR_CASO_#28_` | 8 | 2025-08-18 | CASO_#23_EJC202310090957_LLR_CASO_#28_ Fecha Mínima: 5/08/2026 DiasVencimiento: 0 FechaMin |
| `CASO_#20_EJC202310090957_LLR_CASO_#28_` | 6 | 2025-05-24 | CASO_#20_EJC202310090957_LLR_CASO_#28_ Fecha Mínima: 31/12/2025 DiasVencimiento: 0 FechaMi |
| `CASO_#20_EJC202310090957_LLR_CASO_#29_` | 4 | 2025-08-14 | CASO_#20_EJC202310090957_LLR_CASO_#29_ Fecha Mínima: 20/01/2028 DiasVencimiento: 0 FechaMi |
| `CASO_#24_EJC202310090957_LLR_CASO_#28_` | 3 | 2025-06-10 | CASO_#24_EJC202310090957_LLR_CASO_#28_ Fecha Mínima: 31/12/2028 DiasVencimiento: 0 FechaMi |
| `CASO_#23_EJC202310090957_LLR_CASO_#29_` | 1 | 2025-08-11 | CASO_#23_EJC202310090957_LLR_CASO_#29_ Fecha Mínima: 1/07/2026 DiasVencimiento: 0 FechaMin |
| `CASO_#9_EJC202310090957` | 1 | 2025-06-10 | CASO_#9_EJC202310090957 Fecha Mínima: 31/12/2028 DiasVencimiento: FechaMinimaVenceZonaPic |
| `CASO_#23_EJC202310090957_LLR_CASO_#31_` | 1 | 2025-05-24 | CASO_#23_EJC202310090957_LLR_CASO_#31_ Fecha Mínima: 31/12/2025 DiasVencimiento: 0 FechaMi |

## BYB PRD

| Caso_Reserva | Conteo | Última |
|---|---:|---|
| `CASO_#8_EJC202310090957` | 2 | 2025-10-22 |

> NOTA: la mayoria de registros BYB historicos tienen `Caso_Reserva = NULL` (706 registros del 2023-12-15). Probablemente el campo se agrego despues. La actividad reciente es minima.

## CEALSA QAS

**0 registros** en `trans_pe_det_log_reserva`.

Confirma el modelo 3PL: el motor de reserva del WMS legacy **no se ejecuta en CEALSA**. Las salidas se hacen discrecionalmente segun polizas y peticion del cliente.

## Diff con escenarios RES-001..024 del bridge

| RES | Caso observado en log | ¿Existe en prod? |
|---|---|---|
| RES-001..007 | (ninguno con esos numeros) | NO |
| RES-008 | CASO_#8 | **SI** (1,675 registros) |
| RES-009 | CASO_#9 | SI (1 registro - rarisimo) |
| RES-010 | (no observado) | NO |
| RES-011 | (no observado) | NO |
| RES-012 | CASO_#12 | **SI** (276) |
| RES-013..019 | (no observados) | NO |
| RES-020 | CASO_#20 | **SI** (267) |
| RES-021 | CASO_#21 | **SI** (1,065) |
| RES-022 | CASO_#22 | SI (44) |
| RES-023 | CASO_#23 | **SI** (125) |
| RES-024 | CASO_#24 | **SI** (18,587 - dominante) |
| RES-DIN | (corre dinamico) | OK |

**Conclusion**: solo 9 de los 21 escenarios del bridge tienen evidencia productiva. Los otros 12 son **teoricos** (legacy doc) o **target del nuevo motor (reserva-webapi)** sin contraparte en prod actual.

## Casos secundarios (LLR)

Aparecen `_LLR_CASO_#28_`, `_LLR_CASO_#29_`, `_LLR_CASO_#31_` como sufijos de #20, #23, #24. Probable interpretacion:
- LLR = "Llamado Luego de Reserva"
- El caso principal se ejecuto y luego se llamo al caso secundario para refinar/rectificar.
- Casos #28, #29, #31 NO existen como casos primarios en log → solo aparecen como derivados.

**Pendiente**: desambiguar el significado exacto de `_LLR_CASO_#X_` revisando el codigo VB del legacy.
