---
tipo: other
clientes: [killios]
---
# Correo gerencial — Pedro David España (Garesa.co)

**Caso**: CP-014 — Diferencia Kardex SAP vs Stock WMS Killios
**Solicitado por**: Pedro David España (Gerente Garesa.co), 2026-05-09 12:23
**Redactado por**: Erik José Calderón (Director Desarrollo TOMWMS, DTS)
**Estado del borrador**: para revisión de Erik antes de envío

---

## Cabecera sugerida

**Para**: Pedro David España <pedrodavid@garesa.co>
**CC**: Zulma Martínez <zulma@garesa.co>; Carolina Fuentes <carolinaf@dts.com.gt>; soportegt@dts.com.gt; Carlos España <carlos@killios.com>; Mario Santizo <mario@garesa.co>
**Asunto**: RE: Diferencia Kardex Guinda Mitades Bolsa — Informe gerencial de trazabilidad y seguimiento (caso CP-014)

---

## Cuerpo

Estimado Pedro,

Buenas tardes. Como me solicitás, te comparto el informe gerencial de
seguimiento sobre las diferencias entre el kardex de SAP y el stock
físico/lógico del WMS reportadas por Zulma y el equipo operativo de
Killios. El alcance cubre el reporte original (Maíz Dulce Miguels Galón
6/2900g, código WMS62) y la metodología que estamos aplicando, idéntica
a la que corresponde sobre Guinda Mitades Bolsa una vez que Zulma nos
confirme el delta cuantificado para ese SKU.

### 1. Resumen ejecutivo

- **Diferencia detectada**: el WMS reporta más unidades en stock que las
  que respalda el kardex contable de SAP. Para el caso testigo WMS62
  (Maíz Dulce Miguels Galón) la diferencia es **+10 cajas exactas** (60
  unidades de medida) sobre un total de 456,83 cajas reportadas en bodega.
- **Causa identificada**: la diferencia **no es un error de SAP**. Son
  unidades que el WMS dio de alta en su tabla de stock pero que **no
  generaron el movimiento contable correspondiente** que debe llegar a
  SAP. Las matrículas (pallets) involucradas existen físicamente en el
  WMS pero no tienen historial de entrada en la tabla de movimientos.
- **Trazabilidad**: identificadas y aisladas las **17 matrículas
  específicas** y las **27 líneas de stock** que componen la diferencia,
  con su ubicación física exacta dentro de la bodega para que el equipo
  operativo pueda validar antes de que se haga el ajuste.
- **Categorización**: el caso es una **variante** de un patrón de bug
  que DTS ya tiene catalogado e investigado a nivel código fuente
  (referencia interna BUG-001), confirmado en este momento sobre 3 de los
  clientes activos del WMS. Killios entra como segundo caso operativo
  documentado.
- **Próximo paso bloqueante**: validación física de las 27 líneas por
  parte del equipo de Zulma, en sitio, antes de que se ejecute el ajuste
  contable contra SAP.

### 2. Cronología del caso

| Fecha | Hito |
|---|---|
| 2026-05-08 | Zulma reporta la diferencia inicial al canal de soporte de DTS Guatemala. |
| 2026-05-09 (mañana) | DTS conecta read-only a la base productiva de Killios y ejecuta el diagnóstico forense sobre snapshot del 2026-05-09 10:23 UTC. |
| 2026-05-09 (mañana) | Confirmado el match exacto: stock WMS = 2.741 UM (456,83 cajas) vs kardex SAP cliente = 2.681 UM (446,83 cajas). Diferencia = +60 UM = +10 cajas. |
| 2026-05-09 (mañana) | Identificadas las 17 matrículas vivas sin contraparte en la tabla de movimientos. Cruce contra el log histórico = 0 coincidencias. |
| 2026-05-09 (mediodía) | Generado el informe técnico para Carolina (4 hipótesis priorizadas) y la traza línea por línea para validación operativa por parte de Killios. |
| 2026-05-09 (tarde) | Pedro solicita informe gerencial. Este documento. |

### 3. Diagnóstico técnico, en lenguaje gerencial

El WMS de Killios mantiene dos tablas que deben estar siempre cuadradas:

- Una tabla de **stock vivo**: qué hay físicamente en cada ubicación de
  la bodega ahora mismo.
- Una tabla de **movimientos**: el historial de cómo llegó cada unidad
  ahí (recepción, traslado, picking, ajuste, etc.). Lo que se reporta a
  SAP sale exclusivamente de esta tabla.

Cuando ambas están cuadradas, el kardex de SAP refleja exactamente lo
que el WMS dice tener. Cuando se desincronizan, el WMS y SAP discrepan
aunque la bodega física esté correcta.

En este caso, el equipo de DTS detectó que **17 matrículas que el WMS
reporta como vivas en bodega no tienen ningún registro en la tabla de
movimientos**. Es decir, aparecen físicamente en el sistema pero nunca
se contabilizaron. Eso explica la diferencia exacta de +10 cajas con SAP.

La causa raíz, sobre la cual el equipo de desarrollo de DTS ya está
trabajando a nivel código, está asociada a dos operativas que están
permitidas por configuración del producto pero que en su flujo actual
**no escriben en la tabla de movimientos**:

1. **Reemplazo de pallet durante picking** (operativa autorizada vía
   handheld con clave supervisoria).
2. **Reclasificación manual de estado de producto** (cambio de "Buen
   estado" a "Reempacar" desde el módulo de back-office).

Ambas acciones son legítimas desde el punto de vista operativo, pero
en su implementación actual quedan fuera de la auditoría contable que
viaja a SAP. Esto se confirmó cruzando el catálogo interno de tipos de
tarea del producto, donde estas dos operaciones figuran explícitamente
con la marca "no contabilizar".

### 4. Cómo se ha trabajado el caso

DTS aplicó el siguiente protocolo, que es el estándar interno para esta
familia de incidentes:

1. **Aislamiento del producto**: filtrado del SKU reportado por el cliente
   sobre la base productiva, en modo solo lectura, sin modificar nada.
2. **Cuantificación del delta**: comparación numérica directa contra el
   kardex de SAP que aportó Zulma.
3. **Identificación de matrículas sospechosas**: separación de las que
   tienen historial contable de las que no lo tienen.
4. **Cruce contra log histórico**: validación de que las matrículas
   sospechosas tampoco aparecieron en el pasado bajo otra forma.
5. **Categorización del caso** dentro del catálogo interno de DTS,
   contra el bug raíz BUG-001 que ya tiene investigación previa.
6. **Generación de documentación segregada por audiencia**:
   - una hoja con las 27 líneas físicas para que el equipo operativo de
     Killios valide en sitio antes del ajuste,
   - un informe técnico interno para el equipo de desarrollo, con las 4
     hipótesis de causa raíz ordenadas por probabilidad,
   - un playbook de remediación que cubre tanto el ajuste contable
     inmediato como la corrección definitiva en el código fuente.
7. **Vínculo al bug raíz**: el caso quedó registrado dentro del
   repositorio interno de conocimiento técnico de DTS como variante
   formal del patrón BUG-001, junto con los otros casos previos.

### 5. Próximos pasos y compromisos

| # | Acción | Responsable | Fecha objetivo |
|---|---|---|---|
| 1 | Validación física, en sitio, de las 27 líneas de stock detectadas (especialmente las 8 marcadas como "Reempacar" o "Mal estado"). | Zulma + equipo operativo Killios | 48 h hábiles desde recibido este informe |
| 2 | Aplicación del ajuste contable acordado, registrado en SAP con la trazabilidad documental del caso. | DTS + Killios, con evidencia escrita previa | una vez completado el paso 1 |
| 3 | Análisis del SKU "Guinda Mitades Bolsa" con la misma metodología, en cuanto Zulma confirme el delta cuantificado. | DTS Soporte | 24 h hábiles desde la confirmación |
| 4 | Plan de remediación definitivo del bug raíz a nivel de código fuente del WMS, alcanzando todas las variantes detectadas. Incluye la corrección dentro del calendario de releases del producto. | DTS Desarrollo | a coordinar con Carolina y dirección comercial |

DTS asume el compromiso de que **ninguna de las matrículas identificadas
se modificará automáticamente** en el WMS sin la validación física previa
del equipo operativo. Todo ajuste será trazable, documentado y
contraseñado con evidencia escrita por ambas partes.

### 6. Documentación de respaldo

La evidencia y la trazabilidad completa del caso quedaron versionadas en
el repositorio interno de conocimiento técnico de DTS. Los enlaces a
continuación son internos de DTS; cualquier extracto puede compartirse
bajo solicitud expresa.

- Índice del caso CP-014:
  https://github.com/ejcalderongt/tomwms-replit-client-automate/blob/wms-brain/wms-brain/brain/customer-open-cases/CP-014-killios-wms62-maiz-galon/INDEX.md
- Traza forense con las 27 líneas físicas:
  https://github.com/ejcalderongt/tomwms-replit-client-automate/blob/wms-brain/wms-brain/brain/customer-open-cases/CP-014-killios-wms62-maiz-galon/traza-001-stock-fantasma.md
- Informe operativo para Killios (Zulma):
  https://github.com/ejcalderongt/tomwms-replit-client-automate/blob/wms-brain/wms-brain/brain/customer-open-cases/CP-014-killios-wms62-maiz-galon/INFORME-CLIENTE-KILLIOS.md
- Informe técnico interno DTS (Carolina) — 4 hipótesis priorizadas:
  https://github.com/ejcalderongt/tomwms-replit-client-automate/blob/wms-brain/wms-brain/brain/customer-open-cases/CP-014-killios-wms62-maiz-galon/INFORME-CAROLINA.md
- Playbook de remediación del bug raíz:
  https://github.com/ejcalderongt/tomwms-replit-client-automate/blob/wms-brain/wms-brain/brain/customer-open-cases/CP-014-killios-wms62-maiz-galon/PLAYBOOK-FIX.md
- Catálogo del bug raíz BUG-001 (clientes afectados y variantes):
  https://github.com/ejcalderongt/tomwms-replit-client-automate/blob/wms-brain/wms-brain/brain/wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md

Cualquier consulta o necesidad de aclaración adicional, quedo a
disposición tuya, de Carlos y del equipo de Killios. Carolina y soporte
Guatemala están en copia para asegurar continuidad.

Saludos cordiales,

Erik José Calderón
Director de Desarrollo — TOMWMS
DTS
ejcalderon@dts.com.gt

---

## Notas internas para Erik (NO enviar)

1. **Aclaración Maíz Galón vs Guinda Mitades Bolsa**: el asunto del
   correo de Pedro menciona "Guinda Mitades Bolsa" pero todo el
   forense documentado en CP-014 es sobre WMS62 (Maíz Dulce Miguels
   Galón). El cuerpo asume que CP-014 es el caso testigo y deja Guinda
   Mitades Bolsa como item #3 del plan de próximos pasos. Si Zulma ya
   te pasó el delta de Guinda y querés que lo incluya cuantificado en
   este mismo informe, decime y lo agrego.
2. **Acceso a los links de GitHub**: los enlaces apuntan a un repo
   privado de la organización ejcalderongt. Pedro y Garesa **NO** tienen
   acceso. Opciones:
   (a) dejar los links como referencia interna, como está ahora,
   (b) adjuntar los 5 archivos como PDF al correo,
   (c) abrir un repositorio público o un compartido temporal.
   Mi recomendación es (b) — armo los PDFs si me decís que sí.
3. **Tono**: gerencial-cliente, neutro, sin admitir culpa explícita pero
   sin esquivar el hecho de que el bug está en el WMS y DTS lo va a
   arreglar. Si querés que lo suavice más (más diplomático) o que sea
   más directo (más asertivo), decime y reviso.
4. **El compromiso de fechas en el cuadro de próximos pasos** lo dejé
   abierto ("a coordinar con Carolina y dirección comercial") para el
   item #4 (plan de release del fix). Si tenés ya un compromiso de
   release, reemplazalo.
5. (Resuelto en esta versión) Verificado el link a `INFORME-CAROLINA.md`.
