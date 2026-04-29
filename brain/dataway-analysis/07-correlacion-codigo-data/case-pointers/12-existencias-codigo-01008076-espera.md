# CP-012 — frmExistenciasConReserva breakpoint `Codigo = "01008076"` con `Debug.Print("Espera")`

> Tercer breakpoint arqueológico en archivos del módulo Reportes. Para el debugger cuando el código de producto es `"01008076"` durante el cálculo de existencias con reserva. Está activo. Aparece junto a otro guard comentado `If P.Cant_Presentacion = 0 Then ... End If`.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-012-existencias-codigo-01008076-espera |
| Tipo | breakpoint arqueológico (instancia del pattern P-001) |
| Estado | documentado — `Debug.Print` activo |
| Severidad estimada | baja |
| Persistencia | no |
| Archivo | `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Resumen_Stock/frmExistenciasConReserva.vb` |
| Línea | 283 |
| Detectado en | wave-13-8 vía `rg 'Codigo\s*=\s*"[0-9]{8,}"'` |

## Cita textual

```vb
For Each P In DsExistenciasConReserva.Detalle()

    ChangeLabelMsg("Actualizando_Reservas: " & P.Codigo)

    If P.Codigo = "01008076" Then
        Debug.Print("Espera")                  ' <-- ACTIVO
    End If

    'If P.Cant_Presentacion = 0 Then           ' <-- guard COMENTADO
    '    P.Cant_Presentacion = 0
    '    End If

    If P.Cant_UMBas = 0 Then
        P.Cant_UMBas = 0
    End If

    If P.Cant_UMBas > 0 Then
        ' ... resto del flujo de actualización de reservas
```

## Lectura

Tres elementos juntos en el mismo entorno:

1. **Breakpoint activo** para `"01008076"`.
2. **Guard comentado** que igualaba `P.Cant_Presentacion` a sí mismo (no-op): `If P.Cant_Presentacion = 0 Then P.Cant_Presentacion = 0 End If` — esto es **muerto-muerto** (operación nula).
3. **Guard activo** que iguala `P.Cant_UMBas` a sí mismo (también no-op): `If P.Cant_UMBas = 0 Then P.Cant_UMBas = 0 End If`.

Los dos guards "asignar a sí mismo" son **placeholders abandonados**: el desarrollador tenía la intención de hacer algo (probablemente recalcular `Cant_Presentacion` cuando es 0, o similar) y dejó la estructura armada con cuerpo nulo. Es una **firma de "vuelvo más tarde"** que no volvió.

Eso convierte a este sitio en un caso doble: breakpoint activo + intención abandonada. Vale tenerlo en bitácora.

## Acción propuesta esta wave

Ninguna — solo medición.

## Acciones propuestas (próximas waves)

1. **Confirmar autoría** del breakpoint y de los guards.
2. **Query 14** sobre Killios PRD (cuando Erik lo apruebe): ¿el producto `01008076` tiene patrón reciente de problema en cálculo de reservas?
3. **Limpieza candidato**: tres acciones independientes (comentar `Debug.Print`, eliminar el guard comentado muerto-muerto, decidir qué hacer con el guard activo no-op).

## Cross-refs

- `case-pointers/patterns/breakpoint-arqueologico-codigo-hardcoded.md` — pattern P-001
- `brain/debuged-cases/CP-012.md` — bitácora viva
