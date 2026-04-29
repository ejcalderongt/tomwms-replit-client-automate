# L-036 — ARCH: stock_jornada se dispara al primer login del dia + horario_ejecucion_historico

> Etiqueta: `L-036_ARCH_STOCK-JORNADA-TRIGGER_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuestas Carolina a Q-STOCK-JORNADA-MAMPA y Q-STOCK-JORNADA-PROCESO

## Hallazgo

El cierre diario de `stock_jornada` **no es un servicio Windows ni un job de SQL Agent**. Es disparado por el **primer login del dia al WMS**, controlado por dos parametros:

1. **`Empresa.generar_stock_jornada`** (bandera bool):
   - `true` → ejecuta el proceso de cierre diariamente.
   - `false` → no se ejecuta. La tabla puede existir vacia (caso BECO/K7/BYB).

2. **`Bodega.horario_ejecucion_historico`** (hora):
   - Si al primer login el cierre del dia anterior no se hizo, se programa para esa hora.

Esto explica la observacion previa (L-026 area + DISCOVERY_TREE wave 9) de que MAMPA tenia 21.883 filas en `stock_jornada` mientras BECO/K7/BYB tenian la tabla vacia: **MAMPA tiene `generar_stock_jornada = true`, los otros lo tienen en `false` o no lo activaron** (no es propiedad regulatoria del cliente, es bandera).

## Citas literales Carolina (Wave 11)

> Q-STOCK-JORNADA-MAMPA:
> "El stock jornada se habilita con la bandera que esta en Empresa y se llama `generar_stock_jornada`, si este parametro esta en true se ejecuta este proceso diariamente, en caso contrario no."

> Q-STOCK-JORNADA-PROCESO:
> "Este proceso se ejecuta al abrir el WMS por primera vez en el dia y en caso de que no este cerrado se configura la hora en la Bodega en el campo `horario_ejecucion_historico` y cuando sea esa hora se ejecutara el proceso."

## Implicaciones tecnicas

1. **No hay scheduler externo.** El "cron" del WMS es el operador. Si nadie abre el WMS un dia, la jornada no se cierra.
2. **Single-bodega-friendly, multi-bodega complica.** El campo es por bodega → cada bodega tiene su propia hora de cierre. ¿Que pasa si dos bodegas comparten Empresa pero tienen horarios distintos? Probable bug latente. (Ver Q nueva.)
3. **El `stock_jornada_desfase` (Q28) se calcula automaticamente** por proceso interno del WMS — Carolina no especifico cual, pero esta dentro del cierre de jornada.
4. **Para activar stock_jornada en cliente nuevo**: setear `Empresa.generar_stock_jornada = true` y `Bodega.horario_ejecucion_historico` a una hora valida. NO tocar codigo.

## Patron mas amplio que confirma

Esto es ejemplo concreto de L-035: **NO se codifica por cliente**. Que CEALSA cierre stock_jornada y BECO no, no es codigo, es la bandera `Empresa.generar_stock_jornada`.

## Q-* abiertas a abrir (bloque 14 cuestionario)

- Q-STOCK-JORNADA-MULTIBODEGA: si una empresa tiene varias bodegas con `horario_ejecucion_historico` distintos, ¿el proceso corre N veces o una sola? ¿Es transaccional?
- Q-STOCK-JORNADA-FALLO: si el primer login del dia falla durante el proceso (timeout, deadlock), ¿se reintenta? ¿Se loggea? ¿Bloquea al usuario?
- Q-STOCK-JORNADA-DESFASE-COMPONENTE: cual es el SP / clase / componente concreto que calcula `stock_jornada_desfase`.

## Vinculos

- L-035: ejemplo prototipico del patron "no codificar por cliente".
- L-037 (Wave 11): consumers de stock_jornada en CEALSA (regulatorio + facturacion + portal).
