# CP-015 — [DEPRECATED 2026-04-30] Bug `dañado_picking` sin descuento de stock — TRANSVERSAL multi-cliente

> **DEPRECATED 2026-04-30**: este caso transversal fue absorbido por
> **`wms-known-issues/BUG-001-danado-picking-no-resta-inventario/`**
> segun la nueva taxonomia documentada en `agent-context/TAXONOMIA.md`.
>
> **Por que se deprecada**: este "caso" siempre fue una vista del bug
> del producto, no un caso de un cliente particular. Con la nueva
> taxonomia, los bugs del producto viven en `wms-known-issues/` con
> prefijo `BUG-NNN`, y los casos de cliente viven en `customer-open-cases/`
> o `customer-closed-cases/` con prefijo `CP-NNN`. Este folder no entra
> en ninguna de las dos categorias correctamente.
>
> **Donde esta cada cosa ahora**:
> - Descripcion del bug, severidad, mecanica → `wms-known-issues/BUG-001-.../INDEX.md`
> - Matriz de clientes afectados con cifras → `wms-known-issues/BUG-001-.../CLIENTES-AFECTADOS.md`
> - Listado de CPs y traces relacionados → `wms-known-issues/BUG-001-.../CASOS-RELACIONADOS.md`
> - Caso anchor del cliente Killios (PLAYBOOK-FIX) → `customer-open-cases/CP-013-killios-wms164/`
> - Trace de codigo profundo → `code-deep-flow/traza-002-danado-picking.md`
>
> **Que se mantiene aqui**: este archivo + `DATOS-COMPARATIVOS.md` +
> `REPORTE-MULTI-BD.md` quedan como **referencia historica del momento
> del descubrimiento del bug** (2026-04-30 turno previo). NO actualizar
> mas. Toda actualizacion va en BUG-001.

---

## Contenido historico (no actualizar mas)

> Lo que sigue es el INDEX original del CP-015 al momento de la
> deprecacion. Se conserva para no perder contexto historico.

---

**Renumeracion historica**: este caso se creo originalmente como CP-014 en
GitHub rama wms-brain (carpeta `CP-014-bug-danado-picking-transversal/`).
Al traer al workspace local el 2026-04-30 se descubrio que CP-014 ya esta
ocupado. El siguiente CP libre era CP-015. Despues, este turno (post
acuerdo con Erik sobre taxonomia tipo Microsoft), se absorbe en BUG-001.

**Titulo original**: Bug `dañado_picking` sin descuento de stock — TRANSVERSAL multi-cliente
**Severidad**: CRITICA
**Fecha de descubrimiento**: 2026-04-30
**Caso padre original**: CP-013-killios-wms164 (ahora `customer-open-cases/CP-013-killios-wms164/`)
**Estado al momento de la deprecacion**: TRACE EN CODIGO COMPLETADO — fix encolado en CP-013/PLAYBOOK-FIX.md, no aplicado todavia

### Archivos en este caso (no actualizar mas)

| Archivo | Descripcion | Estado post-deprecacion |
|:---|:---|:---|
| `INDEX.md` | Este archivo. | DEPRECATED, header al inicio. |
| `REPORTE-MULTI-BD.md` | Reporte original: 4 BDs afectadas, cronologias, top productos, top usuarios. | Datos absorbidos en `BUG-001/CLIENTES-AFECTADOS.md`. |
| `DATOS-COMPARATIVOS.md` | Tablas comparativas crudas: estructuras, metricas BD por BD. | Datos absorbidos en `BUG-001/CLIENTES-AFECTADOS.md`. |

### TL;DR original (heredado del CP-014, validado por la traza-002)

- **Bug del software, no de configuracion Killios.** Confirmado.
- **Confirmado en 4 de 7 BDs** comparables: Killios x2, BYB, MERCOPAN.
- **MERCOPAN es el caso mas grave en volumen**: 19,598 lineas / 574,155 UM en 29 meses.
- **BECOFARMA, CEALSA, MAMPA NO tienen el bug** porque NO usan la feature `dañado_picking`.
- **97-99 % desde el BOF VB.NET**, 1-21 % desde HH (BYB es el outlier con 21 %).
- **100 % sin AJCANTN asociado** — comportamiento deterministico.

### Hallazgos originales (heredados al BUG-001)

- En BD Killios productiva: **6,500 / 26,567 (24%) de filas con `dañado_picking=1`**.
- **CERO triggers, CERO SPs** que toquen el campo. Toda la logica esta en VB.NET.
- **Anomalia de schema**: `dañado_picking` (pos 41) tiene default NULL,
  mientras que `dañado_verificacion` (pos 27) tiene default ((0)).
- **El bug es CONOCIDO por el equipo**: en `dev_2028_merge` hay un fix
  parcial (lineas comentadas en `clsLnStock_res_Partial.vb`).
- **Linea historica**: `clsLnTrans_ubic_hh_enc_Partial.vb` linea 1394 en
  rama 2023 tiene comentario `'#AT 20220110 Se cambio del valor a true`
  - el bug arranca al menos en enero 2022.

---

## Cross-refs (al 2026-04-30, post-deprecacion)

- **Reemplazado por**: `wms-known-issues/BUG-001-danado-picking-no-resta-inventario/`
- **Caso anchor de cliente**: `customer-open-cases/CP-013-killios-wms164/`
- **Trace de codigo**: `code-deep-flow/traza-002-danado-picking.md`
- **Convencion taxonomica nueva**: `agent-context/TAXONOMIA.md`
- **Convencion numeracion**: `agent-context/NUMERACION.md`
