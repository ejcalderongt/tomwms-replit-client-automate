# CP-011 — clsLnStock_res_Partial breakpoint `Codigo = "00091035"` con `Debug.Write("Espera")`

> Segundo breakpoint arqueológico en `clsLnStock_res_Partial.vb`. Para el debugger cuando el código de producto es `"00091035"`. Usa `Debug.Write` (no `Debug.Print`). Está activo.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-011-stockres-codigo-00091035-espera |
| Tipo | breakpoint arqueológico (instancia del pattern P-001) |
| Estado | documentado — `Debug.Write` activo |
| Severidad estimada | baja |
| Persistencia | no |
| Archivo | `TOMWMS_BOF/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` |
| Línea | 27264 |
| Detectado en | wave-13-8 vía `rg 'Codigo\s*=\s*"[0-9]{8,}"'` |

## Cita textual

```vb
                If BeProducto.Codigo = "00091035" Then
                    Debug.Write("Espera")           ' <-- ACTIVO, ojo: Debug.Write no Debug.Print
                End If

                If lBeStockExistente.Count > 0 Then
                    ' ... resto del flujo de procesamiento de stock existente
```

## Diferencia entre `Debug.Print` y `Debug.Write`

- `Debug.Print(s)` agrega newline al final.
- `Debug.Write(s)` escribe sin newline.

Funcionalmente equivalentes para parar un breakpoint (ambos requieren llegar al call). La diferencia es **estética en el panel Output**. Que en este sitio se usó `Debug.Write` (en vez del `Debug.Print` habitual) sugiere otra mano o un copy-paste de otro contexto. **Pista de autoría débil**.

## Lectura

El breakpoint está justo antes de `If lBeStockExistente.Count > 0`, es decir, vos querías parar **antes** de que el flujo decidiera si había stock disponible o no para el producto `00091035`.

## Acción propuesta esta wave

Ninguna — solo medición.

## Acciones propuestas (próximas waves)

1. **Confirmar autoría**: el `Debug.Write` (vs `Debug.Print`) puede ser pista de mano distinta.
2. **Query 13** sobre Killios PRD (cuando Erik lo apruebe): ¿el producto `00091035` tiene patrón reciente de problemas de stock disponible vs reservado?
3. **Limpieza candidato**: comentar la línea.

## Cross-refs

- `case-pointers/patterns/breakpoint-arqueologico-codigo-hardcoded.md` — pattern P-001
- `case-pointers/10-stockres-codigo-00190454-aqui.md` — CP-010, primer breakpoint del mismo archivo
- `brain/debuged-cases/CP-011.md` — bitácora viva
