# CP-015 — Index del caso

> **Renumeracion**: este caso se creo originalmente como CP-014 en GitHub
> rama wms-brain (carpeta `CP-014-bug-danado-picking-transversal/`). Al traer
> al workspace local el 2026-04-30 se descubrio que CP-014 ya esta ocupado.
> El siguiente CP libre es CP-015. Mantener vinculo bidireccional con
> el CP-014 historico hasta consolidar la limpieza.

**Titulo**: Bug `dañado_picking` sin descuento de stock — TRANSVERSAL multi-cliente
**Severidad**: CRITICA
**Fecha de descubrimiento**: 2026-04-30
**Caso padre**: [CP-013-killios-wms164](../CP-013-killios-wms164/)
**Estado**: TRACE EN CODIGO COMPLETADO — fix encolado en CP-013/PLAYBOOK-FIX.md, no aplicado todavia

---

## Archivos en este caso

| Archivo | Descripcion |
|:---|:---|
| `INDEX.md` | Este archivo. |
| `REPORTE-MULTI-BD.md` | Reporte original: 4 BDs afectadas, cronologias, top productos, top usuarios. Heredado del CP-014 GitHub. |
| `DATOS-COMPARATIVOS.md` | Tablas comparativas crudas: estructuras, metricas BD por BD. Heredado del CP-014 GitHub. |

## Trace de codigo asociado

`brain/code-deep-flow/traza-002-danado-picking.md` — agregado el 2026-04-30:
- Ramas BOF comparadas: `dev_2023_estable` (Killios productivo) vs `dev_2028_merge` (MAMPA QA).
- 10 archivos VB.NET descargados, anotados, diff cuantificado.
- Mapa de setters / lectores / queries que filtran `dañado_picking=0`.
- **Hallazgo nuevo**: en `dev_2028_merge` alguien comento un setter de
  `Dañado_picking=True` en `clsLnStock_res_Partial.vb` lineas 1998-2008 —
  intento de fix parcial NO aplicado a la rama productiva 2023.
- 6 colas Q-* abiertas para cerrar el caso (Forms BOF, frm_ HH, constraint
  de cantidad, outlier BYB, git blame del fix parcial, default de migracion).

## Caso padre CP-013 (descargado parcialmente al workspace)

Archivos bajados desde GitHub:
- `../CP-013-killios-wms164/PLAYBOOK-FIX.md` — fix tecnico propuesto, vigente.
- `../CP-013-killios-wms164/INFORME-EJECUTIVO.md` — reporte ejecutivo a Carol.
- `../CP-013-killios-wms164/REPORTE.md` — reporte tecnico.

Archivos NO bajados (siguen solo en GitHub):
- EVIDENCIAS-CRONICIDAD-V2.md a V6.md
- REPORTE-CONCLUSION-V2.md, V3.md
- REPORTE-STOCK-EN-FECHA-V1.md
- REPORTE-wave-13-10.md, 13-11.md
- pedido-extraccion-hh-cest.md
- queries/, outputs/

Si se necesitan, bajar via API REST GitHub (ver `agent-context/GITHUB_SYNC.md`).

---

## TL;DR (heredado del CP-014, validado por la traza-002)

- **Bug del software, no de configuracion Killios.** Confirmado.
- **Confirmado en 4 de 7 BDs** comparables: Killios x2, BYB, MERCOPAN.
- **MERCOPAN es el caso mas grave en volumen**: 19.598 lineas / 574.155 UM en 29 meses.
- **BECOFARMA, CEALSA, MAMPA NO tienen el bug** porque NO usan la feature
  `dañado_picking` (cero registros con la flag en 1).
- **97-99 % desde el BOF VB.NET**, 1-21 % desde HH (BYB es el outlier con 21 %).
- **100 % sin AJCANTN asociado** — comportamiento deterministico.

## Hallazgos nuevos de la traza-002 (2026-04-30)

- En BD Killios productiva: **6.500 / 26.567 (24%) de filas con `dañado_picking=1`**.
- **CERO triggers, CERO SPs** que toquen el campo. Toda la logica esta en VB.NET.
- **Anomalia de schema**: `dañado_picking` (pos 41) tiene default NULL,
  mientras que `dañado_verificacion` (pos 27) tiene default ((0)). La
  migracion que agrego el campo no replico el default.
- **El bug es CONOCIDO por el equipo**: en `dev_2028_merge` hay un fix
  parcial (lineas comentadas en `clsLnStock_res_Partial.vb`). Pero:
  - El fix nunca se aplico a `dev_2023_estable` (donde corre Killios PRD).
  - El fix parcial es incompleto: solo bloquea un punto de marcaje, NO
    agrega la logica de descuento de stock requerida.
- **Linea historica**: `clsLnTrans_ubic_hh_enc_Partial.vb` linea 1394 en
  rama 2023 tiene comentario `'#AT 20220110 Se cambio del valor a true`
  - el bug arranca al menos en enero 2022.

## Proximas acciones encoladas

1. Cerrar los Q-* abiertos en la traza-002 seccion 7 (6 colas).
2. Aislar 5-10 casos HH para ver si el webservice deshace los movimientos
   (heredado del CP-014).
3. Confirmar con cliente Killios estado actual de MERCOPAN y BYB.
4. Ejecutar PLAYBOOK-FIX de CP-013 en QA Killios — atencion: aplicar a
   `dev_2023_estable`, no a `dev_2028_merge`.
5. Validar con replay de casos del ultimo mes Killios.

## Cross-refs

- Trace de codigo: `brain/code-deep-flow/traza-002-danado-picking.md`
- Caso padre: `brain/debuged-cases/CP-013-killios-wms164/`
- Convencion ramas/clientes: `brain/agent-context/RAMAS_Y_CLIENTES.md`
- Convencion numeracion: `brain/agent-context/NUMERACION.md`
- Como subir cambios: `brain/agent-context/GITHUB_SYNC.md`
