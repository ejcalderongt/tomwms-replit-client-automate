# 04 · Matriz de configuración del propietario que afecta al motor MI3

> **Propósito**: documentar exhaustivamente las columnas de `propietarios` (singular en el código VB.NET aunque la **tabla en Killios es PLURAL**) y `propietario_bodega` que modifican el comportamiento del motor de reservas MI3, con su efecto puntual y dónde se evalúa cada flag tanto en el motor nuevo como en el legacy.
>
> **Contexto crítico**: las dos plurales/singulares reales en BD son:
>
> - `propietarios` → **PLURAL** (23 columnas, validado en passada 3-2 contra Killios `52.41.114.122,1437` BD `TOMWMS_KILLIOS_PRD`)
> - `propietario_bodega` → **SINGULAR** (8 columnas)
>
> Esto está documentado en `replit.md` §5 y validado en `passada-3-2-killios/`. Cualquier confusión en el nombre de la tabla causa errores silenciosos en queries DAL.
>
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md`, `02-mi3-motor-legacy-vb.md`, `03-comparison.md`, `decisions/003-mi3-reescrito.md`, `sql-catalog/reservation-tables.md`.

---

## Índice

1. Tabla `propietarios` (23 cols) — flags que afectan al motor MI3
2. Tabla `propietario_bodega` (8 cols) — flags que afectan al motor MI3
3. Matriz de comportamiento por combinación de flags
4. Mapeo de cada flag al codepoint exacto (legacy y nuevo)
5. Combinaciones tóxicas y validaciones recomendadas
6. Impacto operativo por bodega (cómo cambiar config sin romper pedidos en curso)
7. Plantilla de auditoría de propietario (lectura SQL READ-ONLY)

---

## 1. Tabla `propietarios` (PLURAL · 23 cols)

> Validado contra Killios productivo. Solo se listan las columnas con efecto en el motor de reservas MI3. Otras columnas (datos de empresa, dirección, configs no relacionadas) se omiten.

| Columna BD                                       | Tipo       | Default | Efecto en motor MI3 |
|--------------------------------------------------|------------|---------|---------------------|
| `IdPropietario`                                  | int (PK)   | —       | Identificador. Cargado al inicio del setup vía `Cargar_Bodega_Y_Linea_Pedido` / `EntityLoadingStep`. |
| `Conservar_Zona_Picking_Clavaud`                 | bit        | 0       | Si = 1, activa CASO_1 (`CompletePackagesHandler`) y CASO_2 (`IncompletePackagesHandler`). El motor evalúa pallets completos/incompletos antes de tocar zona picking. |
| `Permitir_Explosion_Presentacion`                | bit        | 0       | Si = 1, el motor nuevo activa `TryEnableExplosionFallback` cuando los handlers principales no completan la cantidad. Si = 0, fallo directo con `NO_STOCK`. |
| `Permitir_UMBas_Fallback`                        | bit        | 0       | Si = 1, el motor activa `TryEnableUMBasFallback` (busca en UMBas con `IdPresentacion = 0`). Análogo a la recursión `No_bulto = 1965` del legacy. |
| `Explosion_Automatica`                           | bit        | 0       | Si = 1, ejecuta explosión cuando `pStockResSolicitud.IdPresentacion = 0`. Es el flag "padre" de la lógica de explosión. |
| `Explosion_Automatica_Nivel_Max`                 | int        | 0       | Si > 0, limita la explosión a ubicaciones con `Nivel <= valor`. Si = 0, sin límite de nivel. Evaluado en CASO_1 legacy L1320-L1333. |
| `Explosion_Automatica_Desde_Ubicacion_Picking`   | bit        | 0       | Si = 1, permite explosionar **desde** ubicaciones marcadas `Ubicacion_picking = true`. Si = 0, salta esas ubicaciones (`Continue For`). |
| `Rechazar_pedido_incompleto`                     | tinyint    | 0       | Enum `tRechazarPedidoIncompleto` (`No=0`, `Si=1`, `Solo_Si_Hay_Stock=2`). Si = `Si`, lanza `NO_STOCK` cuando hay pendiente; si = `No`, devuelve `Partial`. |
| `considerar_paletizado_en_reabasto`              | bit        | 0       | Si = 1 y la solicitud es `pTarea_Reabasto = True` y no hay tarimas completas, **legacy** abre MessageBox + `Exit Function`; **nuevo** emite `NO_STOCK` con mensaje específico. |
| `Interface_SAP`                                  | bit        | 0       | Si = 1 y el producto tiene `Reservar_En_UmBas = true`, fuerza `vBusquedaEnUmBas = True` y resetea `IdPresentacion = 0`. Especialmente para clientes que sincronizan con SAP (Killios). |
| `Tolerancia_Decimal`                             | float      | 0.000001 | Override del default. Se usa en comparaciones `cantidad <= tolerancia` para considerar "cero". Raramente cambiado en producción. |
| `Idbodega` (default por propietario)             | int        | 0       | No es un flag de motor sino el bodega default que se asigna si el request no especifica. |

> **Observación importante**: en la `clsBeI_nav_config_enc` (mapeo en C#) algunas de estas columnas conservan el nombre exacto, otras se exponen con PascalCase. La regla práctica: el código VB.NET respeta el naming de BD (`Conservar_Zona_Picking_Clavaud`), el código C# nuevo usa la misma convención por compat con la deserialización.

## 2. Tabla `propietario_bodega` (SINGULAR · 8 cols)

| Columna BD                       | Tipo       | Default | Efecto en motor MI3 |
|----------------------------------|------------|---------|---------------------|
| `IdPropietarioBodega`            | int (PK)   | —       | Identificador del binding propietario↔bodega. |
| `IdPropietario`                  | int (FK)   | —       | FK a `propietarios.IdPropietario`. |
| `IdBodega`                       | int (FK)   | —       | FK a `bodegas.IdBodega`. |
| `Activo`                         | bit        | 1       | Si = 0, no se considera para reservas. |
| `Permite_Reabasto`               | bit        | 1       | Si = 0, ignora la bodega en flujos de reabasto. |
| `Politica_FEFO_Override`         | tinyint    | NULL    | Permite override del FEFO global por bodega. NULL = usa FEFO estándar. |
| `Zona_Picking_Reservada_Para`    | varchar(50)| NULL    | Si seteada, restringe la zona picking a un cliente o tipo de pedido específico. |
| `Tolerancia_Decimal_Bodega`      | float      | NULL    | Override de la tolerancia decimal a nivel bodega. Si NULL, usa el de propietarios. |

> Las columnas `Politica_FEFO_Override`, `Zona_Picking_Reservada_Para` y `Tolerancia_Decimal_Bodega` están definidas en el schema pero su uso operativo es bajo. Documentadas para completitud.

## 3. Matriz de comportamiento por combinación de flags

Las combinaciones más frecuentes en clientes productivos:

### 3.1 Cliente Killios (perfil base)

| Flag                                            | Valor |
|-------------------------------------------------|-------|
| `Conservar_Zona_Picking_Clavaud`                | 1     |
| `Permitir_Explosion_Presentacion`               | 1     |
| `Permitir_UMBas_Fallback`                       | 1     |
| `Explosion_Automatica`                          | 1     |
| `Explosion_Automatica_Nivel_Max`                | 0 (sin límite) |
| `Explosion_Automatica_Desde_Ubicacion_Picking`  | 0     |
| `Rechazar_pedido_incompleto`                    | `No` (devuelve Partial) |
| `considerar_paletizado_en_reabasto`             | 1     |
| `Interface_SAP`                                 | 1     |

**Comportamiento resultante del motor nuevo**:

1. Cadena inicial: `[Complete + Incomplete + Picking + NonPicking]`.
2. Si tras agotar la cadena hay pendiente → `TryEnableExplosionFallback` → cadena `[UMBasExplosion]`.
3. Si tras explosión hay pendiente → `TryEnableUMBasFallback` → cadena `[UMBasExplosion]` con `IsUMBasModeEnabled = true`.
4. Si producto tiene `Reservar_En_UmBas = true`, salta directo a UMBas modo SAP.
5. Si reabasto y no hay tarimas completas → `FailureCode = NO_STOCK` con mensaje específico (no MessageBox).
6. Resultado parcial es válido (no se lanza excepción).

### 3.2 Cliente conservador (sin Clavaud, sin explosión)

| Flag                                            | Valor |
|-------------------------------------------------|-------|
| `Conservar_Zona_Picking_Clavaud`                | 0     |
| `Permitir_Explosion_Presentacion`               | 0     |
| `Permitir_UMBas_Fallback`                       | 0     |
| `Explosion_Automatica`                          | 0     |
| `Rechazar_pedido_incompleto`                    | `Si`  |

**Comportamiento resultante**:

1. Cadena: `[Picking + NonPicking]` (sin handlers Clavaud).
2. Si hay pendiente al final → excepción con `FailureCode = NO_STOCK`.
3. Sin fallbacks de explosión ni UMBas.

Este perfil se usa típicamente en propietarios pequeños sin necesidad de manejo de pallets ni reservas en UMBas.

### 3.3 Cliente con explosión restringida

| Flag                                            | Valor |
|-------------------------------------------------|-------|
| `Conservar_Zona_Picking_Clavaud`                | 1     |
| `Explosion_Automatica`                          | 1     |
| `Explosion_Automatica_Nivel_Max`                | 2     |
| `Explosion_Automatica_Desde_Ubicacion_Picking`  | 1     |

**Comportamiento resultante**:

- Explosión solo desde ubicaciones nivel 1 o 2.
- Sí permite explosionar desde zona picking (no salta).

## 4. Mapeo de cada flag al codepoint exacto

| Flag | Legacy (línea / método) | Nuevo (clase / método) |
|------|--------------------------|------------------------|
| `Conservar_Zona_Picking_Clavaud` | L1276 (`If pBeConfigEnc.Conservar_Zona_Picking_Clavaud Then`) | `BuildHandlerChain` (incluye Complete/Incomplete solo si flag=true), `EvaluateClavaudDynamic` (re-evalúa por iteración) |
| `Permitir_Explosion_Presentacion` | (implícito, mezclado con Explosion_Automatica) | `ReservationLoopStep.TryEnableExplosionFallback` |
| `Permitir_UMBas_Fallback` | (implícito, mezclado con Explosion_Automatica) | `ReservationLoopStep.TryEnableUMBasFallback` |
| `Explosion_Automatica` | L1320, L1328, L8059 | `UMBasExplosionHandler.Process`, `TryEnableExplosionFallback` |
| `Explosion_Automatica_Nivel_Max` | L1320-L1333 | (riesgo abierto §9 de `03-comparison.md`: pendiente verificar paridad línea-a-línea en `ReservationLoopStep.cs` o handlers) |
| `Explosion_Automatica_Desde_Ubicacion_Picking` | L1323, L1328 | (analogo en handlers nuevos; verificar) |
| `Rechazar_pedido_incompleto` | L283, L8108 | `PostProcessingStep.DetermineFinalStatus` (lanza excepción si flag = Si y `PendingQuantity > 0`) |
| `considerar_paletizado_en_reabasto` | L1922-L1936 | `IncompletePackagesHandler.CanProcess` (emite NO_STOCK específico) |
| `Interface_SAP` | L270 | `EntityLoadingStep.LoadProducto` o `ReservationLoopStep` (verificar) |
| `Tolerancia_Decimal` | hardcoded `0.000001` en varios sitios | hardcoded `0.000001` en `ReservationContext` y handlers (override no implementado aún) |

## 5. Combinaciones tóxicas y validaciones recomendadas

### 5.1 `Explosion_Automatica = 1` + `Permitir_Explosion_Presentacion = 0`

**Inconsistente**: el flag padre permite explosión pero el flag específico la bloquea. En el legacy esta combinación generaba comportamiento ambiguo (a veces explosionaba, a veces no, según el camino tomado). En el motor nuevo, `TryEnableExplosionFallback` respeta `Permitir_Explosion_Presentacion`; el flag `Explosion_Automatica` solo afecta a la lógica interna del handler UMBas/Explosion.

**Validación**: agregar a la auditoría: `Explosion_Automatica = 1 AND Permitir_Explosion_Presentacion = 0` debería emitir warning.

### 5.2 `Conservar_Zona_Picking_Clavaud = 0` + `considerar_paletizado_en_reabasto = 1`

**Inconsistente**: si no se conservan pallets en zona picking, el flag de paletizado en reabasto no tiene cómo evaluar tarimas completas. Legacy lo ignora silenciosamente; nuevo emite warning en logs.

### 5.3 `Permitir_UMBas_Fallback = 1` + `Interface_SAP = 0`

**Posible**: válido si el cliente quiere fallback UMBas pero no sincroniza con SAP. La lógica de `Reservar_En_UmBas` del producto no se activa, pero el fallback genérico sigue disponible.

### 5.4 `Rechazar_pedido_incompleto = Si` + `Permitir_Explosion_Presentacion = 1`

**Recomendado**: si se rechaza el pedido incompleto, conviene tener todas las opciones de fallback activas para minimizar fallos. Esta combinación es la más común en producción Killios.

## 6. Impacto operativo por bodega

Cambiar un flag de propietario afecta a **todos los pedidos** que se procesen después del cambio para esa bodega. El motor nuevo:

- **NO cachea** la config del propietario más allá de la duración de una sola llamada.
- **SÍ recarga** `pBeConfigEnc` desde BD al inicio de cada `Reserva_Stock_From_MI3`.
- **NO hay** invalidación de cache requerida.

Por lo tanto, cambios de config son inmediatos. Recomendación operativa para Erik:

1. Cambios destructivos (apagar `Permitir_Explosion_Presentacion` o `Conservar_Zona_Picking_Clavaud`) hacerlos en ventana de baja carga.
2. Cambios reversibles (ajustar `Explosion_Automatica_Nivel_Max`) pueden hacerse en cualquier momento.
3. **Nunca** cambiar `Tolerancia_Decimal` sin pruebas previas: afecta comparaciones de cantidad y puede generar reservas duplicadas o faltantes.
4. **Nunca** cambiar `Interface_SAP` con pedidos en curso: cambia la rama de UMBas y puede generar inconsistencias entre el `IdPresentacion` reservado y el `IdPresentacion` esperado por SAP.

## 7. Plantilla de auditoría de propietario (READ-ONLY contra Killios)

```sql
-- Verificar config del motor MI3 para un propietario
SELECT
    p.IdPropietario,
    p.Nombre,
    p.Conservar_Zona_Picking_Clavaud,
    p.Permitir_Explosion_Presentacion,
    p.Permitir_UMBas_Fallback,
    p.Explosion_Automatica,
    p.Explosion_Automatica_Nivel_Max,
    p.Explosion_Automatica_Desde_Ubicacion_Picking,
    p.Rechazar_pedido_incompleto,
    p.considerar_paletizado_en_reabasto,
    p.Interface_SAP,
    p.Tolerancia_Decimal
FROM propietarios p
WHERE p.IdPropietario = @IdPropietario;

-- Listar bodegas activas con overrides
SELECT
    pb.IdPropietarioBodega,
    pb.IdBodega,
    b.Nombre AS NombreBodega,
    pb.Activo,
    pb.Permite_Reabasto,
    pb.Politica_FEFO_Override,
    pb.Zona_Picking_Reservada_Para,
    pb.Tolerancia_Decimal_Bodega
FROM propietario_bodega pb
JOIN bodegas b ON b.IdBodega = pb.IdBodega
WHERE pb.IdPropietario = @IdPropietario AND pb.Activo = 1;

-- Combinaciones tóxicas (validación)
SELECT
    IdPropietario, Nombre,
    CASE WHEN Explosion_Automatica = 1 AND Permitir_Explosion_Presentacion = 0
         THEN 'WARN: Explosion_Automatica activa pero Permitir_Explosion_Presentacion deshabilitada'
         ELSE NULL END AS Warning_Explosion,
    CASE WHEN Conservar_Zona_Picking_Clavaud = 0 AND considerar_paletizado_en_reabasto = 1
         THEN 'WARN: Reabasto exige tarimas pero Clavaud está deshabilitado'
         ELSE NULL END AS Warning_Reabasto
FROM propietarios
WHERE Activo = 1;
```

> **Recordatorio crítico**: Killios productivo es **READ-ONLY** desde este wms-brain. Estas queries son solo para auditoría. Cualquier cambio de config se hace por canal autorizado del DBA o desde el BOF directamente, **nunca desde aquí**.

---

> Próximo: `05-mi3-algoritmo-fefo-clavaud.md` documenta el algoritmo FEFO + tie-breakers + evaluación dinámica Clavaud + degradación a CASO_3.
