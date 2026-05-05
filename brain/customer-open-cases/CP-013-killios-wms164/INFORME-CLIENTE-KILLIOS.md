---
id: INFORME-CLIENTE-KILLIOS
tipo: cp-open
estado: vigente
titulo: Informe al cliente Killios — Caso WMS164 (análisis de código y hallazgo del problema)
cliente: killios
producto: WMS164
materializa_bug: BUG-001
fecha: 2026-05-05
audiencia: cliente-final
emite: PrograX24 — Dirección de Desarrollo TOMWMS
tags: [informe-cliente, cliente/killios, producto/wms164, bug/picking, bug/critico]
relacionado_con: [CP-013, BUG-001, traza-002, PLAYBOOK-FIX]
---

# Informe al cliente Killios — Caso WMS164

**Para**: Equipo de Operaciones e IT de Killios
**De**: Erik Calderón — Director de Desarrollo TOMWMS, PrograX24
**Caso interno PrograX24**: CP-013
**Identificador del bug del producto**: BUG-001
**Fecha del informe**: 2026-05-05
**Tipo**: análisis de causa raíz en el código del WMS y plan de remediación

---

## 1. Resumen ejecutivo

El ticket WMS164 que su equipo operativo reportó el **23 de abril de 2026** no es un incidente aislado, sino la manifestación visible de un **defecto del producto TOMWMS** (no de su configuración ni de su operación) que está activo en su base de datos productiva desde, al menos, **mayo de 2025**. El defecto provoca que cuando se marca una línea de picking como "producto dañado", el sistema **registra la marca pero no descuenta la mercadería del inventario lógico**, generando stock fantasma de forma silenciosa y acumulativa.

Hemos identificado el bug con **precisión de archivo y línea de código** en los repositorios fuente del WMS, hemos cuantificado su magnitud en su base de datos con consultas auditables, y tenemos un plan de remediación documentado en dos planos: el **fix técnico** del producto y la **reconciliación retroactiva** de las cantidades fantasma acumuladas.

**Magnitud actual confirmada en `TOMWMS_KILLIOS_PRD_2026`** (medición del 2026-05-05): **10,565 líneas** marcadas como dañadas sin descuento de stock asociado, equivalentes a **318,191 unidades de medida** fantasma, distribuidas en 11 meses de operación.

---

## 2. El caso reportado — WMS164

| Dato | Valor |
|---|---|
| Producto | **WMS164** (IdProducto=77, IdProductoBodega=381) |
| Lote en alerta | **BG2512** (recepción 10-feb-2026) |
| Bodega | Killios B1 (Bodega Principal) |
| Síntoma reportado | "El stock entre físico y WMS no es el mismo y el WMS tiene más de lo que en realidad hay" |
| Cuantificación original | WMS sobrestima en aproximadamente **14 cajas** vs. físico real |
| Fecha del reclamo | 23-abril-2026 |

### Cuadre matemático del lote BG2512

| Métrica | Valor |
|---|---:|
| Stock activo del lote en BD | 135 UM (= 27 cajas) |
| Conteo físico real | ~13 cajas |
| **Diferencia (sobrestimación BD)** | **14 cajas / 70 UM** |
| Líneas marcadas como dañadas en este lote, aún activas en BD | 12 líneas |
| Total UM marcadas como dañadas en este lote | 148 UM (29.6 cajas) |

La diferencia de 14 cajas coincide al **100%** con la denuncia original de su equipo. La explicación matemática es directa: aproximadamente 14 cajas de las 29.6 cajas marcadas como dañadas fueron **retiradas físicamente** de la bodega (el operador llevó la mercadería al área de daños o descarte), pero **el sistema no descontó esas unidades del inventario lógico**.

---

## 3. Hallazgo: causa raíz en el código del WMS

### 3.1 Qué hace el sistema hoy (comportamiento defectuoso)

Cuando un operador o supervisor marca una línea de picking como dañada (sea desde el handheld o desde el backoffice), el sistema ejecuta solamente un paso:

1. Actualiza la fila de `trans_picking_ubic` poniendo el flag `dañado_picking = 1`.

Y omite los tres pasos que **debería** ejecutar:

2. Generar un movimiento de inventario (`trans_movimientos`) que descuente la mercadería de la ubicación de origen.
3. Mover la mercadería dañada a una ubicación de tipo "MERMA" definida para la bodega.
4. Generar un ajuste contable (`AJCANTN`, IdTipoTarea=17) que reconcilie la diferencia frente al ERP.

**Consecuencia operativa**: el operador retira físicamente la mercadería dañada de la góndola pero el sistema sigue mostrándola como disponible. La próxima orden de picking puede asignarle al operador siguiente esa misma mercadería que ya no existe físicamente, y así el problema se cascada en el tiempo.

### 3.2 Dónde vive el defecto en el código

El defecto está en el código del **backoffice TOMWMS BOF (VB.NET)** y, en menor medida, en el flujo de cambio de estado del **handheld Android (TOMHH2025)**. Se trata de un **error por omisión**: el código contempla el paso de marcar la flag pero olvida los pasos de descuento de stock.

Los archivos identificados con precisión son los siguientes (rama `dev_2028_merge` del repositorio `TOMWMS_BOF` en Azure DevOps):

| # | Archivo | Líneas | Función |
|---|---|---|---|
| 1 | `clsLnTrans_ubic_hh_enc_Partial.vb` | 690-696, 719-725, 760-767, 1032-1040, 1059-1067, 1373-1396 | Flujo CEST (cambio de estado desde handheld) — origen histórico del bug, comentario `'#AT 20220110` |
| 2 | `clsLnStock_res_Partial.vb` | 1998-2008 (bloque comentado), 2306-2354 (setters reales) | Reemplazo en stock reservado — desde backoffice |
| 3 | `clsLnTrans_pe_det_Partial.vb` | 822-823, 985-986 | Lectura de totales (esto está correcto: filtra dañados de los totales del pedido, sin tocar) |
| 4 | `WSHHRN/Update_BD_WMS.sql` | DDL de la columna `dañado_picking` con anomalía de default NULL |

### 3.3 Confirmaciones técnicas relevantes

Durante la investigación validamos en la base de datos productiva de Killios:

- **Cero triggers** sobre la tabla `trans_picking_ubic` que pudieran compensar el descuento desde la BD.
- **Cero stored procedures** que toquen la columna `dañado_picking` para escribir.
- **Cero llamadas desde el WebAPI .NET nuevo** al flujo defectuoso (toda la operación de daños sigue por SOAP/WSHHRN, no por el REST nuevo).
- La columna `dañado_picking` tiene `default NULL` mientras que su columna hermana `dañado_verificacion` tiene `default ((0))` — anomalía heredada de una migración antigua que se documentó como ítem secundario a corregir.

**Conclusión técnica**: toda la lógica que debería compensar el descuento de stock cuando se marca una línea como dañada **debería estar en el código VB.NET y no está**. La base de datos no es el problema; el código es el problema.

### 3.4 Indicio de intento previo de corrección

En el archivo `clsLnStock_res_Partial.vb`, líneas 1998-2008 de la rama `dev_2028_merge`, encontramos un bloque de código **comentado** que muestra que alguien del equipo de desarrollo previo intentó iniciar una corrección del defecto. La intervención quedó incompleta: solamente bloqueaba uno de los 6 puntos de marcaje y no agregaba la lógica de descuento. La corrección nunca se terminó ni se propagó a la rama productiva (`dev_2023_estable`) que es la que su instalación está usando hoy.

---

## 4. Magnitud confirmada en su base de datos

Mediciones ejecutadas el **2026-05-05** sobre `TOMWMS_KILLIOS_PRD_2026` (modo solo-lectura, sin alterar datos):

### 4.1 Indicadores principales

| Indicador | Valor |
|---|---:|
| Líneas con `dañado_picking = 1` (histórico acumulado) | **10,565** |
| Unidades de medida marcadas como dañadas sin descuento | **318,191 UM** |
| Líneas con `dañado_verificacion = 1` (segundo flag, igualmente afectado) | 0 (su operación no usa este flujo) |
| Ajustes locales pendientes de sincronizar al ERP (`Enviado_A_ERP=0`) | **709 de 986** (72%) |
| Ventana temporal del bug | **junio 2025 → abril 2026** (11 meses) |
| Mes pico | **abril 2026** (1,904 líneas, 62,000 UM) |

### 4.2 Distribución por usuario operador (top 5)

| Usuario | Líneas afectadas | UM afectadas | Período |
|---|---:|---:|---|
| Heidi López (user_agr=20) | 4,036 | 88,848 | nov-2025 a abr-2026 |
| (user_agr=10) | 3,426 | 142,509 | jun-2025 a abr-2026 |
| Mario Santizo (user_agr=13) | 1,332 | 41,618 | sep-2025 a abr-2026 |
| (user_agr=12) | 1,153 | 31,709 | jul-2025 a feb-2026 |
| (user_agr=28) | 217 | 5,682 | jun-2025 a ene-2026 |

Esta distribución confirma que el defecto **no es atribuible a una persona ni a un puesto operativo específico**: afecta a todos los operadores que utilizan la funcionalidad de marca de daños, en proporción al volumen de operación de cada uno.

### 4.3 Productos top afectados (priorizar para auditoría física)

| IdProductoBodega | UM marcadas como dañadas | Lotes |
|---:|---:|---:|
| 395 | 24,580 | 19 |
| 1515 | 21,421 | 24 |
| 1350 | 18,978 | 11 |
| 730 | 16,068 | 5 |
| 1315 | 15,622 | 9 |
| 1320 | 13,535 | 12 |
| 1375 | 11,353 | 7 |
| 1495 | 11,186 | 8 |

El producto WMS164 reportado tiene **322 UM** afectadas distribuidas en 4 lotes (BG2512, BM2601, BM2511, bm2508). Es un caso pequeño en el contexto general — el problema sistémico es órdenes de magnitud mayor.

### 4.4 Confirmación de cronicidad histórica

La tabla `trans_ajuste_enc` muestra que su equipo viene aplicando **ajustes manuales** para compensar este patrón desde **2019**. Distribución temporal:

| Período | Ajustes positivos | Ajustes negativos | Lectura |
|---|---:|---:|---|
| 2019-09 a 2023-04 | 596 | 925 | Equipo corrigiendo permanentemente el síntoma |
| 2023-04 a 2025-11 | 0 | 0 | Gap de 2.5 años sin auditoría aparente |
| 2025-11 a 2026-04 | 5 | 122 | Reactivación de la corrección manual: 96% son ajustes negativos |

Los motivos de ajuste registrados confirman el mismo patrón:

- "Ajuste Negativo · Despacho Licencia" (105 casos): "se despachó pero no se descontó"
- "Ajuste Positivo · Falla de sistema" (297 casos)
- "Ajuste Negativo · Ajuste contra físico" (105 casos)

Es decir, el problema **ya era conocido operativamente** dentro de Killios desde hace años, pero hasta ahora no se había escalado al equipo de desarrollo del WMS con evidencia suficiente para localizarlo en código.

---

## 5. Por qué este caso no es exclusivo de Killios

Durante el análisis cruzamos la misma medición contra las bases de datos de otros clientes que utilizan TOMWMS. El defecto está activo a escala industrial en otras instalaciones:

| Cliente | Líneas afectadas | Estado |
|---|---:|---|
| **Killios PRD 2026** (su instalación) | **10,565** | Cliente que reportó, severidad CRÍTICA |
| MERCOPAN PRD | 19,607 | Mayor volumen acumulado histórico |
| BYB PRD | 484 | Severidad MEDIA |
| BECOFARMA, CEALSA, MAMPA, MERHONSA | 0 | No utilizan la funcionalidad |

Esta evidencia cross-cliente es valiosa para Killios por dos motivos:

1. **Confirma que el defecto es del producto TOMWMS, no de su configuración** ni de la forma en que su equipo opera el sistema.
2. **El fix beneficiará a todos los clientes simultáneamente** una vez aplicado a la rama base del producto.

---

## 6. Plan de remediación

El plan se ejecuta en **dos planos paralelos pero independientes**:

### 6.1 Fix técnico del producto

Documentado con precisión de archivo y línea en el documento interno `PLAYBOOK-FIX.md`. Resumen del fix:

1. **Aplicar el cambio en la rama `dev_2028_merge`** del repositorio `TOMWMS_BOF`. Esta es la rama de estabilización que será el próximo release general del producto. El cambio:
   - Termina el bloque comentado en `clsLnStock_res_Partial.vb` (líneas 1998-2008) agregando la lógica de descuento de stock omitida.
   - Aplica el mismo patrón en los 5 setters de `clsLnTrans_ubic_hh_enc_Partial.vb` que faltan.
   - Crea una nueva tabla `trans_picking_danado_log` para auditoría inmutable de cada marca de daño.
   - Bloquea la operación si la bodega no tiene una ubicación de tipo "MERMA" configurada (validación obligatoria).

2. **Validar el fix con 6 casos de prueba "golden"** definidos formalmente:
   - Caso 1: marca de daño desde backoffice sin license plate.
   - Caso 2: marca de daño desde handheld con license plate (flujo CEST).
   - Caso 3: bodega sin ubicación MERMA configurada (debe rechazar la operación con error claro).
   - Caso 4: marca de daño con destino "descarte total" (genera AJCANTN negativo).
   - Caso 5: ejecución del script de reconciliación histórica.
   - Caso 6: idempotencia (no duplicar movimientos si se vuelve a llamar al setter).

3. **Roll-out gradual sugerido**:

| Semana | Cliente | Validación |
|---|---|---|
| 1 | MAMPA QA | golden 1-6 en ambiente de pruebas |
| 2 | MAMPA PRD | smoke test con operación real |
| 3 | Killios (clon de PRD a QA) | golden 1-6 + replay de 100 casos históricos |
| 4 | **Killios PRD** | aplicación con ventana de mantenimiento + script de reconciliación |
| 5+ | BYB, MERCOPAN | mismo proceso |

### 6.2 Hotfix anticipado a la rama actual de Killios (opcional)

Si su operación no puede esperar al release general de la rama `dev_2028_merge`, PrograX24 puede preparar un **hotfix puntual** sobre la rama `dev_2023_estable` que su instalación usa hoy. Este hotfix:

- Sería un cherry-pick del commit del fix una vez validado en MAMPA QA.
- Iría en una rama dedicada `dev_2023_estable_hotfix_danado` para no contaminar la línea base con otros cambios.
- Requeriría una ventana de mantenimiento acordada.

**Criterio de decisión**: lo recomendamos solamente si el costo operativo del bug en las próximas 4-8 semanas supera el costo logístico de aplicar un hotfix.

### 6.3 Reconciliación retroactiva de las 318,191 UM fantasma

El fix técnico **detiene el sangrado** pero no limpia el stock fantasma ya acumulado. Para reconciliar el inventario lógico contra el físico proponemos:

1. **Inventario físico priorizado** sobre los productos top afectados (lista de 30 productos = aproximadamente 47% del problema). PrograX24 entrega la lista priorizada.
2. **Recálculo del delta esperado** por producto comparando stock BD vs físico real.
3. **Aplicación de ajustes AJCANTN** automatizados por producto, con marca `Reconciliado_aj=1` para no contar dos veces.
4. **Inventario general anual** completo como red de seguridad final.

### 6.4 Política de inventarios cíclicos (recomendación de proceso)

Detectamos que su instalación tuvo solamente 6 movimientos de inventario formal en su historia, todos del 30-noviembre-2025. Recomendamos implementar una política de **conteo cíclico ABC**:

| Clasificación | Cobertura | Frecuencia |
|---|---|---|
| A (top 20% productos = 80% facturación) | conteo mensual | obligatorio |
| B (siguiente 30%) | conteo trimestral | obligatorio |
| C (resto) | conteo semestral | obligatorio |

Con un reporte de discrepancia obligatorio cada conteo: si el delta supera el 5%, escalación a supervisión.

---

## 7. Cronograma propuesto

| Fase | Responsable | Plazo estimado |
|---|---|---|
| Aplicación del fix en `dev_2028_merge` y validación QA | PrograX24 | 1 semana |
| Validación smoke en MAMPA PRD | PrograX24 | 1 semana |
| Decisión hotfix vs. esperar release general | Killios + PrograX24 | conjunta, semana 2 |
| Inventario físico priorizado (top 30 productos) | Killios | 2 semanas (paralelo) |
| Aplicación del fix en Killios PRD + script de reconciliación | PrograX24 con ventana acordada | 1 día (operativo) |
| Validación post-fix con queries de control | PrograX24 | 30 días de ventana |
| Cierre formal del caso CP-013 | conjunto | post-validación |

---

## 8. Compromisos de PrograX24

1. **Transparencia técnica completa**: la bitácora íntegra del análisis, las consultas SQL ejecutadas, las salidas crudas de cada wave de investigación y el plan de fix línea por línea están publicados en el repositorio interno `wms-brain` del proyecto y disponibles para revisión técnica de Killios cuando lo solicite.
2. **Cero modificación de su BD productiva durante el análisis**: todas las consultas se ejecutaron en modo solo-lectura, auditadas, reproducibles. Ningún `INSERT`, `UPDATE`, `DELETE`, `ALTER`, `DROP` ni `EXEC sp_*` se ejecutó en producción durante esta investigación.
3. **Auditabilidad total**: cada cifra de este informe está respaldada por un archivo de salida crudo archivado y reproducible.
4. **Validación post-fix con métrica objetiva**: PrograX24 entregará junto con el fix una query SQL que ejecutada sobre su BD confirma que el defecto ya no se reproduce (cero líneas nuevas con `dañado_picking=1` sin movimiento de descuento asociado, en ventana de 30 días post-fix).
5. **Comunicación periódica**: reporte semanal de avance hasta el cierre del caso.

---

## 9. Qué necesitamos de Killios

1. **Aprobación del plan de remediación** y selección entre las opciones del punto 6 (release general vs. hotfix anticipado).
2. **Coordinación de la ventana de mantenimiento** para la aplicación del fix en producción.
3. **Designación del equipo de inventario físico** que ejecutará el conteo priorizado.
4. **Punto de contacto técnico** del lado de Killios para validación de los casos golden y la reconciliación.

---

## 10. Anexos disponibles bajo solicitud

- Bitácora técnica completa del análisis (21 waves de investigación, archivada en repositorio).
- Diff exacto del fix de código (rama `dev_2028_merge`, línea por línea).
- Script de reconciliación retroactiva (versión draft).
- Lista priorizada de 500+ productos afectados con cantidades exactas.
- Queries SQL de validación post-fix.
- Trace de código profundo (`traza-002-danado-picking.md`, 21 KB) con análisis de los 10 archivos involucrados.

---

**Contacto técnico de PrograX24 para profundizar**: Erik Calderón — Director de Desarrollo TOMWMS.

*Informe generado el 2026-05-05 sobre la base del análisis técnico exhaustivo realizado entre el 23 de abril y el 5 de mayo de 2026 (21 waves de investigación archivadas en el repositorio interno `tomwms-replit-client-automate` rama `wms-brain`).*
