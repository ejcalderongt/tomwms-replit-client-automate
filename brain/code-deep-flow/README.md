# code-deep-flow — el embudo holistico → micro

> Wave 6+ del brain. Inicia 28-abr-2026 despues de cerrar el inventario
> de las 4 capas fundamentales (`heat-map-params/`).
>
> **Objetivo**: dado un parametro / capability del WMS, mapear como viaja
> end-to-end por todas las capas de codigo (BOF, WebService SOAP,
> WebAPI REST, HH Android, ERPs externos, DB) y que tablas / procesos
> afecta. Despues medir si hay **variantes por empresa** y registrarlas.

---

## 1. Por que este folder existe

Hasta wave 5 trabajamos sobre el **shape de los datos** (que cols
existen, que valores tienen por cliente). Eso responde "que es lo que
hay declarado". No responde "como se usa, donde se lee, que pasa cuando
se prende, en que cambia BOF vs HH".

El code-deep-flow cierra ese gap: cruza los hallazgos del heat-map con
el codigo real (TOMWMS_BOF en VB.NET + TOMHH2025 en Java/Android +
WMSWebAPI en .NET) y deja **trazas reproducibles** para usar de
referencia cuando construyamos el WebAPI .NET 10 nuevo.

---

## 2. Approach: macro → micro (embudo de Erik)

Erik confirmo el 28-abr-2026:

> WMS tiene los mismos campos / parametros para todas las empresas.
> Entonces partiendo del flujo general hay variantes por empresa? Eso
> es lo que necesitamos indexar.

Por eso cada traza arranca con la **vista holistica general** y baja
al detalle por empresa SOLO si el flujo varia.

### El embudo en pasos

1. **Mapa de cajas macro** (este folder, archivo `00-mapa-de-cajas.md`)
   — diagrama unico de toda la arquitectura. No depende de un parametro.
2. **Por cada parametro / capability**, una traza nueva
   `traza-NNN-<param>.md` con:
   - Hilo BOF (donde se lee, donde se escribe en el front office VB.NET)
   - Hilo WSHHRN / WMSWebAPI (SOAP legacy + REST nuevo)
   - Hilo HH (donde lo consume Android, si lo consume)
   - Hilo DB (que tablas se afectan, que SPs participan)
   - Hilo ERP externo (SAP B1 / NAV / Prefactura) si aplica
3. **Variantes por empresa** dentro de cada traza:
   - Comportamiento general primero.
   - Despues por cliente: BECO, K7, MAMPA, BYB, CEALSA.
   - Solo se documentan las divergencias reales (encontradas en codigo
     o en datos).

---

## 3. Estructura de cada `traza-NNN-<param>.md`

```
# Traza NNN — <PARAMETRO>

## 0. Resumen ejecutivo
- Que es el parametro
- Donde vive en DB (tabla.col)
- Estado real cross-cliente (1 linea por cliente)
- Hilos afectados (checkmark BOF / WSHHRN / WMSWebAPI / HH / ERP / DB)

## 1. Hilo BOF (TOMWMS_BOF, VB.NET)
- Forms / clases que leen el parametro
- Forms / clases que lo escriben (admin)
- Logica condicional (if param then ...)
- Archivos exactos con path Azure DevOps (rama dev_2028_merge)

## 2. Hilo WSHHRN (SOAP legacy, TOMHHWS.asmx.vb)
- Metodos del web service que consultan el parametro
- DataSets / DTOs involucrados
- Conexion al ERP externo si aplica

## 3. Hilo WMSWebAPI (REST nuevo, .NET)
- Controllers / Services / Models
- Endpoints HTTP que retornan / consumen el parametro
- Mapping_Profile (AutoMapper) si aplica

## 4. Hilo HH (TOMHH2025, Android Java)
- Activities (frm_*) que leen el parametro
- Llega via WebService.java (SOAP) o ApiService.java (REST)?
- Manager / Adapter que lo procesa
- Cambia el comportamiento de la UI?

## 5. Hilo DB
- Tablas tocadas (lectura, escritura)
- SPs involucrados
- Triggers / vistas relacionadas
- Constraints / indices relevantes

## 6. Hilo ERP externo (si aplica)
- Sync que lo lee (SAPBOSync*.exe / NavSync.exe / CEALSASync.exe)
- Tabla espejo en ERP destino
- Frecuencia / disparador

## 7. Variantes por empresa
- BECOFARMA: ...
- KILLIOS: ...
- MAMPA: ...
- BYB: ...
- CEALSA: ...
(Solo divergencias reales, no repetir el general)

## 8. Hipotesis Q-* abiertas
- ...

## 9. Lecturas para WebAPI .NET 10 nuevo
- ...
```

Las trazas **no** intentan documentar todo el flujo de cada capa — solo
lo que toca el parametro en cuestion. El mapa macro general queda en
`00-mapa-de-cajas.md`.

---

## 4. Convenciones

- Paths de archivos: siempre relativos al repo raiz, prefijados con
  `[BOF]` o `[HH]` para evitar ambigüedad. Ej:
  `[BOF] /TOMIMSV4/Transacciones/Bodega/frmBodega.vb` o
  `[HH] /app/src/main/java/com/dts/tom/frm_picking.java`.
- Branch siempre `dev_2028_merge` (la rama de trabajo del equipo). Si
  alguna vez se mira `master` se aclara explicitamente.
- Cuando un mismo archivo existe duplicado entre `/TOMIMSV4/...` y
  `/TOMIMSV4/TOMIMSV4/...`, marcar cual es el activo (suele ser el
  primero, ver `agent-context/AZURE_ACCESS.md` seccion 6).
- Las queries de DB para validar lo que dice el codigo se ejecutan
  solo READ-ONLY contra EC2 (`52.41.114.122,1437`). EC2 es copia
  parcial — la productiva real esta en la laptop de Erik.
- Los nombres de archivos `traza-NNN-*` van en orden de creacion, no
  en orden de importancia. Cuando una traza empuje cambios a otra,
  cross-link con `[ver traza-MMM]`.

---

## 5. Indice de trazas

(Se va llenando en orden de creacion.)

| # | Parametro | Capa origen | Estado |
|---|---|---|---|
| 00 | Mapa de cajas macro (no es traza) | — | aplicado |
| 01 | (pendiente eleccion Erik) | — | — |

---

## 6. Como elegir el siguiente parametro

Criterios (en orden):
1. **Cross-cutting**: el que tocaria mas hilos a la vez (mejor candidato
   para validar el metodo).
2. **Drift conocido**: donde ya sabemos por wave 1-5 que hay variantes
   reales por empresa (ej. `notificacion_voz`, `verificacion_consolidada`,
   `genera_lp`, `control_lote`).
3. **Bloquea decisiones del WebAPI nuevo**: si el comportamiento real es
   ambiguo, conocerlo nos desbloquea.
4. **Valor para Erik**: si Erik esta por refactorear o reescribir esa
   parte, prioridad alta.

---

## 7. Que NO va aca

- Documentacion de toda la arquitectura. Para eso esta el INDEX y los
  README por capa.
- Diagramas UML completos. Solo lo que sirve para una traza puntual.
- Comentarios sobre estilo de codigo / decisiones esteticas. Solo el
  flujo funcional.
