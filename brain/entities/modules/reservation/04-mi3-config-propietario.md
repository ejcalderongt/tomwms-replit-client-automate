---
id: 04-mi3-config-propietario
tipo: entity
estado: vigente
titulo: "04 · Matriz de configuración del motor MI3 (tabla `i_nav_config_enc`)"
modulo: [reservation]
tags: [entity, modulo/reservation]
---

# 04 · Matriz de configuración del motor MI3 (tabla `i_nav_config_enc`)

> **CORRECCIÓN respecto a la versión anterior**: la versión 1 de este documento ubicaba erróneamente los flags del motor MI3 en la tabla `propietarios`. Tras consulta READ-ONLY a Killios productivo (`52.41.114.122,1437`, BD `TOMWMS_KILLIOS_PRD`), se confirmó que:
>
> - La tabla `propietarios` (PLURAL, 23 cols) **solo contiene datos básicos del propietario** (contacto, dirección, NIT, etc.) y NO contiene flags del motor.
> - La tabla `propietario_bodega` (SINGULAR, 8 cols) **solo contiene el binding propietario↔bodega** con flag `activo`. NO contiene `Politica_FEFO_Override`, `Zona_Picking_Reservada_Para` ni `Tolerancia_Decimal_Bodega` (campos inventados en versión anterior).
> - **Los flags del motor MI3 viven en `i_nav_config_enc`** (69 cols) con granularidad `(idempresa, idbodega, idPropietario, idUsuario)`. Esto significa que la configuración del motor es **por combinación bodega+propietario+usuario**, no por propietario global.
>
> Esta es la versión corregida con schema validado contra Killios productivo el 2026-04-27.
>
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md`, `02-mi3-motor-legacy-vb.md`, `03-comparison.md`, `decisions/003-mi3-reescrito.md`, `sql-catalog/reservation-tables.md` (a producir).

---

## Índice

1. Tabla `i_nav_config_enc` (69 cols) — granularidad y flags MI3
2. Tabla `propietarios` (23 cols) — solo datos básicos
3. Tabla `propietario_bodega` (8 cols) — binding
4. Tabla `bodega` — flags relacionados (Interface_SAP)
5. Mapeo de cada flag al codepoint exacto (legacy y nuevo)
6. Typo histórico: `explosio_automatica_nivel_max` vs `explosion_automatica_nivel_max`
7. Combinaciones tóxicas y validaciones recomendadas
8. Plantilla de auditoría READ-ONLY

---

## 1. Tabla `i_nav_config_enc` (69 cols) · granularidad y flags MI3

### 1.1 Identidad y granularidad

```
PK:                 idnavconfigenc int identity
Granularidad:       (idempresa, idbodega, idPropietario, idUsuario)
Mapeo en código:    clsBeI_nav_config_enc (entidad VB.NET)
```

> **Implicación operativa**: cada combinación de empresa+bodega+propietario+usuario puede tener su propia configuración del motor. En la práctica `idUsuario` suele ser NULL (config a nivel propietario+bodega), pero el schema permite override por usuario.

### 1.2 Flags que afectan al motor MI3

| Columna BD                                          | Tipo  | Default | Efecto en motor MI3 |
|-----------------------------------------------------|-------|---------|---------------------|
| `rechazar_pedido_incompleto`                        | int   | NULL    | Enum `tRechazarPedidoIncompleto` (`No=0`, `Si=1`, `Solo_Si_Hay_Stock=2`). Si = `Si`, lanza `NO_STOCK` cuando hay pendiente; si = `No`, devuelve `Partial`. Nota: es **int**, no bit. |
| `despachar_existencia_parcial`                      | int   | NULL    | Si > 0, permite despachar parcialmente (relacionado pero distinto a `rechazar_pedido_incompleto`). |
| `convertir_decimales_a_umbas`                       | int   | NULL    | Si > 0, convierte cantidades fraccionales a UMBas en vez de rechazar. |
| `reservar_umbas_primero`                            | bit   | 0 NOT NULL | Si = 1, prioriza la búsqueda en UMBas antes que la búsqueda en presentación. Equivalente conceptual al `Permitir_UMBas_Fallback` que documenté erróneamente en v1. |
| `implosion_automatica`                              | bit   | 0 NOT NULL | Si = 1, permite consolidar UMBas sueltas en presentación (operación inversa a explosión). Para escenarios de reabasto. |
| `explosion_automatica`                              | bit   | 0 NOT NULL | Si = 1, ejecuta explosión cuando `pStockResSolicitud.IdPresentacion = 0`. Es el flag "padre" de la lógica de explosión. |
| `Ejecutar_En_Despacho_Automaticamente`              | bit   | 0 NOT NULL | Si = 1, la explosión/implosión ocurre durante el despacho (no en reserva). |
| `IdTipoRotacion`                                    | int   | NULL    | Política FEFO/LIFO/FIFO (por defecto FEFO si NULL). Mapeado vía catálogo. |
| `explosio_automatica_nivel_max` ⚠️ TYPO            | int   | NULL    | **Columna histórica con typo** (sin `n` en "explosio"). Se mantiene por compat. Ver §6. |
| `explosion_automatica_nivel_max`                    | int   | NULL    | Versión corregida del flag. Limita explosión a ubicaciones con `Nivel <= valor`. Si NULL o 0, sin límite. **El código tiene que decidir cuál de las dos usar** — riesgo abierto. |
| `explosion_automatica_desde_ubicacion_picking`      | bit   | NULL    | Si = 1, permite explosionar desde ubicaciones marcadas `Ubicacion_picking = true`. Si = 0/NULL, salta esas ubicaciones. |
| `conservar_zona_picking_clavaud`                    | bit   | NULL    | Si = 1, activa CASO_1 (`CompletePackagesHandler`) y CASO_2 (`IncompletePackagesHandler`). El motor evalúa pallets completos/incompletos antes de tocar zona picking. |
| `excluir_ubicaciones_reabasto`                      | bit   | 0 NOT NULL | Si = 1, las ubicaciones marcadas para reabasto se excluyen de las búsquedas de stock. |
| `considerar_paletizado_en_reabasto`                 | bit   | 0 NOT NULL | Si = 1 y la solicitud es `pTarea_Reabasto = True` y no hay tarimas completas, **legacy** abre MessageBox + `Exit Function`; **nuevo** emite `NO_STOCK` con mensaje específico. |
| `considerar_disponibilidad_ubicacion_reabasto`      | bit   | 0 NOT NULL | Si = 1, valida que la ubicación destino del reabasto tenga disponibilidad antes de generar la tarea. |
| `dias_vida_defecto_perecederos`                     | int   | 0 NOT NULL | Días mínimos de vida útil que un stock debe tener para ser elegible. Sobrescribe el default global. |
| `interface_sap`                                     | bit   | 0 NOT NULL | Si = 1 y el producto tiene `Reservar_En_UmBas = true`, fuerza modo UMBas y resetea `IdPresentacion = 0`. Especialmente para clientes que sincronizan con SAP (Killios). |
| `excluir_recepcion_picking`                         | bit   | 0 NOT NULL | Si = 1, la recepción no genera stock en ubicaciones picking (va directo a ALM). |

### 1.3 Flags **NO existentes** que documenté erróneamente en v1

| Flag inventado en v1                | Realidad                                                         |
|-------------------------------------|------------------------------------------------------------------|
| `Permitir_Explosion_Presentacion`   | NO EXISTE. La explosión se controla solo con `explosion_automatica`. |
| `Permitir_UMBas_Fallback`           | NO EXISTE. El equivalente es `reservar_umbas_primero` o `convertir_decimales_a_umbas`. |
| `Tolerancia_Decimal` (en propietarios) | NO EXISTE. Está **hardcoded `0.000001`** en el motor. |
| `Politica_FEFO_Override` (en propietario_bodega) | NO EXISTE. Equivalente real: `IdTipoRotacion` en `i_nav_config_enc`. |
| `Zona_Picking_Reservada_Para` (en propietario_bodega) | NO EXISTE. |
| `Tolerancia_Decimal_Bodega` (en propietario_bodega) | NO EXISTE. |

### 1.4 Implicación crítica de la granularidad

Como `i_nav_config_enc` se filtra por `(idempresa, idbodega, idPropietario, idUsuario)`, cualquier carga de config en el motor MI3 debe especificar las 3-4 claves. El método `Cargar_Bodega_Y_Linea_Pedido` del legacy (y `EntityLoadingStep` del nuevo) hace esta carga pasando typically `idUsuario = NULL` para obtener la config "default" del propietario+bodega.

> **Riesgo**: si en una bodega+propietario hay 2+ filas en `i_nav_config_enc` (una con `idUsuario` y otra sin), la query puede traer la fila incorrecta. Validar que el WHERE incluya `(idUsuario = @idUsuario OR idUsuario IS NULL) ORDER BY idUsuario DESC` para preferir la específica.

## 2. Tabla `propietarios` (PLURAL · 23 cols) · solo datos básicos

| Columna BD                       | Tipo            | Notas |
|----------------------------------|-----------------|-------|
| `IdPropietario`                  | int (PK)        | Identificador. |
| `IdEmpresa`                      | int             | FK empresa. |
| `IdTipoActualizacionCosto`       | int             | Política de costo. |
| `contacto`                       | nvarchar(100) NOT NULL | Persona de contacto. |
| `nombre_comercial`               | nvarchar(100) NOT NULL | Nombre del propietario. |
| `imagen`                         | image           | Logo. |
| `telefono`, `direccion`, `email` | nvarchar        | Contacto. |
| `activo`                         | bit             | Soft-delete. |
| `user_agr/fec_agr/user_mod/fec_mod` | audit       | Auditoría estándar. |
| `actualiza_costo_oc`             | bit             | Si actualiza costo desde OC. |
| `color`, `codigo`, `sistema`     | varios          | Cosmético/identificación. |
| `NIT`                            | nvarchar(50)    | Identificador fiscal. |
| `codigo_acceso`, `clave_acceso`  | nvarchar(50)    | Credenciales (poco usado). |
| `es_consolidador`                | bit             | Marca consolidación logística. |
| `controlux`                      | bit NOT NULL    | Flag específico ControlLux. |

**No tiene flags del motor MI3**. Si en el código aparece `pBeConfigEnc.Conservar_Zona_Picking_Clavaud`, NO viene de esta tabla — viene de `i_nav_config_enc` cargada vía `Cargar_Bodega_Y_Linea_Pedido`.

## 3. Tabla `propietario_bodega` (SINGULAR · 8 cols) · binding

| Columna BD                       | Tipo  | Notas |
|----------------------------------|-------|-------|
| `IdPropietarioBodega`            | int (PK) | Identificador del binding. |
| `IdPropietario`                  | int FK   | FK a `propietarios.IdPropietario`. |
| `IdBodega`                       | int FK   | FK a `bodega.IdBodega`. |
| `user_agr/fec_agr/user_mod/fec_mod` | audit | Auditoría. |
| `activo`                         | bit      | Si la bodega está activa para el propietario. |

**Solo binding + audit + activo**. Ningún flag de comportamiento del motor.

## 4. Tabla `bodega` — flags relacionados

Solo se documentan los flags relevantes al motor MI3 (la tabla tiene muchas más columnas no relacionadas):

| Columna BD                                  | Tipo  | Efecto |
|---------------------------------------------|-------|--------|
| `restringir_areas_sap`                      | bit   | Si = 1, restringe áreas de stock al integrar con SAP. |
| `interface_SAP`                             | bit   | Override a nivel bodega del flag `interface_sap` de `i_nav_config_enc`. |
| `permitir_decimales`                        | bit   | Permite cantidades fraccionales en reservas. Si = 0, el motor redondea o rechaza. |
| `permitir_buen_estado_en_reemplazo`         | bit   | Para reservas en flujo de reemplazo (no MI3 directo). |
| `permitir_no_encontrado_picking`            | bit   | Si = 1, permite picks de stock no localizado físicamente (telltale para auditoría). |
| `advertir_mpq_umbas`                        | bit   | Advierte cuando UMBas != múltiplo de MPQ del producto. |

## 5. Mapeo de cada flag al codepoint exacto

| Flag (`i_nav_config_enc`)                            | Legacy (línea / método)                          | Nuevo (clase / método)                              |
|------------------------------------------------------|--------------------------------------------------|-----------------------------------------------------|
| `conservar_zona_picking_clavaud`                     | L1276 (`If pBeConfigEnc.Conservar_Zona_Picking_Clavaud Then`) | `BuildHandlerChain` (incluye Complete/Incomplete solo si flag=true), `EvaluateClavaudDynamic` (re-evalúa por iteración) |
| `explosion_automatica`                               | L1320, L1328, L8059                              | `UMBasExplosionHandler.Process`, `TryEnableExplosionFallback` |
| `explosion_automatica_nivel_max` (o el typo)         | L1320-L1333                                      | (riesgo abierto §9 de `03-comparison.md`)           |
| `explosion_automatica_desde_ubicacion_picking`       | L1323, L1328                                     | (analogo en handlers nuevos; verificar)             |
| `rechazar_pedido_incompleto`                         | L283, L8108                                      | `PostProcessingStep.DetermineFinalStatus` (lanza excepción si flag = 1 y `PendingQuantity > 0`) |
| `considerar_paletizado_en_reabasto`                  | L1922-L1936                                      | `IncompletePackagesHandler.CanProcess`              |
| `interface_sap`                                      | L270                                             | `EntityLoadingStep.LoadProducto` o `ReservationLoopStep` (verificar) |
| `reservar_umbas_primero`                             | (verificar en código legacy)                     | `ReservationLoopStep` (orden de invocación)         |
| `convertir_decimales_a_umbas`                        | (verificar en código legacy)                     | `ReservationLoopStep.TryEnableUMBasFallback`        |
| `dias_vida_defecto_perecederos`                      | implícito en `IsExpired`                         | `BaseReservationHandler.IsExpired`                  |
| `excluir_ubicaciones_reabasto`                       | (verificar en query de stock)                    | `StockQueryStep`                                    |
| `IdTipoRotacion`                                     | (verificar)                                      | `BaseReservationHandler` ordering                   |

## 6. Typo histórico: `explosio_automatica_nivel_max` vs `explosion_automatica_nivel_max`

La tabla `i_nav_config_enc` tiene **dos columnas** que parecen referirse al mismo concepto:

```
explosio_automatica_nivel_max int(10) NULL   ← typo histórico (falta la 'n')
explosion_automatica_nivel_max int(10) NULL   ← versión corregida
```

### 6.1 Hipótesis del origen

En algún momento del histórico, la columna se creó con typo. Posteriormente se agregó la columna corregida pero **no se eliminó la original** (probablemente por temor a romper código que la consultaba). Resultado: el código legacy puede leer cualquiera de las dos.

### 6.2 Riesgo

1. Si el código nuevo lee `explosion_automatica_nivel_max` pero el código legacy persistente lee `explosio_automatica_nivel_max`, las configuraciones quedan desincronizadas.
2. Cualquier UI de admin que cambie un valor podría escribir solo en una de las dos columnas, dejando la otra obsoleta.
3. La query de auditoría debe verificar **ambas** columnas y alertar si difieren.

### 6.3 Acción recomendada

Auditar:

```sql
SELECT
    idnavconfigenc, idbodega, idPropietario,
    explosio_automatica_nivel_max  AS Typo,
    explosion_automatica_nivel_max AS Correcta,
    CASE WHEN ISNULL(explosio_automatica_nivel_max, -1)
            <> ISNULL(explosion_automatica_nivel_max, -1)
         THEN 'INCONSISTENT' ELSE 'OK' END AS Status
FROM i_nav_config_enc
WHERE explosio_automatica_nivel_max IS NOT NULL
   OR explosion_automatica_nivel_max IS NOT NULL;
```

Si `Status = INCONSISTENT`, decidir cuál es la fuente de verdad y unificar (por canal autorizado del DBA, no desde aquí).

## 7. Combinaciones tóxicas y validaciones recomendadas

### 7.1 `explosion_automatica = 1` + `explosion_automatica_desde_ubicacion_picking = 0` + sin pallets en ALM

**Resultado**: explosión activada pero no encuentra dónde explotar. El motor degrada a `Failed` con `NO_STOCK`.

### 7.2 `conservar_zona_picking_clavaud = 0` + `considerar_paletizado_en_reabasto = 1`

**Inconsistente**: si no se conservan pallets en zona picking, el flag de paletizado en reabasto no tiene cómo evaluar tarimas completas. Legacy lo ignora silenciosamente; nuevo emite warning en logs.

### 7.3 `reservar_umbas_primero = 1` + `interface_sap = 0`

**Posible**: válido si el cliente quiere prioridad UMBas sin sincronización SAP. La lógica de `Reservar_En_UmBas` del producto no se activa, pero el motor sigue priorizando UMBas en la búsqueda inicial.

### 7.4 `rechazar_pedido_incompleto = 1` + `convertir_decimales_a_umbas = 0`

**Recomendado evitar**: si se rechaza el pedido incompleto, conviene tener `convertir_decimales_a_umbas` activo para minimizar fallos por fracciones.

### 7.5 Typo + valor (§6)

**Combinación peor**: una columna tiene valor y la otra está NULL. El código que lea la NULL hará explosión sin límite cuando el otro código limita.

## 8. Plantilla de auditoría READ-ONLY

```sql
-- Config completa de motor MI3 para una bodega+propietario
SELECT
    c.idnavconfigenc,
    c.idempresa, c.idbodega, c.idPropietario, c.idUsuario,
    c.conservar_zona_picking_clavaud,
    c.explosion_automatica,
    c.explosio_automatica_nivel_max,
    c.explosion_automatica_nivel_max,
    c.explosion_automatica_desde_ubicacion_picking,
    c.implosion_automatica,
    c.reservar_umbas_primero,
    c.convertir_decimales_a_umbas,
    c.rechazar_pedido_incompleto,
    c.despachar_existencia_parcial,
    c.considerar_paletizado_en_reabasto,
    c.considerar_disponibilidad_ubicacion_reabasto,
    c.excluir_ubicaciones_reabasto,
    c.dias_vida_defecto_perecederos,
    c.interface_sap,
    c.IdTipoRotacion,
    c.Ejecutar_En_Despacho_Automaticamente
FROM i_nav_config_enc c
WHERE c.idempresa = @IdEmpresa
  AND c.idbodega = @IdBodega
  AND c.idPropietario = @IdPropietario
ORDER BY c.idUsuario DESC; -- Específica primero, NULL al final

-- Datos del propietario
SELECT IdPropietario, codigo, nombre_comercial, NIT, activo
FROM propietarios WHERE IdPropietario = @IdPropietario;

-- Bindings activos del propietario
SELECT pb.IdBodega, b.codigo AS CodBodega, b.nombre, b.interface_SAP, b.permitir_decimales
FROM propietario_bodega pb
JOIN bodega b ON b.IdBodega = pb.IdBodega
WHERE pb.IdPropietario = @IdPropietario AND pb.activo = 1;

-- Detección de inconsistencias en typo de columna
SELECT idnavconfigenc, idbodega, idPropietario,
       explosio_automatica_nivel_max, explosion_automatica_nivel_max
FROM i_nav_config_enc
WHERE ISNULL(explosio_automatica_nivel_max,-1) <> ISNULL(explosion_automatica_nivel_max,-1);

-- Configs sospechosas (combinaciones tóxicas)
SELECT idnavconfigenc, idbodega, idPropietario,
       CASE WHEN conservar_zona_picking_clavaud = 0
                 AND considerar_paletizado_en_reabasto = 1
            THEN 'WARN: paletizado-reabasto sin clavaud' END AS Warning
FROM i_nav_config_enc
WHERE conservar_zona_picking_clavaud = 0
  AND considerar_paletizado_en_reabasto = 1;
```

> **Recordatorio crítico**: Killios productivo es **READ-ONLY** desde este wms-brain. Estas queries son solo para auditoría. Cualquier cambio de config se hace por canal autorizado del DBA o desde el BOF directamente.

---

> Próximo: `08-mi3-tablas-killios.md` documenta el schema completo de las tablas críticas (`stock` 33 cols, `stock_res` 35 cols, `trans_pe_det` 44 cols, `trans_pe_enc` 70 cols, `i_nav_ped_traslado_det` 22 cols, `log_error_wms` 15 cols) — todos validados contra Killios productivo.
