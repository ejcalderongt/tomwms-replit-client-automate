# Handoff Operativo — HH/BOF Etiquetas + Reemplazo Talla/Color

Fecha: 2026-06-11  
Branch base: `dev_2028_merge`  
Capas: `HH Android (TOMHH2025)` + `BOF/DAL VB (TOMIMSV4)` + `DB SQL`

## Objetivo

Consolidar hallazgos y patrones reutilizables para incidentes de:

- Impresión de licencia/fardo y refresco de pantalla.
- Persistencia de etiquetas impresas por presentación.
- Carga de Talla/Color (TC) al seleccionar/escanear licencia en HH.
- Reemplazo en picking/verificación respetando misma talla/color.
- Diferencias JSON vs SOAP en carga de listas de picking.

## Síntomas Observados

1. En HH, al escanear o seleccionar licencia: `Talla` y `Color` en `-`.
2. En reemplazo, registros llegaban sin `IdProductoTallaColor`/`Codigo_Talla`/`Codigo_Color`.
3. En flujos específicos, la lista de picking cargada por JSON no traía TC consistente.
4. En BOF/HH, impresión mostraba datos incompletos por variantes de nombres de columna (`Codigo_Talla` vs `Talla`, `Codigo_Color` vs `Color`).

## Causas Raíz

1. **Mapeo frágil en HH por índice de cursor** (`DT.getInt(41)`, etc.), sensible a orden de columnas.
2. **Contrato de datos heterogéneo** entre vistas/consultas (`Talla/Color` vs `Codigo_Talla/Codigo_Color`).
3. **Ruta JSON sin validación defensiva** en escenarios TC, permitiendo continuar con lista incompleta.

## Patrón de Solución Reutilizable

### A) JSON primero + fallback SOAP controlado

- Mantener JSON como ruta principal (rápida y estable).
- Si `Control_Talla_Color = true` y la lista viene sin TC, ejecutar reintento SOAP tipado.
- Evitar forzar SOAP en todos los casos para no degradar rendimiento base.

### B) Mapeo robusto por nombre de columna

- En HH (reemplazo picking/verificación), leer TC por nombre de columna con fallback por índice.
- Esto desacopla del orden de columnas y reduce regresiones de cursor.

### C) Mapeo VB defensivo por alias de columna

- En `clsLnTrans_picking_ubic.Cargar(...)`, si no viene `Codigo_Talla/Codigo_Color`, usar `Talla/Color`.
- Normaliza el contrato cuando la vista no expone alias homogéneos.

## Archivos Clave (referencia rápida)

- `TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Picking/frm_detalle_tareas_picking.java`
- `TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Picking/frm_list_prod_reemplazo_picking.java`
- `TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Verificacion/frm_list_prod_reemplazo_verif.java`
- `TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic.vb`
- `WSHHRN/TOMHHWS.asmx.vb` (`Get_All_PickingUbic_By_IdPickingEnc_Tipo_Json`)

## Validaciones Mínimas Post-fix

1. Escanear licencia con producto TC:
   - Debe mostrar `Talla`/`Color` correctos en pantalla de detalle.
2. Reemplazo picking/verificación:
   - Debe listar candidatos con misma TC cuando aplique.
   - Debe persistir `IdProductoTallaColor` y códigos TC.
3. Recarga de lista:
   - JSON normal en caso base.
   - Fallback SOAP solo si JSON llega sin TC.
4. Build HH:
   - `:app:compileDebugJavaWithJavac` exitoso.

## Guardrails de no regresión

- No tocar `Reference.vb`.
- No mezclar cambios HH y VB en un mismo commit de entrega, salvo orden explícita.
- Cuando se agreguen columnas en consultas de reemplazo:
  - Preferir consumo por nombre de columna en HH.
- Si se modifica una vista con TC:
  - Mantener alias coherentes (`IdProductoTallaColor`, `Codigo_Talla`, `Codigo_Color`).

## Checklist de Diagnóstico Rápido (próximos incidentes)

1. Verificar si TC viene en payload/lista de picking.
2. Confirmar contrato de columnas en SQL/vista (alias esperados).
3. Revisar mapeo en DAL VB (`Cargar`) y mapeo HH (cursor).
4. Reproducir escaneo + reemplazo en un caso controlado con TC.
5. Correlacionar estado UI vs persistencia en BD antes de tocar lógica de negocio.

