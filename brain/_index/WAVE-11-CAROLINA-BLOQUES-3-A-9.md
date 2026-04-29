# Wave 11 — Carolina responde Bloques 2 (cierre), 3, 4, 5, 6, 7, 9 del cuestionario

**Fecha**: 2026-04-30
**Trigger**: Carolina envio segunda tanda de respuestas (continuacion Wave 10).
Cubrio cierre del Bloque 2 (Q12-Q15), parcialmente Bloque 3 (delegado Erik/Efren),
completo Bloque 4 (stock_jornada), Bloque 5 (DALCore + generador), Bloque 6
(multi-tenancy), Bloque 7 (capabilities + licencias), Bloque 8 (web BOF — no sabe),
y Bloque 9 (bonus).

**Resultado**: 14 nuevos `learnings/L-035..L-048`, ~25 Q-* respondidas
(parcial o totalmente), Bloques 3 y 8 quedan delegados a Erik/Efren.

---

## Resumen ejecutivo

**Hallazgo critico Wave 11**: **L-035 — NO se codifica por cliente, todo se
parametriza** (tablas Bodega, Producto, i_nav_config_enc principalmente). Esto
reescribe como interpretar diferencias por cliente en TODO el codigo del WMS.
Junto con L-025 (Wave 10, NAV no es NAV), forma el par de **convenciones
arquitectonicas fundamentales** del WMS.

**Otros descubrimientos clave**:
- **MHS go-live 20-ago-2026** (L-038), primer cliente WMSWebAPI con scope cerrado
  (9 maestros + 4 transacciones). Erik depurando bug de traslados.
- **Stock_jornada** controlado por bandera `Empresa.generar_stock_jornada` +
  `Bodega.horario_ejecucion_historico`, disparado al primer login del dia
  (L-036). En CEALSA alimenta SAT, Aseguradora, facturacion, portal y
  reporte "Stock en linea" (L-037).
- **HH llama directo a NAV WS** por tipo de documento (L-039); WSHHRN NO es
  proxy NAV, es solo el bus interno BOF↔HH.
- **BD NAV inaccesible** para WMS, todo via WebServices (L-040).
- **DALCore solo lo consume WMSWebAPI** (L-041). DMS sigue con stack legacy.
- **Generador de codigo BE/LN standalone**, fuera de TOMWMS_BOF, repo desconocido
  (L-042). Erik+Efren lo mantienen.
- **MAMPA usa SAP HANA** y NO vende carne — la merma de carne probablemente
  es de La Cumbre (L-044).
- **Perceptron K7 abandonado** (L-045) — Erik lo reemplazo por metodo nuevo
  de ubicacion sugerida.
- **K7 maneja 18 tipos de OC** enumerados oficialmente (L-046).
- **Clientes no-3PL usan UN propietario default** en la tabla `propietarios`
  (L-048) — confirma que el WMS es 3PL-native by design.

---

## Q-* RESUELTAS

### Bloque 2 (cierre)

| ID | Resolucion | Learning |
|---|---|---|
| Q-MHS-COMO-CLIENTE | GoLive 20-ago-2026, primer WMSWebAPI, 9 maestros + 4 trans | L-038 |
| Q-WSHHRN-AS-PROXY-BYB | WSHHRN NO es proxy NAV; HH llama directo a NAV WS | L-039 |
| Q-BYB-NO-DISPONIBLE-NAV-BD | BD NAV inaccesible, todo via WS NAV | L-040 |
| Q-CHATGPT-SERVICE | Lo creo Efren, no esta activo en TOMWMS, pendiente Efren | (sin learning, nota en cuestionario) |

### Bloque 3 — Portal CEALSA / DMS

**Carolina derivo todo a Erik o Efren**. Q16-Q24 quedan abiertas para esa segunda
ronda. No hay learnings nuevos en este bloque.

### Bloque 4 — Stock_jornada

| ID | Resolucion | Learning |
|---|---|---|
| Q-STOCK-JORNADA-CONSUMER | SAT, Aseguradora, facturacion, portal CEALSA, reporte "Stock en linea" | L-037 |
| Q-STOCK-JORNADA-MAMPA | Bandera `Empresa.generar_stock_jornada` controla, no es regulatorio | L-036 |
| Q-STOCK-JORNADA-PROCESO | Disparado al primer login del dia + `Bodega.horario_ejecucion_historico` | L-036 |
| Q-STOCK-JORNADA-DESFASE | Proceso interno automatico de TOMWMS | L-036 (parcial) |

### Bloque 5 — DALCore / Generador

| ID | Resolucion | Learning |
|---|---|---|
| Q-DALCORE-CONSUMERS | Solo WMSWebAPI; DMS no lo usa | L-041 |
| Q-GENERADOR-UBICACION | Standalone, fuera de TOMWMS_BOF, repo desconocido, EJC+GT mantienen | L-042 |
| Q-GENERADOR-INPUTS | Carolina no sabe → pendiente Erik/Efren | (abierta) |
| Q-GENERADOR-WEB-VARIANTE | Carolina no sabe → pendiente Erik/Efren | (abierta) |

### Bloque 6 — Multi-tenancy 3PL

| ID | Resolucion | Learning |
|---|---|---|
| Q-PROPIETARIO-AGNOSTICO | No-3PL usan 1 propietario default en tabla `propietarios` | L-048 |
| Q-CEALSA-PREFACTURA-MODELO | Carolina derivo a Efren | (abierta) |
| Q-CEALSA-RH-HR | No trabajan RH; consultar Efren | (abierta) |

### Bloque 7 — Recepcion / capabilities

| ID | Resolucion | Learning |
|---|---|---|
| Q-CAPABILITY-FLAG | NO codifican por cliente, todo parametrizado (Bodega, Producto, i_nav_config_enc) | **L-035 (CRITICO)** |
| Q-LICENCIAS-MODELO-NUEVO | Licencias por Presentacion, parametro `producto_presentacion.IdTipoEtiqueta`, MHS estrenando | L-043 |

### Bloque 8 — Web BOF

| ID | Resolucion |
|---|---|
| Q-WEB-BOF-STACK + Q-WEB-BOF-TIMELINE | Carolina no sabe → pendiente Erik |

### Bloque 9 — Bonus

| ID | Resolucion | Learning |
|---|---|---|
| Q-K7-ML-MODELO / Q-PERCEPTRON-USO-REAL | Perceptron abandonado, reemplazado por metodo nuevo de ubicacion sugerida | L-045 |
| Q-MAMPA-MERMA-CARNE-FLUJO | MAMPA NO vende carne; probable La Cumbre | L-044 |
| Q-MAMPA-ERP | SAP HANA | L-044 |
| Q-MAMPA-CEDIS-PANTALLA-LEGACY | CEDIS = Centro Distribucion (terminologia cliente, no legacy) | L-047 |
| Q-K7-OC-TIPOS | 18 tipos enumerados (faltan 6, 9, 11, 12) | L-046 |
| Q-WMS-EXE-CONFIG-EN-WSHHRN | Carolina no sabe → pendiente Erik | (abierta) |

---

## Bloques completamente cerrados Wave 11

- **Bloque 4 (Stock_jornada)** — todas respondidas.
- **Bloque 7 (Recepcion / capabilities)** — todas respondidas.

## Bloques parcialmente cerrados

- **Bloque 2** — Q12-Q15 respondidas (Q15 ChatGPT delegado Efren).
- **Bloque 5** — Q29, Q30 respondidas; Q31, Q32 quedan abiertas.
- **Bloque 6** — Q33 respondido; Q34, Q35 delegados Efren.
- **Bloque 9** — A, B, C, D, E respondidos; F abierto.

## Bloques pendientes

- **Bloque 3** completo — pendiente Erik/Efren (portal CEALSA, DMS, multitenancy decision).
- **Bloque 8** — pendiente Erik (web BOF stack y timeline).

---

## Q-* NUEVAS abiertas Wave 11 (para bloque 14)

Derivadas de los 14 learnings:

| Origen | Q nueva |
|---|---|
| L-035 | Q-PARAMETROS-CATALOGO, Q-PARAMETROS-DEFAULTS |
| L-036 | Q-STOCK-JORNADA-MULTIBODEGA, Q-STOCK-JORNADA-FALLO, Q-STOCK-JORNADA-DESFASE-COMPONENTE |
| L-037 | Q-STOCK-EN-LINEA-DEEP-FLOW, Q-PORTAL-CEALSA-USA-STOCK-JORNADA, Q-FACTURACION-CEALSA-FORMULA |
| L-038 | Q-MHS-TRASLADOS-STATUS, Q-MHS-WEBAPI-ENDPOINTS-CHECKLIST, Q-MHS-AUTENTICACION, Q-MHS-AMBIENTE, Q-MHS-LICENCIAS-PRESENTACION-DISEÑO |
| L-039 | Q-HH-NAV-CACHE-OFFLINE, Q-HH-NAV-RETRY, Q-HH-NAV-TIMEOUT, Q-HH-NAV-ENDPOINTS-LISTA |
| L-040 | Q-NAV-WS-WEBHOOKS, Q-NAV-WS-VERSIONADO, Q-NAV-WS-CACHE-WMS, Q-ODOO-WS-EQUIVALENTE |
| L-041 | Q-DALCORE-COVERAGE-VS-DAL-CLASICO, Q-DALCORE-VS-DAL-CONFLICTOS, Q-DMS-DAL-STACK, Q-DALCORE-PLAN-LARGO-PLAZO |
| L-042 | Q-GENERADOR-REPO-EXACTO, Q-GENERADOR-BRANCHES-ACTIVAS, Q-GENERADOR-CICD |
| L-043 | Q-LICENCIAS-TIPO-ETIQUETA-TABLA, Q-LICENCIAS-DISEÑADOR, Q-LICENCIAS-IMPRESORA-MHS, Q-LICENCIAS-MIGRACION-DEFAULT, Q-LICENCIAS-RETROCOMPATIBLE |
| L-044 | Q-MAMPA-SAP-HANA-WS-CONTRATO, Q-CUMBRE-MERMA-CARNE-FLUJO, Q-NAV-CONFIG-ENC-MULTI-ERP, Q-CUMBRE-OTROS-PERECEDEROS, Q-MAMPA-CEDIS-COSTEO-FORMULA |
| L-045 | Q-UBICACION-SUGERIDA-METODO-ACTUAL, Q-PERCEPTRON-CODIGO-LIMPIAR, Q-PERCEPTRON-DATOS-RESIDUALES |
| L-046 | Q-K7-TIPOS-OC-FALTANTES, Q-K7-TIPO-OC-TABLA, Q-OTROS-CLIENTES-TIPOS-OC, Q-K7-TRANSFERENCIA-VS-TRANSFERENCIAWMS, Q-K7-FACTURA-RESERVA-PROV |
| L-047 | Q-CEDIS-COSTEO-FORMULA, Q-CEDIS-OTROS-CLIENTES, Q-VOCABULARIO-CLIENTE-MAPEO |
| L-048 | Q-PROPIETARIO-DEFAULT-CONVENCION, Q-PROPIETARIO-UI-OCULTO, Q-PROPIETARIO-MIGRACION-NO3PL-A-3PL, Q-PROPIETARIO-PERMISOS, Q-MATRIZ-3PL-POR-CLIENTE |

Total: ~45 Q-* nuevas. Se incorporan al cuestionario en bloques 14-15.

---

## Archivos generados (commit Wave 11)

- `learnings/L-035-meta-no-codificar-por-cliente.md` (CRITICO)
- `learnings/L-036-arch-stock-jornada-trigger.md`
- `learnings/L-037-client-stock-jornada-cealsa-consumers.md`
- `learnings/L-038-client-mhs-golive-scope.md`
- `learnings/L-039-arch-hh-llama-directo-nav-ws.md`
- `learnings/L-040-arch-bd-nav-inaccesible-solo-ws.md`
- `learnings/L-041-arch-dalcore-solo-wmswebapi.md`
- `learnings/L-042-arch-generador-codigo-standalone.md`
- `learnings/L-043-arch-licencias-por-presentacion-mhs.md`
- `learnings/L-044-client-mampa-erp-sap-hana-y-no-vende-carne.md`
- `learnings/L-045-arch-perceptron-k7-abandonado.md`
- `learnings/L-046-client-k7-tipos-oc-enumerados.md`
- `learnings/L-047-meta-cedis-no-es-pantalla-legacy.md`
- `learnings/L-048-proc-clientes-no-3pl-usan-un-propietario.md`
- `_index/WAVE-11-CAROLINA-BLOQUES-3-A-9.md` (este archivo)

## Pendiente (Wave 12)

1. **Erik responde Bloque 3** (portal CEALSA, DMS, repos vacios) → 9 Q-*.
2. **Erik responde Bloque 8** (web BOF stack y timeline) → 2 Q-*.
3. **Efren responde**:
   - Bloque 2: Q-CHATGPT-SERVICE detalle.
   - Bloque 5: Q-GENERADOR-INPUTS, Q-GENERADOR-WEB-VARIANTE.
   - Bloque 6: Q-CEALSA-PREFACTURA-MODELO, Q-CEALSA-RH-HR.
   - Bonus F: Q-WMS-EXE-CONFIG-EN-WSHHRN.
4. **Editar cuestionario** marcando RESUELTAS Wave 11 + agregar Q nuevas como bloque 14-15.
5. **Editar INDEX, DISCOVERY_TREE, HOLDING_IDEALSA** con Wave 11 + Wave 10 (paso 2 pendiente).

---

## Para naked-erik-anatomy/ (candidates Wave 11)

- **"La neuronita que envejecio mal"** — historia perceptron K7 (L-045).
- **"NAV ya no es NAV, parte 2: SAP HANA tambien"** — extension de naked-erik 002 con caso MAMPA (L-044).
- **"El operador es el cron"** — stock_jornada disparado al primer login: la jornada no se cierra si nadie abre el WMS (L-036).
