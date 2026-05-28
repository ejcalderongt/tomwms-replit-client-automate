---
tipo: other
autores: [erik]
---
# Log diario

> Sigue protocolo `wms-brain/brain/reference/protocolo-log-diario.md`.

## 2026-05-22 - Optimizaciones de rendimiento BOF/HH

### Resumen

Se trabajo una mejora de fluidez para procesos de inventario, recepcion HH y recepcion BOF, priorizando cambios quirurgicos: reducir roundtrips, agregar read-models SQL con fallback y mantener contratos legacy vivos para no romper pantallas ni clientes existentes.

El foco final del dia fue `frmRecepcion` BOF: carga de un registro en modo editar y creacion de recepcion desde documento de ingreso/orden de compra. Se agrego un read-model de detalle de recepcion y cache local de lookups para atacar los N+1 queries detectados al cargar lineas, presentaciones, estados, talla/color y detalle OC.

### Cambios / analisis por area

#### BOF

- `frmInventarioImport`: se optimizo la validacion/pintado de grid con control de repintado y reduccion de refrescos durante loops largos.
- `frmInventario`: se optimizo carga de comparacion de inventario con read-model/fallback para reducir consultas por fila.
- `frmRecepcion`: se agrego cache local por formulario para presentaciones por producto, estados por propietario/bodega y presentaciones por bodega.
- `frmRecepcion`: se reutiliza el detalle OC ya hidratado para evitar reconsultas de talla/color/SKU y detalle OC por linea.
- `clsLnTrans_re_det`: se agrego consumo de `dbo.usp_wms_recepcion_detalle_readmodel_v1` con fallback al flujo legacy si el SP no existe o falla.
- `clsLnTrans_oc_det`: se extendio el read-model de OC para traer `CodigoSKU`.

#### BD

- Nuevo script:
  - `tools/recepcion-readmodel/001_create_wms_recepcion_detalle_readmodel_v1.sql`
- SP nuevo:
  - `dbo.usp_wms_recepcion_detalle_readmodel_v1`
- SP/read-model OC existente extendido:
  - `tools/ordencompra-readmodel/001_create_wms_ordencompra_readmodel_v1.sql`
  - agrega `ProductoTallaColor_CodigoSKU`.

#### WebService / HH

- Se agregaron rutas JSON paralelas para reducir costo de XML/SOAP en flujos HH sin eliminar contratos legacy.
- Se agrego bootstrap JSON de sesion HH para agrupar identidad, bodega, permisos/menu y contadores iniciales.
- La HH Android consume bootstrap y recepcion JSON de forma gradual, manteniendo compatibilidad con metodos SOAP existentes.

#### Brain / documentacion

- Se actualizo `brain/agents/domain-performance.yml` con reglas reutilizables:
  - catalog cache HH con TTL corto;
  - stock por ubicacion HH con read-model/fallback;
  - bootstrap HH JSON;
  - recepcion JSON HH;
  - monitor `frmPrincipal02` con read-model/fallback;
  - `frmRecepcion` con read-model de detalle y cache local.

### Validaciones

- BOF/WS:

```powershell
MSBuild WSHHRN.vbproj
```

Resultado: compilacion OK; quedo warning previo `MSB3331` relacionado con `sap.data.hana.v4.5`.

- HH Android:

```powershell
.\gradlew.bat assembleDebug
```

Resultado: compilacion OK.

- BOF WinForms:

```powershell
MSBuild TOMIMSV4\TOMIMSV4\WMS.vbproj /t:Build /p:Configuration=Debug /p:Platform=AnyCPU
```

Resultado: compilacion OK con 0 errores; solo warnings existentes de referencias/DevExpress.

- Git BOF:

```powershell
git push origin dev_2028_merge
```

Resultado: push OK a Azure DevOps.

### Riesgos / what-if pendientes

- `dbo.usp_wms_recepcion_detalle_readmodel_v1` debe desplegarse en cada BD destino para activar el camino rapido; si no esta instalado, el BOF usa fallback legacy.
- Validar en ambiente de cliente con recepciones grandes que el grid de `frmRecepcion` conserva columnas, seleccion, cantidades pendientes, talla/color/SKU y estados.
- No cachear datos operativos mutables como stock, cantidades recibidas, encabezado/detalle de recepcion o estado OC; solo lookups de baja volatilidad por sesion/formulario.
- Quedo fuera del commit un cambio local de whitespace en `TOMIMSV4/DAL/Transacciones/Stock/clsLnStock_Partial.vb`.

### Proximos pasos

- Desplegar `dbo.usp_wms_recepcion_detalle_readmodel_v1` en la BD objetivo.
- Abrir `frmRecepcion` en modo editar con una recepcion grande y medir tiempo antes/despues.
- Crear nueva recepcion desde documento de ingreso/OC y validar que talla/color/SKU, presentacion, estado y cantidades pendientes se ven igual que antes.
- Si la medicion confirma mejora, replicar el patron a otros formularios con N+1 queries en carga inicial.

### Links / artefactos

- BOF commit:
  - `2b55d9c2 Optimiza carga de recepcion BOF`
- BOF/WS commits previos de performance del dia:
  - `2dddbf19 #EJC_MEJORAS_RENDIMIENTO_HH: usp_wms_hh_stock_by_ubicacion_v1 +`
  - `8d7d7433 Optimiza endpoints JSON para HH`
- HH commit:
  - `618102fe Consume bootstrap y recepcion JSON en HH`
- Archivos principales:
  - `TOMIMSV4/TOMIMSV4/Transacciones/Recepcion/frmRecepcion.vb`
  - `TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Detalle/clsLnTrans_re_det_Partial.vb`
  - `tools/recepcion-readmodel/001_create_wms_recepcion_detalle_readmodel_v1.sql`
  - `tools/ordencompra-readmodel/001_create_wms_ordencompra_readmodel_v1.sql`
- `brain/agents/domain-performance.yml`

---

## 2026-05-26 - Correcciones operativas WMS + base de automatizacion local

### BOF/HH - incidentes corregidos y diagnosticados

- Se corrigio en BOF la consulta de existencias que disparaba:
  - `Incorrect syntax near the keyword 'Of'`.
  - Causa: uso de `Convert(Of Date, ...)` en SQL string.
  - Ajuste: reemplazo a `Convert(Date, Fecha_Ingreso)` y normalizacion de alias en consulta.
- Se atendio el flujo de verificacion/reemplazo/no encontrado con excepciones:
  - `Index was out of range`.
  - Conflicto `CHECK Stock_NonNegative_20250228_CKFK` sobre `trans_picking_ubic.cantidad_solicitada`.
  - Se aplicaron fixes de logica y se estabilizo data de QA para picking 3524 sin limpiar packing global.
- Se corrigio en packing HH el filtro de licencia:
  - ahora el campo Licencia filtra por licencia de packing (`MM...`) y no por licencia de stock (`LL/WE...`).
  - ajuste en `frm_preparacion_packing.java` + hint de UI.
- Se implemento mejora de rendimiento en inventario inicial:
  - DAL: se removio `MaxID(...)` innecesario en `InsertarSinID` (costo por registro).
  - HH: se evito roundtrip repetido de `Actualizar_Inventario_Inicial_By_BeTransInvTramo` por cada conteo cuando ya esta en proceso.

### CEALSA - traza documento 17946 (BACK ORDER)

- Validacion en BD `IMS4MB_CEALSA_QAP`:
  - `IdOrdenCompraEnc=17946`, estado `BACK ORDER`, tipo `Transferencia WMS`, pendiente `0`.
  - `Enviado_A_ERP = NULL` (equivale a no enviado en la UI).
- Hallazgo raiz:
  - El boton/banderita `No Enviado` tenia logica activa en `frmOrdenCompra.vb`,
    pero no estaba enlazado al `RibbonPageGroup` en el `Designer`.
- Fix aplicado:
  - se agrego `mnuEstadoEnviadoAERP` al grupo de ribbon del formulario de OC.

### Brain / proceso Jira (modo borrador)

- Se agrego en `brain/agents/coordinator.yml` la seccion `jira_reporting` con guardrails:
  - modo `draft_only`;
  - no publicar en Jira sin confirmacion explicita de Erik;
  - preparar primero dimensionamiento, descripcion, esfuerzo, evidencia y riesgos.

### Base nueva: EJC Python Agents (MVP)

- Se creo `tools/ejc-python-agents` para acelerar diagnostico en dos vias:
  - **Data lane**: estado/cantidades/tablas/query templates.
  - **Operativity lane**: flujo UI/HH/WS, reglas de visibilidad/transicion.
- Archivos:
  - `tools/ejc-python-agents/ejc_agent.py`
  - `tools/ejc-python-agents/agents/ejc-python-agent.yml`
  - `tools/ejc-python-agents/README.md`
- Salida del motor:
  - `tools/ejc-python-agents/out/current-case-report.md`

### 2026-05-26 (actualizacion) - Jira Task Assistant (draft_only)

- Se implemento MVP `tools/jira-task-assistant` para cruce automatico:
  - commits Git (TOMWMS + TOMHH2025),
  - `BITACORA_JIRA.md`,
  - `LOG_DIARIO.md`.
- Objetivo: detectar commits no clasificados como tarea Jira y proponer backlog en modo borrador.
- Script principal:
  - `tools/jira-task-assistant/main.py`
- Documentacion:
  - `tools/jira-task-assistant/README.md`
- Resultado de ejecucion (rango 2026-05-19 a 2026-05-26):
  - `tools/jira-task-assistant/out/jira_candidates.md`
  - `tools/jira-task-assistant/out/jira_payload.json`
  - Conteo inicial detectado: 101 commits no clasificados (pendiente consolidacion por tarea funcional).
- Control:
  - no se ejecutaron llamadas de escritura a Jira; salida queda solo para revision/aprobacion de Erik.
- Integracion de entorno Jira (persistente con `setx`):
  - `JIRA_URL=https://dtsolutionsdevops.atlassian.net`
  - `JIRA_EMAIL=dtsolutionsgt@gmail.com`
  - `JIRA_ACCOUNT_ID=61d3d6500586a20069465469`

### 2026-05-26 (actualizacion) - Trazabilidad fina inventario ciclico (grafo YAML)

- Se documenta el proceso completo de inventario ciclico en un solo archivo segmentado por capas (HH, WS, BOF, DAL, BD), con aristas de flujo y reglas de negocio:
  - `INV-CIC-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Decision de diseño documental:
  - mantener **un solo archivo ramificado** en lugar de separar BOF/HH, para conservar trazabilidad cross-layer sin drift.
- Incluye subflujos:
  - conteo HH base,
  - reconteo,
  - caja master (gate por `Control_Talla_Color`),
  - regularizacion BOF.
- Incluye puntos finos sensibles para cambios futuros:
  - resolucion por barra/unidad,
  - overwrite de caja master,
  - contrato funcional del gate de caja master.

### 2026-05-26 (actualizacion) - Fix rapido inventario ciclico (DAL + HH)

- Hallazgo 1 (DAL): mapeo incorrecto de banderas de producto en consultas de inventario ciclico.
  - En ciertos `SELECT`, `genera_lote` y `control_vencimiento` salian desde `control_peso`.
  - Corregido a:
    - `genera_lote = producto.control_lote`
    - `control_vencimiento = producto.control_vencimiento`
  - Archivo:
    - `C:/Users/yejc2/source/repos/TOMWMS/TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- Hallazgo 2 (HH): validacion redundante obligaba licencia en escenarios donde no aplica LP.
  - Ajuste: exigir licencia solo cuando el producto tiene `Genera_lp = true`.
  - Archivo:
    - `C:/Users/yejc2/StudioProjects/TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/InventarioCiclico/frm_inv_cic_add.java`
- Tag aplicado en comentarios de cambio:
  - `#EJC20260526`

### 2026-05-26 (actualizacion) - Homologacion JSON en recepcion HH (GetSingleRec)

- Problema observado:
  - en `frm_detalle_ingresos` aun se llamaba `GetSingleRec` (XML/SOAP), lo que podia provocar mismatch de deserializacion con campos nuevos (ej. `IdProductoTallaColor`).
- Ajuste aplicado:
  - `ws.callback=3` migra de `callMethod("GetSingleRec",...)` a `callMethodJsonPost("GetSingleRec_JSON",...)`.
  - `processgBeRecepcion()` deja de parsear por `XMLObject` y pasa a `Gson` con `ws.xmlresult` JSON.
  - se agrega reuso de `gl.gBeRecepcion` cuando ya viene precargado desde `frm_lista_tareas_recepcion`, evitando roundtrip redundante.
- Archivo:
  - `C:/Users/yejc2/StudioProjects/TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_detalle_ingresos.java`
- Tag aplicado:
  - `#EJC20260526`

### 2026-05-26 (actualizacion) - Mapa fino de recepcion HH (latencia + flujo)

- Se crea trazabilidad dedicada del proceso de recepcion HH (lista -> detalle -> datos) con nodos por capa HH/WS/DAL/BD, aristas, puntos de costo y quick wins de optimizacion.
- Archivo:
  - `C:/Users/yejc2/source/repos/wms-brain/wms-brain/brain/handoffs/2026-05-22-codex-performance-bof-hh/RECEPCION-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Incluye:
  - causas de lentitud observadas,
  - propuesta de instrumentacion (TTI/callback/endpoints),
  - checklist de validacion post-fix JSON.

### 2026-05-26 (actualizacion) - Expansión de trazas finas operativas

- Se crean trazas finas adicionales para procesos críticos:
  - `PICKING-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
  - `PACKING-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
  - `VERIFICACION-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
  - `REEMPLAZO-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
  - `EXISTENCIAS-HH-BOF-GRAFO-TRAZA-FINA-2026-05-26.yml`
- Se crea índice maestro de trazas:
  - `TRAZAS-FINAS-OPERATIVAS-INDEX-2026-05-26.yml`
- Se documenta estrategia consolidada de uso:
  - `ESTRATEGIA-TRAZAS-FINAS-OPERATIVAS-2026-05-26.md`
- Se actualiza contexto principal para preflight obligatorio por traza fina:
  - `brain/agents/coordinator.yml`
  - `brain/agents/domain-hh-android.yml`
  - `C:/Users/yejc2/source/repos/TOMWMS/AGENTS.md`
