---
protocolVersion: 2
id: Q-015
askedBy: Carol (CKFK / KKKL)
askedAt: 2026-04-28T20:00:00-03:00
relayedBy: Erik Calderon (PrograX24)
dimension: afinidad-de-procesos
status: pending-clarification
priority: alta
modules: [cambio_ubicacion, BOF, HH, regla_ubic, indice_rotacion]
---

# Q-015 - Regla restrictiva de cambio de ubicacion (BOF + HH)

> **Origen**: Carol pone a prueba al brain con una consulta sobre la regla operativa de cambio de ubicacion. Trasladada por Erik en sesion del 28-abr-2026.

## Texto literal de la consulta

> "Al realizar el cambio de ubicacion de un producto en BOF y en HH que no se pueda mover un producto a una posicion donde no le corresponde, siempre se tiene que cumplir la regla de ubicacion, el mensaje debe ser 100% restrictivo.
>
> La unica condicion valida es que si puedo colocar un producto de mayor indice en una ubicacion de menor indice de rotacion.
>
> La otra condicion es que el producto de ambas posiciones sea el mismo A y B iguales C y D iguales"

## Reformulacion en lenguaje formal (interpretacion del brain)

| Regla | Descripcion |
|-------|-------------|
| **R0 (regla principal)** | Bloquear cualquier cambio de ubicacion donde el producto no coincida con la regla de ubicacion definida para la ubicacion destino. Mensaje 100% restrictivo, sin override. Aplica en BOF y en HH. |
| **E1 (excepcion - downgrade de rotacion)** | Permitir cuando el producto tiene indice de rotacion *mas alto* que el indice de la ubicacion destino (es decir, producto rapido se puede colocar en ubicacion lenta). |
| **E2 (excepcion - mismo producto)** | Permitir cuando "el producto de ambas posiciones sea el mismo A y B iguales C y D iguales" (interpretacion pendiente de aclaracion de Carol). |

## Ambiguedades pendientes de aclarar con Carol

1. **E1 - sentido del "mayor/menor indice"**: en `dbo.indice_rotacion.IndicePrioridad` (5 valores), ¿el numero mayor representa rotacion mas ALTA o mas BAJA? La interpretacion operativa habitual es "indice 1 = ALTA, indice 5 = BAJA" pero hay que confirmar antes de codificar la condicion.

2. **E2 - significado de "A y B iguales C y D iguales"**: tres lecturas posibles:
   - (a) Coordenadas A,B,C,D de las dos ubicaciones coinciden -> imposible (seria la misma ubicacion).
   - (b) Atributos del producto (lote=A, vencimiento=B, presentacion=C, calidad=D) coinciden ademas de ser el mismo SKU.
   - (c) Coordenadas A,B coinciden en una posicion (mismo macro-bloque) Y C,D coinciden en otra (mismo nivel).

3. **Donde se enforce hoy**: la regla esta solo en UI o tambien en SP/trigger? El modelo `regla_ubic_*` existe pero hay que confirmar si el SP de cambio de ubicacion realmente lo consulta antes de mover stock.

## Respuesta esperada

- Aclaracion de E1 y E2.
- Confirmacion de si se valida server-side hoy o solo en UI.
- Permiso para emitir ADR-013 con la regla formalizada.
