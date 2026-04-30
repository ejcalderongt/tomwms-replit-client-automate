---
id: traza-002
tipo: trace
relacionado_con: [BUG-001, CP-013]
materializa_bug: BUG-001
ramas: [dev_2023_estable, dev_2028_merge]
modulo: trans_picking_ubic
archivos_analizados: 10
tags: [trace/picking, modulo/bof-vbnet, bug/critico]
---
# Traza 002 — Dañado_picking (flag huérfano que sangra stock)

**Parámetro fuente**: `trans_picking_ubic.dañado_picking` (bit, NULL, default NULL)

**Por qué importa**: es el flag que el operador (HH) o backoffice (BOF) prende cuando una línea de picking tiene mercadería dañada. **El sistema lo usa SOLO como filtro de "no contar para totales", pero NUNCA genera el movimiento de stock que compense la pérdida**. Resultado: stock contable que el sistema cree disponible pero físicamente no está. En Killios productivo: **6.500 / 26.567 (24%) de líneas marcadas, ~320.000 UM fantasma**.

**Caso ligado**: `brain/debuged-cases/CP-015-bug-danado-picking-transversal/` (síntoma transversal multi-cliente) y `brain/debuged-cases/CP-013-killios-wms164/PLAYBOOK-FIX.md` (fix propuesto, no aplicado).

**Fecha trace**: 2026-04-30. **Agente**: Replit. **Rama BOF base**: `dev_2023_estable` (productiva 4 clientes incluyendo Killios) con diff vs `dev_2028_merge` (MAMPA QA, donde se intentó un fix parcial nunca terminado).

---

## 0. Resumen ejecutivo

### 0.1 Hallazgo central en una línea

El flag `dañado_picking` se setea desde 3 puntos de código (HH cambio de estado, BOF reemplazo de ubicación, BOF stock reservado), participa en **>40 SELECTs** que filtran "no contar dañados" para totales, y **CERO** SPs / triggers / código BOF generan el descuento de stock compensatorio. La pérdida es silenciosa y acumulativa.

### 0.2 Hilos afectados (matriz cross-capa)

| Hilo | Archivo | Setters de Dañado=True | SELECTs con Dañado=0 | UPDATE de stock | Diagnóstico |
|---|---|:-:|:-:|:-:|---|
| **BOF DAL** | `clsLnTrans_ubic_hh_enc_Partial.vb` | 5 | 0 | NO | Flujo CEST (cambio de estado), origen principal del bug |
| **BOF DAL** | `clsLnStock_res_Partial.vb` | 4 (en 2023, 2 en 2028) | 0 | NO | Reemplazo en stock reservado, segundo origen |
| **BOF DAL** | `clsLnTrans_picking_ubic_Partial.vb` | 0 | 5 (2023), 1 (2028) | NO | Lectura, filtra dañados de operación pendiente |
| **BOF DAL** | `clsLnTrans_pe_det_Partial.vb` | 0 | 3 | NO | Cálculo de total entregado al pedido |
| **BOF DAL** | `clsLnTrans_picking_enc_Partial.vb` | 0 | 4 | NO | Header de picking |
| **BOF DAL** | `clsLnTrans_picking_det_Partial.vb` | 0 | 1 | NO | Detalle de picking |
| **BOF DAL** | `clsLnTarea_hh_Partial.vb` | 0 | 1 (2023), 2 (2028) | NO | Asignación de tareas HH |
| **BOF DAL** | `clsLnTrans_despacho_det_Partial.vb` | 0 | 1 | NO | Despacho (lectura) |
| **BOF Entity** | `clsBeTrans_picking_ubic.vb` | n/a | n/a | n/a | Property pública, mapping ORM |
| **HH Java** | `clsBeTrans_picking_ubic.java` | n/a | n/a | n/a | Property Java `Danado_picking` (sin Ñ por transliteración SOAP) |
| **DB Killios** | 2 tablas + 18 vistas + 2 funciones escalares | n/a | varias | NO | Toda la lógica de "no contar dañado" en views, sin compensación |
| **DB Killios** | trans_picking_ubic | TRIGGERS=0, SPs=0 | n/a | NO | El flag no dispara nada en BD |
| **WMSWebAPI** | (REST nuevo .NET) | 0 | 0 | NO | NO MIGRADO, sigue todo en SOAP |
| **WSHHRN SOAP** | `Update_BD_WMS.sql` | 1 (ALTER TABLE ADD) | 0 | NO | Solo definición DDL del campo |

### 0.3 Variantes por empresa (ejecutado en BD productivas via audit-bug-danado-multi-bd.py)

| Cliente | Rama productiva | dañado_picking=1 (filas) | UM perdidas | Estado bug |
|---|---|---:|---:|---|
| **KILLIOS** | dev_2023_estable | 6.500 / 26.567 (24%) | ~320.000 | **CRITICO** |
| **MERCOPAN** | dev_2023_estable | 19.598 | 574.155 | **MAS GRAVE en volumen** |
| **BYB** | dev_2023_estable | confirmado | (ver CP-015) | bug + 21% via HH (outlier) |
| **BECOFARMA** | dev_2023_estable | 0 | 0 | NO usa la feature (cero registros) |
| **CEALSA** | dev_2023_estable | 0 | 0 | NO usa la feature |
| **MAMPA** | dev_2028_merge | 0 | 0 | NO usa la feature (QA, sin operación real) |

### 0.4 La hoja de ruta del bug (estado al 2026-04-30)

```
2022-01-10  '#AT' agrega Dañado_picking=True en cambio de estado HH
            (clsLnTrans_ubic_hh_enc_Partial.vb linea 1394-1396)
            -> arranca la sangre

2022-12-05  '#CKFK20221205' agrega IdPickingEnc a queries de filtrado
            (clsLnTrans_picking_ubic_Partial.vb)
            -> consolida el patron "filtrar dañados sin compensar"

2025-05-28  Killios: primer registro de duplicado de stock (931 dias atras)
            (CP-013 INFORME-EJECUTIVO.md seccion 2)

2025-11-29  Killios: pico historico, 210 filas duplicadas en un solo dia
            (CP-013 INFORME-EJECUTIVO.md seccion 2)

2026-04-23  Erik abre WMS164 a partir de queja del operario
            (CP-013-killios-wms164/REPORTE.md)

2026-04-29  CP-013 confirma 919 filas duplicadas, 18.7% del stock activo
            INFORME-EJECUTIVO.md y EVIDENCIAS-CRONICIDAD.md

2026-04-30  CP-015 (este caso) cruza datos multi-BD, 4/7 BDs afectadas
            REPORTE-MULTI-BD.md, DATOS-COMPARATIVOS.md

2026-04-30  Esta traza confirma:
            - Codigo en 2028 tiene un fix PARCIAL comentado (lineas comentadas
              en clsLnStock_res_Partial.vb 1998-2008 SOLO en 2028)
            - 2028 elimino 4 queries de filtrado en clsLnTrans_picking_ubic_Partial.vb
            - Killios PRD corre 2023 -> sigue sangrando hoy
            - El fix completo NUNCA fue implementado; esta propuesto en
              CP-013/PLAYBOOK-FIX.md (incluye §G guia de codigo y §H
              estrategia de ramas: 2028 primero, hotfix a 2023 solo si
              Killios no aguanta esperar al release de 2028)
```

---

## 1. Hilo BOF (TOMWMS_BOF, VB.NET) — el corazón del bug

Path Azure DevOps: `https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF`

### 1.1 Setters del flag (donde el sistema marca dañado=TRUE)

#### A. `clsLnTrans_ubic_hh_enc_Partial.vb` — flujo CEST (cambio de estado handheld)

Cinco bloques `If EsPicking Then bePickingUbicExistenteDañado.Dañado_picking = True`. Líneas (rama 2023):
- L695-696, L724-725, L766-767, L1039-1040, L1068-1069
- L1394-1396 (línea histórica con comentario `'#AT 20220110 Se cambio del valor a true`)

Patrón de cada bloque:

```vbnet
If EsPicking Then
    bePickingUbicExistenteDañado.Dañado_picking = True
Else
    bePickingUbicExistenteDañado.Dañado_verificacion = True
End If
' (...continua creando o updateando trans_picking_ubic SIN tocar stock...)
```

**Diagnóstico**: el `EsPicking` viene del contexto del cambio de estado. Cuando el operador HH marca un producto como dañado durante picking, la línea de `trans_picking_ubic` se actualiza con `Dañado_picking = True`. Inmediatamente después se llama a `clsLnBodega.Get_IdMotivoUbicacion_Dañado_Picking(IdBodega)` para resolver una ubicación de motivo (línea 785, 1134, 1408 en 2023). **Pero esa ubicación se usa solo para el header `clsLnTrans_ubic_hh_enc`, no para mover stock**.

#### B. `clsLnStock_res_Partial.vb` — reemplazo en stock reservado

Cuatro setters en rama 2023 (líneas 2266, 2308) y dos en rama 2028 (más dos comentados con `'`).

Diff crítico 2023 vs 2028:

```diff
  rama dev_2023_estable (PRODUCCION):                    rama dev_2028_merge (QA):
  L1986: bePickingUbicExistente.Dañado_picking = False   L1998: '   If EsPicking Then  ← COMENTADO
                                                          L1999: '       bePickingUbicExistente.Dañado_picking = True
                                                          L2000: '   Else
                                                          L2007: '   End If
                                                          L2008: '       bePickingUbicExistente.Dañado_picking = False
```

**Diagnóstico**: en algún momento del 2025-2026, alguien (probablemente en el contexto del descubrimiento del bug) **intentó deshabilitar comentando** un setter de `Dañado_picking = True` en la rama 2028. La intervención está incompleta: NO agregó la lógica de descuento de stock; solo bloqueó un punto de marcaje. Y la rama 2023 (productiva) nunca recibió ni siquiera este parche parcial.

### 1.2 Lectores del flag (filtros "no contar dañados")

#### `clsLnTrans_pe_det_Partial.vb` — total entregado al pedido

Tres queries del tipo:

```sql
SELECT ...
FROM stock_res Res
JOIN trans_picking_ubic ON ...
WHERE Res.IdPedido = @IdPedido
  AND ISNULL(trans_picking_ubic.dañado_verificacion, 0) = 0
  AND ISNULL(trans_picking_ubic.dañado_picking, 0) = 0
  AND ISNULL(trans_picking_ubic.no_encontrado, 0) = 0
```

**Diagnóstico**: el "total entregado" del pedido **excluye** las líneas dañadas. Combinado con la falta de descuento de stock origen, esto produce: pedido cerrado con menos cantidad entregada de la solicitada (correcto contablemente para el cliente final), pero stock origen sin moverse (incorrecto: el WMS sigue creyendo que tiene esa mercadería disponible para reservar de nuevo).

#### `clsLnTrans_picking_ubic_Partial.vb` — operaciones pendientes

Cinco queries en rama 2023 (líneas 1067, 1170, 1249, 1370, 1448, 1527, 1655) tipo:

```sql
SELECT * FROM VW_PickingUbic_By_IdPickingDet
WHERE dañado_picking = 0
  AND cantidad_solicitada <> cantidad_recibida
```

**En rama 2028 estos queries fueron ELIMINADOS** (4 desaparecieron del archivo). Sin saber cuál es el reemplazo, parece un refactor de la lógica de "líneas pendientes a operar" donde el filtro se movió a otro lado. Es divergencia de lógica entre las dos ramas.

### 1.3 Mapa de llamadas (call graph reducido)

```
HH operador marca dañado en pantalla picking
   |
   v
SOAP TOMHHWS.asmx.vb -> Set_Trans_picking_ubic_Activa(...)  [no descargado, hipotesis]
   |
   v
BOF clsLnTrans_picking_ubic_Partial.Update(... Dañado_picking ...)
   |
   v (caso CEST)
clsLnTrans_ubic_hh_enc_Partial.vb (5 puntos: L695, L724, L766, L1039, L1068)
   |  Setea bePickingUbic.Dañado_picking = True
   |  Llama Get_IdMotivoUbicacion_Dañado_Picking
   |  Crea trans_ubic_hh_enc header
   X  NO crea trans_movimientos
   X  NO ajusta producto_bodega_stock

BOF backoffice marca dañado en grilla picking
   |
   v
clsLnStock_res_Partial.vb (4 puntos rama 2023 / 2 puntos rama 2028)
   |  Setea bePickingUbicExistente.Dañado_picking = True
   X  NO crea trans_movimientos
   X  NO ajusta stock
```

### 1.4 Forms del BOF que llaman estos DAL

Pendiente de descarga: la API de search no devolvió matches por `Danado_picking` capitalizado en BOF. Hipótesis razonable: los Forms (`frmPickingDetalle.vb`, `frmPickingDanado.vb` o equivalente) llaman al DAL via property `Dañado_picking` con Ñ, igual que el resto. **Cola C-006**: descargar lista completa de Forms de TOMIMSV4 que usan `clsBeTrans_picking_ubic` para localizar UI exacta.

---

## 2. Hilo HH (TOMHH2025, Java Android)

Path Azure DevOps: `https://dev.azure.com/ejcalderon0892/TOMHH2025/_git/TOMHH2025`

### 2.1 Entity Java

**Archivo**: `app/src/main/java/com/dts/classes/Transacciones/Picking/clsBeTrans_picking_ubic.java`

Property: `public boolean Danado_picking = false` (línea 41 — **sin Ñ por la transliteración del WSDL SOAP**, el WSDL lo expone como `Danado_picking`).

Getter/setter: líneas 354-358. Constructor full-args: línea 88, parámetro #16.

### 2.2 Activity / Frm que dispara el set

Pendiente de localizar exactamente qué `frm_*.java` setea el flag. La búsqueda Azure DevOps dio solo 1 hit (la entity). Hipótesis: en `frm_picking_*.java` o `frm_dañado_*.java` el operador toca un botón "Marcar dañado" y se llama a `setDanado_picking(true)` antes de hacer el `webService.execws(...)` correspondiente. **Cola C-007**: descargar `frm_picking_*.java` y buscar referencias.

### 2.3 SOAP transport (cómo viaja BOF <-> HH)

El WSDL `WSHHRN/WMS.xml` y `TOMIMSV4/Service References/wsTOMHH/Reference.vb` declaran el campo `Danado_picking` en el contrato SOAP. La serialización XML mapea automáticamente:

```
HH Java:           Danado_picking (boolean)
SOAP XML:          <Danado_picking>true</Danado_picking>
WSHHRN VB.NET:     Danado_picking (Boolean property en proxy)
BOF DAL:           bePickingUbic.Dañado_picking (Boolean property con Ñ en VB.NET)
DB:                trans_picking_ubic.dañado_picking (bit con Ñ)
```

El mapeo Ñ ↔ N lo resuelve el ORM por convención de nombres (no hay attribute `<XmlElement Name="dañado_picking">` declarado).

---

## 3. Hilo DB (Killios productiva)

Server: `52.41.114.122,1437` DB: `TOMWMS_KILLIOS_PRD`

### 3.1 Estructura de la columna

```sql
trans_picking_ubic
  ...
  pos 27: dañado_verificacion  bit  NULL  default ((0))
  ...
  pos 41: dañado_picking       bit  NULL  default NULL    ← anomalia
  ...
```

**Anomalía detectada**: `dañado_picking` tiene `default NULL`, mientras que `dañado_verificacion` (pos 27) tiene `default ((0))`. El campo se agregó tarde (posición alta 41 vs 27 del hermano) y la migración no replicó el default. Esto explica además que en queries con `ISNULL(...,0)` aparezca el patrón defensivo: el código sabe que el valor puede venir NULL.

### 3.2 Triggers

```
trans_picking_ubic.triggers = 0
```

**Confirmado**: cero triggers. La BD no compensa nada por su cuenta.

### 3.3 SPs / functions / views

```
SQL_STORED_PROCEDURE: 0  ← cero
SQL_TRIGGER: 0  ← cero
SQL_TABLE_VALUED_FUNCTION: 0  ← cero
SQL_SCALAR_FUNCTION: 2 (Get_Porcentaje_Avance_Pedido, Get_Porcentaje_Avance_Picking) — solo lectura
VIEW: 16 (todas filtran o exponen el campo, ninguna escribe)
```

**Confirmado**: la columna no se modifica desde ningún SP. Toda la escritura viene del código BOF / HH.

### 3.4 Constraint relacionado al bug

Según la hipótesis A del INFORME-EJECUTIVO de CP-013:

> *"La base de datos tiene una regla de seguridad que prohíbe que una fila de stock quede en cantidad cero (Cantidad > 0). Si el código del HH hace un UPDATE que dejaría la fila en cero o negativo, ese UPDATE falla. Si el código tiene un try/catch que ante el error inserta una fila nueva en lugar de manejarlo bien, eso explica el patrón."*

**Pendiente verificar** (cola C-008): identificar el constraint `CHECK (Cantidad > 0)` (o similar) en la tabla `producto_bodega_stock` o `stock` y mapear el try/catch en el DAL que lo captura.

### 3.5 Conteos en vivo Killios (2026-04-30)

```
trans_picking_ubic total rows: 26.567
trans_picking_ubic donde dañado_picking=1: 6.500 (24%)
```

---

## 4. Hilo WMSWebAPI (REST .NET nuevo)

**Estado**: cero migración del flag. Búsqueda Azure DevOps cuenta:

```
WMS.EntityCore/Picking/clsBeTrans_picking_ubic.cs   ← entity nueva, hereda el campo
WMS.EntityCore/Dtos/Picking/PickingUbicDto.cs       ← DTO REST
WMSPortal/Library/Database/ApplicationDbContext.cs  ← EF Core mapping
WMS.DALCore/Picking/clsLnTrans_picking_ubic.cs      ← DAL Core nuevo
```

Estos 4 archivos sí incluyen el campo en su modelo, pero **ningún Controller / Service del REST lo expone como endpoint operativo**. El picking sigue 100% via SOAP/WSHHRN.

**Implicación para WebAPI .NET 10 nuevo (que viene)**: el modelo de servicios debe incluir explícitamente la lógica de descuento de stock cuando se prende `dañado_picking`. Es el invariante crítico que la base actual no garantiza.

---

## 5. Hilo ERP externo (SAP B1 / NAV / etc.)

`dañado_picking` **no se sincroniza al ERP**. Es un flag interno del WMS. El ERP recibe el resultado neto del despacho (cantidad realmente entregada al cliente), no el detalle de qué se dañó internamente. Esto es coherente: si el bug se arregla, **no hay impacto en el contrato ERP**.

---

## 6. Variantes por empresa

### KILLIOS (rama dev_2023_estable, BD TOMWMS_KILLIOS_PRD)
- Bug activo: 24% de líneas marcadas, ~320k UM fantasma.
- Empezó 2025-05-28, pico 2025-11-29.
- Operador descubre síntoma 2026-04-23 (caso WMS164).

### MERCOPAN (rama dev_2023_estable)
- 19.598 líneas afectadas, **574.155 UM perdidas** — el más grave en volumen absoluto.
- Pendiente verificar si Mercopan sigue activo / qué backup tiene.

### BYB (rama dev_2023_estable, BD IMS4MB_BYB_PRD)
- Bug activo, con outlier: 21% de las marcaciones vienen del HH (vs 1-3% en otros). Implica que el operador en BYB usa más la marca en HH que el backoffice. **Cola C-009**: investigar por qué BYB tiene esa proporción.

### BECOFARMA (rama dev_2023_estable, BD IMS4MB_BECOFARMA_PRD)
- 0 registros con flag=1. **NO usa la feature.**

### CEALSA (rama dev_2023_estable, BD IMS4MB_CEALSA_QAS)
- 0 registros con flag=1. **NO usa la feature.**

### MAMPA (rama dev_2028_merge, BD TOMWMS_MAMPA_QA)
- 0 registros con flag=1. QA, sin operación real todavía.
- **Pero** la rama tiene el fix parcial comentado. Esto sugiere que el equipo intentaba probar el fix en MAMPA antes de propagar a 2023, pero no terminó la implementación.

---

## 7. Hipótesis Q-* abiertas

- **Q-DANADO-FORM-BOF**: identificar el Form / pantalla del BOF que dispara `clsLnStock_res_Partial.UpdateXxx_Dañado` (cola C-006).
- **Q-DANADO-FRM-HH**: identificar el `frm_*.java` que setea `Danado_picking=true` antes del SOAP send (cola C-007).
- **Q-CONSTRAINT-CANTIDAD**: confirmar el constraint `CHECK (Cantidad > 0)` o equivalente, y mapear el try/catch que lo captura (cola C-008).
- **Q-BYB-HH-OUTLIER**: por qué BYB tiene 21% de marcaciones via HH vs 1-3% del resto (cola C-009).
- **Q-FIX-PARCIAL-2028**: quién comentó las líneas en `clsLnStock_res_Partial.vb` rama 2028, en qué commit, con qué intención (cola C-010 — requiere git log/blame en Azure DevOps).
- **Q-MIGRACION-DEFAULT**: por qué la migración que agregó `dañado_picking` no puso default 0 como en `dañado_verificacion`, y si vale la pena ALTER para igualar (cola C-011).

---

## 8. Lecturas para WebAPI .NET 10 nuevo

El servicio que reemplace a WSHHRN debe garantizar como invariante:

```
Si Trans_Picking_Ubic.Dañado_picking pasa de False a True:
  -> generar Trans_Movimientos {
        IdTipoTarea: 8 (PIK) o 17 (AJCANTN) según destino seleccionado,
        Origen: ubicación de picking origen,
        Destino: ubicación MERMA configurada para la bodega,
        Cantidad: cantidad_solicitada - cantidad_verificada (la diferencia dañada),
        Motivo + Observación: obligatorios desde input del operador
     }
  -> ejecutar UPDATE producto_bodega_stock origen restando cantidad
  -> ejecutar UPDATE/INSERT producto_bodega_stock destino sumando cantidad
  -> registrar fila en trans_picking_danado_log (tabla nueva propuesta en PLAYBOOK-FIX)
```

Si la bodega **no tiene** ubicación MERMA configurada (`bodega.IdMotivoUbicacionDañadoPicking = NULL`), el servicio debe **rechazar** la operación con error explícito. Hoy esto se loguea silenciosamente.

---

## 9. Anexo — citas de archivos descargados

Snapshot Azure DevOps al 2026-04-30, rama `dev_2023_estable`:

| Archivo | bytes 2023 | bytes 2028 | Δ |
|---|---:|---:|---|
| clsLnStock_res_Partial.vb | 2.223.826 | 2.234.840 | +11 KB en 2028 (incluye comentarios del fix parcial) |
| clsLnTrans_picking_ubic_Partial.vb | 491.730 | 482.131 | -9 KB en 2028 (queries eliminadas) |
| clsLnTrans_pe_det_Partial.vb | 258.064 | 261.530 | +3 KB en 2028 |
| clsLnTrans_picking_det_Partial.vb | 210.791 | 137.685 | **-73 KB en 2028** (refactor mayor) |
| clsLnTrans_ubic_hh_enc_Partial.vb | 169.340 | 167.308 | -2 KB |
| clsLnTrans_picking_enc_Partial.vb | 159.280 | 168.118 | +9 KB en 2028 |
| Update_BD_WMS.sql | 140.156 | 140.156 | idéntico (DDL no cambió) |
| clsLnTarea_hh_Partial.vb | 85.133 | 91.430 | +6 KB en 2028 |
| clsLnTrans_despacho_det_Partial.vb | 33.309 | 34.499 | +1 KB |
| clsBeTrans_picking_ubic.vb | 2.383 | 2.439 | idéntico funcional |

Snapshots locales (ephemerals, en `/tmp/wms-azure-snippets/` del workspace Replit, no commitead). Para regenerar, ver `data-seek-strategy/` o usar `scripts/sync-to-github.py` con extensión que pueda hacer fetch Azure DevOps en lote.

---

## 10. Cómo continuar

1. **Cerrar los Q-***: descargar Forms BOF y `frm_*.java` HH para localizar UI (C-006, C-007).
2. **Confirmar el constraint** que dispara la hipótesis A del INFORME-EJECUTIVO (C-008).
3. **Hacer git blame** del fix parcial en 2028 (C-010) → llevará a la persona que detectó el bug originalmente. Este paso es bloqueante antes de tocar las líneas comentadas: puede haber contexto que cambie el plan.
4. **Ejecutar el PLAYBOOK-FIX de CP-013** según la estrategia §H:
   - **Target principal**: terminar el fix parcial en `dev_2028_merge`, validar en MAMPA QA con los 6 casos golden de §G.4.
   - **Target secundario**: cherry-pick a `dev_2023_estable_hotfix_danado` solo si Killios no aguanta esperar al release general de 2028. Roll-out gradual según §G.5.
5. **Validar contra los 5-10 casos golden** definidos en §G.4 del PLAYBOOK (incluye caso negativo "bodega sin MERMA configurada", idempotencia, AJCANTN, reconciliación).
6. **Reconciliar las 919 filas duplicadas** según receta de PLAYBOOK-FIX §B.

---

## Resumen de una línea para Erik

El flag `dañado_picking` es un drenaje de stock silencioso de 4 años, conocido por el equipo (alguien intentó parchar en 2028 y dejó código comentado sin terminar), en producción para Killios + BYB + Mercopan, con 24% de líneas marcadas en Killios y ~320k UM fantasma — el fix técnico está completo en `CP-013/PLAYBOOK-FIX.md` con guía de código línea por línea (§G) y estrategia de ramas (§H, 2028 primero como continuación del trabajo iniciado, hotfix puntual a 2023 solo si la urgencia operativa lo justifica).
