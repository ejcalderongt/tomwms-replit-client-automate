# L-048 — PROC: clientes no-3PL usan UN solo propietario default en la tabla `propietarios`

> Etiqueta: `L-048_PROC_NO-3PL-UN-PROPIETARIO_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-PROPIETARIO-AGNOSTICO

## Hallazgo

En clientes que NO operan como 3PL (BECO, BYB, K7), la **tabla `propietarios` se usa igual** que en clientes 3PL — con la diferencia que **solo tienen un registro**, que actua como propietario default.

**No se ignora la tabla, no hay un patron alternativo.** El modelo es uniforme: el WMS siempre piensa en clave `propietario` aunque solo haya uno.

## Cita literal Carolina (Wave 11)

> "Es correcto solo utilizan un propietario en la Tabla."

## Implicaciones

1. **El WMS es 3PL-native by design**. No hay rama de codigo "modo single-tenant". Todo query, todo reporte, todo modulo asume `propietario_id` como dimension.
2. **Para clientes no-3PL, ese unico propietario es invisible al usuario** (probable que no se muestre en UI, que se inyecte por default en transacciones).
3. **Esto facilita migrar un cliente non-3PL a 3PL en el futuro**: solo agregar registros nuevos en `propietarios` y reasignar transacciones por `propietario_id`. No hay refactor estructural.
4. **Confirma L-035**: NO codifican `if Cliente.Es3PL Then ... Else ... End If`. La diferencia 3PL vs no-3PL se resuelve en la cardinalidad de `propietarios`, no en codigo.
5. **Para el deep-flow**: en cualquier tabla con FK a propietarios (probable: `stock`, `transacciones_*`, `picking_*`, `ingreso_*`, `egreso_*`), no asumir que `propietario_id IS NOT NULL` significa 3PL.

## Patron correlacionado

Esto es ejemplo de **diseño multitenancy degradado a single-tenant via cardinalidad** en lugar de via flag. Patron limpio, escalable, pero requiere disciplina en queries (siempre filtrar por propietario, incluso cuando "solo hay uno", para no perder el habito).

## Q-* abiertas

- Q-PROPIETARIO-DEFAULT-CONVENCION: ¿hay nombre/codigo convencional para el propietario unico (ej. "DEFAULT", "EMPRESA", el nombre del cliente)? ¿O cada cliente lo nombra distinto?
- Q-PROPIETARIO-UI-OCULTO: ¿hay flag tipo `Empresa.mostrar_propietario_en_ui` o similar para ocultar el dropdown a clientes no-3PL?
- Q-PROPIETARIO-MIGRACION-NO3PL-A-3PL: ¿hay precedente de un cliente que paso de no-3PL a 3PL? ¿Como se hizo?
- Q-PROPIETARIO-PERMISOS: ¿el modelo de permisos tiene granularidad por propietario incluso en single-tenant, o se ignora?

## Vinculos

- L-035: ejemplo concreto de "no codificar por cliente, parametrizar".
- L-031: master congelada — el modelo `propietarios` es parte de la master uniforme.
- L-027: orden migracion 2028 — clientes 3PL (CEALSA, MAMPA-?, MERCOSAL?) y no-3PL (BYB, BECO, K7) siguen el mismo schema.

## Pendiente identificar

¿Cuales clientes son 3PL y cuales no? Reconstruir matriz:

| Cliente | 3PL | Notas |
|---|---|---|
| CEALSA | SI | propietarios multiples con prefactura (Q34) |
| MAMPA | ? | retail multi-categoria (L-044), probable no-3PL |
| BYB | NO | confirmado Carolina Wave 11 |
| BECO | NO | confirmado Carolina Wave 11 |
| K7 | NO | confirmado Carolina Wave 11 |
| IDEALSA | ? | holding con MERCOPAN+MERCOSAL — probable no-3PL pero multi-empresa |
| INELAC | ? | holding IDEALSA |
| MERCOPAN | ? | holding IDEALSA |
| MERCOSAL | ? | holding IDEALSA (L-034) |
| Cumbre | ? | perecederos, sector carniceria (L-044) |
| Becofarma | ? | farmacia |
| Clarispharma | ? | farmacia (L-033) |
| MHS | SI/NO ? | primer cliente WMSWebAPI (L-038) |

→ Q nueva: completar matriz con Carolina o Erik.
