---
id: colas-pendientes
tipo: documentation
estado: vigente
titulo: Colas pendientes — investigaciones a profundizar
ramas: [240byb, dev_2023_estable, dev_2028_merge]
tags: [documentation]
---

# Colas pendientes — investigaciones a profundizar

Cada cola es una pregunta abierta o tarea encolada. Cuando se cierra, mover a `debuged-cases/` o eliminar.

---

## C-001 — Aislar 5-10 casos HH puros del bug `dañado_picking`

**Origen:** CP-014 §5
**Pregunta:** los ~678 casos de bug originados desde HH (`IdOperadorBodega_Pickeo > 0`), ¿se comportan distinto que los del BOF? Hipótesis Erik: la HH sí intenta mover stock pero el webservice del BOF lo deshace.

**Plan:**
1. En `TOMWMS_KILLIOS_PRD_2026`, listar los 72 casos con `IdOperadorBodega_Pickeo > 0 AND dañado_picking=1 AND cantidad_verificada=0`.
2. Para cada uno, correr `audit-linea-tiempo-producto.py --idpb <X>` y revisar:
   - ¿Hay movimientos posteriores cerca de la fecha del marcado?
   - ¿Hay alguna evidencia de stock movido y luego revertido?
3. Comparar contra 72 casos análogos de BOF (`IdOperadorBodega_Pickeo = 0`).
4. Si HH sí mueve stock y el BOF deshace → bug del webservice.
5. Si HH tampoco mueve nada → bug compartido del flujo común post-marca.

**Salida esperada:** documento corto en CP-014 con tabla de comparación.

---

## C-002 — Confirmar estado operativo de MERCOPAN y BYB

**Origen:** CP-014, los backups en EC2 terminan en julio-2024 (MERCOPAN) y diciembre-2023 (BYB).

**Pregunta:** ¿siguen operando con TOMWMS hoy, abril-2026? Si sí, ¿el bug sigue acumulando?

**Plan:**
1. Erik confirma con su equipo si MERCOPAN/BYB están activos.
2. Si activos, pedir backup actualizado para reflashear en EC2.
3. Re-correr `audit-bug-danado-multi-bd.py --dbs IMS4MB_MERCOPAN_PRD IMS4MB_BYB_PRD` con los datos nuevos.
4. Calcular crecimiento del bug entre el backup viejo y el nuevo.

**Salida esperada:** anexo "estado-2026.md" en CP-014 con cifras actualizadas.

---

## C-003 — Verificar comportamiento HH BYB (outlier 21 % HH)

**Origen:** CP-014 — BYB es la única BD donde el bug viene 21 % de la HH (vs 1-3 % en el resto).

**Pregunta:** ¿BYB tiene una versión distinta del HH Android? ¿O su flujo HH es operativamente distinto?

**Plan:**
1. Identificar si `producto_bodega.sistema` u otros campos delatan la versión HH instalada en BYB.
2. Comparar `parametros_sistema` (si existe) entre KILLIOS, MERCOPAN, BYB.
3. Aislar los 101 casos HH de BYB y mirar usuarios/horarios/lic_plates en busca de patrón.

**Salida esperada:** nota en `client-index/byb.yml` con conclusión.

---

## C-004 — Por qué BECOFARMA NO usa `dañado_picking`

**Origen:** CP-014 §4
**Pregunta:** BECOFARMA dispara 591 AJCANTN en 3 meses sin marcar nunca dañados. ¿Su flujo es "ajusto antes de picking"? ¿Se podría replicar en Killios como mitigación temporal del bug?

**Plan:**
1. Identificar quién (rol/usuario) dispara los 591 AJCANTN en BECOFARMA — ¿supervisor? ¿auditoría? ¿operador?
2. Ver si hay configuración de `parametros_sistema` que oculte el botón "marcar dañado" en su BOF.
3. Documentar el flujo alternativo y proponer si es transferible a Killios mientras se hace el fix BOF.

**Salida esperada:** sección en `becofarma.yml` y posible nota en `CP-013/PLAYBOOK-FIX.md` como mitigación temporal.

---

## C-005 — Validar `audit-linea-tiempo-producto.py` con caso WMS164 conocido

**Origen:** CP-013 (caso ya analizado al detalle).

**Plan:** correr el nuevo programa con WMS164 y confirmar que la línea de tiempo coincide con lo documentado en `REPORTE-CONCLUSION-V3.md`. Si difiere, ajustar el programa.

```bash
python3 audit-linea-tiempo-producto.py --db TOMWMS_KILLIOS_PRD_2026 --codigo WMS164 --desde 2025-11-01
```

**Salida esperada:** confirmación o lista de ajustes necesarios.

---

(Mover entradas cerradas a `debuged-cases/` o eliminar. Crear nuevas con id incremental C-NNN.)

## C-006 — Identificar Form BOF que dispara `Dañado_picking=True` desde stock reservado

**Origen:** `code-deep-flow/traza-002-danado-picking.md` §1.4 + §7
**Pregunta:** la búsqueda Azure DevOps por `Danado_picking` capitalizado en BOF dio 0 hits. ¿Qué Form (`frm*.vb`) llama a `clsLnStock_res_Partial.UpdateXxx_Dañado` o equivalente?
**Plan:** descargar listado de Forms `TOMIMSV4/Forms/Picking/*.vb` y `Stock/*.vb`, grep por `Dañado_picking` (con Ñ, mayúscula y minúscula).
**Salida esperada:** path exacto del Form + nombre del control UI que dispara, agregado a §1.4 de la traza-002.

---

## C-007 — Identificar `frm_*.java` HH que setea `Danado_picking=true`

**Origen:** `code-deep-flow/traza-002-danado-picking.md` §2.2
**Pregunta:** la búsqueda Azure DevOps en TOMHH2025 dio solo 1 hit (la entity). ¿En qué activity Android el operador toca el botón "Marcar dañado"?
**Plan:** descargar `app/src/main/java/com/dts/activities/frm_picking_*.java` y buscar `setDanado_picking(true)`.
**Salida esperada:** path exacto del frm + flujo UI documentado, agregado a §2.2 de la traza-002.

---

## C-008 — Confirmar constraint `CHECK (Cantidad > 0)` que dispara hipótesis A

**Origen:** `CP-013-killios-wms164/INFORME-EJECUTIVO.md` §4 hipótesis A + traza-002 §3.4
**Pregunta:** ¿existe en `producto_bodega_stock` o `stock` un constraint que falle cuando un UPDATE deja la fila en cero, y un try/catch que ante el error inserte fila nueva en lugar de manejarlo?
**Plan:**
1. Query Killios: `SELECT * FROM sys.check_constraints WHERE definition LIKE '%cantidad%' AND parent_object_id IN (OBJECT_ID('producto_bodega_stock'), OBJECT_ID('stock'))`.
2. Buscar en BOF `clsLnStock_*.vb` patrón `Try ... Catch ex As SqlException ... Insert`.
**Salida esperada:** confirmación o refutación de hipótesis A, agregado a CP-013 y traza-002.

---

## C-009 — Por qué BYB tiene 21% de marcaciones via HH (vs 1-3% del resto)

**Origen:** `code-deep-flow/traza-002-danado-picking.md` §6 BYB
**Pregunta:** ¿es configuración del cliente (BYB usa más HH para esta operación), o es una variante de código en el proyecto Android?
**Plan:**
1. Revisar `parametros_sistema` en BD BYB (IMS4MB_BYB_PRD).
2. Ver si TOMHH2025 tiene variantes en ramas `byb` / `240byb` (existen según RAMAS_Y_CLIENTES.md §1.2).
**Salida esperada:** razón documentada, eventual entrada en `clients/byb.md`.

---

## C-010 — Git blame del fix parcial 2028 (líneas comentadas en clsLnStock_res_Partial.vb)

**Origen:** `code-deep-flow/traza-002-danado-picking.md` §1.1.B + §0.4
**Pregunta:** ¿quién comentó las líneas 1998-2008 de `clsLnStock_res_Partial.vb` en rama `dev_2028_merge`, en qué commit, con qué mensaje?
**Plan:** API Azure DevOps git blame (`/_apis/git/repositories/TOMWMS_BOF/items?path=...&versionDescriptor.version=dev_2028_merge&includeContent=false&latestProcessedChange=true`) para obtener el commit ID, después fetch del commit para ver autor y mensaje.
**Salida esperada:** identidad del autor + intención original, agregado a §0.4 de la traza-002. Probable lleva al técnico que primero detectó el bug.

---

## C-011 — Migración del default NULL en `dañado_picking`

**Origen:** `code-deep-flow/traza-002-danado-picking.md` §3.1
**Pregunta:** ¿por qué la migración que agregó `dañado_picking` (pos 41) puso default NULL, mientras que `dañado_verificacion` (pos 27) tiene default ((0))? ¿Vale la pena un ALTER de homogeneización?
**Plan:**
1. Buscar en `WSHHRN/Update_BD_WMS.sql` (140 KB) la línea exacta que agregó la columna.
2. Decidir si el ALTER `ALTER TABLE trans_picking_ubic ADD CONSTRAINT DF_trans_picking_ubic_dañado_picking DEFAULT 0 FOR dañado_picking` se aplica como parte del PLAYBOOK-FIX o se deja como está.
**Salida esperada:** decisión documentada en `CP-013/PLAYBOOK-FIX.md` y traza-002 §3.1.

---

## C-012 — Pre-flight `es_rack` en 8 BDs (CP-016 §4.1)

**Origen:** `CP-016-feature-AG29042026-validacion-implosion-rack/INDEX.md` §4.1
**Pregunta:** ¿la columna `bodega_ubicacion.es_rack` existe y está populada en cada cliente? Si está NULL en alguna BD, el feature AG lanza `InvalidCastException` (`CBool(NULL)`).
**Plan:**
1. Correr en read-only en las 8 BDs (KILLIOS PRD, BYB, BECO, CEALSA, MERCOPAN, MAMPA QA, IDEALSA si existe, MERHONSA si existe):
   ```sql
   SELECT '<DBNAME>' AS db,
          CASE WHEN EXISTS (SELECT 1 FROM sys.columns
                            WHERE object_id=OBJECT_ID('bodega_ubicacion') AND name='es_rack')
          THEN 'EXISTE' ELSE 'FALTA' END AS estado,
          (SELECT COUNT(*) FROM bodega_ubicacion WHERE es_rack IS NULL) AS nulls;
   ```
2. Si alguna devuelve `FALTA` → bloquea promoción 2028 a esa PRD hasta agregar la columna.
3. Si devuelve `EXISTE` con NULLs > 0 → ejecutar `UPDATE bodega_ubicacion SET es_rack=0 WHERE es_rack IS NULL` con backup previo.

**Salida esperada:** matriz de estado por BD, agregada a CP-016 §4.7.

---

## C-013 — Documentar asimetría EsCambioEstado HH vs server (CP-016 §3.4)

**Origen:** `CP-016/INDEX.md` §3.4
**Pregunta:** el wrapper unitario `Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack` recibe `EsCambioEstado` del HH explícitamente, pero el wrapper de licencia completa lo CALCULA en el server (`IdEstadoOrigen <> IdEstadoDestino`). ¿Es asimetría intencional o bug latente?
**Plan:** preguntar a Erik/Marcela el motivo. Documentar en `CP-016 §3.4` y eventualmente en un ADR si es decisión arquitectónica.
**Salida esperada:** nota explicativa en `CP-016 §3.4` o ADR-007.

---

## C-014 — Plan de extracción quirúrgica del feature AG desde commit mamut

**Origen:** `CP-016/INDEX.md` §1.1
**Pregunta:** si Killios necesita el feature como hotfix a 2023 (caso urgencia), ¿cómo se extrae el feature del commit `b8ae38a5` (84 changes) sin arrastrar RFID, frmCliente, frmPicking, etc.?
**Plan:**
1. Listar los archivos relevantes al feature (12: ver §3 del CP-016).
2. Crear rama `dev_2023_estable_feature_implosion` desde `dev_2023_estable`.
3. Para cada archivo: `git show b8ae38a5:<path> > <local_path>` y commitear individualmente con mensaje descriptivo.
4. Validar compilación + casos golden GOLD-AG-01..07.
**Salida esperada:** script reproducible o procedimiento manual documentado, agregado a CP-016 §H (si Erik decide hotfix).

---

## C-015 — Auditar valores válidos de `gl.modo_cambio` en HH (CP-016 §3.5)

**Origen:** `CP-016/INDEX.md` §3.5
**Pregunta:** el HH manda `gl.modo_cambio == 2` como `EsCambioEstado=true`. ¿Qué otros valores usa `modo_cambio`? ¿1 = ubicación? ¿0 = libre? ¿Hay valores no documentados que el server interprete erróneamente?
**Plan:** descargar `appGlobals.java` de TOMHH2025 rama `dev_2028_merge` y grep `modo_cambio`. Documentar enum.
**Salida esperada:** tabla de valores en CP-016 §3.5.

---

## C-016 — Verificar compatibilidad método antiguo `Aplica_Cambio_Estado_Ubic_HH` (CP-016 §4.4)

**Origen:** `CP-016/INDEX.md` §4.4
**Pregunta:** ¿el método antiguo (sin `_ConValidacionRack`) sigue existiendo en `WSHHRN/TOMHHWS.asmx.vb` para apks viejas, o se eliminó (rompiendo retrocompatibilidad)?
**Plan:**
```bash
rg -n "Public Function Aplica_Cambio_Estado_Ubic_HH\b" /tmp/wms-azure-snippets/AG29042026/BOF_TOMHHWS.asmx.vb
```
Si NO aparece sin `_ConValidacionRack` → contrato roto. Identificar versiones apk en producción y planear forced upgrade antes de promover 2028.
**Salida esperada:** decisión documentada en CP-016 §4.4 (mantener antiguo deprecated o forced upgrade).

---

## C-017 — Diff `frmPicking.vb` entre 2023 y 2028 (CP-016 §4.5)

**Origen:** `CP-016/INDEX.md` §4.5
**Pregunta:** el commit AG modificó `frmPicking.vb` (272 KB) sin documentación. ¿Qué cambió? ¿Hay regresión latente en el flujo principal de picking de Killios/BYB/CEALSA?
**Plan:**
1. Bajar `frmPicking.vb` rama `dev_2023_estable` y rama `dev_2028_merge`.
2. `diff -u` y revisar líneas modificadas.
3. Categorizar: feature relacionado / feature ortogonal / regresión potencial.
**Salida esperada:** sección nueva en CP-016 con conclusión por categoría.

---

## C-018 — Agregar entrada AG29042026 a `PARCHES_APLICADOS.md`

**Origen:** `CP-016/INDEX.md` §4.6
**Pregunta:** la bitácora de Erik no tiene entrada para el feature. Es trabajo en su archivo del repo TOMWMS_BOF, no del brain. Lo hace Erik directamente.
**Plan:** Erik agrega línea al final de `PARCHES_APLICADOS.md` con formato existente.
**Salida esperada:** commit posterior al feature con la entrada bitácora.

---

## C-019 — Validar interacción CP-013 fix vs feature AG cuando se implemente CP-013

**Origen:** `CP-016/INDEX.md` §6
**Pregunta:** cuando se descomente el bloque del fix CP-013 en `clsLnStock_res_Partial.vb` (líneas 1998-2008) y se agregue la generación de `trans_movimientos` compensatorio, ¿interfiere con el orquestador AG? ¿El movimiento compensatorio se duplica con el del PASO 2 implosión del orquestador?
**Plan:** trace estático del call chain `Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack` → `Aplica_Cambio_Estado_Ubic` → `clsLnStock_res.Update_*` → confirmar si llega a los setters de `Dañado_picking`. Si sí, validar que la generación de movimiento del fix CP-013 no se duplica.
**Salida esperada:** sección en `CP-013/PLAYBOOK-FIX.md` §G (nota sobre interacción AG).
