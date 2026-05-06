---
id: BUG-001-RECONCILIACION-2026-05-06
tipo: replanteo
relacionado_con: [BUG-001, CP-013, traza-002, INFORME-CLIENTE-KILLIOS]
fecha: 2026-05-06
agente: Replit (sesión Erik)
gatillado_por: Carolina Fuentes (CF) — observación de operaciones
estado_previo: BUG-001 OPEN, INFORME-CLIENTE-KILLIOS pendiente de envío
estado_nuevo: BUG-001 tesis-central-refutada, replanteo-en-curso
tags: [bug/picking, bug/critico, replanteo, cliente/killios]
---
# Replanteo BUG-001 tras datos crudos de Carolina (2026-05-06)

## 0. TL;DR

La tesis central de BUG-001 (y por lo tanto del INFORME-CLIENTE-KILLIOS)
**es falsa en su forma fuerte**. El sistema **SÍ** genera
`trans_movimientos` y `trans_ubic_hh_enc` con motivo dañado para el
flujo CEST. El bug existe pero es **mucho más sutil**: los movimientos
generados son **identidad contable** (origen=destino, estado=estado) y
por eso no descuentan stock real.

## 1. Contexto

- 2026-05-05: enviado INFORME-CLIENTE-KILLIOS (anchor §3.1) afirmando
  que el sistema **omite** la generación de `trans_movimientos` para
  marcas de daño en picking.
- 2026-05-06: Carolina Fuentes (CF) observa **12 IdTransaccion** reales
  en BD para el caso anchor (FU06688 / IdProductoBodega=381 /
  IdUbicacion=308): 1113, 1114, 1380, 1712, 1713, 1714, 1715, 1716,
  1927, 1928, 1929, 1930.
- Se reabre la traza para validar contra BD productiva.

## 2. Datos duros (BD `TOMWMS_KILLIOS_PRD_2026`, 2026-05-06)

> Nota: la BD vieja `TOMWMS_KILLIOS_PRD` no tiene rastro del caso. El
> informe y todas las cifras se refieren a `TOMWMS_KILLIOS_PRD_2026`.

### 2.1 trans_picking_ubic dañado_picking=1

12 filas con `IdProductoBodega=381 lic_plate='FU06688' IdUbicacion=308`.

- IdPickingUbic: 135801, 135823, 141063, 151916, 151917, 151998 (+6 más)
- IdPickingEnc únicos: 5318 (feb), 5647 (feb), 6188 (mar), …
- user_agr en todas: '20' (Heidi López)
- user_mod: '20'
- IdStockRes asignados: 126962, 132222, 142814, 142815, …

### 2.2 trans_ubic_hh_enc IN (12 ids Carolina)

12 filas, **todas** con:

- `IdMotivoUbicacion = 3` ("Cambio de estado por reemplazo de
  producto" — motivo configurado para dañado en Killios)
- `Observacion = 'Cambio de estado por reemplazo de producto'`
- `user_agr = '124'` (operador HH distinto al de la marca BOF; revisar
  quién es 124)
- `cambio_estado = true` en 11/12, false en 1 (1114)
- estado: 'Finalizado' en 10/12, 'Finalizado Parcial' en 2 (1114, 1380)

→ **Las 12 IdTransaccion de Carolina son `IdTareaUbicacionEnc` de
`trans_ubic_hh_enc`, NO `IdMovimiento`**. La tabla `trans_movimientos`
tiene una columna `IdTransaccion` que apunta justo a ese encabezado, de
ahí la confusión nominal.

### 2.3 trans_ubic_hh_Det por esos IdStocks

12 filas. **10 con `Realizado=true`**, **2 con `Realizado=false`**:

| IdTareaUbicacionEnc | IdStock | cantidad | recibido | Realizado | estado_enc |
|---|---:|---:|---:|---|---|
| 1114 | 104155 | 17 | 0 | **false** | Finalizado Parcial |
| 1380 | 107869 | 30 | 0 | **false** | Finalizado Parcial |

→ **47 UM realmente huérfanas** en este caso, no las 318,191 declaradas
a escala de cliente.

### 2.4 trans_movimientos IN (12 ids Carolina)

**10 movimientos** generados (faltan los 2 de las tareas no realizadas):

```
IdTransaccion → Cantidad
1113 → 195
1712 → 40
1713 → 40
1714 → 5
1715 → 15
1716 → 135
1927 → 5
1928 → 15
1929 → 40
1930 → 15
TOTAL: 505 UM
```

**Patrón uniforme y crítico** en los 10:

```
IdUbicacionOrigen   = 308
IdUbicacionDestino  = 308     ← MISMA UBICACIÓN
IdEstadoOrigen      = 1       ← disponible
IdEstadoDestino     = 1       ← disponible
IdTipoTarea         = 2       ← no es 17 (AJCANTN)
usuario_agr         = '124'
barra_pallet        = 'FU06688'
lote                = 'BG2512'
```

**El movimiento es identidad contable**. No mueve mercadería entre
ubicaciones, no cambia su estado. Delta neto sobre `producto_bodega_stock`
para Origen 308 = 0. El sistema graba la fila pero no descuenta nada.

## 3. Lectura del código (rama main del repo local, equivalente a
`dev_2028_merge` por contenido)

`TOMIMSV4/DAL/Transacciones/Transaccion_Ubicacion_HH/clsLnTrans_ubic_hh_enc_Partial.vb`

| Línea | Hallazgo | Implicación |
|---:|---|---|
| 691, 720, 761 | `bePickingUbic.Dañado_picking = True` (3 setters en bloques If-EsPicking) | Confirma 3+ puntos de marca; trace decía 5-6 |
| 778 | `clsLnBodega.Get_IdMotivoUbicacion_Dañado_Picking(IdBodega)` | Lectura del motivo, **no es null en Killios** |
| 780 | `Throw ... no está definido IdMotivoUbicacion, no puede realizarse el reemplazo` | Validación existe — refuta hipótesis "bodega sin MERMA" |
| 1374 | `bePickingUbic.Dañado_picking = True` con comentario `'#AT 20220110` | Línea histórica del trace |
| 1377 | `clsLnStock_res.Actualizar_Stock_Reservado_By_IdStockRes(IdStockRes, ...)` | **SÍ toca stock_res** — refuta "no toca stock" |
| 1387 | `Get_IdMotivoUbicacion_Dañado_Picking` (segundo punto del flujo) | Idem 778 |
| 1407 | `beUbicHHEnc.IdTipoTarea = 3` | Header tipo 3, no 17/AJCANTN — coherente con el dato observado |
| 1397 | `Observacion = "Cambio de estado por reemplazo de producto"` | String exacto que aparece en BD |

## 4. Reconciliación de hipótesis vs realidad

| Afirmación INFORME-CLIENTE-KILLIOS § | Estado |
|---|---|
| §3.1.2 "Omite generar `trans_movimientos`" | **REFUTADA** (10 de 12 generados) |
| §3.1.3 "Omite mover a ubicación MERMA" | **REFUTADA en su forma fuerte** (header `trans_ubic_hh_enc` con IdMotivoUbicacion=3 sí se crea; lo que falla es que el detalle `trans_ubic_hh_Det` tiene Origen=Destino, no apunta a MERMA física) |
| §3.1.4 "Omite generar AJCANTN" | **CIERTO PERO IRRELEVANTE** (Erik: dañado no debe ajustar al ERP) |
| §3.3 "Cero llamadas desde WebAPI nuevo" | sin re-validar, probablemente cierto |
| §4.1 "10,565 líneas / 318,191 UM fantasma" | **CIFRA SOSPECHOSA**: la query original probablemente cuenta cualquier `dañado_picking=1` sin descartar las que sí tienen `trans_movimientos.Realizado=true`. Re-auditar |
| §4.4 "ajustes manuales 2019-2023 confirman cronicidad" | independiente, probablemente cierto pero ya no apoya la tesis original |

## 5. El bug REAL (replanteo)

### 5.1 Bug primario: movimientos identidad

El flujo CEST de "reemplazo por dañado" genera `trans_movimientos` con
`Origen = Destino` y `EstadoOrigen = EstadoDestino`. Esto es
contablemente inocuo: no descuenta stock, no cambia el estado del
producto. El registro queda como evidencia del evento operativo pero no
afecta inventario.

**Causa raíz pendiente de localizar**: en el DAL hay que buscar dónde se
arman `IdUbicacionOrigen/Destino` y `IdEstadoOrigen/Destino` del
detalle. Hipótesis: la lógica de "reemplazo" original asumía un patrón
distinto (mover el dañado a una ubicación de merma física) pero la
implementación actual auto-rellena con la ubicación de origen.

### 5.2 Bug secundario: tareas 'Finalizado Parcial' huérfanas

~17% de las marcas (2/12 en este caso anchor) quedan con `Realizado=false`
y nunca generan `trans_movimientos`. Son tareas HH abiertas que el
operador no completó. Si extrapolás a las 10,565 marcas → ~1,760 tareas
HH huérfanas → ~50,000 UM realmente sin movimiento, no 318,191.

### 5.3 Diferencia con el bug informado

| Dimensión | Bug informado | Bug real |
|---|---|---|
| Naturaleza | Omisión total (no se generan filas) | Movimientos generados pero contablemente inocuos |
| Magnitud Killios | 318,191 UM | Pendiente, estimado 50k-150k UM |
| Fix | Agregar lógica omitida en VB.NET (5+ archivos) | Cambiar destino del movimiento a ubicación MERMA real (1 archivo, posiblemente 1 SP) |
| Severidad | CRÍTICA | ALTA (sigue siendo bug, sangrado real, pero menor escala y fix más quirúrgico) |
| Riesgo del fix | Alto (lógica nueva extensa) | Bajo (corrección de parámetros de movimiento existente) |

## 6. Acciones recomendadas

1. **Detener envío del INFORME-CLIENTE-KILLIOS** si no salió. Si salió,
   preparar nota de corrección técnica para Killios.
2. **Re-auditar cifra "318,191 UM"** con query correcta:

   ```sql
   -- Marcas dañadas SIN movimiento real de descuento
   SELECT COUNT(*) AS marcas_realmente_huerfanas
   FROM trans_picking_ubic pu
   WHERE pu.dañado_picking = 1
     AND NOT EXISTS (
       SELECT 1 FROM trans_ubic_hh_Det d
       JOIN trans_ubic_hh_enc e ON e.IdTareaUbicacionEnc = d.IdTareaUbicacionEnc
       WHERE d.IdStock = pu.IdStock
         AND d.Realizado = 1
         AND e.IdMotivoUbicacion = 3  -- motivo dañado
     );
   ```

   Y por separado: cuantificar cuántos de los movimientos generados son
   identidad (Origen=Destino, Estado=Estado).
3. **Marcar BUG-001 como `tesis-central-refutada, replanteo-en-curso`**
   en el INDEX.md. No borrar el contenido — vale como trazabilidad de
   error analítico para futuro.
4. **Anular follow-ups #7, #8, #9** si dependían de la tesis vieja
   (revisar contenido).
5. **Abrir nueva línea de investigación**: por qué el movimiento sale
   con Origen=Destino. Candidatos:
   - Revisar `trans_ubic_hh_Det` setter en el DAL.
   - Revisar SP que inserta `trans_movimientos` (no apareció en search
     de SPs por nombre, probablemente es generado desde código).
   - Ver `clsLnTrans_movimientos.vb` (si existe) para parámetros del
     insert.
6. **Identificar quién es user_agr='124'**: aparece en los 12 headers
   pero el `user_agr` de las marcas en `trans_picking_ubic` es '20'
   (Heidi). Confirmar si '124' es operador HH físico distinto o cuenta
   técnica.

## 7. Cola de validación pendiente

- [ ] Cuantificar movimientos identidad (Origen=Destino) en universo
      Killios completo.
- [ ] Confirmar usuario 124.
- [ ] Localizar setter de IdUbicacionDestino del trans_ubic_hh_Det.
- [ ] Re-correr análisis cross-cliente con la query corregida (Mercopan,
      BYB) — pueden tener bug distinto al supuesto.
- [ ] Decidir narrativa para Killios (transparencia: corregir
      diagnóstico antes que minimizar).

## 8. Lecciones (meta)

1. **Validar siempre datos contra BD antes de afirmar "no se genera X"**
   en informes a cliente. La tesis BUG-001 nació del análisis de código
   estático y se asumió cierta sin contraste contra trans_movimientos.
2. **Carolina (operación) vio en 5 minutos lo que el análisis técnico
   de 21 waves no vio**. Operaciones tiene visión de datos que el código
   solo no da.
3. **Confusión nominal `IdTransaccion` vs `IdMovimiento`** propagó la
   confusión: `trans_movimientos.IdTransaccion` apunta a
   `trans_ubic_hh_enc.IdTareaUbicacionEnc`, no es PK propia.

---

**Pendiente discusión con Erik**: ¿narrativa al cliente?, ¿prioridad de
los 2 bugs (identidad vs tareas huérfanas)?, ¿afecta el plan F0-F7 ya
cerrado?
