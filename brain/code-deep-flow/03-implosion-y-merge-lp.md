---
id: 03-implosion-y-merge-lp
tipo: code-deep-flow
estado: vigente
titulo: 03 - Implosión, Cambio de Ubicación y Merge LP (2028)
ramas: [dev_2023_estable, dev_2028_merge]
tags: [code-deep-flow]
---

# 03 - Implosión, Cambio de Ubicación y Merge LP (2028)

> **Wave 7 — Resolución de Q-LP-WHEN-DESTROYED + Q-LP-MERGE-EN-DESTINO + Q-CAPABILITY-FLAG**
> Fuentes: revelación verbal Erik 2026-04-29 + lectura `frmImplosion.vb`, `frmCambioUbicacion.vb` (2023 vs 2028), módulo HH `CambioUbicacion/`, descubrimiento `i_nav_config_enc`.
> Estado: HIPÓTESIS DOCUMENTADA con evidencia de código y schema. Pendiente confirmación end-to-end con Erik en 2-3 puntos abiertos al final.

---

## TL;DR (para Carolina)

1. **Implosión = "deshacer la explosión" sobre un LP**. En el WMS, el stock se "explota" al recibirse (un pallet completo se desglosa por nivel/posición). La implosión es lo inverso: tomar varios items y volverlos a consolidar en un LP unificado.
2. La implosión existe en TRES capas distintas, y a Erik le costó recordar cuál es cuál:
   - **BOF**: `frmImplosion.vb` (1332 líneas) — forma de oficina para consolidar stock reservado por pedido. **Existe pero suele estar oculta** salvo en clientes específicos (Cumbre).
   - **HH**: módulo `CambioUbicacion/` — forma móvil que en Cumbre se usa explícitamente como "implosión" cuando un producto A con LP `f(y)` se *coloca* sobre otro LP existente.
   - **Parámetro**: `i_nav_config_enc.implosion_automatica` (bit) — flag por bodega que decide si la implosión ocurre automáticamente al cambiar ubicación.
3. **2028 unifica los tres**: el "cambio de ubicación" pasa a contemplar nativamente que si la ubicación destino tiene un producto B con LP `f(z)`, entonces el LP origen `f(y)` deja de existir y todo el stock pasa a `f(z)`. El log queda en `trans_movimientos` con el rastro `f(y) → f(z)`.
4. **Esto resuelve la pregunta abierta hace meses**: *"¿cuándo deja de existir un LP?"*. Respuesta: **al ser implosionado/mergeado en otro LP destino**. El LP no se borra físicamente, queda histórico en `trans_movimientos.lic_plate` y `barra_pallet` para trazabilidad.

---

## Contexto: por qué esta traza importa

A lo largo de las Waves 1-6 quedaron varias preguntas abiertas:

| Q-* | Pregunta original | Resolución en esta wave |
|-----|-------------------|--------------------------|
| Q-LP-WHEN-DESTROYED | ¿Cuándo deja de existir un LP en el ciclo de vida WMS? | **Resuelta**: en la implosión (BOF/HH explícita) y en el cambio de ubicación 2028 cuando el destino ya tiene un LP existente con stock del mismo producto. |
| Q-LP-MERGE-EN-DESTINO | ¿Existe un proceso de "fusionar" LPs cuando coinciden en ubicación? | **Resuelta sí**: 2028 lo unifica. Pre-2028 era manual via `frmImplosion`. |
| Q-CAPABILITY-FLAG | ¿Hay una tabla maestra que decida qué capabilities tiene cada cliente? | **Resuelta**: es `i_nav_config_enc` (un registro por bodega/propietario). 50+ flags. |
| Q-NAV-MULTI-ERP | ¿El WMS solo se integra con NAV o también con SAP? | **Resuelta nueva**: soporta AMBOS (`crear_recepcion_de_compra_nav` + `interface_sap`). |

---

## Capa 1 — `frmImplosion.vb` (BOF, 1332 líneas)

### Ubicación
`TOMIMSV4/TOMIMSV4/Transacciones/Implosion/frmImplosion.vb`

### Identidad de tablas
```
clsBeVW_Stock_Res_Pedido      ← vista de stock reservado por pedido
clsBeVW_stock_res             ← vista de stock con reservas
clsBeStock                    ← entidad stock
clsBeTrans_movimientos        ← LOG de movimientos (la base de auditoría)
clsBeTrans_ubic_hh_enc        ← encabezado tarea HH cambio ubicación
clsBeTrans_ubic_hh_det        ← detalle tarea HH cambio ubicación
clsBeTrans_ubic_hh_op         ← operadores asignados a la tarea
clsBeTrans_ubic_tarima        ← tarimas (LPs) involucradas
```

### Funciones clave
```vb
Public Function ImplosionarStockSeleccionado(
    ByVal listaStockSeleccionado As List(Of clsBeVW_stock_res),
    ByVal IdUsuario As Integer,
    ByRef pListStockMov As List(Of clsBeStock)
)
    ' 1. Itera sobre items de stock seleccionados
    ' 2. Crea registros clsBeTrans_movimientos con:
    Dim mov As New clsBeTrans_movimientos()
    mov.Barra_pallet = pObjStock.Lic_plate    ' LP origen
    ' 3. Asocia los items a una tarea HH (clsBeTrans_ubic_hh_enc)
    ' 4. Persiste en BD via capa de negocio (clsLn*)
End Function

Public Sub ActualizarTablaStockTrasImplosion(...)
    ' Refresca grilla local tras consolidar
End Sub
```

### Source de datos al abrir
```vb
DT = clsLnStock.Get_Reporte_Stock_By_IdBodega_and_IdPropietario_For_Implosion(
    vIdBodega, vIdPropietarioBodega
)
```
→ Existe un SP/función nombrada explícitamente `*_For_Implosion` que filtra el stock candidato. **Pendiente buscar el SP backend** para entender el filtro (probablemente: stock reservado pero no consumido).

### Lo que NO está en frmImplosion 2028 vs 2023
**Diff `--stat` entre `dev_2023_estable` y `dev_2028_merge`** sobre carpeta `Implosion/`: **0 cambios**. El form sigue exactamente igual (1332 líneas en ambos branches). Esto valida que la implosión vieja (manual, BOF, escritorio) NO se tocó, sino que se complementó con el flujo nuevo en `Cambio_Ubicacion_Estado/` (capa 3 abajo).

### Hipótesis: por qué Erik dijo "no existe en BOF"
Erik trabaja primariamente con clientes nuevos (CEALSA, MAMPA, BECO) donde el menú BOF tiene este form **oculto o deshabilitado** vía permisos de rol (`OpcionesMenu`). Solo en Cumbre (cliente legacy) está visible. **Pendiente confirmar con Erik**.

---

## Capa 2 — Módulo HH `CambioUbicacion/`

### Estructura del módulo (Java/Android, paquete `com.dts.tom.Transacciones.CambioUbicacion`)

| Archivo | Líneas | Rol |
|---------|--------|-----|
| `frm_cambio_ubicacion_ciega.java` | **5334** | Cambio de ubicación SIN guía del sistema (operador escanea LP origen → destino libremente). El más complejo. |
| `frm_cambio_ubicacion_dirigida.java` | 1075 | Cambio guiado por sistema (BOF crea la tarea, HH la ejecuta). |
| `frm_detalle_cambio_ubicacion.java` | 699 | Vista de detalle de una tarea de cambio. |
| `frm_tareas_cambio_ubicacion.java` | — | Listado de tareas pendientes asignadas al operador. |

### Modelos de datos (paquete `com.dts.classes.Transacciones.CambioUbicacion`)
```
clsBeMotivo_ubicacion                ← catálogo de motivos del cambio (ej: "implosión", "consolidación", "movimiento operativo")
clsBeTrans_ubic_hh_enc               ← encabezado tarea (compartido con BOF capa 1)
clsBeTrans_ubic_hh_det               ← detalle (mismo)
clsBeDetalleCambioUbicacion          ← entidad UI con cantidad, ubicación, LP
```

### Patrón Cumbre (donde "implosión" es explícita en HH)
1. Operador escanea producto A en LP origen `f(y)`
2. Sistema le pide ubicación destino
3. Operador escanea destino que ya contiene producto B (mismo IdProducto) con LP `f(z)`
4. Sistema detecta colisión y ofrece: **"¿implosionar f(y) sobre f(z)?"**
5. Si confirma: el stock de A pasa físicamente a `f(z)` y `f(y)` queda histórico
6. Se inserta movimiento en `trans_movimientos` con `lic_plate = f(z)`, `barra_pallet = f(y)` (o un par origen/destino — pendiente verificar el patrón exacto)

### Pre-2028: implosión = motivo manual
En 2023, el operador tenía que **elegir manualmente el `IdMotivoUbicacion = "Implosión"`** desde un combo. El sistema no lo proponía automáticamente.

---

## Capa 3 — Cambio de Ubicación 2028 (la unificación)

### Diff entre 2023 y 2028 (carpeta `Cambio_Ubicacion_Estado/`)
```
frmCambioUbicacion.Designer.vb            |  92 +/-
frmCambioUbicacion.resx                   | 345 +/-
frmCambioUbicacion.vb                     | 669 +/-  ← +303% en lógica
frmCambioUbicacion_List.Designer.vb       |   8 +/-
frmCambioUbicacion_List.resx              | 147 +/-
frmCambioUbicacion_List.vb                |  32 +/-
frmCantidadUbicacion.vb                   |  42 +/-
frmControlUbicaciones.vb                  |  16 +/-
frmResultadoValidacionCambioUbic.Designer.vb  | 142 +  (NUEVO)
frmResultadoValidacionCambioUbic.resx     | 154 +    (NUEVO)
frmResultadoValidacionCambioUbic.vb       | 120 +    (NUEVO)
TOTAL: 11 archivos, +1029 / -738
```

### Qué cambia en 2028
1. **`frmCambioUbicacion.vb` pasa de 1332 a 2837 líneas** (+113%)
2. **Nueva forma `frmResultadoValidacionCambioUbic.vb`** (120 líneas):
   - Recibe `BeListaErrores` (lista de errores de validación pre-cambio)
   - Cols: `IdStock`, `CodigoProducto`, `NombreProducto`, `IdUbicacionOrigen`, `IdUbicacionDestino`, `Motivo`
   - **Patrón nuevo**: validar TODO el batch antes de ejecutar, mostrar errores en grilla, permitir corregir/seguir solo con los válidos.
3. **Validación pre-cambio en bloque** (no item-por-item como 2023)
4. **Lógica de detección de LP existente en destino IMPLÍCITA** en `lnkUbicacionDestino_LinkClicked` (línea 1479) y `cmdGuardar_Click` (línea 1023). NO usa palabras "implosi/merge/consolidar" — la lógica está en el `Stock.pObjStock.Lic_plate` que se compara.

### Funciones nuevas o modificadas en `frmCambioUbicacion.vb` 2028
```vb
Public Sub Verifica_Permiso_Ubicacion_Sin_HH()    ' línea 61   ← NUEVA: permiso para mover sin pasar por HH
Private Sub InicializarFormulario()                ' línea 140
Private Sub Validar_Operadores()                   ' línea 315
Private Function Guardar() As Boolean              ' línea 340  ← refactor masivo
Private Function Actualizar() As Boolean           ' línea 463
Private Sub Cargar_Detalle(ByVal pGuardo As Boolean)  ' línea 483
Private Sub cmdGuardar_Click(...)                  ' línea 1023 ← AQUÍ está el merge LP
Private Sub lnkUbicacionDestino_LinkClicked(...)   ' línea 1479 ← AQUÍ se detecta LP en destino
Private Sub Asignar_Operador_Seleccion_Multiple()  ' línea 1676 ← NUEVA: multi-LP
```

### Patrón f(y) → f(z) en `trans_movimientos`
**Cols disponibles** (35 totales):
- `lic_plate` (nvarchar 100) — UN solo LP por registro
- `barra_pallet` (nvarchar 100) — código de barra del pallet (alias del LP en muchos casos)
- `IdUbicacionOrigen` / `IdUbicacionDestino` — separados, OK
- `IdEstadoOrigen` / `IdEstadoDestino` — separados (ej: APROBADO → CUARENTENA)
- `cantidad_hist` / `peso_hist` — para before-image

**NO existen** `lic_plate_origen` ni `lic_plate_destino` separados. **Hipótesis del log f(y,z)**:
- (a) Inserta DOS registros: uno con `lic_plate = f(y)` (salida del LP origen) y otro con `lic_plate = f(z)` (entrada al LP destino), ambos con mismo `IdTransaccion` o agrupados por timestamp/operador.
- (b) Inserta UN registro con `lic_plate = f(z)` (destino) y `barra_pallet = f(y)` (origen) usando los dos campos como par.
- (c) Existe una tabla auxiliar de bitácora (no detectada aún en schema discovery).

**Pendiente verificar** consultando `trans_movimientos` real en MERCOPAN (que tiene 323374 movimientos, masa suficiente para detectar el patrón). **Q-* nueva**: `Q-MERGE-LP-LOG-PATRON` (ver cuestionario Carolina).

---

## Capa 4 — `i_nav_config_enc` como tabla maestra de capabilities

### Hallazgo
**Resuelve Q-CAPABILITY-FLAG** (abierta desde Wave 4): la tabla `i_nav_config_enc` es donde TODO se decide. Un registro por (IdBodega, IdPropietario), con 50+ flags booleanos.

### Flags relacionados con LP / implosión / explosión
| Flag | Tipo | MERHONSA | MERCOPAN | CEALSA | MAMPA | Significado |
|------|------|----------|----------|--------|-------|-------------|
| `genera_lp` | bit | False | True | True | True | La bodega genera LPs nativos vs heredarlos del ERP |
| `implosion_automatica` | bit | True | False | False | False | Si el cambio de ubicación implosiona automáticamente |
| `explosion_automatica` | bit | True | False | False | False | Si la recepción explota automáticamente por nivel/posición |
| `explosion_automatica_desde_ubicacion_picking` | bit | True | None | False | False | Si la explosión también ocurre al sacar de picking |
| `explosion_automatica_nivel_max` | int | -1 | None | -1 | -1 | -1 = todos los niveles, N = hasta el N |
| `Ejecutar_En_Despacho_Automaticamente` | bit | False | False | True | True | Auto-ejecutar despacho al confirmar pedido |

→ **MERHONSA es el ÚNICO con `implosion_automatica=True`** entre los 4 muestreados. Esto explica por qué Erik recordó la implosión "en Cumbre via HH" — Cumbre y MERHONSA siguen el patrón clásico, los demás clientes lo desactivaron.

### Flags de control de producto
| Flag | Tipo | Significado |
|------|------|-------------|
| `control_lote` | bit | Por bodega: si se exige captura de lote en stock |
| `control_vencimiento` | bit | Por bodega: si se exige fecha de vencimiento |
| `control_peso` | bit | Por bodega: si se controla peso variable |
| `dias_vida_defecto_perecederos` | int | Días de vida útil DEFAULT para perecederos sin fecha |

→ Esto **destruye la asunción** de que `control_lote` y `control_vencimiento` son tablas separadas. Son **flags de bodega**, no entidades. Las "trazas" anteriores que decían "validar contra control_lote" estaban mal apuntadas.

### Flags de integraciones ERP (HALLAZGO BRUTAL)
| Flag | Tipo | Significado |
|------|------|-------------|
| `crear_recepcion_de_compra_nav` | bit | Push recepción a Microsoft Dynamics NAV |
| `crear_recepcion_de_transferencia_nav` | bit | Push transferencia a NAV |
| `push_ingreso_nav_desde_hh` | bit | El ingreso desde HH dispara push NAV |
| `interface_sap` | bit | **TIENE INTEGRACIÓN SAP** (no solo NAV) |
| `sap_control_draft_ajustes` | bit | SAP en modo draft para ajustes |
| `sap_control_draft_traslados` | bit | SAP en modo draft para traslados |
| `inferir_bonificacion_pedido_sap` | bit | Detección automática bonificaciones desde SAP |
| `rechazar_bonificacion_incompleta` | bit | Política bonus |

→ **El WMS soporta NAV y SAP simultáneamente** como ERPs. La parametrización dice qué interface usa cada bodega. Esto era completamente desconocido antes de Wave 7.

### Flags de despacho/pedidos
| Flag | Significado |
|------|-------------|
| `rechazar_pedido_incompleto` | Política: ¿se permite despachar pedido con faltantes? |
| `despachar_existencia_parcial` | Política: ¿se permite cubrir parcial? |
| `equiparar_cliente_con_propietario_en_doc_salida` | Mapping cliente↔propietario en doc fiscal |
| `reservar_umbas_primero` | UMB = unidad mínima básica? Reserva primero por UMB |
| `convertir_decimales_a_umbas` | Conversión automática decimales → UMB |
| `excluir_ubicaciones_reabasto` | Reabasto excluye ciertas ubicaciones |
| `considerar_paletizado_en_reabasto` | Tomar pallet completo en reabasto |
| `considerar_disponibilidad_ubicacion_reabasto` | Validar capacidad destino antes de reabastecer |
| `conservar_zona_picking_clavaud` | "Clavaud" parece ser proveedor o método? — pendiente investigar |

### Flags de NC (notas crédito) y producto
| Flag | Significado |
|------|-------------|
| `codigo_bodega_erp_nc` | Código de bodega en ERP para devoluciones/NC |
| `lote_defecto_entrada_nc` | Lote default para entradas via NC |
| `vence_defecto_nc` | Fecha vencimiento default para NC |
| `IdProductoEstado_NC` | Estado de producto al ingresar por NC (ej: cuarentena) |
| `equiparar_productos` | Mapping multi-código de producto |

---

## Síntesis: cómo termina existiendo (o no) un LP

**Ciclo de vida completo del LP, integrando todas las waves**:

```
1. CREACIÓN (cobertura: traza-001)
   └─ Recepción → SP genera LP en NAV (correlativo o random según cliente)
       └─ trans_movimientos.lic_plate = f(x)

2. VIDA ÚTIL (LP intacto)
   └─ Cambio ubicación SIN colisión → f(x) viaja con su stock
   └─ Cambio estado (APROBADO/CUARENTENA/RECHAZADO) → f(x) sigue siendo f(x)
   └─ Conteos cíclicos → ajustan cantidad pero no LP

3. MUERTE DEL LP — 5 caminos posibles (Wave 7)
   ├─ (a) Despacho COMPLETO del stock asociado → cantidad=0, LP queda histórico
   ├─ (b) Implosión BOF (frmImplosion) → consolidación manual de varios LPs
   ├─ (c) Implosión HH Cumbre (cambio_ubicacion + IdMotivo='Implosión')
   ├─ (d) Cambio ubicación 2028 con LP destino preexistente → MERGE auto si i_nav_config_enc.implosion_automatica=True
   └─ (e) Anulación de recepción → LP nunca se "consume", queda zombie con cantidad=0

4. PERSISTENCIA HISTÓRICA
   └─ El LP NUNCA se borra. Queda en trans_movimientos.lic_plate y .barra_pallet
   └─ Para ver "LPs activos" hay que JOIN con stock WHERE cantidad > 0
```

---

## Q-* abiertas tras esta wave

### Críticas
- **Q-MERGE-LP-LOG-PATRON**: ¿el log f(y)→f(z) en `trans_movimientos` usa 1 fila con dos campos, 2 filas pareadas, o hay tabla auxiliar? (Ver hipótesis a/b/c arriba)
- **Q-IMPLOSION-BOF-VISIBILIDAD**: ¿`frmImplosion.vb` está habilitado solo en Cumbre via permisos de rol, o realmente en todos los clientes y nadie lo usa?
- **Q-CLAVAUD-MEANING**: ¿qué es "Clavaud" en `conservar_zona_picking_clavaud`? ¿Persona, método, proveedor?

### De segundo orden
- **Q-SAP-CLIENTES**: ¿qué clientes tienen `interface_sap=True`? ¿Coexiste con NAV en la misma bodega?
- **Q-UMB-CONCEPT**: ¿UMB = "unidad mínima básica"? ¿O es otra cosa?
- **Q-LP-ZOMBIE**: si despacho completo deja LP con cantidad=0, ¿hay un job de purga periódica?

### Resueltas en Wave 7
- ~~Q-LP-WHEN-DESTROYED~~ → 5 caminos identificados arriba
- ~~Q-LP-MERGE-EN-DESTINO~~ → 2028 lo unifica via flag `implosion_automatica`
- ~~Q-CAPABILITY-FLAG~~ → es `i_nav_config_enc` con 50+ flags
- ~~Q-CONTROL-LOTE-TABLA~~ → no es tabla, es FLAG en `i_nav_config_enc`

---

## Archivos correlacionados en el brain
- `traza-001-license-plate.md` (Wave 6) — vida útil del LP, complementa esta wave
- `02-portal-y-dms.md` (Wave 6) — historia portales, donde aparece el cambio 2028
- `RAMAS_Y_CLIENTES.md` (Wave 6) — qué cliente está en qué branch
- `HOLDING_IDEALSA.md` (Wave 7) — contexto MERHONSA + MERCOPAN
- `i_nav_config_enc-MAPA-DE-FLAGS.md` (Wave 7) — referencia completa de flags
- `CUESTIONARIO_CAROLINA.md` — bloque 11 con las Q-* nuevas

---

## Apéndice: comandos de validación reproducibles

```bash
# Sobre /tmp/repos/TOMWMS_BOF
git show 'origin/dev_2028_merge:TOMIMSV4/TOMIMSV4/Transacciones/Implosion/frmImplosion.vb' | wc -l
git diff --stat origin/dev_2023_estable..origin/dev_2028_merge -- TOMIMSV4/TOMIMSV4/Transacciones/

# Sobre EC2 SQL Server
SELECT name, implosion_automatica, explosion_automatica, genera_lp
FROM IMS4MB_MERHONSA_PRD.dbo.i_nav_config_enc;

SELECT TOP 20 IdMovimiento, IdUbicacionOrigen, IdUbicacionDestino, lic_plate, barra_pallet, cantidad, cantidad_hist
FROM IMS4MB_MERCOPAN_PRD.dbo.trans_movimientos WITH (NOLOCK)
WHERE IdUbicacionOrigen <> IdUbicacionDestino
ORDER BY fecha DESC;
```
