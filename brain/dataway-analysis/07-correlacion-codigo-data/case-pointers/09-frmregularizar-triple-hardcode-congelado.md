# CP-009 — frmRegularizarInventario triple hardcode `01007121 / 01007011 / IdStock=4427`

> En el flujo `Congelado` de regularización de inventario, hay un guard de breakpoint que combina **dos códigos de producto hardcodeados** y **un `IdStock` numérico hardcodeado** unidos con `OrElse`. El cuerpo es un `Debug.Print("Espera")` **comentado**. Es la firma clásica del patrón "breakpoint arqueológico multi-objeto" — vos paraste el debugger en tres casos pegados juntos.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-009-frmregularizar-triple-hardcode-congelado |
| Tipo | breakpoint arqueológico (instancia del pattern P-001) |
| Estado | documentado — sin efecto en producción (cuerpo comentado) |
| Severidad estimada | media (revela tres casos investigados juntos, posible correlación) |
| Persistencia | no — solo decoración con cuerpo inerte |
| Archivo | `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Transacciones/Inventario/frmRegularizarInventario.vb` |
| Línea | 526 (también clon en `vctmp50560_255693.frmRegularizarInventario.d67850d0.Resultado.vb:526`) |
| Detectado en | wave-13-8 vía `rg 'Codigo\s*=\s*"[0-9]{8,}"'` |

## Cita textual

```vb
For Each BeVWStockRes In Congelado

    Producto.IdProducto = BeTransInvStockProd.IdProducto

    Producto = clsLnProducto.GetSingle(BeTransInvStockProd.IdProducto,
                                       lConnection,
                                       lTransaction)

    If Producto.Codigo = "01007121" OrElse Producto.Codigo = "01007011" OrElse BeVWStockRes.IdStock = 4427 Then
        'Debug.Print("Espera")                  ' <-- cuerpo COMENTADO
    End If

    If BeTransInvStockProd.Fecha_vence <> BeVWStockRes.Fecha_Vence Then
        ' ... resto del flujo: genera ajuste por fecha de vencimiento
    End If

Next
```

## Lectura

Tres elementos pegados juntos en un `OrElse`:

| # | Tipo | Valor | Hipótesis |
|---|---|---|---|
| 1 | `Producto.Codigo` | `"01007121"` | producto específico investigado |
| 2 | `Producto.Codigo` | `"01007011"` | producto similar — códigos consecutivos en serie `0100`, posible familia |
| 3 | `BeVWStockRes.IdStock` | `4427` | **fila concreta de stock**, no un producto entero |

La mezcla de "código de producto" + "IdStock concreto" sugiere:

- Los productos `01007121` y `01007011` tuvieron problemas reproducibles en general.
- El `IdStock = 4427` fue un caso puntual — quizás una fila de stock que se comportaba mal sin ser explicada por el código del producto. Pero `IdStock` **no es estable** (ver glosario), por lo cual ese valor concreto puede haber dejado de existir hace años (DELETE + INSERT lo desplazaron).

## Contexto: el flujo `Congelado`

El bloque corre dentro de un `For Each BeVWStockRes In Congelado`. Es decir, sobre la lista de stock **congelado** (estado bloqueado, no disponible para reserva). El propósito del flujo siguiente es generar un **ajuste por fecha de vencimiento**: cuando la `Fecha_vence` del registro de stock proveniente de inventario no coincide con la del registro de stock real, se crea una línea de ajuste.

El breakpoint estaba puesto **antes** del check de fecha — es decir, vos querías parar el debugger **antes** de que el flujo decidiera si generar ajuste o no. La pregunta arqueológica: ¿qué estabas investigando? Probablemente un caso donde el ajuste se generaba mal o se generaba de más para esos tres elementos.

## Clon

Existe un clon en:

```
TOMWMS_BOF/reservastockfrommi3/vctmp50560_255693.frmRegularizarInventario.d67850d0.Resultado.vb:526
```

El nombre `vctmp50560_*` y `Resultado.vb` huele a archivo temporal generado por una operación de merge o conflict resolution de Visual SourceSafe / similar. **Es código basura que sobrevivió al control de versiones**, no un clon experimental legítimo.

→ Acción complementaria: revisar si la carpeta `reservastockfrommi3/` está siendo compilada o es código muerto en disco. Si es muerto, candidato a limpieza.

## Acción propuesta esta wave

Ninguna — solo medición.

## Acciones propuestas (próximas waves)

1. **Confirmar autoría**: el comentario está sin firma. Probable EJC pero no se puede asegurar sin entrevista.
2. **Query 11** sobre BD de Killios PRD (cuando Erik lo apruebe): ¿esos dos productos `01007121` y `01007011` tienen patrón reciente de ajustes raros?
3. **Limpieza basura `vctmp50560_*`**: confirmar si es código muerto y proponer barrido.

## Cross-refs

- `case-pointers/patterns/breakpoint-arqueologico-codigo-hardcoded.md` — pattern P-001, instancias incluyen CP-001, CP-009, CP-010, CP-011, CP-012
- `case-pointers/01-stockfecha-codigo-030772033524.md` — CP-001 (instancia más vieja del pattern, `030772033524`)
- `brain/debuged-cases/CP-009.md` — bitácora viva
