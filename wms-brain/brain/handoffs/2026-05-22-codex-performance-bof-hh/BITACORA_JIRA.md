# Bitacora Jira - Optimizaciones BOF/HH

> Registro resumido de avances para copiar/adaptar en Jira, daily o cierre de tarea.

## 2026-05-26

### Resumen ejecutivo

Jornada enfocada en incidencias HH de QA La Cumbre relacionadas con permisos de menu, consulta de existencias, flujo de verificacion por escaneo y reemplazo/no encontrado en picking con validaciones nuevas de base de datos (`cantidad_solicitada > 0`).

### Actividades tipo Jira (hoy)

- Diagnostique inconsistencia de menu HH por rol (pickeador/recepcion) y valide contrato de permisos en respuesta del servicio para variantes de nombre/ID de opcion.
- Analice errores de reserva/no-reserva en QA (`SIN_VENCIMIENTO_VALIDO`) y valide evidencia de stock aplicable vs reglas de vencimiento/ubicacion.
- Corregi error de existencias HH `Incorrect syntax near the keyword 'Of'` en DAL (`clsLnStock_CI`), reemplazando sintaxis invalida en SQL.
- Endureci consultas de existencias HH parametrizando filtro `pNombre/lote` para evitar fallos por texto y mejorar seguridad/estabilidad.
- Corregi flujo de verificacion HH fuera de lista general: request de escaneo enviaba `gIdPedidoEnc` en vez de `pIdPedidoEnc`, impidiendo navegar a confirmacion.
- Refuerce matching de escaneo en verificacion para contemplar tambien `CodigoSKU` (casos talla/color).
- Diagnostique y corregi falla recurrente de reemplazo/no encontrado por constraint `Stock_NonNegative_20250228_CKFK` en `trans_picking_ubic.cantidad_solicitada`.
- Ajuste rutas DAL para no ejecutar updates con `cantidad_solicitada <= 0` y desviar correctamente a cierre/eliminacion.
- Corregi caso borde en `Reemplaza_Producto_Dannado_Menor` cuando `CantidadPendiente = 0` (antes dejaba cantidad dañada en 0 y disparaba error aguas abajo).
- Agregue guard clause en `Modificar_PickingUbic_By_Reem` para omitir procesamiento invalido cuando `CantSol <= 0`.
- Valide evidencia en BD QA (`log_error_wms_pick`) para confirmar ruta real de fallo y cierre de causa raiz.
- Actualice contexto operativo (`codex-context-mi3-di-estatus.yml`) con hallazgos y correcciones para continuidad entre sesiones.

### Evidencia / referencias de archivos tocados

- `TOMIMSV4/DAL/Transacciones/Stock/clsLnStock_CI.vb`
- `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
- `TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic_Partial.vb`
- `TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Verificacion/frm_detalle_tareas_verificacion.java`
- `codex-context-mi3-di-estatus.yml`

### Esfuerzo estimado para Jira (hoy)

Total horas hombre estimadas: **7.5 h**

- Diagnostico funcional HH (menu/permisos, existencias, verificacion): **2.0 h**
- Diagnostico SQL/QA + trazabilidad en logs de error picking: **1.5 h**
- Correcciones DAL (existencias + reemplazo/no encontrado): **2.0 h**
- Correcciones HH Android (escaneo fuera de lista + SKU): **1.0 h**
- Validaciones cruzadas, cierre tecnico y documentacion/contexto: **1.0 h**

Horas computadas/automatizadas estimadas: **0.8 h**

- Consultas DB QA (`sqlcmd`) para reproduccion/validacion: **0.3 h**
- Busquedas y trazabilidad en repo (`rg`, diffs): **0.3 h**
- Edicion de bitacora/contexto y consolidacion de evidencia: **0.2 h**

## Semana 2026-05-19 al 2026-05-23

### Resumen ejecutivo semanal

Semana concentrada en correcciones funcionales y mejoras de rendimiento sobre procesos criticos de WMS: reserva MI3/BYB, recepcion BOF/HH, inventario, picking, pedido, endpoints HH y documentacion operativa en Brain. El patron principal fue corregir reglas de negocio sensibles sin romper contratos existentes, agregar trazabilidad para diagnostico, reducir reprocesos/N+1 queries y abrir caminos graduales hacia read-models SQL y JSON.

### Reserva MI3 / BYB / Core / Legacy

- Corregi el proceso de reserva MI3/BYB cuando la linea ya venia normalizada a UMBAS, evitando calculos incorrectos por presentacion nula o `IdPresentacion` en 0.
- Ajuste la logica de cantidad pendiente para usar la cantidad real pendiente y evitar sobre-reserva en escenarios con conversion de unidad de medida.
- Corregi casos donde `IdStock` quedaba en 0 en `stock_res` durante procesos de explosion/reserva.
- Agregue defensas para no insertar reservas vacias, respetando el `CHECK cantidad > 0` de `stock_res`.
- Evite que un candidato agotado o descartado pudiera revertir reservas validas ya encontradas.
- Ajuste el flujo para que si la zona ALM/no-picking exige explosion y picking cubre la demanda, la reserva quede coherente con la ejecucion real.
- Blinde reversa de picking para que no impacte stock/reserva cuando no hubo ejecucion real.
- Homologue motivos de no reserva entre flujo legacy y WebAPI/Core.
- Mejore el diagnostico de no reserva MI3 en log legado y `Process_Result`, evitando sobrescrituras que ocultaban la causa real.
- Optimice la resta de stock reservado en reserva MI3 y reduje reprocesos de stock.
- Agregue trazas/eventos de tiempo para diagnosticar costos de reserva y facilitar soporte productivo.
- Documente el playbook y patrones de reserva MI3/BYB en Brain para repetir el diagnostico sin depender de memoria tribal.

Commits referencia: `d5ec5e92`, `51bf68db`, `4806210e`, `faaf8534`, `cb4726b9`, `6ff10f45`, `0798df6c`, `35f01749`, `07202354`, `311d3f29`, `9e0bc73b`, `d620dead`, `6d7352e1`, `0f4a6776`, `e06126ca`, `3bd004f7`, `83cf4268`, `c4d46d6a`, `5f1790ae`, `4ef4109e`, `1b83dbae`, `d7437b1a`, `fe8455e3`, `9e958ecd`, `b00f2763`.

### Recepcion HH

- Corregi recepcion HH con pallet proveedor, cuidando el mapeo de cantidad de presentacion, UMP y UMBAS.
- Corregi consulta de stock HH y manejo de errores del WebService para que la HH no falle de forma silenciosa o poco diagnosticable.
- Ajuste carga de detalle de recepcion y asignacion del objeto de detalle al editar/continuar registros.
- Corregi llenado de pallet y cantidad en recepcion HH.
- Centralice la prioridad de ubicacion en recepcion HH para que el mapeo sea consistente.
- Ajuste flujos de existencias/reimpresion y seleccion de stock para reemplazo/picking.
- Migre parcialmente consumos HH a bootstrap JSON y recepcion JSON, conservando compatibilidad con SOAP/XML.

Commits referencia HH: `c622cd77`, `0f891b0b`, `eb3b4bfc`, `067bd3f0`, `388dde49`, `fafdf940`, `1b647d2f`, `2dc5172a`, `aa502225`, `3a10353e`, `c4f15e04`, `284c7da9`, `618102fe`.

### BOF - Recepcion / Pedido / Picking

- Corregi recepcion outbound idempotente cuando el registro outbound no existia o quedaba huerfano.
- Corregi guardado de `trans_re_det` por cambio a identity.
- Optimice carga de recepcion BOF con read-model SQL, cache de lookups y fallback legacy.
- Mejore carga de pedido y orden de compra, incluyendo visualizacion de tiempo de carga y helpers de totales.
- Corregi totales en pedido y ajustes en `frmPedido`.
- Corregi movimientos de picking y agregue read-model para optimizar carga de picking.
- Ajuste formularios de pedido/picking para reducir tiempos de apertura y refresh.

Commits referencia: `99d6bdef`, `fe41358b`, `ff635f54`, `5f4fb080`, `bc6721cb`, `888e696d`, `f36e9a26`, `ddcefe48`, `7bc246e4`, `7a3adaf4`, `718b3060`, `08149bc6`, `2b55d9c2`.

### BOF - Inventario / Importacion

- Mejore trazabilidad de importacion de inventario y columnas omitidas.
- Optimice validacion de inventario importado con cache y read-model, reduciendo llamadas repetitivas durante `Validar_Datos`.
- Mejore aplicacion de teorico usando cache para disminuir costo de lectura repetida.
- Agregue mecanismos de cancelacion/progreso en importacion y batch insert para mejorar control operativo en procesos largos.
- Agregue batch de stock/producto para procesos masivos.

Commits referencia: `5bfea1dc`, `e91725ce`, `6b3a5d36`, `9d0d8d6d`, `d54577b3`, `2233f457`, `a599caf5`.

### WebService HH / JSON / Read-models

- Agregue endpoint/bootstrap JSON para HH y endpoints JSON paralelos de recepcion, manteniendo contratos SOAP/XML existentes.
- Optimice stock por ubicacion HH con SP/read-model `usp_wms_hh_stock_by_ubicacion_v1`.
- Reduje costo de serializacion y parsing para rutas HH que ya pueden consumir JSON.
- Aplique patron de fallback para que el codigo pueda desplegarse aunque los SPs nuevos aun no esten instalados.

Commits referencia: `2dddbf19`, `8d7d7433`, `618102fe`.

### Integraciones / Documentos / MI3

- Agregue endpoint `mi3/di-estatus` para estatus resumido de OC/documento de ingreso por referencia.
- Corregi/mejore carga de documentos de ingreso y salida.
- Ajuste indicador rapido de pedido.
- Agregue mejoras visuales y parametros en forma de bodega/documentos ERP.
- Sincronice reglas y contexto de WebService/MI3 para mantener trazabilidad de cambios recientes.

Commits referencia: `aa51ccc3`, `c0c112a7`, `68045f09`, `5ffe278e`, `375eb06c`, `85462206`, `37c19d6d`.

### Brain / Handoffs / Documentacion

- Versione contexto incremental MI3 y handoffs para no perder reglas aprendidas.
- Agregue documentacion de agentes/workflows para performance WebAPI, BOF y HH Android.
- Agregue playbook de reserva MI3 y diagnostico de no reserva.
- Cree bitacora diaria y bitacora Jira para registrar avances resumidos por proceso.

Commits referencia Brain/TOMWMS docs: `6f4d2a7e`, `184ef535`, `ae81bda2`, `b00f2763`, `c341569`, `b7aea6c`, `179c016`, `8adf0fa`, `dce7f61`.

### Esfuerzo semanal estimado para Jira

Total horas hombre estimadas: **38.0 h**

Desglose sugerido:

- Correcciones reserva MI3/BYB/Core/Legacy: **12.0 h**
- Correcciones HH recepcion/stock/ubicacion/reemplazo: **6.0 h**
- Optimizaciones BOF recepcion/pedido/picking: **7.0 h**
- Optimizaciones inventario/importacion/cache/progreso: **5.0 h**
- WebService HH, JSON y read-models SQL: **4.0 h**
- Documentacion Brain, handoffs, bitacora y validaciones: **4.0 h**

Horas computadas/automatizadas estimadas: **2.5 h**

- Compilaciones BOF/WS/HH, revision de diffs y validaciones locales: **1.0 h**
- Busqueda de commits, trazabilidad git y agrupacion de cambios: **0.5 h**
- Indexacion/documentacion/handoffs y soporte de analisis con herramientas: **1.0 h**

Nota: las horas son estimadas para registro Jira; los commits cubren trabajo funcional, diagnostico, pruebas locales, documentacion y soporte de despliegue.

### Texto breve semanal sugerido para Jira

Durante la semana se corrigieron y optimizaron procesos criticos de WMS: reserva MI3/BYB, recepcion HH/BOF, inventario, pedido y picking. Se corrigieron reglas de reserva en UMBAS, defensas contra sobre-reserva/reservas vacias, errores de stock/reserva, recepcion outbound idempotente, carga de detalle de recepcion y mapeos HH de pallet/cantidad/ubicacion. Tambien se agregaron read-models SQL, caches seguros, endpoints JSON paralelos para HH, mejoras de trazabilidad y documentacion Brain, manteniendo compatibilidad con contratos legacy SOAP/XML y flujos existentes.

## 2026-05-22

### Resumen ejecutivo

Se trabajo una carga importante de optimizaciones de rendimiento en BOF, WebService HH y HH Android, enfocadas en reducir tiempos de carga, eliminar consultas repetitivas, mejorar fluidez visual y preparar caminos JSON/read-model sin romper contratos legacy SOAP/XML ni objetos existentes.

### BOF - Inventario

- Corregi/reduje el retraso visual en `frmInventarioImport` durante `Validar_Datos`, evitando repintados innecesarios del grid mientras se recorren y actualizan filas.
- Mejore la experiencia de refresco del grid de inventario usando control de layout/redraw para que la validacion no se sienta congelada por cada asignacion de celda.
- Optimice la carga de `frmInventario` posterior a la importacion usando mecanismos de carga masiva del grid/datos.
- Agregue/prepare read-model SQL para comparacion de inventario y reduje dependencia de llamadas costosas por fila en `Get_All_By_Comparacion_Inventario`.
- Mantengo fallback al flujo anterior para evitar ruptura si el SP nuevo aun no esta desplegado.

### BOF - Recepcion

- Analice `frmRecepcion` en modo editar y en creacion de recepcion desde orden de compra/documento de ingreso.
- Identifique N+1 queries en carga de detalle: producto, presentacion, unidad de medida, estado, talla/color, SKU, detalle OC y validaciones relacionadas.
- Agregue el SP read-model `dbo.usp_wms_recepcion_detalle_readmodel_v1` para devolver el detalle de recepcion ya enriquecido.
- Integre el read-model en `clsLnTrans_re_det` con fallback automatico al query/vista legacy si el SP no existe o falla.
- Agregue cache local por formulario en `frmRecepcion` para:
  - presentaciones por producto;
  - estados por propietario/bodega;
  - presentaciones por bodega.
- Mejore la carga del detalle de recepcion reutilizando informacion ya hidratada del objeto en lugar de consultar repetidamente por linea.
- Optimice la creacion desde OC reutilizando el read-model de orden de compra y evitando reconsultas de talla/color/SKU cuando ya vienen hidratados.
- Extendi el read-model de OC para devolver `CodigoSKU`.
- Mantengo contratos existentes de entidades y metodos publicos para minimizar riesgo de impacto.

### WebService HH

- Agregue cache controlado para catalogos HH de baja volatilidad, cuidando no cachear datos operativos sensibles como stock, picking, recepcion activa o reservas.
- Agregue endpoint JSON de bootstrap de sesion HH para agrupar datos iniciales de login/sesion.
- Agregue endpoints JSON paralelos para recepcion HH, manteniendo vivos los metodos SOAP/XML existentes.
- Optimice consulta de stock por ubicacion HH con read-model SQL y fallback al query anterior.
- Reduje costo de serializacion/parsing para flujos HH que ya pueden migrar gradualmente de XML a JSON.

### HH Android

- Actualice HH para consumir bootstrap JSON de sesion.
- Actualice pantalla/listado de recepciones HH para consumir endpoint JSON cuando esta disponible.
- Mantengo compatibilidad con el flujo anterior para no romper clientes que sigan usando SOAP/XML.
- Mejore la carga inicial de contadores/tareas evitando consultas redundantes posteriores.

### SQL / Read-models

- Agregue/actualice scripts SQL bajo `tools/*-readmodel` para rutas de alto costo.
- Cree read-model de recepcion:
  - `tools/recepcion-readmodel/001_create_wms_recepcion_detalle_readmodel_v1.sql`
- Actualice read-model de orden de compra:
  - `tools/ordencompra-readmodel/001_create_wms_ordencompra_readmodel_v1.sql`
- Use patron de fallback en DAL para que el codigo pueda desplegarse antes que los SPs sin romper operacion.

### Documentacion / Brain

- Documente patrones reutilizables de performance en `brain/agents/domain-performance.yml`.
- Registre criterios de cache seguro:
  - cachear catalogos/lookups de baja volatilidad;
  - no cachear datos operativos donde la integridad depende de lectura fresca.
- Deje bitacora diaria del trabajo en `LOG_DIARIO.md`.

### Validaciones ejecutadas

- Compile BOF/WS:
  - `MSBuild WSHHRN.vbproj`
  - Resultado: OK; warning previo relacionado con `sap.data.hana.v4.5`.
- Compile HH Android:
  - `.\gradlew.bat assembleDebug`
  - Resultado: OK.
- Compile BOF WinForms:
  - `MSBuild TOMIMSV4\TOMIMSV4\WMS.vbproj /t:Build /p:Configuration=Debug /p:Platform=AnyCPU`
  - Resultado: OK, 0 errores; solo warnings existentes de referencias/DevExpress.
- Revise diff y stage para no mezclar cambios no relacionados.

### Esfuerzo estimado

#### Horas hombre estimadas laboradas

Total estimado: **8.0 h**

Desglose:

- Analisis de performance BOF/HH y trazado de puntos calientes: **1.5 h**
- Revision de `frmInventarioImport`, `frmInventario` y patrones de grid/redraw/cache: **1.0 h**
- Analisis de WebService HH, contratos XML/SOAP y migracion gradual a JSON: **1.0 h**
- Implementacion BOF/WS de read-models, cache y endpoints JSON: **1.5 h**
- Ajustes HH Android para bootstrap/recepcion JSON y compatibilidad legacy: **1.0 h**
- Analisis e implementacion de optimizacion `frmRecepcion` BOF con SP/read-model/fallback: **1.5 h**
- Validaciones, build, revision de diff, commits y push: **0.5 h**

#### Horas computadas / automatizadas

Total estimado: **0.4 h**

Desglose:

- Compilacion BOF/WS con MSBuild: **0.1 h**
- Compilacion HH Android `assembleDebug`: **0.1 h**
- Compilacion BOF WinForms `WMS.vbproj`: **0.1 h**
- Busquedas/indexacion local con `rg`, revision de diffs y verificacion git: **0.1 h**

Nota: las horas computadas representan tiempo efectivo de maquina/herramientas, no esfuerzo humano continuo.

### Commits / envios

- BOF/WS:
  - `2dddbf19 #EJC_MEJORAS_RENDIMIENTO_HH: usp_wms_hh_stock_by_ubicacion_v1 +`
  - `8d7d7433 Optimiza endpoints JSON para HH`
  - `2b55d9c2 Optimiza carga de recepcion BOF`
- HH Android:
  - `618102fe Consume bootstrap y recepcion JSON en HH`
- Rama destino:
  - `dev_2028_merge`
- Push BOF:
  - Enviado a `origin/dev_2028_merge`.

### Pendientes / seguimiento

- Desplegar `dbo.usp_wms_recepcion_detalle_readmodel_v1` en la BD destino para activar el camino rapido de `frmRecepcion`.
- Validar con una recepcion grande en ambiente cliente:
  - tiempo de apertura en modo editar;
  - carga de detalle;
  - talla/color/SKU;
  - presentacion;
  - estado;
  - cantidad solicitada/recibida/pendiente.
- Validar creacion de recepcion desde documento de ingreso/OC.
- Medir antes/despues para documentar mejora real percibida por usuario.
- Mantener fuera del commit el cambio local no relacionado de whitespace en `TOMIMSV4/DAL/Transacciones/Stock/clsLnStock_Partial.vb`.

### Texto breve sugerido para Jira

Se optimizaron flujos de carga BOF/HH reduciendo roundtrips y consultas repetitivas. Se agregaron read-models SQL con fallback para inventario/recepcion/stock HH, cache local seguro de catalogos/lookups y endpoints JSON paralelos para HH. En `frmRecepcion` se agrego un read-model de detalle enriquecido y cache por formulario para mejorar la apertura de recepciones en edicion y creacion desde OC sin romper contratos legacy. Compilaciones BOF, WS y HH completadas correctamente; pendiente desplegar SP de recepcion en BD destino y validar tiempos con recepciones grandes.

---

## 2026-05-26 - Actualizacion sugerida (modo borrador, no publicar aun)

### Resumen ejecutivo

Se atendieron incidencias operativas de verificacion/reemplazo/picking y CEALSA BACK ORDER, se corrigieron errores SQL y de UI que bloqueaban flujo, se estabilizo data QA de casos puntuales sin limpieza masiva, y se implemento base Python para analisis acelerado en dos vias (datos + operatividad). Se mantiene regla de no publicar en Jira sin aprobacion explicita.

### Tareas sugeridas para Jira (draft)

1. **WMS-BOF: Correccion de SQL en existencias**
   - Problema: error `Incorrect syntax near keyword 'Of'` por expresion `Convert(Of Date, ...)`.
   - Solucion: ajuste a sintaxis T-SQL valida (`Convert(Date, Fecha_Ingreso)`) y verificacion de consulta.
   - Impacto: elimina fallo en consulta de existencias escaneando producto.

2. **WMS-PICK/VERI: Estabilizacion de flujo reemplazo/no encontrado**
   - Problemas: `Index out of range` y conflicto `Stock_NonNegative_20250228_CKFK` en `trans_picking_ubic.cantidad_solicitada`.
   - Solucion: ajustes de logica y saneo dirigido de data QA (picking 3524) preservando packing.
   - Impacto: continuidad de flujo al marcar danado/merma/no encontrado.

3. **WMS-PACKING-HH: Filtro por licencia de packing**
   - Requerimiento: filtrar por licencia de empaque `MM...` y no por licencia de stock.
   - Solucion: ajuste en `frm_preparacion_packing.java` + pista visual en UI.
   - Impacto: busqueda coherente en preparacion/verificacion basada en licencia packing.

4. **WMS-CEALSA-OC: Banderita No Enviado no visible en BACK ORDER**
   - Diagnostico: `mnuEstadoEnviadoAERP` tenia logica activa, pero no estaba enlazado al ribbon group.
   - Solucion: agregar item al `RibbonPageGroup` del diseñador de `frmOrdenCompra`.
   - Impacto: se restaura accion para marcar envio ERP y cerrar flujo de BACK ORDER.

5. **WMS-PERF-INVINI: Mejora de tiempo de carga inventario inicial**
   - DAL: remocion de `MaxID(...)` innecesario en `InsertarSinID`.
   - HH: evita actualizar tramo en proceso por cada conteo cuando ya esta sincronizado.
   - Impacto esperado: menos roundtrips y menor tiempo por lote de registros.

6. **WMS-TOOLING: EJC Python Agents (MVP)**
   - Entregable: motor local para analisis de casos en dos vias (data/operatividad).
   - Uso: genera reporte consolidado para acelerar traza, debug y dimensionamiento.
   - Estado: operativo en modo draft, sin integraciones de escritura externas.

### Esfuerzo estimado (referencial)

- Analisis y traza de incidentes cruzando codigo + BD: **3.0 h**
- Correcciones BOF/HH en flujos y SQL: **3.5 h**
- Estabilizacion de data QA (targeted fix): **1.0 h**
- Implementacion MVP herramienta Python (agente + reporte): **1.5 h**
- Validacion tecnica/builds y documentacion Brain/Jira draft: **1.0 h**
- **Total estimado: 10.0 h**

### Nota de control

Este bloque queda intencionalmente como **borrador interno**.
No publicar en Jira hasta confirmar:
- dimensionamiento final,
- descripcion funcional,
- esfuerzo definitivo por tarea,
- prioridad y alcance por cliente/ambiente.
