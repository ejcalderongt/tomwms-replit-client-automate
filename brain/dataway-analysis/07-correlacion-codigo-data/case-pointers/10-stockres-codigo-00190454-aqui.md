# CP-010 — clsLnStock_res_Partial breakpoint `Codigo = "00190454"` con `Debug.Print("Aqui")`

> En el bloque `EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING` del DAL de stock reservado, hay un breakpoint arqueológico activo que para el debugger cuando el código de producto es `"00190454"`. El `Debug.Print("Aqui")` está **sin comentar**. Es debug fosilizado en producción.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-010-stockres-codigo-00190454-aqui |
| Tipo | breakpoint arqueológico (instancia del pattern P-001) |
| Estado | documentado — Debug.Print activo (ruido en logs, sin efecto funcional) |
| Severidad estimada | baja |
| Persistencia | no — solo emite a Debug output |
| Archivo | `TOMWMS_BOF/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` |
| Línea | 20947 |
| Tag interno | `EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING` |
| Detectado en | wave-13-8 vía `rg 'Codigo\s*=\s*"[0-9]{8,}"'` |

## Cita textual

```vb
EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING:
#Region "Reservar stock de zona de picking"

    If Not vCantidadCompletada Then

        If BeProducto.Codigo = "00190454" Then
            Debug.Print("Aqui")                ' <-- ACTIVO
        End If

        FechaMinimaVenceStock = Get_Fecha_Vence_Minima_Stock_Reserva_MI3(...)
```

## Lectura

| Elemento | Valor | Significado |
|---|---|---|
| Label | `EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING` | label-firma con timestamp `2023-08-08 12:48` |
| Producto | `"00190454"` | SKU específico investigado |
| Acción | `Debug.Print("Aqui")` | breakpoint activo (no comentado) |

El **label** `EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING:` es interesante: es una **etiqueta de bloque GoTo** firmada con tu convención (`EJC` + timestamp `202308081248`). Podés saltar a este bloque con `GoTo EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING`. Es la firma de wave del bloque entero (cuándo lo escribiste/refactorizaste), no del breakpoint individual.

→ El breakpoint `If BeProducto.Codigo = "00190454" Then Debug.Print("Aqui")` probablemente se agregó **después** del label, en una sesión de debug específica donde ese SKU estaba dando problema de reserva desde zona de picking.

## Por qué `Debug.Print` es ruido y no riesgo

`Debug.Print` solo emite al panel de Output del IDE de Visual Studio. En **release builds** está usualmente compilado fuera (depende de config de proyecto). Aún si quedara en runtime, no afecta datos ni performance significativamente. Pero **es ruido** y **es deuda visible**.

## Acción propuesta esta wave

Ninguna — solo medición.

## Acciones propuestas (próximas waves)

1. **Confirmar autoría**: timestamp del label sugiere EJC pero el breakpoint puede ser posterior y de otra mano.
2. **Query 12** sobre Killios PRD (cuando Erik lo apruebe): ¿el producto `00190454` tiene patrón reciente de problemas de reserva desde zona de picking? Tabla candidata: `tarea_reabasto`, `stock`, `stock_res`.
3. **Limpieza candidato**: si nada está pendiente para `00190454`, comentar el `Debug.Print` (no eliminarlo, dejarlo como rastro arqueológico).

## Cross-refs

- `case-pointers/patterns/breakpoint-arqueologico-codigo-hardcoded.md` — pattern P-001
- `case-pointers/11-stockres-codigo-00091035-espera.md` — CP-011, otro breakpoint en el mismo archivo monstruo
- `brain/debuged-cases/CP-010.md` — bitácora viva
